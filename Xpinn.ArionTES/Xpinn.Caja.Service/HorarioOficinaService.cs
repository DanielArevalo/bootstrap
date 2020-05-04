using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Horario Oficina
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HorarioOficinaService
    {
        private HorarioOficinaBusiness BOHorarioOficina;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para HorarioOficina
        /// </summary>
        public HorarioOficinaService()
        {
            BOHorarioOficina = new HorarioOficinaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoHorarioOficina;
        public string CodigoPrograma { get { return "120206"; } }

        /// <summary>
        /// Crea un HorarioOficina
        /// </summary>
        /// <param name="pEntity">Entidad HorarioOficina</param>
        /// <returns>Entidad creada</returns>
        public Caja.Entities.HorarioOficina CrearHorarioOficina(Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.CrearHorarioOficina(pHorario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "CrearHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Horario de Oficina
        /// </summary>
        /// <param name="pEntity">Entidad HorarioOficina</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.HorarioOficina ModificarHorarioOficina(Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.ModificarHorarioOficina(pHorario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "ModificarHorarioOficina", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Caja
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        public void EliminarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOHorarioOficina.EliminarHorarioOficina(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "EliminarHorarioOficina", ex);
            }
        }

        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina ConsultarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.ConsultarHorarioOficina(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "ConsultarHorarioOficina", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina getHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.getHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "getHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina VerificarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.VerificarHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "VerificarHorarioOficina", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Cajas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.HorarioOficina> ListarHorarioOficina(Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.ListarHorarioOficina(pHorario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "ListarHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un ConsultarCajeroXCaja
        /// </summary>
        /// <param name="pId">identificador del ConsultarCajeroXCaja</param>
        /// <returns>ConsultarCajeroXCaja consultada</returns>
        public HorarioOficina ConsultarHorarioXOficina(HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.ConsultarHorarioXOficina(pHorario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "ConsultarHorarioXOficina", ex);
                return null;
            }

        }

        public string getDiaHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOHorarioOficina.getDiaHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaService", "getDiaHorarioOficina", ex);
                return null;
            }

        }
        

    }
}
