using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ClasificacionData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ClasificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Clasificacion CrearClasificacion(Clasificacion pClasificacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pClasificacion.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pClasificacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pClasificacion.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pClasificacion.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pClasificacion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pClasificacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter paportes_garantia = cmdTransaccionFactory.CreateParameter();
                        paportes_garantia.ParameterName = "p_aportes_garantia";
                        if (pClasificacion.aportes_garantia == null)
                            paportes_garantia.Value = DBNull.Value;
                        else
                            paportes_garantia.Value = pClasificacion.aportes_garantia;
                        paportes_garantia.Direction = ParameterDirection.Input;
                        paportes_garantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paportes_garantia);

                        DbParameter paportes_gar_clasificacion = cmdTransaccionFactory.CreateParameter();
                        paportes_gar_clasificacion.ParameterName = "p_aportes_clasificacion";
                        if (pClasificacion.aportes_garantia == null)
                            paportes_gar_clasificacion.Value = DBNull.Value;
                        else
                            paportes_gar_clasificacion.Value = pClasificacion.aportes_gar_clasificacion;
                        paportes_gar_clasificacion.Direction = ParameterDirection.Input;
                        paportes_gar_clasificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paportes_gar_clasificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CLASIFICAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionData", "CrearClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public Clasificacion ModificarClasificacion(Clasificacion pClasificacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pClasificacion.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pClasificacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pClasificacion.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pClasificacion.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pClasificacion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pClasificacion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter paportes_garantia = cmdTransaccionFactory.CreateParameter();
                        paportes_garantia.ParameterName = "p_aportes_garantia";
                        if (pClasificacion.aportes_garantia == null)
                            paportes_garantia.Value = DBNull.Value;
                        else
                            paportes_garantia.Value = pClasificacion.aportes_garantia;
                        paportes_garantia.Direction = ParameterDirection.Input;
                        paportes_garantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paportes_garantia);

                        DbParameter paportes_gar_clasificacion = cmdTransaccionFactory.CreateParameter();
                        paportes_gar_clasificacion.ParameterName = "p_aportes_clasificacion";
                        if (pClasificacion.aportes_garantia == null)
                            paportes_gar_clasificacion.Value = DBNull.Value;
                        else
                            paportes_gar_clasificacion.Value = pClasificacion.aportes_gar_clasificacion;
                        paportes_gar_clasificacion.Direction = ParameterDirection.Input;
                        paportes_gar_clasificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(paportes_gar_clasificacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CLASIFICAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionData", "ModificarClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarClasificacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Clasificacion pClasificacion = new Clasificacion();
                        pClasificacion = ConsultarClasificacion(pId, vUsuario);

                        DbParameter pcod_clasifica = cmdTransaccionFactory.CreateParameter();
                        pcod_clasifica.ParameterName = "p_cod_clasifica";
                        pcod_clasifica.Value = pClasificacion.cod_clasifica;
                        pcod_clasifica.Direction = ParameterDirection.Input;
                        pcod_clasifica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_clasifica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CLASIFICAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionData", "EliminarClasificacion", ex);
                    }
                }
            }
        }


        public Clasificacion ConsultarClasificacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Clasificacion entidad = new Clasificacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Clasificacion WHERE COD_CLASIFICA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["APORTES_GARANTIA"] != DBNull.Value) entidad.aportes_garantia = Convert.ToInt32(resultado["APORTES_GARANTIA"]);
                            if (resultado["APORTES_GAR_CLASIFICACION"] != DBNull.Value) entidad.aportes_gar_clasificacion = Convert.ToInt32(resultado["APORTES_GAR_CLASIFICACION"]);
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
                        BOExcepcion.Throw("ClasificacionData", "ConsultarClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<Clasificacion> ListarClasificacion(Clasificacion pClasificacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Clasificacion> lstClasificacion = new List<Clasificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Clasificacion.*, TipoTasaHist.descripcion As nom_tipo_historico FROM Clasificacion LEFT JOIN TipoTasaHist ON TipoTasaHist.tipo_historico = Clasificacion.tipo_historico " + ObtenerFiltro(pClasificacion, "Clasificacion.") + " ORDER BY Clasificacion.COD_CLASIFICA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Clasificacion entidad = new Clasificacion();
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["COD_CLASIFICA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["NOM_TIPO_HISTORICO"] != DBNull.Value) entidad.nom_tipo_historico = Convert.ToString(resultado["NOM_TIPO_HISTORICO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["APORTES_GARANTIA"] != DBNull.Value) entidad.aportes_garantia = Convert.ToInt32(resultado["APORTES_GARANTIA"]);
                            if (resultado["APORTES_GAR_CLASIFICACION"] != DBNull.Value) entidad.aportes_gar_clasificacion = Convert.ToInt32(resultado["APORTES_GAR_CLASIFICACION"]);
                            lstClasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<ClasificacionCartera> listarpersonas(Usuario pUsuario, string fechainicio, string fechafin,string oficina)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> lstComponenteAdicional = new List<ClasificacionCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        Configuracion conf = new Configuracion();
                      
                            string sql = " select distinct numero_radicacion,p.identificacion,p.primer_apellido || ' ' || p.segundo_apellido  || ' ' || p.primer_nombre || ' ' || p.segundo_nombre as nombre from historico_cre h join persona p on h.cod_cliente=p.cod_persona where fecha_historico between to_date('" + fechainicio+"','dd/mm/yyyy') and to_date('"+fechafin+"','dd/mm/yyyy') "+oficina;
                   


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NUMERO_RADICACION = (Convert.ToString(resultado["numero_radicacion"]));
                            if (resultado["identificacion"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["nombre"]);
                           

                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "listarpersonas", ex);
                        return null;
                    }
                }
            }
        }
        public List<ClasificacionCartera> listarfechas(Usuario pUsuario, string fechainicio, string fechafin)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> lstComponenteAdicional = new List<ClasificacionCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        Configuracion conf = new Configuracion();

                        string sql = "select distinct  EXTRACT(DAY FROM fecha_historico)||' de '|| to_char(fecha_historico, 'Month','nls_date_language=spanish')||' de '|| EXTRACT(YEAR FROM fecha_historico) as FECHA,fecha_historico from historico_cre where fecha_historico between to_date('" + fechainicio + "','dd/mm/yyyy') and to_date('" + fechafin + "','dd/mm/yyyy') order by fecha_historico";



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();

                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = (Convert.ToString(resultado["fecha"]));
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToString(resultado["fecha_historico"]);

                            
                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "listarfechas", ex);
                        return null;
                    }
                }
            }
        }
        public ClasificacionCartera ConsultarClasificacionHist(string numero_radicacion, string cod_clasifica,string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ClasificacionCartera entidad = new ClasificacionCartera();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        DateTime date =Convert.ToDateTime(fecha);
                        string sql = @"select  (select cod_categoria from historico_cre where cod_clasifica=1 and hc.consecutivo=consecutivo) AS CC, 
                                    (select cod_categoria from historico_cre where cod_clasifica=2 and hc.consecutivo=consecutivo) AS CO,
                                    (select cod_categoria from historico_cre where cod_clasifica=3 and hc.consecutivo=consecutivo) AS VI,
                                    (select cod_categoria from historico_cre where cod_clasifica=4 and hc.consecutivo=consecutivo) AS MI  from historico_cre hc where fecha_historico=to_date('" + date.ToShortDateString()+"','dd/mm/yyyy') and  numero_radicacion= "+numero_radicacion;




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CC"] != DBNull.Value) entidad.CC = Convert.ToString(resultado["CC"]);
                            if (resultado["CO"] != DBNull.Value) entidad.CO = Convert.ToString(resultado["CO"]);
                            if (resultado["VI"] != DBNull.Value) entidad.VI = Convert.ToString(resultado["VI"]);
                            if (resultado["MI"] != DBNull.Value) entidad.MI = Convert.ToString(resultado["MI"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        cmdTransaccionFactory.Dispose();
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarCarteraOficinas", ex);
                        return null;
                    }
                }
            }
        }

    }
}
