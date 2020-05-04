using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    
    public class InversionesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        
        public InversionesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public TipoInversiones CrearTipoInversion(TipoInversiones pTipoInversion, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipo = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo.ParameterName = "p_cod_tipo";
                        pcod_tipo.Value = pTipoInversion.cod_tipo;
                        pcod_tipo.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pcod_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTipoInversion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pTipoInversion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_CON_TIPOINVERS_CREAR" : "USP_XPINN_CON_TIPOINVERS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pTipoInversion.cod_tipo = Convert.ToInt32(pcod_tipo.Value);
                        return pTipoInversion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "CrearTipoInversion", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoInversiones> ListarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoInversiones> lstTipoInversiones = new List<TipoInversiones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_INVERSION "  + pFiltro + " ORDER BY COD_TIPO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoInversiones entidad = new TipoInversiones();
                            if (resultado["COD_TIPO"] != DBNull.Value) entidad.cod_tipo = Convert.ToInt32(resultado["COD_TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoInversiones.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoInversiones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "ListarTipoInversiones", ex);
                        return null;
                    }
                }
            }
        }

        public TipoInversiones ConsultarTipoInversiones(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoInversiones entidad = new TipoInversiones();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_INVERSION " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_TIPO"] != DBNull.Value) entidad.cod_tipo = Convert.ToInt32(resultado["COD_TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "ConsultarTipoInversiones", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarTipoInversiones(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipo = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo.ParameterName = "p_cod_tipo";
                        pcod_tipo.Value = pId;
                        pcod_tipo.Direction = ParameterDirection.Input;
                        pcod_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOINVERS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "ListarTipoInversiones", ex);
                    }
                }
            }
        }



        public Inversiones CrearInversiones(Inversiones pInversiones, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_inversion = cmdTransaccionFactory.CreateParameter();
                        pcod_inversion.ParameterName = "p_cod_inversion";
                        pcod_inversion.Value = pInversiones.cod_inversion;
                        pcod_inversion.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        pcod_inversion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_inversion);

                        DbParameter pnumero_titulo = cmdTransaccionFactory.CreateParameter();
                        pnumero_titulo.ParameterName = "p_numero_titulo";
                        pnumero_titulo.Value = pInversiones.numero_titulo;
                        pnumero_titulo.Direction = ParameterDirection.Input;
                        pnumero_titulo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_titulo);

                        DbParameter pvalor_capital = cmdTransaccionFactory.CreateParameter();
                        pvalor_capital.ParameterName = "p_valor_capital";
                        if (pInversiones.valor_capital == null)
                            pvalor_capital.Value = DBNull.Value;
                        else
                            pvalor_capital.Value = pInversiones.valor_capital;
                        pvalor_capital.Direction = ParameterDirection.Input;
                        pvalor_capital.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_capital);

                        DbParameter pvalor_interes = cmdTransaccionFactory.CreateParameter();
                        pvalor_interes.ParameterName = "p_valor_interes";
                        if (pInversiones.valor_interes == null)
                            pvalor_interes.Value = DBNull.Value;
                        else
                            pvalor_interes.Value = pInversiones.valor_interes;
                        pvalor_interes.Direction = ParameterDirection.Input;
                        pvalor_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_interes);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        if (pInversiones.plazo == null)
                            pplazo.Value = DBNull.Value;
                        else
                            pplazo.Value = pInversiones.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pfecha_emision = cmdTransaccionFactory.CreateParameter();
                        pfecha_emision.ParameterName = "p_fecha_emision";
                        if (pInversiones.fecha_emision == null)
                            pfecha_emision.Value = DBNull.Value;
                        else
                            pfecha_emision.Value = pInversiones.fecha_emision;
                        pfecha_emision.Direction = ParameterDirection.Input;
                        pfecha_emision.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_emision);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pInversiones.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pInversiones.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pInversiones.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = pInversiones.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pInversiones.cod_persona == null)
                            pcod_banco.Value = DBNull.Value;
                        else
                            pcod_banco.Value = pInversiones.cod_persona;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pcod_tipo = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo.ParameterName = "p_cod_tipo";
                        if (pInversiones.cod_tipo == null)
                            pcod_tipo.Value = DBNull.Value;
                        else
                            pcod_tipo.Value = pInversiones.cod_tipo;
                        pcod_tipo.Direction = ParameterDirection.Input;
                        pcod_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pInversiones.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pInversiones.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_CON_INVERSION_CREAR" : "USP_XPINN_CON_INVERSION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pInversiones.cod_inversion = Convert.ToInt64(pcod_inversion.Value);
                        return pInversiones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "CrearInversiones", ex);
                        return null;
                    }
                }
            }
        }

        public Inversiones ConsultarInversiones(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Inversiones entidad = new Inversiones();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT I.*,T.DESCRIPCION AS NOM_TIPO, P.NOMBRE as NOMBREBANCO, P.IDENTIFICACION 
                                        FROM INVERSION I INNER JOIN TIPO_INVERSION T ON T.COD_TIPO = I.COD_TIPO
                                        LEFT JOIN V_PERSONA P ON P.COD_PERSONA = I.COD_PERSONA " + pFiltro.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_INVERSION"] != DBNull.Value) entidad.cod_inversion = Convert.ToInt64(resultado["COD_INVERSION"]);
                            if (resultado["NUMERO_TITULO"] != DBNull.Value) entidad.numero_titulo = Convert.ToString(resultado["NUMERO_TITULO"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToDecimal(resultado["VALOR_INTERES"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["FECHA_EMISION"] != DBNull.Value) entidad.fecha_emision = Convert.ToDateTime(resultado["FECHA_EMISION"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_TIPO"] != DBNull.Value) entidad.cod_tipo = Convert.ToInt32(resultado["COD_TIPO"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "ConsultarInversiones", ex);
                        return null;
                    }
                }
            }
        }

        public List<Inversiones> ListarInversiones(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Inversiones> lstInversiones = new List<Inversiones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT I.*,T.DESCRIPCION AS NOM_TIPO, P.NOMBRE as NOMBREBANCO
                                        FROM INVERSION I INNER JOIN TIPO_INVERSION T ON T.COD_TIPO = I.COD_TIPO
                                        LEFT JOIN V_PERSONA P ON P.COD_PERSONA = I.COD_PERSONA " + pFiltro.ToString();
                        sql += " ORDER BY I.COD_INVERSION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Inversiones entidad = new Inversiones();
                            if (resultado["COD_INVERSION"] != DBNull.Value) entidad.cod_inversion = Convert.ToInt64(resultado["COD_INVERSION"]);
                            if (resultado["NUMERO_TITULO"] != DBNull.Value) entidad.numero_titulo = Convert.ToString(resultado["NUMERO_TITULO"]);
                            if (resultado["VALOR_CAPITAL"] != DBNull.Value) entidad.valor_capital = Convert.ToDecimal(resultado["VALOR_CAPITAL"]);
                            if (resultado["VALOR_INTERES"] != DBNull.Value) entidad.valor_interes = Convert.ToDecimal(resultado["VALOR_INTERES"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["FECHA_EMISION"] != DBNull.Value) entidad.fecha_emision = Convert.ToDateTime(resultado["FECHA_EMISION"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_TIPO"] != DBNull.Value) entidad.cod_tipo = Convert.ToInt32(resultado["COD_TIPO"]);
                            if (resultado["NOM_TIPO"] != DBNull.Value) entidad.nom_tipo = Convert.ToString(resultado["NOM_TIPO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            lstInversiones.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInversiones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "ListarInversiones", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarInversiones(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_inversion = cmdTransaccionFactory.CreateParameter();
                        pcod_inversion.ParameterName = "p_cod_inversion";
                        pcod_inversion.Value = pId;
                        pcod_inversion.Direction = ParameterDirection.Input;
                        pcod_inversion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_inversion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_INVERSION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InversionesData", "EliminarInversiones", ex);
                    }
                }
            }
        }



    }
}
