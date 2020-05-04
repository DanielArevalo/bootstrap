using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class FormaPagoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public FormaPagoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene una lista de cierres
        /// </summary>
        /// <param name="pConceptoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de cierres obtenidos</returns>
        public List<FormaPago> ListarFormaPago(FormaPago pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<FormaPago> lstFormaPago = new List<FormaPago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  tipo_pago " + ObtenerFiltro(pTipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            FormaPago entidad = new FormaPago();

                            if (resultado["COD_TIPO_PAGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstFormaPago.Add(entidad);
                        }

                        return lstFormaPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormaPagoData", "ListarFormaPago", ex);
                        return null;
                    }
                }
            }
        }


    }
}
