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
    public class EmpresaEstructuraCargaServices : GlobalData
    {

        private EmpresaEstructuraCargaBusiness BOEmpresaEstructuraCarga;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public EmpresaEstructuraCargaServices()
        {
            BOEmpresaEstructuraCarga = new EmpresaEstructuraCargaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180201"; } }


        public EmpresaEstructuraCarga CrearEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            try
            {
                pEmpresaEstructuraCarga = BOEmpresaEstructuraCarga.CrearEmpresaEstructuraCarga(pEmpresaEstructuraCarga, vUsuario);
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
                pEmpresaEstructuraCarga = BOEmpresaEstructuraCarga.ModificarEmpresaEstructuraCarga(pEmpresaEstructuraCarga, vUsuario);

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
                BOEmpresaEstructuraCarga.EliminarEmpresaEstructuraCarga(pId, vUsuario);
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


