using System;
using System.Globalization;
using System.Text;
using System.IO;
using System.Data.Common;
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
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Microsoft.Reporting.WebForms;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.IO.Packaging;
using System.Net;
using System.ComponentModel;
using Ionic.Zip;

public partial class Page_Asesores_Colocacion_Lista : GlobalWeb
{
    private ClienteService clienteServicio = new ClienteService();
    private CreditosService creditoServicio = new CreditosService();
    private Persona1Service personaService = new Persona1Service();
    private int contador = 0;

    string cDocsSubDir = "/Cartas";
    Int64 credito;
    private const long BUFFER_SIZE = 4096;
    String nombrearchivo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(clienteServicio.CodigoProgramaRealRecuperaciones, "L");
            Site toolBar = (Site)this.Master;

            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoProgramaRealRecuperaciones, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LlenarComboOficinasvalidado(ddlOficina);
                LlenarComboZonasvalidado(ddlZona);
                CargarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
                if (Session[creditoServicio.CodigoPrograma + ".consulta"] != null)
                {
                    Actualizar();
                }
                //Modificado ya que generaba error al cargar la página
                if (Session["ESTADOCUENTA"] != null)
                {
                    //    //Producto persona;
                    //    //persona = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
                    //    //String nameCache = persona.Persona.IdPersona.ToString();
                    Int64 cod = Convert.ToInt64(Session["ESTADOCUENTA"]);
                    txtCliente.Text = cod.ToString();
                    Actualizar();
                    Session["ESTADOCUENTA"] = null;
                    LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
                    Session["Texto"] = null;
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvListaCreditos.DataSource = null;
        gvListaCreditos.DataBind();
        LblReportes.Text = "";
        LimpiarValoresConsulta(pConsulta, creditoServicio.CodigoPrograma);
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String idCliente = gvListaCreditos.SelectedRow.Cells[1].Text;
        Session[clienteServicio.CodigoPrograma + ".id"] = idCliente;
        Session[clienteServicio.CodigoPrograma + ".from"] = "l";
        String idCredito = gvListaCreditos.SelectedRow.Cells[3].Text;
        Session[creditoServicio.CodigoPrograma + ".id"] = idCredito;
        Session[creditoServicio.CodigoPrograma + ".from"] = "l";
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaCreditos.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void LlenarComboOficinas(DropDownList ddlOficina)
    {
        OficinaService oficinaService = new OficinaService(); Usuario usuap = (Usuario)Session["usuario"];
        Usuario usuap1 = (Usuario)Session["usuario"];
        Diligencia credito = new Diligencia();

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["usuario"]);
        if (consulta >= 1)
        {
            Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
        }
        else
        {
            Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
            List<Xpinn.FabricaCreditos.Entities.Oficina> lstOficina = new List<Xpinn.FabricaCreditos.Entities.Oficina>();
            oficina.Codigo = Convert.ToInt32(usuap1.cod_oficina);
            oficina.Nombre = usuap1.nombre_oficina;
            lstOficina.Add(oficina);
            ddlOficina.DataSource = lstOficina;
        }
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        ddlOficina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboOficinasAsesores(DropDownList ddlOficina)
    {
        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinasAsesores(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        ddlOficina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));

    }

