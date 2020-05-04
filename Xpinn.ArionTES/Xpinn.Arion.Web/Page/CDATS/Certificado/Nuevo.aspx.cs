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
using System.IO;


partial class Nuevo : GlobalWeb
{

    AperturaCDATService AperturaService = new AperturaCDATService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AperturaService.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AperturaService.CodigoProgramaCertificacion, "E");
            else
                VisualizarOpciones(AperturaService.CodigoProgramaCertificacion, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarImprimir(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Session["DatosDetalle"] = null;
            if (Session["RETURNO"] == null)
                Session["RETURNO"] = "";
            if (!Page.IsPostBack)
            {
                Site toolBar = (Site)this.Master;
                mvPrincipal.Visible = true;
                mvReporte.Visible = false;
                mvPrincipal.ActiveViewIndex = 0;
                txtCodigo.Enabled = false;
                txtFecha.ToDateTime = DateTime.Now;

                ObtenerDatos(idObjeto); 
  
                
                
               
                cargarDropdown();
                toolBar.MostrarImprimir(true);
                if (Session[AperturaService.CodigoProgramaCertificacion + ".id"] != null)
                {
                    idObjeto = Session[AperturaService.CodigoProgramaCertificacion + ".id"].ToString();
                    Session.Remove(AperturaService.CodigoProgramaCertificacion + ".id");
                    ObtenerDatos(idObjeto);                    
                    lblMsj.Text = " modificada ";
                   
                    toolBar.MostrarImprimir(true);
                    if (Session["ADMI"].ToString() == "REIMPRIMIR")
                    {
                        btnDatos.Visible = false;
                        muestraInformeReporte();
                    }
                }
                else
                {
                    txtFechaApertura.Text = DateTime.Today.ToShortDateString();
                    lblMsj.Text = " grabada ";
                    Usuario vUsu = (Usuario)Session["usuario"];
                       


                }                
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "Page_Load", ex);
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
        pentidad.descripcion = "  ";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }




    void cargarDropdown()
    { 
        Cdat Data = new Cdat();
        List<Cdat> lstTipoLinea = new List<Cdat>();

        lstTipoLinea = AperturaService.ListarTipoLineaCDAT(Data,(Usuario)Session["usuario"]);
        if (lstTipoLinea.Count > 0)
        {
            ddlTipoLinea.DataSource = lstTipoLinea;
            ddlTipoLinea.DataTextField = "NOMBRE";
            ddlTipoLinea.DataValueField = "COD_LINEACDAT";
            ddlTipoLinea.Items.Insert(0, new ListItem("  ", "0"));
            ddlTipoLinea.SelectedIndex = 0;
          
        }

 
        //PoblarLista("TIPOTASA", ddlFormaCaptacion); //PRUEBA
       
        PoblarLista("Tipomoneda", ddlTipoMoneda);

        ddlTipoCalendario.Items.Insert(0, new ListItem("  ", "0"));
        ddlTipoCalendario.Items.Insert(1, new ListItem("Comercial", "1"));
        ddlTipoCalendario.Items.Insert(2, new ListItem("Calendario", "2"));
        ddlTipoCalendario.SelectedIndex = 0;
      

        //PoblarLista("TIPOTASA", ddlDestinacion); //PRUEBA
      
        
        List<Cdat> lstAsesores = new List<Cdat>();
       


        List<Cdat> lstOficina = new List<Cdat>();


        lstOficina = AperturaService.ListarOficinas(Data, (Usuario)Session["usuario"]);
        if (lstOficina.Count > 0)
        {
            ddlOficina.DataSource = lstOficina;
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "COD_OFICINA";
            ddlOficina.Items.Insert(0, new ListItem("  ","0"));
            ddlOficina.SelectedIndex = 0;
         
        }

        PoblarLista("Periodicidad", ddlPeriodicidad);
      

    }



    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        ObtenerListaDetalle();

        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();

        if (Session["DatosDetalle"] != null)
        {
            lstDetalle = (List<Detalle_CDAT>)Session["DatosDetalle"];

            for (int i = 1; i <= 1; i++)
            {
                Detalle_CDAT eApert = new Detalle_CDAT();
                eApert.cod_persona = null;
                eApert.principal = null;
                eApert.conjuncion = "";
                lstDetalle.Add(eApert);
            }
            gvDetalle.PageIndex = gvDetalle.PageCount;
            gvDetalle.DataSource = lstDetalle;
            gvDetalle.DataBind();

            Session["DatosDetalle"] = lstDetalle;
        }
        else
        {
            InicializarDetalle();
        }
    }


    protected void InicializarDetalle()
    {
        List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
        for (int i = gvDetalle.Rows.Count; i < 3; i++)
        {
            Detalle_CDAT eApert = new Detalle_CDAT();
            eApert.cod_persona = null;
            eApert.principal = null;
            eApert.conjuncion = "";
            lstDetalle.Add(eApert);
        }
        gvDetalle.DataSource = lstDetalle;
        gvDetalle.DataBind();
        Session["DatosDetalle"] = lstDetalle;
    }





    //Eventos Grilla

    protected void gvDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlConjuncion = (DropDownListGrid)e.Row.FindControl("ddlConjuncion");
            if (ddlConjuncion != null)
            {
                ddlConjuncion.Items.Insert(0, new ListItem(" ", "0"));
                ddlConjuncion.Items.Insert(1, new ListItem("Y", "Y"));
                ddlConjuncion.Items.Insert(2, new ListItem("O", "O"));
            }

            Label lblConjuncion = (Label)e.Row.FindControl("lblConjuncion");
            if (lblConjuncion != null)
                ddlConjuncion.SelectedValue = lblConjuncion.Text;

            
               

            BusquedaRapida ctlListadoPersona = (BusquedaRapida)e.Row.FindControl("ctlListadoPersona");
            if (ctlListadoPersona != null)
                ctlListadoPersona.eventotxtIdentificacion_TextChanged += txtIdentificacion_TextChanged;
        }
    }


    protected void gvDetalle_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetalle.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDetalle();

        List<Detalle_CDAT> LstDeta;
        LstDeta = (List<Detalle_CDAT>)Session["DatosDetalle"];

        if (conseID > 0)
        {
            try
            {
                foreach (Detalle_CDAT Deta in LstDeta)
                {
                   
                        AperturaService.EliminarTitularCdat(conseID, (Usuario)Session["usuario"]);
                        LstDeta.Remove(Deta);
                        break;
                    
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDeta.RemoveAt((gvDetalle.PageIndex * gvDetalle.PageSize) + e.RowIndex);
        }

        gvDetalle.DataSourceID = null;
        gvDetalle.DataBind();

        gvDetalle.DataSource = LstDeta;
        gvDetalle.DataBind();

        Session["DatosDetalle"] = LstDeta;
    }




    protected void btnListadoPersona_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPersona = (ButtonGrid)sender;
        if (btnListadoPersona != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPersona.CommandArgument);
            BusquedaRapida ctlListadoPer = (BusquedaRapida)gvDetalle.Rows[rowIndex].FindControl("ctlListadoPersona");
            ctlListadoPer.Motrar(true, "lblcod_persona", "txtIdentificacion", "", "lblNombre", "lblApellidos");            
        }
    }



    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

        TextBox lblcod_persona = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblcod_persona");
        TextBox lblNombre = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblNombre");
        TextBox lblApellidos = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblApellidos");
        TextBox lblCiudad = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblCiudad");
        TextBox lblDireccion = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lblDireccion");
        TextBox lbltelefono = (TextBox)gvDetalle.Rows[rowIndex].FindControl("lbltelefono");

        Detalle_CDAT DataPersona = new Detalle_CDAT();
        DataPersona.identificacion = txtIdentificacion.Text;
        DataPersona = AperturaService.ConsultarPersona(DataPersona, (Usuario)Session["usuario"]);

        if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
        {
            if (DataPersona.cod_persona != 0 && DataPersona.cod_persona != null)
                lblcod_persona.Text = DataPersona.cod_persona.ToString();

            if (DataPersona.nombres != "" && DataPersona.nombres != null)
                lblNombre.Text = DataPersona.nombres;

            if (DataPersona.apellidos != "" && DataPersona.apellidos != null)
                lblApellidos.Text = DataPersona.apellidos;

            if (DataPersona.ciudad != "" && DataPersona.ciudad != null)
                lblCiudad.Text = DataPersona.ciudad;

            if (DataPersona.direccion != "" && DataPersona.direccion != null)
                lblDireccion.Text = DataPersona.direccion;

            if (DataPersona.telefono != "" && DataPersona.telefono != null)
                lbltelefono.Text = DataPersona.telefono;
        }
        else 
        {
            lblcod_persona.Text = ""; lblNombre.Text = ""; lblApellidos.Text = "";
            lblCiudad.Text = ""; lblDireccion.Text = ""; lbltelefono.Text = "";
        }
    }

    protected void chkPrincipal_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkPrincipal = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkPrincipal.CommandArgument);

        if (chkPrincipal != null)
        {
            foreach (GridViewRow rFila in gvDetalle.Rows)
            {
                CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("chkPrincipal");
                check.Checked = false;
                if (rFila.RowIndex == rowIndex)
                {
                    check.Checked = true;
                }
            }
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
                eDeta.codigo_cdat = Convert.ToInt64(lblcodigo.Text);

            Label lblidentificacion = (Label)rfila.FindControl("lblidentificacion");
            if (lblidentificacion != null)
                eDeta.identificacion = Convert.ToString(lblidentificacion.Text);

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

           
        
             

            lista.Add(eDeta);
            Session["DatosDetalle"] = lista;

            if (eDeta.cod_persona != 0 && eDeta.cod_persona != null)
            {
                lstDetalle.Add(eDeta);
                Session["DTAPERTURA"] = lstDetalle; // CAPTURA DATOS PARA IMPRESION
            }
        }

        return lstDetalle;
    }


    protected void ddlPeriodicidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Cdat Data = new Cdat();

        Data = AperturaService.ConsultarDiasPeriodicidad(Convert.ToInt32(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);

        if (Data.numdias != 0)
            txtDiasValida.Text = Data.numdias.ToString();
    }



    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Cdat vApe = new Cdat();
            
            vApe = AperturaService.ConsultarApertura((Usuario)Session["usuario"]);

            if (vApe.codigo_cdat != 0) txtCodigo.Text = vApe.codigo_cdat.ToString();

            if (vApe.numero_cdat != "") txtNumCDAT.Text = vApe.numero_cdat;

            Session["nroCDAT"] = vApe.numero_cdat;

  

            if (vApe.cod_oficina != 0) ddlOficina.SelectedValue = vApe.cod_oficina.ToString();

            if (vApe.cod_lineacdat != "") ddlTipoLinea.SelectedValue = vApe.cod_lineacdat;


            if (vApe.fecha_apertura != DateTime.MinValue) txtFechaApertura.Text = vApe.fecha_apertura.ToShortDateString();




            if (vApe.plazo != 0) txtPlazo.Text = vApe.plazo.ToString();

            if (vApe.tipo_calendario != 0) ddlTipoCalendario.SelectedValue = vApe.tipo_calendario.ToString();

            if (vApe.valor != 0) txtValor.Text = vApe.valor.ToString();

            if (vApe.cod_moneda != 0) ddlTipoMoneda.SelectedValue = vApe.cod_moneda.ToString();

  
            //fecha vencimiento no lo recupero
            if (vApe.fecha_vencimiento != DateTime.MinValue) txtFecha.Text = vApe.fecha_vencimiento.ToShortDateString();


           

            if (vApe.cod_periodicidad_int != 0)
                ddlPeriodicidad.SelectedValue = vApe.cod_periodicidad_int.ToString();
           
            if (vApe.modalidad_int != 0)
                rblModalidadInt.SelectedValue = vApe.modalidad_int.ToString();

         

            /*Datos que no recupero
             * Tasa nominal,TASA_EFECTIVA,INTERESES_CAP,RETENCION_CAP,FECHA_INTERESES,ESTADO
             */



            //RECUPERAR GRILLA DETALLE 
            List<Detalle_CDAT> lstDetalle = new List<Detalle_CDAT>();
            
            lstDetalle = AperturaService.ListarDetalle((Usuario)Session["usuario"]);

            if (lstDetalle.Count > 0)
            {
                gvDetalle.DataSource = lstDetalle;
                gvDetalle.DataBind();
            }
            else
            {
                InicializarDetalle();                
            }
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "ObtenerDatos", ex);
        }
    }


    
    Boolean ValidarDatos()
    {
        if (txtNumCDAT.Visible == true)
        {
            if (txtNumCDAT.Text == "")
            {
                VerError("Ingrese el numero de CDAT");
                return false;
            }
        }

        if (txtFechaApertura.Text == "")
        {
            VerError("Ingrese la Fecha de Apertura");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la Fecha de Vencimiento");
            return false;
        }
           
        if (txtValor.Text == "0")
        {
            VerError("Ingrese el Valor");
            return false;
        }
        if (ddlTipoMoneda.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Moneda");
            return false;
        }
            if (txtPlazo.Text == "")
        {
            VerError("Ingrese el Plazo correspondiente");
            return false;
        }
        if (ddlTipoCalendario.SelectedIndex == 0)
        {
            VerError("Seleccione el Tipo de Calendario");
            return false;
        }        
       
        if (ddlOficina.SelectedIndex == 0)
        {
            VerError("Seleccione la Oficina perteneciente al Asesor");
            return false;
        }
                
        
        if (ddlPeriodicidad.SelectedIndex == 0)
        {
            VerError("Seleccione la Periodicidad correspondiente");
            return false;
        }

        List<Detalle_CDAT> LstDetalle = new List<Detalle_CDAT>();
        LstDetalle = ObtenerListaDetalle();

        if (LstDetalle.Count == 0)
        {
            VerError("Debe Ingresar un Titular Principal");
            return false;
        }
        int cont = 0;
        
            if (LstDetalle.Count > 1)
            {
                VerError("Solo debe ingresar un Titular para la Modalidad INDIVIDUAL");
                return false;
            }

            foreach (Detalle_CDAT deta in LstDetalle)
            {
                if (deta.principal == 1)
                {
                    cont++;
                }
            }
            if (cont != 1)
            {
                VerError("Debe selecciona un titular principal");
                return false;
            }
        
        else 
        {
            if (LstDetalle.Count > 0)
            {
                foreach (Detalle_CDAT deta in LstDetalle)
                {
                    if (deta.principal == 1)
                    {
                        cont++;
                    }
                }
                if (cont != 1)
                {
                    VerError("Debe selecciona un titular principal");
                    return false;
                }
            }
        }

        int rango1, rango2, plazo;
        rango1 = Convert.ToInt32(txtDiasValida.Text) - 5;
        rango2 = Convert.ToInt32(txtDiasValida.Text) + 5;
        plazo = Convert.ToInt32(txtPlazo.Text);
        if (plazo < rango1 && plazo > rango2)
        {
            VerError("El Plazo ingresado no es valido para la Periodicidad Seleccionada");
            return false;
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
                string msj;
                msj = idObjeto != "" ? "Modificar" : "Grabar";
                ctlMensaje.MostrarMensaje("Desea "+msj+" los datos ingresados?");
            }
        }       
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Cdat vApert = new Cdat();

            if (idObjeto != "")
                vApert.codigo_cdat = Convert.ToInt64(txtCodigo.Text);
            else
                vApert.codigo_cdat = 0;

            //CONSULTA DE GENERACION NUMERICA CDAT
            Cdat Data = new Cdat();
            Data = AperturaService.ConsultarNumeracionCDATS(Data, (Usuario)Session["usuario"]);

            if (Data.valor == 1)
            {
                vApert.opcion = 1; //AUTOGENERE
            }
            else
            {
                vApert.opcion = 0;//NO AUTOGENERE                
            }

            if (txtNumCDAT.Visible == true)
                vApert.numero_cdat = txtNumCDAT.Text;
            else
                vApert.numero_cdat = null;
      

            vApert.cod_oficina = Convert.ToInt32(ddlOficina.SelectedValue);

            if (ddlTipoLinea.SelectedIndex != 0)
                vApert.cod_lineacdat = ddlTipoLinea.SelectedValue;
            else
                vApert.cod_lineacdat = null;

         
            vApert.fecha_apertura = Convert.ToDateTime(txtFechaApertura.Text);
           

          
            vApert.plazo = Convert.ToInt32(txtPlazo.Text);
            vApert.tipo_calendario = Convert.ToInt32(ddlTipoCalendario.SelectedValue);
            vApert.valor = Convert.ToDecimal(txtValor.Text);
            vApert.cod_moneda = Convert.ToInt32(ddlTipoMoneda.SelectedValue);
      

            vApert.fecha_vencimiento = Convert.ToDateTime(txtFecha.Text);
      

           
            vApert.cod_periodicidad_int = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            if (rblModalidadInt.SelectedItem != null)
                vApert.modalidad_int = Convert.ToInt32(rblModalidadInt.SelectedValue);
            else
                vApert.modalidad_int = 0;

            
             
            //VALORES NULOS
            vApert.tasa_nominal = 0;
            vApert.tasa_efectiva = 0;
            vApert.intereses_cap = 0;
            vApert.retencion_cap = 0;
            vApert.fecha_intereses = DateTime.MinValue;

            vApert.estado = 1; //por defecto
     

            vApert.lstDetalle = new List<Detalle_CDAT>();
            vApert.lstDetalle = ObtenerListaDetalle();

            List<Beneficiario> lstBeneficiariosCdat = new List<Beneficiario>();



            if (idObjeto != "")
                AperturaService.ModificarAperturaCDAT(vApert, (Usuario)Session["usuario"], lstBeneficiariosCdat);
            else
                AperturaService.CrearAperturaCDAT(vApert, (Usuario)Session["usuario"],lstBeneficiariosCdat);

            Session["nroCDAT"] = vApert.numero_cdat.ToString();

            //GRABAR AUDITORIA

            CDAT_AUDITORIA Audi = new CDAT_AUDITORIA();
            Usuario vUsu = (Usuario)Session["usuario"];

            Audi.cod_auditoria_cdat = 0;
            Audi.codigo_cdat = vApert.codigo_cdat;
            Audi.tipo_registro_aud = 1;
            Audi.fecha_aud = DateTime.Now;
            Audi.cod_usuario_aud = vUsu.codusuario;
            Audi.ip_aud = vUsu.IP;

            if (idObjeto == "")
                AperturaService.CrearAuditoriaCdat(Audi, (Usuario)Session["usuario"]);//Crear

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarImprimir(false);

            mvPrincipal.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AperturaService.CodigoProgramaCertificacion, "btnContinuarMen_Click", ex);
        }    
    }



    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session["RETURNO"].ToString() == "CERTIFICADO")
            Response.Redirect("~/Page/CDATS/Certificado/Lista.aspx");
        else
            Navegar(Pagina.Lista);
    }


    //IMPRIMIR

    void muestraInformeReporte()
    {
        VerError("");
        ObtenerListaDetalle();
        if (Session["DTAPERTURA"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            mvPrincipal.Visible = false;
            mvReporte.Visible = true;
            mvReporte.ActiveViewIndex = 0;

            List<Detalle_CDAT> lstConsulta = new List<Detalle_CDAT>();
            lstConsulta = (List<Detalle_CDAT>)Session["DTAPERTURA"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("identificacion");
            table.Columns.Add("cod_persona");
            table.Columns.Add("nombres");
            table.Columns.Add("apellidos");
            table.Columns.Add("ciudad");
            table.Columns.Add("direccion");
            table.Columns.Add("telefono");
            table.Columns.Add("principal");

            foreach (Detalle_CDAT item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.identificacion;
                datarw[1] = item.cod_persona;
                datarw[2] = item.nombres;
                datarw[3] = item.apellidos;
                datarw[4] = item.ciudad;
                datarw[5] = item.direccion;
                datarw[6] = item.telefono;
                if (item.principal == 1)
                    datarw[7] = "*";
                else
                    datarw[7] = null;
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[17];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            param[2] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[3] = new ReportParameter("Usuario", pUsuario.nombre);
            param[4] = new ReportParameter("NroCdat", Session["nroCDAT"].ToString());
            param[5] = new ReportParameter("fecha_aper", txtFechaApertura.Text);
            param[6] = new ReportParameter("tipoLinea", ddlTipoLinea.SelectedItem.Text);
            param[7] = new ReportParameter("modalidad", ddlOficina.SelectedItem.Text);
            param[8] = new ReportParameter("Valor", txtValor.Text);
            param[9] = new ReportParameter("Moneda", ddlTipoMoneda.SelectedItem.Text);
            param[10] = new ReportParameter("Plazo", txtPlazo.Text);
            param[11] = new ReportParameter("TipoCalendario", ddlTipoCalendario.SelectedItem.Text);
            param[12] = new ReportParameter("Destinacion", ddlPeriodicidad.SelectedItem.Text);
            param[13] = new ReportParameter("Asesor", ddlTipoLinea.SelectedItem.Text);
            param[14] = new ReportParameter("OficinaAsesor", ddlOficina.SelectedItem.Text);
            param[15] = new ReportParameter("NumPreImpreso", txtNumCDAT.Text);
            param[16] = new ReportParameter("ImagenReport", ImagenReporte());

            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();
        }
        frmPrint.Visible = false;
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        muestraInformeReporte();
    }


    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (rvReporte.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rvReporte.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
            frmPrint.Visible = true;
            rvReporte.Visible = false;

        }
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {

        mvPrincipal.Visible = true;
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);
        toolBar.MostrarGuardar(true);

        mvPrincipal.ActiveViewIndex = 0;
        mvReporte.Visible = false;        
    }
}