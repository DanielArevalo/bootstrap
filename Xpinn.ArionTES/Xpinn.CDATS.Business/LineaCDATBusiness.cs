using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Business
{
    public class LineaCDATBusiness : GlobalBusiness
    {
        private LineaCDATData DALineaCDAT;

        public LineaCDATBusiness()
        {
            DALineaCDAT = new LineaCDATData();
        }

        public LineaCDAT CrearLineaCDAT(LineaCDAT pLineaCDAT, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaCDAT = DALineaCDAT.CrearLineaCDAT(pLineaCDAT, pusuario);
                    if (pLineaCDAT != null)
                    {
                        RangoCDATData DARangoCDAT = new RangoCDATData();
                        foreach (RangoCDAT rango in pLineaCDAT.lstRangos)
                        {
                            rango.cod_lineacdat = pLineaCDAT.cod_lineacdat;
                            if (rango.cod_rango != null)
                                if (rango.tipo_tope != null)
                                    DARangoCDAT.CrearRangoCDAT(rango, pusuario);
                        }
                    }
                    ts.Complete();
                }

                return pLineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "CrearLineaCDAT", ex);
                return null;
            }
        }


        public LineaCDAT ModificarLineaCDAT(LineaCDAT pLineaCDAT, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineaCDAT = DALineaCDAT.ModificarLineaCDAT(pLineaCDAT, pusuario);
                    if (pLineaCDAT != null)
                    {
                        RangoCDATData DARangoCDAT = new RangoCDATData();
                        foreach (RangoCDAT rango in pLineaCDAT.lstRangos)
                        {
                            rango.cod_lineacdat = pLineaCDAT.cod_lineacdat;
                            if (rango.tipo_tope != null)
                            {
                                if (rango.cod_rango > 0)
                                    DARangoCDAT.ModificarRangoCDAT(rango, pusuario);
                                else
                                    DARangoCDAT.CrearRangoCDAT(rango, pusuario);
                            }
                        }
                    }
                    ts.Complete();
                }

                return pLineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "ModificarLineaCDAT", ex);
                return null;
            }
        }


        public void EliminarLineaCDAT(string pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineaCDAT.EliminarLineaCDAT(pId, pusuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "EliminarLineaCDAT", ex);
            }
        }


        public LineaCDAT ConsultarLineaCDAT(string pId, Usuario pusuario)
        {
            try
            {
                LineaCDAT LineaCDAT = new LineaCDAT();
                LineaCDAT = DALineaCDAT.ConsultarLineaCDAT(pId, pusuario);
                if (LineaCDAT != null)
                {
                    RangoCDATData DARangoCDAT = new RangoCDATData();
                    RangoCDAT prango = new RangoCDAT();
                    prango.cod_lineacdat = LineaCDAT.cod_lineacdat;
                    LineaCDAT.lstRangos = DARangoCDAT.ListarRangoCDAT(prango, pusuario);
                }
                return LineaCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "ConsultarLineaCDAT", ex);
                return null;
            }
        }

        public RangoCDAT ConsultarRangoCDATPorLineaYTipoTope(RangoCDAT rango, Usuario pusuario)
        {
            try
            {
                return DALineaCDAT.ConsultarRangoCDATPorLineaYTipoTope(rango, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "ConsultarRangoCDATPorLineaYTipoTope", ex);
                return null;
            }
        }

        public List<LineaCDAT> ListarLineaCDAT(LineaCDAT pLinea, Usuario pusuario)
        {
            try
            {
                return DALineaCDAT.ListarLineaCDAT(pLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "ListarLineaCDAT", ex);
                return null;
            }
        }

        public void EliminarRangoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    RangoCDATData DARangoCDAT = new RangoCDATData();
                    DARangoCDAT.EliminarRangoCDAT(pId, pusuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCDATBusiness", "EliminarRangoCDAT", ex);
            }
        }

    }

}