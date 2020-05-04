using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    
    public class TransicionRangosNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

      
        public TransicionRangosNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public TransicionRangosNIF CrearTransicionRangos(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pRango.codrango;
                        pcodrango.Direction = ParameterDirection.Output;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pRango.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pdias_minimo = cmdTransaccionFactory.CreateParameter();
                        pdias_minimo.ParameterName = "p_dias_minimo";
                        pdias_minimo.Value = pRango.dias_minimo;
                        pdias_minimo.Direction = ParameterDirection.Input;
                        pdias_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_minimo);

                        DbParameter pdias_maximo = cmdTransaccionFactory.CreateParameter();
                        pdias_maximo.ParameterName = "p_dias_maximo";
                        pdias_maximo.Value = pRango.dias_maximo;
                        pdias_maximo.Direction = ParameterDirection.Input;
                        pdias_maximo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSICION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRango.codrango = Convert.ToInt32(pcodrango.Value);
                        return pRango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionRangosNIFData", "CrearTransicionRangos", ex);
                        return null;
                    }
                }
            }
        }


        public TransicionRangosNIF ModificarTransicionRangos(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pRango.codrango;
                        pcodrango.Direction = ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pRango.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pdias_minimo = cmdTransaccionFactory.CreateParameter();
                        pdias_minimo.ParameterName = "p_dias_minimo";
                        pdias_minimo.Value = pRango.dias_minimo;
                        pdias_minimo.Direction = ParameterDirection.Input;
                        pdias_minimo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_minimo);

                        DbParameter pdias_maximo = cmdTransaccionFactory.CreateParameter();
                        pdias_maximo.ParameterName = "p_dias_maximo";
                        pdias_maximo.Value = pRango.dias_maximo;
                        pdias_maximo.Direction = ParameterDirection.Input;
                        pdias_maximo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSICION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pRango;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionRangosNIFData", "ModificarTransicionRangos", ex);
                        return null;
                    }
                }
            }
        }

        
        public List<TransicionRangosNIF> ListarTransicionRango(TransicionRangosNIF pRango, Usuario vUsuario) 
        {
            DbDataReader resultado;
            List<TransicionRangosNIF> lstTransi = new List<TransicionRangosNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TRANSICION_RANGOS_NIF ORDER BY CODRANGO ";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransicionRangosNIF entidad = new TransicionRangosNIF();
                            if (resultado["CODRANGO"] != DBNull.Value) entidad.codrango = Convert.ToInt32(resultado["CODRANGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DIAS_MINIMO"] != DBNull.Value) entidad.dias_minimo = Convert.ToInt32(resultado["DIAS_MINIMO"]);
                            if (resultado["DIAS_MAXIMO"] != DBNull.Value) entidad.dias_maximo = Convert.ToInt32(resultado["DIAS_MAXIMO"]);
                            lstTransi.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionRangosNIFData", "ListarTransicionRango", ex);
                        return null;
                    }
                }
            }
        }




        public void EliminarTransicionRango(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrango = cmdTransaccionFactory.CreateParameter();
                        pcodrango.ParameterName = "p_codrango";
                        pcodrango.Value = pId;
                        pcodrango.Direction = ParameterDirection.Input;
                        pcodrango.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrango);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TRANSICION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransicionRangosNIFData", "EliminarTransicionRango", ex);
                    }
                }
            }
        }


    }
}