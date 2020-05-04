using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;


namespace Xpinn.CDATS.Business
{
    public class ProrrogaCDATBusiness : GlobalBusiness
    {
        ProrrogaCDATData BAProrro;

        public ProrrogaCDATBusiness()
        {
            BAProrro = new ProrrogaCDATData();
        }


        public Xpinn.CDATS.Entities.Cdat ModificarCDATProrroga(Xpinn.CDATS.Entities.Cdat pCdat, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAProrro.ModificarCDATProrroga(pCdat, vUsuario);

                    ts.Complete();
                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProrrogaCDATBusiness", "ModificarCDATProrroga", ex);
                return null;
            }
        }


        public ProrrogaCDAT CrearCDATProrroga(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProrro = BAProrro.CrearCDATProrroga(pProrro, vUsuario);

                    ts.Complete();
                }
                return pProrro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProrrogaCDATBusiness", "CrearCDATProrroga", ex);
                return null;
            }
        }


        public ProrrogaCDAT ModificarProrroga_CDAT(ProrrogaCDAT pProrro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProrro = BAProrro.ModificarProrroga_CDAT(pProrro, vUsuario);

                    ts.Complete();
                }
                return pProrro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProrrogaCDATBusiness", "ModificarProrroga_CDAT", ex);
                return null;
            }
        }


        public ProrrogaCDAT ConsultarCDATProrroga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ProrrogaCDAT pProrro = new ProrrogaCDAT();
                pProrro = BAProrro.ConsultarCDATProrroga(pId, vUsuario);
                return pProrro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProrrogaCDATBusiness", "ConsultarCDATProrroga", ex);
                return null;
            }
        }

        public Boolean SolicitarRenovacionCdat(List<SolicitudRenovacion> lstRenovacion, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstRenovacion != null)
                    {
                        if (lstRenovacion.Count > 0)
                        {
                            foreach (SolicitudRenovacion nRenovacion in lstRenovacion)
                            {
                                string Error = string.Empty;
                                SolicitudRenovacion pEntidad = new SolicitudRenovacion();
                                pEntidad = BAProrro.CrearSolicitudRenovacion(nRenovacion, ref Error, vUsuario);
                                if (!string.IsNullOrEmpty(Error))
                                    return false;
                            }
                        }
                    }
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }
    }
}
