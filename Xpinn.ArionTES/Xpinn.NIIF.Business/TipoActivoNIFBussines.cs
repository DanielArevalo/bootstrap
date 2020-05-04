using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.NIIF.Data;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Business
{
    /// <summary>
    /// Objeto de negocio para Parametros
    /// </summary>
    public class TipoActivoNIFBussines : GlobalData
    {
        private TipoActivoNIFData BATipo;

       
        public TipoActivoNIFBussines()
        {
            BATipo = new TipoActivoNIFData();
        }


        public TipoActivoNIF CrearTipoActivoNIF(TipoActivoNIF pActivo, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivo = BATipo.CrearTipoActivoNIF(pActivo, vUsuario,opcion);

                    ts.Complete();
                }

                return pActivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFBussines", "CrearTipoActivoNIF", ex);
                return null;
            }
        }



        public void EliminarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BATipo.EliminarTipoActivo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFBussines", "EliminarTipoActivo", ex);
            }
        }


        public TipoActivoNIF ConsultarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return BATipo.ConsultarTipoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFBussines", "ConsultarTipoActivo", ex);
                return null;
            }
        }


        public List<TipoActivoNIF> ListarTipoActivo(String filtro, Usuario pUsuario)
        {
            try
            {
                return BATipo.ListarTipoActivo(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoNIFBussines", "ListarTipoActivo", ex);
                return null;
            }
        }

    }
}