using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla OBComponenteAdicional
    /// </summary>
    public class ComponenteAdicionalData : GlobalData
    {
         protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ComponenteAdicional
        /// </summary>
        public ComponenteAdicionalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCOMPONENTESADICIONAL dados unos filtros
        /// </summary>
        /// <param name="pComponenteAdicional">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComponenteAdicional obtenidos</returns>
        public List<ComponenteAdicional> ListarComponenteAdicional(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ComponenteAdicional> lstComponenteAdicional = new List<ComponenteAdicional>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  CODCOMPONENTE, CODOBLIGACION,(select y.nombre from obcomponente y where y.CODCOMPONENTE=x.CODCOMPONENTE and visible=1) NOMCOMPONENTE, " +
                                      " decode(x.FORMULA,1,'Constante',decode(x.FORMULA,2,'Monto Por Valor',decode(x.FORMULA,3,'Cuota por Valor',decode(x.FORMULA,4,'(Valor por Monto)/Plazo')))) NOMFORMULA,"+
                                      "x.VALOR VALOR,x.FINANCIADO FINANCIADO, x.VALORCALCULADO VALORCALCULADO  FROM  OBCOMPONENTESADICIONALES x Where x.codobligacion= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ComponenteAdicional entidad = new ComponenteAdicional();
                            if (resultado["CODCOMPONENTE"] != DBNull.Value) entidad.CODCOMPONENTE = Convert.ToInt64(resultado["CODCOMPONENTE"]);                         
                            if (resultado["CODOBLIGACION"] != DBNull.Value) entidad.CODOBLIGACION= Convert.ToInt64(resultado["CODOBLIGACION"]);
                            if (resultado["NOMCOMPONENTE"] != DBNull.Value) entidad.NOMCOMPONENTE= Convert.ToString(resultado["NOMCOMPONENTE"]);
                            if (resultado["NOMFORMULA"] != DBNull.Value) entidad.NOMFORMULA = Convert.ToString(resultado["NOMFORMULA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["FINANCIADO"] != DBNull.Value) entidad.FINANCIADO = Convert.ToInt64(resultado["FINANCIADO"]);
                            if (resultado["VALORCALCULADO"] != DBNull.Value) entidad.VALOR_CALCULADO = Convert.ToInt64(resultado["VALORCALCULADO"]);

                            entidad.DESCRIPCION = entidad.NOMCOMPONENTE + ' ' + entidad.VALOR_CALCULADO;

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex) 
                    {
                        BOExcepcion.Throw("ComponenteAdicionalData", "ListarComponenteAdicional", ex);
                        return null;
                    }
                }
            }
        }
    }
}
