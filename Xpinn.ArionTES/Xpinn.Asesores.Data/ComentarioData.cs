using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Reflection;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Comentario
    /// </summary>
    public class ComentarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Comentario
        /// </summary>
        public ComentarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Comentario de la base de datos
        /// </summary>
        /// <param name="pComentario">Entidad Comentario</param>
        /// <returns>Entidad Comentario creada</returns>
        public Comentario Crear(Comentario pComentario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidComentario = cmdTransaccionFactory.CreateParameter();
                        pidComentario.Direction = ParameterDirection.Output;
                        pidComentario.ParameterName = "p_idComentario";
                        pidComentario.DbType = DbType.Int64;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pComentario.fecha;

                        DbParameter phora = cmdTransaccionFactory.CreateParameter();
                        phora.ParameterName = "p_hora";
                        phora.Value = pComentario.hora;

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pComentario.descripcion;

                        DbParameter pidPersona = cmdTransaccionFactory.CreateParameter();
                        pidPersona.ParameterName = "p_idPersona";
                        pidPersona.Value = pComentario.idPersona;

                        DbParameter pNumCredito = cmdTransaccionFactory.CreateParameter();
                        pNumCredito.ParameterName = "p_numeroCredito";
                        pNumCredito.Value = pComentario.numeroCredito;

                        DbParameter p_verAsociado = cmdTransaccionFactory.CreateParameter();
                        p_verAsociado.ParameterName = "p_verAsociado";
                        p_verAsociado.Value = Convert.ToInt32(pComentario.puedeVerAsociado);  // true - 1 => Si puede, false - 0 => No puede

                        cmdTransaccionFactory.Parameters.Add(pidComentario);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(phora);
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
                        cmdTransaccionFactory.Parameters.Add(pidPersona);
                        cmdTransaccionFactory.Parameters.Add(pNumCredito);
                        cmdTransaccionFactory.Parameters.Add(p_verAsociado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_COMENTARIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pComentario, "Comentario",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pComentario.idComentario = Convert.ToInt64(pidComentario.Value);
                        return pComentario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "CrearComentario", ex);
                        return null;
                    }
                }
            }
        }

        public void ModificarComentario(Comentario comentario, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidComentario = cmdTransaccionFactory.CreateParameter();
                        pidComentario.ParameterName = "p_idComentario";
                        pidComentario.Value = comentario.idComentario;
                        pidComentario.Direction = ParameterDirection.Input;
                        pidComentario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidComentario);

                        DbParameter p_verAsociado = cmdTransaccionFactory.CreateParameter();
                        p_verAsociado.ParameterName = "p_verAsociado";
                        p_verAsociado.Value = Convert.ToInt32(comentario.puedeVerAsociado); // true - 1 => Si puede, false - 0 => No puede
                        p_verAsociado.Direction = ParameterDirection.Input;
                        p_verAsociado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_verAsociado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_COMENTARIO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "ModificarComentario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Comentario dados unos filtros
        /// </summary>
        /// <param name="pComentario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Comentario obtenidos</returns>
        public List<Comentario> Listar(Producto pProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Comentario> lstComentario = new List<Comentario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT * FROM ASCOMENTARIO WHERE idPersona = " + pProducto.Persona.IdPersona + " ORDER BY FECHA DESC";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Comentario entidad = new Comentario();
                            if (resultado["idComentario"] != DBNull.Value)  entidad.idComentario    = Convert.ToInt64(resultado["idComentario"]);
                            if (resultado["fecha"] != DBNull.Value)         entidad.fecha           = resultado["fecha"].ToString();
                            if (resultado["hora"] != DBNull.Value)          entidad.hora            = Convert.ToString(resultado["hora"]);
                            if (resultado["descripcion"] != DBNull.Value)   entidad.descripcion     = Convert.ToString(resultado["descripcion"]);
                            if (resultado["idPersona"] != DBNull.Value)     entidad.idPersona       = Convert.ToInt64(resultado["idPersona"]);
                            if (resultado["numeroCredito"] != DBNull.Value) entidad.numeroCredito   = Convert.ToInt64(resultado["numeroCredito"]);
                            if (resultado["VER_ASOCIADO"] != DBNull.Value)  entidad.puedeVerAsociado = Convert.ToBoolean(resultado["VER_ASOCIADO"]);
                            lstComentario.Add(entidad);
                        }

                        return lstComentario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "ListarComentario", ex);
                        return null;
                    }
                }
            }
        }


        public List<Comentario> Listarcomentario(Producto pProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Comentario> lstComentario = new List<Comentario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT * FROM ASCOMENTARIO WHERE idPersona = " + pProducto.IdPersona;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Comentario entidad = new Comentario();
                            if (resultado["idComentario"] != DBNull.Value) entidad.idComentario = Convert.ToInt64(resultado["idComentario"]);
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = resultado["fecha"].ToString();
                            if (resultado["hora"] != DBNull.Value) entidad.hora = Convert.ToString(resultado["hora"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["idPersona"] != DBNull.Value) entidad.idPersona = Convert.ToInt64(resultado["idPersona"]);
                            if (resultado["numeroCredito"] != DBNull.Value) entidad.numeroCredito = Convert.ToInt64(resultado["numeroCredito"]);
                            lstComentario.Add(entidad);
                        }

                        return lstComentario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "ListarComentario", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarComentario(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Comentario pComentario = new Comentario();
                     

                        DbParameter pidComentario = cmdTransaccionFactory.CreateParameter();
                        pidComentario.ParameterName = "p_idComentario";
                        pidComentario.Value = pId;
                        pidComentario.Direction = ParameterDirection.Input;
                        pidComentario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidComentario);

                       

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_COMENTARIO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "EliminarComentario", ex);
                    }
                }
            }
        }

        public Comentario ConsultarComentario(Int64 codigo, Usuario pUsuario)
        {
            DbDataReader resultado;
            Comentario entidad = new Comentario();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from ascomentario where idpersona=" + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["idpersona"] != DBNull.Value) entidad.idPersona = Convert.ToInt64(resultado["idpersona"]);                            
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComentarioData", "ConsultarComentario", ex);
                        return null;
                    }
                }
            }
        }

    }
}