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
    public class CentroCostoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public CentroCostoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public CentroCosto CrearCentroCosto(CentroCosto pCentroCosto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pCentroCosto.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pnom_centro = cmdTransaccionFactory.CreateParameter();
                        pnom_centro.ParameterName = "p_nom_centro";
                        pnom_centro.Value = pCentroCosto.nom_centro;
                        pnom_centro.Direction = ParameterDirection.Input;
                        pnom_centro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_centro);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pCentroCosto.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pCentroCosto.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CENTRO_COS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCentroCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "CrearCentroCosto", ex);
                        return null;
                    }
                }
            }
        }

        public CentroCosto ModificarCentroCosto(CentroCosto pCentroCosto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pCentroCosto.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pnom_centro = cmdTransaccionFactory.CreateParameter();
                        pnom_centro.ParameterName = "p_nom_centro";
                        pnom_centro.Value = pCentroCosto.nom_centro;
                        pnom_centro.Direction = ParameterDirection.Input;
                        pnom_centro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_centro);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pCentroCosto.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        pprincipal.Value = pCentroCosto.principal;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CENTRO_COS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCentroCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "ModificarCentroCosto", ex);
                        return null;
                    }
                }
            }
        }

        public List<CentroCosto> ListarCentroCosto(CentroCosto pCentroCosto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CentroCosto> lstCentroCosto = new List<CentroCosto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM centro_costo " + ObtenerFiltro(pCentroCosto) + " ORDER BY centro_costo ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CentroCosto entidad = new CentroCosto();
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["NOM_CENTRO"] != DBNull.Value) entidad.nom_centro = Convert.ToString(resultado["NOM_CENTRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            lstCentroCosto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCentroCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "ListarCentroCosto", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCentroCosto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CentroCosto pCentroCosto = new CentroCosto();

                        DbParameter p_CentroCosto = cmdTransaccionFactory.CreateParameter();
                        p_CentroCosto.ParameterName = "p_centro_costo";
                        p_CentroCosto.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_CentroCosto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CENTRO_COS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        public CentroCosto ConsultarCentroCosto(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CentroCosto entidad = new CentroCosto();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM centro_costo WHERE centro_costo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["NOM_CENTRO"] != DBNull.Value) entidad.nom_centro = Convert.ToString(resultado["NOM_CENTRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
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
                        BOExcepcion.Throw("CentroCostoData", "ConsultarCentroCosto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar los centros de costo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<CentroCosto> ListarCentroCosto(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CentroCosto> lstCentroCosto = new List<CentroCosto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from centro_costo " + filtro + " Order by centro_costo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CentroCosto entidad = new CentroCosto();

                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["NOM_CENTRO"] != DBNull.Value) entidad.nom_centro = Convert.ToString(resultado["NOM_CENTRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            lstCentroCosto.Add(entidad);
                        }

                        return lstCentroCosto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "ListarCentroCosto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para determina rango de centro de costo
        /// </summary>
        /// <param name="CenIni">Devuelve el centro de costo mínimo</param>
        /// <param name="CenFin">Devuelve el centro de costo máximo</param>
        public void RangoCentroCosto(ref Int64 CenIni, ref Int64 CenFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select min(centro_costo) As CenIni, max(centro_costo) As CenFin from centro_costo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CENINI"] != DBNull.Value) CenIni = Convert.ToInt64(resultado["CENINI"]);
                            if (resultado["CENFIN"] != DBNull.Value) CenFin = Convert.ToInt64(resultado["CENFIN"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentroCostoData", "ListarCentroCosto", ex);
                        return;
                    }
                }
            }
        }


    }
}
