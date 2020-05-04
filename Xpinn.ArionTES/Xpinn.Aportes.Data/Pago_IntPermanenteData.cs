using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    public class Pago_IntPermanenteData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public Pago_IntPermanenteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public void CrearPago_IntPermanente(Pago_IntPermanente pPago_IntPermanente, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pPago_IntPermanente.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Input;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pPago_IntPermanente.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pPago_IntPermanente.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPago_IntPermanente.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPago_IntPermanente.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pPago_IntPermanente.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pPago_IntPermanente.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPago_IntPermanente.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PAGO_INTPE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                  
                }
            }
        }


        public Pago_IntPermanente ModificarPago_IntPermanente(Pago_IntPermanente pPago_IntPermanente, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidpagperm = cmdTransaccionFactory.CreateParameter();
                        pidpagperm.ParameterName = "p_idpagperm";
                        pidpagperm.Value = pPago_IntPermanente.idpagperm;
                        pidpagperm.Direction = ParameterDirection.Input;
                        pidpagperm.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidpagperm);

                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pPago_IntPermanente.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Input;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pPago_IntPermanente.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPago_IntPermanente.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPago_IntPermanente.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pPago_IntPermanente.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pPago_IntPermanente.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPago_IntPermanente.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PAGO_INTPE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPago_IntPermanente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Pago_IntPermanenteData", "ModificarPago_IntPermanente", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPago_IntPermanente(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Pago_IntPermanente pPago_IntPermanente = new Pago_IntPermanente();
                        pPago_IntPermanente = ConsultarPago_IntPermanente(pId, vUsuario);

                        DbParameter pidpagperm = cmdTransaccionFactory.CreateParameter();
                        pidpagperm.ParameterName = "p_idpagperm";
                        pidpagperm.Value = pPago_IntPermanente.idpagperm;
                        pidpagperm.Direction = ParameterDirection.Input;
                        pidpagperm.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidpagperm);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PAGO_INTPE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Pago_IntPermanenteData", "EliminarPago_IntPermanente", ex);
                    }
                }
            }
        }


        public Pago_IntPermanente ConsultarPago_IntPermanente(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Pago_IntPermanente entidad = new Pago_IntPermanente();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAGO_INTPERMANENTE WHERE IDPAGPERM = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPAGPERM"] != DBNull.Value) entidad.idpagperm = Convert.ToInt32(resultado["IDPAGPERM"]);
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("Pago_IntPermanenteData", "ConsultarPago_IntPermanente", ex);
                        return null;
                    }
                }
            }
        }


        public List<Pago_IntPermanente> ListarPago_IntPermanente(Pago_IntPermanente pPago_IntPermanente, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Pago_IntPermanente> lstPago_IntPermanente = new List<Pago_IntPermanente>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAGO_INTPERMANENTE " + ObtenerFiltro(pPago_IntPermanente) + " ORDER BY IDPAGPERM ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Pago_IntPermanente entidad = new Pago_IntPermanente();
                            if (resultado["IDPAGPERM"] != DBNull.Value) entidad.idpagperm = Convert.ToInt32(resultado["IDPAGPERM"]);
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstPago_IntPermanente.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPago_IntPermanente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Pago_IntPermanenteData", "ListarPago_IntPermanente", ex);
                        return null;
                    }
                }
            }
        }

        public List<Pago_IntPermanente> ListarIntPermanenteRec(Int64 pNumeroRecaudo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Pago_IntPermanente> lstPago_IntPermanente = new List<Pago_IntPermanente>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT A.* FROM DETRECAUDO_MASIVO A 
                                        INNER JOIN PAGO_INTPERMANENTE P on P.COD_PERSONA = A.COD_CLIENTE and P.NUMERO_APORTE = A.NUMERO_PRODUCTO
                                        LEFT JOIN APORTE R ON P.NUMERO_APORTE = R.NUMERO_APORTE 
                                        WHERE A.NUMERO_RECAUDO = " + pNumeroRecaudo + @" AND (P.PAGADO = 0 OR P.PAGADO IS NULL) AND R.ESTADO = 1 ORDER BY IDDETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Pago_IntPermanente entidad = new Pago_IntPermanente();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt32(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstPago_IntPermanente.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPago_IntPermanente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Pago_IntPermanenteData", "ListarIntPermanenteRec", ex);
                        return null;
                    }
                }
            }
        }

    }
}