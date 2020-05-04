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
    public class OpcionesCreditoService
    {
        private OpcionesCreditoBusiness BOOpcionesCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para OpcionesCredito
        /// </summary>
        public OpcionesCreditoService()
        {
            BOOpcionesCredito = new OpcionesCreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100207"; } }

        /// <summary>
        /// Servicio para crear OpcionesCredito
        /// </summary>
        /// <param name="pEntity">Entidad OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito creada</returns>
        public OpcionesCredito CrearOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.CrearOpcionesCredito(pOpcionesCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoService", "CrearOpcionesCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar OpcionesCredito
        /// </summary>
        /// <param name="pOpcionesCredito">Entidad OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito modificada</returns>
        public OpcionesCredito ModificarOpcionesCredito(OpcionesCredito pOpcionesCredito, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.ModificarOpcionesCredito(pOpcionesCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoService", "ModificarOpcionesCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar OpcionesCredito
        /// </summary>
        /// <param name="pId">identificador de OpcionesCredito</param>
        public void EliminarOpcionesCredito(string pId, Usuario pUsuario)
        {
            try
            {
                BOOpcionesCredito.EliminarOpcionesCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarOpcionesCredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener OpcionesCredito
        /// </summary>
        /// <param name="pId">identificador de OpcionesCredito</param>
        /// <returns>Entidad OpcionesCredito</returns>
        public OpcionesCredito ConsultarOpcionesCredito(string pId, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.ConsultarOpcionesCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("OpcionesCreditoService", "ConsultarOpcionesCredito", ex);
                return null;
            }
        }

        public List<OpcionesCredito> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.ListarOpciones(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ListarOpciones", ex);
                return null;
            }
        }
        public List<OpcionesCredito> ListarOpcionesModulo(Int64 IdPerfil, Int64 CodModulo, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.ListarOpcionesModulo(IdPerfil, CodModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PerfilService", "ListarOpcionesModulo", ex);
                return null;
            }

        }
        /// <summary>
        /// Servicio para obtener lista de Modulos a partir de unos filtros
        /// </summary>
        /// <param name="pModulo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<OpcionesCredito> ListarModulo(OpcionesCredito pModulo, Usuario pUsuario)
        {
            try
            {
                return BOOpcionesCredito.ListarModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloService", "ListarModulo", ex);
                return null;
            }
        }

    }
}