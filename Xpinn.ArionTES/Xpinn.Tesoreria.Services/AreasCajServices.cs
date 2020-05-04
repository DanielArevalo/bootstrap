using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AreasCajService
    {
        private AreasCajBusiness BOAreasCaj;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public AreasCajService()
        {
            BOAreasCaj = new AreasCajBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40604"; } }

        /// <summary>
        /// Servicio para crear AreasCaj
        /// </summary>
        /// <param name="pEntity">Entidad AreasCaj</param>
        /// <returns>Entidad AreasCaj creada</returns>
        public AreasCaj CrearAreasCaj(AreasCaj vAreasCaj, Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.CrearAreasCaj(vAreasCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "CrearAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar AreasCaj
        /// </summary>
        /// <param name="pAreasCaj">Entidad AreasCaj</param>
        /// <returns>Entidad AreasCaj modificada</returns>
        public AreasCaj ModificarAreasCaj(AreasCaj vAreasCaj, Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.ModificarAreasCaj(vAreasCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ModificarAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar AreasCaj
        /// </summary>
        /// <param name="pId">identificador de AreasCaj</param>
        public void EliminarAreasCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAreasCaj.EliminarAreasCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAreasCaj", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener AreasCaj
        /// </summary>
        /// <param name="pId">identificador de AreasCaj</param>
        /// <returns>Entidad AreasCaj</returns>
        public AreasCaj ConsultarAreasCaj(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.ConsultarAreasCaj(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ConsultarAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de AreasCaj 
        /// </summary>
        /// <param name="pAreasCaj">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AreasCaj obtenidos</returns>
        /// <returns>Conjunto de AreasCaj obtenidos</returns>
        public List<AreasCaj> ListarAreasCaj(AreasCaj vAreasCaj, Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.ListarAreasCaj(vAreasCaj, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ListarAreasCaj", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtener el siguiente código
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }      


        /// <summary>
        /// Obtener codigo de la caja
        /// </summary>
        /// <param name="pIdarea">identificador de AreasCaj</param>
        /// <returns>Entidad AreasCaj</returns>
        public AreasCaj ConsultarCajaMenor(int codusuario, Usuario pUsuario)
        {
            try
            {
                return BOAreasCaj.ConsultarCajaMenor(codusuario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ConsultarCajaMenor", ex);
                return null;
            }
        }
        /// <summary>
        /// Insertar datos principales del arqueo y el detalle
        /// </summary>
        /// <param name="pArqueo">identificador de ArqueoDetalle</param>
        /// <returns>Entidad ArqueoDetalle</returns>
        public ArqueoDetalle CrearDetalleArqueo(List<ArqueoDetalle> lista, ArqueoCaj vArqueoCaj, Usuario vUsuario)
        {
            try
            {
                return BOAreasCaj.CrearDetalleArqueo(lista, vArqueoCaj, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "CrearDetalleArqueo", ex);
                return null;
            }
        }
        public void ModificarArqueoCaja(Int64? id_Arqueo, Int64 total_arqueo, Usuario pUsuario)
        {
            try
            {
                BOAreasCaj.ModificarArqueoCaja(id_Arqueo, total_arqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreasCajService", "ModificarAreasCaj", ex);
            }
        }


    }
}