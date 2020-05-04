using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Business
{

    public class LineaProgramado_TasaBusiness : GlobalBusiness
    {

        private LineaProgramado_TasaData DALineaProgramado_tasa;

        public LineaProgramado_TasaBusiness()
        {
            DALineaProgramado_tasa = new LineaProgramado_TasaData();
        }

        public LineaProgramado_Tasa CrearLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_tasa = DALineaProgramado_tasa.CrearLineaProgramado_tasa(pLineaProgramado_tasa, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_tasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_tasaBusiness", "CrearLineaProgramado_tasa", ex);
                return null;
            }
        }


        public LineaProgramado_Tasa ModificarLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_tasa = DALineaProgramado_tasa.ModificarLineaProgramado_tasa(pLineaProgramado_tasa, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_tasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_tasaBusiness", "ModificarLineaProgramado_tasa", ex);
                return null;
            }
        }


        public void EliminarLineaProgramado_tasa(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaProgramado_tasa.EliminarLineaProgramado_tasa(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_tasaBusiness", "EliminarLineaProgramado_tasa", ex);
            }
        }


        public LineaProgramado_Tasa ConsultarLineaProgramado_tasa(Int64 pId_rango, string p_idlinea, Usuario pusuario)
        {
            try
            {
                LineaProgramado_Tasa LineaProgramado_tasa = new LineaProgramado_Tasa();
                LineaProgramado_tasa = DALineaProgramado_tasa.ConsultarLineaProgramado_tasa(pId_rango,p_idlinea, pusuario);
                return LineaProgramado_tasa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_tasaBusiness", "ConsultarLineaProgramado_tasa", ex);
                return null;
            }
        }


        public List<LineaProgramado_Tasa> ListarLineaProgramado_tasa(LineaProgramado_Tasa pLineaProgramado_tasa, Usuario pusuario)
        {
            try
            {
                return DALineaProgramado_tasa.ListarLineaProgramado_tasa(pLineaProgramado_tasa, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_tasaBusiness", "ListarLineaProgramado_tasa", ex);
                return null;
            }
        }


    }
}

