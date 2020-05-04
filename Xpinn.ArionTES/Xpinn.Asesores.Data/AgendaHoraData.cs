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
    /// Objeto de acceso a datos para la tabla age_hora
    /// </summary>
    public class AgendaHoraData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla age_hora
        /// </summary>
        public AgendaHoraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla age_hora de la base de datos
        /// </summary>
        /// <param name="pAgendaHora">Entidad AgendaHora</param>
        /// <returns>Entidad AgendaHora creada</returns>
        public AgendaHora CrearAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDHORA = cmdTransaccionFactory.CreateParameter();
                        pIDHORA.ParameterName = "p_IdHora";
                        pIDHORA.Value = pAgendaHora.idhora;
                        pIDHORA.Direction = ParameterDirection.InputOutput;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_Hora";
                        pHORA.Value = pAgendaHora.hora;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_Tipo";
                        pTIPO.Value = pAgendaHora.tipo;


                        cmdTransaccionFactory.Parameters.Add(pIDHORA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_HORA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaHora, "age_hora",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pAgendaHora.idhora = Convert.ToInt64(pIDHORA.Value);
                        return pAgendaHora;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaHoraData", "CrearAgendaHora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla age_hora de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad AgendaHora modificada</returns>
        public AgendaHora ModificarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDHORA = cmdTransaccionFactory.CreateParameter();
                        pIDHORA.ParameterName = "p_IDHORA";
                        pIDHORA.Value = pAgendaHora.idhora;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_HORA";
                        pHORA.Value = pAgendaHora.hora;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_TIPO";
                        pTIPO.Value = pAgendaHora.tipo;

                        cmdTransaccionFactory.Parameters.Add(pIDHORA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_HORA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaHora, "age_hora",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pAgendaHora;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaHoraData", "ModificarAgendaHora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla age_hora de la base de datos
        /// </summary>
        /// <param name="pId">identificador de age_hora</param>
        public void EliminarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        AgendaHora pAgendaHora = new AgendaHora();

                        //if (pUsuario.programaGeneraLog)
                        //    pAgendaHora = ConsultarAgendaHora(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDHORA = cmdTransaccionFactory.CreateParameter();
                        pIDHORA.ParameterName = "p_IdHora";
                        pIDHORA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDHORA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_AGENDA_HORA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pAgendaHora, "age_hora", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaHoraData", "EliminarAgendaHora", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla age_hora de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla age_hora</param>
        /// <returns>Entidad AgendaHora consultado</returns>
        public AgendaHora ConsultarAgendaHora(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            AgendaHora entidad = new AgendaHora();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  AGE_HORA WHERE IdHora = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDHORA"] != DBNull.Value) entidad.idhora = Convert.ToInt64(resultado["IDHORA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToInt64(resultado["HORA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaHoraData", "ConsultarAgendaHora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla age_hora dados unos filtros
        /// </summary>
        /// <param name="page_hora">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de AgendaHora obtenidos</returns>
        public List<AgendaHora> ListarAgendaHora(AgendaHora pAgendaHora, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AgendaHora> lstAgendaHora = new List<AgendaHora>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select IDACTIVIDAD,idhora, AH.hora, decode(tipo,1,'a.m.', 2, 'p.m.') as tipo, AH.hora ||' '|| decode(tipo,1,'a.m.', 2, 'p.m.') as horatipo,aa.descripcion,aa.respuesta from AGE_HORA AH LEFT JOIN AGE_ACTIVIDAD AA ON AA.HORA = AH.IDHORA and aa.fecha =to_date( '" + pAgendaHora.respuesta + "','dd/mm/yyyy') And Aa.idasesor = " + pAgendaHora.idactividad + " order by tipo, hora";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AgendaHora entidad = new AgendaHora();

                            if (resultado["IDHORA"] != DBNull.Value) entidad.idhora = Convert.ToInt64(resultado["IDHORA"]);
                            if (resultado["HORA"] != DBNull.Value) entidad.hora = Convert.ToDecimal(resultado["HORA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["HORATIPO"] != DBNull.Value) entidad.horatipo = Convert.ToString(resultado["HORATIPO"]);
                            if (resultado["IDACTIVIDAD"] != DBNull.Value) entidad.idactividad = Convert.ToInt64(resultado["IDACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);

                            lstAgendaHora.Add(entidad);
                        }

                        return lstAgendaHora;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AgendaHoraData", "ListarAgendaHora", ex);
                        return null;
                    }
                }
            }
        }
    }
}