using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Services
{

    public class EmpresaRecaudoServices
    {

        private EmpresaRecaudoBusiness BOEmpresaRecaudoBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public EmpresaRecaudoServices()
        {
            BOEmpresaRecaudoBusiness = new EmpresaRecaudoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180201"; } }


        public EmpresaRecaudo CrearEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.CrearEmpresaRecaudo(pEmpresaRecaudo, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "CrearEmpresaRecaudo", ex);
                return null;
            }
        }

        public EmpresaRecaudo ModificarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario vUsuario)
        {

            try
            {
                return BOEmpresaRecaudoBusiness.ModificarEmpresaRecaudo(pEmpresaRecaudo, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ModificarEmpresaRecaudo", ex);
                return null;
            }
        }


        public void EliminarEmpresaRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEmpresaRecaudoBusiness.EliminarEmpresaRecaudo(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "EliminarEmpresaRecaudo", ex);
            }
        }



        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ListarEmpresaRecaudo(pEmpresaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }



        public List<EmpresaRecaudo> ListarEmpresaRecaudoPersona(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ListarEmpresaRecaudoPersona(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ListarEmpresaRecaudo", ex);
                return null;
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(EmpresaRecaudo pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ConsultarEMPRESARECAUDO(pEntidad, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ConsultarEMPRESARECAUDO", ex);
                return null;
            }
        }

        public EmpresaRecaudo ConsultarEMPRESARECAUDO(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ConsultarEMPRESARECAUDO(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ConsultarEMPRESARECAUDO", ex);
                return null;
            }
        }


        public List<EMPRESARECAUDO_CONCEPTO> ListarEMPRESACONCEPTO(EMPRESARECAUDO_CONCEPTO pConcepto, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ListarEMPRESACONCEPTO(pConcepto, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ListarEMPRESACONCEPTO", ex);
                return null;
            }
        }


        public List<EmpresaRecaudo_Programacion> ListarEMPRESAPROGRAMACION(EmpresaRecaudo_Programacion pProgramacion, Usuario vUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ListarEMPRESAPROGRAMACION(pProgramacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ListarEMPRESAPROGRAMACION", ex);
                return null;
            }
        }

        public EmpresaRecaudo_Programacion ConsultarEMPRESAPROGRAMACION(Int64 pCodEmpresa, Int64 pTipoLista, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.ConsultarEMPRESAPROGRAMACION(pCodEmpresa, pTipoLista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "ConsultarEMPRESAPROGRAMACION", ex);
                return null;
            }
        }

        public void EliminarEmpresaPrograma(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEmpresaRecaudoBusiness.EliminarEmpresaPrograma(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "EliminarEmpresaPrograma", ex);
            }
        }

        public void EliminarEmpresaConcepto(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEmpresaRecaudoBusiness.EliminarEmpresaConcepto(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "EliminarEmpresaConcepto", ex);
            }
        }

        public bool VerificarQueYaNoSeHallaCargadoLaMismaNovedad(EmpresaRecaudo empresa, Usuario usuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.VerificarQueYaNoSeHallaCargadoLaMismaNovedad(empresa, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "VerificarQueYaNoSeHallaCargadoLaMismaNovedad", ex);
                return false;
            }
        }

        public List<EmpresaRecaudo_Programacion> GenerarPeriodos(Int64 pCodEmpresa, Int64 pTipoLista, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.GenerarPeriodos(pCodEmpresa, pTipoLista, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "EliminarEmpresaConcepto", ex);
                return null;
            }
        }


        public DateTime? CalcularFechaInicialNovedad(Int64 pCodEmpresa, Int64 pTipoLista, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOEmpresaRecaudoBusiness.CalcularFechaInicialNovedad(pCodEmpresa, pTipoLista, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaRecaudosService", "CalcularFechaInicialNovedad", ex);
                return null;
            }
        }

    }
}
