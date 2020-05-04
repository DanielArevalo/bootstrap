using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivoExcepcionService
    {

        private MotivoExcepcionBusiness BOMotivoExcepcion;
        private ExcepcionBusiness BOExcepcion;

        public MotivoExcepcionService()
        {
            BOMotivoExcepcion = new MotivoExcepcionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170205"; } }

        public MotivoExcepcion CrearMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
        {
            try
            {
                pMotivoExcepcion = BOMotivoExcepcion.CrearMotivoExcepcion(pMotivoExcepcion, pusuario);
                return pMotivoExcepcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoExcepcionService", "CrearMotivoExcepcion", ex);
                return null;
            }
        }


        public MotivoExcepcion ModificarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
        {
            try
            {
                pMotivoExcepcion = BOMotivoExcepcion.ModificarMotivoExcepcion(pMotivoExcepcion, pusuario);
                return pMotivoExcepcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoExcepcionService", "ModificarMotivoExcepcion", ex);
                return null;
            }
        }


        public void EliminarMotivoExcepcion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOMotivoExcepcion.EliminarMotivoExcepcion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoExcepcionService", "EliminarMotivoExcepcion", ex);
            }
        }


        public MotivoExcepcion ConsultarMotivoExcepcion(Int64 pId, Usuario pusuario)
        {
            try
            {
                MotivoExcepcion MotivoExcepcion = new MotivoExcepcion();
                MotivoExcepcion = BOMotivoExcepcion.ConsultarMotivoExcepcion(pId, pusuario);
                return MotivoExcepcion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoExcepcionService", "ConsultarMotivoExcepcion", ex);
                return null;
            }
        }


        public List<MotivoExcepcion> ListarMotivoExcepcion(MotivoExcepcion pMotivoExcepcion, Usuario pusuario)
        {
            try
            {
                return BOMotivoExcepcion.ListarMotivoExcepcion(pMotivoExcepcion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoExcepcionService", "ListarMotivoExcepcion", ex);
                return null;
            }
        }


    }
}