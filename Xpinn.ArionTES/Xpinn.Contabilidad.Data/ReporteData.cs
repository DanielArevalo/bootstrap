using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    public class ReporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteInteresPagados(Usuario pUsuario, DateTime fechaini, DateTime fechafinal)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_INTERES_PAG_CREDITOS A WHERE FECHA_OPER  between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And  to_date('" + fechafinal.ToString("dd/MM/yyyy") + "' ,'dd/MM/yyyy') ORDER BY 1 ASC";
                         

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.Direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                         
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteInteresPagados", ex);
                        return null;
                    }
                }
            }
        }
       
    }
}
