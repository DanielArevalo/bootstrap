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
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Comun.Entities;

partial class Lista : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlTiemposServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    String HojaRuta = "";
    private Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ControlTiemposServicio.CodigoProgramaHojaRuta, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ViewState["NumeroCredito"] = "0";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["Usuario"];

            if (!IsPostBack)
            {
                CargarListas();
                LlenarComboOficinas(ddlOficina);
                CargarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
                if (Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".consulta"] != null)
                {
                    Actualizar();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            String ListaSolicitada = null;

            ListaSolicitada = "Asesor";
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = ControlTiemposServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
            DdlResponsable.DataSource = lstDatosSolicitud;
            DdlResponsable.DataTextField = "ListaDescripcion";
            DdlResponsable.DataValueField = "ListaId";
            DdlResponsable.DataBind();
            DdlResponsable.Items.Add("");
            DdlResponsable.Text = "";

            ListaSolicitada = "EstadoProceso";
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = ControlTiemposServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();
            ddlEstado.Items.Add("");
            ddlEstado.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
        Navegar(Pagina.Nuevo);
    }



    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        gvLista.DataSourceID = null;
        gvLista.DataBind();
        gvLista.Visible = false;
        lblTotalRegs.Visible = false;
        LimpiarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
        txtIdenificacion.Text = "";
        DdlResponsable.Text = "";
        ddlEstado.SelectedIndex = 0;
    }



    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[2].Text;
        String identificacion = gvLista.SelectedRow.Cells[5].Text;
        String proceso = gvLista.SelectedRow.Cells[3].Text;
        String SiguienteProceso = gvLista.SelectedRow.Cells[4].Text;
        String FechaDatacredito = gvLista.SelectedRow.Cells[8].Text;
        String Estado = gvLista.SelectedRow.Cells[12].Text;

        Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".id"] = id;
        Session["Numeroidentificacion"] = identificacion;
        Session["NumeroCredito"] = id;
        Session["Proceso"] = proceso;
        Session["Datacredito"] = FechaDatacredito;
        Session["Estado"] = Estado;
        Session["SiguienteProceso"] = SiguienteProceso;
        Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".id"] = id;
        Navegar(Pagina.Editar);

    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[2].Text;
        String identificacion = gvLista.Rows[e.NewEditIndex].Cells[5].Text;
        String proceso = gvLista.Rows[e.NewEditIndex].Cells[3].Text;
        String FechaDatacredito = gvLista.Rows[e.NewEditIndex].Cells[8].Text;
        String Estado = gvLista.Rows[e.NewEditIndex].Cells[12].Text;
        String SiguienteProceso = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".id"] = id;

        HyperLink HyperConsulta = (HyperLink)gvLista.Rows[e.NewEditIndex].FindControl("HyperConsulta");
        ImageButton btnurl = (ImageButton)gvLista.Rows[e.NewEditIndex].FindControl("btnurl");
        HyperConsulta.Enabled = false;

        if (HyperConsulta != null)
        {
            if (HyperConsulta.Visible == true)
            {
                if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Aprobado" && gvLista.Rows[e.NewEditIndex].Cells[12].Text == "Aprobado")
                {
                    Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                    Session[creditoServicio.CodigoPrograma + ".id"] = id;
                    Session["Datacredito"] = FechaDatacredito;
                }
                if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Solicitado" || gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Referencias Verificadas" || gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Aprobado" || gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Documentos Generados" || gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Desembolsado" || gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Pre Aprobado")
                {
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Referencias Verificadas")
                    {
                        Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();
                        Session[ReferenciaServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Scoring Realizado")
                    {
                        CreditoService _creditoService = new CreditoService();
                        Session[_creditoService.CodigoProgramaAnalisisCredito + ".id"] = id;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Aprobado")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
                        Session[creditoServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Documentos Generados")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                        Session[creditoServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Desembolsado")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                        Session[creditoServicio.CodigoProgramaoriginal + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Check List Positivo Operaciones" && gvLista.Rows[e.NewEditIndex].Cells[12].Text == "Aprobado")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
                        Session[creditoServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Check List Positivo Operaciones" && gvLista.Rows[e.NewEditIndex].Cells[12].Text == "Referencias Verificadas")
                    {
                        Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
                        Session[creditoServicio.CodigoPrograma + ".id"] = id;
                        Session["Datacredito"] = FechaDatacredito;
                    }
                    //Agregado para proceso Pre Aprobado
                    if (gvLista.Rows[e.NewEditIndex].Cells[4].Text == "Pre Aprobado")
                    {
                        CreditoService _creditoService = new CreditoService();
                        Session[_creditoService.CodigoProgramaAnalisisCredito + ".id"] = Convert.ToInt64(id);
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
            Session["SiguienteProceso"] = SiguienteProceso;
            Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".id"] = id;
            Navegar(Pagina.Editar);
        }
        else
        {
            Session["Numeroidentificacion"] = identificacion;
            Session["NumeroCredito"] = id;
            Session["Proceso"] = proceso;
            Session["Datacredito"] = FechaDatacredito;
            Session["Estado"] = Estado;
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
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "gvLista_RowDeleting", ex);
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
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "gvLista_PageIndexChanging", ex);
        }
    }

    private void Actualizar()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();
            lstConsulta = ControlTiemposServicio.ListarControlTiempos(ObtenerValores(), (Usuario)Session["usuario"]);

            foreach(ControlTiempos control in lstConsulta)
            {
                if(control.nom_proceso == "Negado")
                {
                    control.sig_proceso_nom = "Fin tiempos de Respuesta";
                    control.nom_tipo_proceso = "Manual";
                    control.sig_proceso_tipo = "2";
                }
                if (control.nom_proceso == "Aplazado")
                {
                    control.sig_proceso_nom = "Fin tiempos de Respuesta";
                    control.nom_tipo_proceso = "Manual";
                    control.sig_proceso_tipo = "2";
                }
            }

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

            Session.Add(ControlTiemposServicio.CodigoProgramaHojaRuta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Actualizar", ex);
        }
    }

    private Xpinn.FabricaCreditos.Entities.ControlTiempos ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones vUsuarioAtribuciones = new Xpinn.FabricaCreditos.Entities.UsuarioAtribuciones();
        Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService UsuarioAtribucionesServicio = new Xpinn.FabricaCreditos.Services.UsuarioAtribucionesService();
        Xpinn.FabricaCreditos.Entities.ControlTiempos vControlTiempos = new Xpinn.FabricaCreditos.Entities.ControlTiempos();

        Usuario pUsuario = (Usuario)Session["usuario"];
        vUsuarioAtribuciones = UsuarioAtribucionesServicio.ConsultarUsuarioAtribuciones(pUsuario.codusuario, (Usuario)Session["usuario"]);

        if (ddlOficina.Text.Trim() != "")
            vControlTiempos.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
        else  // Consulta solo los creditos de la oficina del usuario logueado
        {
            vControlTiempos.cod_oficina = pUsuario.cod_oficina;
        }

        if (DdlResponsable.Text.Trim() != "")
            vControlTiempos.encargado = Convert.ToString(DdlResponsable.SelectedItem.Text);

        if (ddlEstado.Text.Trim() != "")
            vControlTiempos.ultimoproceso = Convert.ToString(ddlEstado.SelectedItem.Text);

        if (txtNumeroCredito.Text.Trim() != "")
            vControlTiempos.numerocredito = Convert.ToString(txtNumeroCredito.Text.Trim());
        if (txtIdenificacion.Text.Trim() != "")
            vControlTiempos.identificacion = Convert.ToString(txtIdenificacion.Text.Trim());
        if (txtCodigoNomina.Text.Trim() != "")
            vControlTiempos.cod_nomina += Convert.ToString(txtCodigoNomina.Text.Trim());

        if (txtFechaProceso.Text.Trim() != "")
            vControlTiempos.fechaproceso += Convert.ToString(txtFechaProceso.Text.Trim());


        return vControlTiempos;
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
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
        Actualizar();
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HojaRuta = (String)Session["HojaRuta"];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink HyperConsulta = (HyperLink)e.Row.FindControl("HyperConsulta");
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            ImageButton btnInfo = (ImageButton)e.Row.FindControl("btnInfo");
            String radicado = e.Row.Cells[2].Text;
            if (e.Row.Cells[4].Text == "Solicitado" || e.Row.Cells[4].Text == "Referencias Verificadas" || e.Row.Cells[4].Text == "Scoring Realizado" || e.Row.Cells[4].Text == "Aprobado" || e.Row.Cells[4].Text == "Documentos Generados" || e.Row.Cells[4].Text == "Desembolsado" || e.Row.Cells[4].Text == "Pre Aprobado" || e.Row.Cells[4].Text == "Control Documentos")
            {
                if (btnEditar != null)
                    btnEditar.Visible = true;
                if (btnInfo != null)
                    btnInfo.Visible = false;
                if (e.Row.Cells[4].Text == "Referencias Verificadas")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "../../FabricaCreditos/Referenciacion/Nuevo.aspx?radicado=" + radicado;
                }
                if (e.Row.Cells[4].Text == "Scoring Realizado")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "../../FabricaCreditos/Analisis/Nuevo.aspx?radicado=" + radicado;
                }
                if (e.Row.Cells[4].Text == "Aprobado")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "../../FabricaCreditos/CreditosPorAprobar/Detalle.aspx?radicado=" + radicado;
                }
                if (e.Row.Cells[4].Text == "Documentos Generados")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "..//../FabricaCreditos/GeneracionDocumentos/Nuevo.aspx?radicado=" + radicado;
                }
                if (e.Row.Cells[4].Text == "Desembolsado")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "..//../FabricaCreditos/Desembolso/Nuevo.aspx?radicado=" + radicado;
                }
                //Agregado para Pre Aprobado
                if (e.Row.Cells[4].Text == "Pre Aprobado")
                {
                    HyperConsulta.Visible = true;
                    HyperConsulta.NavigateUrl = "../../FabricaCreditos/Analisis/Nuevo.aspx?radicado=" + radicado;
                }
                //Agregado para Control Documentos
                if (e.Row.Cells[4].Text == "Control Documentos")
                {
                    HyperConsulta.Visible = true;                                        
                    HyperConsulta.NavigateUrl = "../../FabricaCreditos/ControlDocumentos/Nuevo.aspx?o=E&radicado=" + radicado;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(e.Row.Cells[4].Text) || e.Row.Cells[4].Text == "&nbsp;")
                {
                    HyperConsulta.Visible = false;
                    if (btnEditar != null)
                        btnEditar.Visible = false;
                    if (btnInfo != null)
                        btnInfo.Visible = true;
                }
                else
                {
                    HyperConsulta.Visible = false;
                    if (btnEditar != null)
                        btnEditar.Visible = false;
                    if (btnInfo != null)
                        btnInfo.Visible = true;
                }
            }
        }

        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta + "L", "gvLista_RowDataBound", ex);
        }
    }

    /// <summary>
    /// LLenar el dropdownlist que permite filtras por oficinas
    /// </summary>
    /// <param name="ddlOficina"></param>
    protected void LlenarComboOficinas(DropDownList ddlOficina)
    {
        OficinaService oficinaService = new OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();
        Usuario usuap = (Usuario)Session["usuario"];
        int cod = Convert.ToInt32(usuap.codusuario);

        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        if (consulta >= 1)
        {
            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "codigo";
            ddlOficina.DataBind();
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
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

    protected void ddlOficina_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar();
    }

    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            ButtonGrid gvButton = sender as ButtonGrid;
            int index = Convert.ToInt32(gvButton.CommandArgument);
            string identificacion = gvLista.Rows[index].Cells[5].Text;
            string numeroRadicacion = gvLista.Rows[index].Cells[2].Text;

            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(_usuario.idEmpresa, _usuario);

            Xpinn.Asesores.Entities.Persona persona = _tipoDocumentoServicio.ConsultarCorreoPersona(0, _usuario, identificacion);

            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)TipoDocumentoCorreo.HojaRuta, _usuario);

            if (string.IsNullOrWhiteSpace(persona.Email))
            {
                VerError("El asociado no tiene email registrado");
                return;
            }
            else if (string.IsNullOrWhiteSpace(empresa.e_mail) || string.IsNullOrWhiteSpace(empresa.clave_e_mail))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }

            LlenarDiccionarioGlobalWebParaCorreo(Convert.ToInt64(numeroRadicacion));

            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

            CorreoHelper correoHelper = new CorreoHelper(persona.Email, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, Correo.Gmail, modificardocumento.descripcion, _usuario.empresa);

            if (!exitoso)
            {
                VerError("Error al enviar el correo");
                return;
            }

            gvButton.Text = "Envio Satisfactorio";
            gvButton.Enabled = false;
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }

    private void LlenarDiccionarioGlobalWebParaCorreo(long numeroRadicacion)
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();
        
        CreditoService creditoService = new CreditoService();
        Credito credito = creditoService.ConsultarCredito(Convert.ToInt64(numeroRadicacion), _usuario);

        parametrosFormatoCorreo.Add(ParametroCorreo.NumeroRadicacion, numeroRadicacion.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, credito.identificacion);
        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, credito.nombre);
        parametrosFormatoCorreo.Add(ParametroCorreo.FechaCredito, credito.fecha_aprobacion.HasValue ? credito.fecha_aprobacion.Value.ToShortDateString() : " ");
        parametrosFormatoCorreo.Add(ParametroCorreo.PlazoCredito, credito.plazo.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.MontoCredito, credito.monto.ToString());
        parametrosFormatoCorreo.Add(ParametroCorreo.LineaCredito, credito.linea_credito);
    }
}