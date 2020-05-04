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
    public class ParametrosAporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ParametrosAporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public ParametrosAporte CrearPar_Cue_LinApo(ParametrosAporte pParametro, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigoInt = cmdTransaccionFactory.CreateParameter();
                        pcodigoInt.ParameterName = "p_codigoInt";
                        if (pParametro.codigoInt != 0) pcodigoInt.Value = pParametro.codigoInt; else pcodigoInt.Value = DBNull.Value;
                        pcodigoInt.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigoInt);

                        DbParameter pcodigoStr = cmdTransaccionFactory.CreateParameter();
                        pcodigoStr.ParameterName = "p_codigoStr";
                        if (pParametro.codigoStr != null) pcodigoStr.Value = pParametro.codigoStr; else pcodigoStr.Value = DBNull.Value; 
                        pcodigoStr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodigoStr);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pParametro.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_opcion = cmdTransaccionFactory.CreateParameter();
                        pcod_opcion.ParameterName = "p_cod_opcion";
                        pcod_opcion.Value = pParametro.cod_opcion;
                        pcod_opcion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_opcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PARAMETROS_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PARAMETROS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParametro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("APOData", "CrearPar_Cue_LinApo", ex);
                        return null;
                    }
                }
            }
        }



        public List<ParametrosAporte> ListarParametrosAporte(string filtro,string orden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrosAporte> lstConsulta = new List<ParametrosAporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT VPARAMETROS.*,  case cod_opcion when 1 then 'Actividad' when 2 then 'Tipo Identificación' " 
                                        + "when 3 then 'Cargo' when 4 then 'Nivel Escolaridad' when 5 then 'Estado Civil' when 6 then 'Tipo Contrato' when 7 then 'Parentesco' when 8 then 'Tipos Documento' end as nom_opcion "
                                        + "FROM VPARAMETROS WHERE 1 = 1 " + filtro;

                        if (orden != "")
                            sql += " ORDER BY " + orden;
                        else
                            sql += " ORDER BY CODIGO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ParametrosAporte entidad = new ParametrosAporte();
                            if (resultado["COD_OPCION"] != DBNull.Value) entidad.cod_opcion = Convert.ToInt32(resultado["COD_OPCION"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigoStr = Convert.ToString(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]).Trim();
                            if (resultado["NOM_OPCION"] != DBNull.Value) entidad.nom_opcion = Convert.ToString(resultado["NOM_OPCION"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAporteData", "ListarParametrosAporte", ex);
                        return null;
                    }
                }
            }
        }




        public void EliminarParametroAporte(string pId,int opcion,Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (opcion == 1)
                            sql = "DELETE FROM ACTIVIDAD WHERE CODACTIVIDAD = '" + pId.ToString() + "'";
                        else if (opcion == 2)
                            sql = "DELETE FROM TIPOIDENTIFICACION WHERE codtipoidentificacion = " + pId.ToString();
                        else if (opcion == 3)
                            sql = "DELETE FROM CARGO WHERE codcargo = " + pId.ToString();
                        else if (opcion == 4)
                            sql = "DELETE FROM NIVELESCOLARIDAD WHERE codescolaridad = " + pId.ToString();
                        else if (opcion == 5)
                            sql = "DELETE FROM ESTADOCIVIL WHERE codestadocivil = " + pId.ToString();
                        else if (opcion == 6)
                            sql = "DELETE FROM TIPOCONTRATO WHERE codtipocontrato =" + pId.ToString();
                        else if (opcion == 7)
                            sql = "DELETE FROM PARENTESCOS WHERE codparentesco = " + pId.ToString();
                        else if (opcion == 8)
                            sql = "DELETE FROM TIPOSDOCUMENTO WHERE tipo_documento = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAporteData", "EliminarParametroAporte", ex);
                    }
                }
            }
        }



        public Int32 ObtenerSiguienteCodigo(int opcion,Usuario pUsuario)
        {
            Int32 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "";
                        if (opcion == 1)
                            sql = "select MAX(codactividad)+1 as numero from actividad";
                        else if (opcion == 2)
                            sql = "select MAX(codtipoidentificacion)+1 as numero from TIPOIDENTIFICACION";
                        else if (opcion == 3)
                            sql = "select MAX(codcargo)+1 as numero from CARGO";
                        else if (opcion == 4)
                            sql = "select MAX(codescolaridad)+1 as numero from NIVELESCOLARIDAD";
                        else if (opcion == 5)
                            sql = "select MAX(codestadocivil)+1 as numero from ESTADOCIVIL";
                        else if (opcion == 6)
                            sql = "select MAX(codtipocontrato)+ 1 as numero from TIPOCONTRATO";
                        else if (opcion == 7)
                            sql = "select MAX(codparentesco) +1 as numero from PARENTESCOS";
                        else if (opcion == 8)
                            sql = "select MAX(tipo_documento) +1 as numero from TIPOSDOCUMENTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt32(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAporteData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public ParametrosAporte ConsultarParametrosAporte(string pId,int opcion, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametrosAporte entidad = new ParametrosAporte();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (opcion == 1)
                            sql = "select codactividad  as CODIGO, descripcion from actividad where codactividad = '" + pId.ToString() + "'";
                        else if (opcion == 2)
                            sql = "select codtipoidentificacion as CODIGO, descripcion from TIPOIDENTIFICACION where codtipoidentificacion = " + pId.ToString();
                        else if (opcion == 3)
                            sql = "select codcargo as CODIGO,descripcion from CARGO where codcargo = " + pId.ToString();
                        else if (opcion == 4)
                            sql = "select codescolaridad as CODIGO , descripcion from NIVELESCOLARIDAD where codescolaridad = " + pId.ToString();
                        else if (opcion == 5)
                            sql = "select codestadocivil as CODIGO , descripcion from ESTADOCIVIL where codestadocivil = " + pId.ToString();
                        else if (opcion == 6)
                            sql = "select codtipocontrato as CODIGO , descripcion from TIPOCONTRATO where codtipocontrato = " + pId.ToString();
                        else if (opcion == 7)
                            sql = "select codparentesco as CODIGO , descripcion from PARENTESCOS where codparentesco =" + pId.ToString();
                        else if (opcion == 8)
                            sql = "select tipo_documento as CODIGO , descripcion from TIPOSDOCUMENTO where tipo_documento =" + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigoStr = Convert.ToString(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosAporteData", "ConsultarParametrosAporte", ex);
                        return null;
                    }
                }
            }
        }



    }
}
