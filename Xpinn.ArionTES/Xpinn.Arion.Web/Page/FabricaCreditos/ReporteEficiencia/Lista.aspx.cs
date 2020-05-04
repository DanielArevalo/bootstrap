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
using System.Text;
using System.Xml;
using System.Design;
using System.Threading;
using System.Globalization;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
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
            VisualizarOpciones(ControlTiemposServicio.CodigoProgramaRpEficiencia, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ViewState["NumeroCredito"] = "0";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaRpEficiencia, "Page_PreInit", ex);
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
                LlenarComboOficinas();

                EjecutivoService serviceEjecutivo = new EjecutivoService();
                Ejecutivo ejec = new Ejecutivo();           
                Usuario usuap = (Usuario)Session["usuario"];
                OficinaService oficinaService = new OficinaService();
               
                int cod = Convert.ToInt32(usuap.codusuario);
                int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
                if (consulta >= 1) // Trae Todos los asesores
                {
                    DdlEjecutivo.DataSource = serviceEjecutivo.ListartodosAsesoresRuta((Usuario)Session["usuario"]);
                   
               
                    DdlEjecutivo.DataTextField = "NOMBRECOMPLETO";
                    DdlEjecutivo.DataValueField = "ICODIGO";
                    DdlEjecutivo.DataBind();
                    DdlEjecutivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
      
                }
                else
                {
                    Int64 oficina = usuap.cod_oficina;
                    LlenarComboAsesores(oficina);
                }
                CargarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaRpEficiencia);
                //if (Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".consulta"] != null)
               // {
                  //  Actualizar();
                    
               // }                
               
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaRpEficiencia, "Page_Load", ex);
        }
    }

    protected void LlenarComboOficinas()
    {
        OficinaService oficinaService = new OficinaService();

        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);

        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinasUsuarios(oficina, (Usuario)Session["usuario"]);

        if (consulta >= 1) // Trae Todos las oficinas
        {
            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);

        }
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
      
    }
   

    private void CargarListas()
    {
        try
        {

            //ListaSolicitada = "Asesor";
            //TraerResultadosLista();
            //DdlEjecutivo.DataSource = lstDatosSolicitud;
            //DdlEjecutivo.DataTextField = "ListaDescripcion";
            //DdlEjecutivo.DataValueField = "ListaId";
            //DdlEjecutivo.DataBind();
            //DdlEjecutivo.Items.Add("");
            //DdlEjecutivo.Text = "";

            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();
            ddlEstado.Items.Add("");
            ddlEstado.Text = "";

            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlEstado2.DataSource = lstDatosSolicitud;
            ddlEstado2.DataTextField = "ListaDescripcion";
            ddlEstado2.DataValueField = "ListaId";
            ddlEstado2.DataBind();
            ddlEstado2.Items.Add("");
            ddlEstado2.Text = "";

            
         
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
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaRpEficiencia);
        Navegar(Pagina.Nuevo);
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
     
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaRpEficiencia);
      //  txtIdenificacion.Text= "";
        DdlEjecutivo.SelectedIndex=0;
        ddlEstado.SelectedIndex = 0;

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
        String Estado = gvLista.SelectedRow.Cells[13].Text;
      

        Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".id"] = id;
    
        //HyperLink HyperConsulta = (HyperLink)gvLista.FooterRow.FindControl("HyperConsulta");
        //ImageButton btnurl = (ImageButton)gvLista.FooterRow.FindControl("btnurl");

        //HyperConsulta.Enabled = false;
       
        Session["Numeroidentificacion"] = identificacion;
        Session["NumeroCredito"] = id;
        Session["Proceso"] = proceso;
        Session["Datacredito"] = FechaDatacredito;
        Session["Estado"] = Estado;

        Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".id"] = id;
        Navegar(Pagina.Editar);
       
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {

        String id = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[6].Text;
        String proceso = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        String FechaDatacredito = gvLista.Rows[e.NewEditIndex].Cells[9].Text;
        String Estado = gvLista.Rows[e.NewEditIndex].Cells[13].Text;   

        Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".id"] = id;

      
        HyperLink HyperConsulta = (HyperLink)gvLista.Rows[e.NewEditIndex].FindControl("HyperConsulta");
        ImageButton btnurl = (ImageButton)gvLista.Rows[e.NewEditIndex].FindControl("btnurl");
       // HyperConsulta.Target = "_blank";  
        HyperConsulta.Enabled = false;
       
            if (HyperConsulta != null)
            {
                if (HyperConsulta.Visible == true)
                {
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Aprobado" && gvLista.Rows[e.NewEditIndex].Cells[13].Text == "Aprobado")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                        Session[creditoServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;


                    }

                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Solicitado" || gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Referencias Verificadas" || gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Aprobado" || gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Documentos Generados" || gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Desembolsado")
                    {

                       
                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Referencias Verificadas")
                        {
                          
                            Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();
                            Session[ReferenciaServicio.CodigoPrograma + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
      
                        }
                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Aprobado")
                        {
                            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
                            Session[creditoServicio.CodigoPrograma + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
                            
                           }
                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Documentos Generados")
                        {
                            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                            Session[creditoServicio.CodigoPrograma + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
                            
                        }
                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Desembolsado")
                        {
                            
                            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                            Session[creditoServicio.CodigoProgramaoriginal + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
                        }

                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Check List Positivo Operaciones"&&  gvLista.Rows[e.NewEditIndex].Cells[13].Text == "Aprobado")
                        {

                            Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                            Session[creditoServicio.CodigoPrograma + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
                        }
                        if (gvLista.Rows[e.NewEditIndex].Cells[5].Text == "Check List Positivo Operaciones" && gvLista.Rows[e.NewEditIndex].Cells[13].Text == "Referencias Verificadas")
                        {

                            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
                            Session[creditoServicio.CodigoPrograma + ".id"] = id;
                            Session["Datacredito"] = FechaDatacredito;
                        }


                    }
                   

                        Response.Redirect(HyperConsulta.NavigateUrl, false);
                        return;
                    }
               

                Session["Numeroidentificacion"] = identificacion;
                Session["NumeroCredito"] = id;
                Session["Proceso"] = proceso;
                Session["Datacredito"] = FechaDatacredito;
                Session["Estado"] = Estado;


                Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".id"] = id;
                Navegar(Pagina.Editar);
            }
        
        else
        {
            Session["Numeroidentificacion"] = identificacion;
            Session["NumeroCredito"] = id;
            Session["Proceso"] = proceso;
            Session["Datacredito"] = FechaDatacredito;
            Session["Estado"] = Estado;

           // Session[ControlTiemposServicio.CodigoProgramaRpEficiencia + ".id"] = id;
            //Navegar(Pagina.Editar);
        }

    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ControlTiemposServicio.EliminarControlTiempos(id, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaRpEficiencia, "gvLista_RowDeleting", ex);
        }
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
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaRpEficiencia, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstConsulta = ControlTiemposServicio.ListarControlTiemposEfic(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            Session["DTREPORTEEFICIENCIA"] = lstConsulta;
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

            Session.Add(ControlTiemposServicio.CodigoProgramaRpEficiencia + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaRpEficiencia, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ControlTiempos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.ControlTiempos vControlTiempos = new Xpinn.FabricaCreditos.Entities.ControlTiempos();
        Usuario pUsuario = (Usuario)Session["usuario"];
        vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(pUsuario.codusuario, (Usuario)Session["usuario"]);

            if (ddlOficina.Text.Trim() != "")
            {
                vControlTiempos.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
           
            }
        else  
        {
            vControlTiempos.cod_oficina = pUsuario.cod_oficina;
        }


            if (DdlEjecutivo.Text.Trim() != "")
            {
                vControlTiempos.asesor = Convert.ToString(DdlEjecutivo.SelectedValue);
                if (vControlTiempos.asesor=="0")
                {
                    vControlTiempos.asesor = null;
                }
       
            }

        if (ddlEstado.Text.Trim() != "")
            vControlTiempos.proceso1 = Convert.ToString(ddlEstado.SelectedValue);
        if (vControlTiempos.proceso1 == null)
            {

                vControlTiempos.proceso1 = "1";
            }

        if (ddlEstado2.Text.Trim() != "")
            vControlTiempos.proceso2 = Convert.ToString(ddlEstado2.SelectedValue);

        if (vControlTiempos.proceso2 == null)
        {

            vControlTiempos.proceso2 = "1";
        }
        if (txtNumeroCredito.Text.Trim() != "")            
            vControlTiempos.numerocredito = Convert.ToString(txtNumeroCredito.Text.Trim());
        
        
        if (txtFechaProcesoIn.Text.Trim() != "")
            vControlTiempos.fechaprocesoin += Convert.ToString(txtFechaProcesoIn.Text.Trim());
        if (txtFechaProcesoFin.Text.Trim() != "")
            vControlTiempos.fechaprocesofin += Convert.ToString(txtFechaProcesoFin.Text.Trim());
     
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
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaRpEficiencia);
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

    double promedioMinutos = 0;
 
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int horas = 0, minutos = 0;
            string tiempoTotal = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "tiempototal"));
            if (tiempoTotal.Contains("horas"))
            {
                horas = Convert.ToInt32(tiempoTotal.Substring(0, tiempoTotal.IndexOf("horas")));
                tiempoTotal = tiempoTotal.Substring(tiempoTotal.IndexOf("horas")+5, tiempoTotal.Length-(tiempoTotal.IndexOf("horas")+5));
            }
            if (tiempoTotal.Contains("min"))
            {
                minutos = Convert.ToInt32(tiempoTotal.Substring(0, tiempoTotal.IndexOf("min")));
            }
            promedioMinutos += ((horas * 60) + minutos);

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[8].Text = "Promedio:";
            if (gvLista.Rows.Count != 0)
            {
                promedioMinutos = Math.Round(promedioMinutos / gvLista.Rows.Count);
                double horas = Math.Truncate(promedioMinutos / 60);
                e.Row.Cells[9].Text = horas.ToString() + "horas ";
                double minutos = Math.Round(((promedioMinutos / 60) - horas)*60);
                e.Row.Cells[9].Text += minutos.ToString() + "min";
            }
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;           
            e.Row.Font.Bold = true;
        }

    }



    protected void LlenarComboAsesores(Int64 iOficina)
    {
     
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();
        ejec.IOficina = iOficina;
        DdlEjecutivo.DataSource = serviceEjecutivo.ListarAsesores(ejec, (Usuario)Session["usuario"]);      
        DdlEjecutivo.DataTextField = "NombreCompleto";
        DdlEjecutivo.DataValueField = "IdEjecutivo";
        DdlEjecutivo.DataBind();
        DdlEjecutivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        
    }
    protected void LlenarComboAsesoresTodos()
    {

        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        EjecutivoService serviceEjecutivo = new EjecutivoService();
        Ejecutivo ejec = new Ejecutivo();        
        DdlEjecutivo.DataSource = serviceEjecutivo.ListartodosAsesoresRuta((Usuario)Session["usuario"]);
        DdlEjecutivo.DataTextField = "NombreCompleto";
        DdlEjecutivo.DataValueField = "IdEjecutivo";
        DdlEjecutivo.DataBind();
        DdlEjecutivo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

    }
    
    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOficina.SelectedValue == "0")
        {
            LlenarComboAsesoresTodos();
        }
        else
        {
            LlenarComboAsesores(Convert.ToInt64(ddlOficina.SelectedValue));
        
        }
     
    }
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Actualizar();
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        if (Session["DTREPORTEEFICIENCIA"] == null)
        {
            VerError("No ha generado el Reporte para poder imprimir informacion");
        }
        else
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstConsulta = (List<Xpinn.FabricaCreditos.Entities.ControlTiempos>)Session["DTREPORTEEFICIENCIA"];

            // LLenar data table con los datos a recoger
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Credito");
            table.Columns.Add("Proceso1");
            table.Columns.Add("fechaProceso1");
            table.Columns.Add("Proceso2");
            table.Columns.Add("FechaProceso2");     
            table.Columns.Add("TiempoProcesos");
            table.Columns.Add("FechaSolicitud");
            table.Columns.Add("FechaDatacaredito");
            table.Columns.Add("TiempoSolDatacre");             
            table.Columns.Add("fechaEntrega");
            table.Columns.Add("Identificacion");
            table.Columns.Add("Oficina");
            table.Columns.Add("Asesor");
            table.Columns.Add("MontoAprobado");
            table.Columns.Add("Aprobador");

            foreach (Xpinn.FabricaCreditos.Entities.ControlTiempos item in lstConsulta)
            {
                DataRow datarw;
                datarw = table.NewRow();
                datarw[0] = item.numerocredito;
                datarw[1] = item.proceso1;
                datarw[2] = item.fecha1;
                datarw[3] = item.proceso2;
                datarw[4] = item.fecha2;
                datarw[5] = item.tiempototal;
                datarw[6] = item.fechas;
                datarw[7] = item.fechadata;
                datarw[8] = item.tiempodatacredito;
                datarw[9] = item.fechaentrega;
                datarw[10] = item.identificacion;
                datarw[11] = item.cod_oficina;
                datarw[12] = item.asesor;
                datarw[13] = item.monto;
                datarw[14] = item.nombreaprobador;


                table.Rows.Add(datarw);
            }



            // ---------------------------------------------------------------------------------------------------------
            // Pasar datos al reporte
            // ---------------------------------------------------------------------------------------------------------

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            ReportParameter[] param = new ReportParameter[5];
            param[0] = new ReportParameter("entidad", pUsuario.empresa);
            param[1] = new ReportParameter("nit", pUsuario.nitempresa);           
            param[2] = new ReportParameter("Usuario", pUsuario.nombre);
            param[3] = new ReportParameter("Oficina", pUsuario.nombre_oficina);
            param[4] = new ReportParameter("ImagenReport", ImagenReporte());


            rvReporte.LocalReport.EnableExternalImages = true;
            rvReporte.LocalReport.SetParameters(param);

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            rvReporte.LocalReport.DataSources.Clear();
            rvReporte.LocalReport.DataSources.Add(rds);
            rvReporte.LocalReport.Refresh();


            // Mostrar el reporte en pantalla.
            mvControlTiempos.ActiveViewIndex = 1;
        }
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (gvLista.Rows.Count > 0)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=reporte_eficiencia.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");  
    }
}