    protected void LlenarComboOficinasUsuarios(DropDownList ddlOficina)
    {

        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        ddlOficina.DataSource = oficinaService.ListarOficinasUsuarios(oficina, (Usuario)Session["usuario"]);
        ddlOficina.DataTextField = "Nombre";
        ddlOficina.DataValueField = "Codigo";
        ddlOficina.DataBind();
        ddlOficina.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));

    }


    protected void LlenarComboOficinasvalidado(DropDownList ddlOficina)
    {
        UsuarioAtribucionesService UsuarioAtribucionesServicio = new UsuarioAtribucionesService();
        UsuarioAtribuciones vUsuarioAtribuciones = new UsuarioAtribuciones();
        List<UsuarioAtribuciones> lstAtribuciones = null;
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);

        try
        {
            vUsuarioAtribuciones.codusuario = usuap.codusuario;
            lstAtribuciones = UsuarioAtribucionesServicio.ListarUsuarioAtribuciones(vUsuarioAtribuciones, (Usuario)Session["usuario"]);
        }
        catch
        {
            vUsuarioAtribuciones.tipoatribucion = -1;
            vUsuarioAtribuciones.activo = -1;
            lstAtribuciones.Add(vUsuarioAtribuciones);
            // VerError("El usuario no tiene atribuciones definadas");
        }

        if (lstAtribuciones != null)
        {
            if (lstAtribuciones.Any(x => x.tipoatribucion == 0 && x.activo == 0))
            {
                LlenarComboOficinasAsesores(ddlOficina);    // por ejecutivo
            }
            else if (lstAtribuciones.Any(x => x.tipoatribucion == 0 && x.activo == 1))
            {
                LlenarComboOficinasUsuarios(ddlOficina);    //por oficina
            }
            else if (lstAtribuciones.Any(x => x.tipoatribucion == 1 && x.activo == 1))
            {
                LlenarComboOficinas(ddlOficina);            //todo
            }
            else
            {
                LlenarComboOficinas(ddlOficina);            //todo
                try
                {
                    ddlOficina.SelectedValue = ((Usuario)Session["Usuario"]).cod_oficina.ToString();
                }
                catch { }
                ddlOficina.Enabled = false;
            }
        }

    }

    protected void LlenarComboZona(DropDownList ddlZona)
    {
        CiudadService ciudadService = new CiudadService();
        Ciudad zona = new Ciudad();
        zona.tipo = 5;
        ddlZona.DataSource = ciudadService.ListarZonas((Usuario)Session["usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboZonaXasesor(DropDownList ddlZona)
    {
        CiudadService ciudadService = new CiudadService();
        Ciudad zona = new Ciudad();
        zona.tipo = 5;
        ddlZona.DataSource = ciudadService.ListarZonas((Usuario)Session["usuario"]);
        ddlZona.DataTextField = "NOMCIUDAD";
        ddlZona.DataValueField = "CODCIUDAD";
        ddlZona.DataBind();
        ddlZona.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboZonasvalidado(DropDownList ddlZona)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = creditoServicio.UsuarioEsEjecutivo(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            LlenarComboZona(ddlZona);
        }
        else
        {
            LlenarComboZonaXasesor(ddlZona);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        Page pagina = new Page();
        dynamic form = new HtmlForm();
        gvListaCreditos.AllowPaging = false;
        gvListaCreditos.DataSource = Session["DTCREDITOS"];
        gvListaCreditos.DataBind();
        //gvListaCreditos.Columns[0].Visible = false;
        gvListaCreditos.EnableViewState = false;
        pagina.EnableEventValidation = false;
        pagina.DesignerInitialize();
        pagina.Controls.Add(form);

        form.Controls.Add(gvListaCreditos);
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=ReporteCreditos.xls");
        Response.Charset = "UTF-8";
        Response.ContentEncoding = Encoding.Default;
        pagina.RenderControl(htw);
        Response.Write(sb.ToString());
        Response.End();
    }

    public DataTable CrearDataTable()
    {
        String filtro = String.Empty;
        String orden = String.Empty;
        System.Data.DataTable table = new System.Data.DataTable();
        List<Creditos> lstConsultaCreditos = new List<Creditos>();
        filtro = obtFiltro();
        orden = obtOrden();
        lstConsultaCreditos = (List<Creditos>)Session["DTCREDITOS"];

        table.Columns.Add("Id_informe");
        table.Columns.Add("Codigo_cliente");
        table.Columns.Add("Numero_radicacion");
        table.Columns.Add("Linea");
        table.Columns.Add("Fecha_solicitud");
        table.Columns.Add("Monto_aprobado");
        table.Columns.Add("Saldo");
        table.Columns.Add("Cuota");
        table.Columns.Add("Plazo");
        table.Columns.Add("Cuotas_pagadas");
        table.Columns.Add("Fecha_proximo_pago");
        table.Columns.Add("Dias_mora");
        table.Columns.Add("Saldo_mora");
        table.Columns.Add("Saldo_atributos_mora");
        table.Columns.Add("Oficina");
        table.Columns.Add("Calif_promedio");
        table.Columns.Add("Calif_cliente");
        table.Columns.Add("Estado");
        table.Columns.Add("Estado_juridico");

        foreach (Creditos item in lstConsultaCreditos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.idinforme;
            datarw[1] = item.codigo_cliente;
            datarw[2] = item.numero_radicacion;
            datarw[3] = item.linea_credito;
            datarw[4] = item.fecha_solicitud_string;
            datarw[5] = item.monto_aprobado.ToString("0,0");
            datarw[6] = item.saldo_capital.ToString("0,0");
            datarw[7] = item.valor_cuota.ToString("0,0");
            datarw[8] = item.plazo;
            datarw[9] = item.cuotas_pagadas;
            datarw[10] = item.fecha_prox_pago_string;
            datarw[11] = item.dias_mora;
            datarw[12] = item.saldo_mora.ToString("0,0");
            datarw[13] = item.saldo_atributos_mora.ToString("0,0");
            datarw[14] = item.oficina;
            datarw[15] = item.calificacion_promedio;
            datarw[16] = item.calificacion_cliente;
            datarw[17] = item.estado;
            datarw[18] = item.estado_juridico;
            table.Rows.Add(datarw);
        }
        return table;
    }

    protected void btnInforme_Click(object sender, EventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["usuario"];

        ReportParameter[] param = new ReportParameter[4];
        param[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now));
        param[1] = new ReportParameter("ImagenReport", ImagenReporte());
        param[2] = new ReportParameter("entidad", pUsuario.empresa);
        param[3] = new ReportParameter("nit", pUsuario.nitempresa);

        ReportViewer1.LocalReport.EnableExternalImages = true;
        ReportViewer1.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", CrearDataTable());
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(rds);
        ReportViewer1.LocalReport.Refresh();

        mvLista.ActiveViewIndex = 1;
    }



    private string obtFiltro()
    {
        String filtro = String.Empty;
        if (ddlZona.SelectedIndex != 0)
            filtro += " and cod_zona = " + ddlZona.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and cod_oficina = " + ddlOficina.SelectedValue;
        if (ddlCalificacion.SelectedIndex != 0 && ddlCalificacion.SelectedIndex != 6)
            filtro += " and calificacion_cliente = " + ddlCalificacion.SelectedValue;
        if (ddlMora.SelectedIndex != 0 && ddlMora.SelectedValue != "10")
            filtro += " and dias_mora between " + ddlMora.SelectedValue;
        if (txtCredito.Text != "")
            filtro += " and numero_radicacion= " + txtCredito.Text;
        if (txtCliente.Text != "")
            filtro += " and cod_persona = " + txtCliente.Text;
        if (txtIdentiCliente.Text != "")
            filtro += " and  identificacion_persona = '" + txtIdentiCliente.Text + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina = '" + txtCodigoNomina.Text + "'";

        if (ddlEstado.SelectedValue == "1")
        {
            filtro += " and (estado = 'Desembolsado' Or estado = 'DESEMBOLSADO') and saldo_capital != 0";
        }
        else if (ddlEstado.SelectedValue == "2")
        {
            filtro += " and (estado = 'Terminado' Or estado = 'TERMINADO')";
        }

        //filtro += " and numero_radicacion not in (select ca.numero_radicacion from cobros_credito ca where ca.numero_radicacion = numero_Radicacion)";


        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " Where " + filtro;
        }
        return filtro;
    }

    private string obtFiltro2()
    {
        String filtro = String.Empty;
        if (ddlZona.SelectedIndex != 0)
            filtro += " and cod_zona= " + ddlZona.SelectedValue;
        if (ddlOficina.SelectedIndex != 0)
            filtro += " and cod_oficina= " + ddlOficina.SelectedValue;
        if (ddlCalificacion.SelectedIndex != 0 && ddlCalificacion.SelectedIndex != 6)
            filtro += " and calificacion_cliente= " + ddlCalificacion.SelectedValue;
        if (ddlMora.SelectedIndex != 0 && ddlMora.SelectedValue != "10" && ddlMora.SelectedValue != "11")
            filtro += " and dias_mora between " + ddlMora.SelectedValue;
        if (ddlMora.SelectedValue == "11")
            filtro += " and dias_mora > 360";
        if (txtCredito.Text != "")
            filtro += " and numero_radicacion= " + txtCredito.Text;
        if (txtCliente.Text != "")
            filtro += " and cod_persona = " + txtCliente.Text;
        if (txtIdentiCliente.Text != "")
            filtro += " and identificacion_persona = '" + txtIdentiCliente.Text + "'";
        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina = '" + txtCodigoNomina.Text + "'";
        if (ddlEstado.SelectedValue == "1")
        {
            filtro += " and (estado = 'Desembolsado' Or estado = 'DESEMBOLSADO') and saldo_capital != 0";
        }
        else if (ddlEstado.SelectedValue == "2")
        {
            filtro += " and (estado = 'Terminado' Or estado = 'TERMINADO')";
        }

        //filtro += " and numero_radicacion not in (select ca.numero_radicacion from cobros_credito ca where ca.numero_radicacion = numero_Radicacion)";

        if (!string.IsNullOrEmpty(filtro))
        {
            filtro = filtro.Substring(4);
            filtro = " where " + filtro;
        }
        return filtro;
    }

    private string obtOrden()
    {
        String orden = String.Empty;
        if (ddlOrdenar.SelectedIndex != 0)
        {
            orden += " order by " + ddlOrdenar.SelectedValue + " asc";
        }
        else
        {
            orden += " order by numero_radicacion asc ";
        }
        return orden;
    }

    private void Actualizar()
    {
        UsuarioAtribucionesService UsuarioAtribucionesServicio = new UsuarioAtribucionesService();
        UsuarioAtribuciones vUsuarioAtribuciones = new UsuarioAtribuciones();
        List<UsuarioAtribuciones> lstAtribuciones = null;
        BtnDescargar.Visible = false;
        BtnDescargarMasivo.Visible = false;
        LblReportes.Visible = false;
        Usuario usuap1 = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap1.codperfil);

        Diligencia credito = new Diligencia();

        credito = creditoServicio.ConsultarparametroUsuarioAsesor((Usuario)Session["usuario"]);
        Int64 codperfilasesor = credito.codigo_parametro;

        int consulta = creditoServicio.usuariopermisos(cod, (Usuario)Session["usuario"]);

        //Si el usuario es administrativo (Si en la tabla Usuario_atribuciones: TipoAtribucion =0 Activo=1) => puede consultar todos los creditos
        Usuario pUsuario = (Usuario)Session["usuario"];
        try
        {
            vUsuarioAtribuciones.codusuario = pUsuario.codusuario;
            lstAtribuciones = UsuarioAtribucionesServicio.ListarUsuarioAtribuciones(vUsuarioAtribuciones, (Usuario)Session["usuario"]);
        }
        catch
        {
            vUsuarioAtribuciones.tipoatribucion = 0;
            vUsuarioAtribuciones.activo = 1;
            lstAtribuciones.Add(vUsuarioAtribuciones);
            //VerError("El usuario no tiene atribuciones definidas");
        }

        try
        {
            String emptyQuery = "Fila de datos vacia";
            String filtro;
            String orden;
            String filtro2;

            List<Creditos> lstConsultaCreditos = new List<Creditos>();
            filtro2 = obtFiltro2();
            filtro = obtFiltro();
            orden = obtOrden();

            int codoficina = Convert.ToInt32(usuap1.cod_oficina);

            bool resultAtribucion = lstAtribuciones.Any(x => (x.tipoatribucion == 0 || x.tipoatribucion == 2) && x.activo == 1);
            //Consulta solo los creditos de su oficina
            if (resultAtribucion || lstAtribuciones.Count <= 0)
            {
                lstConsultaCreditos = creditoServicio.ListarCreditoAsesor(new Creditos(), (Usuario)Session["usuario"], filtro2, orden);

                gvListaCreditos.EmptyDataText = emptyQuery;
                gvListaCreditos.DataSource = lstConsultaCreditos;

                if (lstConsultaCreditos.Count > 0)
                {
                    gvListaCreditos.Visible = true;
                    lblTotalRegs.Visible = true;
                    mvLista.ActiveViewIndex = 0;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCreditos.Count.ToString();
                    if (lstConsultaCreditos.Count < 10)
                        gvListaCreditos.Height = lstConsultaCreditos.Count * 30;
                    gvListaCreditos.DataBind();
                }
                else
                {
                    gvListaCreditos.Visible = false;
                    lblTotalRegs.Visible = false;
                    Response.Write("<script language='JavaScript'>alert('No se encuentra información...!!!');</script>");
                }
            }

            resultAtribucion = lstAtribuciones.Any(x => x.tipoatribucion == 1 || x.tipoatribucion == 4 && x.activo == 1);
            // Consulta créditos de cualquier oficina
            if (resultAtribucion)
            {
                lstConsultaCreditos = creditoServicio.ListarCreditoXOficina(pUsuario.cod_oficina, (Usuario)Session["usuario"], filtro, orden, true);
                gvListaCreditos.EmptyDataText = emptyQuery;
                gvListaCreditos.DataSource = lstConsultaCreditos;
                if (lstConsultaCreditos.Count > 0)
                {
                    gvListaCreditos.Visible = true;
                    lblTotalRegs.Visible = true;
                    mvLista.ActiveViewIndex = 0;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCreditos.Count.ToString();
                    gvListaCreditos.DataBind();
                }
                else
                {
                    gvListaCreditos.Visible = false;
                    lblTotalRegs.Visible = false;
                    Response.Write("<script language='JavaScript'>alert('cliente no pertenece a su oficina...!!!');</script>");
                }
            }

            // Si es un Jefe de Oficina puede consultar todo
            if (cod == codperfilasesor)
            {
                lstConsultaCreditos = creditoServicio.ListarCreditoXAsesor(new Creditos(), (Usuario)Session["usuario"], filtro, orden);
                gvListaCreditos.EmptyDataText = emptyQuery;
                gvListaCreditos.DataSource = lstConsultaCreditos;
                if (lstConsultaCreditos.Count > 0)
                {
                    gvListaCreditos.Visible = true;
                    lblTotalRegs.Visible = true;
                    mvLista.ActiveViewIndex = 0;
                    lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsultaCreditos.Count.ToString();
                    gvListaCreditos.DataBind();
                }
                else
                {
                    gvListaCreditos.Visible = false;
                    lblTotalRegs.Visible = false;
                    Response.Write("<script language='JavaScript'>alert('cliente no pertenece a su usuario...!!!');</script>");

                }
            }
            Session["DTCREDITOS"] = null;
            if (lstConsultaCreditos.Count > 0)
                Session["DTCREDITOS"] = lstConsultaCreditos;
            Session.Add(creditoServicio.CodigoPrograma + ".consulta", 1);
            Session[MOV_GRAL_CRED_PRODUC] = null;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Actualizar", ex);
        }

    }

    private void Actualizar2()
    {
        UsuarioAtribucionesService UsuarioAtribucionesServicio = new UsuarioAtribucionesService();
        UsuarioAtribuciones vUsuarioAtribuciones = new UsuarioAtribuciones();
        Usuario usuap1 = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap1.codperfil);

        Diligencia credito = new Diligencia();

        credito = creditoServicio.ConsultarparametroUsuarioAsesor((Usuario)Session["usuario"]);
        Int64 codperfilasesor = credito.codigo_parametro;

        int consulta = creditoServicio.usuariopermisos(cod, (Usuario)Session["usuario"]);

        //Si el usuario es administrativo (Si en la tabla Usuario_atribuciones: TipoAtribucion =0 Activo=1) => puede consultar todos los creditos
        Usuario pUsuario = (Usuario)Session["usuario"];
        try
        {
            vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(pUsuario.codusuario, (Usuario)Session["usuario"]);
        }
        catch
        {
            vUsuarioAtribuciones.tipoatribucion = 0;
            vUsuarioAtribuciones.activo = 1;
            //VerError("El usuario no tiene atribuciones definidas");
        }

        try
        {
            String filtro;
            String orden;
            String filtro2;

            List<Creditos> lstConsultaCreditos = new List<Creditos>();
            filtro2 = obtFiltro2();
            filtro = obtFiltro();
            orden = obtOrden();

            int codoficina = Convert.ToInt32(usuap1.cod_oficina);
            if (vUsuarioAtribuciones != null)
            {
                //Consulta solo los creditos de su oficina
                if ((vUsuarioAtribuciones.tipoatribucion == 0 || vUsuarioAtribuciones.tipoatribucion == 2) && vUsuarioAtribuciones.activo == 1)
                {
                    lstConsultaCreditos = creditoServicio.ListarCreditoAsesor(new Creditos(), (Usuario)Session["usuario"], filtro2, orden);

                }

                // Consulta créditos de cualquier oficina
                if (vUsuarioAtribuciones.tipoatribucion == 1 && vUsuarioAtribuciones.activo == 1)
                {
                    lstConsultaCreditos = creditoServicio.ListarCreditoXOficina(pUsuario.cod_oficina, (Usuario)Session["usuario"], filtro, orden, true);

                }
            }


            // Si es un Jefe de Oficina puede consultar todo
            if (cod == codperfilasesor)
            {
                lstConsultaCreditos = creditoServicio.ListarCreditoXAsesor(new Creditos(), (Usuario)Session["usuario"], filtro, orden);

            }

            if (lstConsultaCreditos.Count == 0)
                lstConsultaCreditos = creditoServicio.ListarCreditoXOficina(pUsuario.cod_oficina, (Usuario)Session["usuario"], filtro, orden, true);


            Session["DTCREDITOS"] = null;
            if (lstConsultaCreditos.Count > 0)
                Session["DTCREDITOS"] = lstConsultaCreditos;

            Session.Add(creditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(clienteServicio.CodigoPrograma, "Actualizar", ex);
        }

    }



    protected void btnCartaHabeasData_Click(object sender, EventArgs e)
    {

    }

    private String UploadMasivo()
    {
        String saveDir = Server.MapPath("~/Archivos/Diligencias/DiligenciasMasivas/");
        String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));


        string appPath = Request.PhysicalApplicationPath;
        if (FileUploadCargarArchivo.HasFile == true)
        {
            String fileName = Fecha + "-" + FileUploadCargarArchivo.FileName;
            string savePath = saveDir + fileName;

            FileUploadCargarArchivo.SaveAs(savePath);
            return savePath;
        }
        else
        {
            return String.Empty;
        }
    }

    protected void BtnCargar_Click(object sender, EventArgs e)
    {
        LblArchivo.Text = "";
        if (FileUploadCargarArchivo.HasFile == true)
            cargarArchivoMasivo();
        else
        {
            Response.Write("<script language='JavaScript'>alert('Por favor cargar un archivo...!!!');</script>");
        }

    }

    private void cargarArchivoMasivo()
    {
        DiligenciaService diligenciaServicio = new DiligenciaService();
        List<Xpinn.Asesores.Data.PlanoDiligenciaData> listaplano = new List<Xpinn.Asesores.Data.PlanoDiligenciaData>();
        Xpinn.Asesores.Data.PlanoDiligenciaData planoDiligenciaData = null;
        String serverarchivo;
        String linea = "";
        String[] campos;
        int numregistro = 1;
        System.IO.StreamReader archivo;
        Boolean existeerror = false;
        String mensajeerror = "";
        Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();

        serverarchivo = this.UploadMasivo();
        archivo = new System.IO.StreamReader(serverarchivo);
        linea = archivo.ReadLine();
        while (linea != null)
        {
            campos = linea.Split(';');
            planoDiligenciaData = new Xpinn.Asesores.Data.PlanoDiligenciaData();
            try
            {
                planoDiligenciaData.NUMERO_RADICACION1 = campos[0].ToString();
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "NUMERO_RADICACION: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }

            try
            {
                String Fechadiligencia = campos[1].ToString();
                String format = "MM/dd/yyyy";
                planoDiligenciaData.FECHA_DILIGENCIA1 = DateTime.ParseExact(Fechadiligencia, format, CultureInfo.InvariantCulture);


            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "FECHA_DILIGENCIA: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }


            try
            {
                planoDiligenciaData.TIPO_DILIGENCIA1 = Convert.ToInt64(campos[2].ToString());
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "TIPO_DILIGENCIA: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }


            try
            {
                planoDiligenciaData.ATENDIO1 = (campos[3].ToString());
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "ATENDIO: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }


            try
            {
                planoDiligenciaData.RESPUESTA1 = (campos[4].ToString());
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "RESPUESTA: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }


            try
            {
                //; planoDiligenciaData.FECHA_ACUERDO1 = DateTime.ParseExact(campos[5].ToString(), "dd/MMM/yy hh:mm:ss tt", System.Globalization.CultureInfo.GetCultureInfo("en-US"), System.Globalization.DateTimeStyles.None);
                String fechaacuerdo;
                fechaacuerdo = campos[6].ToString();

                String fechavalidacion1 = "";
                if (fechaacuerdo == fechavalidacion1)
                {
                    String Fechaacuerdo = "01/01/0001";
                    String format = "dd/MM/yyyy";
                    planoDiligenciaData.FECHA_ACUERDO1 = DateTime.ParseExact(Fechaacuerdo, format, CultureInfo.InvariantCulture);
                }
                else
                {
                    String Fecha_Acuerdo1 = campos[6];
                    planoDiligenciaData.FECHA_ACUERDO1 = DateTime.ParseExact(Fecha_Acuerdo1, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {

                planoDiligenciaData.Mensaje1 = "FECHA_ACUERDO: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }

            try
            {
                String fechaacuerdo;
                fechaacuerdo = campos[7].ToString();
                String fechavalidacion1 = "";

                if (fechaacuerdo == fechavalidacion1)
                {
                    planoDiligenciaData.VALOR_ACUERDO1 = 0;
                }
                else
                {
                    planoDiligenciaData.VALOR_ACUERDO1 = Convert.ToInt64(campos[7].ToString());
                }
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "VALOR_ACUERDO: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }


            try
            {
                planoDiligenciaData.OBSERVACION1 = (campos[5].ToString());
            }
            catch (Exception ex)
            {
                planoDiligenciaData.Mensaje1 = "OBSERVACION: " + ex.Message + "\n " + planoDiligenciaData.Mensaje1;
            }

            planoDiligenciaData.Numregistro1 = numregistro;
            listaplano.Add(planoDiligenciaData);
            numregistro++;
            linea = archivo.ReadLine();
        }
        archivo.Close();
        foreach (Xpinn.Asesores.Data.PlanoDiligenciaData plano in listaplano)
        {
            if (plano.Mensaje1.Trim().Length > 0)
            {
                existeerror = true;
                mensajeerror = plano.Mensaje1 + "\n" + mensajeerror;
            }
        }
        if (!existeerror)
        {

            foreach (Xpinn.Asesores.Data.PlanoDiligenciaData plano in listaplano)
            {
                vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
                vDiligencia.numero_radicacion = Convert.ToInt64(plano.NUMERO_RADICACION1);
                vDiligencia.fecha_diligencia = plano.FECHA_DILIGENCIA1;


                vDiligencia.tipo_diligencia = plano.TIPO_DILIGENCIA1;
                vDiligencia.atendio = "";
                vDiligencia.respuesta = plano.RESPUESTA1;
                vDiligencia.observacion = plano.OBSERVACION1;
                vDiligencia.fecha_acuerdo = plano.FECHA_ACUERDO1;
                vDiligencia.valor_acuerdo = plano.VALOR_ACUERDO1;

                vDiligencia.acuerdo = 0;
                vDiligencia.anexo = "";
                vDiligencia.tipo_contacto = Convert.ToInt32(plano.ATENDIO1);
                Usuario usuap = (Usuario)Session["usuario"];
                int cod = Convert.ToInt32(usuap.codusuario);
                vDiligencia.codigo_usuario_regis = cod;
                diligenciaServicio.CrearDiligencia(vDiligencia, (Usuario)Session["usuario"]);
                this.LblReportes.Text = "Archivo cargado exitosamente: Número de registros procesados: " + numregistro.ToString();

            }

        }
        else
        {
            this.LblReportes.Text = mensajeerror;
        }
    }

    protected void btnCargaMasiva_Click(object sender, EventArgs e)
    {
        mpeEstructuraArchivo.Show();
    }

    protected void btnCloseEstructura_Click(object sender, EventArgs e)
    {
        mpeEstructuraArchivo.Hide();
    }

    protected void btnActualizarDiligencias_Click(object sender, EventArgs e)
    {
        DiligenciaService diligenciaServicio = new DiligenciaService();
        Xpinn.Asesores.Entities.Diligencia vDiligencia = new Xpinn.Asesores.Entities.Diligencia();
        diligenciaServicio.ActualizarEstadosDiligencia(vDiligencia, (Usuario)Session["usuario"]);
        if (vDiligencia.codigo_diligencia == 1)
        {
            this.LblReportes.Text = "Diligencias actualizadas exitosamente";
        }
        else
        {
            this.LblReportes.Text = "Error al momento de actualizar";
        }
    }

    protected void btnHabeasData_Click(object sender, EventArgs e)
    {

    }

    private void ConvertPdf(string html, string pDocumentoGenerado)
    {
        Document doc;
        using (var ms = new MemoryStream())
        {
            //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            using (doc = new Document(PageSize.A4, 80f, 60f, 35f, 35f))
            {

                //Create a writer that's bound to our PDF abstraction and our stream
                using (var writer = PdfWriter.GetInstance(doc, new FileStream(pDocumentoGenerado, FileMode.OpenOrCreate)))
                {
                    //Open the document for writing
                    doc.Open();

                    //Create a new HTMLWorker bound to our document
                    using (var htmlWorker = new HTMLWorker(doc))
                    {

                        //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                        using (var sr = new StringReader(html.Replace(@"^data:image\/[a-zA-Z]+;base64,", string.Empty)))
                        {

                            var tags = new HTMLTagProcessors();
                            // Replace the built-in image processor
                            tags[HtmlTags.IMG] = new CustomImageHTMLTagProcessor();
                            try
                            {
                                List<IElement> ie = HTMLWorker.ParseToList(sr, null, tags, null);
                                foreach (IElement element in ie)
                                {
                                    try
                                    {
                                        doc.Add((IElement)element);
                                    }
                                    catch (Exception ex) { VerError(ex.Message); }
                                }
                            }
                            catch (Exception ex) { VerError(ex.Message); }
                        }
                    }
                    doc.Close();
                }
            }
        }

    }

    private MemoryStream ConvertPdfs(string html)
    {
        Document doc;
        using (var ms = new MemoryStream())
        {
            //Create an iTextSharp Document which is an abstraction of a PDF but **NOT** a PDF
            using (doc = new Document(PageSize.A4, 80f, 60f, 35f, 35f))
            {

                //Create a writer that's bound to our PDF abstraction and our stream
                using (var writer = PdfWriter.GetInstance(doc, ms))
                {

                    //Open the document for writing
                    doc.Open();

                    //Create a new HTMLWorker bound to our document
                    using (var htmlWorker = new HTMLWorker(doc))
                    {

                        //HTMLWorker doesn't read a string directly but instead needs a TextReader (which StringReader subclasses)
                        using (var sr = new StringReader(html.Replace(@"^data:image\/[a-zA-Z]+;base64,", string.Empty)))
                        {

                            var tags = new HTMLTagProcessors();
                            // Replace the built-in image processor
                            tags[HtmlTags.IMG] = new CustomImageHTMLTagProcessor();
                            try
                            {
                                List<IElement> ie = HTMLWorker.ParseToList(sr, null, tags, null);
                                foreach (IElement element in ie)
                                {
                                    try
                                    {
                                        doc.Add((IElement)element);
                                    }
                                    catch (Exception ex) { VerError(ex.Message); }
                                }
                            }
                            catch (Exception ex) { VerError(ex.Message); }
                        }
                    }
                    doc.Close();
                    return ms;
                }
            }
        }

    }

    private void iReemplazarEnDocumentoDeWordYGuardarPDF(string pTexto, List<DatosDeDocumento> plstReemplazos, string pDocumentoGenerado, string name)
    {

        // Validar que exista el texto
        if (pTexto.Trim().Length <= 0)
            return;
        // Hacer los reemplazos de los campos
        foreach (DatosDeDocumento dFila in plstReemplazos)
        {
            try
            {
                string cCampo = dFila.Campo.ToString().Trim();
                string cValor = "";
                if (dFila.Valor != null)
                    cValor = dFila.Valor.ToString().Trim().Replace("'", "");
                else
                    cValor = "";
                pTexto = pTexto.Replace(cCampo, cValor).Replace("'", "");
            }
            catch
            {
            }
        }

      
        ConvertPdf(pTexto, pDocumentoGenerado);

        string sourceFile = pDocumentoGenerado;
        nombrearchivo = name + Convert.ToString(credito) + ".pdf";
        string destino = Server.MapPath("~/Page/Asesores/Recuperacion/Cartas/Cartas.zip");
        AddFileToZip(destino, sourceFile, false);
        BtnDescargar.Visible = true;
        BtnDescargarMasivo.Visible = true;
        LblReportes.Visible = true;
        LblReportes.Text = "Los documentos fueron generados,por favor descargarlos";
        //File.Delete(Server.MapPath("~/Page/Asesores/Recuperacion/Cartas/"));
    }

    private void iReemplazarEnDocumentoDeWordYGuardarPDF(string pTexto, List<List<DatosDeDocumento>> plstReemplazos, string pDocumentoGenerado, string name)
    {
        // Validar que exista el texto
        if (pTexto.Trim().Length <= 0)
            return;

        string html;
        List<string> Htmls = new List<string>();
        foreach (var item in plstReemplazos)
        {
            html = "<br /><br />" + pTexto;
            // Hacer los reemplazos de los campos
            foreach (DatosDeDocumento dFila in item)
            {
                try
                {
                    string cCampo = dFila.Campo.Trim();
                    string cValor = "";
                    cValor = dFila.Valor != null ? dFila.Valor.Trim().Replace("'", "") : "";
                    html = html.Replace(cCampo, cValor).Replace("'", "");

                }
                catch (Exception)
                {
                    //
                }
            }
            // html = pTexto.Replace("'", "");
            Htmls.Add(html);
        }

        List<byte[]> pdfBytes = new List<byte[]>();
        foreach (var item in Htmls)
        {
            StringReader sr = new StringReader(item);
            Document pdfDoc = new Document(PageSize.A4, 22f, 12f, 12f, 12f);


            pdfBytes.Add(ConvertPdfs(item).ToArray());
        }

        byte[] all;

        using (MemoryStream ms = new MemoryStream())
        {
            Document doc = new Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.SetPageSize(PageSize.LETTER);
            doc.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;

            PdfReader reader;
            foreach (byte[] p in pdfBytes)
            {
                reader = new PdfReader(p);
                int pages = reader.NumberOfPages;

                for (int i = 1; i <= pages; i++)
                {
                    doc.SetPageSize(PageSize.LETTER);
                    doc.NewPage();
                    page = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page, 0, 0);
                }
            }

            doc.Close();
            all = ms.GetBuffer();
            ms.Flush();
            ms.Dispose();
        }

        File.WriteAllBytes(pDocumentoGenerado, all);

        string sourceFile = pDocumentoGenerado;
        nombrearchivo = name + Convert.ToString(credito) + ".pdf";
        string destino = Server.MapPath("~/Page/Asesores/Recuperacion/Cartas/Cartas.zip");
        AddFileToZip(destino, sourceFile, false);
        BtnDescargar.Visible = true;
        BtnDescargarMasivo.Visible = true;
        LblReportes.Visible = true;
        LblReportes.Text = "Los documentos fueron generados,por favor descargarlos";
        //File.Delete(Server.MapPath("~/Page/Asesores/Recuperacion/Cartas/"));
    }

    private static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
    {
        long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
        byte[] buffer = new byte[bufferSize];
        int bytesRead = 0;
        long bytesWritten = 0;
        while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            outputStream.Write(buffer, 0, bytesRead);
            bytesWritten += bufferSize;
        }
    }

    private static void AddFileToZip(string zipFilename, string fileToAdd, bool IsFolder)
    {
        if (IsFolder == false)
        {
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + Path.GetFileName(fileToAdd);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = part.GetStream())
                    {
                        CopyStream(fileStream, dest);
                        dest.Close();
                    }
                    fileStream.Close();
                }
            }
            return;
        }
    }

    protected void ddlTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Solicitando la información  del tipo de documento para saber si existe el tipo documento 0 
        ctlMensaje.MostrarMensaje("Desea generar las cartas estas quedaran guardadas en C:/cartas/ ");

    }

    protected void btnCartas_Click(object sender, EventArgs e)
    {
        ddlTipoDocumento_SelectedIndexChanged(ddlTipoDocumento, null);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        TiposDocumentoService tipoDocumentoServicio = new TiposDocumentoService();
        string nombre;
        TiposDocumento vTipoDoc = new TiposDocumento();
        List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();
        List<Creditos> lstConsultaCreditos = new List<Creditos>();

        if (ddlTipoDocumento.SelectedValue != "0")
        {
            Session["texto"] = null;
            vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumentoCobranzas(Convert.ToInt64(ddlTipoDocumento.SelectedValue), (Usuario)Session["usuario"]);

            if (vTipoDoc.tipo_documento != 0) nombre = ddlTipoDocumento.SelectedItem.Text;
            //Buscar los datos 
            Actualizar2();
            lstConsultaCreditos = (List<Creditos>)Session["DTCREDITOS"];
            string pathString = Server.MapPath("~/Page/Asesores/Recuperacion/Cartas/");

            if (!Directory.Exists(pathString)) Directory.CreateDirectory(pathString);

            string[] filePaths = Directory.GetFiles(pathString);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    // the following entry will be inserted at the root in the archive.
                    zip.AddFile("~/Page/Asesores/Recuperacion/Cartas/Cartas.txt", "");
                    // this image file will be inserted into the "images" directory in the archive.

                    zip.Save("Cartas.zip");
                }
            }
            catch (Exception ex1)
            {
                Console.Error.WriteLine("exception: {0}", ex1);
            }
            nombre = ddlTipoDocumento.SelectedItem.Text;
            DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
            List<List<DatosDeDocumento>> dct = new List<List<DatosDeDocumento>>();

            foreach (Creditos rFila in lstConsultaCreditos)
            {
                credito = rFila.numero_radicacion;
                string identificacion = rFila.identificacion;
                Persona1Service Persona1Servicio = new Persona1Service();
                List<Persona1> lstConsultas = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(credito.ToString()), Usuario);
                CobrosCreditoService cobroCreditoServicio = new CobrosCreditoService();
                CobrosCredito cobroCredito = new CobrosCredito();
                cobroCredito = cobroCreditoServicio.ConsultarCobrosCredito(rFila.numero_radicacion, (Usuario)Session["usuario"]);
                if (cobroCredito.numero_radicacion == 0)
                {
                    //NUMERO DE CREDITO Seleccionado       
                    string cDocumentoGenerado = Server.MapPath("~/Page/Asesores/Recuperacion" + cDocsSubDir + "/" + nombre + "_" + identificacion + "_Credito_" + credito + '.' + 'p' + 'd' + 'f');
                    // Solicitando la información que debe ser mostrada en el documento parametrizado 
                    if (ddlTipoDocumento.SelectedValue != "10")
                    {
                        lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormatoCartasMasivas(Convert.ToInt64(credito), (Usuario)Session["usuario"]);
                        if (!string.IsNullOrEmpty(vTipoDoc.texto))
                            iReemplazarEnDocumentoDeWordYGuardarPDF(vTipoDoc.texto, lstDatosDeDocumento, cDocumentoGenerado, nombre);
                        else
                        {
                            iReemplazarEnDocumentoDeWordYGuardarPDF(Encoding.ASCII.GetString(vTipoDoc.Textos), lstDatosDeDocumento, cDocumentoGenerado, nombre);
                        }
                    }
                    else
                    {
                        if (lstConsultas.Count > 0)
                        {
                            foreach (Persona1 rFilas in lstConsultas)
                            {
                                string cDocumentoGenerados = Server.MapPath("~/Page/Asesores/Recuperacion" + cDocsSubDir + "/" + nombre + "_" + identificacion + "_Credito_" + credito + "_Codeudor_" + rFilas.cod_persona + '.' + "pdf");
                                lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(Convert.ToInt64(credito), (Usuario)Session["usuario"], rFilas.cod_persona);
                                if (!string.IsNullOrEmpty(vTipoDoc.texto))
                                    iReemplazarEnDocumentoDeWordYGuardarPDF(vTipoDoc.texto, lstDatosDeDocumento, cDocumentoGenerados, nombre);
                                else
                                {
                                    iReemplazarEnDocumentoDeWordYGuardarPDF(Encoding.ASCII.GetString(vTipoDoc.Textos), lstDatosDeDocumento, cDocumentoGenerados, nombre);
                                }
                            }
                        }
                    }
                    dct.Add(lstDatosDeDocumento);
                }
            }
            if (!string.IsNullOrEmpty(vTipoDoc.texto))
                iReemplazarEnDocumentoDeWordYGuardarPDF(vTipoDoc.texto, dct, Server.MapPath("~/Page/Asesores/Recuperacion/" + cDocsSubDir + "/CartasGenerales.pdf"), nombre);
            else
                iReemplazarEnDocumentoDeWordYGuardarPDF(Encoding.ASCII.GetString(vTipoDoc.Textos), dct, Server.MapPath("~/Page/Asesores/Recuperacion/" + cDocsSubDir + "/CartasGenerales.pdf"), nombre);
        }

    }

    Persona1 ObtenerValoresCodeudores(string numeroRadicacion)
    {
        Persona1 vPersona1 = new Persona1();

        if (numeroRadicacion != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(numeroRadicacion);

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }
    protected void btnConsultar_Click1(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea generar las cartas estas quedaran guardadas en C:/cartas/ ");

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Navegar("..//Recuperacion/cartas/cartas.zip");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Navegar("..//Recuperacion/cartas/CartasGenerales.pdf");
    }

    protected void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        LblReportes.Text = "Los documentos fueron generados,por favor descargarlos";

    }
    public class CustomImageHTMLTagProcessor : IHTMLTagProcessor
    {
        /// <summary>
        /// Tells the HTMLWorker what to do when a close tag is encountered.
        /// </summary>
        public void EndElement(HTMLWorker worker, string tag)
        {
        }

        /// <summary>
        /// Tells the HTMLWorker what to do when an open tag is encountered.
        /// </summary>
        public void StartElement(HTMLWorker worker, string tag, IDictionary<string, string> attrs)
        {
            iTextSharp.text.Image image;
            var src = attrs["src"];

            if (src.StartsWith("data:image/"))
            {
                // data:[<MIME-type>][;charset=<encoding>][;base64],<data>
                var base64Data = src.Substring(src.IndexOf(",") + 1);
                var imagedata = Convert.FromBase64String(base64Data);
                image = iTextSharp.text.Image.GetInstance(imagedata);
            }
            else
            {
                image = iTextSharp.text.Image.GetInstance(src);
            }

            worker.UpdateChain(tag, attrs);
            worker.ProcessImage(image, attrs);
            worker.UpdateChain(tag);
        }
    }

}