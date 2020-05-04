using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ObligacionCreditoService
    {
        private ObligacionCreditoBusiness BOObligacionCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ObligacionCredito
        /// </summary>
        public ObligacionCreditoService()
        {
            BOObligacionCredito = new ObligacionCreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "130102"; } }
        public string CodigoPrograma2 { get { return "130103"; } }
        public string CodigoPrograma3 { get { return "130104"; } }
        public string CodigoPrograma4 { get { return "130105"; } }
        public string CodigoPrograma5 { get { return "130106"; } }
        public string CodigoPrograma6 { get { return "130107"; } }
        public string CodigoPrograma7 { get { return "130108"; } }

        /// <summary>
        /// Servicio para crear ObligacionCredito
        /// </summary>
        /// <param name="pEntity">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito creada</returns>
        public ObligacionCredito CrearObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.CrearObligacionCredito(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "CrearObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ObligacionCredito
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito modificada</returns>
        public ObligacionCredito ModificarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ModificarObligacionCredito(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ModificarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ObligacionCredito
        /// </summary>
        /// <param name="pId">identificador de ObligacionCredito</param>
        public void EliminarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOObligacionCredito.EliminarObligacionCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarObligacionCredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ObligacionCredito
        /// </summary>
        /// <param name="pId">identificador de ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito</returns>
        public ObligacionCredito ConsultarObligacionCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ConsultarObligacionCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ConsultarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ObligacionCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarObligacionCredito(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionCredito", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarObligaciones(String filtro, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarObligaciones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligaciones", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarProvicionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOObligacionCredito.ListarProvicionCredito(pObligacionCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionCredito", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarProvisionFechas(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarProvisionFechas(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para guardar los datos de provisión del crédito en la base de datos
        /// </summary>
        /// <param name="pObligacionCredito"></param>
        /// <param name="datos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public ObligacionCredito ModificarProvision(ObligacionCredito pObligacionCredito, GridView datos, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ModificarProvision(pObligacionCredito, datos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ObligacionCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Pendiente por pagar obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionPendPagar(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarObligacionPendPagar(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionPendPagar", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de ObligacionCreditos Vencidos a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito vencidos obtenidos</returns>
        public List<ObligacionCredito> ListarObligacionCreditoVencido(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarObligacionCreditoVencido(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarObligacionCreditoVencido", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de ObligacionCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Datos Solicitud obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitud(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarDatosSolicitud(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarDatosSolicitud", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ObligacionCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Datos Solicitud obtenidos</returns>
        public List<ObligacionCredito> ListarDatosSolicitudAprobacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarDatosSolicitudAprobacion(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarDatosSolicitudAprobacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Movs de Obligaciones a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Datos Solicitud obtenidos</returns>
        public List<ObligacionCredito> ListarPlanObligacion(ObligacionCredito pObligacionCredito, decimal monto, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarPlanObligacion(pObligacionCredito, monto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarPlanObligacion", ex);
                return null;
            }
        }

        public List<ObligacionCredito> ListarMovsObligacion(ObligacionCredito pObligacionCredito, decimal monto, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarMovsObligacion(pObligacionCredito, monto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarMovsObligacion", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de Movs de Obligaciones a partir de unos filtros
        /// </summary>
        /// <param name="pObligacionCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ObligacionCredito Datos Solicitud obtenidos</returns>
        public List<ObligacionCredito> ListarDistribPagosPendCuotas(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarDistribPagosPendCuotas(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarDistribPagosPendCuotas", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear ObligacionCredito
        /// </summary>
        /// <param name="pEntity">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito creada</returns>
        public ObligacionCredito CrearTransacOpePagoOb(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.CrearTransacOpePagoOb(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "CrearTransacOpePagoOb", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<ObligacionCredito> ListarOperaciones(ObligacionCredito pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ListarOperaciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ListarOperaciones", ex);
                return null;
            }
        }


        public ObligacionCredito ProvisionCredito(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        {
            try
            {
                return BOObligacionCredito.ProvisionCredito(pObligacionCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObligacionCreditoService", "ProvisionCredito", ex);
                return null;
            }
        }

        //public ObligacionCredito AnularOperacion(ObligacionCredito pObligacionCredito, Usuario pUsuario)
        //{
        //    try
        //    {
        //        return BOObligacionCredito.AnularOperacion(pObligacionCredito, pUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        BOExcepcion.Throw("ObligacionCreditoService", "AnularOperacion", ex);
        //        return null;
        //    }
        //}

    }
}