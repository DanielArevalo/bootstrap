using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Empresa_SucursalBusiness : GlobalBusiness
    {

        private Empresa_SucursalData DAEmpresa_Sucursal;

        public Empresa_SucursalBusiness()
        {
            DAEmpresa_Sucursal = new Empresa_SucursalData();
        }

        public Empresa_Sucursal CrearEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresa_Sucursal = DAEmpresa_Sucursal.CrearEmpresa_Sucursal(pEmpresa_Sucursal, pusuario);

                    ts.Complete();

                }

                return pEmpresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "CrearEmpresa_Sucursal", ex);
                return null;
            }
        }


        public Empresa_Sucursal ModificarEmpresa_Sucursal(Empresa_Sucursal pEmpresa_Sucursal, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresa_Sucursal = DAEmpresa_Sucursal.ModificarEmpresa_Sucursal(pEmpresa_Sucursal, pusuario);

                    ts.Complete();

                }

                return pEmpresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "ModificarEmpresa_Sucursal", ex);
                return null;
            }
        }


        public void EliminarEmpresa_Sucursal(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpresa_Sucursal.EliminarEmpresa_Sucursal(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "EliminarEmpresa_Sucursal", ex);
            }
        }


        public Empresa_Sucursal ConsultarEmpresa_Sucursal(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empresa_Sucursal Empresa_Sucursal = new Empresa_Sucursal();
                Empresa_Sucursal = DAEmpresa_Sucursal.ConsultarEmpresa_Sucursal(pId, pusuario);
                return Empresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "ConsultarEmpresa_Sucursal", ex);
                return null;
            }
        }


       public Empresa_Sucursal ConsultarDartosEmpresa_Sucursal(string pId, Usuario pusuario)
        {
            try
            {
                Empresa_Sucursal Empresa_Sucursal = new Empresa_Sucursal();
                Empresa_Sucursal = DAEmpresa_Sucursal.ConsultarDartosEmpresa_Sucursal(pId, pusuario);
                return Empresa_Sucursal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "ConsultarEmpresa_Sucursal", ex);
                return null;
            }
        }


        public List<Empresa_Sucursal> ListarEmpresa_Sucursal(string filtro, Usuario pusuario)
        {
            try
            {
                return DAEmpresa_Sucursal.ListarEmpresa_Sucursal(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empresa_SucursalBusiness", "ListarEmpresa_Sucursal", ex);
                return null;
            }
        }


    }
}

