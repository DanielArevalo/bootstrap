using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Business
{

    public class LineaProgramado_RequisitoBusiness : GlobalBusiness
    {

        private LineaProgramado_RequisitoData DALineaProgramado_Requisito;

        public LineaProgramado_RequisitoBusiness()
        {
            DALineaProgramado_Requisito = new LineaProgramado_RequisitoData();
        }

        public LineaProgramado_Requisito CrearLineaProgramado_Requisito(LineaProgramado_Requisito pLineaProgramado_Requisito, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_Requisito = DALineaProgramado_Requisito.CrearLineaProgramado_Requisito(pLineaProgramado_Requisito, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_Requisito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "CrearLineaProgramado_Requisito", ex);
                return null;
            }
        }


        public LineaProgramado_Requisito ModificarLineaProgramado_Requisito(LineaProgramado_Requisito pLineaProgramado_Requisito, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaProgramado_Requisito = DALineaProgramado_Requisito.ModificarLineaProgramado_Requisito(pLineaProgramado_Requisito, pusuario);

                    ts.Complete();

                }

                return pLineaProgramado_Requisito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "ModificarLineaProgramado_Requisito", ex);
                return null;
            }
        }


        public void EliminarLineaProgramado_Requisito(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaProgramado_Requisito.EliminarLineaProgramado_Requisito(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "EliminarLineaProgramado_Requisito", ex);
            }
        }


        public LineaProgramado_Requisito ConsultarLineaProgramado_Requisito(Int64 pId, Usuario pusuario)
        {
            try
            {
                LineaProgramado_Requisito LineaProgramado_Requisito = new LineaProgramado_Requisito();
                LineaProgramado_Requisito = DALineaProgramado_Requisito.ConsultarLineaProgramado_Requisito(pId, pusuario);
                return LineaProgramado_Requisito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "ConsultarLineaProgramado_Requisito", ex);
                return null;
            }
        }


        public List<LineaProgramado_Requisito> ListarLineaProgramado_Requisito(Int64 pId_rango, string p_idlinea, Usuario pusuario)
        {
            try
            {
                return DALineaProgramado_Requisito.ListarLineaProgramado_Requisito(pId_rango, p_idlinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "ListarLineaProgramado_Requisito", ex);
                return null;
            }
        }

        public LineaProgramado_Requisito ConsultarLineaProgramadoRango(Int64 pId_plazo, string p_idlinea, Usuario pusuario)
        {
            try
            {
                return DALineaProgramado_Requisito.ConsultarLineaProgramadoRango(pId_plazo, p_idlinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaProgramado_RequisitoBusiness", "ConsultarLineaProgramadoRango", ex);
                return null;
            }
        }


    }
}
