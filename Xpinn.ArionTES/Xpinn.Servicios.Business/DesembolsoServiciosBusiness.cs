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
    public class DesembolsoServiciosBusiness : GlobalBusiness
    {
        private DesembolsoServicioData BADesembolso;

        public DesembolsoServiciosBusiness()
        {
            BADesembolso = new DesembolsoServicioData();
        }


        public DesembosoServicios CrearTransaccionDesembolso(DesembosoServicios pDesembolso, Xpinn.Tesoreria.Entities.Operacion pOperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // OPERACION
                    Xpinn.Tesoreria.Data.OperacionData OperacionData = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOperacion = new Tesoreria.Entities.Operacion();
                    vOperacion = OperacionData.GrabarOperacion(pOperacion, vUsuario);

                    // TRANSACCION
                    pDesembolso.cod_ope = vOperacion.cod_ope;
                    pDesembolso.fecha_desembolso = vOperacion.fecha_oper;
                    pDesembolso = BADesembolso.CrearTransaccionDesembolso(pDesembolso, vUsuario);

                    //CONSULTAR SERVICIO
                    AprobacionServiciosData DAServi = new AprobacionServiciosData();
                    Servicio entServi = new Servicio();
                    entServi = DAServi.ConsultarSERVICIO(pDesembolso.numero_servicio, vUsuario);

                    //RENOVACION
                    CausacionServiciosData DARENOVACION = new CausacionServiciosData();
                    RenovacionServicios entidad = new RenovacionServicios();
                    RenovacionServicios pRenova = new RenovacionServicios();
                    pRenova.idrenovacion = 0;
                    pRenova.numero_servicio = pDesembolso.numero_servicio;
                    pRenova.fecha_renovacion = DateTime.Now;
                    pRenova.cod_ope = pDesembolso.cod_ope;
                    pRenova.fecha_inicial_vigencia = entServi.Fec_ini;
                    pRenova.fecha_final_vigencia = entServi.Fec_fin;
                    entServi.valor_total = entServi.valor_total != null ? entServi.valor_total : 0;
                    pRenova.valor_total = entServi.valor_total;
                    entServi.valor_cuota = entServi.valor_cuota != null ? entServi.valor_cuota : 0;
                    pRenova.valor_cuota = entServi.valor_cuota;
                    entServi.numero_cuotas = entServi.numero_cuotas != null ? entServi.numero_cuotas : 0;
                    pRenova.plazo = entServi.numero_cuotas;
                    pRenova.tipo = 2;
                    entidad = DARENOVACION.CrearRenovacionServicios(pRenova, vUsuario);

                    ts.Complete();
                }

                return pDesembolso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoServiciosBusiness", "CrearTransaccionDesembolso", ex);
                return null;
            }
        }


        public DateTime? ObtenerFechaInicioServicio(DateTime pFechaActual, int pFormaPago, Int64 pCodEmpresa, string pPeriodicidad, Usuario vUsuario)
        {
            DateTime? pFecInicio = null;
            try
            {
                if (pFormaPago == 1)
                    pFecInicio = BADesembolso.FechaInicioServicioCaja(pFechaActual, pPeriodicidad, vUsuario);
                else
                    pFecInicio = BADesembolso.FechaInicioServicioNomina(pFechaActual, pCodEmpresa, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DesembolsoServiciosBusiness", "ObtenerFechaInicioServicio", ex);
                return null;
            }
            return pFecInicio;
        }

    }
}
