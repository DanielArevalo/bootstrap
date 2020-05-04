using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;


namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AdministracionCDATService
    {
        AdministracionCDATBusiness BOAdmi ;
        ExcepcionBusiness BOException;

        public AdministracionCDATService()
        {
            BOAdmi = new AdministracionCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaADM { get { return "220302"; } }
        public string CodigoProgramaCDAT{ get { return "220317"; } }
        public string CodigoProgramaListarCDAT { get { return "220318"; } }



        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento)
        {
            try
            {
                return BOAdmi.ListarCdat(filtro, FechaApe, vUsuario, FechaVencimiento);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ListarCdat", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> ListarCdatCancelacion(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento,DateTime  FechaVencimientoFinal)
        {
            try
            {
                return BOAdmi.ListarCdatCancelacion(filtro, FechaApe, vUsuario, FechaVencimiento,FechaVencimientoFinal);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ListarCdatCancelacion", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> ListarCdatProroga(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimiento)
        {
            try
            {
                return BOAdmi.ListarCdatProroga(filtro, FechaApe, vUsuario, FechaVencimiento);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ListarCdatProroga", ex);
                return null;
            }
        }

        public List<AdministracionCDAT> ListarIntereses(DateTime FechaApe, Usuario vUsuario)
        {
            try
            {
                return BOAdmi.ListarIntereses(FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ListarCdat", ex);
                return null;
            }
        }

        //Anderson acuña--- Reporte CDAT
        public List<Cdat> ListarCDAT(Cdat pCdta, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAdmi.ListarCDAT(pCdta, pUsuario, filtro);
                 
            }
            catch (Exception ex)
            {
                BOException.Throw("CdtaService", "ListarCdtas", ex);
                return null;
            }
        }


        public AdministracionCDAT ModificarSoloNUmFISICO_CDAT(AdministracionCDAT pCdat, Usuario vUsuario)
        {
            try
            {
                return BOAdmi.ModificarSoloNUmFISICO_CDAT(pCdat, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ModificarSoloNUmFISICO_CDAT", ex);
                return null;
            }
        }


        public List<AdministracionCDAT> guardargrilla( AdministracionCDAT vprovision, List<AdministracionCDAT> lstLista, Usuario vUsuario)
        {
            try
            {
               return BOAdmi.guardargrilla(vprovision, lstLista, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ModificarSoloNUmFISICO_CDAT", ex);
                return null;
            }
        }


        public List<AdministracionCDAT> getListarCdatBloquearServices(string filtro, DateTime FechaApe, Usuario vUsuario) 
        {
            try
            {
                return BOAdmi.getListarCdatBloquearBusiness(filtro, FechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "getListarCdatBloquearServices", ex);
                return null;
            }
        }

        public AdministracionCDAT getcdatByIdBusiness(Int64 Codigo, Usuario pUsuario) 
        {
            try
            {
                return BOAdmi.getcdatByIdBusiness(Codigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "getListarCdatBloquearServices", ex);
                return null;
            }
        }

        public int InsertNovedadAndEstadoCdatSerrvices(NovedadCDAT Entidad, Usuario pUsuario, Int32 codigoCDAT)
        {
            int respuesta = 0;
            try
            {
                return BOAdmi.InsertNovedadAndEstadoCdat(Entidad, pUsuario, codigoCDAT);
            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "InsertNovedadAndEstadoCdatSerrvices", ex);
                return respuesta;
            }
        }

        //Anderson acuña--- Reporte 1020
        public List<Cdat> Reporte_1020(Cdat pCdta, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAdmi.Reporte_1020(pCdta, pUsuario, filtro);

            }
            catch (Exception ex)
            {
                BOException.Throw("AdministracionCDATService", "ListarCdtas", ex);
                return null;
            }
        }


    }
}
