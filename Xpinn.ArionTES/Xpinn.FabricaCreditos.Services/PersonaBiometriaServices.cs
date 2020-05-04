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
    public class PersonaBiometriaService
    {
        private PersonaBiometriaBusiness BOPersonaBiometria;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PersonaBiometria
        /// </summary>
        public PersonaBiometriaService()
        {
            BOPersonaBiometria = new PersonaBiometriaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }

        /// <summary>
        /// Servicio para crear PersonaBiometria
        /// </summary>
        /// <param name="pEntity">Entidad PersonaBiometria</param>
        /// <returns>Entidad PersonaBiometria creada</returns>
        public PersonaBiometria CrearPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.CrearPersonaBiometria(pPersonaBiometria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaService", "CrearPersonaBiometria", ex);
                return null;
            }
        }

        public PersonaBiometria ModificarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.ModificarPersonaBiometria(pPersonaBiometria, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaService", "ModificarPersonaBiometria", ex);
                return null;
            }
        }

        public Int64 ExistePersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.ExistePersonaBiometria(pId, pNumeroDedo, pUsuario); ;
            }
            catch
            {                
                return -1;
            }
        }

        public PersonaBiometria ConsultarPersonaBiometria(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.ConsultarPersonaBiometria(pId, pNumeroDedo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaService", "ConsultarPersonaBiometria", ex);
                return null;
            }
        }

        public List<PersonaBiometria> ListarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.ListarPersonaBiometria(pPersonaBiometria, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaService", "ListarPersonaBiometria", ex);
                return null;
            }
        }

        public PersonaBiometria ConsultarPersonaBiometriaSECUGEN(Int64 pId, Int32 pNumeroDedo, Usuario pUsuario)
        {
            try
            {
                return BOPersonaBiometria.ConsultarPersonaBiometriaSECUGEN(pId, pNumeroDedo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBiometriaService", "ConsultarPersonaBiometriaSECUGEN", ex);
                return null;
            }
        }

    }
}