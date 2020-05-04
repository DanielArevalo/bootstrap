using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PERSONA
    /// </summary>
    public class Persona1Data : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public Persona1Data()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Persona1 ConsultarPersona2Param(Persona1 identificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT PERSONA.*, persona_afiliacion.estado As estados, persona_afiliacion.fecha_afiliacion, T.descripcion AS nomtipocontrato, Z.DESCRIPCION ZONA_NOMBRE
                                    FROM PERSONA Left Join persona_afiliacion on persona.cod_persona = persona_afiliacion.cod_persona
                                    LEFT JOIN TIPOCONTRATO T ON PERSONA.CODTIPOCONTRATO = T.CODTIPOCONTRATO
                                    LEFT JOIN ZONAS Z ON Z.COD_ZONA = PERSONA.COD_ZONA
                                    WHERE persona.identificacion = '" + identificacion.identificacion.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADOS"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADOS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value)
                                entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            else
                                entidad.codciudadexpedicion = -1;
                            if (resultado["SEXO"] != DBNull.Value)
                                entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            else
                                entidad.sexo = "";
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (entidad.tipo_persona == "N")
                                entidad.nombres = entidad.primer_nombre + " " + entidad.segundo_nombre;
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (entidad.tipo_persona == "N")
                                entidad.apellidos = entidad.primer_apellido + " " + entidad.segundo_apellido;
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (entidad.tipo_persona == "J")
                                entidad.nombres = entidad.razon_social;
                            entidad.nombre = entidad.tipo_persona == "N" ? (entidad.nombres + " " + entidad.apellidos).Trim() : entidad.razon_social;
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividadStr = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt64(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value)
                                entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            else
                                entidad.tipovivienda = "";
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.ValorArriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barrioResidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dirCorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telCorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value)
                                entidad.ciuCorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            else
                                entidad.ciuCorrespondencia = -1;
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barrioCorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.ActividadEconomicaEmpresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.relacionEmpleadosEmprender = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.CelularEmpresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profecion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.PersonasAcargo = Convert.ToInt32(resultado["NUMPERSONASACARGO"]);
                            if (resultado["NOMBRE_FUNCIONARIO"] != DBNull.Value) entidad.nombre_funcionario = Convert.ToString(resultado["NOMBRE_FUNCIONARIO"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingresoempresa = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["EMPLEADO_ENTIDAD"] != DBNull.Value) entidad.empleado_entidad = Convert.ToInt32(resultado["EMPLEADO_ENTIDAD"]);
                            if (resultado["MUJER_FAMILIA"] != DBNull.Value) entidad.mujer_familia = Convert.ToInt32(resultado["MUJER_FAMILIA"]);
                            if (resultado["JORNADA_LABORAL"] != DBNull.Value) entidad.jornada_laboral = Convert.ToInt32(resultado["JORNADA_LABORAL"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacionApo = Convert.ToInt32(resultado["OCUPACION"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["NOMTIPOCONTRATO"] != DBNull.Value) entidad.tipocontrato = Convert.ToString(resultado["NOMTIPOCONTRATO"]);
                            if (resultado["ZONA_NOMBRE"] != DBNull.Value) entidad.nom_zona = Convert.ToString(resultado["ZONA_NOMBRE"]);
                            
                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarIdentificacionPersona(long codPersona, Usuario usuario)
        {
            DbDataReader resultado;
            string identificacion = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    { 

                        string sql = @"select identificacion from persona where cod_persona = " + codPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION"] != DBNull.Value) identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return identificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarIdentificacionPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ConsultarDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 Persona1 = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from asclientes where CODTIPOIDENTIFICACION = " + pPersona.tipo_identificacion + " and SIDENTIFICACION = '" + pPersona.identificacion + "' ";                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ICODIGO"] != DBNull.Value) Persona1.cod_persona = Convert.ToInt64(resultado["ICODIGO"]);
                            if (resultado["SNOMBRE1"] != DBNull.Value) Persona1.primer_nombre = Convert.ToString(resultado["SNOMBRE1"]);
                            if (resultado["SNOMBRE2"] != DBNull.Value) Persona1.segundo_nombre = Convert.ToString(resultado["SNOMBRE2"]);
                            if (resultado["SAPELLIDO1"] != DBNull.Value) Persona1.primer_apellido = Convert.ToString(resultado["SAPELLIDO1"]);
                            if (resultado["SAPELLIDO2"] != DBNull.Value) Persona1.segundo_apellido = Convert.ToString(resultado["SAPELLIDO2"]);
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) Persona1.tipo_identificacion = Convert.ToInt64(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["SIDENTIFICACION"] != DBNull.Value) Persona1.identificacion = Convert.ToString(resultado["SIDENTIFICACION"]);
                            if (resultado["SDIRECCION"] != DBNull.Value) Persona1.direccion = Convert.ToString(resultado["SDIRECCION"]);
                            if (resultado["ICODZONA"] != DBNull.Value) Persona1.zona = Convert.ToInt64(resultado["ICODZONA"]);
                            if (resultado["STELEFONO"] != DBNull.Value) Persona1.telefono = Convert.ToString(resultado["STELEFONO"]);
                            if (resultado["SEMAIL"] != DBNull.Value) Persona1.email = Convert.ToString(resultado["SEMAIL"]);
                            if (resultado["SRAZONSOC"] != DBNull.Value) Persona1.razon_social = Convert.ToString(resultado["SRAZONSOC"]);
                            if (resultado["SSIGLA"] != DBNull.Value) Persona1.nombre = Convert.ToString(resultado["SSIGLA"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) Persona1.ActividadEconomicaEmpresa = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Persona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultaDatosPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 consultaridentificacion(Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"select * from identificacion_huella";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);


                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }


        public Persona1 crearidentificacion(Persona1 pPersona1, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = pPersona1.identificacion;
                        P_IDENTIFICACION.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_FAB_IDENTIFICA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pPersona1;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("Persona1Data", "crearidentificacion", ex);
                        return null;
                    }
                }
            }
        }


        public void eliminaridentificacion(Persona1 pPersona1, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Persona1 pAhorroVista = new Persona1();
                        pAhorroVista = consultaridentificacion(vUsuario);

                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = pPersona1.identificacion;
                        P_IDENTIFICACION.Direction = ParameterDirection.Input;
                        P_IDENTIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_FAB_IDENTIFICA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("eliminaridentificacion", "eliminaridentificacion", ex);
                    }
                }
            }
        }


        public Persona1 CrearPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = 0;
                        pCOD_PERSONA.Direction = ParameterDirection.InputOutput;

                        DbParameter pTIPO_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PERSONA.ParameterName = "p_TIPO_PERSONA";
                        pTIPO_PERSONA.Value = pPersona1.tipo_persona;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        if (!string.IsNullOrWhiteSpace(pPersona1.identificacion))
                        {
                            pIDENTIFICACION.Value = pPersona1.identificacion;
                        }
                        else
                        {
                            pIDENTIFICACION.Value = DBNull.Value;
                        }

                        DbParameter pDIGITO_VERIFICACION = cmdTransaccionFactory.CreateParameter();
                        pDIGITO_VERIFICACION.ParameterName = "p_DIGITO_VERIFICACION";
                        pDIGITO_VERIFICACION.Value = pPersona1.digito_verificacion;

                        DbParameter pTIPO_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_IDENTIFICACION.ParameterName = "p_TIPO_IDENTIFICACION";
                        pTIPO_IDENTIFICACION.Value = pPersona1.tipo_identificacion;

                        DbParameter pFECHAEXPEDICION = cmdTransaccionFactory.CreateParameter();
                        pFECHAEXPEDICION.ParameterName = "p_FECHAEXPEDICION";
                        if (pPersona1.fechaexpedicion == null)
                            pFECHAEXPEDICION.Value = System.DateTime.Now;
                        else
                            pFECHAEXPEDICION.Value = pPersona1.fechaexpedicion;

                        DbParameter pCODCIUDADEXPEDICION = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADEXPEDICION.ParameterName = "p_CODCIUDADEXPEDICION";
                        if (pPersona1.codciudadexpedicion == null)
                            pCODCIUDADEXPEDICION.Value = 0;
                        else
                            pCODCIUDADEXPEDICION.Value = pPersona1.codciudadexpedicion;

                        DbParameter pSEXO = cmdTransaccionFactory.CreateParameter();
                        pSEXO.ParameterName = "p_SEXO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.sexo))
                        {
                            pSEXO.Value = pPersona1.sexo;
                        }
                        else
                        {
                            pSEXO.Value = DBNull.Value;
                        }

                        DbParameter pPRIMER_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_NOMBRE.ParameterName = "p_PRIMER_NOMBRE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.primer_nombre))
                        {
                            pPRIMER_NOMBRE.Value = pPersona1.primer_nombre;
                        }
                        else
                        {
                            pPRIMER_NOMBRE.Value = DBNull.Value;
                        }


                        DbParameter pSEGUNDO_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_NOMBRE.ParameterName = "p_SEGUNDO_NOMBRE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.segundo_nombre))
                        {
                            pSEGUNDO_NOMBRE.Value = pPersona1.segundo_nombre;
                        }
                        else
                        {
                            pSEGUNDO_NOMBRE.Value = DBNull.Value;
                        }

                        DbParameter pPRIMER_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_APELLIDO.ParameterName = "p_PRIMER_APELLIDO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.primer_apellido))
                        {
                            pPRIMER_APELLIDO.Value = pPersona1.primer_apellido;
                        }
                        else
                        {
                            pPRIMER_APELLIDO.Value = DBNull.Value;
                        }

                        DbParameter pSEGUNDO_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_APELLIDO.ParameterName = "p_SEGUNDO_APELLIDO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.segundo_apellido))
                        {
                            pSEGUNDO_APELLIDO.Value = pPersona1.segundo_apellido;
                        }
                        else
                        {
                            pSEGUNDO_APELLIDO.Value = DBNull.Value;
                        }


                        DbParameter pRAZON_SOCIAL = cmdTransaccionFactory.CreateParameter();
                        pRAZON_SOCIAL.ParameterName = "p_RAZON_SOCIAL";
                        if (!string.IsNullOrWhiteSpace(pPersona1.razon_social))
                        {
                            pRAZON_SOCIAL.Value = pPersona1.razon_social;
                        }
                        else
                        {
                            pRAZON_SOCIAL.Value = DBNull.Value;
                        }


                        DbParameter pFECHANACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pFECHANACIMIENTO.ParameterName = "p_FECHANACIMIENTO";
                        if (pPersona1.fechanacimiento == null)
                            pFECHANACIMIENTO.Value = System.DateTime.Now;
                        else
                            pFECHANACIMIENTO.Value = pPersona1.fechanacimiento;

                        DbParameter pCODCIUDADNACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADNACIMIENTO.ParameterName = "p_CODCIUDADNACIMIENTO";
                        if (pPersona1.codciudadnacimiento == null)
                            pCODCIUDADNACIMIENTO.Value = 0;
                        else
                            pCODCIUDADNACIMIENTO.Value = pPersona1.codciudadnacimiento;

                        DbParameter pCODESTADOCIVIL = cmdTransaccionFactory.CreateParameter();
                        pCODESTADOCIVIL.ParameterName = "p_CODESTADOCIVIL";
                        if (pPersona1.codestadocivil == 0)
                        {
                            pCODESTADOCIVIL.Value = DBNull.Value;
                        }
                        else
                        {
                            pCODESTADOCIVIL.Value = pPersona1.codestadocivil;
                        }

                        DbParameter pCODESCOLARIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODESCOLARIDAD.ParameterName = "p_CODESCOLARIDAD";
                        if (pPersona1.codescolaridad == null)
                            pCODESCOLARIDAD.Value = DBNull.Value;
                        else
                            pCODESCOLARIDAD.Value = pPersona1.codescolaridad;

                        DbParameter pCODACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODACTIVIDAD.ParameterName = "p_CODACTIVIDAD";
                        if (pPersona1.codactividadStr == null)
                            pCODACTIVIDAD.Value = DBNull.Value;
                        else
                            pCODACTIVIDAD.Value = pPersona1.codactividadStr;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        if (!string.IsNullOrWhiteSpace(pPersona1.direccion))
                        {
                            pDIRECCION.Value = pPersona1.direccion;
                        }
                        else
                        {
                            pDIRECCION.Value = DBNull.Value;
                        }

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefono))
                        {
                            pTELEFONO.Value = pPersona1.telefono;
                        }
                        else
                        {
                            pTELEFONO.Value = DBNull.Value;
                        }

                        DbParameter pCODCIUDADRESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADRESIDENCIA.ParameterName = "p_CODCIUDADRESIDENCIA";
                        if (pPersona1.codciudadresidencia == null)
                            pCODCIUDADRESIDENCIA.Value = 0;
                        else
                            pCODCIUDADRESIDENCIA.Value = pPersona1.codciudadresidencia;

                        DbParameter pANTIGUEDADLUGAR = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLUGAR.ParameterName = "p_ANTIGUEDADLUGAR";
                        pANTIGUEDADLUGAR.Value = pPersona1.antiguedadlugar;

                        DbParameter pTIPOVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        pTIPOVIVIENDA.ParameterName = "p_TIPOVIVIENDA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.tipovivienda))
                        {
                            pTIPOVIVIENDA.Value = pPersona1.tipovivienda;
                        }
                        else
                        {
                            pTIPOVIVIENDA.Value = DBNull.Value;
                        }


                        DbParameter pARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pARRENDADOR.ParameterName = "p_ARRENDADOR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.arrendador))
                        {
                            pARRENDADOR.Value = pPersona1.arrendador;
                        }
                        else
                        {
                            pARRENDADOR.Value = DBNull.Value;
                        }


                        DbParameter pTELEFONOARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOARRENDADOR.ParameterName = "p_TELEFONOARRENDADOR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefonoarrendador))
                        {
                            pTELEFONOARRENDADOR.Value = pPersona1.telefonoarrendador;
                        }
                        else
                        {
                            pTELEFONOARRENDADOR.Value = DBNull.Value;
                        }

                        DbParameter pVALORARRIENDO = cmdTransaccionFactory.CreateParameter();
                        pVALORARRIENDO.ParameterName = "p_VALORARRIENDO";
                        pVALORARRIENDO.Value = pPersona1.ValorArriendo;

                        DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        pCELULAR.ParameterName = "p_CELULAR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.celular))
                        {
                            pCELULAR.Value = pPersona1.celular;
                        }
                        else
                        {
                            pCELULAR.Value = DBNull.Value;
                        }

                        DbParameter pEMAIL = cmdTransaccionFactory.CreateParameter();
                        pEMAIL.ParameterName = "p_EMAIL";
                        if (!string.IsNullOrWhiteSpace(pPersona1.email))
                        {
                            pEMAIL.Value = pPersona1.email;
                        }
                        else
                        {
                            pEMAIL.Value = DBNull.Value;
                        }

                        DbParameter pEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA.ParameterName = "p_EMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.empresa))
                        {
                            pEMPRESA.Value = pPersona1.empresa;
                        }
                        else
                        {
                            pEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pTELEFONOEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOEMPRESA.ParameterName = "p_TELEFONOEMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefonoempresa))
                        {
                            pTELEFONOEMPRESA.Value = pPersona1.telefonoempresa;
                        }
                        else
                        {
                            pTELEFONOEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pDIRECCIONEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pDIRECCIONEMPRESA.ParameterName = "p_DIRECCIONEMPRESA";
                        if (pPersona1.direccionempresa == null)
                            pDIRECCIONEMPRESA.Value = DBNull.Value;
                        else
                            pDIRECCIONEMPRESA.Value = pPersona1.direccionempresa;

                        DbParameter pANTIGUEDADLUGAREMPRESA = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLUGAREMPRESA.ParameterName = "p_ANTIGUEDADLUGAREMPRESA";
                        pANTIGUEDADLUGAREMPRESA.Value = pPersona1.antiguedadlugarempresa;

                        DbParameter pCODCARGO = cmdTransaccionFactory.CreateParameter();
                        pCODCARGO.ParameterName = "p_CODCARGO";
                        if (pPersona1.codcargo == 0)
                            pCODCARGO.Value = DBNull.Value;
                        else
                            pCODCARGO.Value = pPersona1.codcargo;

                        DbParameter pCODTIPOCONTRATO = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOCONTRATO.ParameterName = "p_CODTIPOCONTRATO";
                        if (pPersona1.codtipocontrato == 0)
                            pCODTIPOCONTRATO.Value = DBNull.Value;
                        else
                            pCODTIPOCONTRATO.Value = pPersona1.codtipocontrato;

                        DbParameter pCOD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR.ParameterName = "p_COD_ASESOR";
                        if (pPersona1.cod_asesor != 0)
                            pCOD_ASESOR.Value = pPersona1.cod_asesor;
                        else
                            pCOD_ASESOR.Value = DBNull.Value;

                        DbParameter pRESIDENTE = cmdTransaccionFactory.CreateParameter();
                        pRESIDENTE.ParameterName = "p_RESIDENTE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.residente))
                        {
                            pRESIDENTE.Value = pPersona1.residente;
                        }
                        else
                        {
                            pRESIDENTE.Value = DBNull.Value;
                        }

                        DbParameter pFECHA_RESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_RESIDENCIA.ParameterName = "p_FECHA_RESIDENCIA";
                        if (pPersona1.fecha_residencia != DateTime.MinValue) pFECHA_RESIDENCIA.Value = pPersona1.fecha_residencia; else pFECHA_RESIDENCIA.Value = DBNull.Value;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = "p_COD_OFICINA";
                        pCOD_OFICINA.Value = pPersona1.cod_oficina;

                        DbParameter pTRATAMIENTO = cmdTransaccionFactory.CreateParameter();
                        pTRATAMIENTO.ParameterName = "p_TRATAMIENTO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.tratamiento))
                        {
                            pTRATAMIENTO.Value = pPersona1.tratamiento;
                        }
                        else
                        {
                            pTRATAMIENTO.Value = DBNull.Value;
                        }

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.estado))
                        {
                            pESTADO.Value = pPersona1.estado;
                        }
                        else
                        {
                            pESTADO.Value = DBNull.Value;
                        }

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHACREACION.Value = pPersona1.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "p_USUARIOCREACION";
                        pUSUARIOCREACION.Value = pPersona1.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "p_FECULTMOD";
                        pFECULTMOD.Value = pPersona1.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        pUSUULTMOD.Value = pPersona1.usuultmod;

                        DbParameter pBARRESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pBARRESIDENCIA.ParameterName = "p_BARRESIDENCIA";
                        if (pPersona1.barrioResidencia == null)
                            pBARRESIDENCIA.Value = DBNull.Value;
                        else
                            pBARRESIDENCIA.Value = pPersona1.barrioResidencia;

                        DbParameter pDIRCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pDIRCORRESPONDENCIA.ParameterName = "p_DIRCORRESPONDENCIA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.dirCorrespondencia))
                        {
                            pDIRCORRESPONDENCIA.Value = pPersona1.dirCorrespondencia;
                        }
                        else
                        {
                            pDIRCORRESPONDENCIA.Value = DBNull.Value;
                        }


                        DbParameter pBARCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pBARCORRESPONDENCIA.ParameterName = "p_BARCORRESPONDENCIA";
                        if (pPersona1.barrioCorrespondencia.HasValue)
                        {
                            pBARCORRESPONDENCIA.Value = pPersona1.barrioCorrespondencia;
                        }
                        else
                        {
                            pBARCORRESPONDENCIA.Value = DBNull.Value;
                        }


                        DbParameter pTELCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pTELCORRESPONDENCIA.ParameterName = "p_TELCORRESPONDENCIA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telCorrespondencia))
                        {
                            pTELCORRESPONDENCIA.Value = pPersona1.telCorrespondencia;
                        }
                        else
                        {
                            pTELCORRESPONDENCIA.Value = DBNull.Value;
                        }

                        DbParameter pCIUCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pCIUCORRESPONDENCIA.ParameterName = "p_CIUCORRESPONDENCIA";
                        if (pPersona1.ciuCorrespondencia.HasValue)
                        {
                            pCIUCORRESPONDENCIA.Value = pPersona1.ciuCorrespondencia;
                        }
                        else
                        {
                            pCIUCORRESPONDENCIA.Value = DBNull.Value;
                        }

                        //Nuevos campos solicitados:

                        DbParameter pNUMHIJOS = cmdTransaccionFactory.CreateParameter();
                        pNUMHIJOS.ParameterName = "p_NUMHIJOS";
                        pNUMHIJOS.Value = pPersona1.numHijos;

                        DbParameter pNUMPERSONASACARGO = cmdTransaccionFactory.CreateParameter();
                        pNUMPERSONASACARGO.ParameterName = "p_NUMPERSONASACARGO";
                        if (pPersona1.PersonasAcargo.HasValue)
                        {
                            pNUMPERSONASACARGO.Value = pPersona1.PersonasAcargo;
                        }
                        else
                        {
                            pNUMPERSONASACARGO.Value = DBNull.Value;
                        }

                        DbParameter pPROFESION = cmdTransaccionFactory.CreateParameter();
                        pPROFESION.ParameterName = "p_PROFESION";
                        if (!string.IsNullOrWhiteSpace(pPersona1.profecion))
                        {
                            pPROFESION.Value = pPersona1.profecion;
                        }
                        else
                        {
                            pPROFESION.Value = DBNull.Value;
                        }

                        DbParameter pSALARIO = cmdTransaccionFactory.CreateParameter();
                        pSALARIO.ParameterName = "p_SALARIO";
                        if (pPersona1.salario != null)
                            pSALARIO.Value = pPersona1.salario;
                        else
                            pSALARIO.Value = DBNull.Value;

                        DbParameter pANTIGUEDADLABORAL = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLABORAL.ParameterName = "p_ANTIGUEDADLABORAL";
                        pANTIGUEDADLABORAL.Value = pPersona1.antiguedadLaboral;

                        //--------------------------------------------------------------------------------
                        //Campos Faltantes
                        //--------------------------------------------------------------------------------

                        DbParameter pACTIVIDADECONOMICA = cmdTransaccionFactory.CreateParameter();
                        pACTIVIDADECONOMICA.ParameterName = "p_ACTIVIDADECONOMICA";

                        if (pPersona1.ActividadEconomicaEmpresaStr == "" || pPersona1.ActividadEconomicaEmpresaStr == null)
                            pACTIVIDADECONOMICA.Value = DBNull.Value;
                        else
                            pACTIVIDADECONOMICA.Value = pPersona1.ActividadEconomicaEmpresaStr;

                        DbParameter pCIUDADEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pCIUDADEMPRESA.ParameterName = "p_CIUDADEMPRESA";
                        if (pPersona1.ciudad.HasValue)
                        {
                            pCIUDADEMPRESA.Value = pPersona1.ciudad;
                        }
                        else
                        {
                            pCIUDADEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pPARENTESCO.ParameterName = "p_PARENTESCO";
                        pPARENTESCO.Value = pPersona1.relacionEmpleadosEmprender;

                        DbParameter pCELULAREMPRESA = cmdTransaccionFactory.CreateParameter();
                        pCELULAREMPRESA.ParameterName = "p_CELULAREMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.CelularEmpresa))
                        {
                            pCELULAREMPRESA.Value = pPersona1.CelularEmpresa;
                        }
                        else
                        {
                            pCELULAREMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pESTRATO = cmdTransaccionFactory.CreateParameter();
                        pESTRATO.ParameterName = "p_ESTRATO";
                        if (pPersona1.Estrato.HasValue)
                        {
                            pESTRATO.Value = pPersona1.Estrato;
                        }
                        else
                        {
                            pESTRATO.Value = DBNull.Value;
                        }

                        DbParameter p_fecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_ingreso.ParameterName = "p_FECHA_INGRESO";
                        if (pPersona1.fecha_ingresoempresa != DateTime.MinValue)
                        {
                            p_fecha_ingreso.Value = pPersona1.fecha_ingresoempresa;
                        }
                        else
                        {
                            p_fecha_ingreso.Value = DBNull.Value;
                        }

                        DbParameter p_ocupacion = cmdTransaccionFactory.CreateParameter();
                        p_ocupacion.ParameterName = "p_OCUPACION";
                        if (pPersona1.ocupacion != "" && pPersona1.ocupacion != null)
                        {
                            p_ocupacion.Value = Convert.ToInt64(pPersona1.ocupacion);
                        }
                        else
                        {
                            p_ocupacion.Value = DBNull.Value;
                        }

                        DbParameter p_empleadoentidad = cmdTransaccionFactory.CreateParameter();
                        p_empleadoentidad.ParameterName = "P_EMPLEADOENTIDAD";
                        if (pPersona1.empleado_entidad != int.MinValue)
                        {
                            p_empleadoentidad.Value = Convert.ToInt64(pPersona1.empleado_entidad);
                        }
                        else
                        {
                            p_empleadoentidad.Value = DBNull.Value;
                        }

                        DbParameter p_cod_zona = cmdTransaccionFactory.CreateParameter();
                        p_cod_zona.ParameterName = "P_COD_ZONA";
                        if (pPersona1.zona != int.MinValue || pPersona1.zona == null)
                        {
                            p_cod_zona.Value = pPersona1.zona;
                        }
                        else
                        {
                            p_cod_zona.Value = DBNull.Value;
                        }


                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pDIGITO_VERIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_IDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHAEXPEDICION);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADEXPEDICION);
                        cmdTransaccionFactory.Parameters.Add(pSEXO);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pRAZON_SOCIAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHANACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADNACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCODESTADOCIVIL);
                        cmdTransaccionFactory.Parameters.Add(pCODESCOLARIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCODACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADRESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLUGAR);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVIVIENDA);
                        cmdTransaccionFactory.Parameters.Add(pARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pVALORARRIENDO);
                        cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        cmdTransaccionFactory.Parameters.Add(pEMAIL);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCIONEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLUGAREMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pCODCARGO);
                        cmdTransaccionFactory.Parameters.Add(pCODTIPOCONTRATO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR);
                        cmdTransaccionFactory.Parameters.Add(pRESIDENTE);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_RESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pTRATAMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pBARRESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pDIRCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pBARCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pTELCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCIUCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNUMHIJOS);
                        cmdTransaccionFactory.Parameters.Add(pNUMPERSONASACARGO);
                        cmdTransaccionFactory.Parameters.Add(pPROFESION);
                        cmdTransaccionFactory.Parameters.Add(pSALARIO);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLABORAL);
                        cmdTransaccionFactory.Parameters.Add(pACTIVIDADECONOMICA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDADEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pCELULAREMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pESTRATO);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_ingreso);
                        cmdTransaccionFactory.Parameters.Add(p_ocupacion);
                        cmdTransaccionFactory.Parameters.Add(p_empleadoentidad);
                        cmdTransaccionFactory.Parameters.Add(p_cod_zona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PERSONA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPersona1, "PERSONA", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pPersona1.cod_persona = pCOD_PERSONA.Value != DBNull.Value ? Convert.ToInt64(pCOD_PERSONA.Value) : 0;
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersona1;
                    }
                    catch (Exception ex)
                    {
                        string a = ex.ToString();
                        BOExcepcion.Throw("Persona1Data", "CrearPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ConsultaDatosCliente(Persona1 pPersona, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from asclientes where sidentificacion = "+pPersona.identificacion+" and codtipoidentificacion = "+pPersona.tipo_identificacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ICODIGO"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["ICODIGO"]);
                            if (resultado["SNOMBRE1"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["SNOMBRE1"]);
                            if (resultado["SNOMBRE2"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SNOMBRE2"]);
                            if (resultado["SAPELLIDO1"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["SAPELLIDO1"]);
                            if (resultado["SAPELLIDO2"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SAPELLIDO2"]);
                            if (resultado["CODTIPOIDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["CODTIPOIDENTIFICACION"]);
                            if (resultado["SIDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["SIDENTIFICACION"]);
                            if (resultado["SDIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["SDIRECCION"]);
                            if (resultado["ICODZONA"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["ICODZONA"]);
                            if (resultado["STELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["STELEFONO"]);
                            if (resultado["SEMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["SEMAIL"]);                            
                            if (resultado["SRAZONSOC"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["SRAZONSOC"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.ActividadEconomicaEmpresa = Convert.ToInt64(resultado["CODACTIVIDAD"]);                                                                                    
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
                        BOExcepcion.Throw("Persona1Data", "ConsultaDatosCliente", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarSiPersonaEsNatural(long codigoPersona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            bool esNatural = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select TIPO_PERSONA from persona where COD_PERSONA = " + codigoPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            string tipoPersona = string.Empty;

                            if (resultado["TIPO_PERSONA"] != DBNull.Value) tipoPersona = Convert.ToString(resultado["TIPO_PERSONA"]);

                            if (tipoPersona == "N")
                            {
                                esNatural = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return esNatural;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "VerificarSiPersonaEsNatural", ex);
                        return false;
                    }
                }
            }
        }

        public bool VerificarSiPersonaEsAsociado(long codigoPersona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            bool esAsociado = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select estado from PERSONA_AFILIACION where cod_persona = " + codigoPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            string estado = string.Empty;

                            if (resultado["estado"] != DBNull.Value) estado = Convert.ToString(resultado["estado"]);

                            if (estado == "A")
                            {
                                esAsociado = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return esAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "VerificarSiPersonaEsAsociado", ex);
                        return false;
                    }
                }
            }
        }

        public Persona1 BuscarDepartamentoPorCodigoCiudad(long ciudad, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 departamento = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from ciudades where codciudad = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = " + ciudad + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODCIUDAD"] != DBNull.Value) departamento.ciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) departamento.nombre_ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return departamento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "BuscarDepartamentoPorCodigoCiudad", ex);
                        return null;
                    }
                }
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(string identificacion, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            long cod_empresa = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select cod_empresa
                                        from PERSONA_EMPRESA_RECAUDO emp
                                        join persona per on per.COD_PERSONA = emp.COD_PERSONA
                                        where per.identificacion = " + identificacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_empresa"] != DBNull.Value) cod_empresa = Convert.ToInt64(resultado["cod_empresa"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_empresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarCodigoEmpresaPagaduria", ex);
                        return 0;
                    }
                }
            }
        }

        public long ConsultarCodigoEmpresaPagaduria(Int64 pCodPersona, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            long cod_empresa = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select emp.cod_empresa
                                        from PERSONA_EMPRESA_RECAUDO emp
                                        where emp.cod_persona = " + pCodPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_empresa"] != DBNull.Value) cod_empresa = Convert.ToInt64(resultado["cod_empresa"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_empresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarCodigoEmpresaPagaduria", ex);
                        return 0;
                    }
                }
            }
        }

        public List<Persona1> ConsultarPersonasAfiliadas(string filtro, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstPersona1 = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT per.cod_persona, per.identificacion, tip.DESCRIPCION AS TipoIdentificacion, NVL(perafi.ESTADO, per.ESTADO) As estado, per.EMAIL, per.razon_social,
                                            per.PRIMER_NOMBRE || ' ' || per.segundo_nombre || ' ' || per.PRIMER_APELLIDO || ' ' || per.segundo_apellido AS Nombre, 
                                            perafi.FECHA_AFILIACION, perafi.FECHA_RETIRO 
                                        FROM persona per LEFT JOIN PERSONA_AFILIACION perafi ON perafi.COD_PERSONA = per.COD_PERSONA
                                        JOIN tipoidentificacion tip ON per.TIPO_IDENTIFICACION = tip.codtipoidentificacion   "
                                        + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPOIDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion_descripcion = Convert.ToString(resultado["TIPOIDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["FECHA_RETIRO"] != DBNull.Value) entidad.fecha_retiro = Convert.ToDateTime(resultado["FECHA_RETIRO"]);

                            lstPersona1.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersonasAfiliadas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Persona1 modificada</returns>
        public Persona1 ModificarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pOrigen = cmdTransaccionFactory.CreateParameter();
                        pOrigen.ParameterName = "p_Origen";
                        pOrigen.Value = pPersona1.origen;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPersona1.cod_persona;

                        DbParameter pTIPO_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PERSONA.ParameterName = "p_TIPO_PERSONA";
                        pTIPO_PERSONA.Value = pPersona1.tipo_persona;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        pIDENTIFICACION.Value = pPersona1.identificacion;

                        DbParameter pDIGITO_VERIFICACION = cmdTransaccionFactory.CreateParameter();
                        pDIGITO_VERIFICACION.ParameterName = "p_DIGITO_VERIFICACION";
                        pDIGITO_VERIFICACION.Value = pPersona1.digito_verificacion;

                        DbParameter pTIPO_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_IDENTIFICACION.ParameterName = "p_TIPO_IDENTIFICACION";
                        pTIPO_IDENTIFICACION.Value = pPersona1.tipo_identificacion;

                        DbParameter pFECHAEXPEDICION = cmdTransaccionFactory.CreateParameter();
                        pFECHAEXPEDICION.ParameterName = "p_FECHAEXPEDICION";
                        pFECHAEXPEDICION.DbType = DbType.Date;
                        if (pPersona1.fechaexpedicion == null)
                        {
                            pFECHAEXPEDICION.Value = DBNull.Value;
                        }
                        else
                            pFECHAEXPEDICION.Value = pPersona1.fechaexpedicion;

                        DbParameter pCODCIUDADEXPEDICION = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADEXPEDICION.ParameterName = "p_CODCIUDADEXPEDICION";
                        if (pPersona1.codciudadexpedicion == null)
                            pCODCIUDADEXPEDICION.Value = 0;
                        else
                            pCODCIUDADEXPEDICION.Value = pPersona1.codciudadexpedicion;

                        DbParameter pSEXO = cmdTransaccionFactory.CreateParameter();
                        pSEXO.ParameterName = "p_SEXO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.sexo))
                        {
                            pSEXO.Value = pPersona1.sexo;
                        }
                        else
                        {
                            pSEXO.Value = DBNull.Value;
                        }

                        DbParameter pPRIMER_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_NOMBRE.ParameterName = "p_PRIMER_NOMBRE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.primer_nombre))
                        {
                            pPRIMER_NOMBRE.Value = pPersona1.primer_nombre;
                        }
                        else
                        {
                            pPRIMER_NOMBRE.Value = DBNull.Value;
                        }

                        DbParameter pSEGUNDO_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_NOMBRE.ParameterName = "p_SEGUNDO_NOMBRE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.segundo_nombre))
                        {
                            pSEGUNDO_NOMBRE.Value = pPersona1.segundo_nombre;
                        }
                        else
                        {
                            pSEGUNDO_NOMBRE.Value = DBNull.Value;
                        }

                        DbParameter pPRIMER_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_APELLIDO.ParameterName = "p_PRIMER_APELLIDO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.primer_apellido))
                        {
                            pPRIMER_APELLIDO.Value = pPersona1.primer_apellido;
                        }
                        else
                        {
                            pPRIMER_APELLIDO.Value = DBNull.Value;
                        }

                        DbParameter pSEGUNDO_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_APELLIDO.ParameterName = "p_SEGUNDO_APELLIDO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.segundo_apellido))
                        {
                            pSEGUNDO_APELLIDO.Value = pPersona1.segundo_apellido;
                        }
                        else
                        {
                            pSEGUNDO_APELLIDO.Value = DBNull.Value;
                        }

                        DbParameter pRAZON_SOCIAL = cmdTransaccionFactory.CreateParameter();
                        pRAZON_SOCIAL.ParameterName = "p_RAZON_SOCIAL";
                        if (pPersona1.razon_social != "") pRAZON_SOCIAL.Value = pPersona1.razon_social; else pRAZON_SOCIAL.Value = DBNull.Value;

                        DbParameter pFECHANACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pFECHANACIMIENTO.ParameterName = "p_FECHANACIMIENTO";
                        pFECHANACIMIENTO.DbType = DbType.Date;

                        if (pPersona1.fechanacimiento == null)
                        {
                            pFECHANACIMIENTO.Value = DBNull.Value;
                        }
                        else
                            pFECHANACIMIENTO.Value = pPersona1.fechanacimiento;

                        DbParameter pCODCIUDADNACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADNACIMIENTO.ParameterName = "p_CODCIUDADNACIMIENTO";
                        if (pPersona1.codciudadnacimiento == null)
                            pCODCIUDADNACIMIENTO.Value = 0;
                        else
                            pCODCIUDADNACIMIENTO.Value = pPersona1.codciudadnacimiento;


                        DbParameter pCODESTADOCIVIL = cmdTransaccionFactory.CreateParameter();
                        pCODESTADOCIVIL.ParameterName = "p_CODESTADOCIVIL";
                        if (pPersona1.codestadocivil == 0 || pPersona1.codestadocivil == null)
                        {
                            pCODESTADOCIVIL.Value = DBNull.Value;
                        }
                        else
                            pCODESTADOCIVIL.Value = pPersona1.codestadocivil;

                        DbParameter pCODESCOLARIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODESCOLARIDAD.ParameterName = "p_CODESCOLARIDAD";

                        if (pPersona1.codescolaridad == 0 || pPersona1.codescolaridad == null)
                        {
                            pCODESCOLARIDAD.Value = DBNull.Value;
                        }
                        else
                            pCODESCOLARIDAD.Value = pPersona1.codescolaridad;


                        DbParameter pCODACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODACTIVIDAD.ParameterName = "p_CODACTIVIDAD";
                        if (pPersona1.codactividadStr == "" || pPersona1.codactividadStr == null)
                            pCODACTIVIDAD.Value = DBNull.Value;
                        else
                            pCODACTIVIDAD.Value = pPersona1.codactividadStr;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        if (!string.IsNullOrWhiteSpace(pPersona1.direccion))
                        {
                            pDIRECCION.Value = pPersona1.direccion;
                        }
                        else
                        {
                            pDIRECCION.Value = DBNull.Value;
                        }


                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefono))
                        {
                            pTELEFONO.Value = pPersona1.telefono;
                        }
                        else
                        {
                            pTELEFONO.Value = DBNull.Value;
                        }

                        DbParameter pCODCIUDADRESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADRESIDENCIA.ParameterName = "p_CODCIUDADRESIDENCIA";
                        if (pPersona1.codciudadresidencia == 0)
                        {
                            pCODCIUDADRESIDENCIA.Value = DBNull.Value;
                        }
                        else
                            if (pPersona1.codciudadresidencia != null)
                            pCODCIUDADRESIDENCIA.Value = pPersona1.codciudadresidencia;
                        else pCODCIUDADRESIDENCIA.Value = DBNull.Value;

                        DbParameter pANTIGUEDADLUGAR = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLUGAR.ParameterName = "p_ANTIGUEDADLUGAR";
                        pANTIGUEDADLUGAR.Value = pPersona1.antiguedadlugar;

                        DbParameter pTIPOVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        pTIPOVIVIENDA.ParameterName = "p_TIPOVIVIENDA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.tipovivienda))
                        {
                            pTIPOVIVIENDA.Value = pPersona1.tipovivienda;
                        }
                        else
                        {
                            pTIPOVIVIENDA.Value = DBNull.Value;
                        }

                        DbParameter pARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pARRENDADOR.ParameterName = "p_ARRENDADOR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.arrendador))
                        {
                            pARRENDADOR.Value = pPersona1.arrendador;
                        }
                        else
                        {
                            pARRENDADOR.Value = DBNull.Value;
                        }

                        DbParameter pTELEFONOARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOARRENDADOR.ParameterName = "p_TELEFONOARRENDADOR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefonoarrendador))
                        {
                            pTELEFONOARRENDADOR.Value = pPersona1.telefonoarrendador;
                        }
                        else
                        {
                            pTELEFONOARRENDADOR.Value = DBNull.Value;
                        }

                        DbParameter pVALORARRIENDO = cmdTransaccionFactory.CreateParameter();
                        pVALORARRIENDO.ParameterName = "p_VALORARRIENDO";
                        pVALORARRIENDO.Value = pPersona1.ValorArriendo;

                        DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        pCELULAR.ParameterName = "p_CELULAR";
                        if (!string.IsNullOrWhiteSpace(pPersona1.celular))
                        {
                            pCELULAR.Value = pPersona1.celular;
                        }
                        else
                        {
                            pCELULAR.Value = DBNull.Value;
                        }

                        DbParameter pEMAIL = cmdTransaccionFactory.CreateParameter();
                        pEMAIL.ParameterName = "p_EMAIL";
                        if (!string.IsNullOrWhiteSpace(pPersona1.email))
                        {
                            pEMAIL.Value = pPersona1.email;
                        }
                        else
                        {
                            pEMAIL.Value = DBNull.Value;
                        }

                        DbParameter pEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pEMPRESA.ParameterName = "p_EMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.empresa))
                        {
                            pEMPRESA.Value = pPersona1.empresa;
                        }
                        else
                        {
                            pEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pTELEFONOEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOEMPRESA.ParameterName = "p_TELEFONOEMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telefonoempresa))
                        {
                            pTELEFONOEMPRESA.Value = pPersona1.telefonoempresa;
                        }
                        else
                        {
                            pTELEFONOEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pDIRECCIONEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pDIRECCIONEMPRESA.ParameterName = "p_DIRECCIONEMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.direccionempresa))
                        {
                            pDIRECCIONEMPRESA.Value = pPersona1.direccionempresa;
                        }
                        else
                        {
                            pDIRECCIONEMPRESA.Value = DBNull.Value;
                        }

                        DbParameter pANTIGUEDADLUGAREMPRESA = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLUGAREMPRESA.ParameterName = "p_ANTIGUEDADLUGAREMPRESA";
                        pANTIGUEDADLUGAREMPRESA.Value = pPersona1.antiguedadlugarempresa;

                        DbParameter pCODCARGO = cmdTransaccionFactory.CreateParameter();
                        pCODCARGO.ParameterName = "p_CODCARGO";
                        if (pPersona1.codcargo == 0)
                            pCODCARGO.Value = DBNull.Value;
                        else
                            pCODCARGO.Value = pPersona1.codcargo;



                        DbParameter pCODTIPOCONTRATO = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOCONTRATO.ParameterName = "p_CODTIPOCONTRATO";

                        if (pPersona1.codtipocontrato == 0)
                            pCODTIPOCONTRATO.Value = DBNull.Value;
                        else
                            pCODTIPOCONTRATO.Value = pPersona1.codtipocontrato;


                        DbParameter pCOD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR.ParameterName = "p_COD_ASESOR";
                        if (pPersona1.cod_asesor == 0)
                            pCOD_ASESOR.Value = DBNull.Value;
                        else
                            pCOD_ASESOR.Value = pPersona1.cod_asesor;

                        DbParameter pRESIDENTE = cmdTransaccionFactory.CreateParameter();
                        pRESIDENTE.ParameterName = "p_RESIDENTE";
                        if (!string.IsNullOrWhiteSpace(pPersona1.residente))
                        {
                            pRESIDENTE.Value = pPersona1.residente;
                        }
                        else
                        {
                            pRESIDENTE.Value = DBNull.Value;
                        }

                        DbParameter pFECHA_RESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_RESIDENCIA.ParameterName = "p_FECHA_RESIDENCIA";
                        pFECHAEXPEDICION.DbType = DbType.Date;
                        pFECHA_RESIDENCIA.Value = pPersona1.fecha_residencia;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = "p_COD_OFICINA";
                        if (pPersona1.cod_oficina == 0)
                            pCOD_OFICINA.Value = DBNull.Value;
                        else
                            pCOD_OFICINA.Value = pPersona1.cod_oficina;


                        DbParameter pTRATAMIENTO = cmdTransaccionFactory.CreateParameter();
                        pTRATAMIENTO.ParameterName = "p_TRATAMIENTO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.tratamiento))
                        {
                            pTRATAMIENTO.Value = pPersona1.tratamiento;
                        }
                        else
                        {
                            pTRATAMIENTO.Value = DBNull.Value;
                        }


                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        if (!string.IsNullOrWhiteSpace(pPersona1.estado))
                        {
                            pESTADO.Value = pPersona1.estado;
                        }
                        else
                        {
                            pESTADO.Value = DBNull.Value;
                        }


                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHAEXPEDICION.DbType = DbType.Date;
                        pFECHACREACION.Value = pPersona1.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "p_USUARIOCREACION";
                        pUSUARIOCREACION.Value = pPersona1.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "p_FECULTMOD";
                        pFECHAEXPEDICION.DbType = DbType.Date;
                        pFECULTMOD.Value = pPersona1.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        pUSUULTMOD.Value = pPersona1.usuultmod;

                        // Nuevos campos solicitados:

                        DbParameter pBARRESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pBARRESIDENCIA.ParameterName = "p_BARRESIDENCIA";
                        if (pPersona1.barrioResidencia == null)
                            pBARRESIDENCIA.Value = DBNull.Value;
                        else
                            pBARRESIDENCIA.Value = pPersona1.barrioResidencia;

                        DbParameter pDIRCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pDIRCORRESPONDENCIA.ParameterName = "p_DIRCORRESPONDENCIA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.dirCorrespondencia))
                        {
                            pDIRCORRESPONDENCIA.Value = pPersona1.dirCorrespondencia;
                        }
                        else
                        {
                            pDIRCORRESPONDENCIA.Value = DBNull.Value;
                        }


                        DbParameter pBARCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pBARCORRESPONDENCIA.ParameterName = "p_BARCORRESPONDENCIA";
                        if (pPersona1.barrioCorrespondencia.HasValue)
                        {
                            pBARCORRESPONDENCIA.Value = pPersona1.barrioCorrespondencia;
                        }
                        else
                        {
                            pBARCORRESPONDENCIA.Value = DBNull.Value;
                        }


                        DbParameter pTELCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pTELCORRESPONDENCIA.ParameterName = "p_TELCORRESPONDENCIA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.telCorrespondencia))
                        {
                            pTELCORRESPONDENCIA.Value = pPersona1.telCorrespondencia;
                        }
                        else
                        {
                            pTELCORRESPONDENCIA.Value = DBNull.Value;
                        }

                        DbParameter pCIUCORRESPONDENCIA = cmdTransaccionFactory.CreateParameter();
                        pCIUCORRESPONDENCIA.ParameterName = "p_CIUCORRESPONDENCIA";
                        if (pPersona1.ciuCorrespondencia.HasValue)
                        {
                            pCIUCORRESPONDENCIA.Value = pPersona1.ciuCorrespondencia;
                        }
                        else
                        {
                            pCIUCORRESPONDENCIA.Value = DBNull.Value;
                        }

                        //Nuevos campos solicitados:

                        DbParameter pNUMHIJOS = cmdTransaccionFactory.CreateParameter();
                        pNUMHIJOS.ParameterName = "p_NUMHIJOS";
                        pNUMHIJOS.Value = pPersona1.numHijos;

                        DbParameter pNUMPERSONASACARGO = cmdTransaccionFactory.CreateParameter();
                        pNUMPERSONASACARGO.ParameterName = "p_NUMPERSONASACARGO";
                        if (pPersona1.PersonasAcargo.HasValue)
                        {
                            pNUMPERSONASACARGO.Value = pPersona1.PersonasAcargo;
                        }
                        else
                        {
                            pNUMPERSONASACARGO.Value = DBNull.Value;
                        }

                        DbParameter pPROFESION = cmdTransaccionFactory.CreateParameter();
                        pPROFESION.ParameterName = "p_PROFESION";
                        if (!string.IsNullOrWhiteSpace(pPersona1.profecion))
                        {
                            pPROFESION.Value = pPersona1.profecion;
                        }
                        else
                        {
                            pPROFESION.Value = DBNull.Value;
                        }

                        DbParameter pSALARIO = cmdTransaccionFactory.CreateParameter();
                        pSALARIO.ParameterName = "p_SALARIO";
                        pSALARIO.Value = pPersona1.salario;

                        DbParameter pANTIGUEDADLABORAL = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDADLABORAL.ParameterName = "p_ANTIGUEDADLABORAL";
                        pANTIGUEDADLABORAL.Value = pPersona1.antiguedadLaboral;

                        //--------------------------------------------------------------------------------
                        //Campos Faltantes
                        //--------------------------------------------------------------------------------

                        DbParameter pACTIVIDADECONOMICA = cmdTransaccionFactory.CreateParameter();
                        pACTIVIDADECONOMICA.ParameterName = "p_ACTIVIDADECONOMICA";

                        if (pPersona1.ActividadEconomicaEmpresaStr == "" || pPersona1.ActividadEconomicaEmpresaStr == null)
                            pACTIVIDADECONOMICA.Value = DBNull.Value;
                        else
                            pACTIVIDADECONOMICA.Value = pPersona1.ActividadEconomicaEmpresaStr;

                        DbParameter pCIUDADEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pCIUDADEMPRESA.ParameterName = "p_CIUDADEMPRESA";
                        if (pPersona1.ciudad.HasValue)
                        {
                            pCIUDADEMPRESA.Value = pPersona1.ciudad;
                        }
                        else
                        {
                            pCIUDADEMPRESA.Value = DBNull.Value;
                        }


                        DbParameter pPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pPARENTESCO.ParameterName = "p_PARENTESCO";
                        pPARENTESCO.Value = pPersona1.relacionEmpleadosEmprender;

                        DbParameter pCELULAREMPRESA = cmdTransaccionFactory.CreateParameter();
                        pCELULAREMPRESA.ParameterName = "p_CELULAREMPRESA";
                        if (!string.IsNullOrWhiteSpace(pPersona1.CelularEmpresa))
                        {
                            pCELULAREMPRESA.Value = pPersona1.CelularEmpresa;
                        }
                        else
                        {
                            pCELULAREMPRESA.Value = DBNull.Value;
                        }


                        DbParameter pESTRATO = cmdTransaccionFactory.CreateParameter();
                        pESTRATO.ParameterName = "p_ESTRATO";
                        if (pPersona1.Estrato.HasValue)
                        {
                            pESTRATO.Value = pPersona1.Estrato;
                        }
                        else
                        {
                            pESTRATO.Value = DBNull.Value;
                        }

                        DbParameter Pcod_Negocio = cmdTransaccionFactory.CreateParameter();
                        Pcod_Negocio.Size = 200;
                        Pcod_Negocio.ParameterName = "Pcod_Negocio";
                        if (pPersona1.cod_negocio == null || pPersona1.cod_negocio == "")
                            Pcod_Negocio.Value = DBNull.Value;
                        else
                            Pcod_Negocio.Value = pPersona1.cod_negocio;
                        Pcod_Negocio.Direction = ParameterDirection.Output;


                        DbParameter p_fecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        p_fecha_ingreso.ParameterName = "p_fecha_ingreso";
                        if (pPersona1.fecha_ingresoempresa != DateTime.MinValue)
                        {
                            p_fecha_ingreso.Value = pPersona1.fecha_ingresoempresa;
                        }
                        else
                        {
                            p_fecha_ingreso.Value = DBNull.Value;
                        }

                        cmdTransaccionFactory.Parameters.Add(pOrigen);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pDIGITO_VERIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_IDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHAEXPEDICION);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADEXPEDICION);
                        cmdTransaccionFactory.Parameters.Add(pSEXO);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pRAZON_SOCIAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHANACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADNACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCODESTADOCIVIL);
                        cmdTransaccionFactory.Parameters.Add(pCODESCOLARIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCODACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADRESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLUGAR);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVIVIENDA);
                        cmdTransaccionFactory.Parameters.Add(pARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pVALORARRIENDO);
                        cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        cmdTransaccionFactory.Parameters.Add(pEMAIL);
                        cmdTransaccionFactory.Parameters.Add(pEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCIONEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLUGAREMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pCODCARGO);
                        cmdTransaccionFactory.Parameters.Add(pCODTIPOCONTRATO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR);
                        cmdTransaccionFactory.Parameters.Add(pRESIDENTE);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_RESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pTRATAMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pBARRESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pDIRCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pBARCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pTELCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCIUCORRESPONDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNUMHIJOS);
                        cmdTransaccionFactory.Parameters.Add(pNUMPERSONASACARGO);
                        cmdTransaccionFactory.Parameters.Add(pPROFESION);
                        cmdTransaccionFactory.Parameters.Add(pSALARIO);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDADLABORAL);

                        //--------------------------------------------------------------------------------
                        //Campos Faltantes
                        //--------------------------------------------------------------------------------
                        cmdTransaccionFactory.Parameters.Add(pACTIVIDADECONOMICA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDADEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pCELULAREMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pESTRATO);
                        cmdTransaccionFactory.Parameters.Add(Pcod_Negocio);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_ingreso);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PERSONA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        pPersona1.cod_negocio = Convert.ToString(Pcod_Negocio.Value);

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPersona1, "PERSONA", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ModificarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERSONA</param>
        public void EliminarPersona1(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Persona1 pPersona1 = new Persona1();

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PERSONA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPersona1, "PERSONA", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "InsertarPersona1", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad Persona1 consultado</returns>
        public Persona1 ConsultarPersona1(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT Persona.*, (Select Ciudades.Nomciudad from Ciudades where Ciudades.Codciudad = persona.CODCIUDADRESIDENCIA) as nomCiudad "
                        + "FROM PERSONA WHERE COD_PERSONA = " + pId.ToString();

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
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.ValorArriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]); else entidad.estado = "";
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barrioResidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
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
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }
        public Persona1 ConsultarNotificaciones(Int64 pId, Usuario pUsuario, int opcion)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (opcion == 1)
                        {
                            sql = "SELECT * FROM NOTIFICACION where tipo='C'";
                        }
                        if (opcion == 2)
                        {
                            sql = "SELECT * FROM NOTIFICACION where tipo='FC'";
                        }
                        if (opcion == 3)
                        {
                            sql = "SELECT * FROM NOTIFICACION where tipo='AP'";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {

                            //if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.idNotificaion = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.Texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["DiasAviso"] != DBNull.Value) entidad.DiasAviso = Convert.ToInt32(resultado["DiasAviso"]);


                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "ConsultarNotificaciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad Persona1 consultado</returns>
        public Persona1 ConsultarPersona1TrasladoOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.cod_persona,p.tipo_persona,p.Identificacion,p.Nombre,e.cod_empresa from v_persona p left join PERSONA_EMPRESA_RECAUDO e on p.cod_persona = e.cod_persona  where p.cod_persona = " + pId.ToString();
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
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.idEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        sql = @"SELECT * FROM V_PRODUCTOSPERSONA WHERE COD_PERSONA=" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Productos_Persona productos = new Productos_Persona();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) productos.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) productos.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) productos.num_producto = Convert.ToInt64(resultado["NUM_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) productos.cod_linea = Convert.ToInt64(resultado["COD_LINEA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) productos.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["SALDO"] != DBNull.Value) productos.saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) productos.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) productos.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            entidad.Lista_Producto.Add(productos);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1TrasladoOficina", ex);
                        return null;
                    }
                }
            }
        }


        public Productos_Persona ModificarTrasladoOficina(Productos_Persona pProducto, Int64 cod_persona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_producto.ParameterName = "p_cod_tipo_producto";
                        p_cod_tipo_producto.Value = pProducto.cod_tipo_producto;
                        p_cod_tipo_producto.Direction = ParameterDirection.Input;
                        p_cod_tipo_producto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_producto);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "p_num_producto";
                        pnum_producto.Value = pProducto.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        pcod_linea.Value = pProducto.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter p_cod_ofi = cmdTransaccionFactory.CreateParameter();
                        p_cod_ofi.ParameterName = "p_cod_ofi";
                        p_cod_ofi.Value = pProducto.cod_oficina;
                        p_cod_ofi.Direction = ParameterDirection.Input;
                        p_cod_ofi.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ofi);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_TRASLADOOFI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoPagaduriasData", "ModificarTrasladoPagadurias", ex);
                        return null;
                    }
                }
            }
        }



        public List<Referencia> referencias(Persona1 pPersona1, long radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<Referencia> listreferencia = new List<Referencia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GEOREFERENCIA  WHERE COD_PERSONA = " + "" + pPersona1.cod_persona + " and numero_radicacion=" + radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referencia entidad = new Referencia();

                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NOMBRE_REFERENCIAS"] != DBNull.Value) entidad.nombrereferencia = Convert.ToString(resultado["NOMBRE_REFERENCIAS"]);
                            if (resultado["TIEMPO_NEGOCIO"] != DBNull.Value) entidad.tiempo = Convert.ToString(resultado["TIEMPO_NEGOCIO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["PROPIETARIO_SI_NO"] != DBNull.Value) entidad.propietario = Convert.ToString(resultado["PROPIETARIO_SI_NO"]);
                            listreferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listreferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 Consultarnegocio(string cod, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT n.cod_negocio,p.identificacion FROM  PERSONA p 
                                        left join negocio n on p.cod_persona=n.cod_persona
                                        WHERE p.cod_persona =" + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_negocio"] != DBNull.Value) entidad.cod_negocio = Convert.ToString(resultado["cod_negocio"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro en la tabla PERSONA de la base de datos por cedula
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERSONA</param>
        /// <returns>Entidad Persona1 consultado</returns>
        public Persona1 ConsultarPersona1Param(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "SELECT tipoidentificacion.descripcion as tipoidentificaciondescripcion, persona.*, persona_biometria.template_huella, persona_afiliacion.estado AS estados FROM  persona INNER JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona LEFT JOIN persona_biometria ON persona.cod_persona = persona_biometria.cod_persona LEFT JOIN tipoidentificacion on persona.tipo_identificacion = tipoidentificacion.codtipoidentificacion ";
                        if (pPersona1.noTraerHuella != null)
                            if (pPersona1.noTraerHuella == 1)
                                sql = "SELECT tipoidentificacion.descripcion as tipoidentificaciondescripcion, persona.*, null AS template_huella, persona_afiliacion.estado AS estados FROM  persona LEFT JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona LEFT JOIN tipoidentificacion on persona.tipo_identificacion = tipoidentificacion.codtipoidentificacion ";
                        if (pPersona1.soloPersona != null)
                            if (pPersona1.soloPersona == 1)
                                sql = "SELECT tipoidentificacion.descripcion as tipoidentificaciondescripcion, persona.*, null AS template_huella, persona.estado AS estados FROM  persona LEFT JOIN tipoidentificacion on persona.tipo_identificacion = tipoidentificacion.codtipoidentificacion";
                        switch (pPersona1.seleccionar)
                        {
                            case "Cod_persona":
                                sql = sql + " WHERE persona.COD_PERSONA = " + pPersona1.cod_persona.ToString();
                                break;
                            case "Identificacion":
                                sql = sql + " WHERE persona.IDENTIFICACION = '" + pPersona1.identificacion.ToString() + "'";
                                break;
                            case "CodNomina":
                                sql = sql + " WHERE persona.COD_NOMINA = '" + pPersona1.cod_nomina_empleado.ToString() + "'";
                                break;
                            case "Codeudor":
                                sql = sql + " WHERE persona.IDENTIFICACION = '" + pPersona1.identificacion.ToString() + "'";
                                break;
                        }

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
                            if (resultado["tipoidentificaciondescripcion"] != DBNull.Value) entidad.tipo_identificacion_descripcion = Convert.ToString(resultado["tipoidentificaciondescripcion"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value)
                                entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            else
                                entidad.codciudadexpedicion = -1;
                            if (resultado["SEXO"] != DBNull.Value)
                                entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            else
                                entidad.sexo = "";
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (entidad.tipo_persona == "N")
                                entidad.nombres = entidad.primer_nombre + " " + entidad.segundo_nombre;
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (entidad.tipo_persona == "N")
                                entidad.apellidos = entidad.primer_apellido + " " + entidad.segundo_apellido;
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (entidad.tipo_persona == "J")
                                entidad.nombres = entidad.razon_social;
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividadStr = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt64(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value)
                                entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            else
                                entidad.tipovivienda = "";
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.ValorArriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barrioResidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dirCorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telCorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value)
                                entidad.ciuCorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            else
                                entidad.ciuCorrespondencia = -1;
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barrioCorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.ActividadEconomicaEmpresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.relacionEmpleadosEmprender = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.CelularEmpresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profecion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.PersonasAcargo = Convert.ToInt32(resultado["NUMPERSONASACARGO"]);
                            if (resultado["NOMBRE_FUNCIONARIO"] != DBNull.Value) entidad.nombre_funcionario = Convert.ToString(resultado["NOMBRE_FUNCIONARIO"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingresoempresa = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["EMPLEADO_ENTIDAD"] != DBNull.Value) entidad.empleado_entidad = Convert.ToInt32(resultado["EMPLEADO_ENTIDAD"]);
                            if (resultado["MUJER_FAMILIA"] != DBNull.Value) entidad.mujer_familia = Convert.ToInt32(resultado["MUJER_FAMILIA"]);
                            if (resultado["JORNADA_LABORAL"] != DBNull.Value) entidad.jornada_laboral = Convert.ToInt32(resultado["JORNADA_LABORAL"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacionApo = Convert.ToInt32(resultado["OCUPACION"]);
                            if (resultado["TEMPLATE_HUELLA"] != DBNull.Value) entidad.template = Convert.ToString(resultado["TEMPLATE_HUELLA"]);
                            if (resultado["ESTADOS"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADOS"]);
                            if (resultado["CODSECTOR"] != DBNull.Value) entidad.sector = Convert.ToInt32(resultado["CODSECTOR"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.zona = Convert.ToInt32(resultado["COD_ZONA"]);
                            if (resultado["NACIONALIDAD"] != DBNull.Value) entidad.nacionalidad = Convert.ToInt64(resultado["NACIONALIDAD"]);
                            if (resultado["UBICACION_RESIDENCIA"] != DBNull.Value) entidad.ubicacion_residencia = Convert.ToInt32(resultado["UBICACION_RESIDENCIA"]);
                            if (resultado["UBICACION_CORRESPONDENCIA"] != DBNull.Value) entidad.ubicacion_correspondencia = Convert.ToInt32(resultado["UBICACION_CORRESPONDENCIA"]);
                            if (resultado["NIT_EMPRESA"] != DBNull.Value) entidad.nit_empresa = Convert.ToInt64(resultado["NIT_EMPRESA"]);
                            if (resultado["TIPO_EMPRESA"] != DBNull.Value) entidad.tipo_empresa = Convert.ToInt32(resultado["TIPO_EMPRESA"]);
                            if (resultado["ACT_CIIU_EMPRESA"] != DBNull.Value) entidad.act_ciiu_empresa = Convert.ToString(resultado["ACT_CIIU_EMPRESA"]);
                            if (resultado["PARENTESCO_MADMINIS"] != DBNull.Value) entidad.parentesco_madminis = Convert.ToInt32(resultado["PARENTESCO_MADMINIS"]);
                            if (resultado["PARENTESCO_MCONTROL"] != DBNull.Value) entidad.parentesco_mcontrol = Convert.ToInt32(resultado["PARENTESCO_MCONTROL"]);
                            if (resultado["PARENTESCO_PEP"] != DBNull.Value) entidad.parentesco_pep = Convert.ToInt32(resultado["PARENTESCO_PEP"]);
                            if (resultado["UBICACION_EMPRESA"] != DBNull.Value) entidad.ubicacion_empresa = Convert.ToInt32(resultado["UBICACION_EMPRESA"]);
                            if (resultado["IDESCALAFON"] != DBNull.Value) entidad.idescalafon = Convert.ToInt32(resultado["IDESCALAFON"]);
                            if (resultado["ACCESO_OFICINA"] != DBNull.Value) entidad.acceso_oficina = Convert.ToInt32(resultado["ACCESO_OFICINA"]);                           
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public bool validadMoroso(Usuario pUsuario, string identificacion)
        {
            DbDataReader resultado;
            DateTime? fecha = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ap.fecha_proximo_pago FROM persona p Inner Join aporte ap ON p.cod_persona = ap.cod_persona Inner Join lineaaporte l On ap.cod_linea_aporte = l.cod_linea_aporte
                                         WHERE p.identificacion = '" + identificacion + "' AND l.tipo_aporte = 1 AND l.obligatorio = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                            if (resultado["fecha_proximo_pago"] != DBNull.Value)
                                fecha = Convert.ToDateTime(resultado["fecha_proximo_pago"]);

                        dbConnectionFactory.CerrarConexion(connection);

                        if (fecha > DateTime.Now)
                        {
                            return true;// el usuario esta en mora 
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return false;
                    }
                }
            }
        }

        public Persona1 ConsultarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        sql = @"SELECT P.* ,A.NOMBRES AS NOMBRE_APP,A.APELLIDOS AS APELLIDOS_APP,A.EMAIL AS EMAIL_APP,C.NOMCIUDAD AS NOMCIUDAD_RESID,
                                        L.NOMCIUDAD AS NOMCIUDAD_LAB , A.CLAVE, T.DESCRIPCION NOM_TIPO_IDENTIFICACION, E.SEMAIL
                                        FROM PERSONA P LEFT JOIN PERSONA_ACCESO A ON A.COD_PERSONA = P.COD_PERSONA 
                                        LEFT JOIN TIPOIDENTIFICACION T ON P.TIPO_IDENTIFICACION = T.CODTIPOIDENTIFICACION
                                        LEFT JOIN CIUDADES C ON C.CODCIUDAD = P.CODCIUDADRESIDENCIA 
                                        LEFT JOIN CIUDADES L ON L.CODCIUDAD = P.CIUDADEMPRESA
                                        LEFT JOIN ASEJECUTIVOS E ON P.COD_ASESOR = E.IUSUARIO ";
                        switch (pPersona1.seleccionar)
                        {
                            case "Cod_persona":
                                sql += " WHERE P.COD_PERSONA = " + pPersona1.cod_persona.ToString();
                                break;
                            case "Identificacion":
                                sql += " WHERE P.IDENTIFICACION = '" + pPersona1.identificacion.ToString() + "'";
                                break;
                            case "CodNomina":
                                sql += " WHERE persona.COD_NOMINA = '" + pPersona1.cod_nomina_empleado.ToString() + "'";
                                break;
                        }

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
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value)
                                entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            else
                                entidad.codciudadexpedicion = -1;
                            if (resultado["SEXO"] != DBNull.Value)
                                entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            else
                                entidad.sexo = "";
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]); else entidad.primer_nombre = "";
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]); else entidad.segundo_nombre = "";
                            if (entidad.tipo_persona == "N")
                                entidad.nombres = entidad.primer_nombre + " " + entidad.segundo_nombre;
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]); else entidad.primer_apellido = "";
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]); else entidad.segundo_apellido = "";
                            if (entidad.tipo_persona == "N")
                                entidad.apellidos = entidad.primer_apellido + " " + entidad.segundo_apellido;
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (entidad.tipo_persona == "J")
                                entidad.nombres = entidad.razon_social;
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimientoAPP = Convert.ToDateTime(resultado["FECHANACIMIENTO"]).ToShortDateString(); else entidad.fechanacimientoAPP = "";
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["CODESTADOCIVIL"] != DBNull.Value) entidad.codestadocivil = Convert.ToInt64(resultado["CODESTADOCIVIL"]);
                            if (resultado["CODESCOLARIDAD"] != DBNull.Value) entidad.codescolaridad = Convert.ToInt64(resultado["CODESCOLARIDAD"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividadStr = Convert.ToString(resultado["CODACTIVIDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]); else entidad.direccion = "";
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]); else entidad.telefono = "";
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]); else entidad.codciudadresidencia = 0;
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedadlugar = Convert.ToInt64(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value)
                                entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            else
                                entidad.tipovivienda = "";
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.ValorArriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]); else entidad.celular = "";
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]); else entidad.email = "";
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]); else entidad.telefonoempresa = "";
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]); else entidad.direccionempresa = "";
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value)
                                entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            else
                                entidad.estado = "";
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barrioResidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dirCorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telCorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value)
                                entidad.ciuCorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            else
                                entidad.ciuCorrespondencia = -1;
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barrioCorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.ActividadEconomicaEmpresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDADEMPRESA"]); else entidad.ciudad = 0;
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.relacionEmpleadosEmprender = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.CelularEmpresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["PROFESION"] != DBNull.Value) entidad.profecion = Convert.ToString(resultado["PROFESION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.PersonasAcargo = Convert.ToInt32(resultado["NUMPERSONASACARGO"]);
                            if (resultado["NOMBRE_FUNCIONARIO"] != DBNull.Value) entidad.nombre_funcionario = Convert.ToString(resultado["NOMBRE_FUNCIONARIO"]);
                            if (resultado["FECHA_INGRESOEMPRESA"] != DBNull.Value) entidad.fecha_ingresoempresa = Convert.ToDateTime(resultado["FECHA_INGRESOEMPRESA"]);
                            if (resultado["EMPLEADO_ENTIDAD"] != DBNull.Value) entidad.empleado_entidad = Convert.ToInt32(resultado["EMPLEADO_ENTIDAD"]);
                            if (resultado["MUJER_FAMILIA"] != DBNull.Value) entidad.mujer_familia = Convert.ToInt32(resultado["MUJER_FAMILIA"]);
                            if (resultado["JORNADA_LABORAL"] != DBNull.Value) entidad.jornada_laboral = Convert.ToInt32(resultado["JORNADA_LABORAL"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacionApo = Convert.ToInt32(resultado["OCUPACION"]);

                            if (resultado["NOMBRE_APP"] != DBNull.Value) entidad.nombre_app = Convert.ToString(resultado["NOMBRE_APP"]); else entidad.nombre_app = "";
                            if (resultado["APELLIDOS_APP"] != DBNull.Value) entidad.apellidos_app = Convert.ToString(resultado["APELLIDOS_APP"]); else entidad.apellidos_app = "";
                            if (resultado["EMAIL_APP"] != DBNull.Value) entidad.email_app = Convert.ToString(resultado["EMAIL_APP"]); else entidad.email_app = "";
                            if (resultado["NOMCIUDAD_RESID"] != DBNull.Value) entidad.nomciudad_resid = Convert.ToString(resultado["NOMCIUDAD_RESID"]); else entidad.nomciudad_resid = "null";
                            if (resultado["NOMCIUDAD_LAB"] != DBNull.Value) entidad.nomciudad_lab = Convert.ToString(resultado["NOMCIUDAD_LAB"]); else entidad.nomciudad_lab = "null";
                            if (resultado["CLAVE"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["CLAVE"]);
                            if (resultado["NOM_TIPO_IDENTIFICACION"] != DBNull.Value) entidad.nomtipo_identificacion = Convert.ToString(resultado["NOM_TIPO_IDENTIFICACION"]);
                            if (resultado["SEMAIL"] != DBNull.Value) entidad.email_asesor = Convert.ToString(resultado["SEMAIL"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value)
                            {
                                entidad.nombre = entidad.primer_nombre + " " + entidad.segundo_nombre + " " + entidad.primer_apellido;
                            }
                            if (entidad.clave != null)
                            {
                                CifradoBusiness SegCifrado = new CifradoBusiness();
                                entidad.clavesinecriptar = SegCifrado.Desencriptar(entidad.clave);
                            }
                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ConsultarPersona1conyuge(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;

                        sql = sql = @"select * from persona "
                                   + "where cod_persona In (SELECT cod_conyuge FROM  conyuge WHERE COD_PERSONA = " + pPersona1.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);

                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value)
                                entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            else
                                entidad.codciudadexpedicion = -1;
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);

                            //informacion laboral
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);

                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["SEXO"] != DBNull.Value)
                                entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            else
                                entidad.sexo = "";
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["CODCIUDADNACIMIENTO"] != DBNull.Value) entidad.codciudadnacimiento = Convert.ToInt64(resultado["CODCIUDADNACIMIENTO"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);

                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1conyuge", ex);
                        return null;
                    }
                }
            }
        }

        public CreditoPlan ConsultarPersona1Paramcred(long radicacion, string identificacion, Usuario pUsuario)
        {
            DbDataReader resultado;

            CreditoPlan entidad = new CreditoPlan();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_REPORTE_CRED_SOLICITUD  WHERE IDENTIFICACION = " + "'" + identificacion + "' and numero_radicacion=" + radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_CLIENTE"] != DBNull.Value) entidad.Cod_persona = Convert.ToInt64(resultado["CODIGO_CLIENTE"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.Numero_Obligacion = Convert.ToInt64(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.Monto = Convert.ToInt64(resultado["MONTO_SOLICITADO"]);
                            if (resultado["PLAZO_SOLICITADO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO_SOLICITADO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["MEDIO"] != DBNull.Value) entidad.Medio = Convert.ToString(resultado["MEDIO"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (entidad.Medio == null)
                            {
                                entidad.Medio = " ";
                            }
                            if (resultado["ESTADOCIVIL"] != DBNull.Value) entidad.EstadoCivil = Convert.ToString(resultado["ESTADOCIVIL"]);
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipo_propiedad = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (entidad.tipo_propiedad == "P")
                            {
                                entidad.tipo_propiedad = "PROPIA";
                            }
                            if (entidad.tipo_propiedad == "A")
                            {
                                entidad.tipo_propiedad = "ARRENDADA";
                            }
                            if (entidad.tipo_propiedad == "F")
                            {
                                entidad.tipo_propiedad = "FAMILIAR";
                            }
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["ANTIGUEDADLUGAR"] != DBNull.Value) entidad.antiguedad = Convert.ToString(resultado["ANTIGUEDADLUGAR"]);
                            if (resultado["TELEFARRENDADOR"] != DBNull.Value) entidad.telefonoarren = Convert.ToString(resultado["TELEFARRENDADOR"]);
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToString(resultado["VALORARRIENDO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["GARANTIAREAL"] != DBNull.Value) entidad.garantiareal = Convert.ToString(resultado["GARANTIAREAL"]);
                            if (entidad.garantiareal == "0")
                            {
                                entidad.garantiareal = "";
                            }
                            if (entidad.garantiareal == "1")
                            {
                                entidad.garantiareal = "X";
                            }
                            if (resultado["GARANTIACOMUNITARIA"] != DBNull.Value) entidad.garantiacom = Convert.ToString(resultado["GARANTIACOMUNITARIA"]);
                            if (entidad.garantiacom == "0")
                            {
                                entidad.garantiacom = "";
                            }
                            if (entidad.garantiacom == "1")
                            {
                                entidad.garantiacom = "X";
                            }
                            if (resultado["POLIZA"] != DBNull.Value) entidad.poliza = Convert.ToString(resultado["POLIZA"]);
                            if (entidad.poliza == "0")
                            {
                                entidad.poliza = "";
                            }
                            if (entidad.poliza == "1")
                            {
                                entidad.poliza = "X";
                            }
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.Observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["EJECUTIVO"] != DBNull.Value) entidad.Ejecutivo = Convert.ToString(resultado["EJECUTIVO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["OFICINA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public InformacionNegocio Consultardatosnegocio(long radicacion, string identificacion, Usuario pUsuario)
        {
            DbDataReader resultado;

            InformacionNegocio entidad = new InformacionNegocio();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_REPORTE_SOLI_NEGOCIO  WHERE IDENTIFICACION = " + "'" + identificacion + "' and numero_radicacion=" + radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NOMBRENEGOCIO"] != DBNull.Value) entidad.nombrenegocio = Convert.ToString(resultado["NOMBRENEGOCIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["PROPIEDAD"] != DBNull.Value) entidad.tipo_propiedad = Convert.ToString(resultado["PROPIEDAD"]);
                            if (entidad.tipo_propiedad == "0")
                            {
                                entidad.tipo_propiedad = "PROPIA";
                            }
                            if (entidad.tipo_propiedad == "1")
                            {
                                entidad.tipo_propiedad = "ARRENDADO";
                            }
                            if (entidad.tipo_propiedad == "2")
                            {
                                entidad.tipo_propiedad = "FAMILIAR";
                            }
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (entidad.arrendador == null)
                            {
                                entidad.arrendador = "";
                            }
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (entidad.telefonoarrendador == null)
                            {
                                entidad.telefonoarrendador = "";
                            }
                            if (resultado["ARRIENDO"] != DBNull.Value) entidad.valor_arriendo = Convert.ToInt64(resultado["ARRIENDO"]);
                            if (resultado["EXPERIENCIA"] != DBNull.Value) entidad.experiencia = Convert.ToDecimal(resultado["EXPERIENCIA"]);
                            if (resultado["ANTIGUEDAD"] != DBNull.Value) entidad.antiguedad = Convert.ToInt64(resultado["ANTIGUEDAD"]);
                            if (resultado["EMPLEADOSPERM"] != DBNull.Value) entidad.emplperm = Convert.ToInt64(resultado["EMPLEADOSPERM"]);
                            if (resultado["EMPLEADOSTEMP"] != DBNull.Value) entidad.empltem = Convert.ToInt64(resultado["EMPLEADOSTEMP"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrioneg = Convert.ToString(resultado["BARRIO"]);
                            if (resultado["ACTIVIDAD"] != DBNull.Value) entidad.descactividad = Convert.ToString(resultado["ACTIVIDAD"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public List<VentasSemanales> ListadoEstacionalidadSemanal(VentasSemanales pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<VentasSemanales> LstVentasSemanales = new List<VentasSemanales>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * from VENTASSEMANALES A INNER JOIN PERSONA B ON A.CODPERSONA=B.COD_PERSONA  WHERE TIPOVENTAS NOT IN(4) AND B.IDENTIFICACION   = " + "'" + pEntidad.identificacion.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            VentasSemanales entidad = new VentasSemanales();

                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventa = Convert.ToString(resultado["TIPOVENTAS"]);

                            if (entidad.tipoventa == "1")
                            {
                                entidad.tipoventa = "VTAS BUENAS";
                            }
                            if (entidad.tipoventa == "2")
                            {
                                entidad.tipoventa = "VTAS REGULARES";
                            }
                            if (entidad.tipoventa == "3")
                            {
                                entidad.tipoventa = "VTAS MALAS";
                            }

                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["LUNES"] != DBNull.Value) entidad.lunesrepo = Convert.ToString(resultado["LUNES"]);
                            if (entidad.lunesrepo == "1")
                            {
                                entidad.lunesrepo = "X";
                            }
                            else
                            {
                                entidad.lunesrepo = " ";
                            }
                            if (resultado["MARTES"] != DBNull.Value) entidad.martesrepo = Convert.ToString(resultado["MARTES"]);

                            if (entidad.martesrepo == "1")
                            {
                                entidad.martesrepo = "X";
                            }
                            else
                            {
                                entidad.martesrepo = " ";
                            }
                            if (resultado["MIERCOLES"] != DBNull.Value) entidad.miercolesrepo = Convert.ToString(resultado["MIERCOLES"]);
                            if (entidad.miercolesrepo == "1")
                            {
                                entidad.miercolesrepo = "X";
                            }
                            else
                            {
                                entidad.miercolesrepo = " ";
                            }
                            if (resultado["JUEVES"] != DBNull.Value) entidad.juevesrepo = Convert.ToString(resultado["JUEVES"]);
                            if (entidad.juevesrepo == "1")
                            {
                                entidad.juevesrepo = "X";
                            }
                            else
                            {
                                entidad.juevesrepo = " ";
                            }
                            if (resultado["VIERNES"] != DBNull.Value) entidad.viernesrepo = Convert.ToString(resultado["VIERNES"]);
                            if (entidad.viernesrepo == "1")
                            {
                                entidad.viernesrepo = "X";
                            }
                            else
                            {
                                entidad.viernesrepo = " ";
                            }
                            if (resultado["SABADOS"] != DBNull.Value) entidad.sabadorepo = Convert.ToString(resultado["SABADOS"]);
                            if (entidad.sabadorepo == "1")
                            {
                                entidad.sabadorepo = "X";
                            }
                            else
                            {
                                entidad.sabadorepo = " ";
                            }
                            if (resultado["DOMINGO"] != DBNull.Value) entidad.domingorepo = Convert.ToString(resultado["DOMINGO"]);
                            if (entidad.domingorepo == "1")
                            {
                                entidad.domingorepo = "X";
                            }
                            else
                            {
                                entidad.domingorepo = " ";
                            }
                            if (resultado["TOTAL"] != DBNull.Value) entidad.totalSemanal = Convert.ToInt64(resultado["TOTAL"]);
                            LstVentasSemanales.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return LstVentasSemanales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoEstacionalidadSemanal", ex);
                        return null;
                    }
                }
            }
        }

        public List<EstacionalidadMensual> ListadoEstacionalidadMensual(EstacionalidadMensual pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<EstacionalidadMensual> LstVentasMensuales = new List<EstacionalidadMensual>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * from VENTASMENSUALES A INNER JOIN PERSONA B ON A.CODPERSONA=B.COD_PERSONA  WHERE TIPOVENTAS NOT IN(4) AND B.IDENTIFICACION   = " + "'" + pEntidad.identificacion.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstacionalidadMensual entidad = new EstacionalidadMensual();

                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventa = Convert.ToString(resultado["TIPOVENTAS"]);

                            if (entidad.tipoventa == "1")
                            {
                                entidad.tipoventa = "VTAS BUENAS";
                            }
                            if (entidad.tipoventa == "2")
                            {
                                entidad.tipoventa = "VTAS REGULARES";
                            }
                            if (entidad.tipoventa == "3")
                            {
                                entidad.tipoventa = "VTAS MALAS";
                            }

                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["ENERO"] != DBNull.Value) entidad.enerorepo = Convert.ToString(resultado["ENERO"]);
                            if (entidad.enerorepo == "1")
                            {
                                entidad.enerorepo = "X";
                            }
                            else
                            {
                                entidad.enerorepo = " ";
                            }
                            if (resultado["FEBRERO"] != DBNull.Value) entidad.febrerorepo = Convert.ToString(resultado["FEBRERO"]);

                            if (entidad.febrerorepo == "1")
                            {
                                entidad.febrerorepo = "X";
                            }
                            else
                            {
                                entidad.febrerorepo = " ";
                            }
                            if (resultado["MARZO"] != DBNull.Value) entidad.marzorepo = Convert.ToString(resultado["MARZO"]);
                            if (entidad.marzorepo == "1")
                            {
                                entidad.marzorepo = "X";
                            }
                            else
                            {
                                entidad.marzorepo = " ";
                            }
                            if (resultado["ABRIL"] != DBNull.Value) entidad.abrilrepo = Convert.ToString(resultado["ABRIL"]);
                            if (entidad.abrilrepo == "1")
                            {
                                entidad.abrilrepo = "X";
                            }
                            else
                            {
                                entidad.abrilrepo = " ";
                            }
                            if (resultado["MAYO"] != DBNull.Value) entidad.mayorepo = Convert.ToString(resultado["MAYO"]);
                            if (entidad.mayorepo == "1")
                            {
                                entidad.mayorepo = "X";
                            }
                            else
                            {
                                entidad.mayorepo = " ";
                            }
                            if (resultado["JUNIO"] != DBNull.Value) entidad.juniorepo = Convert.ToString(resultado["JUNIO"]);
                            if (entidad.juniorepo == "1")
                            {
                                entidad.juniorepo = "X";
                            }
                            else
                            {
                                entidad.juniorepo = " ";
                            }
                            if (resultado["JULIO"] != DBNull.Value) entidad.juliorepo = Convert.ToString(resultado["JULIO"]);
                            if (entidad.juliorepo == "1")
                            {
                                entidad.juliorepo = "X";
                            }
                            else
                            {
                                entidad.juliorepo = " ";
                            }
                            if (resultado["AGOSTO"] != DBNull.Value) entidad.agostorepo = Convert.ToString(resultado["AGOSTO"]);
                            if (entidad.agostorepo == "1")
                            {
                                entidad.agostorepo = "X";
                            }
                            else
                            {
                                entidad.agostorepo = " ";
                            }

                            if (resultado["SEPTIEMBRE"] != DBNull.Value) entidad.septiembrerepo = Convert.ToString(resultado["SEPTIEMBRE"]);
                            if (entidad.septiembrerepo == "1")
                            {
                                entidad.septiembrerepo = "X";
                            }
                            else
                            {
                                entidad.septiembrerepo = " ";
                            }

                            if (resultado["OCTUBRE"] != DBNull.Value) entidad.octubrerepo = Convert.ToString(resultado["OCTUBRE"]);
                            if (entidad.octubrerepo == "1")
                            {
                                entidad.octubrerepo = "X";
                            }
                            else
                            {
                                entidad.octubrerepo = " ";
                            }

                            if (resultado["NOVIEMBRE"] != DBNull.Value) entidad.noviembrerepo = Convert.ToString(resultado["NOVIEMBRE"]);
                            if (entidad.noviembrerepo == "1")
                            {
                                entidad.noviembrerepo = "X";
                            }
                            else
                            {
                                entidad.noviembrerepo = " ";
                            }

                            if (resultado["DICIEMBRE"] != DBNull.Value) entidad.diciembrerepo = Convert.ToString(resultado["DICIEMBRE"]);
                            if (entidad.diciembrerepo == "1")
                            {
                                entidad.diciembrerepo = "X";
                            }
                            else
                            {
                                entidad.diciembrerepo = " ";
                            }
                            if (resultado["TOTAL"] != DBNull.Value) entidad.totalMensual = Convert.ToInt64(resultado["TOTAL"]);
                            LstVentasMensuales.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return LstVentasMensuales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }

        public List<Referncias> ListadoPersonas1ReporteReferencias(Referncias pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            List<Referncias> lstreferencias = new List<Referncias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;

                        sql = "SELECT * FROM  V_Referencias_Solicitud_repo WHERE rownum <=3 and  numero_radicacion = " + pEntidad.numero_radicacion.ToString(); ;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referncias entidad = new Referncias();
                            if (resultado["referencia"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["referencia"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["descvinculo"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descvinculo"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            lstreferencias.Add(entidad);
                        }

                        if (resultado.HasRows == false)
                        {
                            Referncias entidad = new Referncias();
                            entidad.nombres = "";
                            entidad.direccion = "";
                            entidad.telefono = "";
                            entidad.descripcion = "";
                            entidad.ListaDescripcion = "";
                            lstreferencias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstreferencias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1ReporteReferencias", ex);
                        return null;
                    }

                }
            }
        }

        public Referncias ListadoPersonas1ReporteReferencias(Int64 pnumero_credito, string pidentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;

                        sql = "SELECT * FROM  V_Referencias_Solicitud WHERE numero_radicacion = " + pnumero_credito.ToString() + " And identificacion = '" + pidentificacion + "' ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Referncias entidad = new Referncias();
                            if (resultado["referencia"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["referencia"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["descvinculo"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descvinculo"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["TIPOREFERENCIA"]);
                            return entidad;
                        }

                        if (resultado.HasRows == false)
                        {
                            Referncias entidad = new Referncias();
                            entidad.nombres = "";
                            entidad.direccion = "";
                            entidad.telefono = "";
                            entidad.descripcion = "";
                            entidad.ListaDescripcion = "";
                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1ReporteReferencias", ex);
                        return null;
                    }

                }
            }
        }

        public List<Persona1> ListadoPersonas1ReporteCodeudor(Persona1 pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            List<Persona1> lstcodeudores = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;

                        sql = "SELECT * FROM  V_CODEUDORES WHERE numero_radicacion = " + pEntidad.numeroRadicacion.ToString(); ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrioCorresponden = Convert.ToString(resultado["BARRIO"]);
                            lstcodeudores.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1ReporteCodeudor", ex);
                        return null;
                    }

                }
            }
        }

        public List<Persona1> ListadoPersonas1ReporteFamiliares(Persona1 pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            List<Persona1> lstfamiliares = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;

                        sql = "select * from FAMILIARES a  inner join PARENTESCOS b  on a.CODPARENTESCO = b.CODPARENTESCO inner join persona p	on p.COD_PERSONA=a.COD_PERSONA	  WHERE rownum <= 4 and p.identificacion = " + "'" + pEntidad.identificacion.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["ACARGO"] != DBNull.Value) entidad.acargo = Convert.ToString(resultado["ACARGO"]);
                            if (entidad.acargo == "1")

                                entidad.acargo = "SI";
                            else
                                entidad.acargo = "NO";
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.Observaciones = Convert.ToString(resultado["OBSERVACIONES"]);

                            lstfamiliares.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstfamiliares;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1ReporteFamiliares", ex);
                        return null;
                    }

                }
            }
        }

        public List<Persona1> ListadoPersonas1Reporte(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstPersona1 = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // string sql = "Select * from Persona   Where Cod_Persona= " + pPersona1.cod_persona + " Or  Cod_Persona In (Select Co.Cod_Conyuge From Conyuge Co  Inner Join Persona P On P.Cod_Persona = Co.Cod_Persona Where Co.Cod_Persona=" + pPersona1.cod_persona + ")    Or Cod_Persona In (Select Co.Codpersona From Codeudores Co, Persona P,  Credito C Where Co.Numero_Radicacion=C.Numero_Radicacion And Co.Codpersona=P.Cod_Persona and C.Numero_Radicacion= " + pPersona1.numeroRadicacion + ")";
                        string sql = "select p.salario,p.*,t.DESCRIPCION,tc.DESCRIPCION as CONTRATO, c.NOMCIUDAD  from Persona p inner join tipoidentificacion t  on p.TIPO_IDENTIFICACION = t.CODTIPOIDENTIFICACION INNER JOIN CIUDADES c on p.CODCIUDADNACIMIENTO=c.CODCIUDAD left join tipocontrato tc on p.CODTIPOCONTRATO=tc.CODTIPOCONTRATO   Where Cod_Persona= " + pPersona1.cod_persona + " Or  Cod_Persona In (Select Co.Cod_Conyuge From Conyuge Co  Inner Join Persona P On P.Cod_Persona = Co.Cod_Persona Where Co.Cod_Persona=" + pPersona1.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipo_identif = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CODCIUDADEXPEDICION"] != DBNull.Value)
                                entidad.codciudadexpedicion = Convert.ToInt64(resultado["CODCIUDADEXPEDICION"]);
                            else
                                entidad.codciudadexpedicion = -1;
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudadexpedicion = Convert.ToString(resultado["NOMCIUDAD"]);

                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);

                            if (entidad.sexo == "F")
                            {
                                entidad.sexo = "FEMENINO";
                            }
                            if (entidad.sexo == "M")
                            {
                                entidad.sexo = "MASCULINO";
                            }
                            if (resultado["CONTRATO"] != DBNull.Value) entidad.tipocontrato = Convert.ToString(resultado["CONTRATO"]);
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
                            if (resultado["TIPOVIVIENDA"] != DBNull.Value)
                                entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            else
                                entidad.tipovivienda = "";
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value)
                                entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            else
                                entidad.estado = "";
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["BARRESIDENCIA"] != DBNull.Value) entidad.barrioResidencia = Convert.ToInt64(resultado["BARRESIDENCIA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.dirCorrespondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telCorrespondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value)
                                entidad.ciuCorrespondencia = Convert.ToInt64(resultado["CIUCORRESPONDENCIA"]);
                            else
                                entidad.ciuCorrespondencia = -1;
                            if (resultado["BARCORRESPONDENCIA"] != DBNull.Value) entidad.barrioCorrespondencia = Convert.ToInt64(resultado["BARCORRESPONDENCIA"]);
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.ActividadEconomicaEmpresa = Convert.ToInt32(resultado["ACTIVIDADEMPRESA"]);
                            if (resultado["CIUDADEMPRESA"] != DBNull.Value) entidad.ciudad = Convert.ToInt32(resultado["CIUDADEMPRESA"]);
                            if (resultado["PARENTESCOEMPLEADO"] != DBNull.Value) entidad.relacionEmpleadosEmprender = Convert.ToInt32(resultado["PARENTESCOEMPLEADO"]);
                            if (resultado["CELULAREMPRESA"] != DBNull.Value) entidad.CelularEmpresa = Convert.ToString(resultado["CELULAREMPRESA"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.profecion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["ESTRATO"] != DBNull.Value) entidad.Estrato = Convert.ToInt32(resultado["ESTRATO"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.PersonasAcargo = Convert.ToInt32(resultado["NUMPERSONASACARGO"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt32(resultado["ANTIGUEDADLUGAREMPRESA"]);

                            lstPersona1.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERSONA dados unos filtros
        /// </summary>
        /// <param name="pPERSONA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Persona1 obtenidos</returns>
        public List<Persona1> ListarPersona1(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstPersona1 = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        switch (pPersona1.seleccionar)
                        {
                            case "C": //Conyuge
                                sql = "SELECT PERSONA.*, 0 ORDEN FROM PERSONA WHERE cod_persona IN (SELECT COD_CONYUGE FROM CONYUGE WHERE COD_PERSONA = " + pPersona1.cod_persona.ToString() + ")";
                                break;
                            case "CD":
                                sql = "SELECT * FROM PERSONA, CODEUDORES WHERE PERSONA.cod_persona = CODEUDORES.codpersona and CODEUDORES.NUMERO_RADICACION = " + pPersona1.numeroRadicacion;
                                break;
                            case "":
                                sql = "select persona.*, 0 ORDEN from persona";
                                break;

                            case "CDS":
                                sql = "SELECT * FROM PERSONA, SOLICITUDCREDCODEUDORES WHERE PERSONA.cod_persona = SOLICITUDCREDCODEUDORES.codpersona and SOLICITUDCREDCODEUDORES.NUMEROsolicitud= " + pPersona1.numeroRadicacion;
                                break;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.idcodeudor = Convert.ToInt64(resultado["IDCODEUD"]);
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
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.ValorArriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["CODCARGO"] != DBNull.Value) entidad.codcargo = Convert.ToInt64(resultado["CODCARGO"]);
                            if (resultado["CODTIPOCONTRATO"] != DBNull.Value) entidad.codtipocontrato = Convert.ToInt64(resultado["CODTIPOCONTRATO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToInt32(resultado["ORDEN"]);
                            lstPersona1.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public long consultarSolicitud(long radicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT   NUMERO_OBLIGACION FROM CREDITO where NUMERO_RADICACION= " + radicacion;
                        long solicitud = 0;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) solicitud = Convert.ToInt64(resultado["NUMERO_OBLIGACION"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return solicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultaDatosPersona", ex);
                        return 0;
                    }
                }
            }
        }

        public List<Persona1> Listarsolicitudesdecredito(Persona1 pPersona1, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstReferencia = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from V_LISTAR_SOLICITUD_CODEUDORES " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["NUMERO_OBLIGACION"] != DBNull.Value) entidad.NUMERO_OBLIGACION = Convert.ToInt64(resultado["NUMERO_OBLIGACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numeroRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);

                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ListarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona1> ListasBarrios(Int32 ciudad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstBarrios = new List<Persona1>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select codciudad as ListaId, nomciudad as ListaDescripcion From ciudades Where depende_de = " + ciudad + " And tipo = 6 Order By 2 asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]);

                            lstBarrios.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBarrios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ListarReferencia", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene las listas desplegables de la tabla Persona
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstDatosSolicitud = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        string sLinRes = "";
                        string sLinGer = "";
                        string sLinEdu = "";
                        string sLinEmple = "";
                        // Determinar si debe mostrar créditos gerenciales
                        if (ListaSolicitada == "STipoCreditoComercial" || ListaSolicitada == "STipoCreditoVivienda" || ListaSolicitada == "STipoCreditoMicro" || ListaSolicitada == "STipoCreditoConsumo" || ListaSolicitada == "STipoCredito" || ListaSolicitada == "STipoCreditoEmple")
                        {
                            sLinEmple = "1";
                            sLinGer = "1";
                            sLinEdu = "1";
                            ListaSolicitada = ListaSolicitada.Substring(1, ListaSolicitada.Length - 1);
                        }
                        // Si es consulta de líneas de crédito.
                        if (ListaSolicitada == "TipoCreditoComercial" || ListaSolicitada == "TipoCreditoVivienda" || ListaSolicitada == "TipoCreditoMicro" || ListaSolicitada == "TipoCreditoConsumo" || ListaSolicitada == "TipoCredito")
                        {
                            // Determinar líneas de crédito re-estructurados
                            string sqlRES = "Select valor From general Where codigo = 430";
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sqlRES;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (resultado.Read())
                                if (resultado["valor"] != DBNull.Value) sLinRes = Convert.ToString(resultado["valor"]);
                        }

                        string sql = null;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        switch (ListaSolicitada)
                        {
                            case "LineasCredito":
                                sql = "Select cod_linea_credito as ListaId, nombre as ListaDescripcion From lineascredito";
                                break;
                            case "LineasCreditoReestructurado":
                                sql = "Select cod_linea_credito as ListaId, nombre as ListaDescripcion From lineascredito where VERIFLINEAESREESTRUCTURACION(cod_linea_credito) = 1 ";
                                break;
                            case "TipoCredito":
                                sql = "Select cod_linea_credito as ListaId, nombre as ListaDescripcion From lineascredito Where estado = 1 and aplica_asociado = 1 ";
                                if (sLinGer.Trim() == "1")
                                    sql += " And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                if (sLinRes.Trim() != "")
                                    sql = sql + " And cod_clasifica = 1 And tipo_linea != 2 And cod_linea_credito Not In (" + sLinRes + ") ";
                                if (sLinEdu.Trim() == "1")
                                    sql += " And Nvl(lineascredito.educativo, 0) != 1";
                                break;
                            case "TipoCreditoMicro":
                                sql = "Select cod_linea_credito as ListaId, nombre || ' ' || cod_linea_credito as ListaDescripcion from lineascredito where cod_clasifica In (4) and estado = 1 and tipo_linea != 2 and aplica_asociado = 1 ";
                                if (sLinGer.Trim() == "1")
                                    sql += " And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                if (sLinRes.Trim() != "")
                                    sql = sql + " And cod_linea_credito Not In (" + sLinRes + ") ";
                                if (sLinEdu.Trim() == "1")
                                    sql += " And Nvl(lineascredito.educativo, 0) != 1";
                                break;
                            case "TipoCreditoConsumo":
                                sql = "Select cod_linea_credito as ListaId, nombre || ' ' || cod_linea_credito as ListaDescripcion from lineascredito where cod_clasifica = 1 and estado = 1 and tipo_linea != 2 and aplica_asociado = 1 ";
                                if (sLinGer.Trim() == "1")
                                    sql += " And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                if (sLinRes.Trim() != "")
                                    sql = sql + " And cod_linea_credito Not In (" + sLinRes + ") ";
                                if (sLinEdu.Trim() == "1")
                                    sql += " And Nvl(lineascredito.educativo, 0) != 1";
                                break;
                            case "TipoCreditoVivienda":
                                sql = "Select cod_linea_credito as ListaId, nombre || ' ' || cod_linea_credito as ListaDescripcion from lineascredito where cod_clasifica In (3) and estado = 1 and tipo_linea != 2 and aplica_asociado = 1 ";
                                if (sLinGer.Trim() == "1")
                                    sql += " And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                if (sLinRes.Trim() != "")
                                    sql = sql + " And cod_linea_credito Not In (" + sLinRes + ") ";
                                if (sLinEdu.Trim() == "1")
                                    sql += " And Nvl(lineascredito.educativo, 0) != 1";
                                break;
                            case "TipoCreditoComercial":
                                sql = "Select cod_linea_credito as ListaId, nombre || ' ' || cod_linea_credito as ListaDescripcion from lineascredito where cod_clasifica = 2 and estado = 1 and tipo_linea != 2 and aplica_asociado = 1 ";
                                if (sLinGer.Trim() == "1")
                                    sql += " And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                if (sLinRes.Trim() != "")
                                    sql = sql + " And cod_linea_credito Not In (" + sLinRes + ") ";
                                if (sLinEdu.Trim() == "1")
                                    sql += " And Nvl(lineascredito.educativo, 0) != 1";
                                break;
                            case "TipoCreditoEmple":
                                sql = "Select cod_linea_credito as ListaId, nombre || ' ' || cod_linea_credito as ListaDescripcion from lineascredito where aplica_empleado = 1 And Nvl(lineascredito.educativo, 0) != 1 And Nvl(lineascredito.Credito_Gerencial, 0) != 1";
                                break;
                            case "Periodicidad":
                                sql = "select cod_periodicidad as ListaId, descripcion as ListaDescripcion from periodicidad ORDER BY 1";
                                break;
                            case "PeriodicidadCuotaExt":
                                sql = "select numero_dias as ListaId, descripcion as ListaDescripcion from periodicidad ORDER BY 1";
                                break;
                            case "Medio":
                                sql = "select idm as ListaId, descripcion as ListaDescripcion from medios ";
                                break;
                            case "TipoIdentificacion":
                                sql = "select CODTIPOIDENTIFICACION as ListaId, descripcion as ListaDescripcion from TIPOIDENTIFICACION Order by 1";
                                break;
                            case "Lugares":
                                sql = "select CODCIUDAD  as ListaId, NOMCIUDAD as ListaDescripcion from CIUDADES order by NOMCIUDAD asc";
                                break;
                            case "Ciudades":
                                sql = "select CODCIUDAD  as ListaId, NOMCIUDAD as ListaDescripcion from CIUDADES Where tipo = 3 order by NOMCIUDAD asc";
                                break;
                            case "EstadoCivil":
                                sql = "select CODESTADOCIVIL as ListaId,  DESCRIPCION as ListaDescripcion from ESTADOCIVIL ";
                                break;
                            case "NivelEscolaridad":
                                sql = "select CODESCOLARIDAD as ListaId, DESCRIPCION as ListaDescripcion from NIVELESCOLARIDAD ";
                                break;
                            case "Actividad":
                                sql = "select CODACTIVIDAD as ListaId, DESCRIPCION as ListaDescripcion from Actividad ORDER BY DESCRIPCION";
                                break;
                            case "Actividad_Negocio":
                                sql = "select CODACTIVIDADNEGOCIO as ListaId, DESCRIPCION as ListaDescripcion from actividad_negocio ORDER BY DESCRIPCION";
                                break;
                            case "Actividad2":
                                sql = "select CODACTIVIDAD as ListaId, DESCRIPCION as ListaDescripcion from actividad order by CODACTIVIDAD asc";
                                break;
                            case "Actividad_Laboral":
                                sql = "select COD_ACTIVIDADLABORAL as ListaId, DESCRIPCION as ListaDescripcion from ACTIVIDADLABORAL ORDER BY DESCRIPCION";
                                break;
                            case "TipoContrato":
                                sql = "select CODTIPOCONTRATO as ListaId, DESCRIPCION as ListaDescripcion from TipoContrato ";
                                break;
                            case "Contratacion":
                                sql = "Select COD_CONTRATACION as ListaId,TIPO_CONTRATO as ListaDescripcion from Contratacion";
                                break;
                            case "TipoCargo":
                                sql = "select CODCARGO as ListaId, DESCRIPCION as ListaDescripcion from cargo Order by 2";
                                break;
                            case "ESTADO_ACTIVO":
                                sql = "select ESTADO as ListaId, NOMBRE as ListaDescripcion from estado_cliente ";
                                break;
                            case "Barrio":
                                sql = "select codciudad as ListaId, NOMCIUDAD as ListaDescripcion from ciudades where  tipo=6 order by ListaDescripcion asc";
                                break;
                            case "Parentesco":
                                sql = "select codparentesco as ListaId, descripcion as ListaDescripcion from parentescos ORDER BY DESCRIPCION";
                                break;
                            case "TipoLiquidacion":
                                sql = "select tipo_liquidacion as ListaId, descripcion as ListaDescripcion from TipoLiquidacion";
                                break;
                            case "CreditoRotativo":
                                sql = "select cod_linea_credito as ListaId, nombre as ListaDescripcion from lineascredito where estado = 1 and tipo_linea = 2";
                                break;
                            case "Oficinas":
                                sql = "select cod_oficina as ListaId, nombre as ListaDescripcion from  oficina";
                                break;
                            case "EstadoCredito":
                                sql = "select estado as ListaId, Descripcion as ListaDescripcion from  estado_credito";
                                break;
                            case "Sector":
                                sql = "select codsector as ListaId, Descripcion as ListaDescripcion from  sector_economico ORDER BY DESCRIPCION";
                                break;
                            case "Zona":
                                sql = "select cod_zona as ListaId, Descripcion as ListaDescripcion from  zonas ORDER BY DESCRIPCION";
                                break;
                            case "Empresa":
                                sql = "select cod_empresa as ListaId, nom_empresa as ListaDescripcion from  empresa_recaudo";
                                break;
                            case "Asesor":
                                sql = "select iusuario as ListaId, QUITARESPACIOS(Substr(snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2, 0, 240)) as ListaDescripcion from asejecutivos";
                                break;
                            case "TipoDirectivo":
                                sql = "select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from tipo_directivo ";
                                break;
                            case "NominaEmpleado":
                                sql = "select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from nomina_empleado ";
                                break;
                            case "LineaAhorro":
                                sql = "SELECT cod_linea_ahorro as ListaId, descripcion as ListaDescripcion FROM LineaAhorro ";
                                break;
                            case "TipoComprobante":
                                sql = "SELECT Tipo_Comp as ListaId, Tipo_Comp ||'-'|| Descripcion as ListaDescripcion FROM tipo_comp ORDER BY Tipo_Comp";
                                break;
                            case "BancosEntidad":
                                sql = "SELECT cod_banco ListaId, nombrebanco as ListaDescripcion FROM V_BANCOS_ENTIDAD ";
                                break;
                            case "Bancos":
                                sql = "SELECT cod_banco ListaId, nombrebanco as ListaDescripcion FROM BANCOS ";
                                break;
                            case "CuentaBancariasBancos":
                                sql = "SELECT CU.IDCTABANCARIA as ListaId, CU.NUM_CUENTA || '-' || B.NOMBREBANCO as ListaDescripcion FROM BANCOS B INNER JOIN CUENTA_BANCARIA CU ON CU.COD_BANCO = B.COD_BANCO where CU.ESTADO = '1' order by B.NOMBREBANCO,CU.NUM_CUENTA ";
                                break;
                            case "ClasificacionCreditos":
                                sql = " select cod_clasifica as ListaId, descripcion as ListaDescripcion from clasificacion order by cod_clasifica ";
                                break;
                            case "ConceptoNomina":
                                sql = " SELECT CONSECUTIVO ListaId, DESCRIPCION as ListaDescripcion FROM CONCEPTO_NOMINA ";
                                break;
                            case "ConceptoNomina1":
                                sql = " SELECT CONSECUTIVO ListaId, DESCRIPCION as ListaDescripcion FROM CONCEPTO_NOMINA where TIPO=1";
                                break;
                            case "ConceptoNomina2":
                                sql = " SELECT CONSECUTIVO ListaId, DESCRIPCION as ListaDescripcion FROM CONCEPTO_NOMINA WHERE TIPO=2";
                                break;
                            case "AsociadosEspeciales":
                                sql = " SELECT COD_PERFIL ListaId, DESCRIPCION as ListaDescripcion FROM GR_PERFIL_RIESGO ";
                                break;
                            case "Segmentos":
                                sql = " select codsegmento as ListaId, nombre as ListaDescripcion from segmentos Order by tipo_variable, calificacion_segmento ";
                                break;
                            case "CentroCostos":
                                sql = " select CENTRO_COSTO as ListaId, DESCRIPCION as ListaDescripcion from CENTRO_COSTO ";
                                break;
                            case "Sexo":
                                return new List<Persona1> { new Persona1 { ListaDescripcion = "Hombre", ListaIdStr = "1" }, new Persona1 { ListaDescripcion = "Mujer", ListaIdStr = "0" } };
                            case "FondoSalud":
                                sql = " select CONSECUTIVO as ListaId, NOM_PERSONA as ListaDescripcion from nomina_entidad where clase = 1 ";
                                break;
                            case "FondoPension":
                                sql = " select CONSECUTIVO as ListaId, NOM_PERSONA as ListaDescripcion from nomina_entidad where clase = 2 ";
                                break;
                            case "FondoCesantias":
                                sql = " select CONSECUTIVO as ListaId, NOM_PERSONA as ListaDescripcion from nomina_entidad where clase = 3 ";
                                break;
                            case "FondoARL":
                                sql = " select CONSECUTIVO as ListaId, NOM_PERSONA as ListaDescripcion from nomina_entidad where clase = 4 ";
                                break;
                            case "CajaCompensacion":
                                sql = " select CONSECUTIVO as ListaId, NOMBRE as ListaDescripcion from CajaCompensacion ";
                                break;
                            case "TipoCotizante":
                                sql = " select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from TipoCotizante ";
                                break;
                            case "FormaPago":
                                sql = " select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from FormaPago ";
                                break;
                            case "TipoConceptoNomina":
                                sql = " select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from tipoconcepto_nomina ";
                                break;
                            case "TipoNovedadPrima":
                                sql = " select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from TipoNovedadPrima ";
                                break;
                            case "TipoRetiroContrato":
                                sql = " select CONSECUTIVO as ListaId, Descripcion as ListaDescripcion from TipoRetiroContrato ";
                                break;
                            case "Area":
                                sql = " select IdArea as ListaId,Nombre as ListaDescripcion from Area";
                                break;
                            case "TipoCuotaExtra":
                                sql = " select IDTIPO as ListaId,Descripcion as ListaDescripcion from TIPO_CUOTAS_EXTRAS";
                                break;
                            case "TipoRiesgoArl":
                                sql = " select consecutivo as ListaId,DESCRIPCION as ListaDescripcion from PORCENTAJESARL";
                                break;

                            case "TipoCuentasXpagar":
                                sql = " select COD_TIPOCUENTA as ListaId,DESCRIPCION as ListaDescripcion from TIPOSCUENTASXPAGAR";
                                break;
                            case "TipoEstadoFinanciero":
                                sql = " select CODIGO as ListaId,DESCRIPCION as ListaDescripcion from TIPO_EST_FINAN_NIIF";
                                break;

                            case "Concepto_Niif":
                                sql = " select CODIGO as ListaId,DESCRIPCION as ListaDescripcion from CONCEPTOS_NIIF ORDER BY 1 ";
                                break;

                            case "TipoPersona":
                                Persona1 entidadt1 = new Persona1();
                                entidadt1.ListaIdStr = "N";
                                entidadt1.ListaDescripcion = "NATURAL";
                                lstDatosSolicitud.Add(entidadt1);
                                Persona1 entidadt2 = new Persona1();
                                entidadt2.ListaIdStr = "J";
                                entidadt2.ListaDescripcion = "JURIDICA";
                                lstDatosSolicitud.Add(entidadt2);
                                break;
                            case "TipoCliente":
                                Persona1 entidad1 = new Persona1();
                                entidad1.ListaIdStr = "A";
                                entidad1.ListaDescripcion = "Activo";
                                lstDatosSolicitud.Add(entidad1);
                                Persona1 entidad2 = new Persona1();
                                entidad2.ListaIdStr = "R";
                                entidad2.ListaDescripcion = "Retirado";
                                lstDatosSolicitud.Add(entidad2);
                                break;
                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (ListaSolicitada == "LineasCredito" || ListaSolicitada == "TipoCreditoMicro" || ListaSolicitada == "TipoCreditoConsumo" || ListaSolicitada == "TipoCredito" || ListaSolicitada == "TipoCreditoEmple" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares" || ListaSolicitada == "ESTADO_ACTIVO" || ListaSolicitada == "Actividad2" || ListaSolicitada == "Actividad" || ListaSolicitada == "Actividad_Negocio")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]); }
                            else
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); }
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]);
                            lstDatosSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClienteData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona1> ListadoPersonas1(Persona1 pPersona1, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstPersona1 = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        sql = "Select v_persona.*, tipoidentificacion.descripcion As nomtipo_identificacion From v_persona Left Join tipoidentificacion On v_persona.tipo_identificacion = tipoidentificacion.codtipoidentificacion " + ObtenerFiltro(pPersona1, "v_persona.");

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMTIPO_IDENTIFICACION"] != DBNull.Value) entidad.nomtipo_identificacion = Convert.ToString(resultado["NOMTIPO_IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CIUDADRESIDENCIA"] != DBNull.Value) entidad.nomciudad_resid = Convert.ToString(resultado["CIUDADRESIDENCIA"]);
                            
                            lstPersona1.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona1> ListarddLinea(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstReferencia = new List<Persona1>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select l.cod_linea_credito, l.nombre from lineascredito l " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();

                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_linea_credito"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["nombre"]);

                            lstReferencia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferencia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ListarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ConsultaDatosPersona(string pidentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 Persona1 = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from v_persona where identificacion = '" + pidentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) Persona1.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) Persona1.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) Persona1.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) Persona1.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) Persona1.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) Persona1.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) Persona1.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) Persona1.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) Persona1.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) Persona1.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) Persona1.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) Persona1.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) Persona1.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRE"] != DBNull.Value) Persona1.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["EMAIL"] != DBNull.Value) Persona1.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["CIUDADRESIDENCIA"] != DBNull.Value) Persona1.nomciudad_resid = Convert.ToString(resultado["CIUDADRESIDENCIA"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Persona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultaDatosPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ConsultaDatosPersona(Int64 pCodPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 Persona1 = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select p.*, t.descripcion As nomtipo_identificacion From v_persona p Left Join tipoidentificacion t On p.tipo_identificacion = t.codtipoidentificacion Where p.cod_persona = " + pCodPersona.ToString() + " ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) Persona1.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) Persona1.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) Persona1.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) Persona1.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) Persona1.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) Persona1.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) Persona1.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) Persona1.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) Persona1.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) Persona1.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) Persona1.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) Persona1.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) Persona1.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NOMBRE"] != DBNull.Value) Persona1.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CIUDADRESIDENCIA"] != DBNull.Value) Persona1.nomciudad_resid = Convert.ToString(resultado["CIUDADRESIDENCIA"]);
                            if (resultado["ESTADO"] != DBNull.Value) Persona1.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMTIPO_IDENTIFICACION"] != DBNull.Value) Persona1.nomtipo_identificacion = Convert.ToString(resultado["NOMTIPO_IDENTIFICACION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Persona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultaDatosPersona", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado grabacion de persona

        public Persona1 CrearPersonaAporte(Persona1 pPersona, Usuario vUsuario, int opcion)
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
                        if (opcion == 1)
                            pcod_persona.Direction = ParameterDirection.Output;
                        else
                            pcod_persona.Direction = ParameterDirection.Input;
                        //pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pPersona.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pPersona.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        //pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pdigito_verificacion = cmdTransaccionFactory.CreateParameter();
                        pdigito_verificacion.ParameterName = "p_digito_verificacion";
                        pdigito_verificacion.Value = pPersona.digito_verificacion;
                        pdigito_verificacion.Direction = ParameterDirection.Input;
                        //pdigito_verificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdigito_verificacion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        ptipo_identificacion.Value = pPersona.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        //ptipo_identificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pfechaexpedicion = cmdTransaccionFactory.CreateParameter();
                        pfechaexpedicion.ParameterName = "p_fechaexpedicion";
                        if (pPersona.fechaexpedicion != DateTime.MinValue) pfechaexpedicion.Value = pPersona.fechaexpedicion; else pfechaexpedicion.Value = DBNull.Value;
                        pfechaexpedicion.Direction = ParameterDirection.Input;
                        //pfechaexpedicion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaexpedicion);

                        DbParameter pcodciudadexpedicion = cmdTransaccionFactory.CreateParameter();
                        pcodciudadexpedicion.ParameterName = "p_codciudadexpedicion";
                        if (pPersona.codciudadexpedicion == null)
                            pcodciudadexpedicion.Value = DBNull.Value;
                        else
                            pcodciudadexpedicion.Value = pPersona.codciudadexpedicion;
                        pcodciudadexpedicion.Direction = ParameterDirection.Input;
                        //pcodciudadexpedicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadexpedicion);

                        DbParameter psexo = cmdTransaccionFactory.CreateParameter();
                        psexo.ParameterName = "p_sexo";
                        if (pPersona.sexo != null) psexo.Value = pPersona.sexo; else psexo.Value = DBNull.Value;
                        psexo.Direction = ParameterDirection.Input;
                        //psexo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psexo);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        pprimer_nombre.Value = pPersona.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        //pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pPersona.segundo_nombre != null) psegundo_nombre.Value = pPersona.segundo_nombre; else psegundo_nombre.Value = DBNull.Value;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        //psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        pprimer_apellido.Value = pPersona.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        //pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pPersona.segundo_apellido != null) psegundo_apellido.Value = pPersona.segundo_apellido; else psegundo_apellido.Value = DBNull.Value;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        //psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter prazon_social = cmdTransaccionFactory.CreateParameter();
                        prazon_social.ParameterName = "p_razon_social";
                        prazon_social.Value = pPersona.razon_social;
                        prazon_social.Direction = ParameterDirection.Input;
                        //prazon_social.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(prazon_social);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pPersona.fechanacimiento == null)
                            pfechanacimiento.Value = DBNull.Value;
                        else
                            pfechanacimiento.Value = pPersona.fechanacimiento;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        //pfechanacimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        DbParameter pcodciudadnacimiento = cmdTransaccionFactory.CreateParameter();
                        pcodciudadnacimiento.ParameterName = "p_codciudadnacimiento";
                        if (pPersona.codciudadnacimiento == null)
                            pcodciudadnacimiento.Value = DBNull.Value;
                        else
                            pcodciudadnacimiento.Value = pPersona.codciudadnacimiento;
                        pcodciudadnacimiento.Direction = ParameterDirection.Input;
                        //pcodciudadnacimiento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadnacimiento);

                        DbParameter pcodestadocivil = cmdTransaccionFactory.CreateParameter();
                        pcodestadocivil.ParameterName = "p_codestadocivil";
                        if (pPersona.codestadocivil != null) pcodestadocivil.Value = pPersona.codestadocivil; else pcodestadocivil.Value = DBNull.Value;
                        pcodestadocivil.Direction = ParameterDirection.Input;
                        //pcodestadocivil.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodestadocivil);

                        DbParameter pcodescolaridad = cmdTransaccionFactory.CreateParameter();
                        pcodescolaridad.ParameterName = "p_codescolaridad";
                        pcodescolaridad.Value = pPersona.codescolaridad;
                        pcodescolaridad.Direction = ParameterDirection.Input;
                        //pcodescolaridad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodescolaridad);

                        DbParameter pcodactividad = cmdTransaccionFactory.CreateParameter();
                        pcodactividad.ParameterName = "p_codactividad";
                        if (pPersona.codactividadStr != null) pcodactividad.Value = pPersona.codactividadStr; else pcodactividad.Value = DBNull.Value;
                        pcodactividad.Direction = ParameterDirection.Input;
                        //pcodactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodactividad);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pPersona.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        //pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pPersona.telefono != null) ptelefono.Value = pPersona.telefono; else ptelefono.Value = DBNull.Value;
                        ptelefono.Direction = ParameterDirection.Input;
                        //ptelefono.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pcodciudadresidencia = cmdTransaccionFactory.CreateParameter();
                        pcodciudadresidencia.ParameterName = "p_codciudadresidencia";
                        if (pPersona.codciudadresidencia != null) pcodciudadresidencia.Value = pPersona.codciudadresidencia; else pcodciudadresidencia.Value = DBNull.Value;
                        pcodciudadresidencia.Direction = ParameterDirection.Input;
                        //pcodciudadresidencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadresidencia);

                        DbParameter pantiguedadlugar = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlugar.ParameterName = "p_antiguedadlugar";
                        pantiguedadlugar.Value = pPersona.antiguedadlugar;
                        pantiguedadlugar.Direction = ParameterDirection.Input;
                        //pantiguedadlugar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlugar);

                        DbParameter ptipovivienda = cmdTransaccionFactory.CreateParameter();
                        ptipovivienda.ParameterName = "p_tipovivienda";
                        if (pPersona.tipovivienda != null) ptipovivienda.Value = pPersona.tipovivienda; else ptipovivienda.Value = DBNull.Value;
                        ptipovivienda.Direction = ParameterDirection.Input;
                        //ptipovivienda.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipovivienda);

                        DbParameter parrendador = cmdTransaccionFactory.CreateParameter();
                        parrendador.ParameterName = "p_arrendador";
                        if (pPersona.arrendador != null) parrendador.Value = pPersona.arrendador; else parrendador.Value = DBNull.Value;
                        parrendador.Direction = ParameterDirection.Input;
                        //parrendador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(parrendador);

                        DbParameter ptelefonoarrendador = cmdTransaccionFactory.CreateParameter();
                        ptelefonoarrendador.ParameterName = "p_telefonoarrendador";
                        if (pPersona.telefonoarrendador != null) ptelefonoarrendador.Value = pPersona.telefonoarrendador; else ptelefonoarrendador.Value = DBNull.Value;
                        ptelefonoarrendador.Direction = ParameterDirection.Input;
                        //ptelefonoarrendador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoarrendador);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (pPersona.celular != null) pcelular.Value = pPersona.celular; else pcelular.Value = DBNull.Value;
                        pcelular.Direction = ParameterDirection.Input;
                        //pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pPersona.email != null) pemail.Value = pPersona.email; else pemail.Value = DBNull.Value;
                        pemail.Direction = ParameterDirection.Input;
                        //pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (pPersona.empresa != null) pempresa.Value = pPersona.empresa; else pempresa.Value = DBNull.Value;
                        pempresa.Direction = ParameterDirection.Input;
                        //pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter ptelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        ptelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona.telefonoempresa != null) ptelefonoempresa.Value = pPersona.telefonoempresa; else ptelefonoempresa.Value = DBNull.Value;
                        ptelefonoempresa.Direction = ParameterDirection.Input;
                        //ptelefonoempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoempresa);

                        DbParameter pcodcargo = cmdTransaccionFactory.CreateParameter();
                        pcodcargo.ParameterName = "p_codcargo";
                        if (pPersona.codcargo != 0) pcodcargo.Value = pPersona.codcargo; else pcodcargo.Value = DBNull.Value;
                        pcodcargo.Direction = ParameterDirection.Input;
                        //pcodcargo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodcargo);

                        DbParameter pcodtipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodtipocontrato.ParameterName = "p_codtipocontrato";
                        if (pPersona.codtipocontrato != 0) pcodtipocontrato.Value = pPersona.codtipocontrato; else pcodtipocontrato.Value = DBNull.Value;
                        pcodtipocontrato.Direction = ParameterDirection.Input;
                        //pcodtipocontrato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipocontrato);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pPersona.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        //pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_asesor = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor.ParameterName = "p_cod_asesor";
                        pcod_asesor.Value = pPersona.cod_asesor;
                        pcod_asesor.Direction = ParameterDirection.Input;
                        //pcod_asesor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor);

                        DbParameter presidente = cmdTransaccionFactory.CreateParameter();
                        presidente.ParameterName = "p_residente";
                        if (pPersona.residente != null) presidente.Value = pPersona.residente; else presidente.Value = DBNull.Value;
                        presidente.Direction = ParameterDirection.Input;
                        //presidente.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(presidente);

                        DbParameter pfecha_residencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_residencia.ParameterName = "p_fecha_residencia";
                        if (pPersona.fecha_residencia != DateTime.MinValue) pfecha_residencia.Value = pPersona.fecha_residencia; else pfecha_residencia.Value = DBNull.Value;
                        pfecha_residencia.Direction = ParameterDirection.Input;
                        //pfecha_residencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_residencia);

                        DbParameter ptratamiento = cmdTransaccionFactory.CreateParameter();
                        ptratamiento.ParameterName = "p_tratamiento";
                        if (pPersona.tratamiento != null) ptratamiento.Value = pPersona.tratamiento; else ptratamiento.Value = DBNull.Value;
                        ptratamiento.Direction = ParameterDirection.Input;
                        ptratamiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptratamiento);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pPersona.estado != null) pestado.Value = pPersona.estado; else pestado.Value = DBNull.Value;
                        pestado.Direction = ParameterDirection.Input;
                        //pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pvalorarriendo = cmdTransaccionFactory.CreateParameter();
                        pvalorarriendo.ParameterName = "p_valorarriendo";
                        if (pPersona.ValorArriendo != 0) pvalorarriendo.Value = pPersona.ValorArriendo; else pvalorarriendo.Value = DBNull.Value;
                        pvalorarriendo.Direction = ParameterDirection.Input;
                        //pvalorarriendo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvalorarriendo);

                        DbParameter pdireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pdireccionempresa.ParameterName = "p_direccionempresa";
                        if (pPersona.direccionempresa != null) pdireccionempresa.Value = pPersona.direccionempresa; else pdireccionempresa.Value = DBNull.Value;
                        pdireccionempresa.Direction = ParameterDirection.Input;
                        //pdireccionempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccionempresa);

                        DbParameter pantiguedadlugarempresa = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlugarempresa.ParameterName = "p_antiguedadlugarempresa";
                        pantiguedadlugarempresa.Value = pPersona.antiguedadlugarempresa;
                        pantiguedadlugarempresa.Direction = ParameterDirection.Input;
                        //pantiguedadlugarempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlugarempresa);

                        DbParameter pbarresidencia = cmdTransaccionFactory.CreateParameter();
                        pbarresidencia.ParameterName = "p_barresidencia";
                        if (pPersona.barrioResidencia != 0) pbarresidencia.Value = pPersona.barrioResidencia; else pbarresidencia.Value = DBNull.Value;
                        pbarresidencia.Direction = ParameterDirection.Input;
                        //pbarresidencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pbarresidencia);

                        DbParameter pdircorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pdircorrespondencia.ParameterName = "p_dircorrespondencia";
                        if (pPersona.dirCorrespondencia != "" && pPersona.dirCorrespondencia != null) pdircorrespondencia.Value = pPersona.dirCorrespondencia; else pdircorrespondencia.Value = DBNull.Value;
                        pdircorrespondencia.Direction = ParameterDirection.Input;
                        //pdircorrespondencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdircorrespondencia);

                        DbParameter ptelcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        ptelcorrespondencia.ParameterName = "p_telcorrespondencia";
                        if (pPersona.telCorrespondencia != "" && pPersona.telCorrespondencia != null) ptelcorrespondencia.Value = pPersona.telCorrespondencia; else ptelcorrespondencia.Value = DBNull.Value;
                        ptelcorrespondencia.Direction = ParameterDirection.Input;
                        //ptelcorrespondencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelcorrespondencia);

                        DbParameter pciucorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pciucorrespondencia.ParameterName = "p_ciucorrespondencia";
                        pciucorrespondencia.Value = pPersona.ciuCorrespondencia;
                        pciucorrespondencia.Direction = ParameterDirection.Input;
                        //pciucorrespondencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciucorrespondencia);

                        DbParameter pbarcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pbarcorrespondencia.ParameterName = "p_barcorrespondencia";
                        pbarcorrespondencia.Value = pPersona.barrioCorrespondencia;
                        pbarcorrespondencia.Direction = ParameterDirection.Input;
                        //pbarcorrespondencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pbarcorrespondencia);

                        //DbParameter pcod_identificacion = cmdTransaccionFactory.CreateParameter();
                        //pcod_identificacion.ParameterName = "p_identificacion";
                        //pcod_identificacion.Value = pPersona.identificacion;
                        //pcod_identificacion.DbType = DbType.AnsiStringFixedLength;
                        //pcod_identificacion.Size = 50;
                        //pcod_identificacion.Direction = ParameterDirection.InputOutput;
                        //cmdTransaccionFactory.Parameters.Add(pcod_identificacion);

                        //DbParameter pcod_nomina = cmdTransaccionFactory.CreateParameter();
                        //pcod_nomina.ParameterName = "P_CODIGO_NOMINA";
                        //pcod_nomina.Value = pPersona.cod_nomina;
                        //pcod_nomina.DbType = DbType.AnsiStringFixedLength;
                        //pcod_nomina.Size = 50;
                        //pcod_nomina.Direction = ParameterDirection.InputOutput;
                        //cmdTransaccionFactory.Parameters.Add(pcod_nomina);

                        //DbParameter p_salario = cmdTransaccionFactory.CreateParameter();
                        //p_salario.ParameterName = "p_salario";
                        //p_salario.Value = pPersona.salario;
                        //p_salario.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(p_salario);

                        //DbParameter pcuota_aporte = cmdTransaccionFactory.CreateParameter();
                        //pcuota_aporte.ParameterName = "p_cuota_aporte";
                        //pcuota_aporte.Value = pPersona.cuota;
                        //pcuota_aporte.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(pcuota_aporte);

                        //DbParameter pnumhijos = cmdTransaccionFactory.CreateParameter();
                        //pnumhijos.ParameterName = "p_numhijos";
                        //pnumhijos.Value = pPersona.numhijos;
                        //pnumhijos.Direction = ParameterDirection.Input;
                        //pnumhijos.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(pnumhijos);

                        DbParameter pnumpersonasacargo = cmdTransaccionFactory.CreateParameter();
                        pnumpersonasacargo.ParameterName = "p_numpersonasacargo";
                        if (pPersona.PersonasAcargo != null) pnumpersonasacargo.Value = pPersona.PersonasAcargo; else pnumpersonasacargo.Value = DBNull.Value;
                        pnumpersonasacargo.Direction = ParameterDirection.Input;
                        //pnumpersonasacargo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumpersonasacargo);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pPersona.profecion != null) pprofesion.Value = pPersona.profecion; else pprofesion.Value = DBNull.Value;
                        pprofesion.Direction = ParameterDirection.Input;
                        //pocupacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);
                       
                        DbParameter pantiguedadlaboral = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlaboral.ParameterName = "p_antiguedadlaboral";
                        pantiguedadlaboral.Value = pPersona.antiguedadlugarempresa;
                        pantiguedadlaboral.Direction = ParameterDirection.Input;
                        //pantiguedadlaboral.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlaboral);

                        DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                        pestrato.ParameterName = "p_estrato";
                        if (pPersona.Estrato != null) pestrato.Value = pPersona.Estrato; else pestrato.Value = DBNull.Value;
                        pestrato.Direction = ParameterDirection.Input;
                        //pestrato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestrato);

                        DbParameter pcelularempresa = cmdTransaccionFactory.CreateParameter();
                        pcelularempresa.ParameterName = "p_celularempresa";
                        if (pPersona.CelularEmpresa != null && pPersona.CelularEmpresa != "") pcelularempresa.Value = pPersona.CelularEmpresa; else pcelularempresa.Value = DBNull.Value;
                        pcelularempresa.Direction = ParameterDirection.Input;
                        pcelularempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelularempresa);

                        DbParameter pciudadempresa = cmdTransaccionFactory.CreateParameter();
                        pciudadempresa.ParameterName = "p_ciudadempresa";
                        if (pPersona.ciudad != null) pciudadempresa.Value = pPersona.ciudad; else pciudadempresa.Value = DBNull.Value;
                        pciudadempresa.Direction = ParameterDirection.Input;
                        //pciudadempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pciudadempresa);

                        //DbParameter pposicionempresa = cmdTransaccionFactory.CreateParameter();
                        //pposicionempresa.ParameterName = "p_posicionempresa";
                        //pposicionempresa.Value = pPersona.posicionempresa;
                        //pposicionempresa.Direction = ParameterDirection.Input;
                        //pposicionempresa.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(pposicionempresa);

                        DbParameter pactividadempresa = cmdTransaccionFactory.CreateParameter();
                        pactividadempresa.ParameterName = "p_actividadempresa";
                        if (pPersona.ActividadEconomicaEmpresaStr != null) pactividadempresa.Value = pPersona.ActividadEconomicaEmpresaStr; else pactividadempresa.Value = DBNull.Value;
                        pactividadempresa.Direction = ParameterDirection.Input;
                        //pactividadempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pactividadempresa);

                        DbParameter pparentescoempleado = cmdTransaccionFactory.CreateParameter();
                        pparentescoempleado.ParameterName = "p_parentescoempleado";
                        if (pPersona.relacionEmpleadosEmprender != 0) pparentescoempleado.Value = pPersona.relacionEmpleadosEmprender; else pparentescoempleado.Value = DBNull.Value;
                        pparentescoempleado.Direction = ParameterDirection.Input;
                        //pparentescoempleado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentescoempleado);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pPersona.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        //pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        if (pPersona.usuariocreacion != null) pusuariocreacion.Value = pPersona.usuariocreacion; else pusuariocreacion.Value = DBNull.Value;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        //pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (opcion == 1)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pPersona.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        //pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        if (opcion == 1)
                            pusuultmod.Value = DBNull.Value;
                        else
                            pusuultmod.Value = pPersona.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        //pusuultmod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        DbParameter pnombre_funcionario = cmdTransaccionFactory.CreateParameter();
                        pnombre_funcionario.ParameterName = "p_nombre_funcionario";
                        if (pPersona.nombre_funcionario != null) pnombre_funcionario.Value = pPersona.nombre_funcionario; else pnombre_funcionario.Value = DBNull.Value;
                        pnombre_funcionario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnombre_funcionario);

                        DbParameter pfecha_ingresoempresa = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingresoempresa.ParameterName = "p_fecha_ingresoempresa";
                        if (pPersona.fecha_ingresoempresa != DateTime.MinValue) pfecha_ingresoempresa.Value = pPersona.fecha_ingresoempresa; else pfecha_ingresoempresa.Value = DBNull.Value;
                        pfecha_ingresoempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingresoempresa);

                        DbParameter pcod_empleado = cmdTransaccionFactory.CreateParameter();
                        pcod_empleado.ParameterName = "p_codigo_nomina";
                        if (pPersona.cod_nomina_empleado == null)
                        {
                            pcod_empleado.Value = DBNull.Value;
                        }
                        else
                        {
                            pcod_empleado.Value = pPersona.cod_nomina_empleado;
                        }
                        pcod_empleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_empleado);

                        DbParameter pempleado_entidad = cmdTransaccionFactory.CreateParameter();
                        pempleado_entidad.ParameterName = "p_empleado_entidad";
                        pempleado_entidad.Value = pPersona.empleado_entidad;
                        pempleado_entidad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pempleado_entidad);

                        DbParameter pmujer_familia = cmdTransaccionFactory.CreateParameter();
                        pmujer_familia.ParameterName = "p_mujer_familia";
                        if (pPersona.mujer_familia != -1) pmujer_familia.Value = pPersona.mujer_familia; else pmujer_familia.Value = DBNull.Value;
                        pmujer_familia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pmujer_familia);

                        DbParameter pjornada_laboral = cmdTransaccionFactory.CreateParameter();
                        pjornada_laboral.ParameterName = "p_jornada_laboral";
                        pjornada_laboral.Value = pPersona.jornada_laboral;
                        pjornada_laboral.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pjornada_laboral);

                        DbParameter pocupacion = cmdTransaccionFactory.CreateParameter();
                        pocupacion.ParameterName = "p_ocupacion";
                        if (pPersona.ocupacionApo != 0) pocupacion.Value = pPersona.ocupacionApo; else pocupacion.Value = DBNull.Value;
                        pocupacion.Direction = ParameterDirection.Input;
                        //pocupacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pocupacion);

                        DbParameter escalafon = cmdTransaccionFactory.CreateParameter();
                        escalafon.ParameterName = "P_IDESCALAFON";
                        if (pPersona.idescalafon == 0) escalafon.Value = DBNull.Value; else escalafon.Value = pPersona.idescalafon;
                        escalafon.Direction = ParameterDirection.Input;
                        //pocupacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(escalafon);

                        DbParameter pcod_sector = cmdTransaccionFactory.CreateParameter();
                        pcod_sector.ParameterName = "p_cod_sector";
                        if (pPersona.sector != null) pcod_sector.Value = pPersona.sector; else pcod_sector.Value = DBNull.Value;
                        pcod_sector.Direction = ParameterDirection.Input;
                        pcod_sector.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_sector);

                        DbParameter pcod_zona = cmdTransaccionFactory.CreateParameter();
                        pcod_zona.ParameterName = "p_cod_zona";
                        if (pPersona.zona != null) pcod_zona.Value = pPersona.zona; else pcod_zona.Value = DBNull.Value;
                        pcod_zona.Direction = ParameterDirection.Input;
                        pcod_zona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_zona);

                        DbParameter pnacionalidad = cmdTransaccionFactory.CreateParameter();
                        pnacionalidad.ParameterName = "p_nacionalidad";
                        if (pPersona.nacionalidad != null) pnacionalidad.Value = pPersona.nacionalidad; else pnacionalidad.Value = DBNull.Value;
                        pnacionalidad.Direction = ParameterDirection.Input;
                        pnacionalidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnacionalidad);

                        DbParameter pubicacion_correspondencia = cmdTransaccionFactory.CreateParameter();
                        pubicacion_correspondencia.ParameterName = "p_ubicacion_correspondencia";
                        if (pPersona.ubicacion_correspondencia != null) pubicacion_correspondencia.Value = pPersona.ubicacion_correspondencia; else pubicacion_correspondencia.Value = DBNull.Value;
                        pubicacion_correspondencia.Direction = ParameterDirection.Input;
                        pubicacion_correspondencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_correspondencia);

                        DbParameter pubicacion_residencia = cmdTransaccionFactory.CreateParameter();
                        pubicacion_residencia.ParameterName = "p_ubicacion_residencia";
                        if (pPersona.ubicacion_residencia != null) pubicacion_residencia.Value = pPersona.ubicacion_residencia; else pubicacion_residencia.Value = DBNull.Value;
                        pubicacion_residencia.Direction = ParameterDirection.Input;
                        pubicacion_residencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_residencia);

                        DbParameter pnit_empresa = cmdTransaccionFactory.CreateParameter();
                        pnit_empresa.ParameterName = "p_nit_empresa";
                        if (pPersona.nit_empresa != null) pnit_empresa.Value = pPersona.nit_empresa; else pnit_empresa.Value = DBNull.Value;
                        pnit_empresa.Direction = ParameterDirection.Input;
                        pnit_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnit_empresa);

                        DbParameter ptipo_empresa = cmdTransaccionFactory.CreateParameter();
                        ptipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pPersona.tipo_empresa != null) ptipo_empresa.Value = pPersona.tipo_empresa; else ptipo_empresa.Value = DBNull.Value;
                        ptipo_empresa.Direction = ParameterDirection.Input;
                        ptipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_empresa);

                        DbParameter pact_ciiu_empresa = cmdTransaccionFactory.CreateParameter();
                        pact_ciiu_empresa.ParameterName = "p_act_ciiu_empresa";
                        if (pPersona.act_ciiu_empresa != null) pact_ciiu_empresa.Value = pPersona.act_ciiu_empresa; else pact_ciiu_empresa.Value = DBNull.Value;
                        pact_ciiu_empresa.Direction = ParameterDirection.Input;
                        pact_ciiu_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pact_ciiu_empresa);

                        DbParameter pparentesco_madminis = cmdTransaccionFactory.CreateParameter();
                        pparentesco_madminis.ParameterName = "p_parentesco_madminis";
                        if (pPersona.parentesco_madminis != null) pparentesco_madminis.Value = pPersona.parentesco_madminis; else pparentesco_madminis.Value = DBNull.Value;
                        pparentesco_madminis.Direction = ParameterDirection.Input;
                        pparentesco_madminis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_madminis);

                        DbParameter pparentesco_mcontrol = cmdTransaccionFactory.CreateParameter();
                        pparentesco_mcontrol.ParameterName = "p_parentesco_mcontrol";
                        if (pPersona.parentesco_mcontrol != null) pparentesco_mcontrol.Value = pPersona.parentesco_mcontrol; else pparentesco_mcontrol.Value = DBNull.Value;
                        pparentesco_mcontrol.Direction = ParameterDirection.Input;
                        pparentesco_mcontrol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_mcontrol);

                        DbParameter pparentesco_pep = cmdTransaccionFactory.CreateParameter();
                        pparentesco_pep.ParameterName = "p_parentesco_pep";
                        if (pPersona.parentesco_pep != null) pparentesco_pep.Value = pPersona.parentesco_pep; else pparentesco_pep.Value = DBNull.Value;
                        pparentesco_pep.Direction = ParameterDirection.Input;
                        pparentesco_pep.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_pep);

                        DbParameter pubicacion_empresa = cmdTransaccionFactory.CreateParameter();
                        pubicacion_empresa.ParameterName = "p_ubicacion_empresa";
                        if (pPersona.ubicacion_empresa != null) pubicacion_empresa.Value = pPersona.ubicacion_empresa; else pubicacion_empresa.Value = DBNull.Value;
                        pubicacion_empresa.Direction = ParameterDirection.Input;
                        pubicacion_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_empresa);

                        //agregado -- acceso o no a Oficina Virtual
                        DbParameter pacceso_oficina = cmdTransaccionFactory.CreateParameter();
                        pacceso_oficina.ParameterName = "p_acceso_oficina";
                        pacceso_oficina.Value = pPersona.acceso_oficina;
                        pacceso_oficina.Direction = ParameterDirection.Input;
                        pacceso_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pacceso_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONA_MOD";//MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                        {
                            if (opcion == 1)
                            {
                                DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Aportes, "Creacion de aportes para la persona");
                            }
                            else
                            {
                                DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Modificar.ToString());
                            }
                        }

                        pPersona.cod_persona = Convert.ToInt64(pcod_persona.Value);

                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "CrearPersonaAporte", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ModificarPersonaConyugue(Persona1 pPersona, Usuario vUsuario)
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
                        //pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        pprimer_nombre.Value = pPersona.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        //pprimer_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pPersona.segundo_nombre != null) psegundo_nombre.Value = pPersona.segundo_nombre; else psegundo_nombre.Value = DBNull.Value;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        //psegundo_nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        pprimer_apellido.Value = pPersona.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        //pprimer_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pPersona.segundo_apellido != null) psegundo_apellido.Value = pPersona.segundo_apellido; else psegundo_apellido.Value = DBNull.Value;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        //psegundo_apellido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        ptipo_identificacion.Value = pPersona.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        //ptipo_identificacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pPersona.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        //pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pcodciudadexpedicion = cmdTransaccionFactory.CreateParameter();
                        pcodciudadexpedicion.ParameterName = "p_codciudadexpedicion";
                        if (pPersona.codciudadexpedicion == null)
                            pcodciudadexpedicion.Value = DBNull.Value;
                        else
                            pcodciudadexpedicion.Value = pPersona.codciudadexpedicion;
                        pcodciudadexpedicion.Direction = ParameterDirection.Input;
                        //pcodciudadexpedicion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadexpedicion);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (pPersona.celular != null) pcelular.Value = pPersona.celular; else pcelular.Value = DBNull.Value;
                        pcelular.Direction = ParameterDirection.Input;
                        //pcelular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        DbParameter psexo = cmdTransaccionFactory.CreateParameter();
                        psexo.ParameterName = "p_sexo";
                        if (pPersona.sexo != null) psexo.Value = pPersona.sexo; else psexo.Value = DBNull.Value;
                        psexo.Direction = ParameterDirection.Input;
                        //psexo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psexo);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pPersona.fechanacimiento == null || pPersona.fechanacimiento == DateTime.MinValue)
                            pfechanacimiento.Value = DBNull.Value;
                        else
                            pfechanacimiento.Value = pPersona.fechanacimiento;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        //pfechanacimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (pPersona.empresa != null) pempresa.Value = pPersona.empresa; else pempresa.Value = DBNull.Value;
                        pempresa.Direction = ParameterDirection.Input;
                        //pempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pantiguedadlugarempresa = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlugarempresa.ParameterName = "p_antiguedadlugarempresa";
                        pantiguedadlugarempresa.Value = pPersona.antiguedadlugarempresa;
                        pantiguedadlugarempresa.Direction = ParameterDirection.Input;
                        //pantiguedadlugarempresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlugarempresa);

                        DbParameter pcodtipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodtipocontrato.ParameterName = "p_codtipocontrato";
                        if (pPersona.codtipocontrato != 0) pcodtipocontrato.Value = pPersona.codtipocontrato; else pcodtipocontrato.Value = DBNull.Value;
                        pcodtipocontrato.Direction = ParameterDirection.Input;
                        //pcodtipocontrato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipocontrato);

                        DbParameter pcodcargo = cmdTransaccionFactory.CreateParameter();
                        pcodcargo.ParameterName = "p_codcargo";
                        if (pPersona.codcargo != 0) pcodcargo.Value = pPersona.codcargo; else pcodcargo.Value = DBNull.Value;
                        pcodcargo.Direction = ParameterDirection.Input;
                        //pcodcargo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodcargo);

                        DbParameter ptelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        ptelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona.telefonoempresa != null) ptelefonoempresa.Value = pPersona.telefonoempresa; else ptelefonoempresa.Value = DBNull.Value;
                        ptelefonoempresa.Direction = ParameterDirection.Input;
                        //ptelefonoempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoempresa);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pPersona.email != null) pemail.Value = pPersona.email; else pemail.Value = DBNull.Value;
                        pemail.Direction = ParameterDirection.Input;
                        //pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pdireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pdireccionempresa.ParameterName = "p_direccionempresa";
                        if (pPersona.direccionempresa != null) pdireccionempresa.Value = pPersona.direccionempresa; else pdireccionempresa.Value = DBNull.Value;
                        pdireccionempresa.Direction = ParameterDirection.Input;
                        //pdireccionempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccionempresa);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        if (pPersona.fecultmod == DateTime.MinValue)
                            pfecultmod.Value = DBNull.Value;
                        else
                            pfecultmod.Value = pPersona.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        //pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter p_codciudadnacimiento = cmdTransaccionFactory.CreateParameter();
                        p_codciudadnacimiento.ParameterName = "p_codciudadnacimiento";
                        if (pPersona.codciudadnacimiento == 0)
                            p_codciudadnacimiento.Value = DBNull.Value;
                        else
                            p_codciudadnacimiento.Value = pPersona.codciudadnacimiento;
                        p_codciudadnacimiento.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codciudadnacimiento);

                        DbParameter p_fechaexpedicion = cmdTransaccionFactory.CreateParameter();
                        p_fechaexpedicion.ParameterName = "p_fechaexpedicion";
                        if (pPersona.fechaexpedicion == DateTime.MinValue)
                            p_fechaexpedicion.Value = DBNull.Value;
                        else
                            p_fechaexpedicion.Value = pPersona.fechaexpedicion;
                        p_fechaexpedicion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_fechaexpedicion);

                        DbParameter p_estrato = cmdTransaccionFactory.CreateParameter();
                        p_estrato.ParameterName = "p_estrato";
                        if (pPersona.Estrato == 0)
                            p_estrato.Value = 0;
                        else
                            p_estrato.Value = pPersona.Estrato;
                        p_estrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_estrato);

                        DbParameter p_ocupacion = cmdTransaccionFactory.CreateParameter();
                        p_ocupacion.ParameterName = "p_ocupacion";
                        if (pPersona.ocupacion == "" || pPersona.ocupacion == null)
                            p_ocupacion.Value = 0;
                        else
                            p_ocupacion.Value = Convert.ToInt64(pPersona.ocupacion);
                        p_ocupacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_ocupacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONACONY_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "CrearPersonaConyugue", ex);
                        return null;
                    }
                }
            }
            return pPersona;
        }

        public Persona1 ModificarPersonaAtencionCliente(Persona1 pPersona1, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPersona1.cod_persona;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        pIDENTIFICACION.Value = pPersona1.identificacion;

                        DbParameter pPRIMER_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_NOMBRE.ParameterName = "p_PRIMER_NOMBRE";
                        pPRIMER_NOMBRE.Value = pPersona1.primer_nombre;

                        DbParameter pSEGUNDO_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_NOMBRE.ParameterName = "p_SEGUNDO_NOMBRE";
                        if (pPersona1.segundo_nombre != null) pSEGUNDO_NOMBRE.Value = pPersona1.segundo_nombre; else pSEGUNDO_NOMBRE.Value = DBNull.Value;

                        DbParameter pPRIMER_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pPRIMER_APELLIDO.ParameterName = "p_PRIMER_APELLIDO";
                        pPRIMER_APELLIDO.Value = pPersona1.primer_apellido;

                        DbParameter pSEGUNDO_APELLIDO = cmdTransaccionFactory.CreateParameter();
                        pSEGUNDO_APELLIDO.ParameterName = "p_SEGUNDO_APELLIDO";
                        if (pPersona1.segundo_apellido != null) pSEGUNDO_APELLIDO.Value = pPersona1.segundo_apellido; else pSEGUNDO_APELLIDO.Value = DBNull.Value;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        if (pPersona1.direccion != null) pDIRECCION.Value = pPersona1.direccion; else pDIRECCION.Value = DBNull.Value;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        if (pPersona1.telefono != null) pTELEFONO.Value = pPersona1.telefono; else pTELEFONO.Value = DBNull.Value;

                        DbParameter pCODCIUDADRESIDENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODCIUDADRESIDENCIA.ParameterName = "p_CODCIUDADRESIDENCIA";
                        if (pPersona1.codciudadresidencia != 0) pCODCIUDADRESIDENCIA.Value = pPersona1.codciudadresidencia; else pCODCIUDADRESIDENCIA.Value = DBNull.Value;

                        DbParameter pEMAIL = cmdTransaccionFactory.CreateParameter();
                        pEMAIL.ParameterName = "p_EMAIL";
                        if (pPersona1.email != null) pEMAIL.Value = pPersona1.email; else pEMAIL.Value = DBNull.Value;

                        DbParameter pTELEFONOEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOEMPRESA.ParameterName = "p_TELEFONOEMPRESA";
                        if (pPersona1.telefonoempresa != null) pTELEFONOEMPRESA.Value = pPersona1.telefonoempresa; else pTELEFONOEMPRESA.Value = DBNull.Value;

                        DbParameter pDIRECCIONEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pDIRECCIONEMPRESA.ParameterName = "p_DIRECCIONEMPRESA";
                        if (pPersona1.direccionempresa != null) pDIRECCIONEMPRESA.Value = pPersona1.direccionempresa; else pDIRECCIONEMPRESA.Value = DBNull.Value;

                        DbParameter pCIUDADEMPRESA = cmdTransaccionFactory.CreateParameter();
                        pCIUDADEMPRESA.ParameterName = "p_CIUDADEMPRESA";
                        if (pPersona1.ciudad != 0) pCIUDADEMPRESA.Value = pPersona1.ciudad; else pCIUDADEMPRESA.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_NOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pPRIMER_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pSEGUNDO_APELLIDO);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pCODCIUDADRESIDENCIA);
                        cmdTransaccionFactory.Parameters.Add(pEMAIL);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCIONEMPRESA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDADEMPRESA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ATE_PERSONA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ModificarPersonaAtencionCliente", ex);
                        return null;
                    }
                }
            }
        }

        public Boolean ModificarPersonaAPP(Persona1 pPersona1, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersona1.cod_persona;

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pPersona1.direccion;

                        DbParameter pTelefono = cmdTransaccionFactory.CreateParameter();
                        pTelefono.ParameterName = "P_Telefono";
                        pTelefono.Value = pPersona1.telefono;

                        DbParameter pCodciudadresidencia = cmdTransaccionFactory.CreateParameter();
                        pCodciudadresidencia.ParameterName = "P_Codciudadresidencia";
                        if (pPersona1.codciudadresidencia != 0) pCodciudadresidencia.Value = pPersona1.codciudadresidencia; else pCodciudadresidencia.Value = DBNull.Value;

                        DbParameter pCelular = cmdTransaccionFactory.CreateParameter();
                        pCelular.ParameterName = "P_Celular";
                        if (pPersona1.celular != null) pCelular.Value = pPersona1.celular; else pCelular.Value = DBNull.Value;

                        DbParameter pEmail = cmdTransaccionFactory.CreateParameter();
                        pEmail.ParameterName = "P_Email";
                        if (pPersona1.email != null) pEmail.Value = pPersona1.email; else pEmail.Value = DBNull.Value;

                        DbParameter pTelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        pTelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona1.telefonoempresa != null) pTelefonoempresa.Value = pPersona1.telefonoempresa; else pTelefonoempresa.Value = DBNull.Value;

                        DbParameter pDireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pDireccionempresa.ParameterName = "P_Direccionempresa";
                        if (pPersona1.direccionempresa != null) pDireccionempresa.Value = pPersona1.direccionempresa; else pDireccionempresa.Value = DBNull.Value;

                        DbParameter pCiudadempresa = cmdTransaccionFactory.CreateParameter();
                        pCiudadempresa.ParameterName = "p_ciudadempresa";
                        if (pPersona1.ciudad != 0) pCiudadempresa.Value = pPersona1.ciudad; else pCiudadempresa.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pdireccion);
                        cmdTransaccionFactory.Parameters.Add(pTelefono);
                        cmdTransaccionFactory.Parameters.Add(pCodciudadresidencia);
                        cmdTransaccionFactory.Parameters.Add(pCelular);
                        cmdTransaccionFactory.Parameters.Add(pEmail);
                        cmdTransaccionFactory.Parameters.Add(pTelefonoempresa);
                        cmdTransaccionFactory.Parameters.Add(pDireccionempresa);
                        cmdTransaccionFactory.Parameters.Add(pCiudadempresa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APP_PERSONA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ModificarPersonaAPP", ex);
                        return false;
                    }
                }
            }
        }

        public bool ConsultaClavePersona(string pIdentificacion, string pClave, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            bool bEncontro = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from persona where identificacion = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            bEncontro = true;

                        dbConnectionFactory.CerrarConexion(connection);

                        return bEncontro;
                    }
                    catch
                    {
                        return bEncontro;
                    }
                }
            }
        }

        public long ConsultarCodigopersona(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            long codigo_persona = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_persona from persona where identificacion = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["cod_persona"] != DBNull.Value) codigo_persona = Convert.ToInt64(resultado["cod_persona"]);

                        dbConnectionFactory.CerrarConexion(connection);

                        return codigo_persona;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public List<PersonaAutorizacion> ListarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaAutorizacion> lstPersonaAutorizacion = new List<PersonaAutorizacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PERSONA_AUTORIZACION " + ObtenerFiltro(pPersonaAutorizacion) + " ORDER BY IDAUTORIZACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaAutorizacion entidad = new PersonaAutorizacion();
                            if (resultado["IDAUTORIZACION"] != DBNull.Value) entidad.idautorizacion = Convert.ToInt64(resultado["IDAUTORIZACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstPersonaAutorizacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ListarPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaAutorizacion> ListarPersonaAutorizacion(string pIdentificacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaAutorizacion> lstPersonaAutorizacion = new List<PersonaAutorizacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PERSONA_AUTORIZACION.* FROM PERSONA_AUTORIZACION INNER JOIN PERSONA ON PERSONA_AUTORIZACION.COD_PERSONA = PERSONA.COD_PERSONA WHERE PERSONA.IDENTIFICACION = '" + pIdentificacion + "' AND PERSONA_AUTORIZACION.ESTADO IN (0, 1) ORDER BY PERSONA_AUTORIZACION.IDAUTORIZACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaAutorizacion entidad = new PersonaAutorizacion();
                            if (resultado["IDAUTORIZACION"] != DBNull.Value) entidad.idautorizacion = Convert.ToInt64(resultado["IDAUTORIZACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstPersonaAutorizacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ListarPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }

        public PersonaAutorizacion ModificarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidautorizacion = cmdTransaccionFactory.CreateParameter();
                        pidautorizacion.ParameterName = "p_idautorizacion";
                        pidautorizacion.Value = pPersonaAutorizacion.idautorizacion;
                        pidautorizacion.Direction = ParameterDirection.Input;
                        pidautorizacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidautorizacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pPersonaAutorizacion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pPersonaAutorizacion.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pPersonaAutorizacion.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "p_numero_producto";
                        pnumero_producto.Value = pPersonaAutorizacion.numero_producto;
                        pnumero_producto.Direction = ParameterDirection.Input;
                        pnumero_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        pip.Value = pPersonaAutorizacion.ip;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = pPersonaAutorizacion.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPersonaAutorizacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERSONAAUTOR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "CrearPersonaAutorizacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<PersonaAutorizacion> ListarUsuarioAutorizacion(Int64 pCodUsuario, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PersonaAutorizacion> lstPersonaAutorizacion = new List<PersonaAutorizacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PERSONA_AUTORIZACION.*, PERSONA.IDENTIFICACION, PERSONA.PRIMER_NOMBRE, PERSONA.SEGUNDO_NOMBRE, PERSONA.PRIMER_APELLIDO, PERSONA.SEGUNDO_APELLIDO,
                                        PERSONA.DIRECCION, PERSONA.TELEFONO
                                        FROM PERSONA_AUTORIZACION INNER JOIN PERSONA ON PERSONA_AUTORIZACION.COD_PERSONA = PERSONA.COD_PERSONA WHERE PERSONA_AUTORIZACION.COD_USUARIO = " + pCodUsuario + " AND PERSONA_AUTORIZACION.ESTADO IN (0, 1) ORDER BY PERSONA_AUTORIZACION.IDAUTORIZACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PersonaAutorizacion entidad = new PersonaAutorizacion();
                            if (resultado["IDAUTORIZACION"] != DBNull.Value) entidad.idautorizacion = Convert.ToInt64(resultado["IDAUTORIZACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstPersonaAutorizacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersonaAutorizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListarUsuarioAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public Xpinn.Comun.Entities.General consultarsalariominimo(Int64? pCodigo, Usuario vUsuario)
        {
            Xpinn.Comun.Entities.General entidad = new Xpinn.Comun.Entities.General();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pCodigo == 0)
                            pCodigo = 10;
                        string sql = @"Select * From general Where codigo = " + pCodigo.ToString() + " Order by 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "consultarsalariominimo", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 consultaraportes(Int64? pCodUsuario, string pId, Usuario vUsuario)
        {
            Persona1 entidad = new Persona1();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Sum(saldo) as saldo From aporte a Inner Join lineaaporte l On a.cod_linea_aporte = l.cod_linea_aporte Where a.cod_persona = " + pCodUsuario + " And l.tipo_aporte = 1 And a.estado = '1'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) entidad.SALDOaportes = Convert.ToInt64(resultado["SALDO"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ListarUsuarioAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public Persona1 consultarcreditos(Int64? pCodUsuario, string pId, Usuario vUsuario)
        {
            Persona1 entidad = new Persona1();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Sum(saldo_capital) as saldo From credito c Where c.cod_deudor = " + pCodUsuario + " And c.estado = 'C'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldocreditos = Convert.ToInt64(resultado["SALDO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaAutorizacionData", "ListarUsuarioAutorizacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<Credito> ConsultarResumenPersona(Int64 pCodPersona, Usuario vUsuario)
        {
            List<Credito> lstInformacion = null;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select 1 PRODUCTO, saldo as saldo, Calcular_VrAPagarAporte(NUMERO_APORTE, SYSDATE) VALOR_PAGO, TO_CHAR(L.Tipo_Aporte) as linea, NOMBRE
                                    From aporte a Inner Join lineaaporte l On a.cod_linea_aporte = l.cod_linea_aporte
                                    Where a.cod_persona = " + pCodPersona + @" And l.tipo_aporte in (1, 2)
                                    and (l.cruzar = 1 or a.estado = '1')
                                    union all                                    
                                    Select 2 PRODUCTO, c.saldo_capital as saldo, Calcular_VrAPagar(c.NUMERO_RADICACION, SYSDATE) VALOR_PAGO, TO_CHAR(c.cod_linea_credito) as LINEA, NOMBRE
                                    From credito c
                                    inner join lineascredito d on c.cod_linea_credito = d.COD_LINEA_CREDITO
                                    Where c.cod_deudor = " + pCodPersona + @" And c.estado = 'C'
                                    union all
                                    Select 3 PRODUCTO, a.saldo_total as saldo, NVL(CALCULAR_CUOTA_AHORRO_VISTA(a.NUMERO_CUENTA),0) VALOR_PAGO, TO_CHAR(a.COD_LINEA_AHORRO) as LINEA, l.DESCRIPCION as NOMBRE
                                    From ahorro_vista a 
                                    inner join lineaahorro l on a.cod_linea_ahorro =  l.cod_linea_ahorro
                                    Where a.cod_persona = " + pCodPersona + @" And a.estado = 1
                                    union all
                                    Select 4 PRODUCTO, s.SALDO as saldo, NVL(CALCULAR_PAGO_SERVICIO(s.NUMERO_SERVICIO, sysdate),0) VALOR_PAGO, TO_CHAR(s.COD_LINEA_SERVICIO) as LINEA, NOMBRE
                                    From servicios s
                                    inner join lineasservicios e on s.cod_linea_servicio = e.cod_linea_servicio
                                    Where s.cod_persona = " + pCodPersona + @" And s.estado = 'C'
                                    union all
                                    Select 5 PRODUCTO, c.valor as saldo, NVL(c.valor,0) VALOR_PAGO, TO_CHAR(c.COD_LINEACDAT) as LINEA, t.DESCRIPCION as NOMBRE
                                    From CDAT c inner join cdat_titular t on t.codigo_cdat = c.codigo_cdat
                                    inner join lineacdat t on c.cod_lineacdat = t.cod_lineacdat
                                    Where t.cod_persona = " + pCodPersona + @" And c.estado in (1,2)
                                    union all
                                    Select 9 PRODUCTO, a.saldo as saldo, NVL(CALCULAR_CUOTA_PROGRAMADO(a.NUMERO_PROGRAMADO),0) VALOR_PAGO, TO_CHAR(a.COD_LINEA_PROGRAMADO) as LINEA, NOMBRE
                                    From AHORRO_PROGRAMADO a inner join lineaprogramado p on a.cod_linea_programado = p.cod_linea_programado 
                                    Where a.COD_PERSONA = " + pCodPersona + @" And a.estado = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstInformacion = new List<Credito>();
                            Credito entidad;
                            while (resultado.Read())
                            {
                                entidad = new Credito();
                                if (resultado["PRODUCTO"] != DBNull.Value) entidad.descrpcion = Convert.ToString(resultado["PRODUCTO"]);
                                if (resultado["SALDO"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO"]);
                                if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_PAGO"]);
                                if (resultado["LINEA"] != DBNull.Value) entidad.Tipo_Linea = Convert.ToInt32(resultado["LINEA"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                lstInformacion.Add(entidad);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarResumenPersona", ex);
                        return null;
                    }
                }
            }
        }


        public Persona1 ConsultarPersona(Int64? pCod, string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT persona.*, t.descripcion As nomtipo_identificacion, o.nombre As oficina, f.estado As estadoafi 
                                        FROM persona 
                                        LEFT JOIN persona_afiliacion f ON persona.cod_persona = f.cod_persona
                                        LEFT JOIN tipoidentificacion t ON persona.tipo_identificacion = t.codtipoidentificacion 
                                        LEFT JOIN oficina o ON persona.cod_oficina = o.cod_oficina ";
                        if (pCod != null)
                            sql = sql + "WHERE persona.cod_persona = " + pCod.ToString();
                        if (pId != null)
                            sql = sql + "WHERE persona.identificacion = '" + pId.ToString() + "' ";
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
                            if (resultado["NOMTIPO_IDENTIFICACION"] != DBNull.Value) entidad.nomtipo_identificacion = Convert.ToString(resultado["NOMTIPO_IDENTIFICACION"]);
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
                            if (resultado["ACTIVIDADEMPRESA"] != DBNull.Value) entidad.codactividadStr = Convert.ToString(resultado["ACTIVIDADEMPRESA"]);
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
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt32(resultado["COD_ASESOR"]);
                            if (resultado["RESIDENTE"] != DBNull.Value) entidad.residente = Convert.ToString(resultado["RESIDENTE"]);
                            if (resultado["FECHA_RESIDENCIA"] != DBNull.Value) entidad.fecha_residencia = Convert.ToDateTime(resultado["FECHA_RESIDENCIA"]);
                            if (resultado["TRATAMIENTO"] != DBNull.Value) entidad.tratamiento = Convert.ToString(resultado["TRATAMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (entidad.estado == "" || entidad.estado == null)
                                if (resultado["ESTADOAFI"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADOAFI"]);
                            entidad.nomestado = NomEstado(entidad.estado);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadlugarempresa = Convert.ToInt32(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            if (resultado["OCUPACION"] != DBNull.Value) entidad.ocupacion = Convert.ToString(resultado["OCUPACION"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
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
                        BOExcepcion.Throw("TerceroData", "ConsultarTercero", ex);
                        return null;
                    }
                }
            }
        }

        public string NomEstado(string pEstado)
        {
            if (pEstado == "A") return "Activo";
            if (pEstado == "I") return "Inactivo";
            if (pEstado == "R") return "Retirado";
            return "";
        }


        /// <summary>
        /// Modifica un registro en la tabla PERSONA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Persona1 modificada</returns>
        public Persona1 ModificarPersonasAporte(Persona1 pPersona, Usuario vUsuario)
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
                        pcod_persona.Direction = ParameterDirection.InputOutput;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_identificacion = cmdTransaccionFactory.CreateParameter();
                        pcod_identificacion.ParameterName = "p_identificacion";
                        if (pPersona.identificacion != null)
                            pcod_identificacion.Value = pPersona.identificacion;
                        else
                            pcod_identificacion.Value = DBNull.Value;
                        pcod_identificacion.DbType = DbType.AnsiStringFixedLength;
                        pcod_identificacion.Size = 50;
                        pcod_identificacion.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(pcod_identificacion);

                        DbParameter pcod_nomina = cmdTransaccionFactory.CreateParameter();
                        pcod_nomina.ParameterName = "p_cod_nomina";
                        if (pPersona.cod_nomina != null)
                            pcod_nomina.Value = pPersona.cod_nomina_empleado;
                        else
                            pcod_nomina.Value = DBNull.Value;
                        pcod_nomina.DbType = DbType.AnsiStringFixedLength;
                        pcod_nomina.Size = 50;
                        pcod_nomina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_nomina);

                        DbParameter pcod_asesor = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor.ParameterName = "p_cod_asesor";
                        pcod_asesor.Value = pPersona.cod_asesor;
                        pcod_asesor.Direction = ParameterDirection.Input;
                        //pcod_asesor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor);

                        DbParameter pcod_zona = cmdTransaccionFactory.CreateParameter();
                        pcod_zona.ParameterName = "p_cod_zona";
                        if (pPersona.zona != 0) pcod_zona.Value = pPersona.zona; else pcod_zona.Value = DBNull.Value;
                        pcod_zona.Direction = ParameterDirection.Input;
                        pcod_zona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_zona);

                        DbParameter p_salario = cmdTransaccionFactory.CreateParameter();
                        p_salario.ParameterName = "p_salario";
                        if (pPersona.salario != null)
                            p_salario.Value = pPersona.salario;
                        else
                            p_salario.Value = DBNull.Value;
                        p_salario.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_salario);


                        DbParameter pcuota_aporte = cmdTransaccionFactory.CreateParameter();
                        pcuota_aporte.ParameterName = "p_cuota_aporte";
                        if (pPersona.cuota != null)
                            pcuota_aporte.Value = pPersona.cuota;
                        else
                            pcuota_aporte.Value = DBNull.Value;
                        pcuota_aporte.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcuota_aporte);

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Value = "";
                        p_error.DbType = DbType.AnsiStringFixedLength;
                        p_error.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PER_ACT_MOD";//MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Crear.ToString());

                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "CrearPersonaAporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<Persona1> ListarPersonasAporte(string pcod_ini, string pcod_fin, string pcod_empresa, string pcod_asesor, string pcod_zona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstPersona1 = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        sql = @"select v.cod_persona,v.identificacion,v.nombre,v.cod_nomina,v.cod_asesor,"
                                + "QUITARESPACIOS(Substr(a.snombre1 || ' ' || a.snombre2 || ' ' || a.sapellido1 || ' ' || a.sapellido2, 0, 240)) as nombre_asesor,"
                                + "p.COD_ZONA,z.Descripcion "
                                + "from v_persona v left join persona p on v.cod_persona=p.cod_persona Left join asejecutivos a on v.cod_asesor = a.icodigo Left join zonas z on p.cod_zona=z.cod_zona left join persona_empresa_recaudo e on v.cod_persona=e.cod_persona ";
                        if (pcod_ini != "")
                        {
                            sql = sql + "WHERE v.cod_persona >= " + pcod_ini + " ";
                        }
                        if (pcod_fin != "")
                        {
                            if (sql.Contains("WHERE"))
                                sql = sql + "AND v.cod_persona <= " + pcod_fin + " ";
                            else
                                sql = sql + "WHERE v.cod_persona <= " + pcod_fin + " ";
                        }
                        if (pcod_empresa != "Seleccione un item")
                        {
                            if (sql.Contains("WHERE"))
                                sql = sql + "AND e.cod_empresa = " + pcod_empresa + " ";
                            else
                                sql = sql + "WHERE e.cod_empresa = " + pcod_empresa + " ";
                        }
                        if (pcod_asesor != "Seleccione un item")
                        {
                            if (sql.Contains("WHERE"))
                                sql = sql + "AND v.cod_asesor = " + pcod_asesor + " ";
                            else
                                sql = sql + "WHERE v.cod_asesor = " + pcod_asesor + " ";
                        }
                        if (pcod_zona != "Seleccione un item")
                        {
                            if (sql.Contains("WHERE"))
                                sql = sql + "AND z.cod_zona = " + pcod_zona + " ";
                            else
                                sql = sql + "WHERE z.cod_zona = " + pcod_zona + " ";
                        }
                        sql = sql + " Order by v.cod_persona";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["COD_ZONA"] != DBNull.Value) entidad.zona = Convert.ToInt64(resultado["COD_ZONA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_zona = Convert.ToString(resultado["DESCRIPCION"]);
                            lstPersona1.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPersona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 ValidarPersona(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado;
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT t.descripcion AS tipoidentificaciondescripcion, p.*, a.estado AS estados, ESNULO(a.saldo, 0) AS saldo_afiliacion, a.cod_motivo,
                                ESNULO((Select Count(*) From aporte c Where c.cod_persona = p.cod_persona And c.estado = '1'), 0) num_aportes,
                                ESNULO((Select sum(c.saldo) From aporte c inner join LINEAAPORTE l on c.cod_linea_aporte = l.cod_linea_aporte
                                Where c.cod_persona = p.cod_persona and l.tipo_aporte = 1 And c.estado = '1'), 0) saldo_aportes, 
                                ESNULO((Select Count(*) From credito c Where c.cod_deudor = p.cod_persona And c.estado = 'C'), 0) num_creditos,
                                ESNULO((Select Count(*) From aporte c Where c.cod_persona = p.cod_persona And c.estado = '1' And TRUNCAR(c.fecha_proximo_pago) < TRUNCAR(SYSDATE)), 0) As num_aportes_en_mora,
                                ESNULO((Select Count(*) From credito c Where c.cod_deudor = p.cod_persona And c.estado = 'C' And TRUNCAR(c.fecha_proximo_pago) < TRUNCAR(SYSDATE)), 0) As num_creditos_en_mora,
                                ESNULO((Select Count(*) From credito c Where c.cod_deudor = p.cod_persona And c.estado = 'C' And TRUNCAR(c.fecha_proximo_pago) < TRUNCAR(SYSDATE) 
                                         And c.cod_linea_credito In (Select l.cod_linea_credito From parametros_linea l Where l.cod_parametro = 320)), 0) As num_creditos_castigados,
                                ESNULO((Select Count(*) From persona_controlbiom b Where b.tipo = " + pTipo + @" And  b.cod_persona = p.cod_persona), 0) As num_control
                                FROM persona p LEFT JOIN persona_afiliacion a ON p.cod_persona = a.cod_persona LEFT JOIN tipoidentificacion t on p.tipo_identificacion = t.codtipoidentificacion
                                WHERE p.identificacion = '" + pIdentificacion + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            int cod_motivo = 0;
                            decimal saldo_afiliacion = 0;
                            // ADICIONADO ULTIMO 
                            decimal saldo_aporte = 0;
                            // 
                            int num_aportes_en_mora = 0;
                            int num_creditos_en_mora = 0;
                            int num_creditos_castigados = 0;
                            int num_control = 0;
                            int num_aportes = 0;
                            int num_creditos = 0;
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIGITO_VERIFICACION"] != DBNull.Value) entidad.digito_verificacion = Convert.ToInt64(resultado["DIGITO_VERIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["TIPOIDENTIFICACIONDESCRIPCION"] != DBNull.Value) entidad.tipo_identificacion_descripcion = Convert.ToString(resultado["TIPOIDENTIFICACIONDESCRIPCION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (entidad.tipo_persona == "N")
                                entidad.nombres = entidad.primer_nombre + " " + entidad.segundo_nombre;
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (entidad.tipo_persona == "N")
                                entidad.apellidos = entidad.primer_apellido + " " + entidad.segundo_apellido;
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (entidad.tipo_persona == "J")
                                entidad.nombres = entidad.razon_social;
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["ESTADOS"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADOS"]);
                            if (resultado["SALDO_AFILIACION"] != DBNull.Value) saldo_afiliacion = Convert.ToDecimal(resultado["SALDO_AFILIACION"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) cod_motivo = Convert.ToInt32(resultado["COD_MOTIVO"]);
                            if (resultado["NUM_APORTES_EN_MORA"] != DBNull.Value) num_aportes_en_mora = Convert.ToInt32(resultado["NUM_APORTES_EN_MORA"]);
                            if (resultado["NUM_CREDITOS_EN_MORA"] != DBNull.Value) num_creditos_en_mora = Convert.ToInt32(resultado["NUM_CREDITOS_EN_MORA"]);
                            if (resultado["NUM_CREDITOS_CASTIGADOS"] != DBNull.Value) num_creditos_castigados = Convert.ToInt32(resultado["NUM_CREDITOS_CASTIGADOS"]);
                            if (resultado["NUM_CONTROL"] != DBNull.Value) num_control = Convert.ToInt32(resultado["NUM_CONTROL"]);
                            if (entidad.estado == "A" && saldo_afiliacion == 0 && num_aportes_en_mora == 0 && num_creditos_en_mora == 0 && num_creditos_castigados == 0 && num_control == 0)
                            {
                                entidad.Observaciones = "VALIDADO";
                            }
                            else
                            {
                                if (resultado["num_aportes"] != DBNull.Value) num_aportes = Convert.ToInt32(resultado["num_aportes"]);
                                if (resultado["num_creditos"] != DBNull.Value) num_creditos = Convert.ToInt32(resultado["num_creditos"]);

                                entidad.Observaciones = "NO VALIDADO";
                                if (entidad.estado == "A" || (entidad.estado == "I" && cod_motivo != 7))
                                {
                                    if (num_aportes == 0 && num_creditos == 0)
                                        entidad.Observaciones = "NO VALIDADO";
                                    if ((num_aportes > 0 || num_creditos > 0) && num_aportes_en_mora == 0 && num_creditos_en_mora == 0 && num_creditos_castigados == 0 && num_control == 0)
                                    {
                                        // ADICIONADO ULTIMO 
                                        if (resultado["saldo_aportes"] != DBNull.Value) saldo_aporte = Convert.ToDecimal(resultado["saldo_aportes"]);
                                        if (saldo_aporte == 0)
                                            entidad.Observaciones = "NO VALIDADO";
                                        else
                                            entidad.Observaciones = "VALIDADO";
                                        entidad.Observaciones += " - La persona se encuentra con estado : " + entidad.estado + " Motivo: " + cod_motivo.ToString();
                                    }
                                }

                                if (!(num_control == 0))
                                    entidad.Observaciones += " - El asociado ya se le entregó el regalo.";
                                if (!(entidad.estado == "A" || (entidad.estado == "I" && cod_motivo != 7)))
                                    entidad.Observaciones += " - La persona no esta en estado A=Habil o I=Inhabil. Estado: " + entidad.estado + " Motivo: " + cod_motivo.ToString();
                                if (!(saldo_afiliacion == 0))
                                    entidad.Observaciones += " - El asociado tiene saldo en afiliación. Estado:" + saldo_afiliacion.ToString("0:n");
                                if (!(num_aportes_en_mora == 0))
                                    entidad.Observaciones += " - El asociado tiene aportes en mora.";
                                if (!(num_creditos_en_mora == 0))
                                    entidad.Observaciones += " - El asociado tiene créditos en mora.";
                                if (!(num_creditos_castigados == 0))
                                    entidad.Observaciones += " - El asociado tiene créditos castigados.";
                            }
                        }
                        else
                        {
                            entidad.nombre = "errordedatos";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarPersona1", ex);
                        return null;
                    }
                }
            }
        }

        public Int64? GrabarControl(string pIdentificacion, int pTipo, Usuario pUsuario)
        {
            Int64? idControl = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = pIdentificacion;
                        P_IDENTIFICACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);

                        DbParameter P_TIPO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO.ParameterName = "P_TIPO";
                        P_TIPO.Value = pTipo;
                        P_TIPO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO);

                        DbParameter P_COD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_USUARIO.ParameterName = "P_COD_USUARIO";
                        P_COD_USUARIO.Value = pUsuario.codusuario;
                        P_COD_USUARIO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_USUARIO);

                        DbParameter P_IDCONTROL = cmdTransaccionFactory.CreateParameter();
                        P_IDCONTROL.ParameterName = "P_IDCONTROL";
                        P_IDCONTROL.Value = 0;
                        P_IDCONTROL.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(P_IDCONTROL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERSONABIOCONT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (P_IDCONTROL.Value != null)
                            idControl = Convert.ToInt64(P_IDCONTROL.Value);

                        return idControl;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
        public Persona1 FechaNacimiento(Int64 CodPersona, Usuario vUsuario)
        {


            DbDataReader resultado;
            Persona1 entidad = new Persona1();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select (to_number(to_char(sysdate,'YYYY')) - to_number(to_char(fechanacimiento,'YYYY'))) -  CASE WHEN to_char(SYSDATE, 'MMDD') < to_char(fechanacimiento, 'MMDD') THEN 1 ELSE 0
                                        END as Edad,fechanacimiento ,t.SIGLA
                                        from persona p join tipoidentificacion t on p.tipo_identificacion=t.codtipoidentificacion where cod_persona=" + CodPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["fechanacimiento"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["fechanacimiento"]);
                            if (resultado["Edad"] != DBNull.Value) entidad.Edad = Convert.ToInt32(resultado["Edad"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.tipo_identif = Convert.ToString(resultado["SIGLA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "FechaNacimiento", ex);
                        return entidad;
                    }
                }
            }
        }

        public int NotificacionidMax(Usuario vUsuario)
        {
            int Fecha = 0;

            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select nvl(max(consecutivo),0)+1 siguiente from notificacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            Fecha = Convert.ToInt32(resultado["siguiente"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Fecha;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "NotificacionidMax", ex);
                        return Fecha;
                    }
                }
            }
        }

        public Persona1 Notificacion(Persona1 pPersona, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        P_CONSECUTIVO.ParameterName = "P_CONSECUTIVO";
                        P_CONSECUTIVO.Value = pPersona.idNotificaion;
                        P_CONSECUTIVO.Direction = ParameterDirection.Input;
                        P_CONSECUTIVO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_CONSECUTIVO);

                        DbParameter P_Texto = cmdTransaccionFactory.CreateParameter();
                        P_Texto.ParameterName = "P_Texto";
                        P_Texto.Value = pPersona.Texto;
                        P_Texto.Direction = ParameterDirection.Input;
                        P_Texto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_Texto);

                        DbParameter P_DiasAviso = cmdTransaccionFactory.CreateParameter();
                        P_DiasAviso.ParameterName = "P_DiasAviso";
                        P_DiasAviso.Value = pPersona.DiasAviso;
                        P_DiasAviso.Direction = ParameterDirection.Input;
                        P_DiasAviso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_DiasAviso);

                        DbParameter P_Tipo = cmdTransaccionFactory.CreateParameter();
                        P_Tipo.ParameterName = "P_Tipo";
                        P_Tipo.Value = pPersona.TipoNot;
                        P_Tipo.Direction = ParameterDirection.Input;
                        P_Tipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_Tipo);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NOTIFI_CRE";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NOTIFI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "Notificacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<Persona1> ProxVencer(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Persona1> lstConsulta = new List<Persona1>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.numero_cdat,c.fecha_vencimiento,p.email,p.primer_nombre,p.segundo_nombre,p.primer_apellido,p.segundo_apellido,
                            (SELECT texto FROM notificacion WHERE tipo='C') as Texto,'CDAT' as Tipo
                            FROM cdat c JOIN cdat_titular t ON c.codigo_cdat=t.codigo_cdat JOIN persona p ON t.cod_persona=p.cod_persona
                            join persona_afiliacion a on a.cod_persona=t.cod_persona
                            WHERE to_char(SYSDATE+(SELECT diasaviso FROM notificacion WHERE tipo='C'),'dd/mm/yyyy')=to_char(c.fecha_vencimiento,'dd/mm/yyyy')
                            AND c.estado IN (1,2) AND t.principal=1 
                            UNION ALL 
                            SELECT ap.Numero_programado,ap.Fecha_cierre,p.email,p.primer_nombre,p.segundo_nombre,p.primer_apellido,p.segundo_apellido,
                            (SELECT texto FROM notificacion WHERE tipo='AP') as Texto,'Ahorro Programado' as Tipo
                            FROM ahorro_programado ap JOIN persona p ON p.cod_persona=ap.cod_persona
                            JOIN persona_afiliacion pa ON pa.cod_persona=ap.cod_persona
                            WHERE to_char(SYSDATE+(SELECT diasaviso FROM notificacion WHERE tipo='AP'),'dd/mm/yyyy')=to_char(ap.fecha_Cierre,'dd/mm/yyyy')
                            AND ap.estado IN (1)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (resultado["numero_cdat"] != DBNull.Value) entidad.NumeroPoducto = Convert.ToString(resultado["numero_cdat"]);
                            if (resultado["fecha_vencimiento"] != DBNull.Value) entidad.VencimientoProducto = Convert.ToDateTime(resultado["fecha_vencimiento"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);
                            if (resultado["Texto"] != DBNull.Value) entidad.Texto = Convert.ToString(resultado["Texto"]);
                            if (resultado["Tipo"] != DBNull.Value) entidad.tipocontrato = Convert.ToString(resultado["Tipo"]);
                            lstConsulta.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ProxVencer", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 TabPersonal(Persona1 pPersona, int opcion, Usuario vUsuario)
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
                        if (opcion == 1)
                            pcod_persona.Direction = ParameterDirection.Output;
                        else
                            pcod_persona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pPersona.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pPersona.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pdigito_verificacion = cmdTransaccionFactory.CreateParameter();
                        pdigito_verificacion.ParameterName = "p_digito_verificacion";
                        pdigito_verificacion.Value = pPersona.digito_verificacion;
                        pdigito_verificacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdigito_verificacion);

                        DbParameter ptipo_identificacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_identificacion.ParameterName = "p_tipo_identificacion";
                        ptipo_identificacion.Value = pPersona.tipo_identificacion;
                        ptipo_identificacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_identificacion);

                        DbParameter pfechaexpedicion = cmdTransaccionFactory.CreateParameter();
                        pfechaexpedicion.ParameterName = "p_fechaexpedicion";
                        if (pPersona.fechaexpedicion != DateTime.MinValue) pfechaexpedicion.Value = pPersona.fechaexpedicion; else pfechaexpedicion.Value = DBNull.Value;
                        pfechaexpedicion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfechaexpedicion);

                        DbParameter pcodciudadexpedicion = cmdTransaccionFactory.CreateParameter();
                        pcodciudadexpedicion.ParameterName = "p_codciudadexpedicion";
                        if (pPersona.codciudadexpedicion == null)
                            pcodciudadexpedicion.Value = DBNull.Value;
                        else
                            pcodciudadexpedicion.Value = pPersona.codciudadexpedicion;
                        pcodciudadexpedicion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadexpedicion);

                        DbParameter psexo = cmdTransaccionFactory.CreateParameter();
                        psexo.ParameterName = "p_sexo";
                        if (pPersona.sexo != null) psexo.Value = pPersona.sexo; else psexo.Value = DBNull.Value;
                        psexo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(psexo);

                        DbParameter pprimer_nombre = cmdTransaccionFactory.CreateParameter();
                        pprimer_nombre.ParameterName = "p_primer_nombre";
                        pprimer_nombre.Value = pPersona.primer_nombre;
                        pprimer_nombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pprimer_nombre);

                        DbParameter psegundo_nombre = cmdTransaccionFactory.CreateParameter();
                        psegundo_nombre.ParameterName = "p_segundo_nombre";
                        if (pPersona.segundo_nombre != null) psegundo_nombre.Value = pPersona.segundo_nombre; else psegundo_nombre.Value = DBNull.Value;
                        psegundo_nombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(psegundo_nombre);

                        DbParameter pprimer_apellido = cmdTransaccionFactory.CreateParameter();
                        pprimer_apellido.ParameterName = "p_primer_apellido";
                        pprimer_apellido.Value = pPersona.primer_apellido;
                        pprimer_apellido.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pprimer_apellido);

                        DbParameter psegundo_apellido = cmdTransaccionFactory.CreateParameter();
                        psegundo_apellido.ParameterName = "p_segundo_apellido";
                        if (pPersona.segundo_apellido != null) psegundo_apellido.Value = pPersona.segundo_apellido; else psegundo_apellido.Value = DBNull.Value;
                        psegundo_apellido.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(psegundo_apellido);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pPersona.fechanacimiento == null)
                            pfechanacimiento.Value = DBNull.Value;
                        else
                            pfechanacimiento.Value = pPersona.fechanacimiento;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        DbParameter pcodciudadnacimiento = cmdTransaccionFactory.CreateParameter();
                        pcodciudadnacimiento.ParameterName = "p_codciudadnacimiento";
                        if (pPersona.codciudadnacimiento == null)
                            pcodciudadnacimiento.Value = DBNull.Value;
                        else
                            pcodciudadnacimiento.Value = pPersona.codciudadnacimiento;
                        pcodciudadnacimiento.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadnacimiento);

                        DbParameter pcodestadocivil = cmdTransaccionFactory.CreateParameter();
                        pcodestadocivil.ParameterName = "p_codestadocivil";
                        if (pPersona.codestadocivil != null) pcodestadocivil.Value = pPersona.codestadocivil; else pcodestadocivil.Value = DBNull.Value;
                        pcodestadocivil.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodestadocivil);

                        DbParameter pcodescolaridad = cmdTransaccionFactory.CreateParameter();
                        pcodescolaridad.ParameterName = "p_codescolaridad";
                        pcodescolaridad.Value = pPersona.codescolaridad;
                        pcodescolaridad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodescolaridad);

                        DbParameter pcodactividad = cmdTransaccionFactory.CreateParameter();
                        pcodactividad.ParameterName = "p_codactividad";
                        if (!string.IsNullOrEmpty(pPersona.codactividadStr)) pcodactividad.Value = pPersona.codactividadStr; else pcodactividad.Value = DBNull.Value;
                        pcodactividad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodactividad);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (!string.IsNullOrEmpty(pPersona.direccion))
                            pdireccion.Value = pPersona.direccion;
                        else
                            pdireccion.Value = DBNull.Value;
                        pdireccion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (!string.IsNullOrEmpty(pPersona.telefono)) ptelefono.Value = pPersona.telefono; else ptelefono.Value = DBNull.Value;
                        ptelefono.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter pcodciudadresidencia = cmdTransaccionFactory.CreateParameter();
                        pcodciudadresidencia.ParameterName = "p_codciudadresidencia";
                        if (pPersona.codciudadresidencia != null) pcodciudadresidencia.Value = pPersona.codciudadresidencia; else pcodciudadresidencia.Value = DBNull.Value;
                        pcodciudadresidencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodciudadresidencia);

                        DbParameter pantiguedadlugar = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlugar.ParameterName = "p_antiguedadlugar";
                        pantiguedadlugar.Value = pPersona.antiguedadlugar;
                        pantiguedadlugar.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlugar);

                        DbParameter ptipovivienda = cmdTransaccionFactory.CreateParameter();
                        ptipovivienda.ParameterName = "p_tipovivienda";
                        if (!string.IsNullOrEmpty(pPersona.tipovivienda)) ptipovivienda.Value = pPersona.tipovivienda; else ptipovivienda.Value = DBNull.Value;
                        ptipovivienda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipovivienda);

                        DbParameter parrendador = cmdTransaccionFactory.CreateParameter();
                        parrendador.ParameterName = "p_arrendador";
                        if (!string.IsNullOrEmpty(pPersona.arrendador)) parrendador.Value = pPersona.arrendador; else parrendador.Value = DBNull.Value;
                        parrendador.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(parrendador);

                        DbParameter ptelefonoarrendador = cmdTransaccionFactory.CreateParameter();
                        ptelefonoarrendador.ParameterName = "p_telefonoarrendador";
                        if (!string.IsNullOrEmpty(pPersona.telefonoarrendador)) ptelefonoarrendador.Value = pPersona.telefonoarrendador; else ptelefonoarrendador.Value = DBNull.Value;
                        ptelefonoarrendador.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoarrendador);

                        DbParameter pvalorarriendo = cmdTransaccionFactory.CreateParameter();
                        pvalorarriendo.ParameterName = "p_valorarriendo";
                        if (pPersona.ValorArriendo != 0) pvalorarriendo.Value = pPersona.ValorArriendo; else pvalorarriendo.Value = DBNull.Value;
                        pvalorarriendo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalorarriendo);

                        DbParameter pcelular = cmdTransaccionFactory.CreateParameter();
                        pcelular.ParameterName = "p_celular";
                        if (!string.IsNullOrEmpty(pPersona.celular)) pcelular.Value = pPersona.celular; else pcelular.Value = DBNull.Value;
                        pcelular.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcelular);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (!string.IsNullOrEmpty(pPersona.email)) pemail.Value = pPersona.email; else pemail.Value = DBNull.Value;
                        pemail.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pPersona.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);


                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (!string.IsNullOrEmpty(pPersona.estado)) pestado.Value = pPersona.estado; else pestado.Value = DBNull.Value;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pPersona.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        if (pPersona.usuariocreacion != null) pusuariocreacion.Value = pPersona.usuariocreacion; else pusuariocreacion.Value = DBNull.Value;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        DbParameter pbarresidencia = cmdTransaccionFactory.CreateParameter();
                        pbarresidencia.ParameterName = "p_barresidencia";
                        if (pPersona.barrioResidencia != 0) pbarresidencia.Value = pPersona.barrioResidencia; else pbarresidencia.Value = DBNull.Value;
                        pbarresidencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pbarresidencia);

                        DbParameter pdircorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pdircorrespondencia.ParameterName = "p_dircorrespondencia";
                        if (pPersona.dirCorrespondencia != "" && pPersona.dirCorrespondencia != null) pdircorrespondencia.Value = pPersona.dirCorrespondencia; else pdircorrespondencia.Value = DBNull.Value;
                        pdircorrespondencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdircorrespondencia);

                        DbParameter pbarcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pbarcorrespondencia.ParameterName = "p_barcorrespondencia";
                        pbarcorrespondencia.Value = pPersona.barrioCorrespondencia;
                        pbarcorrespondencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pbarcorrespondencia);

                        DbParameter ptelcorrespondencia = cmdTransaccionFactory.CreateParameter();
                        ptelcorrespondencia.ParameterName = "p_telcorrespondencia";
                        if (pPersona.telCorrespondencia != "" && pPersona.telCorrespondencia != null) ptelcorrespondencia.Value = pPersona.telCorrespondencia; else ptelcorrespondencia.Value = DBNull.Value;
                        ptelcorrespondencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptelcorrespondencia);

                        DbParameter pciucorrespondencia = cmdTransaccionFactory.CreateParameter();
                        pciucorrespondencia.ParameterName = "p_ciucorrespondencia";
                        pciucorrespondencia.Value = pPersona.ciuCorrespondencia;
                        pciucorrespondencia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pciucorrespondencia);


                        DbParameter pnumpersonasacargo = cmdTransaccionFactory.CreateParameter();
                        pnumpersonasacargo.ParameterName = "p_numpersonasacargo";
                        if (pPersona.PersonasAcargo != null) pnumpersonasacargo.Value = pPersona.PersonasAcargo; else pnumpersonasacargo.Value = DBNull.Value;
                        pnumpersonasacargo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumpersonasacargo);

                        DbParameter pprofesion = cmdTransaccionFactory.CreateParameter();
                        pprofesion.ParameterName = "p_profesion";
                        if (pPersona.profecion != null) pprofesion.Value = pPersona.profecion; else pprofesion.Value = DBNull.Value;
                        pprofesion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pprofesion);

                        DbParameter pestrato = cmdTransaccionFactory.CreateParameter();
                        pestrato.ParameterName = "p_estrato";
                        if (pPersona.Estrato != null) pestrato.Value = pPersona.Estrato; else pestrato.Value = DBNull.Value;
                        pestrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestrato);

                        DbParameter pmujer_familia = cmdTransaccionFactory.CreateParameter();
                        pmujer_familia.ParameterName = "p_mujer_familia";
                        if (pPersona.mujer_familia != -1) pmujer_familia.Value = pPersona.mujer_familia; else pmujer_familia.Value = DBNull.Value;
                        pmujer_familia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pmujer_familia);

                        DbParameter pocupacion = cmdTransaccionFactory.CreateParameter();
                        pocupacion.ParameterName = "p_ocupacion";
                        if (pPersona.ocupacionApo != 0) pocupacion.Value = pPersona.ocupacionApo; else pocupacion.Value = DBNull.Value;
                        pocupacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pocupacion);

                        DbParameter pnacionalidad = cmdTransaccionFactory.CreateParameter();
                        pnacionalidad.ParameterName = "p_nacionalidad";
                        if (pPersona.nacionalidad != null) pnacionalidad.Value = pPersona.nacionalidad; else pnacionalidad.Value = DBNull.Value;
                        pnacionalidad.Direction = ParameterDirection.Input;
                        pnacionalidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnacionalidad);

                        DbParameter pubicacion_correspondencia = cmdTransaccionFactory.CreateParameter();
                        pubicacion_correspondencia.ParameterName = "p_ubicacion_correspondencia";
                        if (pPersona.ubicacion_correspondencia != null) pubicacion_correspondencia.Value = pPersona.ubicacion_correspondencia; else pubicacion_correspondencia.Value = DBNull.Value;
                        pubicacion_correspondencia.Direction = ParameterDirection.Input;
                       // pubicacion_correspondencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_correspondencia);

                        DbParameter pubicacion_residencia = cmdTransaccionFactory.CreateParameter();
                        pubicacion_residencia.ParameterName = "p_ubicacion_residencia";
                        if (pPersona.ubicacion_residencia != null) pubicacion_residencia.Value = pPersona.ubicacion_residencia; else pubicacion_residencia.Value = DBNull.Value;
                        pubicacion_residencia.Direction = ParameterDirection.Input;
                       // pubicacion_residencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_residencia);

                        DbParameter pparentescoempleado = cmdTransaccionFactory.CreateParameter();
                        pparentescoempleado.ParameterName = "p_parentescoempleado";
                        if (pPersona.relacionEmpleadosEmprender != 0) pparentescoempleado.Value = pPersona.relacionEmpleadosEmprender; else pparentescoempleado.Value = DBNull.Value;
                        pparentescoempleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pparentescoempleado);

                        DbParameter pparentesco_madminis = cmdTransaccionFactory.CreateParameter();
                        pparentesco_madminis.ParameterName = "p_parentesco_madminis";
                        if (pPersona.parentesco_madminis != null) pparentesco_madminis.Value = pPersona.parentesco_madminis; else pparentesco_madminis.Value = DBNull.Value;
                        pparentesco_madminis.Direction = ParameterDirection.Input;
                        //pparentesco_madminis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_madminis);

                        DbParameter pparentesco_mcontrol = cmdTransaccionFactory.CreateParameter();
                        pparentesco_mcontrol.ParameterName = "p_parentesco_mcontrol";
                        if (pPersona.parentesco_mcontrol != null) pparentesco_mcontrol.Value = pPersona.parentesco_mcontrol; else pparentesco_mcontrol.Value = DBNull.Value;
                        pparentesco_mcontrol.Direction = ParameterDirection.Input;
                       // pparentesco_mcontrol.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_mcontrol);

                        DbParameter pparentesco_pep = cmdTransaccionFactory.CreateParameter();
                        pparentesco_pep.ParameterName = "p_parentesco_pep";
                        if (pPersona.parentesco_pep != null) pparentesco_pep.Value = pPersona.parentesco_pep; else pparentesco_pep.Value = DBNull.Value;
                        pparentesco_pep.Direction = ParameterDirection.Input;
                        //pparentesco_pep.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pparentesco_pep);

                        DbParameter pcod_zona = cmdTransaccionFactory.CreateParameter();
                        pcod_zona.ParameterName = "p_cod_zona";
                        if (pPersona.zona != null) pcod_zona.Value = pPersona.zona; else pcod_zona.Value = DBNull.Value;
                        pcod_zona.Direction = ParameterDirection.Input;
                        pcod_zona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_zona);

                        //agregado -- acceso o no a Oficina Virtual
                        DbParameter pacceso_oficina = cmdTransaccionFactory.CreateParameter();
                        pacceso_oficina.ParameterName = "p_acceso_oficina";
                        pacceso_oficina.Value = pPersona.acceso_oficina;
                        pacceso_oficina.Direction = ParameterDirection.Input;
                        pacceso_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pacceso_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TPERSONAL_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TPERSONAL_MOD";//MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                        {
                            if (opcion == 1)
                            {
                                DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Aportes, "Creacion de aportes para la persona");
                            }
                            else
                            {
                                DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Modificar.ToString());
                            }
                        }

                        pPersona.cod_persona = Convert.ToInt64(pcod_persona.Value);

                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "TabPersonal", ex);
                        return null;
                    }
                }
            }
        }

        public Persona1 TabLaboral(Persona1 pPersona, Usuario vUsuario)
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
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter ptipo_empresa = cmdTransaccionFactory.CreateParameter();
                        ptipo_empresa.ParameterName = "p_tipo_empresa";
                        if (pPersona.tipo_empresa != null) ptipo_empresa.Value = pPersona.tipo_empresa; else ptipo_empresa.Value = DBNull.Value;
                        ptipo_empresa.Direction = ParameterDirection.Input;
                        ptipo_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_empresa);

                        DbParameter pempresa = cmdTransaccionFactory.CreateParameter();
                        pempresa.ParameterName = "p_empresa";
                        if (pPersona.empresa != null) pempresa.Value = pPersona.empresa; else pempresa.Value = DBNull.Value;
                        pempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pempresa);

                        DbParameter pnit_empresa = cmdTransaccionFactory.CreateParameter();
                        pnit_empresa.ParameterName = "p_nit_empresa";
                        if (pPersona.nit_empresa != null) pnit_empresa.Value = pPersona.nit_empresa; else pnit_empresa.Value = DBNull.Value;
                        pnit_empresa.Direction = ParameterDirection.Input;
                        pnit_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnit_empresa);

                        DbParameter pact_ciiu_empresa = cmdTransaccionFactory.CreateParameter();
                        pact_ciiu_empresa.ParameterName = "p_act_ciiu_empresa";
                        if (pPersona.act_ciiu_empresa != null) pact_ciiu_empresa.Value = pPersona.act_ciiu_empresa; else pact_ciiu_empresa.Value = DBNull.Value;
                        pact_ciiu_empresa.Direction = ParameterDirection.Input;
                        pact_ciiu_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pact_ciiu_empresa);

                        DbParameter pactividadempresa = cmdTransaccionFactory.CreateParameter();
                        pactividadempresa.ParameterName = "p_actividadempresa";
                        if (pPersona.ActividadEconomicaEmpresaStr != null) pactividadempresa.Value = pPersona.ActividadEconomicaEmpresaStr; else pactividadempresa.Value = DBNull.Value;
                        pactividadempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pactividadempresa);

                        DbParameter pcodcargo = cmdTransaccionFactory.CreateParameter();
                        pcodcargo.ParameterName = "p_codcargo";
                        if (pPersona.codcargo != 0) pcodcargo.Value = pPersona.codcargo; else pcodcargo.Value = DBNull.Value;
                        pcodcargo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodcargo);

                        DbParameter pcodtipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodtipocontrato.ParameterName = "p_codtipocontrato";
                        if (pPersona.codtipocontrato != 0) pcodtipocontrato.Value = pPersona.codtipocontrato; else pcodtipocontrato.Value = DBNull.Value;
                        pcodtipocontrato.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodtipocontrato);

                        DbParameter pfecha_ingresoempresa = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingresoempresa.ParameterName = "p_fecha_ingresoempresa";
                        if (pPersona.fecha_ingresoempresa != DateTime.MinValue) pfecha_ingresoempresa.Value = pPersona.fecha_ingresoempresa; else pfecha_ingresoempresa.Value = DBNull.Value;
                        pfecha_ingresoempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingresoempresa);

                        DbParameter pantiguedadlugarempresa = cmdTransaccionFactory.CreateParameter();
                        pantiguedadlugarempresa.ParameterName = "p_antiguedadlugarempresa";
                        pantiguedadlugarempresa.Value = pPersona.antiguedadlugarempresa;
                        pantiguedadlugarempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pantiguedadlugarempresa);

                        DbParameter pcod_sector = cmdTransaccionFactory.CreateParameter();
                        pcod_sector.ParameterName = "p_cod_sector";
                        if (pPersona.sector != null) pcod_sector.Value = pPersona.sector; else pcod_sector.Value = DBNull.Value;
                        pcod_sector.Direction = ParameterDirection.Input;
                        pcod_sector.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_sector);

                        DbParameter pcod_empleado = cmdTransaccionFactory.CreateParameter();
                        pcod_empleado.ParameterName = "p_codigo_nomina";
                        if (pPersona.cod_nomina_empleado == null) { pcod_empleado.Value = DBNull.Value; } else { pcod_empleado.Value = pPersona.cod_nomina_empleado; }
                        pcod_empleado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_empleado);

                        DbParameter escalafon = cmdTransaccionFactory.CreateParameter();
                        escalafon.ParameterName = "P_IDESCALAFON";
                        if (pPersona.idescalafon == 0) escalafon.Value = DBNull.Value; else escalafon.Value = pPersona.idescalafon;
                        escalafon.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(escalafon);

                        DbParameter pempleado_entidad = cmdTransaccionFactory.CreateParameter();
                        pempleado_entidad.ParameterName = "p_empleado_entidad";
                        pempleado_entidad.Value = pPersona.empleado_entidad;
                        pempleado_entidad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pempleado_entidad);

                        DbParameter pjornada_laboral = cmdTransaccionFactory.CreateParameter();
                        pjornada_laboral.ParameterName = "p_jornada_laboral";
                        pjornada_laboral.Value = pPersona.jornada_laboral;
                        pjornada_laboral.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pjornada_laboral);

                        DbParameter pubicacion_empresa = cmdTransaccionFactory.CreateParameter();
                        pubicacion_empresa.ParameterName = "p_ubicacion_empresa";
                        if (pPersona.ubicacion_empresa != null) pubicacion_empresa.Value = pPersona.ubicacion_empresa; else pubicacion_empresa.Value = DBNull.Value;
                        pubicacion_empresa.Direction = ParameterDirection.Input;
                        pubicacion_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pubicacion_empresa);

                        DbParameter pdireccionempresa = cmdTransaccionFactory.CreateParameter();
                        pdireccionempresa.ParameterName = "p_direccionempresa";
                        if (pPersona.direccionempresa != null) pdireccionempresa.Value = pPersona.direccionempresa; else pdireccionempresa.Value = DBNull.Value;
                        pdireccionempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdireccionempresa);

                        DbParameter pciudadempresa = cmdTransaccionFactory.CreateParameter();
                        pciudadempresa.ParameterName = "p_ciudadempresa";
                        if (pPersona.ciudad != null) pciudadempresa.Value = pPersona.ciudad; else pciudadempresa.Value = DBNull.Value;
                        pciudadempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pciudadempresa);

                        DbParameter pcelularempresa = cmdTransaccionFactory.CreateParameter();
                        pcelularempresa.ParameterName = "p_celularempresa";
                        if (pPersona.CelularEmpresa != null && pPersona.CelularEmpresa != "") pcelularempresa.Value = pPersona.CelularEmpresa; else pcelularempresa.Value = DBNull.Value;
                        pcelularempresa.Direction = ParameterDirection.Input;
                        pcelularempresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcelularempresa);

                        //DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        //pemail.ParameterName = "P_WEBEMAIL_EMPRESA";
                        //if (pPersona.email_empresa != null) pemail.Value = pPersona.email_empresa; else pemail.Value = DBNull.Value;
                        //pemail.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter ptelefonoempresa = cmdTransaccionFactory.CreateParameter();
                        ptelefonoempresa.ParameterName = "p_telefonoempresa";
                        if (pPersona.telefonoempresa != null) ptelefonoempresa.Value = pPersona.telefonoempresa; else ptelefonoempresa.Value = DBNull.Value;
                        ptelefonoempresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptelefonoempresa);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pPersona.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuultmod = cmdTransaccionFactory.CreateParameter();
                        pusuultmod.ParameterName = "p_usuultmod";
                        pusuultmod.Value = pPersona.usuultmod;
                        pusuultmod.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pusuultmod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TLABORAL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (vUsuario.programaGeneraLog)
                        {
                            DAauditoria.InsertarLog(pPersona, "PERSONA", vUsuario, Accion.Modificar.ToString());
                        }

                        pPersona.cod_persona = Convert.ToInt64(pcod_persona.Value);

                        return pPersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "TabLaboral", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Crea una solicitud de retiro de un asociado
        /// </summary>
        /// <param name="pPersona"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public int CrearSolicitudRetiro(Persona1 pPersona, Usuario pUsuario)
        {
            int salida = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                         
                        DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_RETIRO";
                        P_ID_SOL_RETIRO.Value = 0;
                        P_ID_SOL_RETIRO.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = Convert.ToInt32(pPersona.cod_persona);
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_COD_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        P_COD_MOTIVO.ParameterName = "P_COD_MOTIVO";
                        P_COD_MOTIVO.Value = pPersona.cod_motivo;
                        P_COD_MOTIVO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_MOTIVO);

                        DbParameter P_OBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        P_OBSERVACIONES.ParameterName = "P_OBSERVACIONES";
                        P_OBSERVACIONES.Value = pPersona.Observaciones;
                        P_OBSERVACIONES.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_OBSERVACIONES);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = Convert.ToInt32(pPersona.estado);
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOL_RETIRO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (P_ID_SOL_RETIRO != null)
                        {
                            salida = Convert.ToInt32(P_ID_SOL_RETIRO.Value);
                        }

                        return salida;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public void InsertarRespuestasRetiro(List<Persona1> pPersona, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    cmdTransaccionFactory.Connection = connection;                    
                    // Cargar las cuotas extras
                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                    if (pPersona != null)
                    {
                        foreach (Persona1 item in pPersona)
                        {
                            cmdTransaccionFactory.Parameters.Clear();

                            //P_ID_SOL_RETIRO IN NUMBER,
                            //P_PREGUNTA PREGUNTA_RETIRO.PREGUNTA % TYPE,
                            //P_RESPUESTA
                            DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                            P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_RETIRO";
                            P_ID_SOL_RETIRO.Value = Convert.ToInt32(item.id_solicitud);
                            P_ID_SOL_RETIRO.Direction = ParameterDirection.Input;
                            P_ID_SOL_RETIRO.DbType = DbType.Int32;
                            cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                            DbParameter P_PREGUNTA = cmdTransaccionFactory.CreateParameter();
                            P_PREGUNTA.ParameterName = "P_PREGUNTA";
                            P_PREGUNTA.Value = item.pregunta;
                            P_PREGUNTA.Direction = ParameterDirection.Input;
                            P_PREGUNTA.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(P_PREGUNTA);

                            DbParameter P_RESPUESTA = cmdTransaccionFactory.CreateParameter();
                            P_RESPUESTA.ParameterName = "P_RESPUESTA";
                            P_RESPUESTA.Value = item.respuesta;
                            P_RESPUESTA.Direction = ParameterDirection.Input;
                            P_RESPUESTA.DbType = DbType.String;
                            cmdTransaccionFactory.Parameters.Add(P_RESPUESTA);
                            
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PREGRETIRO_CRE";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                    }

                    try
                    {
                        foreach (var item in pPersona)
                        {
                            
                        }
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }


        public decimal ConsultarNivelEndeudamiento(string identificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            decimal _nivelEndeudamiento = 0;
            decimal salario = 0, sueldo_persona = 0, honorarios = 0, otros_ingresos = 0;
            decimal _totalIngresos = 0, _totalEgresos = 0;
            decimal cuota = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql1 = @"SELECT p.salario, e.sueldo_persona, e.honorarios, e.otros_ingresos
                                        FROM PERSONA p LEFT JOIN INFORMACION_INGRE_EGRE e ON p.cod_persona = e.cod_persona
                                        WHERE p.identificacion = '" + identificacion.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql1;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["SALARIO"] != DBNull.Value) salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["SUELDO_PERSONA"] != DBNull.Value) sueldo_persona = Convert.ToDecimal(resultado["SUELDO_PERSONA"]);
                            if (resultado["HONORARIOS"] != DBNull.Value) honorarios = Convert.ToDecimal(resultado["HONORARIOS"]);
                            if (resultado["OTROS_INGRESOS"] != DBNull.Value) otros_ingresos = Convert.ToDecimal(resultado["OTROS_INGRESOS"]);
                            if (sueldo_persona == 0)
                                sueldo_persona = salario;
                            _totalIngresos = sueldo_persona + honorarios + otros_ingresos;
                        }
                  
                        string sql2 = @"SELECT Sum(e.cuota) As cuota
                                        FROM PERSONA p LEFT JOIN APORTE e ON p.cod_persona = e.cod_persona
                                        WHERE p.identificacion = '" + identificacion.ToString() + "' AND e.estado = 1 ";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql2;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            cuota = 0;
                            if (resultado["CUOTA"] != DBNull.Value) cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            _totalEgresos = _totalEgresos + cuota;
                        }

                        string sql3 = @"SELECT Sum(e.valor_cuota) As cuota
                                        FROM PERSONA p LEFT JOIN CREDITO e ON p.cod_persona = e.cod_deudor
                                        WHERE p.identificacion = '" + identificacion.ToString() + "' AND e.estado = 'C' ";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql3;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            cuota = 0;
                            if (resultado["CUOTA"] != DBNull.Value) cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            _totalEgresos = _totalEgresos + cuota;
                        }

                        string sql4 = @"SELECT Sum(e.valor_cuota) As cuota
                                        FROM PERSONA p LEFT JOIN AHORRO_VISTA e ON p.cod_persona = e.cod_persona
                                        WHERE p.identificacion = '" + identificacion.ToString() + "' AND e.estado IN (0, 1) ";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql4;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            cuota = 0;
                            if (resultado["CUOTA"] != DBNull.Value) cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            _totalEgresos = _totalEgresos + cuota;
                        }

                        string sql5 = @"SELECT Sum(e.valor_cuota) As cuota
                                        FROM PERSONA p LEFT JOIN AHORRO_PROGRAMADO e ON p.cod_persona = e.cod_persona
                                        WHERE p.identificacion = '" + identificacion.ToString() + "' AND e.estado IN (0, 1) ";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql5;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            cuota = 0;
                            if (resultado["CUOTA"] != DBNull.Value) cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            _totalEgresos = _totalEgresos + cuota;
                        }

                        // Sumar el porcentaje de descuentos salud
                        _totalEgresos += Convert.ToDecimal(Math.Round(Convert.ToDouble(sueldo_persona) * 0.09));

                        if (_totalEgresos != 0)
                            _nivelEndeudamiento = Math.Round( (_totalEgresos/_totalIngresos) * 100, 2);

                        dbConnectionFactory.CerrarConexion(connection);
                        return _nivelEndeudamiento;
                    }
                    catch (Exception ex)
                    {
                        return _nivelEndeudamiento;
                    }
                }
            }
        }

        public Int64? ConsultarCodigoPersona(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64? _cod_persona = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "SELECT persona.COD_PERSONA FROM persona WHERE persona.IDENTIFICACION = '" + pIdentificacion.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) _cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);                           
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return _cod_persona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultarCodigoPersona", ex);
                        return null;
                    }
                }
            }
        }



    }
}