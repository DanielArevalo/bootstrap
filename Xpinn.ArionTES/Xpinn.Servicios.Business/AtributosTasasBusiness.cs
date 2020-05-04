using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Data;
using Xpinn.Util;
using System.Transactions;

namespace Xpinn.Servicios.Business
{
    public class AtributosTasasBusiness : GlobalBusiness
    {
        private AtributosTasasData DARangoTasa;

        public AtributosTasasBusiness()
        {
            DARangoTasa = new AtributosTasasData();
        }


        public Boolean CrearRangoTasas(RangoTasas pRangoTasas, AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangoTasas = DARangoTasa.CrearModRangoTasas(pRangoTasas, vUsuario, 1);

                    int Cod = pRangoTasas.codrango;

                    //INSERTANDO LOS TOPES
                    if (pRangoTasas.lstTopes != null && pRangoTasas.lstTopes.Count > 0)
                    {
                        foreach (RangoTasasTope eRangoTopes in pRangoTasas.lstTopes)
                        {
                            eRangoTopes.codrango = Cod;
                            eRangoTopes.cod_linea_servicio = pRangoTasas.cod_linea_servicio;
                            RangoTasasTope nTope = new RangoTasasTope();
                            nTope = DARangoTasa.CrearModRangoTasasTope(eRangoTopes, vUsuario, 1);
                        }
                    }
                    //INSERTANDO EL ATRIBUTO DE TASA
                    if (pAtributos != null)
                    {
                        pAtributos.codrango = Cod;
                        pAtributos.cod_linea_servicio = pRangoTasas.cod_linea_servicio;
                        AtributosLineaServicio pAtrib = new AtributosLineaServicio();
                        pAtrib = DARangoTasa.CrearModAtributosLineaServicio(pAtributos, vUsuario, 1);
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


        public Boolean ModificarRangoTasas(RangoTasas pRangoTasas, AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRangoTasas = DARangoTasa.CrearModRangoTasas(pRangoTasas, vUsuario, 2);

                    int Cod = pRangoTasas.codrango;

                    //INSERTANDO LOS TOPES
                    if (pRangoTasas.lstTopes != null && pRangoTasas.lstTopes.Count > 0)
                    {
                        foreach (RangoTasasTope eRangoTopes in pRangoTasas.lstTopes)
                        {
                            eRangoTopes.codrango = Cod;
                            eRangoTopes.cod_linea_servicio = pRangoTasas.cod_linea_servicio;
                            RangoTasasTope nTope = new RangoTasasTope();
                            if (eRangoTopes.codrango <= 0)
                                nTope = DARangoTasa.CrearModRangoTasasTope(eRangoTopes, vUsuario, 1);
                            else
                                nTope = DARangoTasa.CrearModRangoTasasTope(eRangoTopes, vUsuario, 2);
                        }
                    }
                    //INSERTANDO EL ATRIBUTO DE TASA
                    if (pAtributos != null)
                    {
                        pAtributos.codrango = Cod;
                        pAtributos.cod_linea_servicio = pRangoTasas.cod_linea_servicio;
                        AtributosLineaServicio pAtrib = new AtributosLineaServicio();
                        if (pAtributos.codatrser <= 0)
                            pAtrib = DARangoTasa.CrearModAtributosLineaServicio(pAtributos, vUsuario, 1);
                        else
                            pAtrib = DARangoTasa.CrearModAtributosLineaServicio(pAtributos, vUsuario, 2);
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

        public void EliminarSoloRangoTasasTope(long conseID, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARangoTasa.EliminarSoloRangoTasasTope(conseID, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasBusiness", "EliminarSoloRangoTasasTope", ex);
            }
        }

        public void EliminarRangoTopes(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARangoTasa.EliminarRangoTopes(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasBusiness", "EliminarRangoTopes", ex);
            }
        }


        public List<RangoTasas> ListarRangoTasas(RangoTasas pRangoTasas, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DARangoTasa.ListarRangoTasas(pRangoTasas, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AtributosTasasBusiness", "ListarRangoTasas", ex);
                return null;
            }
        }


        public Boolean ConsultarRangoTasasGeneral(ref RangoTasas eResult, ref AtributosLineaServicio pAtributos, ref string pError, Usuario vUsuario)
        {
            try
            {
                eResult = DARangoTasa.ConsultarRangoTasas(eResult, ref pError, vUsuario);
                if (eResult != null || pError != "")
                {
                    //Recuperando topes del rango actual.
                    eResult.lstTopes = new List<RangoTasasTope>();
                    RangoTasasTope pTope = new RangoTasasTope();
                    pTope.cod_linea_servicio = eResult.cod_linea_servicio;
                    pTope.codrango = eResult.codrango;
                    eResult.lstTopes = DARangoTasa.ListarRangoTasas(pTope, vUsuario);
                    //Recuperando Atributos del rango actual.
                    pAtributos.cod_linea_servicio = eResult.cod_linea_servicio;
                    pAtributos.codrango = eResult.codrango;
                    pAtributos = DARangoTasa.ConsultarAtributosLineaServicio(pAtributos, vUsuario);
                }
                else
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }

        public Int64 ObtenerValorTopeMaximo(RangoTasasTope pRangoTasasTope, Usuario pUsuario)
        {
            try
            {
                return DARangoTasa.ObtenerValorTopeMaximo(pRangoTasasTope, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public Int64 ValorPagoServicio(string tipoPago, string numeroservicios, string fecha, Usuario vUsuario)
        {
            try
            {
                return DARangoTasa.ValorPagoServicio(tipoPago, numeroservicios, fecha, vUsuario);
            }
            catch
            {
                return 0;
            }            
        }

    }
}
