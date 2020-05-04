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
    public class EmpresaExcluyenteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EmpresaExcluyenteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public EmpresaExcluyente CrearEmpresaExcluyente(EmpresaExcluyente pEmpresa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidempexcluyente = cmdTransaccionFactory.CreateParameter();
                        pidempexcluyente.ParameterName = "p_idempexcluyente";
                        pidempexcluyente.Value = pEmpresa.idempexcluyente;
                        pidempexcluyente.Direction = ParameterDirection.Output;
                        pidempexcluyente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidempexcluyente);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pEmpresa.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcod_empresa_excluye = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa_excluye.ParameterName = "p_cod_empresa_excluye";
                        pcod_empresa_excluye.Value = pEmpresa.cod_empresa_excluye;
                        pcod_empresa_excluye.Direction = ParameterDirection.Input;
                        pcod_empresa_excluye.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa_excluye);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_EXCLUYENTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        pEmpresa.idempexcluyente = Convert.ToInt32(pidempexcluyente.Value);
                        return pEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaExcluyenteData", "CrearEmpresaExcluyente", ex);
                        return null;
                    }
                }
            }
        }


        public List<EmpresaExcluyente> ListarEmpresaExcluyente(Int32 cod_empresa, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaExcluyente> lstEmpresa = new List<EmpresaExcluyente>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT E.Cod_Empresa,E.Nom_Empresa,Ee.Cod_Empresa_Excluye,Ee.Idempexcluyente "
                                        + "FROM Empresa_Recaudo E LEFT JOIN Empresa_Recaudo_Excluyente Ee ON E.Cod_Empresa = Ee.Cod_Empresa_Excluye AND Ee.Cod_Empresa = " + cod_empresa.ToString()
                                         +" ORDER BY 1 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaExcluyente entidad = new EmpresaExcluyente();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["COD_EMPRESA_EXCLUYE"] != DBNull.Value) entidad.cod_empresa_excluye = Convert.ToInt32(resultado["COD_EMPRESA_EXCLUYE"]);
                            if (entidad.cod_empresa_excluye != 0)
                                entidad.seleccionar = 1;
                            else
                                entidad.seleccionar = 0;
                            if (resultado["IDEMPEXCLUYENTE"] != DBNull.Value) entidad.idempexcluyente = Convert.ToInt32(resultado["IDEMPEXCLUYENTE"]);
                            
                            if (cod_empresa != entidad.cod_empresa)
                                lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaExcluyenteData", "ListarEmpresaExcluyente", ex);
                        return null;
                    }
                }
            }
        }

        //CONSULTAR EMPRESAS EXCLUYENTES DE MODO INVERSO
        public List<EmpresaExcluyente> ListarEmpresaExcluyenteINV(Int32 cod_empresa, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<EmpresaExcluyente> lstEmpresa = new List<EmpresaExcluyente>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT Ee.Cod_Empresa,E.Nom_Empresa, Ee.Cod_Empresa_Excluye,Ee.Idempexcluyente "
                                        +"FROM Empresa_Recaudo E LEFT JOIN Empresa_Recaudo_Excluyente Ee "
                                        +"ON E.Cod_Empresa = Ee.Cod_Empresa "
                                        +"WHERE Cod_Empresa_Excluye = " + cod_empresa.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EmpresaExcluyente entidad = new EmpresaExcluyente();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["COD_EMPRESA_EXCLUYE"] != DBNull.Value) entidad.cod_empresa_excluye = Convert.ToInt32(resultado["COD_EMPRESA_EXCLUYE"]);
                            if (entidad.cod_empresa_excluye != 0)
                                entidad.seleccionar = 1;
                            else
                                entidad.seleccionar = 0;
                            if (resultado["IDEMPEXCLUYENTE"] != DBNull.Value) entidad.idempexcluyente = Convert.ToInt32(resultado["IDEMPEXCLUYENTE"]);

                            if (cod_empresa != entidad.cod_empresa)
                                lstEmpresa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaExcluyenteData", "ListarEmpresaExcluyenteINV", ex);
                        return null;
                    }
                }
            }
        }

        
        public void EliminarEmpresaExcluyente(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM Empresa_Recaudo_Excluyente WHERE COD_EMPRESA = "+pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EmpresaEstructuraCargaData", "EliminarEmpresaExcluyente", ex);
                    }
                }
            }
        }


    }
}




