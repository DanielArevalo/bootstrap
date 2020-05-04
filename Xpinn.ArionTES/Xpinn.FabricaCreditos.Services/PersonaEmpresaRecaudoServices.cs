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
    public class PersonaEmpresaRecaudoServices
    {
        private PersonaEmpresaRecaudoBusiness BOPersonaEmpresaRecaudo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PersonaEmpresaRecaudo
        /// </summary>
        public PersonaEmpresaRecaudoServices()
        {
            BOPersonaEmpresaRecaudo = new PersonaEmpresaRecaudoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30704"; } }

        /// <summary>
        /// Servicio para crear PersonaEmpresaRecaudo
        /// </summary>
        /// <param name="pEntity">Entidad PersonaEmpresaRecaudo</param>
        /// <returns>Entidad PersonaEmpresaRecaudo creada</returns>
        public PersonaEmpresaRecaudo CrearPersonaEmpresaRecaudo(PersonaEmpresaRecaudo vPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.CrearPersonaEmpresaRecaudo(vPersonaEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "CrearPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar PersonaEmpresaRecaudo
        /// </summary>
        /// <param name="pPersonaEmpresaRecaudo">Entidad PersonaEmpresaRecaudo</param>
        /// <returns>Entidad PersonaEmpresaRecaudo modificada</returns>
        public PersonaEmpresaRecaudo ModificarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo vPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.ModificarPersonaEmpresaRecaudo(vPersonaEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "ModificarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar PersonaEmpresaRecaudo
        /// </summary>
        /// <param name="pId">identificador de PersonaEmpresaRecaudo</param>
        public void EliminarPersonaEmpresaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPersonaEmpresaRecaudo.EliminarPersonaEmpresaRecaudo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPersonaEmpresaRecaudo", ex);
            }
        }


        public PersonaEmpresaRecaudo ConsultarPersonaEmpresaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.ConsultarPersonaEmpresaRecaudo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "ConsultarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PersonaEmpresaRecaudos a partir de unos filtros
        /// </summary>
        /// <param name="pPersonaEmpresaRecaudo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PersonaEmpresaRecaudo obtenidos</returns>
        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(PersonaEmpresaRecaudo vPersonaEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.ListarPersonaEmpresaRecaudo(vPersonaEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<PersonaEmpresaRecaudo> ListarPersonaEmpresaRecaudo(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.ListarPersonaEmpresaRecaudo(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "ListarPersonaEmpresaRecaudo", ex);
                return null;
            }
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(Usuario vUsuario)
        {
            return ListarEmpresaRecaudo(false, vUsuario);
        }

        public List<PersonaEmpresaRecaudo> ListarEmpresaRecaudo(bool alfabetico, Usuario pUsuario)
        {
            try
            {
                return BOPersonaEmpresaRecaudo.ListarEmpresaRecaudo(alfabetico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaEmpresaRecaudoService", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }



    }
}