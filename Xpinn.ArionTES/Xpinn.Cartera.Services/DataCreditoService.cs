using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;


namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataCreditoServices
    {
        private DatacreditoBusines BODataCredito;
        private DatacreditoBusines BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public DataCreditoServices()
        {
            BODataCredito = new DatacreditoBusines();
            BOExcepcion = new DatacreditoBusines();
        }

        public string CodigoPrograma { get { return "60111"; } }



        public void ArchivoPlano(DateTime fecha, string tipo, string oficina, string ciudad, string codigo, string tipoEntrega, int archivo, int creditosEmpleados, Usuario pUsuario)
        {
            try
            {
                BODataCredito.ArchivoPlano(fecha, tipo, oficina, ciudad, codigo, tipoEntrega, archivo, creditosEmpleados, pUsuario);                                   
            }
            catch
            { }
       
        }

        public void ArchivoPlanoCIFIN(DateTime fecha, int pCod_paquete, int pTipoEntidad, string pCodigoEntidad, string pProbabilidad, string tipoEntrega, int archivo, int creditosEmpleados, Boolean IsAhorro, Usuario pUsuario)
        {
            try
            {
                BODataCredito.ArchivoPlanoCIFIN(fecha, pCod_paquete, pTipoEntidad, pCodigoEntidad, pProbabilidad, tipoEntrega, archivo, creditosEmpleados, IsAhorro, pUsuario);
            }
            catch
            { }
        }

        public List<DataCredito> listarArchivoPlano(Usuario pUsuario)
        {
            try
            {
              return  BODataCredito.listarArchivoPlano(pUsuario);
                       
            
            }
            catch
            { return null; }       
        }

        public Int64 ValidarInformacionCentrales(DateTime pfecha, string pFiltro, Usuario pUsuario)
        {
            return BODataCredito.ValidarInformacionCentrales(pfecha, pFiltro, pUsuario);
        }

        public void InformacionCentralesRiesgo(DateTime pfecha, int pNuevo, int pCodeudores, int pTipo_producto, Usuario pUsuario, ref string serror)
        {
            try
            {
                BODataCredito.InformacionCentralesRiesgo(pfecha, pNuevo, pCodeudores, pTipo_producto, pUsuario, ref serror);
            }
            catch (Exception ex)
            {
                serror = ex.Message;
            }
        }

    }
}

