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
    /// Objeto de acceso a datos para la tabla PERFIL_USUARIO
    /// </summary>
    public class PerfilData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERFIL_USUARIO
        /// </summary>
        public PerfilData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PERFIL_USUARIO de la base de datos
        /// </summary>
        /// <param name="pPerfil">Entidad Perfil</param>
        /// <returns>Entidad Perfil creada</returns>
        public Perfil CrearPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pPerfil.codperfil;
                        pCODPERFIL.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBREPERFIL = cmdTransaccionFactory.CreateParameter();
                        pNOMBREPERFIL.ParameterName = "p_nombreperfil";
                        pNOMBREPERFIL.Value = pPerfil.nombreperfil;

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        pcod_periodicidad.Value = pPerfil.cod_periodicidad;

                        DbParameter pes_administrador = cmdTransaccionFactory.CreateParameter();
                        pes_administrador.ParameterName = "p_es_administrador";
                        pes_administrador.Value = pPerfil.es_administrador;

                        DbParameter p_caracter_clave = cmdTransaccionFactory.CreateParameter();
                        p_caracter_clave.ParameterName = "P_CARACTER_CLAVE";
                        p_caracter_clave.Value = pPerfil.caracter ? 1 : 0;

                        DbParameter p_longitud_clave = cmdTransaccionFactory.CreateParameter();
                        p_longitud_clave.ParameterName = "P_LONGITUD_CLAVE";
                        p_longitud_clave.Value = pPerfil.longitud;

                        DbParameter p_numero_clave = cmdTransaccionFactory.CreateParameter();
                        p_numero_clave.ParameterName = "P_NUMERO_CLAVE";
                        p_numero_clave.Value = pPerfil.numero ? 1 : 0;

                        DbParameter p_mayuscula_clave = cmdTransaccionFactory.CreateParameter();
                        p_mayuscula_clave.ParameterName = "P_MAYUSCULA_CLAVE";
                        p_mayuscula_clave.Value = pPerfil.mayuscula ? 1 : 0;

                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pNOMBREPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pes_administrador);
                        cmdTransaccionFactory.Parameters.Add(p_caracter_clave);
                        cmdTransaccionFactory.Parameters.Add(p_longitud_clave);
                        cmdTransaccionFactory.Parameters.Add(p_numero_clave);
                        cmdTransaccionFactory.Parameters.Add(p_mayuscula_clave);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PERFIL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPerfil, "PERFIL_USUARIO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pPerfil.codperfil = Convert.ToInt64(pCODPERFIL.Value);
                        return pPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "CrearPerfil", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64 NumeroSiguiente = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Max(codperfil)+1 as numero From PERFIL_USUARIO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                            if (resultado["numero"] != DBNull.Value) NumeroSiguiente = Convert.ToInt64(resultado["numero"]);

                        return NumeroSiguiente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PERFIL_USUARIO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Perfil modificada</returns>
        public Perfil ModificarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pPerfil.codperfil;

                        DbParameter pNOMBREPERFIL = cmdTransaccionFactory.CreateParameter();
                        pNOMBREPERFIL.ParameterName = "p_nombreperfil";
                        pNOMBREPERFIL.Value = pPerfil.nombreperfil;

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pPerfil.cod_periodicidad != null)
                            pcod_periodicidad.Value = pPerfil.cod_periodicidad;
                        else
                            pcod_periodicidad.Value = DBNull.Value;

                        DbParameter pes_administrador = cmdTransaccionFactory.CreateParameter();
                        pes_administrador.ParameterName = "p_es_administrador";
                        pes_administrador.Value = pPerfil.es_administrador;

                        DbParameter p_caracter_clave = cmdTransaccionFactory.CreateParameter();
                        p_caracter_clave.ParameterName = "P_CARACTER_CLAVE";
                        p_caracter_clave.Value = pPerfil.caracter == true ? 1 : 0;

                        DbParameter p_longitud_clave = cmdTransaccionFactory.CreateParameter();
                        p_longitud_clave.ParameterName = "P_LONGITUD_CLAVE";
                        p_longitud_clave.Value = pPerfil.longitud;

                        DbParameter p_numero_clave = cmdTransaccionFactory.CreateParameter();
                        p_numero_clave.ParameterName = "P_NUMERO_CLAVE";
                        p_numero_clave.Value = pPerfil.numero == true ? 1 : 0;

                        DbParameter p_mayuscula_clave = cmdTransaccionFactory.CreateParameter();
                        p_mayuscula_clave.ParameterName = "P_MAYUSCULA_CLAVE";
                        p_mayuscula_clave.Value = pPerfil.mayuscula == true ? 1 : 0;

                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pNOMBREPERFIL);
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pes_administrador);
                        cmdTransaccionFactory.Parameters.Add(p_caracter_clave);
                        cmdTransaccionFactory.Parameters.Add(p_longitud_clave);
                        cmdTransaccionFactory.Parameters.Add(p_numero_clave);
                        cmdTransaccionFactory.Parameters.Add(p_mayuscula_clave);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PERFIL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPerfil, "PERFIL_USUARIO", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ModificarPerfil", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PERFIL_USUARIO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PERFIL_USUARIO</param>
        public void EliminarPerfil(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Perfil pPerfil = new Perfil();

                        if (pUsuario.programaGeneraLog)
                            pPerfil = ConsultarPerfil(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODPERFIL = cmdTransaccionFactory.CreateParameter();
                        pCODPERFIL.ParameterName = "p_codperfil";
                        pCODPERFIL.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODPERFIL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PERFIL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPerfil, "PERFIL_USUARIO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "EliminarPerfil", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PERFIL_USUARIO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PERFIL_USUARIO</param>
        /// <returns>Entidad Perfil consultado</returns>
        public Perfil ConsultarPerfil(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Perfil entidad = new Perfil();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PERFIL_USUARIO WHERE codperfil = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["ADMINISTRADOR"] != DBNull.Value) entidad.es_administrador = Convert.ToInt32(resultado["ADMINISTRADOR"]);
                            if (resultado["CARACTER_CLAVE"] != DBNull.Value) entidad.caracter = Convert.ToBoolean(resultado["CARACTER_CLAVE"]);
                            if (resultado["LONGITUD_CLAVE"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD_CLAVE"]);
                            if (resultado["NUMERO_CLAVE"] != DBNull.Value) entidad.numero = Convert.ToBoolean(resultado["NUMERO_CLAVE"]);
                            if (resultado["MAYUSCULA_CLAVE"] != DBNull.Value) entidad.mayuscula = Convert.ToBoolean(resultado["MAYUSCULA_CLAVE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ConsultarPerfil", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PERFIL_USUARIO dados unos filtros
        /// </summary>
        /// <param name="pPERFIL_USUARIO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Perfil obtenidos</returns>
        public List<Perfil> ListarPerfil(Perfil pPerfil, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Perfil> lstPerfil = new List<Perfil>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PERFIL_USUARIO " + ObtenerFiltro(pPerfil) + " ORDER BY CODPERFIL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Perfil entidad = new Perfil();

                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["NOMBREPERFIL"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["NOMBREPERFIL"]);

                            lstPerfil.Add(entidad);
                        }

                        return lstPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ListarPerfil", ex);
                        return null;
                    }
                }
            }
        }


        public List<Acceso> ListarOpciones(Int64 IdPerfil, Int64 CodModulo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Acceso> lstPerfil = new List<Acceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Perfil_Acceso.codperfil, Opciones.cod_opcion, Opciones.nombre, Perfil_Acceso.consultar, Perfil_Acceso.insertar, Perfil_Acceso.modificar, Perfil_Acceso.borrar, Opciones.cod_proceso, opciones.permisoscampos
                                        FROM Opciones INNER JOIN Proceso ON Opciones.cod_proceso = Proceso.cod_proceso LEFT JOIN Perfil_Acceso ON Opciones.cod_opcion = Perfil_Acceso.cod_opcion AND Perfil_Acceso.codperfil =  " + IdPerfil.ToString() +
                                      " WHERE Proceso.cod_modulo = " + CodModulo.ToString() + " ORDER BY Opciones.cod_opcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Acceso entidad = new Acceso();

                            if (resultado["CODPERFIL"] != DBNull.Value) entidad.codigoperfil = Convert.ToInt64(resultado["CODPERFIL"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreopcion = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
                            if (resultado["permisoscampos"] != DBNull.Value) entidad.PermisoCampo = Convert.ToInt32(resultado["permisoscampos"]);
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

        public Acceso ConsultarPerfilAcceso(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Acceso entidad = new Acceso();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM perfil_acceso WHERE codacceso = " + pId.ToString();

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
                            if (resultado["CONSULTAR"] != DBNull.Value) entidad.consultar = Convert.ToInt64(resultado["CONSULTAR"]);
                            if (resultado["INSERTAR"] != DBNull.Value) entidad.insertar = Convert.ToInt64(resultado["INSERTAR"]);
                            if (resultado["MODIFICAR"] != DBNull.Value) entidad.modificar = Convert.ToInt64(resultado["MODIFICAR"]);
                            if (resultado["BORRAR"] != DBNull.Value) entidad.borrar = Convert.ToInt64(resultado["BORRAR"]);
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
                        BOExcepcion.Throw("PerfilAccesoData", "ConsultarPerfilAcceso", ex);
                        return null;
                    }
                }
            }
        }

        public bool CrearCamposPerfil(CamposPermiso CPerfil, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PDescripcion = cmdTransaccionFactory.CreateParameter();
                        PDescripcion.ParameterName = "PDescripcion";
                        PDescripcion.Value = CPerfil.Campo;

                        DbParameter PVisible = cmdTransaccionFactory.CreateParameter();
                        PVisible.ParameterName = "PVisible";
                        PVisible.Value = CPerfil.Visible;

                        DbParameter PEditable = cmdTransaccionFactory.CreateParameter();
                        PEditable.ParameterName = "PEditable";
                        PEditable.Value = CPerfil.Editable;

                        DbParameter pcodPerfil = cmdTransaccionFactory.CreateParameter();
                        pcodPerfil.ParameterName = "pcodPerfil";
                        pcodPerfil.Value = CPerfil.CodPerfl;

                        cmdTransaccionFactory.Parameters.Add(PDescripcion);
                        cmdTransaccionFactory.Parameters.Add(PVisible);
                        cmdTransaccionFactory.Parameters.Add(PEditable);
                        cmdTransaccionFactory.Parameters.Add(pcodPerfil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PER_CAMPOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(CPerfil, "PERFIL_USUARIO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "CrearCamposPerfil", ex);
                        return false;
                    }
                }
            }
        }

        public bool EliminarCamposPerfil(CamposPermiso CPerfil, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcodPerfil = cmdTransaccionFactory.CreateParameter();
                        pcodPerfil.ParameterName = "pcodPerfil";
                        pcodPerfil.Value = CPerfil.CodPerfl;

                        cmdTransaccionFactory.Parameters.Add(pcodPerfil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_PER_CAMPOS_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(CPerfil, "PERFIL_USUARIO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "EliminarCamposPerfil", ex);
                        return false;
                    }
                }
            }
        }

        public List<CamposPermiso> ConsultarCamposPerfil(CamposPermiso CPerfil, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CamposPermiso> lstCamposPerfil = new List<CamposPermiso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT * FROM Campos_Perfil WHERE Cod_Perfil = " + CPerfil.CodPerfl;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CamposPermiso entidad = new CamposPermiso();

                            if (resultado["idCamPer"] != DBNull.Value) entidad.IdComPerfil = Convert.ToInt64(resultado["idCamPer"]);
                            if (resultado["Cod_Perfil"] != DBNull.Value) entidad.CodPerfl = Convert.ToInt32(resultado["Cod_Perfil"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["Descripcion"]);
                            if (resultado["Visible"] != DBNull.Value) entidad.Visible = Convert.ToInt32(resultado["Visible"]);
                            if (resultado["Editable"] != DBNull.Value) entidad.Editable = Convert.ToInt32(resultado["Editable"]);

                            lstCamposPerfil.Add(entidad);
                        }

                        return lstCamposPerfil;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilData", "ConsultarCamposPerfil", ex);
                        return null;
                    }
                }
            }
        }
    }
}