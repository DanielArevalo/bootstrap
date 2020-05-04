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


partial class Nuevo : GlobalWeb
{

    AperturaCDATService AperturaService = new AperturaCDATService();
    AnulacionCDATService AnulaService = new AnulacionCDATService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AnulaService.CodigoProgramaANU + ".id"] != null)
                VisualizarOpciones(AnulaService.CodigoProgramaANU, "E");
            else
                VisualizarOpciones(AnulaService.CodigoProgramaANU, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaANU, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["DatosDetalle"] = null;
            PanelBloqueo.Enabled = false;
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
               
                cargarDropdown();
                if (Session[AnulaService.CodigoProgramaANU + ".id"] != null)
                {
                    idObjeto = Session[AnulaService.CodigoProgramaANU + ".id"].ToString();
                    Session.Remove(AnulaService.CodigoProgramaANU + ".id");
                    ObtenerDatos(idObjeto);
                }               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaANU, "Page_Load", ex);
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




    void cargarDropdown()
    {
        ctlTasaInteres.Inicializar();

        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data,(Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoLinea.SelectedIndex = 0;
            ddlTipoLinea.DataBind();
        }

        ddlModalidad.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlModalidad.Items.Insert(1, new ListItem("INDIVIDUAL", "IND"));
        ddlModalidad.Items.Insert(2, new ListItem("CONJUNTA", "CON"));
        ddlModalidad.Items.Insert(3, new ListItem("ALTERNA", "ALT"));
        ddlModalidad.SelectedIndex = 0;
        ddlModalidad.DataBind();
        
        PoblarLista("Tipomoneda", ddlTipoMoneda);

        ddlTipoCalendario.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
        ddlTipoCalendario.DataBind();
                

        List<Cdat> lstOficina = new List<Cdat>();

        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new ListItem("Seleccione un item","0"));
            ddlOficina.SelectedIndex = 0;
            ddlOficina.DataBind();
        }


    }


    

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlModalidad.SelectedItem.Text == "CONJUNTA")
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

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }


            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

        }
    }

    protected List<Detalle_CDAT> ObtenerListaDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        List<Detalle_CDAT> lista = new List<Detalle_CDAT>();

        foreach (GridViewRow rfila in gvDetalle.Rows)
        {
            Detalle_CDAT eDeta = new Detalle_CDAT();
            
            Label lblcodigo = (Label)rfila.FindControl("lblcodigo");
            if (lblcodigo != null)
                eDeta.cod_usuario_cdat = Convert.ToInt64(lblcodigo.Text);

            TextBoxGrid txtIdentificacion = (TextBoxGrid)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eDeta.identificacion = txtIdentificacion.Text;

            TextBox lblcod_persona = (TextBox)rfila.FindControl("lblcod_persona");
            if (lblcod_persona.Text != "")
                eDeta.cod_persona = Convert.ToInt64(lblcod_persona.Text);

            TextBox lblNombre = (TextBox)rfila.FindControl("lblNombre");
            if (lblNombre.Text != "")
                eDeta.nombres = lblNombre.Text;

            TextBox lblApellidos = (TextBox)rfila.FindControl("lblApellidos");
            if (lblApellidos.Text != "")
                eDeta.apellidos = lblApellidos.Text;

            TextBox lblCiudad = (TextBox)rfila.FindControl("lblCiudad");
            if (lblCiudad.Text != "")
                eDeta.ciudad = lblCiudad.Text;

            TextBox lblDireccion = (TextBox)rfila.FindControl("lblDireccion");
            if (lblDireccion.Text != "")
                eDeta.direccion = lblDireccion.Text;

            TextBox lbltelefono = (TextBox)rfila.FindControl("lbltelefono");
            if (lbltelefono.Text != "")
                eDeta.telefono = lbltelefono.Text;

            CheckBoxGrid chkPrincipal = (CheckBoxGrid)rfila.FindControl("chkPrincipal");
            if (chkPrincipal.Checked)
                eDeta.principal = 1;
            else
                eDeta.principal = 0;

            if (ddlModalidad.SelectedItem.Text == "CONJUNTA")
            {
                DropDownListGrid ddlConjuncion = (DropDownListGrid)rfila.FindControl("ddlConjuncion");
                if (ddlConjuncion.SelectedIndex != 0)
                    eDeta.conjuncion = ddlConjuncion.SelectedValue;
                else
                    eDeta.conjuncion = null;
            }
            else
                eDeta.conjuncion = null;

            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null  && eDeta.nombres.Trim() != null && eDeta.apellidos.Trim() != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Cdat vApe = new Cdat();
            
            vApe = AperturaService.ConsultarApertu(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "") txtNumCDAT.Text = vApe.numero_cdat;
            
            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();
            if (vApe.fecha_vencimiento != DateTime.MinValue) txtFechaVenci.Text = vApe.fecha_vencimiento.ToShortDateString();

            if (vApe.cod_lineacdat != "") ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();

            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.modalidad != "" && vApe.modalidad != null) ddlModalidad.SelectedValue = vApe.modalidad;


            if (vApe.tipo_interes != null)
            {
                if (!string.IsNullOrEmpty(vApe.tipo_interes.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vApe.tipo_interes.ToString().Trim());
                if (!string.IsNullOrEmpty(vApe.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.tipo_historico.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.desviacion.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.cod_tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vApe.cod_tipo_tasa.ToString().Trim()));
                if (!string.IsNullOrEmpty(vApe.tasa_interes.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vApe.tasa_interes.ToString().Trim()));
            }

            Session["COD_PERIODICIDAD"] = 0;
            if (vApe.cod_periodicidad_int != 0)
                Session["COD_PERIODICIDAD"] = vApe.cod_periodicidad_int;

            Session["FECHA_INICIO"] = DateTime.MinValue;
            if (vApe.fecha_inicio != DateTime.MinValue)
                Session["FECHA_INICIO"] = vApe.fecha_inicio;


            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
            
            lstDetalle = AperturaService.ListarDetalleTitulares(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
            }
            
            ddlModalidad_SelectedIndexChanged(ddlModalidad, null);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaANU, "ObtenerDatos", ex);
        }
    }


    
    Boolean ValidarDatos()
    {
       
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarDatos())
            {
                ctlMensaje.MostrarMensaje("Desea generar la Anulación?");
            }
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaANU, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Cdat vAnula = new Cdat();

            if (idObjeto != "")
                vAnula.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
            else
                vAnula.codigo_cdat = 0;

            vAnula.estado = 4;
            if (txtObservacion.Text != "")
                vAnula.observacion = txtObservacion.Text;
            else
                vAnula.observacion = null;

            AnulaService.ModificarCDATAnulacion(vAnula, (Usuario)Session["usuario"]);

            //GRABACION AUDITORIA

            CDAT_AUDITORIA Audi = new CDAT_AUDITORIA();
            Usuario vUsu = (Usuario)Session["usuario"];
            Audi.cod_auditoria_cdat = 0;
            Audi.codigo_cdat = vAnula.codigo_cdat;
            Audi.tipo_registro_aud = 5;
            Audi.fecha_aud = DateTime.Now;
            Audi.cod_usuario_aud = vUsu.codusuario;
            Audi.ip_aud = vUsu.IP;

            AperturaService.CrearAuditoriaCdat(Audi, (Usuario)Session["usuario"]);//Crear

            Site toolbar = (Site)Master;
            toolbar.MostrarGuardar(false);
            toolbar.MostrarExportar(false);
            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AnulaService.CodigoProgramaANU, "btnContinuarMen_Click", ex);
        }    
    }



    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }



}