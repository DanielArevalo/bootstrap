using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class ResumenExtractosBusiness : GlobalData
    {
        private ResumenExtractosData DAResumenExtractos;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public ResumenExtractosBusiness()
        {
            DAResumenExtractos = new ResumenExtractosData();
        }

        /// <summary>
        /// Obtiene la lista de ResumenExtractoss
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de ResumenExtractoss obtenidas</returns>        
        public List<ResumenExtractos> ListarResumenExtractos(ResumenExtractos pResumenExtractos, Int64 vNumRadic, out DataTable dDataTable, Usuario pUsuario)
        {
            try
            {
                return DAResumenExtractos.ListarResumenExtractos(pResumenExtractos, vNumRadic, out dDataTable, pUsuario);
            }
            catch (Exception ex)
            {
                dDataTable = new DataTable();
                BOExcepcion.Throw("ResumenExtractosBusiness", "ListarResumenExtractoss", ex);
                return null;
            }
        }


        public List<ResumenExtractos> GeneraryListarResumen(ResumenExtractos entidad, Usuario pUsuario)
        {
            try
            {
                return DAResumenExtractos.GeneraryListarResumen(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosBusiness", "GeneraryListarResumen", ex);
                return null;
            }
        }


        public List<ResumenExtractos> ListarBancos(string codlinea, Usuario pUsuario)
        {
            try
            {
                return DAResumenExtractos.ListarBancos(codlinea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosBusiness", "ListarBancos", ex);
                return null;
            }
        }



        public List<ResumenExtractos> GeneraryListarResumenDetalle(ResumenExtractos entidad, Usuario pUsuario)
        {
            try
            {
                return DAResumenExtractos.GeneraryListarResumenDetalle(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ResumenExtractosBusiness", "GeneraryListarResumenDetalle", ex);
                return null;
            }
        }

    }
}