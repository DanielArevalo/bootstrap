using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;


namespace Xpinn.CDATS.Business
{
    public class AdministracionCDATBusiness : GlobalBusiness
    {
        AdministracionCDATData BAAdmi;

        public AdministracionCDATBusiness()
        {
            BAAdmi = new AdministracionCDATData();
        }



        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario,DateTime FechaVencimiento)
        {
            try
            {
                return BAAdmi.ListarCdat(filtro, FechaApe, vUsuario, FechaVencimiento);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "ListarCdat", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> ListarCdatCancelacion(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento, DateTime FechaVencimientoFinal)
        {
            try
            {
                return BAAdmi.ListarCdatCancelacion(filtro, FechaApe, vUsuario, FechaVencimiento,FechaVencimientoFinal);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "ListarCdatCancelacion", ex);
                return null;
            }
        }


        public List<AdministracionCDAT> ListarCdatProroga(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento)
        {
            try
            {
                return BAAdmi.ListarCdatProroga(filtro, FechaApe, vUsuario, FechaVencimiento);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "ListarCdatProroga", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> ListarIntereses(DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BAAdmi.ListarIntereses(FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "ListarCdat", ex);
                return null;
            }
        }

        //Anderson Acuña-- Reporte CDAT
        public List<Xpinn.CDATS.Entities.Cdat> ListarCDAT(Xpinn.CDATS.Entities.Cdat pCdta, Usuario pUsuario, String filtro)
        {
            try
            {
                return BAAdmi.ListarCDAT(pCdta, pUsuario, filtro);            
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditos", ex);
                return null;
            }
        }

        public AdministracionCDAT ModificarSoloNUmFISICO_CDAT(AdministracionCDAT pCdat, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAAdmi.ModificarSoloNUmFISICO_CDAT(pCdat, vUsuario);
                   
                    ts.Complete();
                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "ModificarSoloNUmFISICO_CDAT", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> getListarCdatBloquearBusiness(string filtro, DateTime FechaApe, Usuario vUsuario) 
        {
            try
            {
                return BAAdmi.getListarCdatBloquear(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "getListarCdatBloquearBusiness", ex);
                return null;
            }
        }

        public AdministracionCDAT getcdatByIdBusiness(Int64 Codigo, Usuario pUsuario) 
        {
            try
            {
                return BAAdmi.getcdatById(Codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "getListarCdatBloquearBusiness", ex);
                return null;
            }
        }

        public int InsertNovedadAndEstadoCdat(NovedadCDAT Entidad, Usuario pUsuario, Int32 codigoCDAT) 
        {
            int respuesta = 0;
            try
            {
                using (TransactionScope tr = new TransactionScope())
                {
                    BAAdmi.insertNovedadCDAT(Entidad, pUsuario);
                    respuesta = BAAdmi.updateCDAT(codigoCDAT, pUsuario);
                    tr.Complete();
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATBusiness", "getListarCdatBloquearBusiness", ex);
                return respuesta;
            }
        }


        public List<AdministracionCDAT> guardargrilla(AdministracionCDAT vprovision,List<AdministracionCDAT> lstLista, Usuario vUsuario)
        {
            try
            {
                return BAAdmi.guardarCausacion(vprovision, lstLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATService", "ModificarSoloNUmFISICO_CDAT", ex);
                return null;
            }
        }

        //Anderson Acuña-- Reporte 1020
        public List<Xpinn.CDATS.Entities.Cdat > Reporte_1020(Xpinn.CDATS.Entities.Cdat  pCdta, Usuario pUsuario, String filtro)
        {
            try
            {
                return BAAdmi.Reporte_1020(pCdta, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AdministracionCDATService", "ListarCreditos", ex);
                return null;
            }
        }

    }
}
