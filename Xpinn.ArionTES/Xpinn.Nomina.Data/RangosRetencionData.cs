using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class RangosRetencionData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public RangosRetencionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public RangosRetencion CrearRangosRetencion(RangosRetencion pRangosRetencion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo= cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRangosRetencion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                       
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRangosRetencion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRangosRetencion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


              


                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangosRetencion.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangosRetencion.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);


                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pRangosRetencion.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pRangosRetencion.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_RAN_RETEN_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pRangosRetencion.consecutivo = Convert.ToInt64(pconsecutivo.Value);


                        dbConnectionFactory.CerrarConexion(connection);
                        return pRangosRetencion;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosRetencionData", "CrearRangosFondoSolidaridad", ex);
                        return null;
                    }
                }
            }
        }
      
    
        public List<RangosRetencion> ListarRangosRetencion(Usuario usuario)
        {
            DbDataReader resultado;
            List<RangosRetencion> listaEntidades = new List<RangosRetencion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_RETENCION ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RangosRetencion entidad = new RangosRetencion();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);


                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosRetencionData", "ListarRangosRetencion", ex);
                        return null;
                    }
                }
            }
        }

        public RangosRetencion ModificarRangosRetencion(RangosRetencion pRangosRetencion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pRangosRetencion.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);


                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pRangosRetencion.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pRangosRetencion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pRangosRetencion.maximo == null)
                            pmaximo.Value = DBNull.Value;
                        else
                            pmaximo.Value = pRangosRetencion.maximo;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);


                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pRangosRetencion.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pRangosRetencion.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);


                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_RANGOS_RETEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        return pRangosRetencion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosRetencionData", "ModificarRangosRetencion", ex);
                        return null;
                    }
                }
            }
        }
        
        public RangosRetencion ConsultarRangosRetencion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            RangosRetencion entidad = new RangosRetencion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_RETENCION WHERE consecutivo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("RangosRetencionData", "ConsultarRangosrETENCION", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<RangosRetencion> ListarRangosRetencion(string pid, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RangosRetencion> lstRangos = new List<RangosRetencion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RANGOS_RETENCION" + pid.ToString() + " ORDER BY consecutivo ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RangosRetencion entidad = new RangosRetencion();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                             if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);



                            lstRangos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RangosRetencionData", "ListarRangosRetencion", ex);
                        return null;
                    }
                }
            }
        }
       

    }
}