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
    /// Objeto de acceso a datos para la tabla Documento
    /// </summary>
    public class DocumentoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Documento
        /// </summary>
        public DocumentoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Documento de la base de datos
        /// </summary>
        /// <param name="pDocumento">Entidad Documento</param>
        /// <returns>Entidad Documento creada</returns>
        public Documento CrearDocumentoGenerado(Documento pDocumento, Int64 numero_radicacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = numero_radicacion;

                        DbParameter p_tipo_documento = cmdTransaccionFactory.CreateParameter();
                        p_tipo_documento.ParameterName = "p_tipo_documento";
                        p_tipo_documento.Value = pDocumento.tipo_documento;

                        DbParameter p_referencia = cmdTransaccionFactory.CreateParameter();
                        p_referencia.ParameterName = "p_referencia";

                        if (!string.IsNullOrWhiteSpace(pDocumento.referencia))
                        {
                            p_referencia.Value = pDocumento.referencia;
                        }
                        else
                        {
                            p_referencia.Value = DBNull.Value;
                        }

                        DbParameter p_ruta = cmdTransaccionFactory.CreateParameter();
                        p_ruta.ParameterName = "p_ruta";

                        if (!string.IsNullOrWhiteSpace(pDocumento.referencia))
                        {
                            p_ruta.Value = pDocumento.ruta;
                        }
                        else
                        {
                            p_ruta.Value =DBNull.Value;
                        }

                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_documento);
                        cmdTransaccionFactory.Parameters.Add(p_referencia);
                        cmdTransaccionFactory.Parameters.Add(p_ruta);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCGARANTIA_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pDocumento, "Documento", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return pDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "CrearDocumentoGenerado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Documento de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Documento modificada</returns>
        public Documento ModificarDocumento(Documento pDocumento, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = param + "COD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pDocumento.cod_linea_credito;

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = param + "TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pDocumento.tipo_documento;

                        DbParameter pDESCRIPCION_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION_DOCUMENTO.ParameterName = param + "DESCRIPCION_DOCUMENTO";
                        pDESCRIPCION_DOCUMENTO.Value = pDocumento.descripcion_documento;

                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION_DOCUMENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_GeneracionDocumentos_Documento_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDocumento, "Documento", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ModificarDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Documento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Documento</param>
        public void EliminarDocumento(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Documento pDocumento = new Documento();

                        if (pUsuario.programaGeneraLog)
                            pDocumento = ConsultarDocumento(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = param + "COD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPINN_GENERACIONDOCUMENTOS_DOCUMENTO_D";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDocumento, "Documento", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "InsertarDocumento", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Documento de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Documento</param>
        /// <returns>Entidad Documento consultado</returns>
        public Documento ConsultarDocumento(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Documento entidad = new Documento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_linea_credio, t.tipo_documento, t.descripcion, aplica_codeudor from docrequeridoslinea d, tiposdocumento t where t.tipo_documento=d.tipo_documento and cod_linea_credio=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] == DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] == DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] == DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("DocumentoData", "ConsultarDocumento", ex);
                        return null;
                    }
                }
            }
        }        


        public Documento ConsultarDocumentos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Documento entidad = new Documento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select credito.numero_radicacion,d.cod_linea_credito, t.tipo_documento, t.descripcion, Decode(requerido, 1,'Si', 0, 'No') as requerido, d.plantilla as ruta from tiposdocumento t, docgarantialinea d inner join credito on credito.cod_linea_credito=d.cod_linea_credito  where  t.tipo='G' and t.tipo_documento=d.tipo_documento and credito.numero_radicacion=" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("DocumentoData", "ConsultarDocumento", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Documento dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoAGenerar(Documento pDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Documento> lstDocumento = new List<Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_linea_credito, t.tipo_documento, t.descripcion, Decode(requerido, 1,'Si', 0, 'No') as requerido, d.plantilla as ruta from tiposdocumento t, docgarantialinea d where  t.tipo='G' and t.tipo_documento=d.tipo_documento and cod_linea_credito = '" + Convert.ToString(pDocumento.cod_linea_credito) + "' ";
                            
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Documento entidad = new Documento();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToString(resultado["REQUERIDO"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            lstDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ListarDocumentoAGenerar", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Documento dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacion(Documento pDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Documento> lstDocumento = new List<Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_linea_credito, t.tipo_documento, t.descripcion, Decode(requerido, 1,'Si', 0, 'No') as requerido, d.plantilla as ruta from tiposdocumento t, docgarantialinea d where  t.tipo='D' and t.tipo_documento=d.tipo_documento and cod_linea_credito=" + Convert.ToString(pDocumento.cod_linea_credito);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Documento entidad = new Documento();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToString(resultado["REQUERIDO"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            lstDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ListarCartaAprobacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Documento dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarDocumentoGenerado(Documento pDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Documento> lstDocumento = new List<Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select d.iddocumento, t.tipo_documento, t.descripcion, d.referencia, d.ruta_pdf as ruta  from documentosgarantia d, tiposdocumento t where  t.tipo='G' and t.tipo_documento=d.tipo_documento and numero_radicacion=" + pDocumento.numero_radicacion;
                              
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Documento entidad = new Documento();

                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            lstDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ListarDocumentoGenerado", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Documento dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public Documento ConsultarDocumentoAprobacion(Int64 documento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Documento entidad = new Documento();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select d.iddocumento, t.tipo_documento, t.descripcion, d.referencia, d.ruta_pdf as ruta  from documentosgarantia d, tiposdocumento t where  t.tipo='D' and t.tipo_documento=d.tipo_documento and numero_radicacion=" + documento;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                         //   Documento entidad = new Documento();

                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ConsultarDocumentoAprobacion", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Documento dados unos filtros
        /// </summary>
        /// <param name="pDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Documento obtenidos</returns>
        public List<Documento> ListarCartaAprobacionGenerado(Documento pDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Documento> lstDocumento = new List<Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select d.iddocumento, t.tipo_documento, t.descripcion, d.referencia, d.ruta_pdf as ruta  from documentosgarantia d, tiposdocumento t where t.tipo='D' and  t.tipo_documento=d.tipo_documento and numero_radicacion=" + pDocumento.numero_radicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Documento entidad = new Documento();

                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_documento = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);

                            lstDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ListarDocumentoGenerado", ex);
                        return null;
                    }
                }
            }
        }
        public String Listarconsecutivo(string tipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Documento> lstDocumento = new List<Documento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "select Max(to_number(referencia))+1 as iddocumento from DOCUMENTOSGARANTIA where tipo_documento = " + tipo;
                        else
                            sql = "select Max(referencia)+1 as iddocumento from DOCUMENTOSGARANTIA where tipo_documento = " + tipo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string resultado1="";
                        if (resultado.Read())
                        {
                            Documento entidad = new Documento();
                            if (resultado["iddocumento"].ToString().Trim() == "")
                                entidad.numero_consecutivo = "0";
                            else
                                entidad.numero_consecutivo = Convert.ToString(resultado["iddocumento"]);
                            resultado1 = entidad.numero_consecutivo;
                        }
                        else
                        {                            
                            resultado1 = "1";
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentoData", "ListarDocumentoGenerado", ex);
                        return null;
                    }
                }
            }

        }
    }
}