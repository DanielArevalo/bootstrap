using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Opcion
    /// </summary>
    public class OpcionBusiness : GlobalBusiness
    {
        private OpcionData DAOpcion;

        /// <summary>
        /// Constructor del objeto de negocio para Opcion
        /// </summary>
        public OpcionBusiness()
        {
            DAOpcion = new OpcionData();
        }

        /// <summary>
        /// Crea un Opcion
        /// </summary>
        /// <param name="pOpcion">Entidad Opcion</param>
        /// <returns>Entidad Opcion creada</returns>
        public Opcion CrearOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOpcion = DAOpcion.CrearOpcion(pOpcion, pUsuario);

                    ts.Complete();
                }

                return pOpcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "CrearOpcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Opcion
        /// </summary>
        /// <param name="pOpcion">Entidad Opcion</param>
        /// <returns>Entidad Opcion modificada</returns>
        public Opcion ModificarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOpcion = DAOpcion.ModificarOpcion(pOpcion, pUsuario);

                    ts.Complete();
                }

                return pOpcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ModificarOpcion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Opcion
        /// </summary>
        /// <param name="pId">Identificador de Opcion</param>
        public void EliminarOpcion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAOpcion.EliminarOpcion(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "EliminarOpcion", ex);
            }
        }

        /// <summary>
        /// Obtiene un Opcion
        /// </summary>
        /// <param name="pId">Identificador de Opcion</param>
        /// <returns>Entidad Opcion</returns>
        public Opcion ConsultarOpcion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAOpcion.ConsultarOpcion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ConsultarOpcion", ex);
                return null;
            }
        }

       

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pOpcion">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Opcion obtenidos</returns>
        public List<Opcion> ListarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            try
            {
                return DAOpcion.ListarOpcion(pOpcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }


        public Opcion Modificargeneral(Opcion pgeneral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pgeneral = DAOpcion.Modificargeneral(pgeneral, pusuario);

                    ts.Complete();

                }

                return pgeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("generalBusiness", "Modificargeneral", ex);
                return null;
            }
        }


        public List<Opcion> ListarOpcionesGeneral(string filtro,Usuario pUsuario)
        {
            try
            {
                return DAOpcion.ListarOpcionesGeneral(filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }

        public List<Opcion> ListarOpciones(Usuario pUsuario)
        {
            try
            {
                return DAOpcion.ListarOpciones(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionBusiness", "ListarOpcion", ex);
                return null;
            }
        }

    }
}