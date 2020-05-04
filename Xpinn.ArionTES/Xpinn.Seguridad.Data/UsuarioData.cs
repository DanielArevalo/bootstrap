using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla USUARIOS
    /// </summary>
    public class UsuarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla USUARIOS
        /// </summary>
        public UsuarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public PersonaUsuario CrearPersonaUsuario(PersonaUsuario pAPP, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidacceso = cmdTransaccionFactory.CreateParameter();
                        pidacceso.ParameterName = "p_idacceso";
                        pidacceso.Value = pAPP.idacceso;
                        pidacceso.Direction = ParameterDirection.Output;
                        pidacceso.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidacceso);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAPP.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pclave = cmdTransaccionFactory.CreateParameter();
                        pclave.ParameterName = "p_clave";
                        pclave.Value = pAPP.clave;
                        pclave.Direction = ParameterDirection.Input;
                        pclave.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pclave);

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "p_fecha_creacion";
                        pfecha_creacion.Value = pAPP.fecha_creacion;
                        pfecha_creacion.Direction = ParameterDirection.Input;
                        pfecha_creacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pAPP.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pnombres = cmdTransaccionFactory.CreateParameter();
                        pnombres.ParameterName = "p_nombres";
                        if (pAPP.nombres != null) pnombres.Value = pAPP.nombres; else pnombres.Value = DBNull.Value;
                        pnombres.Direction = ParameterDirection.Input;
                        pnombres.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombres);

                        DbParameter papellidos = cmdTransaccionFactory.CreateParameter();
                        papellidos.ParameterName = "p_apellidos";
                        if (pAPP.apellidos != null) papellidos.Value = pAPP.apellidos; else papellidos.Value = DBNull.Value;
                        papellidos.Direction = ParameterDirection.Input;
                        papellidos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(papellidos);

                        DbParameter pemail = cmdTransaccionFactory.CreateParameter();
                        pemail.ParameterName = "p_email";
                        if (pAPP.email != null) pemail.Value = pAPP.email; else pemail.Value = DBNull.Value;
                        pemail.Direction = ParameterDirection.Input;
                        pemail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pemail);

                        DbParameter pfechanacimiento = cmdTransaccionFactory.CreateParameter();
                        pfechanacimiento.ParameterName = "p_fechanacimiento";
                        if (pAPP.fechanacimiento != null) pfechanacimiento.Value = pAPP.fechanacimiento; else pfechanacimiento.Value = DBNull.Value;
                        pfechanacimiento.Direction = ParameterDirection.Input;
                        pfechanacimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechanacimiento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APP_PERS_ACCES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pAPP.idacceso = Convert.ToInt64(pidacceso.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAPP;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearPersonaUsuario", ex);
                        return null;
                    }
                }
            }
        }


        public PersonaUsuario ConsultarPersonaUsuario(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            PersonaUsuario entidad = new PersonaUsuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_PERSONA FROM PERSONA_ACCESO WHERE COD_PERSONA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarPersonaUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public Usuario ValidarUsuarioOficina(string pUsuario, string password, string ip, Usuario vUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select u.codusuario, u.identificacion, u.login, u.estado, u.Codperfil
                                       from usuarios u
                                       inner join usuario_ipacceso i on u.codusuario = i.codusuario
                                       where u.identificacion = '" + pUsuario+"' and i.direccionip = '"+ip+"'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["codusuario"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["codusuario"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["login"] != DBNull.Value) entidad.login = Convert.ToString(resultado["login"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["Codperfil"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["Codperfil"]);
                            
                        }
                        else
                        {
                            entidad.nombre = "error";
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public Usuario CrearUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = ObtenerSiguienteCodigo(vUsuario);
                        pCODUSUARIO.Direction = ParameterDirection.InputOutput;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_identificacion";
                        pIDENTIFICACION.Value = pUsuario.identificacion;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pUsuario.nombre;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_direccion";
                        pDIRECCION.Value = pUsuario.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_telefono";
                        pTELEFONO.Value = pUsuario.telefono;

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = "p_login";
                        pLOGIN.Value = pUsuario.login;

                        DbParameter pCLAVE = cmdTransaccionFactory.CreateParameter();
                        pCLAVE.ParameterName = "p_clave";
                        pCLAVE.Value = pUsuario.clave;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_fechaCreacion";
                        pFECHACREACION.Value = pUsuario.fechacreacion;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        pESTADO.Value = pUsuario.estado;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pUsuario.codperfil;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = "p_cod_oficina";
                        pCOD_OFICINA.Value = pUsuario.cod_oficina;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_cod_persona";
                        if (pUsuario.cod_persona > 0)
                            pCOD_PERSONA.Value = pUsuario.cod_persona;
                        else pCOD_PERSONA.Value = DBNull.Value;



                        DbParameter P_COD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_COD_CUENTA.ParameterName = "P_COD_CUENTA";
                        P_COD_CUENTA.Value = pUsuario.cod_cuenta;



                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCLAVE);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(P_COD_CUENTA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pUsuario.codusuario = Convert.ToInt64(pCODUSUARIO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public bool ValidarActualizacion(Int64 cod_persona, string fecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool salida = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT distinct(u.COD_PERSONA) FROM USUARIO_INGRESO u
                                        WHERE u.COD_PERSONA = "+cod_persona+@"
                                        AND u.Fecha_Horaingreso > TO_DATE('"+fecha+"', 'YYYY-MM-DD')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) salida = true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return salida;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ValidarActualizacion", ex);
                        return false;
                    }
                }
            }
        }



        /// <summary>
        /// Modifica un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Usuario modificada</returns>
        public Usuario ModificarUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_CODUSUARIO";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        pIDENTIFICACION.Value = pUsuario.identificacion;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pUsuario.nombre;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        pDIRECCION.Value = pUsuario.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        pTELEFONO.Value = pUsuario.telefono;

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = "p_LOGIN";
                        pLOGIN.Value = pUsuario.login;

                        DbParameter pCLAVE = cmdTransaccionFactory.CreateParameter();
                        pCLAVE.ParameterName = "p_clave";
                        pCLAVE.Value = pUsuario.clave;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHACREACION.Value = pUsuario.fechacreacion;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pUsuario.estado;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_CODPERFIL";
                        pCODPERFIL.Value = pUsuario.codperfil;

                        DbParameter pCOD_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCOD_OFICINA.ParameterName = "p_COD_OFICINA";
                        pCOD_OFICINA.Value = pUsuario.cod_oficina;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_cod_persona";
                        if (pUsuario.cod_persona > 0 && pUsuario.cod_persona != null)
                            pCOD_PERSONA.Value = pUsuario.cod_persona;
                        else pCOD_PERSONA.Value = DBNull.Value;

                        DbParameter P_COD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_COD_CUENTA.ParameterName = "P_COD_CUENTA";
                        P_COD_CUENTA.Value = pUsuario.cod_cuenta;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCLAVE);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(P_COD_CUENTA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIOS_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        /*if (pUsuario.estado == 1)
                            DesbloquearUsuario(pUsuario, vUsuario);*/

                        return pUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ModificarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public void DesbloquearUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string manejoUsuarios = ConfigurationManager.AppSettings["ManejoUsuarios"];
                    if (manejoUsuarios == "1")
                    {
                        
                        DbParameter p_nom_usuario = cmdTransaccionFactory.CreateParameter();
                        p_nom_usuario.ParameterName = "p_nom_usuario";
                        p_nom_usuario.Value = "U" + pUsuario.identificacion;
                        cmdTransaccionFactory.Parameters.Add(p_nom_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USU_DESBLOQUEAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de USUARIOS</param>
        public void EliminarUsuario(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuario pUsuario = new Usuario();

                        if (vUsuario.programaGeneraLog)
                            pUsuario = ConsultarUsuario(pId, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIOS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarUsuario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla USUARIOS</param>
        /// <returns>Entidad Usuario consultado</returns>
        public Usuario ConsultarUsuario(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.nombre As NomEmpresa, empresa.nit As NitEmpresa, empresa.tipo, persona.IDENTIFICACION AS DOCUMENTO " +
                                     " FROM usuarios " +
                                     " INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil " +
                                     " INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina " +
                                     " LEFT JOIN empresa ON empresa.cod_empresa = 1 LEFT JOIN persona ON PERSONA.COD_PERSONA = USUARIOS.COD_PERSONA " +
                                     " WHERE usuarios.CODUSUARIO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["NOMEMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOMEMPRESA"]);
                            if (resultado["NITEMPRESA"] != DBNull.Value) entidad.nitempresa = Convert.ToString(resultado["NITEMPRESA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DOCUMENTO"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["DOCUMENTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
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
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla USUARIOS dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuario obtenidos</returns>
        public List<Usuario> ListarUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Usuario> lstUsuario = new List<Usuario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  USUARIOS " + ObtenerFiltro(pUsuario);
                        if (vUsuario.codperfil != 1)
                            sql += sql.Contains("WHERE") ? " AND CODPERFIL != 1" : " WHERE CODPERFIL != 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Usuario entidad = new Usuario();

                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);


                            lstUsuario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;
            Acceso entidad = new Acceso();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(codusuario) + 1 FROM  USUARIOS ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }

        public string ValidarUsuarioEstado(string pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT usuarios.estado
                                        FROM usuarios 
                                        WHERE usuarios.identificacion = '" + pUsuario + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);

                        }
                        else
                            entidad.estado = 0;
                        dbConnectionFactory.CerrarConexion(connection);
                        return Convert.ToString(entidad.estado);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public string ValidarUsuarioip(string pUsuBD, Usuario pUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pUsuBD = string.IsNullOrEmpty(pUsuBD) ? pUsuario.identificacion : pUsuBD;
                        string sql = @"select * from usuario_ipacceso where codusuario in (SELECT usuarios.codusuario "
                                          + "FROM usuarios WHERE QUITARESPACIOS(UPPER(usuarios.identificacion)) = QUITARESPACIOS(UPPER('" + pUsuBD + "')))";
                        if (pUsuario.identificacion.ToUpper() == "XPINNADM" || pUsuario.identificacion.ToUpper() == "TARJETAD" || pUsuario.identificacion.ToUpper() == "BIOMETRIA")
                            sql = @"select * from usuario_ipacceso where codusuario in (SELECT usuarios.codusuario "
                                          + "FROM usuarios WHERE QUITARESPACIOS(UPPER(usuarios.identificacion)) = QUITARESPACIOS(UPPER('" + pUsuBD + "')))";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string respuesta;
                        if (resultado.Read())
                        {
                            respuesta = "1";
                            dbConnectionFactory.CerrarConexion(connection);
                            return respuesta;
                        }
                        else
                        {
                            respuesta = "0";
                            dbConnectionFactory.CerrarConexion(connection);
                            return respuesta;
                        }

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }




        public string ValidarUsuarioMac(string pUsuBD, Usuario pUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pUsuBD = string.IsNullOrEmpty(pUsuBD) ? pUsuario.identificacion : pUsuBD;
                        string sql = @"select * from usuario_Macacceso where codusuario in (SELECT usuarios.codusuario "
                                                     + "FROM usuarios WHERE QUITARESPACIOS(UPPER(usuarios.identificacion)) = QUITARESPACIOS(UPPER('" + pUsuBD + "')))";
                        if (pUsuario.identificacion.ToUpper() == "XPINNADM" || pUsuario.identificacion.ToUpper() == "TARJETAD")
                            sql = @"select * from usuario_Macacceso where codusuario in (SELECT usuarios.codusuario "
                                                     + "FROM usuarios WHERE QUITARESPACIOS(UPPER(usuarios.identificacion)) = QUITARESPACIOS(UPPER('" + pUsuBD + "')))";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string respuesta;
                        if (resultado.Read())
                        {
                            respuesta = "1";
                            dbConnectionFactory.CerrarConexion(connection);
                            return respuesta;
                        }
                        else
                        {
                            respuesta = "0";
                            dbConnectionFactory.CerrarConexion(connection);
                            return respuesta;
                        }

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }







        /// <summary>
        /// Valida el usuario que ingresa al sistema
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public Usuario ValidarUsuario(string pUsuario, string pClave, string validaip, string ip,string validamac, string mac, Usuario vUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        string condicion = " QUITARESPACIOS(UPPER(identificacion)) = QUITARESPACIOS(UPPER('" + pUsuario + "')) ";
                        if (pUsuario.ToUpper() == "XPINNADM")
                            condicion = " Upper(identificacion) = '" + pUsuario + "' ";
                        if ( pUsuario.ToUpper() == "TARJETAD")
                        { 
                            condicion = " Upper(identificacion) = '" + pUsuario + "' ";
                            validaip = "0";
                        }

                        if (validaip == "" || validaip == "0")
                            sql = "SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.revisor, empresa.cod_empresa, empresa.nit, empresa.nombre As nom_empresa, " + 
                                        " empresa.representante_legal, empresa.contador, empresa.tarjeta_contador, empresa.tipo, empresa.Reporte_Egreso, empresa.Reporte_Ingreso, empresa.cod_persona, " +
                                        " empresa.cod_uiaf, empresa.direccion as DirEmpresa, empresa.version_pl, empresa.sigla, perfil_usuario.administrador " +
                                        " FROM usuarios " +
                                        " INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil " +
                                        " INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina " +
                                        " LEFT JOIN empresa ON empresa.cod_empresa = 1 " +
                                        " WHERE " + condicion + " AND login = '" + pClave + "'";

                        if (validaip == "1")
                            sql = @"SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.revisor, empresa.cod_empresa, empresa.nit, empresa.nombre As nom_empresa, 
                                     empresa.representante_legal, empresa.contador, empresa.tarjeta_contador, empresa.tipo, empresa.Reporte_Egreso, empresa.Reporte_Ingreso, empresa.cod_persona,
                                     empresa.cod_uiaf, empresa.direccion as DirEmpresa, empresa.version_pl, empresa.sigla, perfil_usuario.administrador
                                     FROM usuarios 
                                     INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil 
                                     INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina
                                     INNER join usuario_ipacceso ip on ip.codusuario=usuarios.codusuario
                                     LEFT JOIN empresa ON empresa.cod_empresa = 1 
                                     WHERE " + condicion + " AND login = '" + pClave + "' And  DIRECCIONIP= '" + ip + "'";


                        if (validamac == "1")
                            sql = @"SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.REVISOR, empresa.cod_empresa, empresa.nit, empresa.nombre As nom_empresa, 
                                     empresa.representante_legal, empresa.contador, empresa.tarjeta_contador, empresa.tipo, empresa.Reporte_Egreso, empresa.Reporte_Ingreso, empresa.cod_persona,
                                     empresa.cod_uiaf, empresa.direccion as DirEmpresa, empresa.version_pl, empresa.sigla, perfil_usuario.administrador
                                     FROM usuarios 
                                     INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil 
                                     INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina
                                     INNER join usuario_Macacceso ip on ip.codusuario=usuarios.codusuario
                                     LEFT JOIN empresa ON empresa.cod_empresa = 1 
                                     WHERE " + condicion + " AND login = '" + pClave + "' And  DIRECCIONMAC= '" + mac + "'";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["DIREMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIREMPRESA"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["CODPERFIL_HELPDESK"] != DBNull.Value) entidad.codperfilhelpdesk = Convert.ToInt64(resultado["CODPERFIL_HELPDESK"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.idEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla_empresa = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nitempresa = Convert.ToString(resultado["NIT"]);
                            if (resultado["REPRESENTANTE_LEGAL"] != DBNull.Value) entidad.representante_legal = Convert.ToString(resultado["REPRESENTANTE_LEGAL"]);
                            if (resultado["CONTADOR"] != DBNull.Value) entidad.contador = Convert.ToString(resultado["CONTADOR"]);
                            if (resultado["TARJETA_CONTADOR"] != DBNull.Value) entidad.tarjeta_contador = Convert.ToString(resultado["TARJETA_CONTADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["REPORTE_EGRESO"] != DBNull.Value) entidad.reporte_egreso = Convert.ToString(resultado["REPORTE_EGRESO"]);
                            if (resultado["REPORTE_INGRESO"] != DBNull.Value) entidad.reporte_ingreso = Convert.ToString(resultado["REPORTE_INGRESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["REVISOR"] != DBNull.Value) entidad.revisor_Fiscal = Convert.ToString(resultado["REVISOR"]);
                            if (resultado["COD_UIAF"] != DBNull.Value) entidad.cod_uiaf = Convert.ToString(resultado["COD_UIAF"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["VERSION_PL"] != DBNull.Value) entidad.version_pl = Convert.ToString(resultado["VERSION_PL"]);
                            if (resultado["ADMINISTRADOR"] != DBNull.Value) entidad.administrador = Convert.ToString(resultado["ADMINISTRADOR"]);
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
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public byte[] ObtenerLogoEmpresaIniciar(Usuario pUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT LOGOEMPRESA FROM empresa WHERE COD_EMPRESA = " + pUsuario.idEmpresa;
                        byte[] imagenData = null;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["LOGOEMPRESA"] != DBNull.Value) imagenData = (byte[])resultado["LOGOEMPRESA"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return imagenData;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public Usuario ValidarUsuarioSinClave(string pUsuario, string validaip, string ip, Usuario vUsuario)
        {
            DbDataReader resultado;
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string condicion = " identificacion = '" + pUsuario + "' ";
                        if (pUsuario.ToUpper() == "XPINNADM" || pUsuario.ToUpper() == "BIOMETRIA")
                            condicion = " Upper(identificacion) = '" + pUsuario + "' ";
                        if (validaip == "" || validaip == "0")
                            sql = "SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.cod_empresa, empresa.nit, empresa.nombre As nom_empresa, empresa.representante_legal, empresa.contador, empresa.tarjeta_contador, empresa.tipo, empresa.Reporte_Egreso, empresa.Reporte_Ingreso, empresa.cod_persona,empresa.cod_uiaf " +
                                        " FROM usuarios " +
                                        " INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil " +
                                        " INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina " +
                                        " LEFT JOIN empresa ON empresa.cod_empresa = 1 " +
                                        " WHERE " + condicion;

                        if (validaip == "1")
                            sql = @"SELECT usuarios.*, perfil_usuario.nombreperfil, oficina.nombre nombre_oficina, empresa.cod_empresa, empresa.nit, empresa.nombre As nom_empresa, empresa.representante_legal, empresa.contador, empresa.tarjeta_contador, empresa.tipo, empresa.Reporte_Egreso, empresa.Reporte_Ingreso, empresa.cod_persona,empresa.cod_uiaf
                                     FROM usuarios 
                                     INNER JOIN perfil_usuario ON usuarios.codperfil = perfil_usuario.codperfil 
                                     INNER JOIN oficina ON usuarios.cod_oficina = oficina.cod_oficina
                                     INNER join usuario_ipacceso ip on ip.codusuario=usuarios.codusuario
                                     LEFT JOIN empresa ON empresa.cod_empresa = 1 
                                     WHERE " + condicion + " And direccionIP= '" + ip + "'";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.login = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["LOGIN"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["LOGIN"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.idEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nitempresa = Convert.ToString(resultado["NIT"]);
                            if (resultado["REPRESENTANTE_LEGAL"] != DBNull.Value) entidad.representante_legal = Convert.ToString(resultado["REPRESENTANTE_LEGAL"]);
                            if (resultado["CONTADOR"] != DBNull.Value) entidad.contador = Convert.ToString(resultado["CONTADOR"]);
                            if (resultado["TARJETA_CONTADOR"] != DBNull.Value) entidad.tarjeta_contador = Convert.ToString(resultado["TARJETA_CONTADOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["REPORTE_EGRESO"] != DBNull.Value) entidad.reporte_egreso = Convert.ToString(resultado["REPORTE_EGRESO"]);
                            if (resultado["REPORTE_INGRESO"] != DBNull.Value) entidad.reporte_ingreso = Convert.ToString(resultado["REPORTE_INGRESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_UIAF"] != DBNull.Value) entidad.cod_uiaf = Convert.ToString(resultado["COD_UIAF"]);

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
                        BOExcepcion.Throw("UsuarioData", "ConsultarUsuario", ex);
                        return null;
                    }
                }
            }
        }


        public Xpinn.FabricaCreditos.Entities.Persona1 ValidarPersonaUsuario(string pUsuario, string pClave, Usuario vUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Persona1 entidad = new Xpinn.FabricaCreditos.Entities.Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PA.COD_PERSONA,V.IDENTIFICACION,trim(PA.NOMBRES)||' '||trim(PA.APELLIDOS) as NOMBRE, V.DIRECCION, V.TELEFONO, PA.CLAVE, PA.FECHA_CREACION AS FECHACREACION, 
                                        PA.EMAIL, V.TIPO_PERSONA, PA.FECHANACIMIENTO, V.TIPO_IDENTIFICACION, T.DESCRIPCION AS NOM_TIPOIDENTIFICACION, V.COD_OFICINA,
                                        O.NOMBRE AS NOMBRE_OFICINA, O.COD_EMPRESA, E.NOMBRE AS NOM_EMPRESA, E.NIT, NVL(A.ESTADO, V.ESTADO) AS ESTADO,
                                        V.SEXO, PA.NOMBRES, PA.APELLIDOS, E.URL_DROID_APP, E.URL_IOS_APP, E.VERSION_APP, E.FEC_ULT_PUBLICACION_APP, v.ACCESO_OFICINA
                                        FROM PERSONA_ACCESO PA INNER JOIN V_PERSONA V  ON PA.COD_PERSONA = V.COD_PERSONA 
                                        LEFT JOIN Persona_Afiliacion A ON V.COD_PERSONA = A.COD_PERSONA 
                                        INNER JOIN OFICINA O ON O.COD_OFICINA = V.COD_OFICINA 
                                        INNER JOIN EMPRESA E ON O.COD_EMPRESA = E.COD_EMPRESA 
                                        INNER JOIN TIPOIDENTIFICACION T ON T.CODTIPOIDENTIFICACION = V.TIPO_IDENTIFICACION 
                                        WHERE V.IDENTIFICACION = '" + pUsuario + "' AND PA.CLAVE = '" + pClave + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CLAVE"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["CLAVE"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacionAPP = Convert.ToDateTime(resultado["FECHACREACION"]).ToString("dd/MM/yyyy"); else entidad.fechacreacionAPP = "";
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimientoAPP = Convert.ToDateTime(resultado["FECHANACIMIENTO"]).ToString("dd/MM/yyyy"); else entidad.fechanacimientoAPP = "";
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt64(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOM_TIPOIDENTIFICACION"] != DBNull.Value) entidad.nomtipo_identificacion = Convert.ToString(resultado["NOM_TIPOIDENTIFICACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.idEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.nit = Convert.ToString(resultado["NIT"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["URL_DROID_APP"] != DBNull.Value) entidad.url_droid_app = Convert.ToString(resultado["URL_DROID_APP"]);
                            if (resultado["URL_IOS_APP"] != DBNull.Value) entidad.url_ios_app = Convert.ToString(resultado["URL_IOS_APP"]);
                            if (resultado["VERSION_APP"] != DBNull.Value) entidad.version_app = Convert.ToString(resultado["VERSION_APP"]);
                            if (resultado["FEC_ULT_PUBLICACION_APP"] != DBNull.Value) entidad.fec_ult_publicacion_app = Convert.ToDateTime(resultado["FEC_ULT_PUBLICACION_APP"]);
                            if (resultado["ACCESO_OFICINA"] != DBNull.Value) entidad.acceso_oficina = Convert.ToInt32(resultado["ACCESO_OFICINA"]);
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
                        BOExcepcion.Throw("UsuarioData", "ConsultarPersonaUsuario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Metodo para modificar la clave de un usuario
        /// </summary>
        /// <param name="pNuevaClave">nueva clave</param>
        /// <param name="pUsuario">Usuario de sesion</param>
        public void CambiarClave(string pNuevaClave, string pNuevaClave_sinencriptar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pUsuario.clave_sinencriptar = pNuevaClave_sinencriptar.Trim();

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_identificacion";
                        pIDENTIFICACION.Value = pUsuario.identificacion;

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = "p_login";
                        pLOGIN.Value = pNuevaClave;

                        DbParameter pCLAVE = cmdTransaccionFactory.CreateParameter();
                        pCLAVE.ParameterName = "p_clave";
                        pCLAVE.Value = pUsuario.clave_sinencriptar;

                        DbParameter p_ManejaUsuarios = cmdTransaccionFactory.CreateParameter();
                        p_ManejaUsuarios.ParameterName = "p_ManejaUsuarios";
                        p_ManejaUsuarios.DbType = DbType.String;
                        p_ManejaUsuarios.Value = ConfigurationManager.AppSettings["ManejoUsuarios"];

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCLAVE);
                        cmdTransaccionFactory.Parameters.Add(p_ManejaUsuarios);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIOS_CLAVE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pUsuario.login = pNuevaClave;

                        DAauditoria.InsertarLog(pUsuario, "USUARIOS", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CambiarClave", ex);
                    }
                }
            }
        }

        public bool CambiarClavePersona(string pIdentificacion, string pAntiguaClave, string pNuevaClave, ref string pError)
        {
            Usuario pUsuario = new Usuario();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pIdentificacion;
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);

                        DbParameter p_claveAntigua = cmdTransaccionFactory.CreateParameter();
                        p_claveAntigua.ParameterName = "p_claveAntigua";
                        if (pAntiguaClave == null || pAntiguaClave == "") p_claveAntigua.Value = DBNull.Value; p_claveAntigua.Value = pAntiguaClave;
                        cmdTransaccionFactory.Parameters.Add(p_claveAntigua);

                        DbParameter p_claveNueva = cmdTransaccionFactory.CreateParameter();
                        p_claveNueva.ParameterName = "p_claveNueva";
                        p_claveNueva.Value = pNuevaClave;
                        cmdTransaccionFactory.Parameters.Add(p_claveNueva);
                        
                        DbParameter pmensaje_error = cmdTransaccionFactory.CreateParameter();
                        pmensaje_error.ParameterName = "p_mensaje_error";
                        pmensaje_error.Value = DBNull.Value;
                        pmensaje_error.Size = 8000;
                        pmensaje_error.DbType = DbType.String;
                        pmensaje_error.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pmensaje_error);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PERSONACCE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pError = Convert.ToString(pmensaje_error.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad Usuario</param>
        /// <returns>Entidad Usuario creada</returns>
        public Boolean CrearUsuarioBD(Usuario pUsuario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pLOGIN = cmdTransaccionFactory.CreateParameter();
                        pLOGIN.ParameterName = "p_login";
                        pLOGIN.Value = pUsuario.identificacion;

                        DbParameter pCLAVE = cmdTransaccionFactory.CreateParameter();
                        pCLAVE.ParameterName = "p_clave";
                        pCLAVE.DbType = DbType.String;
                        pCLAVE.Value = pUsuario.clave_sinencriptar;

                        DbParameter p_ManejaUsuarios = cmdTransaccionFactory.CreateParameter();
                        p_ManejaUsuarios.ParameterName = "p_ManejaUsuarios";
                        p_ManejaUsuarios.DbType = DbType.String;
                        p_ManejaUsuarios.Value = ConfigurationManager.AppSettings["ManejoUsuarios"];

                        cmdTransaccionFactory.Parameters.Add(pLOGIN);
                        cmdTransaccionFactory.Parameters.Add(pCLAVE);
                        cmdTransaccionFactory.Parameters.Add(p_ManejaUsuarios);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIOS_CREARBD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearUsuarioBD", ex);
                        return false;
                    }
                }
            }
        }

        public Usuario ListarIPUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            pUsuario.LstIP = new List<string>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT direccionip FROM  usuario_ipacceso WHERE codusuario = " + pUsuario.codusuario;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            string entidad = "";

                            if (resultado["direccionip"] != DBNull.Value) entidad = Convert.ToString(resultado["direccionip"]);

                            pUsuario.LstIP.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarIPUsuario", ex);
                        return pUsuario;
                    }
                }
            }
        }


        public Usuario ListarMACUsuario(Usuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            pUsuario.LstMac= new List<string>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT direccionmac FROM  usuario_macacceso WHERE codusuario = " + pUsuario.codusuario;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            string entidad = "";

                            if (resultado["direccionmac"] != DBNull.Value) entidad = Convert.ToString(resultado["direccionmac"]);

                            pUsuario.LstMac.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarMACUsuario", ex);
                        return pUsuario;
                    }
                }
            }
        }

        public Boolean CrearDireccionIp(Int64 pCodUsuario, string pDireccionIp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pCodUsuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_direccionip";
                        pIDENTIFICACION.Value = pDireccionIp;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_DIRIP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCodUsuario, "USUARIOS", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearDireccionIp", ex);
                        return false;
                    }
                }
            }
        }

        public void EliminarDireccionIp(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuario pUsuario = new Usuario();

                        if (vUsuario.programaGeneraLog)
                            pUsuario = ConsultarUsuario(pId, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_DIRIP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarDireccionIp", ex);
                    }
                }
            }
        }






        public Boolean CrearDireccionMac(Int64 pCodUsuario, string pDireccionMac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pCodUsuario;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_direccionMac";
                        pIDENTIFICACION.Value = pDireccionMac;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_DIRMAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCodUsuario, "USUARIOS", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearDireccionMac", ex);
                        return false;
                    }
                }
            }
        }

        public void EliminarDireccionMac(Int64 pMac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuario pUsuario = new Usuario();

                        if (vUsuario.programaGeneraLog)
                            pUsuario = ConsultarUsuario(pMac, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pMac;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_DIRMAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarDireccionMac", ex);
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------

        public Boolean CrearAtribucion(Int64 pCodUsuario, int pTipoAtribucion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pCodUsuario;

                        DbParameter pTIPOATRIBUCION = cmdTransaccionFactory.CreateParameter();
                        pTIPOATRIBUCION.ParameterName = "p_tipoatribucion";
                        pTIPOATRIBUCION.Value = pTipoAtribucion;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pTIPOATRIBUCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ATRIBUCION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pCodUsuario, "USUARIO_ATRIBUCION", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearAtribucion", ex);
                        return false;
                    }
                }
            }
        }

        public void EliminarAtribucion(Int64 pId, int pTipoAtribucion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Usuario pUsuario = new Usuario();

                        if (vUsuario.programaGeneraLog)
                            pUsuario = ConsultarUsuario(pId, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pId;

                        DbParameter pTIPOATRIBUCION = cmdTransaccionFactory.CreateParameter();
                        pTIPOATRIBUCION.ParameterName = "p_tipoatribucion";
                        pTIPOATRIBUCION.Value = pTipoAtribucion;

                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pTIPOATRIBUCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ATRIBUCION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuario, "USUARIOS", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarAtribucion", ex);
                    }
                }
            }
        }

        public Usuario ListarAtribuciones(Usuario pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            pUsuario.LstAtribuciones = new List<int>();
            for (int i = 0; i <= 7; i += 1)
            {
                if (i >= pUsuario.LstAtribuciones.Count())
                    pUsuario.LstAtribuciones.Add(0);
                pUsuario.LstAtribuciones[i] = 0;
            }

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT tipoatribucion, activo FROM  usuario_atribuciones WHERE codusuario = " + pUsuario.codusuario + " ORDER BY tipoatribucion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            int tipoatribucion = 0;
                            int activo = 0;
                            if (resultado["tipoatribucion"] != DBNull.Value) tipoatribucion = Convert.ToInt32(resultado["tipoatribucion"]);
                            if (resultado["activo"] != DBNull.Value) activo = Convert.ToInt32(resultado["activo"]);
                            pUsuario.LstAtribuciones[tipoatribucion] = 1;
                            if (activo != 1)
                                pUsuario.LstAtribuciones[tipoatribucion] = 0;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ListarAtribuciones", ex);
                        return pUsuario;
                    }
                }
            }
        }



        public Persona1 ConsultarPersona1(string persona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 entidad = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT  cod_persona,identificacion, fechacreacion , 
                                      primer_nombre || ' ,' || primer_apellido || ' ' || segundo_apellido as nombre ,
                                      telefono , estado, cod_oficina, direccion 
                                      FROM PERSONA WHERE IDENTIFICACION = '" + persona + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        if (resultado.Read())
                        {

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]); else entidad.estado = "";
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
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


        public Persona1 ConsultaPersonaAcceso(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 Persona1 = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT P.*, p.email as email_acceso FROM persona p INNER JOIN persona_acceso A ON p.cod_persona = A.cod_persona " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Persona1.rptaingreso = false;
                        if (resultado.Read())
                        {
                            Persona1.rptaingreso = true;
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
                            if (resultado["email_acceso"] != DBNull.Value) Persona1.email = Convert.ToString(resultado["email_acceso"]);
                            if (resultado["codciudadresidencia"] != DBNull.Value) Persona1.codciudadresidencia = Convert.ToInt64(resultado["codciudadresidencia"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Persona1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ConsultaPersonaAcceso", ex);
                        return null;
                    }
                }
            }
        }


        public Perfil ConsultarFechaperiodicidad(Int64 CodUsuario, Int64 CodPerfil, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Perfil entidad = new Perfil();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Max(fecha) As fecha
                                       FROM historial_cambioclave 
                                       WHERE codusuario = " + CodUsuario;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                  
                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        }



                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarFechaperiodicidad", ex);
                        return entidad;
                    }
                };           

            }

        }

        public Usuario ConsultarEmpresa(Int32 codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Usuario entidad = new Usuario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select SIGLA,TIPO from Empresa where COD_EMPRESA = " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        if (resultado.Read())
                        {
                            if (resultado["SIGLA"] != DBNull.Value) entidad.sigla_empresa = Convert.ToString(resultado["SIGLA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ConsultarEmpresa", ex);
                        return null;
                    }
                }
            }
        }


        public Ingresos CrearUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ingreso = cmdTransaccionFactory.CreateParameter();
                        pcod_ingreso.ParameterName = "p_cod_ingreso";
                        pcod_ingreso.Value = pIngreso.cod_ingreso;
                        pcod_ingreso.Direction = ParameterDirection.Output;
                        pcod_ingreso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ingreso);

                        DbParameter pfecha_horaingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_horaingreso.ParameterName = "p_fecha_horaingreso";
                        pfecha_horaingreso.Value = pIngreso.fecha_horaingreso;
                        pfecha_horaingreso.Direction = ParameterDirection.Input;
                        pfecha_horaingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_horaingreso);

                        DbParameter pfecha_horasalida = cmdTransaccionFactory.CreateParameter();
                        pfecha_horasalida.ParameterName = "p_fecha_horasalida";
                        if (pIngreso.fecha_horasalida != DateTime.MinValue) pfecha_horasalida.Value = pIngreso.fecha_horasalida; else pfecha_horasalida.Value = DBNull.Value;
                        pfecha_horasalida.Direction = ParameterDirection.Input;
                        pfecha_horasalida.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_horasalida);

                        DbParameter pdireccionip = cmdTransaccionFactory.CreateParameter();
                        pdireccionip.ParameterName = "p_direccionip";
                        pdireccionip.Value = pIngreso.direccionip;
                        pdireccionip.Direction = ParameterDirection.Input;
                        pdireccionip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccionip);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pIngreso.codusuario == null) pcodusuario.Value = DBNull.Value; else pcodusuario.Value = pIngreso.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pIngreso.cod_persona == null) pcod_persona.Value = DBNull.Value; else pcod_persona.Value = pIngreso.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIO_IN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pIngreso.cod_ingreso = Convert.ToInt32(pcod_ingreso.Value);
                        return pIngreso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "CrearUsuarioIngreso", ex);
                        return null;
                    }
                }
            }
        }


        public Ingresos ModificarUsuarioIngreso(Ingresos pIngreso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ingreso = cmdTransaccionFactory.CreateParameter();
                        pcod_ingreso.ParameterName = "p_cod_ingreso";
                        pcod_ingreso.Value = pIngreso.cod_ingreso;
                        pcod_ingreso.Direction = ParameterDirection.Input;
                        pcod_ingreso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ingreso);

                        DbParameter pfecha_horasalida = cmdTransaccionFactory.CreateParameter();
                        pfecha_horasalida.ParameterName = "p_fecha_horasalida";
                        pfecha_horasalida.Value = pIngreso.fecha_horasalida;
                        pfecha_horasalida.Direction = ParameterDirection.Input;
                        pfecha_horasalida.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_horasalida);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_USUARIO_IN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pIngreso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "ModificarUsuarioIngreso", ex);
                        return null;
                    }
                }
            }
        }



        public PersonaUsuario ConsultarPersonaUsuarioGeneral(PersonaUsuario pEntidad, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            PersonaUsuario entidad = new PersonaUsuario();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM persona_Acceso " + ObtenerFiltro(pEntidad) + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDACCESO"] != DBNull.Value) entidad.idacceso = Convert.ToInt64(resultado["IDACCESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["CLAVE"] != DBNull.Value) entidad.clave = Convert.ToString(resultado["CLAVE"]);
                            if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PersonaUsuarioData", "ConsultarPersonaUsuarioGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public string ProbarConexión(Usuario PUsuario)
        {
            try
            { 
                using (DbConnection connection = dbConnectionFactory.ObtenerConexion(PUsuario))
                {
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        string estado = "";
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                            estado = "Open";
                        dbConnectionFactory.CerrarConexion(connection);
                        return "Estado:" + estado + " String:" + dbConnectionFactory.ObtenerStringConexion(PUsuario);
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message + dbConnectionFactory.ObtenerStringConexion(PUsuario);
            }

        }



    }

}