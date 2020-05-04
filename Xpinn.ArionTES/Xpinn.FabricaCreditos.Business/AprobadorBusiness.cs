using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class AprobadorBusiness : GlobalData
    {
        private AprobadorData DAAprobador;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public AprobadorBusiness()
        {
            DAAprobador = new AprobadorData();
        }

        /// <summary>
        /// Crea un aprobador
        /// </summary>
        /// <param name="pEntity">Entidad aprobador</param>
        /// <returns>Entidad creada</returns>
        public Aprobador CrearAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAprobador = DAAprobador.InsertarAprobador(pAprobador, pUsuario);
                    ts.Complete();
                }
                return pAprobador;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "CrearAprobador", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ListarAprobador(pAprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ListarAprobador", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActa(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ListarAprobadorActa(pAprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ListarAprobadorActa", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActaRestructurados(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ListarAprobadorActaRestructurados(pAprobador, pUsuario);
            
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ListarAprobadorActaRestructurados", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina un Aprobador
        /// </summary>
        /// <param name="pId">identificador del aprobador</param>
        public void EliminarAprobador(Int32 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAAprobador.EliminarAprobador(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Modifica un Aprobador
        /// </summary>
        /// <param name="pEntity">Entidad Aprobador</param>
        /// <returns>Entidad modificada</returns>
        public Aprobador ModificarAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAprobador = DAAprobador.ModificarAprobador(pAprobador, pUsuario);

                    ts.Complete();
                }

                return pAprobador;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ModificarAprobador", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene los datos de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public Aprobador ConsultarAprobador(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ConsultarAprobador(pAprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ConsultarAprobador", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene los datos de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public Aprobador ConsultarAprobadorActa(Aprobador pAprobador, Usuario pUsuario)
        {
            try
            {
                return DAAprobador.ConsultarAprobadorActa(pAprobador, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "ConsultarAprobadorActa", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el ultimo Id de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>IdAprobador obtenido</returns>
        public object UltimoIdAprobador(Usuario pUsuario)
        {
            try
            {
                return DAAprobador.UltimoIdAprobador(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobadorBusiness", "UltimoIdAprobador", ex);
                return null;
            }
        }
    }
}
