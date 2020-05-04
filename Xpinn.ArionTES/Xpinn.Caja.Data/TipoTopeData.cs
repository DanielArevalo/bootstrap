using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    public class TipoTopeData:GlobalData
    {
        
        protected ConnectionDataBase dbConnectionFactory;

        public TipoTopeData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene un registro de la tabla TipoTope-TopeCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>TipoTope-TopeCaja consultada</returns>
        public TipoTope ConsultarTipoTopeCaja(Int64 pId, Int64 pMon, Int64 pCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoTope entidad = new TipoTope();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM TOPESCAJA where tipo_tope=" + pId.ToString() + " and cod_caja=" + pCaja +" and cod_moneda="+pMon;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["valor_minimo"] != DBNull.Value) entidad.valor_minimo = long.Parse(resultado["valor_minimo"].ToString());
                            if (resultado["valor_maximo"] != DBNull.Value) entidad.valor_maximo  = long.Parse(resultado["valor_maximo"].ToString());
                        }
                        else
                        {
                            entidad.valor_minimo = 0;
                            entidad.valor_maximo = 0;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTopeData", "ConsultarTipoTopeCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Tipos Tope dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Tope obtenidas</returns>
        public List<TipoTope> ListarTipoTope(TipoTope pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoTope> lstTipoTope = new List<TipoTope>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT a.tipo_tope tipotope, a.descripcion descTope, b.cod_moneda cod_moneda,b.descripcion descMoneda,b.simbolo simbol FROM TIPO_TOPE_CAJA a,tipomoneda b " + ObtenerFiltro(pEntidad) + " Order By a.tipo_tope asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoTope entidad = new TipoTope();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToString(resultado["cod_moneda"]);
                            if (resultado["descTope"] != DBNull.Value) entidad.descTope = Convert.ToString(resultado["descTope"]);
                            if (resultado["tipotope"] != DBNull.Value) entidad.tipotope= long.Parse(resultado["tipotope"].ToString());
                            if (resultado["descMoneda"] != DBNull.Value) entidad.descMoneda = Convert.ToString(resultado["descMoneda"]);
                            if (resultado["simbol"] != DBNull.Value) entidad.simbol = Convert.ToString(resultado["simbol"]);
                            lstTipoTope.Add(entidad);
                        }

                        return lstTipoTope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTopeData", "ListarTipoTope", ex);
                        return null;
                    }
                }
            }
        }
    }
}
