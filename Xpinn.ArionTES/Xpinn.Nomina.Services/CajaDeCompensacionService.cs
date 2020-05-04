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
    public class CajaDeCompensacionService
    {
        private CajaDeCompensacionBussines BODIRCAJADECOMPENSACION;
        private ExcepcionBusiness BOExcepcion;

        public CajaDeCompensacionService()
        {
            BODIRCAJADECOMPENSACION = new CajaDeCompensacionBussines();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250101"; } }

        public CajaDeCompensacion CrearDIRCAJADECOMPENSACION(CajaDeCompensacion pDIRCAJADECOMPENSACION, Usuario pusuario)
        {
            try
            {
                pDIRCAJADECOMPENSACION = BODIRCAJADECOMPENSACION.CrearDIRCAJADECOMPENSACION(pDIRCAJADECOMPENSACION, pusuario);
                return pDIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "CrearDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public CajaDeCompensacion ModificarDIRCAJADECOMPENSACION(CajaDeCompensacion pDIRCAJADECOMPENSACION, Usuario pusuario)
        {
            try
            {
                pDIRCAJADECOMPENSACION = BODIRCAJADECOMPENSACION.ModificarDIRCAJADECOMPENSACION(pDIRCAJADECOMPENSACION, pusuario);
                return pDIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "ModificarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public void EliminarDIRCAJADECOMPENSACION(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODIRCAJADECOMPENSACION.EliminarDIRCAJADECOMPENSACION(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "EliminarDIRCAJADECOMPENSACION", ex);
            }
        }

        public CajaDeCompensacion ConsultarDIRCAJADECOMPENSACION(Int64 pId, Usuario pusuario)
        {
            try
            {
                CajaDeCompensacion DIRCAJADECOMPENSACION = new CajaDeCompensacion();
                DIRCAJADECOMPENSACION = BODIRCAJADECOMPENSACION.ConsultarDIRCAJADECOMPENSACION(pId, pusuario);
                return DIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "ConsultarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public CajaDeCompensacion ConsultarDatosDIRCAJADECOMPENSACION(string pId, Usuario pusuario)
        {
            try
            {
                CajaDeCompensacion DIRCAJADECOMPENSACION = new CajaDeCompensacion();
                DIRCAJADECOMPENSACION = BODIRCAJADECOMPENSACION.ConsultarDatosDIRCAJADECOMPENSACION(pId, pusuario);
                return DIRCAJADECOMPENSACION;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "ConsultarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

        public List<CajaDeCompensacion> ListarDIRCAJADECOMPENSACION(string filtro, Usuario pusuario)
        {
            try
            {
                return BODIRCAJADECOMPENSACION.ListarDIRCAJADECOMPENSACION(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaDeCompensacionService", "ListarDIRCAJADECOMPENSACION", ex);
                return null;
            }
        }

    }
}
