using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Util;


#region Enums


// Columnas del formulario de Historia del Asociado con "IdFormulario = 1" (Bases de datos Asociados Fonsodi)
public enum ColumnasFormularioHistoriaAsociado
{
    SinColumna,
    InformacionGeneral,
    NumeroIdentificacion,
    NombreApellido,
    Contacto,
    Direccion,
    Departamento,
    Telefono,
    Celular,
    Email,
    Estado,
    TipoIdentificacion,
    Genero,
    FechaNacimiento,
    Empresa,
    FechaAfiliacion,
    FechaRetiro,
    Aprobado,
    Estrato
}

// Columnas del formulario de Solicitud de Credito con "IdFormulario = 10003" (Bases de datos Solicitud Creditos)
public enum ColumnasFormularioSolicitudCredito
{
    SinColumna,
    HistoriaAsociado,
    TipoDePersona,
    RazonSocial,
    TipoSolicitudCredito,
    TipoDeRol,
    Ciudad,
    ResultadoAprobacionCredito,
    ValorDeCredito,
    NumeroRadicacion,
}

// Pasos del proceso de Credito
public enum StepsWorkManagementWorkFlowCredito
{
    SinStep,
    Solicitud = 1,
    CargaDocumentosDeLaSolicitud = 2,
    AprobacionCredito = 3,
    AprobacionCredito2 = 4,
    CargaTablaAmortizacion = 5,
    CargaComprobanteDesembolso = 6,
    CargaComprobanteGiro = 7,
    CargaCopiaTransferencia = 8,
    CargaDocumentosCredito = 9
}

// Pasos del proceso Retiro de Asociados
public enum StepsWorkManagementWorkFlowRetiroAsociados
{
    SinStep,
    Radicacion = 1,
    ValidacionComercial = 2,
    CruceDeCuentas = 3,
    CargaDocumentosCruceDeCuentas = 4,
    CargaComprobanteDeDesembolso = 5,
    CargaComprobanteGiro = 6,
    CargaComprobanteDeTransferencia = 7
}

public enum StepsWorkManagementWorkFlowPagoProveedores
{
    SinStep,
    Radicacion = 1,
    Administrativa = 2,
    AprobacionJefeDeArea = 3,
    AprobacionGerencia = 4,
    Causacion = 5,
    Pago = 6,
}

// Todas las operaciones posibles en el WorkManagement (Usado para Auditoria)
public enum OperacionesWorkManagement
{
    SinOperacion,
    InsertarAfiliacion = 1,
    ActualizarAfiliacion = 2,
    AprobacionCredito = 3,
    DesembolsoCredito = 4,
    SolicitudCredito = 5,
    CreacionFormularioSolicitudCredito = 6,
    CruceDeCuentas = 7,
    AnalisisDeCredito = 8,
    AnexoDocumentoTablaAmortizacion = 9,
    AnexoDocumentoCruceCuentas = 10,
    AnexoDocumentoSolicitudCredito = 11,
    AnexoDocumentoComprobanteDesembolsoCredito = 12,
    AnexoDocumentoComprobanteGiroCredito = 13,
    AnexoDocumentoComprobanteDesembolsoCruceCuentas = 14,
    AnexoDocumentoComprobanteGiroCruceCuentas = 15,
    AnexoDocumentoDocumentosAnexoAfiliacion = 16,
    CargaDocumentosCruceDeCuentas = 17,
    CargaComprobanteDeDesembolso = 18,
    CargaComprobanteGiro = 19,
    CausacionPagoProveedor = 20,
    PagoFacturaPagoProveedor = 21,
    AnexoDocumentoComprobanteCausacionPagoProveedores = 22,
    AnexoDocumentoComprobanteEgresoPagoProveedores = 23
}

// Codigo de los Formularios en el WorkManagement
public enum FormulariosWorkManagement
{
    SinFormulario,
    HistoriaAsociado,
    SolicitudCredito,
    CorrespondenciaRecibida,
    FacturasRecibidas
}

// Operadores que usa el WorkManagement
public enum OperadoresWorkManagement
{
    IgualA,
    NoIgualA,
    IzquierdaMayorIgualQueDerecha,
    IzquierdaMenorIgualQueDerecha,
    Like
}

// Codigo de Tipo de Archivos para identificarlos en la carga de documentos al WorkFlow
// Su valor es un parametro general que se busca en el metodo "HomologarTipoArchivo"
public enum TipoArchivoWorkManagement
{
    SinTipoArchivo,
    SolicitudCredito = 52,
    TablaAmortizacion = 53,
    CruceCuentas = 54,
    ComprobanteDesembolsoCredito = 55,
    ComprobanteGiroCredito = 56,
    ComprobanteDesembolsoCruceCuentas = 57,
    ComprobanteGiroCruceCuentas = 58,
    DocumentosAnexoAfiliacion = 61,
    ComprobanteCausacionPagoProveedores = 64,
    ComprobanteEgresoPagoProveedores = 65
}


#endregion


/// <summary>
/// Interfaz para comunicars con WorkManagement
/// </summary>
public class InterfazWorkManagement
{


    #region Variables ReadOnly para Configuracion


    // ESTE USUARIO DEBE EXISTIR EN EL WM, EL WM NO AGARRA CODIGO DE USUARIO, TRABAJA CON NOMBRE DEL USUARIO TENIENDO EN CUENTA MAYUSCULAS Y MINUSCULAS
    // SI EL USUARIO NO EXISTE EXPLOTARA Y NO DIRA PORQUE EXPLOTO, NO SALDRA MENSAJE DE QUE EL USUARIO NO EXISTE
    public const string _usuarioDefault = "Administrador";

    // Codigo Directorio de Archivos para Cartera (General = 46)
    readonly string _codigoDirectorioArchivosCartera;

    // Codigo Directorio de Archivos para Afiliacion (General = 47)
    readonly string _codigoDirectorioArchivosAfiliacion;

    // Codigo Directorio de Archivos para Retiro Asociado (General = 59)
    readonly string _codigoDirectorioArchivosRetiro;

    // Codigo Directorio de Archivos para Pago Proveedores (General = 63)
    readonly string _codigoDirectorioArchivosPagoProveedores;

    // Codigo de identificacion de proceso de "Credito" en WM (General = 48)
    readonly int _codigoProcesoCredito;

    // Codigo de identificacion de proceso de "Afiliacion" en WM (General = 49)
    readonly int _codigoProcesoAfiliacion;

    // Codigo de identificacion de proceso de "Retiro de Afiliado" en WM (General = 51)
    readonly int _codigoProcesoRetiroAsociado;

    // Codigo de identificacion de proceso de "Pago a Proveedores" en WM (General = 62)
    readonly int _codigoProcesoPagoAProveedores;

    // Codigo de SucursalFonsodi registrado en WM
    readonly string _codigoSucursal = "01";

    // Header te autenticacion para poder usar WM
    readonly HeaderDto _headerAuthenticacion;

    // Service de Financial para insertar auditorias
    readonly WorkManagementServices _workManagementService;

    // Usuario usado para registrar la auditoria
    readonly Usuario _usuario;

    // Diccionario para codigo de formularios de WorkManagement
    readonly Dictionary<FormulariosWorkManagement, string> _diccionarioCodigoFormularios;

    // Diccionado para las columnas del formulario con "IdFormulario = 1" (Bases de datos Asociados Fonsodi)
    readonly Dictionary<ColumnasFormularioHistoriaAsociado, string> _diccionarioColumnasFormulario_1;

    // Diccionado para las columnas del formulario con "IdFormulario = 10003" (Bases de datos Solicitud Creditos)
    readonly Dictionary<ColumnasFormularioSolicitudCredito, string> _diccionarioColumnasFormulario_10003;

    // Operadores disponibles en WorkManagement
    readonly Dictionary<OperadoresWorkManagement, string> _operadores;


    #endregion


    #region Constructor


