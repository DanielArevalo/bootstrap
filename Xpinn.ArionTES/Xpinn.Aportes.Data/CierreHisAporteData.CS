using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;



namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla cierre histórico
    /// </summary>
    public class CierreHisAporteData : GlobalData
    {      
         
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public CierreHisAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        
        public Aporte CierreHistorico(Aporte pentidad,string valor, DateTime fechas, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            serror = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                                                  

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = fechas;
                        PFECHA.DbType = DbType.Date;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = valor;
                        
                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = cod_usuario;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Usp_Xpinn_Apo_Cierrehistorico";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pentidad;


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHisAPorteData", "CrearCierremensual", ex);
                       
                        serror = ex.Message;
                        return null; 
                    }
                }
            }
        }


        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);           

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea" + ObtenerFiltro(pCierea) + " Order by fecha desc";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }


        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1911 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }


        public void CierreHistoricoPersonas(string valor, DateTime fechas, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            serror = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = fechas;
                        PFECHA.DbType = DbType.Date;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = valor;

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = cod_usuario;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Usp_Xpinn_Apo_CierrePersonas";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        serror = ex.Message;
                        return;
                    }
                }
            }
        }



        public List<CausacionPermanente> ListarCausacionPermanentes(DateTime pFechaIni, CausacionPermanente pAhorroPerm, ref string pError, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CausacionPermanente> lstProvision = new List<CausacionPermanente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        fecha_apertura.ParameterName = "PFECHA";
                        fecha_apertura.Direction = ParameterDirection.Input;
                        if (pFechaIni != DateTime.MinValue) fecha_apertura.Value = pFechaIni; else fecha_apertura.Value = DBNull.Value;
                        fecha_apertura.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_apertura);

                        DbParameter cod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        cod_linea_aporte.ParameterName = "PESTADO";
                        cod_linea_aporte.Direction = ParameterDirection.Input;
                        cod_linea_aporte.Value = pAhorroPerm.estado;
                        cod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_linea_aporte);

                        DbParameter usuario = cmdTransaccionFactory.CreateParameter();
                        usuario.ParameterName = "PUSUARIO";
                        usuario.Direction = ParameterDirection.Input;
                        usuario.Value = pAhorroPerm.usuario;
                        usuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(usuario);

                        DbParameter cod_oficina = cmdTransaccionFactory.CreateParameter();
                        cod_oficina.ParameterName = "PCOD_OFICINA";
                        cod_oficina.Direction = ParameterDirection.Input;
                        cod_oficina.Value = pAhorroPerm.cod_oficina;
                        cod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(cod_oficina);

                        DbParameter pmensaje_error = cmdTransaccionFactory.CreateParameter();
                        pmensaje_error.ParameterName = "P_MENSAJE_ERROR";
                        pmensaje_error.Value = DBNull.Value;
                        pmensaje_error.Size = 8000;
                        pmensaje_error.DbType = DbType.String;
                        pmensaje_error.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pmensaje_error);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CAUSACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_PROVISIONAPO";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader(CommandBehavior.SingleResult);

                        pError = Convert.ToString(pmensaje_error.Value);
                        if (!string.IsNullOrEmpty(pError))
                        {
                            return null;
                        }

                        int IndexNumero_cuenta = resultado.GetOrdinal("NUMERO_APORTE");
                        int IndexFecha_apertura = resultado.GetOrdinal("FECHA_APERTURA");
                        int IndexSaldo_total = resultado.GetOrdinal("SALDO_TOTAL");
                        int IndexSaldo_base = resultado.GetOrdinal("SALDO_BASE");
                        int IndexProvision_interes = resultado.GetOrdinal("PROVISION_INTERES");
                        int IndexRetencion = resultado.GetOrdinal("RETENCION");
                        int IndexIdentificacion = resultado.GetOrdinal("IDENTIFICACION");
                        int IndexCod_oficina = resultado.GetOrdinal("COD_OFICINA");
                        int IndexNombre = resultado.GetOrdinal("NOMBRE");
                        int IndexCod_linea_aporte = resultado.GetOrdinal("COD_LINEA_APORTE");
                        int IndexTasa = resultado.GetOrdinal("TASA");
                        int IndexDias = resultado.GetOrdinal("DIAS");
                        int IndexValor_acumulado = resultado.GetOrdinal("VALOR_ACUMULADO");

                        CausacionPermanente entidad;
                        while (resultado.Read())
                        {
                            entidad = new CausacionPermanente();
                            if (resultado.GetValue(IndexNumero_cuenta) != DBNull.Value) entidad.numero_aporte = resultado.GetInt64(IndexNumero_cuenta);
                            if (resultado.GetValue(IndexFecha_apertura) != DBNull.Value) entidad.fecha_causacion = resultado.GetDateTime(IndexFecha_apertura);
                            if (resultado.GetValue(IndexSaldo_total) != DBNull.Value) entidad.saldo_total = resultado.GetDecimal(IndexSaldo_total);
                            if (resultado.GetValue(IndexSaldo_base) != DBNull.Value) entidad.saldo_base = resultado.GetInt64(IndexSaldo_base);
                            if (resultado.GetValue(IndexProvision_interes) != DBNull.Value) entidad.int_causados = resultado.GetInt64(IndexProvision_interes);
                            if (resultado.GetValue(IndexRetencion) != DBNull.Value) entidad.retencion = resultado.GetDecimal(IndexRetencion);
                            if (resultado.GetValue(IndexIdentificacion) != DBNull.Value) entidad.identificacion = resultado.GetString(IndexIdentificacion);
                            if (resultado.GetValue(IndexCod_oficina) != DBNull.Value) entidad.cod_oficina = resultado.GetInt32(IndexCod_oficina);
                            if (resultado.GetValue(IndexNombre) != DBNull.Value) entidad.nombre = resultado.GetString(IndexNombre);
                            if (resultado.GetValue(IndexCod_linea_aporte) != DBNull.Value) entidad.cod_linea_aporte = resultado.GetInt32(IndexCod_linea_aporte);
                            if (resultado.GetValue(IndexTasa) != DBNull.Value) entidad.tasa = resultado.GetDecimal(IndexTasa);
                            if (resultado.GetValue(IndexDias) != DBNull.Value) entidad.dias_causados = resultado.GetInt32(IndexDias);
                            if (resultado.GetValue(IndexValor_acumulado) != DBNull.Value) entidad.valor_acumulado = resultado.GetDecimal(IndexValor_acumulado);
                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


    }
}