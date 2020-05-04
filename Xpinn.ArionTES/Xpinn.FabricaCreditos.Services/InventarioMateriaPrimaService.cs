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
    public class InventarioMateriaPrimaService
    {
        private InventarioMateriaPrimaBusiness BOInventarioMateriaPrima;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para InventarioMateriaPrima
        /// </summary>
        public InventarioMateriaPrimaService()
        {
            BOInventarioMateriaPrima = new InventarioMateriaPrimaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100131

        /// <summary>
        /// Servicio para crear InventarioMateriaPrima
        /// </summary>
        /// <param name="pEntity">Entidad InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima creada</returns>
        public InventarioMateriaPrima CrearInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return BOInventarioMateriaPrima.CrearInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaService", "CrearInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar InventarioMateriaPrima
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima modificada</returns>
        public InventarioMateriaPrima ModificarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return BOInventarioMateriaPrima.ModificarInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaService", "ModificarInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar InventarioMateriaPrima
        /// </summary>
        /// <param name="pId">identificador de InventarioMateriaPrima</param>
        public void EliminarInventarioMateriaPrima(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            try
            {
                BOInventarioMateriaPrima.EliminarInventarioMateriaPrima(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarInventarioMateriaPrima", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener InventarioMateriaPrima
        /// </summary>
        /// <param name="pId">identificador de InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima</returns>
        public InventarioMateriaPrima ConsultarInventarioMateriaPrima(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOInventarioMateriaPrima.ConsultarInventarioMateriaPrima(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaService", "ConsultarInventarioMateriaPrima", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InventarioMateriaPrimas a partir de unos filtros
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return BOInventarioMateriaPrima.ListarInventarioMateriaPrima(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaService", "ListarInventarioMateriaPrima", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de InventarioMateriaPrimas a partir de unos filtros
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrimaRepo(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            try
            {
                return BOInventarioMateriaPrima.ListarInventarioMateriaPrimaRepo(pInventarioMateriaPrima, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventarioMateriaPrimaService", "ListarInventarioMateriaPrimaRepo", ex);
                return null;
            }
        }
        

        /// <summary>
        /// Servicio para obtener calculos sobre las Ventas Semanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public InventarioMateriaPrima CalculosInventarioMateriaPrima(List<InventarioMateriaPrima> lstConsulta)
        {
            try
            {
                return BOInventarioMateriaPrima.CalculosInventarioMateriaPrima(lstConsulta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }



    }
}