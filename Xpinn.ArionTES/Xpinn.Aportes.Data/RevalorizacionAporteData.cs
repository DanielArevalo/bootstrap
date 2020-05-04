using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using System.Reflection;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Aportes.Data

{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoLiqAporteS
    /// </summary>
    public class RevalorizacionAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        Configuracion global = new Configuracion();
        string FormatoFecha = " ";

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoLiqAporteS
        /// </summary>
        public RevalorizacionAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public RevalorizacionAportes ConsultarFecUltCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            RevalorizacionAportes entidad = new RevalorizacionAportes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(fecha) as fecha  from cierea WHERE  tipo='J' AND estado='D'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]);
                        }


                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RevalorizacionAporteData", "ConsultarFecUltCierre", ex);
                        return null;
                    }

                }

            }
        }

        

        public List<RevalorizacionAportes> Listar(RevalorizacionAportes pEntidad, ref List<RevalorizacionAportes> lstNoCalculados, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<RevalorizacionAportes> lstRevalorizacion = new List<RevalorizacionAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    Int64 row = 0;
                    try
                    {
                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "pcod_linea_aporte";
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        pcod_linea_aporte.DbType = DbType.Int64;
                        pcod_linea_aporte.Value = pEntidad.lineaaporte;
                        

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "ptipo_distribucion";
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int64;
                        ptipo_distribucion.Value = pEntidad.tipodistrib;
                 

                        DbParameter pValor = cmdTransaccionFactory.CreateParameter();
                        pValor.ParameterName = "pValor";
                        pValor.Direction = ParameterDirection.Input;
                        pValor.DbType = DbType.Decimal;
                        pValor.Value = pEntidad.valordist;
                     

                        DbParameter pPorcentaje = cmdTransaccionFactory.CreateParameter();
                        pPorcentaje.ParameterName = "pPorcentaje";
                        pPorcentaje.Direction = ParameterDirection.Input;
                        pPorcentaje.DbType = DbType.Decimal;
                        pPorcentaje.Value = pEntidad.pordist;



                        DbParameter pIncluirRetirados = cmdTransaccionFactory.CreateParameter();
                        pIncluirRetirados.ParameterName = "pIncluirRetirados";
                        pIncluirRetirados.Direction = ParameterDirection.Input;
                        pIncluirRetirados.DbType = DbType.Int64;
                        pIncluirRetirados.Value = pEntidad.asretirados;
                      

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;                        
                        pfecha.DbType = DbType.Date;
                        pfecha.Value = pEntidad.fecha;
                        pfecha.Value = pEntidad.fecha.ToString(FormatoFecha);



                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pValor);
                        cmdTransaccionFactory.Parameters.Add(pPorcentaje);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pIncluirRetirados);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_REVALORIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_REVALORIZAR";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = default(DbDataReader);
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        RevalorizacionAportes entidad;
                        while (resultado.Read())
                        {
                            entidad = new RevalorizacionAportes();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"].ToString());
                            if (resultado["codigo"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["codigo"].ToString());
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"].ToString());
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["Nombre"].ToString());
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"].ToString());
                            if (resultado["numero_aporte"] != DBNull.Value) entidad.num_aporte = Convert.ToInt64(resultado["numero_aporte"].ToString());
                            if (resultado["saldo_base"] != DBNull.Value) entidad.saldo_base = Convert.ToDecimal(resultado["saldo_base"].ToString());
                            if (resultado["valor_a_distribuir"] != DBNull.Value) entidad.valordist = Convert.ToDecimal(resultado["valor_a_distribuir"].ToString());
                            if (resultado["retencion"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["retencion"].ToString());

                            lstRevalorizacion.Add(entidad);
                        }

                        //Capturando aportes no calculados
                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        sql = "Select * From TEMP_NOREVALORIZADOS";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = default(DbDataReader);
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int indexfecha = resultado.GetOrdinal("fecha");
                        int indexcodigo = resultado.GetOrdinal("codigo");
                        int indexidentificacion = resultado.GetOrdinal("identificacion");
                        int indexNombre = resultado.GetOrdinal("Nombre");
                        int indexEstado = resultado.GetOrdinal("Estado");
                        int indexnumero_aporte = resultado.GetOrdinal("numero_aporte");
                        int indexsaldo_base = resultado.GetOrdinal("saldo_base");
                        int indexvalor_a_distribuir = resultado.GetOrdinal("valor_a_distribuir");
                        
                        while (resultado.Read())
                        {
                            entidad = new RevalorizacionAportes();
                            if (resultado.GetValue(indexfecha) != DBNull.Value) entidad.fecha = resultado.GetDateTime(indexfecha);
                            if (resultado.GetValue(indexcodigo) != DBNull.Value) entidad.codigo = resultado.GetInt64(indexcodigo);
                            if (resultado.GetValue(indexidentificacion) != DBNull.Value) entidad.identificacion = resultado.GetString(indexidentificacion);
                            if (resultado.GetValue(indexNombre) != DBNull.Value) entidad.nombres = resultado.GetString(indexNombre);
                            if (resultado.GetValue(indexEstado) != DBNull.Value) entidad.estado = resultado.GetString(indexEstado);
                            if (resultado.GetValue(indexnumero_aporte) != DBNull.Value) entidad.num_aporte = resultado.GetInt64(indexnumero_aporte);
                            if (resultado.GetValue(indexsaldo_base) != DBNull.Value) entidad.saldo_base = resultado.GetDecimal(indexsaldo_base);
                            if (resultado.GetValue(indexvalor_a_distribuir) != DBNull.Value) entidad.valordist = resultado.GetDecimal(indexvalor_a_distribuir);
                            lstNoCalculados.Add(entidad);
                            row++;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRevalorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RevalorizacionAporteData", "Listar " + row.ToString(), ex);
                        return null;
                    }
                }
            }
        }

        public List<RevalorizacionAportes> ListarRevalorizacion(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<RevalorizacionAportes> lstRevalorizacion = new List<RevalorizacionAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from revalorizar ";
                            sql += pFiltro;
                            sql += " Order by IDREVALORIZAR ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        //CAPTURANDO INDICES
                        int indexId = resultado.GetOrdinal("IDREVALORIZAR");
                        int indexFecha = resultado.GetOrdinal("FECHA");
                        int indexCodPersona = resultado.GetOrdinal("COD_PERSONA");
                        int indexNumAporte = resultado.GetOrdinal("NUMERO_APORTE");
                        int indexSaldo = resultado.GetOrdinal("SALDO_BASE");
                        int indexVrDistri = resultado.GetOrdinal("VALOR_A_DISTRIBUIR");

                        RevalorizacionAportes entidad;
                        while (resultado.Read())
                        {
                            entidad = new RevalorizacionAportes();
                            if (resultado.GetValue(indexId) != DBNull.Value) entidad.id = resultado.GetInt64(indexId);
                            if (resultado.GetValue(indexFecha) != DBNull.Value) entidad.fecha = resultado.GetDateTime(indexFecha);
                            if (resultado.GetValue(indexCodPersona) != DBNull.Value) entidad.codigo = resultado.GetInt64(indexCodPersona);
                            if (resultado.GetValue(indexNumAporte) != DBNull.Value) entidad.num_aporte = resultado.GetInt64(indexNumAporte);
                            if (resultado.GetValue(indexSaldo) != DBNull.Value) entidad.saldo_base = resultado.GetDecimal(indexSaldo);
                            if (resultado.GetValue(indexVrDistri) != DBNull.Value) entidad.valordist = resultado.GetDecimal(indexVrDistri);

                            lstRevalorizacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        return lstRevalorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RevalorizacionAporteData", "ListarDatosComprobante", ex);
                        return null;
                    }
                }
            }
        }


        public List<RevalorizacionAportes> ListarDatosComprobante(String filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<RevalorizacionAportes> lstRevalorizacion = new List<RevalorizacionAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select a.*,b.fecha_oper,b.tipo_ope,b.cod_ofi from tran_aporte a inner join operacion b on a.cod_ope = b.cod_ope where b.tipo_ope = 19 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RevalorizacionAportes entidad = new RevalorizacionAportes();
                            if (resultado["numero_transaccion"] != DBNull.Value) entidad.id = Convert.ToInt64(resultado["numero_transaccion"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"].ToString());
                            if (resultado["cod_persona"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["cod_persona"].ToString());
                            if (resultado["numero_aporte"] != DBNull.Value) entidad.num_aporte = Convert.ToInt64(resultado["numero_aporte"].ToString());
                            if (resultado["valor"] != DBNull.Value) entidad.saldo_base = Convert.ToInt64(resultado["valor"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipooperacion = Convert.ToInt64(resultado["tipo_ope"].ToString());
                            if (resultado["cod_ofi"] != DBNull.Value) entidad.oficina = Convert.ToInt64(resultado["cod_ofi"].ToString());
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_oper"].ToString());

                            lstRevalorizacion.Add(entidad);
                        }
                        // dbConnectionFactory.CerrarConexion(connection);
                        return lstRevalorizacion;
                    }
                    catch (Exception ex)
                    { 
                        BOExcepcion.Throw("RevalorizacionAporteData", "ListarDatosComprobante", ex);
                        return null;
                    }
                }
            }
        }
        public RevalorizacionAportes GrabarRevalorizacion(RevalorizacionAportes pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        pcod_ope.Value = pEntidad.cod_ope;


                        DbParameter pCodigo = cmdTransaccionFactory.CreateParameter();
                        pCodigo.ParameterName = "pCodigo";
                        pCodigo.Direction = ParameterDirection.Input;
                        pCodigo.DbType = DbType.Int64;
                        pCodigo.Value = pEntidad.codigo;

                        DbParameter pIdentificacion = cmdTransaccionFactory.CreateParameter();
                        pIdentificacion.ParameterName = "pIdentificacion";
                        pIdentificacion.Direction = ParameterDirection.Input;
                        pIdentificacion.DbType = DbType.String;
                        pIdentificacion.Value = pEntidad.identificacion;


                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        pnombre.Value = pEntidad.nombres;


                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        pestado.Value = pEntidad.estado;



                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        pfecha.Value = pEntidad.fecharevalorizacion.ToString(FormatoFecha);
                       // pfecha.Value = pEntidad.fecha.ToString(FormatoFecha);


                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "pnumero_aporte";
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        pnumero_aporte.Value = pEntidad.num_aporte;


                        DbParameter psaldo_base = cmdTransaccionFactory.CreateParameter();
                        psaldo_base.ParameterName = "psaldo_base";
                        psaldo_base.Direction = ParameterDirection.Input;
                        psaldo_base.DbType = DbType.Decimal;
                        psaldo_base.Value = pEntidad.saldo_base;


                        DbParameter pvalor_a_distribuir = cmdTransaccionFactory.CreateParameter();
                        pvalor_a_distribuir.ParameterName = "pvalor_a_distribuir";
                        pvalor_a_distribuir.Direction = ParameterDirection.Input;
                        pvalor_a_distribuir.DbType = DbType.Decimal;
                        pvalor_a_distribuir.Value = pEntidad.valordist;

                        DbParameter pretencion = cmdTransaccionFactory.CreateParameter();
                        pretencion.ParameterName = "pretencion";
                        pretencion.Direction = ParameterDirection.Input;
                        pretencion.DbType = DbType.Decimal;
                        pretencion.Value = pEntidad.retencion;



                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pIdentificacion);
                       cmdTransaccionFactory.Parameters.Add(pCodigo);
                        cmdTransaccionFactory.Parameters.Add(pnombre);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);
                        cmdTransaccionFactory.Parameters.Add(psaldo_base);
                        cmdTransaccionFactory.Parameters.Add(pvalor_a_distribuir);
                        cmdTransaccionFactory.Parameters.Add(pretencion);

                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_REVALORIZAR_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                       // pEntidad. = Convert.ToInt64(pcod_rango_atr.Value);



                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RevalorizacionAporteData", "Grabarrevalorizacion", ex);
                    }
                    return pEntidad;
                }
            }
        }


        
    }
}