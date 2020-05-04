using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TerceroS
    /// </summary>
    public class TerceroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TerceroS
        /// </summary>
        public TerceroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TerceroS de la base de datos
        /// </summary>
        /// <param name="pTercero">Entidad Tercero</param>
        /// <returns>Entidad Tercero creada</returns>
        public Tercero CrearTercero(Tercero pTercero, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTercero.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Output;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pTercero.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pTercero.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pdigito_verificacion = cmdTransaccionFactory.CreateParameter();
                        pdigito_verificacion.ParameterName = "p_digito_verificacion";
                        pdigito_verificacion.Value = pTercero.digito_verificacion;
                        pdigito_verificacion.Direction = ParameterDirection.Input;
                        pdigito_verificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdigito_verificacion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        ptipo_identificacion.Value = pTercero.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        ptipo_identificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pfechaexpedicion = cmdTransaccionFactory.CreateParameter();
                        pfechaexpedicion.ParameterName = "p_fechaexpedicion";
                        pfechaexpedicion.Value = pTercero.fechaexpedicion;
                        pfechaexpedicion.Direction = ParameterDirection.Input;
                        pfechaexpedicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaexpedicion);

                        DbParameter pcodciudadexpedicion = cmdTransaccionFactory.CreateParameter();
                        pcodciudadexpedicion.ParameterName = "p_codciudadexpedicion";
                        pcodciudadexpedicion.Value = pTercero.codciudadexpedicion;
                        pcodciudadexpedicion.Direction = ParameterDirection.Input;
                        pcodciudadexpedicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadexpedicion);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        pprimer_apellido.Value = pTercero.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter prazon_social = cmdTransaccionFactory.CreateParameter();
                        prazon_social.ParameterName = "p_razon_social";
                        prazon_social.Value = pTercero.razon_social;
                        prazon_social.Direction = ParameterDirection.Input;
                        prazon_social.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social);

                        DbParameter pcodactividadeco = cmdTransaccionFactory.CreateParameter();
                        pcodactividadeco.ParameterName = "P_CODACTIVIDADECONOMICA";
                        if (string.IsNullOrEmpty(pTercero.ActividadEconomicaEmpresaStr))
                            pcodactividadeco.Value = DBNull.Value;
                        else
                            pcodactividadeco.Value = pTercero.ActividadEconomicaEmpresaStr;
                        pcodactividadeco.Direction = ParameterDirection.Input;
                        pcodactividadeco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodactividadeco);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pTercero.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        ptelefono.Value = pTercero.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        pemail.Value = pTercero.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pTercero.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTercero.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pregimen = cmdTransaccionFactory.CreateParameter();
                        pregimen.ParameterName = "p_regimen";
                        pregimen.Value = pTercero.regimen;
                        pregimen.Direction = ParameterDirection.Input;
                        pregimen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pregimen);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pTercero.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pTercero.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pTercero.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pTercero.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pTipo_Acto_Creacion = cmdTransaccionFactory.CreateParameter();
                        pTipo_Acto_Creacion.ParameterName = "p_Tipo_Acto_Creacion";
                        if (pTercero.tipo_acto_creacion != 0) pTipo_Acto_Creacion.Value = pTercero.tipo_acto_creacion; else pTipo_Acto_Creacion.Value = DBNull.Value;
                        pTipo_Acto_Creacion.Direction = ParameterDirection.Input;
                        pTipo_Acto_Creacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pTipo_Acto_Creacion);

                        DbParameter pnum_acto_creacion = cmdTransaccionFactory.CreateParameter();
                        pnum_acto_creacion.ParameterName = "P_Num_Acto_Creacion";
                        if (pTercero.num_acto_creacion != null) pnum_acto_creacion.Value = pTercero.num_acto_creacion; else pnum_acto_creacion.Value = DBNull.Value;
                        pnum_acto_creacion.Direction = ParameterDirection.Input;
                        pnum_acto_creacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_acto_creacion);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (pTercero.celular != null) pcelular.Value = pTercero.celular; else pcelular.Value = DBNull.Value;
                        pcelular.Direction = ParameterDirection.Input;
                        pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pTercero.fechanacimiento == DateTime.MinValue || pTercero.fechanacimiento == null)
                            pfechanacimiento.Value = DBNull.Value;
                        else
                            pfechanacimiento.Value = pTercero.fechanacimiento;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        pfechanacimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        DbParameter pcamara_comercio = cmdTransaccionFactory.CreateParameter();
                        pcamara_comercio.ParameterName = "p_camara_comercio";
                        if (pTercero.camara_comercio == null)
                            pcamara_comercio.Value = DBNull.Value;
                        else
                            pcamara_comercio.Value = pTercero.camara_comercio;
                        pcamara_comercio.Direction = ParameterDirection.Input;
                        pcamara_comercio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcamara_comercio);

                        DbParameter pcod_representante = cmdTransaccionFactory.CreateParameter();
                        pcod_representante.ParameterName = "p_representante";
                        if (pTercero.cod_representante == 0)
                            pcod_representante.Value = DBNull.Value;
                        else
                            pcod_representante.Value = pTercero.cod_representante;
                        pcod_representante.Direction = ParameterDirection.Input;
                        pcod_representante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_representante);

                        DbParameter pcodactividad = cmdTransaccionFactory.CreateParameter();
                        pcodactividad.ParameterName = "P_CODACTIVIDAD";
                        if (string.IsNullOrEmpty(pTercero.codactividadStr))
                            pcodactividad.Value = DBNull.Value;
                        else
                            pcodactividad.Value = pTercero.codactividadStr;
                        pcodactividad.Direction = ParameterDirection.Input;
                        pcodactividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodactividad);

                        DbParameter penteterritorial = cmdTransaccionFactory.CreateParameter();
                        penteterritorial.ParameterName = "P_ENTE_TERRITORIAL";
                        penteterritorial.Value = pTercero.EnteTerritorial;
                        penteterritorial.Direction = ParameterDirection.Input;
                        penteterritorial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(penteterritorial);

                        DbParameter p_tipo_empresa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pTercero.tipo_empresa != 0) p_tipo_empresa.Value = pTercero.tipo_empresa; else p_tipo_empresa.Value = DBNull.Value;
                        p_tipo_empresa.Direction = ParameterDirection.Input;
                        p_tipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_empresa);

                        DbParameter p_codciudadnacimiento = cmdTransaccionFactory.CreateParameter();
                        p_codciudadnacimiento.ParameterName = "p_codciudadnacimiento";
                        if (pTercero.codciudadnacimiento != 0 && pTercero.codciudadnacimiento != null)
                            p_codciudadnacimiento.Value = pTercero.codciudadnacimiento;
                        else
                            p_codciudadnacimiento.Value = DBNull.Value;
                        p_codciudadnacimiento.Direction = ParameterDirection.Input;
                        p_codciudadnacimiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_codciudadnacimiento);

                        DbParameter p_ubicacion_empresa = cmdTransaccionFactory.CreateParameter();
                        p_ubicacion_empresa.ParameterName = "p_ubicacion_empresa";
                        if (pTercero.ubicacion_empresa != 0 && pTercero.ubicacion_empresa != null)
                            p_ubicacion_empresa.Value = pTercero.ubicacion_empresa;
                        else
                            p_ubicacion_empresa.Value = DBNull.Value;
                        p_ubicacion_empresa.Direction = ParameterDirection.Input;
                        p_ubicacion_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ubicacion_empresa);

                        DbParameter p_dircorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_dircorrespondencia.ParameterName = "p_dircorrespondencia";
                        if (pTercero.dircorrespondencia != "" && pTercero.dircorrespondencia != null)
                           p_dircorrespondencia.Value = pTercero.dircorrespondencia;
                        else
                            p_dircorrespondencia.Value = DBNull.Value;
                        p_dircorrespondencia.Direction = ParameterDirection.Input;
                        p_dircorrespondencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_dircorrespondencia);

                        DbParameter p_ciucorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_ciucorrespondencia.ParameterName = "p_ciucorrespondencia";
                        if (pTercero.ciucorrespondencia != 0 && pTercero.ciucorrespondencia != null)
                            p_ciucorrespondencia.Value = pTercero.ciucorrespondencia;
                        else
                            p_ciucorrespondencia.Value = DBNull.Value;
                        p_ciucorrespondencia.Direction = ParameterDirection.Input;
                        p_ciucorrespondencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_ciucorrespondencia);

                        DbParameter p_barcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_barcorrespondencia.ParameterName = "p_barcorrespondencia";
                        if (pTercero.barcorrespondencia != 0 && pTercero.barcorrespondencia != null)
                            p_barcorrespondencia.Value = pTercero.barcorrespondencia;
                        else
                            p_barcorrespondencia.Value = DBNull.Value;
                        p_barcorrespondencia.Direction = ParameterDirection.Input;
                        p_barcorrespondencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_barcorrespondencia);

                        DbParameter p_telcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_telcorrespondencia.ParameterName = "p_telcorrespondencia";
                        if (pTercero.telcorrespondencia != "" && pTercero.telcorrespondencia != null)
                            p_telcorrespondencia.Value = pTercero.telcorrespondencia;
                        else
                            p_telcorrespondencia.Value = DBNull.Value;
                        p_telcorrespondencia.Direction = ParameterDirection.Input;
                        p_telcorrespondencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_telcorrespondencia);

                        DbParameter p_fecha_residencia = cmdTransaccionFactory.CreateParameter();
                        p_fecha_residencia.ParameterName = "p_fecha_residencia";
                        if (pTercero.fecha_residencia != DateTime.MinValue && pTercero.fecha_residencia != null)
                            p_fecha_residencia.Value = pTercero.fecha_residencia;
                        else
                            p_fecha_residencia.Value = DBNull.Value;
                        p_fecha_residencia.Direction = ParameterDirection.Input;
                        p_fecha_residencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_residencia);

                        DbParameter p_cod_zona = cmdTransaccionFactory.CreateParameter();
                        p_cod_zona.ParameterName = "P_COD_ZONA";
                        if (pTercero.cod_zona != null)
                            p_cod_zona.Value = pTercero.cod_zona;
                        else
                            p_cod_zona.Value = DBNull.Value;
                        p_cod_zona.Direction = ParameterDirection.Input;
                        p_cod_zona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_zona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TERCERO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        pTercero.cod_persona = Convert.ToInt64(pcod_persona.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "CrearTercero", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<bool, string> CambiarTipoDePersona(long id, string tipoPersona, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_codigopersona";
                        p_cod_persona.Value = id;
                        p_cod_persona.DbType = DbType.Int64;
                        p_cod_persona.Direction = ParameterDirection.Input;

                        DbParameter p_tipopersona = cmdTransaccionFactory.CreateParameter();
                        p_tipopersona.ParameterName = "p_tipopersona";
                        p_tipopersona.Value = tipoPersona;
                        p_tipopersona.DbType = DbType.String;
                        p_tipopersona.Direction = ParameterDirection.Input;

                        DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
                        p_mensajeerror.ParameterName = "p_mensajeerror";
                        p_mensajeerror.Value = DBNull.Value;

                        // No quitar, molesta si lo quitas
                        p_mensajeerror.Size = 400;
                        p_mensajeerror.DbType = DbType.String;
                        p_mensajeerror.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);
                        cmdTransaccionFactory.Parameters.Add(p_tipopersona);
                        cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TIPOPER_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string error = p_mensajeerror.Value.ToString();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (string.IsNullOrEmpty(error))
                        {
                            return Tuple.Create(true, string.Empty);
                        }
                        else
                        {
                            return Tuple.Create(false, error);
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "CambiarTipoDePersona", ex);
                        return Tuple.Create(false, ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TerceroS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Tercero modificada</returns>
        public Tercero ModificarTercero(Tercero pTercero, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pTercero.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pTercero.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pTercero.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pdigito_verificacion = cmdTransaccionFactory.CreateParameter();
                        pdigito_verificacion.ParameterName = "p_digito_verificacion";
                        pdigito_verificacion.Value = pTercero.digito_verificacion;
                        pdigito_verificacion.Direction = ParameterDirection.Input;
                        pdigito_verificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdigito_verificacion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        ptipo_identificacion.Value = pTercero.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        ptipo_identificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pfechaexpedicion = cmdTransaccionFactory.CreateParameter();
                        pfechaexpedicion.ParameterName = "p_fechaexpedicion";
                        pfechaexpedicion.Value = pTercero.fechaexpedicion;
                        pfechaexpedicion.Direction = ParameterDirection.Input;
                        pfechaexpedicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaexpedicion);

                        DbParameter pcodciudadexpedicion = cmdTransaccionFactory.CreateParameter();
                        pcodciudadexpedicion.ParameterName = "p_codciudadexpedicion";
                        pcodciudadexpedicion.Value = pTercero.codciudadexpedicion;
                        pcodciudadexpedicion.Direction = ParameterDirection.Input;
                        pcodciudadexpedicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadexpedicion);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        pprimer_apellido.Value = pTercero.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter prazon_social = cmdTransaccionFactory.CreateParameter();
                        prazon_social.ParameterName = "p_razon_social";
                        prazon_social.Value = pTercero.razon_social;
                        prazon_social.Direction = ParameterDirection.Input;
                        prazon_social.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social);

                        DbParameter pcodactividadeco = cmdTransaccionFactory.CreateParameter();
                        pcodactividadeco.ParameterName = "P_CODACTIVIDADECONOMICA";
                        if (string.IsNullOrEmpty(pTercero.ActividadEconomicaEmpresaStr))
                            pcodactividadeco.Value = DBNull.Value;
                        else
                            pcodactividadeco.Value = pTercero.ActividadEconomicaEmpresaStr;
                        pcodactividadeco.Direction = ParameterDirection.Input;
                        pcodactividadeco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodactividadeco);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pTercero.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        ptelefono.Value = pTercero.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        pemail.Value = pTercero.email;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pTercero.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pTercero.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pregimen = cmdTransaccionFactory.CreateParameter();
                        pregimen.ParameterName = "p_regimen";
                        pregimen.Value = pTercero.regimen;
                        pregimen.Direction = ParameterDirection.Input;
                        pregimen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pregimen);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pTercero.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pTercero.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "P_Fecultmod";
                        pfecultmod.Value = pTercero.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "P_Usuultmod";
                        pusuultmod.Value = pTercero.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pTipo_Acto_Creacion = cmdTransaccionFactory.CreateParameter();
                        pTipo_Acto_Creacion.ParameterName = "p_Tipo_Acto_Creacion";
                        if (pTercero.tipo_acto_creacion != 0) pTipo_Acto_Creacion.Value = pTercero.tipo_acto_creacion; else pTipo_Acto_Creacion.Value = DBNull.Value;
                        pTipo_Acto_Creacion.Direction = ParameterDirection.Input;
                        pTipo_Acto_Creacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pTipo_Acto_Creacion);

                        DbParameter pnum_acto_creacion = cmdTransaccionFactory.CreateParameter();
                        pnum_acto_creacion.ParameterName = "p_Num_Acto_Creacion";
                        if (pTercero.num_acto_creacion != null) pnum_acto_creacion.Value = pTercero.num_acto_creacion; else pnum_acto_creacion.Value = DBNull.Value;
                        pnum_acto_creacion.Direction = ParameterDirection.Input;
                        pnum_acto_creacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_acto_creacion);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (pTercero.celular != null) pcelular.Value = pTercero.celular; else pcelular.Value = DBNull.Value;
                        pcelular.Direction = ParameterDirection.Input;
                        pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pTercero.fechanacimiento == DateTime.MinValue || pTercero.fechanacimiento == null)
                            pfechanacimiento.Value = DBNull.Value;
                        else
                            pfechanacimiento.Value = pTercero.fechanacimiento;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        pfechanacimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        DbParameter pcamara_comercio = cmdTransaccionFactory.CreateParameter();
                        pcamara_comercio.ParameterName = "p_camara_comercio";
                        if (!string.IsNullOrEmpty(pTercero.camara_comercio))
                            pcamara_comercio.Value = pTercero.camara_comercio;
                        else
                            pcamara_comercio.Value = DBNull.Value;
                        pcamara_comercio.Direction = ParameterDirection.Input;
                        pcamara_comercio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcamara_comercio);

                        DbParameter pcod_representante = cmdTransaccionFactory.CreateParameter();
                        pcod_representante.ParameterName = "p_representante";
                        pcod_representante.Value = pTercero.cod_representante;
                        pcod_representante.Direction = ParameterDirection.Input;
                        pcod_representante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_representante);

                        DbParameter pcodactividad = cmdTransaccionFactory.CreateParameter();
                        pcodactividad.ParameterName = "P_CODACTIVIDAD";
                        if (string.IsNullOrEmpty(pTercero.codactividadStr))
                            pcodactividad.Value = DBNull.Value;
                        else
                            pcodactividad.Value = pTercero.codactividadStr;
                        pcodactividad.Direction = ParameterDirection.Input;
                        pcodactividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodactividad);

                        DbParameter penteterritorial = cmdTransaccionFactory.CreateParameter();
                        penteterritorial.ParameterName = "P_ENTE_TERRITORIAL";
                        penteterritorial.Value = pTercero.EnteTerritorial;
                        penteterritorial.Direction = ParameterDirection.Input;
                        penteterritorial.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(penteterritorial);

                        DbParameter p_tipo_empresa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pTercero.tipo_empresa != 0) 
                            p_tipo_empresa.Value = pTercero.tipo_empresa; 
                        else
                            p_tipo_empresa.Value = DBNull.Value;
                        p_tipo_empresa.Direction = ParameterDirection.Input;
                        p_tipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_empresa);

                        DbParameter p_codciudadnacimiento = cmdTransaccionFactory.CreateParameter();
                        p_codciudadnacimiento.ParameterName = "p_codciudadnacimiento";
                        if (pTercero.codciudadnacimiento != 0 && pTercero.codciudadnacimiento != null)
                            p_codciudadnacimiento.Value = pTercero.codciudadnacimiento;
                        else
                            p_codciudadnacimiento.Value = DBNull.Value;
                        p_codciudadnacimiento.Direction = ParameterDirection.Input;
                        p_codciudadnacimiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_codciudadnacimiento);

                        DbParameter p_ubicacion_empresa = cmdTransaccionFactory.CreateParameter();
                        p_ubicacion_empresa.ParameterName = "p_ubicacion_empresa";
                        if (pTercero.ubicacion_empresa != 0 && pTercero.ubicacion_empresa != null)
                            p_ubicacion_empresa.Value = pTercero.ubicacion_empresa;
                        else
                            p_ubicacion_empresa.Value = DBNull.Value;
                        p_ubicacion_empresa.Direction = ParameterDirection.Input;
                        p_ubicacion_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_ubicacion_empresa);

                        DbParameter p_dircorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_dircorrespondencia.ParameterName = "p_dircorrespondencia";
                        if (pTercero.dircorrespondencia != "" && pTercero.dircorrespondencia != null)
                            p_dircorrespondencia.Value = pTercero.dircorrespondencia;
                        else
                            p_dircorrespondencia.Value = DBNull.Value;
                        p_dircorrespondencia.Direction = ParameterDirection.Input;
                        p_dircorrespondencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_dircorrespondencia);

                        DbParameter p_ciucorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_ciucorrespondencia.ParameterName = "p_ciucorrespondencia";
                        if (pTercero.ciucorrespondencia != 0 && pTercero.ciucorrespondencia != null)
                            p_ciucorrespondencia.Value = pTercero.ciucorrespondencia;
                        else
                            p_ciucorrespondencia.Value = DBNull.Value;
                        p_ciucorrespondencia.Direction = ParameterDirection.Input;
                        p_ciucorrespondencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_ciucorrespondencia);

                        DbParameter p_barcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_barcorrespondencia.ParameterName = "p_barcorrespondencia";
                        if (pTercero.barcorrespondencia != 0 && pTercero.barcorrespondencia != null)
                            p_barcorrespondencia.Value = pTercero.barcorrespondencia;
                        else
                            p_barcorrespondencia.Value = DBNull.Value;
                        p_barcorrespondencia.Direction = ParameterDirection.Input;
                        p_barcorrespondencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_barcorrespondencia);

                        DbParameter p_telcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        p_telcorrespondencia.ParameterName = "p_telcorrespondencia";
                        if (pTercero.telcorrespondencia != "" && pTercero.telcorrespondencia != null)
                            p_telcorrespondencia.Value = pTercero.telcorrespondencia;
                        else
                            p_telcorrespondencia.Value = DBNull.Value;
                        p_telcorrespondencia.Direction = ParameterDirection.Input;
                        p_telcorrespondencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_telcorrespondencia);

                        DbParameter p_fecha_residencia = cmdTransaccionFactory.CreateParameter();
                        p_fecha_residencia.ParameterName = "p_fecha_residencia";
                        if (pTercero.fecha_residencia != DateTime.MinValue && pTercero.fecha_residencia != null)
                            p_fecha_residencia.Value = pTercero.fecha_residencia;
                        else
                            p_fecha_residencia.Value = DBNull.Value;
                        p_fecha_residencia.Direction = ParameterDirection.Input;
                        p_fecha_residencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_residencia);

                        DbParameter p_cod_zona = cmdTransaccionFactory.CreateParameter();
                        p_cod_zona.ParameterName = "P_COD_ZONA";
                        p_cod_zona.Value = pTercero.cod_zona;
                        p_cod_zona.Direction = ParameterDirection.Input;
                        p_cod_zona.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_zona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TERCERO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ModificarTercero", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TerceroS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TerceroS</param>
        public void EliminarTercero(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pId;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TERCERO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "EliminarTercero", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TerceroS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TerceroS</param>
        /// <returns>Entidad Tercero consultado</returns>
        public Tercero ConsultarTercero(Int64? pCod, string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tercero entidad = new Tercero();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM persona ";
                        if (pCod != null)
                            sql = sql + "WHERE cod_persona = " + pCod.ToString();
                        if (pId != null)
                            sql = sql + "WHERE identificacion = '" + pId.ToString() + "' ";
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
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
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
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt32(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt32(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividadStr = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt32(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt32(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt32(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt32(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.cod_zona = Convert.ToInt32(resultado["COD_ZONA"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt32(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barresidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dircorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telcorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value) entidad.ciucorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barcorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["NUMHIJOS"] != DBNull.Value) entidad.numhijos = Convert.ToInt32(resultado["NUMHIJOS"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.numpersonasacargo = Convert.ToInt32(resultado["NUMPERSONASACARGO"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["ANTIGUEDADLABORAL"] != DBNull.Value) entidad.antiguedadlaboral = Convert.ToInt32(resultado["ANTIGUEDADLABORAL"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt32(resultado["POSICIONEMPRESA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["TIPO_ACTO_CREACION"] != DBNull.Value) entidad.tipo_acto_creacion = Convert.ToInt32(resultado["TIPO_ACTO_CREACION"]);
                            if (resultado["NUM_ACTO_CREACION"] != DBNull.Value) entidad.num_acto_creacion = Convert.ToString(resultado["NUM_ACTO_CREACION"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CAMARA_COMERCIO"] != DBNull.Value) entidad.camara_comercio = Convert.ToString(resultado["CAMARA_COMERCIO"]);
                            if (resultado["COD_REPRESENTANTE"] != DBNull.Value) entidad.cod_representante = Convert.ToInt64(resultado["COD_REPRESENTANTE"]);
                            if (resultado["ENTE_TERRITORIAL"] != DBNull.Value) entidad.EnteTerritorial = Convert.ToInt32(resultado["ENTE_TERRITORIAL"]);
                            if (resultado["TIPO_EMPRESA"] != DBNull.Value) entidad.tipo_empresa = Convert.ToInt32(resultado["TIPO_EMPRESA"]);
                            if (resultado["UBICACION_EMPRESA"] != DBNull.Value) entidad.ubicacion_empresa = Convert.ToInt32(resultado["UBICACION_EMPRESA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        //Agregado para consultar información de representante legal
                        sql = @"SELECT TIPO_IDENTIFICACION, IDENTIFICACION, PRIMER_NOMBRE ||' '|| SEGUNDO_NOMBRE ||' '|| PRIMER_APELLIDO ||' '|| SEGUNDO_APELLIDO 
                              AS NOMBRES FROM PERSONA WHERE COD_PERSONA = " + entidad.cod_representante;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_id_representante = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.id_representante = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nom_representante = Convert.ToString(resultado["NOMBRES"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ConsultarTercero", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarRegimen(Int64 pCod, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tercero entidad = new Tercero();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM Regimen WHERE codpersona = " + pCod.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_REGIMEN"] != DBNull.Value) entidad.regimen = Convert.ToString(resultado["TIPO_REGIMEN"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.regimen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ConsultarRegimen", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Tercero dados unos filtros
        /// </summary>
        /// <param name="pTercero">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Terceros obtenidos</returns>
        public List<Tercero> ListarTercero(Tercero pTercero, Usuario vUsuario)
        {
            return ListarTercero(pTercero, "", "0", vUsuario);
        }

        public List<Tercero> ListarTercero(Tercero pTercero, string pFiltro, string pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Tercero> lstTercero = new List<Tercero>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sFiltroEnt = ObtenerFiltro(pTercero, "Persona.");
                        string sql = "";
                        sql = "Select Persona.*,Persona_Afiliacion.Valor, (Select Count(*) From persona_biometria x Where x.cod_persona = persona.cod_persona) As biometria From Persona left Join Persona_Afiliacion "
                               + "On Persona.Cod_Persona = Persona_Afiliacion.Cod_Persona " + sFiltroEnt;

                        if (pFiltro != null && pFiltro.Trim() != "")
                        {
                            if (sFiltroEnt.Trim() != "")
                                sql = sql + " AND " + pFiltro;
                            else
                                sql = sql + " WHERE " + pFiltro;
                        }

                        if (pOrden == null || pOrden.Trim() == "" || pOrden.Trim() == "0")
                            sql = sql + " ORDER BY Persona.cod_persona ";
                        else
                            if (Convert.ToInt32(pOrden) > 0)
                            sql = sql + " ORDER BY " + pOrden.Trim() + " ASC ";
                        else
                            sql = sql + " ORDER BY " + Math.Abs(Convert.ToInt64(pOrden)) + " DESC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tercero entidad = new Tercero();
                            try
                            {
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
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
                                if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToDecimal(resultado["VALORARRIENDO"]);
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
                                if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                                if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                                if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                                if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt32(resultado["POSICIONEMPRESA"]);
                                if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                                if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                                if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                                if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                                if (entidad.tipo_persona == "N") entidad.nom_tipo_persona = "Natural";
                                else if (entidad.tipo_persona == "J") entidad.nom_tipo_persona = "Juridica";
                                else if (entidad.tipo_persona == "M") entidad.nom_tipo_persona = "Menor de Edad";
                                else entidad.nom_tipo_persona = "";
                                if (resultado["VALOR"] != DBNull.Value) entidad.valor_afiliacion = Convert.ToDecimal(resultado["VALOR"]);
                                if (resultado["BIOMETRIA"] != DBNull.Value) entidad.biometria = Convert.ToInt32(resultado["BIOMETRIA"]);
                            }
                            catch (OverflowException)
                            {
                                entidad.error = "..";
                            }
                            catch (Exception ex)
                            {
                                entidad.error = "Error: " + ex.Message;
                            }
                            lstTercero.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarTercero", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tercero> ListarTerceroSoloAfiliados(Tercero pTercero, string pFiltro, Usuario vUsuario, string pOrden)
        {
            DbDataReader resultado;
            List<Tercero> lstTercero = new List<Tercero>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sFiltroEnt = ObtenerFiltro(pTercero, "Persona.");
                        string sql = "";
                        sql = "Select Persona.*, Persona_Afiliacion.Valor, (Select Count(*) From persona_biometria x Where x.cod_persona = persona.cod_persona) As biometria From Persona Inner Join Persona_Afiliacion "
                               + "On Persona.Cod_Persona = Persona_Afiliacion.Cod_Persona " + sFiltroEnt;

                        if (pFiltro.Trim() != "")
                        {
                            if (sFiltroEnt.Trim() != "")
                                sql = sql + " AND " + pFiltro;
                            else
                                sql = sql + " WHERE " + pFiltro;
                        }

                        if (pOrden == null || pOrden.Trim() == "" || pOrden.Trim() == "0")
                            sql = sql + " ORDER BY Persona.cod_persona ";
                        else
                            if (Convert.ToInt32(pOrden) > 0)
                            sql = sql + " ORDER BY " + pOrden.Trim() + " ASC ";
                        else
                            sql = sql + " ORDER BY " + Math.Abs(Convert.ToInt64(pOrden)) + " DESC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tercero entidad = new Tercero();
                            try
                            {
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
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
                                if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToDecimal(resultado["VALORARRIENDO"]);
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
                                if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                                if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                                if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                                if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt32(resultado["POSICIONEMPRESA"]);
                                if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                                if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                                if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                                if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                                if (entidad.tipo_persona == "N") entidad.nom_tipo_persona = "Natural";
                                else if (entidad.tipo_persona == "J") entidad.nom_tipo_persona = "Juridica";
                                else if (entidad.tipo_persona == "M") entidad.nom_tipo_persona = "Menor de Edad";
                                else entidad.nom_tipo_persona = "";
                                if (resultado["VALOR"] != DBNull.Value) entidad.valor_afiliacion = Convert.ToDecimal(resultado["VALOR"]);
                                if (resultado["BIOMETRIA"] != DBNull.Value) entidad.biometria = Convert.ToInt32(resultado["BIOMETRIA"]);
                            }
                            catch (OverflowException)
                            {
                                entidad.error = "..";
                            }
                            lstTercero.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarTerceroSoloAfiliados", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tercero> ListarTerceroNoAfiliados(Tercero pTercero, string pFiltro, Usuario vUsuario, string pOrden)
        {
            DbDataReader resultado;
            List<Tercero> lstTercero = new List<Tercero>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sFiltroEnt = ObtenerFiltro(pTercero, "Persona.");
                        string sql = "";
                        sql = "Select Persona.*, (Select Count(*) From persona_biometria x Where x.cod_persona = persona.cod_persona) As biometria From Persona Where Persona.cod_persona Not In (Select a.cod_persona From Persona_Afiliacion a Where a.cod_persona =  persona.cod_persona) ";

                        if (pFiltro.Trim() != "")
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql = sql + " AND " + pFiltro;
                            else
                                sql = sql + " WHERE " + pFiltro;
                        }

                        if (!string.IsNullOrWhiteSpace(sFiltroEnt))
                        {
                            if (sFiltroEnt.ToUpper().Contains("WHERE"))
                                sFiltroEnt = sFiltroEnt.Replace(" WHERE ", " AND ");
                            sql += sFiltroEnt;
                        }

                        if (pOrden == null || pOrden.Trim() == "" || pOrden.Trim() == "0")
                            sql = sql + " ORDER BY Persona.cod_persona ";
                        else
                            if (Convert.ToInt32(pOrden) > 0)
                            sql = sql + " ORDER BY " + pOrden.Trim() + " ASC ";
                        else
                            sql = sql + " ORDER BY " + Math.Abs(Convert.ToInt64(pOrden)) + " DESC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tercero entidad = new Tercero();
                            try
                            {
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt32(resultado["DIGITO_VERIFICACION"]);
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
                                if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToDecimal(resultado["VALORARRIENDO"]);
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
                                if (resultado["ESTRATO"] != DBNull.Value) entidad.estrato = Convert.ToInt32(resultado["ESTRATO"]);
                                if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.celularempresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                                if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudadempresa = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                                if (resultado["POSICIONEMPRESA"] != DBNull.Value) entidad.posicionempresa = Convert.ToInt32(resultado["POSICIONEMPRESA"]);
                                if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.actividadempresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                                if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.parentescoempleado = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                                if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                                if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                                if (entidad.tipo_persona == "N") entidad.nom_tipo_persona = "Natural";
                                else if (entidad.tipo_persona == "J") entidad.nom_tipo_persona = "Juridica";
                                else if (entidad.tipo_persona == "M") entidad.nom_tipo_persona = "Menor de Edad";
                                else entidad.nom_tipo_persona = "";
                                if (resultado["BIOMETRIA"] != DBNull.Value) entidad.biometria = Convert.ToInt32(resultado["BIOMETRIA"]);
                            }
                            catch (OverflowException)
                            {
                                entidad.error = "..";
                            }
                            lstTercero.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarTerceroNoAfiliados", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Valida el Tercero que ingresa al sistema
        /// </summary>
        /// <param name="pTercero"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public Tercero ValidarTercero(Int64 pTercero, Usuario pUsuario)
        {
            DbDataReader resultado;
            Tercero entidad = new Tercero();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_persona, identificacion " +
                                     " FROM Persona " +
                                     " WHERE cod_persona = '" + pTercero.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
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
                        BOExcepcion.Throw("TerceroData", "ValidarTercero", ex);
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
                        string sql = "SELECT MAX(cod_persona) + 1 FROM persona ";

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
                        return 1;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un asociado registrado para la persona juridica
        /// </summary>
        /// <param name="vAsociado"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Tercero CrearAsociado(Tercero vAsociado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_asociado = cmdTransaccionFactory.CreateParameter();
                        p_cod_asociado.ParameterName = "p_cod_asociado";
                        p_cod_asociado.Value = vAsociado.cod_representante;
                        p_cod_asociado.Direction = ParameterDirection.Output;
                        p_cod_asociado.DbType = DbType.Int64;
                        
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = vAsociado.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;

                        DbParameter p_nombres = cmdTransaccionFactory.CreateParameter();
                        p_nombres.ParameterName = "p_nombres";
                        p_nombres.Value = vAsociado.nombres;
                        p_nombres.Direction = ParameterDirection.Input;
                        p_nombres.DbType = DbType.String;

                        DbParameter p_tipo_id = cmdTransaccionFactory.CreateParameter();
                        p_tipo_id.ParameterName = "p_tipo_id";
                        p_tipo_id.Value = vAsociado.tipo_identificacion;
                        p_tipo_id.Direction = ParameterDirection.Input;
                        p_tipo_id.DbType = DbType.Int64;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = vAsociado.identificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.Int64;

                        DbParameter p_fecha_expedicion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_expedicion.ParameterName = "p_fecha_expedicion";
                        p_fecha_expedicion.Value = vAsociado.fechaexpedicion;
                        p_fecha_expedicion.Direction = ParameterDirection.Input;
                        p_fecha_expedicion.DbType = DbType.DateTime;

                        DbParameter p_porcentaje_patrimonio = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_patrimonio.ParameterName = "p_porcentaje_patrimonio";
                        p_porcentaje_patrimonio.Value = vAsociado.porcentaje_patrimonio;
                        p_porcentaje_patrimonio.Direction = ParameterDirection.Input;
                        p_porcentaje_patrimonio.DbType = DbType.Int64;

                        //Agregado
                        DbParameter p_cotiza_bolsa = cmdTransaccionFactory.CreateParameter();
                        p_cotiza_bolsa.ParameterName = "p_cotiza_bolsa";
                        p_cotiza_bolsa.Value = vAsociado.cotiza_bolsa;
                        p_cotiza_bolsa.Direction = ParameterDirection.Input;
                        p_cotiza_bolsa.DbType = DbType.Int32;

                        DbParameter p_vincula_pep = cmdTransaccionFactory.CreateParameter();
                        p_vincula_pep.ParameterName = "p_vincula_pep";
                        p_vincula_pep.Value = vAsociado.vincula_pep;
                        p_vincula_pep.Direction = ParameterDirection.Input;
                        p_vincula_pep.DbType = DbType.Int32;

                        DbParameter p_tributacion = cmdTransaccionFactory.CreateParameter();
                        p_tributacion.ParameterName = "p_tributacion";
                        p_tributacion.Value = vAsociado.tributacion;
                        p_tributacion.Direction = ParameterDirection.Input;
                        p_tributacion.DbType = DbType.Int32;

                        cmdTransaccionFactory.Parameters.Add(p_cod_asociado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);                        
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_id);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_expedicion);
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_patrimonio);
                        cmdTransaccionFactory.Parameters.Add(p_cotiza_bolsa);
                        cmdTransaccionFactory.Parameters.Add(p_vincula_pep);
                        cmdTransaccionFactory.Parameters.Add(p_tributacion);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ASOCJUR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        vAsociado.cod_representante = Convert.ToInt64(p_cod_asociado.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return vAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "CrearAsociado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar un asociado relacionado para la persona juridica
        /// </summary>
        /// <param name="vAsociado">Objeto de tipo Tercero con los datos del asociado</param>
        /// <param name="vUsuario">Variable de usaurio</param>
        /// <returns></returns>
        public Tercero ModificarAsociado(Tercero vAsociado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_asociado = cmdTransaccionFactory.CreateParameter();
                        p_cod_asociado.ParameterName = "p_cod_asociado";
                        p_cod_asociado.Value = vAsociado.cod_representante;
                        p_cod_asociado.Direction = ParameterDirection.Input;
                        p_cod_asociado.DbType = DbType.Int64;

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = vAsociado.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;

                        DbParameter p_nombres = cmdTransaccionFactory.CreateParameter();
                        p_nombres.ParameterName = "p_nombres";
                        p_nombres.Value = vAsociado.nombres;
                        p_nombres.Direction = ParameterDirection.Input;
                        p_nombres.DbType = DbType.String;

                        DbParameter p_tipo_id = cmdTransaccionFactory.CreateParameter();
                        p_tipo_id.ParameterName = "p_tipo_id";
                        p_tipo_id.Value = vAsociado.tipo_identificacion;
                        p_tipo_id.Direction = ParameterDirection.Input;
                        p_tipo_id.DbType = DbType.Int64;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = vAsociado.identificacion;
                        p_identificacion.Direction = ParameterDirection.Input;
                        p_identificacion.DbType = DbType.Int64;

                        DbParameter p_fecha_expedicion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_expedicion.ParameterName = "p_fecha_expedicion";
                        p_fecha_expedicion.Value = vAsociado.fechaexpedicion;
                        p_fecha_expedicion.Direction = ParameterDirection.Input;
                        p_fecha_expedicion.DbType = DbType.DateTime;

                        DbParameter p_porcentaje_patrimonio = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_patrimonio.ParameterName = "p_porcentaje_patrimonio";
                        p_porcentaje_patrimonio.Value = vAsociado.porcentaje_patrimonio;
                        p_porcentaje_patrimonio.Direction = ParameterDirection.Input;
                        p_porcentaje_patrimonio.DbType = DbType.Int32;

                        //Agregado
                        DbParameter p_cotiza_bolsa = cmdTransaccionFactory.CreateParameter();
                        p_cotiza_bolsa.ParameterName = "p_cotiza_bolsa";
                        p_cotiza_bolsa.Value = vAsociado.cotiza_bolsa;
                        p_cotiza_bolsa.Direction = ParameterDirection.Input;
                        p_cotiza_bolsa.DbType = DbType.Int32;

                        DbParameter p_vincula_pep = cmdTransaccionFactory.CreateParameter();
                        p_vincula_pep.ParameterName = "p_vincula_pep";
                        p_vincula_pep.Value = vAsociado.vincula_pep;
                        p_vincula_pep.Direction = ParameterDirection.Input;
                        p_vincula_pep.DbType = DbType.Int32;

                        DbParameter p_tributacion = cmdTransaccionFactory.CreateParameter();
                        p_tributacion.ParameterName = "p_tributacion";
                        p_tributacion.Value = vAsociado.tributacion;
                        p_tributacion.Direction = ParameterDirection.Input;
                        p_tributacion.DbType = DbType.Int32;

                        cmdTransaccionFactory.Parameters.Add(p_cod_asociado);
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_id);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_expedicion);
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_patrimonio);
                        cmdTransaccionFactory.Parameters.Add(p_cotiza_bolsa);
                        cmdTransaccionFactory.Parameters.Add(p_vincula_pep);
                        cmdTransaccionFactory.Parameters.Add(p_tributacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ASOCJUR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        
                        dbConnectionFactory.CerrarConexion(connection);

                        return vAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ModificarAsociado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar un registro de los asociados relacionados para la persona juridica
        /// </summary>
        /// <param name="pId"> Código unico del asociado</param>
        /// <param name="vUsuario"> Variable de usuario</param>
        public void EliminarAsociado(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_asociado = cmdTransaccionFactory.CreateParameter();
                        p_cod_asociado.ParameterName = "p_cod_asociado";
                        p_cod_asociado.Value = pId;
                        p_cod_asociado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asociado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ASOCJUR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "EliminarAsociado", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Listar asociados de una persona juridica
        /// </summary>
        /// <param name="cod_persona">Código de la persona juridica</param>
        /// <param name="pUsuario">Variable de Usuario</param>
        /// <returns></returns>
        public List<Tercero> ListarAsociados(Int64 cod_persona, Usuario pUsuario)
        {
            List<Tercero> lstAsociados = new List<Tercero>();
            DbDataReader resultado;
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM ASOCIADO_PERSONAJURIDICA WHERE COD_PERSONA = " + cod_persona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tercero entidad = new Tercero();

                            if (resultado["COD_ASOCIADO"] != DBNull.Value) entidad.cod_representante = Convert.ToInt64(resultado["COD_ASOCIADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["TIPO_ID"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_ID"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_EXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHA_EXPEDICION"]);
                            if (resultado["PORCENTAJE_PATRIMONIO"] != DBNull.Value) entidad.porcentaje_patrimonio = Convert.ToInt32(resultado["PORCENTAJE_PATRIMONIO"]);
                            if (resultado["COTIZA_EN_BOLSA"] != DBNull.Value) entidad.cotiza_bolsa = Convert.ToInt32(resultado["COTIZA_EN_BOLSA"]);
                            if (resultado["VINCULA_PEP"] != DBNull.Value) entidad.vincula_pep = Convert.ToInt32(resultado["VINCULA_PEP"]);
                            if (resultado["TRIBUTACION"] != DBNull.Value) entidad.tributacion = Convert.ToInt32(resultado["TRIBUTACION"]);
                            
                            lstAsociados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAsociados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TerceroData", "ListarAsociados", ex);
                        return null;
                    }
                }
            }
        }

    }
}