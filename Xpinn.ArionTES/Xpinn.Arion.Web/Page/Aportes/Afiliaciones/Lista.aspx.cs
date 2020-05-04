using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Comun.Entities;
using Xpinn.Asesores.Entities;

partial class Lista : GlobalWeb
{
    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Usuario _usuario;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoNuevo += btnNuevo_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];
            if (!IsPostBack)
            {
                Session["lstNatural"] = null;
                Session["lstJuridica"] = null;
                Session["lstCtaBancarias"] = null;
                Session["cod_per"] = null;
                Session[Usuario.codusuario + "Cod_persona"] = null;
                LimpiarLlamadoEstadoCuenta();
                CargarListas();
                CargaFormatosFecha();
                ucFecha.Text = DateTime.Now.ToShortDateString();
                CargarValoresConsulta(pConsulta, AfiliacionServicio.CodigoPrograma);
                ViewState["CurrentAlphabet"] = "TODO";
                GenerateAlphabets();
                LlenarComboPageSize();

                if (Session[AfiliacionServicio.CodigoPrograma + ".nid"] != null)
                {
                    txtNumeIdentificacion.Text = Session[AfiliacionServicio.CodigoPrograma + ".nid"].ToString();
                    Session.Remove(AfiliacionServicio.CodigoPrograma + ".nid");
                    Actualizar(0);
                }
                if (Session[AfiliacionServicio.CodigoPrograma + ".consulta"] != null)
                    Actualizar(0);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void LimpiarLlamadoEstadoCuenta()
    {
        Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
            Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
        Session.Remove(AfiliacionServicio.CodigoPrograma + ".modificar");
        GuardarValoresConsulta(pConsulta, AfiliacionServicio.CodigoPrograma);
        Navegar(Pagina.Nuevo);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            GuardarValoresConsulta(pConsulta, AfiliacionServicio.CodigoPrograma);
            Actualizar(0);
        }
        else
        {
            Session["lstNatural"] = null;
            Session["lstJuridica"] = null;
            Session["lstCtaBancarias"] = null;
            mvPrincipal.ActiveViewIndex = 0;
            Site toolBar = (Site)Master;
            toolBar.MostrarLimpiar(true);
            toolBar.MostrarExportar(true);
            toolBar.MostrarImportar(true);
            toolBar.MostrarNuevo(true);
            toolBar.MostrarGuardar(false);
        }
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (mvPrincipal.ActiveViewIndex == 0)
        {
            txtNumeIdentificacion.Text = "";
            LimpiarValoresConsulta(pBusqueda, AfiliacionServicio.CodigoPrograma);
        }
        else
        {
            LimpiarDataImportacion();
        }
    }

    protected void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (gvLista.Rows.Count > 0 && Session["DTPERSONAS"] != null)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pagina = new Page();
            dynamic form = new HtmlForm();
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[2].Visible = false;
            gvLista.AllowPaging = false;
            gvLista.DataSource = Session["DTPERSONAS"];
            gvLista.DataBind();
            gvLista.EnableViewState = false;
            pagina.EnableEventValidation = false;
            pagina.DesignerInitialize();
            pagina.Controls.Add(form);
            form.Controls.Add(gvLista);
            pagina.RenderControl(htw);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=Personas.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            VerError("No existen datos, genere la consulta");
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnEliminar");
            Label lblBiometria = (Label)e.Row.FindControl("lblBiometria");
            if (lblBiometria != null)
            {
                if (lblBiometria.Text.Trim() != "")
                {
                    Int32 indicador = Convert.ToInt32(lblBiometria.Text);
                    if (indicador > 0)
                    {
                        Image imgBiometria = (Image)e.Row.FindControl("imgBiometria");
                        if (imgBiometria != null)
                        {
                            imgBiometria.Visible = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
        Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 1;
        String tipo_persona = gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Detalle);
        else
        {
            PersonaResponsable pResponsable = new PersonaResponsable();
            string pFiltro = "WHERE R.COD_PERSONA = " + id;
            pResponsable = AfiliacionServicio.ConsultarPersonaResponsable(pFiltro, (Usuario)Session["usuario"]);
            //if (pResponsable != null && pResponsable.consecutivo != 0 && pResponsable.cod_persona_tutor != 0)
            //    Navegar("../Personas/NuevoMDE.aspx");
            //else
            //    Navegar("../Personas/Nuevo.aspx?" + id);

            if (VerficarTipoFormulario(id))
                Navegar("../Personas/Nuevo.aspx");
            else
                Navegar("../Personas/NuevoMDE.aspx");
        }
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int pos = Convert.ToInt32(e.CommandArgument.ToString());
        }
        else
        {
            int pOrden = 0;
            try { pOrden = Convert.ToInt32(e.CommandName); Actualizar(pOrden); }
            catch { }
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Value.ToString();
        Session[AfiliacionServicio.CodigoPrograma + ".id"] = id;
        Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 0;
        String tipo_persona = gvLista.Rows[e.NewEditIndex].Cells[4].Text;
        if (tipo_persona == "Juridica")
            Navegar(Pagina.Nuevo);
        else
        {            
            //if (pResponsable != null && pResponsable.consecutivo != 0 && pResponsable.cod_persona_tutor != 0)
            //    Navegar("../Personas/NuevoMDE.aspx");
            //else
            //    Navegar("../Personas/Nuevo.aspx?" + id);

            if (VerficarTipoFormulario(id))
                Navegar("../Personas/Tab/Persona.aspx");
            else
                Navegar("../Personas/NuevoMDE.aspx");
        }
    }

    private bool VerficarTipoFormulario(string id)
    {
        //Consultar parametro general de mayoria de edad
        Xpinn.Comun.Services.GeneralService generalServicio = new Xpinn.Comun.Services.GeneralService();
        General pGeneral = new General();
        pGeneral = generalServicio.ConsultarGeneral(104, (Usuario)Session["usuario"]);

        //Consultar edad de la persona
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Persona1 vPersona = new Persona1();
        vPersona = personaServicio.FechaNacimiento(Convert.ToInt64(id), (Usuario)Session["usuario"]);
        //Si la persona es mayor de edad eliminar el responsable y redireccionar el formulario de mayor de edad
        if (pGeneral.valor.Trim() == "")
            return true;
        if (Convert.ToInt64(pGeneral.valor) <= vPersona.Edad)
        {
            PersonaResponsable pResponsable = new PersonaResponsable();
            pResponsable.cod_persona = Convert.ToInt64(id);
            string pFiltro = "WHERE R.COD_PERSONA = " + id;

            pResponsable = AfiliacionServicio.ConsultarPersonaResponsable(pFiltro, (Usuario)Session["usuario"]);
            if (pResponsable.cod_persona_tutor != 0)
            {
                AfiliacionServicio.Eliminar_PersonaResponsable(pResponsable, (Usuario)Session["usuario"]);
                Session["alertaMayoriaEdad"] = true;
            }

            return true;
        }
        else
            return false;
    }

    protected void gvLista_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            try
            {
                TerceroServicio.EliminarTercero(id, (Usuario)Session["usuario"]);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            Actualizar(0);
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar(0);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    public void Actualizar(int actualizar = 0)
    {
        VerError("");
        btnCorreo.Visible = false;

        string sLetra = ViewState["CurrentAlphabet"].ToString();
        string Filtro = "";
        if (ViewState["Filtro"] != null)
            Filtro = ViewState["Filtro"].ToString();
        string sOrdenar = "";
        string sFiltro = "";
        if (sLetra != "TODO" && sLetra.Trim() != "")
        {
            sFiltro = " (primer_apellido Like '" + sLetra + "%' OR razon_social Like '" + sLetra + "%') ";
        }
        if (Filtro != null)
        {
            if (Filtro.Trim() != "")
            {
                sFiltro += Filtro;
            }
        }

        //Código nómina
        if (!string.IsNullOrWhiteSpace(txtCodigoNomina.Text))
            sFiltro += (!string.IsNullOrWhiteSpace(sFiltro) ? " and " : "") + (" cod_nomina = " + txtCodigoNomina.Text);

        bool diaCumpleañosVacio = string.IsNullOrWhiteSpace(txtDiaCumpleaños.Text);
        bool mesCumpleañosVacio = string.IsNullOrWhiteSpace(ddlMesCumpleaños.SelectedValue);

        if (!diaCumpleañosVacio || !mesCumpleañosVacio)
        {
            if (diaCumpleañosVacio && mesCumpleañosVacio)
            {
                VerError("Debe filtrar tanto por mes como por fecha ");
                return;
            }

            if (!string.IsNullOrWhiteSpace(sFiltro))
            {
                sFiltro += " and ";
            }

            string filtroCumpleaños = txtDiaCumpleaños.Text + "/" + ddlMesCumpleaños.SelectedValue + "/2016";

            sFiltro += " datemonth(fechanacimiento) = datemonth(to_date('" + filtroCumpleaños + "', '" + gFormatoFecha + "')) and dateday(fechanacimiento) = dateday(to_date('" + filtroCumpleaños + "', '" + gFormatoFecha + "')) ";
            btnCorreo.Visible = true;
        }


        if (ddlOrdenar.SelectedValue != null)
            sOrdenar = ddlOrdenar.SelectedValue;

        try
        {
            List<Xpinn.Contabilidad.Entities.Tercero> lstConsulta = new List<Xpinn.Contabilidad.Entities.Tercero>();
            if (ddlTipoRol.SelectedItem.Value == "A")
                lstConsulta = TerceroServicio.ListarTerceroSoloAfiliados(ObtenerValores(), sFiltro, (Usuario)Session["usuario"], sOrdenar);
            else if (ddlTipoRol.SelectedItem.Value == "T")
                lstConsulta = TerceroServicio.ListarTerceroNoAfiliados(ObtenerValores(), sFiltro, (Usuario)Session["usuario"], sOrdenar);
            else
                lstConsulta = TerceroServicio.ListarTercero(ObtenerValores(), sFiltro, sOrdenar, (Usuario)Session["usuario"]);

            gvLista.PageSize = Convert.ToInt32(ddlPageSize.SelectedItem.Text);
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                lblInfo.Visible = false;
                Session["DTPERSONAS"] = lstConsulta;
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
                Session["DTPERSONAS"] = null;
            }
            Session.Add(TerceroServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }

    private Xpinn.Contabilidad.Entities.Tercero ObtenerValores()
    {
        Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
        if (ddlTipoPersona.SelectedValue.Trim() != "")
            vTercero.tipo_persona = Convert.ToString(ddlTipoPersona.SelectedValue.Trim());
        if (txtCodigo.Text.Trim() != "")
            vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
        if (txtNumeIdentificacion.Text.Trim() != "")
            vTercero.identificacion = Convert.ToString(txtNumeIdentificacion.Text.Trim());
        if (txtNombres.Text.Trim() != "")
            vTercero.primer_nombre = Convert.ToString(txtNombres.Text.Trim());
        if (txtApellidos.Text.Trim() != "")
            vTercero.primer_apellido = Convert.ToString(txtApellidos.Text.Trim());
        if (txtRazonSocial.Text.Trim() != "")
            vTercero.razon_social = Convert.ToString(txtRazonSocial.Text.Trim());
        if (ddlCiudad.SelectedValue.Trim() != "")
            vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue.Trim());
        return vTercero;
    }

    private void CargarListas()
    {
        try
        {
            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaIdStr";
            ddlCiudad.DataSource = TraerResultadosLista("Ciudades");
            ddlCiudad.DataBind();

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    private void GenerateAlphabets()
    {
        List<ListItem> alphabets = new List<ListItem>();
        ListItem alphabet = new ListItem();
        alphabet.Value = "TODO";
        alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
        alphabets.Add(alphabet);
        for (int i = 65; i <= 90; i++)
        {
            alphabet = new ListItem();
            alphabet.Value = Char.ConvertFromUtf32(i);
            alphabet.Selected = alphabet.Value.Equals(ViewState["CurrentAlphabet"]);
            alphabets.Add(alphabet);
        }
        rptAlphabets.DataSource = alphabets;
        rptAlphabets.DataBind();
    }

    protected void Alphabet_Click(object sender, EventArgs e)
    {
        LinkButton lnkAlphabet = (LinkButton)sender;
        ViewState["CurrentAlphabet"] = lnkAlphabet.Text;
        this.GenerateAlphabets();
        gvLista.PageIndex = 0;
        Actualizar(0);
    }

    protected void LlenarComboPageSize()
    {
        int tamaño = 50;
        int contador = 1;

        ddlPageSize.Items.Clear();
        ddlPageSize.Items.Insert(0, "1");
        if (tamaño < 5)
            ddlPageSize.Items.Insert(0, tamaño.ToString());
        for (int i = 5; i <= tamaño; i = i + 5)
        {
            if (i == pageSize)
            {
                ddlPageSize.Items.Insert(contador, i.ToString());
                ddlPageSize.SelectedValue = pageSize.ToString();
            }
            else
            {
                ddlPageSize.Items.Insert(contador, i.ToString());
            }

            contador = contador + 1;
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvLista.PageSize = Convert.ToInt32(ddlPageSize.Text);
        Actualizar(0);
    }

    protected void ddlOrdenar_SelectedIndexChanged(object sender, EventArgs e)
    {
        Actualizar(0);
    }



    #region CODIGO DE IMPORTACION

    void CargaFormatosFecha()
    {
        ddlFormatoFecha.Items.Insert(0, new ListItem("Seleccione un Item", "0"));
        ddlFormatoFecha.Items.Insert(1, new ListItem("dd/MM/yyyy", "dd/MM/yyyy"));
        ddlFormatoFecha.Items.Insert(2, new ListItem("yyyy/MM/dd", "yyyy/MM/dd"));
        ddlFormatoFecha.Items.Insert(3, new ListItem("MM/dd/yyyy", "MM/dd/yyyy"));
        ddlFormatoFecha.Items.Insert(4, new ListItem("ddMMyyyy", "ddMMyyyy"));
        ddlFormatoFecha.Items.Insert(5, new ListItem("yyyyMMdd", "yyyyMMdd"));
        ddlFormatoFecha.Items.Insert(6, new ListItem("MMddyyyy", "MMddyyyy"));
        ddlFormatoFecha.SelectedIndex = 0;
        ddlFormatoFecha.DataBind();
    }


    void LimpiarDataImportacion()
    {
        pErrores.Visible = false;
        gvDatos.DataSource = null;
        gvJuridica.DataSource = null;
        panelNatural2.Visible = false;
        panelJuridica2.Visible = false;
        panelCuentasBancarias2.Visible = false;
        ucFecha.Text = DateTime.Now.ToShortDateString();
        ddlFormatoFecha.SelectedIndex = 0;
        rblTipoPersona.SelectedIndex = 0;
        rblTipoPersona_SelectedIndexChanged(rblTipoPersona, null);
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        mvPrincipal.ActiveViewIndex = 1;
        Site toolBar = (Site)Master;
        toolBar.MostrarLimpiar(true);
        toolBar.MostrarExportar(false);
        toolBar.MostrarImportar(false);
        toolBar.MostrarNuevo(false);
        LimpiarDataImportacion();
    }


    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = "";
            if (ddlFormatoFecha.SelectedIndex == 0)
            {
                VerError("Seleccione el tipo de fecha que se carga en el archivo.");
                ddlFormatoFecha.Focus();
                return;
            }
            if (ucFecha.Text == "")
            {
                VerError("Ingrese la fecha de carga");
                ucFecha.Focus();
                return;
            }
            if (fupArchivoPersona.HasFile)
            {
                Stream stream = fupArchivoPersona.FileContent;
                List<Xpinn.FabricaCreditos.Entities.Persona1> lstPersonas = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
                List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica = new List<Xpinn.Contabilidad.Entities.Tercero>();
                List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias = new List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>();
                List<Xpinn.FabricaCreditos.Entities.ErroresCarga> plstErrores = new List<Xpinn.FabricaCreditos.Entities.ErroresCarga>();

                PersonaService.CargarPersonas(ucFecha.ToDateTime, rblTipoCarga.SelectedValue, rblTipoPersona.SelectedValue, ddlFormatoFecha.SelectedValue, stream, ref error, ref lstJuridica, ref lstPersonas, ref lstCtaBancarias, ref plstErrores, (Usuario)Session["usuario"]);

                if (error.Trim() != "")
                {
                    VerError(error);
                    return;
                }
                if (plstErrores.Count() > 0)
                {
                    pErrores.Visible = true;
                    gvErrores.DataSource = plstErrores;
                    gvErrores.DataBind();
                    cpeDemo1.CollapsedText = "(Click Aqui para ver " + plstErrores.Count() + " errores...)";
                    cpeDemo1.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                panelNatural2.Visible = false;
                panelJuridica2.Visible = false;
                panelCuentasBancarias2.Visible = false;

                if (rblTipoCarga.SelectedValue == "P")
                {
                    if (rblTipoPersona.SelectedValue == "N")
                    {
                        if (lstPersonas.Count > 0)
                        {
                            Session["lstNatural"] = lstPersonas;
                            panelNatural2.Visible = true;
                            //CARGAR DATOS A GRILLA DE NATURALES
                            gvDatos.DataSource = lstPersonas;
                            gvDatos.DataBind();
                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarGuardar(true);
                        }
                    }
                    else
                    {
                        if (lstJuridica.Count > 0)
                        {
                            Session["lstJuridica"] = lstJuridica;
                            panelJuridica2.Visible = true;
                            //CARGAR DATOS A GRILLA DE JURIDICOS
                            gvJuridica.DataSource = lstJuridica;
                            gvJuridica.DataBind();
                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarGuardar(true);
                        }
                    }
                }
                else
                {
                    Session["lstCtaBancarias"] = lstCtaBancarias;
                    panelCuentasBancarias2.Visible = true;
                    //CARGAR DATOS A GRILLA DE CUENTAS BANCARIAS
                    gvCuentasBancarias.DataSource = lstCtaBancarias;
                    gvCuentasBancarias.DataBind();
                    Site toolBar = (Site)this.Master;
                    toolBar.MostrarGuardar(true);
                }

            }
            else
            {
                VerError("Seleccione el archivo a cargar, verifique los datos.");
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "btnCargarPersonas_Click", ex);
        }
    }


    Boolean ValidarData()
    {
        if (ucFecha.Text == "")
        {
            VerError("Ingrese la fecha de carga");
            ucFecha.Focus();
            return false;
        }

        if (rblTipoCarga.SelectedValue == "P")
        {
            if (rblTipoPersona.SelectedItem == null)
            {
                VerError("Seleccione el Tipo de persona a la cual pertencen los datos cargados.");
                rblTipoPersona.Focus();
                return false;
            }
            if (rblTipoPersona.SelectedValue == "N")
            {
                if (gvDatos.Rows.Count <= 0)
                {
                    VerError("No existen datos por registrar, verifique los datos.");
                    return false;
                }
            }
            else //Validacion a personas juridicas.
            {
                if (gvJuridica.Rows.Count <= 0)
                {
                    VerError("No existen datos por registrar, verifique los datos.");
                    return false;
                }
            }
        }
        else
        {
            if (gvCuentasBancarias.Rows.Count <= 0)
            {
                VerError("No existen datos por registrar, verifique los datos.");
                return false;
            }
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (ValidarData())
                ctlMensaje.MostrarMensaje("Desea realizar la grabación de datos?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstNatural = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica = new List<Xpinn.Contabilidad.Entities.Tercero>();
            List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias = new List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>();

            //REALIZAR LA GRABACIÓN
            if (rblTipoCarga.SelectedValue == "P")
            {
                if (rblTipoPersona.SelectedValue == "N")
                    lstNatural = (List<Xpinn.FabricaCreditos.Entities.Persona1>)Session["lstNatural"];
                else
                    lstJuridica = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["lstJuridica"];
            }
            else if (rblTipoCarga.SelectedValue == "C")
            {
                lstCtaBancarias = (List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>)Session["lstCtaBancarias"];
                int pNumCuenta = 0;
                // 0 = ok, 1 = error en cantidad de principales, 2 = misma cuenta
                int resultValidacion = 0;
                string principales = "";
                foreach (CuentasBancarias pCuenta in lstCtaBancarias)
                {
                    pNumCuenta = lstCtaBancarias.Where(x => x.principal == 1 && x.cod_persona == pCuenta.cod_persona).Count();
                    if (pNumCuenta > 1)
                    {
                        resultValidacion = 1;
                        principales = " " + pCuenta.cod_persona;
                        break;
                    }
                    pNumCuenta = lstCtaBancarias.Where(x => x.cod_persona == pCuenta.cod_persona && x.numero_cuenta == pCuenta.numero_cuenta).Count();
                    if (pNumCuenta > 1)
                    {
                        resultValidacion = 2;
                        principales = " " + pCuenta.cod_persona;
                        break;
                    }
                }
                if (resultValidacion == 1)
                {
                    VerError("Se presentó inconsistencia de datos en la información cargada, se requiere cargar una sola cuenta principal para la persona con código:"+principales);
                    return;
                }
                if (resultValidacion == 2)
                {
                    VerError("Se presentó inconsistencia de datos en la información cargada, se detectó cuentas bancarias duplicadas para la persona con código:"+principales);
                    return;
                }
            }

            //LLAMANDO AL METODO DE GRABACION
            string pError = "", pTipoPersona = "", pTipoCarga = "";
            pTipoPersona = rblTipoPersona.SelectedValue;
            pTipoCarga = rblTipoCarga.SelectedValue;
            PersonaService.CrearPersonaImportacion(ucFecha.ToDateTime, ref pError, pTipoCarga, pTipoPersona, lstNatural, lstJuridica, lstCtaBancarias, (Usuario)Session["usuario"]);
            if (pError != "")
            {
                VerError(pError);
                return;
            }
            mvPrincipal.ActiveViewIndex = 2;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarLimpiar(false);
            Session.Remove("lstNatural");
            Session.Remove("lstJuridica");
            Session.Remove("lstCtaBancarias");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstNatural = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstNatural = (List<Xpinn.FabricaCreditos.Entities.Persona1>)Session["lstNatural"];

            lstNatural.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

            gvDatos.DataSourceID = null;
            gvDatos.DataBind();

            gvDatos.DataSource = lstNatural;
            gvDatos.DataBind();
            Session["lstNatural"] = lstNatural;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvDatos_RowDeleting", ex);
        }
    }


    protected void gvJuridica_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Tercero> lstJuridica = new List<Xpinn.Contabilidad.Entities.Tercero>();
            lstJuridica = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["lstJuridica"];

            lstJuridica.RemoveAt((gvJuridica.PageIndex * gvJuridica.PageSize) + e.RowIndex);

            gvJuridica.DataSourceID = null;
            gvJuridica.DataBind();

            gvJuridica.DataSource = lstJuridica;
            gvJuridica.DataBind();
            Session["lstJuridica"] = lstJuridica;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvJuridica_RowDeleting", ex);
        }
    }

    protected void rblTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoPersona.SelectedValue == "N")
        {
            panelNatural.Visible = true;
            panelJuridica.Visible = false;
        }
        else
        {
            panelNatural.Visible = false;
            panelJuridica.Visible = true;
        }
    }

    #endregion

    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(_usuario.idEmpresa, _usuario);

            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)TipoDocumentoCorreo.FormatoCumpleaños, _usuario);

            List<Xpinn.Contabilidad.Entities.Tercero> lstPersonas = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["DTPERSONAS"];

            if (string.IsNullOrWhiteSpace(empresa.e_mail) || string.IsNullOrWhiteSpace(empresa.clave_e_mail))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }

            foreach (var persona in lstPersonas)
            {
                if (string.IsNullOrWhiteSpace(persona.email))
                {
                    continue;
                }

                LlenarDiccionarioGlobalWebParaCorreo(persona);

                modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

                CorreoHelper correoHelper = new CorreoHelper(persona.email, empresa.e_mail, empresa.clave_e_mail);
                bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, Correo.Gmail, modificardocumento.descripcion, _usuario.empresa);

                // hacer algo si el correo a un asociado falla, no se si informar o ignorar y pasar
                if (!exitoso)
                {
                    continue;
                }
            }

            btnCorreo.Text = "Envio Satisfactorio";
            btnCorreo.Enabled = false;
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }


    private void LlenarDiccionarioGlobalWebParaCorreo(Xpinn.Contabilidad.Entities.Tercero persona)
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        parametrosFormatoCorreo.Add(ParametroCorreo.PrimerNombre, persona.primer_nombre);
        parametrosFormatoCorreo.Add(ParametroCorreo.SegundoNombre, persona.segundo_nombre);
        parametrosFormatoCorreo.Add(ParametroCorreo.PrimerApellido, persona.primer_apellido);
        parametrosFormatoCorreo.Add(ParametroCorreo.SegundoApellido, persona.segundo_apellido);
        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, persona.primer_nombre + " " + persona.segundo_nombre + " " + persona.primer_apellido + " " + persona.segundo_apellido);
        parametrosFormatoCorreo.Add(ParametroCorreo.RazonSocial, persona.razon_social);
    }


    protected void gvCuentasBancarias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> lstCtaBancarias = new List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>();
            lstCtaBancarias = (List<Xpinn.FabricaCreditos.Entities.CuentasBancarias>)Session["lstCtaBancarias"];

            lstCtaBancarias.RemoveAt((gvCuentasBancarias.PageIndex * gvCuentasBancarias.PageSize) + e.RowIndex);

            gvCuentasBancarias.DataSourceID = null;
            gvCuentasBancarias.DataBind();

            gvCuentasBancarias.DataSource = lstCtaBancarias;
            gvCuentasBancarias.DataBind();
            Session["lstCtaBancarias"] = lstCtaBancarias;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.CodigoPrograma, "gvCuentasBancarias_RowDeleting", ex);
        }
    }

    protected void rblTipoCarga_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblTipoCarga.SelectedValue == "C")
        {
            panelNatural.Visible = false;
            panelJuridica.Visible = false;
            panelCuentasBancarias.Visible = true;
            rblTipoPersona.Enabled = false;
        }
        else
        {
            rblTipoPersona.SelectedIndex = 0;
            panelJuridica.Visible = false;
            panelCuentasBancarias.Visible = false;
            panelNatural.Visible = true;
            rblTipoPersona.Enabled = true;
        }

    }
}