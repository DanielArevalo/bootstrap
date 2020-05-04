using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ReporteAuditoria 
    /// </summary>
    public class Rep_AuditoriaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ReporteAuditoria
        /// </summary>
        public Rep_AuditoriaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<ReporteAuditoria> ConsultarReporte(ReporteAuditoria pReporteAuditoria, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstReporteAuditoria = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reportes_auditorias " + ObtenerFiltro(pReporteAuditoria) + " ORDER BY 1 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["IDREPORTE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstReporteAuditoria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteAuditoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteAuditoriaData", "ConsultarReporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteAuditoria> ListarRep_aud_persona(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstReporteAuditoria = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AUD_PERSONA  WHERE  identificacion = '" + pId.ToString() + "' ORDER BY FECHA_CAMBIO  desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt64(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.cod_zona = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barresidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dircorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telcorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value) entidad.ciucorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barcorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["NUMHIJOS"] != DBNull.Value) entidad.numhijos = Convert.ToInt64(resultado["NUMHIJOS"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.numpersonasacargo = Convert.ToInt64(resultado["NUMPERSONASACARGO"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["ANTIGUEDADLABORAL"] != DBNull.Value) entidad.antiguedadlaboral = Convert.ToInt64(resultado["ANTIGUEDADLABORAL"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt64(resultado["ESTRATO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt64(resultado["POSICIONEMPRESA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt64(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt64(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio = Convert.ToString(resultado["USUARIO_CAMBIO"]);
                            lstReporteAuditoria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteAuditoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteAuditoriaData", "ListarReporteAuditoria", ex);
                        return null;
                    }
                }
            }
        }

        public List<Auditoria> ListarAuditoriaDeTablaAuditoria(int tipoReporte, Usuario usuario)
        {
            DbDataReader resultado;
            List<Auditoria> lstReporteAuditoria = new List<Auditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select aud.* , usu.NOMBRE, ops.nombre as nombreOpcion
                                        from auditoria aud
                                        left join USUARIOS usu on usu.codusuario = aud.codusuario
                                        left join OPCIONES ops on ops.COD_OPCION = aud.CODOPCION
                                        WHERE aud.TIPOAUDITORIA = " + tipoReporte + " ORDER BY aud.fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Auditoria entidad = new Auditoria();

                            if (resultado["COD_AUDITORIA"] != DBNull.Value) entidad.cod_auditoria = Convert.ToInt64(resultado["COD_AUDITORIA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_usuario = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CODOPCION"] != DBNull.Value) entidad.codopcion = Convert.ToInt64(resultado["CODOPCION"]);
                            if (resultado["nombreOpcion"] != DBNull.Value) entidad.nombre_opcion = Convert.ToString(resultado["nombreOpcion"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["TIPOAUDITORIA"] != DBNull.Value) entidad.tipo_auditoria = Convert.ToInt32(resultado["TIPOAUDITORIA"]);
                            if (resultado["INFORMACIONANTERIOR"] != DBNull.Value) entidad.informacionanterior = Convert.ToString(resultado["INFORMACIONANTERIOR"]);

                            lstReporteAuditoria.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteAuditoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteAuditoriaData", "ListarAuditoriaDeTablaAuditoria", ex);
                        return null;
                    }
                }
            }
        }

        public ReporteAuditoria ConsultarReportPersonas(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ReporteAuditoria entidad = new ReporteAuditoria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM aud_persona WHERE  = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value) entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt64(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.cod_zona = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barresidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dircorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telcorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value) entidad.ciucorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barcorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["NUMHIJOS"] != DBNull.Value) entidad.numhijos = Convert.ToInt64(resultado["NUMHIJOS"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.numpersonasacargo = Convert.ToInt64(resultado["NUMPERSONASACARGO"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["ANTIGUEDADLABORAL"] != DBNull.Value) entidad.antiguedadlaboral = Convert.ToInt64(resultado["ANTIGUEDADLABORAL"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt64(resultado["ESTRATO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt64(resultado["POSICIONEMPRESA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt64(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt64(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio = Convert.ToString(resultado["USUARIO_CAMBIO"]);
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
                        BOExcepcion.Throw("ReporteAuditoriaData", "ConsultarReporteAuditoria", ex);
                        return null;
                    }
                }
            }
        }

        public ReporteAuditoria ConsultarRep_aud_credito(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ReporteAuditoria entidad = new ReporteAuditoria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM aud_credito WHERE  = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_DESEMBOLSADO"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["MONTO_DESEMBOLSADO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_PRIMERPAGO"] != DBNull.Value) entidad.fecha_primerpago = Convert.ToDateTime(resultado["FECHA_PRIMERPAGO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToInt64(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt64(resultado["TIPO_GRACIA"]);
                            if (resultado["COD_ATR_GRA"] != DBNull.Value) entidad.cod_atr_gra = Convert.ToInt64(resultado["COD_ATR_GRA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.tipo_credito = Convert.ToString(resultado["TIPO_CREDITO"]);
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.num_radic_origen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"]);
                            if (resultado["VRREESTRUCTURADO"] != DBNull.Value) entidad.vrreestructurado = Convert.ToInt64(resultado["VRREESTRUCTURADO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.cod_pagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"]);
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.gradiente = Convert.ToInt64(resultado["GRADIENTE"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.dias_ajuste = Convert.ToInt64(resultado["DIAS_AJUSTE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio = Convert.ToString(resultado["USUARIO_CAMBIO"]);
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
                        BOExcepcion.Throw("AUDITORIAData", "ConsultarAuditoriaCredito", ex);
                        return null;
                    }
                }
            }
        }



        public List<ReporteAuditoria> ListarRep_aud_credito(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstAUDITORIA = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AUD_CREDITO WHERE numero_radicacion = " + pId.ToString() + " ORDER BY FECHA_CAMBIO  desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.numero_obligacion = Convert.ToString(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_solicitado = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["MONTO_DESEMBOLSADO"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["MONTO_DESEMBOLSADO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_PRIMERPAGO"] != DBNull.Value) entidad.fecha_primerpago = Convert.ToDateTime(resultado["FECHA_PRIMERPAGO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToInt64(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["TIPO_GRACIA"] != DBNull.Value) entidad.tipo_gracia = Convert.ToInt64(resultado["TIPO_GRACIA"]);
                            if (resultado["COD_ATR_GRA"] != DBNull.Value) entidad.cod_atr_gra = Convert.ToInt64(resultado["COD_ATR_GRA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.otros_saldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.tipo_credito = Convert.ToString(resultado["TIPO_CREDITO"]);
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.num_radic_origen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"]);
                            if (resultado["VRREESTRUCTURADO"] != DBNull.Value) entidad.vrreestructurado = Convert.ToInt64(resultado["VRREESTRUCTURADO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.cod_pagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"]);
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.gradiente = Convert.ToInt64(resultado["GRADIENTE"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.dias_ajuste = Convert.ToInt64(resultado["DIAS_AJUSTE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio = Convert.ToString(resultado["USUARIO_CAMBIO"]);
                            lstAUDITORIA.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAUDITORIA;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AUDITORIAData", "ListarAuditoriaCredito", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteAuditoria> ListarRep_aud_operacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstAUDITORIA = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM AUD_OPERACION WHERE cod_ope = " + pId.ToString() + " ORDER BY FECHA_CAMBIO  desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["TIPO_OPE"]);
                            if (resultado["COD_USU"] != DBNull.Value) entidad.cod_usu = Convert.ToInt64(resultado["COD_USU"]);
                            if (resultado["COD_OFI"] != DBNull.Value) entidad.cod_ofi = Convert.ToInt64(resultado["COD_OFI"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["NUM_RECIBO"] != DBNull.Value) entidad.num_recibo = Convert.ToInt64(resultado["NUM_RECIBO"]);
                            if (resultado["FECHA_REAL"] != DBNull.Value) entidad.fecha_real = Convert.ToDateTime(resultado["FECHA_REAL"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToDateTime(resultado["HORA"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["FECHA_CALC"] != DBNull.Value) entidad.fecha_calc = Convert.ToDateTime(resultado["FECHA_CALC"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado_ope = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["NUM_LISTA"] != DBNull.Value) entidad.num_lista = Convert.ToInt64(resultado["NUM_LISTA"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio_ope = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio_ope = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio_ope = Convert.ToString(resultado["USUARIO_CAMBIO"]);
                            lstAUDITORIA.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAUDITORIA;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AUDITORIAData", "ListarRep_aud_operacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<ReporteAuditoria> ListarRep_aud_comprobante(Int64 pId, string pTipo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstAUDITORIA = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pTipo == "1")
                            sql = @"SELECT * FROM AUD_E_COMINGRES WHERE NUM_COMP = " + pId.ToString() + " ORDER BY FECHA_CAMBIO  desc";
                        else if (pTipo == "5")
                            sql = @"SELECT * FROM AUD_E_COMEGRES WHERE NUM_COMP = " + pId.ToString() + " ORDER BY FECHA_CAMBIO  desc";
                        else
                            sql = @"SELECT * FROM AUD_E_COMCONTA WHERE NUM_COMP = " + pId.ToString() + " AND TIPO_COMP= " + pTipo + " ORDER BY FECHA_CAMBIO  desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_compr = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (pTipo != "1" && pTipo != "5")
                                if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_compr = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToDateTime(resultado["HORA"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["TOTALCOM"] != DBNull.Value) entidad.totalcom = Convert.ToInt64(resultado["TOTALCOM"]);
                            if (resultado["TIPO_BENEF"] != DBNull.Value) entidad.tipo_benef = Convert.ToString(resultado["TIPO_BENEF"]);
                            if (resultado["COD_BENEF"] != DBNull.Value) entidad.cod_benef = Convert.ToInt64(resultado["COD_BENEF"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (pTipo != "1")
                            {
                                if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado_compr = Convert.ToString(resultado["ESTADO"]);
                            }
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["TIPO_CAMBIO"] != DBNull.Value) entidad.tipo_cambio_compr = Convert.ToString(resultado["TIPO_CAMBIO"]);
                            if (resultado["FECHA_CAMBIO"] != DBNull.Value) entidad.fecha_cambio_compr = Convert.ToDateTime(resultado["FECHA_CAMBIO"]);
                            if (resultado["USUARIO_CAMBIO"] != DBNull.Value) entidad.usuario_cambio_compr = Convert.ToString(resultado["USUARIO_CAMBIO"]);
                            if (pTipo == "1" || pTipo == "5")
                            {
                                if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago_compr = Convert.ToInt64(resultado["TIPO_PAGO"]);
                                if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToInt64(resultado["ENTIDAD"]);
                            }
                            if (pTipo == "1")
                                if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.numero_pago_compr = Convert.ToInt64(resultado["NUM_CONSIG"]);
                            if (pTipo == "5")
                                if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.numero_documento_compr = Convert.ToInt64(resultado["N_DOCUMENTO"]);

                            lstAUDITORIA.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAUDITORIA;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AUDITORIAData", "ListarRep_aud_operacion", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable GenerarReporte(string tablaAuditoria, ref string[] aColumnas, ref Type[] aTipos, ref int numerocolumnas, ref string sError, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtReporte = new DataTable();
            numerocolumnas = 0;
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        Xpinn.Reporteador.Entities.Reporte eReporte = new Xpinn.Reporteador.Entities.Reporte();
                        string sql = "SELECT * FROM "+tablaAuditoria;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtReporte.Clear();
                        Xpinn.Reporteador.Data.ReporteData reportData = new Reporteador.Data.ReporteData();                        
                        Boolean bRes = reportData.TraerResultados(resultado, ref dtReporte, ref aColumnas, ref aTipos, ref numerocolumnas, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "GenerarReporte", ex);
                        return null;
                    }
                }
            }
        }
        public List<ReporteAuditoria> ConsultarReportePerfil(string  pReporteAuditoria, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteAuditoria> lstReporteAuditoria = new List<ReporteAuditoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select u.CODUSUARIO,u.IDENTIFICACION,u.NOMBRE,perusu.nombreperfil,op.cod_opcion,op.nombre opciones,
                                       case peracces.insertar when 1 then 'Si' else 'No' end as Insertar,case peracces.modificar when 1 then 'Si' else 'No' end as Modificar,
                                       case peracces.borrar when 1 then 'Si' else 'No' end as Borrar,case peracces.Consultar when 1 then 'Si' else 'No' end as Consultar
                                       from usuarios u join perfil_usuario perusu
                                       on u.codperfil=perusu.codperfil join perfil_acceso peracces on peracces.codperfil=perusu.codperfil
                                       join opciones op on peracces.cod_opcion=op.cod_opcion " + pReporteAuditoria + " ORDER BY 1 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteAuditoria entidad = new ReporteAuditoria();
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.cod_usu = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["nombreperfil"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["nombreperfil"]);
                            if (resultado["cod_opcion"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["cod_opcion"]);
                            if (resultado["opciones"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["opciones"]);
                            if (resultado["Insertar"] != DBNull.Value) entidad.Insertar = Convert.ToString(resultado["Insertar"]);
                            if (resultado["Modificar"] != DBNull.Value) entidad.Modificar = Convert.ToString(resultado["Modificar"]);
                            if (resultado["Borrar"] != DBNull.Value) entidad.Borrar = Convert.ToString(resultado["Borrar"]);
                            if (resultado["Consultar"] != DBNull.Value) entidad.Consultar = Convert.ToString(resultado["Consultar"]);

                            lstReporteAuditoria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteAuditoria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteAuditoriaData", "ConsultarReportePerfil", ex);
                        return null;
                    }
                }
            }
        }
    }

}