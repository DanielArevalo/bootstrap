using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Services;
using System.Configuration;

namespace WebServices
{
    /// <summary>
    /// Descripción breve de WSintegracion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WSintegracion : System.Web.Services.WebService
    {
        #region METODOS IMONEY

        [WebMethod(Description = "Obtiene la lista de operadores disponibles")]
        public List<Operators> getOperators(string sec)
        {
            List<Operators> lst = new List<Operators>();
            Xpinn.Integracion.Services.ImoneyService iService = new Xpinn.Integracion.Services.ImoneyService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = iService.getOperators(usuario);
            return lst;
        }

        [WebMethod(Description = "Obtiene la lista de paquetes de un operador")]
        public List<Package> getPackages(string id_operator, string sec)
        {
            List<Package> lst = new List<Package>();
            Xpinn.Integracion.Services.ImoneyService iService = new Xpinn.Integracion.Services.ImoneyService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = iService.getPackages(id_operator, usuario);
            return lst;
        }

        [WebMethod(Description = "Solicita la creación de una transacción de recarga")]
        public Fullmovil createFullMovilTransaction(Fullmovil transact, string sec)
        {
            Fullmovil full = new Fullmovil();
            Xpinn.Integracion.Services.ImoneyService iService = new Xpinn.Integracion.Services.ImoneyService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            full = iService.createFullMovilTransaction(transact, usuario);
            return full;
        }

        [WebMethod(Description = "Lista las transacciones de una persona")]
        public List<Fullmovil> ListFullMovilTransaction(string cod_persona, string sec)
        {
            List<Fullmovil> lst = new List<Fullmovil>();
            Xpinn.Integracion.Services.ImoneyService iService = new Xpinn.Integracion.Services.ImoneyService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = iService.getFulltransactionList(cod_persona, usuario);
            return lst;
        }
        #endregion

        #region METODOS MONEDERO
        /// <summary>
        /// crea o activa el monedero
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        [WebMethod(Description = "crea o activa el monedero para la persona")]
        public Monedero crearMonedero(int cod_persona, string sec)
        {
            Monedero mon = new Monedero();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            mon = monService.crearMonedero(cod_persona, usuario);
            return mon;
        }

        /// <summary>
        /// consulta monedero
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        [WebMethod(Description = "crea o activa el monedero para la persona")]
        public Monedero consultarMonedero(int cod_persona, string sec)
        {
            Monedero mon = new Monedero();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            mon = monService.consultarMonedero(cod_persona, usuario);
            return mon;
        }

        /// <summary>
        /// consulta monedero destino
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        [WebMethod(Description = "consulta monedero de destino")]
        public PersonaMonedero consultarPersonaMonedero(string identificacion, string sec)
        {
            PersonaMonedero mon = new PersonaMonedero();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            mon = monService.consultarPersonaMonedero(identificacion, usuario);
            return mon;
        }

        /// <summary>
        /// Consultar Operaciones
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        [WebMethod(Description = "Lista operaciones disponibles")]
        public List<Operaciones> consultarOperaciones(string sec)
        {
            List<Operaciones> lst = new List<Operaciones>();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = monService.consultarOperaciones(true, usuario);
            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cod_persona"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        [WebMethod(Description = "Lista productos de origen para recargar monedero")]
        public List<ProductoOrigen> consultarProductosOrigen(long cod_persona, string sec)
        {
            List<ProductoOrigen> lst = new List<ProductoOrigen>();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = monService.consultarProductosOrigen(cod_persona, usuario);
            return lst;
        }

        [WebMethod(Description = "Lista productos de origen para recargar monedero")]
        public TranMonedero guardarTransaccionMonedero(TranMonedero transac, string sec)
        {
            TranMonedero tran = new TranMonedero();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            tran = monService.guardarTransaccionMonedero(transac, usuario);
            return tran;
        }

        [WebMethod(Description = "Lista productos de origen para recargar monedero")]
        public List<TranMonedero> listarTranMonederoPersona(string cod_persona, string sec)
        {
            List<TranMonedero> lst = new List<TranMonedero>();
            Xpinn.Integracion.Services.MonederoService monService = new Xpinn.Integracion.Services.MonederoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            lst = monService.listarTranMonederoPersona(cod_persona, usuario);
            return lst;
        }
        #endregion

        #region METODOS INTEGRACION
        [WebMethod(Description = "Consulta convenio")]
        public Integracion consultarConvenioIntegracion(int id, string sec)
        {
            Integracion integra = new Integracion();
            Xpinn.Integracion.Services.IntegracionService intService = new Xpinn.Integracion.Services.IntegracionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            integra = intService.consultarConvenioIntegracion(id, usuario);
            return integra;
        }
        #endregion

        #region ACH - PSE

        [WebMethod(Description = "Creación de Transacción Pago ACH.")]
        public ACHPayment CreatePaymentTransaction(ACHPayment pPaymentRequest, string sec)
        {
            ACHPayment pPayment = new ACHPayment();
            try
            {
                if (pPaymentRequest == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "No se realizó el envío de manera correcta, verifique los datos.";
                    return pPayment;
                }
                // GENERANDO VALIDACION DE CONEXION
                Conexion conexion = new Conexion();
                Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "Error en conexión, consulte con el administrador del sistema sobre este caso";
                    return pPayment;
                }
                // GENERANDO CONSUMO DE METODO
                ACHServices ServicePayment = new ACHServices();
                pPayment = ServicePayment.CreatePaymentACHServices(pPaymentRequest, usuario);
            }
            catch (Exception ex)
            {
                pPayment = new ACHPayment();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }

        [WebMethod(Description = "Actualiza transacciones de Pagos ACH.")]
        public bool UpdatePaymentsACH(long cod_persona, string sec)
        {
            List<Operators> lst = new List<Operators>();
            Xpinn.Integracion.Services.ACHServices iService = new Xpinn.Integracion.Services.ACHServices();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return false;

            return iService.UpdatePaymentsACH(cod_persona, usuario);
        }

        [WebMethod(Description = "Consulta Transacción de Pago ACH.")]
        public ACHPayment ConsultPaymentTransaction(string pPaymentFilter, string sec)
        {
            ACHPayment pPayment = new ACHPayment();
            try
            {
                // GENERANDO VALIDACION DE CONEXION
                Conexion conexion = new Conexion();
                Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "Error en conexión, consulte con el administrador del sistema sobre este caso";
                    return pPayment;
                }

                string pError = string.Empty;
                // GENERANDO CONSUMO DE METODO
                ACHServices ServicePayment = new ACHServices();
                pPayment = ServicePayment.ConsultPaymentACHServices(pPaymentFilter, ref pError, usuario);

                if (!string.IsNullOrEmpty(pError))
                {
                    pPayment = new ACHPayment();
                    pPayment.Success = false;
                    pPayment.ErrorMessage = pError;
                }
                else
                {
                    if (pPayment.ID > 0)
                        pPayment.Success = true;
                }
            }
            catch (Exception ex)
            {
                pPayment = new ACHPayment();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }

        [WebMethod(Description = "Lista de Transacciones Pagos ACH.")]
        public PaymentACHResult ListPaymentTransaction(string pPaymentFilter, string sec)
        {
            PaymentACHResult pPayment = new PaymentACHResult();
            List<ACHPayment> lstPayments = null;
            try
            {
                // GENERANDO VALIDACION DE CONEXION
                Conexion conexion = new Conexion();
                Usuario usuario = conexion.DeterminarUsuarioOficina(sec);
                if (usuario == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "Error en conexión, consulte con el administrador del sistema sobre este caso";
                    return pPayment;
                }

                // CONSULTAR USUARIO CONFIGURADO POR DEFECTO
                if (ConfigurationManager.AppSettings["CodUsuario"] == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "No se configuró el usuario por defecto.";
                    return pPayment;
                }
                string pCodUsuario = ConfigurationManager.AppSettings["CodUsuario"];
                if (string.IsNullOrEmpty(pCodUsuario))
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "No se configuró el usuario por defecto.";
                    return pPayment;
                }

                string pError = string.Empty;
                // GENERANDO CONSUMO DE METODO
                ACHServices ServicePayment = new ACHServices();
                lstPayments = ServicePayment.ListPaymentACHServices(pPaymentFilter, ref pError, usuario);

                if (!string.IsNullOrEmpty(pError))
                {
                    pPayment = new PaymentACHResult();
                    pPayment.Success = false;
                    pPayment.ErrorMessage = pError;
                }
                else
                {
                    pPayment.Success = true;
                    pPayment.ErrorMessage = string.Empty;
                    pPayment.PaymentList = lstPayments;
                }
            }
            catch (Exception ex)
            {
                pPayment = new PaymentACHResult();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }

        #endregion

        #region PQR
        [WebMethod(Description = "Crea petición queja o reclamo.")]
        public int crearPQR(PQR peticion, string sec)
        {
            List<Operators> lst = new List<Operators>();
            PqrService pqrService = new PqrService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            return pqrService.crearPQR(peticion, usuario);
        }

        [WebMethod(Description = "Actualiza petición queja o reclamo.")]
        public int actualizarPQR(PQR peticion, string sec)
        {
            List<Operators> lst = new List<Operators>();
            PqrService pqrService = new PqrService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;

            return pqrService.actualizarPQR(peticion, usuario);
        }

        [WebMethod(Description = "Obtiene datos de un pqr con seguimiento.")]
        public PQR obtenerPQR(int id_pqr, string sec)
        {
            List<Operators> lst = new List<Operators>();
            PqrService pqrService = new PqrService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return pqrService.obtenerPQR(id_pqr, usuario);
        }

        [WebMethod(Description = "Lista PQR según filtro")]
        public List<PQR> listarPQR(string filtro, string sec)
        {
            List<Operators> lst = new List<Operators>();
            PqrService pqrService = new PqrService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return pqrService.listarPQR(filtro, usuario);
        }


        #endregion

        #region METODOS NOTIFICACION
        [WebMethod(Description = "Envía clave dinamica y retorna su valor.")]
        public Notificacion enviarClaveDinamica(Notificacion noti, string sec)
        {
            NotificacionService notiService = new NotificacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return notiService.enviarClaveDinamica(noti, usuario);
        }

        [WebMethod(Description = "Envía notificaciones por sms y email.")]
        public Notificacion enviarNotificaciones(Notificacion noti, string sec)
        {
            NotificacionService notiService = new NotificacionService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return notiService.enviarNotificaciones(noti, usuario);
        }
        #endregion

        #region METODOS PAGO INTERNO
        [WebMethod(Description = "Lista productos por pagar con valor y fecha")]
        public List<ProductoPorPagar> listarProductosPorPagar(long cod_persona, string filtro, string sec)
        {
            List<ProductoPorPagar> lst = new List<ProductoPorPagar>();
            PagoInternoService pagoService = new PagoInternoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return pagoService.listarProductosPorPagar(cod_persona, filtro, usuario);
        }

        [WebMethod(Description = "Lista productos por pagar con valor y fecha")]
        public List<ProductoOrigenPago> listarProductoOrigenPago(long cod_persona, string filtro, string sec)
        {
            List<ProductoOrigenPago> lst = new List<ProductoOrigenPago>();
            PagoInternoService pagoService = new PagoInternoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return null;

            return pagoService.listarProductoOrigenPago(cod_persona, filtro, usuario);
        }

        [WebMethod(Description = "procesar Transaccion")]
        public Int32 procesarPagoInterno(PagoInterno pago, string sec)
        {
            PagoInternoService pagoService = new PagoInternoService();
            Conexion conexion = new Conexion();
            Usuario usuario = new Usuario();
            usuario = conexion.DeterminarUsuarioOficina(sec);
            if (usuario == null)
                return 0;
            return pagoService.procesarPagoInterno(pago, usuario);
        }

        #endregion
    }
}
