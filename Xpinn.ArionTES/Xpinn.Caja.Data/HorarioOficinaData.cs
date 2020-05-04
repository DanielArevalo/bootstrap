using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla HorarioOficina
    /// </summary>
    public class HorarioOficinaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Horario
        /// </summary>
        public HorarioOficinaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un Horario de Oficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Horario Oficina</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.HorarioOficina InsertarHorarioOficina(Xpinn.Caja.Entities.HorarioOficina pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int16;
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcod_diasemana = cmdTransaccionFactory.CreateParameter();
                        pcod_diasemana.ParameterName = "pcodigodiasemana";
                        pcod_diasemana.Value = pEntidad.dia;
                        pcod_diasemana.DbType = DbType.Int16;
                        pcod_diasemana.Size = 8;
                        pcod_diasemana.Direction = ParameterDirection.Input;

                        DbParameter p_horaini = cmdTransaccionFactory.CreateParameter();
                        p_horaini.ParameterName = "phoraini";
                        p_horaini.Value = pEntidad.hora_inicial;
                        p_horaini.DbType = DbType.Date;
                        p_horaini.Direction = ParameterDirection.Input;
                        p_horaini.Size = 7;

                        DbParameter p_horafin = cmdTransaccionFactory.CreateParameter();
                        p_horafin.ParameterName = "phorafin";
                        p_horafin.Value = pEntidad.hora_final;
                        p_horafin.DbType = DbType.DateTime;
                        p_horafin.Direction = ParameterDirection.Input;
                        p_horafin.Size = 7;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = pEntidad.tipo_horario;
                        ptipo_horario.DbType = DbType.Int64;
                        ptipo_horario.Direction = ParameterDirection.Input;
                        ptipo_horario.Size = 8;

                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_diasemana);
                        cmdTransaccionFactory.Parameters.Add(p_horaini);
                        cmdTransaccionFactory.Parameters.Add(p_horafin);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_HORARIOOFICINA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string sql = "SELECT max(cod_horario) conteo FROM HORARIOOFICINA where cod_oficina=" + pEntidad.cod_oficina + " and tipo_horario=" + pEntidad.tipo_horario;
                        DbDataReader resultado = default(DbDataReader);
                        HorarioOficina entidad = new HorarioOficina();

                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = long.Parse(resultado["conteo"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        pEntidad.cod_horario = Convert.ToInt64(entidad.conteo);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "InsertarHorarioOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad HorarioOficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad HorarioOficina</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.HorarioOficina ModificarHorarioOficina(Caja.Entities.HorarioOficina pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        DbParameter pcod_horario = cmdTransaccionFactory.CreateParameter();
                        pcod_horario.ParameterName = "pcodigohorario";
                        pcod_horario.Value = pEntidad.cod_horario;
                        pcod_horario.DbType = DbType.Int16;
                        pcod_horario.Size = 8;
                        pcod_horario.Direction = ParameterDirection.Input;

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int16;
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcod_diasemana = cmdTransaccionFactory.CreateParameter();
                        pcod_diasemana.ParameterName = "pcodigodiasemana";
                        pcod_diasemana.Value = pEntidad.dia;
                        pcod_diasemana.DbType = DbType.Int16;
                        pcod_diasemana.Size = 8;
                        pcod_diasemana.Direction = ParameterDirection.Input;

                        DbParameter p_horaini = cmdTransaccionFactory.CreateParameter();
                        p_horaini.ParameterName = "phoraini";
                        p_horaini.Value = pEntidad.hora_inicial;
                        p_horaini.DbType = DbType.Date;
                        p_horaini.Direction = ParameterDirection.Input;
                        p_horaini.Size = 7;

                        DbParameter p_horafin = cmdTransaccionFactory.CreateParameter();
                        p_horafin.ParameterName = "phorafin";
                        p_horafin.Value = pEntidad.hora_final;
                        p_horafin.DbType = DbType.DateTime;
                        p_horafin.Direction = ParameterDirection.Input;
                        p_horafin.Size = 7;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = pEntidad.tipo_horario;
                        ptipo_horario.DbType = DbType.Int64;
                        ptipo_horario.Direction = ParameterDirection.Input;
                        ptipo_horario.Size = 8;

                        cmdTransaccionFactory.Parameters.Add(pcod_horario);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_diasemana);
                        cmdTransaccionFactory.Parameters.Add(p_horaini);
                        cmdTransaccionFactory.Parameters.Add(p_horafin);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_HORARIOOFICINA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "ModificarHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Elimina una HorarioOficina en la base de datos
        /// </summary>
        /// <param name="pId">identificador de HorarioOficina</param>
        public void EliminarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Caja.Entities.HorarioOficina pEntidad = new Xpinn.Caja.Entities.HorarioOficina();

                        DbParameter pcod_horario = cmdTransaccionFactory.CreateParameter();
                        pcod_horario.ParameterName = "pcodigohorario";
                        pcod_horario.Value = pId;
                        pcod_horario.DbType = DbType.Int16;
                        pcod_horario.Size = 8;
                        pcod_horario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_horario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_HORARIOOFICINA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "EliminarHorarioOficina", ex);
                    }
                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Horario Oficina de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina ConsultarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.HorarioOficina entidad = new Caja.Entities.HorarioOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.cod_horario codhorario,a.cod_oficina codoficina, a.tipo_horario tipohorario,a.dia ndia,a.hora_inicial horaini,a.hora_final horafin FROM HORARIOOFICINA a where a.cod_horario=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["codhorario"] != DBNull.Value) entidad.cod_horario = Convert.ToInt64(resultado["codhorario"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["codoficina"]);
                            // if (resultado["nomoficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToInt64(resultado["nomoficina"]);
                            if (resultado["tipohorario"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt64(resultado["tipohorario"]);
                            if (resultado["ndia"] != DBNull.Value) entidad.dia = Convert.ToInt64(resultado["ndia"]);
                            if (resultado["horaini"] != DBNull.Value) entidad.hora_inicial = Convert.ToDateTime(resultado["horaini"]);
                            if (resultado["horafin"] != DBNull.Value) entidad.hora_final = Convert.ToDateTime(resultado["horafin"]);

                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "ConsultarHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Horario Oficina de la base de datos incluyendo la fecha de sistema
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina getHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.HorarioOficina entidad = new Caja.Entities.HorarioOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select a.cod_horario, a.hora_inicial horaini, a.hora_final horafin, sysdate fecha_hoy From HorarioOficina a Where a.cod_oficina = " + pId.ToString() + " and dia = (select to_char(max(fecha_proceso),'D') from procesooficina where cod_oficina = " + pId.ToString() + ") and tipo_horario = (select x.tipo_horario from procesooficina x where x.fecha_proceso = (select max(y.fecha_proceso) from procesooficina y where y.cod_oficina = " + pId.ToString() + " ) and x.cod_oficina = " + pId.ToString() + ")";
                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_horario"] != DBNull.Value) entidad.cod_horario = Convert.ToInt64(resultado["cod_horario"]);
                            if (resultado["horaini"] != DBNull.Value) entidad.hora_inicial = Convert.ToDateTime(resultado["horaini"]);
                            if (resultado["horafin"] != DBNull.Value) entidad.hora_final = Convert.ToDateTime(resultado["horafin"]);
                            if (resultado["fecha_hoy"] != DBNull.Value) entidad.fecha_hoy = Convert.ToDateTime(resultado["fecha_hoy"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "getHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Obtiene una respuesta si el horario de la oficina esta activo o no para el dia especifico
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Horario Oficina consultada</returns>
        public Caja.Entities.HorarioOficina VerificarHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.HorarioOficina entidad = new Caja.Entities.HorarioOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select count(*) conteo From horariooficina Where cod_oficina = " + pId.ToString() + " And dia = (select to_char(max(fecha_proceso),'D') from procesooficina where cod_oficina = " + pId.ToString() + ") And tipo_horario = (select Max(x.tipo_horario) from procesooficina x where x.fecha_proceso=(select max(y.fecha_proceso) from procesooficina y where y.cod_oficina=" + pId.ToString() + " ) And x.cod_oficina = " + pId.ToString() + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = Convert.ToInt64(resultado["conteo"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "VerificarHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla CajeroXCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>CajeroXCaja consultada</returns>
        public HorarioOficina ConsultarHorarioXOficina(HorarioOficina pHorario, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            HorarioOficina entidad = new HorarioOficina();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo FROM HORARIOOFICINA where cod_oficina=" + pHorario.cod_oficina + " and tipo_horario=" + pHorario.tipo_horario + " and dia=" + pHorario.dia;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = long.Parse(resultado["conteo"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "ConsultarHorarioXOficina", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de HorarioOficina dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Horario Oficina obtenidas</returns>
        public List<Caja.Entities.HorarioOficina> ListarHorarioOficina(Caja.Entities.HorarioOficina pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.HorarioOficina> lstHorario = new List<Caja.Entities.HorarioOficina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT cod_horario,cod_oficina,tipo_horario,dia,hora_inicial,hora_final,(select nom_diasemana from diassemana where cod_diasemana=dia) nombredia FROM HORARIOOFICINA where cod_oficina=" + pEntidad.cod_oficina.ToString() + " and tipo_horario=" + pEntidad.tipo_horario.ToString() + " Order by dia";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.HorarioOficina entidad = new Caja.Entities.HorarioOficina();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_horario"] != DBNull.Value) entidad.cod_horario = Convert.ToInt64(resultado["cod_horario"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["tipo_horario"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt64(resultado["tipo_horario"]);
                            if (resultado["dia"] != DBNull.Value) entidad.dia = Convert.ToInt64(resultado["dia"]);
                            if (resultado["hora_inicial"] != DBNull.Value) entidad.hora_inicial = Convert.ToDateTime(resultado["hora_inicial"]);
                            if (resultado["hora_final"] != DBNull.Value) entidad.hora_final = Convert.ToDateTime(resultado["hora_final"]);
                            if (resultado["nombredia"] != DBNull.Value) entidad.nom_dia = resultado["nombredia"].ToString();
                            lstHorario.Add(entidad);
                        }

                        return lstHorario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "ListarHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Método para determinar el día en que esta operando la caaja
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public string getDiaHorarioOficina(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string diaHorario = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select nom_diasemana From diassemana Where cod_diasemana = (select to_char(max(fecha_proceso),'D') As dia from procesooficina where cod_oficina = " + pId.ToString() + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nom_diasemana"] != DBNull.Value) diaHorario = Convert.ToString(resultado["nom_diasemana"]);
                        }

                        return diaHorario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("HorarioOficinaData", "getDiaHorarioOficina", ex);
                        return null;
                    }

                }
            }
        }

    }
}
