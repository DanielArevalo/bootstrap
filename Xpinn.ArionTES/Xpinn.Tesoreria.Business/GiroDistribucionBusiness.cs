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

    public class GiroDistribucionBusiness : GlobalBusiness
    {

        private GiroDistribucionTesoreriaData DAGiroDistribucion;

        public GiroDistribucionBusiness()
        {
            DAGiroDistribucion = new GiroDistribucionTesoreriaData();
        }


        public void InsertarGiros(List<Xpinn.Tesoreria.Entities.Giro> lstGiros, Int64 idGiro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CuentasPorPagarData DACuentasPagar = new CuentasPorPagarData();

                    DAGiroDistribucion.updateGiro(idGiro, 5, pUsuario);// se actualiza el giro seleccionado

                    foreach (Xpinn.Tesoreria.Entities.Giro item in lstGiros)
                    {
                        //CREACION DEL NUEVO GIRO
                        Xpinn.Tesoreria.Entities.Giro pGiroRtn = new Xpinn.Tesoreria.Entities.Giro();
                        pGiroRtn = DACuentasPagar.CrearGiro(item, pUsuario, 1);

                        //CREACION EN LA TABLA GIRO_DISTRIBUCION
                        GiroDistribucion pEntidad = new GiroDistribucion();
                        pEntidad.iddetgiro = pGiroRtn.idgiro;
                        pEntidad.idgiro = idGiro;
                        pEntidad.fecha_distribucion = DateTime.Now;
                        pEntidad.cod_persona = item.cod_persona_deta > 0 ? Convert.ToInt64(item.cod_persona_deta) : 0;
                        pEntidad.valor = item.valor;
                        pEntidad.estado = 1;
                        pEntidad.identificacion = item.identificacion;
                        pEntidad.nombre = item.nombre;
                        pEntidad = DAGiroDistribucion.CrearGiroDistribucion(pEntidad, pUsuario);
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "InsertarGiros", ex);
            }
        }


        public GiroDistribucion ModificarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pGiroDistribucion = DAGiroDistribucion.ModificarGiroDistribucion(pGiroDistribucion, pusuario);

                    ts.Complete();

                }

                return pGiroDistribucion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "ModificarGiroDistribucion", ex);
                return null;
            }
        }


        public void EliminarGiroDistribucion(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAGiroDistribucion.EliminarGiroDistribucion(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "EliminarGiroDistribucion", ex);
            }
        }


        public GiroDistribucion ConsultarGiroDistribucion(Int64 pId, Usuario pusuario)
        {
            try
            {
                GiroDistribucion GiroDistribucion = new GiroDistribucion();
                GiroDistribucion = DAGiroDistribucion.ConsultarGiroDistribucion(pId, pusuario);
                return GiroDistribucion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "ConsultarGiroDistribucion", ex);
                return null;
            }
        }


        public List<GiroDistribucion> ListarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario pusuario)
        {
            try
            {
                return DAGiroDistribucion.ListarGiroDistribucion(pGiroDistribucion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "ListarGiroDistribucion", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlTipoComBusines(Usuario pUsuario,int opcion) 
        {
            try
            {
                return DAGiroDistribucion.listarDDlTipoCom(pUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "listarDDlTipoComBusines", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlGeneradoEnBusines(Usuario pUsuario) 
        {
            try
            {
                return DAGiroDistribucion.listarDDlGeneradoEn(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "listarDDlGeneradoEnBusines", ex);
                return null;
            }
        }

        public List<GiroDistribucion> getListaGiroBusines(Usuario pUsuario, string pFiltro) 
        {
            try
            {
                return DAGiroDistribucion.getListaGiro(pUsuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "getListaGiroBusines", ex);
                return null;
            }
        }

        public GiroDistribucion ConsultarGiroDistribucionBusines(Int64 pId, Usuario vUsuario) 
        {
            try
            {
                return DAGiroDistribucion.ConsultarGiroDistribucion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "ConsultarGiroDistribucionBusines", ex);
                return null;
            }
        }

        public List<GiroDistribucion> listarDDlFormaPagoInv(Usuario pUsuario)
        {
            try
            {
                return DAGiroDistribucion.listarDDlFormaPagoInv(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroDistribucionBusiness", "listarDDlFormaPagoInv", ex);
                return null;
            }
        }




    }
}
