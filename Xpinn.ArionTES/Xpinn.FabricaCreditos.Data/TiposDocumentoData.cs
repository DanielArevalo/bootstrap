using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TiposDocumento
    /// </summary>
    public class TiposDocumentoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TiposDocumento
        /// </summary>
        public TiposDocumentoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocumento CrearTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.tipo_documento;
                        pTIPO_DOCUMENTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pTiposDocumento.descripcion;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_TIPO";
                        if (pTiposDocumento.tipo == null || pTiposDocumento.tipo == "")
                            pTIPO.Value = DBNull.Value;
                        else
                            pTIPO.Value = pTiposDocumento.tipo;

                        DbParameter pESORDEN = cmdTransaccionFactory.CreateParameter();
                        pESORDEN.ParameterName = "p_ES_ORDEN";
                        pESORDEN.Value = pTiposDocumento.es_orden;


                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pESORDEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_TPDOC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTiposDocumento, "TiposDocumento", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTiposDocumento.tipo_documento = Convert.ToInt64(pTIPO_DOCUMENTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pTiposDocumento.Textos != null)
                        {
                            ActualizarImagen(pTiposDocumento, pUsuario);
                        }
                        return pTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "CrearTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TiposDocumento modificada</returns>
        public TiposDocumento ModificarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.tipo_documento;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pTiposDocumento.descripcion;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_TIPO";
                        pTIPO.Value = pTiposDocumento.tipo;

                        DbParameter pESORDEN = cmdTransaccionFactory.CreateParameter();
                        pESORDEN.ParameterName = "p_ES_ORDEN";
                        pESORDEN.Value = pTiposDocumento.es_orden;


                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pESORDEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_TPDOC_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pTiposDocumento.Textos != null)
                        {
                            ActualizarImagen(pTiposDocumento, pUsuario);
                        }
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTiposDocumento, "TiposDocumento", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ModificarTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        public void EliminarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TiposDocumento pTiposDocumento = new TiposDocumento();

                        if (pUsuario.programaGeneraLog)
                            pTiposDocumento = ConsultarTiposDocumento(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_TPDOC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTiposDocumento, "TiposDocumento", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "EliminarTiposDocumento", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TiposDocumento</param>
        /// <returns>Entidad TiposDocumento consultado</returns>
        public TiposDocumento ConsultarTiposDocumento(Int64 pId, Usuario pUsuario, string tipoDoc = null)
        {
            DbDataReader resultado;
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (tipoDoc == null)
                            sql = "SELECT * FROM  TIPOSDOCUMENTO WHERE TIPO_DOCUMENTO = " + pId.ToString();
                        else
                            sql = "SELECT * FROM  TIPOSDOCUMENTO WHERE Tipo = '" + tipoDoc + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["Texto_Array"] != DBNull.Value) entidad.Textos = (byte[])resultado["Texto_Array"];
                            if (resultado["ES_ORDEN"] != DBNull.Value) entidad.es_orden = Convert.ToInt32(resultado["ES_ORDEN"]);
                        }
                        else
                        {
                            //  throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }

        public List<TiposDocumento> ConsultarTiposDocumento(String pTipo, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TiposDocumento> lsEntidades = new List<TiposDocumento>();
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT TIPO_DOCUMENTO , DESCRIPCION , TIPO, TEXTO, TEXTO_ARRAY FROM  TIPOSDOCUMENTO WHERE TIPO = '" + pTipo + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new TiposDocumento();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["TEXTO_ARRAY"] != DBNull.Value) entidad.Textos = (byte[])resultado["TEXTO_ARRAY"];
                            lsEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lsEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla General de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla General</param>
        /// <returns>Entidad TiposDocumento consultado</returns>
        public TiposDocumento ConsultarParametroTipoDocumento(Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM   general WHERE codigo=11";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["valor"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarParametroTipoDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TiposDocumento</param>
        /// <returns>Entidad TiposDocumento consultado</returns>
        public TiposDocumento ConsultarMaxTiposDocumento(Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(tipo_documento) as tipo_documento  FROM  TIPOSDOCUMENTO WHERE TIPO_DOCUMENTO not in(999) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarMaxTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }

        //Consulto Los tipos del documento Creados en la tabla Tipo_DelDocumento 
        public List<TipoDocumento> ConsultarTipoDoc(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TipoDocumento> lstTipo = new List<TipoDocumento>();
            

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SElect * from Tipo_DelDocumento";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoDocumento entidad = new TipoDocumento();
                            if (resultado["IDTIPO"] != DBNull.Value) entidad.idTipo = Convert.ToString(resultado["IDTIPO"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.Detalle = Convert.ToString(resultado["DETALLE"]);
                            lstTipo.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarTipoDoc", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TiposDocumento dados unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocumento> ListarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TiposDocumento> lstTiposDocumento = new List<TiposDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCUMENTO " + ObtenerFiltro(pTiposDocumento) + " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TiposDocumento entidad = new TiposDocumento();

                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo == "G" || entidad.tipo == "1")
                                entidad.nomtipo = "Garantía";
                            if (entidad.tipo == "R" || entidad.tipo == "2")
                                entidad.nomtipo = "Requerido";
                            if (entidad.tipo == "A" || entidad.tipo == "3")
                                entidad.nomtipo = "Anexo";
                            //Modificación uso de tipo C para Certificados de Cartera
                            if (entidad.tipo == "C" || entidad.tipo == "4")
                                entidad.nomtipo = "Certificado";
                            //if(entidad.tipo != "C")//Se eliminan los c (CDAT) ya que no pertenece a fabrica de creditos
                            lstTiposDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ListarTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TiposDocumento</param>
        /// <returns>Entidad TiposDocumento consultado</returns>
        public TiposDocumento ConsultarTiposDocumentoCobranzas(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCCOBRANZAS WHERE TIPO_DOCUMENTO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["TEXTO_ARRAY"] != DBNull.Value) entidad.Textos = (byte[])resultado["TEXTO_ARRAY"];
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarTiposDocumentoCobranzas", ex);
                        return null;
                    }
                }
            }
        }

        public TiposDocumento ConsultarDocumentoOrden(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocumento entidad = new TiposDocumento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCUMENTO WHERE ES_ORDEN = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["TEXTO_ARRAY"] != DBNull.Value) entidad.Textos = (byte[])resultado["TEXTO_ARRAY"];
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarDocumentoOrden", ex);
                        return null;
                    }
                }
            }
        }

        //Guarda el Html en un byte array
        public void ActualizarImagen(TiposDocumento ptiposdocueDocumento, Usuario pUsuario)
        {
            Xpinn.Imagenes.Data.ImagenesORAData DAImagenes = new Imagenes.Data.ImagenesORAData();
            DAImagenes.ActualizarImagen(ptiposdocueDocumento.tipo_documento, ptiposdocueDocumento.Textos, pUsuario, "TIPOSDOCUMENTO", "TIPO_DOCUMENTO");
        }
    }
}