using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class CuentaPorCobrarCreData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CuentaPorCobrarCreData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void CrearCuentaPorCobrar(CuentaPorCobrarCre cuentaporcobrar, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = cuentaporcobrar.numero_radicacion;
                        pnumero_radicacion.DbType = DbType.Int64;
                        pnumero_radicacion.Direction = ParameterDirection.Input;

                        DbParameter ptipo_cta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cta.ParameterName = "ptipo_cta";
                        ptipo_cta.Value = cuentaporcobrar.tipo_cta;
                        ptipo_cta.DbType = DbType.Int64;
                        ptipo_cta.Direction = ParameterDirection.Input;

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = cuentaporcobrar.cod_ope;
                        pcod_ope.DbType = DbType.Int64;
                        pcod_ope.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cta = cmdTransaccionFactory.CreateParameter();
                        pfecha_cta.ParameterName = "pfecha_cta";
                        pfecha_cta.Value = cuentaporcobrar.fecha_cta;
                        pfecha_cta.DbType = DbType.DateTime;
                        pfecha_cta.Direction = ParameterDirection.Input;

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "ptotal";
                        ptotal.Value = cuentaporcobrar.total;
                        ptotal.DbType = DbType.Double;
                        ptotal.Direction = ParameterDirection.Input;

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcod_usuario";
                        pcod_usuario.Value = cuentaporcobrar.cod_usuario;
                        pcod_usuario.DbType = DbType.Int64;
                        pcod_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo_cta);
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cta);
                        cmdTransaccionFactory.Parameters.Add(ptotal);
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CUENTAPORCOBRAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }

        public void CerrarCuentaPorCobrar(CuentaPorCobrarCre cuentaporcobrar, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = cuentaporcobrar.numero_radicacion;
                        pnumero_radicacion.DbType = DbType.Int64;
                        pnumero_radicacion.Direction = ParameterDirection.Input;

                        DbParameter ptipo_cta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cta.ParameterName = "ptipo_cta";
                        ptipo_cta.Value = cuentaporcobrar.tipo_cta;
                        ptipo_cta.DbType = DbType.Int64;
                        ptipo_cta.Direction = ParameterDirection.Input;

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = cuentaporcobrar.cod_ope;
                        pcod_ope.DbType = DbType.Int64;
                        pcod_ope.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cta = cmdTransaccionFactory.CreateParameter();
                        pfecha_cta.ParameterName = "pfecha_cta";
                        pfecha_cta.Value = cuentaporcobrar.fecha_cta;
                        pfecha_cta.DbType = DbType.DateTime;
                        pfecha_cta.Direction = ParameterDirection.Input;

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcod_usuario";
                        pcod_usuario.Value = cuentaporcobrar.cod_usuario;
                        pcod_usuario.DbType = DbType.Int64;
                        pcod_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo_cta);
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cta);
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTACOBRAR_ACT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }

        public double ConsultarCuentaPorCobrar(CuentaPorCobrarCre pcuentaporcobrar, Usuario pUsuario, ref string Error)
        {
            DbDataReader resultado = default(DbDataReader);
            double valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "Select Sum(saldo) as saldo From cuenta_porcobrar_cre Where numero_radicacion = " + pcuentaporcobrar.numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                            
                            if (resultado["SALDO"] != DBNull.Value) valor = Convert.ToDouble(resultado["SALDO"]);
                            return valor;
                        }

                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarCuentaPorCobrar", ex);
                        return valor;
                    }
                }
            }
        }

    }
}