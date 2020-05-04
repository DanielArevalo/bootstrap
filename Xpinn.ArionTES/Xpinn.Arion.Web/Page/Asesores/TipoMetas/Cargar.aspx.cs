using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class Cargar : GlobalWeb
{
    Usuario usuario = new Usuario();
    EjecutivoMetaService serviceEjecutivoMeta = new EjecutivoMetaService();
   
    
    EjecutivoMeta entityEjecutivoMeta = new EjecutivoMeta();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    String operacion;
    static ArrayList ar = new ArrayList();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEjecutivoMeta.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoGuardar += btnGuardar_Click;

            //ucImprimir.PrintCustomEvent += ucImprimir_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
                if (Session[serviceEjecutivoMeta.GetType().Name + ".consulta"] != null)
                    operacion = (String)Session["operacion"];
                if (operacion == null)
                {
                    Actualizar();                    
                }
            }
            msg.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "Page_Load", ex);
        }

    }
   


    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
        Navegar(Pagina.Nuevo);
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        Guardar();
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
        //if (txtMes.Text == "")
        //    txtMes.Text = Convert.ToString(0);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, serviceEjecutivoMeta.GetType().Name);
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            long idObjeto = Convert.ToInt64(gvLista.Rows[e.RowIndex].Cells[0].Text);
            serviceEjecutivoMeta.EliminarEjecutivoMeta(idObjeto, (Usuario)Session["usuario"]);
            Actualizar();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.CodigoPrograma + "L", "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Editar")
        {
            String[] tmp = evt.CommandArgument.ToString().Split('|');
            EjecutivoMeta ejeMeta = new EjecutivoMeta();
            ejeMeta.IdEjecutivo = Convert.ToInt64(tmp[0]);
            ejeMeta.IdEjecutivoMeta = Convert.ToInt64(tmp[1]);
            ejeMeta.IdMeta = Convert.ToInt64(tmp[2]);
            ejeMeta.PrimerNombre = tmp[3];
            ejeMeta.SegundoNombre = tmp[4];
            ejeMeta.NombreOficina = tmp[5];
            ejeMeta.Mes = tmp[6];
            ejeMeta.NombreMeta = tmp[7];
            ejeMeta.VlrMeta = 8;

            Session["EditMetaEjecutivo"] = ejeMeta;
            Session[serviceEjecutivoMeta.CodigoPrograma + ".id"] = ejeMeta.IdEjecutivoMeta;
            Session[serviceEjecutivoMeta.CodigoPrograma + ".from"] = "l";
            Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(serviceEjecutivoMeta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {

        try
        {
            List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();
            lstConsulta = serviceEjecutivoMeta.ListarEjecutivos(ObtenerValores(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(serviceEjecutivoMeta.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEjecutivoMeta.CodigoPrograma, "Actualizar", ex);
        }

    }

    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    private string obtFiltroColocNumero()
    {
        String filtro = String.Empty; 
                 
        filtro = "   where icodmeta=1";
        
        return filtro;
    }

    private string obtFiltroColocPesos()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=2";

        return filtro;
    }
    private string obtFiltroRodamientoPorcentaje()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=3";

        return filtro;
    }

    private string obtFiltroRodamientoPesos()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=4";

        return filtro;
    }
    private string obtFiltroCartMenorPorcentaje()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=5";

        return filtro;
    }

    private string obtFiltroCartMenorPesos()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=6";

        return filtro;
    }
   
    private string obtFiltroCartMayorPorcentaje()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=7";

        return filtro;
    }

    private string obtFiltroCartMayorPesos()
    {
        String filtro = String.Empty;

        filtro = "   where icodmeta=8";

        return filtro;
    }

    private EjecutivoMeta ObtenerValores()
    {
        entityEjecutivoMeta = new EjecutivoMeta();

        //if (!string.IsNullOrEmpty(txtPrimerNombre.Text.Trim()))
        //    entityEjecutivoMeta.PrimerNombre = txtPrimerNombre.Text.Trim();
        //if (!string.IsNullOrEmpty(txtMes.Text.Trim()))
        //    entityEjecutivoMeta.Mes = txtMes.Text;

        return entityEjecutivoMeta;
    }

    protected void btnDescargarMetas_Click(object sender, EventArgs e)
    {
        //MemoryStream memory;
        //entityEjecutivoMeta = new EjecutivoMeta();
        //entityEjecutivoMeta.Mes = txtMes.Text;
        //entityEjecutivoMeta.PrimerNombre = txtPrimerNombre.Text;

        //memory = serviceEjecutivoMeta.DescargarArchivoEjecutivoMeta(entityEjecutivoMeta, (Usuario)Session["usuario"]);

        //if (memory != null)
        //{
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=MetasEjectutivos.csv");
        //    Response.AppendHeader("Content-Length", memory.ToArray().Length.ToString());
        //    Response.ContentType = "application/octet-stream";
        //    Response.BinaryWrite(memory.ToArray());
        //    Response.End();
        //}
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
            Response.AddHeader("Content-Disposition", "attachment;filename=data.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
            VerError("Se debe generar el reporte primero");
    }

    //protected void btnCargarMetas_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (FileUploadMetas.HasFile)
    //        {
    //            entityEjecutivoMeta = new EjecutivoMeta();
    //            entityEjecutivoMeta.stream = FileUploadMetas.FileContent;
    //            if (serviceEjecutivoMeta.CargarArchivoEjecutivoMeta(entityEjecutivoMeta, (Usuario)Session["usuario"]))
    //            {
    //                Actualizar();
    //                LblOficina.Visible = true;
    //                LblOficina.Text = "Su Archivo Se ha Cargado";
    //            }
    //            else
    //            {
    //                LblOficina.Visible = true;
    //                LblOficina.Text = "Archivo No Valido";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.ToString());
    //    }

    //}
    
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        TextBox txtRodamientoPesos = ((TextBox)(e.Row.FindControl("txtRodamientoPesos")));
        TextBox txtRodamientoPorc = ((TextBox)(e.Row.FindControl("txtRodamientoPorc")));
        TextBox txtMenor30Porc = ((TextBox)(e.Row.FindControl("txtMenor30Porc")));
        TextBox txtMenor30Pesos = ((TextBox)(e.Row.FindControl("txtMenor30Pesos")));
        TextBox txtMayor30Por = ((TextBox)(e.Row.FindControl("txtMayor30Por")));
        TextBox txtMayor30Pesos = ((TextBox)(e.Row.FindControl("txtMayor30Pesos")));
        TextBox txtnumcolocacion = ((TextBox)(e.Row.FindControl("txtnumcolocacion")));
        TextBox txtpesoscolocacion = ((TextBox)(e.Row.FindControl("txtpesoscolocacion")));
        if (e.Row.RowType == DataControlRowType.DataRow)
        {                 
            if (ChkPorcentaje.Checked == true)
            {
                ChkPesos.Checked = false;
                txtRodamientoPorc.Visible = true;
                txtRodamientoPesos.Visible = false;
            }
            if (ChkPesos.Checked==true)
            {
                ChkPorcentaje.Checked = false;
                txtRodamientoPorc.Visible = false;
                txtRodamientoPesos.Visible = true;
            }           
            if (ChkPorcCarMenor.Checked)
            {
                ChkPesosCarMenor.Checked = false;
                txtMenor30Porc.Visible = true;
                txtMenor30Pesos.Visible = false;
            }
            if (ChkPesosCarMenor.Checked)
            {
                ChkPorcCarMenor.Checked = false;
                txtMenor30Porc.Visible = false;
                txtMenor30Pesos.Visible = true;
            }            
            if (ChkPorcCarMayor.Checked)
            {
                ChkPesosCarMayor.Checked = false; 
                txtMayor30Por.Visible = true;
                txtMayor30Pesos.Visible = false;
            }
            if (ChkPesosCarMayor.Checked)
            {
                ChkPorcCarMayor.Checked = false;
                txtMayor30Por.Visible = false;
                txtMayor30Pesos.Visible = true;
            }
            if (ChkColocNumero.Checked)
            {
                txtnumcolocacion.Visible = true;              
            }
            if (ChkColocNumero.Checked==false)
            {
                txtnumcolocacion.Visible = false;
            }
            if (ChkColocPesos.Checked)
            {
                txtpesoscolocacion.Visible = true;
            }
            if (ChkColocPesos.Checked==false)
            {
                txtpesoscolocacion.Visible = false;
            }           
        }   
    }

    protected List<EjecutivoMeta> ObtenerListaTopes()
    {
        List<EjecutivoMeta> lstTopes = new List<EjecutivoMeta>();
        List<EjecutivoMeta> lista = new List<EjecutivoMeta>();

        foreach (GridViewRow rfila in gvLista.Rows)
        {
            EjecutivoMeta ejecutivos = new EjecutivoMeta();


            DropDownListGrid ddlOficinas = (DropDownListGrid)rfila.FindControl("ddlOficinas");
            if (ddlOficinas.SelectedValue != null || ddlOficinas.SelectedIndex != 0)
                ejecutivos.Codficina = Convert.ToInt32(ddlOficinas.SelectedValue);

            lstTopes.Add(ejecutivos);
           
        }
        return lstTopes;
    }

    protected void InicializarLista()
    {
        List<EjecutivoMeta> lstConsulta = new List<EjecutivoMeta>();
        for (int i = gvLista.Rows.Count; i < 4; i++)
        {
            EjecutivoMeta ejecutivo = new EjecutivoMeta();
            ejecutivo.IdEjecutivo = 0;
            ejecutivo.Nombres = "";
            ejecutivo.NombreOficina = "";
            ejecutivo.NombreMeta = "";
            

            lstConsulta.Add(ejecutivo);
        }
        gvLista.DataSource = lstConsulta;
        gvLista.DataBind();
        Session["Ejecutivos"] = lstConsulta;


    }

    protected void chkSeleccionarcentro_CheckedChanged(object sender, EventArgs e)
    {

    }

    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {        
        
    }

    protected void ChkPorcentaje_CheckedChanged(object sender, EventArgs e)
    {   
        if (ChkPorcentaje.Checked==true)
        {
            Actualizar();
            ChkPesos.Checked = false;               
        }
        if (ChkPorcentaje.Checked == false && ChkPesos.Checked == false)
        {
            ChkPorcentaje.Checked = true; 
        }
            
    }

    protected void ChkPesos_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkPesos.Checked==true)
        {
            ChkPorcentaje.Checked = false; 
            Actualizar();
        } 
        if (ChkPorcentaje.Checked == false && ChkPesos.Checked == false)
        {
            ChkPorcentaje.Checked = true; 
            Actualizar();
        }
       
    }

    protected void ChkPorcCarMenor_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkPorcCarMenor.Checked)
        {
            ChkPesosCarMenor.Checked = false; 
            Actualizar();
        }
        if (ChkPorcCarMenor.Checked == false && ChkPesosCarMenor.Checked == false)
        {
            ChkPorcCarMenor.Checked = true;
            Actualizar();
        }
                   
    }

    protected void ChkPesosCarMenor_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkPesosCarMenor.Checked)
        {
            ChkPorcCarMenor.Checked = false; 
            Actualizar();
           
        }
        if (ChkPorcCarMenor.Checked == false && ChkPesosCarMenor.Checked == false)
        {
            ChkPorcCarMenor.Checked = true;
            Actualizar();
        }
    }

    protected void ChkPorcCarMayor_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkPorcCarMayor.Checked)
        {
            ChkPesosCarMayor.Checked = false;
            Actualizar();

        }
        if (ChkPorcCarMayor.Checked == false && ChkPesosCarMayor.Checked == false)
        {
            ChkPorcCarMayor.Checked = true;
            Actualizar();
        }
        
    }

    protected void ChkPesosCarMayor_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkPesosCarMayor.Checked)
        {
            ChkPorcCarMayor.Checked = false; 
            Actualizar();

        }
        if (ChkPorcCarMayor.Checked == false && ChkPesosCarMayor.Checked == false)
        {
            ChkPorcCarMayor.Checked = true;
            Actualizar();
        }
      
    }

    private void    Guardar()
    {   
        String filtro;
        filtro = obtFiltroColocNumero();
         String filtro2;
         filtro2 = obtFiltroColocPesos();
         String filtro3;
         filtro3 = obtFiltroRodamientoPorcentaje();
         String filtro4;
         filtro4 = obtFiltroRodamientoPesos();
         String filtro5;
         filtro5 = obtFiltroCartMenorPorcentaje();
         String filtro6;
         filtro6 = obtFiltroCartMenorPesos();
         String filtro7;
         filtro7 = obtFiltroCartMayorPorcentaje();
         String filtro8;
         filtro8 = obtFiltroCartMayorPesos();
        try
        {     
            Label lblcodigo;
            TextBox txtnumcolocacion;
            TextBox txtpesoscolocacion;
            TextBox txtRodamientoPesos;
            TextBox txtRodamientoPorc;
            TextBox txtMenor30Porc;
            TextBox txtMenor30Pesos;
            TextBox txtMayor30Por ;
            TextBox txtMayor30Pesos;

            EjecutivoMeta datosApp = new EjecutivoMeta();

            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();         
              
            foreach (GridViewRow wrow in gvLista.Rows)
            {
                EjecutivoMeta lstConsultaCierreMensual = new EjecutivoMeta();
               
                lblcodigo = (Label)wrow.FindControl("lblcodigo");
                txtnumcolocacion = (TextBox)wrow.FindControl("txtnumcolocacion");
                txtpesoscolocacion = (TextBox)wrow.FindControl("txtpesoscolocacion");
                txtRodamientoPesos = ((TextBox)(wrow.FindControl("txtRodamientoPesos")));
                txtRodamientoPorc = ((TextBox)(wrow.FindControl("txtRodamientoPorc")));
                txtMenor30Porc = ((TextBox)(wrow.FindControl("txtMenor30Porc")));
                txtMenor30Pesos = ((TextBox)(wrow.FindControl("txtMenor30Pesos")));
                txtMayor30Por = ((TextBox)(wrow.FindControl("txtMayor30Por")));
                txtMayor30Pesos = ((TextBox)(wrow.FindControl("txtMayor30Pesos")));

                datosApp.Mes = DdlMes.SelectedValue;
                datosApp.Year = DdlYear.SelectedValue;
                datosApp.Vigencia = DdlPeriodicidad.SelectedValue;
                //colocaciones
                EjecutivoMeta lstConsultaMetaColoPesos = new EjecutivoMeta();
                EjecutivoMeta lstConsultaMetaColoNum = new EjecutivoMeta();
                // rodamiento
                EjecutivoMeta lstRodamientoPorcentaje = new EjecutivoMeta();
                EjecutivoMeta lstRodamientoPesos = new EjecutivoMeta();
                // cartera menor
                EjecutivoMeta lstCartMenorPorcentaje = new EjecutivoMeta();
                EjecutivoMeta lstCartMenorPesos = new EjecutivoMeta();
                // cartera mayor
                EjecutivoMeta lstCartMayorPorcentaje = new EjecutivoMeta();
                EjecutivoMeta lstCartMayorPesos = new EjecutivoMeta();
              
                //colocaciones
                lstConsultaMetaColoNum  = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro);
                lstConsultaMetaColoPesos = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro2);
                // rodamiento
                lstRodamientoPorcentaje = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro3);
                lstRodamientoPesos = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro4);
                // cartera menor
                lstCartMenorPorcentaje = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro5);
                lstCartMenorPesos = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro6);
                // cartera mayor
                lstCartMayorPorcentaje = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro7);
                lstCartMayorPesos = serviceEjecutivoMeta.ConsultarMeta((Usuario)Session["usuario"], filtro8);
                
                if (ChkColocNumero.Checked)
                {
                    if(txtpesoscolocacion.Text!="")
                    {
                    
                    datosApp.IdMeta = lstConsultaMetaColoPesos.IdMeta;//Colocacion  en pesos
                    datosApp.VlrMeta = Convert.ToInt64(txtpesoscolocacion.Text);
                    datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                    try
                    {
                        lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                       
                    }
                    catch (Exception ex)
                    {
                        int n = 1;
                        if (ex.Message.Contains("ORA-20101:"))
                            n = ex.Message.IndexOf("ORA-20101:") + 10;
                        if (n > 0)
                        {
                             lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                        }
                        else
                        {
                            lblMensaje.Text = ex.Message;
                        }
                    }
                    }
                }

                
                if (ChkColocPesos.Checked)
                {
                    if (txtnumcolocacion.Text != "")
                    {
                        datosApp.IdMeta = lstConsultaMetaColoNum.IdMeta;//colocacion en numeros

                        datosApp.VlrMeta = Convert.ToInt64(txtnumcolocacion.Text);
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                            
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                if (ChkPorcentaje.Checked)
                {
                    if (txtRodamientoPorc.Text != "")
                    {
                        datosApp.IdMeta = lstRodamientoPorcentaje.IdMeta;//Rodamiento en porcentaje 
                        datosApp.VlrMeta = Convert.ToInt64(txtRodamientoPorc.Text);
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                            
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                  lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                 lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                if (ChkPorcCarMenor.Checked)
                {
                    if (txtMenor30Porc.Text != "")
                    {
                        datosApp.IdMeta = lstCartMenorPorcentaje.IdMeta;//cartera menor a 30 Porcentaje                   
                        datosApp.VlrMeta = Convert.ToInt64(txtMenor30Porc.Text);                       
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                                      }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                 lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                if (ChkPorcCarMayor.Checked)
                {
                    if (txtMayor30Por.Text != "")
                    {
                        datosApp.IdMeta = lstCartMayorPorcentaje.IdMeta; //cartera mnayor a 30                      
                        datosApp.VlrMeta = Convert.ToInt64(txtMayor30Por.Text);
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                            
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                   lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                 lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }

                if (ChkPesos.Checked)
                {
                    if (txtRodamientoPesos.Text != "")
                    {
                        datosApp.IdMeta = lstRodamientoPesos.IdMeta;//rodamiento en pesos 
                        datosApp.VlrMeta = Convert.ToInt64(txtRodamientoPesos.Text);                      
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                           
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                  lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                if (ChkPesosCarMenor.Checked)
                {
                    if (txtMenor30Pesos.Text != "")
                    {
                        datosApp.IdMeta = lstCartMenorPesos.IdMeta;//cartera menor a 30 en pesos                   
                        datosApp.VlrMeta = Convert.ToInt64(txtMenor30Pesos.Text);
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                          
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                 lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                if (ChkPesosCarMayor.Checked)
                {
                    if (txtMayor30Pesos.Text != "")
                    {
                        datosApp.IdMeta = lstCartMayorPesos.IdMeta;//cartera mayor a 30 en pesos                              
                        datosApp.VlrMeta = Convert.ToInt64(txtMayor30Pesos.Text);
                        datosApp.IdEjecutivo = Convert.ToInt64(lblcodigo.Text);
                        try
                        {
                            lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                         
                        }
                        catch (Exception ex)
                        {
                            int n = 1;
                            if (ex.Message.Contains("ORA-20101:"))
                                n = ex.Message.IndexOf("ORA-20101:") + 10;
                            if (n > 0)
                            {
                                lblMensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                            }
                            else
                            {
                                 lblMensaje.Text = ex.Message;
                            }
                        }
                    }

                }
                   // EjecutivoMeta lstConsultaCierreMensual = new EjecutivoMeta();
                    //try
                    //{
                    //    lstConsultaCierreMensual = serviceEjecutivoMeta.CrearEjecutivoMeta(datosApp, (Usuario)Session["usuario"]);
                    //   // lblmensaje.Text = "Cierre Mensual Terminado Correctamente";
                    //}
                    //catch (Exception ex)
                    //{
                    //    int n = 1;
                    //    if (ex.Message.Contains("ORA-20101:"))
                    //        n = ex.Message.IndexOf("ORA-20101:") + 10;
                    //    if (n > 0)
                    //    {
                    //     //   lblmensaje.Text = ex.Message.Substring(n, ex.Message.Length - n);
                    //    }
                    //    else
                    //    {
                    //       // lblmensaje.Text = ex.Message;
                    //    }
                    
                //}
            }
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(CierreMensualSer.CodigoPrograma, "Actualizar", ex);
        }


    }

    protected void ChkColocNumero_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkColocNumero.Checked == true)        
        {           
            Actualizar();
        }
        if (ChkColocNumero.Checked == false)
        {
            Actualizar();
        }
        
       
    }

    protected void ChkColocPesos_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkColocPesos.Checked == true)
        {
            Actualizar();
        }
        if (ChkColocPesos.Checked == false)
        {
            Actualizar();
        }
    }

}