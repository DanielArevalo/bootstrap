using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xpinn.Util
{
    // Desaparecer numeros magicos Ej: Codigo == 0

    public enum ParametroCorreo { NombreCompletoPersona, Identificacion, NumeroRadicacion, LineaCredito, FechaCredito, MontoCredito, PlazoCredito, DocumentosPendientes, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, RazonSocial, NumeroIcetex, CodConvocatoria, FechaIcetex, MontoIcetex, ObservacionIcetex, NumeroSolicitud, OficinaPago, FechaPago, nombreProducto }

    public enum OperacionCRUD
    {
        Crear,
        Consultar,
        Modificar,
        Borrar
    }

    public enum TipoDeudor
    {
        Deudor,
        Codeudor1,
        Codeudor2,
        Codeudor3
    }

    public enum TienePermisos
    {
        Si,
        No
    }

    public enum TipoActivoFijo
    {
        SinTipoActivoFijoDefinido = 0,
        Inmueble = 'H',
        Vehiculo = 'P'
    }

    public enum TipoNormaComprobante
    {
        Local,
        Niff,
        Local_Niff
    }

    public enum Tiene
    {
        No,
        Si
    }

    public enum TipoReferencia
    {
        SinTipoReferenciaDefinido = 0,
        Familiar = 1,
        Personal = 2,
        Comercial = 3
    }

    public enum ClasificacionCredito
    {
        SinClasificacion = 0,
        Consumo = 1,
        Comercial = 2,
        Vivienda = 3,
        MicroCredito = 4
    }

    public enum TipoProyeccionRiesgoLiquidez
    {
        SinTipo,
        Disponible,
        AhorroPermanente,
        Cartera,
        Aporte
    }

    public enum TipoDocumentoCorreo
    {
        Ninguno = 0,
        CreditoAprobado = 1,
        CreditoNegado = 2,
        CreditoAplazado = 3,
        ControlDocumentos = 4,
        HojaRuta = 5,
        FormatoCumpleaños = 6,
        IcetexAprobado = 7,
        IcetexNegado = 8,
        IcetexAplazado = 9,
        SolicitudCreditoAtencionWeb = 10,
        IcetexAprobadoInscrito = 11,
        IcetexNegadoInscrito = 12,
        IcetexAplazadoInscrito = 13,
        PagosPorVentanilla = 14,
        EstadoCuenta = 15,
        ExtractoAhorros = 16,
        ConfirmaciónProductoWeb = 17,
        RechazoProductoWeb = 18
    }

    public enum TipoLista
    {
        TipoIdentificacion,
        TipoDirectivo,
        LineasCredito,
        LineasCreditoReestructurado,
        Asesor,
        EstadoCivil,
        TipoCargo,
        Ciudades,
        TipoContrato,
        Oficinas,
        Empresa,// Empresa Recaudo
        Entidades,
        NominaEmpleado,
        LineaAhorro,
        TipoComprobante,
        BancosEntidad,
        Bancos,
        CuentaBancariasBancos,
        ConceptoNomina,
        ClasificacionCreditos,
        Barrio,
        Lugares,
        Actividad2,
        NivelEscolaridad,
        Actividad_Negocio,
        ESTADO_ACTIVO,
        Actividad_Laboral,
        Sexo,
        SinTipoLista,
        Segmentos,
        Parentesco,
        CentroCostos,
        FondoSalud,
        FondoPension,
        FondoCesantias,
        FondoARL,
        CajaCompensacion,
        TipoCotizante,
        FormaPago,
        TipoConceptoNomina,
        TipoNovedadPrima,
        TipoRetiroContrato,
        Contratacion,
        Area,
        TipoPersona,
        TipoCliente,
        Jurisdiccion,
        ValoracionJurisdiccion,
        TipoRiesgoArl,
        TipoCuentasXpagar,
        TipoEstadoFinanciero,
        Concepto_Niif
    }

    public enum TipoListaHelpDesk
    {
        Criticidad,
        TipoRequerimiento,
        Encargado,
        TipoProducto,
        Modulo,
        Clientes,
        CriticadesSecundarias
    }

    public enum EstadosHistoricoHelpDesk
    {
        RequerimientoSolicitado,
        RequerimientoAsignado,
        RequerimientoCerrado,
        RequerimientoEnAsignacionPruebas,
        SeguimientoAgregado,
        RequerimientoModificado,
        RequerimientoSolucionado,
        RequerimientoDevueltoIngenieria,
        RequerimientoDevueltoCliente,
        RequerimientoEnPruebas
    }

    public enum EstadoNovedad
    {
        Generadas = 1,
        Aplicadas = 2,
        Anuladas = 3
    }

    public enum Entidades
    {
        PrestadorasDeSalud = 1,
        FondoDePensiones = 2,
        Cesantias = 3,
        Arl = 4
    }

    public enum EstadoRequerimientoHelpDesk
    {
        Solicitado = 0,
        Asignado = 1,
        Desarrollado = 2,
        Cerrado = 3,
        EnPruebas = 4,
        DevueltoIngenieria = 5,
        DevueltoCliente = 6,
        Solucionado = 7,
        SinEstado = 20,
    }

    public enum TipoUsuarioHelpDesk
    {
        Cliente = 0,
        Ingeniero = 1,
        Administrador = 2,
        SinTipoUsuario = 20
    }


    public enum TipoReporteCartera
    {
        SinTipoReporteCarteraDefinido = 0,
        CierreCartera = 1,
        CausacionCartera = 2,
        ProvisionCartera = 3
    }

    public enum FormatoArchivo // No cambiar el case, el case es importante para renderizar un ReportViewer
    {
        Excel,
        WORD,
        PDF,
        IMAGE
    }

    public enum TipoDeProducto
    {
        SinTipoDeProductoDefinido = 0,
        Aporte = 1,
        Credito = 2,
        AhorrosVista = 3,
        Servicios = 4,
        CDATS = 5,
        Afiliacion = 6,
        Otros = 7,
        Devoluciones = 8,
        AhorroProgramado = 9,
        ObligacionesFinancieras = 10,
        ActivosFijos = 11
    }

    public enum TipoDeMovimiento
    {
        SinTipodeMovimiento = 0,
        Debito = 1,
        Credito = 2
    }

    public enum TipoFormaPago
    {
        SinTipoFormaPagoDefinido = 0,
        Caja = 1,
        Nomina = 2
    }

    public enum TipoFormaDesembolso
    {
        SinTipoFormaDesembolsoDefinido = 0,
        Efectivo = 1,
        Cheque = 2,
        Transferencia = 3,
        TranferenciaAhorroVistaInterna = 4,
        Otros = 5,
    }

    public enum TipoBanco
    {
        SinBancoDefinido = 0, //Antes BancodelaRepublica = 0,
        BancoBogota = 1,
        BancoPopular = 2,
        BancoCafetero = 5,
        Itau = 6, //Antes BancoSantander = 6, 
        Bancolombia = 7,
        BancoReal = 8, //Confirmar ROYAL BANK OF SCOTLAND 
        CitiBank = 9,
        BancoGNBColombia = 10, //Antes BancoAngloColombiano = 10,
        CajaCreditoAgrario = 11,
        BancoSudameris = 12,
        Bbva = 13, //Antes BancoGanadero
        HelmBank = 14,
        BancoAndino = 16,
        BNC = 18,
        Colpatria = 19,
        BancoDelEstado = 20,
        BancoUnion = 22,
        BancoOccidente = 23,
        BancoExteBandes = 24,
        BancoOfAmerica = 26,
        BancoMercantil = 28,
        BancoTequendama = 29,
        BanColdex = 31,
        BancoCajaSocial = 32,
        BancoSuperior = 34,
        MegaBanco = 36,
        BankOfBoston = 37,
        BancoAgrario = 40,
        JpMorgan = 41,
        BnpParibasColombia = 42,
        BancoDavivienda = 51,
        BancoAvVillas = 52, //Antes BancoLasVillas = 52,
        BancoGranahorrar = 54,
        BancoConavi = 55,
        BancoColmena = 57,
        BancoProcredit = 58, //Antes FiduciariaSumarBogota = 58,
        Bancamia = 59,
        BancoPichincha = 60, //Antes FinAmerica = 60,
        Bancomeva = 61, //Antes BID = 61,
        BancoFalabella = 62,
        BancoFinandina = 63, //Antes SegurosLaEquidad = 63,
        MultiBank = 64, //Antes CrediFlorez = 64,
        BancoSantanderNegocios = 65,
        BancoCoopcentral = 66, //Antes Cooptenjo
        BanCompartir = 67, //Antes Eclof = 67,
        CooperativaEmprender = 68,
        BancoWMB = 70,
        Coompensar = 83,
        GestionYContacto = 84,
        Asopagos = 86,
        Simple = 88,
        EnlaceOperativo = 89,
        CorfiColombiana = 90,
        FinancieraJuriscoop = 121,
        FinancieraAntioquia = 283,
        Cotrafa = 289,
        Confiar = 292,
        Coltefinanciera = 370
    }

    public enum TipoCuentaBanco
    {
        //SinTipoCuentaDefinido = 0,
        Ahorro = 0,
        Corriente = 1
        //Corriente = 2
    }

    public enum ClienteEnt
    {
        COOACEDED,
        COOPCHIPAQUE,
        COOPECOL,
        COOTREGUA,
        FECEM,
        FONSODI,
        PRUEBAS,
        FONDECOLP,
        FONCALDAS
    }

    public enum TipoAuditoria
    {
        SinTipoAuditoria = 0,
        Afiliaciones = 5,
        CruceDeCuentas = 6,
        Aportes = 7,
        Creditos = 8,
        AhorroVista = 9,
        CDAT = 10,
        AhorroProgramado = 11,
        Servicios = 12,
        Auxilios = 13,
        Comprobante = 14,
        Tesoreria = 15,
        Giros = 16,
        CajaFinanciera = 17,
        RecaudosMasivos = 18,
        GestionRiesgo = 19,
        PagosPSE = 20
    }

    public enum EstadoTarjetaEnpacto
    {
        Pendiente = 0,
        Activa = 1,
        Inactiva = 2,
        Bloqueada = 3
    }

    public enum FormaPagoEnum
    {
        SinFormaPago = 0,
        Efectivo = 1,
        Cheque = 2,
        Transferencia = 3
    }

    public enum ProcesosOffline
    {
        CreacionCaja,
        AperturaCierreCaja,
        CreacionCajero,
        EliminacionCajero,
        AsignacionCajero,
        AperturaCierreOficina,
        RegistroOperacion,
        ConsignacionOperacion,
        ChequeCanje,
        TrasladoDinero,
        RecepcionDinero,
        ReversionOperacion,
        Nulo
    }

    public enum UnidadConceptoNomina
    {
        SinUnidadConcepto = 0,
        Valor = 1,
        Cantidad = 2
    }

    public enum ProcesoAtencionCliente
    {
        ActualizacionDatos,
        ModificacionProducto,
        RegistroAsociado,
        RenovacionCdat,
        RealizacionPagoACH,
        SolicitudAfiliacion,
        SolicitudCredito,
        SolicitudCruceCooperativa,
        RecuperarClave,
        SolicitudRetiroAsociado,
        SolicitudAhorroProgramado,
        SolicitudAhorros,
        SolicitudCDAT,
        SolicitudServicio,
        SolicitudRetiroAhorros,
        SolicitudCambioCuota
    }


}
