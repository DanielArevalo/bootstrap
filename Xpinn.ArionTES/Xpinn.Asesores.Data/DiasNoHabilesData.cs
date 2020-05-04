using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla atributos
    /// </summary>
    public class DiasNoHabilesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla atributos
        /// </summary>
        public DiasNoHabilesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla atributos de la base de datos
        /// </summary>
        /// <param name="pDias">Entidad Atributo</param>
        /// <returns>Entidad Atributo creada</returns>
        public Dias_no_habiles CrearDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDias.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pmes = cmdTransaccionFactory.CreateParameter();
                        pmes.ParameterName = "p_mes";
                        pmes.Value = pDias.mes;
                        pmes.Direction = ParameterDirection.Input;
                        pmes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmes);

                        DbParameter pano = cmdTransaccionFactory.CreateParameter();
                        pano.ParameterName = "p_ano";
                        pano.Value = pDias.ano;
                        pano.Direction = ParameterDirection.Input;
                        pano.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pano);

                        DbParameter pdia_festivo = cmdTransaccionFactory.CreateParameter();
                        pdia_festivo.ParameterName = "p_dia_festivo";
                        pdia_festivo.Value = pDias.dia_festivo;
                        pdia_festivo.Direction = ParameterDirection.Input;
                        pdia_festivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdia_festivo);

                        DbParameter pdia_semana = cmdTransaccionFactory.CreateParameter();
                        pdia_semana.ParameterName = "p_dia_semana";
                        pdia_semana.Value = pDias.dia_semana;
                        pdia_semana.Direction = ParameterDirection.Input;
                        pdia_semana.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdia_semana);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DIASNOHABI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiasNoHabilesData", "CrearDiasNoHabiles", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM DIAS_NO_HABILES WHERE MES = "+pDias.mes+" AND ANO = " + pDias.ano;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiasNoHabilesData", "EliminarDias", ex);
                    }
                }
            }
        }


        public List<Dias_no_habiles> ListarDiasNoHabiles(Dias_no_habiles pDias, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Dias_no_habiles> lstDias = new List<Dias_no_habiles>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DIAS_NO_HABILES " + ObtenerFiltro(pDias) + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Dias_no_habiles entidad = new Dias_no_habiles();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToInt32(resultado["MES"]);
                            if (resultado["ANO"] != DBNull.Value) entidad.ano = Convert.ToInt32(resultado["ANO"]);
                            if (resultado["DIA_FESTIVO"] != DBNull.Value) entidad.dia_festivo = Convert.ToInt32(resultado["DIA_FESTIVO"]);
                            if (resultado["DIA_SEMANA"] != DBNull.Value) entidad.dia_semana = Convert.ToInt32(resultado["DIA_SEMANA"]);
                            string fechaunida = entidad.dia_festivo + "/" + entidad.mes + "/" + entidad.ano;
                            try
                            {
                                entidad.fecha = Convert.ToDateTime(fechaunida);
                            }
                            catch
                            {
                                entidad.fecha = DateTime.MinValue;
                            }
                            lstDias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiasNoHabilesData", "ListarDiasNoHabiles", ex);
                        return null;
                    }
                }
            }
        }


        public Dias_no_habiles ConsultarDiasNoHabiles(Dias_no_habiles pDia, Usuario vUsuario)
        {
            DbDataReader resultado;
            Dias_no_habiles entidad = new Dias_no_habiles();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DIAS_NO_HABILES WHERE DIA_FESTIVO = "+pDia.dia_festivo+" and MES = "+pDia.mes+" and ANO = " + pDia.ano;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt32(resultado["CONSECUTIVO"]);
                            if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToInt32(resultado["MES"]);
                            if (resultado["ANO"] != DBNull.Value) entidad.ano = Convert.ToInt32(resultado["ANO"]);
                            if (resultado["DIA_FESTIVO"] != DBNull.Value) entidad.dia_festivo = Convert.ToInt32(resultado["DIA_FESTIVO"]);
                            if (resultado["DIA_SEMANA"] != DBNull.Value) entidad.dia_semana = Convert.ToInt32(resultado["DIA_SEMANA"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DiasNoHabilesData", "ConsultarDiasNoHabiles", ex);
                        return null;
                    }
                }
            }
        }


        public Int32 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int32 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(CONSECUTIVO) + 1 FROM DIAS_NO_HABILES ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt32(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        return 1;
                    }
                }
            }
        }

    }
}