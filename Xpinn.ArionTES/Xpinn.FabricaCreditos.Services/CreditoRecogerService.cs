using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreditoRecogerService
    {
        private CreditoRecogerBusiness BOCreditoRecoger;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CreditoRecoger
        /// </summary>
        public CreditoRecogerService()
        {
            BOCreditoRecoger = new CreditoRecogerBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100140"; } }

        /// <summary>
        /// Servicio para crear CreditoRecoger
        /// </summary>
        /// <param name="pEntity">Entidad CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger creada</returns>
        public CreditoRecoger CrearCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.CrearCreditoRecoger(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "CrearCreditoRecoger", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar CreditoRecoger
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger modificada</returns>
        public CreditoRecoger ModificarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ModificarCreditoRecoger(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ModificarCreditoRecoger", ex);
                return null;
            }
        }



        public List<CreditoRecoger> ListarCreditoARecoger(string cod, DateTime pfechacalculo, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ListarCreditoARecoger(cod, pfechacalculo, pUsuario);
            }
            catch (Exception ex)
            {                
                BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
                return null;
            }
        }

        public List<CreditoRecoger> ListarCreditoARecoger(string cod, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ListarCreditoARecoger(cod, DateTime.Now, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
                return null;
            }
        }

        public int ConsultaPermisoModificarTasa(string cod, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ConsultaPermisoModificarTasa(cod, pUsuario);
            }
            catch (Exception ex)
            {                
                BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
                return 0;
            }
        }
        /// <summary>
        /// Servicio para Eliminar CreditoRecoger
        /// </summary>
        /// <param name="pId">identificador de CreditoRecoger</param>
        public void EliminarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCreditoRecoger.EliminarCreditoRecoger(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CreditoRecoger
        /// </summary>
        /// <param name="pId">identificador de CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger</returns>
        public List<CreditoRecoger> ConsultarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ConsultarCreditoRecoger(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ConsultarCreditoRecoger", ex);
                return null;
            }
        }



        public List<CreditoRecoger> Consultarterminosfijos(string radicacion, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.Consultarterminosfijos(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ConsultarCreditoRecoger", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de CreditoRecogers a partir de unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ListarCreditoRecoger(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ListarCreditoRecoger", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de CreditoRecogers a partir de unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecogerSolicitud(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return BOCreditoRecoger.ListarCreditoRecogerSolicitud(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ListarCreditoRecogerSolicitud", ex);
                return null;
            }
        }

        public decimal ConsultarValorNoCapitalizado(string numeroRadicacion, Usuario usuario)
        {
            try
            {
                return BOCreditoRecoger.ConsultarValorNoCapitalizado(numeroRadicacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ConsultarValorNoCapitalizado", ex);
                return 0;
            }
        }
    }



}