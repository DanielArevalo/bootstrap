using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;


namespace Xpinn.Cartera.Business
{
    public class DatacreditoBusines : GlobalData
    {

        private DataCreditoData DADataCredito;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public DatacreditoBusines()
        {
            DADataCredito = new DataCreditoData();
        }


        public void ArchivoPlano(DateTime fecha, string tipo, string oficina, string ciudad, string codigo, string tipoEntrega, int archivo, int creditosEmpleados, Usuario pUsuario)
        {
            try
            {
                DADataCredito.ArchivoPlano(fecha, tipo, oficina, ciudad, codigo, tipoEntrega, archivo, creditosEmpleados, pUsuario);
            }
            catch
            { }

        }


        public void ArchivoPlanoCIFIN(DateTime fecha, int pCod_paquete, int pTipoEntidad, string pCodigoEntidad, string pProbabilidad, string tipoEntrega, int archivo, int creditosEmpleados, Boolean IsAhorro, Usuario pUsuario)
        {
            try
            {
                DADataCredito.ArchivoPlanoCIFIN(fecha, pCod_paquete, pTipoEntidad, pCodigoEntidad, pProbabilidad, tipoEntrega, archivo, creditosEmpleados, IsAhorro, pUsuario);
            }
            catch
            { }
        }

        public List<DataCredito> listarArchivoPlano(Usuario pUsuario)
        {
            try
            {
               return DADataCredito.listarArchivoPlano(pUsuario);
            }
            catch
            { return null; }

        }

        public Int64 ValidarInformacionCentrales(DateTime pfecha, string pFiltro, Usuario pUsuario)
        {
            return DADataCredito.ValidarInformacionCentrales(pfecha, pFiltro, pUsuario);
        }

        public void InformacionCentralesRiesgo(DateTime pfecha, int pNuevo, int pCodeudores, int pTipo_producto, Usuario pUsuario, ref string serror)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    DADataCredito.InformacionCentralesRiesgo(pfecha, pNuevo, pCodeudores, pTipo_producto, pUsuario, ref serror);
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                serror = ex.Message;
            }
        }

    }
}
