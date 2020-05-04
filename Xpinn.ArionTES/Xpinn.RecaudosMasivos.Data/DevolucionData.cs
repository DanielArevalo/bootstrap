using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class DevolucionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public DevolucionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Devolucion Crear_Mod_Devolucion(Devolucion pDevol, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pDevol.num_devolucion;
                        if (opcion == 1) //CREAR
                            pnum_devolucion.Direction = ParameterDirection.Output;
                        else if (opcion == 2)//MODIFICAR
                            pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pDevol.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pDevol.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        pconcepto.Value = pDevol.concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pfecha_descuento = cmdTransaccionFactory.CreateParameter();
                        pfecha_descuento.ParameterName = "p_fecha_descuento";
                        if (pDevol.fecha_descuento != DateTime.MinValue) pfecha_descuento.Value = pDevol.fecha_descuento; else pfecha_descuento.Value = DBNull.Value;
                        pfecha_descuento.Direction = ParameterDirection.Input;
                        pfecha_descuento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_descuento);

                        DbParameter pfecha_devolucion = cmdTransaccionFactory.CreateParameter();
                        pfecha_devolucion.ParameterName = "p_fecha_devolucion";
                        pfecha_devolucion.Value = pDevol.fecha_devolucion;
                        pfecha_devolucion.Direction = ParameterDirection.Input;
                        pfecha_devolucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_devolucion);

                        DbParameter pnum_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnum_recaudo.ParameterName = "p_num_recaudo";
                        if (pDevol.num_recaudo != 0 && pDevol.num_recaudo != null) pnum_recaudo.Value = pDevol.num_recaudo; else pnum_recaudo.Value = DBNull.Value;
                        pnum_recaudo.Direction = ParameterDirection.Input;
                        pnum_recaudo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_recaudo);

                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        if (pDevol.iddetalle != 0) piddetalle.Value = pDevol.iddetalle; else piddetalle.Value = DBNull.Value;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDevol.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter porigen = cmdTransaccionFactory.CreateParameter();
                        porigen.ParameterName = "p_origen";
                        if (pDevol.origen != null) porigen.Value = pDevol.origen; else porigen.Value = DBNull.Value;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pDevol.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pDevol.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)//CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DEVOLUCION_CREAR";
                        else if (opcion == 2) //MODIFICAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DEVOLUCION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1) // CREAR
                            pDevol.num_devolucion = Convert.ToInt32(pnum_devolucion.Value);
                        return pDevol;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "Crear_Mod_Devolucion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDevolucion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pId;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = 2;
                        p_tipo.Direction = ParameterDirection.Input;
                        p_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DEVOLUCION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "EliminarDevolucion", ex);
                    }
                }
            }
        }


        public List<Devolucion> ListarDevolucion(Devolucion pDevolucion, DateTime pFecha, Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<Devolucion> lstEmpresaRecaudo = new List<Devolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select oficina.nombre as oficina ,d.num_devolucion, d.cod_persona, p.identificacion, p.primer_nombre||' '||p.segundo_nombre "
                                        + "||' '|| p.primer_apellido||' '||p.segundo_apellido as Nombre, p.cod_nomina, d.fecha_devolucion, d.fecha_descuento, "
                                        + "d.num_recaudo, d.iddetalle, d.valor, d.saldo, d.origen, case d.estado when '0' then 'PENDIENTE' when '1' then 'PENDIENTE' when '2' then 'PAGADA' "
                                        + "when '3' then 'ANULADA' end as Estado, d.concepto "
                                        + "From devolucion d Left Join persona p on d.cod_persona = p.cod_persona inner join oficina on p.cod_oficina=oficina.cod_oficina";
                        if (filtro.Trim() != "")
                        {
                            if (filtro.TrimStart().ToUpper().StartsWith("AND "))
                                if (filtro.Length > 4)
                                    filtro = filtro.Substring(4, filtro.Length - 4);
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And " + filtro;
                            else
                                sql += " Where " + filtro;
                        }
                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " d.fecha_devolucion  = To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " d.fecha_devolucion = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY d.NUM_DEVOLUCION ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Devolucion entidad = new Devolucion();
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt32(resultado["NUM_DEVOLUCION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_DEVOLUCION"] != DBNull.Value) entidad.fecha_devolucion = Convert.ToDateTime(resultado["FECHA_DEVOLUCION"]);
                            if (resultado["FECHA_DESCUENTO"] != DBNull.Value) entidad.fecha_descuento = Convert.ToDateTime(resultado["FECHA_DESCUENTO"]);
                            if (resultado["NUM_RECAUDO"] != DBNull.Value) entidad.num_recaudo = Convert.ToInt64(resultado["NUM_RECAUDO"]);
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ORIGEN"] != DBNull.Value) entidad.origen = Convert.ToString(resultado["ORIGEN"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["OFICINA"]);

                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ListarDevolucion", ex);
                        return null;
                    }
                }
            }
        }


        public Devolucion ConsultarDevolucion(int pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Devolucion entidad = new Devolucion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select devolucion.* ,persona.primer_nombre||' '||persona.segundo_nombre||' '|| persona.primer_apellido||' '||persona.segundo_apellido as Nombre,persona.identificacion as IDENTIFI "
                            + "from devolucion inner join persona on persona.cod_persona = devolucion.cod_persona "
                            + "where NUM_DEVOLUCION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt32(resultado["NUM_DEVOLUCION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFI"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFI"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["FECHA_DESCUENTO"] != DBNull.Value) entidad.fecha_descuento = Convert.ToDateTime(resultado["FECHA_DESCUENTO"]);
                            if (resultado["FECHA_DEVOLUCION"] != DBNull.Value) entidad.fecha_devolucion = Convert.ToDateTime(resultado["FECHA_DEVOLUCION"]);
                            if (resultado["NUM_RECAUDO"] != DBNull.Value) entidad.num_recaudo = Convert.ToInt64(resultado["NUM_RECAUDO"]);
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ORIGEN"] != DBNull.Value) entidad.origen = Convert.ToString(resultado["ORIGEN"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ConsultarDevolucion", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(NUM_DEVOLUCION) + 1 FROM devolucion ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public Devolucion ConsultarDetalleRecaudo(int pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Devolucion entidad = new Devolucion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select numero_recaudo,tipo_producto||' - '||numero_producto||' - '||valor as Detallereca "
                                       + "from detrecaudo_masivo where iddetalle = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["numero_recaudo"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt32(resultado["numero_recaudo"]);
                            if (resultado["detallereca"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["detallereca"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ConsultarDevolucion", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarDevolucion(Devolucion pDevolucion, Int64 pCod_Ope, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pCod_Ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pDevolucion.num_devolucion;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter p_valor_aplica = cmdTransaccionFactory.CreateParameter();
                        p_valor_aplica.ParameterName = "p_valor_aplica";
                        p_valor_aplica.Value = pDevolucion.valor_a_aplicar;
                        p_valor_aplica.Direction = ParameterDirection.Input;
                        p_valor_aplica.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_aplica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_DEVOLUCION_APLI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "AplicarDevolucion", ex);
                    }
                }
            }
        }


        public Devolucion CrearTransaccionDevolucion(Devolucion pTraslado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pTraslado.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pTraslado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_devolucion = cmdTransaccionFactory.CreateParameter();
                        pnum_devolucion.ParameterName = "p_num_devolucion";
                        pnum_devolucion.Value = pTraslado.num_devolucion;
                        pnum_devolucion.Direction = ParameterDirection.Input;
                        pnum_devolucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_devolucion);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pTraslado.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTraslado.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTraslado.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TRASLA_DEV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTraslado.numero_transaccion = Convert.ToInt64(pnumero_transaccion.Value);
                        return pTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "CrearTransaccionDevolucion", ex);
                        return null;
                    }
                }
            }
        }



        public List<Devolucion> ConsultarDevolucionDetalle(int pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Devolucion> LstDevol = new List<Devolucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select TD.NUM_DEVOLUCION, O.NUM_COMP, O.TIPO_COMP, O.FECHA_OPER, TD.VALOR
                                        from TRAN_DEVOLUCION TD
                                        inner join OPERACION O on TD.COD_OPE = O.COD_OPE
                                        where NUM_DEVOLUCION = " + pId.ToString();
                        sql += @"union
                                 select D.NUM_DEVOLUCION, O.NUM_COMP, O.TIPO_COMP, O.FECHA_OPER, D.VALOR
                                 from DEVOLUCION D
                                 inner join OPERACION O on D.ORIGEN =  TO_CHAR(O.COD_OPE)
                                 where D.NUM_DEVOLUCION = " + pId.ToString() +
                                 @" AND D.ORIGEN NOT IN (SELECT TO_CHAR(T.COD_OPE) FROM TRAN_DEVOLUCION T WHERE T.NUM_DEVOLUCION = D.NUM_DEVOLUCION)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Devolucion entidad = new Devolucion();
                            if (resultado["NUM_DEVOLUCION"] != DBNull.Value) entidad.num_devolucion = Convert.ToInt64(resultado["NUM_DEVOLUCION"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            LstDevol.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDevol;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DevolucionData", "ConsultarDevolucionDetalle", ex);
                        return null;
                    }
                }
            }
        }
    }
}




