using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class Usuario_LinkData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public Usuario_LinkData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Usuario_Link> ListarUsuario_Link(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Usuario_Link> lstLinks = new List<Usuario_Link>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT L.*,O.Ruta FROM USUARIO_LINK L INNER JOIN OPCIONES O ON L.Cod_Opcion = O.Cod_Opcion WHERE L.CODUSUARIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Usuario_Link entidad = new Usuario_Link();
                            if (resultado["IDLINK"] != DBNull.Value) entidad.idlink = Convert.ToInt64(resultado["IDLINK"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt32(resultado["COD_OPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["RUTA"] != DBNull.Value) entidad.ruta = Convert.ToString(resultado["RUTA"]);
                            lstLinks.Add(entidad);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLinks;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Usuario_LinkData", "ListarUsuario_Link", ex);
                        return null;
                    }
                }
            }
        }


        public Usuario_Link CrearUsuario_Link(Usuario_Link pLink, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidlink = cmdTransaccionFactory.CreateParameter();
                        pidlink.ParameterName = "p_idlink";
                        pidlink.Value = pLink.idlink;
                        pidlink.Direction = ParameterDirection.Output;
                        pidlink.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidlink);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        pcodusuario.Value = pLink.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pcod_opcion = cmdTransaccionFactory.CreateParameter();
                        pcod_opcion.ParameterName = "p_cod_opcion";
                        pcod_opcion.Value = pLink.cod_opcion;
                        pcod_opcion.Direction = ParameterDirection.Input;
                        pcod_opcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_opcion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pLink.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_USUAR_LINK_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLink.idlink = Convert.ToInt64(pidlink.Value);
                        return pLink;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Usuario_LinkData", "CrearUsuario_Link", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarLink(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM Usuario_Link WHERE Idlink = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Usuario_LinkData", "EliminarLink", ex);
                    }
                }
            }
        }


    }
}