using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Services
{
    public class EmpresaExcluyenteServices : GlobalData
    {

        private EmpresaExcluyenteBusiness EmpresaExcluyenteBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public EmpresaExcluyenteServices()
        {
            EmpresaExcluyenteBusiness = new EmpresaExcluyenteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public List<EmpresaExcluyente> ListarEmpresaExcluyente(Int32 cod_empresa, Usuario vUsuario)
        {
            try
            {
                return EmpresaExcluyenteBusiness.ListarEmpresaExcluyente(cod_empresa, vUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteServices", "ListarEmpresaExcluyente", ex);
                return null;
            }
        }

        //CONSULTAR EMPRESAS EXCLUYENTES DE MODO INVERSO
        public List<EmpresaExcluyente> ListarEmpresaExcluyenteINV(Int32 cod_empresa, Usuario vUsuario)
        {
            try
            {
                return EmpresaExcluyenteBusiness.ListarEmpresaExcluyenteINV(cod_empresa, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteBusiness", "ListarEmpresaExcluyenteINV", ex);
                return null;
            }
        }

    }
}


