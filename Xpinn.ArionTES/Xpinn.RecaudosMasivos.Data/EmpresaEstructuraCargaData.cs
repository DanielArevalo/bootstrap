using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Data
{
    public class EmpresaEstructuraCargaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EmpresaEstructuraCargaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public EmpresaEstructuraCarga CrearEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodemparchivo = cmdTransaccionFactory.CreateParameter();
                        pcodemparchivo.ParameterName = "p_codemparchivo";
                        pcodemparchivo.Value = pEmpresaEstructuraCarga.codemparchivo;
                        pcodemparchivo.Direction = ParameterDirection.Output;
                        pcodemparchivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodemparchivo);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresaEstructuraCarga.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pEmpresaEstructuraCarga.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pEmpresaEstructuraCarga.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pEmpresaEstructuraCarga.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPESTCARGA_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEmpresaEstructuraCarga.codemparchivo = Convert.ToInt64(pcodemparchivo.Value);

                        return pEmpresaEstructuraCarga;
                    }
                    catch (Exception ex)
                    {
                        // BOExcepcion.Throw("EmpresaEstructuraCargaData", "CrearEmpresaEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }


        public EmpresaEstructuraCarga ModificarEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodemparchivo = cmdTransaccionFactory.CreateParameter();
                        pcodemparchivo.ParameterName = "p_codemparchivo";
                        pcodemparchivo.Value = pEmpresaEstructuraCarga.codemparchivo;
                        pcodemparchivo.Direction = ParameterDirection.Input;
                        pcodemparchivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodemparchivo);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresaEstructuraCarga.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcod_estructura_carga = cmdTransaccionFactory.CreateParameter();
                        pcod_estructura_carga.ParameterName = "p_cod_estructura_carga";
                        pcod_estructura_carga.Value = pEmpresaEstructuraCarga.cod_estructura_carga;
                        pcod_estructura_carga.Direction = ParameterDirection.Input;
                        pcod_estructura_carga.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_estructura_carga);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pEmpresaEstructuraCarga.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pEmpresaEstructuraCarga.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPESTCARGA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEmpresaEstructuraCarga;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaEstructuraCargaData", "ModificarEmpresaEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarEmpresaEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EmpresaEstructuraCarga pEmpresaEstructuraCarga = new EmpresaEstructuraCarga();
                        pEmpresaEstructuraCarga = ConsultarEmpresaEstructuraCarga(pId, vUsuario);

                        DbParameter pcodemparchivo = cmdTransaccionFactory.CreateParameter();
                        pcodemparchivo.ParameterName = "p_codemparchivo";
                        pcodemparchivo.Value = pEmpresaEstructuraCarga.codemparchivo;
                        pcodemparchivo.Direction = ParameterDirection.Input;
                        pcodemparchivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodemparchivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EMPESTCARGA_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaEstructuraCargaData", "EliminarEmpresaEstructuraCarga", ex);
                    }
                }
            }
        }


        public List<EmpresaEstructuraCarga> ListarEmpresaEstructuraCarga(EmpresaEstructuraCarga pEmpresaEstructuraCarga, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaEstructuraCarga> lstEmpresaEstructuraCarga = new List<EmpresaEstructuraCarga>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa_Estructura_Carga " + ObtenerFiltro(pEmpresaEstructuraCarga) + " ORDER BY CODEMPARCHIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaEstructuraCarga entidad = new EmpresaEstructuraCarga();
                            if (resultado["CODEMPARCHIVO"] != DBNull.Value) entidad.codemparchivo = Convert.ToInt64(resultado["CODEMPARCHIVO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["COD_ESTRUCTURA_CARGA"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt64(resultado["COD_ESTRUCTURA_CARGA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            lstEmpresaEstructuraCarga.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaEstructuraCarga;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaEstructuraCargaData", "ListarEmpresaEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }



        public EmpresaEstructuraCarga ConsultarEmpresaEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            EmpresaEstructuraCarga entidad = new EmpresaEstructuraCarga();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Empresa_Estructura_Carga WHERE CODEMPARCHIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODEMPARCHIVO"] != DBNull.Value) entidad.codemparchivo = Convert.ToInt64(resultado["CODEMPARCHIVO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["COD_ESTRUCTURA_CARGA"] != DBNull.Value) entidad.cod_estructura_carga = Convert.ToInt64(resultado["COD_ESTRUCTURA_CARGA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
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
                        BOExcepcion.Throw("EmpresaEstructuraCargaData", "ConsultarEmpresaEstructuraCarga", ex);
                        return null;
                    }
                }
            }
        }

    }
}




