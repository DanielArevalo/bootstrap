using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla LineaCDATS
    /// </summary>
    public class LineaCDATData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineaCDATS
        /// </summary>
        public LineaCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public LineaCDAT CrearLineaCDAT(LineaCDAT pLineaCDAT, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        pcod_lineacdat.Value = pLineaCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pLineaCDAT.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pLineaCDAT.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcalculo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa.ParameterName = "p_calculo_tasa";
                        pcalculo_tasa.Value = pLineaCDAT.calculo_tasa;
                        pcalculo_tasa.Direction = ParameterDirection.Input;
                        pcalculo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineaCDAT.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineaCDAT.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaCDAT.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineaCDAT.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaCDAT.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaCDAT.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineaCDAT.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineaCDAT.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (pLineaCDAT.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pLineaCDAT.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pLineaCDAT.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);


                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineaCDAT.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineaCDAT.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        if (pLineaCDAT.retencion == 0)
                            pretencion.Value = 0;
                        else
                            pretencion.Value = pLineaCDAT.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        DbParameter ptipocalendario = cmdTransaccionFactory.CreateParameter();
                        ptipocalendario.ParameterName = "ptipocalendario";
                        ptipocalendario.Value = pLineaCDAT.tipo_calendario;
                        ptipocalendario.Direction = ParameterDirection.Input;
                        ptipocalendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipocalendario);

                        DbParameter p_interes_anticipado = cmdTransaccionFactory.CreateParameter();
                        p_interes_anticipado.ParameterName = "p_interes_anticipado";
                        p_interes_anticipado.Value = pLineaCDAT.interes_anticipado;
                        p_interes_anticipado.Direction = ParameterDirection.Input;
                        p_interes_anticipado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_interes_anticipado);


                        DbParameter pcalculo_tasa_ven = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa_ven.ParameterName = "p_calculo_tasa_ven";
                        pcalculo_tasa_ven.Value = pLineaCDAT.calculo_tasa_ven;
                        pcalculo_tasa_ven.Direction = ParameterDirection.Input;
                        pcalculo_tasa_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa_ven);

                        DbParameter pcod_tipo_tasa_ven = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa_ven.ParameterName = "p_cod_tipo_tasa_ven";
                        if (pLineaCDAT.cod_tipo_tasa_ven == null)
                            pcod_tipo_tasa_ven.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa_ven.Value = pLineaCDAT.cod_tipo_tasa_ven;
                        pcod_tipo_tasa_ven.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa_ven);

                        DbParameter ptasa_ven = cmdTransaccionFactory.CreateParameter();
                        ptasa_ven.ParameterName = "p_tasa_ven";
                        if (pLineaCDAT.tasa_ven == null)
                            ptasa_ven.Value = DBNull.Value;
                        else
                            ptasa_ven.Value = pLineaCDAT.tasa_ven;
                        ptasa_ven.Direction = ParameterDirection.Input;
                        ptasa_ven.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_ven);

                        DbParameter ptipo_historico_ven = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico_ven.ParameterName = "p_tipo_historico_ven";
                        if (pLineaCDAT.tipo_historico_ven == null)
                            ptipo_historico_ven.Value = DBNull.Value;
                        else
                            ptipo_historico_ven.Value = pLineaCDAT.tipo_historico_ven;
                        ptipo_historico_ven.Direction = ParameterDirection.Input;
                        ptipo_historico_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico_ven);

                        DbParameter pdesviacion_ven = cmdTransaccionFactory.CreateParameter();
                        pdesviacion_ven.ParameterName = "p_desviacion_ven";
                        if (pLineaCDAT.desviacion_ven == null)
                            pdesviacion_ven.Value = DBNull.Value;
                        else
                            pdesviacion_ven.Value = pLineaCDAT.desviacion_ven;
                        pdesviacion_ven.Direction = ParameterDirection.Input;
                        pdesviacion_ven.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion_ven);


                        DbParameter p_capitaliza_interes = cmdTransaccionFactory.CreateParameter();
                        p_capitaliza_interes.ParameterName = "p_capitaliza_interes";
                        p_capitaliza_interes.Value = pLineaCDAT.capitaliza_interes;
                        p_capitaliza_interes.Direction = ParameterDirection.Input;
                        p_capitaliza_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_capitaliza_interes);




                        DbParameter p_numeropreimpreso = cmdTransaccionFactory.CreateParameter();
                        p_numeropreimpreso.ParameterName = "p_numero_preimpreso";
                        p_numeropreimpreso.Value = pLineaCDAT.numero_pre_impreso;
                        p_numeropreimpreso.Direction = ParameterDirection.Input;
                        p_numeropreimpreso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numeropreimpreso);

                        DbParameter ptasa_simulacion = cmdTransaccionFactory.CreateParameter();
                        ptasa_simulacion.ParameterName = "p_tasa_simulacion";
                        if (pLineaCDAT.tasa_simulacion == null)
                            ptasa_simulacion.Value = System.DBNull.Value;
                        else
                            ptasa_simulacion.Value = pLineaCDAT.tasa_simulacion;
                        ptasa_simulacion.Direction = ParameterDirection.Input;
                        ptasa_simulacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptasa_simulacion);




                        DbParameter p_interes_prroroga = cmdTransaccionFactory.CreateParameter();
                        p_interes_prroroga.ParameterName = "p_interes_prroroga";
                        if (pLineaCDAT.interes_prroroga == null)
                            p_interes_prroroga.Value = System.DBNull.Value;
                        else
                            p_interes_prroroga.Value = pLineaCDAT.interes_prroroga;
                        p_interes_prroroga.Direction = ParameterDirection.Input;
                        p_interes_prroroga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_interes_prroroga);


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LINEACDAT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCDATData", "CrearLineaCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public RangoCDAT ConsultarRangoCDATPorLineaYTipoTope(RangoCDAT pLineaCDAT, Usuario pUsuario)
        {
            DbDataReader resultado;
            RangoCDAT entidad = new RangoCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * 
                                        from RANGO_CDAT 
                                        where tipo_tope = " + pLineaCDAT.tipo_tope +
                                        " and cod_lineacdat = " + pLineaCDAT.cod_lineacdat;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["COD_RANGO"] != DBNull.Value) entidad.cod_rango = Convert.ToInt64(resultado["COD_RANGO"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt32(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["MAXIMO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCDATData", "ConsultarRangoCDATPorLineaYTipoTope", ex);
                        return null;
                    }
                }
            }
        }


        public LineaCDAT ModificarLineaCDAT(LineaCDAT pLineaCDAT, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        pcod_lineacdat.Value = pLineaCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pLineaCDAT.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pLineaCDAT.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcalculo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa.ParameterName = "p_calculo_tasa";
                        pcalculo_tasa.Value = pLineaCDAT.calculo_tasa;
                        pcalculo_tasa.Direction = ParameterDirection.Input;
                        pcalculo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pLineaCDAT.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pLineaCDAT.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pLineaCDAT.tasa == null)
                            ptasa.Value = DBNull.Value;
                        else
                            ptasa.Value = pLineaCDAT.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pLineaCDAT.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pLineaCDAT.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pLineaCDAT.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pLineaCDAT.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (pLineaCDAT.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pLineaCDAT.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);


                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pLineaCDAT.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);


                        DbParameter pinteres_por_cuenta = cmdTransaccionFactory.CreateParameter();
                        pinteres_por_cuenta.ParameterName = "p_interes_por_cuenta";
                        if (pLineaCDAT.interes_por_cuenta == null)
                            pinteres_por_cuenta.Value = System.DBNull.Value;
                        else
                            pinteres_por_cuenta.Value = pLineaCDAT.interes_por_cuenta;
                        pinteres_por_cuenta.Direction = ParameterDirection.Input;
                        pinteres_por_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pinteres_por_cuenta);

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        if (pLineaCDAT.retencion == 0)
                            pretencion.Value = 0;
                        else
                            pretencion.Value = pLineaCDAT.retencion;
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pretencion);


                        DbParameter p_interes_anticipado = cmdTransaccionFactory.CreateParameter();
                        p_interes_anticipado.ParameterName = "p_interes_anticipado";
                        p_interes_anticipado.Value = pLineaCDAT.interes_anticipado;
                        p_interes_anticipado.Direction = ParameterDirection.Input;
                        p_interes_anticipado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_interes_anticipado);




                        DbParameter ptipocalendario = cmdTransaccionFactory.CreateParameter();
                        ptipocalendario.ParameterName = "ptipocalendario";
                        ptipocalendario.Value = pLineaCDAT.tipo_calendario;
                        ptipocalendario.Direction = ParameterDirection.Input;
                        ptipocalendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipocalendario);

                        DbParameter pcalculo_tasa_ven = cmdTransaccionFactory.CreateParameter();
                        pcalculo_tasa_ven.ParameterName = "p_calculo_tasa_ven";
                        pcalculo_tasa_ven.Value = pLineaCDAT.calculo_tasa_ven;
                        pcalculo_tasa_ven.Direction = ParameterDirection.Input;
                        pcalculo_tasa_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_tasa_ven);

                        DbParameter pcod_tipo_tasa_ven = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa_ven.ParameterName = "p_cod_tipo_tasa_ven";
                        if (pLineaCDAT.cod_tipo_tasa_ven == null)
                            pcod_tipo_tasa_ven.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa_ven.Value = pLineaCDAT.cod_tipo_tasa_ven;
                        pcod_tipo_tasa_ven.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa_ven);

                        DbParameter ptasa_ven = cmdTransaccionFactory.CreateParameter();
                        ptasa_ven.ParameterName = "p_tasa_ven";
                        if (pLineaCDAT.tasa_ven == null)
                            ptasa_ven.Value = DBNull.Value;
                        else
                            ptasa_ven.Value = pLineaCDAT.tasa_ven;
                        ptasa_ven.Direction = ParameterDirection.Input;
                        ptasa_ven.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_ven);

                        DbParameter ptipo_historico_ven = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico_ven.ParameterName = "p_tipo_historico_ven";
                        if (pLineaCDAT.tipo_historico_ven == null)
                            ptipo_historico_ven.Value = DBNull.Value;
                        else
                            ptipo_historico_ven.Value = pLineaCDAT.tipo_historico_ven;
                        ptipo_historico_ven.Direction = ParameterDirection.Input;
                        ptipo_historico_ven.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico_ven);

                        DbParameter pdesviacion_ven = cmdTransaccionFactory.CreateParameter();
                        pdesviacion_ven.ParameterName = "p_desviacion_ven";
                        if (pLineaCDAT.desviacion_ven == null)
                            pdesviacion_ven.Value = DBNull.Value;
                        else
                            pdesviacion_ven.Value = pLineaCDAT.desviacion_ven;
                        pdesviacion_ven.Direction = ParameterDirection.Input;
                        pdesviacion_ven.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion_ven);

                        DbParameter p_capitaliza_interes = cmdTransaccionFactory.CreateParameter();
                        p_capitaliza_interes.ParameterName = "p_capitaliza_interes";
                        p_capitaliza_interes.Value = pLineaCDAT.capitaliza_interes;
                        p_capitaliza_interes.Direction = ParameterDirection.Input;
                        p_capitaliza_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_capitaliza_interes);


                        DbParameter p_numeropreimpreso = cmdTransaccionFactory.CreateParameter();
                        p_numeropreimpreso.ParameterName = "p_numero_preimpreso";
                        p_numeropreimpreso.Value = pLineaCDAT.numero_pre_impreso;
                        p_numeropreimpreso.Direction = ParameterDirection.Input;
                        p_numeropreimpreso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numeropreimpreso);


                        DbParameter ptasa_simulacion = cmdTransaccionFactory.CreateParameter();
                        ptasa_simulacion.ParameterName = "p_tasa_simulacion";
                        if (pLineaCDAT.tasa_simulacion == null)
                            ptasa_simulacion.Value = System.DBNull.Value;
                        else
                            ptasa_simulacion.Value = pLineaCDAT.tasa_simulacion;
                        ptasa_simulacion.Direction = ParameterDirection.Input;
                        ptasa_simulacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptasa_simulacion);



                        DbParameter p_interes_prroroga = cmdTransaccionFactory.CreateParameter();
                        p_interes_prroroga.ParameterName = "p_interes_prroroga";
                        if (pLineaCDAT.interes_prroroga == null)
                            p_interes_prroroga.Value = System.DBNull.Value;
                        else
                            p_interes_prroroga.Value = pLineaCDAT.interes_prroroga;
                        p_interes_prroroga.Direction = ParameterDirection.Input;
                        p_interes_prroroga.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_interes_prroroga);




                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LINEACDAT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCDATData", "CrearLineaCDAT", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarLineaCDAT(string pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaCDAT pLineaCDAT = new LineaCDAT();
                        pLineaCDAT = ConsultarLineaCDAT(pId, pUsuario);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        pcod_lineacdat.Value = pLineaCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LINEACDAT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCDATData", "EliminarLineaCDAT", ex);
                    }
                }
            }
        }
        public LineaCDAT ConsultarLineaCDAT(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaCDAT entidad = new LineaCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LineaCDAT WHERE COD_LINEACDAT = " + pId.ToString();
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["CALENDARIO"]);
                            if (resultado["INTERES_ANTICIPADO"] != DBNull.Value) entidad.interes_anticipado = Convert.ToInt32(resultado["INTERES_ANTICIPADO"]);
                            if (resultado["CALCULO_TASA_VEN"] != DBNull.Value) entidad.calculo_tasa_ven = Convert.ToInt32(resultado["CALCULO_TASA_VEN"]);
                            if (resultado["COD_TIPO_TASA_VEN"] != DBNull.Value) entidad.cod_tipo_tasa_ven = Convert.ToInt32(resultado["COD_TIPO_TASA_VEN"]);
                            if (resultado["TASA_VEN"] != DBNull.Value) entidad.tasa_ven = Convert.ToDecimal(resultado["TASA_VEN"]);
                            if (resultado["TIPO_HISTORICO_VEN"] != DBNull.Value) entidad.tipo_historico_ven = Convert.ToInt32(resultado["TIPO_HISTORICO_VEN"]);
                            if (resultado["DESVIACION_VEN"] != DBNull.Value) entidad.desviacion_ven = Convert.ToDecimal(resultado["DESVIACION_VEN"]);
                            if (resultado["CAPITALIZA_INTERES"] != DBNull.Value) entidad.capitaliza_interes = Convert.ToInt32(resultado["CAPITALIZA_INTERES"]);
                            if (resultado["NUMERO_PRE_IMPRESO"] != DBNull.Value) entidad.numero_pre_impreso = Convert.ToInt32(resultado["NUMERO_PRE_IMPRESO"]);
                            if (resultado["TASA_SIMULACION"] != DBNull.Value) entidad.tasa_simulacion = Convert.ToInt32(resultado["TASA_SIMULACION"]);

                            if (resultado["INTERES_PRORROGA"] != DBNull.Value) entidad.interes_prroroga = Convert.ToInt32(resultado["INTERES_PRORROGA"]);


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
                        BOExcepcion.Throw("LineaCDATData", "ConsultarLineaCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineaCDAT> ListarLineaCDAT(LineaCDAT pLineaCDAT, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineaCDAT> lstLineaCDAT = new List<LineaCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT LineaCDAT.*, TipoTasa.nombre As nom_tipo_tasa, TipoMoneda.descripcion As nom_moneda, rango_cdat.minimo, rango_cdat.maximo FROM LineaCDAT LEFT JOIN TipoTasa ON LineaCDAT.cod_tipo_tasa = TipoTasa.cod_tipo_tasa LEFT JOIN TipoMoneda ON LineaCDAT.cod_moneda = TipoMoneda.cod_moneda left join rango_cdat ON rango_cdat.cod_lineacdat = LineaCDAT.cod_lineacdat and rango_cdat.tipo_tope = 2 " + ObtenerFiltro(pLineaCDAT, "LineaCDAT.") + " ORDER BY LineaCDAT.COD_LINEACDAT";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaCDAT entidad = new LineaCDAT();
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (entidad.calculo_tasa == 1) entidad.nom_calculo_tasa = "Tasa Fija";
                            if (entidad.calculo_tasa == 2) entidad.nom_calculo_tasa = "Histórico Fijo";
                            if (entidad.calculo_tasa == 3) entidad.nom_calculo_tasa = "Histórico Variable";
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["NOM_TIPO_TASA"] != DBNull.Value) entidad.nom_tipo_tasa = Convert.ToString(resultado["NOM_TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            if (resultado["INTERES_POR_CUENTA"] != DBNull.Value) entidad.interes_por_cuenta = Convert.ToInt32(resultado["INTERES_POR_CUENTA"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["INTERES_ANTICIPADO"] != DBNull.Value) entidad.interes_anticipado = Convert.ToInt32(resultado["INTERES_ANTICIPADO"]);
                            if (resultado["NUMERO_PRE_IMPRESO"] != DBNull.Value) entidad.numero_pre_impreso = Convert.ToInt32(resultado["NUMERO_PRE_IMPRESO"]);

                            if (resultado["TASA_SIMULACION"] != DBNull.Value) entidad.tasa_simulacion = Convert.ToInt32(resultado["TASA_SIMULACION"]);

                            if (resultado["MINIMO"] != DBNull.Value) entidad.plazo_minimo = Convert.ToInt32(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.plazo_maximo = Convert.ToInt32(resultado["MAXIMO"]);
                            if (resultado["INTERES_PRORROGA"] != DBNull.Value) entidad.interes_prroroga = Convert.ToInt32(resultado["INTERES_PRORROGA"]);


                            lstLineaCDAT.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCDATData", "ListarLineaCDAT", ex);
                        return null;
                    }
                }
            }
        }


    }
}
