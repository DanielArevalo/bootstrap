using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Data;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Persona
    /// </summary>
    public class PersonaBusiness : GlobalData
    {

        private PersonaData DAPersona;

        /// <summary>
        /// Constructor del objeto de negocio para Persona
        /// </summary>
        public PersonaBusiness()
        {
            DAPersona = new PersonaData();
        }

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la Consulta de una Persona
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarPersona(Persona pPersona,Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarPersona", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la Consulta de una Persona
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Persona consultada por Codigo</returns>
        public Persona ConsultarPersonaXCodigo(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarPersonaXCodigo(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarPersonaXCodigo", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la Consulta de una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Empresa</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarEmpresa(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarEmpresa(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarEmpresa", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la Consulta de una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Empresa</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorEfectivo(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarValorEfectivo(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarValorEfectivo", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la Consulta de una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Empresa</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorCheque(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarValorCheque(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarValorCheque", ex);
                return null;
            }
        }

        public Persona ConsultarValorChequeCaja(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarValorChequeCaja(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarValorChequeCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la Consulta de una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Empresa</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorOtros(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarValorOtros(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarValorOtros", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Personaes con sus creditos obtenidos</returns>
        public List<Persona> ListarDatosCreditoPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarDatosCreditoPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarDatosCreditoPersona", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Personaes con sus creditos obtenidos</returns>
        public List<Persona> ListarDatosCreditoPersonaAhorros(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarDatosCreditoPersonaAhorros(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarDatosCreditoPersonaAhorros", ex);
                return null;
            }

        }

        public List<Persona> ListarDatosCreditoPersonaAportes(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarDatosCreditoPersonaAportes(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarDatosCreditoPersonaAportes", ex);
                return null;
            }

        }

        public List<Persona> ListarPersonasAfiliacion(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarPersonasAfiliacion(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarPersonasAfiliacion", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Personaes con sus creditos obtenidos</returns>
        public Persona ConsultarDatosCreditoPersona(String pId, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ConsultarDatosCreditoPersona(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarDatosCreditoPersona", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Personaes con sus creditos obtenidos</returns>
        public List<Persona> ListarDatosAportesPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarDatosAportesPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarDatosAportesPersona", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Creditos de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de CReditos de un cliente</returns>
        public List<Persona> ListarCreditosCliente(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarCreditosCliente(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarCreditoCliente", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Activos  Hipotecarios de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Activos de un cliente</returns>
        public List<Persona> ListarActivosHipotecarios(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarActivosHipotecarios(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarActivosHipotecarios", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Activos Prendarios  de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos de Activos de un cliente</returns>
        public List<Persona> ListarActivosPrendarios(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarActivosPrendarios(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarActivosPrendarios", ex);
                return null;
            }
        }

        public Persona ConsultarPersonaXIdentificacion(string identificacion, Usuario usuario)
        {
            try
            {
                return DAPersona.ConsultarPersonaXIdentificacion(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarPersonaXIdentificacion", ex);
                return null;
            }
        }

        public DataTable ConsultarPersonasProductos(DateTime pFechaCorte, Usuario usuario)
        {
            try
            {
                return DAPersona.ListarPersonasProductos(pFechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ConsultarPersonaXIdentificacion", ex);
                return null;
            }
        }
    }
}
