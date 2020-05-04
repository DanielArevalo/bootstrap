using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Icetex.Services;
using Xpinn.Icetex.Entities;

public partial class Lista : GlobalWeb
{
    ConvocatoriaServices ConvocatoriaServ = new ConvocatoriaServices();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ConvocatoriaServ.CodigoProgramaCarga, "L");
            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvocatoriaServ.CodigoProgramaCarga, "Page_PreInit", ex);
        }
    }
    

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvocatoriaServ.CodigoProgramaCarga, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            GuardarValoresConsulta(pConsulta, ConvocatoriaServ.CodigoProgramaCarga);
            Actualizar();
        }
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        LimpiarValoresConsulta(pConsulta, ConvocatoriaServ.CodigoProgramaCarga);
        mvPrincipal.ActiveViewIndex = 0;
        txtNumCredito.Text = "";
        txtIdentificacion.Text = "";
        txtNombre.Text = "";
        txtFecIni.Text = "";
        txtFecFin.Text = "";
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
    }

    

    protected bool ObtenerListaDocumentos(ref List<CreditoIcetexDocumento> lstArchivos)
    {
        //Guardando documentos
        foreach (GridViewRow rFila in gvDocumentosReq.Rows)
        {
            int pCod_tipo_doc = Convert.ToInt32(gvDocumentosReq.DataKeys[rFila.RowIndex].Value.ToString());
            CreditoIcetexDocumento pEntidad = new CreditoIcetexDocumento();
            Label lblPegrunta = (Label)rFila.FindControl("lblPegrunta");
            FileUpload fuArchivo = (FileUpload)rFila.FindControl("fuArchivo");
            CheckBox chkRespuesta = (CheckBox)rFila.FindControl("chkRespuesta");

            if (fuArchivo != null)
            {
                if(fuArchivo.HasFile)
                {
                    String extension = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName).ToLower();
                    if (extension != ".pdf")
                    {
                        VerError("El archivo en la Fila " + (rFila.RowIndex + 1) + " no tiene la extensión PDF");
                        return false;
                    }
                    //Capturando el tamaño establecido
                    int tamMax = 1048576;

                    if (fuArchivo.FileBytes.Length > tamMax)
                    {
                        VerError("El tamaño del archivo en la fila " + (rFila.RowIndex + 1) + " excede el tamaño limite de ( 1MB )");
                        return false;
                    }

                    StreamsHelper streamHelper = new StreamsHelper();
                    byte[] bytesArrImagen;
                    using (System.IO.Stream streamImagen = fuArchivo.PostedFile.InputStream)
                    {
                        bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
                    }

                    pEntidad.cod_credoc = 0;
                    pEntidad.numero_credito = Convert.ToInt64(lblNumCredito.Text);
                    pEntidad.cod_tipo_doc = pCod_tipo_doc;
                    pEntidad.pregunta = lblPegrunta.Text.Trim() != "" ? lblPegrunta.Text.Trim() : null;
                    if (pEntidad.pregunta == null)
                        pEntidad.respuesta = null;
                    else
                        pEntidad.respuesta = chkRespuesta.Checked ? "1" : "0";
                    pEntidad.imagen = bytesArrImagen;
                    lstArchivos.Add(pEntidad);
                }
            }

            
        }
        return true;
    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            List<CreditoIcetexDocumento> lstArchivos = new List<CreditoIcetexDocumento>();
            if (ObtenerListaDocumentos(ref lstArchivos))
            {
                if (lstArchivos.Count > 0)
                {
                    bool rpta = ConvocatoriaServ.CrearCreditoIcetexDocumento(lstArchivos, Usuario);
                    if (!rpta)
                    {
                        VerError("Se generó un error a grabar los documentos");
                        return;
                    }
                    lblMsj.Text = "Se cargaron correctamente los documentos para el crédito Nro : " + lblNumCredito.Text;
                    Site toolBar = (Site)Master;
                    toolBar.MostrarLimpiar(false);
                    toolBar.MostrarGuardar(false);
                    mvPrincipal.ActiveViewIndex = 2;
                }
                else
                {
                    VerError("No existen documentos seleccionados para realizar la carga");
                    return;
                }
            }
            else
            {
                VerError("Se generó un error a obtener los documentos a grabar");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    
    private string obtFiltro()
    {
        ConnectionDataBase conexion = new ConnectionDataBase();
        string sFiltro = string.Empty;
        if (txtNumCredito.Text.Trim() != "")
        {
            sFiltro += " AND C.NUMERO_CREDITO = " + txtNumCredito.Text.Trim();
        }
        if (txtIdentificacion.Text.Trim() != "")
        {
            sFiltro += " AND C.IDENTIFICACION = '" + txtIdentificacion.Text.Trim() + "'";
        }
        if (txtNombre.Text.Trim() != "")
        {
            sFiltro += " AND UPPER(Trim(Substr(C.primer_nombre || ' ' || C.segundo_nombre || ' ' ||  C.primer_apellido || ' ' || C.segundo_apellido, 0, 240))) like '" + txtNombre.Text.Trim().ToUpper() + "'";
        }
        if (txtFecIni.TieneDatos)
            if (txtFecIni.ToDate.Trim() != "")
                if (conexion.TipoConexion() == "ORACLE")
                    sFiltro += " And C.FECHA_SOLICITUD >= To_Date('" + txtFecIni.ToDate.Trim() + "', '" + gFormatoFecha + "')";
                else
                    sFiltro += " And C.FECHA_SOLICITUD <= '" + txtFecIni.ToDate.Trim() + "', '" + gFormatoFecha + "'";
        if (txtFecFin.TieneDatos)
            if (txtFecFin.ToDate.Trim() != "")
                if (conexion.TipoConexion() == "ORACLE")
                    sFiltro += " And C.FECHA_SOLICITUD <= To_Date('" + txtFecFin.ToDate.Trim() + "', '" + gFormatoFecha + "')";
                else
                    sFiltro += " And C.FECHA_SOLICITUD <= '" + txtFecFin.ToDate.Trim() + "', '" + gFormatoFecha + "'";
        if (ddlEstado.SelectedIndex > 0)
            sFiltro += " AND C.ESTADO = '" + ddlEstado.SelectedValue + "'";

        if (!string.IsNullOrEmpty(sFiltro))
        {
            sFiltro = sFiltro.Substring(4);
            sFiltro = " WHERE " + sFiltro;
        }
        return sFiltro;
    }

    private void Actualizar()
    {
        try
        {
            List<CreditoIcetex> lstConsulta = new List<CreditoIcetex>();
            String pFiltro = obtFiltro();
            lstConsulta = ConvocatoriaServ.ListarCreditosIcetex(pFiltro, (Usuario)Session["usuario"]);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfo.Visible = false;
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
                lblInfo.Visible = true;
            }
            Session.Add(ConvocatoriaServ.CodigoProgramaCarga + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ConvocatoriaServ.CodigoProgramaCarga, "Actualizar", ex);
        }
    }


    protected void ActualizarDocumentos(Int64 pCod_convocatoria)
    {
        try
        {
            List<IcetexDocumentos> lstDocumentos = new List<IcetexDocumentos>();
            string pFiltro = " where C.COD_CONVOCATORIA = " + pCod_convocatoria + " AND C.TIPO_INFORMACION = 1";
            lstDocumentos = ConvocatoriaServ.ListarConvocatoriaDocumentos(pFiltro,Usuario);
            Site toolBar = (Site)Master;
            if (lstDocumentos.Count() > 0)
            {
                gvDocumentosReq.Visible = true;
                lblInfoDocu.Visible = false;
                gvDocumentosReq.DataSource = lstDocumentos;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                gvDocumentosReq.Visible = false;
                lblInfoDocu.Visible = true;
                gvDocumentosReq.DataSource = null;
                toolBar.MostrarGuardar(false);
            }
            gvDocumentosReq.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            BOexcepcion.Throw(ConvocatoriaServ.CodigoProgramaCarga, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.DataKeys[e.NewEditIndex].Values[0].ToString();
        string pConvocatoria = gvLista.DataKeys[e.NewEditIndex].Values[1].ToString();
        string pIdenti = gvLista.Rows[e.NewEditIndex].Cells[5].Text;
        string pNombres = string.Empty, pApellidos = string.Empty, pNombre = string.Empty, pTipoBene = string.Empty;
        pNombres = gvLista.Rows[e.NewEditIndex].Cells[6].Text + " " + gvLista.Rows[e.NewEditIndex].Cells[7].Text;
        pApellidos = gvLista.Rows[e.NewEditIndex].Cells[8].Text + " " + gvLista.Rows[e.NewEditIndex].Cells[9].Text;
        pNombre = pNombres.Trim() + " " + pApellidos.Trim();
        pTipoBene = gvLista.Rows[e.NewEditIndex].Cells[4].Text;        
        //Asignando valores a labels
        lblNumCredito.Text = id;
        lblIdentificacion.Text = pIdenti != null ? pIdenti.Trim() : "";
        lblNombre.Text = pNombre;
        lblTipoBene.Text = pTipoBene;
        lblCodBene.Text = gvLista.DataKeys[e.NewEditIndex].Values[2].ToString();
        ActualizarDocumentos(Convert.ToInt64(pConvocatoria));
        mvPrincipal.ActiveViewIndex = 1;
        e.NewEditIndex = -1;
    }


    protected void gvDocumentosReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPegrunta = (Label)e.Row.FindControl("lblPegrunta");
            if (lblPegrunta != null)
            {
                CheckBox chkRespuesta = (CheckBox)e.Row.FindControl("chkRespuesta");
                chkRespuesta.Visible = lblPegrunta.Text.Trim() != "" ? true : false;
            }
        }
    }


}