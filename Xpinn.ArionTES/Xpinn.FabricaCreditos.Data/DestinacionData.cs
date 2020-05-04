using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Imagenes.Data;

namespace Xpinn.FabricaCreditos.Data
{
    public class DestinacionData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;
        private ImagenesORAData DAImagenes;

        public DestinacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Destinacion CrearDestinacion(Destinacion pDestinacion, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pDestinacion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pDestinacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter p_oficina_virtual = cmdTransaccionFactory.CreateParameter();
                        p_oficina_virtual.ParameterName = "P_OFICINA_VIRTUAL";
                        p_oficina_virtual.Value = pDestinacion.oficinaVirtual;
                        p_oficina_virtual.Direction = ParameterDirection.Input;
                        p_oficina_virtual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_oficina_virtual);

                        DbParameter p_enlace = cmdTransaccionFactory.CreateParameter();
                        p_enlace.ParameterName = "P_ENLACE";
                        p_enlace.Value = pDestinacion.enlace;
                        p_enlace.Direction = ParameterDirection.Input;
                        p_enlace.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_enlace);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESTINACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "CrearDestinacion", ex);
                        return null;
                    }
                }
            }
        }        

        public Destinacion ModificarDestinacion(Destinacion pDestinacion, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pDestinacion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pDestinacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter p_oficina_virtual = cmdTransaccionFactory.CreateParameter();
                        p_oficina_virtual.ParameterName = "P_OFICINA_VIRTUAL";
                        p_oficina_virtual.Value = pDestinacion.oficinaVirtual;
                        p_oficina_virtual.Direction = ParameterDirection.Input;
                        p_oficina_virtual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_oficina_virtual);

                        DbParameter p_enlace = cmdTransaccionFactory.CreateParameter();
                        p_enlace.ParameterName = "P_ENLACE";
                        p_enlace.Value = pDestinacion.enlace;
                        p_enlace.Direction = ParameterDirection.Input;
                        p_enlace.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_enlace);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESTINACIO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);                        

                        /*******************ACTUALIZO CON LA IMAGEN Y EL BANNER*********************/
                        DAImagenes = new ImagenesORAData();
                        if (foto != null || banner != null)
                            DAImagenes.imagenDestinacion(pDestinacion.cod_destino.ToString(), foto, banner, vUsuario);


                        return pDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "ModificarDestinacion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDestinacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Destinacion pDestinacion = new Destinacion();
                        pDestinacion = ConsultarDestinacion(pId, vUsuario);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESTINACIO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "EliminarDestinacion", ex);
                    }
                }
            }
        }


        public Destinacion ConsultarDestinacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Destinacion entidad = new Destinacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Destinacion WHERE COD_DESTINO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FOTO"] != DBNull.Value) entidad.foto = (byte[])resultado["FOTO"];
                            if (resultado["OFICINA_VIRTUAL"] != DBNull.Value) entidad.oficinaVirtual = Convert.ToInt32(resultado["OFICINA_VIRTUAL"]);
                            if (resultado["ENLACE"] != DBNull.Value) entidad.enlace = Convert.ToString(resultado["ENLACE"]);
                            if (resultado["BANNER"] != DBNull.Value) entidad.banner = (byte[])resultado["BANNER"];
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
                        BOExcepcion.Throw("DestinacionData", "ConsultarDestinacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Destinacion> lstDestinacion = new List<Destinacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Destinacion " + ObtenerFiltro(pDestinacion) + " ORDER BY COD_DESTINO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Destinacion entidad = new Destinacion();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FOTO"] != DBNull.Value) entidad.foto = (byte[])resultado["FOTO"];
                            if (resultado["OFICINA_VIRTUAL"] != DBNull.Value) entidad.oficinaVirtual = Convert.ToInt32(resultado["OFICINA_VIRTUAL"]);
                            if (resultado["ENLACE"] != DBNull.Value) entidad.enlace = Convert.ToString(resultado["ENLACE"]);
                            if (resultado["BANNER"] != DBNull.Value) entidad.banner = (byte[])resultado["BANNER"];
                            lstDestinacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "ListarDestinacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineaCred_Destinacion> ListarLineaCred_Destinacion(LineaCred_Destinacion pDestinacion, string pFiltro, Usuario pusuario)
        {
            DbDataReader resultado;
            List<LineaCred_Destinacion> lstDestinacion = new List<LineaCred_Destinacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select d.*, L.Cod_Linea_Credito
                                       from lineascredito l
                                       inner join Lineacred_Destinacion ld on Ld.Cod_Linea_Credito = L.Cod_Linea_Credito
                                       inner join Destinacion d on D.Cod_Destino = Ld.Cod_Destino
                                       where l.COD_LINEA_CREDITO in (SELECT VALOR FROM parametros_linea WHERE COD_PARAMETRO = 600) and "+pFiltro+@" order by d.descripcion desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaCred_Destinacion entidad = new LineaCred_Destinacion();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FOTO"] != DBNull.Value) entidad.foto = (byte[])resultado["FOTO"];
                            if (resultado["OFICINA_VIRTUAL"] != DBNull.Value) entidad.oficinaVirtual = Convert.ToInt32(resultado["OFICINA_VIRTUAL"]);
                            if (resultado["ENLACE"] != DBNull.Value) entidad.enlace = Convert.ToString(resultado["ENLACE"]);
                            if (resultado["BANNER"] != DBNull.Value) entidad.banner = (byte[])resultado["BANNER"];
                            if (resultado["Cod_Linea_Credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToInt32(resultado["Cod_Linea_Credito"]);
                            lstDestinacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "LineaCred_Destinacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}