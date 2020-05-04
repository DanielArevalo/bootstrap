using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;
using System.Globalization;
using System.Web.UI.HtmlControls;

public partial class Lista : GlobalWeb
{
    EmpresaNovedadService _empresaService = new EmpresaNovedadService();
    List<Vacaciones> _lstRegistroVacaciones;
    int _contadorRegistro;
    Usuario _usuario;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_empresaService.CodigoProgramaVacaciones, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += ctlContinuarMen_Click;
            ctlMensajeBorrar.eventoClick += CtlMensajeBorrar_eventoClick;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += (s, evt) => ctlMensaje.MostrarMensaje("Quieres proceder con la importación?");
            toolBar.eventoImportar += (s, evt) =>
            {
                VerError("");
                toolBar.MostrarCancelar(true);
                toolBar.MostrarConsultar(false);
                toolBar.MostrarImportar(false);
                toolBar.MostrarLimpiar(false);
                toolBar.MostrarNuevo(false);

                gvDatos.DataSource = null;
                gvDatos.DataBind();
                gvErrores.DataSource = null;
                gvErrores.DataBind();
                cpeDemo1.CollapsedText = "(Click Aquí para Mostrar Detalles...)";
                pnlNotificacion.Visible = false;

                mvPrincipal.ActiveViewIndex = 1;
            };

            toolBar.eventoCancelar += (s, evt) =>
            {
                VerError("");
                toolBar.MostrarCancelar(false);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarConsultar(true);
                toolBar.MostrarImportar(true);
                toolBar.MostrarLimpiar(true);
                toolBar.MostrarNuevo(true);

                mvPrincipal.ActiveViewIndex = 0;
            };

