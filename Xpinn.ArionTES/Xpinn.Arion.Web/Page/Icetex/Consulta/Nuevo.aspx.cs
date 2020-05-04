using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;
using System.IO;

public partial class Nuevo : GlobalWeb
{
    ConvocatoriaServices ConvocatoriaService = new ConvocatoriaServices();
    AprobacionServices ModificacionIctx = new AprobacionServices();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[ModificacionIctx.CodigoProgramaModifi + ".id"] != null)
                VisualizarOpciones(ModificacionIctx.CodigoProgramaConsul, "E");
            else
                VisualizarOpciones(ModificacionIctx.CodigoProgramaConsul, "N");
            Site toolBar = (Site)Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaConsul, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                panelGeneral.Visible = true;
                panelFinal.Visible = false;
                if (Session[ModificacionIctx.CodigoProgramaConsul + ".id"] != null)
                {
                    idObjeto = Session[ModificacionIctx.CodigoProgramaConsul + ".id"].ToString();
                    Session.Remove(ModificacionIctx.CodigoProgramaConsul + ".id");
                    CargarDropDown();
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaConsul, "Page_Load", ex);
        }
    }

    protected void CargarDropDown()
    {
        ddlTipoBeneficiario.Items.Insert(0, new ListItem("Asociado", "0"));
        ddlTipoBeneficiario.Items.Insert(1, new ListItem("Hijo del Asociado", "1"));
        ddlTipoBeneficiario.Items.Insert(2, new ListItem("Nieto del Asociado", "2"));
        ddlTipoBeneficiario.Items.Insert(3, new ListItem("Empleado", "3"));

        poblar.PoblarListaDesplegable("tipoidentificacion", ddlTipoDoc, Usuario);
        poblar.PoblarListaDesplegable("universidad", "", "","2",ddlUniversidad, Usuario);

        string pError = "";
        string pFiltro = " WHERE C.NUMERO_CREDITO = " + idObjeto;
        List<CreditoIcetexAprobacion> lstAprobaciones = ModificacionIctx.ListarCreditosAprobacion(pFiltro, ref pError, Usuario);
        if (lstAprobaciones.Count > 0)
        {
            ddlAprobacion.DataSource = lstAprobaciones;
            ddlAprobacion.DataTextField = "descripcion";
            //ddlAprobacion.DataTextField = "idaprobacion";
            ddlAprobacion.DataValueField = "idaprobacion";
            ddlAprobacion.DataBind();
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            CreditoIcetex pEntidad = new CreditoIcetex();
            string pFiltro = obtFiltro();
            if (Session[ModificacionIctx.CodigoProgramaConsul + ".cod_convocatoria"] != null)
            {
                lblConvocatoria.Text = Session[ModificacionIctx.CodigoProgramaConsul + ".cod_convocatoria"].ToString();
            }
            pEntidad = ConvocatoriaService.ConsultarCreditoIcetex(pFiltro, Usuario);
            if (pEntidad != null)
            {
                if (pEntidad.numero_credito > 0)
                {
                    //Consultando datos de la persona
                    Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
                    Xpinn.FabricaCreditos.Entities.Persona1 pPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
                    pPersona = PersonaService.ConsultaDatosPersona(pEntidad.cod_persona, Usuario);
                    if (pPersona.cod_persona > 0)
                    {
                        lblCod_Persona.Text = pPersona.cod_persona.ToString();
                        lblIdentificacion.Text = pPersona.identificacion != null ? pPersona.identificacion : "";
                        string Nombre = "";
                        if (pPersona.primer_nombre != null)
                            Nombre += pPersona.primer_nombre;
                        if (pPersona.segundo_nombre != null)
                            Nombre += " " + pPersona.segundo_nombre;
                        if (pPersona.primer_apellido != null)
                            Nombre += " " + pPersona.primer_apellido;
                        if (pPersona.segundo_apellido != null)
                            Nombre += " " + pPersona.segundo_apellido;
                        lblNombre.Text = Nombre;
                    }

                    if (pEntidad.fecha_solicitud != DateTime.MinValue)
                        txtFecha.Text = pEntidad.fecha_solicitud.ToString(gFormatoFecha);
                    if (pEntidad.tipo_beneficiario != null)
                        ddlTipoBeneficiario.SelectedValue = pEntidad.tipo_beneficiario;
                    if (pEntidad.codtipoidentificacion != null)
                        ddlTipoDoc.SelectedValue = pEntidad.codtipoidentificacion.ToString();
                    if (pEntidad.identificacion != null)
                        txtNroDoc.Text = pEntidad.identificacion;
                    if (pEntidad.primer_apellido != null)
                        txtApellido1.Text = pEntidad.primer_apellido;
                    if (pEntidad.segundo_apellido != null)
                        txtApellido2.Text = pEntidad.segundo_apellido;
                    if (pEntidad.primer_nombre != null)
                        txtNombre1.Text = pEntidad.primer_nombre;
                    if (pEntidad.segundo_nombre != null)
                        txtNombre2.Text = pEntidad.segundo_nombre;
                    if (pEntidad.direccion != null)
                        txtDireccion.Text = HttpUtility.HtmlDecode(pEntidad.direccion);
                    if (pEntidad.telefono != null)
                        txtTelefono.Text = pEntidad.telefono;
                    if (pEntidad.email != null)
                        txtEmail.Text = pEntidad.email;
                    if (pEntidad.estrato != null)
                        txtEstrato.Text = pEntidad.estrato.ToString();
                    if (pEntidad.cod_universidad != null)
                    {
                        ddlUniversidad.SelectedValue = pEntidad.cod_universidad;
                        ddlUniversidad_SelectedIndexChanged(ddlUniversidad, null);
                    }
                    if (pEntidad.cod_programa != null)
                        ddlPrograma.SelectedValue = pEntidad.cod_programa;
                    if (pEntidad.tipo_programa != 0)
                        ddlTipoPrograma.SelectedValue = pEntidad.tipo_programa.ToString();
                    if (pEntidad.valor > 0)
                        txtValorPrograma.Text = pEntidad.valor.ToString();
                    if (pEntidad.periodos != null)
                        ddlPeriodos.SelectedValue = pEntidad.periodos.ToString();
                    if (pEntidad.estado != null)
                    {
                        lblCodEstado.Text = pEntidad.estado;
                        switch (pEntidad.estado)
                        {
                            case "S":
                                lblEstado.Text = "Pre-Inscrito";
                                break;
                            case "A":
                                lblEstado.Text = "Aprobado";
                                break;
                            case "Z":
                                lblEstado.Text = "Aplazado";
                                break;
                            case "N":
                                lblEstado.Text = "Negado";
                                break;
                            case "I":
                                lblEstado.Text = "Inscrito";
                                break;
                            case "T":
                                lblEstado.Text = "Terminado";
                                break;
                        }
                    }
                    ActualizarDocumentos();
                    ddlAprobacion_SelectedIndexChanged(ddlAprobacion, null);
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaConsul, "ObtenerDatos", ex);
        }
    }

    protected void ddlAprobacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActualizarCheckList();
    }

    protected void ActualizarCheckList()
    {
        try
        {
            List<CreditoIcetexCheckList> lstCheck = new List<CreditoIcetexCheckList>();
            string pFiltro = string.Empty;
            pFiltro = " WHERE c.numero_credito = " + idObjeto + " and c.idaprobacion = " + ddlAprobacion.SelectedValue;
            string pError = "";
            lstCheck = ModificacionIctx.ListarCreditoIcetexCheckList(pFiltro, ref pError, Usuario);
            if (!string.IsNullOrEmpty(pError))
            {
                VerError(pError);
                return;
            }
            lblTotalCheck.Visible = true;
            if (lstCheck.Count > 0)
            {
                gvCheckList.Visible = true;
                gvCheckList.DataSource = lstCheck;
                lblTotalCheck.Text = "Registros encontrados : <b>" + lstCheck.Count + "</b>";
            }
            else
            {
                gvCheckList.DataSource = null;
                gvCheckList.Visible = false;
                lblTotalCheck.Text = "Su consulta no obtuvo ningún resultado <br/>";
            }
            gvCheckList.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ActualizarDocumentos()
    {
        try
        {
            List<ListadosIcetex> lstDocumentos = new List<ListadosIcetex>();
            string pfiltro = string.Empty, pfiltroDoc = string.Empty;
            string pError = "";
            if (!string.IsNullOrEmpty(idObjeto))
            {
                pfiltro = " Where C.NUMERO_CREDITO = " + idObjeto;
                lstDocumentos = ModificacionIctx.ListarDocumentosIcetex(pfiltro, ref pError, (Usuario)Session["usuario"]);
                if (pError.Trim() != "")
                {
                    VerError(pError.Trim());
                    return;
                }

                if (lstDocumentos.Count > 0)
                {
                    gvDocumentos.DataSource = lstDocumentos;
                    gvDocumentos.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            throw;
        }
    }

    private string obtFiltro()
    {
        string pFiltro = string.Empty;
        if (idObjeto != null && idObjeto != "")
            pFiltro = " WHERE c.numero_credito = " + idObjeto;

        return pFiltro;
    }

    protected void ddlUniversidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDropDownPrograma();
    }

    private void CargarDropDownPrograma()
    {
        ddlPrograma.Items.Clear();
        if (ddlUniversidad.SelectedIndex > 0)
            poblar.PoblarListaDesplegable("PROGRAMA", "cod_programa, descripcion", " Cod_universidad = " + ddlUniversidad.SelectedValue, "2", ddlPrograma, Usuario);
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModificacionIctx.CodigoProgramaConsul, "btnCancelar_Click", ex);
        }
    }

    
    protected void gvDocumentos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string id = gvDocumentos.DataKeys[e.NewEditIndex].Values[0].ToString();
        e.NewEditIndex = -1;
        //Mostrar Imagen
        LlenarImagenDocumento(id);
    }

    private void LlenarImagenDocumento(string pId)
    {
        ByteHelper Convert = new ByteHelper();
        //Consultando el archivo
        string pError = "";
        string pFiltro = " WHERE C.COD_CREDOC = " + pId;
        List<ListadosIcetex> lstDocumento = new List<ListadosIcetex>();
        lstDocumento = ModificacionIctx.ListarDocumentosIcetex(pFiltro, ref pError, (Usuario)Session["usuario"]);
        if (pError.Trim() != "")
        {
            VerError(pError.ToString());
            return;
        }
        if (lstDocumento.Count > 0)
        {
            //imgDocumento.ImageUrl = Convert.ConvertByteArrToBase64String(lstDocumento.First().archivo);
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    if (ficheroActual.Contains(pNomUsuario))
                        File.Delete(ficheroActual);
            }
            catch
            { }
            //CREANDO REPORTE
            byte[] bytes = lstDocumento.First().archivo;
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            //MOSTRANDO REPORTE
            string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"550px\">";
            adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            adjuntar += "</object>";

            ltReport.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
        }
    }
    
}