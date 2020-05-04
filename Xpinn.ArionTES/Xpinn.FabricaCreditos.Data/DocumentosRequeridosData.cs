using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{


    public class DocumentosRequeridosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ControlCreditos
        /// </summary>
        public DocumentosRequeridosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public documentosrequeridos CrearDOCUMENTOSREQUERIDOS(documentosrequeridos pDOCUMENTOSREQUERIDOS, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credio.ParameterName = "p_cod_linea_credio";
                        pcod_linea_credio.Value = pDOCUMENTOSREQUERIDOS.cod_linea_credio;
                        pcod_linea_credio.Direction = ParameterDirection.Input;
                        pcod_linea_credio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credio);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "p_tipo_documento";
                        ptipo_documento.Value = pDOCUMENTOSREQUERIDOS.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        DbParameter paplica_codeudor = cmdTransaccionFactory.CreateParameter();
                        paplica_codeudor.ParameterName = "p_aplica_codeudor";
                        if (pDOCUMENTOSREQUERIDOS.aplica_codeudor == null)
                            paplica_codeudor.Value = DBNull.Value;
                        else
                            paplica_codeudor.Value = pDOCUMENTOSREQUERIDOS.aplica_codeudor;
                        paplica_codeudor.Direction = ParameterDirection.Input;
                        paplica_codeudor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(paplica_codeudor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCREQUERI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDOCUMENTOSREQUERIDOS;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "CrearDOCUMENTOSREQUERIDOS", ex);
                        return null;
                    }
                }
            }
        }
        
        public documentosrequeridos ModificarDOCUMENTOSREQUERIDOS(documentosrequeridos pDOCUMENTOSREQUERIDOS, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credio.ParameterName = "p_cod_linea_credio";
                        pcod_linea_credio.Value = pDOCUMENTOSREQUERIDOS.cod_linea_credio;
                        pcod_linea_credio.Direction = ParameterDirection.Input;
                        pcod_linea_credio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credio);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "p_tipo_documento";
                        ptipo_documento.Value = pDOCUMENTOSREQUERIDOS.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        DbParameter paplica_codeudor = cmdTransaccionFactory.CreateParameter();
                        paplica_codeudor.ParameterName = "p_aplica_codeudor";
                        if (pDOCUMENTOSREQUERIDOS.aplica_codeudor == null)
                            paplica_codeudor.Value = DBNull.Value;
                        else
                            paplica_codeudor.Value = pDOCUMENTOSREQUERIDOS.aplica_codeudor;
                        paplica_codeudor.Direction = ParameterDirection.Input;
                        paplica_codeudor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(paplica_codeudor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCREQUERI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDOCUMENTOSREQUERIDOS;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "ModificarDOCUMENTOSREQUERIDOS", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDOCUMENTOSREQUERIDOS(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        documentosrequeridos pDOCUMENTOSREQUERIDOS = new documentosrequeridos();
                        pDOCUMENTOSREQUERIDOS = ConsultarDOCUMENTOSREQUERIDOS(pId, vUsuario);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "p_tipo_documento";
                        ptipo_documento.Value = pDOCUMENTOSREQUERIDOS.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCREQUERI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "EliminarDOCUMENTOSREQUERIDOS", ex);
                    }
                }
            }
        }


        public documentosrequeridos ConsultarDOCUMENTOSREQUERIDOS(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            documentosrequeridos entidad = new documentosrequeridos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DOCREQUERIDOSLINEA WHERE TIPO_DOCUMENTO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDIO"] != DBNull.Value) entidad.cod_linea_credio = Convert.ToString(resultado["COD_LINEA_CREDIO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
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
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "ConsultarDOCUMENTOSREQUERIDOS", ex);
                        return null;
                    }
                }
            }
        }


        public List<documentosrequeridos> ListarDocumentosRequeridos(documentosrequeridos pDOCUMENTOSREQUERIDOS, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<documentosrequeridos> lstDOCUMENTOSREQUERIDOS = new List<documentosrequeridos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select doc.cod_linea_credio,doc.tipo_documento,doc.aplica_codeudor,td.descripcion,td.tipo from  DOCREQUERIDOSLINEA doc inner join tiposdocumento td on doc.tipo_documento=td.tipo_documento " + ObtenerFiltro(pDOCUMENTOSREQUERIDOS) + " ORDER BY TIPO_DOCUMENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            documentosrequeridos entidad = new documentosrequeridos();
                            if (resultado["cod_linea_credio"] != DBNull.Value) entidad.cod_linea_credio = Convert.ToString(resultado["COD_LINEA_CREDIO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
                            if (entidad.aplica_codeudor == "0")
                            {
                                entidad.aplica_codeudor = "SI";
                            }
                            else if (entidad.aplica_codeudor == "N.A")
                            {
                                entidad.aplica_codeudor = "NO";
                            }
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo == "2")
                            {
                                entidad.tipo = "SI";
                            }
                            else
                            {
                                entidad.tipo = "NO";
                            }
                            lstDOCUMENTOSREQUERIDOS.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDOCUMENTOSREQUERIDOS;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "ListarDocumentosRequeridos", ex);
                        return null;
                    }
                }
            }
        }


        public List<documentosrequeridos> ListarDocumentosCredito(string radicado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<documentosrequeridos> lstDOCUMENTOSREQUERIDOS = new List<documentosrequeridos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.cod_linea_credito, d.TIPO_DOCUMENTO, 2 as APLICA_CODEUDOR, t.DESCRIPCION, t.tipo
                                        FROM DOCGARANTIALINEA d
                                        inner join TIPOSDOCUMENTO t on d.TIPO_DOCUMENTO = t.TIPO_DOCUMENTO
                                        inner join CREDITO c on c.COD_LINEA_CREDITO = d.COD_LINEA_CREDITO
                                        WHERE c.NUMERO_RADICACION ="+radicado;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            documentosrequeridos entidad = new documentosrequeridos();
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credio = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
                            if (entidad.aplica_codeudor == "0")
                            {
                                entidad.aplica_codeudor = "SI";
                            }
                            else if (entidad.aplica_codeudor == "2")
                            {
                                entidad.aplica_codeudor = "N.A";
                            }
                            else
                            {

                            }
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo == "2")
                            {
                                entidad.tipo = "SI";
                            }
                            else
                            {
                                entidad.tipo = "NO";
                            }
                            lstDOCUMENTOSREQUERIDOS.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDOCUMENTOSREQUERIDOS;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DOCUMENTOSREQUERIDOSData", "ListarDocumentosRequeridos", ex);
                        return null;
                    }
                }
            }
        }

    }
}