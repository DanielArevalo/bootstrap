using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para Giro
    /// </summary>
    public class GiroBusiness : GlobalBusiness
    {
        private GiroData DAGiro;

        /// <summary>
        /// Constructor del objeto de negocio para TransaccionCaja
        /// </summary>
        public GiroBusiness()
        {
            DAGiro = new GiroData();
        }

        public List<Giro> ListarGiro(Giro pGiro, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAGiro.ListarGiro(pGiro, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "ListarGiro", ex);
                return null;
            }
        }
        public List<Giro> ListarGiroConsulta(Giro pGiro, string pFiltro, string pOrdenar,  Usuario vUsuario)
        {
            try
            {
                return DAGiro.ListarGiroConsulta(pGiro, pFiltro, pOrdenar ,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "ListarGiro", ex);
                return null;
            }
        }


        public Giro ConsultarGiro(string pId, Usuario vUsuario)
        {
            try
            {
                return DAGiro.ConsultarGiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "ConsultarGiro", ex);
                return null;
            }
        }

        public Giro Crear_ModGiro(Giro pGiro, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Giro pEntidad = new Giro();
                    pEntidad = DAGiro.Crear_ModGiro(pGiro, vUsuario, opcion);

                    //GRABAR LA FORMA DE PAGO DEL CDAT
                    
                    if (pGiro.formadesembolso=="TranferenciaAhorroVistaInterna")
                    {
                        Xpinn.Ahorros.Business.AhorroVistaBusiness ahorroBusiness = new Xpinn.Ahorros.Business.AhorroVistaBusiness();
                        Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista
                        {

                            numero_cuenta = pGiro.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pGiro.cod_persona,
                            cod_ope = Convert.ToInt32(pGiro.cod_ope),
                            fecha_cierre = pGiro.fec_reg,
                            V_Traslado = pGiro.valor,
                            codusuario = vUsuario.codusuario,
                           
                        };

                        Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                        DAAhorroVistaData.IngresoCuenta(ahorro, vUsuario);
                        if  (pGiro.valor > 0)
                        {
                            pEntidad = DAGiro.ModificarGiroXCod_ope(pGiro, vUsuario);

                        }


                    }


                    ts.Complete();
                }
                return pGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "Crear_ModGiro", ex);
                return null;
            }
        }

        public void AnularGiro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                DAGiro.AnularGiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "AnularGiro", ex);
                return;
            }
        }

        public List<Giro> ConciliarGiro(Giro pGiro, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAGiro.ConciliarGiro(pGiro, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroBusiness", "ConciliarGiro", ex);
                return null;
            }
        }


    }
}
