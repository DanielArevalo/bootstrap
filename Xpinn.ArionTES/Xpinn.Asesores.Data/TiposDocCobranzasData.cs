using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;

namespace Xpinn.Asesores.Data
{
    public class TiposDocCobranzasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;
        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TiposDocumento
        /// </summary>
        public TiposDocCobranzasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TiposDocumento de la base de datos
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocCobranzas CrearTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "P_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.tipo_documento;
                        pTIPO_DOCUMENTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "P_DESCRIPCION";
                        pDESCRIPCION.Value = pTiposDocumento.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_TIPOSDOC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pTiposDocumento.tipo_documento = Convert.ToInt64(pTIPO_DOCUMENTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception e)
                    {
                        //ModificarTiposDocumento(pTiposDocumento, pUsuario);
                        return null;
                    }
                }
            }

            if (pTiposDocumento.Textos != null)
            {
                ActualizarImagen(pTiposDocumento, pUsuario);
            }
            return pTiposDocumento;

        }

        public TiposDocCobranzas CrearFormatoDocumentoCorreo(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_doc = cmdTransaccionFactory.CreateParameter();
                        p_cod_doc.ParameterName = "p_cod_doc";
                        p_cod_doc.Value = 0;
                        p_cod_doc.Direction = ParameterDirection.Input;

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_tipo";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.tipo;
                        pTIPO_DOCUMENTO.Direction = ParameterDirection.Input;

                        DbParameter P_TEXTO = cmdTransaccionFactory.CreateParameter();
                        P_TEXTO.ParameterName = "P_TEXTO";
                        P_TEXTO.Value = pTiposDocumento.texto;
                        P_TEXTO.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTiposDocumento.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_doc);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(P_TEXTO);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_FORMATODOC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "CrearFormatoDocumentoCorreo", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Asesores.Entities.Persona ConsultarCorreoPersona(long pId, Usuario pUsuario, string identificacion = null)
        {
            DbDataReader resultado;
            Xpinn.Asesores.Entities.Persona entidad = new Xpinn.Asesores.Entities.Persona();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Empty;

                        if (pId == 0 && !string.IsNullOrWhiteSpace(identificacion))
                        {
                            sql = "select email from persona where identificacion = '" + identificacion + "'";
                        }
                        else
                        {
                            sql = "select email from persona where cod_persona = " + pId;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["email"] != DBNull.Value) entidad.Email = Convert.ToString(resultado["email"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }


        public Empresa ConsultarCorreoEmpresa(long pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Empresa entidad = new Empresa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro = @" where cod_empresa = " + pId;
                        string sql = "select E_MAIL, CLAVECORREO from empresa ";

                        if (pId != 0)
                        {
                            sql += filtro;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["CLAVECORREO"] != DBNull.Value) entidad.clave_e_mail = Convert.ToString(resultado["CLAVECORREO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public TiposDocCobranzas ModificarFormatoDocumentoCorreo(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_documento = cmdTransaccionFactory.CreateParameter();
                        p_cod_documento.ParameterName = "p_cod_documento";
                        p_cod_documento.Value = pTiposDocumento.id;
                        p_cod_documento.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTiposDocumento.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pTiposDocumento.tipo;
                        p_tipo.Direction = ParameterDirection.Input;

                        DbParameter p_texto = cmdTransaccionFactory.CreateParameter();
                        p_texto.ParameterName = "p_texto";
                        p_texto.Value = pTiposDocumento.texto;
                        p_texto.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_cod_documento);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_texto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_FORMATODOC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ModificarFormatoDocumentoCorreo", ex);
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
        public TiposDocCobranzas ModificarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "P_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.tipo_documento;
                        pTIPO_DOCUMENTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "P_DESCRIPCION";
                        pDESCRIPCION.Value = pTiposDocumento.descripcion;


                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_TIPOSDOC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ModificarTiposDocumento", ex);
                        return null;
                    }
                }
            }

            if (pTiposDocumento.Textos != null)
            {
                ActualizarImagen(pTiposDocumento, pUsuario);
            }
            return pTiposDocumento;

        }

        public TiposDocCobranzas ConsultarFormatoDocumentoCorreo(long pId, Usuario pUsuario, bool verificarSoloSiExiste = false)
        {
            DbDataReader resultado;
            TiposDocCobranzas entidad = new TiposDocCobranzas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string datoDevolver = "TEXTO";
                        if (verificarSoloSiExiste)
                        {
                            datoDevolver = "COD_DOCUMENTO";
                        }

                        string sql = "SELECT DESCRIPCION, " + datoDevolver + " FROM FORMATODOCUMENTOSCORREO where tipo=" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (!verificarSoloSiExiste && resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (verificarSoloSiExiste && resultado["COD_DOCUMENTO"] != DBNull.Value) entidad.id = Convert.ToInt64(resultado["COD_DOCUMENTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
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
                        TiposDocCobranzas pTiposDocumento = new TiposDocCobranzas();

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "P_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_TIPOSDOC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

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
        public TiposDocCobranzas ConsultarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocCobranzas entidad = new TiposDocCobranzas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCCOBRANZAS where tipo_documento=" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTO_Array"] != DBNull.Value) entidad.Textos = (byte[])resultado["TEXTO_Array"];
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

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TiposDocumento dados unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocCobranzas> ListarTiposDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TiposDocCobranzas> lstTiposDocumento = new List<TiposDocCobranzas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCCOBRANZAS ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TiposDocCobranzas entidad = new TiposDocCobranzas();

                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);

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

        public List<TiposDocCobranzas> ListarTiposDocumentoCobranzas(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<TiposDocCobranzas> lstTiposDocumento = new List<TiposDocCobranzas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCCOBRANZAS " + ObtenerFiltro(pTiposDocumento) + " ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TiposDocCobranzas entidad = new TiposDocCobranzas();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            // if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            lstTiposDocumento.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTiposDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ConsultarTiposDocumentoCobranzas", ex);
                        return null;
                    }
                }
            }
        }
        public List<TiposDocCobranzas> GenerarDocumento(TiposDocCobranzas pTiposDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                DbDataReader resultado = default(DbDataReader);
                List<TiposDocCobranzas> lstdocumentosgenerados = new List<TiposDocCobranzas>();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "P_NUMERO_RADICACION";
                        pTIPO_DOCUMENTO.Value = pTiposDocumento.numero_radicacion;
                        pTIPO_DOCUMENTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "P_IDENTIFICACION";
                        pDESCRIPCION.Value = pTiposDocumento.identificacion;

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "P_FECHA";
                        PFECHA.Value = pTiposDocumento.fecha;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(PFECHA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DOCUMENTOS_VARS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        lstdocumentosgenerados.Add(pTiposDocumento);

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();

                        string sql = "Select * From temp_gendoc WHERE NUMERO_RADICACION=" + pTiposDocumento.numero_radicacion + " ORDER BY 1";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TiposDocCobranzas entidad = new TiposDocCobranzas();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt64(resultado["ID"].ToString());
                            if (resultado["CAMPO"] != DBNull.Value) entidad.campo = Convert.ToString(resultado["CAMPO"].ToString());
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"].ToString());

                            lstdocumentosgenerados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstdocumentosgenerados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TiposDocumentoData", "ModificarTiposDocumento", ex);
                        return null;
                    }
                }
            }
        }



        public TiposDocCobranzas ConsultarTipoDocumento(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocCobranzas entidad = new TiposDocCobranzas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOSDOCCOBRANZAS where tipo_documento=" + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                        }
                        else
                        {
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
        public TiposDocCobranzas ConsultarMaxTipoDocumento(Usuario pUsuario)
        {
            DbDataReader resultado;
            TiposDocCobranzas entidad = new TiposDocCobranzas();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT max(tipo_documento) as tipo_documento  FROM  TIPOSDOCCOBRANZAS WHERE TIPO_DOCUMENTO not in(999)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);

                        }
                        else
                        {
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public void ActualizarImagen(TiposDocCobranzas ptiTiposDocCobranzas, Usuario pUsuario)
        {
            Xpinn.Imagenes.Data.ImagenesORAData DAImagenes = new Imagenes.Data.ImagenesORAData();
            DAImagenes.ActualizarImagen(ptiTiposDocCobranzas.tipo_documento, ptiTiposDocCobranzas.Textos, pUsuario, "TIPOSDOCCOBRANZAS", "Tipo_Documento");
        }


    }
}


