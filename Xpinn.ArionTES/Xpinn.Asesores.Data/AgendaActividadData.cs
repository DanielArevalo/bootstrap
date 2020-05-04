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
    /// Objeto de acceso a datos para la tabla AGE_ACTIVIDAD
    /// </summary>
    public class AgendaActividadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AGE_ACTIVIDAD
        /// </summary>
        public AgendaActividadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla AGE_ACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pAgendaActividad">Entidad AgendaActividad</param>
        /// <returns>Entidad AgendaActividad creada</returns>
        public AgendaActividad CrearAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pIDACTIVIDAD.Direction = ParameterDirection.Output;
                        pIDACTIVIDAD.ParameterName = "p_IdActividad";
                        pIDACTIVIDAD.Value = pAgendaActividad.idactividad;
                        pIDACTIVIDAD.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.Direction = ParameterDirection.Input;
                        pFECHA.ParameterName = "p_Fecha";
                        pFECHA.Value = pAgendaActividad.fecha;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.Direction = ParameterDirection.Input;
                        pHORA.ParameterName = "p_Hora";
                        pHORA.Value = pAgendaActividad.hora;

                        DbParameter pIDCLIENTE = cmdTransaccionFactory.CreateParameter();
                        pIDCLIENTE.Direction = ParameterDirection.Input;
                        pIDCLIENTE.ParameterName = "p_IdCliente";
                        pIDCLIENTE.Value = pAgendaActividad.idcliente;

                        DbParameter pTIPOACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pTIPOACTIVIDAD.Direction = ParameterDirection.Input;
                        pTIPOACTIVIDAD.ParameterName = "p_TipoActividad";
                        pTIPOACTIVIDAD.Value = pAgendaActividad.tipoactividad;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.Direction = ParameterDirection.Input;
                        pDESCRIPCION.ParameterName = "p_Descripcion";
                        pDESCRIPCION.Value = pAgendaActividad.descripcion;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.ParameterName = "p_Estado";
                        pESTADO.Value = pAgendaActividad.estado;

                        DbParameter pIDASESOR = cmdTransaccionFactory.CreateParameter();
                        pIDASESOR.Direction = ParameterDirection.Input;
                        pIDASESOR.ParameterName = "p_IdAsesor";
                        pIDASESOR.Value = pAgendaActividad.idasesor;

                        cmdTransaccionFactory.Parameters.Add(pIDACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pIDCLIENTE);
                        cmdTransaccionFactory.Parameters.Add(pTIPOACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pIDASESOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_ACT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaActividad, "AGE_ACTIVIDAD",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAgendaActividad.idactividad = Convert.ToInt64(pIDACTIVIDAD.Value);
                        return pAgendaActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaActividadData", "CrearAgendaActividad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla AGE_ACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad AgendaActividad modificada</returns>
        public AgendaActividad ModificarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    //try
                    //{
                        DbParameter pIDACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pIDACTIVIDAD.ParameterName = "p_IdActividad";
                        pIDACTIVIDAD.Value = pAgendaActividad.idactividad;
                        pIDACTIVIDAD.Direction = ParameterDirection.InputOutput;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_Estado";
                        pESTADO.Value = pAgendaActividad.estado;

                        DbParameter pATENDIDO = cmdTransaccionFactory.CreateParameter();
                        pATENDIDO.ParameterName = "p_Atendido";
                        pATENDIDO.Value = pAgendaActividad.atendido;

                        DbParameter pIDPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pIDPARENTESCO.ParameterName = "p_IdParentesco";
                        pIDPARENTESCO.Value = pAgendaActividad.idparentesco;

                        DbParameter pRESPUESTA = cmdTransaccionFactory.CreateParameter();
                        pRESPUESTA.ParameterName = "p_Respuesta";
                        pRESPUESTA.Value = pAgendaActividad.respuesta;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_Observaciones";
                        pOBSERVACIONES.Value = pAgendaActividad.observaciones;

                        cmdTransaccionFactory.Parameters.Add(pIDACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pATENDIDO);
                        cmdTransaccionFactory.Parameters.Add(pIDPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pRESPUESTA);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_ACT_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaActividad, "AGE_ACTIVIDAD",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAgendaActividad;
                    //}
                    //catch (Exception ex)
                    //{
                    //    BOExcepcion.Throw("AgendaActividadData", "ModificarAgendaActividad", ex);
                    //    return null;
                    //}
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla AGE_ACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pId">identificador de AGE_ACTIVIDAD</param>
        public void EliminarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //AgendaActividad pAgendaActividad = new AgendaActividad();

                        //if (pUsuario.programaGeneraLog)
                        //    pAgendaActividad = ConsultarAgendaActividad(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pIDACTIVIDAD.ParameterName = "p_IdActividad";
                        pIDACTIVIDAD.Direction = ParameterDirection.Input;
                        pIDACTIVIDAD.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDACTIVIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_ACT_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaActividad, "AGE_ACTIVIDAD", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaActividadData", "EliminarAgendaActividad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla AGE_ACTIVIDAD de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla AGE_ACTIVIDAD</param>
        /// <returns>Entidad AgendaActividad consultado</returns>
        public AgendaActividad ConsultarAgendaActividad(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            AgendaActividad entidad = new AgendaActividad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_ACTIVIDAD WHERE IdActividad = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDACTIVIDAD"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["IDACTIVIDAD"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToInt64(resultado["HORA"]);
                            if (resultado["IDCLIENTE"] != DBNull.Value) entidad.idcliente = Convert.ToInt64(resultado["IDCLIENTE"]);
                            if (resultado["TIPOACTIVIDAD"] != DBNull.Value) entidad.tipoactividad = Convert.ToString(resultado["TIPOACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ATENDIDO"] != DBNull.Value) entidad.atendido = Convert.ToString(resultado["ATENDIDO"]);
                            if (resultado["IDPARENTESCO"] != DBNull.Value) entidad.idparentesco = Convert.ToInt64(resultado["IDPARENTESCO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["IDASESOR"] != DBNull.Value) entidad.idasesor = Convert.ToInt64(resultado["IDASESOR"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaActividadData", "ConsultarAgendaActividad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla AGE_ACTIVIDAD dados unos filtros
        /// </summary>
        /// <param name="pAGE_ACTIVIDAD">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaActividad obtenidos</returns>
        public List<AgendaActividad> ListarAgendaActividad(AgendaActividad pAgendaActividad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AgendaActividad> lstAgendaActividad = new List<AgendaActividad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "SELECT * FROM V_AGENDAACTIVIDAD WHERE idasesor = " + pAgendaActividad.idasesor + " AND trunc(fecha) = To_Date('" + pAgendaActividad.fecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AgendaActividad entidad = new AgendaActividad();

                            if (resultado["IDACTIVIDAD"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["IDACTIVIDAD"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToInt64(resultado["HORA"]);
                            if (resultado["IDCLIENTE"] != DBNull.Value) entidad.idcliente = Convert.ToInt64(resultado["IDCLIENTE"]);
                            if (resultado["NOMBRECLIENTE"] != DBNull.Value) entidad.nombrecliente = Convert.ToString(resultado["NOMBRECLIENTE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ATENDIDO"] != DBNull.Value) entidad.atendido = Convert.ToString(resultado["ATENDIDO"]);
                            if (resultado["IDPARENTESCO"] != DBNull.Value) entidad.idparentesco = Convert.ToInt64(resultado["IDPARENTESCO"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["IDASESOR"] != DBNull.Value) entidad.idasesor = Convert.ToInt64(resultado["IDASESOR"]);
                            if (resultado["TIPO_ACTIVIDAD"] != DBNull.Value) entidad.tipoactividad = Convert.ToString(resultado["TIPO_ACTIVIDAD"]);
                       
                            lstAgendaActividad.Add(entidad);
                        }

                        return lstAgendaActividad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaActividadData", "ListarAgendaActividad", ex);
                        return null;
                    }
                }
            }
        }



    }
}