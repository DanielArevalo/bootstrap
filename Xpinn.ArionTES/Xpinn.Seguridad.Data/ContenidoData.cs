using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Seguridad.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.Seguridad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla OPCIONES
    /// </summary>
    public class ContenidoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla OPCIONES
        /// </summary>
        public ContenidoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Contenido de la base de datos
        /// </summary>
        /// <param name="pContenido">Entidad Opcion</param>
        /// <returns>Entidad Opcion creada</returns>
        public Contenido CrearContenido(Contenido pContenido, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_opcion = cmdTransaccionFactory.CreateParameter();
                        p_cod_opcion.ParameterName = "p_cod_opcion";
                        p_cod_opcion.Value = pContenido.cod_opcion;
                        p_cod_opcion.Direction = ParameterDirection.InputOutput;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pContenido.nombre;

                        DbParameter p_html = cmdTransaccionFactory.CreateParameter();
                        p_html.ParameterName = "p_html";
                        p_html.Value = "";

                        DbParameter p_mostrarOficina = cmdTransaccionFactory.CreateParameter();
                        p_mostrarOficina.ParameterName = "p_mostrarOficina";
                        p_mostrarOficina.Value = pContenido.mostrarOficina;
                        
                        cmdTransaccionFactory.Parameters.Add(p_cod_opcion);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_html);
                        cmdTransaccionFactory.Parameters.Add(p_mostrarOficina);                        


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_CONTENIDO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pContenido, "CONTENIDO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pContenido.cod_opcion = Convert.ToInt64(p_cod_opcion.Value);
                        if (pContenido.cod_opcion != 0 && !string.IsNullOrEmpty(pContenido.html))
                        {
                            ImagenesORAData DAImagenes = new ImagenesORAData();                            
                                DAImagenes.guardarContenidoOficina(pContenido.cod_opcion, pContenido.html, pUsuario);
                        }
                        return pContenido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "CrearContenido", ex);
                        return null;
                    }
                }
            }
        }
        

        /// <summary>
        /// Modifica un registro en la tabla Contenido de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Opcion modificada</returns>
        public Contenido ModificarContenido(Contenido pContenido, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_opcion = cmdTransaccionFactory.CreateParameter();
                        p_cod_opcion.ParameterName = "p_cod_opcion";
                        p_cod_opcion.Value = pContenido.cod_opcion;
                        p_cod_opcion.Direction = ParameterDirection.InputOutput;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pContenido.nombre;

                        DbParameter p_html = cmdTransaccionFactory.CreateParameter();
                        p_html.ParameterName = "p_html";
                        p_html.Value = "";

                        DbParameter p_mostrarOficina = cmdTransaccionFactory.CreateParameter();
                        p_mostrarOficina.ParameterName = "p_mostrarOficina";
                        p_mostrarOficina.Value = pContenido.mostrarOficina;

                        cmdTransaccionFactory.Parameters.Add(p_cod_opcion);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_html);
                        cmdTransaccionFactory.Parameters.Add(p_mostrarOficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_CONTENIDO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pContenido.cod_opcion != 0 && !string.IsNullOrEmpty(pContenido.html))
                        {
                            ImagenesORAData DAImagenes = new ImagenesORAData();
                            DAImagenes.guardarContenidoOficina(pContenido.cod_opcion, pContenido.html, pUsuario);
                        }

                        return pContenido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "ModificarCONTENIDO", ex);
                        return null;
                    }
                }
            }
        }
        

        /// <summary>
        /// Elimina un registro en la tabla CONTENIDO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de CONTENIDO</param>
        public void EliminarContenido(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Contenido pContenido = new Contenido();

                        if (pUsuario.programaGeneraLog)
                            pContenido = ConsultarContenido(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_cod_opcion = cmdTransaccionFactory.CreateParameter();
                        p_cod_opcion.ParameterName = "p_cod_opcion";
                        p_cod_opcion.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_cod_opcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_CONTENIDO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pContenido, "CONTENIDO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OpcionData", "InsertarCONTENIDO", ex);
                    }
                }
            }
        }    

        /// <summary>
        /// Obtiene un registro en la tabla CONTENIDO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla CONTENIDO</param>
        /// <returns>Entidad Opcion consultado</returns>
        public Contenido ConsultarContenido(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Contenido entidad = new Contenido();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONTENIDO WHERE cod_opcion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["HTML"] != DBNull.Value) entidad.html = Convert.ToString(resultado["HTML"]);
                            if (resultado["MOSTRAR_OFICINA"] != DBNull.Value) entidad.mostrarOficina = Convert.ToInt64(resultado["MOSTRAR_OFICINA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ContenidoData", "ConsultarContenido", ex);
                        return null;
                    }
                }
            }
        }
       
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CONTENIDO dados unos filtros
        /// </summary>
        /// <param name="pCONTENIDO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Opcion obtenidos</returns>
        public List<Contenido> ListarContenido(Contenido pContenido, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Contenido> lstContenido = new List<Contenido>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT COD_OPCION,NOMBRE,MOSTRAR_OFICINA FROM CONTENIDO order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Contenido entidad = new Contenido();

                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);                            
                            if (resultado["MOSTRAR_OFICINA"] != DBNull.Value) entidad.mostrarOficina = Convert.ToInt64(resultado["MOSTRAR_OFICINA"]);

                            lstContenido.Add(entidad);
                        }

                        return lstContenido;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContenidoData", "ListarContenido", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una entidad de contenido
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Contenido ObtenerContenido(long pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Contenido content = new Contenido();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CONTENIDO WHERE MOSTRAR_OFICINA = 1 AND COD_OPCION="+pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                          
                            if (resultado["COD_OPCION"] != DBNull.Value) content.cod_opcion = Convert.ToInt64(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) content.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["HTML"] != DBNull.Value) content.html = Convert.ToString(resultado["HTML"]);
                            if (resultado["MOSTRAR_OFICINA"] != DBNull.Value) content.mostrarOficina = Convert.ToInt64(resultado["MOSTRAR_OFICINA"]);
                        }

                        return content;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ContenidoData", "ObtenerContenido", ex);
                        return null;
                    }
                }
            }
        }

    }
}