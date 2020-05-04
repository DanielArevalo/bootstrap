using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para HorarioOficina
    /// </summary>
    public class HorarioOficinaBusiness:GlobalData
    {
        
        private HorarioOficinaData DAHorarioOficina;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public HorarioOficinaBusiness()
        {
            DAHorarioOficina = new HorarioOficinaData();
        }

        /// <summary>
        /// Crea una Horario de Oficina
        /// </summary>
        /// <param name="pEntity">Entidad HorarioOficina</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.HorarioOficina CrearHorarioOficina(Xpinn.Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHorario = DAHorarioOficina.InsertarHorarioOficina(pHorario, pUsuario);

                    ts.Complete();
                }

                return pHorario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "CrearHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Horario de Oficina
        /// </summary>
        /// <param name="pEntity">Entidad Horario Oficina</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.HorarioOficina ModificarHorarioOficina(Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHorario = DAHorarioOficina.ModificarHorarioOficina(pHorario, pUsuario);

                    ts.Complete();
                }

                return pHorario;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "ModificarHorarioOficina", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador del Horario Oficina</param>
        public void EliminarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAHorarioOficina.EliminarHorarioOficina(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "EliminarHorarioOficina", ex);
            }
        }

        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario consultado</returns>
        public Caja.Entities.HorarioOficina ConsultarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.ConsultarHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "ConsultarHorarioOficina", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario consultado</returns>
        public Caja.Entities.HorarioOficina VerificarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.VerificarHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "VerificarHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Horario Oficina
        /// </summary>
        /// <param name="pId">identificador de Horario Oficina</param>
        /// <returns>Horario consultado</returns>
        public Caja.Entities.HorarioOficina getHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.getHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "getHorarioOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Horario Oficina dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Horarios obtenidos</returns>
        public List<Caja.Entities.HorarioOficina> ListarHorarioOficina(Caja.Entities.HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.ListarHorarioOficina(pHorario, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioOficinaBusiness", "ListarHorarioOficina", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de TipoOperacion-Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CajeroXCaja - Caja obtenidos</returns>
        public HorarioOficina ConsultarHorarioXOficina(HorarioOficina pHorario, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.ConsultarHorarioXOficina(pHorario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioBusiness", "ConsultarHorarioXOficina", ex);
                return null;
            }
        }

        public string getDiaHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAHorarioOficina.getDiaHorarioOficina(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorarioBusiness", "getDiaHorarioOficina", ex);
                return null;
            }
        }

    }
}
