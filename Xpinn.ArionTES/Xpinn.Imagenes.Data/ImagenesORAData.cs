using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Icetex.Entities;
using Xpinn.Seguridad.Entities;
using Xpinn.FabricaCreditos.Entities;
using Oracle.DataAccess.Client;

namespace Xpinn.Imagenes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Imagenes
    /// </summary>
    public class ImagenesORAData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Imagenes
        /// </summary>
        public ImagenesORAData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pImagenes">Entidad Imagenes</param>
        /// <returns>Entidad Imagenes creada</returns>
        public Xpinn.FabricaCreditos.Entities.Imagenes CrearImagenesPersona(Xpinn.FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"Insert into IMAGENES_PERSONA (IDIMAGEN, COD_PERSONA, TIPO_DOCUMENTO, IMAGEN, FECHA, TIPO_IMAGEN)
                                    Values (:p_IDIMAGEN, :p_COD_PERSONA, :p_TIPO_DOCUMENTO, :p_IMAGEN, :p_FECHA, :p_TIPO_IMAGEN)
                                    RETURNING IDIMAGEN INTO :p_IDIMAGEN";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDIMAGEN = new OracleParameter();
                    p_IDIMAGEN.OracleDbType = OracleDbType.Long;
                    p_IDIMAGEN.ParameterName = "p_IDIMAGEN";
                    p_IDIMAGEN.Value = pImagenes.idimagen;
                    p_IDIMAGEN.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_IDIMAGEN);

                    OracleParameter p_COD_PERSONA = new OracleParameter();
                    p_COD_PERSONA.OracleDbType = OracleDbType.Long;
                    p_COD_PERSONA.ParameterName = "p_COD_PERSONA";
                    p_COD_PERSONA.Value = pImagenes.cod_persona;
                    cmd.Parameters.Add(p_COD_PERSONA);

                    OracleParameter p_TIPO_DOCUMENTO = new OracleParameter();
                    p_TIPO_DOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_TIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                    p_TIPO_DOCUMENTO.Value = pImagenes.tipo_documento;
                    cmd.Parameters.Add(p_TIPO_DOCUMENTO);

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pImagenes.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    OracleParameter p_FECHAANEXO = new OracleParameter();
                    p_FECHAANEXO.OracleDbType = OracleDbType.Date;
                    p_FECHAANEXO.ParameterName = "p_FECHA";
                    p_FECHAANEXO.Value = pImagenes.fecha;
                    cmd.Parameters.Add(p_FECHAANEXO);

                    OracleParameter p_TIPO_IMAGEN = new OracleParameter();
                    p_TIPO_IMAGEN.OracleDbType = OracleDbType.Int32;
                    p_TIPO_IMAGEN.ParameterName = "p_TIPO_IMAGEN";
                    p_TIPO_IMAGEN.Value = pImagenes.imagenEsPDF ? 1 : 0;
                    cmd.Parameters.Add(p_TIPO_IMAGEN);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    if (p_IDIMAGEN.Value != null)
                        pImagenes.idimagen = Convert.ToInt64(p_IDIMAGEN.Value.ToString());

                    return pImagenes;
                }
            }
        }

        public void guardarContenidoOficina(long cod_opcion, string html, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string inf = "";
                    if (html != null)
                        inf = "HTML = :P_HTML";

                    string sql = @"UPDATE CONTENIDO SET " + inf + " WHERE COD_OPCION = " + cod_opcion + "";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    if (html != null)
                    {
                        OracleParameter P_HTML = new OracleParameter();
                        P_HTML.OracleDbType = OracleDbType.Clob;
                        P_HTML.ParameterName = "P_HTML";
                        P_HTML.Value = html;
                        cmd.Parameters.Add(P_HTML);
                    }

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                }
            }
        }

        public Credito CrearDocumentosGarantia(Credito pDocumentosAnexos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Insert into DOCUMENTOSGARANTIA (IDDOCUMENTO, NUMERO_RADICACION, TIPO_DOCUMENTO, REFERENCIA, RUTA_PDF, IMAGEN)" +
                                    "Values (:p_IDDOCUMENTO, :p_NUMERO_RADICACION, :p_TIPO_DOCUMENTO, '', null,null)";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDDOCUMENTO = new OracleParameter();
                    p_IDDOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_IDDOCUMENTO.ParameterName = "p_IDDOCUMENTO";
                    p_IDDOCUMENTO.Value = pDocumentosAnexos.iddocumento;
                    p_IDDOCUMENTO.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_IDDOCUMENTO);

                    OracleParameter p_NUMERO_RADICACION = new OracleParameter();
                    p_NUMERO_RADICACION.OracleDbType = OracleDbType.Long;
                    p_NUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                    p_NUMERO_RADICACION.Value = pDocumentosAnexos.numero_radicacion;
                    cmd.Parameters.Add(p_NUMERO_RADICACION);

                    OracleParameter p_TIPO_DOCUMENTO = new OracleParameter();
                    p_TIPO_DOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_TIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                    p_TIPO_DOCUMENTO.Value = pDocumentosAnexos.tipo_documento;
                    cmd.Parameters.Add(p_TIPO_DOCUMENTO);

                    //OracleParameter p_REFERENCIA = new OracleParameter();
                    //p_REFERENCIA.OracleDbType = OracleDbType.Long;
                    //p_REFERENCIA.ParameterName = "p_REFERENCIA";
                    //p_REFERENCIA.Value = DBNull.Value;
                    //cmd.Parameters.Add(p_REFERENCIA);

                    //OracleParameter p_IMAGEN = new OracleParameter();
                    //p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    //p_IMAGEN.ParameterName = "p_IMAGEN";
                    //p_IMAGEN.Value = DBNull.Value;
                    //cmd.Parameters.Add(p_IMAGEN);

                    //OracleParameter p_RUTA_PDF = new OracleParameter();
                    //p_RUTA_PDF.OracleDbType = OracleDbType.Varchar2;
                    //p_RUTA_PDF.ParameterName = "RUTA_PDF";
                    //p_RUTA_PDF.Size = 250;
                    //p_RUTA_PDF.Value = DBNull.Value;                    
                    //cmd.Parameters.Add(p_RUTA_PDF);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pDocumentosAnexos;
                }
            }
        }
        public void imagenServicio(string cod_linea, Int32 tipoLinea, byte[] foto, byte[] banner, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string arc = "";
                    if (foto != null)
                        arc = "FOTO = :p_FOTO";
                    if (banner != null)
                    {
                        if (arc != "")
                            arc += ", ";
                        arc += "BANNER = :p_BANNER";
                    }

                    string sql = @"UPDATE LINEASSERVICIOS SET " + arc + @"
                                    WHERE COD_LINEA_SERVICIO = " + cod_linea + " AND TIPO_SERVICIO = " + tipoLinea;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    if (foto != null)
                    {
                        OracleParameter P_FOTO = new OracleParameter();
                        P_FOTO.OracleDbType = OracleDbType.Blob;
                        P_FOTO.ParameterName = "p_FOTO";
                        P_FOTO.Value = foto;
                        cmd.Parameters.Add(P_FOTO);
                    }
                    if (banner != null)
                    {
                        OracleParameter P_BANNER = new OracleParameter();
                        P_BANNER.OracleDbType = OracleDbType.Blob;
                        P_BANNER.ParameterName = "p_BANNER";
                        P_BANNER.Value = banner;
                        cmd.Parameters.Add(P_BANNER);
                    }

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                }
            }
        }


        public void imagenDestinacion(string cod_destino, byte[] foto, byte[] banner, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string arc = "";
                    if (foto != null)
                        arc = "FOTO = :p_FOTO";
                    if (banner != null)
                    {
                        if (arc != "")
                            arc += ", ";
                        arc += "BANNER = :p_BANNER";
                    }

                    string sql = @"UPDATE Destinacion SET " + arc + @"
                                    WHERE COD_DESTINO = " + cod_destino;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    if (foto != null)
                    {
                        OracleParameter P_FOTO = new OracleParameter();
                        P_FOTO.OracleDbType = OracleDbType.Blob;
                        P_FOTO.ParameterName = "p_FOTO";
                        P_FOTO.Value = foto;
                        cmd.Parameters.Add(P_FOTO);
                    }
                    if (banner != null)
                    {
                        OracleParameter P_BANNER = new OracleParameter();
                        P_BANNER.OracleDbType = OracleDbType.Blob;
                        P_BANNER.ParameterName = "p_BANNER";
                        P_BANNER.Value = banner;
                        cmd.Parameters.Add(P_BANNER);
                    }

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                }
            }
        }



        public Ahorros.Entities.Imagenes ModificarImagenesAhorros(Ahorros.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Update AHORRO_VISTA_IMAGENES SET imagen = :p_IMAGEN, fecha = :p_FECHA  WHERE NUMERO_CUENTA = " + pImagenes.Numero_cuenta + " And tipo_documento = " + pImagenes.tipo_imagen;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pImagenes.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    OracleParameter p_FECHAANEXO = new OracleParameter();
                    p_FECHAANEXO.OracleDbType = OracleDbType.Date;
                    p_FECHAANEXO.ParameterName = "p_FECHA";
                    p_FECHAANEXO.Value = pImagenes.fecha;
                    cmd.Parameters.Add(p_FECHAANEXO);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pImagenes;
                }
            }
        }


        /// <summary>
        /// Crea un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pImagenes">Entidad Imagenes</param>
        /// <returns>Entidad Imagenes creada</returns>
        public Ahorros.Entities.Imagenes CrearImagenesAhorros(Ahorros.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"Insert into AHORRO_VISTA_IMAGENES (IDIMAGEN, NUMERO_CUENTA, TIPO_DOCUMENTO, IMAGEN, FECHA)
                                    Values (:p_IDIMAGEN, :p_NUMERO_CUENTA, :p_TIPO_DOCUMENTO, :p_IMAGEN, :p_FECHA)
                                    RETURNING IDIMAGEN INTO :p_IDIMAGEN";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDIMAGEN = new OracleParameter();
                    p_IDIMAGEN.OracleDbType = OracleDbType.Long;
                    p_IDIMAGEN.ParameterName = "p_IDIMAGEN";
                    p_IDIMAGEN.Value = pImagenes.idimagen;
                    p_IDIMAGEN.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_IDIMAGEN);

                    OracleParameter p_NUMERO_CUENTA = new OracleParameter();
                    p_NUMERO_CUENTA.OracleDbType = OracleDbType.Varchar2;
                    p_NUMERO_CUENTA.ParameterName = "p_NUMERO_CUENTA";
                    p_NUMERO_CUENTA.Value = pImagenes.Numero_cuenta;
                    cmd.Parameters.Add(p_NUMERO_CUENTA);

                    OracleParameter p_TIPO_DOCUMENTO = new OracleParameter();
                    p_TIPO_DOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_TIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                    p_TIPO_DOCUMENTO.Value = pImagenes.tipo_imagen;
                    cmd.Parameters.Add(p_TIPO_DOCUMENTO);

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pImagenes.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    OracleParameter p_FECHAANEXO = new OracleParameter();
                    p_FECHAANEXO.OracleDbType = OracleDbType.Date;
                    p_FECHAANEXO.ParameterName = "p_FECHA";
                    p_FECHAANEXO.Value = pImagenes.fecha;
                    cmd.Parameters.Add(p_FECHAANEXO);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    if (p_IDIMAGEN.Value != null)
                        pImagenes.idimagen = Convert.ToInt64(p_IDIMAGEN.Value.ToString());

                    return pImagenes;
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="empresaParaInsertar">Entidad Imagenes</param>
        /// <returns>Entidad Imagenes creada</returns>
        public Empresa ModificarLogoEmpresa(Empresa empresaParaInsertar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();

                    string sql = @"UPDATE EMPRESA SET logoempresa = :p_logoempresa WHERE cod_empresa = " + empresaParaInsertar.cod_empresa;

                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    OracleParameter p_logoempresa = new OracleParameter();
                    p_logoempresa.ParameterName = "p_logoempresa";
                    p_logoempresa.OracleDbType = OracleDbType.Blob;
                    p_logoempresa.Value = empresaParaInsertar.logoempresa_bytes;
                    cmd.Parameters.Add(p_logoempresa);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    return empresaParaInsertar;
                }
            }
        }

        public FabricaCreditos.Entities.Imagenes ConsultarImageneDocumentosPersona(FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario, bool buscarSoloID = false)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Empty;

                        if (buscarSoloID)
                        {
                            sql = @"Select img.idimagen as ID
                                       where img.tipo_documento = " + pImagenes.tipo + " and img.cod_persona = " + pImagenes.cod_persona;
                        }
                        else
                        {
                            sql = @"Select img.idimagen as ID, img.imagen, img.TIPO_IMAGEN
                                       from IMAGENES_PERSONA img
                                       where img.tipo_documento = " + pImagenes.tipo + " and img.cod_persona = " + pImagenes.cod_persona;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID"] != DBNull.Value) pImagenes.idimagen = Convert.ToInt64(resultado["ID"]);

                            if (buscarSoloID == false)
                            {
                                if (resultado["IMAGEN"] != DBNull.Value) pImagenes.imagen = (byte[])resultado["IMAGEN"];
                                if (resultado["TIPO_IMAGEN"] != DBNull.Value) pImagenes.imagenEsPDF = Convert.ToBoolean(resultado["TIPO_IMAGEN"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return pImagenes;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public Xpinn.FabricaCreditos.Entities.Imagenes ModificarImagenesPersona(Xpinn.FabricaCreditos.Entities.Imagenes pImagenes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Update IMAGENES_PERSONA SET imagen = :p_IMAGEN, fecha = :p_FECHA, tipo_imagen = :p_TIPO_IMAGEN WHERE cod_persona = " + pImagenes.cod_persona + " And tipo_documento = " + pImagenes.tipo_documento;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pImagenes.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    OracleParameter p_FECHAANEXO = new OracleParameter();
                    p_FECHAANEXO.OracleDbType = OracleDbType.Date;
                    p_FECHAANEXO.ParameterName = "p_FECHA";
                    p_FECHAANEXO.Value = pImagenes.fecha;
                    cmd.Parameters.Add(p_FECHAANEXO);

                    OracleParameter p_TIPO_IMAGEN = new OracleParameter();
                    p_TIPO_IMAGEN.OracleDbType = OracleDbType.Int32;
                    p_TIPO_IMAGEN.ParameterName = "p_TIPO_IMAGEN";
                    p_TIPO_IMAGEN.Value = pImagenes.imagenEsPDF ? 1 : 0;
                    cmd.Parameters.Add(p_TIPO_IMAGEN);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pImagenes;
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Imagenes</param>
        /// <returns>Entidad Imagenes consultado</returns>
        public byte[] ConsultarImagenPersona(Int64 pId, Int64 pTipoImagen, ref Int64 pIdImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
            pIdImagen = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM IMAGENES_PERSONA WHERE COD_PERSONA = " + pId.ToString() + " AND tipo_documento = " + pTipoImagen + " and Tipo_Imagen!=1 AND IDIMAGEN = (SELECT MAX(IDIMAGEN) FROM IMAGENES_PERSONA WHERE COD_PERSONA = " + pId.ToString() + " and Tipo_Imagen !=1 AND tipo_documento = " + pTipoImagen + " )";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            //if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["IDIMAGEN"] != DBNull.Value) pIdImagen = Convert.ToInt64(resultado["IDIMAGEN"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public byte[] ConsultarImagenHuellaPersona(Int64 pId, Int64 pDedo, ref Int64 pIdImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
            pIdImagen = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM PERSONA_BIOMETRIA WHERE COD_PERSONA = " + pId.ToString() + " AND NUMERO_DEDO = " + pDedo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDBIOMETRIA"] != DBNull.Value) pIdImagen = Convert.ToInt64(resultado["IDBIOMETRIA"]);
                            if (resultado["HUELLA"] != DBNull.Value) entidad.imagen = (byte[])resultado["HUELLA"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public Xpinn.FabricaCreditos.Entities.Imagenes ConsultarImagenesPersona(Int64 pId, Int64 pTipoImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  IMAGENES_PERSONA WHERE COD_PERSONA = " + pId.ToString() + " AND tipo_documento = " + pTipoImagen;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDIMAGEN"] != DBNull.Value) entidad.idimagen = Convert.ToInt64(resultado["IDIMAGEN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_imagen = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
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
                        BOExcepcion.Throw("ImagenesData", "ConsultarImagenes", ex);
                        return null;
                    }
                }
            }
        }


        public Xpinn.FabricaCreditos.Entities.Imagenes ConsultarImagenesPersonaIdentificacion(string pId, Int64 pTipoImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT i.* FROM  IMAGENES_PERSONA i INNER JOIN PERSONA p ON i.cod_persona = p.cod_persona WHERE p.identificacion = '" + pId.ToString() + "' AND i.tipo_documento = " + pTipoImagen;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            if (resultado["IDIMAGEN"] != DBNull.Value) entidad.idimagen = Convert.ToInt64(resultado["IDIMAGEN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_imagen = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.foto = (byte[])resultado["IMAGEN"];
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        else
                        {
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "ConsultarImagenes", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Imagenes dados unos filtros
        /// </summary>
        /// <param name="pImagenes">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Imagenes obtenidos</returns>
        public List<Xpinn.FabricaCreditos.Entities.Imagenes> Handler(Xpinn.FabricaCreditos.Entities.Imagenes vImagenes, Usuario pUsuario)
        {
            List<Xpinn.FabricaCreditos.Entities.Imagenes> lstImagenes = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT da.imagen FROM  IMAGENES_PERSONA da WHERE da.idimagen = " + vImagenes.idimagen;
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            lstImagenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImagenes;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "Handler", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> HandlerHuella(Xpinn.FabricaCreditos.Entities.Imagenes vImagenes, Usuario pUsuario)
        {
            List<Xpinn.FabricaCreditos.Entities.Imagenes> lstImagenes = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT da.huella FROM PERSONA_BIOMETRIA da WHERE da.cod_persona = " + vImagenes.cod_persona;
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
                            if (resultado["HUELLA"] != DBNull.Value) entidad.imagen = (byte[])resultado["HUELLA"];
                            lstImagenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImagenes;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "HandlerHuella", ex);
                        return null;
                    }
                }
            }
        }

        public bool ExisteImagenPersona(Int64 CodPersona, int IdTipo, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool bresultado = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM IMAGENES_PERSONA WHERE COD_PERSONA = " + CodPersona.ToString() + " and tipo_documento = " + IdTipo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                            bresultado = true;
                        else
                            bresultado = false;
                        dbConnectionFactory.CerrarConexion(connection);
                        return bresultado;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla DocumentosAnexos de la base de datos
        /// </summary>
        /// <param name="pDocumentosAnexos">Entidad DocumentosAnexos</param>
        /// <returns>Entidad DocumentosAnexos creada</returns>
        public DocumentosAnexos CrearDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"Insert into DOCUMENTOSANEXOS (IDDOCUMENTO, NUMEROSOLICITUD, NUMERO_RADICACION, TIPO_DOCUMENTO, COD_ASESOR, FECHAANEXO, IMAGEN, DESCRIPCION, ESTADO, FEC_ESTIMADA_ENTREGA, TIPO_PRODUCTO, EXTENSION)
                                    Values (:p_IDDOCUMENTO, :p_NUMEROSOLICITUD, :p_NUMERORADICACION, :p_TIPO_DOCUMENTO, :p_COD_ASESOR, :p_FECHAANEXO, :p_IMAGEN, :p_DESCRIPCION, :p_ESTADO, :p_FEC_ESTIMADA, :p_TIPO_PRODUCTO, :p_EXTENSION)
                                        returning IDDOCUMENTO into :p_IDDOCUMENTO";

                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDDOCUMENTO = new OracleParameter();
                    p_IDDOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_IDDOCUMENTO.ParameterName = "p_IDDOCUMENTO";
                    p_IDDOCUMENTO.Value = pDocumentosAnexos.iddocumento;
                    p_IDDOCUMENTO.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_IDDOCUMENTO);

                    OracleParameter p_NUMEROSOLICITUD = new OracleParameter();
                    p_NUMEROSOLICITUD.OracleDbType = OracleDbType.Long;
                    p_NUMEROSOLICITUD.ParameterName = "p_NUMEROSOLICITUD";
                    if (pDocumentosAnexos.numerosolicitud == 0)
                        p_NUMEROSOLICITUD.Value = DBNull.Value;
                    else
                        p_NUMEROSOLICITUD.Value = pDocumentosAnexos.numerosolicitud;
                    cmd.Parameters.Add(p_NUMEROSOLICITUD);

                    OracleParameter p_NUMERORADICACION = new OracleParameter();
                    p_NUMERORADICACION.OracleDbType = OracleDbType.Long;
                    p_NUMERORADICACION.ParameterName = "p_NUMERORADICACION";
                    if (pDocumentosAnexos.numero_radicacion == 0)
                        p_NUMERORADICACION.Value = DBNull.Value;
                    else
                        p_NUMERORADICACION.Value = pDocumentosAnexos.numero_radicacion;
                    cmd.Parameters.Add(p_NUMERORADICACION);

                    OracleParameter p_TIPO_DOCUMENTO = new OracleParameter();
                    p_TIPO_DOCUMENTO.OracleDbType = OracleDbType.Long;
                    p_TIPO_DOCUMENTO.ParameterName = "p_TIPO_DOCUMENTO";
                    p_TIPO_DOCUMENTO.Value = pDocumentosAnexos.tipo_documento;
                    cmd.Parameters.Add(p_TIPO_DOCUMENTO);

                    OracleParameter p_COD_ASESOR = new OracleParameter();
                    p_COD_ASESOR.OracleDbType = OracleDbType.Long;
                    p_COD_ASESOR.ParameterName = "p_COD_ASESOR";
                    if (pDocumentosAnexos.cod_asesor == 0)
                        p_COD_ASESOR.Value = DBNull.Value;
                    else
                        p_COD_ASESOR.Value = pDocumentosAnexos.cod_asesor;
                    cmd.Parameters.Add(p_COD_ASESOR);

                    OracleParameter p_FECHAANEXO = new OracleParameter();
                    p_FECHAANEXO.OracleDbType = OracleDbType.Date;
                    p_FECHAANEXO.ParameterName = "p_FECHAANEXO";
                    p_FECHAANEXO.Value = pDocumentosAnexos.fechaanexo;
                    cmd.Parameters.Add(p_FECHAANEXO);

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pDocumentosAnexos.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    OracleParameter p_DESCRIPCION = new OracleParameter();
                    p_DESCRIPCION.OracleDbType = OracleDbType.Varchar2;
                    p_DESCRIPCION.ParameterName = "p_DESCRIPCION";
                    p_DESCRIPCION.Size = 250;
                    if (pDocumentosAnexos.descripcion == null)
                        p_DESCRIPCION.Value = DBNull.Value;
                    else
                        p_DESCRIPCION.Value = pDocumentosAnexos.descripcion;
                    cmd.Parameters.Add(p_DESCRIPCION);

                    OracleParameter p_ESTADO = new OracleParameter();
                    p_ESTADO.OracleDbType = OracleDbType.Varchar2;
                    p_ESTADO.ParameterName = "p_ESTADO";
                    p_ESTADO.Size = 2;
                    p_ESTADO.Value = pDocumentosAnexos.estado;
                    cmd.Parameters.Add(p_ESTADO);

                    OracleParameter p_FEC_ESTIMADA = new OracleParameter();
                    p_FEC_ESTIMADA.OracleDbType = OracleDbType.Date;
                    p_FEC_ESTIMADA.ParameterName = "p_FEC_ESTIMADA";
                    if (pDocumentosAnexos.fechaentrega == null)
                        p_FEC_ESTIMADA.Value = DBNull.Value;
                    else
                        p_FEC_ESTIMADA.Value = pDocumentosAnexos.fechaentrega;
                    cmd.Parameters.Add(p_FEC_ESTIMADA);

                    OracleParameter p_TIPO_PRODUCTO = new OracleParameter();
                    p_TIPO_PRODUCTO.OracleDbType = OracleDbType.Int32;
                    p_TIPO_PRODUCTO.ParameterName = "p_TIPO_PRODUCTO";
                    p_TIPO_PRODUCTO.Size = 4;
                    p_TIPO_PRODUCTO.Value = pDocumentosAnexos.tipo_producto;
                    cmd.Parameters.Add(p_TIPO_PRODUCTO);


                    OracleParameter p_EXTENSION = new OracleParameter();
                    p_EXTENSION.OracleDbType = OracleDbType.Varchar2;
                    p_EXTENSION.ParameterName = "p_EXTENSION";
                    p_EXTENSION.Size = 10;
                    p_EXTENSION.Value = pDocumentosAnexos.extension;
                    cmd.Parameters.Add(p_EXTENSION);


                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();

                    if (p_IDDOCUMENTO.Value != DBNull.Value || p_IDDOCUMENTO.Value != null)
                        pDocumentosAnexos.iddocumento = Convert.ToInt64(p_IDDOCUMENTO.Value.ToString());

                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    return pDocumentosAnexos;
                }
            }
        }


        public Credito CrearCreditoPreAnalisis(Credito pPersona, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();

                    string sql = @"INSERT INTO PREANALISIS_CREDITO(idpreanalisis, cod_persona, fecha, saldo_disponible, cuota_credito, cuota_servicios,
                                        pago_terceros, cuota_otros, ingresos_adicionales, menos_smlmv, total_disponible, aportes, creditos, capitalizacion, cod_usuario,
                                        monto_solicitado, plazo_solicitado, cod_linea_credito, imagen)
                                   VALUES(:p_idpreanalisis, :p_cod_persona, :p_fecha, :p_saldo_disponible, :p_cuota_credito, :p_cuota_servicios, :p_pago_terceros,
                                        :p_cuota_otros, :p_ingresos_adicionales, :p_menos_smlmv, :p_total_disponible, :p_aportes, :p_creditos, :p_capitalizacion, :p_cod_usuario,
                                        :p_monto_solicitado, :p_plazo_solicitado, :p_cod_linea_credito, :p_imagen)
                                            returning idpreanalisis into :p_idpreanalisis";

                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_idpreanalisis = new OracleParameter();
                    p_idpreanalisis.OracleDbType = OracleDbType.Long;
                    p_idpreanalisis.ParameterName = "p_idpreanalisis";
                    p_idpreanalisis.Value = pPersona.idpreanalisis;
                    p_idpreanalisis.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_idpreanalisis);

                    OracleParameter p_cod_persona = new OracleParameter();
                    p_cod_persona.OracleDbType = OracleDbType.Long;
                    p_cod_persona.ParameterName = "p_cod_persona";
                    p_cod_persona.Value = pPersona.cod_persona;
                    cmd.Parameters.Add(p_cod_persona);

                    OracleParameter p_fecha = new OracleParameter();
                    p_fecha.OracleDbType = OracleDbType.Date;
                    p_fecha.ParameterName = "p_fecha";
                    p_fecha.Value = pPersona.fecha;
                    cmd.Parameters.Add(p_fecha);

                    OracleParameter psaldo_disponible = new OracleParameter();
                    psaldo_disponible.OracleDbType = OracleDbType.Decimal;
                    psaldo_disponible.ParameterName = "p_saldo_disponible";
                    if (pPersona.saldo_disponible == 0)
                        psaldo_disponible.Value = DBNull.Value;
                    else
                        psaldo_disponible.Value = pPersona.saldo_disponible;
                    cmd.Parameters.Add(psaldo_disponible);

                    OracleParameter p_cuota_credito = new OracleParameter();
                    p_cuota_credito.OracleDbType = OracleDbType.Decimal;
                    p_cuota_credito.ParameterName = "p_cuota_credito";
                    if (pPersona.cuota_credito == 0)
                        p_cuota_credito.Value = DBNull.Value;
                    else
                        p_cuota_credito.Value = pPersona.cuota_credito;
                    cmd.Parameters.Add(p_cuota_credito);

                    OracleParameter p_cuota_servicios = new OracleParameter();
                    p_cuota_servicios.OracleDbType = OracleDbType.Decimal;
                    p_cuota_servicios.ParameterName = "p_cuota_servicios";
                    if (pPersona.cuota_servicios == 0)
                        p_cuota_servicios.Value = DBNull.Value;
                    else
                        p_cuota_servicios.Value = pPersona.cuota_servicios;
                    cmd.Parameters.Add(p_cuota_servicios);

                    OracleParameter p_pago_terceros = new OracleParameter();
                    p_pago_terceros.OracleDbType = OracleDbType.Decimal;
                    p_pago_terceros.ParameterName = "p_pago_terceros";
                    if (pPersona.pago_terceros == 0)
                        p_pago_terceros.Value = DBNull.Value;
                    else
                        p_pago_terceros.Value = pPersona.pago_terceros;
                    cmd.Parameters.Add(p_pago_terceros);

                    OracleParameter pcuota_otros = new OracleParameter();
                    pcuota_otros.OracleDbType = OracleDbType.Decimal;
                    pcuota_otros.ParameterName = "p_cuota_otros";
                    if (pPersona.cuota_otros == 0)
                        pcuota_otros.Value = DBNull.Value;
                    else
                        pcuota_otros.Value = pPersona.cuota_otros;
                    cmd.Parameters.Add(pcuota_otros);

                    OracleParameter p_ingresos_adicionales = new OracleParameter();
                    p_ingresos_adicionales.OracleDbType = OracleDbType.Decimal;
                    p_ingresos_adicionales.ParameterName = "p_ingresos_adicionales";
                    if (pPersona.ingresos_adicionales == 0)
                        p_ingresos_adicionales.Value = DBNull.Value;
                    else
                        p_ingresos_adicionales.Value = pPersona.ingresos_adicionales;
                    cmd.Parameters.Add(p_ingresos_adicionales);

                    OracleParameter p_menos_smlmv = new OracleParameter();
                    p_menos_smlmv.OracleDbType = OracleDbType.Decimal;
                    p_menos_smlmv.ParameterName = "p_menos_smlmv";
                    if (pPersona.menos_smlmv == 0)
                        p_menos_smlmv.Value = DBNull.Value;
                    else
                        p_menos_smlmv.Value = pPersona.menos_smlmv;
                    cmd.Parameters.Add(p_menos_smlmv);

                    OracleParameter p_total_disponible = new OracleParameter();
                    p_total_disponible.OracleDbType = OracleDbType.Decimal;
                    p_total_disponible.ParameterName = "p_total_disponible";
                    if (pPersona.total_disponible == 0)
                        p_total_disponible.Value = DBNull.Value;
                    else
                        p_total_disponible.Value = pPersona.total_disponible;
                    cmd.Parameters.Add(p_total_disponible);

                    OracleParameter p_aportes = new OracleParameter();
                    p_aportes.OracleDbType = OracleDbType.Decimal;
                    p_aportes.ParameterName = "p_aportes";
                    if (pPersona.aportes == 0)
                        p_aportes.Value = DBNull.Value;
                    else
                        p_aportes.Value = pPersona.aportes;
                    cmd.Parameters.Add(p_aportes);

                    OracleParameter p_creditos = new OracleParameter();
                    p_creditos.OracleDbType = OracleDbType.Decimal;
                    p_creditos.ParameterName = "p_creditos";
                    if (pPersona.creditos == 0)
                        p_creditos.Value = DBNull.Value;
                    else
                        p_creditos.Value = pPersona.creditos;
                    cmd.Parameters.Add(p_creditos);

                    OracleParameter p_capitalizacion = new OracleParameter();
                    p_capitalizacion.OracleDbType = OracleDbType.Decimal;
                    p_capitalizacion.ParameterName = "p_capitalizacion";
                    if (pPersona.capitalizacion == 0)
                        p_capitalizacion.Value = DBNull.Value;
                    else
                        p_capitalizacion.Value = pPersona.capitalizacion;
                    cmd.Parameters.Add(p_capitalizacion);

                    OracleParameter p_cod_usuario = new OracleParameter();
                    p_cod_usuario.OracleDbType = OracleDbType.Int32;
                    p_cod_usuario.ParameterName = "p_cod_usuario";
                    p_cod_usuario.Value = pPersona.cod_usuario;
                    cmd.Parameters.Add(p_cod_usuario);

                    OracleParameter p_monto_solicitado = new OracleParameter();
                    p_monto_solicitado.OracleDbType = OracleDbType.Decimal;
                    p_monto_solicitado.ParameterName = "p_monto_solicitado";
                    if (pPersona.monto == 0)
                        p_monto_solicitado.Value = DBNull.Value;
                    else
                        p_monto_solicitado.Value = pPersona.monto;
                    cmd.Parameters.Add(p_monto_solicitado);

                    OracleParameter p_plazo_solicitado = new OracleParameter();
                    p_plazo_solicitado.OracleDbType = OracleDbType.Int64;
                    p_plazo_solicitado.ParameterName = "p_plazo_solicitado";
                    if (pPersona.plazo == 0)
                        p_plazo_solicitado.Value = DBNull.Value;
                    else
                        p_plazo_solicitado.Value = pPersona.plazo;
                    cmd.Parameters.Add(p_plazo_solicitado);

                    OracleParameter p_cod_linea_credito = new OracleParameter();
                    p_cod_linea_credito.OracleDbType = OracleDbType.Varchar2;
                    p_cod_linea_credito.Size = 16;
                    p_cod_linea_credito.ParameterName = "p_cod_linea_credito";
                    if (pPersona.cod_linea_credito == null)
                        p_cod_linea_credito.Value = DBNull.Value;
                    else
                        p_cod_linea_credito.Value = pPersona.cod_linea_credito;
                    cmd.Parameters.Add(p_cod_linea_credito);

                    OracleParameter p_imagen = new OracleParameter();
                    p_imagen.OracleDbType = OracleDbType.Blob;
                    p_imagen.ParameterName = "p_imagen";
                    if (pPersona.imagen == null)
                        p_imagen.Value = DBNull.Value;
                    else
                        p_imagen.Value = pPersona.imagen;
                    cmd.Parameters.Add(p_imagen);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();

                    if (p_idpreanalisis.Value != DBNull.Value && p_idpreanalisis.Value != null)
                        pPersona.idpreanalisis = Convert.ToInt64(p_idpreanalisis.Value.ToString());

                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    return pPersona;
                }
            }
        }


        public DocumentosAnexos ModificarDocumentosAnexos(DocumentosAnexos pDocumentosAnexos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"UPDATE DOCUMENTOSANEXOS 
                                    SET IMAGEN = :p_IMAGEN
                                    WHERE IDDOCUMENTO = " + pDocumentosAnexos.iddocumento.ToString();

                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    OracleParameter p_IMAGEN = new OracleParameter();
                    p_IMAGEN.OracleDbType = OracleDbType.Blob;
                    p_IMAGEN.ParameterName = "p_IMAGEN";
                    p_IMAGEN.Value = pDocumentosAnexos.imagen;
                    cmd.Parameters.Add(p_IMAGEN);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pDocumentosAnexos;
                }
            }
        }


        /// <summary>
        /// Crea un registro en la tabla PersonaBiometria de la base de datos
        /// </summary>
        /// <param name="pPersonaBiometria">Entidad PersonaBiometria</param>
        /// <returns>Entidad PersonaBiometria creada</returns>
        public PersonaBiometria CrearPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Insert Into Persona_Biometria (idbiometria, cod_persona, numero_dedo, huella, template_huella, codusuario, fecha, huella_secugen)" +
                                    "Values (:p_IDBIOMETRIA, :p_COD_PERSONA, :p_NUMERO_DEDO, :p_HUELLA, :p_TEMPLATE_HUELLA, :p_CODUSUARIO, :p_FECHA, :p_HUELLA_SECUGEN)" +
                                    "Returning idbiometria Into :p_IDBIOMETRIA";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDBIOMETRIA = new OracleParameter();
                    p_IDBIOMETRIA.OracleDbType = OracleDbType.Long;
                    p_IDBIOMETRIA.ParameterName = "p_IDBIOMETRIA";
                    p_IDBIOMETRIA.Value = pPersonaBiometria.idbiometria;
                    p_IDBIOMETRIA.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(p_IDBIOMETRIA);

                    OracleParameter p_COD_PERSONA = new OracleParameter();
                    p_COD_PERSONA.OracleDbType = OracleDbType.Long;
                    p_COD_PERSONA.ParameterName = "p_COD_PERSONA";
                    p_COD_PERSONA.Value = pPersonaBiometria.cod_persona;
                    cmd.Parameters.Add(p_COD_PERSONA);

                    OracleParameter p_NUMERO_DEDO = new OracleParameter();
                    p_NUMERO_DEDO.OracleDbType = OracleDbType.Long;
                    p_NUMERO_DEDO.ParameterName = "p_NUMERO_DEDO";
                    p_NUMERO_DEDO.Value = pPersonaBiometria.numero_dedo;
                    cmd.Parameters.Add(p_NUMERO_DEDO);

                    OracleParameter p_HUELLA = new OracleParameter();
                    p_HUELLA.OracleDbType = OracleDbType.Blob;
                    p_HUELLA.ParameterName = "p_HUELLA";
                    if (pPersonaBiometria.huella == null)
                        p_HUELLA.Value = DBNull.Value;
                    else
                        p_HUELLA.Value = pPersonaBiometria.huella;
                    cmd.Parameters.Add(p_HUELLA);

                    OracleParameter p_TEMPLATE_HUELLA = new OracleParameter();
                    p_TEMPLATE_HUELLA.OracleDbType = OracleDbType.Varchar2;
                    p_TEMPLATE_HUELLA.ParameterName = "p_TEMPLATE_HUELLA";
                    p_TEMPLATE_HUELLA.Value = pPersonaBiometria.template_huella;
                    cmd.Parameters.Add(p_TEMPLATE_HUELLA);

                    OracleParameter p_CODUSUARIO = new OracleParameter();
                    p_CODUSUARIO.OracleDbType = OracleDbType.Long;
                    p_CODUSUARIO.ParameterName = "p_CODUSUARIO";
                    p_CODUSUARIO.Value = pPersonaBiometria.codusuario;
                    cmd.Parameters.Add(p_CODUSUARIO);

                    OracleParameter p_FECHA = new OracleParameter();
                    p_FECHA.OracleDbType = OracleDbType.Date;
                    p_FECHA.ParameterName = "p_FECHA";
                    p_FECHA.Value = pPersonaBiometria.fecha;
                    cmd.Parameters.Add(p_FECHA);

                    OracleParameter p_HUELLA_SECUGEN = new OracleParameter();
                    p_HUELLA_SECUGEN.OracleDbType = OracleDbType.Raw;
                    p_HUELLA_SECUGEN.ParameterName = "p_HUELLA_SECUGEN";
                    if (pPersonaBiometria.huella_secugen == null)
                        p_HUELLA_SECUGEN.Value = DBNull.Value;
                    else
                        p_HUELLA_SECUGEN.Value = pPersonaBiometria.huella_secugen;
                    cmd.Parameters.Add(p_HUELLA_SECUGEN);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();

                    if (p_IDBIOMETRIA.Value.ToString().Trim() != "")
                        try
                        {
                            pPersonaBiometria.idbiometria = Convert.ToInt64(p_IDBIOMETRIA.Value);
                        }
                        catch
                        {
                            pPersonaBiometria.idbiometria = 1;
                        }

                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    return pPersonaBiometria;
                }
            }
        }

        public PersonaBiometria ModificarPersonaBiometria(PersonaBiometria pPersonaBiometria, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Update Persona_Biometria SET huella = :p_HUELLA, template_huella = '" + pPersonaBiometria.template_huella + "', codusuario = :p_CODUSUARIO, fecha = :p_FECHA, huella_secugen = :p_HUELLA_SECUGEN WHERE cod_persona = " + pPersonaBiometria.cod_persona + " And numero_dedo = " + pPersonaBiometria.numero_dedo;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS                    
                    OracleParameter p_HUELLA = new OracleParameter();
                    p_HUELLA.OracleDbType = OracleDbType.Blob;
                    p_HUELLA.ParameterName = "p_HUELLA";
                    p_HUELLA.Value = pPersonaBiometria.huella;
                    cmd.Parameters.Add(p_HUELLA);

                    OracleParameter p_CODUSUARIO = new OracleParameter();
                    p_CODUSUARIO.OracleDbType = OracleDbType.Long;
                    p_CODUSUARIO.ParameterName = "p_CODUSUARIO";
                    p_CODUSUARIO.Value = pPersonaBiometria.codusuario;
                    cmd.Parameters.Add(p_CODUSUARIO);

                    OracleParameter p_FECHA = new OracleParameter();
                    p_FECHA.OracleDbType = OracleDbType.Date;
                    p_FECHA.ParameterName = "p_FECHA";
                    p_FECHA.Value = pPersonaBiometria.fecha;
                    cmd.Parameters.Add(p_FECHA);

                    OracleParameter p_HUELLA_SECUGEN = new OracleParameter();
                    p_HUELLA_SECUGEN.OracleDbType = OracleDbType.Blob;
                    p_HUELLA_SECUGEN.ParameterName = "p_HUELLA_SECUGEN";
                    p_HUELLA_SECUGEN.Value = pPersonaBiometria.huella_secugen;
                    cmd.Parameters.Add(p_HUELLA_SECUGEN);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pPersonaBiometria;
                }
            }
        }


        public CreditoIcetexDocumento CrearCreditoIcetexDocumento(CreditoIcetexDocumento pCreditoIcetx, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"Insert Into Creditoicetexdoc (Cod_Credoc, Numero_Credito, Cod_Tipo_Doc, Pregunta, Respuesta, Archivo)
                                  Values ( :P_Cod_Credoc, :P_Numero_Credito, :P_Cod_Tipo_Doc, :P_Pregunta, :P_Respuesta , :p_archivo)
                                  RETURNING cod_credoc INTO :p_cod_credoc";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter pcod_credoc = new OracleParameter();
                    pcod_credoc.OracleDbType = OracleDbType.Long;
                    pcod_credoc.ParameterName = "p_cod_credoc";
                    pcod_credoc.Value = pCreditoIcetx.cod_credoc;
                    pcod_credoc.Direction = ParameterDirection.InputOutput;
                    cmd.Parameters.Add(pcod_credoc);

                    OracleParameter pnumero_credito = new OracleParameter();
                    pnumero_credito.OracleDbType = OracleDbType.Long;
                    pnumero_credito.ParameterName = "p_numero_credito";
                    pnumero_credito.Value = pCreditoIcetx.numero_credito;
                    cmd.Parameters.Add(pnumero_credito);

                    OracleParameter pcod_tipo_doc = new OracleParameter();
                    pcod_tipo_doc.OracleDbType = OracleDbType.Int32;
                    pcod_tipo_doc.ParameterName = "p_cod_tipo_doc";
                    pcod_tipo_doc.Value = pCreditoIcetx.cod_tipo_doc;
                    cmd.Parameters.Add(pcod_tipo_doc);

                    OracleParameter ppregunta = new OracleParameter();
                    ppregunta.OracleDbType = OracleDbType.Varchar2;
                    ppregunta.ParameterName = "p_pregunta";
                    if (pCreditoIcetx.pregunta == null)
                        ppregunta.Value = DBNull.Value;
                    else
                        ppregunta.Value = pCreditoIcetx.pregunta;
                    cmd.Parameters.Add(ppregunta);

                    OracleParameter prespuesta = new OracleParameter();
                    prespuesta.OracleDbType = OracleDbType.Varchar2;
                    prespuesta.ParameterName = "p_respuesta";
                    if (pCreditoIcetx.respuesta == null)
                        prespuesta.Value = DBNull.Value;
                    else
                        prespuesta.Value = pCreditoIcetx.respuesta;
                    cmd.Parameters.Add(prespuesta);

                    OracleParameter parchivo = new OracleParameter();
                    parchivo.OracleDbType = OracleDbType.Blob;
                    parchivo.ParameterName = "p_archivo";
                    parchivo.Value = pCreditoIcetx.imagen;
                    cmd.Parameters.Add(parchivo);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    if (pcod_credoc.Value != null)
                        pCreditoIcetx.cod_credoc = Convert.ToInt32(pcod_credoc.Value.ToString());

                    return pCreditoIcetx;
                }
            }
        }


        public CreditoIcetexDocumento ModificarCreditoIcetexDocumento(CreditoIcetexDocumento pCreditoIcetx, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = "Update Creditoicetexdoc SET ARCHIVO = :p_archivo, Observacion = :p_observacion WHERE COD_CREDOC = " + pCreditoIcetx;
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS                    
                    OracleParameter pcodigo = new OracleParameter();
                    pcodigo.OracleDbType = OracleDbType.Int32;
                    pcodigo.ParameterName = "p_codigo";
                    pcodigo.Value = pCreditoIcetx.cod_credoc;
                    cmd.Parameters.Add(pcodigo);

                    OracleParameter parchivo = new OracleParameter();
                    parchivo.OracleDbType = OracleDbType.Blob;
                    parchivo.ParameterName = "p_archivo";
                    parchivo.Value = pCreditoIcetx.imagen;
                    cmd.Parameters.Add(parchivo);

                    OracleParameter pobservacion = new OracleParameter();
                    pobservacion.OracleDbType = OracleDbType.Varchar2;
                    pobservacion.ParameterName = "p_observacion";
                    if (pCreditoIcetx.observacion == null)
                        pobservacion.Value = DBNull.Value;
                    else
                        pobservacion.Value = pCreditoIcetx.observacion;
                    cmd.Parameters.Add(pobservacion);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return pCreditoIcetx;
                }
            }
        }

        public CreditoIcetexAprobacion CrearCreditoIcetexAprobacion(CreditoIcetexAprobacion pcredito, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"Insert Into creditoicetexaprobacion (IDAPROBACION, NUMERO_CREDITO, FECHA_APROBACION, COD_USUARIO, OBSERVACIONES, DOCUMENTO_SOPORTE, ESTADO, TIPO_APROBACION)
                                  Values ( :p_idaprobacion, :p_numero_credito, :p_fecha_aprobacion, :p_cod_usuario, :p_observaciones , :p_documento_soporte, :p_estado, :p_tipo_aprobacion)
                                  RETURNING IDAPROBACION INTO :p_idaprobacion";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    OracleParameter pidaprobacion = new OracleParameter();
                    pidaprobacion.OracleDbType = OracleDbType.Int32;
                    pidaprobacion.ParameterName = "p_idaprobacion";
                    pidaprobacion.Value = pcredito.idaprobacion;
                    pidaprobacion.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                    cmd.Parameters.Add(pidaprobacion);

                    OracleParameter pnumero_credito = new OracleParameter();
                    pnumero_credito.OracleDbType = OracleDbType.Long;
                    pnumero_credito.ParameterName = "p_numero_credito";
                    if (pcredito.numero_credito == null)
                        pnumero_credito.Value = DBNull.Value;
                    else
                        pnumero_credito.Value = pcredito.numero_credito;
                    pnumero_credito.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(pnumero_credito);

                    OracleParameter pfecha_aprobacion = new OracleParameter();
                    pfecha_aprobacion.OracleDbType = OracleDbType.Date;
                    pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                    pfecha_aprobacion.Value = pcredito.fecha_aprobacion;
                    pfecha_aprobacion.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(pfecha_aprobacion);

                    OracleParameter pcod_usuario = new OracleParameter();
                    pcod_usuario.OracleDbType = OracleDbType.Int32;
                    pcod_usuario.ParameterName = "p_cod_usuario";
                    pcod_usuario.Value = pcredito.cod_usuario;
                    pcod_usuario.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(pcod_usuario);

                    OracleParameter pobservaciones = new OracleParameter();
                    pobservaciones.OracleDbType = OracleDbType.Varchar2;
                    pobservaciones.ParameterName = "p_observaciones";
                    if (pcredito.observaciones == null)
                        pobservaciones.Value = DBNull.Value;
                    else
                        pobservaciones.Value = pcredito.observaciones;
                    pobservaciones.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(pobservaciones);

                    OracleParameter pdocumento_soporte = new OracleParameter();
                    pdocumento_soporte.OracleDbType = OracleDbType.Blob;
                    pdocumento_soporte.ParameterName = "p_documento_soporte";
                    if (pcredito.documento_soporte == null)
                        pdocumento_soporte.Value = DBNull.Value;
                    else
                        pdocumento_soporte.Value = pcredito.documento_soporte;
                    cmd.Parameters.Add(pdocumento_soporte);

                    OracleParameter pestado = new OracleParameter();
                    pestado.OracleDbType = OracleDbType.Varchar2;
                    pestado.ParameterName = "p_estado";
                    if (pcredito.estado == null)
                        pestado.Value = DBNull.Value;
                    else
                        pestado.Value = pcredito.estado;
                    pestado.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(pestado);

                    OracleParameter ptipo_aprobacion = new OracleParameter();
                    ptipo_aprobacion.OracleDbType = OracleDbType.Int32;
                    ptipo_aprobacion.ParameterName = "p_tipo_aprobacion";
                    if (pcredito.tipo_aprobacion == null)
                        ptipo_aprobacion.Value = DBNull.Value;
                    else
                        ptipo_aprobacion.Value = pcredito.tipo_aprobacion;
                    ptipo_aprobacion.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(ptipo_aprobacion);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    if (pidaprobacion.Value != null && pOpcion == 1)
                        pcredito.idaprobacion = Convert.ToInt32(pidaprobacion.Value.ToString());

                    return pcredito;
                }
            }
        }
        public byte[] ConsultarDocPersona(Int64 pId, ref Int64 pIdImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Ahorros.Entities.Imagenes entidad = new Ahorros.Entities.Imagenes();
            pIdImagen = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM IMAGENES_PERSONA WHERE IDIMAGEN = " + pId.ToString() + " AND TIPO_IMAGEN=1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            //if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));

                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> ListaDocumentos(Xpinn.FabricaCreditos.Entities.Imagenes vImagenes, Usuario pUsuario)
        {
            List<Xpinn.FabricaCreditos.Entities.Imagenes> lstImagenes = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql = "SELECT * FROM IMAGENES_PERSONA WHERE COD_PERSONA = " + vImagenes.cod_persona + " AND tipo_imagen = 1";
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
                            if (resultado["IDIMAGEN"] != DBNull.Value) entidad.idimagen = Convert.ToInt64(resultado["IDIMAGEN"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            lstImagenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImagenes;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "ListaDocumentos", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.FabricaCreditos.Entities.Imagenes ConsultarDocumentosAnexos(Int64 pId, ref Int64 pIdImagen, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
            pIdImagen = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM documentosanexos  WHERE iddocumento = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        //System.Text.Encoding enc = System.Text.Encoding.ASCII;
                        if (resultado.Read())
                        {
                            //if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = enc.GetBytes(Convert.ToString(resultado["IMAGEN"]));
                            if (resultado["iddocumento"] != DBNull.Value) pIdImagen = Convert.ToInt64(resultado["iddocumento"]);
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                            if (resultado["ESPDF"] != DBNull.Value) entidad.imagenEsPDF = Convert.ToBoolean(resultado["ESPDF"]);
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

        public void guardarDoctCDAT(string cod_solicitud, byte[] consignacion, byte[] declaracion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string arc = "";
                    if (consignacion != null)
                        arc = "CONSIGNACION = :P_CONSIGNACION";
                    if (declaracion != null)
                    {
                        if (arc != "")
                            arc += ", ";
                        arc += "DECLARACION = :P_DECLARACION";
                    }

                    string sql = @"UPDATE SOLICITUD_PRODUCTO_WEB SET " + arc + @"
                                    WHERE ID_SOL_PRODUCTO = " + cod_solicitud + "";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    if (consignacion != null)
                    {
                        OracleParameter P_CONSIGNACION = new OracleParameter();
                        P_CONSIGNACION.OracleDbType = OracleDbType.Blob;
                        P_CONSIGNACION.ParameterName = "P_CONSIGNACION";
                        P_CONSIGNACION.Value = consignacion;
                        cmd.Parameters.Add(P_CONSIGNACION);
                    }
                    if (declaracion != null)
                    {
                        OracleParameter P_DECLARACION = new OracleParameter();
                        P_DECLARACION.OracleDbType = OracleDbType.Blob;
                        P_DECLARACION.ParameterName = "P_DECLARACION";
                        P_DECLARACION.Value = declaracion;
                        cmd.Parameters.Add(P_DECLARACION);
                    }

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                }
            }
        }

        public void ActualizarImagen(Int64 ptipo_documento, byte[] pTextos, Usuario pUsuario, string nomTabla, string nomColumn)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string arc = "";
                    if (pTextos != null)
                        arc = "Texto_Array = :P_texto ";

                    string sql = @"UPDATE " + nomTabla + " SET " + arc + @"
                                    WHERE " + nomColumn + " = " + ptipo_documento + "";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    if (pTextos != null)
                    {
                        OracleParameter P_IMAGEN = new OracleParameter();
                        P_IMAGEN.OracleDbType = OracleDbType.Blob;
                        P_IMAGEN.ParameterName = "P_texto";
                        P_IMAGEN.Value = pTextos;
                        cmd.Parameters.Add(P_IMAGEN);
                    }

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                }
            }
        }

        public List<Xpinn.FabricaCreditos.Entities.Imagenes> ListaDocumentosAnexos(int pTipoReferencia, Int64 pNumeroSolicitud, int tipo_producto, Usuario pUsuario)
        {
            List<Xpinn.FabricaCreditos.Entities.Imagenes> lstImagenes = new List<Xpinn.FabricaCreditos.Entities.Imagenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   // Despues de poner los datos que no son imagen en el gridview, se selecciona la imagen y se raliza su respectiva relacion

                        string sql;
                        if (pTipoReferencia == 1)
                            sql = "SELECT D.*, T.DESCRIPCION AS NOM_TIPO_DOCUMENTO FROM DOCUMENTOSANEXOS D LEFT JOIN TIPOSDOCUMENTO T ON d.tipo_documento = t.tipo_documento WHERE d.numerosolicitud = " + pNumeroSolicitud + " and d.tipo_producto = " + tipo_producto + " ORDER BY d.IDDOCUMENTO";
                        else
                            sql = "SELECT D.*, T.DESCRIPCION AS NOM_TIPO_DOCUMENTO FROM DOCUMENTOSANEXOS D LEFT JOIN TIPOSDOCUMENTO T ON d.tipo_documento = t.tipo_documento WHERE d.numero_radicacion = " + pNumeroSolicitud + " and d.tipo_producto in( " + tipo_producto + ", 999) ORDER BY d.IDDOCUMENTO";
                        DbDataReader resultado = default(DbDataReader);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();
                            if (resultado["IDDOCUMENTO"] != DBNull.Value) entidad.idimagen = Convert.ToInt64(resultado["IDDOCUMENTO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (entidad.tipo == 2) //cuando es credito
                            {
                                if (resultado["NOM_TIPO_DOCUMENTO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_TIPO_DOCUMENTO"]);
                            }
                            else // otros tipos
                                if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["EXTENSION"] != DBNull.Value) entidad.Formato = Convert.ToString(resultado["EXTENSION"]);
                            lstImagenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImagenes;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ImagenesData", "ListaDocumentosAnexos", ex);
                        return null;
                    }
                }
            }
        }

        public byte[] ConsultarDocAnexo(Int64 pIdDocumento, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Imagenes entidad = new Xpinn.FabricaCreditos.Entities.Imagenes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM DOCUMENTOSANEXOS WHERE IDDOCUMENTO = " + pIdDocumento.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IMAGEN"] != DBNull.Value) entidad.imagen = (byte[])resultado["IMAGEN"];
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad.imagen;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }


        public DocumentosAnexos ModificarDocAnexosImagen(DocumentosAnexos pDocumentosAnexos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        OracleCommand cmd = new OracleCommand();
                        string sql = @"UPDATE DOCUMENTOSANEXOS SET IMAGEN=:P_IMAGEN,ESPDF=:P_ESPDF WHERE IDDOCUMENTO=" + pDocumentosAnexos.iddocumento;
                        cmd = new OracleCommand(sql, con);
                        cmd.CommandType = CommandType.Text;

                        OracleParameter p_IMAGEN = new OracleParameter();
                        p_IMAGEN.OracleDbType = OracleDbType.Blob;
                        p_IMAGEN.ParameterName = "p_IMAGEN";
                        p_IMAGEN.Value = pDocumentosAnexos.imagen;
                        cmd.Parameters.Add(p_IMAGEN);

                        OracleParameter P_ESPDF = new OracleParameter();
                        P_ESPDF.OracleDbType = OracleDbType.Int32;
                        P_ESPDF.ParameterName = "P_ESPDF";
                        P_ESPDF.Value = pDocumentosAnexos.espdf;

                        cmd.Parameters.Add(P_ESPDF);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocumentosAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DocumentosAnexosData", "ModificarDocAnexosImagen", ex);
                        return null;
                    }
                }
            }
        }

        public DocumentosAnexos CrearDocAnexosImagen(DocumentosAnexos pDocumentosAnexos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    try
                    {
                        con.Open();
                        OracleCommand cmd = new OracleCommand();
                        string sql = @"INSERT INTO DOCUMENTOSANEXOS (IDDOCUMENTO, NUMEROSOLICITUD, NUMERO_RADICACION, TIPO_DOCUMENTO,   
                                    COD_ASESOR, FECHAANEXO, IMAGEN, DESCRIPCION, ESTADO,FEC_ESTIMADA_ENTREGA,ESPDF)   
                                   VALUES (:P_IDDOCUMENTO, :P_NUMEROSOLICITUD, :P_NUMERO_RADICACION, :P_TIPO_DOCUMENTO, :P_COD_ASESOR,   
                                           :P_FECHAANEXO, :P_IMAGEN, :P_DESCRIPCION, :P_ESTADO,:P_FECHAENTREGA,:P_ESPDF) ";
                        cmd = new OracleCommand(sql, con);
                        cmd.CommandType = CommandType.Text;

                        OracleParameter p_iddocumento = new OracleParameter();
                        p_iddocumento.OracleDbType = OracleDbType.Long;
                        p_iddocumento.ParameterName = "P_IDDOCUMENTO";
                        p_iddocumento.Value = pDocumentosAnexos.iddocumento;
                        p_iddocumento.Direction = ParameterDirection.InputOutput;
                        cmd.Parameters.Add(p_iddocumento);


                        OracleParameter p_numerosolicitud = new OracleParameter();
                        p_numerosolicitud.OracleDbType = OracleDbType.Long;
                        p_numerosolicitud.ParameterName = "P_NUMEROSOLICITUD";
                        if (pDocumentosAnexos.numerosolicitud == int.MinValue || pDocumentosAnexos.numerosolicitud == 0)
                            p_numerosolicitud.Value = DBNull.Value;
                        else
                            p_numerosolicitud.Value = pDocumentosAnexos.numerosolicitud;

                        cmd.Parameters.Add(p_numerosolicitud);


                        OracleParameter p_numero_radicacion = new OracleParameter();
                        p_numero_radicacion.OracleDbType = OracleDbType.Long;
                        p_numero_radicacion.ParameterName = "P_NUMERO_RADICACION";
                        p_numero_radicacion.Value = pDocumentosAnexos.numero_radicacion;

                        cmd.Parameters.Add(p_numero_radicacion);

                        OracleParameter p_tipo_documento = new OracleParameter();
                        p_tipo_documento.OracleDbType = OracleDbType.Long;
                        p_tipo_documento.ParameterName = "P_TIPO_DOCUMENTO";
                        p_tipo_documento.Value = pDocumentosAnexos.tipo_documento;

                        cmd.Parameters.Add(p_tipo_documento);

                        OracleParameter P_COD_ASESOR = new OracleParameter();
                        P_COD_ASESOR.OracleDbType = OracleDbType.Long;
                        P_COD_ASESOR.ParameterName = "P_COD_ASESOR";
                        P_COD_ASESOR.Value = 0;

                        cmd.Parameters.Add(P_COD_ASESOR);


                        OracleParameter p_fechaanexo = new OracleParameter();
                        p_fechaanexo.ParameterName = "P_FECHAANEXO";
                        p_fechaanexo.OracleDbType = OracleDbType.Date;
                        if (pDocumentosAnexos.fechaanexo == null)
                            p_fechaanexo.Value = DBNull.Value;
                        else
                            p_fechaanexo.Value = pDocumentosAnexos.fechaanexo;

                        cmd.Parameters.Add(p_fechaanexo);

                        OracleParameter p_IMAGEN = new OracleParameter();
                        p_IMAGEN.OracleDbType = OracleDbType.Blob;
                        p_IMAGEN.ParameterName = "p_IMAGEN";
                        p_IMAGEN.Value = pDocumentosAnexos.imagen;
                        cmd.Parameters.Add(p_IMAGEN);

                        OracleParameter p_descripcion = new OracleParameter();
                        p_descripcion.ParameterName = "P_DESCRIPCION";
                        p_descripcion.OracleDbType = OracleDbType.Varchar2;
                        if (pDocumentosAnexos.descripcion == null)
                            p_descripcion.Value = DBNull.Value;
                        else
                            p_descripcion.Value = pDocumentosAnexos.descripcion;

                        cmd.Parameters.Add(p_descripcion);


                        OracleParameter p_estado = new OracleParameter();
                        p_estado.OracleDbType = OracleDbType.Varchar2;
                        p_estado.ParameterName = "P_ESTADO";
                        p_estado.Value = pDocumentosAnexos.estados;

                        cmd.Parameters.Add(p_estado);

                        OracleParameter p_fechaentrega = new OracleParameter();
                        p_fechaentrega.OracleDbType = OracleDbType.Date;
                        p_fechaentrega.ParameterName = "P_FECHAENTREGA";
                        if (pDocumentosAnexos.fechaentrega == null)
                            p_fechaentrega.Value = DBNull.Value;
                        else
                            p_fechaentrega.Value = pDocumentosAnexos.fechaentrega;

                        cmd.Parameters.Add(p_fechaentrega);

                        OracleParameter P_ESPDF = new OracleParameter();
                        P_ESPDF.OracleDbType = OracleDbType.Int32;
                        P_ESPDF.ParameterName = "P_ESPDF";
                        P_ESPDF.Value = pDocumentosAnexos.espdf;

                        cmd.Parameters.Add(P_ESPDF);

                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

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




    }
}