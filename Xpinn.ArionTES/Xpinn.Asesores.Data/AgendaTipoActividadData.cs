using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AGE_TIPOACTIVIDAD
    /// </summary>
    public class AgendaTipoActividadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AGE_TIPOACTIVIDAD
        /// </summary>
        public AgendaTipoActividadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla AGE_TIPOACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pAgendaTipoActividad">Entidad AgendaTipoActividad</param>
        /// <returns>Entidad AgendaTipoActividad creada</returns>
        public AgendaTipoActividad CrearAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDTIPO = cmdTransaccionFactory.CreateParameter();
                        pIDTIPO.ParameterName = "p_IdTipo";
                        pIDTIPO.Value = pAgendaTipoActividad.idtipo;
                        pIDTIPO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_Descripcion";
                        pDESCRIPCION.Value = pAgendaTipoActividad.descripcion;


                        cmdTransaccionFactory.Parameters.Add(pIDTIPO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_TIPOACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaTipoActividad, "AGE_TIPOACTIVIDAD",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAgendaTipoActividad.idtipo = Convert.ToInt64(pIDTIPO.Value);
                        return pAgendaTipoActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaTipoActividadData", "CrearAgendaTipoActividad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla AGE_TIPOACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad AgendaTipoActividad modificada</returns>
        public AgendaTipoActividad ModificarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDTIPO = cmdTransaccionFactory.CreateParameter();
                        pIDTIPO.ParameterName = "p_IDTIPO";
                        pIDTIPO.Value = pAgendaTipoActividad.idtipo;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pAgendaTipoActividad.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pIDTIPO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_TIPOACT_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaTipoActividad, "AGE_TIPOACTIVIDAD",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAgendaTipoActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaTipoActividadData", "ModificarAgendaTipoActividad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla AGE_TIPOACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pId">identificador de AGE_TIPOACTIVIDAD</param>
        public void EliminarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AgendaTipoActividad pAgendaTipoActividad = new AgendaTipoActividad();

                        //if (pUsuario.programaGeneraLog)
                        //    pAgendaTipoActividad = ConsultarAgendaTipoActividad(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDTIPO = cmdTransaccionFactory.CreateParameter();
                        pIDTIPO.ParameterName = "p_IdTipo";
                        pIDTIPO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDTIPO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_TIPOACT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaTipoActividad, "AGE_TIPOACTIVIDAD", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaTipoActividadData", "EliminarAgendaTipoActividad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AGE_TIPOACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AGE_TIPOACTIVIDAD</param>
        /// <returns>Entidad AgendaTipoActividad consultado</returns>
        public AgendaTipoActividad ConsultarAgendaTipoActividad(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            AgendaTipoActividad entidad = new AgendaTipoActividad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_TIPOACTIVIDAD WHERE IdTipo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDTIPO"] != DBNull.Value) entidad.idtipo = Convert.ToInt64(resultado["IDTIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaTipoActividadData", "ConsultarAgendaTipoActividad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AGE_TIPOACTIVIDAD dados unos filtros
        /// </summary>
        /// <param name="pAGE_TIPOACTIVIDAD">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaTipoActividad obtenidos</returns>
        public List<AgendaTipoActividad> ListarAgendaTipoActividad(AgendaTipoActividad pAgendaTipoActividad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AgendaTipoActividad> lstAgendaTipoActividad = new List<AgendaTipoActividad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_TIPOACTIVIDAD " + ObtenerFiltro(pAgendaTipoActividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AgendaTipoActividad entidad = new AgendaTipoActividad();

                            if (resultado["IDTIPO"] != DBNull.Value) entidad.idtipo = Convert.ToInt64(resultado["IDTIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstAgendaTipoActividad.Add(entidad);
                        }

                        return lstAgendaTipoActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaTipoActividadData", "ListarAgendaTipoActividad", ex);
                        return null;
                    }
                }
            }
        }
        public List<Persona> Listarclientes(long cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> lstAgendaActividad = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql;
                    if (cod == 0)
                        sql = "select * from V_GESTIONAR_AGENDA_USUARIOS order by snombre1";
                    else
                        sql = "select * from V_GESTIONAR_AGENDA_USUARIOS WHERE sidentificacion= '" + cod + "' order by snombre1";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        Persona entidad = new Persona();

                        if (resultado["SIDENTIFICACION"] != DBNull.Value) entidad.SegundoNombre = Convert.ToString(resultado["SIDENTIFICACION"]);
                        if (resultado["SNOMBRE1"] != DBNull.Value) entidad.PrimerNombre = Convert.ToString(resultado["SNOMBRE1"]) + " " + Convert.ToString(resultado["SAPELLIDO1"]) + " " + Convert.ToString(resultado["SAPELLIDO2"]);

                        lstAgendaActividad.Add(entidad);
                    }

                    return lstAgendaActividad;

                }
            }

        }

        public Int32 UsuarioEsAsesor(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int32 resul = new Int32();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select count(*) as NUMERO From usuarios Where identificacion not in (Select sidentificacion From asejecutivos Where estado = 1) And usuarios.estado = 1 And usuarios.codusuario = " + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                        resul = Convert.ToInt32(resultado["NUMERO"]);
                    return resul;
                }
            }








        }


        public List<Persona> correo(int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona> resul = new List<Persona>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "select semail,icodigo  from asejecutivos where icodigo =" + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();


                    Persona entidad = new Persona();
                    if (resultado.Read())
                    {
                        entidad.SegundoNombre = Convert.ToString(resultado["semail"]);
                        entidad.PrimerNombre = Convert.ToString(resultado["icodigo"]);
                        resul.Add(entidad);
                    }
                    return resul;

                }
            }








        }
    }
}