    public InterfazWorkManagement(Usuario usuario)
    {
        _usuario = usuario;
        _workManagementService = new WorkManagementServices();

        // Token de autenticacion
        _headerAuthenticacion = new HeaderDto
        {
            User = ConfigurationManager.AppSettings["usuarioWorkManagement"],
            Token = ConfigurationManager.AppSettings["tokenWorkManagement"]
        };

        // Si el token esta en estado invalido, pa fuera
        if (string.IsNullOrWhiteSpace(_headerAuthenticacion.User) || string.IsNullOrWhiteSpace(_headerAuthenticacion.Token))
        {
            throw new InvalidOperationException("No existe usuario o token para usar el API de WorkManagement");
        }

        // Parametros Generales
        GeneralService generalService = new GeneralService();

        // Codigos directorios padres para guardar archivos
        General codigoDirectorioArchivosCartera = generalService.ConsultarGeneral(46, usuario);
        General codigoDirectorioArchivosAfiliacion = generalService.ConsultarGeneral(47, usuario);
        General codigoDirectorioArchivosRetiro = generalService.ConsultarGeneral(59, usuario);
        General codigoDirectorioArchivosPagoProveedores = generalService.ConsultarGeneral(63, usuario);

        // Codigo procesos 
        General codigoProcesoCredito = generalService.ConsultarGeneral(48, usuario);
        General codigoProcesoAfiliacion = generalService.ConsultarGeneral(49, usuario);
        General codigoProcesoRetiro = generalService.ConsultarGeneral(51, usuario);
        General codigoProcesoPagoAProveedores = generalService.ConsultarGeneral(62, usuario);

        // LOS NECESITO SI O SI NO QUITARRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR VALIDACION
        if (codigoDirectorioArchivosCartera == null || codigoDirectorioArchivosAfiliacion == null || codigoDirectorioArchivosRetiro == null || codigoDirectorioArchivosPagoProveedores == null
            || codigoProcesoCredito == null || codigoProcesoAfiliacion == null || codigoProcesoRetiro == null || codigoProcesoPagoAProveedores == null
            || string.IsNullOrWhiteSpace(codigoDirectorioArchivosCartera.valor) || string.IsNullOrWhiteSpace(codigoDirectorioArchivosAfiliacion.valor) || string.IsNullOrWhiteSpace(codigoDirectorioArchivosRetiro.valor) || string.IsNullOrWhiteSpace(codigoDirectorioArchivosPagoProveedores.valor)
            || string.IsNullOrWhiteSpace(codigoProcesoCredito.valor) || string.IsNullOrWhiteSpace(codigoProcesoAfiliacion.valor) || string.IsNullOrWhiteSpace(codigoProcesoRetiro.valor) || string.IsNullOrWhiteSpace(codigoProcesoPagoAProveedores.valor))
        {
            throw new InvalidOperationException("Parametros Generales Faltantes O Invalidos para continuar con la operacion (46,47,48,49,51,59,62)");
        }

        // Directorios
        _codigoDirectorioArchivosCartera = codigoDirectorioArchivosCartera.valor;
        _codigoDirectorioArchivosAfiliacion = codigoDirectorioArchivosAfiliacion.valor;
        _codigoDirectorioArchivosRetiro = codigoDirectorioArchivosRetiro.valor;
        _codigoDirectorioArchivosPagoProveedores = codigoDirectorioArchivosPagoProveedores.valor;

        // Procesos
        _codigoProcesoCredito = Convert.ToInt32(codigoProcesoCredito.valor);
        _codigoProcesoAfiliacion = Convert.ToInt32(codigoProcesoAfiliacion.valor);
        _codigoProcesoRetiroAsociado = Convert.ToInt32(codigoProcesoRetiro.valor);
        _codigoProcesoPagoAProveedores = Convert.ToInt32(codigoProcesoPagoAProveedores.valor);

        // Codigo del formulario
        _diccionarioCodigoFormularios = new Dictionary<FormulariosWorkManagement, string>
        {
            { FormulariosWorkManagement.HistoriaAsociado, "HA" },
            { FormulariosWorkManagement.SolicitudCredito, "SC" },
            { FormulariosWorkManagement.CorrespondenciaRecibida, "CR" },
            { FormulariosWorkManagement.FacturasRecibidas, "FR" }
        };

        // Estructura formulario con id 1
        _diccionarioColumnasFormulario_1 = new Dictionary<ColumnasFormularioHistoriaAsociado, string>
        {
            { ColumnasFormularioHistoriaAsociado.InformacionGeneral, "C0001" },
            { ColumnasFormularioHistoriaAsociado.NumeroIdentificacion, "C0002" },
            { ColumnasFormularioHistoriaAsociado.NombreApellido, "C0003" },
            { ColumnasFormularioHistoriaAsociado.Contacto, "C0004" },
            { ColumnasFormularioHistoriaAsociado.Direccion, "C0005" },
            { ColumnasFormularioHistoriaAsociado.Departamento, "C0006" },
            { ColumnasFormularioHistoriaAsociado.Telefono, "C0007" },
            { ColumnasFormularioHistoriaAsociado.Celular, "C0008" },
            { ColumnasFormularioHistoriaAsociado.Email, "C0009" },
            { ColumnasFormularioHistoriaAsociado.Estado, "C0010" },
            { ColumnasFormularioHistoriaAsociado.TipoIdentificacion, "C0011" },
            { ColumnasFormularioHistoriaAsociado.Genero, "C0012" },
            { ColumnasFormularioHistoriaAsociado.FechaNacimiento, "C0013" },
            { ColumnasFormularioHistoriaAsociado.Empresa, "C0014" },
            { ColumnasFormularioHistoriaAsociado.FechaAfiliacion, "C0015" },
            { ColumnasFormularioHistoriaAsociado.FechaRetiro, "C0016" },
            { ColumnasFormularioHistoriaAsociado.Aprobado, "C0017" },
            { ColumnasFormularioHistoriaAsociado.Estrato, "C0018" }
        };

        // Estructura formulario con id 10003
        _diccionarioColumnasFormulario_10003 = new Dictionary<ColumnasFormularioSolicitudCredito, string>
        {
            { ColumnasFormularioSolicitudCredito.HistoriaAsociado, "C0001" },
            { ColumnasFormularioSolicitudCredito.TipoDePersona, "C0002" },
            { ColumnasFormularioSolicitudCredito.RazonSocial, "C0003" },
            { ColumnasFormularioSolicitudCredito.TipoSolicitudCredito, "C0004" },
            { ColumnasFormularioSolicitudCredito.TipoDeRol, "C0005" },
            { ColumnasFormularioSolicitudCredito.Ciudad, "C0006" },
            { ColumnasFormularioSolicitudCredito.ResultadoAprobacionCredito, "C0007" },
            { ColumnasFormularioSolicitudCredito.ValorDeCredito, "C0008" },
            { ColumnasFormularioSolicitudCredito.NumeroRadicacion, "C0009" },
        };

        // Operadores definidos por el WS 
        _operadores = new Dictionary<OperadoresWorkManagement, string>
        {
            { OperadoresWorkManagement.IgualA, "=" },
            { OperadoresWorkManagement.NoIgualA, "<>" },
            { OperadoresWorkManagement.IzquierdaMayorIgualQueDerecha, ">=" },
            { OperadoresWorkManagement.IzquierdaMenorIgualQueDerecha, "<=" },
            { OperadoresWorkManagement.Like, "Like" },
        };
    }


    #endregion


    #region Metodos para Formulario/WorkFlow de (Historia Asociados)


