using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;


namespace Xpinn.Nomina.Data
{
  
    public class CargosData:GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CargosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Cargos CrearCargo(Cargos pCargosEntities, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDCARGO = cmdTransaccionFactory.CreateParameter();
                        P_IDCARGO.ParameterName = "P_IDCARGO";
                        P_IDCARGO.Value = pCargosEntities.IdCargo;
                        P_IDCARGO.Direction = ParameterDirection.Input;
                        P_IDCARGO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDCARGO);

                        DbParameter P_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE.ParameterName = "P_NOMBRE";
                        P_NOMBRE.Value = pCargosEntities.Nombre;
                        P_NOMBRE.Direction = ParameterDirection.Input;
                        P_NOMBRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE);

                      

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CARGO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCargosEntities;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "CrearCargo", ex);
                        return null;
                    }
                }
            }
        }

        public Cargos ModificarCargo(Cargos pCargosEntities, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDCARGO = cmdTransaccionFactory.CreateParameter();
                        P_IDCARGO.ParameterName = "P_IDCARGO";
                        P_IDCARGO.Value = pCargosEntities.IdCargo;
                        P_IDCARGO.Direction = ParameterDirection.Input;
                        P_IDCARGO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDCARGO);

                        DbParameter P_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE.ParameterName = "P_NOMBRE";
                        P_NOMBRE.Value = pCargosEntities.Nombre;
                        P_NOMBRE.Direction = ParameterDirection.Input;
                        P_NOMBRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CARGO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCargosEntities;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "ModificarCARGO", ex);
                        return null;
                    }
                }
            }
        }

        public Cargos EliminarCargo(Cargos pCargosEntities, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDCARGO = cmdTransaccionFactory.CreateParameter();
                        P_IDCARGO.ParameterName = "P_IDCARGO";
                        P_IDCARGO.Value = pCargosEntities.IdCargo;
                        P_IDCARGO.Direction = ParameterDirection.Input;
                        P_IDCARGO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDCARGO);

                     
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CARGO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCargosEntities;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "EliminarrcARGOS", ex);
                        return null;
                    }
                }
            }
        }
        public List<Cargos> ListarCargos(String filtro,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cargos> lstConsulta = new List<Cargos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.*,'' as cod_cuenta,'' as nombre_cuenta FROM Cargo_nomina a where 1=1 " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cargos entidad = new Cargos();
                            if (resultado["IdCargo"] != DBNull.Value) entidad.IdCargo = Convert.ToInt64(resultado["IdCargo"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["DESCRIPCION"]);

                            lstConsulta.Add(entidad);

                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "ListarCargos", ex);
                        return null;
                    }
                }
            }
        }
     
        public Cargos ListarCargo(Int64 IdCargo,Usuario vUsuario)
        {
            DbDataReader resultado;
            Cargos entidad = new Cargos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cargo_nomina where IdCargo="+IdCargo;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                         
                            if (resultado["IdCargo"] != DBNull.Value) entidad.IdCargo = Convert.ToInt64(resultado["IdCargo"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["DESCRIPCION"]);
                        
                           

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "ListarCargos", ex);
                        return null;
                    }
                }
            }
        }
        public Int64 ConsultarMaxID( Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 Max = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT (MAX(IdCargo))+1 as Mas FROM Cargo_nomina order by IdCargo DESC ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            Max = Convert.ToInt64(resultado["mas"]);
                          
                           
                        }
                        else
                        {
                            Max = 0;
                        }
                     
                        dbConnectionFactory.CerrarConexion(connection);
                        return Max;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CargosData", "ConsultarMaxID", ex);
                        return 0;
                    }
                }
            }
        }
     

    }
}
