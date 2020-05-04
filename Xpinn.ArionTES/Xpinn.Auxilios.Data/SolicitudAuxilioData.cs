using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;

namespace Xpinn.Auxilios.Data
{   
    public class SolicitudAuxilioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public SolicitudAuxilioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public SolicitudAuxilio CrearSolicitudAuxilio(SolicitudAuxilio pAuxilio, Usuario vUsuario)
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
                        pnumero_auxilio.Direction = ParameterDirection.Output;
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
                        pvalor_solicitado.Value = pAuxilio.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAuxilio.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAuxilio.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAuxilio.valor_aprobado != 0) pvalor_aprobado.Value = pAuxilio.valor_aprobado; else pvalor_aprobado.Value = DBNull.Value;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAuxilio.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAuxilio.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilio.detalle != null) pdetalle.Value = pAuxilio.detalle; else pdetalle.Value = DBNull.Value;
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
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pvalor_matricula = cmdTransaccionFactory.CreateParameter();
                        pvalor_matricula.ParameterName = "p_valor_matricula";
                        if (pAuxilio.porc_matricula != 0) pvalor_matricula.Value = pAuxilio.porc_matricula; else pvalor_matricula.Value = DBNull.Value;
                        pvalor_matricula.Direction = ParameterDirection.Input;
                        pvalor_matricula.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_matricula);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pAuxilio.numero_radicacion == null)
                            pnumero_radicacion.Value = DBNull.Value;
                        else
                            pnumero_radicacion.Value = pAuxilio.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilio.numero_auxilio = Convert.ToInt32(pnumero_auxilio.Value);

                        DAauditoria.InsertarLog(pAuxilio, "AUXILIOS", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Auxilios, "Creacion de auxilios con numero de auxilio " + pAuxilio.numero_auxilio);

                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "CrearSolicitudAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public SolicitudAuxilio ModificarSolicitudAuxilio(SolicitudAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SolicitudAuxilio auxilioAnterior = ConsultarAUXILIO(pAuxilio.numero_auxilio, vUsuario);

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
                        pvalor_solicitado.Value = pAuxilio.valor_solicitado;
                        pvalor_solicitado.Direction = ParameterDirection.Input;
                        pvalor_solicitado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_solicitado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pAuxilio.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pAuxilio.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor_aprobado = cmdTransaccionFactory.CreateParameter();
                        pvalor_aprobado.ParameterName = "p_valor_aprobado";
                        if (pAuxilio.valor_aprobado != 0) pvalor_aprobado.Value = pAuxilio.valor_aprobado; else pvalor_aprobado.Value = DBNull.Value;
                        pvalor_aprobado.Direction = ParameterDirection.Input;
                        pvalor_aprobado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_aprobado);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAuxilio.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAuxilio.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilio.detalle != null) pdetalle.Value = pAuxilio.detalle; else pdetalle.Value = DBNull.Value;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAuxilio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pvalor_matricula = cmdTransaccionFactory.CreateParameter();
                        pvalor_matricula.ParameterName = "p_valor_matricula";
                        if (pAuxilio.porc_matricula != 0) pvalor_matricula.Value = pAuxilio.porc_matricula; else pvalor_matricula.Value = DBNull.Value;
                        pvalor_matricula.Direction = ParameterDirection.Input;
                        pvalor_matricula.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_matricula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAuxilio, "AUXILIOS", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Auxilios, "Modificacion de auxilios con numero de auxilio " + pAuxilio.numero_auxilio, auxilioAnterior);

                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ModificarSolicitudAuxilio", ex);
                        return null;
                    }
                }
            }
        }

        public List<SolicitudAuxilio> ListarSolicitudAuxilio(SolicitudAuxilio pAuxilio, DateTime pFechaSol, Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<SolicitudAuxilio> lstAuxilio = new List<SolicitudAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.numero_auxilio,a.fecha_solicitud ,l.descripcion  ,p.identificacion, a.estado, "
                                        +"p.primer_nombre||' '|| p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as nombre," +
                                        "p.cod_nomina, a.valor_solicitado "
                                        +"from auxilios a inner join lineasauxilios l on a.cod_linea_auxilio = l.cod_linea_auxilio "
                                        +"inner join persona p on p.cod_persona = a.cod_persona where 1=1" + filtro;

                        Configuracion conf = new Configuracion();
                        if (pFechaSol != null && pFechaSol != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " a.fecha_solicitud = To_Date('" + Convert.ToDateTime(pFechaSol).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " a.fecha_solicitud = '" + Convert.ToDateTime(pFechaSol).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY a.numero_auxilio ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SolicitudAuxilio entidad = new SolicitudAuxilio();
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);

                            lstAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ListarSolicitudAuxilio", ex);
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
                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pId;
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
                        BOExcepcion.Throw("SolicitudAuxilioData", "EliminarAuxilio", ex);
                    }
                }
            }
        }

        public SolicitudAuxilio ConsultarAUXILIO(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudAuxilio entidad = new SolicitudAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT auxilios.*,persona_afiliacion.estado as nomestado,persona.cod_persona as codigopersona ,persona.identificacion,persona.primer_nombre "
                                       +" ||' '|| persona.segundo_nombre||' '|| persona.primer_apellido||' '||persona.segundo_apellido as Nombre "
                                       +"FROM auxilios inner join persona "
                                       +"on persona.cod_persona = auxilios.cod_persona "
                                       +"left join persona_afiliacion on persona.cod_persona=persona_afiliacion.cod_persona where numero_auxilio = " + pId.ToString();
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
                            if (resultado["VALOR_MATRICULA"] != DBNull.Value) entidad.porc_matricula = Convert.ToDecimal(resultado["VALOR_MATRICULA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarAUXILIO", ex);
                        return null;
                    }
                }
            }
        }

        //Detalle

        public DetalleSolicitudAuxilio CrearDetalleAuxilio(DetalleSolicitudAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodbeneficiarioaux = cmdTransaccionFactory.CreateParameter();
                        pcodbeneficiarioaux.ParameterName = "p_codbeneficiarioaux";
                        pcodbeneficiarioaux.Value = pAuxilio.codbeneficiarioaux;
                        pcodbeneficiarioaux.Direction = ParameterDirection.Output;
                        pcodbeneficiarioaux.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodbeneficiarioaux);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pAuxilio.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pAuxilio.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_parentesco = cmdTransaccionFactory.CreateParameter();
                        pcod_parentesco.ParameterName = "p_cod_parentesco";
                        pcod_parentesco.Value = pAuxilio.cod_parentesco;
                        pcod_parentesco.Direction = ParameterDirection.Input;
                        pcod_parentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_parentesco);

                        DbParameter pporcentaje_beneficiario = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_beneficiario.ParameterName = "p_porcentaje_beneficiario";
                        pporcentaje_beneficiario.Value = pAuxilio.porcentaje_beneficiario;
                        pporcentaje_beneficiario.Direction = ParameterDirection.Input;
                        pporcentaje_beneficiario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_beneficiario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_DETAAUXILI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilio.codbeneficiarioaux = Convert.ToInt32(pcodbeneficiarioaux.Value);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "CrearDetalleAuxilio", ex);
                        return null;
                    }
                }
            }
        }

        public DetalleSolicitudAuxilio ModificarDetalleAuxilio(DetalleSolicitudAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodbeneficiarioaux = cmdTransaccionFactory.CreateParameter();
                        pcodbeneficiarioaux.ParameterName = "p_codbeneficiarioaux";
                        pcodbeneficiarioaux.Value = pAuxilio.codbeneficiarioaux;
                        pcodbeneficiarioaux.Direction = ParameterDirection.Input;
                        pcodbeneficiarioaux.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodbeneficiarioaux);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pAuxilio.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pAuxilio.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcod_parentesco = cmdTransaccionFactory.CreateParameter();
                        pcod_parentesco.ParameterName = "p_cod_parentesco";
                        pcod_parentesco.Value = pAuxilio.cod_parentesco;
                        pcod_parentesco.Direction = ParameterDirection.Input;
                        pcod_parentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_parentesco);

                        DbParameter pporcentaje_beneficiario = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_beneficiario.ParameterName = "p_porcentaje_beneficiario";
                        pporcentaje_beneficiario.Value = pAuxilio.porcentaje_beneficiario;
                        pporcentaje_beneficiario.Direction = ParameterDirection.Input;
                        pporcentaje_beneficiario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_beneficiario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_DETAAUXILI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ModificarDetalleAuxilio", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodbeneficiarioaux = cmdTransaccionFactory.CreateParameter();
                        pcodbeneficiarioaux.ParameterName = "p_codbeneficiarioaux";
                        pcodbeneficiarioaux.Value = pId;
                        pcodbeneficiarioaux.Direction = ParameterDirection.Input;
                        pcodbeneficiarioaux.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodbeneficiarioaux);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_DETAAUXILI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "EliminarDETALLEAuxilio", ex);
                    }
                }
            }
        }


        public List<DetalleSolicitudAuxilio> ConsultarDETALLEAuxilio(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetalleSolicitudAuxilio> LstDetalle = new List<DetalleSolicitudAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM auxilios_beneficiarios WHERE NUMERO_AUXILIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetalleSolicitudAuxilio entidad = new DetalleSolicitudAuxilio();
                            if (resultado["CODBENEFICIARIOAUX"] != DBNull.Value) entidad.codbeneficiarioaux = Convert.ToInt32(resultado["CODBENEFICIARIOAUX"]);
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_PARENTESCO"] != DBNull.Value) entidad.cod_parentesco = Convert.ToInt32(resultado["COD_PARENTESCO"]);
                            if (resultado["PORCENTAJE_BENEFICIARIO"] != DBNull.Value) entidad.porcentaje_beneficiario = Convert.ToDecimal(resultado["PORCENTAJE_BENEFICIARIO"]);
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarDETALLEAuxilio", ex);
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
                        string sql = "select max(numero_auxilio) +1 from auxilios ";

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




        public SolicitudAuxilio ListarLineasDauxilios(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudAuxilio entidad = new SolicitudAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_LINEA_AUXILIO, DESCRIPCION, monto_maximo, numero_auxilios, educativo, porc_matricula, orden_servicio, permite_retirados FROM lineasauxilios WHERE cod_linea_auxilio = '" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["MONTO_MAXIMO"] != DBNull.Value) entidad.monto_maximo = Convert.ToDecimal(resultado["MONTO_MAXIMO"]);
                            if (resultado["NUMERO_AUXILIOS"] != DBNull.Value) entidad.cupos = Convert.ToInt32(resultado["NUMERO_AUXILIOS"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["PORC_MATRICULA"] != DBNull.Value) entidad.porc_matricula = Convert.ToDecimal(resultado["PORC_MATRICULA"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["PERMITE_RETIRADOS"] != DBNull.Value) entidad.permite_retirados = Convert.ToInt32(resultado["PERMITE_RETIRADOS"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ListarLineasDauxilios", ex);
                        return null;
                    }
                }
            }
        }



        //DETALLE VALIDACION REQUISITOS

        public List<Requisitos> ConsultarValidacionRequisitos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Requisitos> LstDetalle = new List<Requisitos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineasauxilios_requisitos where COD_LINEA_AUXILIO = '" + pId.ToString() + "' ORDER BY CODREQUISITOAUX";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Requisitos entidad = new Requisitos();
                            if (resultado["CODREQUISITOAUX"] != DBNull.Value) entidad.codrequisitoaux = Convert.ToInt64(resultado["CODREQUISITOAUX"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarValidacionRequisitos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Requisitos> CargarDatosRequisitos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Requisitos> LstDetalle = new List<Requisitos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.codrequisitoaux,  l.descripcion,a.codrequisitoauxilio,a.numero_auxilio,a.aceptado "
                                      +"from auxilios_requisitos a inner join lineasauxilios_requisitos l "
                                      +"on l.codrequisitoaux = a.codrequisitoaux "
                                      +"where a.numero_auxilio = " + pId.ToString() + " ORDER BY CODREQUISITOAUX";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Requisitos entidad = new Requisitos();
                            if (resultado["CODREQUISITOAUX"] != DBNull.Value) entidad.codrequisitoaux = Convert.ToInt64(resultado["CODREQUISITOAUX"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODREQUISITOAUXILIO"] != DBNull.Value) entidad.codrequisitoauxilio = Convert.ToInt32(resultado["CODREQUISITOAUXILIO"]);
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["ACEPTADO"] != DBNull.Value) entidad.aceptado= Convert.ToInt32(resultado["ACEPTADO"]);                            
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarValidacionRequisitos", ex);
                        return null;
                    }
                }
            }
        }


        public Requisitos CrearAuxiliosRequisitos(Requisitos pAuxilios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrequisitoauxilio = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoauxilio.ParameterName = "p_codrequisitoauxilio";
                        pcodrequisitoauxilio.Value = pAuxilios.codrequisitoauxilio;
                        pcodrequisitoauxilio.Direction = ParameterDirection.Output;
                        pcodrequisitoauxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoauxilio);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilios.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pcodrequisitoaux = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoaux.ParameterName = "p_codrequisitoaux";
                        pcodrequisitoaux.Value = pAuxilios.codrequisitoaux;
                        pcodrequisitoaux.Direction = ParameterDirection.Input;
                        pcodrequisitoaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoaux);

                        DbParameter paceptado = cmdTransaccionFactory.CreateParameter();
                        paceptado.ParameterName = "p_aceptado";
                        paceptado.Value = pAuxilios.aceptado;
                        paceptado.Direction = ParameterDirection.Input;
                        paceptado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paceptado);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_REQUISITOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilios.codrequisitoauxilio = Convert.ToInt32(pcodrequisitoauxilio.Value);
                        return pAuxilios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "CrearAuxiliosRequisitos", ex);
                        return null;
                    }
                }
            }
        }


        public Requisitos ModificarAuxiliosRequisitos(Requisitos pAuxilios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrequisitoauxilio = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoauxilio.ParameterName = "p_codrequisitoauxilio";
                        pcodrequisitoauxilio.Value = pAuxilios.codrequisitoauxilio;
                        pcodrequisitoauxilio.Direction = ParameterDirection.Input;
                        pcodrequisitoauxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoauxilio);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilios.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pcodrequisitoaux = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoaux.ParameterName = "p_codrequisitoaux";
                        pcodrequisitoaux.Value = pAuxilios.codrequisitoaux;
                        pcodrequisitoaux.Direction = ParameterDirection.Input;
                        pcodrequisitoaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoaux);

                        DbParameter paceptado = cmdTransaccionFactory.CreateParameter();
                        paceptado.ParameterName = "p_aceptado";
                        paceptado.Value = pAuxilios.aceptado;
                        paceptado.Direction = ParameterDirection.Input;
                        paceptado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paceptado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_REQUISITOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilios.codrequisitoauxilio = Convert.ToInt32(pcodrequisitoauxilio.Value);
                        return pAuxilios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ModificarAuxiliosRequisitos", ex);
                        return null;
                    }
                }
            }
        }


        public Auxilio_Orden_Servicio CrearAuxilioOdenServicio(Auxilio_Orden_Servicio pAuxilioOden, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pAuxilioOden.idordenservicio;
                        pidordenservicio.Direction = ParameterDirection.Output;
                        pidordenservicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilioOden.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAuxilioOden.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAuxilioOden.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pAuxilioOden.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pAuxilioOden.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pAuxilioOden.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pAuxilioOden.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilioOden.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pAuxilioOden.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";                        
                        if (pAuxilioOden.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pAuxilioOden.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_ORDENSERV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilioOden.idordenservicio = Convert.ToInt32(pidordenservicio.Value);
                        return pAuxilioOden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "CrearAuxilioOdenServicio", ex);
                        return null;
                    }
                }
            }
        }


        public Auxilio_Orden_Servicio ModificarAuxilioOdenServicio(Auxilio_Orden_Servicio pAuxilioOden, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pAuxilioOden.idordenservicio;
                        pidordenservicio.Direction = ParameterDirection.Input;
                        pidordenservicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilioOden.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAuxilioOden.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAuxilioOden.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pAuxilioOden.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pAuxilioOden.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pAuxilioOden.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pAuxilioOden.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pAuxilioOden.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pAuxilioOden.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pAuxilioOden.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pAuxilioOden.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnumero_preimpreso = cmdTransaccionFactory.CreateParameter();
                        pnumero_preimpreso.ParameterName = "p_numero_preimpreso";
                        if (pAuxilioOden.numero_preimpreso == null)
                            pnumero_preimpreso.Value = DBNull.Value;
                        else
                            pnumero_preimpreso.Value = pAuxilioOden.numero_preimpreso;
                        pnumero_preimpreso.Direction = ParameterDirection.Input;
                        pnumero_preimpreso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_preimpreso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_ORDENSERV_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAuxilioOden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ModificarAuxilioOdenServicio", ex);
                        return null;
                    }
                }
            }
        }
        
        public void EliminarAuxilioOrdenServicio(Int32 pId, Int32 pNum_Auxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pId;
                        pidordenservicio.Direction = ParameterDirection.Input;
                        pidordenservicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pNum_Auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_ORDENSERV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "EliminarCreditoOrdenServicio", ex);
                    }
                }
            }
        }


        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            DbDataReader resultado;
            string estado = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT NVL(persona_afiliacion.estado, persona.estado) As estado 
                                    FROM persona LEFT JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona";
                        else
                            sql = @"SELECT CASE persona_afiliacion.estado WHEN Null THEN persona_afiliacion.estado ELSE persona.estado END As estado 
                                    FROM persona LEFT JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona";
                        if (pCodPersona != null || pCodPersona != 0)
                            sql = sql + " WHERE persona.cod_persona = " + pCodPersona.ToString();
                        else
                            sql = sql + " WHERE persona.identificacion = '" + pIdentificacion + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToString(resultado["ESTADO"]);
                            if (estado == pEstado)
                                return true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }



        public Auxilio_Orden_Servicio ConsultarAUX_OrdenServicio(String pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Auxilio_Orden_Servicio entidad = new Auxilio_Orden_Servicio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AUXILIO_ORDEN_SERVICIO " + pFiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDORDENSERVICIO"] != DBNull.Value) entidad.idordenservicio = Convert.ToInt32(resultado["IDORDENSERVICIO"]);
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idproveedor = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nomproveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.numero_preimpreso = Convert.ToInt64(resultado["NUMERO_PREIMPRESO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarAUX_OrdenServicio", ex);
                        return null;
                    }
                }
            }
        }



        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(NUMERO_PREIMPRESO) + 1 from AUXILIO_ORDEN_SERVICIO";

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
                        BOExcepcion.Throw("SolicitudAuxilioData", "ObtenerNumeroPreImpreso", ex);
                        return -1;
                    }
                }
            }
        }

        public void ModificarAuxilio_OrdenServ(Auxilio_Orden_Servicio pAuxi,ref string pError, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "UPDATE AUXILIO_ORDEN_SERVICIO SET NUMERO_PREIMPRESO = "+ pAuxi.numero_preimpreso + " WHERE NUMERO_AUXILIO = " + pAuxi.numero_auxilio;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                    }
                }
            }
        }

        public SolicitudAuxilio Consultar_Auxilio_Variado(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudAuxilio entidad = new SolicitudAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT auxilios.*,persona_afiliacion.estado as nomestado,V_PERSONA.IDENTIFICACION,V_PERSONA.NOMBRE "
                                            +"FROM AUXILIOS INNER JOIN V_PERSONA "
                                            +"ON V_PERSONA.COD_PERSONA = AUXILIOS.COD_PERSONA "
                                            +"left join persona_afiliacion on V_PERSONA.cod_persona = persona_afiliacion.cod_persona " + pFiltro.ToString();
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
                            if (resultado["VALOR_MATRICULA"] != DBNull.Value) entidad.porc_matricula = Convert.ToDecimal(resultado["VALOR_MATRICULA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "Consultar_Auxilio_Variado", ex);
                        return null;
                    }
                }
            }
        }


        public SolicitudAuxilio Generar_desembolso_auxilio(SolicitudAuxilio pAuxilio, Usuario vUsuario)
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

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        if (pAuxilio.fecha_desembolso != DateTime.MinValue) pfecha_desembolso.Value = pAuxilio.fecha_desembolso; else pfecha_desembolso.Value = DBNull.Value;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAuxilio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pAuxilio.numero_radicacion == null)
                            pnumero_radicacion.Value = DBNull.Value;
                        else
                            pnumero_radicacion.Value = pAuxilio.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_DESEMBOLSO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuxilio.numero_auxilio = Convert.ToInt32(pnumero_auxilio.Value);
                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "Generar_desembolso_auxilio", ex);
                        return null;
                    }
                }
            }
        }

    }
}
