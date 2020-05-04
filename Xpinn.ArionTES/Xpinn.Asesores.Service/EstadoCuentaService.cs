using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EstadoCuentaService
    {
        private EstadoCuentaBusiness BOEstadoCuenta;
        private ExcepcionBusiness BOExcepcion;

        public ClientePotencialService serviceCliente = new ClientePotencialService();
        PersonaService servicePersona = new PersonaService();
        ProductoService serviceProducto = new ProductoService();
        DetalleProductoService serviceDetalleProducto = new DetalleProductoService();
        ParametricaService serviceParametrica = new ParametricaService();
        NegocioService serviceNegocio = new NegocioService();
        AcodeudadoService acodeudadoService = new AcodeudadoService();
        ActividadServices serviceActividad = new ActividadServices();

        public string CodigoPrograma { get { return "110120"; } }
        public string CodigoProgramamod { get { return "100152"; } }


    

        public EstadoCuentaService()
        {
            BOEstadoCuenta = new EstadoCuentaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public List<EstadoCuenta> consultaracodeudado(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.consultaracodeudado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroCreditos", ex);
                return null;
            }
        }
        public List<EstadoCuenta> consultarServicios(EstadoCuenta pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.consultarServicios(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "consultarServicios", ex);
                return null;
            }
        }

        public EstadoCuenta enviarAlertas(EstadoCuenta vAlerta, Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.enviarAlertas(vAlerta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "enviarAlerta", ex);
                return null;
            }
        }

        public List<ClientePotencial> ListarClientes(ClientePotencial pEntityCliente, Usuario pUsuario)
        {
            return serviceCliente.ListarCliente(pEntityCliente, pUsuario);
        }

        public Persona ConsultarPersona(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            return servicePersona.ConsultarPersona(pIdEntityPersona, pUsuario);
        }

        public List<Producto> ListarProductosPorEstados(Producto pEntityProducto, Usuario pUsuario, String FiltroEstados)
        {
            return serviceProducto.ListarProductosPorEstados(pEntityProducto, pUsuario, FiltroEstados);
        }

        public List<Producto> ListarProductos(Producto pEntityProducto, Usuario pUsuario)
        {
            return serviceProducto.ListarProductos(pEntityProducto, pUsuario);
        }

        public List<Producto> ListarProductosTodos(Producto pEntityProducto, Usuario pUsuario)
        {
            return serviceProducto.ListarProductosTodos(pEntityProducto, pUsuario);
        }

        public List<Producto> ListarProductoscredito(Int64 credito, Usuario pUsuario)
        {
            return serviceProducto.ListarProductoscredito(credito, 1, pUsuario);
        }

        public List<Producto> ListarProductosCreditoMovimiento(Int64 credito, Usuario pUsuario)
        {
            return serviceProducto.ListarProductoscredito(credito, 0, pUsuario);
        }

        public List<DetalleProducto> ListarDetalleProductos(Producto pEntityProducto, Usuario pUsuario,int detalle, int calcularTotal = 0)
        {
            return serviceDetalleProducto.ListarDetalleProductos(pEntityProducto, pUsuario,detalle, calcularTotal);
        }
        public List<DetalleProducto> ListarValoresAdeudados(Int64 pCredito, Usuario pUsuario)
        {
            return serviceDetalleProducto.ListarValoresAdeudados(pCredito, pUsuario);
        }

        public List<DetalleProducto> ListarValoresAdeudados(Int64 pCredito,DateTime Fecha_Pago, Usuario pUsuario)
        {
            return serviceDetalleProducto.ListarValoresAdeudados(pCredito, Fecha_Pago, pUsuario);
        }
        public List<ConsultaAvance> ListarAvances(Int64 numradicado, Usuario pUsuario, string pfiltro = "")
        {
            return serviceDetalleProducto.ListarAvances(numradicado, pfiltro, pUsuario);
        }

        public List<Negocio> ListarNegocios(Negocio pEntityNegocio, Usuario pUsuario)
        {
            return serviceNegocio.ListarNegocios(pEntityNegocio, pUsuario);
        }

        public List<Acodeudados> ListarAcodeudados(Cliente pCliente, Usuario pUsuario)
        {
            return acodeudadoService.ListarAcodeudados(pCliente, pUsuario);
        }
       
        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividad(Xpinn.Asesores.Entities.Common.Actividad pActividad, Usuario pUsuario)
        {
            return serviceActividad.ListarActividad(pActividad, pUsuario);
        }

        public Producto ConsultarCreditosTerminados(Int64 pIdEntityProducto, Usuario pUsuario)
        {
            return serviceProducto.ConsultarCreditosTerminados(pIdEntityProducto, pUsuario);
        }

        /// <summary>
        /// Servicio para obtener el parametro para Creditos
        /// </summary>
        /// <param name="pId">identificador de Creditos
        /// <returns>Entidad ParametroCreditos/returns>
        public string ConsultarParametroCreditos(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.ConsultarParametroCreditos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para Aportes
        /// </summary>
        /// <param name="pId">identificador de Aportes
        /// <returns>Entidad ParametroAportes</returns>
        public string ConsultarParametrosAportes(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.ConsultarParametrosAporte(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametrosAporte", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Servicio para obtener el parametro para Ahorros
        /// </summary>
        /// <param name="pId">identificador de Ahorros
        /// <returns>Entidad ParametroAportes</returns>
        public string ConsultarParametroAhorros(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.ConsultarParametroAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametrosAhorros", ex);
                return null;
            }
        
         }

   /// <summary>
        /// Servicio para obtener el parametro para Servicios
        /// </summary>
        /// <param name="pId">identificador de Servicios
        /// <returns>Entidad ParametroServicios</returns>
        public string ConsultarParametroServicios(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.ConsultarParametroServicios(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroServicios", ex);
                return null;
            }

        }

        /// <summary>
        /// Servicio para obtener el parametro para Ocultar columnas
        /// </summary>
        /// <param name="pId">identificador 
        /// <returns>Entidad</returns>
        public int ConsultarParametroColumnas(Usuario pUsuario)
        {
            try
            {
                int valor = 0;
                valor=BOEstadoCuenta.ConsultarParametroColumnas(pUsuario);
                return valor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroColumnas", ex);
                return 0;
            }

        }

        /// <summary>
        /// Servicio para obtener el listado de clientes con productos activos
        /// </summary>
        /// <returns>Lista</returns>
        public List<EstadoCuenta> ListarClientes(string filtro, DateTime pFechaCreacion, DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuenta.ListarClientes(filtro, pFechaCreacion, pFechaCorte, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ListarClientes", ex);
                return null;
            }
        }
    }


}
