using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class FormatoDocumentoData : GlobalData
    {
        private ImagenesORAData imagData = new ImagenesORAData();
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public FormatoDocumentoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public FormatoDocumento CrearFormatoDocumentos(FormatoDocumento pFormatoDocumento, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_documento = cmdTransaccionFactory.CreateParameter();
                        pcod_documento.ParameterName = "p_cod_documento";
                        pcod_documento.Value = pFormatoDocumento.cod_documento;
                        if (pOpcion == 1)
                            pcod_documento.Direction = ParameterDirection.Output;
                        else
                            pcod_documento.Direction = ParameterDirection.Input;
                        pcod_documento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_documento);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pFormatoDocumento.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pFormatoDocumento.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pFormatoDocumento.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pFormatoDocumento.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptexto = cmdTransaccionFactory.CreateParameter();
                        ptexto.ParameterName = "p_texto";
                        if (pFormatoDocumento.texto == null)
                            ptexto.Value = DBNull.Value;
                        else
                            ptexto.Value = pFormatoDocumento.texto;
                        ptexto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptexto);

                        DbParameter pnombre_pl = cmdTransaccionFactory.CreateParameter();
                        pnombre_pl.ParameterName = "p_nombre_pl";
                        if (pFormatoDocumento.nombre_pl == null)
                            pnombre_pl.Value = DBNull.Value;
                        else
                            pnombre_pl.Value = pFormatoDocumento.nombre_pl;
                        pnombre_pl.Direction = ParameterDirection.Input;
                        pnombre_pl.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_pl);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_FORMATODOC_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_FORMATODOC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pFormatoDocumento.cod_documento = Convert.ToInt64(pcod_documento.Value);

                        if (pFormatoDocumento.Textos != null)
                            imagData.ActualizarImagen(pFormatoDocumento.cod_documento, pFormatoDocumento.Textos,
                                vUsuario, "FormatoDocumentos", "COD_DOCUMENTO");
                        return pFormatoDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormatoDocumentoData", "CrearFormatoDocumentos", ex);
                        return null;
                    }
                }
            }
        }


        public List<FormatoDocumento> ListarFormatoDocumento(FormatoDocumento pFormatoDocumento, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<FormatoDocumento> lstFormatoDocumento = new List<FormatoDocumento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM FormatoDocumentos " + ObtenerFiltro(pFormatoDocumento) + " ORDER BY COD_DOCUMENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            FormatoDocumento entidad = new FormatoDocumento();
                            if (resultado["COD_DOCUMENTO"] != DBNull.Value) entidad.cod_documento = Convert.ToInt64(resultado["COD_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo == "1")
                                entidad.nomtipo = "Afiliación";
                            if (entidad.tipo == "2")
                                entidad.nomtipo = "Aprobación de Crédito";
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            lstFormatoDocumento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstFormatoDocumento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormatoDocumentoData", "ListarFormatoDocumento", ex);
                        return null;
                    }
                }
            }
        }

        public FormatoDocumento ConsultarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            FormatoDocumento entidad = new FormatoDocumento();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM FormatoDocumentos WHERE COD_DOCUMENTO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_DOCUMENTO"] != DBNull.Value) entidad.cod_documento = Convert.ToInt64(resultado["COD_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.texto = Convert.ToString(resultado["TEXTO"]);
                            if (resultado["NOMBRE_PL"] != DBNull.Value) entidad.nombre_pl = Convert.ToString(resultado["NOMBRE_PL"]);
                            if (resultado["TEXTO_ARRAY"] != DBNull.Value) entidad.Textos = (byte[])resultado["TEXTO_ARRAY"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FormatoDocumentoData", "ConsultarFormatoDocumento", ex);
                        return null;
                    }
                }
            }
        }


       public List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumento(Int64 pVariable, string pNombre_pl, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDatosDeDocumento = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                //connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pNombre_pl;

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "P_VARIABLE";
                        pNUM_RADIC.Value = pVariable;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = vUsuario.codusuario;

                 
                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_VAR_CONS";
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

                        string sql = @" Select id, campo, valor From temp_gendoc Where numero_radicacion = " + pVariable.ToString().Trim();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.DatosDeDocumento entidad = new Xpinn.FabricaCreditos.Entities.DatosDeDocumento();

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
        public List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> ListarDatosDeDocumentoOtros(Int64 pVariable, string pNombre_pl, Usuario vUsuario, String origen)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento> lstDatosDeDocumento = new List<Xpinn.FabricaCreditos.Entities.DatosDeDocumento>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                //connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* Se desactiva esta sección por su dependencia de System.Data.OracleClient */

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pNombre_pl;

                        DbParameter pNUM_RADIC = cmdTransaccionFactory.CreateParameter();
                        pNUM_RADIC.ParameterName = "P_VARIABLE";
                        pNUM_RADIC.Value = pVariable;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Value = DateTime.Now;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = vUsuario.codusuario;

                        DbParameter pORIGEN = cmdTransaccionFactory.CreateParameter();
                        pORIGEN.ParameterName = "pORIGEN";
                        pORIGEN.Value = origen;

                        cmdTransaccionFactory.Parameters.Add(pNUM_RADIC);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pORIGEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GENDOC_VAR_CONS";
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

                        string sql = @" Select id, campo, valor From temp_gendoc Where numero_radicacion = " + pVariable.ToString().Trim();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.DatosDeDocumento entidad = new Xpinn.FabricaCreditos.Entities.DatosDeDocumento();

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

        public void EliminarFormatoDocumento(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_documento = cmdTransaccionFactory.CreateParameter();
                        pcod_documento.ParameterName = "p_cod_documento";
                        pcod_documento.Value = pId;
                        pcod_documento.Direction = ParameterDirection.Input;
                        pcod_documento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_documento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_FORMATODOC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "EliminarFormatoDocumento", ex);
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario vUsuario)
        {
            Int64 resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(COD_DOCUMENTO) + 1 FROM FORMATODOCUMENTOS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        return 1;
                    }
                }
            }
        }


    }
}


