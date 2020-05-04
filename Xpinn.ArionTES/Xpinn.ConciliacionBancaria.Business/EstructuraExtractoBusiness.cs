using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Data;
using Xpinn.ConciliacionBancaria.Entities;


namespace Xpinn.ConciliacionBancaria.Business
{
    public class EstructuraExtractoBusiness : GlobalData
    {

        private EstructuraExtractoData BOEstructura;

        public EstructuraExtractoBusiness()
        {
            BOEstructura = new EstructuraExtractoData();
        }

        public EstructuraExtracto CrearEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructura = BOEstructura.CrearEstructuraCarga(pEstructura, vUsuario);

                    int cod;
                    cod = pEstructura.idestructuraextracto;

                    if (pEstructura.lstDetEstructura != null && pEstructura.lstDetEstructura.Count > 0)
                    {
                        foreach (DetEstructuraExtracto eEstruc in pEstructura.lstDetEstructura)
                        {
                            DetEstructuraExtracto nEstructura = new DetEstructuraExtracto();
                            eEstruc.idestructuraextracto = cod;
                            nEstructura = BOEstructura.CrearEstructuraDetalle(eEstruc, vUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pEstructura;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "CrearEstructuraCarga", ex);
                return null;
            }
        }

        public EstructuraExtracto ModificarEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEstructura = BOEstructura.ModificarEstructuraCarga(pEstructura, vUsuario);

                    int Cod;
                    Cod = pEstructura.idestructuraextracto;

                    if (pEstructura.lstDetEstructura != null && pEstructura.lstDetEstructura.Count > 0)
                    {
                        foreach (DetEstructuraExtracto eEstruc in pEstructura.lstDetEstructura)
                        {
                            eEstruc.idestructuraextracto = Cod;
                            DetEstructuraExtracto nEstructura = new DetEstructuraExtracto();
                            if (eEstruc.iddetestructura <= 0 || eEstruc.iddetestructura == null)
                                nEstructura = BOEstructura.CrearEstructuraDetalle(eEstruc, vUsuario);
                            else
                                nEstructura = BOEstructura.ModificarEstructuraDetalle(eEstruc, vUsuario);
                        }
                    }
                    ts.Complete();
                }

                return pEstructura;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "ModificarEstructuraCarga", ex);
                return null;
            }
        }



        public void EliminarEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOEstructura.EliminarEstructuraCarga(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "EliminarEstructuraCarga", ex);
            }
        }

        public EstructuraExtracto ConsultarEstructuraCarga(EstructuraExtracto pEntidad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = BOEstructura.ConsultarEstructuraCarga(pEntidad, vUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "ConsultarEstructuraCarga", ex);
                return null;
            }
        }


        public List<EstructuraExtracto> ListarEstructuraExtracto(String filtro,Usuario vUsuario)
        {
            try
            {
                return BOEstructura.ListarEstructuraExtracto(filtro,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "ListarEstructuraExtracto", ex);
                return null;
            }
        }




        public List<DetEstructuraExtracto> ListarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOEstructura.ListarEstructuraDetalle(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


        public void EliminarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BOEstructura.EliminarEstructuraDetalle(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraExtractoBusiness", "EliminarEstructuraDetalle", ex);
            }
        }

    }
}


