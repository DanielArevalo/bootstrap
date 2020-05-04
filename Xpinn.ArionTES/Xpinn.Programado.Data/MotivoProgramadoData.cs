using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{
    public class MotivoProgramadoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MotivoProgramadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void creaMotivoProgramado(String pdescripcion, Usuario pUsuario)
        {
            Int64 idcodigo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_motivo_apertura = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo_apertura.ParameterName = "p_cod_motivo_apertura";
                        p_cod_motivo_apertura.Direction = ParameterDirection.Input;
                        p_cod_motivo_apertura.Value = idcodigo;
                        p_cod_motivo_apertura.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo_apertura);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.Value = pdescripcion;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_MOTIVO_PRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "creaMotivoProgramado", ex);
                    }
                }
            }
        }

        public void deleteMotivoProgramado(Int64 idCodigo, Usuario pUsuario) 
        {
            Int64 idcodigo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_motivo_apertura = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo_apertura.ParameterName = "p_cod_motivo_apertura";
                        p_cod_motivo_apertura.Direction = ParameterDirection.Input;
                        p_cod_motivo_apertura.Value = idcodigo;
                        p_cod_motivo_apertura.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo_apertura);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_MOTIVO_PRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "creaMotivoProgramado", ex);
                    }
                }
            }
        }

        public void updateMotivoProgramado(Int64 pIdicodigo, Usuario pUsuario, String pDescripcion) 
        {
            Int64 idcodigo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_motivo_apertura = cmdTransaccionFactory.CreateParameter();
                        p_cod_motivo_apertura.ParameterName = "p_cod_motivo_apertura";
                        p_cod_motivo_apertura.Direction = ParameterDirection.Input;
                        p_cod_motivo_apertura.Value = pIdicodigo;
                        p_cod_motivo_apertura.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_motivo_apertura);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.Value = pDescripcion;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_MOTIVO_PRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "creaMotivoProgramado", ex);
                    }
                }
            }
        }

        public List<MotivoProgramadoE> getListaMotivoProgramado(Usuario pUsuario, String pFiltro) 
        {

            DbDataReader resultado;
            List<MotivoProgramadoE> lstConsulta = new List<MotivoProgramadoE>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * from Motivo_Programado M" + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MotivoProgramadoE entidad = new MotivoProgramadoE();
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.Codigo = Convert.ToInt64(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ListarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public MotivoProgramadoE getMotivoPById(Usuario pUsuario, Int64 pCodigo) 
        {
            MotivoProgramadoE entidad = new MotivoProgramadoE();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * from Motivo_Programado where COD_MOTIVO_APERTURA = " + pCodigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if(resultado.Read())
                        {
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.Codigo = Convert.ToInt64(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ListarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }
    }
}