            toolBar.eventoNuevo += (s, evt) =>
            {
                Session.Remove(_empresaService.CodigoProgramaVacaciones + ".id");
                Navegar(Pagina.Nuevo);
            };

            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_empresaService.CodigoProgramaVacaciones, "Page_PreInit", ex);
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTVACACIONES"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTVACACIONES");
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=Vacaciones.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        _usuario = (Usuario)Session["usuario"];
        _lstErroresCarga = new List<Xpinn.FabricaCreditos.Entities.ErroresCarga>();

        if (!IsPostBack)
        {
            InicializarPagina();
        }
        else
        {
            if (ViewState["_lstRegistroVacacionesImportar"] != null)
            {
                _lstRegistroVacaciones = (List<Vacaciones>)ViewState["_lstRegistroVacacionesImportar"];
            }
        }
    }

    void InicializarPagina()
    {
        ucFecha.Text = DateTime.Today.ToShortDateString();
        LlenarListasDesplegables(TipoLista.Empresa, ddlEntidad, ddlPagaduria);
        if (ddlEntidad.SelectedItem != null)
        {
            ddlEntidad.AppendDataBoundItems = true;
            ddlEntidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEntidad.SelectedIndex = 0;
        }
    }



    #endregion


    #region Eventos Intermedios GridView - Botonera


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value);
        Session[_empresaService.CodigoProgramaVacaciones + ".id"] = id;
        Navegar(Pagina.Nuevo);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvLista.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensajeBorrar.MostrarMensaje("Seguro que deseas eliminar este registro?");
    }

    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _empresaService.EliminarVacaciones(idBorrar, _usuario);
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvGarantias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (_lstRegistroVacaciones != null && _lstRegistroVacaciones.Count > 0)
            {
                _lstRegistroVacaciones.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

                gvDatos.DataSource = _lstRegistroVacaciones;
                gvDatos.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError("Error al eliminar una fila de la tabla, " + ex.Message);
        }
    }

    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtIdentificacion.Text = string.Empty;
        txtFecha.Text = string.Empty;
        txtCodPersona.Text = string.Empty;
    }

    void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            pnlNotificacion.Visible = false;

            if (string.IsNullOrWhiteSpace(ddlPagaduria.SelectedValue))
            {
                VerError("No puedes cargar vacaciones si no hay una pagaduria valida seleccionada!.");
                return;
            }

            if (avatarUpload.PostedFile.ContentLength > 0)
            {
                _contadorRegistro = 1;
                _lstRegistroVacaciones = new List<Vacaciones>();
                ConcurrentHelper<Stream, string[]> concurrentHelper = new ConcurrentHelper<Stream, string[]>();
                StreamsHelper streamHelper = new StreamsHelper();
                Task<bool> producerWork = null;
                Task<bool> consumerWork = null;

                using (Stream stream = avatarUpload.PostedFile.InputStream)
                {
                    // Producer - Consumer Design :D
                    producerWork = Task.Factory.StartNew(() => concurrentHelper.ProduceWork(stream, streamHelper.LeerLineaArchivoDelimitadoYDevolverSinLeerTodo));
                    consumerWork = Task.Factory.StartNew(() => concurrentHelper.ConsumeWork(ProcesarLineaDeArchivo));
                    Task.WaitAll(producerWork, consumerWork);
                }

                if (!producerWork.Result || !consumerWork.Result)
                {
                    VerError("La carga de archivos ha quedado incompleta por un error en el sistema, se muestran los archivos que se han podido cargar");
                }

                if (_lstErroresCarga != null)
                {
                    cpeDemo1.CollapsedText = "(Click Aquí para ver " + _lstErroresCarga.Count + " errores...)";
                }

                gvErrores.DataSource = _lstErroresCarga;
                gvErrores.DataBind();

                //Agregado para validar que la pagaduria de cada persona corresponda con la pagaduria seleccionada
                PersonaEmpresaRecaudoServices PersonaEmpresaServicio = new PersonaEmpresaRecaudoServices();
                PersonaEmpresaRecaudo empresaRecaudo = new PersonaEmpresaRecaudo();
                bool valida_empresa = false;
                String error ="";
                String datos = "";
                foreach (Vacaciones entidad in _lstRegistroVacaciones)
                {
                    List<PersonaEmpresaRecaudo> lstEmpresas = PersonaEmpresaServicio.ListarPersonaEmpresaRecaudo(entidad.cod_persona, (Usuario)Session["usuario"]);
                    lstEmpresas = lstEmpresas.Where(x => x.cod_persona != null && x.idempresarecaudo != null).ToList();
                    foreach (PersonaEmpresaRecaudo empresa in lstEmpresas)
                    {
                        empresaRecaudo = empresa;
                        if (empresa.cod_empresa == Convert.ToInt64(ddlPagaduria.SelectedValue))
                        {
                            valida_empresa = true;
                            break;
                        }
                    }
                    if (valida_empresa == false)
                    {
                        error = "La pagaduria seleccionada no corresponde con la registrada para la persona con No. Identificación: " + entidad.identificacion;
                        datos = "Identificacion: " + entidad.identificacion + ", Pagaduria: " + empresaRecaudo.descripcion;
                        RegistrarErrorImportar(_contadorRegistro, string.Empty, error, datos);
                    }

                    //Validar que el periodo no se encuentre ya generado 
                    if (ddlTipoCalculo.SelectedValue == "0")
                    {
                        EmpresaNovedad pNovedad = new EmpresaNovedad();
                        pNovedad.periodo_corte = Convert.ToDateTime(entidad.fecha_novedad);
                        pNovedad.cod_empresa = Convert.ToInt64(ddlPagaduria.SelectedValue);

                        pNovedad = _empresaService.ListarRecaudo(pNovedad, "", (Usuario)Session["usuario"]).FirstOrDefault();
                        if (pNovedad != null)
                        {
                            if (pNovedad.estado != "3" && pNovedad.estado != null && pNovedad.numero_novedad != 0)
                            {

                                error = "No puede registar datos de vacaciones para la fecha, ya se realizó el proceso de generación de novedades";
                                datos = "Identificación: " + entidad.identificacion + ", fecha novedad: " + entidad.fecha_novedad;
                                RegistrarErrorImportar(_contadorRegistro, string.Empty, error, datos);
                            }
                        }
                    }

                    //Validar que el periodo no se encuentre ya aplicado 
                    if (ddlTipoCalculo.SelectedValue == "1")
                    {
                        RecaudosMasivos pRecaudo = new RecaudosMasivos();
                        RecaudosMasivosService recaudoService = new RecaudosMasivosService();
                        pRecaudo.periodo_corte = Convert.ToDateTime(entidad.fecha_novedad);
                        pRecaudo.cod_empresa = Convert.ToInt64(ddlPagaduria.SelectedValue);

                        pRecaudo = recaudoService.ConsultarRecaudo(pRecaudo, (Usuario)Session["usuario"]);
                        if (pRecaudo != null)
                        {
                            if (pRecaudo.estado == "2")
                            {
                                error = "No puede registar datos de vacaciones para la fecha, ya se realizó la aplicación del recaudo";
                                datos = "Identificación: " + entidad.identificacion + ", fecha novedad: " + entidad.fecha_novedad;
                                RegistrarErrorImportar(_contadorRegistro, string.Empty, error, datos);
                            }
                        }
                    }

                    //Validar que la persona no este retirada
                    string _estado = _empresaService.ConsultarEstadoPersonaAfiliacion(entidad.cod_persona, (Usuario)Session["usuario"]);
                    if (_estado.Trim() == "" || _estado == "R")
                    {
                        bool bPermiteRetirados = false;
                        if (empresaRecaudo != null)
                        { 
                            EmpresaRecaudoServices recaudoEmpresaRecaudo = new EmpresaRecaudoServices();
                            EmpresaRecaudo vRecaudos = new EmpresaRecaudo();
                            vRecaudos.cod_empresa = Convert.ToInt32(empresaRecaudo.cod_empresa);
                            vRecaudos = recaudoEmpresaRecaudo.ConsultarEMPRESARECAUDO(vRecaudos, (Usuario)Session["usuario"]);
                            if (vRecaudos != null)
                            {
                                if (vRecaudos.descuento_retiro == 1)
                                    bPermiteRetirados = true;
                            }
                        }
                        if (!bPermiteRetirados)
                        { 
                            error = "No puede registar personas retiradas. Estado:" + _estado;
                            datos = "Identificación: " + entidad.identificacion + ", fecha novedad: " + entidad.fecha_novedad;
                            RegistrarErrorImportar(_contadorRegistro, string.Empty, error, datos);
                        }
                    }

                }
                if (_lstErroresCarga.Count == 0)
                {
                    gvDatos.DataSource = _lstRegistroVacaciones;
                    gvDatos.DataBind();

                    ViewState.Add("_lstRegistroVacacionesImportar", _lstRegistroVacaciones);

                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(true);
                }
                else
                {
                    cpeDemo1.CollapsedText = "(Click Aquí para ver " + _lstErroresCarga.Count + " errores...)";
                    gvErrores.DataSource = _lstErroresCarga;
                    gvErrores.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            VerError("Error al cargar los datos del archivo" + ex.Message);
        }
    }


    void ctlContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        EmpresaNovedadService novedadServices = new EmpresaNovedadService();
        _lstErroresCarga.Clear();
        _contadorRegistro = 1;

        if (_lstRegistroVacaciones != null && _lstRegistroVacaciones.Count > 0)
        {
            try
            {
                foreach (var vacaciones in _lstRegistroVacaciones)
                {
                    try
                    {
                        //Agregado para no ingresar personas en periodos de vacaciones activos
                        EmpresaNovedadService EmpresaServicio = new EmpresaNovedadService();
                        EmpresaNovedad Empresa = new EmpresaNovedad();
                        String filtro = " WHERE PER.COD_PERSONA = " + vacaciones.cod_persona + " ORDER BY VAC.FECHA_NOVEDAD DESC";
                        Empresa = EmpresaServicio.ConsultarPersonaVacaciones(filtro, (Usuario)Session["usuario"]);
                        if (Empresa != null)
                        {
                            if (vacaciones.fecha_novedad < Empresa.fecha_final)
                            {
                                String error = "La persona con No. Identificación: " + vacaciones.identificacion + " tiene registrado un periodo de vacaciones que no ha finalizado";
                                String datos = "Identificacion: " + vacaciones.identificacion + ", Fecha final vacaciones: " + Convert.ToDateTime(Empresa.fecha_final).ToString("dd/MM/yyyy");
                                datos += "Se adicionará los periodos indicados, por favor verificar.";
                                RegistrarErrorImportar(_contadorRegistro, string.Empty, error, datos);
                            }
                            else
                            {
                                novedadServices.InsertarVacaciones(vacaciones, _usuario);
                            }
                        }
                        else
                            novedadServices.InsertarVacaciones(vacaciones, _usuario);
                    }
                    catch (Exception ex)
                    {
                        RegistrarErrorImportar(_contadorRegistro, string.Empty, ex.Message, vacaciones.numero_novedad.ToString());
                    }
                    finally
                    {
                        _contadorRegistro += 1;
                    }
                }

                if (_lstErroresCarga != null)
                    cpeDemo1.CollapsedText = "(Click Aquí para ver " + _lstErroresCarga.Count + " errores...)";

                if (_lstErroresCarga.Count == 0)
                {
                    lblImportacionCorrecta.Visible = true;
                    lblImportacionConErrores.Visible = false;
                    lblImportacionIncorrecta.Visible = false;
                }
                else if (_lstErroresCarga.Count == _lstRegistroVacaciones.Count)
                {
                    lblImportacionCorrecta.Visible = false;
                    lblImportacionConErrores.Visible = false;
                    lblImportacionIncorrecta.Visible = true;
                }
                else
                {
                    lblImportacionCorrecta.Visible = true;
                    lblImportacionConErrores.Visible = true;
                    lblImportacionIncorrecta.Visible = false;
                }

                gvErrores.DataSource = _lstErroresCarga;
                gvErrores.DataBind();

                pnlNotificacion.Visible = true;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
            }
            catch (Exception ex)
            {
                VerError("Error al guardar las vacaciones para importar, " + ex.Message);
            }
        }
        else
        {
            VerError("Debes subir un archivo!.");
        }
    }


    #endregion


    #region Métodos Ayuda


    void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();
            List<EmpresaNovedad> lstNovedades = _empresaService.ListarEmpresaNovedad(filtro, _usuario);

            if (lstNovedades.Count > 0)
            {
                pnlGrid.Visible = true;
                lblTotalRegs.Text = "Se encontraron " + lstNovedades.Count + " registros!.";
            }
            else
            {
                pnlGrid.Visible = false;
                lblTotalRegs.Text = "Su consulta no obtuvo ningún resultado!.";
            }

            gvLista.DataSource = lstNovedades;
            gvLista.DataBind();
            Session["DTVACACIONES"] = lstNovedades;
        }
        catch (Exception ex)
        {
            VerError("Error al actualizar la grilla, " + ex.Message);
        }
    }


    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(ddlEntidad.SelectedValue))
        {
            if (ddlEntidad.SelectedIndex > 0)
                filtro += " and vac.CODIGO_PAGADURIA = " + ddlEntidad.SelectedValue.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and vac.IDENTIFICACION like '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtCodPersona.Text))
        {
            filtro += " and vac.COD_PERSONA = " + txtCodPersona.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            filtro += " and vac.FECHA_NOVEDAD = to_date('" + Convert.ToDateTime(txtFecha.Text.Trim()).ToShortDateString() + "', 'dd/MM/yyyy') ";
        }

        if (txtCodigoNomina.Text != "")
            filtro += " and cod_nomina like '%" + txtCodigoNomina.Text + "%'";

        return filtro;
    }


    public void ProcesarLineaDeArchivo(string[] lineaAProcesar)
    {
        Vacaciones vacaciones = new Vacaciones();
        Persona1Service personaService = new Persona1Service();
        bool sinErrores = true;

        for (int i = 0; i < 3; i++)
        {

            #region Registros tabla Vacaciones

            if (i == 0)
            {
                try
                {
                    string linea = lineaAProcesar[i].Trim();

                    if (string.IsNullOrWhiteSpace(linea))
                    {
                        throw new Exception("La identificacion debe estar definida!.");
                    }

                    vacaciones.identificacion = linea;
                    vacaciones.cod_persona = personaService.ConsultarCodigopersona(linea, Usuario);

                    if (vacaciones.cod_persona <= 0)
                    {
                        throw new Exception("Esta persona no existe!.");
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false; RegistrarErrorImportar(_contadorRegistro, i.ToString(), "Código de la persona invalido, " + ex.Message, string.Join(" | ", lineaAProcesar)); break;
                }
            }

            if (i == 1)
            { try { vacaciones.fecha_novedad = Convert.ToDateTime(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarErrorImportar(_contadorRegistro, i.ToString(), "Número de novedad invalido, " + ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            if (i == 2)
            { try { vacaciones.numero_cuotas = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarErrorImportar(_contadorRegistro, i.ToString(), "Número de cuotas invalido, " + ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            #endregion

        }

        if (sinErrores)
        {
            vacaciones.fecha_grabacion = DateTime.Today;
            vacaciones.codigo_pagaduria = Convert.ToInt64(ddlPagaduria.SelectedValue);
            vacaciones.tipo_calculo = Convert.ToInt32(ddlTipoCalculo.SelectedValue);

            _lstRegistroVacaciones.Add(vacaciones);
        }

        _contadorRegistro += 1;
    }


    #endregion


}