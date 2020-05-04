using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para VentasSemanales
    /// </summary>
    public class VentasSemanalesBusiness : GlobalData
    {
        private VentasSemanalesData DAVentasSemanales;
        protected ConnectionDataBase dbConnectionFactory;
        Configuracion global = new Configuracion();
        string separadordecimal = ",";

        /// <summary>
        /// Constructor del objeto de negocio para VentasSemanales
        /// </summary>
        public VentasSemanalesBusiness()
        {
            DAVentasSemanales = new VentasSemanalesData();
            separadordecimal = global.ObtenerSeparadorDecimalConfig();
        }

        /// <summary>
        /// Crea un VentasSemanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad VentasSemanales</param>
        /// <returns>Entidad VentasSemanales creada</returns>
        public VentasSemanales CrearVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    CalcularTotal(pVentasSemanales);
                    pVentasSemanales = DAVentasSemanales.CrearVentasSemanales(pVentasSemanales, pUsuario);

                    ts.Complete();
                }

                return pVentasSemanales;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesBusiness", "CrearVentasSemanales", ex);
                return null;
            }
        }


        /// <summary>
        /// Modifica un VentasSemanales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad VentasSemanales</param>
        /// <returns>Entidad VentasSemanales modificada</returns>
        public VentasSemanales ModificarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    CalcularTotal(pVentasSemanales);
                    pVentasSemanales = DAVentasSemanales.ModificarVentasSemanales(pVentasSemanales, pUsuario);
                    ts.Complete();
                }

                return pVentasSemanales;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesBusiness", "ModificarVentasSemanales", ex);
                return null;
            }
        }



        /// <summary>
        /// Elimina un VentasSemanales
        /// </summary>
        /// <param name="pId">Identificador de VentasSemanales</param>
        public void EliminarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAVentasSemanales.EliminarVentasSemanales(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesBusiness", "EliminarVentasSemanales", ex);
            }
        }

        /// <summary>
        /// Obtiene un VentasSemanales
        /// </summary>
        /// <param name="pId">Identificador de VentasSemanales</param>
        /// <returns>Entidad VentasSemanales</returns>
        public VentasSemanales ConsultarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAVentasSemanales.ConsultarVentasSemanales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesBusiness", "ConsultarVentasSemanales", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public List<VentasSemanales> ListarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            try
            {
                  return DAVentasSemanales.ListarVentasSemanales(pVentasSemanales, pUsuario);
               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("VentasSemanalesBusiness", "ListarVentasSemanales", ex);
                return null;
            }
        }

     

        /// <summary>
        /// Obtiene listas desplegables
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<VentasSemanales> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAVentasSemanales.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "ListarEstacionalidadMensual", ex);
                return null;
            }
        }

        private void CalcularTotal(VentasSemanales pVentasSemanales)
        {

            if (pVentasSemanales.lunes != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.martes != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.miercoles != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.jueves != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.viernes != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.sabados != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
            if (pVentasSemanales.domingo != 0)
                pVentasSemanales.total = pVentasSemanales.total + pVentasSemanales.valor;
        }



        /// <summary>
        /// Obtiene calculos de los totales
        /// </summary>
        /// <param name="pVentasSemanales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public VentasSemanales CalculosTotalesSemanales(List<VentasSemanales> lstConsulta)
        {
            try
            {
                VentasSemanales entidad = new VentasSemanales();
                
                for (int numFila = 0; numFila <= lstConsulta.Count() - 1; numFila++)
                    entidad.totalSemanal = entidad.totalSemanal + lstConsulta[numFila].total;
                string semanasMes = ConfigurationManager.AppSettings["SemanasDelMes"].ToString();
                if (separadordecimal == ".")
                    semanasMes = semanasMes.Replace(",", separadordecimal);

                entidad.porContado = Convert.ToInt64(lstConsulta[0].porContado.ToString());
                double ventasMes = entidad.totalSemanal * Convert.ToDouble(semanasMes);
                entidad.ventasMes = Convert.ToInt64(ventasMes);

                entidad.venContado = (entidad.ventasMes * entidad.porContado) / 100;
                entidad.venCredito = (entidad.ventasMes * (100 - entidad.porContado)) / 100;

                return entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstacionalidadMensualBusiness", "CalculosTotalesSemanales", ex);
                return null;
            }
        }




    }
}