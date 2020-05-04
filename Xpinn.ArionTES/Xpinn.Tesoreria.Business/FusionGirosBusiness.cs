using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class FusionGirosBusiness : GlobalBusiness
    {
        private FusionGirosData DAFusion;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public FusionGirosBusiness()
        {
            DAFusion = new FusionGirosData();
        }


        public List<Giro> ListarGiroAprobadosOpendientes(Giro pGiro, String Orden, DateTime pFechaGiro, Usuario vUsuario)
        {
            try
            {
                return DAFusion.ListarGiroAprobadosOpendientes(pGiro, Orden, pFechaGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosBusiness", "ListarGiroAprobadosOpendientes", ex);
                return null;
            }
        }

        public Giro FusionarGiro(Giro pGiroTot, Giro pGiro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pGiroTot.lstGiro != null && pGiroTot.lstGiro.Count > 0)
                    {
                        //CREACION DE LA OPERACION
                        OperacionData DAOperacion = new OperacionData();
                        Operacion vOpe = new Operacion();
                        vOpe.cod_ope = 0;
                        vOpe.tipo_ope = 103; //MODIFICAR
                        vOpe.cod_caja = 0;
                        vOpe.cod_cajero = 0;
                        vOpe.observacion = "Operacion-Fusionar Giro";
                        vOpe.cod_proceso = null;
                        vOpe.fecha_oper = Convert.ToDateTime(pGiro.fec_reg);
                        vOpe.fecha_calc = DateTime.Now;
                        vOpe.cod_ofi = vUsuario.cod_oficina;
                        vOpe = DAOperacion.GrabarOperacion(vOpe, vUsuario);

                        //GRABAR NUEVO GIRO
                        GiroData GiroData = new GiroData();
                        Giro vGiroEntidad = new Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);

                        foreach (Giro nGiro in pGiroTot.lstGiro)
                        {
                            //ACTUALIZAR TABLA GIRO
                            Giro pEntidad = new Giro();
                            nGiro.estado = 4;                  
                            pEntidad = DAFusion.FusionarGiro(nGiro, vUsuario);
                            //INSERTAR EN LA TABLA GIRO FUSIONADO
                            GiroFusionado pFusion = new GiroFusionado();
                            GiroFusionado pGiroFusionado = new GiroFusionado();
                            pGiroFusionado.idfusion = 0;
                            pGiroFusionado.idgiro_fus = nGiro.idgiro;
                            pGiroFusionado.idgiro_nue = Convert.ToInt32(vGiroEntidad.idgiro);
                            pGiroFusionado.fecha_fusion = DateTime.Now;
                            pGiroFusionado.valor = pGiro.valor;
                            pGiroFusionado.estado = 0;

                            pFusion = DAFusion.CrearGiroFUSION(pGiroFusionado, vUsuario);                             
                        }
                    }
                    ts.Complete();
                }
                return pGiroTot;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosBusiness", "FusionarGiro", ex);
                return null;
            }
        }

    }
}