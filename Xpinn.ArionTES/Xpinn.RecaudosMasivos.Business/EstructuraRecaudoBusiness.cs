using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Business
{
    public class EstructuraRecaudoBusiness : GlobalData
    {

        private EstructuraRecaudoData BOEstructuraRecaudo;

        public EstructuraRecaudoBusiness()
        {
            BOEstructuraRecaudo = new EstructuraRecaudoData();
        }

        public Estructura_Carga CrearEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructura = BOEstructuraRecaudo.CrearEstructuraCarga(pEstructura, vUsuario);

                    int? cod;
                    cod = pEstructura.cod_estructura_carga;

                    if (pEstructura.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (Estructura_Carga_Detalle eEstruc in pEstructura.lstDetalle)
                        {
                            Estructura_Carga_Detalle nEstructura = new Estructura_Carga_Detalle();
                            eEstruc.cod_estructura_carga = cod;
                            nEstructura = BOEstructuraRecaudo.CrearEstructuraDetalle(eEstruc, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }

                return pEstructura;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "CrearEstructuraCarga", ex);
                return null;
            }
        }

        public Estructura_Carga ModificarEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructura = BOEstructuraRecaudo.ModificarEstructuraCarga(pEstructura, vUsuario);

                    int? Cod;
                    Cod = pEstructura.cod_estructura_carga;

                    if (pEstructura.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (Estructura_Carga_Detalle eEstruc in pEstructura.lstDetalle)
                        {
                            eEstruc.cod_estructura_carga = Cod;
                            Estructura_Carga_Detalle nEstructura = new Estructura_Carga_Detalle();
                            if (eEstruc.cod_estructura_detalle <= 0 || eEstruc.cod_estructura_detalle == null)
                                nEstructura = BOEstructuraRecaudo.CrearEstructuraDetalle(eEstruc, vUsuario);
                            else
                                nEstructura = BOEstructuraRecaudo.ModificarEstructuraDetalle(eEstruc, vUsuario);
                            num += 1;
                        }
                    }


                    ts.Complete();
                }

                return pEstructura;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "ModificarEstructuraCarga", ex);
                return null;
            }
        }



        public void EliminarEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOEstructuraRecaudo.EliminarEstructuraCarga(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "EliminarEstructuraCarga", ex);
            }
        }

        public Estructura_Carga ConsultarEstructuraCarga(Estructura_Carga pEntidad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = BOEstructuraRecaudo.ConsultarEstructuraCarga(pEntidad, vUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "ConsultarEstructuraCarga", ex);
                return null;
            }
        }


        public List<Estructura_Carga> ListarEstructuraRecaudo(Estructura_Carga pEstructura, Usuario vUsuario,String filtro,int op)
        {
            try
            {
                return BOEstructuraRecaudo.ListarEstructuraRecaudo(pEstructura, vUsuario, filtro,op);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "ListarEstructuraRecaudo", ex);
                return null;
            }
        }




        public List<Estructura_Carga_Detalle> ListarEstructuraDetalle(Estructura_Carga_Detalle pEstructura, string pOrden, Usuario vUsuario)
        {
            try
            {
                return BOEstructuraRecaudo.ListarEstructuraDetalle(pEstructura,pOrden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


        public void EliminarEstructuraDetalle(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOEstructuraRecaudo.EliminarEstructuraDetalle(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoBusiness", "EliminarEstructuraDetalle", ex);
            }
        }

    }
}


