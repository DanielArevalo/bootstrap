using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    HorasExtrasEmpleadosService _horasExtrasService = new HorasExtrasEmpleadosService();
    List<ErroresCarga> _lstErroresCarga;
    List<HorasExtrasEmpleados> _lstHorasExtra;
    int _contadorRegistro;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_horasExtrasService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoImportar += btnImportar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_horasExtrasService.CodigoPrograma + ".idHoraExtra");
                Session.Remove(_horasExtrasService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += ctlMensaje_eventoClick;

            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_horasExtrasService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_horasExtrasService.CodigoPrograma + ".idHoraExtra");
                Session.Remove(_horasExtrasService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
            else
            {
                if (ViewState["_lstHorasExtra"] != null)
                {
                    _lstHorasExtra = (List<HorasExtrasEmpleados>)ViewState["_lstHorasExtra"];
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_horasExtrasService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoEmpleado.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        ddlNomina.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvHoraExtras.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvHoraExtras, "Horas Extras");

        gvHoraExtras.AllowPaging = true;
        Actualizar();
    }

    void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.SetActiveView(viewImportar);

        Site toolBar = (Site)Master;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarImportar(false);
        toolBar.MostrarGuardar(false);

        toolBar.MostrarLimpiar(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarConsultar(false);
        toolBar.MostrarNuevo(false);

        gvDatos.DataSource = null;
        gvDatos.DataBind();
        gvErrores.DataSource = null;
        gvErrores.DataBind();
        pnlNotificacion.Visible = false;

        cpeDemo1.CollapsedText = "(Click Aquí para Mostrar Detalles...)";
    }

    void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.SetActiveView(viewPrincipal);

        Site toolBar = (Site)Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarImportar(true);
        toolBar.MostrarGuardar(false);

        toolBar.MostrarLimpiar(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarNuevo(true);
    }


    #endregion


    #region Eventos Grillas


    protected void gvHoraExtras_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHoraExtras.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvHoraExtras_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvHoraExtras.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvHoraExtras.SelectedRow.Cells[3].Text);

        Session[_horasExtrasService.CodigoPrograma + ".idHoraExtra"] = id;
        Session[_horasExtrasService.CodigoPrograma + ".idEmpleado"] = idEmpleado;

        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvHoraExtras.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensaje.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void ctlMensaje_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                _horasExtrasService.EliminarHorasExtrasEmpleados(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvHoraExtras_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        if (_lstErroresCarga != null)
        {
            _lstErroresCarga.Clear();
        }
        else
        {
            _lstErroresCarga = new List<ErroresCarga>();
        }
        
        _contadorRegistro = 1;

        if (_lstHorasExtra != null && _lstHorasExtra.Count > 0)
        {
            try
            {
                foreach (var horaExtra in _lstHorasExtra)
                {
                    try
                    {
                        _horasExtrasService.CrearHorasExtrasEmpleados(horaExtra, Usuario);
                    }
                    catch (Exception ex)
                    {
                        RegistrarError(_contadorRegistro, string.Empty, ex.Message, horaExtra.codigoempleado.ToString());
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
                _lstHorasExtra.Clear();
            }
            catch (Exception ex)
            {
                VerError("Error al guardar los créditos para importar, " + ex.Message);
            }
        }
    }

    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<HorasExtrasEmpleados> lstConsulta = _horasExtrasService.ListarHorasExtrasEmpleados(filtro, Usuario);

            gvHoraExtras.DataSource = lstConsulta;
            gvHoraExtras.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_horasExtrasService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text))
        {
            filtro += " and hor.CODIGOEMPLEADO = " + txtCodigoEmpleado.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue))
        {
            filtro += " and hor.CodigoNomina = " + ddlNomina.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion



    protected void btnCargarHoras_Click(object sender, EventArgs e)
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
                _lstHorasExtra = new List<HorasExtrasEmpleados>();
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

                gvDatos.DataSource = _lstHorasExtra;
                gvDatos.DataBind();

                ViewState.Add("_lstHorasExtra", _lstHorasExtra);

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
        EmpleadoService empleadoService = new EmpleadoService();
        HorasExtrasEmpleados horasExtrasEmpleado = new HorasExtrasEmpleados();
        bool sinErrores = true;
        string sformato_fecha = ddlFormatoFecha.SelectedValue;
        
        for (int i = 0; i < lineaAProcesar.Count(); i++)
        {
            if (i == 0)
            {
                try
                {
                    horasExtrasEmpleado.fecha = DateTime.ParseExact(lineaAProcesar[i].Trim(), sformato_fecha, null);
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            else if (i == 1)
            {
                try
                {
                    Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleadoPorIdentificacion(lineaAProcesar[i].Trim(), Usuario);
                    horasExtrasEmpleado.codigopersona = empleado.cod_persona;
                    horasExtrasEmpleado.codigoempleado = empleado.consecutivo;
                    horasExtrasEmpleado.identificacion_empleado = lineaAProcesar[i].Trim();

                    if (horasExtrasEmpleado.codigopersona <= 0 || horasExtrasEmpleado.codigoempleado <= 0)
                    {
                        sinErrores = false;
                        throw new InvalidOperationException("El registro no existe!.");
                    }
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            else if (i == 2)
            {
                try
                {
                    horasExtrasEmpleado.cantidadhoras = Convert.ToDecimal(lineaAProcesar[i].Trim());
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            else if (i == 3)
            {
                try
                {
                    horasExtrasEmpleado.codigoconceptohoras = Convert.ToInt64(lineaAProcesar[i].Trim());
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
            else if (i == 4)
            {
                try
                {
                    horasExtrasEmpleado.codigonomina = Convert.ToInt64(lineaAProcesar[i].Trim());
                }
                catch (Exception ex)
                {
                    sinErrores = false;
                    RegistrarError(_contadorRegistro, i.ToString(), ex.Message, string.Join(" | ", lineaAProcesar));
                    break;
                }
            }
        }

        if (sinErrores)
        {
            _lstHorasExtra.Add(horasExtrasEmpleado);
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
            if (_lstHorasExtra != null && _lstHorasExtra.Count > 0)
            {
                _lstHorasExtra.RemoveAt((gvDatos.PageIndex * gvDatos.PageSize) + e.RowIndex);

                gvDatos.DataSource = _lstHorasExtra;
                gvDatos.DataBind();

                ViewState["_lstHorasExtra"] = _lstHorasExtra;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al eliminar una fila de la tabla, " + ex.Message);
        }
    }
}