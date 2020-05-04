using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using System.Collections.Generic;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;


partial class Detalle : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.DocumentosRequeridosService documentoservicio = new Xpinn.FabricaCreditos.Services.DocumentosRequeridosService();
    private Xpinn.FabricaCreditos.Services.CreditoService creditoservicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    //private Xpinn.FabricaCreditos.Services.DocumentosRequeridosService DocumentoRequeridoServicio = new Xpinn.FabricaCreditos.Services.DocumentosRequeridosService();
    private Xpinn.FabricaCreditos.Entities.documentosrequeridos documentos = new Xpinn.FabricaCreditos.Entities.documentosrequeridos();
   
    private Xpinn.FabricaCreditos.Entities.DocumentosAnexos documentos2 = new Xpinn.FabricaCreditos.Entities.DocumentosAnexos();
    private Xpinn.FabricaCreditos.Services.DocumentosAnexosService documentoanexosservicio = new Xpinn.FabricaCreditos.Services.DocumentosAnexosService();
  

    PoblarListas poblar = new PoblarListas();

    private List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables



    /// <summary>
    /// Mostrar la barra de herramientas al ingresar a la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[documentoservicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(documentoservicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(documentoservicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    /// <summary>
    /// Cargar datos de la funcionalidad
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDllLineas();
                if (Session[documentoservicio.CodigoPrograma + ".id"] != null)
                {
                    if (Request.UrlReferrer != null)

                        idObjeto = Session[documentoservicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    Actualizar();
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Page_Load", ex);
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Guardar();

        //lMensaje.MostrarMensaje("Desea grabar los datos de los documentos requeridos");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Guardar();

    }
    private void Guardar()
    {
        try
        {
            CheckBox chkentregado;
            TextBox txtobservaciones;
            Label lbltipodocumento;
            TextBox txtfechaentrega;
            TextBox txtfechanexo;
          //  Label lblmensaje;
            DocumentosAnexos datos = new DocumentosAnexos();
            Boolean continuar = true;
            Configuracion conf = new Configuracion();
            String format = conf.ObtenerFormatoFecha();

            foreach (GridViewRow wrow in gvLista.Rows)
            {
                chkentregado = (CheckBox)wrow.FindControl("ChkEntregado");
                txtobservaciones = (TextBox)wrow.FindControl("txtobservaciones");
                txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
                txtfechanexo = (TextBox)wrow.FindControl("txtfechaanexo");
                lbltipodocumento = (Label)wrow.FindControl("Lbldocumento");
                //lblmensaje = (Label)wrow.FindControl("lblmensaje");

                if (txtNumero_solicitud.Text != null)
                {
                    datos.numerosolicitud = 0;
                }
                else
                {
                    datos.numerosolicitud = Convert.ToInt32(this.txtNumero_solicitud.Text);

                }
                datos.numero_radicacion = Convert.ToInt32(this.txtNumero_radicacion.Text);
                datos.tipo_documento = Convert.ToInt32(lbltipodocumento.Text);

               // if (chkentregado.Checked || txtobservaciones.Text != null || txtfechaentrega.Text != null)
                //{
                    DocumentosAnexos lstConsultadocumentos = new DocumentosAnexos();

                    try
                    {
                        if (chkentregado.Checked == true)
                        {
                            if (txtfechanexo.Text == "")
                            {
                                //lblmensaje.Text = "Debe ingresar una fecha de anexo";
                                continuar = false;
                            }                          
                            
                            else
                            {

                                continuar = true;

                              //  lblmensaje.Text = "";
                            
                            datos.estado = 1;
                            txtfechaentrega.Visible = false;
                            datos.fechaentrega = null;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaanexo = Convert.ToDateTime(txtfechanexo.Text);
                            }
                        }
                        if (chkentregado.Checked == false)
                        
                        {
                            if (txtfechaentrega.Text == "")
                            {
                                //lblmensaje.Text = "Debe ingresar una fecha de posible entrega";
                                continuar = false;
                            }
                            else
                            {
                                continuar = true;
                               // lblmensaje.Text = "";
                            
                            datos.fechaanexo = null;
                            datos.estado = 0;
                            txtfechaentrega.Visible = true;
                            datos.descripcion = txtobservaciones.Text;
                            datos.fechaentrega = Convert.ToDateTime(txtfechaentrega.Text);
                            }
                        }
                        //if (continuar==true)
                           // {
                        lstConsultadocumentos = documentoanexosservicio.CrearDocAnexos(datos, (Usuario)Session["usuario"]);
                            //}
                    }
                    catch (Exception ex)
                    {

                    }
                //}
            }

            Session[documentoservicio.CodigoPrograma + ".id"] = idObjeto;

            Navegar(Pagina.Lista);
            //Site toolBar = (Site)this.Master;
            //  toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    /// <summary>
    /// Evento para consultar los datos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Evento para cancelar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[documentoservicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Detalle);
        }

    }


    /// <summary>
    /// Mostrar los datos del crédito
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Credito vCredito = new Credito();
            vCredito = creditoservicio.ConsultarCredito(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (vCredito.numero_radicacion != Int64.MinValue)
                txtNumero_radicacion.Text = vCredito.numero_radicacion.ToString().Trim();
            if (vCredito.identificacion != string.Empty)
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vCredito.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.tipo_identificacion))
                txtTipo_identificacion.Text = HttpUtility.HtmlDecode(vCredito.tipo_identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vCredito.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.cod_linea_credito))
                this.ddlLineas.SelectedValue = HttpUtility.HtmlDecode(vCredito.cod_linea_credito.ToString().Trim());
            if (vCredito.monto != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(vCredito.monto.ToString().Trim());
            if (vCredito.plazo != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(vCredito.plazo.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.periodicidad))
                txtPeriodicidad.Text = HttpUtility.HtmlDecode(vCredito.periodicidad.ToString().Trim());
            if (vCredito.valor_cuota != Int64.MinValue)
                txtValor_cuota.Text = HttpUtility.HtmlDecode(vCredito.valor_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.forma_pago))
                txtForma_pago.Text = HttpUtility.HtmlDecode(vCredito.forma_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vCredito.estado))
                txtEstado.Text = HttpUtility.HtmlDecode(vCredito.estado);

            if (vCredito.fecha_solicitud != DateTime.MinValue)
                txtFechaSolicitud.Text = vCredito.fecha_solicitud.ToString().Trim();
            if (!string.IsNullOrEmpty(vCredito.numero_obligacion))
                this.txtNumero_solicitud.Text = HttpUtility.HtmlDecode(vCredito.numero_obligacion.ToString().Trim());



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Es para cuando se cambia a la siguiente página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }


    void CargarDllLineas()
    {
        poblar.PoblarListaDesplegable("lineascredito", ddlLineas, (Usuario)Session["usuario"]);

        Xpinn.FabricaCreditos.Data.LineasCreditoData listaLinea = new Xpinn.FabricaCreditos.Data.LineasCreditoData();
        Xpinn.FabricaCreditos.Entities.LineasCredito linea = new Xpinn.FabricaCreditos.Entities.LineasCredito();

        var lista = listaLinea.ListarLineasCredito(linea, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.FabricaCreditos.Entities.LineasCredito { nom_linea_credito = "Seleccione un Item", cod_lineacredito = 0 });
            this.ddlLineas.DataSource = lista;
            ddlLineas.DataTextField = "nom_linea_credito";
            ddlLineas.DataValueField = "Codigo";
            ddlLineas.DataBind();
        }

    }


    private void Actualizar()
    {
        Int64 pId = 0;
        Int32 cod_linea_credito = Convert.ToInt32(ddlLineas.SelectedValue);
        try
        {
            List<DocumentosAnexos> lstConsulta = new List<DocumentosAnexos>();
            documentos2.numero_radicacion= Convert.ToInt32(txtNumero_radicacion.Text);
            lstConsulta = documentoanexosservicio.ListarDocAnexos(documentos2, cod_linea_credito, (Usuario)Session["usuario"]);

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
                //  ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(documentoservicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(documentoservicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void txtPlazo_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ChkEntregado_CheckedChanged(object sender, EventArgs e)
    {
        documentosrequeridos documentosrequeridos = new documentosrequeridos();
        TextBox txtfechaentrega;
        TextBox txtfechaanexo;
        CheckBox ChkEntregado;
        foreach (GridViewRow wrow in gvLista.Rows)
        {
            txtfechaentrega = (TextBox)wrow.FindControl("txtfechaentrega");
            txtfechaanexo = (TextBox)wrow.FindControl("txtfechaanexo");

            ChkEntregado = (CheckBox)wrow.FindControl("ChkEntregado");


            if (ChkEntregado.Checked == true)
            {
                txtfechaentrega.Visible = false;
            }
            else
            {
                txtfechaentrega.Visible = true;
            }

            if (ChkEntregado.Checked == false)
            {
                txtfechaanexo.Visible = false;
            }
            else
            {
                txtfechaanexo.Visible = true;
            }
        }


    }
}