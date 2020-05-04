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
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlTiemposServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();    

    String HojaRuta = "";
    List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar

    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService(); // Permite iniciar la consulta del historial (Segundo GridView)
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
    Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
    Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
    Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
    string vAsesor = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ControlTiemposServicio.CodigoProgramaReporte, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            ViewState["NumeroCredito"] = "0";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaReporte, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
  
        //txtFechaActual.Text = DateTime.Now.ToString();
        try
        {
            if (!IsPostBack)
            {
                CargarListas();
                LlenarComboOficinas(ddlOficina);
                CargarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaReporte);
                if (Session[ControlTiemposServicio.CodigoProgramaReporte + ".consulta"] != null)
                {
                    Actualizar();
                    
                }
                
                if (Request.UrlReferrer != null)
                    if (Request.UrlReferrer.Segments[4].ToString() == "CreditosPorAprobar/")
                    {
                      
                      //  mvControlTiempos.ActiveViewIndex = 1;
                      
                    }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaReporte, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            //ListaSolicitada = "Oficinas";
            //TraerResultadosLista();
            //ddlOficina.DataSource = lstDatosSolicitud;
            //ddlOficina.DataTextField = "ListaDescripcion";
            //ddlOficina.DataValueField = "ListaId";
            //ddlOficina.DataBind();
            //ddlOficina.Items.Add("");
            //ddlOficina.Text = "";

            //ListaSolicitada = "Encargado";
            //TraerResultadosLista();
            //DdlResponsable.DataSource = lstDatosSolicitud;
            //DdlResponsable.DataTextField = "ListaDescripcion";
            //DdlResponsable.DataValueField = "ListaId";
            //DdlResponsable.DataBind();
            //DdlResponsable.Items.Add("");
            //DdlResponsable.Text = "";

            ListaSolicitada = "EstadoProcesoReporte";
            TraerResultadosLista();
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();
            //ddlEstado.Items.Add("");
            //ddlEstado.Text = "";

            
         
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlTiemposServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaReporte);
        Navegar(Pagina.Nuevo);
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaReporte);
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Int64 vNumeroCredito = Convert.ToInt64(gvLista.SelectedRow.Cells[4].Text);
        //String vCedula = gvLista.SelectedRow.Cells[7].Text;
        //vAsesor = gvLista.SelectedRow.Cells[10].Text;   
        //ViewState["NumeroCredito"] = vNumeroCredito.ToString();


        String id = gvLista.SelectedRow.Cells[3].Text;
        String identificacion = gvLista.SelectedRow.Cells[6].Text;
        String proceso = gvLista.SelectedRow.Cells[4].Text;
        String FechaDatacredito = gvLista.SelectedRow.Cells[9].Text;
        Session[ControlTiemposServicio.CodigoProgramaReporte + ".id"] = id;
    
        //HyperLink HyperConsulta = (HyperLink)gvLista.FooterRow.FindControl("HyperConsulta");
        //ImageButton btnurl = (ImageButton)gvLista.FooterRow.FindControl("btnurl");

        //HyperConsulta.Enabled = false;
       
        Session["Numeroidentificacion"] = identificacion;
        Session["NumeroCredito"] = id;
        Session["Proceso"] = proceso;
        Session["Datacredito"] = FechaDatacredito;

        Session[ControlTiemposServicio.CodigoProgramaReporte + ".id"] = id;
        Navegar(Pagina.Editar);
       
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
       
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaReporte, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstConsulta = ControlTiemposServicio.ListarReporteMensajeria(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            Session["DTHOJARUTAREPORTE"] = lstConsulta;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                // ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ControlTiemposServicio.CodigoProgramaReporte + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaReporte, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ControlTiempos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ControlTiempos vControlTiempos = new Xpinn.FabricaCreditos.Entities.ControlTiempos();
       // String Fecha = txtFechaIni.Text.ToString();
       // String format = "MM/dd/yyyy";
      //  DateTime fecha_Proceso;
        //Si el usuario es administrativo (Si en la tabla Usuario_atribuciones: TipoAtribucion =0 Activo=1) => puede consultar todos los creditos

        Usuario pUsuario = (Usuario)Session["usuario"];
        vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(pUsuario.codusuario, (Usuario)Session["usuario"]);

        //if (vUsuarioAtribuciones.tipoatribucion == 0 && vUsuarioAtribuciones.activo == 1) //Consulta todos los creditos
        //{
            if (ddlOficina.Text.Trim() != "")
                vControlTiempos.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
      //  }
        else  // Consulta solo los creditos de la oficina del usuario logueado
        {
            vControlTiempos.cod_oficina = pUsuario.cod_oficina;
        }

        //if (DdlResponsable.Text.Trim() != "")
        //    vControlTiempos.encargado = Convert.ToString(DdlResponsable.SelectedItem.Text);

        if (ddlEstado.Text.Trim() != "")
            vControlTiempos.nom_proceso = Convert.ToString(ddlEstado.SelectedItem.Text);

        if (txtFechaProceso.Text.Trim() != "")
            vControlTiempos.fechaproceso += Convert.ToString(txtFechaProceso.Text.Trim());
     
        return vControlTiempos;
    }


    protected void btnConsultar_Click1(object sender, ImageClickEventArgs e)
    {
       
    }

   


    private Xpinn.FabricaCreditos.Entities.ControlCreditos ObtenerValoresHistorico()
    {
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(ViewState["NumeroCredito"].ToString());
        return vControlCreditos;
    }

  
   

   



    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        mvControlTiempos.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaReporte);
        Actualizar();
       
    }

  

    protected void gvLista_DataBound(object sender, EventArgs e)
    {


    }

    protected void gvLista_DataBinding(object sender, EventArgs e)
    {

    }

    protected void btnConsultar0_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/CreditosPorAprobar/Detalle.aspx");
    }


    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTHOJARUTAREPORTE"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstConsulta = (List<Xpinn.FabricaCreditos.Entities.ControlTiempos>)Session["DTHOJARUTAREPORTE"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Credito");
            table.Columns.Add("Cedula");
            table.Columns.Add("NombreCliente");
            table.Columns.Add("Ejecutivo");
            table.Columns.Add("Oficina");

            foreach (Xpinn.FabricaCreditos.Entities.ControlTiempos item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.numerocredito;
                datarw[1] = item.identificacion;
                datarw[2] = item.nombredeudor;
                datarw[3] = item.asesor;
                datarw[4] = item.nom_oficina;
                table.Rows.Add(datarw);
            }
            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[6];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);
            if (ddlEstado.SelectedValue == "6")
            {
                param[2] = new ReportParameter("Reporte", "Envio Carpetas Mensajero");

            }
            if (ddlEstado.SelectedValue == "12")
            {
                param[2] = new ReportParameter("Reporte", "Devolver Carpetas a Oficinas");

            }
            param[3] = new ReportParameter("Usuario", pUsuario.nombre);
            param[4] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[5] = new ReportParameter("ImagenReport", ImagenReporte());

            rvReporteMensajeria.LocalReport.EnableExternalImages = true;
            rvReporteMensajeria.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet2", table);
            rvReporteMensajeria.LocalReport.DataSources.Clear();
            rvReporteMensajeria.LocalReport.DataSources.Add(rds);
            rvReporteMensajeria.LocalReport.Refresh();


            // Mostrar el reporte en pantalla.
            mvControlTiempos.ActiveViewIndex = 1;
        }
    }

    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficina"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficina)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            //ddlOficina.Enabled = true;
            //ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);

            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "codigo";
            ddlOficina.DataBind();
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

            //ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
        }
        else
        {
            if (consulta == 0)

                LlenarComboOficinasAsesores(ddlOficina);
        }
    }

    protected void LlenarComboOficinasAsesores(DropDownList ddlOficina)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficinas = new Xpinn.FabricaCreditos.Entities.Oficina();
        oficinas.Codigo = Convert.ToInt32(usuap.cod_oficina);
        ddlOficina.DataSource = oficinaService.ListarOficinasUsuarios(oficinas, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        

    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        

        mvControlTiempos.ActiveViewIndex = 0;
    }
    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }
}