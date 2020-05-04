using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Aportes.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.Aportes.Data
{
    public class oficina_ciudadAportesData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public oficina_ciudadAportesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Oficina_ciudad Crearoficina_ciudad(Oficina_ciudad poficina_ciudad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidoficiudad = cmdTransaccionFactory.CreateParameter();
                        pidoficiudad.ParameterName = "p_idoficiudad";
                        pidoficiudad.Value = poficina_ciudad.idoficiudad;
                        pidoficiudad.Direction = ParameterDirection.Input;
                        pidoficiudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidoficiudad);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = poficina_ciudad.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        pcodciudad.Value = poficina_ciudad.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        pcodciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_OFICINA_CI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return poficina_ciudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Crearoficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }


        public Oficina_ciudad Modificaroficina_ciudad(Oficina_ciudad poficina_ciudad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidoficiudad = cmdTransaccionFactory.CreateParameter();
                        pidoficiudad.ParameterName = "p_idoficiudad";
                        pidoficiudad.Value = poficina_ciudad.idoficiudad;
                        pidoficiudad.Direction = ParameterDirection.Input;
                        pidoficiudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidoficiudad);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = poficina_ciudad.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        pcodciudad.Value = poficina_ciudad.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        pcodciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_OFICINA_CI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return poficina_ciudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Modificaroficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }


        public void Eliminaroficina_ciudad(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Oficina_ciudad poficina_ciudad = new Oficina_ciudad();
                        poficina_ciudad = Consultaroficina_ciudad(pId, vUsuario);

                        DbParameter pidoficiudad = cmdTransaccionFactory.CreateParameter();
                        pidoficiudad.ParameterName = "p_idoficiudad";
                        pidoficiudad.Value = poficina_ciudad.idoficiudad;
                        pidoficiudad.Direction = ParameterDirection.Input;
                        pidoficiudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidoficiudad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_OFICINA_CI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Eliminaroficina_ciudad", ex);
                    }
                }
            }
        }


        public Oficina_ciudad Consultaroficina_ciudad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Oficina_ciudad entidad = new Oficina_ciudad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM oficina_ciudad WHERE IDOFICIUDAD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDOFICIUDAD"] != DBNull.Value) entidad.idoficiudad = Convert.ToInt32(resultado["IDOFICIUDAD"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
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
                        BOExcepcion.Throw("oficina_ciudadData", "Consultaroficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }


        public List<Oficina_ciudad> Listaroficina_ciudad(Oficina_ciudad poficina_ciudad, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Oficina_ciudad> lstoficina_ciudad = new List<Oficina_ciudad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select o.cod_oficina, o.nombre from oficina o ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Oficina_ciudad entidad = new Oficina_ciudad();
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre_Oficina = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            lstoficina_ciudad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstoficina_ciudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Listaroficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }

        public List<Oficina_ciudad> listaOficinaCiudad(Usuario vUsuario, String Pfiltro)
        {
            DbDataReader resultado;
            List<Oficina_ciudad> lstoficina_ciudad = new List<Oficina_ciudad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"select oc.idoficiudad, o.cod_oficina, o.nombre, c.codciudad, c.nomciudad
                                       from oficina_ciudad oc inner join ciudades c on oc.codciudad = c.codciudad inner join oficina o 
                                       on oc.cod_oficina = o.cod_oficina " + Pfiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Oficina_ciudad entidad = new Oficina_ciudad();
                            if (resultado["idoficiudad"] != DBNull.Value) entidad.idoficiudad = Convert.ToInt32(resultado["idoficiudad"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["cod_oficina"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.Nombre_Oficina = Convert.ToString(resultado["nombre"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.Nombre_Ciudad = Convert.ToString(resultado["nomciudad"]);
                            lstoficina_ciudad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstoficina_ciudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Listaroficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }

        public Oficina_ciudad validaOficinaGuarda(Usuario vUsuario, Oficina_ciudad entiti, int opcion)
        {
            DbDataReader resultado;
            Oficina_ciudad entidad = new Oficina_ciudad();
             string sql ="";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (opcion)
                        {
                                //Valida Modifica.... Valida que no le asigne a esa Oficina YA Creada Una Ciudad que ya tiene Otra Oficina
                            case 1:
                                sql = @"select * from oficina_ciudad o "
                                        +"where o.cod_oficina = "+entiti.cod_oficina+" and o.codciudad="+entiti.codciudad +
                                        "and o.idoficiudad <>(select f.idoficiudad from oficina_ciudad f where f.cod_oficina = 2 and f.codciudad= "+entiti.codciudad+")";
                                break;
                            case 2:// valida que Al crear una Oficina No Se le asigne Una Ciudad Ya asignada
                                sql = @"select * from oficina_ciudad o 
                                        where o.codciudad ="+entiti.codciudad;//+"  and o.cod_oficina<>(select f.cod_oficina from oficina_ciudad f where f.cod_oficina = "+entiti.cod_oficina+" )";
                                break;
                            case 3: // Valida que no se asigne la misma Ciudad a la misma oficina Dos veces 
                                sql = @"select * from oficina_ciudad o where o.cod_oficina = " + entiti.cod_oficina + " and o.codciudad= " + entiti.codciudad;
                                break;
                        }

                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while(resultado.Read())
                        {
                            if (resultado["IDOFICIUDAD"] != DBNull.Value) entidad.idoficiudad = Convert.ToInt32(resultado["IDOFICIUDAD"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("oficina_ciudadData", "Consultaroficina_ciudad", ex);
                        return null;
                    }
                }
            }
        }

    }
}