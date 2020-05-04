using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class EstadoCuentaBusiness : GlobalBusiness
    {
        private EstadoCuentaData BOEstadoCuentaData;

        public EstadoCuentaBusiness()
        {
            BOEstadoCuentaData = new EstadoCuentaData();
        }
        public List<EstadoCuenta> consultarServicios(EstadoCuenta pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuentaData.consultarServicios(pCodPersona, pUsuario);
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
                return BOEstadoCuentaData.enviarAlertas(vAlerta, pUsuario, 0);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "enviarAlerta", ex);
                return null;
            }
        }

        public List<EstadoCuenta> consultaracodeudado(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuentaData.consultaracodeudado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para Creditos
        /// </summary>
        /// <param name="pId">identificador de Creditos
        /// <returns>Entidad ParametroCreditos</returns>
        public string ConsultarParametroCreditos(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuentaData.ConsultarParametroCredito(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener el parametro para  APortes
        /// </summary>
        /// <param name="pId">identificador de APortes
        /// <returns>Entidad ParametroAPortes</returns>
        public string ConsultarParametrosAporte(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuentaData.ConsultarParametroAportes(pUsuario);
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
        /// <returns>Entidad ParametroAhorros</returns>
        public string ConsultarParametroAhorros(Usuario pUsuario)
        {
            try
            {
                return BOEstadoCuentaData.ConsultarParametroAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaService", "ConsultarParametroAhorros", ex);
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
                return BOEstadoCuentaData.ConsultarParametroServicios(pUsuario);
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
                valor=BOEstadoCuentaData.ConsultarParametroColumnas(pUsuario);
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
                return BOEstadoCuentaData.ListarClientes(filtro, pFechaCreacion, pFechaCorte, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoCuentaBusiness", "ListarClientes", ex);
                return null;
            }
        }
    }
}
