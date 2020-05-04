using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Business
{

    public class UIAF_ExoneradosBusiness : GlobalBusiness
    {

        private UIAF_ExoneradosData DAUIAF_Exonerados;

        public UIAF_ExoneradosBusiness()
        {
            DAUIAF_Exonerados = new UIAF_ExoneradosData();
        }

        public UIAF_Exonerados CrearUIAF_Exonerados(UIAF_Exonerados pUIAF_Exonerados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF_Exonerados = DAUIAF_Exonerados.CrearUIAF_Exonerados(pUIAF_Exonerados, pusuario);

                    ts.Complete();

                }

                return pUIAF_Exonerados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "CrearUIAF_Exonerados", ex);
                return null;
            }
        }


        public UIAF_Exonerados ModificarUIAF_Exonerados(UIAF_Exonerados pUIAF_Exonerados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF_Exonerados = DAUIAF_Exonerados.ModificarUIAF_Exonerados(pUIAF_Exonerados, pusuario);

                    ts.Complete();

                }

                return pUIAF_Exonerados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "ModificarUIAF_Exonerados", ex);
                return null;
            }
        }


        public void EliminarUIAF_Exonerados(string pFiltro,Int64 pIdreporte, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUIAF_Exonerados.EliminarUIAF_Exonerados(pFiltro, pIdreporte, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "EliminarUIAF_Exonerados", ex);
            }
        }


        public UIAF_Exonerados ConsultarUIAF_Exonerados(Int64 pId, Usuario pusuario)
        {
            try
            {
                UIAF_Exonerados UIAF_Exonerados = new UIAF_Exonerados();
                UIAF_Exonerados = DAUIAF_Exonerados.ConsultarUIAF_Exonerados(pId, pusuario);
                return UIAF_Exonerados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "ConsultarUIAF_Exonerados", ex);
                return null;
            }
        }


        public List<UIAF_Exonerados> ListarUIAF_Exonerados(Int64 pIdreporte, Usuario pusuario)
        {
            try
            {
                return DAUIAF_Exonerados.ListarUIAF_Exonerados(pIdreporte, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "ListarUIAF_Exonerados", ex);
                return null;
            }
        }
        public List<UIAF_Exonerados> ListaUIAFExoneradosDate(DateTime pFechaCorte, Usuario pusuario)
        {
            try
            {
                return DAUIAF_Exonerados.ListarUIAF_ExoneradosDate(pFechaCorte, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ExoneradosBusiness", "ListaUIAFExoneradosDate", ex);
                return null;
            }
        }

        
    }
}
