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
    /// Objeto de acceso a datos para la tabla OPCIONES
    /// </summary>
    public class OpcionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla OPCIONES
        /// </summary>
        public OpcionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla OPCIONES de la base de datos
        /// </summary>
        /// <param name="pOpcion">Entidad Opcion</param>
        /// <returns>Entidad Opcion creada</returns>
        public Opcion CrearOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_OPCION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPCION.ParameterName = "p_cod_opcion";
                        pCOD_OPCION.Value = pOpcion.cod_opcion;
                        pCOD_OPCION.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pOpcion.nombre;

                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "p_cod_proceso";
                        pCOD_PROCESO.Value = pOpcion.cod_proceso;

                        DbParameter pRUTA = cmdTransaccionFactory.CreateParameter();
                        pRUTA.ParameterName = "p_ruta";
                        if (pOpcion.ruta != null) pRUTA.Value = pOpcion.ruta; else pRUTA.Value = DBNull.Value;

                        DbParameter pGENERALOG = cmdTransaccionFactory.CreateParameter();
                        pGENERALOG.ParameterName = "p_generalog";
                        pGENERALOG.Value = pOpcion.generalog;

                        DbParameter pACCION = cmdTransaccionFactory.CreateParameter();
                        pACCION.ParameterName = "p_accion";
                        pACCION.Value = pOpcion.accion;
                        if (pOpcion.accion != null) pACCION.Value = pOpcion.accion; else pACCION.Value = DBNull.Value;

                        DbParameter pREFAYUDA = cmdTransaccionFactory.CreateParameter();
                        pREFAYUDA.ParameterName = "p_refayuda";
                        pREFAYUDA.Value = pOpcion.refayuda;

                        DbParameter P_VALIDAR_BIOMETRIA = cmdTransaccionFactory.CreateParameter();
                        P_VALIDAR_BIOMETRIA.ParameterName = "p_validar_biometria";
                        if (pOpcion.validar_Biometria != null) P_VALIDAR_BIOMETRIA.Value = pOpcion.validar_Biometria; else P_VALIDAR_BIOMETRIA.Value = DBNull.Value;


                        DbParameter P_MANEJA_EXCEPCIONES = cmdTransaccionFactory.CreateParameter();
                        P_MANEJA_EXCEPCIONES.ParameterName = "p_maneja_excepciones";
                        if (pOpcion.maneja_excepciones != null) P_MANEJA_EXCEPCIONES.Value = pOpcion.maneja_excepciones; else P_MANEJA_EXCEPCIONES.Value = DBNull.Value;


                        DbParameter p_PermisosCampos = cmdTransaccionFactory.CreateParameter();
                        p_PermisosCampos.ParameterName = "p_PermisosCampos";
                        if (pOpcion.maneja_excepciones != null)
                            p_PermisosCampos.Value = pOpcion.PermisosCampos;
                        else
                            p_PermisosCampos.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCOD_OPCION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pRUTA);
                        cmdTransaccionFactory.Parameters.Add(pGENERALOG);
                        cmdTransaccionFactory.Parameters.Add(pACCION);
                        cmdTransaccionFactory.Parameters.Add(pREFAYUDA);
                        cmdTransaccionFactory.Parameters.Add(P_VALIDAR_BIOMETRIA);
                        cmdTransaccionFactory.Parameters.Add(P_MANEJA_EXCEPCIONES);
                        cmdTransaccionFactory.Parameters.Add(p_PermisosCampos);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_OPCION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pOpcion, "OPCIONES", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pOpcion.cod_opcion = Convert.ToInt64(pCOD_OPCION.Value);
                        return pOpcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "CrearOpcion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla OPCIONES de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Opcion modificada</returns>
        public Opcion ModificarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_OPCION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPCION.ParameterName = "p_cod_opcion";
                        pCOD_OPCION.Value = pOpcion.cod_opcion;
                        pCOD_OPCION.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_nombre";
                        pNOMBRE.Value = pOpcion.nombre;

                        DbParameter pCOD_PROCESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PROCESO.ParameterName = "p_cod_proceso";
                        pCOD_PROCESO.Value = pOpcion.cod_proceso;

                        DbParameter pRUTA = cmdTransaccionFactory.CreateParameter();
                        pRUTA.ParameterName = "p_ruta";
                        if (pOpcion.ruta != null) pRUTA.Value = pOpcion.ruta; else pRUTA.Value = DBNull.Value;

                        DbParameter pGENERALOG = cmdTransaccionFactory.CreateParameter();
                        pGENERALOG.ParameterName = "p_generalog";
                        pGENERALOG.Value = pOpcion.generalog;

                        DbParameter pACCION = cmdTransaccionFactory.CreateParameter();
                        pACCION.ParameterName = "p_accion";
                        if (pOpcion.accion != null) pACCION.Value = pOpcion.accion; else pACCION.Value = DBNull.Value;

                        DbParameter pREFAYUDA = cmdTransaccionFactory.CreateParameter();
                        pREFAYUDA.ParameterName = "p_refayuda";
                        pREFAYUDA.Value = pOpcion.refayuda;

                        DbParameter P_VALIDAR_BIOMETRIA = cmdTransaccionFactory.CreateParameter();
                        P_VALIDAR_BIOMETRIA.ParameterName = "p_validar_biometria";
                        if (pOpcion.validar_Biometria != null) P_VALIDAR_BIOMETRIA.Value = pOpcion.validar_Biometria; else P_VALIDAR_BIOMETRIA.Value = DBNull.Value;


                        DbParameter P_MANEJA_EXCEPCIONES = cmdTransaccionFactory.CreateParameter();
                        P_MANEJA_EXCEPCIONES.ParameterName = "p_maneja_excepciones";
                        if (pOpcion.maneja_excepciones != null) P_MANEJA_EXCEPCIONES.Value = pOpcion.maneja_excepciones; else P_MANEJA_EXCEPCIONES.Value = DBNull.Value;

                        DbParameter p_PermisosCampos = cmdTransaccionFactory.CreateParameter();
                        p_PermisosCampos.ParameterName = "p_PermisosCampos";
                        if (pOpcion.maneja_excepciones != null)
                            p_PermisosCampos.Value = pOpcion.PermisosCampos;
                        else
                            p_PermisosCampos.Value = DBNull.Value;


                        cmdTransaccionFactory.Parameters.Add(pCOD_OPCION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PROCESO);
                        cmdTransaccionFactory.Parameters.Add(pRUTA);
                        cmdTransaccionFactory.Parameters.Add(pGENERALOG);
                        cmdTransaccionFactory.Parameters.Add(pACCION);
                        cmdTransaccionFactory.Parameters.Add(pREFAYUDA);
                        cmdTransaccionFactory.Parameters.Add(P_VALIDAR_BIOMETRIA);
                        cmdTransaccionFactory.Parameters.Add(P_MANEJA_EXCEPCIONES);
                        cmdTransaccionFactory.Parameters.Add(p_PermisosCampos);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_OPCION_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        return pOpcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "ModificarOpcion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla OPCIONES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de OPCIONES</param>
        public void EliminarOpcion(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Opcion pOpcion = new Opcion();

                        if (pUsuario.programaGeneraLog)
                            pOpcion = ConsultarOpcion(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_OPCION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPCION.ParameterName = "p_cod_opcion";
                        pCOD_OPCION.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_OPCION);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_OPCION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pOpcion, "OPCIONES", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "InsertarOpcion", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla OPCIONES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla OPCIONES</param>
        /// <returns>Entidad Opcion consultado</returns>
        public Opcion ConsultarOpcion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Opcion entidad = new Opcion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  OPCIONES WHERE cod_opcion = " + pId.ToString();

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["GENERALOG"] != DBNull.Value) entidad.generalog = Convert.ToInt64(resultado["GENERALOG"]);
                            if (resultado["REFAYUDA"] != DBNull.Value) entidad.refayuda = Convert.ToInt64(resultado["REFAYUDA"]);
                            if (resultado["VALIDAR_BIOMETRIA"] != DBNull.Value) entidad.validar_Biometria = Convert.ToInt32(resultado["VALIDAR_BIOMETRIA"]);
                            if (resultado["MANEJA_EXCEPCIONES"] != DBNull.Value) entidad.maneja_excepciones = Convert.ToInt32(resultado["MANEJA_EXCEPCIONES"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "ConsultarOpcion", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OPCIONES dados unos filtros
        /// </summary>
        /// <param name="pOPCIONES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Opcion obtenidos</returns>
        public List<Opcion> ListarOpcion(Opcion pOpcion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Opcion> lstOpcion = new List<Opcion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT PROCESO.COD_MODULO, OPCIONES.* FROM OPCIONES LEFT JOIN PROCESO ON PROCESO.COD_PROCESO = OPCIONES.COD_PROCESO " + ObtenerFiltro(pOpcion, "OPCIONES.");

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Opcion entidad = new Opcion();

                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["GENERALOG"] != DBNull.Value) entidad.generalog = Convert.ToInt64(resultado["GENERALOG"]);
                            if (resultado["REFAYUDA"] != DBNull.Value) entidad.refayuda = Convert.ToInt64(resultado["REFAYUDA"]);

                            lstOpcion.Add(entidad);
                        }

                        return lstOpcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "ListarOpcion", ex);
                        return null;
                    }
                }
            }
        }



        public List<Opcion> ListarOpcionEstadoCuenta(Int32 pCod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Opcion> lstOpcion = new List<Opcion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT p.Cod_Opcion,p.Cod_Proceso,O.Nombre,O.Ruta FROM Opciones O INNER JOIN Perfil_Acceso P ON O.Cod_Opcion = P.Cod_Opcion AND P.Codperfil = " + pCod.ToString();

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Opcion entidad = new Opcion();

                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (entidad.ruta != "" && entidad.ruta != null)
                                entidad.rutaEstadoCuenta = entidad.cod_modulo + "," + entidad.ruta;
                            lstOpcion.Add(entidad);
                        }

                        return lstOpcion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "ListarOpcionEstadoCuenta", ex);
                        return null;
                    }
                }
            }
        }

        public List<Opcion> ListarOpciones(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Opcion> lstPerfil = new List<Opcion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from opciones where cod_opcion=100102 or cod_opcion=100140 or cod_opcion=100161 or cod_opcion=40302 or cod_opcion=40502 or cod_opcion=100160 or cod_opcion=100158 or cod_opcion=60112";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Opcion entidad = new Opcion();

                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["REFAYUDA"] != DBNull.Value) entidad.refayuda = Convert.ToInt64(resultado["REFAYUDA"]);
                            if (resultado["GENERALOG"] != DBNull.Value) entidad.generalog = Convert.ToInt64(resultado["GENERALOG"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["VALIDAR_BIOMETRIA"] != DBNull.Value) entidad.validar_Biometria = Convert.ToInt32(resultado["VALIDAR_BIOMETRIA"]);
                            if (resultado["MANEJA_EXCEPCIONES"] != DBNull.Value) entidad.maneja_excepciones = Convert.ToInt32(resultado["MANEJA_EXCEPCIONES"]);

                            lstPerfil.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ListarOpciones", ex);
                        return null;
                    }
                }
            }
        }



        public Opcion Modificargeneral(Opcion pgeneral, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pgeneral.cod_opcion;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pgeneral.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pgeneral.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ADM_GENERAL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pgeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("generalData", "Modificargeneral", ex);
                        return null;
                    }
                }
            }
        }



        public List<Opcion> ListarOpcionesGeneral(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Opcion> lstPerfil = new List<Opcion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from general where 1=1 " + filtro + "Order by codigo ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Opcion entidad = new Opcion();

                            if (resultado["CODIGO"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);

                            lstPerfil.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ListarOpciones", ex);
                        return null;
                    }
                }
            }
        }


    }
}