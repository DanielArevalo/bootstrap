using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Imagenes.Data;


namespace Xpinn.FabricaCreditos.Data
{
    public class DatosDeDocumentoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public DatosDeDocumentoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene los campos que se insertarán en el documento
        /// </summary>
        public List<DatosDeDocumento> ListarDatosDeDocumento(Int64 NumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_VAR_CONS";

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "P_VARIABLE";
                        pNUM_RADIC.Value = NumeroRadicacion;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumento", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc";// Where numero_radicacion = " + NumeroRadicacion.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentos", ex);
                        return null;
                    }
                }
            }
        }

        public List<DatosDeDocumento> ListarDatosDeDocumentoReporteCDAT(Int64 CodigoCDAT, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_GENDOC_CDAT";

                        DbParameter pCodigo_CDAT = cmdTransaccionFactory.CreateParameter();
                        pCodigo_CDAT.ParameterName = "P_CODIGO_CDAT";
                        pCodigo_CDAT.Value = CodigoCDAT;

                        cmdTransaccionFactory.Parameters.Add(pCodigo_CDAT);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumento", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene los campos que se insertarán en el documento
        /// </summary>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormato(Int64 NumeroRadicacion, Int64 TipoDocumento, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_SOLICITUD";

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "p_numero_radicacion";
                        pNUM_RADIC.Value = NumeroRadicacion;

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = "p_tipo_documento";
                        pTIPO_DOCUMENTO.Value = TipoDocumento;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormato", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc";// Where numero_radicacion = " + NumeroRadicacion.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentos", ex);
                        return null;
                    }
                }
            }
        }
        /// Obtiene los campos que se insertarán en el documento
        /// </summary>
        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivas(Int64 NumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_CARTAS";

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "p_numero_radicacion";
                        pNUM_RADIC.Value = NumeroRadicacion;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormato", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc Where numero_radicacion = " + NumeroRadicacion.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                        return null;
                    }
                }
            }
        }
        //Metodo para consultar variables  Daniel Arevalo 19/07/2019
        public List<DatosDeDocumento> ListarVariables(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_VAR_Det";


                        DbParameter P_FECHA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA.ParameterName = "P_FECHA";
                        P_FECHA.Value = DateTime.Now;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarVariables", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarVariablesMasivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<DatosDeDocumento> ListarDatosDeDocumentoFormatoCartasMasivasCodeudor(Int64 NumeroRadicacion, Usuario pUsuario, Int64 Codeudor)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOC_CARTAS_CODE";

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "p_numero_radicacion";
                        pNUM_RADIC.Value = NumeroRadicacion;


                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = Codeudor;


                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormato", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc Where numero_radicacion = " + NumeroRadicacion.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                        return null;
                    }
                }
            }
        }
        /// Obtiene los campos que se insertarán en el documento
        /// </summary>
        public List<DatosDeDocumento> ListarDatosDeDocumentoCDAT(Int64 pCodigoCDAT, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_CARTAS";

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "p_numero_radicacion";
                        pNUM_RADIC.Value = pCodigoCDAT;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormato", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @" Select id, campo, valor From temp_gendoc Where numero_radicacion = " + pCodigoCDAT.ToString();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DatosDeDocumento entidad = new DatosDeDocumento();

                            //Asociar todos los valores a la entidad

                            if (resultado["ID"] != DBNull.Value) entidad.Id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["CAMPO"] != DBNull.Value) entidad.Campo = Convert.ToString(resultado["CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.Valor = Convert.ToString(resultado["VALOR"]);
                            lstDatosDeDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosDeDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormatoCartasMasivas", ex);
                        return null;
                    }
                }
            }
        }
        public Documento ConsultarSolicitud(Usuario vUsuario)
        {
            DbDataReader resultado;
            Documento entidad = new Documento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select NVL(max(IDDOCSOLICITUD),0) as limit FROM docsolicicred ORDER BY IDDOCSOLICITUD DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["limit"] != DBNull.Value) entidad.iddocumento = Convert.ToInt64(resultado["limit"].ToString());
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }

        public Documento CrearDocSolicitud(Documento pEntidad, Usuario pUsuario)
        {
            Xpinn.Imagenes.Data.DocumentosSolicitud imagenesData = new Imagenes.Data.DocumentosSolicitud();
            return imagenesData.CrearDocSolicitud(pEntidad, pUsuario);
        }


    }
}
