using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.CDATS.Services;
using Xpinn.CDATS.Entities;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Globalization;

partial class Nuevo : GlobalWeb
{

    AperturaCDATService AperturaService = new AperturaCDATService();
    ProrrogaCDATService ProrrService = new ProrrogaCDATService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ProrrService.CodigoProgramasim + ".id"] != null)
                VisualizarOpciones(ProrrService.CodigoProgramasim, "E");
            else
                VisualizarOpciones(ProrrService.CodigoProgramasim, "A");

            Site toolBar = (Site)this.Master;
           // toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramasim, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            txtFechaApertura.ToDateTime = DateTime.Now;
            Session["DatosDetalle"] = null;
            PanelBloqueo.Enabled = true;
            if (!Page.IsPostBack)
            {
                Session["LineaCdat"] = null;
                mvPrincipal.ActiveViewIndex = 0;
                txtValor.Enabled = true;
              
                cargarDropdown();
                ctlTasa.Inicializar();
                ddlModalidad.Inicializar();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramasim, "Page_Load", ex);
        }
    }


    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }




    private void cargarDropdown()
    {       
        PoblarLista("Tipomoneda", ddlTipoMoneda);
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        LineaCDAT LineaCDAT = new LineaCDAT();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data, (Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }
    }


    protected void InicializarDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        for (int i = gvDetalle.Rows.Count; i < 3; i++)
        {
            Detalle_CDAT eApert = new Detalle_CDAT();
            eApert.cod_usuario_cdat = -1;
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }


    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModalidad.Text == "CONJUNTA")
        {
            for (int i = 0; i < gvDetalle.Rows.Count; i++) 
            {
                gvDetalle.Columns[10].Visible = true;
            }
        }
        else
        {
            for (int i = 0; i < gvDetalle.Rows.Count; i++)
            {
                gvDetalle.Columns[10].Visible = false;
            }
        }
    }



    //Eventos Grilla

    
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        if (!ValidarDatos())
        {
            return;
        }
        VerError("");
        Actualizar();

    }

    private void Actualizar()
    {
        try
        {
            List<Cdat> lstConsulta = new List<Cdat>();
            DateTime FechaApe;
            Cdat vapertura = new Cdat();
            FechaApe = txtFechaApertura.ToDateTime == null ? DateTime.MinValue : txtFechaApertura.ToDateTime;
            vapertura.valor = Convert.ToDecimal(txtValor.Text);
            vapertura.cod_moneda = Convert.ToInt32(ddlTipoMoneda.SelectedIndex);
            vapertura.plazo = Convert.ToInt32(txtPlazo.Text);
            vapertura.tipo_interes = ctlTasa.FormaTasa;
            vapertura.cod_tipo_tasa = ctlTasa.TipoTasa;
            vapertura.tasa_interes = ctlTasa.Tasa;
            vapertura.tipo_historico = ctlTasa.TipoHistorico;
            vapertura.desviacion = ctlTasa.Desviacion;
            vapertura.cod_periodicidad_int = ddlModalidad.cod_periodicidad == 0 ? null : ddlModalidad.cod_periodicidad;
            vapertura.capitalizar_int = Convert.ToInt32(chkCapitalizaInt.Checked);
            vapertura.cobra_retencion = Convert.ToInt32(chkCobraReten.Checked);

            lstConsulta = AperturaService.Listarsimulacion(vapertura,FechaApe, (Usuario)Session["usuario"]);

            gvDetalle.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvDetalle.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                var totalInteres = (from t in lstConsulta select t.intereses_cap).Sum();
                var totalRetencion = (from r in lstConsulta select r.retencion_cap).Sum();
                var totalNeto = (from n in lstConsulta select n.valor).Sum();
                var totalcapitaliza = (from n in lstConsulta select n.capitalizar_int).Sum();

                gvDetalle.DataSource = lstConsulta;
                gvDetalle.DataBind();
                Panelgrilla.Visible = true;

                txtInteres.Visible = true;
                txtTotalRetencion.Visible = true;
                txtCapitaliza.Visible = true;
                txtNeto.Visible = true;
                lblInteres.Visible = true;
                lblRetencion.Visible = true;
                lblCapitaliza.Visible = true;
                lblNeto.Visible = true;


                txtInteres.Text = totalInteres.ToString("##,##0", CultureInfo.InvariantCulture);
                txtTotalRetencion.Text = totalRetencion.ToString("##,##0", CultureInfo.InvariantCulture);
                txtCapitaliza.Text = totalcapitaliza.ToString("##,##0", CultureInfo.InvariantCulture);
                
                var valorcdat = Convert.ToDecimal(txtValor.Text);
                var total = valorcdat + totalNeto;
             //   valorpagar = totalNeto + valorcdat;
               // txtNeto.Text = total.ToString();
               
                txtNeto.Text = total.ToString("##,##0", CultureInfo.InvariantCulture);



                lblMsj.Visible = true;
                lblMsj.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                valor();
               
            }
            else
            {
                Panelgrilla.Visible = false;
                lblMsj.Visible = false;
            }

            Session.Add(AperturaService.CodigoProgramaCertificacion + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "Actualizar", ex);
        }
    }
    protected void gvDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetalle.PageIndex = e.NewPageIndex;
        Actualizar();
    }


    private void valor()
    {
        decimal valorcdat;

        valorcdat = Convert.ToDecimal(txtValor.Text);
    }


    private Boolean ValidarDatos()
    {

        if (txtFechaApertura.Text == "")
        {
            VerError("Ingrese la Fecha de Prorroga");
            return false;
        }
        if (txtValor.Text == "")
        {
            VerError("Ingrese el Nuevo Plazo");
            return false;
        }
        if (Session["LineaCdat"] == null)
        {
            VerError("Debe seleccionar la linea de Cdat");
            return false;
        }
        else
        {
            LineaCDAT lineaCdat = new LineaCDAT();
            lineaCdat = (LineaCDAT)Session["LineaCdat"];
            if(lineaCdat.lstRangos.Where(x => x.tipo_tope != 2 && x.minimo != null && x.maximo != null).ToList().Count>0)
            {
                if (txtPlazo.Text =="" )
                {
                    VerError("Debe digitar los dias del plazo");
                    return false;
                }
                RangoCDAT rango = new RangoCDAT();
                rango = (from RangoCDAT y in lineaCdat.lstRangos
                         where y.tipo_tope == 2
                         select new RangoCDAT { cod_lineacdat= y.cod_lineacdat,
                                                cod_rango = y.cod_rango,
                                                minimo = y.minimo,
                                                maximo = y.maximo}).ToList()[0];
                if (Convert.ToInt64(txtPlazo.Text) < Convert.ToInt64(rango.minimo) || Convert.ToInt64(txtPlazo.Text) > Convert.ToInt64(rango.maximo) )
                {
                    VerError("Digite el valor de los dias que sea mayor de "+ rango.minimo + " y menor que "+ rango.maximo + "");
                    return false;
                }
            }
        }


        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea generar la prorroga?");
            }
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(ProrrService.CodigoProgramaPRO, "btnGuardar_Click", ex);
        }
    }

    

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
       // Navegar(Pagina.Lista);
    }
    protected void ddlTipoLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Generar Consulta de la Linea Seleccionada
        ctlTasa.Inicializar();
        Session["LineaCdat"] = null;
        LineaCDAT vLineaCdat = new LineaCDAT();
        LineaCDATService lineacdatServicio = new LineaCDATService();
        if (this.ddlTipoLinea.SelectedValue == null || ddlTipoLinea.SelectedValue == "" || this.ddlTipoLinea.SelectedValue == "0")
        {
        }
        else
        {
            vLineaCdat = lineacdatServicio.ConsultarLineaCDAT(Convert.ToString(ddlTipoLinea.SelectedValue), (Usuario)Session["usuario"]);
            Session["LineaCdat"] = vLineaCdat;
            if (vLineaCdat.tipo_calendario != null)
            {
                txtPlazo.Visible = true;
            }
            else
            {
                txtPlazo.Visible = false;
            }

         

            if (vLineaCdat.calculo_tasa != null)
            {
                //ctlTasaInteres.Inicializar();
                if (!string.IsNullOrEmpty(vLineaCdat.calculo_tasa.ToString()))
                    ctlTasa.FormaTasa = HttpUtility.HtmlDecode(vLineaCdat.calculo_tasa.ToString().Trim());
                if (!string.IsNullOrEmpty(vLineaCdat.tipo_historico.ToString()))
                    ctlTasa.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.desviacion.ToString()))
                    ctlTasa.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.cod_tipo_tasa.ToString()))
                    ctlTasa.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaCdat.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vLineaCdat.tasa.ToString()))
                    ctlTasa.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.tasa.ToString().Trim()));
            }

            if (vLineaCdat.tasa_simulacion == 1)
            {

                ctlTasa.Enabled_Campos(true);
                panelTasa.Enabled = true;
            }
            if (vLineaCdat.tasa_simulacion == 0)
            {

                ctlTasa.Enabled_Campos(false);
                panelTasa.Enabled = false;
            }
        }
    }


    protected void txtInteres_TextChanged(object sender, EventArgs e)
    {

    }
}