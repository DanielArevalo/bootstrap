using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para OpcionesCredito
    /// </summary>
    public class  OpcionesCreditoBusiness : GlobalBusiness
    {
        private OpcionesCreditoData DAOpcionesCredito;

        /// <summary>
        /// Constructor del objeto de negocio para OpcionesCredito
        /// </summary>
        public OpcionesCreditoBusiness()
        {
            DAOpcionesCredito = new OpcionesCreditoData();
        }

        /// <summary>
        /// Crea un OpcionesCredito
        /// </summary>
        /// <param name="pOpcionesCredito">Entidad OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito creada</returns>
        public OpcionesCredito CrearOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOpcionesCredito = DAOpcionesCredito.CrearOpcionesCredito(pOpcionesCredito, pUsuario);

                    ts.Complete();
                }

                return pOpcionesCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoBusiness", "CrearOpcionesCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un OpcionesCredito
        /// </summary>
        /// <param name="pOpcionesCredito">Entidad OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito modificada</returns>
        public OpcionesCredito ModificarOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOpcionesCredito = DAOpcionesCredito.ModificarOpcionesCredito(pOpcionesCredito, pUsuario);

                    ts.Complete();
                }

                return pOpcionesCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoBusiness", "ModificarOpcionesCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un OpcionesCredito
        /// </summary>
        /// <param name="pId">Identificador de OpcionesCredito</param>
        public void EliminarOpcionesCredito(string pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAOpcionesCredito.EliminarOpcionesCredito(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoBusiness", "EliminarOpcionesCredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un OpcionesCredito
        /// </summary>
        /// <param name="pId">Identificador de OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito</returns>
        public OpcionesCredito ConsultarOpcionesCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return DAOpcionesCredito.ConsultarOpcionesCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoBusiness", "ConsultarOpcionesCredito", ex);
                return null;
            }
        }

        public List<OpcionesCredito> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return DAOpcionesCredito.ListarOpciones(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ListarOpciones", ex);
                return null;
            }
        }


        public List<OpcionesCredito> ListarOpcionesModulo(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return DAOpcionesCredito.ListarOpcionesModulo(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilBusiness", "ListarOpcionesModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pModulo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<OpcionesCredito> ListarModulo(OpcionesCredito pModulo, Usuario pUsuario)
        {
            try
            {
                return DAOpcionesCredito.ListarModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloBusiness", "ListarModulo", ex);
                return null;
            }
        }


   

    }
}