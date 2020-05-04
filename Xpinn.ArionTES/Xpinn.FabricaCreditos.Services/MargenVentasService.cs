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
    public class MargenVentasService
    {
        private MargenVentasBusiness BOMargenVentas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MargenVentas
        /// </summary>
        public MargenVentasService()
        {
            BOMargenVentas = new MargenVentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }   //100132

        /// <summary>
        /// Servicio para crear MargenVentas
        /// </summary>
        /// <param name="pEntity">Entidad MargenVentas</param>
        /// <returns>Entidad MargenVentas creada</returns>
        public MargenVentas CrearMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                return BOMargenVentas.CrearMargenVentas(pMargenVentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasService", "CrearMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MargenVentas
        /// </summary>
        /// <param name="pMargenVentas">Entidad MargenVentas</param>
        /// <returns>Entidad MargenVentas modificada</returns>
        public MargenVentas ModificarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                return BOMargenVentas.ModificarMargenVentas(pMargenVentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasService", "ModificarMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MargenVentas
        /// </summary>
        /// <param name="pId">identificador de MargenVentas</param>
        public void EliminarMargenVentas(Int64 pId, Int64 persona, Usuario pUsuario)
        {
            try
            {
                BOMargenVentas.EliminarMargenVentas(pId, persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarMargenVentas", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener MargenVentas
        /// </summary>
        /// <param name="pId">identificador de MargenVentas</param>
        /// <returns>Entidad MargenVentas</returns>
        public MargenVentas ConsultarMargenVentas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMargenVentas.ConsultarMargenVentas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasService", "ConsultarMargenVentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MargenVentass a partir de unos filtros
        /// </summary>
        /// <param name="pMargenVentas">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MargenVentas obtenidos</returns>
        public List<MargenVentas> ListarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            try
            {
                return BOMargenVentas.ListarMargenVentas(pMargenVentas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MargenVentasService", "ListarMargenVentas", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener calculos sobre las Ventas Semanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public MargenVentas CalculosMargenVentas(List<MargenVentas> lstConsulta)
        {
            try
            {
                return BOMargenVentas.CalculosMargenVentas(lstConsulta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }




    }
}