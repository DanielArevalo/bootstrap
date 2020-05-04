using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Entities;

namespace Xpinn.Auxilios.Data
{
   public class ReporteAuxilioData:GlobalData
    {
       
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteAuxilioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ReporteAuxilio CrearAuxilio(ReporteAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pAuxilio.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAuxilio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pAuxilio.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pvalor_solicitado = cmdTransaccionFactory.CreateParameter();
                        pvalor_solicitado.ParameterName = "p_valor_solicitado";
                        if (pAuxilio.valor_solicitado == null)
                            pvalor_solicitado.Value = DBNull.Value;
                        else
                            pvalor_solicitado.Value = pAuxilio.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAuxilio.fecha_aprobacion == null)
                            pfecha_aprobacion.Value = DBNull.Value;
                        else
                            pfecha_aprobacion.Value = pAuxilio.fecha_aprobacion;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAuxilio.valor_aprobado == null)
                            pvalor_aprobado.Value = DBNull.Value;
                        else
                            pvalor_aprobado.Value = pAuxilio.valor_aprobado;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAuxilio.fecha_desembolso == null)
                            pfecha_desembolso.Value = DBNull.Value;
                        else
                            pfecha_desembolso.Value = pAuxilio.fecha_desembolso;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilio.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pAuxilio.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAuxilio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pAuxilio.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pvalor_matricula = cmdTransaccionFactory.CreateParameter();
                        pvalor_matricula.ParameterName = "p_valor_matricula";
                        if (pAuxilio.valor_matricula == null)
                            pvalor_matricula.Value = DBNull.Value;
                        else
                            pvalor_matricula.Value = pAuxilio.valor_matricula;
                        pvalor_matricula.Direction = ParameterDirection.Input;
                        pvalor_matricula.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_matricula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuxilioData", "CrearAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public ReporteAuxilio ModificarAuxilio(ReporteAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pAuxilio.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAuxilio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pAuxilio.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pvalor_solicitado = cmdTransaccionFactory.CreateParameter();
                        pvalor_solicitado.ParameterName = "p_valor_solicitado";
                        if (pAuxilio.valor_solicitado == null)
                            pvalor_solicitado.Value = DBNull.Value;
                        else
                            pvalor_solicitado.Value = pAuxilio.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAuxilio.fecha_aprobacion == null)
                            pfecha_aprobacion.Value = DBNull.Value;
                        else
                            pfecha_aprobacion.Value = pAuxilio.fecha_aprobacion;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAuxilio.valor_aprobado == null)
                            pvalor_aprobado.Value = DBNull.Value;
                        else
                            pvalor_aprobado.Value = pAuxilio.valor_aprobado;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAuxilio.fecha_desembolso == null)
                            pfecha_desembolso.Value = DBNull.Value;
                        else
                            pfecha_desembolso.Value = pAuxilio.fecha_desembolso;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilio.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pAuxilio.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAuxilio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pAuxilio.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pvalor_matricula = cmdTransaccionFactory.CreateParameter();
                        pvalor_matricula.ParameterName = "p_valor_matricula";
                        if (pAuxilio.valor_matricula == null)
                            pvalor_matricula.Value = DBNull.Value;
                        else
                            pvalor_matricula.Value = pAuxilio.valor_matricula;
                        pvalor_matricula.Direction = ParameterDirection.Input;
                        pvalor_matricula.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_matricula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuxilioData", "ModificarAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAuxilio(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ReporteAuxilio pAuxilio = new ReporteAuxilio();
                        pAuxilio = ConsultarAuxilio(pId, vUsuario);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuxilioData", "EliminarAuxilio", ex);
                    }
                }
            }
        }


        public ReporteAuxilio ConsultarAuxilio(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ReporteAuxilio entidad = new ReporteAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AUXILIOS WHERE NUMERO_AUXILIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["VALOR_MATRICULA"] != DBNull.Value) entidad.valor_matricula = Convert.ToDecimal(resultado["VALOR_MATRICULA"]);
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
                        BOExcepcion.Throw("AuxilioData", "ConsultarAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteAuxilio> ListarAuxilio(String filtro,DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            Configuracion conf = new Configuracion();
            DbDataReader resultado;
            List<ReporteAuxilio> lstAuxilio = new List<ReporteAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT parentescos.descripcion AS DESCRIPCIONES, auxilios.estado as esteestado, persona.estado as nombresestados, LINEASAUXILIOS.estado as EstadoLinea,
                                        LINEASAUXILIOS.*, Auxilios.*, AUXILIOS_BENEFICIARIOS.*, persona.primer_nombre || ' ' || persona.primer_apellido as nombres,
                                        persona.identificacion as identificacionpersona , persona.cod_nomina,FECSUMDIA(FECHA_DESEMBOLSO,NUMERO_DIAS,1) AS Fecha_proxima_solicitud
                                        FROM AUXILIOS 
                                        LEFT JOIN PERSONA ON PERSONA.COD_PERSONA = AUXILIOS.COD_PERSONA 
                                        INNER JOIN LINEASAUXILIOS ON AUXILIOS.COD_LINEA_AUXILIO = LINEASAUXILIOS.COD_LINEA_AUXILIO 
                                        LEFT JOIN PERIODICIDAD ON LINEASAUXILIOS.COD_PERIODICIDAD = PERIODICIDAD.COD_PERIODICIDAD
                                        LEFT JOIN AUXILIOS_BENEFICIARIOS ON AUXILIOS.NUMERO_AUXILIO = AUXILIOS_BENEFICIARIOS.NUMERO_AUXILIO 
                                        LEFT JOIN PARENTESCOS ON auxilios_beneficiarios.cod_parentesco = parentescos.codparentesco 

                                        WHERE 1=1 " + filtro;
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " AUXILIOS.fecha_solicitud >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " AUXILIOS.fecha_solicitud >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " AUXILIOS.fecha_solicitud <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')" + " ORDER BY Auxilios.NUMERO_AUXILIO ";
                            else
                                sql += " AUXILIOS.fecha_solicitud <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' " + " ORDER BY Auxilios.NUMERO_AUXILIO ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuxilio entidad = new ReporteAuxilio();
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_PARENTESCO"] != DBNull.Value) entidad.cod_parentesco = Convert.ToInt64(resultado["COD_PARENTESCO"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DESCRIPCIONES"] != DBNull.Value) entidad.descripciones = Convert.ToString(resultado["DESCRIPCIONES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["IDENTIFICACIONPERSONA"] != DBNull.Value) entidad.identificacionPersona = Convert.ToString(resultado["IDENTIFICACIONPERSONA"]);
                            if (resultado["ESTEESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTEESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["VALOR_MATRICULA"] != DBNull.Value) entidad.valor_matricula = Convert.ToDecimal(resultado["VALOR_MATRICULA"]);
                            if (resultado["Fecha_proxima_solicitud"] != DBNull.Value) entidad.fecha_proxima_solicitud = Convert.ToDateTime(resultado["Fecha_proxima_solicitud"]);

                            
                            lstAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteAuxilioData", "ListarAuxilio", ex);
                        return null;
                    }
                }
            }
        }




        public List<ReporteAuxilio> ListarAuxilioPorAnular(String filtro, Usuario vUsuario)
        {
            Configuracion conf = new Configuracion();
            DbDataReader resultado;
            List<ReporteAuxilio> lstAuxilio = new List<ReporteAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Auxilios.*, LINEASAUXILIOS.DESCRIPCION AS NOMLINEA , V_PERSONA.IDENTIFICACION , V_PERSONA.NOMBRE, V_PERSONA.COD_NOMINA,
                                       CASE AUXILIOS.ESTADO WHEN 'S' THEN 'SOLICITADO' WHEN 'A' THEN 'APROBADO' END AS NOMESTADO
                                       FROM AUXILIOS LEFT JOIN V_PERSONA ON V_PERSONA.COD_PERSONA = AUXILIOS.COD_PERSONA
                                       INNER JOIN LINEASAUXILIOS ON AUXILIOS.COD_LINEA_AUXILIO = LINEASAUXILIOS.COD_LINEA_AUXILIO
                                       where 1 = 1 " + filtro;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuxilio entidad = new ReporteAuxilio();
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);                            
                            if (resultado["VALOR_MATRICULA"] != DBNull.Value) entidad.valor_matricula = Convert.ToDecimal(resultado["VALOR_MATRICULA"]);

                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            //if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMESTADO"]);
                            lstAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuxilioData", "ListarAuxilioPorAnular", ex);
                        return null;
                    }
                }
            }
        }

        public void GenerarAnulacionAuxilio(ReporteAuxilio pData, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pData.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pData.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_ANULACIONAUX";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuxilioData", "GenerarAnulacionAuxilio", ex);
                    }
                }
            }
        }


    }
}
