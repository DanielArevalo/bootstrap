using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para centros de costo
    /// </summary>    
    public class CentroGestionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

       
        public CentroGestionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CentroGestion Crear_Mod_CentroGestion(CentroGestion pCentro, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcentro_gestion = cmdTransaccionFactory.CreateParameter();
                        pcentro_gestion.ParameterName = "p_centro_gestion";
                        pcentro_gestion.Value = pCentro.centro_gestion;
                        if (opcion == 1) // crear
                            pcentro_gestion.Direction = ParameterDirection.Output;
                        else
                            pcentro_gestion.Direction = ParameterDirection.Input;
                        pcentro_gestion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_gestion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pCentro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        pnivel.Value = pCentro.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pCentro.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "p_depende_de";
                        if (pCentro.depende_de != 0) pdepende_de.Value = pCentro.depende_de; else pdepende_de.Value = DBNull.Value;
                        pdepende_de.Direction = ParameterDirection.Input;
                        pdepende_de.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1) // Crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CENTRO_GES_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CENTRO_GES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1)
                            pCentro.centro_gestion = Convert.ToInt32(pcentro_gestion.Value);
                        return pCentro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroGestionData", "Crear_Mod_CentroGestion", ex);
                        return null;
                    }
                }
            }
        }


        public List<CentroGestion> ListarCentroGestion(CentroGestion pCentro,String filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CentroGestion> lstConsulta = new List<CentroGestion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.*, C.DEPENDE_DE ||'-'|| (SELECT G.NOMBRE FROM CENTRO_GESTION G WHERE C.DEPENDE_DE = G.CENTRO_GESTION) AS NOM_DEPENDE "
                                     +"FROM CENTRO_GESTION C ";
                        if (filtro != "")
                            sql += filtro;
                        sql += " ORDER BY C.CENTRO_GESTION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CentroGestion entidad = new CentroGestion();
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt32(resultado["CENTRO_GESTION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt32(resultado["NIVEL"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt32(resultado["DEPENDE_DE"]);
                            if (resultado["NOM_DEPENDE"] != DBNull.Value) entidad.nom_depende = Convert.ToString(resultado["NOM_DEPENDE"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroGestionData", "ListarCentroGestion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM CENTRO_GESTION WHERE Centro_Gestion = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroGestionData", "EliminarCentroGestion", ex);
                    }
                }
            }
        }


        public CentroGestion ConsultarCentroGestion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CentroGestion entidad = new CentroGestion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CENTRO_GESTION WHERE CENTRO_GESTION = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt32(resultado["CENTRO_GESTION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt32(resultado["NIVEL"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToInt32(resultado["DEPENDE_DE"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroGestionData", "ConsultarCentroGestion", ex);
                        return null;
                    }
                }
            }
        }

    }
}
