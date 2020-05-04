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
    /// Objeto de acceso a datos para la tabla Age_Alarma
    /// </summary>
    public class AgendaAlarmaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Age_Alarma
        /// </summary>
        public AgendaAlarmaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Age_Alarma de la base de datos
        /// </summary>
        /// <param name="pAgendaAlarma">Entidad AgendaAlarma</param>
        /// <returns>Entidad AgendaAlarma creada</returns>
        public AgendaAlarma CrearAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDALARMA = cmdTransaccionFactory.CreateParameter();
                        pIDALARMA.ParameterName = "p_idalarma";
                        pIDALARMA.Value = pAgendaAlarma.idalarma;
                        pIDALARMA.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.Value = pAgendaAlarma.fecha;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_hora";
                        pHORA.Value = pAgendaAlarma.hora;

                        DbParameter pTIPOALARMA = cmdTransaccionFactory.CreateParameter();
                        pTIPOALARMA.ParameterName = "p_tipoalarma";
                        pTIPOALARMA.Value = pAgendaAlarma.tipoalarma;

                        DbParameter pIDCLIENTE = cmdTransaccionFactory.CreateParameter();
                        pIDCLIENTE.ParameterName = "p_idcliente";
                        pIDCLIENTE.Value = pAgendaAlarma.idcliente;

                        DbParameter pTIPOACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pTIPOACTIVIDAD.ParameterName = "p_tipoactividad";
                        pTIPOACTIVIDAD.Value = pAgendaAlarma.tipoactividad;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pAgendaAlarma.descripcion;

                        DbParameter pREPETICIONES = cmdTransaccionFactory.CreateParameter();
                        pREPETICIONES.ParameterName = "p_repeticiones";
                        pREPETICIONES.Value = pAgendaAlarma.repeticiones;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        pESTADO.Value = pAgendaAlarma.estado;


                        cmdTransaccionFactory.Parameters.Add(pIDALARMA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOALARMA);
                        cmdTransaccionFactory.Parameters.Add(pIDCLIENTE);
                        cmdTransaccionFactory.Parameters.Add(pTIPOACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pREPETICIONES);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Age_Alarma_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAgendaAlarma, "Age_Alarma", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAgendaAlarma.idalarma = Convert.ToInt64(pIDALARMA.Value);
                        return pAgendaAlarma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaAlarmaData", "CrearAgendaAlarma", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Age_Alarma de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad AgendaAlarma modificada</returns>
        public AgendaAlarma ModificarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDALARMA = cmdTransaccionFactory.CreateParameter();
                        pIDALARMA.ParameterName = "p_IDALARMA";
                        pIDALARMA.Value = pAgendaAlarma.idalarma;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_FECHA";
                        pFECHA.Value = pAgendaAlarma.fecha;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_HORA";
                        pHORA.Value = pAgendaAlarma.hora;

                        DbParameter pTIPOALARMA = cmdTransaccionFactory.CreateParameter();
                        pTIPOALARMA.ParameterName = "p_TIPOALARMA";
                        pTIPOALARMA.Value = pAgendaAlarma.tipoalarma;

                        DbParameter pIDCLIENTE = cmdTransaccionFactory.CreateParameter();
                        pIDCLIENTE.ParameterName = "p_IDCLIENTE";
                        pIDCLIENTE.Value = pAgendaAlarma.idcliente;

                        DbParameter pTIPOACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pTIPOACTIVIDAD.ParameterName = "p_TIPOACTIVIDAD";
                        pTIPOACTIVIDAD.Value = pAgendaAlarma.tipoactividad;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pAgendaAlarma.descripcion;

                        DbParameter pREPETICIONES = cmdTransaccionFactory.CreateParameter();
                        pREPETICIONES.ParameterName = "p_REPETICIONES";
                        pREPETICIONES.Value = pAgendaAlarma.repeticiones;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pAgendaAlarma.estado;

                        cmdTransaccionFactory.Parameters.Add(pIDALARMA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOALARMA);
                        cmdTransaccionFactory.Parameters.Add(pIDCLIENTE);
                        cmdTransaccionFactory.Parameters.Add(pTIPOACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pREPETICIONES);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Age_Alarma_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAgendaAlarma, "Age_Alarma", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAgendaAlarma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaAlarmaData", "ModificarAgendaAlarma", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Age_Alarma de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Age_Alarma</param>
        public void EliminarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AgendaAlarma pAgendaAlarma = new AgendaAlarma();

                        if (pUsuario.programaGeneraLog)
                            pAgendaAlarma = ConsultarAgendaAlarma(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDALARMA = cmdTransaccionFactory.CreateParameter();
                        pIDALARMA.ParameterName = "p_idalarma";
                        pIDALARMA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDALARMA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Asesores_Age_Alarma_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pAgendaAlarma, "Age_Alarma", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaAlarmaData", "EliminarAgendaAlarma", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Age_Alarma de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Age_Alarma</param>
        /// <returns>Entidad AgendaAlarma consultado</returns>
        public AgendaAlarma ConsultarAgendaAlarma(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            AgendaAlarma entidad = new AgendaAlarma();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_ALARMA WHERE idalarma = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDALARMA"] != DBNull.Value) entidad.idalarma = Convert.ToInt64(resultado["IDALARMA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToInt64(resultado["HORA"]);
                            if (resultado["TIPOALARMA"] != DBNull.Value) entidad.tipoalarma = Convert.ToInt64(resultado["TIPOALARMA"]);
                            if (resultado["IDCLIENTE"] != DBNull.Value) entidad.idcliente = Convert.ToInt64(resultado["IDCLIENTE"]);
                            if (resultado["TIPOACTIVIDAD"] != DBNull.Value) entidad.tipoactividad = Convert.ToInt64(resultado["TIPOACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REPETICIONES"] != DBNull.Value) entidad.repeticiones = Convert.ToInt64(resultado["REPETICIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaAlarmaData", "ConsultarAgendaAlarma", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Age_Alarma dados unos filtros
        /// </summary>
        /// <param name="pAge_Alarma">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaAlarma obtenidos</returns>
        public List<AgendaAlarma> ListarAgendaAlarma(AgendaAlarma pAgendaAlarma, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AgendaAlarma> lstAgendaAlarma = new List<AgendaAlarma>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_ALARMA " + ObtenerFiltro(pAgendaAlarma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AgendaAlarma entidad = new AgendaAlarma();

                            if (resultado["IDALARMA"] != DBNull.Value) entidad.idalarma = Convert.ToInt64(resultado["IDALARMA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToInt64(resultado["HORA"]);
                            if (resultado["TIPOALARMA"] != DBNull.Value) entidad.tipoalarma = Convert.ToInt64(resultado["TIPOALARMA"]);
                            if (resultado["IDCLIENTE"] != DBNull.Value) entidad.idcliente = Convert.ToInt64(resultado["IDCLIENTE"]);
                            if (resultado["TIPOACTIVIDAD"] != DBNull.Value) entidad.tipoactividad = Convert.ToInt64(resultado["TIPOACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REPETICIONES"] != DBNull.Value) entidad.repeticiones = Convert.ToInt64(resultado["REPETICIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);

                            lstAgendaAlarma.Add(entidad);
                        }

                        return lstAgendaAlarma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaAlarmaData", "ListarAgendaAlarma", ex);
                        return null;
                    }
                }
            }
        }

    }
}