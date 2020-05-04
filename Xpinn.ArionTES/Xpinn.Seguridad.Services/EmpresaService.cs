using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmpresaService
    {

        private EmpresaBusiness BOEmpresa;
        private ExcepcionBusiness BOExcepcion;

        public EmpresaService()
        {
            BOEmpresa = new EmpresaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250605"; } }

        public Empresa CrearEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                pEmpresa = BOEmpresa.CrearEmpresa(pEmpresa, pusuario);
                return pEmpresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "CrearEmpresa", ex);
                return null;
            }
        }


        public Empresa ModificarEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                pEmpresa = BOEmpresa.ModificarEmpresa(pEmpresa, pusuario);
                return pEmpresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "ModificarEmpresa", ex);
                return null;
            }
        }


        public void EliminarEmpresa(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEmpresa.EliminarEmpresa(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "EliminarEmpresa", ex);
            }
        }


        public Empresa ConsultarEmpresa(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empresa Empresa = new Empresa();
                Empresa = BOEmpresa.ConsultarEmpresa(pId, pusuario);
                return Empresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "ConsultarEmpresa", ex);
                return null;
            }
        }


        public List<Empresa> ListarEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                return BOEmpresa.ListarEmpresa(pEmpresa, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "ListarEmpresa", ex);
                return null;
            }
        }

        public Empresa ConsultarEmpresa(Usuario usuario)
        {
            try
            {
                Empresa Empresa = new Empresa();
                Empresa = BOEmpresa.ConsultarEmpresa(usuario);
                return Empresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaService", "ConsultarEmpresa", ex);
                return null;
            }
        }
    }
}

   