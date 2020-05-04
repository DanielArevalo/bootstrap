using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;


namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Persona
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PersonaService
    {
        private PersonaBusiness BOPersona;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Persona
        /// </summary>
        public PersonaService()
        {
            BOPersona = new PersonaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPersona;

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Persona
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Oficina</param>
        /// <returns>Persona consultada</returns>
        public Persona ConsultarPersona(Persona pPersona,Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Persona
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Oficina</param>
        /// <returns>Persona consultada por Codigo</returns>
        public Persona ConsultarPersonaXCodigo(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarPersonaXCodigo(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarPersonaXCodigo", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarEmpresa(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarEmpresa(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarEmpresa", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorEfectivo(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarValorEfectivo(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarValorEfectivo", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorCheque(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarValorCheque(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarValorCheque", ex);
                return null;
            }
        }

        public Persona ConsultarValorChequeCaja(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarValorChequeCaja(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarValorChequeCaja", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene una Empresa
        /// </summary>
        /// <param name="pPersona">Objeto de la Clase Persona</param>
        /// <returns>Empresa consultada</returns>
        public Persona ConsultarValorOtros(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarValorOtros(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarValorOtros", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarDatosCreditoPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarDatosCreditoPersona(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarDatosCreditoPersona", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarDatosCreditoPersonaAhorros(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarDatosCreditoPersonaAhorros(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarDatosCreditoPersonaAhorros", ex);
                return null;
            }
        }
        public List<Persona> ListarDatosCreditoPersonaAportes(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarDatosCreditoPersonaAportes(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarDatosCreditoPersonaAportes", ex);
                return null;
            }
        }




        public List<Persona> ListarPersonasAfiliacion(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarPersonasAfiliacion(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarPersonasAfiliacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public Persona ConsultarDatosCreditoPersona(String pId, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ConsultarDatosCreditoPersona(pId, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsuktarDatosCreditoPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Personaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarDatosAportesPersona(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarDatosAportesPersona(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarDatosAportesPersona", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de creditos de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarCreditosCliente(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarCreditosCliente(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarCreditoCliente", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de creditos de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarActivosHipotecarios(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarActivosHipotecarios(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarActivosHipotecarios", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de creditos de un cliente dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Personaes obtenidos</returns>
        public List<Persona> ListarActivosPrendarios(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarActivosPrendarios(pPersona, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarActivosPrendarios", ex);
                return null;
            }
        }

        public Persona ConsultarPersonaXIdentificacion(string identificacion, Usuario usuario)
        {
            try
            {
                return BOPersona.ConsultarPersonaXIdentificacion(identificacion, usuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ConsultarPersonaXIdentificacion", ex);
                return null;
            }
        }

        
    }
}