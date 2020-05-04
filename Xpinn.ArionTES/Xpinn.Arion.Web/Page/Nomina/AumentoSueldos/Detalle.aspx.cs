using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.IO;

public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    AumentoSueldoService _aumentoSueldoService = new AumentoSueldoService();
    IngresoPersonalService _ingresoPersonalService = new IngresoPersonalService();
    long? _consecutivoEmpleado;
    long? _consecutivoAumento;
    bool _esNuevoRegistro;
    long? _cargamasiva;
    EmpleadoService _empleadoService = new EmpleadoService();


    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_aumentoSueldoService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aumentoSueldoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_aumentoSueldoService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoAumento = Session[_aumentoSueldoService.CodigoPrograma + ".idAumento"] as long?;


            _cargamasiva = Session[_aumentoSueldoService.CodigoPrograma + ".cargamasiva"] as long?;

            _esNuevoRegistro = !_consecutivoAumento.HasValue;

            
            if (!IsPostBack)
            {
               
                InicializarPagina();
            }
            

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aumentoSueldoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {

        if (_cargamasiva == 1)
        {
            mvDatos.Visible = false;
            mvCargar.Visible = true;
            mvCargar.ActiveViewIndex = 0;
          
        }

        else
        { 
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);

        txtFechaCambio.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        ddlTipoNomina_SelectedIndexChanged(ddlTipoNomina, EventArgs.Empty);

        if (!_esNuevoRegistro)
        {
            LlenarAumento();
        }
        else
        {
            ConsultarDatosPersona();
        }
     }
    }

    void LlenarAumento()
    {
        AumentoSueldo aumentoSueldo = _aumentoSueldoService.ConsultarAumentoSueldo(_consecutivoAumento.Value, Usuario);

        txtCodigoAumento.Text = aumentoSueldo.consecutivo.ToString();
        txtIdentificacionn.Text = aumentoSueldo.identificacion;

        if (!string.IsNullOrWhiteSpace(aumentoSueldo.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = aumentoSueldo.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = aumentoSueldo.codigoempleado.ToString();
        hiddenCodigoPersona.Value = aumentoSueldo.codigopersona.ToString();
        txtNombreCliente.Text = aumentoSueldo.nombre_empleado;

        txtSueldoAnterior.Text = aumentoSueldo.sueldoanterior.HasValue ? _stringHelper.FormatearNumerosComoCurrency(aumentoSueldo.sueldoanterior.ToString()) : "0";
        txtNuevoSueldo.Text = aumentoSueldo.nuevosueldo.HasValue ? _stringHelper.FormatearNumerosComoCurrency(aumentoSueldo.nuevosueldo.ToString()) : "0";
        txtValorAumentar.Text = aumentoSueldo.valorparaaumentar.HasValue ? _stringHelper.FormatearNumerosComoCurrency(aumentoSueldo.valorparaaumentar.ToString()) : "0";
        txtPorcentajeAumentar.Text = aumentoSueldo.porcentajeaumentar.HasValue ? aumentoSueldo.porcentajeaumentar.ToString() : "0";
        txtFechaCambio.Text = aumentoSueldo.fecha.HasValue ? aumentoSueldo.fecha.Value.ToShortDateString() : " ";

        if (aumentoSueldo.codigonomina > 0)
        {
            ddlTipoNomina.SelectedValue = aumentoSueldo.codigonomina.ToString();
        }

        if (aumentoSueldo.codigotipocontrato > 0)
        {
            ddlContrato.SelectedValue = aumentoSueldo.codigotipocontrato.ToString();
        }
    }

    void ConsultarDatosPersona()
    {
        EmpleadoService empleadoService = new EmpleadoService();

        Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleado(_consecutivoEmpleado.Value, Usuario);

        if (empleado == null)
        {
            VerError("No se pudo encontrar a la persona!.");
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
        }
        else
        {
            txtIdentificacionn.Text = empleado.identificacion.ToString();
            ddlTipoIdentificacion.SelectedValue = empleado.cod_identificacion;
            txtNombreCliente.Text = empleado.nombre;
            txtCodigoEmpleado.Text = _consecutivoEmpleado.Value.ToString();
            hiddenCodigoPersona.Value = empleado.cod_persona.ToString();
        }
    }


    #endregion


    #region Eventos Botonera


    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        // Borramos las sesiones para no mezclar cosas luego
        Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idAumento");
        Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idEmpleado");

        if (!_esNuevoRegistro)
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Navegar(Pagina.Nuevo);
        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (_cargamasiva == null)
            {
                if (!ValidarDatos()) return;
                AumentoSueldo aumentoSueldo = ObtenerValores();


                if (_esNuevoRegistro)
                {
                    aumentoSueldo = _aumentoSueldoService.CrearAumentoSueldo(aumentoSueldo, Usuario);
                }
                else
                {
                    _aumentoSueldoService.ModificarAumentoSueldo(aumentoSueldo, Usuario);
                }

                if (aumentoSueldo.consecutivo > 0)
                {
                    mvDatos.SetActiveView(vFinal);

                    // Borramos las sesiones para no mezclar cosas luego
                    Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idAumento");
                    Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idEmpleado");

                    Site toolBar = (Site)Master;
                    toolBar.MostrarGuardar(false);
                }
            }

            if (_cargamasiva == 1 && gvaumentos.Rows.Count > 0 )
            {
                AumentoSueldo aumentomasivo = new AumentoSueldo();

                aumentomasivo.lista = ObtenerLista();

                aumentomasivo = _aumentoSueldoService.CrearAumentoSueldo(aumentomasivo, Usuario);
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(false);
                toolBar.MostrarExportar(false);
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".cargamasiva");
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idAumento");
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idEmpleado");

                Navegar(Pagina.Lista);
            }

        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }


    #endregion


    #region Eventos Varios


    protected void
        txtPorcentajeAumentar_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtPorcentajeAumentar.Text))
        {
            decimal porcentajeAumentar = Convert.ToDecimal(txtPorcentajeAumentar.Text);
            decimal sueldoAnterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtSueldoAnterior.Text));
            decimal valorAumentar = (porcentajeAumentar / 100) * sueldoAnterior;

            txtValorAumentar.Text = _stringHelper.FormatearNumerosComoCurrency(valorAumentar.ToString());
            txtNuevoSueldo.Text = _stringHelper.FormatearNumerosComoCurrency((sueldoAnterior + valorAumentar).ToString());
        }
    }

    protected void txtValorAumentar_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtValorAumentar.Text))
        {
            decimal valorAumentar = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtValorAumentar.Text));
            decimal sueldoAnterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtSueldoAnterior.Text));
            decimal calculo = (valorAumentar * 100) / sueldoAnterior;

            txtPorcentajeAumentar.Text = Math.Round(calculo, 2).ToString();
            txtNuevoSueldo.Text = _stringHelper.FormatearNumerosComoCurrency((sueldoAnterior + valorAumentar).ToString());
            txtValorAumentar.Text = _stringHelper.FormatearNumerosComoCurrency(valorAumentar);
        }
    }

    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            long codigoNomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);
            IngresoPersonal contrato = _ingresoPersonalService.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(_consecutivoEmpleado.Value, codigoNomina, Usuario);

            txtSueldoAnterior.Text = _stringHelper.FormatearNumerosComoCurrency(contrato.salario.Value);

            TipoContratoService tipoContratoService = new TipoContratoService();
            List<TipoContrato> listaTipoContratos = tipoContratoService.ListarTipoContratos(Usuario);

            ddlContrato.DataSource = listaTipoContratos.Where(x => contrato.codigotipocontrato == x.cod_tipo_contrato).ToList();
            ddlContrato.DataTextField = "descripcion";
            ddlContrato.DataValueField = "cod_tipo_contrato";
            ddlContrato.DataBind();
        }
    }


    #endregion


    #region Metodos Ayuda


    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) || string.IsNullOrWhiteSpace(ddlContrato.SelectedValue) || string.IsNullOrWhiteSpace(txtSueldoAnterior.Text)
            || string.IsNullOrWhiteSpace(txtNuevoSueldo.Text) || string.IsNullOrWhiteSpace(txtValorAumentar.Text) || string.IsNullOrWhiteSpace(txtPorcentajeAumentar.Text) || string.IsNullOrWhiteSpace(txtFechaCambio.Text))
        {
            VerError("Faltan datos por validar");
            return false;
        }

        return true;
    }

    AumentoSueldo ObtenerValores()
    {
        AumentoSueldo aumentoSueldo = new AumentoSueldo
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            codigopersona = Convert.ToInt64(hiddenCodigoPersona.Value),
            fecha = !string.IsNullOrWhiteSpace(txtFechaCambio.Text) ? Convert.ToDateTime(txtFechaCambio.Text) : default(DateTime?),
            sueldoanterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtSueldoAnterior.Text)),
            nuevosueldo = !string.IsNullOrWhiteSpace(txtNuevoSueldo.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtNuevoSueldo.Text)) : default(decimal),
            valorparaaumentar = !string.IsNullOrWhiteSpace(txtValorAumentar.Text) ? Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(txtValorAumentar.Text)) : default(decimal),
            porcentajeaumentar = !string.IsNullOrWhiteSpace(txtPorcentajeAumentar.Text) ? Convert.ToDecimal(txtPorcentajeAumentar.Text) : default(decimal),
            consecutivo = _consecutivoAumento.HasValue ? _consecutivoAumento.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigotipocontrato = !string.IsNullOrWhiteSpace(ddlContrato.SelectedValue) ? Convert.ToInt64(ddlContrato.SelectedValue) : default(long),
        };

        return aumentoSueldo;
    }

    protected List<AumentoSueldo> ObtenerLista()
    {
        VerError(gSeparadorMiles);
        List<AumentoSueldo> lstTemp = new List<AumentoSueldo>();
        List<AumentoSueldo> lista = new List<AumentoSueldo>();
        List<AumentoSueldo> lstConsulta= (List<AumentoSueldo>)Session["Aumentos"]; ;
        gvaumentos.AllowPaging = false;
        gvaumentos.DataBind();
        if (gvaumentos.Rows.Count == 0)
        {
            if (Session["Aumentos"] != null)
            {
                gvaumentos.DataSource = lstConsulta;
                gvaumentos.AllowPaging = false;
                gvaumentos.DataBind();
            }
        }          

        return lstConsulta;
       
    }

    #endregion



    protected void btnAceptarCarga_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(false);
        toolBar.MostrarExportar(false);
        toolBar.MostrarRegresar(false);
        VerError("");
        Session["Aumentos"] = null;
        try
        {
            lblmsjCarga.Text = "";
            if (flpArchivo.HasFile)
            {
                String fileName = Path.GetFileName(this.flpArchivo.PostedFile.FileName);
                String extension = Path.GetExtension(this.flpArchivo.PostedFile.FileName).ToLower();
                if (extension != ".txt")
                {
                    lblmsjCarga.Text = "Para realizar la carga de Archivo solo debe seleccionar un archivo de texto";
                    return;
                }

                List<AumentoSueldo> lstDatosCarga = new List<AumentoSueldo>();

                //CARGANDO DATOS AL LISTADO POR SI EXISTEN EN LA GRIDVIEW
                // lstDatosCarga = ObtenerLista();

                string readLine;
                StreamReader strReader;
                Stream stream = flpArchivo.FileContent;

                string ErrorCargue = "";

                //ARCHIVO DE TEXTO
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        //PASANDO LA FILA DEL ARCHIVO A LA VARIABLE
                        readLine = strReader.ReadLine();
                        if (readLine != "")
                        {
                            //SEPARANDO CADA CAMPO
                            if (readLine.Contains("|") == false)
                            {
                                lblmsjCarga.Text = "El Archivo cargado no contiene los separadores correctos, verifique los datos ( Separador correcto : | )";
                                return;
                            }
                            string[] arrayline = readLine.Split('|');
                            int contadorreg = 0;



                            AumentoSueldo aumento = new AumentoSueldo();
                            //INICIAR LA LECTURA DE DATOS DE LA PRIMERA LINEA
                            int posicionInicial = 1;
                            foreach (string variable in arrayline)
                            {
                                if (posicionInicial >= 0)
                                {
                                    if (variable != null)
                                    {
                                        if (contadorreg == 0) //IDENTIFICACIÓN EMPLEADO
                                        {
                                            aumento.identificacion = variable.ToUpper().Trim();
                                        }

                                        Empleados empleado = _empleadoService.ConsultarInformacionPersonaEmpleadoPorIdentificacion(aumento.identificacion, Usuario);


                                        aumento.codigopersona = Convert.ToInt32(empleado.cod_persona);
                                        aumento.codigoempleado = Convert.ToInt32(empleado.consecutivo);
                                        aumento.nombre_empleado = Convert.ToString(empleado.nombre);
                                        aumento.codigonomina = Convert.ToInt64(empleado.codigonomina);
                                        aumento.codigotipocontrato = Convert.ToInt64(empleado.codigotipocontrato);

                                        Decimal sueldoanterior = 0;

                                        AumentoSueldo aumentoSueldo = _aumentoSueldoService.ConsultarAumentoSueldoXEmpleado(aumento.codigopersona, Usuario);

                                        if (aumentoSueldo.sueldoanterior > 0)
                                        {
                                            sueldoanterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(sueldoanterior.ToString()));
                                        }
                                       if ( aumentoSueldo.sueldo > 0)
                                        {
                                            sueldoanterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(aumentoSueldo.sueldo.ToString()));
                                        }


                                        if (contadorreg == 1) //VALOR aumentar
                                        {
                                            String valoraumentar;
                                            decimal calculo = 0;
                                            aumento.valorparaaumentar = Convert.ToDecimal(variable.Trim());

                                            if (aumento.valorparaaumentar > 0)
                                            {
                                                decimal valorAumentar = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(aumento.valorparaaumentar.ToString()));
                                                decimal sueldoAnterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(sueldoanterior.ToString()));
                                                if (valorAumentar > 0 && sueldoAnterior > 0)
                                                    calculo = (valorAumentar * 100) / sueldoAnterior;
                                                aumento.porcentajeaumentar = Convert.ToDecimal(Math.Round(calculo,2).ToString());
                                                aumento.nuevosueldo = sueldoAnterior + valorAumentar;
                                                aumento.sueldoanterior = sueldoAnterior;
                                            }



                                        }

                                        /*if (contadorreg == 2) //porcentaje aumentar
                                        {
                                            String porcentaje;
                                            // decimal valorAumentar = 0;
                                            aumento.porcentajeaumentar = Convert.ToInt32(variable.Trim());
                                            porcentaje = Convert.ToString(aumento.porcentajeaumentar);
                                            if (!string.IsNullOrWhiteSpace(porcentaje) && aumento.porcentajeaumentar > 0)
                                            {
                                                decimal valorAumentar = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(aumento.valorparaaumentar.ToString()));
                                                decimal porcentajeAumentar = Convert.ToDecimal(aumento.porcentajeaumentar);
                                                decimal sueldoAnterior = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(sueldoanterior.ToString()));
                                                if (valorAumentar > 0 && sueldoAnterior > 0)
                                                    valorAumentar = (porcentajeAumentar / 100) * sueldoAnterior;

                                                aumento.valorparaaumentar = Convert.ToDecimal(_stringHelper.DesformatearNumerosDecimales(valorAumentar.ToString()));

                                                aumento.nuevosueldo = sueldoAnterior + valorAumentar;
                                                aumento.sueldoanterior = sueldoAnterior;
                                            }
                                        }
                                        */


                                        if (contadorreg == 2) //fecha
                                        {

                                            string sformato_fecha = "dd/MM/yyyy";
                                            aumento.fecha = DateTime.ParseExact(variable.ToString().Trim(), sformato_fecha, null);

                                        }
                                    }


                                }
                                contadorreg++;



                            }
                            lstDatosCarga.Add(aumento);
                        }
                    }
                }
                    if (ErrorCargue != "")
                        VerError("Han surgido problemas al cargar los siguientes detalles: <br>" + ErrorCargue);

                    Session["Aumentos"] = lstDatosCarga;
                    gvaumentos.DataSource = lstDatosCarga;
                    gvaumentos.DataBind();
                    //  lblTotalRegs.Visible = true;
                    // lblTotalRegs.Text = "<br/> Registros encontrados " + lstDatosCarga.Count.ToString();

                    mvCargar.Visible = true;
                    mvCargar.ActiveViewIndex = 0;

                    //  btnDetalle.Visible = true;
                    Site toolbar = (Site)Master;
                    toolbar.MostrarGuardar(true);
                    toolbar.MostrarImprimir(false);
                    toolbar.MostrarExportar(false);
                    toolbar.MostrarRegresar(true);

            }

            else
            {
                lblmsjCarga.Text = "Seleccione un Archivo para realizar la carga de datos.";
            }
            }
        catch (Exception ex)
        {
            VerError("ERROR: " + ex.Message);
        }
    }


  
}