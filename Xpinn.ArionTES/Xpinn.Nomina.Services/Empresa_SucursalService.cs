using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Empresa_SucursalService
    {

        private Empresa_SucursalBusiness BOEmpresa_Sucursal;
        private ExcepcionBusiness BOExcepcion;

        public Empresa_SucursalService()
        {
            BOEmpresa_Sucursal = new Empresa_SucursalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250104"; } }

        public Empresa_Sucursal CrearEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario pusuario)
        {
            try
            {
                pEmpresa_Sucursal = BOEmpresa_Sucursal.CrearEmpresa_Sucursal(pEmpresa_Sucursal, pusuario);
                return pEmpresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "CrearEmpresa_Sucursal", ex);
                return null;
            }
        }


        public Empresa_Sucursal ModificarEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario pusuario)
        {
            try
            {
                pEmpresa_Sucursal = BOEmpresa_Sucursal.ModificarEmpresa_Sucursal(pEmpresa_Sucursal, pusuario);
                return pEmpresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "ModificarEmpresa_Sucursal", ex);
                return null;
            }
        }


        public void EliminarEmpresa_Sucursal(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEmpresa_Sucursal.EliminarEmpresa_Sucursal(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "EliminarEmpresa_Sucursal", ex);
            }
        }


        public Empresa_Sucursal ConsultarEmpresa_Sucursal(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empresa_Sucursal Empresa_Sucursal = new Empresa_Sucursal();
                Empresa_Sucursal = BOEmpresa_Sucursal.ConsultarEmpresa_Sucursal(pId, pusuario);
                return Empresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "ConsultarEmpresa_Sucursal", ex);
                return null;
            }
        }
        

        public Empresa_Sucursal ConsultarDartosEmpresa_Sucursal(string pId, Usuario pusuario)
        {
            try
            {
                Empresa_Sucursal Empresa_Sucursal = new Empresa_Sucursal();
                Empresa_Sucursal = BOEmpresa_Sucursal.ConsultarDartosEmpresa_Sucursal(pId, pusuario);
                return Empresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "ConsultarEmpresa_Sucursal", ex);
                return null;
            }
        }

        public List<Empresa_Sucursal> ListarEmpresa_Sucursal(string filtro, Usuario pusuario)
        {
            try
            {
                return BOEmpresa_Sucursal.ListarEmpresa_Sucursal(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalService", "ListarEmpresa_Sucursal", ex);
                return null;
            }
        }


    }
}
