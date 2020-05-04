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
    /// Objeto de acceso a datos para la tabla PERFIL_ACCESO
    /// </summary>
    public class AccesoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERFIL_ACCESO
        /// </summary>
        public AccesoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERFIL_ACCESO de la base de datos
        /// </summary>
        /// <param name="pAcceso">Entidad Acceso</param>
        /// <returns>Entidad Acceso creada</returns>
        public Acceso CrearAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODACCESO = cmdTransaccionFactory.CreateParameter();
                        pCODACCESO.ParameterName = "p_codacceso";
                        pCODACCESO.Value = ObtenerSiguienteCodigo(pUsuario);
                        pCODACCESO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pAcceso.codigoperfil;

                        DbParameter pCOD_OPCION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPCION.ParameterName = "p_cod_opcion";
                        pCOD_OPCION.Value = pAcceso.cod_opcion;

                        DbParameter pINSERTAR = cmdTransaccionFactory.CreateParameter();
                        pINSERTAR.ParameterName = "p_insertar";
                        pINSERTAR.Value = pAcceso.insertar;

                        DbParameter pMODIFICAR = cmdTransaccionFactory.CreateParameter();
                        pMODIFICAR.ParameterName = "p_modificar";
                        pMODIFICAR.Value = pAcceso.modificar;

                        DbParameter pBORRAR = cmdTransaccionFactory.CreateParameter();
                        pBORRAR.ParameterName = "p_borrar";
                        pBORRAR.Value = pAcceso.borrar;

                        DbParameter pCONSULTAR = cmdTransaccionFactory.CreateParameter();
                        pCONSULTAR.ParameterName = "p_consultar";
                        pCONSULTAR.Value = pAcceso.consultar;


                        cmdTransaccionFactory.Parameters.Add(pCODACCESO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OPCION);
                        cmdTransaccionFactory.Parameters.Add(pINSERTAR);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICAR);
                        cmdTransaccionFactory.Parameters.Add(pBORRAR);
                        cmdTransaccionFactory.Parameters.Add(pCONSULTAR);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ACCESO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAcceso, "PERFIL_ACCESO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAcceso.codacceso = Convert.ToInt64(pCODACCESO.Value);
                        return pAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "CrearAcceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERFIL_ACCESO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Acceso modificada</returns>
        public Acceso ModificarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODACCESO = cmdTransaccionFactory.CreateParameter();
                        pCODACCESO.ParameterName = "p_codacceso";
                        pCODACCESO.Value = pAcceso.codacceso;

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pAcceso.codigoperfil;

                        DbParameter pCOD_OPCION = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPCION.ParameterName = "p_cod_opcion";
                        pCOD_OPCION.Value = pAcceso.cod_opcion;

                        DbParameter pINSERTAR = cmdTransaccionFactory.CreateParameter();
                        pINSERTAR.ParameterName = "p_insertar";
                        pINSERTAR.Value = pAcceso.insertar;

                        DbParameter pMODIFICAR = cmdTransaccionFactory.CreateParameter();
                        pMODIFICAR.ParameterName = "p_modificar";
                        pMODIFICAR.Value = pAcceso.modificar;

                        DbParameter pBORRAR = cmdTransaccionFactory.CreateParameter();
                        pBORRAR.ParameterName = "p_borrar";
                        pBORRAR.Value = pAcceso.borrar;

                        DbParameter pCONSULTAR = cmdTransaccionFactory.CreateParameter();
                        pCONSULTAR.ParameterName = "p_consultar";
                        pCONSULTAR.Value = pAcceso.consultar;

                        cmdTransaccionFactory.Parameters.Add(pCODACCESO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OPCION);
                        cmdTransaccionFactory.Parameters.Add(pINSERTAR);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICAR);
                        cmdTransaccionFactory.Parameters.Add(pBORRAR);
                        cmdTransaccionFactory.Parameters.Add(pCONSULTAR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ACCESO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAcceso, "PERFIL_ACCESO", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ModificarAcceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERFIL_ACCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERFIL_ACCESO</param>
        public void EliminarAcceso(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Acceso pAcceso = new Acceso();

                        if (pUsuario.programaGeneraLog)
                            pAcceso = ConsultarAcceso(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODACCESO = cmdTransaccionFactory.CreateParameter();
                        pCODACCESO.ParameterName = "p_codacceso";
                        pCODACCESO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODACCESO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ACCESO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAcceso, "PERFIL_ACCESO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "EliminarAcceso", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERFIL_ACCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERFIL_ACCESO</param>
        /// <returns>Entidad Acceso consultado</returns>
        public Acceso ConsultarAcceso(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Acceso entidad = new Acceso();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PERFIL_ACCESO WHERE codacceso = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODACCESO"] != DBNull.Value) entidad.codacceso = Convert.ToInt64(resultado["CODACCESO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codigoperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ConsultarAcceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERFIL_ACCESO dados unos filtros
        /// </summary>
        /// <param name="pPERFIL_ACCESO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Acceso obtenidos</returns>
        public List<Acceso> ListarAcceso(Acceso pAcceso, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acceso> lstAcceso = new List<Acceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT *  FROM V_PERFIL_ACCESO " + ObtenerFiltro(pAcceso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acceso entidad = new Acceso();

                            if (resultado["CODACCESO"] != DBNull.Value) entidad.codacceso = Convert.ToInt64(resultado["CODACCESO"]);
                            if (resultado["CODIGOPERFIL"] != DBNull.Value) entidad.codigoperfil = Convert.ToInt64(resultado["CODIGOPERFIL"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBREOPCION"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBREOPCION"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);


                            lstAcceso.Add(entidad);
                        }

                        return lstAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ListarAcceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene todos los permisos a las opciones para un perfil especifico
        /// </summary>
        /// <param name="pIdPerfil">identificador del perfil</param>
        /// <returns>Conjunto de opciones</returns>
        public List<Acceso> ListarAcceso(Int64 pIdPerfil, Usuario pUsuario, String Idioma)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acceso> lstAcceso = new List<Acceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT perfil_acceso.codacceso, perfil_acceso.codperfil, opciones.cod_opcion, " +
                                     " opciones.nombre nombreopcion, opciones.ruta, perfil_acceso.insertar, " +
                                     " perfil_acceso.modificar, perfil_acceso.borrar,  " +
                                     " perfil_acceso.consultar, opciones.generalog, opciones.accion, " +
                                     " proceso.nombre nombreproceso, modulo.cod_modulo, modulo.nom_modulo,opciones.nombre1,opciones.nombre2 " +
                                     " FROM perfil_acceso " +
                                     " INNER JOIN opciones ON perfil_acceso.cod_opcion = opciones.cod_opcion " +
                                     " INNER JOIN proceso ON proceso.cod_proceso = opciones.cod_proceso " +
                                     " INNER JOIN modulo ON modulo.cod_modulo = proceso.cod_modulo " +
                                     " WHERE NOT (perfil_acceso.insertar = 0 AND perfil_acceso.modificar = 0 AND perfil_acceso.consultar = 0 AND perfil_acceso.borrar = 0) AND codperfil = " + pIdPerfil.ToString() + "ORDER BY opciones.cod_proceso, opciones.cod_opcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acceso entidad = new Acceso();

                            if (resultado["CODACCESO"] != DBNull.Value) entidad.codacceso = Convert.ToInt64(resultado["CODACCESO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codigoperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBREOPCION"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBREOPCION"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);
                            if (resultado["GENERALOG"] != DBNull.Value) entidad.generalog = Convert.ToInt64(resultado["GENERALOG"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["NOMBREPROCESO"] != DBNull.Value) entidad.nombreproceso = Convert.ToString(resultado["NOMBREPROCESO"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOM_MODULO"] != DBNull.Value) entidad.nom_modulo = Convert.ToString(resultado["NOM_MODULO"]);


                            if (Idioma == "2")
                            {
                                if (resultado["NOMBRE2"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBRE2"]);
                            }

                            if (Idioma == "1")
                            {
                                if (resultado["NOMBRE1"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBRE1"]);
                            }

                            lstAcceso.Add(entidad);
                        }

                        return lstAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ListarAcceso", ex);
                        return null;
                    }
                }
            }
        }

        public List<Acceso> ListarAccesoAAC(Int64 pIdModulo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acceso> lstAcceso = new List<Acceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT perfil_acceso.codacceso, perfil_acceso.codperfil, opciones.cod_opcion,
                                    opciones.nombre nombreopcion, opciones.ruta, perfil_acceso.insertar,
                                    perfil_acceso.modificar, perfil_acceso.borrar,
                                    perfil_acceso.consultar, opciones.generalog, opciones.accion,
                                    proceso.nombre nombreproceso, modulo.cod_modulo, modulo.nom_modulo
                                    FROM perfil_acceso
                                    INNER JOIN opciones ON perfil_acceso.cod_opcion = opciones.cod_opcion
                                    INNER JOIN proceso ON proceso.cod_proceso = opciones.cod_proceso
                                    INNER JOIN modulo ON modulo.cod_modulo = proceso.cod_modulo
                                    WHERE NOT (perfil_acceso.insertar = 0 AND perfil_acceso.modificar = 0 AND perfil_acceso.consultar = 0 AND perfil_acceso.borrar = 0)
                                    AND modulo.cod_modulo = " + pIdModulo
                                    + " and perfil_acceso.codperfil not in (1) ORDER BY opciones.cod_proceso, opciones.cod_opcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acceso entidad = new Acceso();

                            if (resultado["CODACCESO"] != DBNull.Value) entidad.codacceso = Convert.ToInt64(resultado["CODACCESO"]);
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codigoperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBREOPCION"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBREOPCION"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);
                            if (resultado["GENERALOG"] != DBNull.Value) entidad.generalog = Convert.ToInt64(resultado["GENERALOG"]);
                            if (resultado["ACCION"] != DBNull.Value) entidad.accion = Convert.ToString(resultado["ACCION"]);
                            if (resultado["NOMBREPROCESO"] != DBNull.Value) entidad.nombreproceso = Convert.ToString(resultado["NOMBREPROCESO"]);
                            if (resultado["COD_MODULO"] != DBNull.Value) entidad.cod_modulo = Convert.ToInt64(resultado["COD_MODULO"]);
                            if (resultado["NOM_MODULO"] != DBNull.Value) entidad.nom_modulo = Convert.ToString(resultado["NOM_MODULO"]);

                            lstAcceso.Add(entidad);
                        }

                        return lstAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ListarAccesoAAC", ex);
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
                        string sql = "SELECT MAX(codacceso) + 1 FROM  PERFIL_ACCESO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        public Int64 ExisteAcceso(Acceso pPerfilAcceso, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64 pIdAcceso = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Perfil_Acceso.codacceso FROM Perfil_Acceso WHERE codperfil = " + pPerfilAcceso.codigoperfil.ToString() + " AND cod_opcion = " + pPerfilAcceso.cod_opcion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODACCESO"] != DBNull.Value) pIdAcceso = Convert.ToInt64(resultado["CODACCESO"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return pIdAcceso;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pIdAcceso;
                    }
                    catch
                    {
                        return pIdAcceso;
                    }
                }
            }
        }


        public List<Acceso> ListarAccesoApp(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Acceso> lstAcceso = new List<Acceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT proceso.cod_proceso, proceso.nombre, opciones.cod_opcion, Opciones.Nombre Nom_Opcion, Opciones.ruta
                                    FROM opciones 
                                    INNER JOIN proceso ON proceso.cod_proceso = opciones.cod_proceso
                                    INNER JOIN modulo ON modulo.cod_modulo = proceso.cod_modulo
                                    WHERE modulo.cod_modulo = 29 
                                    order by 1, 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acceso entidad = new Acceso();
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreproceso = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt32(resultado["COD_OPCION"]);
                            if (resultado["NOM_OPCION"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOM_OPCION"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            lstAcceso.Add(entidad);
                        }
                        return lstAcceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AccesoData", "ListarAccesoApp", ex);
                        return null;
                    }
                }
            }
        }
    }
}