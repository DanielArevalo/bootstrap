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
    public class SucursalBancariaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para centros de costo
        /// </summary>
        public SucursalBancariaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public SucursalBancaria CrearSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidsucursal = cmdTransaccionFactory.CreateParameter();
                        pidsucursal.ParameterName = "p_idsucursal";
                        pidsucursal.Value = 0;
                        pidsucursal.Direction = ParameterDirection.InputOutput;
                        pidsucursal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidsucursal);

                        DbParameter pcod_suc = cmdTransaccionFactory.CreateParameter();
                        pcod_suc.ParameterName = "p_cod_suc";
                        pcod_suc.Value = pSucursalBancaria.cod_suc;
                        pcod_suc.Direction = ParameterDirection.Input;
                        pcod_suc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_suc);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pSucursalBancaria.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnom_suc = cmdTransaccionFactory.CreateParameter();
                        pnom_suc.ParameterName = "p_nom_suc";
                        pnom_suc.Value = pSucursalBancaria.nom_suc;
                        pnom_suc.Direction = ParameterDirection.Input;
                        pnom_suc.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_suc);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pSucursalBancaria.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        pcodciudad.Value = pSucursalBancaria.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        pcodciudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        DbParameter pcod_int = cmdTransaccionFactory.CreateParameter();
                        pcod_int.ParameterName = "p_cod_int";
                        pcod_int.Value = pSucursalBancaria.cod_int;
                        pcod_int.Direction = ParameterDirection.Input;
                        pcod_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_int);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SUC_BAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSucursalBancaria.idsucursal = Convert.ToInt32(pidsucursal.Value);

                        return pSucursalBancaria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SucursalBancariaData", "CrearSucursalBancaria", ex);
                        return null;
                    }
                }
            }
        }

        public SucursalBancaria ModificarSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidsucursal = cmdTransaccionFactory.CreateParameter();
                        pidsucursal.ParameterName = "p_idsucursal";
                        pidsucursal.Value = pSucursalBancaria.idsucursal;
                        pidsucursal.Direction = ParameterDirection.InputOutput;
                        pidsucursal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidsucursal);

                        DbParameter pcod_suc = cmdTransaccionFactory.CreateParameter();
                        pcod_suc.ParameterName = "p_cod_suc";
                        pcod_suc.Value = pSucursalBancaria.cod_suc;
                        pcod_suc.Direction = ParameterDirection.Input;
                        pcod_suc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_suc);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pSucursalBancaria.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnom_suc = cmdTransaccionFactory.CreateParameter();
                        pnom_suc.ParameterName = "p_nom_suc";
                        pnom_suc.Value = pSucursalBancaria.nom_suc;
                        pnom_suc.Direction = ParameterDirection.Input;
                        pnom_suc.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnom_suc);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        pdireccion.Value = pSucursalBancaria.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        pdireccion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        pcodciudad.Value = pSucursalBancaria.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        pcodciudad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        DbParameter pcod_int = cmdTransaccionFactory.CreateParameter();
                        pcod_int.ParameterName = "p_cod_int";
                        pcod_int.Value = pSucursalBancaria.cod_int;
                        pcod_int.Direction = ParameterDirection.Input;
                        pcod_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_int);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SUC_BAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSucursalBancaria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SucursalBancariaData", "ModificarSucursalBancaria", ex);
                        return null;
                    }
                }
            }
        }

        public List<SucursalBancaria> ListarSucursalBancaria(SucursalBancaria pSucursalBancaria, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SucursalBancaria> lstSucursalBancaria = new List<SucursalBancaria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT s.*, b.nombrebanco, c.nomciudad FROM suc_ban s LEFT JOIN bancos b ON s.cod_banco = b.cod_banco LEFT JOIN ciudades c ON s.codciudad = c.codciudad " + ObtenerFiltro(pSucursalBancaria) + " ORDER BY IDSUCURSAL ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SucursalBancaria entidad = new SucursalBancaria();
                            if (resultado["IDSUCURSAL"] != DBNull.Value) entidad.idsucursal = Convert.ToInt32(resultado["IDSUCURSAL"]);
                            if (resultado["COD_SUC"] != DBNull.Value) entidad.cod_suc = Convert.ToInt32(resultado["COD_SUC"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["NOM_SUC"] != DBNull.Value) entidad.nom_suc = Convert.ToString(resultado["NOM_SUC"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt32(resultado["CODCIUDAD"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["COD_INT"] != DBNull.Value) entidad.cod_int = Convert.ToInt32(resultado["COD_INT"]);
                            lstSucursalBancaria.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSucursalBancaria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SucursalBancariaData", "ListarSucursalBancaria", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarSucursalBancaria(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SucursalBancaria pSucursalBancaria = new SucursalBancaria();
                        DbParameter pidsucursal = cmdTransaccionFactory.CreateParameter();
                        pidsucursal.ParameterName = "p_idsucursal";
                        pidsucursal.Value = pSucursalBancaria.idsucursal;
                        pidsucursal.Direction = ParameterDirection.Input;
                        pidsucursal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidsucursal);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SUCURSALBANCARIA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        public SucursalBancaria ConsultarSucursalBancaria(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SucursalBancaria entidad = new SucursalBancaria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM suc_ban WHERE IDSUCURSAL = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDSUCURSAL"] != DBNull.Value) entidad.idsucursal = Convert.ToInt32(resultado["IDSUCURSAL"]);
                            if (resultado["COD_SUC"] != DBNull.Value) entidad.cod_suc = Convert.ToInt32(resultado["COD_SUC"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NOM_SUC"] != DBNull.Value) entidad.nom_suc = Convert.ToString(resultado["NOM_SUC"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt32(resultado["CODCIUDAD"]);
                            if (resultado["COD_INT"] != DBNull.Value) entidad.cod_int = Convert.ToInt32(resultado["COD_INT"]);
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
                        BOExcepcion.Throw("SucursalBancariaData", "ConsultarSucursalBancaria", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(idsucursal) + 1 FROM suc_ban ";

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
                        BOExcepcion.Throw("SucursalBancariaData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

 
    }
}
