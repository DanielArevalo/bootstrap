using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class InversionesBusiness : GlobalBusiness
    {
        private InversionesData DAInversiones;


        public InversionesBusiness()
        {
            DAInversiones = new InversionesData();
        }

        public TipoInversiones CrearTipoInversion(TipoInversiones pTipoInversion, int pOpcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoInversion = DAInversiones.CrearTipoInversion(pTipoInversion, pOpcion, vUsuario);
                    ts.Complete();
                }
                return pTipoInversion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "CrearTipoInversion", ex);
                return null;
            }
        }

        public List<TipoInversiones> ListarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAInversiones.ListarTipoInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "ListarTipoInversiones", ex);
                return null;
            }
        }

        public TipoInversiones ConsultarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAInversiones.ConsultarTipoInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "ConsultarTipoInversiones", ex);
                return null;
            }
        }

        public void EliminarTipoInversiones(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInversiones.EliminarTipoInversiones(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "EliminarTipoInversiones", ex);
            }
        }



        public Inversiones CrearInversiones(Inversiones pInversiones, int pOpcion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInversiones = DAInversiones.CrearInversiones(pInversiones, pOpcion, vUsuario);
                    ts.Complete();
                }
                return pInversiones;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "CrearInversiones", ex);
                return null;
            }
        }

        public Inversiones ConsultarInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAInversiones.ConsultarInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "ConsultarInversiones", ex);
                return null;
            }
        }

        public List<Inversiones> ListarInversiones(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAInversiones.ListarInversiones(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "ListarInversiones", ex);
                return null;
            }
        }


        public void EliminarInversiones(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInversiones.EliminarInversiones(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InversionesBusiness", "EliminarInversiones", ex);
            }
        }



    }
}

