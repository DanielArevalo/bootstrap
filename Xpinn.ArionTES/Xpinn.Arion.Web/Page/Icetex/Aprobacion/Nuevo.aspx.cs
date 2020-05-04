using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;
using System.Configuration;

public partial class Nuevo : GlobalWeb
{
    AprobacionServices AprobacionIctx = new AprobacionServices();
    PoblarListas poblar = new PoblarListas();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AprobacionIctx.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AprobacionIctx.CodigoPrograma, "E");
            else
                VisualizarOpciones(AprobacionIctx.CodigoPrograma, "N");
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //frmPrint.Visible = false;
                panelGeneral.Visible = true;
                panelFinal.Visible = false;
                txtFecha.Text = "";
                lblCodigo.Text = "";
                if (Session[AprobacionIctx.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AprobacionIctx.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AprobacionIctx.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            if (Session[AprobacionIctx.CodigoPrograma + ".fecha"] != null)
            {
                txtFecha.Text = Session[AprobacionIctx.CodigoPrograma + ".fecha"].ToString();
                Session.Remove(AprobacionIctx.CodigoPrograma + ".fecha");
            }
            if (Session[AprobacionIctx.CodigoPrograma + ".cod_convocatoria"] != null)
            {
                lblCod_convocatoria.Text = Session[AprobacionIctx.CodigoPrograma + ".cod_convocatoria"].ToString();
                Session.Remove(AprobacionIctx.CodigoPrograma + ".cod_convocatoria");
            }
            if (Session[AprobacionIctx.CodigoPrograma + ".cod_persona"] != null)
            {
                lblCod_persona.Text = Session[AprobacionIctx.CodigoPrograma + ".cod_persona"].ToString();
                Session.Remove(AprobacionIctx.CodigoPrograma + ".cod_persona");
            }
            if (Session[AprobacionIctx.CodigoPrograma + ".estado"] != null)
            {
                lblEstado.Text = Session[AprobacionIctx.CodigoPrograma + ".estado"].ToString();
                Session.Remove(AprobacionIctx.CodigoPrograma + ".estado");
            }
            if (Session[AprobacionIctx.CodigoPrograma + ".tipo_aprobacion"] != null)
            {
                lblTipoAprobacion.Text = Session[AprobacionIctx.CodigoPrograma + ".tipo_aprobacion"].ToString();
                Session.Remove(AprobacionIctx.CodigoPrograma + ".tipo_aprobacion");
            }

            lblCodigo.Text = idObjeto;

            DataTable dtInformacion = new DataTable();
            List<ListadosIcetex> lstDocumentos = new List<ListadosIcetex>();
            string pfiltro = string.Empty, pfiltroDoc = string.Empty;
            string pError = "";
            pfiltro = " Where C.NUMERO_CREDITO = " + idObjeto;
            dtInformacion = AprobacionIctx.ListarCreditosDocumentosIcetex(pfiltro, ref lstDocumentos, ref pError, (Usuario)Session["usuario"]);
            if (pError.Trim() != "")
            {
                VerError(pError.Trim());
                return;
            }
            if (dtInformacion.Rows.Count > 0)
            {
                gvDatos.DataSource = dtInformacion;
                gvDatos.DataBind();
            }
            if (lstDocumentos.Count > 0)
            {
                gvDocumentos.DataSource = lstDocumentos;
                gvDocumentos.DataBind();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Navegar(Pagina.Lista);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "btnCancelar_Click", ex);
        }
    }


    private Boolean validarDatos()
    {
        ViewState.Add("Documents",null);
        
        if (gvDatos.Rows.Count == 0)
        {
            VerError("No existen datos del beneficiario por registrar");
            return false;
        }
        else
        {
            foreach (GridViewRow rFila in gvDatos.Rows)
            {
                CheckBoxGrid chkAprobado = (CheckBoxGrid)rFila.FindControl("chkAprobado");
                CheckBoxGrid chkAplazado = (CheckBoxGrid)rFila.FindControl("chkAplazado");
                CheckBoxGrid chkNegado = (CheckBoxGrid)rFila.FindControl("chkNegado");

                if (chkAprobado.Checked == false && chkAplazado.Checked == false && chkNegado.Checked == false)
                {
                    VerError("Seleccione el estado en la fila " + (rFila.RowIndex + 1) + " [ Datos del Beneficiario]");
                    return false;
                }
            }
        }
        if (gvDocumentos.Rows.Count == 0)
        {
            VerError("No existen documentos del beneficiario por registrar");
            return false;
        }
        else
        {
            foreach (GridViewRow rFila in gvDocumentos.Rows)
            {
                CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)rFila.FindControl("chkAprobadoDoc");
                CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)rFila.FindControl("chkAplazadoDoc");
                CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)rFila.FindControl("chkNegadoDoc");
                if (chkAprobadoDoc.Checked == false && chkAplazadoDoc.Checked == false && chkNegadoDoc.Checked == false)
                {
                    VerError("Seleccione el estado en la fila " + (rFila.RowIndex + 1) + " [ Datos del Documento]");
                    return false;
                }
            }
        }
        if (txtEstado.Text.Trim() == "")
        {
            VerError("No existe el estado actual del crédito");
            return false;
        }
        return true;
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (!validarDatos())
                return;
            ctlMensaje.MostrarMensaje("Desea realizar la grabación?");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Usuario pUsu = (Usuario)Session["usuario"];
            CreditoIcetexAprobacion pAprobacion = new CreditoIcetexAprobacion();
            pAprobacion.idaprobacion = 0;
            pAprobacion.numero_credito = Convert.ToInt64(lblCodigo.Text);
            pAprobacion.fecha_aprobacion = DateTime.Now;
            pAprobacion.cod_usuario = Convert.ToInt32(pUsu.codusuario);
            pAprobacion.observaciones = txtObservacion.Text.Trim() != "" ? txtObservacion.Text.Trim() : null;
            pAprobacion.documento_soporte = null;
            pAprobacion.tipo_aprobacion = lblEstado.Text == "I" ? 2 : 1;
            if (txtEstado.Text == "Negado")
            {
                pAprobacion.estado = "N";
                hdOperacionIcetex.Value = (lblEstado.Text == "I" || lblTipoAprobacion.Text == "2") ? ((int)TipoDocumentoCorreo.IcetexNegadoInscrito).ToString() : ((int)TipoDocumentoCorreo.IcetexNegado).ToString();
            }
            else if (txtEstado.Text == "Aplazado")
            {
                pAprobacion.estado = "Z";
                hdOperacionIcetex.Value = (lblEstado.Text == "I" || lblTipoAprobacion.Text == "2") ? ((int)TipoDocumentoCorreo.IcetexAplazadoInscrito).ToString() : ((int)TipoDocumentoCorreo.IcetexAplazado).ToString();
            }
            else if (txtEstado.Text == "Aprobado")
            {
                if (lblEstado.Text == "I" || lblTipoAprobacion.Text == "2")
                {
                    pAprobacion.estado = "I";
                    hdOperacionIcetex.Value = ((int)TipoDocumentoCorreo.IcetexAprobadoInscrito).ToString();
                }
                else
                {
                    pAprobacion.estado = "A";
                    hdOperacionIcetex.Value = ((int)TipoDocumentoCorreo.IcetexAprobado).ToString();
                }
            }

            //Capturando archivo
            if (fuDocumento.HasFile)
            {
                StreamsHelper streamHelper = new StreamsHelper();
                byte[] bytesArrImagen;
                using (System.IO.Stream streamImagen = fuDocumento.PostedFile.InputStream)
                {
                    bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                }
                pAprobacion.documento_soporte = bytesArrImagen;
            }
            List<CreditoIcetexCheckList> lstItems = new List<CreditoIcetexCheckList>();
            List<ListadosIcetex> lstDocumentos = new List<ListadosIcetex>();
            lstItems = listaCreditosList(ref lstDocumentos);
            bool rpta = false;
            string pError = "";
            //Pendiente cargar informacion de un aplazado y modificar informacion
            rpta = AprobacionIctx.CrearCreditoCheckList(pAprobacion, lstItems, lstDocumentos, ref pError, (Usuario)Session["usuario"]);
            if (pError.Trim() != "")
            {
                VerError(pError.Trim());
                return;
            }
            if (!rpta)
            {
                VerError("Se generó un error al realizar la Aprobación.");
                return;
            }
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            panelGeneral.Visible = false;
            panelFinal.Visible = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AprobacionIctx.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    protected List<CreditoIcetexCheckList> listaCreditosList(ref List<ListadosIcetex> lstDocumento)
    {
        List<CreditoIcetexCheckList> lstItems = new List<CreditoIcetexCheckList>();
        foreach (GridViewRow rFila in gvDatos.Rows)
        {
            //Captura de valores
            CheckBoxGrid chkAprobado = (CheckBoxGrid)rFila.FindControl("chkAprobado");
            CheckBoxGrid chkAplazado = (CheckBoxGrid)rFila.FindControl("chkAplazado");
            CheckBoxGrid chkNegado = (CheckBoxGrid)rFila.FindControl("chkNegado");
            TextBox txtObservacion = (TextBox)rFila.FindControl("txtObservacion");
            int rpta = 3;
            if (chkAprobado.Checked)
                rpta = 1;
            else if (chkAplazado.Checked)
                rpta = 2;
            else
                rpta = 3;
            string pDescripcion = string.Empty, pValor = string.Empty;
            pDescripcion = gvDatos.DataKeys[rFila.RowIndex].Values[0].ToString();
            pValor = gvDatos.DataKeys[rFila.RowIndex].Values[1].ToString();
            CreditoIcetexCheckList pEntidad = new CreditoIcetexCheckList();
            pEntidad.tipo = 1;
            pEntidad.descripcion = pDescripcion;
            pEntidad.valor = pValor;
            pEntidad.resultado = rpta;
            pEntidad.observacion = txtObservacion.Text.Trim() != "" ? txtObservacion.Text : null;
            lstItems.Add(pEntidad);
        }
        foreach (GridViewRow rFila in gvDocumentos.Rows)
        {
            //Captura de valores
            CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)rFila.FindControl("chkAprobadoDoc");
            CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)rFila.FindControl("chkAplazadoDoc");
            CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)rFila.FindControl("chkNegadoDoc");
            TextBox txtObservacion = (TextBox)rFila.FindControl("txtObservacion");
            int rpta = 3;
            if (chkAprobadoDoc.Checked)
                rpta = 1;
            else if (chkAplazadoDoc.Checked)
                rpta = 2;
            else
                rpta = 3;
            string pValor = string.Empty;
            pValor = gvDocumentos.DataKeys[rFila.RowIndex].Values[1].ToString();

            CreditoIcetexCheckList pEntidad = new CreditoIcetexCheckList();
            pEntidad.tipo = 2;
            pEntidad.valor = pValor;
            pEntidad.resultado = rpta;
            pEntidad.observacion = txtObservacion.Text.Trim() != "" ? txtObservacion.Text : null;
            lstItems.Add(pEntidad);

            ListadosIcetex pDocumento = new ListadosIcetex();
            string pCodigo = string.Empty;
            pCodigo = gvDocumentos.DataKeys[rFila.RowIndex].Values[0].ToString();
            FileUpload fuArchivo = (FileUpload)rFila.FindControl("fuArchivo");
            if (fuArchivo != null)
            {
                if (fuArchivo.HasFile)
                {
                    StreamsHelper streamHelper = new StreamsHelper();
                    byte[] bytesArrImagen;
                    using (System.IO.Stream streamImagen = fuArchivo.PostedFile.InputStream)
                    {
                        bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                    }
                    pDocumento.codigo = Convert.ToInt64(pCodigo);
                    pDocumento.archivo = bytesArrImagen;
                    pDocumento.observacion = txtObservacion.Text.Trim() != "" ? txtObservacion.Text : null;
                    lstDocumento.Add(pDocumento);
                }
            }
        }

        return lstItems;
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
        lstDocumento = AprobacionIctx.ListarDocumentosIcetex(pFiltro, ref pError, (Usuario)Session["usuario"]);
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


    private void ValidarEstado()
    {
        string rpta = string.Empty;
        foreach (GridViewRow rFila in gvDatos.Rows)
        {
            //Captura de valores
            CheckBoxGrid chkAprobado = (CheckBoxGrid)rFila.FindControl("chkAprobado");
            CheckBoxGrid chkAplazado = (CheckBoxGrid)rFila.FindControl("chkAplazado");
            CheckBoxGrid chkNegado = (CheckBoxGrid)rFila.FindControl("chkNegado");
            if (chkNegado.Checked)
            {
                rpta = "N";
                //break;
            }
            else
            {
                if (chkAplazado.Checked)
                {
                    if (rpta != "N" || rpta == string.Empty)
                        rpta = "Z";
                }
                else if (chkAprobado.Checked)
                {
                    if ((rpta != "N" && rpta != "Z") || rpta == string.Empty)
                        rpta = "A";
                }
                else
                {
                    rpta = "";
                    break;
                }
            }
        }

        if (rpta == "Z" || rpta == "A")
        {
            foreach (GridViewRow rFila in gvDocumentos.Rows)
            {
                CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)rFila.FindControl("chkAprobadoDoc");
                CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)rFila.FindControl("chkAplazadoDoc");
                CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)rFila.FindControl("chkNegadoDoc");
                if (chkNegadoDoc.Checked)
                {
                    rpta = "N";
                    //break;
                }
                else
                {
                    if (chkAplazadoDoc.Checked)
                    {
                        if (rpta != "N" || rpta == string.Empty)
                            rpta = "Z";
                    }
                    else if (chkAprobadoDoc.Checked)
                    {
                        if ((rpta != "N" && rpta != "Z") || rpta == string.Empty)
                            rpta = "A";
                    }
                    else
                    {
                        rpta = "";
                        break;
                    }
                }
            }
        }
        if (rpta == "N")
            txtEstado.Text = "Negado";
        else if (rpta == "Z")
            txtEstado.Text = "Aplazado";
        else if (rpta == "A")
            txtEstado.Text = "Aprobado";
        else
            txtEstado.Text = "";
    }

    #region eventos de los checkbox

    protected void chkAprobado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkAprobado = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkAprobado.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAplazado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkAplazado");
        CheckBoxGrid chkNegado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkNegado");
        if (chkAprobado.Checked)
        {
            chkAplazado.Checked = false;
            chkNegado.Checked = false;
        }
        ValidarEstado();
    }

    protected void chkAplazado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkAplazado = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkAplazado.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAprobado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkAprobado");
        CheckBoxGrid chkNegado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkNegado");
        if (chkAplazado.Checked)
        {
            chkAprobado.Checked = false;
            chkNegado.Checked = false;
        }
        ValidarEstado();
    }

    protected void chkNegado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkNegado = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkNegado.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAprobado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkAprobado");
        CheckBoxGrid chkAplazado = (CheckBoxGrid)gvDatos.Rows[rowIndex].FindControl("chkAplazado");
        if (chkNegado.Checked)
        {
            chkAprobado.Checked = false;
            chkAplazado.Checked = false;
        }
        ValidarEstado();
    }
    
    protected void chkAprobadoDoc_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkAprobadoDoc.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkAplazadoDoc");
        CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkNegadoDoc");
        if (chkAprobadoDoc.Checked)
        {
            chkAplazadoDoc.Checked = false;
            chkNegadoDoc.Checked = false;
        }
        ValidarEstado();
    }

    protected void chkAplazadoDoc_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkAplazadoDoc.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkAprobadoDoc");
        CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkNegadoDoc");
        if (chkAplazadoDoc.Checked)
        {
            chkAprobadoDoc.Checked = false;
            chkNegadoDoc.Checked = false;
        }
        ValidarEstado();
    }

    protected void chkNegadoDoc_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkNegadoDoc = (CheckBoxGrid)sender;
        int rowIndex = Convert.ToInt32(chkNegadoDoc.CommandArgument);
        //recupendando los otros Checkbox
        CheckBoxGrid chkAprobadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkAprobadoDoc");
        CheckBoxGrid chkAplazadoDoc = (CheckBoxGrid)gvDocumentos.Rows[rowIndex].FindControl("chkAplazadoDoc");
        if (chkNegadoDoc.Checked)
        {
            chkAprobadoDoc.Checked = false;
            chkAplazadoDoc.Checked = false;
        }
        ValidarEstado();
    }

    #endregion

    #region Envio de Correo

    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();
            Xpinn.FabricaCreditos.Services.Persona1Service PersonaService = new Xpinn.FabricaCreditos.Services.Persona1Service();

            Usuario pUsuario = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
            persona.seleccionar = "Cod_persona";
            persona.cod_persona = Convert.ToInt64(lblCod_persona.Text);
            persona = PersonaService.ConsultarPersonaAPP(persona, pUsuario);

            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), hdOperacionIcetex.Value);

            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

            string correoServer = null, clave = null, pHosting = "";
            int puerto = 0;
            correoServer = ConfigurationManager.AppSettings["CorreoServidor"].ToString();
            clave = ConfigurationManager.AppSettings["Clave"].ToString();
            pHosting = ConfigurationManager.AppSettings["Hosting"].ToString();
            puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());

            if (string.IsNullOrWhiteSpace(persona.email_app))
            {
                VerError("El asociado no tiene email registrado, Codigo " + lblCodigo.Text);
                return;
            }
            else if (string.IsNullOrWhiteSpace(correoServer) || string.IsNullOrWhiteSpace(clave))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }

            LlenarDiccionarioGlobalWebParaCorreo();

            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);
            
            CorreoHelper correoHelper = new CorreoHelper(persona.email_app, correoServer, clave);
            bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, pHosting, puerto, modificardocumento.descripcion, pUsuario.empresa);

            if (!exitoso)
            {
                VerError("Error al enviar el correo");
                return;
            }

            btnCorreo.Text = "Envio Satisfactorio";
            btnCorreo.Enabled = false;
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }


    private void LlenarDiccionarioGlobalWebParaCorreo()
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        ConvocatoriaServices BOConvocatoria = new ConvocatoriaServices();
        CreditoIcetex pEntidad = new CreditoIcetex();

        string pFiltro = " WHERE C.NUMERO_CREDITO = " + lblCodigo.Text + "AND C.COD_CONVOCATORIA = " + lblCod_convocatoria.Text ;
        pEntidad = BOConvocatoria.ConsultarCreditoIcetex(pFiltro, (Usuario)Session["usuario"]);

        if (pEntidad != null)
        {
            parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, pEntidad.nom_asoc);
            parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, pEntidad.identific_asoc);
            parametrosFormatoCorreo.Add(ParametroCorreo.CodConvocatoria, lblCod_convocatoria.Text);
            parametrosFormatoCorreo.Add(ParametroCorreo.FechaIcetex, pEntidad.fecha_solicitud.ToShortDateString());
            parametrosFormatoCorreo.Add(ParametroCorreo.MontoIcetex, pEntidad.valor.ToString());
            String[] arrayLineas = txtObservacion.Text.Split(Convert.ToChar(13));
            string pObservacion = string.Empty;
            foreach (string nInfo in arrayLineas)
            {
                pObservacion += " ";
                pObservacion += nInfo;
                pObservacion += "<br />";
            }
            parametrosFormatoCorreo.Add(ParametroCorreo.ObservacionIcetex, pObservacion);
        }
    }

    #endregion

    
    protected void gvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string pCampo = e.Row.Cells[0].Text;
            string pInfo = pCampo.Substring(0, 2);
            pInfo = !pInfo.Contains("_") ? "A" : pInfo.Substring(0,1);
            if (pInfo == "A" || pInfo == "B" || pInfo == "C")
            {
                TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
                fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");
                DropDownListGrid ddlInfoDrop = (DropDownListGrid)e.Row.FindControl("ddlInfoDrop");
                //Mostrando controles
                if (pInfo.ToUpper() == "A")
                {
                    txtValor.Visible = true;
                    txtValor.Enabled = false;
                }
                else if (pInfo.ToUpper() == "B")
                {
                    txtctlfecha.Visible = true;
                    txtctlfecha.Enabled = false;
                }
                else if (pInfo.ToUpper() == "C")
                {
                    ddlInfoDrop.Visible = true;
                    //Cargando DropDownList
                    Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");
                    if (lblDropdown != null)
                    {
                        string pValorCampo = lblDropdown.Text != null ? lblDropdown.Text : "";
                        if (pValorCampo != "")
                        {
                            switch (pCampo)
                            {
                                case "C_TIPO_BENEFICIARIO":
                                    ddlInfoDrop.Items.Insert(0, new ListItem("Asociado", "0"));
                                    ddlInfoDrop.Items.Insert(1, new ListItem("Hijo del Asociado", "1"));
                                    ddlInfoDrop.Items.Insert(2, new ListItem("Nieto del Asociado", "2"));
                                    ddlInfoDrop.Items.Insert(3, new ListItem("Empleado", "3"));
                                    break;
                                case "C_TIPO_IDENTIFICACION":
                                    poblar.PoblarListaDesplegable("tipoidentificacion", ddlInfoDrop, Usuario);
                                    break;
                                //case "C_UNIVERSIDAD":
                                //    poblar.PoblarListaDesplegable("universidad", ddlInfoDrop, Usuario);
                                //    break;
                                //case "C_PROGRAMA":
                                //    poblar.PoblarListaDesplegable("programa", ddlInfoDrop, Usuario);
                                //    break;
                                case "C_TIPO_PROGRAMA":
                                    ddlInfoDrop.Items.Insert(0, new ListItem("Especialización(1 año)", "1"));
                                    ddlInfoDrop.Items.Insert(1, new ListItem("Maestria(2 años)", "2"));
                                    break;
                                case "C_PERIODOS":
                                    ddlInfoDrop.Items.Insert(0, new ListItem("1 Semestre", "1"));
                                    ddlInfoDrop.Items.Insert(1, new ListItem("2 Semestre", "2"));
                                    ddlInfoDrop.Items.Insert(2, new ListItem("3 Semestre", "3"));
                                    ddlInfoDrop.Items.Insert(3, new ListItem("4 Semestre", "4"));
                                    break;
                            }
                            //Cargando el valor al Dropdown
                            ddlInfoDrop.Enabled = false;
                            ddlInfoDrop.SelectedValue = lblDropdown.Text;
                        }
                    }
                }
            }
        }
    }


}