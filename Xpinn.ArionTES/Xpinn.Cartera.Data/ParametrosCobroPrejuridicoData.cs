using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ParametroCobroPrejuridicoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ParametroCobroPrejuridicoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ParametroCobroPrejuridico CrearParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParametroCobroPrejuridico.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter ptipo_cobro = cmdTransaccionFactory.CreateParameter();
                        ptipo_cobro.ParameterName = "p_tipo_cobro";
                        if (pParametroCobroPrejuridico.tipo_cobro == null)
                            ptipo_cobro.Value = DBNull.Value;
                        else
                            ptipo_cobro.Value = pParametroCobroPrejuridico.tipo_cobro;
                        ptipo_cobro.Direction = ParameterDirection.Input;
                        ptipo_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cobro);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pParametroCobroPrejuridico.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pParametroCobroPrejuridico.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pParametroCobroPrejuridico.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pParametroCobroPrejuridico.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter pforma_cobro = cmdTransaccionFactory.CreateParameter();
                        pforma_cobro.ParameterName = "p_forma_cobro";
                        pforma_cobro.Value = pParametroCobroPrejuridico.forma_cobro;
                        pforma_cobro.Direction = ParameterDirection.Input;
                        pforma_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_cobro);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pParametroCobroPrejuridico.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pParametroCobroPrejuridico.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pParametroCobroPrejuridico.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pParametroCobroPrejuridico.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PARAMETRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParametroCobroPrejuridico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCobroPrejuridicoData", "CrearParametroCobroPrejuridico", ex);
                        return null;
                    }
                }
            }
        }


        public ParametroCobroPrejuridico ModificarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParametroCobroPrejuridico.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter ptipo_cobro = cmdTransaccionFactory.CreateParameter();
                        ptipo_cobro.ParameterName = "p_tipo_cobro";
                        if (pParametroCobroPrejuridico.tipo_cobro == null)
                            ptipo_cobro.Value = DBNull.Value;
                        else
                            ptipo_cobro.Value = pParametroCobroPrejuridico.tipo_cobro;
                        ptipo_cobro.Direction = ParameterDirection.Input;
                        ptipo_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cobro);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pParametroCobroPrejuridico.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pParametroCobroPrejuridico.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pParametroCobroPrejuridico.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pParametroCobroPrejuridico.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter pforma_cobro = cmdTransaccionFactory.CreateParameter();
                        pforma_cobro.ParameterName = "p_forma_cobro";
                        pforma_cobro.Value = pParametroCobroPrejuridico.forma_cobro;
                        pforma_cobro.Direction = ParameterDirection.Input;
                        pforma_cobro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_cobro);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pParametroCobroPrejuridico.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pParametroCobroPrejuridico.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pParametroCobroPrejuridico.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pParametroCobroPrejuridico.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PARAMETRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParametroCobroPrejuridico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCobroPrejuridicoData", "ModificarParametroCobroPrejuridico", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarParametroCobroPrejuridico(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ParametroCobroPrejuridico pParametroCobroPrejuridico = new ParametroCobroPrejuridico();
                        pParametroCobroPrejuridico = ConsultarParametroCobroPrejuridico(pId, vUsuario);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParametroCobroPrejuridico.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PARAMETRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCobroPrejuridicoData", "EliminarParametroCobroPrejuridico", ex);
                    }
                }
            }
        }


        public ParametroCobroPrejuridico ConsultarParametroCobroPrejuridico(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametroCobroPrejuridico entidad = new ParametroCobroPrejuridico();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM parametro_cobro_prejuridico WHERE IDPARAMETRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["TIPO_COBRO"] != DBNull.Value) entidad.tipo_cobro = Convert.ToInt32(resultado["TIPO_COBRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["FORMA_COBRO"] != DBNull.Value) entidad.forma_cobro = Convert.ToInt32(resultado["FORMA_COBRO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
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
                        BOExcepcion.Throw("ParametroCobroPrejuridicoData", "ConsultarParametroCobroPrejuridico", ex);
                        return null;
                    }
                }
            }
        }


        public List<ParametroCobroPrejuridico> ListarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametroCobroPrejuridico> lstParametroCobroPrejuridico = new List<ParametroCobroPrejuridico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM parametro_cobro_prejuridico WHERE TIPO_COBRO = " + pParametroCobroPrejuridico.forma_cobro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ParametroCobroPrejuridico entidad = new ParametroCobroPrejuridico();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["TIPO_COBRO"] != DBNull.Value) entidad.tipo_cobro = Convert.ToInt32(resultado["TIPO_COBRO"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                            if (resultado["FORMA_COBRO"] != DBNull.Value) entidad.forma_cobro = Convert.ToInt32(resultado["FORMA_COBRO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToInt32(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstParametroCobroPrejuridico.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParametroCobroPrejuridico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCobroPrejuridicoData", "ListarParametroCobroPrejuridico", ex);
                        return null;
                    }
                }
            }
        }


    }
}