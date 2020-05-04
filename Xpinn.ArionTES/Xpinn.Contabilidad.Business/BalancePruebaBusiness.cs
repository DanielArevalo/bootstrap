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
    public class BalancePruebaBusiness : GlobalBusiness
    {
        private BalancePruebaData DABalancePrueba;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public BalancePruebaBusiness()
        {
            DABalancePrueba = new BalancePruebaData();
        }

        public List<BalancePrueba> ListarBalance(BalancePrueba pDatos, Usuario pUsuario)
        {
            return DABalancePrueba.ListarBalance(pDatos, pUsuario);
        }
        public List<BalancePrueba> ListarBalanceCentroCosto(Usuario pUsuario, string centro, string fecha)
        {
            return DABalancePrueba.listarbalancecentrocosto(pUsuario, centro, fecha);

        }
        public List<BalancePrueba> ListarCentroCosto(Usuario pUsuario)
        {
            return DABalancePrueba.ListarCentroCosto(pUsuario);

        }
        public BalancePrueba ListarValorCentroCosto(string Centro, string cod_cuenta, Usuario pUsuario)
        {
            return DABalancePrueba.ListarValorCentroCosto(Centro, cod_cuenta, pUsuario);

        }
        public BalancePrueba ConsultarBalanceMes13(BalancePrueba pDatos, Usuario pUsuario)
        {
            return DABalancePrueba.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        public List<BalancePrueba> ListarFechaCierre(Usuario vUsuario)
        {
            // Listar períodos contables
            List<BalancePrueba> lstBalancePrueba = new List<BalancePrueba>();
            lstBalancePrueba = DABalancePrueba.ListarFechaCierre(vUsuario);

            // Insertar fechas de períodos pendientes
            List<Cierremensual> lstCierreMen = new List<Cierremensual>();
            CierreMensualBusiness cierreMensual = new CierreMensualBusiness();
            lstCierreMen = cierreMensual.ListarFechaCierre(vUsuario);
            var resul = (from l1 in lstCierreMen where !(from l2 in lstBalancePrueba select l2.fecha).Contains(l1.fecha) select l1).ToList();
            if (resul != null)
            { 
                foreach (Cierremensual cieMen in resul)
                {
                    BalancePrueba balPru = new BalancePrueba();
                    balPru.fecha = cieMen.fecha;
                    lstBalancePrueba.Insert(0, balPru);
                }
            }
            return lstBalancePrueba;
        }

        public List<BalancePrueba> ListarBalanceComprobacion(BalancePrueba pDatos, ref Double TotDeb, ref Double TotCr, Usuario pUsuario)
        {
            return DABalancePrueba.ListarBalanceComprobacion(pDatos, ref TotDeb, ref TotCr , pUsuario);
        }

        public void AlertaBalance(DateTime pfecha, ref decimal Activo, ref decimal Pasivo, ref decimal Patrimonio, ref decimal Utilidad, ref decimal Diferencia, Usuario vUsuario)
        {
            DABalancePrueba.AlertaBalance(pfecha, ref Activo, ref Pasivo, ref Patrimonio, ref Utilidad, ref Diferencia, vUsuario);
        }

        public List<BalancePrueba> ListarBalanceComprobacionTer(BalancePrueba pDatos, Usuario pUsuario)
        {
            return DABalancePrueba.ListarBalanceComprobacionTer(pDatos, pUsuario);
        }

    }
}

