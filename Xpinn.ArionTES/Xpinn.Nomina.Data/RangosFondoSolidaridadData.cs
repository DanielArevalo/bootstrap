using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class RangosFondoSolidaridadData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public RangosFondoSolidaridadData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public RangosFondoSolidaridad CrearRangosFondoSolidaridad(RangosFondoSolidaridad pRangosFondoSolidaridad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo= cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRangosFondoSolidaridad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                       
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRangosFondoSolidaridad.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRangosFondoSolidaridad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pRangosFondoSolidaridad.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pRangosFondoSolidaridad.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pminimo);



                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangosFondoSolidaridad.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangosFondoSolidaridad.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);


                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pRangosFondoSolidaridad.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pRangosFondoSolidaridad.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_RAN_FONSOL_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pRangosFondoSolidaridad.consecutivo = Convert.ToInt64(pconsecutivo.Value);


                        dbConnectionFactory.CerrarConexion(connection);
                        return pRangosFondoSolidaridad;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosFondoSolidaridadData", "CrearRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }
      
    
        public List<RangosFondoSolidaridad> ListarRangosFondoSolidaridad(Usuario usuario)
        {
            DbDataReader resultado;
            List<RangosFondoSolidaridad> listaEntidades = new List<RangosFondoSolidaridad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_FON_SOL ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RangosFondoSolidaridad entidad = new RangosFondoSolidaridad();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);


                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosFondoSolidaridadData", "ListarRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }

        public RangosFondoSolidaridad ModificarRangosFondoSolidaridad(RangosFondoSolidaridad pRangosFondoSolidaridad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRangosFondoSolidaridad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);


                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRangosFondoSolidaridad.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRangosFondoSolidaridad.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pRangosFondoSolidaridad.minimo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pminimo.Value = pRangosFondoSolidaridad.minimo;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangosFondoSolidaridad.maximo == null)
                            pminimo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangosFondoSolidaridad.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);


                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pRangosFondoSolidaridad.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pRangosFondoSolidaridad.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);






                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_RANGOS_FON_SOL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        return pRangosFondoSolidaridad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosFondoSolidaridadData", "ModificarRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }
        
        public RangosFondoSolidaridad ConsultarRangosFondoSolidaridad(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            RangosFondoSolidaridad entidad = new RangosFondoSolidaridad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_FON_SOL WHERE consecutivo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo= Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);


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
                        BOExcepcion.Throw("RangosFondoSolidaridadData", "ConsultarRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<RangosFondoSolidaridad> ListarRangosFondoSolidaridad(string pid, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RangosFondoSolidaridad> lstRangos = new List<RangosFondoSolidaridad>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_FON_SOL " + pid.ToString() + " ORDER BY consecutivo ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RangosFondoSolidaridad entidad = new RangosFondoSolidaridad();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);



                            lstRangos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosFondoSolidaridadData", "ListarRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }
       

    }
}