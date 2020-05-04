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
    public class AumentoSueldoService
    {

        private AumentoSueldoBusiness BOAumentoSueldo;
        private ExcepcionBusiness BOExcepcion;

        public AumentoSueldoService()
        {
            BOAumentoSueldo = new AumentoSueldoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250207"; } }

        public AumentoSueldo CrearAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario pusuario)
        {
            try
            {
                pAumentoSueldo = BOAumentoSueldo.CrearAumentoSueldo(pAumentoSueldo, pusuario);
                return pAumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "CrearAumentoSueldo", ex);
                return null;
            }
        }


        public AumentoSueldo ModificarAumentoSueldo(AumentoSueldo pAumentoSueldo, Usuario pusuario)
        {
            try
            {
                pAumentoSueldo = BOAumentoSueldo.ModificarAumentoSueldo(pAumentoSueldo, pusuario);
                return pAumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "ModificarAumentoSueldo", ex);
                return null;
            }
        }


        public void EliminarAumentoSueldo(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOAumentoSueldo.EliminarAumentoSueldo(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "EliminarAumentoSueldo", ex);
            }
        }


        public AumentoSueldo ConsultarAumentoSueldo(Int64 pId, Usuario pusuario)
        {
            try
            {
                AumentoSueldo AumentoSueldo = new AumentoSueldo();
                AumentoSueldo = BOAumentoSueldo.ConsultarAumentoSueldo(pId, pusuario);
                return AumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "ConsultarAumentoSueldo", ex);
                return null;
            }
        }

        public AumentoSueldo ConsultarAumentoSueldoXEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                AumentoSueldo AumentoSueldo = new AumentoSueldo();
                AumentoSueldo = BOAumentoSueldo.ConsultarAumentoSueldoXEmpleado(pId, pusuario);
                return AumentoSueldo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "ConsultarAumentoSueldoXEmpleado", ex);
                return null;
            }
        }


        public List<AumentoSueldo> ListarAumentoSueldo(string filtro, Usuario pusuario)
        {
            try
            {
                return BOAumentoSueldo.ListarAumentoSueldo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AumentoSueldoService", "ListarAumentoSueldo", ex);
                return null;
            }
        }


    }
}