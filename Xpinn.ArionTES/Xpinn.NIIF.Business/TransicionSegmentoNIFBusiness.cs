using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using System.Transactions;
using System.Data;
using Xpinn.Util;

namespace Xpinn.NIIF.Business
{
    public class TransicionSegmentoNIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private TransicionSegmentoNIFData BASegmento;

        public TransicionSegmentoNIFBusiness()
        {
            BASegmento = new TransicionSegmentoNIFData();
        }

        


        public TransicionSegmentoNIF CrearTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransi = BASegmento.CrearTransicionSegmento(pTransi, vUsuario);
                    int cod = pTransi.codsegmento;
                    if (pTransi.lstDetalle != null && pTransi.lstDetalle.Count > 0)
                    {
                        foreach (TransicionDetalle pSeg in pTransi.lstDetalle)
                        {
                            TransicionDetalle nSegmento = new TransicionDetalle();
                            pSeg.codsegmento = cod;
                            nSegmento = BASegmento.Crear_MOD_DetalleTransicion(pSeg, vUsuario, 1);//crear
                        }
                    }
                    ts.Complete();
                }

                return pTransi;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "CrearTransicionSegmento", ex);
                return null;
            }
        }



        public TransicionSegmentoNIF ModificarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransi = BASegmento.ModificarTransicionSegmento(pTransi, vUsuario);
                    int cod = pTransi.codsegmento;
                    if (pTransi.lstDetalle != null && pTransi.lstDetalle.Count > 0)
                    {
                        foreach (TransicionDetalle pSeg in pTransi.lstDetalle)
                        {
                            TransicionDetalle nSegmento = new TransicionDetalle();
                            pSeg.codsegmento = cod;
                            if (pSeg.idcondiciontran <= 0 )
                                nSegmento = BASegmento.Crear_MOD_DetalleTransicion(pSeg, vUsuario, 1);//crear
                            else
                                nSegmento = BASegmento.Crear_MOD_DetalleTransicion(pSeg, vUsuario, 2);//Modificar
                        }
                    }
                    ts.Complete();
                }

                return pTransi;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "ModificarTransicionSegmento", ex);
                return null;
            }
        }


        public void EliminarTransicionSegmentoNIF(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BASegmento.EliminarTransicionSegmentoNIF(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "EliminarTransicionSegmentoNIF", ex);
            }
        }


        public TransicionSegmentoNIF ConsultarTransicionSegmentoNIF(TransicionSegmentoNIF pEntidad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = BASegmento.ConsultarTransicionSegmentoNIF(pEntidad, vUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "ConsultarTransicionSegmentoNIF", ex);
                return null;
            }
        }

        public List<TransicionDetalle> ListarDetalleSegmento(int codigoSegmento, Usuario usuario)
        {
            try
            {
                return BASegmento.ListarDetalleSegmento(codigoSegmento, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "ListarDetalleSegmento", ex);
                return null;
            }
        }

        public List<TransicionSegmentoNIF> ListarTransicionSegmento(TransicionSegmentoNIF pTransi, Usuario vUsuario)
        {
            try
            {
                return BASegmento.ListarTransicionSegmento(pTransi, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "ListarTransicionSegmento", ex);
                return null;
            }
        }



        public void EliminarDetalleTransicionNIF(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BASegmento.EliminarDetalleTransicionNIF(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("", "EliminarDetalleTransicionNIF", ex);
            }
        }



        public List<TransicionDetalle> ListarDetalleTransicion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BASegmento.ListarDetalleTransicion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionSegmentoNIFBusiness", "ListarDetalleTransicion", ex);
                return null;
            }
        }

    }
}
