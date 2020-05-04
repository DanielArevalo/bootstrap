using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Business
{
    /// <summary>
    /// Objeto de negocio para Cosechas
    /// </summary>
    public class CosechasBusiness : GlobalBusiness
    {
        private CosechasData DACosechas;

        /// <summary>
        /// Constructor del objeto de negocio para Cosechas
        /// </summary>
        public CosechasBusiness()
        {
            DACosechas = new CosechasData();
        }

        #region General
        /// <summary>
        /// Metodo que crea la lista de los encabezados de la gridview
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="intervalo">se solicita para saber si se genera 1=semanal, 2=mensual o 3=anualmente</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<string> GenerarTitulos(DateTime fecha_inicial, DateTime fecha_final, Int64 intervalo, Usuario pUsuario)
        {
            try
            {
                return DACosechas.GenerarTitulos(fecha_inicial, fecha_final, intervalo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosechasBusiness", "GenerarTitulos", ex);
                return null;
            }
        }
        #endregion

        #region Análisis
        /// <summary>
        /// Metodo que genera la lista del primer reporte de analisis de cosechas
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> Colocacion(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DACosechas.Colocacion(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosechasBusiness", "Colocacion", ex);
                return null;
            }
        }
        /// <summary>
        /// Metodo usado para conocer el estado de la cartera Vencida
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> ListarCarteraVencida(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DACosechas.ListarCarteraVencida(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosechasBusiness", "ListarCarteraVencida", ex);
                return null;
            }
        }
        /// <summary>
        /// Metodo implementado para conocer la calidad de la cosecha
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> ListarCalidadCosecha(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return DACosechas.ListarCalidadCosecha(fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CosechasBusiness", "ListarCalidadCosecha", ex);
                return null;
            }
        }
        #endregion

    }
}