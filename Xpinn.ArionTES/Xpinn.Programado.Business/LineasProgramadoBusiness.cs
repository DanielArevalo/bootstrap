using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Data;

namespace Xpinn.Programado.Business
{
    public class LineasProgramadoBusiness : GlobalBusiness
    {
        private LineasProgramadoData DALineasPro;


        public LineasProgramadoBusiness()
        {
            DALineasPro = new LineasProgramadoData();
        }


        public LineasProgramado CrearMod_LineasProgramado(LineasProgramado pLineas, List<LineaProgramado_Rango> ListaRango, Usuario vUsuario,Int32 opcion)
        {
            LineaProgramado_Rango LPRango = new LineaProgramado_Rango();
            LineaProgramado_RangoData RangoData = new LineaProgramado_RangoData();

            LineaProgramado_Requisito LPRequisito = new LineaProgramado_Requisito();
            LineaProgramado_RequisitoData RequisitoData = new LineaProgramado_RequisitoData();

            LineaProgramado_Tasa LPTasa= new LineaProgramado_Tasa();
            LineaProgramado_TasaData TasaData = new LineaProgramado_TasaData();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLineas = DALineasPro.CrearMod_LineasProgramado(pLineas, vUsuario, opcion);
                    ts.Complete();
                }
                int sec_rango = 0;
                string[] rango_id = new string[ListaRango.Count];
                foreach (LineaProgramado_Rango x in ListaRango)
                {
                    if (x.ListaRequisitos != null)
                    {
                        if (x.idrango > 0)
                        {
                            string[] requisitos_id = new string[x.ListaRequisitos.Count];

                            int sec = 0;
                            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                            {
                                LPRango = RangoData.ModificarLineaProgramado_Rango(x, vUsuario);
                                ts.Complete();
                            }
                            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                            {
                                LPTasa = TasaData.ModificarLineaProgramado_tasa(x.LineaTasa, vUsuario);
                                ts.Complete();
                            }
                            rango_id[sec_rango] = "" + LPRango.idrango + "";


                            foreach (LineaProgramado_Requisito y in x.ListaRequisitos)
                            {
                                if (y.idrequisito != 0)
                                {
                                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                                    {
                                        y.cod_linea_programado = x.cod_linea_programado;
                                        y.idrango = x.idrango;
                                        LPRequisito = RequisitoData.ModificarLineaProgramado_Requisito(y, vUsuario);
                                        ts.Complete();
                                    }
                                    requisitos_id[sec] = "" + LPRequisito.idrequisito + "";
                                    sec += 1;
                                }
                                else
                                {
                                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                                    {
                                        y.idrango = x.idrango;
                                        LPRequisito = RequisitoData.CrearLineaProgramado_Requisito(y, vUsuario);
                                        ts.Complete();
                                    }
                                    requisitos_id[sec] = "" + LPRequisito.idrequisito + "";
                                    sec += 1;
                                }
                            }
                                if (requisitos_id.Count() == 0)
                                {
                                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                                    {
                                        RequisitoData.EliminarLineaProgramado_Requisitos(requisitos_id[0], x.idrango, pLineas.cod_linea_programado, vUsuario);
                                        ts.Complete();
                                    }
                                }
                                else
                                {
                                    string filtro = "";
                                    filtro = "" + requisitos_id[0] + "";
                                    for (int i = 1; i <= requisitos_id.Count() - 1; i++)
                                    {
                                        filtro = filtro + "," + requisitos_id[i] + "";
                                    }
                                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                                    {
                                        RequisitoData.EliminarLineaProgramado_Requisitos(filtro, x.idrango, pLineas.cod_linea_programado, vUsuario);
                                        ts.Complete();
                                    }
                                }
                            


                        }
                        else
                        {
                            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                            {
                                LPRango = RangoData.CrearLineaProgramado_Rango(x, vUsuario);
                                ts.Complete();
                            }
                            rango_id[sec_rango] =  LPRango.idrango.ToString();
                            x.LineaTasa.idrango = LPRango.idrango;
                            x.LineaTasa.cod_linea_programado = pLineas.cod_linea_programado;
                            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                            {
                                LPTasa = TasaData.CrearLineaProgramado_tasa(x.LineaTasa, vUsuario);
                                ts.Complete();
                            }
                            foreach (LineaProgramado_Requisito y in x.ListaRequisitos)
                            {
                                y.idrango = LPRango.idrango;
                                y.cod_linea_programado = pLineas.cod_linea_programado;
                                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                                {
                                    LPRequisito = RequisitoData.CrearLineaProgramado_Requisito(y, vUsuario);
                                    ts.Complete();
                                }
                            }
                        }
                    }
                    else
                    {
                        rango_id[sec_rango] =  x.idrango.ToString();
                    }
                    sec_rango += 1;
                }
                if (rango_id.Count() > 1)
                {
                    string filtro = "";
                    filtro = "" + rango_id[0] + "";
                    for (int i = 1; i <= rango_id.Count() - 1; i++)
                    {
                        filtro = filtro + "," + rango_id[i] + "";
                    }
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        RangoData.EliminarLineaProgramado_Rangos(filtro, pLineas.cod_linea_programado, vUsuario);
                        ts.Complete();
                    }
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        TasaData.EliminarLineaProgramado_tasas(filtro, pLineas.cod_linea_programado, vUsuario);
                        ts.Complete();
                    }
                }
                else if (rango_id.Count()==1)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        RangoData.EliminarLineaProgramado_Rangos(rango_id[0], pLineas.cod_linea_programado,vUsuario);
                        ts.Complete();
                    }
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        TasaData.EliminarLineaProgramado_tasas(rango_id[0], pLineas.cod_linea_programado, vUsuario);
                        ts.Complete();
                    }
                }
                return pLineas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoBusiness", "CrearMod_LineasProgramado", ex);
                return null;
            }
        }

        public void EliminarLineaProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALineasPro.EliminarLineaProgramado(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoBusiness", "EliminarLineaProgramado", ex);
            }
        }


        public List<LineasProgramado> ListarLineasProgramado(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return DALineasPro.ListarLineasProgramado(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoBusiness", "ListarLineasProgramado", ex);
                return null;
            }
        }



        public LineasProgramado ConsultarLineasProgramado(Int64 pId, ref List<LineaProgramado_Rango> ListaRango, Usuario vUsuario)
        {
            try
            {
                LineaProgramado_RangoData RangoData = new LineaProgramado_RangoData();
                ListaRango = RangoData.ListarLineaProgramado_Rango(pId, vUsuario);
                return DALineasPro.ConsultarLineasProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoBusiness", "ConsultarLineasProgramado", ex);
                return null;
            }
        }



        public LineasProgramado ConsultarLineas_Programado(Int64 pId,  Usuario vUsuario)
        {
            try
            {
                LineaProgramado_RangoData RangoData = new LineaProgramado_RangoData();
            
                return DALineasPro.ConsultarLineasProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoBusiness", "ConsultarLineas_Programado", ex);
                return null;
            }
        }


    }
}
