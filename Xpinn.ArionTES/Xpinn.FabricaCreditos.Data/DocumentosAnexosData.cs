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
    /// Objeto de acceso a datos para la tabla DocumentosAnexos
    /// </summary>
    public class DocumentosAnexosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla DocumentosAnexos
        /// </summary>
        public DocumentosAnexosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        
        public DocumentosAnexos CrearDocAnexos(DocumentosAnexos pDocumentosAnexos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_iddocumento = cmdTransaccionFactory.CreateParameter();
                        p_iddocumento.ParameterName = "p_iddocumento";
                        p_iddocumento.Value = pDocumentosAnexos.iddocumento;
                        p_iddocumento.Direction = ParameterDirection.Output;
                        p_iddocumento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_iddocumento);

                        
                        DbParameter p_numerosolicitud = cmdTransaccionFactory.CreateParameter();
                        p_numerosolicitud.ParameterName = "p_numerosolicitud";
                        if (pDocumentosAnexos.numerosolicitud == null || pDocumentosAnexos.numerosolicitud == 0)
                            p_numerosolicitud.Value = DBNull.Value;
                        else
                            p_numerosolicitud.Value = pDocumentosAnexos.numerosolicitud;
                        p_numerosolicitud.Direction = ParameterDirection.Input;
                        p_numerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numerosolicitud);


                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pDocumentosAnexos.numero_radicacion;
                        p_numero_radicacion.Direction = ParameterDirection.Input;
                        p_numero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);

                        DbParameter p_tipo_documento = cmdTransaccionFactory.CreateParameter();
                        p_tipo_documento.ParameterName = "p_tipo_documento";
                        p_tipo_documento.Value = pDocumentosAnexos.tipo_documento;
                        p_tipo_documento.Direction = ParameterDirection.Input;
                        p_tipo_documento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_documento);


                        DbParameter p_fechaanexo = cmdTransaccionFactory.CreateParameter();
                        p_fechaanexo.ParameterName = "p_fechaanexo";

                        if (pDocumentosAnexos.fechaanexo == null)
                            p_fechaanexo.Value = DBNull.Value;
                        else
                            p_fechaanexo.Value = pDocumentosAnexos.fechaanexo;
                        p_fechaanexo.Direction = ParameterDirection.Input;
                        p_fechaanexo.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaanexo);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";

                        if (pDocumentosAnexos.descripcion == null)
                            p_descripcion.Value = DBNull.Value; 
                        else
                             p_descripcion.Value =    pDocumentosAnexos.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);


                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pDocumentosAnexos.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);


                        DbParameter p_fechaentrega = cmdTransaccionFactory.CreateParameter();
                        p_fechaentrega.ParameterName = "p_fechaentrega";
                        if (pDocumentosAnexos.fechaentrega == null)
                            p_fechaentrega.Value = DBNull.Value;
                        else
                            p_fechaentrega.Value = pDocumentosAnexos.fechaentrega;
                        p_fechaentrega.Direction = ParameterDirection.Input;
                        p_fechaentrega.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaentrega);

                        /*
                        DbParameter P_IMAGEN = cmdTransaccionFactory.CreateParameter();
                        P_IMAGEN.ParameterName = "P_IMAGEN";
                        if (pDocumentosAnexos.imagen == null)
                            p_fechaentrega.Value = DBNull.Value;
                        else
                            P_IMAGEN.Value = pDocumentosAnexos.imagen;
                        P_IMAGEN.Direction = ParameterDirection.Input;
                      
                        cmdTransaccionFactory.Parameters.Add(P_IMAGEN);

                        DbParameter P_ESPDF = cmdTransaccionFactory.CreateParameter();
                        P_ESPDF.ParameterName = "P_ESPDF";
                        P_ESPDF.Value = pDocumentosAnexos.espdf;
                        P_ESPDF.Direction = ParameterDirection.Input;
                        P_ESPDF.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESPDF);

                        */


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOC_ANEXOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pDocumentosAnexos.iddocumento = p_iddocumento.Value != DBNull.Value ? Convert.ToInt64(p_iddocumento.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "CrearDocAnexos", ex);
                        return null;
                    }
                }
            }
        }
        
        public DocumentosAnexos ModificarDocAnexos(DocumentosAnexos pDocumentosAnexos,Int64 pdocumento, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_iddocumento = cmdTransaccionFactory.CreateParameter();
                        p_iddocumento.ParameterName = "p_iddocumento";
                        p_iddocumento.Value = pdocumento;
                        p_iddocumento.Direction = ParameterDirection.InputOutput;
                        p_iddocumento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_iddocumento);

                        DbParameter p_numerosolicitud = cmdTransaccionFactory.CreateParameter();
                        p_numerosolicitud.ParameterName = "p_numerosolicitud";
                        if (pDocumentosAnexos.numerosolicitud == null || pDocumentosAnexos.numerosolicitud == 0)
                            p_numerosolicitud.Value = DBNull.Value;
                        else
                            p_numerosolicitud.Value = pDocumentosAnexos.numerosolicitud;
                        p_numerosolicitud.Direction = ParameterDirection.Input;
                        p_numerosolicitud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numerosolicitud);


                        DbParameter p_numero_radicacion = cmdTransaccionFactory.CreateParameter();
                        p_numero_radicacion.ParameterName = "p_numero_radicacion";
                        p_numero_radicacion.Value = pDocumentosAnexos.numero_radicacion;
                        p_numero_radicacion.Direction = ParameterDirection.Input;
                        p_numero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_numero_radicacion);

                        DbParameter p_tipo_documento = cmdTransaccionFactory.CreateParameter();
                        p_tipo_documento.ParameterName = "p_tipo_documento";
                        p_tipo_documento.Value = pDocumentosAnexos.tipo_documento;
                        p_tipo_documento.Direction = ParameterDirection.Input;
                        p_tipo_documento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_documento);


                        DbParameter p_fechaanexo = cmdTransaccionFactory.CreateParameter();
                        p_fechaanexo.ParameterName = "p_fechaanexo";

                        if (pDocumentosAnexos.fechaanexo == null)
                            p_fechaanexo.Value = DBNull.Value;
                        else
                            p_fechaanexo.Value = pDocumentosAnexos.fechaanexo;
                        p_fechaanexo.Direction = ParameterDirection.Input;
                        p_fechaanexo.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaanexo);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";

                        if (pDocumentosAnexos.descripcion == null)
                            p_descripcion.Value = DBNull.Value;
                        else
                            p_descripcion.Value = pDocumentosAnexos.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);


                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = pDocumentosAnexos.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);


                        DbParameter p_fechaentrega = cmdTransaccionFactory.CreateParameter();
                        p_fechaentrega.ParameterName = "p_fechaentrega";
                        if (pDocumentosAnexos.fechaentrega == null)
                            p_fechaentrega.Value = DBNull.Value;
                        else
                            p_fechaentrega.Value = pDocumentosAnexos.fechaentrega;
                        p_fechaentrega.Direction = ParameterDirection.Input;
                        p_fechaentrega.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaentrega);

                        DbParameter P_IMAGEN = cmdTransaccionFactory.CreateParameter();
                        P_IMAGEN.ParameterName = "P_IMAGEN";
                        if (pDocumentosAnexos.imagen == null)
                            p_fechaentrega.Value = DBNull.Value;
                        else
                            P_IMAGEN.Value = pDocumentosAnexos.imagen;
                        P_IMAGEN.Direction = ParameterDirection.Input;
                        P_IMAGEN.DbType = DbType.Byte;
                        cmdTransaccionFactory.Parameters.Add(P_IMAGEN);

                        DbParameter P_ESPDF = cmdTransaccionFactory.CreateParameter();
                        P_ESPDF.ParameterName = "P_ESPDF";
                        P_ESPDF.Value = pDocumentosAnexos.espdf;
                        P_ESPDF.Direction = ParameterDirection.Input;
                        P_ESPDF.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESPDF);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOC_ANEXOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ModificarDocAnexos", ex);
                        return null;
                    }
                }
            }
        }
        

        /// <summary>
        /// Modifica un registro en la tabla DocumentosAnexos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad DocumentosAnexos modificada</returns>
        public DocumentosAnexos ModificarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDDOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pIDDOCUMENTO.ParameterName = "p_IDDOCUMENTO";
                        pIDDOCUMENTO.Value = pDocumentosAnexos.iddocumento;

                        DbParameter p_NUMEROSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        p_NUMEROSOLICITUD.ParameterName = "p_NUMEROSOLICITUD";
                        p_NUMEROSOLICITUD.Value = pDocumentosAnexos.numerosolicitud;

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pDocumentosAnexos.tipo_documento;

                        DbParameter pCOD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        pCOD_ASESOR.ParameterName = "p_COD_ASESOR";
                        pCOD_ASESOR.Value = pDocumentosAnexos.cod_asesor;

                        DbParameter pIMAGEN = cmdTransaccionFactory.CreateParameter();
                        pIMAGEN.ParameterName = "p_IMAGEN";
                        pIMAGEN.Value = pDocumentosAnexos.imagen;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pDocumentosAnexos.descripcion;

                        DbParameter pFECHAANEXO = cmdTransaccionFactory.CreateParameter();
                        pFECHAANEXO.ParameterName = "p_FECHAANEXO";
                        pFECHAANEXO.Value = pDocumentosAnexos.fechaanexo;

                        cmdTransaccionFactory.Parameters.Add(pIDDOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(p_NUMEROSOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ASESOR);
                        cmdTransaccionFactory.Parameters.Add(pIMAGEN);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pFECHAANEXO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOC_ANEXOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDocumentosAnexos, "DocumentosAnexos", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ModificarDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla DocumentosAnexos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de DocumentosAnexos</param>
        public void EliminarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DocumentosAnexos pDocumentosAnexos = new DocumentosAnexos();

                        if (pUsuario.programaGeneraLog)
                            pDocumentosAnexos = ConsultarDocumentosAnexos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDDOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pIDDOCUMENTO.ParameterName = "p_IDDOCUMENTO";
                        pIDDOCUMENTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDDOCUMENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_DANEX_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pDocumentosAnexos, "DocumentosAnexos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "EliminarDocumentosAnexos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla DocumentosAnexos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos consultado</returns>
        public DocumentosAnexos ConsultarDocumentosAnexos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            DocumentosAnexos entidad = new DocumentosAnexos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  DOCUMENTOSANEXOS WHERE IDDOCUMENTO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHAANEXO"] != DBNull.Value) entidad.fechaanexo = Convert.ToDateTime(resultado["FECHAANEXO"]);
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
                        BOExcepcion.Throw("DocumentosAnexosData", "ConsultarDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }


        public DocumentosAnexos ConsultarDocumentosAnexosConFiltro(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            DocumentosAnexos entidad = new DocumentosAnexos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  DOCUMENTOSANEXOS " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHAANEXO"] != DBNull.Value) entidad.fechaanexo = Convert.ToDateTime(resultado["FECHAANEXO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ConsultarDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla DocumentosAnexos dados unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> ListarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DocumentosAnexos> lstDocumentosAnexos = new List<DocumentosAnexos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Se seleccionan los datos que no son imagen para mostrar en el gridview (La imagen se seleccionará posteriormente desde Handler.ashx)
                        string sql = "";
                        if (pTipo == 1)
                            sql = "SELECT td.descripcion, da.fechaanexo, da.iddocumento  FROM  documentosanexos da LEFT JOIN tiposdocumento td ON da.tipo_documento = td.tipo_documento WHERE da.numero_radicacion = " + pDocumentosAnexos.numero_radicacion;
                        else
                            sql = "SELECT td.descripcion, da.fechaanexo, da.iddocumento  FROM  documentosanexos da LEFT JOIN tiposdocumento td ON da.tipo_documento = td.tipo_documento WHERE da.numerosolicitud = " + pDocumentosAnexos.numerosolicitud;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DocumentosAnexos entidad = new DocumentosAnexos();
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHAANEXO"] != DBNull.Value) entidad.fechaanexo = Convert.ToDateTime(resultado["FECHAANEXO"]);

                            lstDocumentosAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ListarDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }
        public List<DocumentosAnexos> ListarDocAnexos(DocumentosAnexos pDocumentosAnexos, Int64 cod_linea_credito,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DocumentosAnexos> lstDocumentosAnexos = new List<DocumentosAnexos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select da.*,doc.aplica_codeudor,td.tipo,td.descripcion as descr from documentosanexos da left join docrequeridoslinea doc on doc.tipo_documento=da.tipo_documento  left join tiposdocumento td on td.tipo_documento=doc.tipo_documento" + ObtenerFiltro(pDocumentosAnexos) + " and doc.cod_linea_credio=" + cod_linea_credito + "ORDER BY td.TIPO_DOCUMENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DocumentosAnexos entidad = new DocumentosAnexos();
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.iddocumento = Convert.ToInt32(resultado["IDDOCUMENTO"]);
                            if (resultado["tipo_documento"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["tipo_documento"]);
                            if (resultado["descr"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descr"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo == "2")
                            {
                                entidad.tipo = "SI";
                            }
                            else
                            {
                                entidad.tipo = "NO";
                            }

                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
                            if (entidad.aplica_codeudor == "0")
                            {
                                entidad.aplica_codeudor = "SI";
                            }
                            else
                            {
                                entidad.aplica_codeudor = "NO";
                            }
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if(entidad.estado==1)
                                {
                                    entidad.estado_doc = true;
                                }
                            else
                                {
                                    entidad.estado_doc = false;
                                }
                            if (resultado["FECHAANEXO"] != DBNull.Value) entidad.fechaanexo = Convert.ToDateTime(resultado["FECHAANEXO"]);
                            if (resultado["FEC_ESTIMADA_ENTREGA"] != DBNull.Value) entidad.fechaentrega = Convert.ToDateTime(resultado["FEC_ESTIMADA_ENTREGA"]);
                           
                           
                           
                                                       
                          
                            lstDocumentosAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ListarDocAnexos", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla DocumentosAnexos dados unos filtros
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DocumentosAnexos obtenidos</returns>
        public List<DocumentosAnexos> Handler(DocumentosAnexos vDocumentosAnexos, Usuario pUsuario)
        {
            List<DocumentosAnexos> lstDocumentosAnexos = new List<DocumentosAnexos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT da.imagen FROM  documentosanexos da LEFT JOIN tiposdocumento td ON da.tipo_documento = td.tipo_documento WHERE da.iddocumento = " + vDocumentosAnexos.iddocumento;
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DocumentosAnexos entidad = new DocumentosAnexos();
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            lstDocumentosAnexos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentosAnexos;                       

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ListarDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }

        public List<DocumentosAnexos> ListadoControlDocumentos(DateTime pFechaAper, String pFiltro, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();

            DbDataReader resultado;
           List<DocumentosAnexos> Lstentidad = new List<DocumentosAnexos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select c.numero_radicacion, c.cod_linea_credito, l.nombre As nom_linea_credito, p.identificacion, p.nombre, c.fecha_solicitud, 
                                     c.fecha_aprobacion, c.monto_solicitado, c.numero_cuotas, c.estado, d.tipo_documento, t.descripcion As nom_tipo_documento, d.fechaanexo, 
                                     d.descripcion, d.estado AS NOMESTADO, d.fec_estimada_entrega 
                                     From credito c Inner Join documentosanexos d On c.numero_radicacion = d.numero_radicacion 
                                     Inner Join v_persona p On c.cod_deudor = p.cod_persona 
                                     Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito 
                                     Inner Join tiposdocumento t On d.tipo_documento = t.tipo_documento Where c.estado Not In ('T', 'B')  " + pFiltro;

                        if (pFechaAper != null && pFechaAper != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " d.fec_estimada_entrega = To_Date('" + Convert.ToDateTime(pFechaAper).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " d.fec_estimada_entrega = '" + Convert.ToDateTime(pFechaAper).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        while(resultado.Read())
                        {
                            DocumentosAnexos entidad = new DocumentosAnexos();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credio = Convert.ToInt16(resultado["cod_linea_credito"]);
                            if (resultado["nom_linea_credito"] != DBNull.Value) entidad.tipo = entidad.cod_linea_credio.ToString() + " " + Convert.ToString(resultado["nom_linea_credito"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.fechaentrega = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.monto_solicitado = Convert.ToDecimal(resultado["monto_solicitado"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.Nun_Cuoatas = Convert.ToInt16(resultado["numero_cuotas"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado_cre = Convert.ToString(resultado["estado"]);
                            if (resultado["tipo_documento"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["tipo_documento"]);
                            if (resultado["nom_tipo_documento"] != DBNull.Value) entidad.nom_tipo_documento = Convert.ToString(resultado["nom_tipo_documento"]);
                            if (resultado["fechaanexo"] != DBNull.Value) entidad.fechaanexo = Convert.ToDateTime(resultado["fechaanexo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["nomestado"] != DBNull.Value) entidad.estados = Convert.ToString(resultado["nomestado"]);
                            if (resultado["fec_estimada_entrega"] != DBNull.Value) entidad.fec_estimada_entrga = Convert.ToDateTime(resultado["fec_estimada_entrega"]);
                            if (entidad.estados == Convert.ToString(1))
                                entidad.estados = Convert.ToString('E');
                            if (entidad.estados == Convert.ToString(0))
                                entidad.estados = Convert.ToString('P');

                            Lstentidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return Lstentidad;
                    }
                    catch 
                    {            
                        return null;
                    }
                }
            }

        }

    }
}