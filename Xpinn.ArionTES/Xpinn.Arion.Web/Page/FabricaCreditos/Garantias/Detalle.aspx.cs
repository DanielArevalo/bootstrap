using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.ActivosFijos.Services;
using Xpinn.ActivosFijos.Entities;
using System.Linq;

public partial class Detalle : GlobalWeb
{
    GarantiaService _garantiasservicio = new GarantiaService();
    Usuario _usuario;
    string _cod_persona;
    long _idGarantia;
    long _idActivo;



    #region Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            // Si estoy entrando a registrar una garantia
            if (Session[_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.Nuevo"] != null)
                VisualizarOpciones(_garantiasservicio.CodigoPrograma2, "E");

            // Si estoy entrando a modificar una garantia
            else
                VisualizarOpciones(_garantiasservicio.CodigoPrograma2, "A");

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlMensajeActivoFijo.eventoClick += btnContinuarMenActivoFijo_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_garantiasservicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Mantengo mis variables globales llenas tras cada postback
            _usuario = (Usuario)Session["usuario"];
            _cod_persona = (string)Session[_garantiasservicio.CodigoPrograma2 + ".codPersona"];
            object idGarantia = ViewState["idGarantia"];

            if (idGarantia != null)
            {
                _idGarantia = (long)idGarantia;
            }

            if (!IsPostBack)
            {
                // Verifico de donde vengo y asigno el numero de radicacion valido
                string noRadicacion = string.Empty;
                object noRadicacionListaGarantia = Session[_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.ListaGarantia"];

                if (noRadicacionListaGarantia != null)
                {
                    noRadicacion = (string)noRadicacionListaGarantia;
                }
                else
                {
                    noRadicacion = (string)Session[_garantiasservicio.CodigoPrograma2 + ".No.Radicacion.Nuevo"];
                }

                // Testeo si vengo de una garantia directa "YA" creada, osea si vengo a modificarla
                // si es asi consulto la garantía
                if (noRadicacionListaGarantia != null)
                {
                    ConsultarGarantia(noRadicacion);
                }

                // Lleno los datos del credito del cliente indiferentemente de donde venga
                LlenarDDLTipoIdentificacion();
                ConsultarDatosCreditoCliente(noRadicacion);
                LlenarGVActivoFijos();
                LlenarDDLTipoActivo();

                // Activo ReadOnly en controles autogenerados. Ej: Fechas
                DeshabilitarControlesFechas();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_garantiasservicio.GetType().Name + "A", "Page_Load", ex);
        }
    }


    // Deshabilito los controles de la fecha de tal manera que solo se pueda llenar con la animacion de seleccion de fecha
    private void DeshabilitarControlesFechas()
    {
        txtFechaSuscrip.Attributes.Add("readonly", "readonly");
        txtFechaVenc.Attributes.Add("readonly", "readonly");
        txtFechaUltAvaluo.Attributes.Add("readonly", "readonly");
        txtFechaLib.Attributes.Add("readonly", "readonly");
        txtModalFechaIni.Attributes.Add("readonly", "readonly");
        txtModalFechaImportacion.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    // Devuelve a la anterior pantalla si estoy en Garantía
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        // Si estoy creando un Activo Fijo regreso a Garantía
        if (mvGarantia.ActiveViewIndex == 1)
        {
            mvGarantia.ActiveViewIndex = 0;
        }
        else
        {
            // Si _idGarantia es 0 estoy en modo CREADOR
            if (_idGarantia != 0)
            {
                Navegar("ListaGarantias.aspx");
            }
            else
            {
                Navegar("Nuevo.aspx");
            }

        }
    }

    private bool ValidarData()
    {
        if (string.IsNullOrEmpty(txtValorGarantia.Text))
        {
            VerError("Ingrese el valor de garantía, campo requerido.");
            txtValorGarantia.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtUbicacion.Text))
        {
            VerError("Ingrese la ubicación de la garantía, campo requerido.");
            txtUbicacion.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(txtEncargado.Text))
        {
            VerError("Ingrese el encargado de la garantía, campo requerido.");
            txtEncargado.Focus();
            return false;
        }

        return true;
    }

