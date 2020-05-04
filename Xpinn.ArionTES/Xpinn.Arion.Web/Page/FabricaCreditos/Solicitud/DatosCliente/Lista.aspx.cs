using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Xpinn.FabricaCreditos.Entities;

partial class Lista : GlobalWeb
{
    Persona1Service _datosClienteServicio = new Persona1Service();
    LineasCreditoService _lineasService = new LineasCreditoService();
    List<Credito> _lstRegistroCredito;
    List<ErroresCarga> _lstErroresCarga;
    Usuario _usuario;
    int _contadorRegistro;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_datosClienteServicio.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += ctlMensaje_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);

            ctlBusquedaPersonas.eventoEditar += gvListaTitulares_SelectedIndexChanged;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_datosClienteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            if (_usuario.codperfil != 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarImportar(false);
            }

            if (!IsPostBack)
            {
                if (Session["Codeudores"] != null)
                    Session.Remove("Codeudores");
                if (Session["Numero_Radicacion"] != null)
                    Session.Remove("Numero_Radicacion");

                rblTipoCredito.SelectedIndex = 0;
                ConsultarLineasCreditosActivasPorClasificacion();
            }
            else
            {
                if (ViewState["_lstRegistroCreditoImportar"] != null)
                {
                    _lstRegistroCredito = (List<Credito>)ViewState["_lstRegistroCreditoImportar"];
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_datosClienteServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");

        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);
    }


    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        mvPrincipal.ActiveViewIndex = 1;
        ucFecha.Text = DateTime.Now.ToShortDateString();

        Site toolBar = (Site)Master;
        toolBar.MostrarImportar(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarCancelar(true);

        gvDatos.DataSource = null;
        gvDatos.DataBind();
        gvErrores.DataSource = null;
        gvErrores.DataBind();
        pnlNotificacion.Visible = false;

        cpeDemo1.CollapsedText = "(Click Aquí para Mostrar Detalles...)";
    }


    private void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.ActiveViewIndex = 0;

        Site toolBar = (Site)Master;
        toolBar.MostrarImportar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarCancelar(false);
    }


    void ConsultarLineasCreditosActivasPorClasificacion()
    {
        for (int i = 0; i < rblTipoCredito.Items.Count; i++)
        {
            bool tieneLinea = _lineasService.ConsultarLineasCreditosActivasPorClasificacion(rblTipoCredito.Items[i].Value, _usuario);

            if (!tieneLinea)
            {
                rblTipoCredito.Items[i].Enabled = false;
            }
        }
    }


    protected void gvListaTitulares_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        ClasificacionCredito clasificacion = default(ClasificacionCredito);

        switch (rblTipoCredito.SelectedIndex)
        {
            case 0:
                clasificacion = ClasificacionCredito.Consumo;
                break;
            case 1:
                clasificacion = ClasificacionCredito.MicroCredito;
                break;
            case 2:
                clasificacion = ClasificacionCredito.Vivienda;
                break;
            case 3:
                clasificacion = ClasificacionCredito.Comercial;
                break;
        }

        Session["TipoCredito"] = clasificacion;

        // Determinar los datos de la persona seleccionada
        GridView gvListaAFiliados = (GridView)sender;
        string identificacion = gvListaAFiliados.Rows[gvListaAFiliados.SelectedRow.RowIndex].Cells[3].Text;
        Session[_datosClienteServicio.CodigoPrograma + ".id"] = identificacion;
        Session["Origen"] = "SDCL";//Solicitud de Datos Cliente Lista

        // Ir a la siguiente página
        Navegar("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx");
    }



    #region Sección Importar


    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        ctlMensaje.MostrarMensaje("Quieres proceder con la importación?");
    }


    private void ctlMensaje_Click(object sender, EventArgs e)
    {
        CreditoService creditoService = new CreditoService();
        _lstErroresCarga.Clear();
        _contadorRegistro = 1;

        if (_lstRegistroCredito != null && _lstRegistroCredito.Count > 0)
        {
            try
            {
                foreach (var credito in _lstRegistroCredito)
                {
                    try
                    {
                        creditoService.CrearCreditoDesdeFuncionImportacion(credito, _usuario);
                    }
                    catch (Exception ex)
                    {
                        RegistrarError(_contadorRegistro, string.Empty, ex.Message, credito.numero_radicacion.ToString());
                    }
                    finally
                    {
                        _contadorRegistro += 1;
                    }
                }

                if (_lstErroresCarga != null)
                {
                    cpeDemo1.CollapsedText = "(Click Aquí para ver " + _lstErroresCarga.Count + " errores...)";
                }

                gvErrores.DataSource = _lstErroresCarga;
                gvErrores.DataBind();

                pnlNotificacion.Visible = true;
                _lstRegistroCredito.Clear();
            }
            catch (Exception ex)
            {
                VerError("Error al guardar los créditos para importar, " + ex.Message);
            }
        }
    }


    protected void btnCargarPersonas_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            pnlNotificacion.Visible = false;

            if (string.IsNullOrWhiteSpace(ucFecha.Text))
            {
                VerError("Ingrese la fecha de carga");
                return;
            }

            if (avatarUpload.PostedFile.ContentLength > 0)
            {
                _contadorRegistro = 1;
                _lstErroresCarga = new List<ErroresCarga>();
                _lstRegistroCredito = new List<Credito>();
                ConcurrentHelper<Stream, string[]> concurrentHelper = new ConcurrentHelper<Stream, string[]>();
                Task<bool> producerWork = null;
                Task<bool> consumerWork = null;

                using (Stream stream = avatarUpload.PostedFile.InputStream)
                {
                    // Producer - Consumer Design :D
                    producerWork = Task.Factory.StartNew(() => concurrentHelper.ProduceWork(stream, LeerLineaDeArchivo));
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

                gvDatos.DataSource = _lstRegistroCredito;
                gvDatos.DataBind();             

                ViewState.Add("_lstRegistroCreditoImportar", _lstRegistroCredito);

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(true);
            }
        }
        catch (Exception ex)
        {
            VerError("Error al cargar los datos del archivo" + ex.Message);
        }
    }


    public IEnumerable<string[]> LeerLineaDeArchivo(Stream stream)
    {
        string linea = string.Empty;
        char separador = '|';

        using (StreamReader strReader = new StreamReader(stream))
        {
            while ((linea = strReader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(linea)) continue;

                // Retorna el string[] por cada vuelta, no espera a que el while termine
                // Despues de retornar, vuelve al while y retorna el siguiente string[]
                // Sale del while al no haber mas lineas que leer (trReader.ReadLine()) == null)
                // Ese es el comportamiento del yield return
                yield return linea.Split(separador);
            }
        }
    }


    public void ProcesarLineaDeArchivo(string[] lineaAProcesar)
    {
        Persona1Service personaService = new Persona1Service();
        Credito credito = new Credito();
        bool sinErrores = true;
        string sformato_fecha = ddlFormatoFecha.SelectedValue;

        for (int i = 0; i < 61; i++)
        {

            #region Registros tabla Credito


            if (i == 0)
            {
                try
                {
                    credito.identificacion = lineaAProcesar[i].Trim();

                    if (!string.IsNullOrWhiteSpace(credito.identificacion))
                    {
                        credito.cod_deudor = personaService.ConsultarCodigopersona(credito.identificacion, _usuario);

                        if (credito.cod_deudor == 0)
                        {
                            RegistrarError(_contadorRegistro, i.ToString(), "Persona no encontrada para la identificación, " + credito.identificacion, string.Join(" | ", lineaAProcesar));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }

            if (i == 1)
            { try { credito.numero_radicacion = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 2)
            { try { credito.codigo_oficina = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 3)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.cod_linea_credito = lineaAProcesar[i].Trim(); else throw new Exception("Codigo Linea de Credito vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            //if (i == 4)
            //{ try { credito.nom_linea_credito = lineaAProcesar[i].Trim(); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            if (i == 5)
            { try { credito.monto_solicitado = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToDecimal(lineaAProcesar[i].Trim()) : 0; } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 6)
            { try { credito.monto_aprobado = Convert.ToDecimal(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 7)
            { try { credito.monto_desembolsado = Convert.ToDecimal(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 8)
            { try { credito.cod_moneda = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 9)
            { try { credito.fecha_solicitud = Convert.ToDateTime(ucFecha.Text); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 10)
            { try { credito.fecha_aprobacion = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null) : default(DateTime?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 11)
            { try { credito.fecha_desembolso = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 12)
            { try { credito.fecha_prim_pago = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 13)
            { try { credito.numero_cuotas = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 14)
            { try { credito.cuotas_pagadas = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 15)
            { try { credito.cuotas_pendientes = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 16)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.periodicidad = lineaAProcesar[i].Trim(); else throw new Exception("Periodicidad vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 17)
            { try { credito.tipo_liquidacion = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 18)
            { try { credito.valor_cuota = Convert.ToDecimal(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 19)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.forma_pago = lineaAProcesar[i].Trim(); else throw new Exception("Forma de Pago vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 20)
            { try { credito.fecha_ultimo_pago = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 21)
            { try { credito.fecha_vencimiento = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 22)
            { try { credito.fecha_prox_pago = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 23)
            { try { credito.tipo_gracia = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToInt32(lineaAProcesar[i].Trim()) : default(int?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 24)
            { try { credito.periodo_gracia = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToDecimal(lineaAProcesar[i].Trim()) : default(decimal?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 25)
            { try { credito.cod_clasifica = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 26)
            { try { credito.saldo_capital = Convert.ToDecimal(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 27)
            { try { credito.otros_saldos = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToInt64(lineaAProcesar[i].Trim()) : 0; } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 28)
            { try { credito.CodigoAsesor = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToInt64(lineaAProcesar[i].Trim()) : 0; } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 29)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.tipo_credito = lineaAProcesar[i].Trim(); else throw new Exception("Tipo de Credito vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 30)
            { try { credito.num_radic_origen = Convert.ToInt64(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 31)
            { try { credito.valor_para_refinanciar = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToDecimal(lineaAProcesar[i].Trim()) : 0; } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            //if (i == 32)
            //{ try { credito.nom_pagaduria = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            if (i == 33)
            { try { credito.cod_pagaduria = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 34)
            { try { credito.gradiente = !string.IsNullOrWhiteSpace(lineaAProcesar[i]) ? Convert.ToInt32(lineaAProcesar[i].Trim()) : 0; } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 35)
            { try { credito.fecha_inicio = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 36)
            { try { credito.dias_ajuste = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            if (i == 37)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.estado = lineaAProcesar[i].Trim(); else throw new Exception("Estado vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Tipo Garantia
            //if (i == 38)
            //{ try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.garan = lineaAProcesar[i].Trim(); else throw new Exception("Estado vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            //Saldo capital Vencido a la fecha
            //if (i == 39)
            //{ try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.estado = lineaAProcesar[i].Trim(); else throw new Exception("Estado vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Saldo de interes corriente vencido a la fecha
            //if (i == 40)
            //{ try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.garan = lineaAProcesar[i].Trim(); else throw new Exception("Estado vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            #endregion


            #region Registros tabla DocumentosGarantias


            credito.Documento_Garantia = new Documento() { numero_radicacion = credito.numero_radicacion, tipo_documento = 1 };
            if (i == 41)
            { try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.Documento_Garantia.referencia = lineaAProcesar[i].Trim(); else throw new Exception("Numero de Pagare vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            #endregion


            #region Registros tabla Codeudores


            credito.Codeudor1 = new codeudores() { numero_radicacion = credito.numero_radicacion };
            credito.Codeudor2 = new codeudores() { numero_radicacion = credito.numero_radicacion };
            credito.Codeudor3 = new codeudores() { numero_radicacion = credito.numero_radicacion };

            if (i == 42)
            {
                try
                {
                    credito.Codeudor1.identificacion = lineaAProcesar[i].Trim();

                    if (!string.IsNullOrWhiteSpace(credito.Codeudor1.identificacion))
                    {
                        credito.Codeudor1.codpersona = personaService.ConsultarCodigopersona(credito.Codeudor1.identificacion, _usuario);

                        if (credito.Codeudor1.codpersona == 0)
                        {
                            RegistrarError(_contadorRegistro, i.ToString(), "Persona no encontrada para la identificación, " + credito.Codeudor1.identificacion, string.Join(" | ", lineaAProcesar));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            if (i == 43)
            {
                try
                {
                    credito.Codeudor2.identificacion = lineaAProcesar[i].Trim();

                    if (!string.IsNullOrWhiteSpace(credito.Codeudor2.identificacion))
                    {
                        credito.Codeudor2.codpersona = personaService.ConsultarCodigopersona(credito.Codeudor2.identificacion, _usuario);

                        if (credito.Codeudor2.codpersona == 0)
                        {
                            RegistrarError(_contadorRegistro, i.ToString(), "Persona no encontrada para la identificación, " + credito.Codeudor2.identificacion, string.Join(" | ", lineaAProcesar));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            if (i == 44)
            {
                try
                {
                    credito.Codeudor3.identificacion = lineaAProcesar[i].Trim();

                    if (!string.IsNullOrWhiteSpace(credito.Codeudor3.identificacion))
                    {
                        credito.Codeudor3.codpersona = personaService.ConsultarCodigopersona(credito.Codeudor3.identificacion, _usuario);

                        if (credito.Codeudor3.codpersona == 0)
                        {
                            RegistrarError(_contadorRegistro, i.ToString(), "Persona no encontrada para la identificación, " + credito.Codeudor3.identificacion, string.Join(" | ", lineaAProcesar));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }


            #endregion


            #region Registros tabla Atributos Credito


            credito.AtributosCredito1 = new AtributosCredito() { cod_atr = 2, numero_radicacion = Convert.ToInt32(credito.numero_radicacion), tipo_tasa = 1, tipo_historico = null, calculo_atr = "1", desviacion = null,  };
            credito.AtributosCredito2 = new AtributosCredito() { cod_atr = 4, numero_radicacion = Convert.ToInt32(credito.numero_radicacion), tipo_tasa = 1, tipo_historico = null, calculo_atr = "1", desviacion = null, };
            credito.AtributosCredito3 = new AtributosCredito() { cod_atr = 7, numero_radicacion = Convert.ToInt32(credito.numero_radicacion), tipo_tasa = 1, tipo_historico = null, calculo_atr = "1", desviacion = null, };
            credito.AtributosCredito4 = new AtributosCredito() { cod_atr = 3, numero_radicacion = Convert.ToInt32(credito.numero_radicacion), tipo_tasa = 1, tipo_historico = null, calculo_atr = "1", desviacion = null, };

            // ATRIBUTO COD = 2

            // Tipo Calculo de Tasa
            //if (i == 45)
            //{ try { if (!string.IsNullOrWhiteSpace(lineaAProcesar[i])) credito.AtributosCredito1.calculo_atr = lineaAProcesar[i].Trim(); else throw new Exception("Tipo Calculo De Tasa vacia o nula"); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Tasa
            if (i == 46)
            { try { credito.AtributosCredito1.tasa = Convert.ToDecimal(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Tipo de Tasa
            //if (i == 47)
            //{ try { credito.AtributosCredito1.tipo_tasa = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            // Tipo pago interes
            //if (i == 48)
            //{ try { credito.AtributosCredito1. = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            // ATRIBUTO COD = 4 

            // Cobran Seguro en la Cuota
            //if (i == 49)
            //{ try { credito.AtributosCredito2. = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            
            // Valor Seguro de Vida Mensual
            if (i == 50)
            { try { credito.AtributosCredito2.tasa = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            // ATRIBUTO COD = 7

            // Valor Seguro de Cartera
            if (i == 51)
            { try { credito.AtributosCredito3.tasa = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Tipo de Pago Seguro Cartera
            //if (i == 52)
            //{ try { credito.AtributosCredito3. = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            // Valor Seguro Cartera Cobrado en Cuota
            //if (i == 53)
            //{ try { credito.AtributosCredito3. = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            // Valor Fijo Otros Conceptos Cobrados en la Cuota
            //if (i == 54)
            //{ try { credito.AtributosCredito3. = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            // ATRIBUTO COD = 3

            // Tasa Interes Mora
            if (i == 55)
            { try { credito.AtributosCredito4.tasa = Convert.ToInt32(lineaAProcesar[i].Trim()); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            #endregion


            #region Registros tabla Cuotas Extras


            credito.CuotasExtras = new CuotasExtras() { numero_radicacion = credito.numero_radicacion };

            //if (i == 56)
            //{ try { credito.CuotasExtras.fecha_pago = !string.IsNullOrWhiteSpace(lineaAProcesar[i].Trim()) ? DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null) : default(DateTime?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            //if (i == 57)
            //{ try { credito.CuotasExtras.valor = !string.IsNullOrWhiteSpace(lineaAProcesar[i].Trim()) ? Convert.ToInt32(lineaAProcesar[i].Trim()) : default(int?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }

            // Numero de Cuotas pactadas
            //if (i == 58)
            //{ try { credito.CuotasExtras.n = !string.IsNullOrWhiteSpace(lineaAProcesar[i].Trim()) ? Convert.ToDateTime(lineaAProcesar[i].Trim()) : default(DateTime?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            // Periodicidad Cuotas Pactadas
            //if (i == 59)
            //{ try { credito.CuotasExtras.perio = !string.IsNullOrWhiteSpace(lineaAProcesar[i].Trim()) ? Convert.ToDateTime(lineaAProcesar[i].Trim()) : default(DateTime?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }
            // Numero de Cuotas Pactadas X Pagar
            //if (i == 60)
            //{ try { credito.CuotasExtras.fecha_pago = !string.IsNullOrWhiteSpace(lineaAProcesar[i].Trim()) ? Convert.ToDateTime(lineaAProcesar[i].Trim()) : default(DateTime?); } catch (Exception ex) { sinErrores = false; RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar)); break; } }


            #endregion

        }

        if (sinErrores)
        {
            credito.cod_usuario = Convert.ToInt32(_usuario.codusuario);
            credito.fecha_creacion = DateTime.Now;

            _lstRegistroCredito.Add(credito);
        }

        _contadorRegistro += 1;
    }


    public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato)
    {
        ErroresCarga registro = new ErroresCarga();
        string placeholder = " Campo No.: ";

        registro.numero_registro = pNumeroLinea.ToString();
        registro.datos = pDato;

        if (string.IsNullOrWhiteSpace(pRegistro))
        {
            placeholder = string.Empty;
        }

        registro.error = placeholder + pRegistro + " Error:" + pError;

        _lstErroresCarga.Add(registro);
    }


    protected void gvDatos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (_lstRegistroCredito != null && _lstRegistroCredito.Count > 0)
            {
                _lstRegistroCredito.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

                gvDatos.DataSource = _lstRegistroCredito;
                gvDatos.DataBind();

                ViewState["_lstRegistroCreditoImportar"] = _lstRegistroCredito;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al eliminar una fila de la tabla, " + ex.Message);
        }
    }


    #endregion


}