using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Business
{
    public class EmpresaEstructuraCargaBusiness : GlobalData
    {

        private EmpresaEstructuraCargaData BOEmpresaEstructuraCarga;

        public EmpresaEstructuraCargaBusiness()
        {
            BOEmpresaEstructuraCarga = new EmpresaEstructuraCargaData();
        }


        public EmpresaEstructuraCarga CrearEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresaEstructuraCarga = BOEmpresaEstructuraCarga.CrearEmpresaEstructuraCarga(pEmpresaEstructuraCarga, vUsuario);
                   
                    ts.Complete();
                }

                return pEmpresaEstructuraCarga;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaEstructuraCargasBusiness", "CrearEmpresaEstructuraCarga", ex);
                return null;
            }
        }

        public EmpresaEstructuraCarga ModificarEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresaEstructuraCarga = BOEmpresaEstructuraCarga.ModificarEmpresaEstructuraCarga(pEmpresaEstructuraCarga, vUsuario);

                    ts.Complete();
                }

                return pEmpresaEstructuraCarga;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaEstructuraCargasBusiness", "ModificarEmpresaEstructuraCarga", ex);
                return null;
            }
        }


        public void EliminarEmpresaEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOEmpresaEstructuraCarga.EliminarEmpresaEstructuraCarga(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaEstructuraCargasBusiness", "EliminarEmpresaEstructuraCarga", ex);
            }
        }


        public List<EmpresaEstructuraCarga> ListarEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaEstructuraCarga.ListarEmpresaEstructuraCarga(pEmpresaEstructuraCarga, pUsuario);

            }
            catch
            {
                return null;
            }
        }

        public EmpresaEstructuraCarga ConsultarEmpresaEstructuraCarga(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaEstructuraCarga.ConsultarEmpresaEstructuraCarga(pId, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaEstructuraCargasBusiness", "ConsultarEmpresaEstructuraCarga", ex);
                return null;
            }
        }


    }
}


