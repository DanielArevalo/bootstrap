using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class CiudadData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public CiudadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Ciudad Crear_Mod_Ciudad(Ciudad pCiudad, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        pcodciudad.Value = pCiudad.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        pcodciudad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        DbParameter pnomciudad = cmdTransaccionFactory.CreateParameter();
                        pnomciudad.ParameterName = "p_nomciudad";
                        pnomciudad.Value = pCiudad.nomciudad;
                        pnomciudad.Direction = ParameterDirection.Input;
                        pnomciudad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomciudad);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pCiudad.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "p_depende_de";
                        pdepende_de.Value = pCiudad.depende_de;
                        pdepende_de.Direction = ParameterDirection.Input;
                        pdepende_de.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CIUDADES_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_CIUDADES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                                                
                        return pCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "Crear_Mod_Ciudad", ex);
                        return null;
                    }
                }
            }
        }


        public Ciudad ConsultarCiudad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Ciudad entidad = new Ciudad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CIUDADES WHERE CODCIUDAD = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt64(resultado["DEPENDE_DE"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ConsultarCiudad", ex);
                        return null;
                    }
                }
            }
        }



        public List<Ciudad> ListarCiudad(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Ciudad> lstCiudad = new List<Ciudad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Ciudades.*, Case Tipo When 1 Then 'País' When 2 Then 'Departamento/Estado' "
                                     +"when 3 then 'Ciudad' when 4 then 'Municipio' when 5 then 'Zona' when 6 then 'Barrio' end as NomTipo "
                                     +"FROM CIUDADES where 1 = 1" + filtro + " ORDER BY CODCIUDAD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Ciudad entidad = new Ciudad();
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt64(resultado["DEPENDE_DE"]);
                            if (resultado["NOMTIPO"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["NOMTIPO"]);
                            lstCiudad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCiudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "ListarCiudad", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCiudad(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Delete from Ciudades where CODCIUDAD = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiudadData", "EliminarCiudad", ex);
                    }
                }
            }
        }

    }
}