    /// <summary>
    /// Metodo para Crear/Modificar formulario de historia asociado
    /// </summary>
    /// <param name="personaParaInteractuar">Informacion de la persona para enviar</param>
    /// <param name="nombreUsuarioRegistrado">Usuario registrado que ejecuto la operacion</param>
    /// <returns></returns>
    public Tuple<bool, string, bool> InteractuarRegistroFormularioHistoriaAsociado(Persona1 personaParaInteractuar, string nombreUsuarioRegistrado = "Administrador")
    {
        // Clause Guards para salir inmediato si no es valido
        if (personaParaInteractuar == null) throw new ArgumentNullException("Persona para insertar no puede estar nula!.");
        if (string.IsNullOrWhiteSpace(personaParaInteractuar.identificacion)) throw new ArgumentException("Idenficacion de la persona no puede estar vacia!.");

        bool exitoso = false;
        string barCode = string.Empty;
        bool esPrimerRegistro = false;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NumeroIdentificacion],
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = personaParaInteractuar.identificacion
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response == null) throw new Exception("Ocurrio un error al intentar conectarse al Work Management");

            // Si no consigo datos debo insertar a la persona
            if (response.Data.Count() <= 0)
            {
                InsertRequestDto insertRequest = new InsertRequestDto
                {
                    OperationUser = nombreUsuarioRegistrado,
                    FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                    OfficeCode = _codigoSucursal,
                    Data = CrearDiccionarioDatosPersona(personaParaInteractuar),
                    Header = _headerAuthenticacion,
                    ProcessId = _codigoProcesoAfiliacion,
                    //nextStepUser = "1"
                };

                InsertResponseDto insertResponse = client.Form_Insert(insertRequest);

                if (insertResponse == null) throw new Exception("Ocurrio un error al intentar insertar un registro del Work Management");

                exitoso = insertResponse.Success;
                barCode = insertResponse.BarCode;
                esPrimerRegistro = true;

                // Auditoria de la operacion
                WorkManagement_Aud workAuditoria = new WorkManagement_Aud
                {
                    exitoso = Convert.ToInt32(insertResponse.Success),
                    tipooperacion = (int)OperacionesWorkManagement.InsertarAfiliacion,
                    jsonEntidadPeticion = JsonConvert.SerializeObject(insertRequest),
                    jsonEntidadRespuesta = JsonConvert.SerializeObject(insertResponse),
                };

                _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);
            }
            else
            {
                // Actualizo si consegui datos consultando a la persona
                UpdateRequestDto updateRequest = new UpdateRequestDto
                {
                    OperationUser = nombreUsuarioRegistrado,
                    FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                    Data = CrearDiccionarioDatosPersona(personaParaInteractuar),
                    Header = _headerAuthenticacion,
                    FilterParameters = new FilterParametersDto
                    {
                        Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NumeroIdentificacion],
                        Operator = _operadores[OperadoresWorkManagement.IgualA],
                        Value = personaParaInteractuar.identificacion
                    }
                };

                ResponseDto responseUpdate = client.Form_Update(updateRequest);

                if (responseUpdate == null) throw new Exception("Ocurrio un error al intentar actualizar un registro del Work Management");

                exitoso = responseUpdate.Success;
                barCode = response.Data.Where(x => x.Field == "Radicado").Select(x => x.Value).FirstOrDefault();

                // Auditoria de la operacion
                WorkManagement_Aud workAuditoria = new WorkManagement_Aud
                {
                    exitoso = Convert.ToInt32(responseUpdate.Success),
                    tipooperacion = (int)OperacionesWorkManagement.ActualizarAfiliacion,
                    jsonEntidadPeticion = JsonConvert.SerializeObject(updateRequest),
                    jsonEntidadRespuesta = JsonConvert.SerializeObject(responseUpdate),
                };

                _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);
            }

            return Tuple.Create(exitoso, barCode, esPrimerRegistro);
        }
    }

    /// <summary>
    /// Metodo usado para consultar la informacion de la persona que quedo registrado en el formulario en el WM
    /// </summary>
    /// <param name="barCodePersona">BarCode de la persona que fue registrado cuando se registro la afiliacion</param>
    /// <returns></returns>
    public Persona1 ConsultarInformacionPersona(string barCodePersona)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = "Cod_Barras",
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = barCodePersona
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response.Data.Count() > 0)
            {
                Persona1 personaParaRetornar = BuildearEntidadPersona(response.Data);

                return personaParaRetornar;
            }
            else
            {
                return null;
            }
        }
    }

    public Persona1 ConsultarInformacionPersonaPorIdentificacion(string identificacion)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NumeroIdentificacion],
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = identificacion
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response != null)
            {
                if (response.Data != null)
                {
                    if (response.Data.Count() > 0)
                    {
                        Persona1 personaParaRetornar = BuildearEntidadPersona(response.Data);

                        return personaParaRetornar;
                    }
                }
            }

            return null;

        }
    }

    // Datos requeridos y disponibles para registrar en el formulario de WM
    DictionaryDto[] CrearDiccionarioDatosPersona(Persona1 personaParaInsertar)
    {
        List<DictionaryDto> listaDatos = new List<DictionaryDto>();

        DictionaryDto identificacion = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NumeroIdentificacion],
            Value = personaParaInsertar.identificacion
        };
        listaDatos.Add(identificacion);

        DictionaryDto nombreApellido = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NombreApellido],
            Value = personaParaInsertar.nombreCompleto
        };
        listaDatos.Add(nombreApellido);

        DictionaryDto contacto = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Contacto],
            Value = new DateTime(1990, 01, 01).ToString("yyyy/MM/dd")
        };
        listaDatos.Add(contacto);

        DictionaryDto direccion = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Direccion],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.direccion) ? personaParaInsertar.direccion : "Sin direccion"
        };
        listaDatos.Add(direccion);

        DictionaryDto departamento = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Departamento],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.nombre_ciudad) ? personaParaInsertar.nombre_ciudad : "Sin ciudad"
        };
        listaDatos.Add(departamento);

        DictionaryDto telefono = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Telefono],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.telefono) ? personaParaInsertar.telefono : "Sin telefono"
        };
        listaDatos.Add(telefono);

        DictionaryDto celular = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Celular],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.celular) ? personaParaInsertar.celular : "Sin celular"
        };
        listaDatos.Add(celular);

        DictionaryDto email = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Email],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.email) ? personaParaInsertar.email : "Sin email"
        };
        listaDatos.Add(email);

        DictionaryDto estado = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Estado],
            Value = "Activo" //personaParaInsertar.nomestado
        };
        listaDatos.Add(estado);

        DictionaryDto tipoIdentificacion = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.TipoIdentificacion],
            Value = personaParaInsertar.tipo_identificacion_descripcion
        };
        listaDatos.Add(tipoIdentificacion);

        DictionaryDto genero = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Genero],
            Value = personaParaInsertar.sexo == "M" ? "Masculino" : "Femenino"
        };
        listaDatos.Add(genero);

        DictionaryDto fechaNacimiento = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.FechaNacimiento],
            Value = personaParaInsertar.fechanacimiento.HasValue ? personaParaInsertar.fechanacimiento.Value.ToString("yyyy/MM/dd") : new DateTime(1990, 01, 01).ToString("yyyy/MM/dd")
        };
        listaDatos.Add(fechaNacimiento);

        DictionaryDto empresa = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Empresa],
            Value = !string.IsNullOrWhiteSpace(personaParaInsertar.empresa) ? personaParaInsertar.empresa : "Sin empresa"
        };
        listaDatos.Add(empresa);

        DictionaryDto fechaAfiliacion = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.FechaAfiliacion],
            Value = personaParaInsertar.fecha_afiliacion != DateTime.MinValue ? personaParaInsertar.fecha_afiliacion.ToString("yyyy/MM/dd") : new DateTime(1990, 01, 01).ToString("yyyy/MM/dd")
        };
        listaDatos.Add(fechaAfiliacion);

        DictionaryDto fechaRetiro = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.FechaRetiro],
            Value = personaParaInsertar.fecha_retiro != DateTime.MinValue ? personaParaInsertar.fecha_retiro.ToString("yyyy/MM/dd") : new DateTime(1990, 01, 01).ToString("yyyy/MM/dd")
        };
        listaDatos.Add(fechaRetiro);

        DictionaryDto aprobado = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Aprobado],
            Value = new DateTime(1990, 01, 01).ToString("yyyy/MM/dd")
        };
        listaDatos.Add(aprobado);

        DictionaryDto estrato = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.Estrato],
            Value = personaParaInsertar.Estrato.HasValue ? personaParaInsertar.Estrato.Value.ToString() : "Sin Estrato"
        };
        listaDatos.Add(estrato);

        return listaDatos.ToArray();
    }


    #endregion


    #region Metodos para Formulario/WorkFlow de (Solicitud de Creditos)


    /// <summary>
    /// Metodo usado para Crear/Actualizar Formulario de solicitud de credito
    /// </summary>
    /// <param name="datosSolicitud">Informacion de datos de la solicitud para crear el formulario de credito</param>
    /// <param name="nombreUsuarioRegistrado">Usuario que quedara registrado como autor de la operacion</param>
    /// <returns></returns>
    public Tuple<bool, string, long?> InteractuarRegistroFormulariosSolicitudCredito(DatosSolicitud datosSolicitud, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        // Clause Guards para salir inmediato si no es valido
        if (datosSolicitud == null) throw new ArgumentNullException("datos solicitud para insertar no puede estar nula!.");
        if (string.IsNullOrWhiteSpace(datosSolicitud.identificacion)) throw new ArgumentException("Idenficacion de la persona no puede estar vacia!.");
        if (string.IsNullOrWhiteSpace(datosSolicitud.nombre)) throw new ArgumentException("Nombre de la persona no puede estar vacia!.");
        if (string.IsNullOrWhiteSpace(datosSolicitud.tipoDePersona)) throw new ArgumentException("Tipo de persona no puede estar vacia!.");
        if (string.IsNullOrWhiteSpace(datosSolicitud.tipocrdito)) throw new ArgumentException("Tipo de credito no puede estar vacia!.");

        bool exitoso = false;
        string barCode = string.Empty;
        long? workflowId = null;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            InsertRequestDto insertRequest = new InsertRequestDto
            {
                OperationUser = nombreUsuarioRegistrado,
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.SolicitudCredito],
                OfficeCode = _codigoSucursal,
                Data = CrearDiccionarioDatosSolicitudCredito(datosSolicitud),
                Header = _headerAuthenticacion,
                ProcessId = _codigoProcesoCredito,
                //nextStepUser = "1"
            };

            InsertResponseDto insertResponse = client.Form_Insert(insertRequest);

            if (insertResponse == null) throw new Exception("Ocurrio un error al intentar insertar un registro del Work Management");

            exitoso = insertResponse.Success;
            barCode = insertResponse.BarCode;
            workflowId = insertResponse.workflowId;

            // Auditoria de la operacion
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(insertResponse.Success),
                tipooperacion = (int)OperacionesWorkManagement.CreacionFormularioSolicitudCredito,
                jsonEntidadPeticion = JsonConvert.SerializeObject(insertRequest),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(insertResponse),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);

            return Tuple.Create(exitoso, barCode, workflowId);
        }
    }

    public Tuple<bool, long> IniciarWorkFlowCredito(string barCode, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        if (string.IsNullOrWhiteSpace(barCode)) throw new ArgumentNullException("barCode vacio, no puedes seguir sin el barCode");

        bool exitoso = false;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Inicia in workflow en el WM
            StartWorkflowRequestDto startWorkFlow = new StartWorkflowRequestDto
            {
                OperationUser = nombreUsuarioRegistrado,
                //NextStepUser = nombreUsuarioRegistrado,
                BarCode = barCode,
                ProcessId = _codigoProcesoCredito,
                Header = _headerAuthenticacion,
            };

            StartWorkflowResponseDto response = client.Workflow_Start(startWorkFlow);

            if (response == null) throw new Exception("Ocurrio un error al intentar iniciar un workflow del Work Management");

            exitoso = response.Success;

            // Auditoria de la operacion
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)OperacionesWorkManagement.SolicitudCredito,
                jsonEntidadPeticion = JsonConvert.SerializeObject(startWorkFlow),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);

            return Tuple.Create(exitoso, response.WorkflowId);
        }
    }

    public bool RunTaskWorkFlowCredito(string barCode, long workFlowId, StepsWorkManagementWorkFlowCredito step, string comentario, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        if (string.IsNullOrWhiteSpace(barCode)) throw new ArgumentNullException("barCode vacio, no puedes seguir sin el barCode");
        if (step == StepsWorkManagementWorkFlowCredito.SinStep) throw new ArgumentException("step invalido!.");

        bool existoso = false;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Marca un step como completado para que pase al siguiente step
            RunTaskRequestDto runTaskWorkFlow = new RunTaskRequestDto
            {
                BarCode = barCode,
                ProcessId = _codigoProcesoCredito,
                WorkflowId = workFlowId,
                //NextStepUser = nombreUsuarioRegistrado,
                Step = (int)step,
                Comment = !string.IsNullOrWhiteSpace(comentario) ? comentario : " Sin Observaciones",
                Header = _headerAuthenticacion
            };

            ResponseDto response = client.Workflow_RunTask(runTaskWorkFlow);

            if (response == null) throw new Exception("Ocurrio un error al intentar correr un task de workflow del Work Management");

            existoso = response.Success;

            OperacionesWorkManagement operacion = OperacionesWorkManagement.SinOperacion;

            // Homologacion para tipo de operacion de la auditoria
            switch (step)
            {
                case StepsWorkManagementWorkFlowCredito.AprobacionCredito:
                    operacion = OperacionesWorkManagement.AnalisisDeCredito;
                    break;
                case StepsWorkManagementWorkFlowCredito.AprobacionCredito2:
                    operacion = OperacionesWorkManagement.AprobacionCredito;
                    break;
            }

            // Auditoria de la operacion
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)operacion,
                jsonEntidadPeticion = JsonConvert.SerializeObject(runTaskWorkFlow),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);
        }

        return existoso;
    }

    // Datos requeridos y disponibles para registrar en el formulario de Solicitud de Credito de WM
    DictionaryDto[] CrearDiccionarioDatosSolicitudCredito(DatosSolicitud datosSolicitud)
    {
        List<DictionaryDto> listaDatos = new List<DictionaryDto>();

        DictionaryDto nombreHistoriaAsociado = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.HistoriaAsociado],
            Value = datosSolicitud.identificacion.Trim()
        };
        listaDatos.Add(nombreHistoriaAsociado);

        DictionaryDto tipoDePersona = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.TipoDePersona],
            Value = datosSolicitud.tipoDePersona
        };
        listaDatos.Add(tipoDePersona);

        DictionaryDto nombreORazonSocial = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.RazonSocial],
            Value = datosSolicitud.nombre
        };
        listaDatos.Add(nombreORazonSocial);

        DictionaryDto tipoDeCredito = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.TipoSolicitudCredito],
            Value = datosSolicitud.tipocrdito
        };
        listaDatos.Add(tipoDeCredito);

        DictionaryDto tipoDeRol = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.TipoDeRol],
            Value = datosSolicitud.esAsociado ? "Asociado" : "Tercero"
        };
        listaDatos.Add(tipoDeRol);

        DictionaryDto ciudad = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.Ciudad],
            Value = !string.IsNullOrWhiteSpace(datosSolicitud.nombre_ciudad) ? datosSolicitud.nombre_ciudad : "Sin ciudad"
        };
        listaDatos.Add(ciudad);

        DictionaryDto resultadoAprobacionCreditos = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.ResultadoAprobacionCredito],
            Value = "-- NULL --"
        };
        listaDatos.Add(resultadoAprobacionCreditos);

        DictionaryDto montoDelCredito = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.ValorDeCredito],
            Value = datosSolicitud.montosolicitado.ToString()
        };
        listaDatos.Add(montoDelCredito);

        DictionaryDto numeroCredito = new DictionaryDto
        {
            Field = _diccionarioColumnasFormulario_10003[ColumnasFormularioSolicitudCredito.NumeroRadicacion],
            Value = datosSolicitud.numeroradicado.ToString()
        };
        listaDatos.Add(numeroCredito);

        return listaDatos.ToArray();
    }


    #endregion


    #region Metodos para Formulario/WorkFlow de (Retiro de Asociados)


    public bool RunTaskWorkFlowRetiroAsociado(string barCode, StepsWorkManagementWorkFlowRetiroAsociados step, string comentario, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        if (string.IsNullOrWhiteSpace(barCode)) throw new ArgumentNullException("barCode vacio, no puedes seguir sin el barCode");
        if (step == StepsWorkManagementWorkFlowRetiroAsociados.SinStep) throw new ArgumentException("step invalido!.");

        bool existoso = false;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Marca un step como completado para que pase al siguiente step
            RunTaskRequestDto runTaskWorkFlow = new RunTaskRequestDto
            {
                BarCode = barCode,
                ProcessId = _codigoProcesoRetiroAsociado,
                //NextStepUser = nombreUsuarioRegistrado,
                Step = (int)step,
                Comment = !string.IsNullOrWhiteSpace(comentario) ? comentario : " Sin Observaciones ",
                Header = _headerAuthenticacion
            };

            ResponseDto response = client.Workflow_RunTask(runTaskWorkFlow);

            if (response == null) throw new Exception("Ocurrio un error al intentar correr un task de workflow del Work Management");

            existoso = response.Success;

            OperacionesWorkManagement operacion = OperacionesWorkManagement.SinOperacion;

            // Homologacion para tipo de operacion de la auditoria
            switch (step)
            {
                case StepsWorkManagementWorkFlowRetiroAsociados.CruceDeCuentas:
                    operacion = OperacionesWorkManagement.CruceDeCuentas;
                    break;
                case StepsWorkManagementWorkFlowRetiroAsociados.CargaDocumentosCruceDeCuentas:
                    operacion = OperacionesWorkManagement.CargaDocumentosCruceDeCuentas;
                    break;
                case StepsWorkManagementWorkFlowRetiroAsociados.CargaComprobanteDeDesembolso:
                    operacion = OperacionesWorkManagement.CargaComprobanteDeDesembolso;
                    break;
                case StepsWorkManagementWorkFlowRetiroAsociados.CargaComprobanteGiro:
                    operacion = OperacionesWorkManagement.CargaComprobanteGiro;
                    break;
            }

            // Auditoria de la operacion
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)operacion,
                jsonEntidadPeticion = JsonConvert.SerializeObject(runTaskWorkFlow),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);
        }

        return existoso;
    }


    #endregion


    #region Metodos para Formulario/WorkFlow de (Pago a Proveedores)


    public bool RunTaskWorkFlowPagoProveedores(string barCode, StepsWorkManagementWorkFlowPagoProveedores step, string comentario, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        if (string.IsNullOrWhiteSpace(barCode)) throw new ArgumentNullException("barCode vacio, no puedes seguir sin el barCode");
        if (step == StepsWorkManagementWorkFlowPagoProveedores.SinStep) throw new ArgumentException("step invalido!.");

        bool existoso = false;

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Marca un step como completado para que pase al siguiente step
            RunTaskRequestDto runTaskWorkFlow = new RunTaskRequestDto
            {
                BarCode = barCode,
                ProcessId = _codigoProcesoPagoAProveedores,
                //NextStepUser = nombreUsuarioRegistrado,
                Step = (int)step,
                Comment = !string.IsNullOrWhiteSpace(comentario) ? comentario : " Sin Observaciones ",
                Header = _headerAuthenticacion
            };

            ResponseDto response = client.Workflow_RunTask(runTaskWorkFlow);

            if (response == null) throw new Exception("Ocurrio un error al intentar correr un task de workflow del Work Management");

            existoso = response.Success;

            OperacionesWorkManagement operacion = OperacionesWorkManagement.SinOperacion;

            // Homologacion para tipo de operacion de la auditoria
            switch (step)
            {
                case StepsWorkManagementWorkFlowPagoProveedores.Causacion:
                    operacion = OperacionesWorkManagement.CausacionPagoProveedor;
                    break;
                case StepsWorkManagementWorkFlowPagoProveedores.Pago:
                    operacion = OperacionesWorkManagement.PagoFacturaPagoProveedor;
                    break;
            }

            // Auditoria de la operacion
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)operacion,
                jsonEntidadPeticion = JsonConvert.SerializeObject(runTaskWorkFlow),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);
        }

        return existoso;
    }


    #endregion


    #region Metodos para Anexar Archivos a Formularios/WorkFlows


    public bool AnexarArchivoAFormularioAfiliacion(WorkFlowFilesDTO workFlowFile, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        return AnexarArchivoAFormularioAfiliacion(new List<WorkFlowFilesDTO> { workFlowFile }, barCode, tipoArchivo, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAFormularioAfiliacion(List<WorkFlowFilesDTO> workFlowFiles, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        // Mandamos el codigo del directorio
        return AnexarArchivoAFormulario(workFlowFiles, barCode, tipoArchivo, _codigoDirectorioArchivosAfiliacion, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAFormularioRetiro(WorkFlowFilesDTO workFlowFile, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        return AnexarArchivoAFormularioRetiro(new List<WorkFlowFilesDTO> { workFlowFile }, barCode, tipoArchivo, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAFormularioRetiro(List<WorkFlowFilesDTO> workFlowFiles, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        // Mandamos el codigo del directorio
        return AnexarArchivoAFormulario(workFlowFiles, barCode, tipoArchivo, _codigoDirectorioArchivosRetiro, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAFormularioPagoProveedores(WorkFlowFilesDTO workFlowFile, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        return AnexarArchivoAFormularioPagoProveedores(new List<WorkFlowFilesDTO> { workFlowFile }, barCode, tipoArchivo, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAFormularioPagoProveedores(List<WorkFlowFilesDTO> workFlowFiles, string barCode, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        // Mandamos el codigo del directorio
        return AnexarArchivoAFormulario(workFlowFiles, barCode, tipoArchivo, _codigoDirectorioArchivosPagoProveedores, nombreUsuarioRegistrado);
    }

    bool AnexarArchivoAFormulario(List<WorkFlowFilesDTO> workFlowFiles, string barCode, TipoArchivoWorkManagement tipoArchivo, string codigoDirectorioArchivos, string nombreUsuarioRegistrado)
    {
        if (tipoArchivo == TipoArchivoWorkManagement.SinTipoArchivo) throw new ArgumentException("Tipo de Archivo invalido!.");
        if (string.IsNullOrWhiteSpace(barCode)) throw new ArgumentNullException("BarCode invalido!.");
        if (workFlowFiles == null || workFlowFiles.Count <= 0) throw new ArgumentNullException("workFlowFiles nulo o vacio!.");
        if (!workFlowFiles.TrueForAll(x => !string.IsNullOrWhiteSpace(x.Base64DataFile) && !string.IsNullOrWhiteSpace(x.Extension)))
        {
            throw new ArgumentException("Uno de los archivos a anexar al workflow esta invalido!.");
        }

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            AttachRequestDto workFlowArchivo = new AttachRequestDto
            {
                OperationUser = nombreUsuarioRegistrado,
                BarCode = barCode,
                Files = BuildearEntidadWorkFlowFiles(workFlowFiles, tipoArchivo, codigoDirectorioArchivos),
                Header = _headerAuthenticacion
            };

            InsertResponseDto response = client.Form_AttachFiles(workFlowArchivo);

            // Homologacion para tipo de operacion de la auditoria
            OperacionesWorkManagement operacion = HomologarTipoArchivoOperacionAuditoria(tipoArchivo);

            // Auditoria de la operacion
            workFlowArchivo.Files = null;
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)operacion,
                jsonEntidadPeticion = JsonConvert.SerializeObject(workFlowArchivo),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);

            return response != null && response.Success;
        }
    }

    public bool AnexarArchivoAWorkFlowCartera(WorkFlowFilesDTO workFlowFile, long workFlowID, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        return AnexarArchivoAWorkFlowCartera(new List<WorkFlowFilesDTO> { workFlowFile }, workFlowID, tipoArchivo, nombreUsuarioRegistrado);
    }

    public bool AnexarArchivoAWorkFlowCartera(List<WorkFlowFilesDTO> workFlowFiles, long workFlowID, TipoArchivoWorkManagement tipoArchivo, string nombreUsuarioRegistrado = _usuarioDefault)
    {
        // Mandamos el codigo del directorio
        return AnexarArchivoAWorkFlow(workFlowFiles, workFlowID, tipoArchivo, _codigoDirectorioArchivosCartera, nombreUsuarioRegistrado);
    }

    bool AnexarArchivoAWorkFlow(List<WorkFlowFilesDTO> workFlowFiles, long workFlowID, TipoArchivoWorkManagement tipoArchivo, string codigoDirectorioArchivos, string nombreUsuarioRegistrado)
    {
        if (tipoArchivo == TipoArchivoWorkManagement.SinTipoArchivo) throw new ArgumentException("Tipo de Archivo invalido!.");
        if (workFlowID <= 0) throw new ArgumentException("WorkFlowID invalido!.");
        if (workFlowFiles == null || workFlowFiles.Count <= 0) throw new ArgumentNullException("workFlowFiles nulo o vacio!.");
        if (!workFlowFiles.TrueForAll(x => !string.IsNullOrWhiteSpace(x.Base64DataFile) && !string.IsNullOrWhiteSpace(x.Extension)))
        {
            throw new ArgumentException("Uno de los archivos a anexar al workflow esta invalido!.");
        }

        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            WorkflowAttachFilesRequestDto workFlowArchivo = new WorkflowAttachFilesRequestDto
            {
                OperationUser = nombreUsuarioRegistrado,
                WorkflowId = workFlowID.ToString(),
                Files = BuildearEntidadWorkFlowFiles(workFlowFiles, tipoArchivo, codigoDirectorioArchivos),
                Header = _headerAuthenticacion
            };

            InsertResponseDto response = client.Workflow_AttachFiles(workFlowArchivo);

            // Homologacion para tipo de operacion de la auditoria
            OperacionesWorkManagement operacion = HomologarTipoArchivoOperacionAuditoria(tipoArchivo);

            // Auditoria de la operacion
            workFlowArchivo.Files = null;
            WorkManagement_Aud workAuditoria = new WorkManagement_Aud
            {
                exitoso = Convert.ToInt32(response.Success),
                tipooperacion = (int)operacion,
                jsonEntidadPeticion = JsonConvert.SerializeObject(workFlowArchivo),
                jsonEntidadRespuesta = JsonConvert.SerializeObject(response),
            };

            _workManagementService.CrearWorkManagement_Aud(workAuditoria, _usuario);

            return response != null && response.Success;
        }
    }

    FileDto[] BuildearEntidadWorkFlowFiles(List<WorkFlowFilesDTO> workFlowFiles, TipoArchivoWorkManagement tipoArchivo, string directoryCode)
    {
        List<FileDto> filesDTO = new List<FileDto>();

        foreach (var file in workFlowFiles)
        {
            FileDto fileToAttach = new FileDto
            {
                Base64String = file.Base64DataFile,
                Description = !string.IsNullOrWhiteSpace(file.Descripcion) ? file.Descripcion : "Sin descripcion",
                Ext = file.Extension,
                DocumentTypeCode = HomologarTipoArchivo(tipoArchivo),
                DirectoryCode = directoryCode
            };

            filesDTO.Add(fileToAttach);
        }

        return filesDTO.ToArray();
    }

    string HomologarTipoArchivo(TipoArchivoWorkManagement tipoArchivo)
    {
        if (tipoArchivo == TipoArchivoWorkManagement.SinTipoArchivo) throw new ArgumentException("Tipo de Documento Invalido!.");

        GeneralService generalService = new GeneralService();

        // Codigo de parametro general va segun el valor en el ENUM
        General parametroCodigoTipoDocumento = generalService.ConsultarGeneral((int)tipoArchivo, _usuario);

        string codigoTipoArchivo = string.Empty;
        if (parametroCodigoTipoDocumento != null && !string.IsNullOrWhiteSpace(parametroCodigoTipoDocumento.valor))
        {
            codigoTipoArchivo = parametroCodigoTipoDocumento.valor;
        }
        else
        {
            codigoTipoArchivo = "00";
        }

        return codigoTipoArchivo;
    }

    OperacionesWorkManagement HomologarTipoArchivoOperacionAuditoria(TipoArchivoWorkManagement tipoArchivo)
    {
        OperacionesWorkManagement operacion = OperacionesWorkManagement.SinOperacion;

        switch (tipoArchivo)
        {
            case TipoArchivoWorkManagement.SolicitudCredito:
                operacion = OperacionesWorkManagement.AnexoDocumentoSolicitudCredito;
                break;
            case TipoArchivoWorkManagement.TablaAmortizacion:
                operacion = OperacionesWorkManagement.AnexoDocumentoTablaAmortizacion;
                break;
            case TipoArchivoWorkManagement.CruceCuentas:
                operacion = OperacionesWorkManagement.AnexoDocumentoCruceCuentas;
                break;
            case TipoArchivoWorkManagement.ComprobanteDesembolsoCredito:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteDesembolsoCredito;
                break;
            case TipoArchivoWorkManagement.ComprobanteGiroCredito:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteGiroCredito;
                break;
            case TipoArchivoWorkManagement.ComprobanteDesembolsoCruceCuentas:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteDesembolsoCruceCuentas;
                break;
            case TipoArchivoWorkManagement.ComprobanteGiroCruceCuentas:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteGiroCruceCuentas;
                break;
            case TipoArchivoWorkManagement.DocumentosAnexoAfiliacion:
                operacion = OperacionesWorkManagement.AnexoDocumentoDocumentosAnexoAfiliacion;
                break;
            case TipoArchivoWorkManagement.ComprobanteCausacionPagoProveedores:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteCausacionPagoProveedores;
                break;
            case TipoArchivoWorkManagement.ComprobanteEgresoPagoProveedores:
                operacion = OperacionesWorkManagement.AnexoDocumentoComprobanteEgresoPagoProveedores;
                break;
        }

        return operacion;
    }


    #endregion


    #region Metodos para Consultar Informacion en Formulario de (Historia Asociados)  "usado en WorkFlow para Afiliaciones"


    /// <summary>
    /// Metodo usado para consultar la informacion de la persona que quedo registrado en el formulario en el WM
    /// </summary>
    /// <param name="barCodePersona">BarCode de la persona que fue registrado cuando se registro la afiliacion</param>
    /// <returns></returns>
    public Persona1 ConsultarInformacionPersonaDeFormularioHistoriaAsociado(string barCodePersona)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = "Cod_Barras",
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = barCodePersona
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response != null && response.Data != null && response.Data.Count() > 0)
            {
                Persona1 personaParaRetornar = BuildearEntidadPersona(response.Data);

                return personaParaRetornar;
            }
            else
            {
                return null;
            }
        }
    }

    public Persona1 ConsultarInformacionPersonaPorIdentificacionDeFormularioHistoriaAsociado(string identificacion)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.HistoriaAsociado],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = _diccionarioColumnasFormulario_1[ColumnasFormularioHistoriaAsociado.NumeroIdentificacion],
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = identificacion
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response != null && response.Data != null && response.Data.Count() > 0)
            {
                Persona1 personaParaRetornar = BuildearEntidadPersona(response.Data);

                return personaParaRetornar;
            }
            else
            {
                return null;
            }

        }
    }

    Persona1 BuildearEntidadPersona(DictionaryDto[] data)
    {
        Persona1 personaParaRetornar = new Persona1();

        if (data != null && data.Count() > 0)
        {
            personaParaRetornar.identificacion = data[0].Value;
            personaParaRetornar.nombre = data[1].Value;
            personaParaRetornar.tipo_identificacion_descripcion = data[2].Value;
            personaParaRetornar.sexo = data[3].Value;

            DateTime fechaNacimiento = DateTime.MinValue;
            DateTime.TryParse(data[4].Value, out fechaNacimiento);

            personaParaRetornar.fechanacimiento = fechaNacimiento != DateTime.MinValue ? fechaNacimiento : default(DateTime?);

            personaParaRetornar.direccion = data[5].Value;
            personaParaRetornar.departamento = data[6].Value;
            personaParaRetornar.telefono = data[7].Value;
            personaParaRetornar.celular = data[8].Value;
            personaParaRetornar.email = data[9].Value;
            personaParaRetornar.estado = data[10].Value;
            personaParaRetornar.empresa = data[11].Value;

            DateTime fechaAfiliacion = DateTime.MinValue;
            DateTime.TryParse(data[12].Value, out fechaAfiliacion);

            personaParaRetornar.fecha_afiliacion = fechaAfiliacion;

            DateTime fechaRetiro = DateTime.MinValue;
            DateTime.TryParse(data[13].Value, out fechaRetiro);

            personaParaRetornar.fecha_retiro = fechaRetiro;

            int estrato = 0;
            int.TryParse(data[15].Value, out estrato);
            personaParaRetornar.Estrato = estrato != 0 ? estrato : default(int?);

            personaParaRetornar.usuariocreacion = data[15].Value;
            if (data.Length > 19)
                personaParaRetornar.barCode = data[19].Value;
        }

        return personaParaRetornar;
    }


    #endregion


    #region Metodos para Consultar Informacion en Formulario de (Correspondencia Recibida) "usado en WorkFlow Retiro de Asociados"


    public string ConsultarRadicadoPorIdentificacionDeFormularioCorrespondeciaRecibida(string identificacion)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.CorrespondenciaRecibida],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = "C0004", // Identificacion asociado
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = identificacion
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response.Data.Count() > 0)
            {
                return response.Data.LastOrDefault(x => x.Field.ToUpper() == "RADICADO").Value; 
            }
            else
            {
                return null;
            }
        }
    }


    #endregion


    #region Metodos para Consultar Informacion en Formulario de (Facturas Recibidas) "usado en WorkFlow Pago a Proveedores"


    public string ConsultarRadicadoPorNumeroFacturaDeFormularioFacturasRecibidas(string numeroFactura)
    {
        using (WorkManagerServicesClient client = new WorkManagerServicesClient())
        {
            // Intenta consultar a la persona en el WM por el numero de identificacion
            QueryRequestDto query = new QueryRequestDto
            {
                FormCode = _diccionarioCodigoFormularios[FormulariosWorkManagement.FacturasRecibidas],
                Header = _headerAuthenticacion,
                FilterParameters = new FilterParametersDto
                {
                    Field = "C0002", // Numero Factura
                    Operator = _operadores[OperadoresWorkManagement.IgualA],
                    Value = numeroFactura
                }
            };

            ResponseDto response = client.Form_GetData(query);

            if (response.Data.Count() > 0)
            {
                return response.Data.LastOrDefault(x => x.Field.ToUpper() == "RADICADO").Value;
            }
            else
            {
                return null;
            }
        }
    }


    #endregion


    // ================================================================
    //                    AUTOGENERADO NO MODIFICAR
    // ================================================================
    // Clases usadas para comunicarse con el WebService del WM
    #region Clases Web Service WorkMangement (WSDL)

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "InsertRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class InsertRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.DictionaryDto[] DataField;

        private InterfazWorkManagement.FileDto[] FilesField;

        private string FormCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string OfficeCodeField;

        private string OperationUserField;

        private System.Nullable<long> ProcessIdField;

        private string nextStepUserField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.DictionaryDto[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FileDto[] Files
        {
            get
            {
                return this.FilesField;
            }
            set
            {
                this.FilesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormCode
        {
            get
            {
                return this.FormCodeField;
            }
            set
            {
                this.FormCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OfficeCode
        {
            get
            {
                return this.OfficeCodeField;
            }
            set
            {
                this.OfficeCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> ProcessId
        {
            get
            {
                return this.ProcessIdField;
            }
            set
            {
                this.ProcessIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nextStepUser
        {
            get
            {
                return this.nextStepUserField;
            }
            set
            {
                this.nextStepUserField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "HeaderDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class HeaderDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string TokenField;

        private string UserField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Token
        {
            get
            {
                return this.TokenField;
            }
            set
            {
                this.TokenField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User
        {
            get
            {
                return this.UserField;
            }
            set
            {
                this.UserField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DictionaryDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class DictionaryDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string FieldField;

        private string ValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Field
        {
            get
            {
                return this.FieldField;
            }
            set
            {
                this.FieldField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FileDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class FileDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string Base64StringField;

        private string DescriptionField;

        private string DirectoryCodeField;

        private string DocumentTypeCodeField;

        private string ExtField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Base64String
        {
            get
            {
                return this.Base64StringField;
            }
            set
            {
                this.Base64StringField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DirectoryCode
        {
            get
            {
                return this.DirectoryCodeField;
            }
            set
            {
                this.DirectoryCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DocumentTypeCode
        {
            get
            {
                return this.DocumentTypeCodeField;
            }
            set
            {
                this.DocumentTypeCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ext
        {
            get
            {
                return this.ExtField;
            }
            set
            {
                this.ExtField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "InsertResponseDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class InsertResponseDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BarCodeField;

        private string[] FileCodesField;

        private string MessageField;

        private long RecordIdField;

        private bool SuccessField;

        private long workflowIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BarCode
        {
            get
            {
                return this.BarCodeField;
            }
            set
            {
                this.BarCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] FileCodes
        {
            get
            {
                return this.FileCodesField;
            }
            set
            {
                this.FileCodesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long RecordId
        {
            get
            {
                return this.RecordIdField;
            }
            set
            {
                this.RecordIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long workflowId
        {
            get
            {
                return this.workflowIdField;
            }
            set
            {
                this.workflowIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "AttachRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class AttachRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BarCodeField;

        private InterfazWorkManagement.FileDto[] FilesField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string OperationUserField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BarCode
        {
            get
            {
                return this.BarCodeField;
            }
            set
            {
                this.BarCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FileDto[] Files
        {
            get
            {
                return this.FilesField;
            }
            set
            {
                this.FilesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "UpdateRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class UpdateRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.DictionaryDto[] DataField;

        private InterfazWorkManagement.FilterParametersDto FilterParametersField;

        private string FormCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string OperationUserField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.DictionaryDto[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FilterParametersDto FilterParameters
        {
            get
            {
                return this.FilterParametersField;
            }
            set
            {
                this.FilterParametersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormCode
        {
            get
            {
                return this.FormCodeField;
            }
            set
            {
                this.FormCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FilterParametersDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class FilterParametersDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string FieldField;

        private string OperatorField;

        private string ValueField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Field
        {
            get
            {
                return this.FieldField;
            }
            set
            {
                this.FieldField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Operator
        {
            get
            {
                return this.OperatorField;
            }
            set
            {
                this.OperatorField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value
        {
            get
            {
                return this.ValueField;
            }
            set
            {
                this.ValueField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ResponseDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class ResponseDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.DictionaryDto[] DataField;

        private string MessageField;

        private bool SuccessField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.DictionaryDto[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DeleteRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class DeleteRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.FilterParametersDto FilterParametersField;

        private string FormCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string OperationUserField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FilterParametersDto FilterParameters
        {
            get
            {
                return this.FilterParametersField;
            }
            set
            {
                this.FilterParametersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormCode
        {
            get
            {
                return this.FormCodeField;
            }
            set
            {
                this.FormCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "QueryRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class QueryRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.FilterParametersDto FilterParametersField;

        private string FormCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FilterParametersDto FilterParameters
        {
            get
            {
                return this.FilterParametersField;
            }
            set
            {
                this.FilterParametersField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FormCode
        {
            get
            {
                return this.FormCodeField;
            }
            set
            {
                this.FormCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FileRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class FileRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BarCodeField;

        private string DirectoryCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BarCode
        {
            get
            {
                return this.BarCodeField;
            }
            set
            {
                this.BarCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DirectoryCode
        {
            get
            {
                return this.DirectoryCodeField;
            }
            set
            {
                this.DirectoryCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FileResponseDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class FileResponseDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.FilePropertiesDto[] FilePropertiesField;

        private string MessageField;

        private bool SuccessField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FilePropertiesDto[] FileProperties
        {
            get
            {
                return this.FilePropertiesField;
            }
            set
            {
                this.FilePropertiesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "FilePropertiesDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class FilePropertiesDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string CodeField;

        private string DescriptionField;

        private string ExtField;

        private long SizeField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code
        {
            get
            {
                return this.CodeField;
            }
            set
            {
                this.CodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description
        {
            get
            {
                return this.DescriptionField;
            }
            set
            {
                this.DescriptionField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Ext
        {
            get
            {
                return this.ExtField;
            }
            set
            {
                this.ExtField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long Size
        {
            get
            {
                return this.SizeField;
            }
            set
            {
                this.SizeField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StringFileRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class StringFileRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string FileCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileCode
        {
            get
            {
                return this.FileCodeField;
            }
            set
            {
                this.FileCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StringFileResponseDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class StringFileResponseDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.FilePropertiesDto FileField;

        private string MessageField;

        private bool SuccessField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FilePropertiesDto File
        {
            get
            {
                return this.FileField;
            }
            set
            {
                this.FileField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StartWorkflowRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class StartWorkflowRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BarCodeField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string NextStepUserField;

        private string OperationUserField;

        private long ProcessIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BarCode
        {
            get
            {
                return this.BarCodeField;
            }
            set
            {
                this.BarCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NextStepUser
        {
            get
            {
                return this.NextStepUserField;
            }
            set
            {
                this.NextStepUserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ProcessId
        {
            get
            {
                return this.ProcessIdField;
            }
            set
            {
                this.ProcessIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "StartWorkflowResponseDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class StartWorkflowResponseDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string MessageField;

        private bool SuccessField;

        private long WorkflowIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long WorkflowId
        {
            get
            {
                return this.WorkflowIdField;
            }
            set
            {
                this.WorkflowIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "WorkflowAttachFilesRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class WorkflowAttachFilesRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.FileDto[] FilesField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string OperationUserField;

        private string WorkflowIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.FileDto[] Files
        {
            get
            {
                return this.FilesField;
            }
            set
            {
                this.FilesField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OperationUser
        {
            get
            {
                return this.OperationUserField;
            }
            set
            {
                this.OperationUserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkflowId
        {
            get
            {
                return this.WorkflowIdField;
            }
            set
            {
                this.WorkflowIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "RunTaskRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class RunTaskRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private string BarCodeField;

        private string CommentField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private string NextStepUserField;

        private System.Nullable<int> ProcessIdField;

        private int StepField;

        private System.Nullable<long> WorkflowIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BarCode
        {
            get
            {
                return this.BarCodeField;
            }
            set
            {
                this.BarCodeField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Comment
        {
            get
            {
                return this.CommentField;
            }
            set
            {
                this.CommentField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NextStepUser
        {
            get
            {
                return this.NextStepUserField;
            }
            set
            {
                this.NextStepUserField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> ProcessId
        {
            get
            {
                return this.ProcessIdField;
            }
            set
            {
                this.ProcessIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Step
        {
            get
            {
                return this.StepField;
            }
            set
            {
                this.StepField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> WorkflowId
        {
            get
            {
                return this.WorkflowIdField;
            }
            set
            {
                this.WorkflowIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ListRequestDto", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class ListRequestDto : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.Nullable<long> FatherItemIdField;

        private InterfazWorkManagement.HeaderDto HeaderField;

        private long ListIdField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> FatherItemId
        {
            get
            {
                return this.FatherItemIdField;
            }
            set
            {
                this.FatherItemIdField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.HeaderDto Header
        {
            get
            {
                return this.HeaderField;
            }
            set
            {
                this.HeaderField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ListId
        {
            get
            {
                return this.ListIdField;
            }
            set
            {
                this.ListIdField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ListResponseDtoOfItemListagpjPdjDr", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class ListResponseDtoOfItemListagpjPdjDr : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private InterfazWorkManagement.ItemLista[] DataField;

        private string MessageField;

        private bool SuccessField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.ItemLista[] Data
        {
            get
            {
                return this.DataField;
            }
            set
            {
                this.DataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success
        {
            get
            {
                return this.SuccessField;
            }
            set
            {
                this.SuccessField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "ItemLista", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class ItemLista : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private bool ActivoField;

        private string CodigoField;

        private System.DateTime FechaField;

        private string HostField;

        private long IdItemField;

        private long IdItemPadreField;

        private long IdListaField;

        private InterfazWorkManagement.Lista ListaField;

        private string NombreField;

        private string UsuarioField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Activo
        {
            get
            {
                return this.ActivoField;
            }
            set
            {
                this.ActivoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Codigo
        {
            get
            {
                return this.CodigoField;
            }
            set
            {
                this.CodigoField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Fecha
        {
            get
            {
                return this.FechaField;
            }
            set
            {
                this.FechaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Host
        {
            get
            {
                return this.HostField;
            }
            set
            {
                this.HostField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdItem
        {
            get
            {
                return this.IdItemField;
            }
            set
            {
                this.IdItemField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdItemPadre
        {
            get
            {
                return this.IdItemPadreField;
            }
            set
            {
                this.IdItemPadreField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdLista
        {
            get
            {
                return this.IdListaField;
            }
            set
            {
                this.IdListaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.Lista Lista
        {
            get
            {
                return this.ListaField;
            }
            set
            {
                this.ListaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre
        {
            get
            {
                return this.NombreField;
            }
            set
            {
                this.NombreField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Usuario
        {
            get
            {
                return this.UsuarioField;
            }
            set
            {
                this.UsuarioField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Lista", Namespace = "http://schemas.datacontract.org/2004/07/WorkManager.Entities")]
    public partial class Lista : object, System.Runtime.Serialization.IExtensibleDataObject
    {

        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        private System.DateTime FechaField;

        private string HostField;

        private long IdListaField;

        private long IdListaPadreField;

        private System.Nullable<long> IdListaV10Field;

        private InterfazWorkManagement.ItemLista[] ItemsListaField;

        private string NombreField;

        private string UsuarioField;

        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Fecha
        {
            get
            {
                return this.FechaField;
            }
            set
            {
                this.FechaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Host
        {
            get
            {
                return this.HostField;
            }
            set
            {
                this.HostField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdLista
        {
            get
            {
                return this.IdListaField;
            }
            set
            {
                this.IdListaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdListaPadre
        {
            get
            {
                return this.IdListaPadreField;
            }
            set
            {
                this.IdListaPadreField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> IdListaV10
        {
            get
            {
                return this.IdListaV10Field;
            }
            set
            {
                this.IdListaV10Field = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public InterfazWorkManagement.ItemLista[] ItemsLista
        {
            get
            {
                return this.ItemsListaField;
            }
            set
            {
                this.ItemsListaField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre
        {
            get
            {
                return this.NombreField;
            }
            set
            {
                this.NombreField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Usuario
        {
            get
            {
                return this.UsuarioField;
            }
            set
            {
                this.UsuarioField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IWorkManagerServices")]
public interface IWorkManagerServices
{

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Insert", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_InsertResponse")]
    InterfazWorkManagement.InsertResponseDto Form_Insert(InterfazWorkManagement.InsertRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Insert", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_InsertResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Form_InsertAsync(InterfazWorkManagement.InsertRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_AttachFiles", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_AttachFilesResponse")]
    InterfazWorkManagement.InsertResponseDto Form_AttachFiles(InterfazWorkManagement.AttachRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_AttachFiles", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_AttachFilesResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Form_AttachFilesAsync(InterfazWorkManagement.AttachRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Update", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_UpdateResponse")]
    InterfazWorkManagement.ResponseDto Form_Update(InterfazWorkManagement.UpdateRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Update", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_UpdateResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_UpdateAsync(InterfazWorkManagement.UpdateRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Delete", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_DeleteResponse")]
    InterfazWorkManagement.ResponseDto Form_Delete(InterfazWorkManagement.DeleteRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_Delete", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_DeleteResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_DeleteAsync(InterfazWorkManagement.DeleteRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetData", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetDataResponse")]
    InterfazWorkManagement.ResponseDto Form_GetData(InterfazWorkManagement.QueryRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetData", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetDataResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_GetDataAsync(InterfazWorkManagement.QueryRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetFileCodes", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetFileCodesResponse")]
    InterfazWorkManagement.FileResponseDto Form_GetFileCodes(InterfazWorkManagement.FileRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetFileCodes", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetFileCodesResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.FileResponseDto> Form_GetFileCodesAsync(InterfazWorkManagement.FileRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetBase64StringFile", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetBase64StringFileResponse")]
    InterfazWorkManagement.StringFileResponseDto Form_GetBase64StringFile(InterfazWorkManagement.StringFileRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Form_GetBase64StringFile", ReplyAction = "http://tempuri.org/IWorkManagerServices/Form_GetBase64StringFileResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.StringFileResponseDto> Form_GetBase64StringFileAsync(InterfazWorkManagement.StringFileRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_Start", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_StartResponse")]
    InterfazWorkManagement.StartWorkflowResponseDto Workflow_Start(InterfazWorkManagement.StartWorkflowRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_Start", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_StartResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.StartWorkflowResponseDto> Workflow_StartAsync(InterfazWorkManagement.StartWorkflowRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_AttachFiles", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_AttachFilesResponse")]
    InterfazWorkManagement.InsertResponseDto Workflow_AttachFiles(InterfazWorkManagement.WorkflowAttachFilesRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_AttachFiles", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_AttachFilesResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Workflow_AttachFilesAsync(InterfazWorkManagement.WorkflowAttachFilesRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_RunTask", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_RunTaskResponse")]
    InterfazWorkManagement.ResponseDto Workflow_RunTask(InterfazWorkManagement.RunTaskRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/Workflow_RunTask", ReplyAction = "http://tempuri.org/IWorkManagerServices/Workflow_RunTaskResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Workflow_RunTaskAsync(InterfazWorkManagement.RunTaskRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/List_GetItems", ReplyAction = "http://tempuri.org/IWorkManagerServices/List_GetItemsResponse")]
    InterfazWorkManagement.ListResponseDtoOfItemListagpjPdjDr List_GetItems(InterfazWorkManagement.ListRequestDto request);

    [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IWorkManagerServices/List_GetItems", ReplyAction = "http://tempuri.org/IWorkManagerServices/List_GetItemsResponse")]
    System.Threading.Tasks.Task<InterfazWorkManagement.ListResponseDtoOfItemListagpjPdjDr> List_GetItemsAsync(InterfazWorkManagement.ListRequestDto request);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IWorkManagerServicesChannel : IWorkManagerServices, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class WorkManagerServicesClient : System.ServiceModel.ClientBase<IWorkManagerServices>, IWorkManagerServices
{

    public WorkManagerServicesClient()
    {
    }

    public WorkManagerServicesClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
    {
    }

    public WorkManagerServicesClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public WorkManagerServicesClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
    {
    }

    public WorkManagerServicesClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
    {
    }

    public InterfazWorkManagement.InsertResponseDto Form_Insert(InterfazWorkManagement.InsertRequestDto request)
    {
        return base.Channel.Form_Insert(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Form_InsertAsync(InterfazWorkManagement.InsertRequestDto request)
    {
        return base.Channel.Form_InsertAsync(request);
    }

    public InterfazWorkManagement.InsertResponseDto Form_AttachFiles(InterfazWorkManagement.AttachRequestDto request)
    {
        return base.Channel.Form_AttachFiles(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Form_AttachFilesAsync(InterfazWorkManagement.AttachRequestDto request)
    {
        return base.Channel.Form_AttachFilesAsync(request);
    }

    public InterfazWorkManagement.ResponseDto Form_Update(InterfazWorkManagement.UpdateRequestDto request)
    {
        return base.Channel.Form_Update(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_UpdateAsync(InterfazWorkManagement.UpdateRequestDto request)
    {
        return base.Channel.Form_UpdateAsync(request);
    }

    public InterfazWorkManagement.ResponseDto Form_Delete(InterfazWorkManagement.DeleteRequestDto request)
    {
        return base.Channel.Form_Delete(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_DeleteAsync(InterfazWorkManagement.DeleteRequestDto request)
    {
        return base.Channel.Form_DeleteAsync(request);
    }

    public InterfazWorkManagement.ResponseDto Form_GetData(InterfazWorkManagement.QueryRequestDto request)
    {
        return base.Channel.Form_GetData(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Form_GetDataAsync(InterfazWorkManagement.QueryRequestDto request)
    {
        return base.Channel.Form_GetDataAsync(request);
    }

    public InterfazWorkManagement.FileResponseDto Form_GetFileCodes(InterfazWorkManagement.FileRequestDto request)
    {
        return base.Channel.Form_GetFileCodes(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.FileResponseDto> Form_GetFileCodesAsync(InterfazWorkManagement.FileRequestDto request)
    {
        return base.Channel.Form_GetFileCodesAsync(request);
    }

    public InterfazWorkManagement.StringFileResponseDto Form_GetBase64StringFile(InterfazWorkManagement.StringFileRequestDto request)
    {
        return base.Channel.Form_GetBase64StringFile(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.StringFileResponseDto> Form_GetBase64StringFileAsync(InterfazWorkManagement.StringFileRequestDto request)
    {
        return base.Channel.Form_GetBase64StringFileAsync(request);
    }

    public InterfazWorkManagement.StartWorkflowResponseDto Workflow_Start(InterfazWorkManagement.StartWorkflowRequestDto request)
    {
        return base.Channel.Workflow_Start(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.StartWorkflowResponseDto> Workflow_StartAsync(InterfazWorkManagement.StartWorkflowRequestDto request)
    {
        return base.Channel.Workflow_StartAsync(request);
    }

    public InterfazWorkManagement.InsertResponseDto Workflow_AttachFiles(InterfazWorkManagement.WorkflowAttachFilesRequestDto request)
    {
        return base.Channel.Workflow_AttachFiles(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.InsertResponseDto> Workflow_AttachFilesAsync(InterfazWorkManagement.WorkflowAttachFilesRequestDto request)
    {
        return base.Channel.Workflow_AttachFilesAsync(request);
    }

    public InterfazWorkManagement.ResponseDto Workflow_RunTask(InterfazWorkManagement.RunTaskRequestDto request)
    {
        return base.Channel.Workflow_RunTask(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.ResponseDto> Workflow_RunTaskAsync(InterfazWorkManagement.RunTaskRequestDto request)
    {
        return base.Channel.Workflow_RunTaskAsync(request);
    }

    public InterfazWorkManagement.ListResponseDtoOfItemListagpjPdjDr List_GetItems(InterfazWorkManagement.ListRequestDto request)
    {
        return base.Channel.List_GetItems(request);
    }

    public System.Threading.Tasks.Task<InterfazWorkManagement.ListResponseDtoOfItemListagpjPdjDr> List_GetItemsAsync(InterfazWorkManagement.ListRequestDto request)
    {
        return base.Channel.List_GetItemsAsync(request);
    }


    #endregion


}