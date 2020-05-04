using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.Seguridad.Business
{

    public class EmpresaBusiness : GlobalBusiness
    {

        private EmpresaComunData DAEmpresa;

        public EmpresaBusiness()
        {
            DAEmpresa = new EmpresaComunData();
        }

        public Empresa CrearEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                pEmpresa = DAEmpresa.CrearEmpresa(pEmpresa, pusuario);

                if (pEmpresa.logoempresa_bytes != null && pEmpresa.logoempresa_bytes.Length > 0)
                {
                    ImagenesORAData imagenesORAData = new ImagenesORAData();
                    imagenesORAData.ModificarLogoEmpresa(pEmpresa, pusuario);
                }

                return pEmpresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "CrearEmpresa", ex);
                return null;
            }
        }


        public Empresa ModificarEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                pEmpresa = DAEmpresa.ModificarEmpresa(pEmpresa, pusuario);

                if (pEmpresa.logoempresa_bytes != null && pEmpresa.logoempresa_bytes.Length > 0)
                {
                    ImagenesORAData imagenesORAData = new ImagenesORAData();
                    imagenesORAData.ModificarLogoEmpresa(pEmpresa, pusuario);
                }

                return pEmpresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "ModificarEmpresa", ex);
                return null;
            }
        }


        public void EliminarEmpresa(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresa.EliminarEmpresa(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "EliminarEmpresa", ex);
            }
        }


        public Empresa ConsultarEmpresa(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empresa Empresa = new Empresa();
                Empresa = DAEmpresa.ConsultarEmpresa(pId, pusuario);
                return Empresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "ConsultarEmpresa", ex);
                return null;
            }
        }


        public List<Empresa> ListarEmpresa(Empresa pEmpresa, Usuario pusuario)
        {
            try
            {
                return DAEmpresa.ListarEmpresa(pEmpresa, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "ListarEmpresa", ex);
                return null;
            }
        }

        public Empresa ConsultarEmpresa(Usuario usuario)
        {
            try
            {
                Empresa Empresa = new Empresa();
                Empresa = DAEmpresa.ConsultarEmpresa(usuario);
                return Empresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaBusiness", "ConsultarEmpresa", ex);
                return null;
            }
        }
    }
}

