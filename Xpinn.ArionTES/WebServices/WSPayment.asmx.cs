using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Services;
using Xpinn.Tesoreria.Services;
using Xpinn.Util;

namespace WebServices
{
    /// <summary>
    /// Summary description for WSPayment
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSPayment : System.Web.Services.WebService
    {

        [WebMethod(Description = "Creación de Transacción Pago ACH.")]
        public Xpinn.Tesoreria.Entities.PaymentACH CreatePaymentTransaction(Xpinn.Tesoreria.Entities.PaymentACH pPaymentRequest, string sec)
        {
            Xpinn.Tesoreria.Entities.PaymentACH pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
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
                PaymentACHServices ServicePayment = new PaymentACHServices();
                pPayment = ServicePayment.CreatePaymentACHServices(pPaymentRequest, usuario);
            }
            catch (Exception ex)
            {
                pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }


        [WebMethod(Description = "Modificación de Transacción Pago ACH.")]
        public Xpinn.Tesoreria.Entities.PaymentACH UpdatePaymentTransaction(Xpinn.Tesoreria.Entities.PaymentACH pPaymentRequest, string sec)
        {
            Xpinn.Tesoreria.Entities.PaymentACH pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
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
                Xpinn.Seguridad.Services.UsuarioService ServicesUsuario = new Xpinn.Seguridad.Services.UsuarioService();
                Usuario pUsuDefault = ServicesUsuario.ConsultarUsuario(Convert.ToInt64(pCodUsuario), usuario);
                if (pUsuDefault == null)
                {
                    pPayment.Success = false;
                    pPayment.ErrorMessage = "Error al consultar al usuario por defecto.";
                    return pPayment;
                }
                usuario.IP = "0.0.0.0";
                usuario.codusuario = pUsuDefault.codusuario;
                usuario.nombre = pUsuDefault.nombre;
                usuario.cod_oficina = pUsuDefault.cod_oficina;

                // GENERANDO CONSUMO DE METODO
                PaymentACHServices ServicePayment = new PaymentACHServices();
                pPayment = ServicePayment.ModifyPaymentACHServices(pPaymentRequest, usuario);
            }
            catch (Exception ex)
            {
                pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }


        [WebMethod(Description = "Consulta Transacción de Pago ACH.")]
        public Xpinn.Tesoreria.Entities.PaymentACH ConsultPaymentTransaction(string pPaymentFilter, string sec)
        {
            Xpinn.Tesoreria.Entities.PaymentACH pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
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
                PaymentACHServices ServicePayment = new PaymentACHServices();
                pPayment = ServicePayment.ConsultPaymentACHServices(pPaymentFilter, ref pError, usuario);

                if (!string.IsNullOrEmpty(pError))
                {
                    pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
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
                pPayment = new Xpinn.Tesoreria.Entities.PaymentACH();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }


        [WebMethod(Description = "Lista de Transacciones Pagos ACH.")]
        public Xpinn.Tesoreria.Entities.PaymentACHResult ListPaymentTransaction(string pPaymentFilter, string sec)
        {
            Xpinn.Tesoreria.Entities.PaymentACHResult pPayment = new Xpinn.Tesoreria.Entities.PaymentACHResult();
            List<Xpinn.Tesoreria.Entities.PaymentACH> lstPayments = null;
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
                PaymentACHServices ServicePayment = new PaymentACHServices();
                lstPayments = ServicePayment.ListPaymentACHServices(pPaymentFilter, ref pError, usuario);

                if (!string.IsNullOrEmpty(pError))
                {
                    pPayment = new Xpinn.Tesoreria.Entities.PaymentACHResult();
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
                pPayment = new Xpinn.Tesoreria.Entities.PaymentACHResult();
                pPayment.Success = false;
                pPayment.ErrorMessage = ex.Message;
            }
            return pPayment;
        }


    }
}
