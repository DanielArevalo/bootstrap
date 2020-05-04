using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AtributoService
    {
        private AtributoBusiness BOAtributo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Atributo
        /// </summary>
        public AtributoService()
        {
            BOAtributo = new AtributoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "10101"; } }

        /// <summary>
        /// Servicio para crear Atributo
        /// </summary>
        /// <param name="pEntity">Entidad Atributo</param>
        /// <returns>Entidad Atributo creada</returns>
        public Atributo CrearAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            try
            {
                return BOAtributo.CrearAtributo(pAtributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoService", "CrearAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Atributo
        /// </summary>
        /// <param name="pAtributo">Entidad Atributo</param>
        /// <returns>Entidad Atributo modificada</returns>
        public Atributo ModificarAtributo(Atributo pAtributo, Usuario pUsuario)
        {
            try
            {
                return BOAtributo.ModificarAtributo(pAtributo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoService", "ModificarAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Atributo
        /// </summary>
        /// <param name="pId">identificador de Atributo</param>
        public void EliminarAtributo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAtributo.EliminarAtributo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAtributo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Atributo
        /// </summary>
        /// <param name="pId">identificador de Atributo</param>
        /// <returns>Entidad Atributo</returns>
        public Atributo ConsultarAtributo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAtributo.ConsultarAtributo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoService", "ConsultarAtributo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Atributos a partir de unos filtros
        /// </summary>
        /// <param name="pAtributo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Atributo obtenidos</returns>
        public List<Atributo> ListarAtributo(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return BOAtributo.ListarAtributo(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributoService", "ListarAtributo", ex);
                return null;
            }
        }
    }
}