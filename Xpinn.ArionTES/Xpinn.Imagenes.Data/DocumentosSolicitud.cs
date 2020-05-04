using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Oracle.DataAccess.Client;

namespace Xpinn.Imagenes.Data
{
    public class DocumentosSolicitud
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public DocumentosSolicitud()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Documento CrearDocSolicitud(Documento pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    // EJECUTAR EL PROCEDIMIENTO PARA GUARDAR LOS DATOS
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"INSERT INTO docsolicicred (IDDocSolicitud,DESCRIPCION,DOCUMENTO,NUMEROSOLICITUD)
                            VALUES (:p_IDDocSolicitud,:p_DESCRIPCION,:p_DOCUMENTO,:p_NumeroSolicitud)";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    //ASIGNAR LOS VALORES A LOS PARAMETROS
                    OracleParameter p_IDDocSolicitud = new OracleParameter();
                    p_IDDocSolicitud.OracleDbType = OracleDbType.Long;
                    p_IDDocSolicitud.ParameterName = "p_IDDocSolicitud";
                    p_IDDocSolicitud.Value = pEntidad.iddocumento;
                    cmd.Parameters.Add(p_IDDocSolicitud);


                    OracleParameter p_DESCRIPCION = new OracleParameter();
                    p_DESCRIPCION.OracleDbType = OracleDbType.Varchar2;
                    p_DESCRIPCION.ParameterName = "p_DESCRIPCION";
                    p_DESCRIPCION.Value = pEntidad.descripcion_documento;
                    cmd.Parameters.Add(p_DESCRIPCION);


                    OracleParameter p_DOCUMENTO = new OracleParameter();
                    p_DOCUMENTO.OracleDbType = OracleDbType.Blob;
                    p_DOCUMENTO.ParameterName = "p_DOCUMENTO";
                    p_DOCUMENTO.Value = pEntidad.foto;
                    cmd.Parameters.Add(p_DOCUMENTO);

                    OracleParameter p_NumeroSolicitud = new OracleParameter();
                    p_NumeroSolicitud.OracleDbType = OracleDbType.Long;
                    p_NumeroSolicitud.ParameterName = "p_NumeroSolicitud";
                    p_NumeroSolicitud.Value = pEntidad.numero_radicacion;
                    cmd.Parameters.Add(p_NumeroSolicitud);

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);

                    if (p_IDDocSolicitud.Value != null)
                        pEntidad.iddocumento = Convert.ToInt64(p_IDDocSolicitud.Value.ToString());

                    return pEntidad;
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla Imagenes de la base de datos
        /// </summary>
        /// <param name="pImagenes">Entidad Imagenes</param>
        /// <returns>Entidad Imagenes creada</returns>
        public bool CrearAdjuntoPQR( byte[] adjunto,int id, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (OracleConnection con = new OracleConnection(connection.ConnectionString))
                {
                    con.Open();
                    OracleCommand cmd = new OracleCommand();
                    string sql = @"UPDATE PQR SET ADJUNTO = :P_ADJUNTO WHERE ID = " + id + "";
                    cmd = new OracleCommand(sql, con);
                    cmd.CommandType = CommandType.Text;

                    OracleParameter P_ADJUNTO = new OracleParameter();
                    P_ADJUNTO.OracleDbType = OracleDbType.Blob;
                    P_ADJUNTO.ParameterName = "P_ADJUNTO";
                    P_ADJUNTO.Value = adjunto;
                    cmd.Parameters.Add(P_ADJUNTO);                   

                    // EJECUTANDO EL PROCEDIMIENTO
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    dbConnectionFactory.CerrarConexion(connection);
                    return true;
                }
            }
        }


    }
}
