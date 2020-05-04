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
    public class TipoCupoService
    {
        private TipoCupoBusiness BOTipoCupo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public TipoCupoService()
        {
            BOTipoCupo = new TipoCupoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100215"; } }

        /// <summary>
        /// Servicio para crear tipo de tasa
        /// </summary>
        /// <param name="pEntity">Entidad tipo tasa</param>
        /// <returns>Entidad tipo tasa creada</returns>
        public TipoCupo CrearTipoCupo(TipoCupo vTipoCupo, Usuario pUsuario)
        {
            try
            {
                return BOTipoCupo.CrearTipoCupo(vTipoCupo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoService", "CrearTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa</param>
        /// <returns>Entidad tipo de tasa modificada</returns>
        public TipoCupo ModificarTipoCupo(TipoCupo vTipoCupo, Usuario pUsuario)
        {
            try
            {
                return BOTipoCupo.ModificarTipoCupo(vTipoCupo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoService", "ModificarTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoCupo
        /// </summary>
        /// <param name="pId">identificador de TipoCupo</param>
        public void EliminarTipoCupo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoCupo.EliminarTipoCupo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoCupo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoCupo
        /// </summary>
        /// <param name="pId">identificador de TipoCupo</param>
        /// <returns>Entidad TipoCupo</returns>
        public TipoCupo ConsultarTipoCupo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoCupo.ConsultarTipoCupo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoService", "ConsultarTipoCupo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoCupo a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoCupo obtenidos</returns>
        public List<TipoCupo> ListarTipoCupo(TipoCupo vTipoCupo, Usuario pUsuario)
        {
            try
            {
                return BOTipoCupo.ListarTipoCupo(vTipoCupo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoService", "ListarTipoCupo", ex);
                return null;
            }
        }

        public TipoCupo ConsultarTipoCupo(TipoCupo pTipoCupo, Usuario pUsuario)
        {
            try
            {
                TipoCupo TipoCupo = new TipoCupo();

                TipoCupo = BOTipoCupo.ConsultarTipoCupo(pTipoCupo, pUsuario);

                return TipoCupo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DetTipoCupo> ListarDetTipoCupo(int pTipoCupo, Usuario vUsuario)
        {
            try
            {
                return BOTipoCupo.ListarDetTipoCupo(pTipoCupo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCupoBusiness", "ListarDetTipoCupo", ex);
                return null;
            }
        }

        public Int64 CrearDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario pusuario)
        {
            try
            {
                return BOTipoCupo.CrearDetTipoCupo(pDetTipoCupo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoService", "CrearDetTipoCupo", ex);
                return 0;
            }
        }


        public DetTipoCupo ModificarDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario pusuario)
        {
            try
            {
                pDetTipoCupo = BOTipoCupo.ModificarDetTipoCupo(pDetTipoCupo, pusuario);
                return pDetTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoService", "ModificarDetTipoCupo", ex);
                return null;
            }
        }


        public void EliminarDetTipoCupo(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOTipoCupo.EliminarDetTipoCupo(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoService", "EliminarDetTipoCupo", ex);
            }
        }


        public DetTipoCupo ConsultarDetTipoCupo(Int64 pId, Usuario pusuario)
        {
            try
            {
                DetTipoCupo DetTipoCupo = new DetTipoCupo();
                DetTipoCupo = BOTipoCupo.ConsultarDetTipoCupo(pId, pusuario);
                return DetTipoCupo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetTipoCupoService", "ConsultarDetTipoCupo", ex);
                return null;
            }
        }




    }
}