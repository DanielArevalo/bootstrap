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
    public class ProductoConsumoService
    {
        private ProductoConsumoBusiness BoProductoConsumo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public ProductoConsumoService()
        {
            BoProductoConsumo = new ProductoConsumoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        
        public List<ProductoConsumo> ListarProductoConsumo(string filtro, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.ListarProductoConsumo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarProductoConsumo", ex);
                return null;
            }
        }


        public List<CategoriaProducto> ListarCatProductoConsumo(string filtro, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.ListarCatProductoConsumo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ListarCatProductoConsumo", ex);
                return null;
            }
        }


        public AutorizaConsulta consultarAutorizaPendiente(Int32 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.consultarAutorizaPendiente(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "consultarAutorizaPendiente", ex);
                return null;
            }
        }

        public AutorizaConsulta verificarAutorizacion(Int32 cod_persona, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.verificarAutorizacion(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "verificarAutorizacion", ex);
                return null;
            }
        }


        public bool verificarAutorizacion(AutorizaConsulta entidad, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.CrearAutorizacion(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "CrearAutorizacion", ex);
                return false;
            }
        }


        public bool CrearAutorizacion(AutorizaConsulta entidad, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.CrearAutorizacion(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "CrearAutorizacion", ex);
                return false;
            }
        }


        public bool ModificarAutorizacion(AutorizaConsulta entidad, Usuario pUsuario)
        {
            try
            {
                return BoProductoConsumo.ModificarAutorizacion(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActasService", "ModificarAutorizacion", ex);
                return false;
            }
        }

    }
}