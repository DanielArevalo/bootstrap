using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    /// <summary>
    /// Objeto de negocio para Atributo
    /// </summary>
    public class AtributoBusiness : GlobalBusiness
    {
        private AtributoData DAAtributo;

        /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public AtributoBusiness()
        {
            DAAtributo = new AtributoData();
        }

        /// <summary>
        /// Crea un Atributo
        /// </summary>
        /// <param name="pAtributo">Entidad Atributo</param>
        /// <returns>Entidad Atributo creada</returns>
        public Atributo CrearAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAtributo = DAAtributo.CrearAtributo(pAtributo, pUsuario);

                    ts.Complete();
                }

                return pAtributo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoBusiness", "CrearAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Atributo
        /// </summary>
        /// <param name="pAtributo">Entidad Atributo</param>
        /// <returns>Entidad Atributo modificada</returns>
        public Atributo ModificarAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAtributo = DAAtributo.ModificarAtributo(pAtributo, pUsuario);

                    ts.Complete();
                }

                return pAtributo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoBusiness", "ModificarAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Atributo
        /// </summary>
        /// <param name="pId">Identificador de Atributo</param>
        public void EliminarAtributo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAtributo.EliminarAtributo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoBusiness", "EliminarAtributo", ex);
            }
        }

        /// <summary>
        /// Obtiene un Atributo
        /// </summary>
        /// <param name="pId">Identificador de Atributo</param>
        /// <returns>Entidad Atributo</returns>
        public Atributo ConsultarAtributo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAAtributo.ConsultarAtributo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoBusiness", "ConsultarAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAtributo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Atributo obtenidos</returns>
        public List<Atributo> ListarAtributo(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return DAAtributo.ListarAtributo(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoBusiness", "ListarAtributo", ex);
                return null;
            }
        }

    }
}