    // Muestro mensaje de confirmacion para guardar
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        string mensaje = "Desea proceder con el guardado?";

        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaSuscrip.Text), 137) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 137 (Contabilización de Garantías)");
            return;
        }
        // Si estoy creando un activo fijo muestro el anuncio para guardar activo fijo
        if (mvGarantia.ActiveViewIndex == 1)
        {
            ctlMensajeActivoFijo.MostrarMensaje(mensaje);
        }
        else
        {
            ctlMensaje.MostrarMensaje(mensaje);
        }
    }


    // Guardo Garantia
    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        string fechaGarantia = "";
        try
        {
            VerError("");

            string error = string.Empty;
            Tuple<long, int> activoSeleccionado = ValidarGVActivos(out error);

            // Si hay un error en la seleccion de gvActivos muestro error y retorno
            if (!string.IsNullOrWhiteSpace(error))
            {
                VerError(error);
                return;
            }

            // activoSeleccionado debe tener un valor valido o ya hubiera retornado
            Garantia garantia = ValidarCamposGarantia(activoSeleccionado, out error);
            fechaGarantia = garantia.FechaGarantia.ToString();

            // Si hay un error en campos muestro error y retorno
            if (!string.IsNullOrWhiteSpace(error))
            {
                VerError(error);
                return;
            }

            if (ddlEstadoGarantia.SelectedValue == "1")
            {

                // Si el _idGarantia es "0" vengo a crear, si no vengo a modificar
                if (_idGarantia == 0)
                {
                    garantia = _garantiasservicio.CrearGarantia(garantia, _usuario);
                }
                else
                {
                    garantia.cod_ope = string.IsNullOrEmpty(lblCodOpe.Text) ? 0 : Convert.ToInt64(lblCodOpe.Text);
                    garantia.origen = 1;

                    garantia = _garantiasservicio.ModificarGarantia(garantia.origen, garantia, _usuario);
                }
            }

            // anular garantia creada 

            if (ddlEstadoGarantia.SelectedValue == "3" || ddlEstadoGarantia.SelectedValue == "2")
            {
                garantia.origen = 2;
                garantia.cod_ope = 0;
                garantia = _garantiasservicio.ModificarGarantia(garantia.origen, garantia, _usuario);
            }

            //GENERAR EL COMPROBANTE
            if (garantia != null)
            {
                if(!string.IsNullOrEmpty(garantia.message))
                {
                    VerError(garantia.message);
                    return;
                }
                if (garantia.cod_ope > 0 || ddlEstadoGarantia.SelectedValue == "3" || ddlEstadoGarantia.SelectedValue == "1" || ddlEstadoGarantia.SelectedValue == "2")
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = garantia.num_comp;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = garantia.tipo_comp;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 137;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = garantia.cod_ope;
                   
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }

            // Activo View de Aviso de Guardado Exitoso, 
            // Oculto el boton de guardar y cancelar
            mvGarantia.ActiveViewIndex = 2;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            btnRegresarLista.Visible = true;
        }
        catch (Exception ex)
        {
            VerError("btnContinuarMen_Click: ->" + fechaGarantia + "<-" + ex.Message);
        }
    }


    // Despues de guardar una garantía vuelvo a Lista
    protected void btnRegresarLista_Click(object sender, EventArgs e)
    {
        Navegar("ListaGarantias.aspx");
    }


    // Muestro formulario para Activo Fijo
    protected void btnCrearActivoFijo_Click(object sender, ImageClickEventArgs e)
    {
        mvGarantia.ActiveViewIndex = 1;

        // Vacio formulario primero
        VaciarFormularioActivoFijo(upReclasificacion);

        // Simulo evento para llenar ddl "Rango Vivienda"
        ddlModalVIS_SelectedIndexChanged(this, EventArgs.Empty);

        // Lleno datos iniciales y pongo en default ddl TipoActivo
        txtModalIdentificacion.Text = txtIdentificacion.Text;
        txtModalNombres.Text = txtNombreCliente.Text;
        ddlModalTipoActivo.SelectedIndex = 0;
        ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
    }


    // Guardo Activo Fijo
    private void btnContinuarMenActivoFijo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            string error = string.Empty;

            // Valido Campos Obligatorios y lleno entidad
            ActivoFijo activoFijo = ValidarCamposActivoFijo(out error);

            // Si hay algun error notifico y retorno
            if (!string.IsNullOrWhiteSpace(error))
            {
                VerError(error);
                return;
            }

            // Lleno el resto de la entidad segun tipo Activo seleccionado en el DDL y procedo a guardar
            // Si tengo un tipo activo invalido retorno
            bool tipoActivoSeleccionadoCorrecto = LlenarEntidadActivoFijoGuardar(activoFijo);

            if (!tipoActivoSeleccionadoCorrecto)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtIDActivo.Text))
            {
                _garantiasservicio.CrearActivoFijoPersonal(activoFijo, _usuario);
            }
            else
            {
                activoFijo.idActivo = Convert.ToInt32(txtIDActivo.Text);
                _garantiasservicio.ModificarActivoFijoPersonal(activoFijo, _usuario);
            }

            // Activo View de "Operacion Exitosa" y doy opcion para regresar a garantia
            mvGarantia.ActiveViewIndex = 2;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            btnRegresarGarantia.Visible = true;
        }
        catch (Exception ex)
        {
            VerError("btnContinuarMenActivoFijo_Click: " + ex.Message);
        }
    }

    // Doy oportunidad de regresar a garantia luego de guardar el activo y refresco la tabla de activo
    protected void btnRegresarGarantia_Click(object sender, EventArgs e)
    {
        LlenarGVActivoFijos();

        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        mvGarantia.ActiveViewIndex = 0;
        btnRegresarGarantia.Visible = false;
    }


    #endregion


    #region Metodos Llenado y Consulta Inicial


    // Consulto Datos del credito
    protected void ConsultarDatosCreditoCliente(string idRadicacion)
    {
        Garantia garantia;

        try
        {
            garantia = _garantiasservicio.ConsultarCreditoCliente(Convert.ToInt64(idRadicacion), _usuario);
            LlenarDatosCliente(idRadicacion, garantia);
        }
        catch (Exception ex)
        {
            VerError("LlenarDatosCliente: " + ex.Message);
            return;
        }
    }


    // Lleno Datos del Credito
    private void LlenarDatosCliente(string idRadicacion, Garantia garantia)
    {
        txtMonto.Text = garantia.monto.ToString();
        txtLinea.Text = garantia.cod_linea_cred.ToString();
        ddlTipoIdentificacion.SelectedValue = garantia.cod_ident.ToString();

        if (!string.IsNullOrWhiteSpace(idRadicacion))
            txtNroRadicacion.Text = idRadicacion;

        if (!string.IsNullOrWhiteSpace(garantia.nom_persona))
        {
            txtNombreCliente.Text = garantia.nom_persona;
        }

        if (!string.IsNullOrWhiteSpace(garantia.identificacion))
        {
            txtIdentificacion.Text = garantia.identificacion;
            if (garantia.cod_persona != 0)
                lblCodPersona.Text = garantia.cod_persona.ToString();
        }

        if (!string.IsNullOrWhiteSpace(garantia.nom_linea_cred))
            txtNombreLinea.Text = garantia.nom_linea_cred;

        txtFechaSuscrip.Text = DateTime.Today.ToString("dd/MM/yyyy");
    }


    // Consulto Garantía
    protected void ConsultarGarantia(string idGarantia)
    {
        Garantia garantia;

        try
        {
            garantia = _garantiasservicio.ConsultarGarantia(Convert.ToInt64(idGarantia), _usuario);

            _idActivo = garantia.IdActivo;
            _idGarantia = garantia.IdGarantia;
            ViewState["idGarantia"] = _idGarantia;

            LlenarGarantia(garantia);
        }
        catch (Exception ex)
        {
            VerError("ConsultarGarantia: " + ex.Message);
            return;
        }
    }


    // Lleno Garantía
    private void LlenarGarantia(Garantia garantia)
    {
        if (garantia.FechaGarantia != DateTime.MinValue)
            txtFechaSuscrip.Text = garantia.FechaGarantia.ToShortDateString();

        txtFechaLib.Text = garantia.FechaLiberacion.HasValue ? garantia.FechaLiberacion.Value.ToString(gFormatoFecha) : string.Empty;

        txtFechaVenc.Text = garantia.FechaVencimiento.HasValue ? garantia.FechaVencimiento.Value.ToString(gFormatoFecha) : string.Empty;

        txtFechaUltAvaluo.Text = garantia.FechaAvaluo.HasValue ? garantia.FechaAvaluo.Value.ToString(gFormatoFecha) : string.Empty;

        if (!string.IsNullOrWhiteSpace(garantia.Ubicacion))
            txtUbicacion.Text = garantia.Ubicacion;

        if (!string.IsNullOrWhiteSpace(garantia.Encargado))
            txtEncargado.Text = garantia.Encargado;

        if (!string.IsNullOrWhiteSpace(garantia.Aseguradora))
            txtAseguradora.Text = garantia.Aseguradora;

        if (!string.IsNullOrWhiteSpace(garantia.Estado))
            ddlEstadoGarantia.SelectedValue = garantia.Estado;

        if (garantia.valor_avaluo > 0)
            txtValorAvaluo.Text = garantia.valor_avaluo.ToString();

        if (garantia.valor_garantia > 0)
            txtValorGarantia.Text = garantia.valor_garantia.ToString();
        lblCodOpe.Text = garantia.cod_ope.ToString();
    }


    // Consulto y lleno GV Activo Fijos, si no tengo Activos la inicializo con una row vacia
    private void LlenarGVActivoFijos()
    {
        List<Garantia> lstConsultas = new List<Garantia>(1);

        try
        {
            lstConsultas = _garantiasservicio.Listaractivos(_cod_persona, _usuario);
        }
        catch (Exception ex)
        {
            VerError("LlenarGVActivoFijos: " + ex.Message);
            return;
        }

        if (lstConsultas.Count == 0)
        {
            lstConsultas.Add(new Garantia());
        }

        gvGarantiasHipo.DataSource = lstConsultas;
        ViewState["lstConsultaGarantias"] = lstConsultas;

        gvGarantiasHipo.DataBind();

        if (_idActivo != 0)
        {
            SeleccionarCheckBoxActivoActual(lstConsultas);
        }
    }


    // Consulto y lleno DDL TipoIdentificación
    protected void LlenarDDLTipoIdentificacion()
    {
        TipoIdenService IdenService = new TipoIdenService();
        List<TipoIden> lstTipoIden = new List<TipoIden>(1);

        try
        {
            lstTipoIden = IdenService.ListarTipoIden(new TipoIden(), _usuario);
        }
        catch (Exception ex)
        {
            VerError("LlenarDDLTipoIdentificacion" + ex.Message);
            return;
        }

        if (lstTipoIden.Count == 0)
        {
            lstTipoIden.Add(new TipoIden());
        }

        ddlTipoIdentificacion.DataSource = lstTipoIden;
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        // Lleno ddlIdentificacion de la View de Registro de Activos
        ddlModalIdentificacion.DataSource = lstTipoIden;
        ddlModalIdentificacion.DataTextField = "descripcion";
        ddlModalIdentificacion.DataValueField = "codtipoidentificacion";
        ddlModalIdentificacion.DataBind();
    }


    // Consulto y lleno DDL TipoActivoFijo con Anonymous Type para no perder los demas datos se utiliza el "-" como separador
    protected void LlenarDDLTipoActivo()
    {
        ActivosFijoservices activoService = new ActivosFijoservices();

        var lstActivoDataSource = from lista in activoService.ListarTipoActivoFijo(_usuario)
                                  select new
                                  {
                                      Descripcion = lista.nomclase,
                                      Value = lista.str_clase.ToString() + "-" + lista.cod_act.ToString()
                                  };

        ddlModalTipoActivo.DataSource = lstActivoDataSource;
        ddlModalTipoActivo.DataTextField = "Descripcion";
        ddlModalTipoActivo.DataValueField = "Value";
        ddlModalTipoActivo.DataBind();

        // Necesario tener el "-" para que no explote en el Split en el SelectIndex Event
        ddlModalTipoActivo.Items.Insert(0, new ListItem("Seleccione un Tipo", "0-0"));
    }


    #endregion


    #region Metodos Validaciones


    // Validaciones en la GVActivos, si NO hay seleccionados, lleno variable de error y retorno
    private Tuple<long, int> ValidarGVActivos(out string error)
    {
        error = string.Empty;
        int count = 0;
        long activo = 0;
        int tipoActivo = 0;

        foreach (GridViewRow fila in gvGarantiasHipo.Rows)
        {
            CheckBox chkSeleccion = (CheckBox)fila.FindControl("chkSelect");

            if (chkSeleccion.Checked == true)
            {
                count += 1;

                int index = fila.RowIndex;

                HiddenField theHiddenField = fila.FindControl("hiddenTipoGarantia") as HiddenField;

                // Check that we successfully found hidden field before using it
                if (theHiddenField != null)
                {
                    if (!string.IsNullOrWhiteSpace(theHiddenField.Value))
                    {
                        tipoActivo = Convert.ToInt32(theHiddenField.Value);
                    }
                }

                activo = Convert.ToInt64(gvGarantiasHipo.DataKeys[index].Value);

                // break si consigo la fila checkeada
                break;
            }
        }

        // Valido una seleccion valida de Activo Fijo, retorno y notifico el error en caso de uno
        if (count == 0)
        {
            error += " Debe Escoger un Activo Fijo, ";
            return Tuple.Create(activo, tipoActivo);
        }
        if (activo == 0)
        {
            error += " Activo Invalido, ";
            return Tuple.Create(activo, tipoActivo);
        }

        return Tuple.Create(activo, tipoActivo);
    }


    //Validaciones en Garantia cada error en campos obligatorios es llenado en variable error y devuelto
    private Garantia ValidarCamposGarantia(Tuple<long, int> activoSeleccionado, out string error)
    {
        Garantia garantia = new Garantia();
        error = string.Empty;

        string noRadicacion = txtNroRadicacion.Text;
        string valorGarantia = txtValorGarantia.Text;
        string fechaGarantia = txtFechaSuscrip.Text;

        // Valido campos obligatorios, en caso de error retorno y notifico
        if (string.IsNullOrWhiteSpace(noRadicacion))
        {
            error += " Error en la radicación,";
            return garantia;
        }

        if (string.IsNullOrWhiteSpace(fechaGarantia))
        {
            error += " Error en la fecha de la garantía,";
            return garantia;
        }

        if (string.IsNullOrWhiteSpace(valorGarantia))
        {
            error += " Error en el valor de la garantía,";
            return garantia;
        }

        garantia.NumeroRadicacion = long.Parse(noRadicacion);
        garantia.FechaGarantia = Convert.ToDateTime(fechaGarantia);
        garantia.valor_garantia = decimal.Parse(valorGarantia);
        garantia.IdActivo = activoSeleccionado.Item1;
        garantia.tipo_garantia = activoSeleccionado.Item2;
        garantia.IdGarantia = _idGarantia;
        garantia.cod_persona = Convert.ToInt64(_cod_persona);
        garantia.valor_avaluo = txtValorAvaluo.Text == "" ? 0 : decimal.Parse(txtValorAvaluo.Text);
        garantia.Ubicacion = txtUbicacion.Text;
        garantia.Encargado = txtEncargado.Text;
        garantia.Aseguradora = txtAseguradora.Text;
        garantia.FechaVencimiento = txtFechaVenc.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaVenc.Text);
        garantia.Estado = ddlEstadoGarantia.SelectedValue;
        garantia.FechaAvaluo = txtFechaUltAvaluo.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaUltAvaluo.Text);
        garantia.FechaLiberacion = txtFechaLib.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaLib.Text);

        return garantia;
    }


    // Validaciones en Activo Fijo cada error en campos obligatorios es llenado en variable error y devuelto
    private ActivoFijo ValidarCamposActivoFijo(out string error)
    {
        ActivoFijo activoFijo = new ActivoFijo();
        string fechaCompra = txtModalFechaIni.Text;
        string valor_compra = txtModalValorComercial.Text;
        string valor_comprometido = txtModalValorComprometido.Text;
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(valor_compra))
        {
            error += " Valor Comercial debe tener un valor valido, ";
            return activoFijo;
        }
        if (string.IsNullOrWhiteSpace(fechaCompra))
        {
            error += " Fecha de Adquisición debe ser llenada, ";
            return activoFijo;
        }

        activoFijo.fecha_compra = Convert.ToDateTime(fechaCompra);
        activoFijo.valor_comprometido = string.IsNullOrWhiteSpace(valor_comprometido) ? 0 : Convert.ToDecimal(valor_comprometido);
        activoFijo.valor_compra = string.IsNullOrWhiteSpace(valor_compra) ? 0 : Convert.ToDecimal(valor_compra);
        activoFijo.cod_persona = Convert.ToInt64(_cod_persona);
        activoFijo.descripcion = txtModalDescripcion.Text;

        return activoFijo;
    }


    #endregion


    #region Llenar Entidad para guardar


    // Verifico que seleccion tiene mi DDL TipoActivo y llamo al metodo adecuado segun seleccion para llenar la entidad
    private bool LlenarEntidadActivoFijoGuardar(ActivoFijo activoFijo)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');
        activoFijo.cod_tipo_activo_per = Convert.ToInt64(tipoActivoSeleccionado[1]);

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            LlenarEntidadActivoFijoInmueble(activoFijo);
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            LlenarEntidadActivoFijoVehiculo(activoFijo);
        }
        else
        {
            return false;
        }

        return true;
    }


    // Lleno entidad en modo VEHICULO
    private void LlenarEntidadActivoFijoVehiculo(ActivoFijo activoFijo)
    {
        string fechaImportacion = txtModalFechaImportacion.Text;

        if (!string.IsNullOrWhiteSpace(fechaImportacion))
        {
            activoFijo.fecha_importacion = Convert.ToDateTime(txtModalFechaImportacion.Text);
        }

        activoFijo.marca = txtModalMarca.Text;
        activoFijo.referencia = txtModalReferencia.Text;
        activoFijo.modelo = txtModalModelo.Text;
        activoFijo.cod_uso = Convert.ToInt32(ddlModalUso.SelectedValue);
        activoFijo.capacidad = txtModalCapacidad.Text;
        activoFijo.num_chasis = txtModalNoChasis.Text;
        activoFijo.num_motor = txtModalNoSerieMotor.Text;
        activoFijo.placa = txtModalPlaca.Text;
        activoFijo.color = txtModalColor.Text;
        activoFijo.documentos_importacion = txtModalDocImportacion.Text;
    }


    // Lleno entidad en modo Inmueble
    private void LlenarEntidadActivoFijoInmueble(ActivoFijo activoFijo)
    {
        activoFijo.direccion = txtModalDireccion.Text;
        activoFijo.localizacion = txtModalLocalizacion.Text;
        activoFijo.matricula = txtModalNoMatricula.Text;
        activoFijo.escritura = txtModalEscritura.Text;
        activoFijo.notaria = txtModalNotaria.Text;
        activoFijo.SENALVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);
        activoFijo.tipo_vivienda = ddlModalTipoVivienda.SelectedValue;
        activoFijo.rango_vivienda = ddlModalRangoVivienda.SelectedValue;
        activoFijo.entidad_redescuento = ddlModalEntidadReDesc.SelectedValue;
        activoFijo.margen_redescuento = txtModalmargenReDesc.Text;
        activoFijo.desembolso_directo = txtModalDesembolsoDirecto.Text;
        activoFijo.desembolso = ddlModalDesembolso.SelectedValue;
    }

    #endregion


    #region Metodo SelectIndex de las DDL


    protected void ddlModalTipoActivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            panelTipoActivoInmueble.Visible = true;
            pnlTipoActivoMaquinaria.Visible = false;
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = true;
        }
        else
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = false;
        }
    }


    // Lleno ddl segun seleccion (si tengo VIS o no), al entrar a crear un activo es llamado manualmente para llenar 
    protected void ddlModalVIS_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataConVIS = new[]
        {
             new { Valor = 1, Descripcion = "Tipo 1: Cuyo valor de la vivienda sea menor o igual a 50 SMML"} ,
             new { Valor = 2, Descripcion = "Tipo 2: Cuyo valor de la vivienda sea mayor a 50 SMML y menor o igual a 70 SMML"} ,
             new { Valor = 3, Descripcion = "Tipo 3: Cuyo valor de la vivienda sea mayor a 70 SMML y menor o igual a 100 SMML"} ,
             new { Valor = 4, Descripcion = "Tipo 4: Cuyo valor de la vivienda sea mayor a 100 SMML y menor o igual a 135 SMML"} ,
        };

        var dataSinVIS = new[]
        {
             new { Valor = 5, Descripcion = "Rango 1: Cuyo monto sea mayor a VIS y menor o igual a 643.100 UVR"} ,
             new { Valor = 6, Descripcion = "Rango 2: Cuyo monto sea mayor a 643.100 UVR y menor o igual a 2’411.625 UVR"} ,
             new { Valor = 7, Descripcion = "Rango 3: Cuyo valor sea mayor a 2’411.625 UVR"} ,
        };


        int tieneVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);

        if (tieneVIS == (int)Tiene.Si)
        {
            ddlModalRangoVivienda.DataSource = dataConVIS;
        }
        else
        {
            ddlModalRangoVivienda.DataSource = dataSinVIS;
        }

        ddlModalRangoVivienda.DataTextField = "Descripcion";
        ddlModalRangoVivienda.DataValueField = "Valor";
        ddlModalRangoVivienda.DataBind();
    }


    #endregion


    #region Metodos para Ordenar GV, seleccionar Checkbox y vaciar Formulario Activo Fijo


    // Muevo el activo actual de la garantia al tope de la lista
    List<Garantia> MoverActivoActualAlTope(List<Garantia> lstConsultas)
    {
        var index = lstConsultas.FindIndex(x => x.IdActivo == _idActivo);

        if (index < 0)
        {
            var activoActual = lstConsultas[index];
            lstConsultas.RemoveAt(index);
            lstConsultas.Insert(0, activoActual);
        }

        return lstConsultas;
    }


    // Selecciono el checkbox de la primera row al ser el activo actual de la garantia
    protected void SeleccionarCheckBoxActivoActual(List<Garantia> lstConsultas)
    {
        // BUSCANDO INDICE 
        var index = lstConsultas.FindIndex(x => x.IdActivo == _idActivo);
        if (index >= 0)
        {
            if (gvGarantiasHipo.Rows.Count >= index)
            {
                CheckBox cb = (CheckBox)gvGarantiasHipo.Rows[index].Cells[0].FindControl("chkSelect");
                cb.Checked = true;
            }
        }
    }


    // Limpio formulario despues de guardar
    public void VaciarFormularioActivoFijo(Control pControl)
    {
        foreach (var controlhij in pControl.Controls)
        {
            if (controlhij is TextBox)
            {
                var texbox = controlhij as TextBox;
                texbox.Text = "";
            }
            else
            {
                VaciarFormularioActivoFijo((Control)controlhij);
            }
        }
    }


    #endregion


    #region Eventos Grilla

    protected void gvBienesActivos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        btnCrearActivoFijo_Click(this, new ImageClickEventArgs(0, 0)); // Inicializa la modal

        GarantiaService garantiaService = new GarantiaService();
        int idActivo = Convert.ToInt32(gvGarantiasHipo.DataKeys[e.NewEditIndex].Value);

        ActivoFijo activo = garantiaService.ConsultarActivoFijoPersonal(idActivo, Usuario);

        LlenarFormularioActivoFijo(activo);

        e.NewEditIndex = -1;
        ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
    }

    void LlenarFormularioActivoFijo(ActivoFijo activoFijo)
    {
        txtIDActivo.Text = activoFijo.idActivo.ToString();

        txtModalFechaIni.Text = activoFijo.fecha_compra.HasValue ? activoFijo.fecha_compra.Value.ToShortDateString() : string.Empty;
        txtModalValorComprometido.Text = activoFijo.valor_comprometido.HasValue ? activoFijo.valor_comprometido.ToString() : string.Empty;
        txtModalValorComercial.Text = activoFijo.valor_compra.ToString();
        txtModalDescripcion.Text = activoFijo.descripcion;

        txtModalMarca.Text = activoFijo.marca;
        txtModalReferencia.Text = activoFijo.referencia;
        txtModalModelo.Text = activoFijo.modelo;

        if (activoFijo.cod_uso.HasValue)
        {
            ddlModalUso.SelectedValue = activoFijo.cod_uso.ToString();
        }

        if (activoFijo.cod_tipo_activo_per.HasValue)
        {
            string valorSeleccionar = activoFijo.str_clase + "-" + activoFijo.cod_tipo_activo_per;
            ddlModalTipoActivo.SelectedValue = valorSeleccionar;
        }

        txtModalCapacidad.Text = activoFijo.capacidad;
        txtModalNoChasis.Text = activoFijo.num_chasis;
        txtModalNoSerieMotor.Text = activoFijo.num_motor;
        txtModalPlaca.Text = activoFijo.placa;
        txtModalColor.Text = activoFijo.color;
        txtModalDocImportacion.Text = activoFijo.documentos_importacion;

        txtModalDireccion.Text = activoFijo.direccion;
        txtModalLocalizacion.Text = activoFijo.localizacion;
        txtModalNoMatricula.Text = activoFijo.matricula;
        txtModalEscritura.Text = activoFijo.escritura;
        txtModalNotaria.Text = activoFijo.notaria;

        if (activoFijo.SENALVIS.HasValue)
        {
            ddlModalVIS.SelectedValue = activoFijo.SENALVIS.ToString();
            ddlModalVIS_SelectedIndexChanged(this, EventArgs.Empty);
        }

        if (!string.IsNullOrWhiteSpace(activoFijo.tipo_vivienda))
        {
            if (activoFijo.tipo_vivienda != "0")
                ddlModalTipoVivienda.SelectedValue = activoFijo.tipo_vivienda;
        }

        if (!string.IsNullOrWhiteSpace(activoFijo.rango_vivienda))
        {
            ddlModalRangoVivienda.SelectedValue = activoFijo.rango_vivienda;
        }

        try
        {
            ddlModalEntidadReDesc.SelectedValue = activoFijo.entidad_redescuento;
        }
        catch
        {
        }

        txtModalmargenReDesc.Text = activoFijo.margen_redescuento;
        txtModalDesembolsoDirecto.Text = activoFijo.desembolso_directo;
        try
        {
            ddlModalDesembolso.SelectedValue = activoFijo.desembolso;
        }
        catch
        {
        }
    }

    protected void gvBienesActivos_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Delete") return;

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow rFila = gvGarantiasHipo.Rows[index];
            CheckBox chkSelect = (CheckBox)rFila.FindControl("chkSelect");

            if (!chkSelect.Checked)
            {
                VerError("");
                GarantiaService garantiaService = new GarantiaService();
                List<Garantia> lstReferencia = (List<Garantia>)ViewState["lstConsultaGarantias"];
                Garantia garantia = lstReferencia[index];

                if (garantia.IdActivo == 0) return;

                bool valor = false;
                string error = "";
                valor = garantiaService.EliminarActivoFijo(garantia.IdActivo, Convert.ToInt64(txtNroRadicacion.Text), ref error, Usuario);

                if (valor == true && error == "") 
                {
                    lstReferencia.RemoveAt(index);

                    if (lstReferencia.Count == 0)
                    {
                        lstReferencia.Add(new Garantia());
                    }
                    gvGarantiasHipo.DataSource = lstReferencia;
                    gvGarantiasHipo.DataBind();
                }
                else if (error != "")
                    VerError(error);
            }
            else
            {
                VerError("No se puede eliminar el activo, se encuentra seleccionado como garantía del crédito");
            }
        }
        catch (Exception ex)
        {
            VerError("gvBienesActivos_OnRowCommand, " + ex.Message);
        }
    }


    // Es necesario este evento vacio para que pueda borrar la Row
    protected void gvBienesActivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    #endregion
}