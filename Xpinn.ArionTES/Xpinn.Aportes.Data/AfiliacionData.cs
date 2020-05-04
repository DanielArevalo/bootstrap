using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class AfiliacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public AfiliacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Afiliacion CrearPersonaAfiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidafiliacion = cmdTransaccionFactory.CreateParameter();
                        pidafiliacion.ParameterName = "p_idafiliacion";
                        pidafiliacion.Value = pAfiliacion.idafiliacion;
                        pidafiliacion.Direction = ParameterDirection.Output;
                        pidafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidafiliacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAfiliacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_afiliacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_afiliacion.ParameterName = "p_fecha_afiliacion";
                        pfecha_afiliacion.Value = pAfiliacion.fecha_afiliacion;
                        pfecha_afiliacion.Direction = ParameterDirection.Input;
                        pfecha_afiliacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_afiliacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAfiliacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pAfiliacion.fecha_retiro != DateTime.MinValue && pAfiliacion.fecha_retiro != null) pfecha_retiro.Value = pAfiliacion.fecha_retiro; else pfecha_retiro.Value = DBNull.Value;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pAfiliacion.valor != 0) pvalor.Value = pAfiliacion.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_primer_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_primer_pago.ParameterName = "p_fecha_primer_pago";
                        if (pAfiliacion.fecha_primer_pago != null) pfecha_primer_pago.Value = pAfiliacion.fecha_primer_pago; else pfecha_primer_pago.Value = DBNull.Value;
                        pfecha_primer_pago.Direction = ParameterDirection.Input;
                        pfecha_primer_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primer_pago);

                        DbParameter pcuotas = cmdTransaccionFactory.CreateParameter();
                        pcuotas.ParameterName = "p_cuotas";
                        if (pAfiliacion.cuotas != 0) pcuotas.Value = pAfiliacion.cuotas; else pcuotas.Value = DBNull.Value;
                        pcuotas.Direction = ParameterDirection.Input;
                        pcuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pAfiliacion.cod_periodicidad != 0) pcod_periodicidad.Value = pAfiliacion.cod_periodicidad; else pcod_periodicidad.Value = DBNull.Value;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        if (pAfiliacion.forma_pago != 0) pforma_pago.Value = pAfiliacion.forma_pago; else pforma_pago.Value = DBNull.Value;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pAfiliacion.empresa_formapago != 0 && pAfiliacion.empresa_formapago != null) pcod_empresa.Value = pAfiliacion.empresa_formapago; else pcod_empresa.Value = DBNull.Value;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pasist_ultasamblea = cmdTransaccionFactory.CreateParameter();
                        pasist_ultasamblea.ParameterName = "p_asist_ultasamblea";
                        pasist_ultasamblea.Value = pAfiliacion.asist_ultasamblea;
                        pasist_ultasamblea.Direction = ParameterDirection.Input;
                        pasist_ultasamblea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasist_ultasamblea);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pAfiliacion.cod_asesor.HasValue && pAfiliacion.cod_asesor != 0)
                            p_cod_asesor.Value = pAfiliacion.cod_asesor;
                        else
                            p_cod_asesor.Value = DBNull.Value;
                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        DbParameter pcod_asociado_especial = cmdTransaccionFactory.CreateParameter();
                        pcod_asociado_especial.ParameterName = "p_cod_asociado_especial";
                        pcod_asociado_especial.Value = pAfiliacion.cod_asociado_especial.HasValue ? pAfiliacion.cod_asociado_especial : (object)DBNull.Value;
                        pcod_asociado_especial.Direction = ParameterDirection.Input;
                        pcod_asociado_especial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asociado_especial);

                        DbParameter p_es_peps = cmdTransaccionFactory.CreateParameter();
                        p_es_peps.ParameterName = "p_es_peps";
                        p_es_peps.Value = (pAfiliacion.Es_PEPS) ? 1 : 0;
                        p_es_peps.Direction = ParameterDirection.Input;
                        p_es_peps.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_es_peps);

                        DbParameter p_cargo_peps = cmdTransaccionFactory.CreateParameter();
                        p_cargo_peps.ParameterName = "p_cargo_peps";
                        if (!string.IsNullOrEmpty(pAfiliacion.cargo_PEPS))
                            p_cargo_peps.Value = pAfiliacion.cargo_PEPS;
                        else
                            p_cargo_peps.Value = DBNull.Value;
                        p_cargo_peps.Direction = ParameterDirection.Input;
                        p_cargo_peps.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cargo_peps);

                        DbParameter p_fecha_vinculacion_peps = cmdTransaccionFactory.CreateParameter();
                        p_fecha_vinculacion_peps.ParameterName = "p_fecha_vinculacion_peps";
                        if (pAfiliacion.fecha_vinculacion_PEPS == null) p_fecha_vinculacion_peps.Value = DateTime.MinValue; else p_fecha_vinculacion_peps.Value = pAfiliacion.fecha_vinculacion_PEPS;
                        p_fecha_vinculacion_peps.Direction = ParameterDirection.Input;
                        p_fecha_vinculacion_peps.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_vinculacion_peps);

                        DbParameter p_fecha_desvinculacion_peps = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desvinculacion_peps.ParameterName = "p_fecha_desvinculacion_peps";
                        if (pAfiliacion.fecha_desvinculacion_PEPS == null) p_fecha_desvinculacion_peps.Value = DateTime.MinValue; else p_fecha_desvinculacion_peps.Value = pAfiliacion.fecha_desvinculacion_PEPS;
                        p_fecha_desvinculacion_peps.Direction = ParameterDirection.Input;
                        p_fecha_desvinculacion_peps.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desvinculacion_peps);

                        DbParameter p_administra_recursos_publicos = cmdTransaccionFactory.CreateParameter();
                        p_administra_recursos_publicos.ParameterName = "p_administra_recursos_publicos";
                        p_administra_recursos_publicos.Value = (pAfiliacion.Administra_recursos_publicos) ? 1 : 0;
                        p_administra_recursos_publicos.Direction = ParameterDirection.Input;
                        p_administra_recursos_publicos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_administra_recursos_publicos);

                        DbParameter p_num_asistencias = cmdTransaccionFactory.CreateParameter();
                        p_num_asistencias.ParameterName = "p_num_asistencias";
                        p_num_asistencias.Value = pAfiliacion.numero_asistencias;
                        p_num_asistencias.Direction = ParameterDirection.Input;
                        p_num_asistencias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_asistencias);

                        DbParameter p_institucion = cmdTransaccionFactory.CreateParameter();
                        p_institucion.ParameterName = "p_institucion";
                        if (!string.IsNullOrEmpty(pAfiliacion.institucion)) p_institucion.Value = pAfiliacion.institucion; else p_institucion.Value = DBNull.Value;
                        p_institucion.Direction = ParameterDirection.Input;
                        p_institucion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_institucion);

                        DbParameter p_entidad_externa = cmdTransaccionFactory.CreateParameter();
                        p_entidad_externa.ParameterName = "p_entidad_externa";
                        if (!string.IsNullOrEmpty(pAfiliacion.entidad_externa)) p_entidad_externa.Value = pAfiliacion.entidad_externa; else p_entidad_externa.Value = DBNull.Value;
                        p_entidad_externa.Direction = ParameterDirection.Input;
                        p_entidad_externa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_entidad_externa);

                        DbParameter p_cargo_directivo = cmdTransaccionFactory.CreateParameter();
                        p_cargo_directivo.ParameterName = "p_cargo_directivo";
                        if (!string.IsNullOrEmpty(pAfiliacion.institucion)) p_cargo_directivo.Value = pAfiliacion.institucion; else p_cargo_directivo.Value = DBNull.Value;
                        p_cargo_directivo.Direction = ParameterDirection.Input;
                        p_cargo_directivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cargo_directivo);

                        DbParameter p_miembro_administracion = cmdTransaccionFactory.CreateParameter();
                        p_miembro_administracion.ParameterName = "p_miembro_administracion";
                        if (pAfiliacion.Miembro_administracion == true) p_miembro_administracion.Value = 1; else p_miembro_administracion.Value = 0;
                        p_miembro_administracion.Direction = ParameterDirection.Input;
                        p_miembro_administracion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_administracion);

                        DbParameter p_miembro_control = cmdTransaccionFactory.CreateParameter();
                        p_miembro_control.ParameterName = "p_miembro_control";
                        if (pAfiliacion.Miembro_control == true) p_miembro_control.Value = 1; else p_miembro_control.Value = 0;
                        p_miembro_control.Direction = ParameterDirection.Input;
                        p_miembro_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_control);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERAFILIAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        DAauditoria.InsertarLog(pAfiliacion, "persona_afiliacion", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Afiliaciones, "Creacion de afiliacion para la persona con codigo " + pAfiliacion.cod_persona); //REGISTRO DE AUDITORIA

                        pAfiliacion.idafiliacion = Convert.ToInt64(pidafiliacion.Value);
                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "CrearPersonaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarEstadoAfiliacion(string identificacion, Usuario usuario)
        {
            DbDataReader resultado;
            string estado = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select afi.estado 
                                        from persona_afiliacion afi
                                        join persona per on per.COD_PERSONA = afi.COD_PERSONA
                                        where per.identificacion = '" + identificacion + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["estado"] != DBNull.Value) estado = Convert.ToString(resultado["estado"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return estado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarEstadoAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarEstadoAfiliacion(Int64 pCodPersona, Usuario usuario)
        {
            DbDataReader resultado;
            string estado = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select afi.estado 
                                        from persona_afiliacion afi
                                        where afi.cod_persona = " + pCodPersona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["estado"] != DBNull.Value) estado = Convert.ToString(resultado["estado"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return estado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarEstadoAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public PersonaParentescos CrearPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = parentesco.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = parentesco.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodigoparentesco.ParameterName = "p_codigoparentesco";
                        pcodigoparentesco.Value = parentesco.codigoparentesco;
                        pcodigoparentesco.Direction = ParameterDirection.Input;
                        pcodigoparentesco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoparentesco);

                        DbParameter pcodigotipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        pcodigotipoidentificacion.ParameterName = "p_codigotipoidentificacion";
                        pcodigotipoidentificacion.Value = parentesco.codigotipoidentificacion;
                        pcodigotipoidentificacion.Direction = ParameterDirection.Input;
                        pcodigotipoidentificacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipoidentificacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = parentesco.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombresapellidos = cmdTransaccionFactory.CreateParameter();
                        pnombresapellidos.ParameterName = "p_nombresapellidos";
                        pnombresapellidos.Value = parentesco.nombresapellidos;
                        pnombresapellidos.Direction = ParameterDirection.Input;
                        pnombresapellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombresapellidos);

                        DbParameter pcodigoactividadnegocio = cmdTransaccionFactory.CreateParameter();
                        pcodigoactividadnegocio.ParameterName = "p_codigoactividadnegocio";
                        if (parentesco.codigoactividadnegocio == null)
                            pcodigoactividadnegocio.Value = DBNull.Value;
                        else
                            pcodigoactividadnegocio.Value = parentesco.codigoactividadnegocio;
                        pcodigoactividadnegocio.Direction = ParameterDirection.Input;
                        pcodigoactividadnegocio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigoactividadnegocio);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (parentesco.empresa == null)
                            pempresa.Value = DBNull.Value;
                        else
                            pempresa.Value = parentesco.empresa;
                        pempresa.Direction = ParameterDirection.Input;
                        pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pcargo = cmdTransaccionFactory.CreateParameter();
                        pcargo.ParameterName = "p_cargo";
                        if (parentesco.cargo == null)
                            pcargo.Value = DBNull.Value;
                        else
                            pcargo.Value = parentesco.cargo;
                        pcargo.Direction = ParameterDirection.Input;
                        pcargo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcargo);

                        DbParameter pingresomensual = cmdTransaccionFactory.CreateParameter();
                        pingresomensual.ParameterName = "p_ingresomensual";
                        if (parentesco.ingresomensual == null)
                            pingresomensual.Value = DBNull.Value;
                        else
                            pingresomensual.Value = parentesco.ingresomensual;
                        pingresomensual.Direction = ParameterDirection.Input;
                        pingresomensual.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pingresomensual);

                        DbParameter p_empleado_entidad = cmdTransaccionFactory.CreateParameter();
                        p_empleado_entidad.ParameterName = "p_empleado_entidad";
                        p_empleado_entidad.Value = parentesco.empleado_entidad;
                        p_empleado_entidad.Direction = ParameterDirection.Input;
                        p_empleado_entidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_empleado_entidad);

                        DbParameter p_miembro_administracion = cmdTransaccionFactory.CreateParameter();
                        p_miembro_administracion.ParameterName = "p_miembro_administracion";
                        p_miembro_administracion.Value = parentesco.miembro_administracion;
                        p_miembro_administracion.Direction = ParameterDirection.Input;
                        p_miembro_administracion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_administracion);

                        DbParameter p_miembro_control = cmdTransaccionFactory.CreateParameter();
                        p_miembro_control.ParameterName = "p_miembro_control";
                        p_miembro_control.Value = parentesco.miembro_control;
                        p_miembro_control.Direction = ParameterDirection.Input;
                        p_miembro_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_control);

                        DbParameter p_es_pep = cmdTransaccionFactory.CreateParameter();
                        p_es_pep.ParameterName = "p_es_pep";
                        p_es_pep.Value = parentesco.es_pep;
                        p_es_pep.Direction = ParameterDirection.Input;
                        p_es_pep.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_es_pep);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_PA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        parentesco.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return parentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "CrearPersonaParentesco", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaParentescos> ListarParentescoDeUnaPersona(long codigoPersona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PersonaParentescos> listaParentescos = new List<PersonaParentescos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from PERSONA_PARENTESCOS where CODIGOPERSONA = " + codigoPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PersonaParentescos entidad = new PersonaParentescos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOPARENTESCO"] != DBNull.Value) entidad.codigoparentesco = Convert.ToInt64(resultado["CODIGOPARENTESCO"]);
                            if (resultado["CODIGOTIPOIDENTIFICACION"] != DBNull.Value) entidad.codigotipoidentificacion = Convert.ToInt64(resultado["CODIGOTIPOIDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRESAPELLIDOS"] != DBNull.Value) entidad.nombresapellidos = Convert.ToString(resultado["NOMBRESAPELLIDOS"]);
                            if (resultado["CODIGOACTIVIDADNEGOCIO"] != DBNull.Value) entidad.codigoactividadnegocio = Convert.ToString(resultado["CODIGOACTIVIDADNEGOCIO"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["CARGO"] != DBNull.Value) entidad.cargo = Convert.ToString(resultado["CARGO"]);
                            if (resultado["INGRESOMENSUAL"] != DBNull.Value) entidad.ingresomensual = Convert.ToDecimal(resultado["INGRESOMENSUAL"]);
                            if (resultado["EMPLEADO_ENTIDAD"] != DBNull.Value) entidad.empleado_entidad = Convert.ToInt32(resultado["EMPLEADO_ENTIDAD"]);
                            if (resultado["MIEMBRO_ADMINISTRACION"] != DBNull.Value) entidad.miembro_administracion = Convert.ToInt32(resultado["MIEMBRO_ADMINISTRACION"]);
                            if (resultado["MIEMBRO_CONTROL"] != DBNull.Value) entidad.miembro_control = Convert.ToInt32(resultado["MIEMBRO_CONTROL"]);
                            if (resultado["ES_PEP"] != DBNull.Value) entidad.es_pep = Convert.ToInt32(resultado["ES_PEP"]);

                            listaParentescos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listaParentescos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarParentescoDeUnaPersona", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para consultar familiar especifico de la persona

        public PersonaParentescos ConsultarParentescoDeUnaPersona(long idPersona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            PersonaParentescos PersonaFamiliar = new PersonaParentescos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from PERSONA_PARENTESCOS where CODIGOPERSONA = " + idPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            PersonaParentescos entidad = new PersonaParentescos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOPARENTESCO"] != DBNull.Value) entidad.codigoparentesco = Convert.ToInt64(resultado["CODIGOPARENTESCO"]);
                            if (resultado["CODIGOTIPOIDENTIFICACION"] != DBNull.Value) entidad.codigotipoidentificacion = Convert.ToInt64(resultado["CODIGOTIPOIDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRESAPELLIDOS"] != DBNull.Value) entidad.nombresapellidos = Convert.ToString(resultado["NOMBRESAPELLIDOS"]);
                            if (resultado["CODIGOACTIVIDADNEGOCIO"] != DBNull.Value) entidad.codigoactividadnegocio = Convert.ToString(resultado["CODIGOACTIVIDADNEGOCIO"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["CARGO"] != DBNull.Value) entidad.cargo = Convert.ToString(resultado["CARGO"]);
                            if (resultado["INGRESOMENSUAL"] != DBNull.Value) entidad.ingresomensual = Convert.ToDecimal(resultado["INGRESOMENSUAL"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return PersonaFamiliar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarParentescoDeUnaPersona", ex);
                        return null;
                    }
                }
            }
        }

        public PersonaParentescos ModificarPersonaParentesco(PersonaParentescos parentesco, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = parentesco.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = parentesco.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodigoparentesco.ParameterName = "p_codigoparentesco";
                        pcodigoparentesco.Value = parentesco.codigoparentesco;
                        pcodigoparentesco.Direction = ParameterDirection.Input;
                        pcodigoparentesco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoparentesco);

                        DbParameter pcodigotipoidentificacion = cmdTransaccionFactory.CreateParameter();
                        pcodigotipoidentificacion.ParameterName = "p_codigotipoidentificacion";
                        pcodigotipoidentificacion.Value = parentesco.codigotipoidentificacion;
                        pcodigotipoidentificacion.Direction = ParameterDirection.Input;
                        pcodigotipoidentificacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipoidentificacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = parentesco.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombresapellidos = cmdTransaccionFactory.CreateParameter();
                        pnombresapellidos.ParameterName = "p_nombresapellidos";
                        pnombresapellidos.Value = parentesco.nombresapellidos;
                        pnombresapellidos.Direction = ParameterDirection.Input;
                        pnombresapellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombresapellidos);

                        DbParameter pcodigoactividadnegocio = cmdTransaccionFactory.CreateParameter();
                        pcodigoactividadnegocio.ParameterName = "p_codigoactividadnegocio";
                        if (parentesco.codigoactividadnegocio == null)
                            pcodigoactividadnegocio.Value = DBNull.Value;
                        else
                            pcodigoactividadnegocio.Value = parentesco.codigoactividadnegocio;
                        pcodigoactividadnegocio.Direction = ParameterDirection.Input;
                        pcodigoactividadnegocio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigoactividadnegocio);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (parentesco.empresa == null)
                            pempresa.Value = DBNull.Value;
                        else
                            pempresa.Value = parentesco.empresa;
                        pempresa.Direction = ParameterDirection.Input;
                        pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pcargo = cmdTransaccionFactory.CreateParameter();
                        pcargo.ParameterName = "p_cargo";
                        if (parentesco.cargo == null)
                            pcargo.Value = DBNull.Value;
                        else
                            pcargo.Value = parentesco.cargo;
                        pcargo.Direction = ParameterDirection.Input;
                        pcargo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcargo);

                        DbParameter pingresomensual = cmdTransaccionFactory.CreateParameter();
                        pingresomensual.ParameterName = "p_ingresomensual";
                        if (parentesco.ingresomensual == null)
                            pingresomensual.Value = DBNull.Value;
                        else
                            pingresomensual.Value = parentesco.ingresomensual;
                        pingresomensual.Direction = ParameterDirection.Input;
                        pingresomensual.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pingresomensual);

                        DbParameter p_empleado_entidad = cmdTransaccionFactory.CreateParameter();
                        p_empleado_entidad.ParameterName = "p_empleado_entidad";
                        p_empleado_entidad.Value = parentesco.empleado_entidad;
                        p_empleado_entidad.Direction = ParameterDirection.Input;
                        p_empleado_entidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_empleado_entidad);

                        DbParameter p_miembro_administracion = cmdTransaccionFactory.CreateParameter();
                        p_miembro_administracion.ParameterName = "p_miembro_administracion";
                        p_miembro_administracion.Value = parentesco.miembro_administracion;
                        p_miembro_administracion.Direction = ParameterDirection.Input;
                        p_miembro_administracion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_administracion);

                        DbParameter p_miembro_control = cmdTransaccionFactory.CreateParameter();
                        p_miembro_control.ParameterName = "p_miembro_control";
                        p_miembro_control.Value = parentesco.miembro_control;
                        p_miembro_control.Direction = ParameterDirection.Input;
                        p_miembro_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_control);

                        DbParameter p_es_pep = cmdTransaccionFactory.CreateParameter();
                        p_es_pep.ParameterName = "p_es_pep";
                        p_es_pep.Value = parentesco.es_pep;
                        p_es_pep.Direction = ParameterDirection.Input;
                        p_es_pep.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_es_pep);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_PA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return parentesco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarPersonaParentesco", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarPersonaParentesco(long consecutivoParaBorrar, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = consecutivoParaBorrar;
                        p_consecutivo.Direction = ParameterDirection.Input;
                        p_consecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_PA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "EliminarPersonaParentesco", ex);
                    }
                }
            }
        }

        public Afiliacion crearcausacionafiliacion(Xpinn.Tesoreria.Entities.Operacion pope, Afiliacion pAfiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcauafilia = cmdTransaccionFactory.CreateParameter();
                        pcauafilia.ParameterName = "P_IDCAUAFILIA";
                        pcauafilia.Value = pAfiliacion.causacionafiliacion;
                        pcauafilia.Direction = ParameterDirection.Input;
                        pcauafilia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcauafilia);

                        DbParameter pidafiliacion = cmdTransaccionFactory.CreateParameter();
                        pidafiliacion.ParameterName = "p_idafiliacion";
                        pidafiliacion.Value = pAfiliacion.idafiliacion;
                        pidafiliacion.Direction = ParameterDirection.Input;
                        pidafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidafiliacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAfiliacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "P_COD_OPE";
                        pcod_ope.Value = pope.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "P_VALOR_CAUSADO";
                        if (pAfiliacion.valor != 0) pvalor.Value = pAfiliacion.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CAUSACION__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        pAfiliacion.idafiliacion = Convert.ToInt64(pidafiliacion.Value);
                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "CrearPersonaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public Afiliacion ModificarPersonaAfiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Afiliacion entidadAnterior = ConsultarAfiliacion(pAfiliacion.idafiliacion, vUsuario);

                        DbParameter pidafiliacion = cmdTransaccionFactory.CreateParameter();
                        pidafiliacion.ParameterName = "p_idafiliacion";
                        pidafiliacion.Value = pAfiliacion.idafiliacion;
                        pidafiliacion.Direction = ParameterDirection.Input;
                        pidafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidafiliacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAfiliacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_afiliacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_afiliacion.ParameterName = "p_fecha_afiliacion";
                        pfecha_afiliacion.Value = pAfiliacion.fecha_afiliacion;
                        pfecha_afiliacion.Direction = ParameterDirection.Input;
                        pfecha_afiliacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_afiliacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAfiliacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pAfiliacion.fecha_retiro != DateTime.MinValue && pAfiliacion.fecha_retiro != null) pfecha_retiro.Value = pAfiliacion.fecha_retiro; else pfecha_retiro.Value = DBNull.Value;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAfiliacion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_primer_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_primer_pago.ParameterName = "p_fecha_primer_pago";
                        if (pAfiliacion.fecha_primer_pago != null) pfecha_primer_pago.Value = pAfiliacion.fecha_primer_pago; else pfecha_primer_pago.Value = DBNull.Value;
                        pfecha_primer_pago.Direction = ParameterDirection.Input;
                        pfecha_primer_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primer_pago);

                        DbParameter pcuotas = cmdTransaccionFactory.CreateParameter();
                        pcuotas.ParameterName = "p_cuotas";
                        pcuotas.Value = pAfiliacion.cuotas;
                        pcuotas.Direction = ParameterDirection.Input;
                        pcuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pAfiliacion.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAfiliacion.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pAfiliacion.empresa_formapago != 0 && pAfiliacion.empresa_formapago != null) pcod_empresa.Value = pAfiliacion.empresa_formapago; else pcod_empresa.Value = DBNull.Value;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pasist_ultasamblea = cmdTransaccionFactory.CreateParameter();
                        pasist_ultasamblea.ParameterName = "p_asist_ultasamblea";
                        pasist_ultasamblea.Value = pAfiliacion.asist_ultasamblea;
                        pasist_ultasamblea.Direction = ParameterDirection.Input;
                        pasist_ultasamblea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasist_ultasamblea);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pAfiliacion.cod_asesor.HasValue && pAfiliacion.cod_asesor != 0)
                        {
                            p_cod_asesor.Value = pAfiliacion.cod_asesor;
                        }
                        else
                        {
                            p_cod_asesor.Value = DBNull.Value;
                        }

                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        DbParameter pcod_asociado_especial = cmdTransaccionFactory.CreateParameter();
                        pcod_asociado_especial.ParameterName = "p_cod_asociado_especial";
                        pcod_asociado_especial.Value = pAfiliacion.cod_asociado_especial.HasValue ? pAfiliacion.cod_asociado_especial : (object)DBNull.Value;
                        pcod_asociado_especial.Direction = ParameterDirection.Input;
                        pcod_asociado_especial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asociado_especial);

                        DbParameter p_es_peps = cmdTransaccionFactory.CreateParameter();
                        p_es_peps.ParameterName = "p_es_peps";
                        p_es_peps.Value = (pAfiliacion.Es_PEPS) ? 1 : 0;
                        p_es_peps.Direction = ParameterDirection.Input;
                        p_es_peps.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_es_peps);

                        DbParameter p_cargo_peps = cmdTransaccionFactory.CreateParameter();
                        p_cargo_peps.ParameterName = "p_cargo_peps";
                        if (!string.IsNullOrEmpty(pAfiliacion.cargo_PEPS)) p_cargo_peps.Value = pAfiliacion.cargo_PEPS; else p_cargo_peps.Value = DBNull.Value;
                        if (pAfiliacion.cargo_PEPS == null || pAfiliacion.cargo_PEPS == "") p_cargo_peps.Value = DBNull.Value;
                        else p_cargo_peps.Value = pAfiliacion.cargo_PEPS;
                        p_cargo_peps.Direction = ParameterDirection.Input;
                        p_cargo_peps.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cargo_peps);

                        DbParameter p_fecha_vinculacion_peps = cmdTransaccionFactory.CreateParameter();
                        p_fecha_vinculacion_peps.ParameterName = "p_fecha_vinculacion_peps";
                        if (pAfiliacion.fecha_vinculacion_PEPS == null) p_fecha_vinculacion_peps.Value = DateTime.MinValue; else p_fecha_vinculacion_peps.Value = pAfiliacion.fecha_vinculacion_PEPS;
                        p_fecha_vinculacion_peps.Direction = ParameterDirection.Input;
                        p_fecha_vinculacion_peps.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_vinculacion_peps);

                        DbParameter p_fecha_desvinculacion_peps = cmdTransaccionFactory.CreateParameter();
                        p_fecha_desvinculacion_peps.ParameterName = "p_fecha_desvinculacion_peps";
                        if (pAfiliacion.fecha_desvinculacion_PEPS == null) p_fecha_desvinculacion_peps.Value = DateTime.MinValue; else p_fecha_desvinculacion_peps.Value = pAfiliacion.fecha_desvinculacion_PEPS;
                        p_fecha_desvinculacion_peps.Direction = ParameterDirection.Input;
                        p_fecha_desvinculacion_peps.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_desvinculacion_peps);

                        DbParameter p_administra_recursos_publicos = cmdTransaccionFactory.CreateParameter();
                        p_administra_recursos_publicos.ParameterName = "p_administra_recursos_publicos";
                        p_administra_recursos_publicos.Value = (pAfiliacion.Administra_recursos_publicos) ? 1 : 0;
                        p_administra_recursos_publicos.Direction = ParameterDirection.Input;
                        p_administra_recursos_publicos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_administra_recursos_publicos);

                        DbParameter p_num_asistencias = cmdTransaccionFactory.CreateParameter();
                        p_num_asistencias.ParameterName = "p_num_asistencias";
                        p_num_asistencias.Value = pAfiliacion.numero_asistencias;
                        p_num_asistencias.Direction = ParameterDirection.Input;
                        p_num_asistencias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_num_asistencias);

                        DbParameter p_institucion = cmdTransaccionFactory.CreateParameter();
                        p_institucion.ParameterName = "p_institucion";
                        if (!string.IsNullOrEmpty(pAfiliacion.institucion)) p_institucion.Value = pAfiliacion.institucion; else p_institucion.Value = DBNull.Value;
                        p_institucion.Direction = ParameterDirection.Input;
                        p_institucion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_institucion);

                        DbParameter p_entidad_externa = cmdTransaccionFactory.CreateParameter();
                        p_entidad_externa.ParameterName = "p_entidad_externa";
                        if (!string.IsNullOrEmpty(pAfiliacion.entidad_externa)) p_entidad_externa.Value = pAfiliacion.entidad_externa; else p_entidad_externa.Value = DBNull.Value;
                        p_entidad_externa.Direction = ParameterDirection.Input;
                        p_entidad_externa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_entidad_externa);

                        DbParameter p_cargo_directivo = cmdTransaccionFactory.CreateParameter();
                        p_cargo_directivo.ParameterName = "p_cargo_directivo";
                        if (!string.IsNullOrEmpty(pAfiliacion.cargo_directivo)) p_cargo_directivo.Value = pAfiliacion.cargo_directivo; else p_cargo_directivo.Value = DBNull.Value;
                        p_cargo_directivo.Direction = ParameterDirection.Input;
                        p_cargo_directivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cargo_directivo);

                        DbParameter p_miembro_administracion = cmdTransaccionFactory.CreateParameter();
                        p_miembro_administracion.ParameterName = "p_miembro_administracion";
                        if (pAfiliacion.Miembro_administracion) p_miembro_administracion.Value = 1; else p_miembro_administracion.Value = 0;
                        p_miembro_administracion.Direction = ParameterDirection.Input;
                        p_miembro_administracion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_administracion);

                        DbParameter p_miembro_control = cmdTransaccionFactory.CreateParameter();
                        p_miembro_control.ParameterName = "p_miembro_control";
                        if (pAfiliacion.Miembro_control) p_miembro_control.Value = 1; else p_miembro_control.Value = 0;
                        p_miembro_control.Direction = ParameterDirection.Input;
                        p_miembro_control.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_miembro_control);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERAFILIAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        DAauditoria.InsertarLog(pAfiliacion, "persona_afiliacion", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Afiliaciones, "Modificacion de afiliacion para la persona con codigo " + pAfiliacion.cod_persona, entidadAnterior); //REGISTRO DE AUDITORIA

                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarPersonaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public Afiliacion ConsultarAfiliacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Afiliacion entidad = new Afiliacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT persona_afiliacion.*, motivo_cambio_estado.descripcion,p.fechacreacion FROM persona_afiliacion LEFT JOIN motivo_cambio_estado ON motivo_cambio_estado.cod_motivo = persona_afiliacion.cod_motivo join persona p on p.cod_persona=persona_afiliacion.cod_persona WHERE persona_afiliacion.cod_persona = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDAFILIACION"] != DBNull.Value) entidad.idafiliacion = Convert.ToInt64(resultado["IDAFILIACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_primer_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["CUOTAS"] != DBNull.Value) entidad.cuotas = Convert.ToInt32(resultado["CUOTAS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.empresa_formapago = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["ASIST_ULTASAMBLEA"] != DBNull.Value) entidad.asist_ultasamblea = Convert.ToInt32(resultado["ASIST_ULTASAMBLEA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["COD_ASOCIADO_ESPECIAL"] != DBNull.Value) entidad.cod_asociado_especial = Convert.ToInt64(resultado["COD_ASOCIADO_ESPECIAL"]);
                            if (resultado["ES_PEPS"] != DBNull.Value) entidad.Es_PEPS = Convert.ToBoolean(resultado["ES_PEPS"]);
                            if (resultado["CARGO_PEPS"] != DBNull.Value) entidad.cargo_PEPS = Convert.ToString(resultado["CARGO_PEPS"]);
                            if (resultado["FECHA_VINCULACION_PEPS"] != DBNull.Value) entidad.fecha_vinculacion_PEPS = Convert.ToDateTime(resultado["FECHA_VINCULACION_PEPS"]);
                            if (resultado["FECHA_DESVINCULACION_PEPS"] != DBNull.Value) entidad.fecha_desvinculacion_PEPS = Convert.ToDateTime(resultado["FECHA_DESVINCULACION_PEPS"]);
                            entidad.Administra_recursos_publicos = resultado["ADMINISTRA_RECURSOS_PUBLICOS"] != DBNull.Value ? Convert.ToBoolean(resultado["ADMINISTRA_RECURSOS_PUBLICOS"]) : false;
                            if (resultado["NUMERO_ASISTENCIAS"] != DBNull.Value) entidad.numero_asistencias = Convert.ToInt32(resultado["NUMERO_ASISTENCIAS"]);
                            if (resultado["INSTITUCION"] != DBNull.Value) entidad.institucion = Convert.ToString(resultado["INSTITUCION"]);
                            if (resultado["ENTIDAD_EXTERNA"] != DBNull.Value) entidad.entidad_externa = Convert.ToString(resultado["ENTIDAD_EXTERNA"]);
                            if (resultado["CARGO_DIRECTIVO"] != DBNull.Value) entidad.cargo_directivo = Convert.ToString(resultado["CARGO_DIRECTIVO"]);
                            //entidad.Miembro_administracion = resultado["MIEMBRO_ADMINISTRACION"] != DBNull.Value ? Convert.ToBoolean(resultado["MIEMBRO_ADMINISTRACION"]) : false;
                            if (resultado["MIEMBRO_ADMINISTRACION"] != DBNull.Value)
                                if (Convert.ToBoolean(resultado["MIEMBRO_ADMINISTRACION"])) entidad.Miembro_administracion = true; else entidad.Miembro_administracion = false;
                            //entidad.Miembro_control = resultado["MIEMBRO_CONTROL"] != DBNull.Value ? Convert.ToBoolean(resultado["MIEMBRO_CONTROL"]) : false;
                            if (resultado["MIEMBRO_CONTROL"] != DBNull.Value)
                                if (Convert.ToBoolean(resultado["MIEMBRO_CONTROL"])) entidad.Miembro_control = true; else entidad.Miembro_control = false;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicial = DateTime.Parse("1/" + pFechaCorte.Month + "/" + pFechaCorte.Year);

                        string sql = @"select p.IDENTIFICACION as ID_CLIENTE 
                        ,TO_CHAR(pa.FECHA_AFILIACION , 'YYYY-MM-DD') as Fecha_ingreso_entidad
                        ,case hp.sexo when 'M' then 1 when 'F' then 2 else 3 end as Género
                        ,p.ESTRATO AS NSE
                        ,ec.COD_GARANTIA as Estado_civil
                        ,case when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 20 then 1 --Menor a 20 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 30 then 2 --20-29 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 40 then 3 --30-39 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 50 then 4 --40-49 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 60 then 5 --50-59 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) > 60 then 6 end as Edad--60 años en adelante
                        ,al.cod_garantia as Actividad_laboral
                        ,tp.cod_garantia as Tipo_contrato_laboral
                        ,p.codactividad as Actividad_economica
                        ,ne.COD_GARANTIA as Nivel_educativo
                        ,case TIPOVIVIENDA when 'P' then 1 when 'A' then 2 when 'F' then 3 end as Tipo_vivienda
                        ,case when p.NUMPERSONASACARGO = 0 then 1
                        when p.NUMPERSONASACARGO = 1 then 2
                        when p.NUMPERSONASACARGO in(2 , 3) then 3
                        when p.NUMPERSONASACARGO > 3 then 4 end as Personas_a_cargo
                        ,SUBSTR(depres.CODCIUDAD , 1 , LENGTH(depres.CODCIUDAD) - 3 ) as Departamento_residencia --Eliminar los ceros adicionales
                        ,case LENGTH(ciures.codciudad) when 4 then '0' || ciures.codciudad else TO_CHAR(ciures.codciudad) end as Municipio_residencia --En caso de que el codigo no tenga 4 caracteres rellena con 0 al inicio
                        ,'' as Tipo_zona_residencia
                        ,pa.cod_asociado_especial as Asociado_Cliente_Especial
                        ,iie.SUELDO_PERSONA as Ingresos_fijos
                        ,'' as Frecuencia_Ingresos_fijos
                        ,'' as Formato_Ingresos_fijos
                        ,'' as Concepto_Ingresos_Fijos
                        ,'' as Ubicación_Ingresos_fijos
                        ,nvl(iie.HONORARIOS,0) + nvl(iie.ARRENDAMIENTO ,0) + nvl(iie.OTROS_INGRESOS,0) as Ingresos_variables
                        ,'' as Frecuencia_Ingresos_variables
                        ,'' as Formato_Ingresos_variables
                        ,'' as Concepto_Ingresos_variables
                        ,'' as Ubicación_Ingresos_Variables
                        ,nvl(iie.GASTOS_FAMILIARES,0) as Egresos_fijos
                        ,'' as Concepto_Egresos_Fijos
                        ,'' as Frecuencia_Egresos_Fijos
                        ,'' as Ubicación_Egresos_Fijos
                        ,nvl(iie.HIPOTECA,0) + nvl(iie.TRARGETACRED,0) + nvl(iie.OTROS_PRESTAMOS,0) + nvl(iie.DESCUENTOS_NOMINA,0) as Egresos_variables
                        ,'' as Concepto_Egresos_variables
                        ,'' as Frecuencia_Egresos_variables
                        ,'' as Ubicación_Egresos_Variables
                        ,nvl(Patrimonio.Total,0) as Valor_Patrimonio
                        from persona_afiliacion pa
                        inner join persona p on p.COD_PERSONA = pa.COD_PERSONA
                        inner join HISTORICO_PERSONA hp on hp.cod_persona = p.cod_persona and hp.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +
                        @"left join EstadoCivil ec on ec.CODESTADOCIVIL = hp.CODESTADOCIVIL
                        left join ActividadLaboral al on al.COD_ACTIVIDADLABORAL = hp.OCUPACION
                        left join TipoContrato tp on tp.CODTIPOCONTRATO = hp.CODTIPOCONTRATO
                        left join NivelEscolaridad ne on ne.CODESCOLARIDAD = hp.CODESCOLARIDAD
                        left join Ciudades ciures on ciures.CODCIUDAD = hp.CODCIUDADRESIDENCIA
                        left join Ciudades depres on depres.CODCIUDAD = ciures.DEPENDE_DE
                        left join INFORMACION_INGRE_EGRE iie on iie.cod_persona = hp.cod_persona
                        left join(
                            select COD_PERSONA, sum(VALOR_COMERCIAL) as Total from ACTIVOS_PERSONA
                            group by COD_PERSONA
                        )Patrimonio on Patrimonio.COD_PERSONA = hp.COD_PERSONA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAfiliados_GarantiasComunitarias", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? FechaInicioAfiliacion(DateTime pFechaDesc, Int64 pCodEmpresaDesc, Usuario vUsuario)
        {
            DateTime? pFechaInicioDesc;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = pFechaDesc;
                        pFecha.Direction = ParameterDirection.Input;
                        pFecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter pCodEmpresa = cmdTransaccionFactory.CreateParameter();
                        pCodEmpresa.ParameterName = "pCodEmpresa";
                        pCodEmpresa.Value = pCodEmpresaDesc;
                        pCodEmpresa.Direction = ParameterDirection.Input;
                        pCodEmpresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodEmpresa);

                        DbParameter pFechaInicio = cmdTransaccionFactory.CreateParameter();
                        pFechaInicio.ParameterName = "pFechaInicio";
                        pFechaInicio.Value = DBNull.Value;
                        pFechaInicio.Direction = ParameterDirection.Output;
                        pFechaInicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pFechaInicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_FECHAINICIO_AFI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pFechaInicioDesc = Convert.ToDateTime(pFechaInicio.Value);

                        return pFechaInicioDesc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "FechaInicioAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? FechaAfiliacion(string pCodPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Estado_Persona> lstEstado = new List<Estado_Persona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    DateTime? fecha_afiliacion = null;
                    try
                    {
                        string sql = @"Select Max(fecha_afiliacion) as FECHA_AFILIACION From persona_afiliacion Where cod_persona = " + pCodPersona + " And estado = 'A'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Estado_Persona entidad = new Estado_Persona();
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return fecha_afiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "FechaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Afiliacion> listarpersonaafiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Afiliacion> lstafiliacion = new List<Afiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT v_persona.identificacion, v_persona.nombres, persona_afiliacion.*, motivo_cambio_estado.descripcion FROM persona_afiliacion 
                                        INNER JOIN v_persona ON v_persona.cod_persona = persona_afiliacion.cod_persona 
                                        LEFT JOIN motivo_cambio_estado ON motivo_cambio_estado.cod_motivo = persona_afiliacion.cod_motivo 
                                        WHERE persona_afiliacion.estado='A' AND persona_afiliacion.idafiliacion NOT IN (Select idafiliacion From causacion_afiliacion cau) ";
                        Configuracion conf = new Configuracion();
                        DateTime fechaIni, fechaFin;
                        fechaIni = new DateTime(pAfiliacion.fecha_afiliacion.Year, pAfiliacion.fecha_afiliacion.Month, 1);
                        fechaFin = fechaIni.AddMonths(1);
                        fechaFin = fechaFin.AddDays(-1);
                        //fechaFin = new DateTime(pAfiliacion.fecha_afiliacion.Year, pAfiliacion.fecha_afiliacion.Month + 1, 1).AddDays(-1);
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " AND persona_afiliacion.fecha_afiliacion Between To_Date('" + fechaIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND To_Date('" + fechaFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += " AND persona_afiliacion.fecha_afiliacion Between '" + fechaIni.ToString(conf.ObtenerFormatoFecha()) + "' AND '" + fechaFin.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Afiliacion entidad = new Afiliacion();
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["IDAFILIACION"] != DBNull.Value) entidad.idafiliacion = Convert.ToInt64(resultado["IDAFILIACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PRIMER_PAGO"] != DBNull.Value) entidad.fecha_primer_pago = Convert.ToDateTime(resultado["FECHA_PRIMER_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["CUOTAS"] != DBNull.Value) entidad.cuotas = Convert.ToInt32(resultado["CUOTAS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.empresa_formapago = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["ASIST_ULTASAMBLEA"] != DBNull.Value) entidad.asist_ultasamblea = Convert.ToInt32(resultado["ASIST_ULTASAMBLEA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            lstafiliacion.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstafiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Afiliacion> ConsultarAportes(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Afiliacion entidad;
            List<Afiliacion> lstentidad = new List<Afiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select lineaaporte.nombre,aporte.*,CALCULAR_CUOTA_APORTE(CUOTA) as CUOTA_CALCULADA from aporte INNER JOIN lineaaporte on aporte.cod_linea_aporte=lineaaporte.cod_linea_aporte WHERE cod_persona = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            entidad = new Afiliacion();
                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_aporte = Convert.ToInt64(resultado["NUMERO_APORTE"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_aporte = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToString(resultado["CUOTA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstentidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAporte", ex);
                        return null;
                    }
                }
            }
        }

        //consulta para controlar la cantidad de realifialiciones permitidas
        public int ConsultarReafilPerm(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            int cantidad = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select count(*) as cantidad from Historico_afiliacion where COD_PERSONA = " + pAfiliacion.cod_persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["cantidad"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["cantidad"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return cantidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAporte", ex);
                        return cantidad;
                    }
                }
            }
        }

        public Afiliacion ModificarReafiliacion(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Afiliacion afiliacionAnterior = ConsultarAfiliacion(pAfiliacion.idafiliacion, vUsuario);

                        DbParameter pidafiliacion = cmdTransaccionFactory.CreateParameter();
                        pidafiliacion.ParameterName = "p_idafiliacion";
                        pidafiliacion.Value = pAfiliacion.idafiliacion;
                        pidafiliacion.Direction = ParameterDirection.Input;
                        pidafiliacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidafiliacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAfiliacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_afiliacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_afiliacion.ParameterName = "p_fecha_afiliacion";
                        pfecha_afiliacion.Value = pAfiliacion.fecha_afiliacion;
                        pfecha_afiliacion.Direction = ParameterDirection.Input;
                        pfecha_afiliacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_afiliacion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAfiliacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_retiro = cmdTransaccionFactory.CreateParameter();
                        pfecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pAfiliacion.fecha_retiro != DateTime.MinValue && pAfiliacion.fecha_retiro != null) pfecha_retiro.Value = pAfiliacion.fecha_retiro; else pfecha_retiro.Value = DBNull.Value;
                        pfecha_retiro.Direction = ParameterDirection.Input;
                        pfecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_retiro);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAfiliacion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_primer_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_primer_pago.ParameterName = "p_fecha_primer_pago";
                        if (pAfiliacion.fecha_primer_pago != null) pfecha_primer_pago.Value = pAfiliacion.fecha_primer_pago; else pfecha_primer_pago.Value = DBNull.Value;
                        pfecha_primer_pago.Direction = ParameterDirection.Input;
                        pfecha_primer_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primer_pago);

                        DbParameter pcuotas = cmdTransaccionFactory.CreateParameter();
                        pcuotas.ParameterName = "p_cuotas";
                        pcuotas.Value = pAfiliacion.cuotas;
                        pcuotas.Direction = ParameterDirection.Input;
                        pcuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pAfiliacion.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pAfiliacion.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "P_SALDO";
                        if (pAfiliacion.saldo == null) psaldo.Value = DBNull.Value; else psaldo.Value = pAfiliacion.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pAfiliacion.empresa_formapago != 0 && pAfiliacion.empresa_formapago != null) pcod_empresa.Value = pAfiliacion.empresa_formapago; else pcod_empresa.Value = DBNull.Value;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pfecha_prox_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_prox_pago.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        if (pAfiliacion.fecha_proximo_pago != null) pfecha_prox_pago.Value = pAfiliacion.fecha_proximo_pago; else pfecha_prox_pago.Value = DBNull.Value;
                        pfecha_prox_pago.Direction = ParameterDirection.Input;
                        pfecha_prox_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_prox_pago);

                        DbParameter pasist_ultasamblea = cmdTransaccionFactory.CreateParameter();
                        pasist_ultasamblea.ParameterName = "p_asist_ultasamblea";
                        if (pAfiliacion.asist_ultasamblea == null) pasist_ultasamblea.Value = DBNull.Value; else pasist_ultasamblea.Value = pAfiliacion.asist_ultasamblea;
                        pasist_ultasamblea.Direction = ParameterDirection.Input;
                        pasist_ultasamblea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasist_ultasamblea);

                        DbParameter pcod_motivo = cmdTransaccionFactory.CreateParameter();
                        pcod_motivo.ParameterName = "P_COD_MOTIVO";
                        if (pAfiliacion.cod_linea_aporte == null) pcod_motivo.Value = DBNull.Value; else pcod_motivo.Value = pAfiliacion.cod_linea_aporte;
                        pcod_motivo.Direction = ParameterDirection.Input;
                        pcod_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_motivo);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (pAfiliacion.cod_oficina == null) pcod_oficina.Value = DBNull.Value; else pcod_oficina.Value = pAfiliacion.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_REAFILIAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pAfiliacion, "persona_afiliacion", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Afiliaciones, "Modificacion de Re-afiliacion " + pAfiliacion.cod_persona, afiliacionAnterior); //REGISTRO DE AUDITORIA

                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarPersonaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public Afiliacion ModificarAportes(Afiliacion pAfiliacion, Usuario vUsuario)

        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMERO_APORTE = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_APORTE.ParameterName = "P_NUMERO_APORTE";
                        P_NUMERO_APORTE.Value = pAfiliacion.numero_aporte;
                        P_NUMERO_APORTE.Direction = ParameterDirection.Input;
                        P_NUMERO_APORTE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_APORTE);

                        DbParameter P_COD_LINEA_APORTE = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEA_APORTE.ParameterName = "P_COD_LINEA_APORTE";
                        P_COD_LINEA_APORTE.Value = pAfiliacion.cod_linea_aporte;
                        P_COD_LINEA_APORTE.Direction = ParameterDirection.Input;
                        P_COD_LINEA_APORTE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEA_APORTE);

                        DbParameter P_FECHA_APERTURA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_APERTURA.ParameterName = "P_FECHA_APERTURA";
                        P_FECHA_APERTURA.Value = pAfiliacion.fecha_apertura;
                        P_FECHA_APERTURA.Direction = ParameterDirection.Input;
                        P_FECHA_APERTURA.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_APERTURA);

                        DbParameter P_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_CUOTA.ParameterName = "P_CUOTA";
                        P_CUOTA.Value = pAfiliacion.cuotas;
                        P_CUOTA.Direction = ParameterDirection.Input;
                        P_CUOTA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CUOTA);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        if (pAfiliacion.forma_pago != null) P_FORMA_PAGO.Value = pAfiliacion.forma_pago; else P_FORMA_PAGO.Value = DBNull.Value;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        if (pAfiliacion.fecha_prox_pago != null) P_FECHA_PROXIMO_PAGO.Value = pAfiliacion.fecha_prox_pago; else P_FECHA_PROXIMO_PAGO.Value = DBNull.Value;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;
                        P_FECHA_PROXIMO_PAGO.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pAfiliacion.estados;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_COD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_USUARIO.ParameterName = "P_COD_USUARIO";
                        P_COD_USUARIO.Value = vUsuario.codusuario;
                        P_COD_USUARIO.Direction = ParameterDirection.Input;
                        P_COD_USUARIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_USUARIO);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (vUsuario.cod_oficina == 0)
                            pcod_oficina.Value = DBNull.Value;
                        else
                            pcod_oficina.Value = vUsuario.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_APORTE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarAportes", ex);
                        return null;
                    }
                }
            }
        }

        //Insertar el historico de la Reafiliacion
        public Afiliacion InsertarHistoReafili(Afiliacion pAfiliacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAfiliacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pAfiliacion.identificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);


                        DbParameter pfecha_afiliacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_afiliacion.ParameterName = "p_fecha_afiliacion";
                        if (pAfiliacion.fecha_ultima_afiliacion != null) pfecha_afiliacion.Value = pAfiliacion.fecha_ultima_afiliacion; else pfecha_afiliacion.Value = DBNull.Value;
                        pfecha_afiliacion.Direction = ParameterDirection.Input;
                        pfecha_afiliacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_afiliacion);

                        DbParameter p_fecha_retiro = cmdTransaccionFactory.CreateParameter();
                        p_fecha_retiro.ParameterName = "p_fecha_retiro";
                        if (pAfiliacion.fecha_retiro != null) p_fecha_retiro.Value = pAfiliacion.fecha_retiro; else p_fecha_retiro.Value = DBNull.Value;
                        p_fecha_retiro.Direction = ParameterDirection.Input;
                        p_fecha_retiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_retiro);

                        DbParameter p_fecha_reafiliacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_reafiliacion.ParameterName = "p_fecha_reafiliacion";
                        if (pAfiliacion.fecha_afiliacion != null) p_fecha_reafiliacion.Value = pAfiliacion.fecha_afiliacion; else p_fecha_reafiliacion.Value = DBNull.Value;
                        p_fecha_reafiliacion.Direction = ParameterDirection.Input;
                        p_fecha_reafiliacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_reafiliacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_HIS_AFILIACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAfiliacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarPersonaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<Estado_Persona> ListarEstadoPersona(Estado_Persona pEstado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Estado_Persona> lstEstado = new List<Estado_Persona>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ESTADO_PERSONA " + ObtenerFiltro(pEstado) + " ORDER BY ESTADO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Estado_Persona entidad = new Estado_Persona();
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstEstado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarEstadoPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaActualizacion> ListarDataPersonasXactualizar(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaActualizacion> lstActualizar = new List<PersonaActualizacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.* ,C.NOMCIUDAD as nom_ciudadresid, CL.NOMCIUDAD as nom_ciudadLabo,v.identificacion, v.cod_nomina,
                                    a.snombre1 ||' '|| a.snombre2 ||' '|| a.sapellido1 ||' '|| a.sapellido2 as asesor
                                    FROM PERSONA_ACTUALIZACION P LEFT JOIN CIUDADES C ON P.CODCIUDADRESIDENCIA = C.CODCIUDAD 
                                    Left join CIUDADES cl on P.CIUDADEMPRESA = CL.CODCIUDAD 
                                    inner join v_persona v on v.cod_persona = p.cod_persona 
                                    left join asejecutivos a on v.cod_asesor=a.iusuario " + pFiltro + " ORDER BY P.IDCONSECUTIVO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        PersonaActualizacion entidad;
                        while (resultado.Read())
                        {
                            entidad = new PersonaActualizacion();
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToDecimal(resultado["IDCONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt32(resultado["CIUDADEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["nom_ciudadresid"] != DBNull.Value) entidad.nomciudadresidencia = Convert.ToString(resultado["nom_ciudadresid"]);
                            if (resultado["nom_ciudadLabo"] != DBNull.Value) entidad.nomciudadempresa = Convert.ToString(resultado["nom_ciudadLabo"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["asesor"]);
                            lstActualizar.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActualizar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarDataPersonasXactualizar", ex);
                        return null;
                    }
                }
            }
        }

        public List<TranAfiliacion> ListarMovAfiliacion(TranAfiliacion pTranAfiliacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TranAfiliacion> lstActualizar = new List<TranAfiliacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select t.*, o.fecha_oper, o.tipo_ope, o.num_comp, o.tipo_comp, p.descripcion As nomtipo_ope, c.descripcion As nomtipo_comp
                                        From Tran_Afiliacion t Left Join operacion o On t.cod_ope = o.cod_ope
                                        Left Join tipo_ope p On o.tipo_ope = p.tipo_ope
                                        Left Join tipo_comp c On o.tipo_comp = c.tipo_comp " + ObtenerFiltro(pTranAfiliacion, "t.");
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TranAfiliacion entidad = new TranAfiliacion();
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToInt64(resultado["NUMERO_TRANSACCION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["NOMTIPO_OPE"] != DBNull.Value) entidad.nomtipo_ope = Convert.ToString(resultado["NOMTIPO_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOMTIPO_COMP"] != DBNull.Value) entidad.nomtipo_comp = Convert.ToString(resultado["NOMTIPO_COMP"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDAFILIACION"] != DBNull.Value) entidad.idafiliacion = Convert.ToInt64(resultado["IDAFILIACION"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_DET_LIS"] != DBNull.Value) entidad.cod_det_lis = Convert.ToInt32(resultado["COD_DET_LIS"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstActualizar.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstActualizar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarMovAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        //PERSONA_RESPONSABLE
        public PersonaResponsable Crear_Mod_PersonaResponsable(PersonaResponsable pPersona, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pPersona.consecutivo;
                        if (pOpcion == 1)
                            pconsecutivo.Direction = ParameterDirection.Output;
                        else
                            pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersona.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_persona_tutor = cmdTransaccionFactory.CreateParameter();
                        pcod_persona_tutor.ParameterName = "p_cod_persona_tutor";
                        pcod_persona_tutor.Value = pPersona.cod_persona_tutor;
                        pcod_persona_tutor.Direction = ParameterDirection.Input;
                        pcod_persona_tutor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona_tutor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_RESPON_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_RESPON_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pPersona.consecutivo = Convert.ToInt64(pconsecutivo.Value);
                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaResponsableData", "Crear_Mod_PersonaResponsable", ex);
                        return null;
                    }
                }
            }
        }

        public bool Eliminar_PersonaResponsable(PersonaResponsable pPersona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersona.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_RESPON_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "Eliminar_PersonaResponsable", ex);
                        return false;
                    }
                }
            }
        }


        public PersonaResponsable ConsultarPersonaResponsable(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            PersonaResponsable entidad = new PersonaResponsable();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT R.CONSECUTIVO, R.COD_PERSONA, R.COD_PERSONA_TUTOR, V.IDENTIFICACION, V.TIPO_IDENTIFICACION, V.NOMBRE FROM PERSONA_RESPONSABLE R 
                                        LEFT JOIN V_PERSONA V ON V.COD_PERSONA = R.COD_PERSONA_TUTOR " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_PERSONA_TUTOR"] != DBNull.Value) entidad.cod_persona_tutor = Convert.ToInt64(resultado["COD_PERSONA_TUTOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_ter = Convert.ToString(resultado["NOMBRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarPersonaResponsable", ex);
                        return null;
                    }
                }
            }
        }

        public List<SolicitudPersonaAfi> ListarDataSolicitudAfiliacion(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SolicitudPersonaAfi> lstSolicitud = new List<SolicitudPersonaAfi>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.* ,T.DESCRIPCION, C.NOMCIUDAD as nom_ciudadresid, CL.NOMCIUDAD as nom_ciudadLabo
                                        FROM SOLICITUD_PERSONA_AFI S 
                                        LEFT JOIN TIPOIDENTIFICACION T ON T.CODTIPOIDENTIFICACION = S.TIPO_IDENTIFICACION
                                        LEFT JOIN CIUDADES C ON S.CIUDAD = C.CODCIUDAD 
                                        LEFT JOIN CIUDADES CL ON S.CIUDAD_EMPRESA = CL.CODCIUDAD " + pFiltro + " ORDER BY S.ID_PERSONA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SolicitudPersonaAfi entidad = new SolicitudPersonaAfi();
                            if (resultado["ID_PERSONA"] != DBNull.Value) entidad.id_persona = Convert.ToInt64(resultado["ID_PERSONA"]);
                            if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_EXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHA_EXPEDICION"]);
                            if (resultado["CIUDAD_EXPEDICION"] != DBNull.Value) entidad.ciudad_expedicion = Convert.ToInt64(resultado["CIUDAD_EXPEDICION"]);
                            if (resultado["FECHA_NACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHA_NACIMIENTO"]);
                            if (resultado["CIUDAD_NACIMIENTO"] != DBNull.Value) entidad.ciudad_nacimiento = Convert.ToInt64(resultado["CIUDAD_NACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt32(resultado["CODESCOLARIDAD"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToInt64(resultado["BARRIO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToInt64(resultado["DEPARTAMENTO"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTADO_EMPRESA"] != DBNull.Value) entidad.estado_empresa = Convert.ToString(resultado["ESTADO_EMPRESA"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["CIUDAD_EMPRESA"] != DBNull.Value) entidad.ciudad_empresa = Convert.ToInt64(resultado["CIUDAD_EMPRESA"]);
                            if (resultado["DEPARTAMENTO_EMPRESA"] != DBNull.Value) entidad.departamento_empresa = Convert.ToInt64(resultado["DEPARTAMENTO_EMPRESA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["COD_PERIODICIDAD_PAGO"] != DBNull.Value) entidad.cod_periodicidad_pago = Convert.ToString(resultado["COD_PERIODICIDAD_PAGO"]);

                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_identificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_CIUDADRESID"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["NOM_CIUDADRESID"]);
                            if (resultado["NOM_CIUDADLABO"] != DBNull.Value) entidad.nom_ciudadempresa = Convert.ToString(resultado["NOM_CIUDADLABO"]);
                            lstSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarDataSolicitudAfiliacion", ex);
                        return null;
                    }
                }
            }
        }
        public ParametrizacionProcesoAfiliacion consultarTextoCorreo(Int32 cod_proceso, Int64 cod_per, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametrizacionProcesoAfiliacion cuerpoCorreo = new ParametrizacionProcesoAfiliacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"
                            SELECT P.PRIMER_NOMBRE||' '||P.SEGUNDO_NOMBRE||' '||P.PRIMER_APELLIDO||' '||P.SEGUNDO_APELLIDO AS NOMBRE_ASOCIADO,
                            A.SNOMBRE1||' '||A.SNOMBRE2||' '||A.SAPELLIDO1||' '||A.SAPELLIDO2 AS NOMBRE_EJECUTIVO, P.EMAIL AS EMAIL_ASOCIADO, A.SEMAIL AS EMAIL_ASESOR, R.CORREO 
                            FROM PARAMETRIZACION_AFILICACION R
                            INNER JOIN PERSONA P ON P.COD_PERSONA = " + cod_per + @"
                            LEFT JOIN PERSONA_AFILIACION F ON P.COD_PERSONA = F.COD_PERSONA
                            LEFT JOIN ASEJECUTIVOS A ON F.COD_ASESOR = A.ICODIGO
                            WHERE R.COD_PROCESO = " + cod_proceso;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NOMBRE_ASOCIADO"] != DBNull.Value) cuerpoCorreo.nombre_asociado = Convert.ToString(resultado["NOMBRE_ASOCIADO"]);
                            if (resultado["NOMBRE_EJECUTIVO"] != DBNull.Value) cuerpoCorreo.nombre_asesor = Convert.ToString(resultado["NOMBRE_EJECUTIVO"]);
                            if (resultado["EMAIL_ASOCIADO"] != DBNull.Value) cuerpoCorreo.email_asociado = Convert.ToString(resultado["EMAIL_ASOCIADO"]);
                            if (resultado["EMAIL_ASESOR"] != DBNull.Value) cuerpoCorreo.email_asesor = Convert.ToString(resultado["EMAIL_ASESOR"]);
                            if (resultado["CORREO"] != DBNull.Value) cuerpoCorreo.correo = Convert.ToString(resultado["CORREO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cuerpoCorreo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "consultarTextoCorreo", ex);
                        return null;
                    }
                }
            }
        }
        public ParametrizacionProcesoAfiliacion controlRutaAfiliacion(ParametrizacionProcesoAfiliacion control, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_control = cmdTransaccionFactory.CreateParameter();
                        pid_control.ParameterName = "P_ID_CONTROL";
                        pid_control.Value = 0;
                        pid_control.Direction = ParameterDirection.Input;
                        pid_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_control);

                        DbParameter p_numero_solicitud = cmdTransaccionFactory.CreateParameter();
                        p_numero_solicitud.ParameterName = "P_NUM_SOLICITUD";
                        p_numero_solicitud.Value = control.numero_solicitud;
                        p_numero_solicitud.Direction = ParameterDirection.Input;
                        p_numero_solicitud.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numero_solicitud);

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "P_IDENTIFICACION";
                        p_identificacion.Value = control.identificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "P_COD_PERSONA";
                        p_cod_persona.Value = control.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_ip_local = cmdTransaccionFactory.CreateParameter();
                        p_ip_local.ParameterName = "P_IP_MAQUINA";
                        p_ip_local.Value = control.ip_local;
                        p_ip_local.Direction = ParameterDirection.Input;
                        p_ip_local.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_ip_local);

                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "P_COD_PROCESO";
                        p_cod_proceso.Value = control.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CTRL_AFI_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return control;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
        public Int64 ConfirmacionSolicitudAfiliacion(SolicitudPersonaAfi pPersona, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_persona = cmdTransaccionFactory.CreateParameter();
                        pid_persona.ParameterName = "p_id_persona";
                        pid_persona.Value = pPersona.id_persona;
                        pid_persona.Direction = ParameterDirection.Input;
                        pid_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_persona);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_AFILIACION_CONFI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPersona.id_persona;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return 0;
                    }
                }
            }
        }
        public Int64 consultarCodigoPersona(Int64 cod, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 resultData = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_PERSONA FROM PERSONA WHERE IDENTIFICACION = (SELECT IDENTIFICACION FROM SOLICITUD_PERSONA_AFI WHERE ID_PERSONA = " + cod + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value)
                                resultData = Convert.ToInt64(resultado["cod_persona"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "consultarCodigoPersona", ex);
                        return 0;
                    }
                }
            }
        }
        public void EliminarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pid_persona = cmdTransaccionFactory.CreateParameter();
                        pid_persona.ParameterName = "p_id_persona";
                        pid_persona.Value = pId;
                        pid_persona.Direction = ParameterDirection.Input;
                        pid_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_SOLIAFILIA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "EliminarSolicitudAfiliacion", ex);
                    }
                }
            }
        }

        public SolicitudPersonaAfi ConsultarSolicitudAfiliacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudPersonaAfi entidad = new SolicitudPersonaAfi(); ;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.* ,T.DESCRIPCION, E.NOMCIUDAD AS NOM_CIUDADEXP, C.NOMCIUDAD AS NOM_CIUDADRESID, C.NOMCIUDAD AS NOM_CIUDADNACI, CL.NOMCIUDAD AS NOM_CIUDADLABO,
                                        EC.DESCRIPCION AS NOM_ESTADOCIVIL, X.DESCRIPCION AS NOM_ESCOLARIDAD, B.NOMBRE AS NOM_BARRIO,TC.DESCRIPCION AS NOM_TIPOCONTRATO
                                        FROM SOLICITUD_PERSONA_AFI S 
                                        LEFT JOIN TIPOIDENTIFICACION T ON T.CODTIPOIDENTIFICACION = S.TIPO_IDENTIFICACION
                                        LEFT JOIN CIUDADES E ON S.CIUDAD_EXPEDICION = E.CODCIUDAD 
                                        LEFT JOIN CIUDADES C ON S.CIUDAD = C.CODCIUDAD 
                                        LEFT JOIN CIUDADES N ON S.CIUDAD_NACIMIENTO = N.CODCIUDAD 
                                        LEFT JOIN CIUDADES CL ON S.CIUDAD_EMPRESA = CL.CODCIUDAD 
                                        LEFT JOIN ESTADOCIVIL EC ON EC.CODESTADOCIVIL = S.CODESTADOCIVIL
                                        LEFT JOIN NIVELESCOLARIDAD X ON X.CODESCOLARIDAD = S.CODESCOLARIDAD
                                        LEFT JOIN BARRIO B ON B.CODBARRIO = S.BARRIO
                                        LEFT JOIN TIPOCONTRATO TC ON TC.CODTIPOCONTRATO = S.CODTIPOCONTRATO
                                        WHERE S.ID_PERSONA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_PERSONA"] != DBNull.Value) entidad.id_persona = Convert.ToInt64(resultado["ID_PERSONA"]);
                            if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_EXPEDICION"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["FECHA_EXPEDICION"]);
                            if (resultado["CIUDAD_EXPEDICION"] != DBNull.Value) entidad.ciudad_expedicion = Convert.ToInt64(resultado["CIUDAD_EXPEDICION"]);
                            if (resultado["FECHA_NACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHA_NACIMIENTO"]);
                            if (resultado["CIUDAD_NACIMIENTO"] != DBNull.Value) entidad.ciudad_nacimiento = Convert.ToInt64(resultado["CIUDAD_NACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt32(resultado["CODESCOLARIDAD"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profesion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToInt64(resultado["BARRIO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["DEPARTAMENTO"] != DBNull.Value) entidad.departamento = Convert.ToInt64(resultado["DEPARTAMENTO"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ESTADO_EMPRESA"] != DBNull.Value) entidad.estado_empresa = Convert.ToString(resultado["ESTADO_EMPRESA"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["CIUDAD_EMPRESA"] != DBNull.Value) entidad.ciudad_empresa = Convert.ToInt64(resultado["CIUDAD_EMPRESA"]);
                            if (resultado["DEPARTAMENTO_EMPRESA"] != DBNull.Value) entidad.departamento_empresa = Convert.ToInt64(resultado["DEPARTAMENTO_EMPRESA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["COD_PERIODICIDAD_PAGO"] != DBNull.Value) entidad.cod_periodicidad_pago = Convert.ToString(resultado["COD_PERIODICIDAD_PAGO"]);
                            if (resultado["CABEZA_FAMILIA"] != DBNull.Value) entidad.cabeza_familia = Convert.ToInt32(resultado["CABEZA_FAMILIA"]);
                            if (resultado["PERSONAS_CARGO"] != DBNull.Value) entidad.personas_cargo = Convert.ToInt32(resultado["PERSONAS_CARGO"]);
                            if (resultado["CARGO_CONTACTO"] != DBNull.Value) entidad.cargo_contacto = Convert.ToString(resultado["CARGO_CONTACTO"]);

                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_identificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_CIUDADEXP"] != DBNull.Value) entidad.nom_ciudadExp = Convert.ToString(resultado["NOM_CIUDADEXP"]);
                            if (resultado["NOM_CIUDADNACI"] != DBNull.Value) entidad.nom_ciudadNaci = Convert.ToString(resultado["NOM_CIUDADNACI"]);
                            if (resultado["NOM_CIUDADRESID"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["NOM_CIUDADRESID"]);
                            if (resultado["NOM_CIUDADLABO"] != DBNull.Value) entidad.nom_ciudadempresa = Convert.ToString(resultado["NOM_CIUDADLABO"]);
                            if (resultado["NOM_ESTADOCIVIL"] != DBNull.Value) entidad.nom_estadoCivil = Convert.ToString(resultado["NOM_ESTADOCIVIL"]);
                            if (resultado["NOM_ESCOLARIDAD"] != DBNull.Value) entidad.nom_escolaridad = Convert.ToString(resultado["NOM_ESCOLARIDAD"]);
                            if (resultado["NOM_BARRIO"] != DBNull.Value) entidad.nom_barrio = Convert.ToString(resultado["NOM_BARRIO"]);
                            if (resultado["NOM_TIPOCONTRATO"] != DBNull.Value) entidad.nom_tipo_contrato = Convert.ToString(resultado["NOM_TIPOCONTRATO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarSolicitudAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Afiliacion> ListarReafiliaciones(Afiliacion pReafiliacion, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Afiliacion> lstReafiliaciones = new List<Afiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT
                                        H.IDENTIFICACION,
                                        P.PRIMER_APELLIDO||P.SEGUNDO_APELLIDO||P.PRIMER_NOMBRE||P.SEGUNDO_NOMBRE AS NOMBRES,
                                        nvl(ER.NOM_EMPRESA, 'NO SE REGISTRO EN LA REAFILIACION') AS PAGADURIA,
                                        MIN(H.FECHA_AFILIACION)AS FECHA_AFILIACION,
                                        MAX(H.FECHA_RETIRO)AS FECHA_RETIRO,
                                        MAX(H.FECHA_REAFILIACION) AS FECHA_REAFILIACION,
                                        COUNT(H.IDENTIFICACION) AS N_REAFILIACIONES
                                        FROM HISTORICO_AFILIACION H
                                        INNER JOIN PERSONA P ON H.COD_PERSONA = P.COD_PERSONA
                                        INNER JOIN PERSONA_AFILIACION PA ON P.COD_PERSONA = PA.COD_PERSONA
                                        LEFT JOIN EMPRESA_RECAUDO ER ON PA.COD_EMPRESA = ER.COD_EMPRESA
                                        WHERE FECHA_REAFILIACION BETWEEN " + filtro +
                                        "group by H.IDENTIFICACION, P.PRIMER_APELLIDO||P.SEGUNDO_APELLIDO||P.PRIMER_NOMBRE||P.SEGUNDO_NOMBRE, ER.NOM_EMPRESA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Afiliacion entidad = new Afiliacion();

                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["PAGADURIA"] != DBNull.Value) entidad.nombre_empresa_pagaduria = Convert.ToString(resultado["PAGADURIA"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);
                            if (resultado["FECHA_REAFILIACION"] != DBNull.Value) entidad.fecha_ultima_afiliacion = Convert.ToDateTime(resultado["FECHA_REAFILIACION"]);
                            if (resultado["N_REAFILIACIONES"] != DBNull.Value) entidad.Num_Reafiliaciones = Convert.ToInt32(resultado["N_REAFILIACIONES"]);

                            lstReafiliaciones.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReafiliaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarReafiliaciones", ex);
                        return null;
                    }
                }
            }
        }

        public int ConsultarCantidadAfiliados(string pCondicion, Usuario vUsuario)
        {
            DbDataReader resultado;
            int resultData = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select count(cod_persona) as Cantidad from V_PERSONASAFILIADAS ";
                        if (pCondicion != null && pCondicion != "")
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            sql += pCondicion;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado.GetValue(0) != DBNull.Value) resultData = resultado.GetInt32(0);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ConsultarCantidadAfiliados", ex);
                        return 0;
                    }
                }
            }
        }

        public List<ConsultarPersonaBasico> ListarPersonasAfiliadasPaginado(string pCondicion, int pIndicePagina, int pRegistrosPagina, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConsultarPersonaBasico> lstTercero = new List<ConsultarPersonaBasico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        int linicio = 0;
                        int lfin = 0;
                        linicio = pIndicePagina * pRegistrosPagina;
                        lfin = linicio + pRegistrosPagina;
                        sql = "select * from V_PERSONASAFILIADAS where nro > " + linicio + " and nro <= " + lfin;
                        if (pCondicion != null && pCondicion != "")
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            sql += pCondicion;
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        ConsultarPersonaBasico entidad;
                        CifradoBusiness SegCifrado = new CifradoBusiness();
                        while (resultado.Read())
                        {
                            entidad = new ConsultarPersonaBasico();
                            entidad.result = true;
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.genero = Convert.ToString(resultado["SEXO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.cod_zona = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["NOMCIUDADRESIDEN"] != DBNull.Value) entidad.nomciudadresidencia = Convert.ToString(resultado["NOMCIUDADRESIDEN"]);
                            if (resultado["NOMCIUDADEMPRESA"] != DBNull.Value) entidad.nomciudadempresa = Convert.ToString(resultado["NOMCIUDADEMPRESA"]);
                            if (resultado["CLAVE"] != DBNull.Value) entidad.clavesinencriptar = Convert.ToString(resultado["CLAVE"]);
                            if (entidad.clavesinencriptar != null)
                            {
                                entidad.clavesinencriptar = SegCifrado.Desencriptar(entidad.clavesinencriptar);
                            }
                            lstTercero.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarPersonasAfiliadasPaginado", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConsultarPersonaBasico> ListarPersonasOficinaVirtual(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConsultarPersonaBasico> lstTercero = new List<ConsultarPersonaBasico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select p.identificacion, A.Cod_Persona, P.Primer_Nombre, P.Segundo_Nombre, P.Primer_Apellido, P.Segundo_Apellido from persona_acceso a inner join persona p on p.cod_persona = a.cod_persona";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        ConsultarPersonaBasico entidad;
                        CifradoBusiness SegCifrado = new CifradoBusiness();
                        while (resultado.Read())
                        {
                            entidad = new ConsultarPersonaBasico();
                            entidad.result = true;
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (entidad.clavesinencriptar != null)
                            {
                                entidad.clavesinencriptar = SegCifrado.Desencriptar(entidad.clavesinencriptar);
                            }
                            lstTercero.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarPersonasAfiliadasPaginado", ex);
                        return null;
                    }
                }
            }
        }


        public LineaAporte ConsultarLineaObligatoria(Usuario vUsuario)
        {
            DbDataReader resultado;
            LineaAporte entidad = new LineaAporte(); ;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select COD_LINEA_APORTE, TIPO_APORTE, TIPO_CUOTA, PORCENTAJE_MINIMO, PORCENTAJE_MAXIMO 
                                        from LINEAAPORTE  where TIPO_APORTE = 1 and TIPO_CUOTA in(4,5)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["TIPO_APORTE"] != DBNull.Value) entidad.tipo_aporte = Convert.ToInt32(resultado["TIPO_APORTE"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["PORCENTAJE_MINIMO"] != DBNull.Value) entidad.porcentaje_minimo = Convert.ToDecimal(resultado["PORCENTAJE_MINIMO"]);
                            if (resultado["PORCENTAJE_MAXIMO"] != DBNull.Value) entidad.porcentaje_maximo = Convert.ToDecimal(resultado["PORCENTAJE_MAXIMO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarLineaObligatoria", ex);
                        return null;
                    }
                }
            }
        }


        public Afiliacion ConsultarCierrePersonas(Usuario vUsuario)
        {
            DbDataReader resultado;
            Afiliacion entidad = new Afiliacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(FECHA) as fecha,estado FROM CIEREA WHERE TIPO = 'P' AND ESTADO = 'D'   group by estado";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadocierre = Convert.ToString(resultado["ESTADO"]);

                        }
                        else
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<TipoInfAdicional> lstTipoInfAdicional(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoInfAdicional> lstTipInfAdicional = new List<TipoInfAdicional>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from TIPO_INFADICIONAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoInfAdicional entidad = new TipoInfAdicional();
                            if (resultado["COD_INFADICIONAL"] != DBNull.Value) entidad.COD_INFADICIONAL = Convert.ToInt64(resultado["COD_INFADICIONAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]).Replace(' ', '_');
                            lstTipInfAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipInfAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "lstTipoInfAdicional", ex);
                        return null;
                    }
                }
            }
        }
        public DataTable ConsultarAfiliados_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario, string TipInfAdicional)
        {
            DbDataReader resultado;

            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicial = DateTime.Parse("1/" + pFechaCorte.Month + "/" + pFechaCorte.Year);

                        string sql = @"select p.IDENTIFICACION as ID_CLIENTE 
                        ,TO_CHAR(pa.FECHA_AFILIACION , 'YYYY-MM-DD') as Fecha_ingreso_entidad
                        ,case hp.sexo when 'M' then 1 when 'F' then 2 else 3 end as Género
                        ,p.ESTRATO AS NSE
                        ,ec.COD_GARANTIA as Estado_civil
                        ,case when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 20 then 1 --Menor a 20 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 30 then 2 --20-29 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 40 then 3 --30-39 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 50 then 4 --40-49 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) < 60 then 5 --50-59 años
                             when (months_between(sysdate,p.FECHANACIMIENTO)/12) > 60 then 6 end as Edad--60 años en adelante
                        ,al.cod_garantia as Actividad_laboral
                        ,tp.DESCRIPCION as Tipo_contrato_laboral
                        ,p.codactividad as Actividad_economica
                        ,ne.COD_GARANTIA as Nivel_educativo
                        ,case TIPOVIVIENDA when 'P' then 1 when 'A' then 2 when 'F' then 3 end as Tipo_vivienda
                        ,case when p.NUMPERSONASACARGO = 0 then 1
                        when p.NUMPERSONASACARGO = 1 then 2
                        when p.NUMPERSONASACARGO in(2 , 3) then 3
                        when p.NUMPERSONASACARGO > 3 then 4 end as Personas_a_cargo
                        ,SUBSTR(depres.CODCIUDAD , 1 , LENGTH(depres.CODCIUDAD) - 3 ) as Departamento_residencia --Eliminar los ceros adicionales
                        ,case LENGTH(ciures.codciudad) when 4 then '0' || ciures.codciudad else TO_CHAR(ciures.codciudad) end as Municipio_residencia --En caso de que el codigo no tenga 4 caracteres rellena con 0 al inicio
                        ,'' as Tipo_zona_residencia
                        ,pa.cod_asociado_especial as Asociado_Cliente_Especial                   
                        ,iie.SUELDO_PERSONA as Ingresos_fijos            
                        ,nvl(iie.HONORARIOS,0) + nvl(iie.ARRENDAMIENTO ,0) + nvl(iie.OTROS_INGRESOS,0) as Ingresos_variables                     
                        ,nvl(iie.GASTOS_FAMILIARES,0) as Egresos_fijos
                        ,nvl(iie.HIPOTECA,0) + nvl(iie.TRARGETACRED,0) + nvl(iie.OTROS_PRESTAMOS,0) + nvl(iie.DESCUENTOS_NOMINA,0) as Egresos_variables              
                        ,nvl(Patrimonio.Total,0) as Valor_Patrimonio" + TipInfAdicional +
                        @" from persona_afiliacion pa
                        inner join persona p on p.COD_PERSONA = pa.COD_PERSONA
                       inner join HISTORICO_PERSONA hp on hp.cod_persona = p.cod_persona and hp.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +
                       @"  left join EstadoCivil ec on ec.CODESTADOCIVIL = hp.CODESTADOCIVIL
                        left join ActividadLaboral al on al.COD_ACTIVIDADLABORAL = hp.OCUPACION
                        left join TipoContrato tp on tp.CODTIPOCONTRATO = hp.CODTIPOCONTRATO
                        left join NivelEscolaridad ne on ne.CODESCOLARIDAD = hp.CODESCOLARIDAD
                        left join Ciudades ciures on ciures.CODCIUDAD = hp.CODCIUDADRESIDENCIA
                        left join Ciudades depres on depres.CODCIUDAD = ciures.DEPENDE_DE
                        left join INFORMACION_INGRE_EGRE iie on iie.cod_persona = hp.cod_persona
                        left join(
                            select COD_PERSONA, sum(VALOR_COMERCIAL) as Total from ACTIVOS_PERSONA
                            group by COD_PERSONA
                        )Patrimonio on Patrimonio.COD_PERSONA = hp.COD_PERSONA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader(), LoadOption.OverwriteChanges);


                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarAfiliados_GarantiasComunitarias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro de un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a registrar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion CrearProceso(ProcesoAfiliacion pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Output;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pProceso.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_oficina = cmdTransaccionFactory.CreateParameter();
                        p_cod_oficina.ParameterName = "p_cod_oficina";
                        if (pProceso.cod_oficina != 0)
                            p_cod_oficina.Value = pProceso.cod_oficina;
                        else
                            p_cod_oficina.Value = DBNull.Value;
                        p_cod_oficina.Direction = ParameterDirection.Input;
                        p_cod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_oficina);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha";
                        p_fecha.Value = pProceso.fecha;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_cod_usuario = cmdTransaccionFactory.CreateParameter();
                        p_cod_usuario.ParameterName = "p_cod_usuario";
                        p_cod_usuario.Value = pProceso.cod_usuario;
                        p_cod_usuario.Direction = ParameterDirection.Input;
                        p_cod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usuario);

                        DbParameter p_numero_acta = cmdTransaccionFactory.CreateParameter();
                        p_numero_acta.ParameterName = "p_numero_acta";
                        p_numero_acta.Value = pProceso.numero_acta;
                        p_numero_acta.Direction = ParameterDirection.Input;
                        p_numero_acta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_acta);

                        DbParameter p_concepto = cmdTransaccionFactory.CreateParameter();
                        p_concepto.ParameterName = "p_concepto";
                        p_concepto.Value = pProceso.concepto;
                        p_concepto.Direction = ParameterDirection.Input;
                        p_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_concepto);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = pProceso.observacion;
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        DbParameter p_tipo_proceso = cmdTransaccionFactory.CreateParameter();
                        p_tipo_proceso.ParameterName = "p_tipo_proceso";
                        p_tipo_proceso.Value = pProceso.tipo_proceso;
                        p_tipo_proceso.Direction = ParameterDirection.Input;
                        p_tipo_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_proceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PROCESOAFI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pProceso.cod_proceso = Convert.ToInt64(p_cod_proceso.Value);
                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "CrearProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de un proceso de afiliación 
        /// </summary>
        /// <param name="pProceso">Objeto a modificar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ModificarProceso(ProcesoAfiliacion pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;
                        p_cod_proceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pProceso.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_oficina = cmdTransaccionFactory.CreateParameter();
                        p_cod_oficina.ParameterName = "p_cod_oficina";
                        p_cod_oficina.Value = pProceso.cod_oficina;
                        p_cod_oficina.Direction = ParameterDirection.Input;
                        p_cod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_oficina);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha";
                        p_fecha.Value = pProceso.fecha;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_cod_usuario = cmdTransaccionFactory.CreateParameter();
                        p_cod_usuario.ParameterName = "p_cod_usuario";
                        p_cod_usuario.Value = pProceso.cod_usuario;
                        p_cod_usuario.Direction = ParameterDirection.Input;
                        p_cod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usuario);

                        DbParameter p_numero_acta = cmdTransaccionFactory.CreateParameter();
                        p_numero_acta.ParameterName = "p_numero_acta";
                        p_numero_acta.Value = pProceso.numero_acta;
                        p_numero_acta.Direction = ParameterDirection.Input;
                        p_numero_acta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_acta);

                        DbParameter p_concepto = cmdTransaccionFactory.CreateParameter();
                        p_concepto.ParameterName = "p_concepto";
                        p_concepto.Value = pProceso.concepto;
                        p_concepto.Direction = ParameterDirection.Input;
                        p_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_concepto);

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = pProceso.observacion;
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PROCESOAFI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ModificarProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar datos de procesos basado en un filtro
        /// </summary>
        /// <param name="filtro">Filtro del listado</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<ProcesoAfiliacion> ListarProcesos(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ProcesoAfiliacion> lstProcesos = new List<ProcesoAfiliacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.COD_PERSONA, CASE P.TIPO_PERSONA WHEN 'N' THEN 'NATURAL' WHEN 'J' THEN 'JURIDICA' ELSE NULL END AS TIPO_PERSONA, 
                                        T.DESCRIPCION AS TIPO_IDENTIFICACION, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.NOMBRE, O.NOMBRE AS OFICINA, A.FECHA, 
                                        U.NOMBRE AS USUARIO, CASE A.CONCEPTO WHEN 1 THEN 'FAVORABLE' WHEN 2 THEN 'DESFAVORABLE' ELSE NULL END AS TIPO_CONCEPTO
                                        FROM PROCESO_AFILIACION A 
                                        INNER JOIN V_PERSONA P ON A.COD_PERSONA = P.COD_PERSONA
                                        INNER JOIN TIPOIDENTIFICACION T ON P.TIPO_IDENTIFICACION = T.CODTIPOIDENTIFICACION
                                        LEFT JOIN USUARIOS U ON A.COD_USUARIO = U.CODUSUARIO
                                        LEFT JOIN OFICINA O ON A.COD_OFICINA = O.COD_OFICINA " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesoAfiliacion entidad = new ProcesoAfiliacion();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.lugar_proceso = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.nombre_usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToString(resultado["TIPO_CONCEPTO"]);

                            lstProcesos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ListarProcesos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar los datos de un proceso en especifico
        /// </summary>
        /// <param name="pProceso">Objeto para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public ProcesoAfiliacion ConsultarProceso(ProcesoAfiliacion pProceso, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProcesoAfiliacion entidad = new ProcesoAfiliacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROCESO_AFILIACION " + ObtenerFiltro(pProceso); ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if (resultado["NUMERO_ACTA"] != DBNull.Value) entidad.numero_acta = Convert.ToInt64(resultado["NUMERO_ACTA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt32(resultado["CONCEPTO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt32(resultado["TIPO_PROCESO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarProceso", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime ConsultarActualziacionDtos(long idPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            DateTime FechaActualizacion = new DateTime();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select Max(Fecha_Act) Fecha_Act from persona_act_datos WHere Cod_persona = " + idPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FECHA_ACT"] != DBNull.Value) FechaActualizacion = Convert.ToDateTime(resultado["FECHA_ACT"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return FechaActualizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AfiliacionData", "ConsultarActualziacionDtos", ex);
                        return Convert.ToDateTime("01/01/0001");
                    }
                }
            }
        }

    }
}
