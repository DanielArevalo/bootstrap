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
    public class ParentescoService
    {
        private ParentescoBusiness BOParentesco;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Parentesco
        /// </summary>
        public ParentescoService()
        {
            BOParentesco = new ParentescoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "CRE"; } }

        /// <summary>
        /// Servicio para crear Parentesco
        /// </summary>
        /// <param name="pEntity">Entidad Parentesco</param>
        /// <returns>Entidad Parentesco creada</returns>
        public Parentesco CrearParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                return BOParentesco.CrearParentesco(pParentesco, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoService", "CrearParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Parentesco
        /// </summary>
        /// <param name="pParentesco">Entidad Parentesco</param>
        /// <returns>Entidad Parentesco modificada</returns>
        public Parentesco ModificarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                return BOParentesco.ModificarParentesco(pParentesco, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoService", "ModificarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Parentesco
        /// </summary>
        /// <param name="pId">identificador de Parentesco</param>
        public void EliminarParentesco(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOParentesco.EliminarParentesco(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarParentesco", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Parentesco
        /// </summary>
        /// <param name="pId">identificador de Parentesco</param>
        /// <returns>Entidad Parentesco</returns>
        public Parentesco ConsultarParentesco(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOParentesco.ConsultarParentesco(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoService", "ConsultarParentesco", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Parentescos a partir de unos filtros
        /// </summary>
        /// <param name="pParentesco">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Parentesco obtenidos</returns>
        public List<Parentesco> ListarParentesco(Parentesco pParentesco, Usuario pUsuario)
        {
            try
            {
                return BOParentesco.ListarParentesco(pParentesco, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParentescoService", "ListarParentesco", ex);
                return null;
            }
        }
    }
}