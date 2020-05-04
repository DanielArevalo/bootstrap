using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    public class TipoImpuestoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TipoImpuestoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public TipoImpuesto CrearTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pTipoImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pnombre_impuesto = cmdTransaccionFactory.CreateParameter();
                        pnombre_impuesto.ParameterName = "p_nombre_impuesto";
                        pnombre_impuesto.Value = pTipoImpuesto.nombre_impuesto;
                        pnombre_impuesto.Direction = ParameterDirection.Input;
                        pnombre_impuesto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_impuesto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTipoImpuesto.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pTipoImpuesto.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter P_PRINCIPAL = cmdTransaccionFactory.CreateParameter();
                        P_PRINCIPAL.ParameterName = "P_PRINCIPAL";
                        P_PRINCIPAL.Value = pTipoImpuesto.principal;
                        P_PRINCIPAL.Direction = ParameterDirection.Input;
                        P_PRINCIPAL.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PRINCIPAL);

                        DbParameter P_DEPENDE_DE = cmdTransaccionFactory.CreateParameter();
                        P_DEPENDE_DE.ParameterName = "P_DEPENDE_DE";
                        P_DEPENDE_DE.Value = pTipoImpuesto.depende_de;
                        P_DEPENDE_DE.Direction = ParameterDirection.Input;
                        P_DEPENDE_DE.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_DEPENDE_DE);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOIMPUES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoImpuestoData", "CrearTipoImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public TipoImpuesto ModificarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pTipoImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pnombre_impuesto = cmdTransaccionFactory.CreateParameter();
                        pnombre_impuesto.ParameterName = "p_nombre_impuesto";
                        pnombre_impuesto.Value = pTipoImpuesto.nombre_impuesto;
                        pnombre_impuesto.Direction = ParameterDirection.Input;
                        pnombre_impuesto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_impuesto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTipoImpuesto.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pTipoImpuesto.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOIMPUES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoImpuestoData", "ModificarTipoImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarTipoImpuesto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoImpuesto pTipoImpuesto = new TipoImpuesto();
                        pTipoImpuesto = ConsultarTipoImpuesto(pId, vUsuario);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pTipoImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOIMPUES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoImpuestoData", "EliminarTipoImpuesto", ex);
                    }
                }
            }
        }


        public TipoImpuesto ConsultarTipoImpuesto(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoImpuesto entidad = new TipoImpuesto();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoImpuesto WHERE COD_TIPO_IMPUESTO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nombre_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("TipoImpuestoData", "ConsultarTipoImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoImpuesto> ListarTipoImpuesto(TipoImpuesto pTipoImpuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoImpuesto> lstTipoImpuesto = new List<TipoImpuesto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoImpuesto " + ObtenerFiltro(pTipoImpuesto) + " ORDER BY COD_TIPO_IMPUESTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoImpuesto entidad = new TipoImpuesto();
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nombre_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoImpuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoImpuestoData", "ListarTipoImpuesto", ex);
                        return null;
                    }
                }
            }
        }


    }
}