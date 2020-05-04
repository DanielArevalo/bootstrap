using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
//using Xpinn.Contabilidad.Business;
//using System.Web.UI.WebControls;

namespace Xpinn.Aportes.Business
{
    public class OperacionApoBusiness : GlobalData
    {

        private OperacionApoData DAOperacion;

        public OperacionApoBusiness()
        {
            DAOperacion = new OperacionApoData();
        }


        public List<OperacionApo> ContabilizarOperacion(List<OperacionApo> pLstOperacion, ref String pError, Usuario pUsuario)
        {
            try
            {
                Int64 pcod_ope;
                Int64 ptip_ope;
                DateTime pfecha;
                Int64 pcod_ofi;
                Int64 pcod_persona;
                Int64 pcod_proceso;
                Int64 pnum_comp;
                Int64 ptipo_comp;
                int contador = 0;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (OperacionApo rFila in pLstOperacion)
                    {
                        if (rFila.seleccionar == true)
                        {
                            pcod_ope = rFila.cod_ope;
                            ptip_ope = rFila.tipo_ope;
                            pcod_ofi = rFila.cod_ofi;
                            pcod_persona = rFila.cod_cliente;
                            pfecha = rFila.fecha_oper;
                            if (rFila.cod_proceso == null)
                                pcod_proceso = 3;
                            else
                                pcod_proceso = Convert.ToInt64(rFila.cod_proceso);
                            pnum_comp = -2;
                            ptipo_comp = -2;
                            if (DAOperacion.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, pUsuario) == false)
                                ts.Dispose();
                            pLstOperacion[contador].num_comp = pnum_comp;
                            pLstOperacion[contador].tipo_comp = ptipo_comp;
                        }
                        contador += 1;
                    }
                    ts.Complete();
                }
                return pLstOperacion;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return pLstOperacion;
            }
        }


        public OperacionApo GrabarOperacion(OperacionApo pEntidad, Usuario pUsuario)
        {

            try
            {
                return DAOperacion.GrabarOperacion(pEntidad, pUsuario);
            }
            catch
            {
                return null;
            }
        }



    }
}


