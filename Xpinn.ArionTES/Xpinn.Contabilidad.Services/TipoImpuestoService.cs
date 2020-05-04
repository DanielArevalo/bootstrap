using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Business;

namespace Xpinn.Contabilidad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoImpuestoService
    {

        private TipoImpuestoBusiness BOTipoImpuesto;
        private ExcepcionBusiness BOExcepcion;

        public TipoImpuestoService()
        {
            BOTipoImpuesto = new TipoImpuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31101"; } }

        public TipoImpuesto CrearTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                pTipoImpuesto = BOTipoImpuesto.CrearTipoImpuesto(pTipoImpuesto, pusuario);
                return pTipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoService", "CrearTipoImpuesto", ex);
                return null;
            }
        }


        public TipoImpuesto ModificarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                pTipoImpuesto = BOTipoImpuesto.ModificarTipoImpuesto(pTipoImpuesto, pusuario);
                return pTipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoService", "ModificarTipoImpuesto", ex);
                return null;
            }
        }


        public void EliminarTipoImpuesto(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOTipoImpuesto.EliminarTipoImpuesto(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoService", "EliminarTipoImpuesto", ex);
            }
        }


        public TipoImpuesto ConsultarTipoImpuesto(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoImpuesto TipoImpuesto = new TipoImpuesto();
                TipoImpuesto = BOTipoImpuesto.ConsultarTipoImpuesto(pId, pusuario);
                return TipoImpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoService", "ConsultarTipoImpuesto", ex);
                return null;
            }
        }


        public List<TipoImpuesto> ListarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario pusuario)
        {
            try
            {
                return BOTipoImpuesto.ListarTipoImpuesto(pTipoImpuesto, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoImpuestoService", "ListarTipoImpuesto", ex);
                return null;
            }
        }


    }
}