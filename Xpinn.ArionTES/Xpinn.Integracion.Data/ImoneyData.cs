using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class ImoneyData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ImoneyData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Entities.Fullmovil consultarTransaccion(int id, Usuario pUsuario)
        {
            DbDataReader resultado;
            Entities.Fullmovil entidad = new Entities.Fullmovil();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from INT_TRAN_IMONEY where Id_Transaccion = "+id;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_TRANSACCION"] != DBNull.Value) entidad.id_transaccion = Convert.ToInt32(resultado["ID_TRANSACCION"]);
                            if (resultado["ID_REF"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID_REF"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO"] != DBNull.Value) entidad.phone = Convert.ToString(resultado["NUMERO"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["SUB_TOTAL"] != DBNull.Value) entidad.subtotal = Convert.ToDecimal(resultado["SUB_TOTAL"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["TOTAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.description = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PLAN_ID"] != DBNull.Value) entidad.package_id = Convert.ToString(resultado["PLAN_ID"]);
                            if (resultado["DESCRIPCION_PLAN"] != DBNull.Value) entidad.descripcion_plan = Convert.ToString(resultado["DESCRIPCION_PLAN"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.status = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_EXPIRACION"] != DBNull.Value) entidad.expiration_date = Convert.ToString(resultado["FECHA_EXPIRACION"]);
                            if (resultado["FECHA_TRAN"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["FECHA_TRAN"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IntegracionData", "ObtenerIntegracion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Fullmovil> listarTransaccionesPersona(string cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Fullmovil> lista = new List<Fullmovil>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from INT_TRAN_IMONEY where cod_persona = " + cod_persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Fullmovil entidad = new Fullmovil();
                            if (resultado["ID_TRANSACCION"] != DBNull.Value) entidad.id_transaccion = Convert.ToInt32(resultado["ID"]);
                            if (resultado["ID_REF"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID_REF"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO"] != DBNull.Value) entidad.phone = Convert.ToString(resultado["NUMERO"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["SUB_TOTAL"] != DBNull.Value) entidad.subtotal = Convert.ToDecimal(resultado["SUB_TOTAL"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["TOTAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.description = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PLAN_ID"] != DBNull.Value) entidad.package_id = Convert.ToString(resultado["PLAN_ID"]);
                            if (resultado["DESCRIPCION_PLAN"] != DBNull.Value) entidad.descripcion_plan = Convert.ToString(resultado["DESCRIPCION_PLAN"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.status = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_EXPIRACION"] != DBNull.Value) entidad.expiration_date = Convert.ToString(resultado["FECHA_EXPIRACION"]);
                            if (resultado["FECHA_TRAN"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["FECHA_TRAN"]);
                            lista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);                        
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IntegracionData", "ObtenerIntegracion", ex);
                        return null;
                    }
                }
            }
        }

        public Fullmovil guardarTransaccion(Fullmovil transac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_TRANSACCION = cmdTransaccionFactory.CreateParameter();
                        P_ID_TRANSACCION.ParameterName = "P_ID_TRANSACCION";
                        P_ID_TRANSACCION.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(P_ID_TRANSACCION);

                        DbParameter P_ID_REF = cmdTransaccionFactory.CreateParameter();
                        P_ID_REF.ParameterName = "P_ID_REF";
                        P_ID_REF.Value = transac.id;
                        P_ID_REF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_REF);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = transac.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_NUMERO = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO.ParameterName = "P_NUMERO";
                        P_NUMERO.Value = transac.phone;
                        P_NUMERO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO);

                        DbParameter P_OPERADOR = cmdTransaccionFactory.CreateParameter();
                        P_OPERADOR.ParameterName = "P_OPERADOR";
                        P_OPERADOR.Value = transac.operador;
                        P_OPERADOR.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_OPERADOR);

                        DbParameter P_SUB_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_SUB_TOTAL.ParameterName = "P_SUB_TOTAL";
                        P_SUB_TOTAL.Value = transac.subtotal;
                        P_SUB_TOTAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_SUB_TOTAL);

                        DbParameter P_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL.ParameterName = "P_TOTAL";
                        P_TOTAL.Value = transac.total;
                        P_TOTAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL);

                        DbParameter P_DESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION.ParameterName = "P_DESCRIPCION";
                        P_DESCRIPCION.Value = transac.description;
                        P_DESCRIPCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION);

                        DbParameter P_PLAN_ID = cmdTransaccionFactory.CreateParameter();
                        P_PLAN_ID.ParameterName = "P_PLAN_ID";
                        P_PLAN_ID.Value = transac.description;
                        P_PLAN_ID.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_PLAN_ID);

                        DbParameter P_DESCRIPCION_PLAN = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION_PLAN.ParameterName = "P_DESCRIPCION_PLAN";
                        P_DESCRIPCION_PLAN.Value = transac.description;
                        P_DESCRIPCION_PLAN.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION_PLAN);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = transac.status;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_TRAN_IMONEY_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        transac.id_transaccion = Convert.ToInt32(P_ID_TRANSACCION.Value);
                        transac.fecha_tran = DateTime.Now;

                        return transac;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImoneyData", "guardarTransaccion", ex);
                        return null;
                    }
                }
            }
        }

        public Fullmovil actualizarTransaccion(Fullmovil transac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_TRANSACCION = cmdTransaccionFactory.CreateParameter();
                        P_ID_TRANSACCION.ParameterName = "P_ID_TRANSACCION";
                        P_ID_TRANSACCION.Value = transac.id_transaccion;
                        P_ID_TRANSACCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_TRANSACCION);

                        DbParameter P_ID_REF = cmdTransaccionFactory.CreateParameter();
                        P_ID_REF.ParameterName = "P_ID_REF";
                        P_ID_REF.Value = transac.id;
                        P_ID_REF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_REF);

                        DbParameter P_SUB_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_SUB_TOTAL.ParameterName = "P_SUB_TOTAL";
                        P_SUB_TOTAL.Value = transac.subtotal;
                        P_SUB_TOTAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_SUB_TOTAL);

                        DbParameter P_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL.ParameterName = "P_TOTAL";
                        P_TOTAL.Value = transac.total;
                        P_TOTAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = transac.status;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_TRAN_IMONEY_ACT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return transac;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImoneyData", "actualizarTransaccion", ex);
                        return null;
                    }
                }
            }
        }

        public Fullmovil actualizarEstadoTransaccion(Fullmovil transac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_TRANSACCION = cmdTransaccionFactory.CreateParameter();
                        P_ID_TRANSACCION.ParameterName = "P_ID_TRANSACCION";
                        P_ID_TRANSACCION.Value = transac.id_transaccion;
                        P_ID_TRANSACCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_TRANSACCION);

                        DbParameter P_ID_REF = cmdTransaccionFactory.CreateParameter();
                        P_ID_REF.ParameterName = "P_ID_REF";
                        P_ID_REF.Value = transac.id;
                        P_ID_REF.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_REF);                       

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = transac.description;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_TRAN_IMONEY_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return transac;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImoneyData", "actualizarEstadoTransaccion", ex);
                        return null;
                    }
                }
            }
        }
    }
}
