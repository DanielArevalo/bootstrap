using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;


namespace Xpinn.Riesgo.Services
/// <summary>
/// Servicios para Programa
/// </summary>
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

  public  class JurisdiccionDepaServices
    {

        private JurisdiccionDepaBusiness BOJurisdiccionDepa;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public JurisdiccionDepaServices()
        {
            BOJurisdiccionDepa = new JurisdiccionDepaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270202"; } }


        public List<JurisdiccionDepa> ListarJurisdiccionDepa(JurisdiccionDepa JurisdiccionDepa, Usuario usuario)
        {
            try
            {
                return BOJurisdiccionDepa.ListarJurisdiccionDepa(JurisdiccionDepa, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "ListarJurisdiccionDepa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear registro de una Jurisdiccion
        /// </summary>
        /// <param name="pJurisdiccionDepa">Objeto con los datos de Jurisdiccion</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public JurisdiccionDepa CrearJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                pJurisdiccionDepa = BOJurisdiccionDepa.CrearJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                return pJurisdiccionDepa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "CrearJurisdiccionDepa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de Jurisdiccion
        /// </summary>
        /// <param name="pActividadEco">Objeto con los datos de Jurisdiccion</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public JurisdiccionDepa ModificarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                pJurisdiccionDepa = BOJurisdiccionDepa.ModificarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                return pJurisdiccionDepa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "ModificarJurisdiccionDepa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de Jurisdiccion
        /// </summary>
        /// <param name="pJurisdiccionDepa">Objeto con el código de Jurisdiccion</param>
        /// <param name="vUsuario"></param>
        public void EliminarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                BOJurisdiccionDepa.EliminarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "EliminarJurisdiccionDepa", ex);
            }
        }

        /// <summary>
        /// Consultar una Jurisdiccion
        /// </summary>
        /// <param name="pJurisdiccionDepa">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public JurisdiccionDepa ConsultarFactorRiesgo(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            try
            {
                pJurisdiccionDepa = BOJurisdiccionDepa.ConsultarJurisdiccionDepa(pJurisdiccionDepa, vUsuario);
                return pJurisdiccionDepa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "ConsultarFactorRiesgo", ex);
                return null;
            }
        }


        public List<JurisdiccionDepa> ListasDesplegables(Usuario pUsuario)
        {
            try
            {
                return BOJurisdiccionDepa.ListasDesplegables(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("JurisdiccionDepaServices", "ListasDesplegables", ex);
                return null;
            }
        }


    }
}
