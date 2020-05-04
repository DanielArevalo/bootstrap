using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Business
{

    public class LineaProgramado_RangoBusiness : GlobalBusiness
    {

        private LineaProgramado_RangoData DALineaProgramado_Rango;

        public LineaProgramado_RangoBusiness()
        {
            DALineaProgramado_Rango = new LineaProgramado_RangoData();
        }

        public LineaProgramado_Rango CrearLineaProgramado_Rango(LineaProgramado_Rango pLineaProgramado_Rango, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_Rango = DALineaProgramado_Rango.CrearLineaProgramado_Rango(pLineaProgramado_Rango, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_Rango;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RangoBusiness", "CrearLineaProgramado_Rango", ex);
                return null;
            }
        }


        public LineaProgramado_Rango ModificarLineaProgramado_Rango(LineaProgramado_Rango pLineaProgramado_Rango, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_Rango = DALineaProgramado_Rango.ModificarLineaProgramado_Rango(pLineaProgramado_Rango, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_Rango;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RangoBusiness", "ModificarLineaProgramado_Rango", ex);
                return null;
            }
        }


        public void EliminarLineaProgramado_Rango(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaProgramado_Rango.EliminarLineaProgramado_Rango(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RangoBusiness", "EliminarLineaProgramado_Rango", ex);
            }
        }


        public LineaProgramado_Rango ConsultarLineaProgramado_Rango(Int64 pId, Usuario pusuario)
        {
            try
            {
                LineaProgramado_Rango LineaProgramado_Rango = new LineaProgramado_Rango();
                LineaProgramado_Rango = DALineaProgramado_Rango.ConsultarLineaProgramado_Rango(pId, pusuario);
                return LineaProgramado_Rango;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RangoBusiness", "ConsultarLineaProgramado_Rango", ex);
                return null;
            }
        }


        public List<LineaProgramado_Rango> ListarLineaProgramado_Rango(Int64 id_linea, Usuario pusuario)
        {
            try
            {
                return DALineaProgramado_Rango.ListarLineaProgramado_Rango(id_linea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RangoBusiness", "ListarLineaProgramado_Rango", ex);
                return null;
            }
        }


    }
}
