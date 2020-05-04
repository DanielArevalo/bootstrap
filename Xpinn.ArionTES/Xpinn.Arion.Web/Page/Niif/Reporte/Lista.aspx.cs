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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.NIIF.Services;
using Xpinn.NIIF.Entities;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using JeffFerguson.Gepsio;
using System.IO;
using Microsoft.Win32;
using System.Text;
using JeffFerguson.Gepsio.Xlink;
using System.Web.SessionState;
using System.Drawing;
using System.Net;
using Subgurim.Controles;
using System.Globalization;
using System.Net.Mail;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using System.Threading;
using System.Data.SqlClient;


public partial class Lista : GlobalWeb
{
    Xpinn.NIIF.Services.BalanceNIIFService MatrizRiesgoNIFServicio = new Xpinn.NIIF.Services.BalanceNIIFService();
    Configuracion conf = new Configuracion();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif.ToString(), "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.MostrarImprimir(false);
            toolBar.MostrarExportar(false);
            mvMatrizRiesgo.ActiveViewIndex = 0;

        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboClasificacion();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(false);
            }
            if (Session[MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif + ".id"] != null)
            {
                idObjeto = Session[MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif + ".id"].ToString();
                Session.Remove(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif + ".id");                
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(MatrizRiesgoNIFServicio.GetType().Name + "A", "Page_Load", ex);
        }
    }


    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        VerError(" ");
        Site toolBar = (Site)this.Master;
        ddlcentrocosto.SelectedIndex = 0;
        ddlFechaCorte.SelectedIndex = 0;
        ddlMoneda.SelectedIndex = 0;
        ddlNivel.SelectedIndex = 0;
        chkmuestraceros.Checked = false;
        lblInfo.Visible = false;
        gvMatrizRiesgo.Visible = false;
        Lblerror.Visible = false;
        LblTitulo.Visible = false;
        lblTotalRegs.Visible = false;
        toolBar.MostrarImprimir(false);
        toolBar.MostrarExportar(false);
        mvMatrizRiesgo.ActiveViewIndex = 0;
    }

    protected void btnExportar_Click(object sender, EventArgs e) 
    {       
        try
        {
            if (chkmuestraceros.Checked == true)
            {
                Xpinn.NIIF.Services.BalanceNIIFService balanceservice = new Xpinn.NIIF.Services.BalanceNIIFService();
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                BalanceNIIF entidadvalor = new BalanceNIIF();
                if (ddlMoneda.SelectedIndex != 0)
                    entidadvalor.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                if (ddlcentrocosto.SelectedValue != "")
                    entidadvalor.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                if (ddlNivel.SelectedIndex != 0)
                    entidadvalor.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
                if (ddlFechaCorte.SelectedIndex != 0)
                    entidadvalor.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
                entidadvalor.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
                lstConsulta = balanceservice.listarbalancereporteXBLRConceros(entidadvalor, ObtenerValoresSb(), (Usuario)Session["usuario"]);
                gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvMatrizRiesgo.DataSource = lstConsulta;
                StringBuilder sb = new StringBuilder();
                if (gvMatrizRiesgo.Rows.Count > 0)
                {
                    // an admittedly contrived example, since URIs are already supported by
                    // the original Load() method, but ... you get the point.

                    var webClient = new WebClient();
                    string path = Server.MapPath(@"../../Niif/ReporteNif/amzn-20120331.xml");
                    string readXml = webClient.DownloadString(path);
                    byte[] byteArray = Encoding.ASCII.GetBytes(readXml);
                    MemoryStream memStream = new MemoryStream(byteArray);
                    var newDoc = new XbrlDocument();
                    newDoc.Load(memStream);
                    var firstFragment = newDoc.XbrlFragments[0];
                    var tree = firstFragment.GetPresentableFactTree();


                    XbrlDocument a = new XbrlDocument();
                    Page pagina = new Page();

                    dynamic form = new HtmlForm();
                    gvMatrizRiesgo.AllowPaging = false;
                    gvMatrizRiesgo.DataBind();
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);

                    form.Controls.Add(gvMatrizRiesgo);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=1.xbrl");
                    Response.Charset = "UTF-32";
                    ////Formato XBRL (ESTE ES EL FORMATO QUE SE SEGUIRIA SI SE QUIERE PONER FUERA DE LAS ETIQUETAS <HTML>
                    ///Response.Write(readXml);
                    ///ESTE FORMATO ES XML (EXPORTA EL HTML DE LA GRILLA A XML
                    StringWriter sbs = new StringWriter();
                    ExpGrilla expGrilla = new ExpGrilla();
                    char es = '"';
                    Response.Write("<xbrl>");
                    Response.Write("<xbrll:schemaRef xlink:type=" + es + "simple" + es + " xlink:arcrole=" + es + "http://www.xbrl.org/2003/linkbase" + es + " xlink:href=" + es + "amzn-20120331.xsd" + es + " xmlns:xlink=" + es + "http://www.w3.org/1999/xlink" + es + " xmlns:xbrll=" + es + "http://www.xbrl.org/2003/linkbase" + es + " />");
                    foreach (BalanceNIIF sdf in lstConsulta)
                    {
                        Response.Write("<br>");
                        Response.Write("<NombreCuenta>"+"CODIGO CUENTA: " + sdf.cod_cuenta_niif + "</NombreCuenta>");
                        Response.Write("</br>");
                        Response.Write("<br>");
                        Response.Write("<NombreValor>" + "VALOR: " + sdf.saldo + "</NombreValor>");
                        Response.Write("</br>");
                        Response.Write("<br>");
                        Response.Write("<NombrePersona>" + "NOMBRE PERSONA: " + sdf.nombre + "</NombrePersona>");
                        Response.Write("</br>");

                    }
                    Response.Write("</xbrl>");
                    ///sbs=expGrilla.ObtenerGrilla(gvMatrizRiesgo, null);
                    newDoc.Load(form);
                         
                }

            }
            else
            {
                Xpinn.NIIF.Services.BalanceNIIFService balanceservice = new Xpinn.NIIF.Services.BalanceNIIFService();
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsultas = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                BalanceNIIF entidadvalor = new BalanceNIIF();
                if (ddlMoneda.SelectedIndex != 0)
                    entidadvalor.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                if (ddlcentrocosto.SelectedValue != "")
                    entidadvalor.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                if (ddlNivel.SelectedIndex != 0)
                    entidadvalor.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
                if (ddlFechaCorte.SelectedIndex != 0)
                    entidadvalor.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
                entidadvalor.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
                lstConsultas = balanceservice.listarbalancereporteXBLR(entidadvalor, ObtenerValoresSb(), (Usuario)Session["usuario"]);
                gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvMatrizRiesgo.DataSource = lstConsultas;
                StringBuilder sb = new StringBuilder();
                if (lstConsultas.Count > 0)
                {
                    // an admittedly contrived example, since URIs are already supported by
                    // the original Load() method, but ... you get the point.

                    var webClient = new WebClient();
                    string path = Server.MapPath(@"../../Niif/ReporteNif/amzn-20120331.xml");
                    string readXml = webClient.DownloadString(path);
                    byte[] byteArray = Encoding.ASCII.GetBytes(readXml);
                    MemoryStream memStream = new MemoryStream(byteArray);
                    var newDoc = new XbrlDocument();
                    newDoc.Load(memStream);
                    var firstFragment = newDoc.XbrlFragments[0];
                    var tree = firstFragment.GetPresentableFactTree();


                    XbrlDocument a = new XbrlDocument();
                    Page pagina = new Page();

                    dynamic form = new HtmlForm();
                    gvMatrizRiesgo.AllowPaging = false;
                    gvMatrizRiesgo.DataBind();
                    pagina.DesignerInitialize();
                    pagina.Controls.Add(form);

                    form.Controls.Add(gvMatrizRiesgo);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition", "attachment;filename=1.xbrl");
                    Response.Charset = "UTF-32";
                    ////Formato XBRL (ESTE ES EL FORMATO QUE SE SEGUIRIA SI SE QUIERE PONER FUERA DE LAS ETIQUETAS <HTML>
                         
                    ///ESTE FORMATO ES XML (EXPORTA EL HTML DE LA GRILLA A XML
                    StringWriter sbs = new StringWriter();
                    ExpGrilla expGrilla = new ExpGrilla();
                    char es = '"';
                    Response.Write("<xbrl>");
                    Response.Write("<xbrll:schemaRef xlink:type=" + es + "simple" + es + " xlink:arcrole=" + es + "http://www.xbrl.org/2003/linkbase" + es + " xlink:href=" + es + "amzn-20120331.xsd" + es + " xmlns:xlink=" + es + "http://www.w3.org/1999/xlink" + es + " xmlns:xbrll=" + es + "http://www.xbrl.org/2003/linkbase" + es + " />");
                    foreach (BalanceNIIF sdf in lstConsultas)
                    {
                        Response.Write("<br>");
                        Response.Write("<NombreCuenta>" + "CODIGO CUENTA: " + sdf.cod_cuenta_niif + "</NombreCuenta>");
                        Response.Write("</br>");
                        Response.Write("<br>");
                        Response.Write("<NombreValor>" + "VALOR: " + sdf.saldo + "</NombreValor>");
                        Response.Write("</br>");
                        Response.Write("<br>");
                        Response.Write("<NombrePersona>" + "NOMBRE PERSONA: " + sdf.nombre + "</NombrePersona>");
                        Response.Write("</br>");

                    }
                    Response.Write("</xbrl>");
                    newDoc.Load(form);
                }
                else
                {
                    VerError("No hay datos para generar el reporte XBRL");
                }
            }
                 
        }
        catch
        {
            Response.End();
        }
    }


    
    

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    /// .<summary>
    /// Método para la consulta de los scoreboards
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {     
        Page.Validate();
        ViewState["gvMatrizRiesgoRow"] = null;
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif);
            Actualizar();
            mvMatrizRiesgo.ActiveViewIndex = 0;
        }
    }

    /// <summary>
    /// Método para cuando se va a crear un score board
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

 
   

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    /// <summary>
    /// Método para mostrar los datos de un score board seleccionado para edición
    /// </summary>
    /// <param name="consecutivo"></param>
    /// <param name="gvMatrizRiesgo"></param>
    /// <param name="Var"></param>

   
    /// <summary>
    /// Método para obtener los datos de consulta
    /// </summary>
    /// <returns></returns>
    private string ObtenerValoresSb()
    {
        

        string filtro = " where 1=1 ";

        if (ddlNivel.SelectedIndex != 0)
        {
            filtro = filtro + " and NIVEL = " + ddlNivel.SelectedValue + "";
        }

        if (ddlFechaCorte.SelectedIndex != 0)
        {
            filtro = filtro + " and To_char(FECHA,'dd/mm/yyyy')= '" + ddlFechaCorte.SelectedValue + "'";
        }

        if (ddlcentrocosto.SelectedIndex !=0)
        {
            filtro = filtro + " and CENTRO_COSTO = " + ddlcentrocosto.SelectedValue;  
        }

        return filtro;
    }

    public void Actualizar()
    {
        try
        {
            if (chkmuestraceros.Checked == false)
            {
                
                Xpinn.NIIF.Services.BalanceNIIFService balanceservice = new Xpinn.NIIF.Services.BalanceNIIFService();
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                BalanceNIIF entidadvalor = new BalanceNIIF();
                if (ddlMoneda.SelectedIndex != 0)
                    entidadvalor.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                if (ddlcentrocosto.SelectedValue != "")
                    entidadvalor.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                if (ddlNivel.SelectedIndex != 0)
                    entidadvalor.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
                if (ddlFechaCorte.SelectedIndex != 0)
                    entidadvalor.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
                entidadvalor.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
                lstConsulta = balanceservice.listarbalancereporteXBLR(entidadvalor, ObtenerValoresSb(), (Usuario)Session["usuario"]);
                gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvMatrizRiesgo.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;
                if (lstConsulta.Count > 0)
                {
                    gvMatrizRiesgo.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvMatrizRiesgo.DataBind();

                    toolBar.MostrarImprimir(true);
                    toolBar.MostrarExportar(true);
                }
                else
                {
                    gvMatrizRiesgo.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = false;
                    toolBar.MostrarImprimir(false);
                    toolBar.MostrarExportar(false);
                }


                Session.Add(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif + ".consulta", 1);

                gvMatrizRiesgo.EditIndex = -1;
                gvMatrizRiesgo.DataBind();



            }

            if (chkmuestraceros.Checked == true)
            {
                VerError("Estos son los datos en cero");
                Xpinn.NIIF.Services.BalanceNIIFService balanceservice = new Xpinn.NIIF.Services.BalanceNIIFService();
                List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
                BalanceNIIF entidadvalor = new BalanceNIIF();
                if (ddlMoneda.SelectedIndex != 0)
                    entidadvalor.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                if (ddlcentrocosto.SelectedValue != "")
                    entidadvalor.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
                if (ddlNivel.SelectedIndex != 0)
                    entidadvalor.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
                if (ddlFechaCorte.SelectedIndex != 0)
                    entidadvalor.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
                entidadvalor.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
                lstConsulta = balanceservice.listarbalancereporteXBLRConceros(entidadvalor, ObtenerValoresSb(), (Usuario)Session["usuario"]);
                gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvMatrizRiesgo.DataSource = lstConsulta;
                Site toolBar = (Site)this.Master;
                if (lstConsulta.Count > 0)
                {
                    gvMatrizRiesgo.Visible = true;
                    lblInfo.Visible = false;
                    lblTotalRegs.Visible = true;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    gvMatrizRiesgo.DataBind();

                    toolBar.MostrarImprimir(true);
                    toolBar.MostrarExportar(true);
                }
                else
                {
                    gvMatrizRiesgo.Visible = false;
                    lblInfo.Visible = true;
                    lblTotalRegs.Visible = false;
                    toolBar.MostrarImprimir(false);
                    toolBar.MostrarExportar(false);
                }


                Session.Add(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif + ".consulta", 1);

                gvMatrizRiesgo.EditIndex = -1;
                gvMatrizRiesgo.DataBind();
            }
        }
        catch (Exception ex)
        {

            BOexcepcion.Throw(MatrizRiesgoNIFServicio.CodigoProgramaReporteNiif, "Actualizar", ex);
        }
    }

    
    /// <summary>
    /// Método para llenar la lista de clasificaciones
    /// </summary>
    protected void LlenarComboClasificacion()
    {
        // LLenar el DDl de centro de costo
        Xpinn.Contabilidad.Services.CentroCostoService CentroCostoService = new Xpinn.Contabilidad.Services.CentroCostoService();
        List<Xpinn.Contabilidad.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Contabilidad.Entities.CentroCosto>();
        string sFiltro = "";
        LstCentroCosto = CentroCostoService.ListarCentroCosto((Usuario)Session["Usuario"], sFiltro);
        ddlcentrocosto.DataSource = LstCentroCosto;
        ddlcentrocosto.DataTextField = "nom_centro";
        ddlcentrocosto.DataValueField = "centro_costo";
        ddlcentrocosto.DataBind();
        ddlcentrocosto.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        // Determinando el centro de costo inicial y final
        Int64 CenIni = Int64.MinValue;
        Int64 CenFin = Int64.MinValue;
        foreach (Xpinn.Contabilidad.Entities.CentroCosto ItemCC in LstCentroCosto)
        {
            if (CenIni == Int64.MinValue)
                CenIni = ItemCC.centro_costo;
            if (CenFin == Int64.MinValue)
                CenFin = ItemCC.centro_costo;
            if (CenIni > ItemCC.centro_costo)
                CenIni = ItemCC.centro_costo;
            if (CenFin < ItemCC.centro_costo)
                CenFin = ItemCC.centro_costo;
        }
        Session["CenIni"] = CenIni;
        Session["CenFin"] = CenFin;

        //// LLenar el DDl de TipoMoneda
        //Xpinn.Contabilidad.Services.TipoMonedaService TipoMonedaService = new Xpinn.Contabilidad.Services.TipoMonedaService();
        //List<Xpinn.Contabilidad.Entities.TipoMoneda> LstTipoMoneda = new List<Xpinn.Contabilidad.Entities.TipoMoneda>();
        //LstTipoMoneda = TipoMonedaService.ListarTipoMoneda((Usuario)Session["Usuario"]);
        //ddlMoneda.DataSource = LstTipoMoneda;
        //ddlMoneda.DataTextField = "descripcion";
        //ddlMoneda.DataValueField = "tipo_moneda";
        //ddlMoneda.DataBind();

        // Llenar el DDL de la fecha de corte 
        Configuracion conf = new Configuracion();
        List<Xpinn.Contabilidad.Entities.BalanceGeneral> lstFechaCierre = new List<Xpinn.Contabilidad.Entities.BalanceGeneral>();
        Xpinn.Contabilidad.Services.BalanceGeneralService BalancePruebaService = new Xpinn.Contabilidad.Services.BalanceGeneralService();
        Xpinn.Contabilidad.Entities.BalanceGeneral BalancePrueba = new Xpinn.Contabilidad.Entities.BalanceGeneral();
        lstFechaCierre = BalancePruebaService.ListarFechaCorte((Usuario)Session["Usuario"]);
        ddlFechaCorte.DataSource = lstFechaCierre;
        ddlFechaCorte.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCorte.DataTextField = "fecha";
        ddlFechaCorte.DataBind();
        ddlFechaCorte.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));


        Xpinn.Caja.Services.TipoTopeService tipoTopeServicio = new Xpinn.Caja.Services.TipoTopeService();
        Xpinn.Caja.Entities.TipoTope tipoTope = new Xpinn.Caja.Entities.TipoTope();
        List<Xpinn.Caja.Entities.TipoTope> lstConsulta = new List<Xpinn.Caja.Entities.TipoTope>();
        lstConsulta = tipoTopeServicio.ListarTipoTope(tipoTope, (Usuario)Session["usuario"]);
        ddlMoneda.DataSource = lstConsulta;
        ddlMoneda.DataTextField = "descmoneda";
        ddlMoneda.DataValueField = "cod_moneda";
        ddlMoneda.DataBind();
        ddlMoneda.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));


        ddlNivel.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlNivel.Items.Insert(1, new ListItem("1-Grupo", "1"));
        ddlNivel.Items.Insert(2, new ListItem("2-Clase", "2"));
        ddlNivel.Items.Insert(3, new ListItem("3-Mayor", "3"));
        ddlNivel.Items.Insert(4, new ListItem("4-Subcuenta", "4"));
        ddlNivel.Items.Insert(5, new ListItem("5-Auxiliar", "5"));
        ddlNivel.Items.Insert(6, new ListItem("6-SubAuxiliar", "6"));
        ddlNivel.Items.Insert(7, new ListItem("7-SubAuxiliar", "7"));
        ddlNivel.SelectedIndex = 0;
        ddlNivel.DataBind();
    }

    private void QuitarFilaInicialScoreBoard()
    {
        try
        {
            int conseID = Convert.ToInt32(gvMatrizRiesgo.DataKeys[0].Values[0].ToString());
            if (conseID <= 0)
            {
                ImageButton link = (ImageButton)this.gvMatrizRiesgo.Rows[0].Cells[0].FindControl("btnEditar");

                link.Enabled = false;

                link.Visible = false;

                this.gvMatrizRiesgo.Rows[0].Cells[1].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[2].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[3].Visible = false;
                this.gvMatrizRiesgo.Rows[0].Cells[4].Visible = false;
            }
        }
        catch
        {
        }

    }
    
    protected void gvMatrizRiesgo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
    }


    protected void gvMatrizRiesgo_RowEditing(object sender, GridViewEditEventArgs e)
    {
       
    }
 
    protected void gvMatrizRiesgo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {        
        
    }


    protected void btnImprimir_Click(object sender, EventArgs e)

    {

        if (chkmuestraceros.Checked == true)
        {

            VerError("Estos son los datos con ceros");
            Xpinn.NIIF.Services.BalanceNIIFService balanceservice = new Xpinn.NIIF.Services.BalanceNIIFService();
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsulta = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            BalanceNIIF entidadvalor = new BalanceNIIF();
            if (ddlMoneda.SelectedIndex != 0)
                entidadvalor.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
            if (ddlcentrocosto.SelectedValue != "")
                entidadvalor.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
            if (ddlNivel.SelectedIndex != 0)
                entidadvalor.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
            if (ddlFechaCorte.SelectedIndex != 0)
                entidadvalor.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
            entidadvalor.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
            lstConsulta = balanceservice.listarbalancereporteXBLRConceros(entidadvalor, ObtenerValoresSb(), (Usuario)Session["usuario"]);
            gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvMatrizRiesgo.DataSource = lstConsulta;

            Site toolBar = (Site)this.Master;


            if (lstConsulta.Count > 0)
            {

                DataTable tablegeneral = new DataTable();
                tablegeneral.Columns.Add("Cod_cuenta");
                tablegeneral.Columns.Add("Nombre");
                tablegeneral.Columns.Add("Valor");


                if (lstConsulta.Count > 0)
                {
                    foreach (BalanceNIIF fila in lstConsulta)
                    {
                        DataRow datarw;
                        datarw = tablegeneral.NewRow();
                        if (fila.cod_cuenta_niif != null)
                        {
                            datarw[0] = fila.cod_cuenta_niif.ToString();
                        }
                        else
                        {
                            datarw[0] = " ";
                        }
                        if (fila.nombre != null)
                        {
                            datarw[1] = fila.nombre.ToString();
                        }
                        else
                        {
                            datarw[1] = " ";
                        }


                        datarw[2] = fila.saldo;



                        tablegeneral.Rows.Add(datarw);
                    }
                }
                Usuario pUsu = (Usuario)Session["usuario"];

                ReportViewer2.LocalReport.DataSources.Clear();
                ReportParameter[] parames = new ReportParameter[2];
                parames[0] = new ReportParameter("Entidad", pUsu.empresa);
                parames[1] = new ReportParameter("Nit", pUsu.nitempresa);

                ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
                ReportViewer2.LocalReport.DataSources.Add(rds1);

                ReportViewer2.LocalReport.EnableExternalImages = true;
                ReportViewer2.LocalReport.SetParameters(parames);
                ReportViewer2.LocalReport.Refresh();

                ReportViewer2.Visible = true;
                frmPrint.Visible = false;
                mvMatrizRiesgo.ActiveViewIndex = 1;
                ReportViewer2.Visible = true;
                toolBar.MostrarExportar(true);

            }
            
        }
        if (chkmuestraceros.Checked == false)
        {
            Site toolBar = (Site)this.Master;
            //Lista de Codeudores
            Xpinn.NIIF.Services.BalanceNIIFService balanceservices = new Xpinn.NIIF.Services.BalanceNIIFService();
            List<Xpinn.NIIF.Entities.BalanceNIIF> lstConsultas = new List<Xpinn.NIIF.Entities.BalanceNIIF>();
            BalanceNIIF entidadvalors = new BalanceNIIF();
            if (ddlMoneda.SelectedIndex != 0)
                entidadvalors.tipo_moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
            if (ddlcentrocosto.SelectedValue != "")
                entidadvalors.centro_costo = Convert.ToInt32(ddlcentrocosto.SelectedValue);
            if (ddlNivel.SelectedIndex != 0)
                entidadvalors.nivel = Convert.ToInt32(ddlNivel.SelectedValue);
            if (ddlFechaCorte.SelectedIndex != 0)
                entidadvalors.fecha = Convert.ToDateTime(ddlFechaCorte.SelectedValue);
            entidadvalors.mostrar_ceros = Convert.ToInt32(chkmuestraceros.Checked);
            lstConsultas = balanceservices.listarbalancereporteXBLR(entidadvalors, ObtenerValoresSb(), (Usuario)Session["usuario"]);
            gvMatrizRiesgo.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            gvMatrizRiesgo.DataSource = lstConsultas;

            if (lstConsultas.Count > 0)
            {

                DataTable tablegeneral = new DataTable();
                tablegeneral.Columns.Add("Cod_cuenta");
                tablegeneral.Columns.Add("Nombre");
                tablegeneral.Columns.Add("Valor");


                if (lstConsultas.Count > 0)
                {
                    foreach (BalanceNIIF fila in lstConsultas)
                    {
                        DataRow datarw;
                        datarw = tablegeneral.NewRow();
                        if (fila.cod_cuenta_niif != null)
                        {
                            datarw[0] = fila.cod_cuenta_niif.ToString();
                        }
                        else
                        {
                            datarw[0] = " ";
                        }
                        if (fila.nombre != null)
                        {
                            datarw[1] = fila.nombre.ToString();
                        }
                        else
                        {
                            datarw[1] = " ";
                        }


                        datarw[2] = fila.saldo;



                        tablegeneral.Rows.Add(datarw);
                    }
                }
                Usuario pUsu = (Usuario)Session["usuario"];

                ReportViewer2.LocalReport.DataSources.Clear();
                ReportParameter[] parames = new ReportParameter[2];
                parames[0] = new ReportParameter("Entidad", pUsu.empresa);
                parames[1] = new ReportParameter("Nit", pUsu.nitempresa);

                ReportDataSource rds1 = new ReportDataSource("DataSet1", tablegeneral);
                ReportViewer2.LocalReport.DataSources.Add(rds1);

                ReportViewer2.LocalReport.EnableExternalImages = true;
                ReportViewer2.LocalReport.SetParameters(parames);
                ReportViewer2.LocalReport.Refresh();

                ReportViewer2.Visible = true;
                frmPrint.Visible = false;
                mvMatrizRiesgo.ActiveViewIndex = 1;
                ReportViewer2.Visible = true;
                toolBar.MostrarExportar(true);

            }
        }
    }

    protected void btnInforme4_Click(object sender, EventArgs e) 
    {

        Navegar(Pagina.Lista);
        mvMatrizRiesgo.ActiveViewIndex = 0;
    }


    protected void btnImprime_Click(object sender, EventArgs e)
    {

        if (ReportViewer2.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
            frmPrint.Visible = true;
            ReportViewer2.Visible = false;

        }
    }

   
}
