using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Business;

namespace Xpinn.Caja.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivoDevCheService
    {

        private MotivoDevCheBusiness BOMotivoDevChe;
        private ExcepcionBusiness BOExcepcion;

        public MotivoDevCheService()
        {
            BOMotivoDevChe = new MotivoDevCheBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120114"; } }

        public MotivoDevChe CrearMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                pMotivoDevChe = BOMotivoDevChe.CrearMotivoDevChe(pMotivoDevChe, pusuario);
                return pMotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheService", "CrearMotivoDevChe", ex);
                return null;
            }
        }


        public MotivoDevChe ModificarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                pMotivoDevChe = BOMotivoDevChe.ModificarMotivoDevChe(pMotivoDevChe, pusuario);
                return pMotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheService", "ModificarMotivoDevChe", ex);
                return null;
            }
        }


        public void EliminarMotivoDevChe(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOMotivoDevChe.EliminarMotivoDevChe(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheService", "EliminarMotivoDevChe", ex);
            }
        }


        public MotivoDevChe ConsultarMotivoDevChe(Int64 pId, Usuario pusuario)
        {
            try
            {
                MotivoDevChe MotivoDevChe = new MotivoDevChe();
                MotivoDevChe = BOMotivoDevChe.ConsultarMotivoDevChe(pId, pusuario);
                return MotivoDevChe;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheService", "ConsultarMotivoDevChe", ex);
                return null;
            }
        }


        public List<MotivoDevChe> ListarMotivoDevChe(MotivoDevChe pMotivoDevChe, Usuario pusuario)
        {
            try
            {
                return BOMotivoDevChe.ListarMotivoDevChe(pMotivoDevChe, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoDevCheService", "ListarMotivoDevChe", ex);
                return null;
            }
        }


    }
}
