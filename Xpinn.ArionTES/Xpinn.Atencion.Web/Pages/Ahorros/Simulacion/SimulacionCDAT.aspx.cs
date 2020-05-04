using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Ahorros_Simulacion_SimulacionCDAT : GlobalWeb
{
    List<xpinnWSEstadoCuenta.ListaDesplegable> lstLineas = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSDeposito.WSDepositoSoapClient DepositoService = new xpinnWSDeposito.WSDepositoSoapClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.SimulacionCDAT, "Sim");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SimulaciónCrédito", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime hoy = DateTime.Now;
            txtFecha.Text = hoy.ToString("dd/MM/yyyy");
            CargarDropDownYCheckBox();
        }
    }
    protected void CargarDropDownYCheckBox()
    {
        lstLineas = EstadoServicio.PoblarListaDesplegable("periodicidad", "", " ", "", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlPeriodicidad, lstLineas);

        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("lineacdat", "", " ESTADO = 1 ", "1", Session["sec"].ToString());
        if (lstLineas.Count > 0)
        {
            LlenarDrop(ddlLineaCDAT, lstLineas);
        }
        lstLineas = EstadoServicio.PoblarListaDesplegable("tipomoneda", "", " ", "", Session["sec"].ToString());
    }

        void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlDropCarga.DataBind();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (!ValidarDatos())
        {
            return;
        }
        VerError("");
        Actualizar();
    }
    private Boolean ValidarDatos()
    {

        if (txtFecha.Text == "")
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
            
            xpinnWSDeposito.LineaCDAT lineaCdat = new xpinnWSDeposito.LineaCDAT();
            lineaCdat = (xpinnWSDeposito.LineaCDAT)Session["LineaCdat"];
            if (lineaCdat.lstRangos.Where(x => x.tipo_tope != 2 && x.minimo != null && x.maximo != null).ToList().Count > 0)
            {
                if (ddlCPlazo.SelectedValue == "0")
                {
                    VerError("Debe seleccionar el plazo");
                    return false;
                }

                xpinnWSDeposito.RangoCDAT rango = new xpinnWSDeposito.RangoCDAT();
                rango = (from xpinnWSDeposito.RangoCDAT y in lineaCdat.lstRangos
                         where y.tipo_tope == 2
                         select new xpinnWSDeposito.RangoCDAT
                         {
                             cod_lineacdat = y.cod_lineacdat,
                             cod_rango = y.cod_rango,
                             minimo = y.minimo,
                             maximo = y.maximo
                         }).ToList()[0];
                if ((Convert.ToInt64(ddlCPlazo.SelectedValue)*30+1) < Convert.ToInt64(rango.minimo) || (Convert.ToInt64(ddlCPlazo.SelectedValue)*30) > Convert.ToInt64(rango.maximo))
                {
                    VerError("Seleccione un plazovalido");
                    return false;
                }
                rango = (from xpinnWSDeposito.RangoCDAT valores in lineaCdat.lstRangos
                         where valores.tipo_tope == 1
                         select valores).ToList()[0];
                if (Convert.ToDecimal(txtValor.Text.Replace(".", "").Replace(",", "").Replace("$", "")) > Convert.ToDecimal(rango.maximo))
                {
                    VerError("El valor máximo para esta línea de CDAT es " + rango.maximo + "");
                    return false;
                }
            }
        }


        return true;
    }

    private void Actualizar()
    {
        try
        {
            List<xpinnWSDeposito.Cdat> lstConsulta = new List<xpinnWSDeposito.Cdat>();
            DateTime FechaApe;
            xpinnWSDeposito.Cdat vapertura = new xpinnWSDeposito.Cdat();
            FechaApe = txtFecha.ToDateTime == null ? DateTime.MinValue : DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            vapertura.valor = Convert.ToDecimal(txtValor.Text.Replace(".", "").Replace(",", "").Replace("$", ""));
            vapertura.cod_moneda = 1;
            vapertura.plazo = Convert.ToInt32(ddlCPlazo.SelectedValue) * 30;
            vapertura.tipo_interes = ctlTasa.FormaTasa;
            vapertura.cod_tipo_tasa = ctlTasa.TipoTasa;
            vapertura.tasa_interes = ctlTasa.Tasa;
            vapertura.tipo_historico = ctlTasa.TipoHistorico;
            vapertura.desviacion = ctlTasa.Desviacion;
            vapertura.cod_periodicidad_int = null;
            if (ddlPeriodicidad.SelectedValue == "0")
                vapertura.cod_periodicidad_int = null;
            else
                vapertura.cod_periodicidad_int =  Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            vapertura.capitalizar_int = Convert.ToInt32(chkCapitalizaInt.Checked);
            vapertura.cobra_retencion = 1;
            vapertura.retencion = true.ToString();

            lstConsulta = DepositoService.Listarsimulacion(vapertura, FechaApe, Session["sec"].ToString());

            gvDetalle.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvDetalle.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvDetalle.DataSource = lstConsulta;
                gvDetalle.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError("Se presentó un error: "+ex.Message); 
        }
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        VerError("");
       
        gvDetalle.DataSource = null;
        gvDetalle.DataBind();
        txtValor.Text = "";
        ddlCPlazo.SelectedValue = "0";
        ctlTasa.Tasa = 0;
        ddlLineaCDAT.SelectedValue = "0";
        ddlPeriodicidad.SelectedValue = "0";
    }
    protected void ddlTipoLinea_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Generar Consulta de la Linea Seleccionada
        ctlTasa.Inicializar();
        Session["LineaCdat"] = null;
        xpinnWSDeposito.LineaCDAT vLineaCdat = new xpinnWSDeposito.LineaCDAT();
     
        if (this.ddlLineaCDAT.SelectedValue == null || ddlLineaCDAT.SelectedValue == "" || this.ddlLineaCDAT.SelectedValue == "0")
        {
        }
        else
        {
            vLineaCdat = DepositoService.ConsultarLineaCDAT(Convert.ToString(ddlLineaCDAT.SelectedValue), Session["sec"].ToString());          
            
            Session["LineaCdat"] = vLineaCdat;
            if (vLineaCdat.tipo_calendario != null)
            {
                ddlCPlazo.Visible = true;
            }
            else
            {
                ddlCPlazo.Visible = false;
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
                {
                    ctlTasa.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaCdat.tasa.ToString().Trim()));
                    decimal tasa = Convert.ToDecimal(vLineaCdat.tasa.ToString()) / 100;
                    double efectiva = ((Math.Pow(Convert.ToDouble(1 + (tasa / 12)), 12) - 1) * 100) / 100;
                    txtCTasaEA.Text = efectiva.ToString("P");
                    txtCTasa.Text = tasa.ToString("P");
                }
            }
            xpinnWSDeposito.RangoCDAT rango = new xpinnWSDeposito.RangoCDAT();
            rango = (from xpinnWSDeposito.RangoCDAT valores in vLineaCdat.lstRangos
                     where valores.tipo_tope == 2
                     select valores).ToList()[0];
            int minimo = 0;
            if ((Convert.ToInt32(rango.minimo) % 30) != 0)
            {
                minimo = (Convert.ToInt32(rango.minimo) / 30) + 1;
            }
            else
            {
                minimo = (Convert.ToInt32(rango.minimo) / 30);
            }
            int maximo = Convert.ToInt32(rango.maximo) / 30;
            ddlCPlazo.Items.Clear();
            ListItem vacio = new ListItem("seleccione un item", "0");
            ddlCPlazo.Items.Add(vacio);
            for (int i = minimo; i <= maximo; i++)
            {
                if (i > 0)
                {
                    ListItem item = new ListItem(i.ToString() + " meses", i.ToString());
                    ddlCPlazo.Items.Add(item);
                }
            }

        }

        
    }

}
   