using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.Cartera.Data
{
    public class CambioLineaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public CambioLineaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para listar los créditos 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select V_CREDITOS.*,  Calcular_TotalAPagar(V_CREDITOS.numero_radicacion, SYSDATE)as VALOR  FROM V_CREDITOS INNER JOIN LINEASCREDITO li ON li.cod_linea_credito=v_creditos.cod_linea_credito where li.maneja_auxilio=1  " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);                    
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioLineaData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene la informacion de los creditos 
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del credito solicitado</returns>
        public Xpinn.FabricaCreditos.Entities.Credito ConsultarCredito(Xpinn.FabricaCreditos.Entities.Credito pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Se comentarea el select inicial donde "c.estado = 'V'". El estado cambia de "S" a "V" cuando el credito pasa por "referenciacion"                        
                        string sql = @"Select V_CREDITOS.*,  Calcular_TotalAPagar(V_CREDITOS.numero_radicacion, SYSDATE)as VALOR  FROM V_CREDITOS INNER JOIN LINEASCREDITO li ON li.cod_linea_credito=v_creditos.cod_linea_credito where li.maneja_auxilio=1 and numero_radicacion= " + pEntidad.numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago_string = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioLineaData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }
        public void LiquidarCredito(Int64 pnum_radic, ref string error, Usuario pusuario)
        {
            error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pnum_radic;
                        pnumero_radicacion.Direction = ParameterDirection.InputOutput;
                        pnumero_radicacion.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECALCULARCRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                }
            }

        }
        public Int64 InsertSolicitudCredito(Usuario pUsuario, Xpinn.FabricaCreditos.Entities.Credito  creditoEduca)
        {
            Int64 Numero_Radicacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        PFECHA_SOLICITUD.ParameterName = "PFECHA_SOLICITUD";
                        if (creditoEduca.fecha_solicitud != null)
                            PFECHA_SOLICITUD.Value = creditoEduca.fecha_solicitud;
                        else
                            PFECHA_SOLICITUD.Value = DateTime.Now;
                        PFECHA_SOLICITUD.Direction = ParameterDirection.Input;
                        PFECHA_SOLICITUD.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA_SOLICITUD);

                        DbParameter PCOD_DEUDOR = cmdTransaccionFactory.CreateParameter();
                        PCOD_DEUDOR.ParameterName = "PCOD_DEUDOR";
                        PCOD_DEUDOR.Value = creditoEduca.cod_deudor;
                        PCOD_DEUDOR.Direction = ParameterDirection.Input;
                        PCOD_DEUDOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_DEUDOR);

                        DbParameter PVALOR_MATRICULA = cmdTransaccionFactory.CreateParameter();
                        PVALOR_MATRICULA.ParameterName = "PVALOR_MATRICULA";
                        PVALOR_MATRICULA.Value = 0;
                        PVALOR_MATRICULA.Direction = ParameterDirection.Input;
                        PVALOR_MATRICULA.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PVALOR_MATRICULA);

                        DbParameter PMONTO_SOLICITADO = cmdTransaccionFactory.CreateParameter();
                        PMONTO_SOLICITADO.ParameterName = "PMONTO_SOLICITADO";
                        PMONTO_SOLICITADO.Value = creditoEduca.monto;
                        PMONTO_SOLICITADO.Direction = ParameterDirection.Input;
                        PMONTO_SOLICITADO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PMONTO_SOLICITADO);

                        DbParameter PNUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUOTAS.ParameterName = "PNUMERO_CUOTAS";
                        PNUMERO_CUOTAS.Value = creditoEduca.plazo;
                        PNUMERO_CUOTAS.Direction = ParameterDirection.Input;
                        PNUMERO_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUOTAS);

                        DbParameter PCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_CREDITO.ParameterName = "PCOD_LINEA_CREDITO";
                        PCOD_LINEA_CREDITO.Value = creditoEduca.cod_linea_credito;
                        PCOD_LINEA_CREDITO.Direction = ParameterDirection.Input;
                        PCOD_LINEA_CREDITO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_CREDITO);

                        DbParameter PCOD_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        PCOD_PERIODICIDAD.ParameterName = "PCOD_PERIODICIDAD";
                        PCOD_PERIODICIDAD.Value = creditoEduca.periodicidad;
                        PCOD_PERIODICIDAD.Direction = ParameterDirection.Input;
                        PCOD_PERIODICIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_PERIODICIDAD);

                        DbParameter PCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        PCOD_OFICINA.ParameterName = "PCOD_OFICINA";
                        PCOD_OFICINA.Value = creditoEduca.codigo_oficina;
                        PCOD_OFICINA.Direction = ParameterDirection.Input;
                        PCOD_OFICINA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_OFICINA);

                        DbParameter PFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_PAGO.ParameterName = "PFORMA_PAGO";
                        PFORMA_PAGO.Value = creditoEduca.forma_pago;
                        PFORMA_PAGO.Direction = ParameterDirection.Input;
                        PFORMA_PAGO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PFORMA_PAGO);

                        DbParameter PFECHA_PRIMERPAGO = cmdTransaccionFactory.CreateParameter();
                        PFECHA_PRIMERPAGO.ParameterName = "PFECHA_PRIMERPAGO";
                        if (creditoEduca.fecha_prim_pago != DateTime.MinValue) PFECHA_PRIMERPAGO.Value = creditoEduca.fecha_prim_pago; else PFECHA_PRIMERPAGO.Value = DBNull.Value;
                        PFECHA_PRIMERPAGO.Direction = ParameterDirection.Input;
                        PFECHA_PRIMERPAGO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA_PRIMERPAGO);

                        DbParameter PCOD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ASESOR.ParameterName = "PCOD_ASESOR";
                        PCOD_ASESOR.Value = 1;
                        PCOD_ASESOR.Direction = ParameterDirection.Input;
                        PCOD_ASESOR.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PCOD_ASESOR);

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = creditoEduca.cod_usuario;
                        PUSUARIO.Direction = ParameterDirection.Input;
                        PUSUARIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                        DbParameter PCOD_EMPRESA = cmdTransaccionFactory.CreateParameter();
                        PCOD_EMPRESA.ParameterName = "PCOD_EMPRESA";
                        if (creditoEduca.empresa != "") 
                            PCOD_EMPRESA.Value = creditoEduca.empresa;
                        else PCOD_EMPRESA.Value = DBNull.Value;
                        PCOD_EMPRESA.Direction = ParameterDirection.Input;
                        PCOD_EMPRESA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCOD_EMPRESA);

                        DbParameter PUNIVERSIDAD = cmdTransaccionFactory.CreateParameter();
                        PUNIVERSIDAD.ParameterName = "PUNIVERSIDAD";
                        PUNIVERSIDAD.Value = DBNull.Value;
                        PUNIVERSIDAD.Direction = ParameterDirection.Input;
                        PUNIVERSIDAD.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PUNIVERSIDAD);

                        DbParameter PSEMESTRE = cmdTransaccionFactory.CreateParameter();
                        PSEMESTRE.ParameterName = "PSEMESTRE";
                        PSEMESTRE.Value = DBNull.Value;                     
                        PSEMESTRE.Direction = ParameterDirection.Input;
                        PSEMESTRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PSEMESTRE);

                        DbParameter PNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_RADICACION.ParameterName = "PNUMERO_RADICACION";
                        PNUMERO_RADICACION.Value = Numero_Radicacion;
                        PNUMERO_RADICACION.Direction = ParameterDirection.InputOutput;
                        PNUMERO_RADICACION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_EDUCATIVO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (PNUMERO_RADICACION.Value != null)
                            Numero_Radicacion = Convert.ToInt64(PNUMERO_RADICACION.Value);

                        return Numero_Radicacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "InsertSolicitudCredito", ex);
                        return -1;
                    }
                }
            }
        }
        public Int64 DesembolsarCredito(Xpinn.FabricaCreditos.Entities.Credito pCredito, ref decimal pMontoDesembolso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pCredito.numero_credito;

                        DbParameter p_fecha_prim_pago = cmdTransaccionFactory.CreateParameter();
                        p_fecha_prim_pago.ParameterName = "p_fecha_prim_pago";
                        if (pCredito.fecha_prim_pago == null)
                            p_fecha_prim_pago.Value = DBNull.Value;
                        else
                            p_fecha_prim_pago.Value = pCredito.fecha_prim_pago;
                        p_fecha_prim_pago.DbType = DbType.Date;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pCredito.estado;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 1;

                        DbParameter p_fecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desembolso.ParameterName = "p_fecha_desembolso";
                        p_fecha_desembolso.Value = pCredito.fecha_desembolso;

                        DbParameter p_Usuario = cmdTransaccionFactory.CreateParameter();
                        p_Usuario.ParameterName = "p_Usuario";
                        p_Usuario.Value = pUsuario.nombre;
                        p_estado.DbType = DbType.AnsiString;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.Size = 50;

                        DbParameter p_Cod_Ope = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Ope.ParameterName = "p_cod_ope";
                        p_Cod_Ope.Value = pCredito.cod_ope;
                        p_Cod_Ope.Direction = ParameterDirection.Output;

                        DbParameter p_monto_desembolsado = cmdTransaccionFactory.CreateParameter();
                        p_monto_desembolsado.ParameterName = "p_monto_desembolsado";
                        p_monto_desembolsado.Value = pCredito.monto;
                        p_monto_desembolsado.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_prim_pago);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desembolso);
                        cmdTransaccionFactory.Parameters.Add(p_Usuario);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Ope);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_monto_desembolsado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_DESEM_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Cod_Ope.Value != null)
                            pCredito.cod_ope = Convert.ToInt64(p_Cod_Ope.Value);
                        else
                            pCredito.cod_ope = 0;

                        pMontoDesembolso = Convert.ToDecimal(p_monto_desembolsado.Value);
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.Creditos, "Desembolso de crédito");
                            //DAauditoria.InsertarLog(pCredito, "Credito", pUsuario, Accion.Modificar.ToString());

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCredito.cod_ope;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioLineaData", "DesembolsarCredito", ex);
                        return 0;
                    }
                }
            }
        }
        public List<Xpinn.FabricaCreditos.Entities.Documento> ListarDocumentosGarantia(Usuario pUsuario, Int64 credito)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Documento> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from DOCUMENTOSGARANTIA WHERE NUMERO_RADICACION=  " + credito;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Documento entidad = new Xpinn.FabricaCreditos.Entities.Documento();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                          
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioLineaData", "ListarDocumentosGarantia", ex);
                        return null;
                    }
                }
            }
        }
        /// Crea un registro en la tabla DocumentosAnexos de la base de datos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public Xpinn.FabricaCreditos.Entities.Credito CrearDocumentosGarantia(Xpinn.FabricaCreditos.Entities.Credito pDocumentosAnexos, Usuario pUsuario)
        {
            try
            {
                ImagenesORAData DAImagenes = new ImagenesORAData();
                return DAImagenes.CrearDocumentosGarantia(pDocumentosAnexos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CambioLineaData", "CrearDocumentosGarantia", ex);
                return null;
            }
        }
    }
}
