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
using System.Globalization;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;

partial class Nuevo : GlobalWeb
{
    String Acta;
    String fecha;
    Int64 lineacredito;
    Credito cargogerente = new Credito();
    Credito cargojunta = new Credito();
    Credito cargojunta2 = new Credito();
    Credito creditoreestructurado = new Credito();


    private Xpinn.FabricaCreditos.Services.ActasService ActaServicio = new Xpinn.FabricaCreditos.Services.ActasService();
    AprobadorService aprobadorServicio = new AprobadorService();
    Configuracion conf = new Configuracion();
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ActaServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnInforme_Click;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                try
                {
                    txtFechaaprobacion.Text = Convert.ToString(DateTime.Now);
                }
                catch (Exception ex)
                {
                    VerError(ex.Message + conf.ObtenerFormatoFecha());
                }
            
                LlenarComboOficinas(ddlOficinas);
                Usuario usuap = (Usuario)Session["usuario"];
                cargogerente = ActaServicio.ConsultarParametrocargoGerente((Usuario)Session["Usuario"]);
                if (usuap.codperfil != cargogerente.paramcargo)               
                {
                    ChkRestructurado.Visible =false;
                    Lblrestructurado.Visible = false;
                }
                CargarValoresConsulta(pConsulta, ActaServicio.CodigoPrograma);
                if (Session[ActaServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar();
                if (LblMensaje.Visible == true && ChkRestructurado.Checked==true)
                {
                    ChkRestructurado.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        Navegar("~/Page/FabricaCreditos/Actas/Lista.aspx");


    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {     
        List<Credito> lstConsulta = new List<Credito>();
        lstConsulta = (List<Credito>)Session["Reporte"];        
        string idacta = DateTime.Now.ToString("yyyyMMddhhmmss");
        Int64? numacta = null;
        int control = 0;
        DateTime Fechaaprobacion = Convert.ToDateTime(txtFechaaprobacion.Text);
        foreach (Credito row in lstConsulta)
        {
           row.acta = 0;
           if (row.numero_radicacion != Int64.MinValue)
           {
                if (row.numero_radicacion.ToString().Length > 0)
                {
                    if (control == 0)
                    {
                        numacta = this.ActaServicio.CrearActaNumero(idacta, Fechaaprobacion, null, ((Usuario)Session["usuario"]).codusuario, (Usuario)Session["usuario"]);
                        Session["Mensaje"] = numacta;
                        control += 1;
                    }
                    row.acta = Convert.ToInt64(numacta);
                    this.ActaServicio.CrearRadicadosActas(row, Fechaaprobacion, ((Usuario)Session["usuario"]).codusuario, (Usuario)Session["usuario"]);
                    numacta = row.acta;
                    fecha = row.fechaacta; 
                    ChkRestructurado.Enabled = false;                    
                }
           }
        }
        String mensaje = "";
        if (Session["Mensaje"] != null)
        { 
            mensaje = "Acta generada correctamente, Numero de Acta: " + " " + Session["Mensaje"].ToString();
        }
        LblMensaje.Visible = true;
        this.LblMensaje.Text = mensaje;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarLimpiar(false);
        toolBar.MostrarImprimir(true);
        txtFechaaprobacion.Enabled = false;
        ddlOficinas.Enabled = false;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ActaServicio.CodigoPrograma);
            Actualizar();
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        LblMensaje.Text = "";
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ActaServicio.CodigoPrograma);
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[ActaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;

        Session[ActaServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
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
            BOexcepcion.Throw(ActaServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Credito>();                      
            String filtro = obtFiltro(ObtenerValores());
            String filtro2 = obtFiltro2(ObtenerValoresRestructurados());       
            LblMensaje.Text="";
            OficinaService oficinaService = new OficinaService();
            Oficina oficina = new Oficina();

            Usuario usuap = (Usuario)Session["usuario"];
            Int64 codoficina = usuap.cod_oficina;
            int cod = Convert.ToInt32(usuap.codusuario);
            int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
            //if (consulta >= 1 && ChkRestructurado.Checked == false)
            //{
                lstConsulta = ActaServicio.ListarCreditos(ObtenerValores(), (Usuario)Session["usuario"], filtro);               
            //}
            //if (consulta == 0)
            //{
            //    lstConsulta = ActaServicio.ListarCreditosUsuarios(ObtenerValores(), (Usuario)Session["usuario"], filtro, codoficina);                
            //}
            //if (consulta >= 1 && ChkRestructurado.Checked == true)
            //{
            //    lstConsulta = ActaServicio.ListarCreditosRestructurados(ObtenerValoresRestructurados(), (Usuario)Session["usuario"], filtro2);
            //}

            Session["Reporte"] = lstConsulta;
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;        
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();               
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(ActaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActaServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValores()
    {
        Configuracion conf = new Configuracion();
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        String Fechaaprobacion = txtFechaaprobacion.Texto;
        
        if (ddlOficinas.SelectedIndex !=0)
           vCredito.oficina = ddlOficinas.SelectedValue;
       
        try
        {
            if (txtFechaaprobacion.Text.Trim() != "")
                vCredito.fecha_aproba = Convert.ToDateTime(txtFechaaprobacion.Texto);
        }
        catch (Exception ex)
        {
            VerError("Error al convertir la fecha de aprobación" + ex.Message);
        }
        return vCredito;
    }

    private Xpinn.FabricaCreditos.Entities.Credito ObtenerValoresRestructurados()
    {
        Configuracion conf = new Configuracion();
        Xpinn.FabricaCreditos.Entities.Credito vCredito = new Xpinn.FabricaCreditos.Entities.Credito();
        String Fechaaprobacion = txtFechaaprobacion.Text;

        if (ddlOficinas.SelectedIndex != 0)
            vCredito.oficina = ddlOficinas.SelectedValue;
        
        if (ChkRestructurado.Checked == true)
        {
            creditoreestructurado = ActaServicio.ConsultarParametroReestructurado((Usuario)Session["Usuario"]);
            vCredito.cod_linea_credito = creditoreestructurado.paramrestruct;
        }

        try
        {
            if (txtFechaaprobacion.Text.Trim() != "")
                vCredito.fecha_aproba = Convert.ToDateTime(Fechaaprobacion);
        }
        catch (Exception ex)
        {
            VerError("Error al convertir la fecha de aprobación" + ex.Message);
        }
        return vCredito;
    }




    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficinas"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficinas)
    {
        OficinaService oficinaService = new OficinaService();
        Oficina oficina = new Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddlOficinas.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
          
            ddlOficinas.DataTextField = "nombre";
            ddlOficinas.DataValueField = "codigo";
            ddlOficinas.DataBind();
            ddlOficinas.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        }
        else
        {
            if (consulta == 0)
                ddlOficinas.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));           
            ddlOficinas.DataBind();                      
        }
    } 

    private Aprobador ObtenerValoresAprobador()
    {
        Aprobador aprobador = new Aprobador();
        Usuario usuap = (Usuario)Session["usuario"];
        aprobador.codusuaAprobador = Convert.ToInt32(usuap.codusuario);
        return aprobador;
    }


    /// <summary>
    /// Generar las condiciones de acuerdo a los filtros ingresados
    /// </summary>
    /// <param name="credito"></param>
    /// <returns></returns>
    private string obtFiltro(Credito credito)
    {         
        String filtro = String.Empty;
        Usuario usuap = (Usuario)Session["usuario"];
        Int32 pIdObjeto;
        pIdObjeto = Convert.ToInt32(usuap.codusuario);
        //try
        //{            
        //    filtro += " And aprobador = " + usuap.codusuario;       
        //}
        //catch (Exception ex)
        //{
        //   BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "Actualizar", ex);
        //}

        if (ddlOficinas.SelectedIndex > 0)
            filtro += " and cod_oficina= '" + credito.oficina + "'";
        if (txtFechaaprobacion.Text.Trim() != "")
        {
            String Fechaaprobacion = txtFechaaprobacion.Text;
            credito.fecha_aprobacion = Convert.ToDateTime(Fechaaprobacion);
            filtro += " and to_CHAR(fecha_aprobacion,'" + conf.ObtenerFormatoFecha() + "')=  '" + Convert.ToDateTime(credito.fecha_aprobacion).ToString("" + conf.ObtenerFormatoFecha() + "") + "'";
        }        
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " and " + filtro;
        }
        return filtro;
    }

    private string obtFiltro2(Credito credito)
    {
        String filtro = String.Empty;
        String filtro2 = String.Empty;
        Usuario usuap = (Usuario)Session["usuario"];
        Int32 pIdObjeto;
        pIdObjeto = Convert.ToInt32(usuap.codusuario);
        try
        {
            int contador = 0;
      
            List<Aprobador> lstConsulta = new List<Aprobador>();
            lstConsulta = aprobadorServicio.ListarAprobadorActaRestructurados(ObtenerValoresAprobador(), (Usuario)Session["usuario"]);
            foreach (Aprobador rFila in lstConsulta)
            {
                if (contador > 0)                
                    filtro += " And(";
                else
                    filtro += " (   cod_linea_credito = '" + rFila.CodLineaCredito + "'";             
                    contador += 1;
            }                      
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "Actualizar", ex);
        }

        if (ddlOficinas.SelectedIndex > 0)
            filtro += " and cod_oficina= '" + credito.oficina + "'";
        if (txtFechaaprobacion.Text.Trim() != "")
        {
            String Fechaaprobacion = txtFechaaprobacion.Text;
            credito.fecha_aprobacion = Convert.ToDateTime(Fechaaprobacion);
            filtro += " and to_CHAR(fecha_aprobacion,'" + conf.ObtenerFormatoFecha() + "')=  '" + Convert.ToDateTime(credito.fecha_aprobacion).ToString("" + conf.ObtenerFormatoFecha() + "") + "'";
        }
        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " and " + filtro;
        }
        return filtro;
    }

  
    protected void txtNombre_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {

    }

