using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class AprobadorData : GlobalData 
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public AprobadorData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Aprobador en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Aprobador</param>
        /// <returns>Entidad creada</returns>
        public Aprobador InsertarAprobador(Aprobador pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigolinea = cmdTransaccionFactory.CreateParameter();
                        p_codigolinea.ParameterName = "p_codigolinea";
                        p_codigolinea.Value = pEntidad.LineaCredito;
                        //p_codigolinea.DbType = DbType.String;
                        //p_codigolinea.Size = 8;
                        p_codigolinea.Direction = ParameterDirection.Input;

                        DbParameter p_codigousuario = cmdTransaccionFactory.CreateParameter();
                        p_codigousuario.ParameterName = "p_codigousuario";
                        p_codigousuario.Value = Int32.Parse(pEntidad.UsuarioAprobador);
                        //p_codigousuario.DbType = DbType.Int32;
                        //p_codigousuario.Size = 8;
                        p_codigousuario.Direction = ParameterDirection.Input;

                        DbParameter p_montominimo = cmdTransaccionFactory.CreateParameter();
                        p_montominimo.ParameterName = "p_montominimo";
                        p_montominimo.Value = pEntidad.MontoMinimo;
                        //p_montominimo.DbType = DbType.Int64;
                        //p_montominimo.Size = 15;
                        p_montominimo.Direction = ParameterDirection.Input;

                        DbParameter p_montomaximo = cmdTransaccionFactory.CreateParameter();
                        p_montomaximo.ParameterName = "p_montomaximo";
                        p_montomaximo.Value = pEntidad.MontoMaximo;
                        //p_montomaximo.DbType = DbType.Int64;
                        //p_montomaximo.Size = 15;
                        p_montomaximo.Direction = ParameterDirection.Input;

                        DbParameter p_nivel = cmdTransaccionFactory.CreateParameter();
                        p_nivel.ParameterName = "p_nivel";
                        p_nivel.Value = pEntidad.Nivel;
                        //p_nivel.DbType = DbType.Int32;
                        //p_nivel.Size = 8;
                        p_nivel.Direction = ParameterDirection.Input;

                        DbParameter p_aprueba = cmdTransaccionFactory.CreateParameter();
                        p_aprueba.ParameterName = "p_aprueba";
                        p_aprueba.Value = pEntidad.Aprueba;
                        //p_aprueba.DbType = DbType.Int32;
                        //p_aprueba.Size = 8;
                        p_aprueba.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_codigolinea);
                        cmdTransaccionFactory.Parameters.Add(p_codigousuario);
                        cmdTransaccionFactory.Parameters.Add(p_montominimo);
                        cmdTransaccionFactory.Parameters.Add(p_montomaximo);
                        cmdTransaccionFactory.Parameters.Add(p_nivel);
                        cmdTransaccionFactory.Parameters.Add(p_aprueba);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_APROB_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "InsertarAprobador", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad Aprobador en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad aprobador</param>
        /// <returns>Entidad modificada</returns>
        public Aprobador ModificarAprobador(Aprobador pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idaprobador = cmdTransaccionFactory.CreateParameter();
                        p_idaprobador.ParameterName = "p_idaprobador";
                        p_idaprobador.Value = pEntidad.Id;
                        //p_idaprobador.DbType = DbType.Int32;
                        //p_idaprobador.Size = 8;
                        p_idaprobador.Direction = ParameterDirection.Input;

                        DbParameter p_codigolinea = cmdTransaccionFactory.CreateParameter();
                        p_codigolinea.ParameterName = "p_codigolinea";
                        p_codigolinea.Value = pEntidad.LineaCredito;
                        //p_codigolinea.DbType = DbType.String;
                        //p_codigolinea.Size = 8;
                        p_codigolinea.Direction = ParameterDirection.Input;

                        DbParameter p_codigousuario = cmdTransaccionFactory.CreateParameter();
                        p_codigousuario.ParameterName = "p_codigousuario";
                        p_codigousuario.Value = Int32.Parse(pEntidad.UsuarioAprobador);
                        //p_codigousuario.DbType = DbType.Int32;
                        //p_codigousuario.Size = 8;
                        p_codigousuario.Direction = ParameterDirection.Input;

                        DbParameter p_montominimo = cmdTransaccionFactory.CreateParameter();
                        p_montominimo.ParameterName = "p_montominimo";
                        p_montominimo.Value = pEntidad.MontoMinimo;
                        //p_montominimo.DbType = DbType.Int64;
                        //p_montominimo.Size = 15;
                        p_montominimo.Direction = ParameterDirection.Input;

                        DbParameter p_montomaximo = cmdTransaccionFactory.CreateParameter();
                        p_montomaximo.ParameterName = "p_montomaximo";
                        p_montomaximo.Value = pEntidad.MontoMaximo;
                        //p_montomaximo.DbType = DbType.Int64;
                        //p_montomaximo.Size = 15;
                        p_montomaximo.Direction = ParameterDirection.Input;

                        DbParameter p_nivel = cmdTransaccionFactory.CreateParameter();
                        p_nivel.ParameterName = "p_nivel";
                        p_nivel.Value = pEntidad.Nivel;
                        //p_nivel.DbType = DbType.Int32;
                        //p_nivel.Size = 8;
                        p_nivel.Direction = ParameterDirection.Input;

                        DbParameter p_aprueba = cmdTransaccionFactory.CreateParameter();
                        p_aprueba.ParameterName = "p_aprueba";
                        p_aprueba.Value = pEntidad.Aprueba;
                        //p_aprueba.DbType = DbType.Int32;
                        //p_aprueba.Size = 8;
                        p_aprueba.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idaprobador);
                        cmdTransaccionFactory.Parameters.Add(p_codigolinea);
                        cmdTransaccionFactory.Parameters.Add(p_codigousuario);
                        cmdTransaccionFactory.Parameters.Add(p_montominimo);
                        cmdTransaccionFactory.Parameters.Add(p_montomaximo);
                        cmdTransaccionFactory.Parameters.Add(p_nivel);
                        cmdTransaccionFactory.Parameters.Add(p_aprueba);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_APROB_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadoraData", "ModificarAprobador", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobador(Aprobador entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Aprobador> lstAprobador = new List<Aprobador>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select a.idaprobador as id, l.cod_linea_credito  || ' - ' ||  l.nombre as linea, u.codusuario || ' - ' || u.nombre as usuario, o.nombre as oficina, a.monto_minimo as minimo, a.monto_maximo as maximo, a.nivel as nivel, a.aprueba as aprueba, o.nombre from aprobadores a, lineascredito l, usuarios u, oficina o where a.cod_linea_credito = l.cod_linea_credito and a.cod_usuario = u.codusuario and u.cod_oficina=o.cod_oficina order by id";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Aprobador();
                            //Asociar todos los valores a la entidad
                            if (resultado["id"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["id"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.UsuarioAprobador = Convert.ToString(resultado["usuario"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.MontoMinimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.MontoMaximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["nivel"] != DBNull.Value) entidad.Nivel = Convert.ToInt16(resultado["nivel"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.Aprueba = Convert.ToInt16(resultado["aprueba"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstAprobador.Add(entidad);
                        }

                        return lstAprobador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "ListarAprobador", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActaRestructurados(Aprobador entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Aprobador> lstAprobador = new List<Aprobador>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select a.idaprobador as id, l.cod_linea_credito, l.cod_linea_credito  || ' - ' ||  l.nombre as linea, u.codusuario , u.nombre as usuario, o.nombre as oficina, a.monto_minimo as minimo, a.monto_maximo as maximo, a.nivel as nivel, a.aprueba as aprueba, o.nombre from aprobadores a, lineascredito l, usuarios u, oficina o where a.cod_linea_credito = l.cod_linea_credito and a.cod_usuario = u.codusuario and u.cod_oficina=o.cod_oficina  and a.COD_LINEA_CREDITO=310 " + "and CODUSUARIO=" + entidad.codusuaAprobador;
                        string sql2 = " order by id";
                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Aprobador();
                            //Asociar todos los valores a la entidad
                            if (resultado["id"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["id"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.UsuarioAprobador = Convert.ToString(resultado["usuario"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.codusuaAprobador = Convert.ToInt32(resultado["codusuario"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.MontoMinimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.MontoMaximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["nivel"] != DBNull.Value) entidad.Nivel = Convert.ToInt16(resultado["nivel"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.Aprueba = Convert.ToInt16(resultado["aprueba"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstAprobador.Add(entidad);
                        }

                        return lstAprobador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "ListarAprobadorActa", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Aprobadores
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Aprobador> ListarAprobadorActa(Aprobador entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Aprobador> lstAprobador = new List<Aprobador>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select a.idaprobador as id, l.cod_linea_credito, l.cod_linea_credito  || ' - ' ||  l.nombre as linea, u.codusuario , u.nombre as usuario, o.nombre as oficina, a.monto_minimo as minimo, a.monto_maximo as maximo, a.nivel as nivel, a.aprueba as aprueba, o.nombre from aprobadores a, lineascredito l, usuarios u, oficina o where a.cod_linea_credito = l.cod_linea_credito and a.cod_usuario = u.codusuario and u.cod_oficina=o.cod_oficina " + "and CODUSUARIO=" + entidad.codusuaAprobador;
                        string sql2 = " order by id";
                        string sql = sql1 + sql2;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Aprobador();
                            //Asociar todos los valores a la entidad
                            if (resultado["id"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["id"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.UsuarioAprobador = Convert.ToString(resultado["usuario"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.codusuaAprobador = Convert.ToInt32(resultado["codusuario"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.MontoMinimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.MontoMaximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["nivel"] != DBNull.Value) entidad.Nivel = Convert.ToInt16(resultado["nivel"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.Aprueba = Convert.ToInt16(resultado["aprueba"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            lstAprobador.Add(entidad);
                        }

                        return lstAprobador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "ListarAprobadorActa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un Aprobador de datos
        /// </summary>
        /// <param name="pId">identificador del aprobador</param>
        public void EliminarAprobador(Int32 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //if (pUsuario.programaGeneraLog)
                        //    pEntidad = ConsultarOficina(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_idaprobador = cmdTransaccionFactory.CreateParameter();
                        p_idaprobador.ParameterName = "p_idaprobador";
                        p_idaprobador.Value = pId;
                        //p_idaprobador.DbType = DbType.Int32;
                        //p_idaprobador.Size = 8;
                        p_idaprobador.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idaprobador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APR_APROB_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "EliminarOficina", ex);
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la informacion de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del Aprobador</returns>
        public Aprobador ConsultarAprobador(Aprobador pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Aprobador entidad = new Aprobador();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select l.cod_linea_credito as linea, u.codusuario as usuario, o.cod_oficina as oficina, a.monto_minimo as minimo, a.monto_maximo as maximo, a.nivel as nivel, a.aprueba as aprueba from aprobadores a, lineascredito l, usuarios u, oficina o where a.cod_linea_credito = l.cod_linea_credito and a.cod_usuario = u.codusuario and u.cod_oficina=o.cod_oficina and a.idaprobador=" + pEntidad.Id;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.UsuarioAprobador = Convert.ToString(resultado["usuario"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.MontoMinimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.MontoMaximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["nivel"] != DBNull.Value) entidad.Nivel = Convert.ToInt16(resultado["nivel"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.Aprueba = Convert.ToInt16(resultado["aprueba"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "ConsultarAprobador", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion de un Aprobador
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del Aprobador</returns>
        public Aprobador ConsultarAprobadorActa(Aprobador pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Aprobador entidad = new Aprobador();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select l.cod_linea_credito as linea, u.codusuario as usuario, o.cod_oficina as oficina, a.monto_minimo as minimo, a.monto_maximo as maximo, a.nivel as nivel, a.aprueba as aprueba from aprobadores a, lineascredito l, usuarios u, oficina o where a.cod_linea_credito = l.cod_linea_credito and a.cod_usuario = u.codusuario and u.cod_oficina=o.cod_oficina and a.cod_usuario=" + pEntidad.Id;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito =  "," + Convert.ToString(resultado["linea"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.UsuarioAprobador =   Convert.ToString(resultado["usuario"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.MontoMinimo =  Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.MontoMaximo =  Convert.ToInt64(resultado["maximo"]);
                            if (resultado["nivel"] != DBNull.Value) entidad.Nivel = Convert.ToInt16(resultado["nivel"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.Aprueba =  Convert.ToInt16(resultado["aprueba"]);
                        }

                       
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "ConsultarAprobador", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta el ultimo Id_Aprobador en la base de datos
        /// </summary>
        /// <param name="pEntidad">Idaprobador</param>
        /// <returns>Entidad modificada</returns>
        public object UltimoIdAprobador(Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select idAprobador from aprobadores order by idaprobador desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        return cmdTransaccionFactory.ExecuteScalar();                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobadorData", "UltimoIdAprobador", ex);
                        return null;
                    }
                }
            }
        }
    }
}
