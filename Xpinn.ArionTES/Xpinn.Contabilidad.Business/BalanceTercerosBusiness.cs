using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class BalanceTercerosBusiness : GlobalBusiness
    {
        private BalanceTercerosData DABalanceTerceros;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public BalanceTercerosBusiness()
        {
            DABalanceTerceros = new BalanceTercerosData();
        }

        public List<BalanceTerceros> ListarBalance(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return DABalanceTerceros.ListarBalance(pDatos, pUsuario);
        }
        public List<BalanceTerceros> ListarBalanceCentroCosto( Usuario pUsuario,string centro,string fecha)
        {
            return DABalanceTerceros.listarbalancecentrocosto(pUsuario,centro,fecha);
           
        }
        public List<BalanceTerceros> ListarCentroCosto(Usuario pUsuario)
        {
            return DABalanceTerceros.ListarCentroCosto(pUsuario);

        }
        public BalanceTerceros ListarValorCentroCosto(string Centro,string cod_cuenta,Usuario pUsuario)
        {
            return DABalanceTerceros.ListarValorCentroCosto(Centro,cod_cuenta,pUsuario);

        }
        public BalanceTerceros ConsultarBalanceMes13(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return DABalanceTerceros.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        public List<BalanceTerceros> ListarFechaCierre(Usuario vUsuario)
        {
            // Listar períodos contables
            List<BalanceTerceros> lstBalancePrueba = new List<BalanceTerceros>();
            lstBalancePrueba = DABalanceTerceros.ListarFechaCierre("C", " ", vUsuario);

            // Insertar fechas de períodos pendientes
            List<Cierremensual> lstCierreMen = new List<Cierremensual>();
            CierreMensualBusiness cierreMensual = new CierreMensualBusiness();
            lstCierreMen = cierreMensual.ListarFechaCierre(vUsuario);
            foreach (Cierremensual cieMen in lstCierreMen)
            {
                BalanceTerceros balPru = new BalanceTerceros();
                balPru.fecha = cieMen.fecha;
                var fecha = from item in lstBalancePrueba where item.fecha == cieMen.fecha select item;
                try
                {
                    if (fecha.First() == null)
                        lstBalancePrueba.Insert(0, balPru);
                }
                catch
                {
                    lstBalancePrueba.Insert(0, balPru);
                }
            }
            return lstBalancePrueba;
        }

        public List<BalanceTerceros> ListarFechaCierreDefinitivo(string pTipo, Usuario pUsuario)
        {
            // Listar períodos contables
            List<BalanceTerceros> lstBalancePrueba = new List<BalanceTerceros>();
            lstBalancePrueba = DABalanceTerceros.ListarFechaCierre(pTipo, "D", pUsuario);
            return lstBalancePrueba;
        }

        public List<BalanceTerceros> ListarBalanceComprobacion(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return DABalanceTerceros.ListarBalanceComprobacion(pDatos, pUsuario);
        }

    }
}

