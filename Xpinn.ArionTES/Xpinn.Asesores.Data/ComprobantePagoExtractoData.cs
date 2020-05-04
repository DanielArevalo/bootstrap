using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class ComprobantePagoExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public ComprobantePagoExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

       
        public List<ComprobantePagoExtracto> ListarComprobantePagoExtractos(ComprobantePagoExtracto entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ComprobantePagoExtracto> lstComprobantePagoExtractos = new List<ComprobantePagoExtracto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " Select c.numero_radicacion as NumeroDeCredito, c.fecha_proximo_pago as FechaLimiteDePago, "
                                       +"(CASE c.estado WHEN 'C' THEN Calcular_VrAPagar(c.numero_radicacion, SYSDATE)  ELSE 0 END) as PagoMinimo, "
                                       +"(CASE c.estado WHEN 'C' THEN Calcular_TotalAPagar(c.numero_radicacion, SYSDATE) ELSE 0 END) as PagoTotal, "
                                       +"'12132(45456)12487999(098)111' as CadenaCodigoDeBarras "
                                       +"From credito c "
                                       +"Where c.numero_radicacion = "+ entidad.NumeroDeCredito ;
     

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            entidad = new ComprobantePagoExtracto();
                            if (resultado["NumeroDeCredito"] != DBNull.Value) entidad.NumeroDeCredito = Convert.ToInt64(resultado["NumeroDeCredito"]);
                            if (resultado["FechaLimiteDePago"] != DBNull.Value) entidad.FechaLimiteDePago = Convert.ToDateTime(resultado["FechaLimiteDePago"]);
                            if (resultado["PagoMinimo"] != DBNull.Value) entidad.PagoMinimo = Convert.ToDouble(resultado["PagoMinimo"]);
                            if (resultado["PagoTotal"] != DBNull.Value) entidad.PagoTotal = Convert.ToDouble(resultado["PagoTotal"]);
                            if (resultado["CadenaCodigoDeBarras"] != DBNull.Value) entidad.CadenaCodigoDeBarras = Convert.ToString(resultado["CadenaCodigoDeBarras"]);
                            lstComprobantePagoExtractos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComprobantePagoExtractos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobantePagoExtractoData", "ListarComprobantePagoExtractos", ex);                        
                        return null;
                    }
                }                
            }
        }



    }
}
