using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Obligaciones.Business
{
    public class ComponenteBusiness : GlobalBusiness
    {

        private ComponenteData DAComponente;

        /// <summary>
        /// Constructor del objeto de negocio para LineaObligacion
        /// </summary>
        public ComponenteBusiness()
        {
            DAComponente = new ComponenteData();
        }

        /// <summary>
        /// Crea un Componente
        /// </summary>
        /// <param name="pComponente">Entidad Componente</param>
        /// <returns>Entidad Componente creada</returns>
        public Componente CrearComponente(Componente pComponente, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComponente = DAComponente.CrearComponente(pComponente, vusuario);

                    ts.Complete();
                }

                return pComponente;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteBusiness", "CrearComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Componente
        /// </summary>
        /// <param name="pComponente">Entidad Componente</param>
        /// <returns>Entidad Componente modificada</returns>
        public Componente ModificarComponente(Componente pComponente, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pComponente = DAComponente.ModificarComponente(pComponente, vusuario);

                    ts.Complete();
                }

                return pComponente;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteBusiness", "ModificarComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Componente
        /// </summary>
        /// <param name="pId">Identificador de Componente</param>
        public void EliminarComponente(Int64 pId, Usuario vusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAComponente.EliminarComponente(pId, vusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteBusiness", "EliminarComponente", ex);
            }
        }

        /// <summary>
        /// Obtiene un Componente
        /// </summary>
        /// <param name="pId">Identificador de Componente</param>
        /// <returns>Entidad Componente</returns>
        public Componente ConsultarComponente(Int64 pId, Usuario vusuario)
        {
            try
            {
                Componente Componente = new Componente();

                Componente = DAComponente.ConsultarComponente(pId, vusuario);

                return Componente;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteBusiness", "ConsultarComponente", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Componentes dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Componentes obtenidos</returns>
        public List<Componente> ListarComponentes(Componente pComponente, Usuario pUsuario)
        {
            try
            {
                return DAComponente.ListarComponentes(pComponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteBusiness", "ListarComponentes", ex);
                return null;
            }
        }
    }
}
