using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class EscalafonSalarialData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public EscalafonSalarialData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public EscalafonSalarial CrearEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidescalafon = cmdTransaccionFactory.CreateParameter();
                        pidescalafon.ParameterName = "p_idescalafon";
                        pidescalafon.Value = pEscalafonSalarial.idescalafon;
                        pidescalafon.Direction = ParameterDirection.Input;
                        pidescalafon.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidescalafon);

                        DbParameter pgrado = cmdTransaccionFactory.CreateParameter();
                        pgrado.ParameterName = "p_grado";
                        pgrado.Value = pEscalafonSalarial.grado;
                        pgrado.Direction = ParameterDirection.Input;
                        pgrado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pgrado);

                        DbParameter pasignacion_mensual = cmdTransaccionFactory.CreateParameter();
                        pasignacion_mensual.ParameterName = "p_asignacion_mensual";
                        if (pEscalafonSalarial.asignacion_mensual == null)
                            pasignacion_mensual.Value = DBNull.Value;
                        else
                            pasignacion_mensual.Value = pEscalafonSalarial.asignacion_mensual;
                        pasignacion_mensual.Direction = ParameterDirection.Input;
                        pasignacion_mensual.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pasignacion_mensual);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_ESCALAFON__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEscalafonSalarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EscalafonSalarialData", "CrearEscalafonSalarial", ex);
                        return null;
                    }
                }
            }
        }


        public EscalafonSalarial ModificarEscalafonSalarial(EscalafonSalarial pEscalafonSalarial, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidescalafon = cmdTransaccionFactory.CreateParameter();
                        pidescalafon.ParameterName = "p_idescalafon";
                        pidescalafon.Value = pEscalafonSalarial.idescalafon;
                        pidescalafon.Direction = ParameterDirection.Input;
                        pidescalafon.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidescalafon);

                        DbParameter pgrado = cmdTransaccionFactory.CreateParameter();
                        pgrado.ParameterName = "p_grado";
                        pgrado.Value = pEscalafonSalarial.grado;
                        pgrado.Direction = ParameterDirection.Input;
                        pgrado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pgrado);

                        DbParameter pasignacion_mensual = cmdTransaccionFactory.CreateParameter();
                        pasignacion_mensual.ParameterName = "p_asignacion_mensual";
                        if (pEscalafonSalarial.asignacion_mensual == null)
                            pasignacion_mensual.Value = DBNull.Value;
                        else
                            pasignacion_mensual.Value = pEscalafonSalarial.asignacion_mensual;
                        pasignacion_mensual.Direction = ParameterDirection.Input;
                        pasignacion_mensual.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pasignacion_mensual);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_ESCALAFON__MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEscalafonSalarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EscalafonSalarialData", "ModificarEscalafonSalarial", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEscalafonSalarial(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EscalafonSalarial pEscalafonSalarial = new EscalafonSalarial();
                        pEscalafonSalarial = ConsultarEscalafonSalarial(pId, vUsuario);

                        DbParameter pidescalafon = cmdTransaccionFactory.CreateParameter();
                        pidescalafon.ParameterName = "p_idescalafon";
                        pidescalafon.Value = pEscalafonSalarial.idescalafon;
                        pidescalafon.Direction = ParameterDirection.Input;
                        pidescalafon.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidescalafon);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_ESCALAFON__ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }


        public EscalafonSalarial ConsultarEscalafonSalarial(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            EscalafonSalarial entidad = new EscalafonSalarial();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ESCALAFON_SALARIAL WHERE IDESCALAFON = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDESCALAFON"] != DBNull.Value) entidad.idescalafon = Convert.ToInt64(resultado["IDESCALAFON"]);
                            if (resultado["GRADO"] != DBNull.Value) entidad.grado = Convert.ToString(resultado["GRADO"]);
                            if (resultado["ASIGNACION_MENSUAL"] != DBNull.Value) entidad.asignacion_mensual = Convert.ToDecimal(resultado["ASIGNACION_MENSUAL"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EscalafonSalarialData", "ConsultarEscalafonSalarial", ex);
                        return null;
                    }
                }
            }
        }


        public List<EscalafonSalarial> ListarEscalafonSalarial(string Filtro,EscalafonSalarial pEscalafonSalarial, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EscalafonSalarial> lstEscalafonSalarial = new List<EscalafonSalarial>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ESCALAFON_SALARIAL " + ObtenerFiltro(pEscalafonSalarial) + Filtro+" ORDER BY IDESCALAFON ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EscalafonSalarial entidad = new EscalafonSalarial();
                            if (resultado["IDESCALAFON"] != DBNull.Value) entidad.idescalafon = Convert.ToInt64(resultado["IDESCALAFON"]);
                            if (resultado["GRADO"] != DBNull.Value) entidad.grado = Convert.ToString(resultado["GRADO"]);
                            if (resultado["ASIGNACION_MENSUAL"] != DBNull.Value) entidad.asignacion_mensual = Convert.ToDecimal(resultado["ASIGNACION_MENSUAL"]);
                            lstEscalafonSalarial.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEscalafonSalarial;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EscalafonSalarialData", "ListarEscalafonSalarial", ex);
                        return null;
                    }
                }
            }
        }


    }
}