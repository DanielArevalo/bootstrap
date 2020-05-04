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
  
    public class AreaData:GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AreaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Area CrearArea(Area pAreaEntitie, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDAREA = cmdTransaccionFactory.CreateParameter();
                        P_IDAREA.ParameterName = "P_IDAREA";
                        P_IDAREA.Value = pAreaEntitie.IdArea;
                        P_IDAREA.Direction = ParameterDirection.Input;
                        P_IDAREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDAREA);

                        DbParameter P_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE.ParameterName = "P_NOMBRE";
                        P_NOMBRE.Value = pAreaEntitie.Nombre;
                        P_NOMBRE.Direction = ParameterDirection.Input;
                        P_NOMBRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE);

                        DbParameter P_COD_EMPRESA = cmdTransaccionFactory.CreateParameter();
                        P_COD_EMPRESA.ParameterName = "P_COD_EMPRESA";
                        P_COD_EMPRESA.Value = pAreaEntitie.CodEmpresa;
                        P_COD_EMPRESA.Direction = ParameterDirection.Input;
                        P_COD_EMPRESA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_EMPRESA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AREA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreaEntitie;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "CrearArea", ex);
                        return null;
                    }
                }
            }
        }

        public Area ModificarArea(Area pAreaEntitie, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDAREA = cmdTransaccionFactory.CreateParameter();
                        P_IDAREA.ParameterName = "P_IDAREA";
                        P_IDAREA.Value = pAreaEntitie.IdArea;
                        P_IDAREA.Direction = ParameterDirection.Input;
                        P_IDAREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDAREA);

                        DbParameter P_NOMBRE = cmdTransaccionFactory.CreateParameter();
                        P_NOMBRE.ParameterName = "P_NOMBRE";
                        P_NOMBRE.Value = pAreaEntitie.Nombre;
                        P_NOMBRE.Direction = ParameterDirection.Input;
                        P_NOMBRE.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NOMBRE);

                        DbParameter P_COD_EMPRESA = cmdTransaccionFactory.CreateParameter();
                        P_COD_EMPRESA.ParameterName = "P_COD_EMPRESA";
                        P_COD_EMPRESA.Value = pAreaEntitie.CodEmpresa;
                        P_COD_EMPRESA.Direction = ParameterDirection.Input;
                        P_COD_EMPRESA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_EMPRESA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AREA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreaEntitie;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "ModificarArea", ex);
                        return null;
                    }
                }
            }
        }

        public Area EliminarrArea(Area pAreaEntitie, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDAREA = cmdTransaccionFactory.CreateParameter();
                        P_IDAREA.ParameterName = "P_IDAREA";
                        P_IDAREA.Value = pAreaEntitie.IdArea;
                        P_IDAREA.Direction = ParameterDirection.Input;
                        P_IDAREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_IDAREA);

                     
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AREA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreaEntitie;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "EliminarrArea", ex);
                        return null;
                    }
                }
            }
        }
        public List<Area> ListarAreas(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Area> lstConsulta = new List<Area>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.*,'' as cod_cuenta,'' as nombre_cuenta FROM Area a order by IDAREA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Area entidad = new Area();
                            if (resultado["IDAREA"] != DBNull.Value) entidad.IdArea = Convert.ToInt64(resultado["IDAREA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.CodEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nombre_cuenta"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["nombre_cuenta"]);

                            lstConsulta.Add(entidad);

                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "ListarAreas", ex);
                        return null;
                    }
                }
            }
        }
        public List<Area> ListarAreasContable(Int64 idparametro,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Area> lstConsulta = new List<Area>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT A.*,ac.cod_cuenta,p.nombre AS nombre_cuenta,ac.idparametro,ac.consecutivo FROM Area A LEFT OUTER JOIN area_ctacontable ac ON A.idarea=ac.cod_area 
                                        LEFT OUTER JOIN plan_cuentas p ON ac.cod_cuenta=p.cod_cuenta where ac.idparametro=" + idparametro+" order by IDAREA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Area entidad = new Area();
                            if (resultado["IDAREA"] != DBNull.Value) entidad.IdArea = Convert.ToInt64(resultado["IDAREA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.CodEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nombre_cuenta"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["nombre_cuenta"]);
                            if (resultado["idparametro"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["idparametro"]);
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);

                            lstConsulta.Add(entidad);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "ListarAreasContable", ex);
                        return null;
                    }
                }
            }
        }

        public Area ListarArea(Int64 IdArea,Usuario vUsuario)
        {
            DbDataReader resultado;
            Area entidad = new Area();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Area where IdArea="+IdArea;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                         
                            if (resultado["IDAREA"] != DBNull.Value) entidad.IdArea = Convert.ToInt64(resultado["IDAREA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.CodEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"]);

                           

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "ListarAreas", ex);
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
                        string sql = @"SELECT (MAX(IDAREA))+1 as Mas FROM Area where 1=1 order by IDAREA DESC ";
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
                        BOExcepcion.Throw("AreaData", "ConsultarMaxID", ex);
                        return 0;
                    }
                }
            }
        }
        public Area Area_Ctacontable_crear(Area pAreaEntitie, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        PCONSECUTIVO.ParameterName = "PCONSECUTIVO";
                        PCONSECUTIVO.Value = pAreaEntitie.consecutivo;
                        PCONSECUTIVO.Direction = ParameterDirection.Output;
                        PCONSECUTIVO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCONSECUTIVO);

                        DbParameter PCOD_AREA = cmdTransaccionFactory.CreateParameter();
                        PCOD_AREA.ParameterName = "PCOD_AREA";
                        PCOD_AREA.Value = pAreaEntitie.IdArea;
                        PCOD_AREA.Direction = ParameterDirection.Input;
                        PCOD_AREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCOD_AREA);
                       
                        DbParameter PIDPARAMETRO = cmdTransaccionFactory.CreateParameter();
                        PIDPARAMETRO.ParameterName = "PIDPARAMETRO";
                        PIDPARAMETRO.Value = pAreaEntitie.idparametro;
                        PIDPARAMETRO.Direction = ParameterDirection.Input;
                        PIDPARAMETRO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PIDPARAMETRO);

                        DbParameter PCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PCOD_CUENTA.ParameterName = "PCOD_CUENTA";
                        PCOD_CUENTA.Value = pAreaEntitie.cod_cuenta;
                        PCOD_CUENTA.Direction = ParameterDirection.Input;
                        PCOD_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCOD_CUENTA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AREACONTABLE_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        pAreaEntitie.consecutivo = PCONSECUTIVO.Value != DBNull.Value ? Convert.ToInt64(PCONSECUTIVO.Value) : 0;

                        return pAreaEntitie;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "Area_Ctacontable_crear", ex);
                        return null;
                    }
                }
            }
        }
        public Area Area_Ctacontable_MOD(Area pAreaEntitie, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        PCONSECUTIVO.ParameterName = "PCONSECUTIVO";
                        PCONSECUTIVO.Value = pAreaEntitie.consecutivo;
                        PCONSECUTIVO.Direction = ParameterDirection.Input;
                        PCONSECUTIVO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCONSECUTIVO);

                        DbParameter PCOD_AREA = cmdTransaccionFactory.CreateParameter();
                        PCOD_AREA.ParameterName = "PCOD_AREA";
                        PCOD_AREA.Value = pAreaEntitie.IdArea;
                        PCOD_AREA.Direction = ParameterDirection.Input;
                        PCOD_AREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCOD_AREA);

                        DbParameter PIDPARAMETRO = cmdTransaccionFactory.CreateParameter();
                        PIDPARAMETRO.ParameterName = "PIDPARAMETRO";
                        PIDPARAMETRO.Value = pAreaEntitie.idparametro;
                        PIDPARAMETRO.Direction = ParameterDirection.Input;
                        PIDPARAMETRO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PIDPARAMETRO);

                        DbParameter PCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PCOD_CUENTA.ParameterName = "PCOD_CUENTA";
                        PCOD_CUENTA.Value = pAreaEntitie.cod_cuenta;
                        PCOD_CUENTA.Direction = ParameterDirection.Input;
                        PCOD_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCOD_CUENTA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_AREACONTABLE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAreaEntitie;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AreaData", "Area_Ctacontable_MOD", ex);
                        return null;
                    }
                }
            }
        }


    }
}