    protected void txtFechaaprobacion_TextChanged(object sender, EventArgs e)
    {
             
    }

    protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {              
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        LblMensaje.Text = "";
        lblTotalRegs.Visible = false;
    }
   
    public DataTable CrearDataTableCreditos()
    {
        String pIdObjeto = Convert.ToString(Session["Mensaje"]);

        List<Credito> LstConsulta = new List<Credito>();
        Credito credito = new Credito();
        string numacta = Convert.ToString(pIdObjeto);
        LstConsulta = ActaServicio.ListarCreditosReporte(credito, (Usuario)Session["usuario"], numacta);
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("NUMERO_RADICACION");
        table.Columns.Add("IDENTIFICACION");
        table.Columns.Add("TIPO_IDENTIFICACION");
        table.Columns.Add("NOMBRES");
        table.Columns.Add("LINEA");
        table.Columns.Add("OFICINA");
        table.Columns.Add("MONTO_APROBADO");
        table.Columns.Add("PLAZO");
        table.Columns.Add("PERIODICIDAD");
        table.Columns.Add("VALOR_CUOTA");
        table.Columns.Add("NUMERO_CUOTAS");
        table.Columns.Add("TASA");
        table.Columns.Add("ASESOR");
        table.Columns.Add("DESCRIPCION_TASA");
        table.Columns.Add("IDEN_CODEUDOR");
        table.Columns.Add("NOM_CODEUDOR");
        table.Columns.Add("ESTADO");

        foreach (Credito item in LstConsulta)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numero_radicacion;
            datarw[1] = item.identificacion;
            datarw[2] = item.tipo_identificacion;
            datarw[3] = item.nombre;
            datarw[4] = item.linea_credito;
            datarw[5] = item.oficina;
            datarw[6] = item.monto.ToString("##,##0");
            datarw[7] = item.plazo;
            datarw[8] = item.periodicidad;
            datarw[9] = item.valor_cuota.ToString("##,##0");
            datarw[10] = item.numero_cuotas;
            datarw[11] = item.tasa;
            datarw[12] = item.NombreAsesor;
            datarw[13] = item.desc_tasa;
            datarw[14] = item.Codeudor;
            datarw[15] = item.NombreCodeudor;
            datarw[16] = item.estado;
            table.Rows.Add(datarw);
        }
        return table;
    }
    protected void ObtenerDatosimprimir(String pIdObjeto)
    {
        try
        {
            Credito credito = new Credito();
            credito.acta = Int32.Parse(pIdObjeto);
            credito = ActaServicio.ConsultarActa(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            Acta = pIdObjeto;
            fecha = credito.fechaacta;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ActaServicio.GetType().Name + "A", "ObtenerDatosimprimir", ex);
        }
    }

    private Boolean VerificarParametrocargoGerente()
    {
        Usuario usuap = (Usuario)Session["usuario"];
        Boolean continuar = true;
        Credito credito = new Credito();
        credito = ActaServicio.ConsultarParametrocargoGerente((Usuario)Session["Usuario"]);
        Int64 cargo = credito.paramcargo;
        Int64 perfil = usuap.codperfil;
        if (perfil != cargo)
        {
            String Error = "No tiene perfil para generar actas";
            this.LblMensaje.Text = Error;
            //this.btnInforme.Visible = false;         

            continuar = false;
        }
        else
        {
            //this.btnInforme.Visible=true;
            this.LblMensaje.Text = "";
        }
        return continuar;
    }
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        if (gvLista.Visible)
        {
            Usuario usuap = (Usuario)Session["usuario"];
            cargogerente = ActaServicio.ConsultarParametrocargoGerente((Usuario)Session["Usuario"]);
            cargojunta = ActaServicio.ConsultarParametrocargoComitedecreditoniv1((Usuario)Session["Usuario"]);
            cargojunta2 = ActaServicio.ConsultarParametrocargoComitedecreditoniv4((Usuario)Session["Usuario"]);
            Int64 cargogerencia = cargogerente.paramcargo;
            Int64 cargocomitedecreditonivel1 = cargojunta.paramcargo;
            Int64 cargocomitedecreditonivel4 = cargojunta2.paramcargo;
            Int64 perfil = usuap.codperfil;

            String Elaboro = Convert.ToString(usuap.nombre);
            String pIdObjeto = Convert.ToString(Session["Mensaje"]);
            String Cargo = Convert.ToString(usuap.nombreperfil);
            String Aprobado = Convert.ToString(usuap.nombreperfil);

            Configuracion conf = new Configuracion();
            VerError("");
            if (LblMensaje.Visible == false)
            {
                VerError("No ha grabado el acta para imprimirla");
            }
            else
            {
                Usuario pUsu = (Usuario)Session["usuario"];
                ReportParameter[] param = new ReportParameter[9];
                param[0] = new ReportParameter("PFecha", Convert.ToString(DateTime.Now));
                param[1] = new ReportParameter("PActa", pIdObjeto.ToString());
                param[2] = new ReportParameter("PFechaActa", Convert.ToString(txtFechaaprobacion.Text).ToString());
                param[3] = new ReportParameter("pEntidad", pUsu.empresa);
                param[4] = new ReportParameter("pElaborado", pUsu.representante_legal);
                param[5] = new ReportParameter("pCargo", "GERENTE GENERAL");
                param[6] = new ReportParameter("pfacultad", "dentro de sus facultades  otorgadas por el Consejo de Administración");
                param[7] = new ReportParameter("pAprobado", "Gerencia");
                param[8] = new ReportParameter("ImagenReport", ImagenReporte());

                mvReporte.Visible = true;
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;

                ReportViewActa.LocalReport.EnableExternalImages = true;

                ReportViewActa.LocalReport.SetParameters(param);
                ReportViewActa.LocalReport.DataSources.Clear();
                ReportDataSource rdscreditos = new ReportDataSource("DataSetCreditosReporte", CrearDataTableCreditos());
                ReportViewActa.LocalReport.DataSources.Add(rdscreditos);
                if (ChkRestructurado.Checked == false)
                {
                    ReportViewActa.LocalReport.ReportPath = "Page\\FabricaCreditos\\Actas\\ReporteActas.rdlc";
                }
                else
                {
                    ReportViewActa.LocalReport.ReportPath = "Page\\FabricaCreditos\\Actas\\ReporteActasRestructurados.rdlc";
                }
                if (usuap.codperfil == cargocomitedecreditonivel1)
                {
                    ReportViewActa.LocalReport.ReportPath = "Page\\FabricaCreditos\\Actas\\ReporteActas.rdlc";
                }
                if (usuap.codperfil == cargocomitedecreditonivel4)
                {
                    ReportViewActa.LocalReport.ReportPath = "Page\\FabricaCreditos\\Actas\\ReporteActasComite.rdlc";
                }
                ReportViewActa.LocalReport.Refresh();
                mvReporte.ActiveViewIndex = 0;

                //Genera el pdf automaticamente 
                Warning[] warnings;
                String[] streamids;
                String mimetype;
                String encoding;
                String extension;
                string _sSuggestedName = String.Empty;
                byte[] bytes = this.ReportViewActa.LocalReport.Render("PDF", null, out mimetype, out encoding, out extension, out streamids, out warnings);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                string ruta = Server.MapPath("~/Archivos/Actas/");
                if (Directory.Exists(ruta))
                {
                    String fileName = "Acta-" + pIdObjeto + ".pdf";
                    string savePath = ruta + fileName;
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(savePath);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                    fs.Close();
                }
            }
        }
        else
        {
            mvReporte.Visible = false;
            gvLista.Visible = true;
            lblTotalRegs.Visible = true;
        }
    }
   
    protected void ddlOficinas_Load(object sender, EventArgs e)
    {

    }

     protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Aprobador aprobador = new Aprobador();
            String Linea;
            String Usuario;
            String Nivel;
            String Minimo;
            String Maximo;
            String Aprueba;
            if (pIdObjeto != null)
            {
                aprobador.Id = Int32.Parse(pIdObjeto);
                aprobador = aprobadorServicio.ConsultarAprobadorActa(aprobador, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(aprobador.LineaCredito))
                    Linea = aprobador.LineaCredito.ToString();
                if (!string.IsNullOrEmpty(aprobador.UsuarioAprobador))
                    Usuario = aprobador.UsuarioAprobador.ToString();
                if (!string.IsNullOrEmpty(aprobador.Nivel.ToString()))
                    Nivel = aprobador.Nivel.ToString();
                if (!string.IsNullOrEmpty(aprobador.MontoMinimo.ToString()))
                    Minimo = HttpUtility.HtmlDecode(aprobador.MontoMinimo.ToString());
                if (!string.IsNullOrEmpty(aprobador.MontoMaximo.ToString()))
                    Maximo = HttpUtility.HtmlDecode(aprobador.MontoMaximo.ToString());
                if (!string.IsNullOrEmpty(aprobador.Aprueba.ToString()))
                {
                    Aprueba = HttpUtility.HtmlDecode(aprobador.Aprueba.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(aprobadorServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
     protected void ChkRestructurado_CheckedChanged(object sender, EventArgs e)
     {
         Actualizar();
     }
}