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
    public class EstadosFinancierosService
    {
        private EstadosFinancierosBusiness BOEstadosFinancieros;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EstadosFinancieros
        /// </summary>
        public EstadosFinancierosService()
        {
            BOEstadosFinancieros = new EstadosFinancierosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100128

        /// <summary>
        /// Servicio para crear EstadosFinancieros
        /// </summary>
        /// <param name="pEntity">Entidad EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros creada</returns>
        public EstadosFinancieros CrearEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
                
            {
                return BOEstadosFinancieros.CrearEstadosFinancieros(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "CrearEstadosFinancieros", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar EstadosFinancieros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros modificada</returns>
        public EstadosFinancieros ModificarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.ModificarEstadosFinancieros(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ModificarEstadosFinancieros", ex);
                return null;
            }
        }

        public EstadosFinancieros RecalcularEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.RecalcularEstadosFinancieros(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "RecalcularEstadosFinancieros", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar EstadosFinancieros
        /// </summary>
        /// <param name="pId">identificador de EstadosFinancieros</param>
        public void EliminarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEstadosFinancieros.EliminarEstadosFinancieros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarEstadosFinancieros", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener EstadosFinancieros
        /// </summary>
        /// <param name="pId">identificador de EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros</returns>
        public EstadosFinancieros ConsultarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.ConsultarEstadosFinancieros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ConsultarEstadosFinancieros", ex);
                return null;
            }
        }

        public EstadosFinancieros listarperosnainfofin(long cod, Usuario pUsuario)
        {
            try
            {
             return   BOEstadosFinancieros.listarperosnainfofin(cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ConsultarEstadosFinancieros", ex);
                return null;
            }
        }
        public void guardarIngreEgre(EstadosFinancieros informacionFinanciera, Usuario pUsuario)
        {
            try
            {
                 BOEstadosFinancieros.guardarIngreEgre(informacionFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ConsultarEstadosFinancieros", ex);
          
            }
        }


        /// <summary>
        /// Servicio para obtener lista de EstadosFinancieross a partir de unos filtros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstadosFinancieros obtenidos</returns>
        public List<EstadosFinancieros> ListarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.ListarEstadosFinancieros(pEstadosFinancieros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ListarEstadosFinancieros", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener calculos sobre las Ventas Semanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public EstadosFinancieros UtilidadEstadosFinancieros(EstadosFinancieros entTotalesEstados)
        {
            try
            {
                return BOEstadosFinancieros.UtilidadEstadosFinancieros(entTotalesEstados);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesService", "ListarVentasSemanales", ex);
                return null;
            }
        }

        public List<EstadosFinancieros> ListarIngresosEgresosRepo(EstadosFinancieros pInformacionfinanciera, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.ListarIngresosEgresosRepo(pInformacionfinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ListarIngresosEgresosRepo", ex);
                return null;
            }
        }

        //Agregado para listar cuentas de moneda extranjera
        public List<EstadosFinancieros> ListarCuentasMonedaExtranjera(Int64 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BOEstadosFinancieros.ListarCuentasMonedaExtranjera(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "ListarCuentasMonedaExtranjera", ex);
                return null;
            }
        }

        //Agregado para eliminara cuentas de moneda extranjera
        public void EliminarCuentasMonedaExtranjera(Int64 pCodMoneda, Usuario vUsuario)
        {
            try
            {
                BOEstadosFinancieros.EliminarCuentasMonedaExtranjera(pCodMoneda, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadosFinancierosService", "EliminarCuentasMonedaExtranjera", ex);
            }
        }
    }
}