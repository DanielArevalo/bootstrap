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
    public class EmpresaExcluyenteBusiness : GlobalData
    {

        private EmpresaExcluyenteData DAEmpresa;

        public EmpresaExcluyenteBusiness()
        {
            DAEmpresa = new EmpresaExcluyenteData();
        }


        public List<EmpresaExcluyente> ListarEmpresaExcluyente(Int32 cod_empresa, Usuario vUsuario)
        {
            try
            {
                return DAEmpresa.ListarEmpresaExcluyente(cod_empresa, vUsuario);

            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteBusiness", "ListarEmpresaExcluyente", ex);
                return null;
            }
        }

        //CONSULTAR EMPRESAS EXCLUYENTES DE MODO INVERSO
        public List<EmpresaExcluyente> ListarEmpresaExcluyenteINV(Int32 cod_empresa, Usuario vUsuario)
        {
            try
            {
                return DAEmpresa.ListarEmpresaExcluyenteINV(cod_empresa, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteBusiness", "ListarEmpresaExcluyenteINV", ex);
                return null;
            }
        }


    }
}


