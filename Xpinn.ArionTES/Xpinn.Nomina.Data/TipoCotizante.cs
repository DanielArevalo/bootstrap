using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Data
{
    public class TipoCotizanteNominaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public TipoCotizanteNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public TipoCotizante CrearTipoCotizante(TipoCotizante pTipoCotizante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo= cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pTipoCotizante.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                       
                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTipoCotizante.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pTipoCotizante.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter PPORCENTAJE_SALUD = cmdTransaccionFactory.CreateParameter();
                        PPORCENTAJE_SALUD.ParameterName = "p_PORCENTAJE_SALUD";
                        PPORCENTAJE_SALUD.Value = pTipoCotizante.porcentaje_salud;
                        PPORCENTAJE_SALUD.Direction = ParameterDirection.Input;
                        PPORCENTAJE_SALUD.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PPORCENTAJE_SALUD);

                        DbParameter PPORCENTAJE_PENSION = cmdTransaccionFactory.CreateParameter();
                        PPORCENTAJE_PENSION.ParameterName = "P_PORCENTAJE_PENSION";
                        PPORCENTAJE_PENSION.Value = pTipoCotizante.porcentaje_pension;
                        PPORCENTAJE_PENSION.Direction = ParameterDirection.Input;
                        PPORCENTAJE_PENSION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PPORCENTAJE_PENSION);



                        DbParameter P_PAGASUBSIDIO = cmdTransaccionFactory.CreateParameter();
                        P_PAGASUBSIDIO.ParameterName = "P_PAGASUBSIDIO";
                        P_PAGASUBSIDIO.Value = pTipoCotizante.paga_subsidio;
                        P_PAGASUBSIDIO.Direction = ParameterDirection.Input;
                        P_PAGASUBSIDIO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_PAGASUBSIDIO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_TIPOCOTIZ_CREAR";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pconsecutivo.Value != null)
                            pTipoCotizante.consecutivo = Convert.ToInt64(pconsecutivo.Value);


                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoCotizante;
                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCotizanteData", "CrearTipoCotizante", ex);
                        return null;
                    }
                }
            }
        }
      
    
        public List<TipoCotizante> ListarTipoCotizante(Usuario usuario)
        {
            DbDataReader resultado;
            List<TipoCotizante> listaEntidades = new List<TipoCotizante>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoCotizante ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoCotizante entidad = new TipoCotizante();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORCENTAJE_SALUD"] != DBNull.Value) entidad.porcentaje_salud = Convert.ToDecimal(resultado["PORCENTAJE_SALUD"]);

                            if (resultado["PORCENTAJE_PENSION"] != DBNull.Value) entidad.porcentaje_pension = Convert.ToDecimal(resultado["PORCENTAJE_PENSION"]);

                            if (resultado["PAGASUBSIDIO"] != DBNull.Value) entidad.paga_subsidio = Convert.ToDecimal(resultado["PAGASUBSIDIO"]);


                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCotizanteData", "ListarTipoContratos", ex);
                        return null;
                    }
                }
            }
        }

        public TipoCotizante ModificarTipoCotizante(TipoCotizante pTipoCotizante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pTipoCotizante.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);


                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTipoCotizante.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pTipoCotizante.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);


                        DbParameter PPORCENTAJE_SALUD = cmdTransaccionFactory.CreateParameter();
                        PPORCENTAJE_SALUD.ParameterName = "p_PORCENTAJE_SALUD";
                        PPORCENTAJE_SALUD.Value = pTipoCotizante.porcentaje_salud;
                        PPORCENTAJE_SALUD.Direction = ParameterDirection.Input;
                        PPORCENTAJE_SALUD.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PPORCENTAJE_SALUD);

                        DbParameter PPORCENTAJE_PENSION = cmdTransaccionFactory.CreateParameter();
                        PPORCENTAJE_PENSION.ParameterName = "P_PORCENTAJE_PENSION";
                        PPORCENTAJE_PENSION.Value = pTipoCotizante.porcentaje_pension;
                        PPORCENTAJE_PENSION.Direction = ParameterDirection.Input;
                        PPORCENTAJE_PENSION.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(PPORCENTAJE_PENSION);


                        DbParameter P_PAGASUBSIDIO = cmdTransaccionFactory.CreateParameter();
                        P_PAGASUBSIDIO.ParameterName = "P_PAGASUBSIDIO";
                        P_PAGASUBSIDIO.Value = pTipoCotizante.paga_subsidio;
                        P_PAGASUBSIDIO.Direction = ParameterDirection.Input;
                        P_PAGASUBSIDIO.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_PAGASUBSIDIO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_TIPOCOTIZ_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        return pTipoCotizante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCotizanteData", "ModificaTipoCotizante", ex);
                        return null;
                    }
                }
            }
        }
        
        public TipoCotizante ConsultarTipoCotizante(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoCotizante entidad = new TipoCotizante();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoCotizante WHERE consecutivo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORCENTAJE_SALUD"] != DBNull.Value) entidad.porcentaje_salud = Convert.ToDecimal(resultado["PORCENTAJE_SALUD"]);

                            if (resultado["PORCENTAJE_PENSION"] != DBNull.Value) entidad.porcentaje_pension= Convert.ToDecimal(resultado["PORCENTAJE_PENSION"]);
                            if (resultado["PAGASUBSIDIO"] != DBNull.Value) entidad.paga_subsidio = Convert.ToDecimal(resultado["PAGASUBSIDIO"]);

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
                        BOExcepcion.Throw("TipoCotizanteData", "ConsultarTipoCotizante", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<TipoCotizante> ListarTipoCotizante(string pid, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoCotizante> lstContratacion = new List<TipoCotizante>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoCotizante " + pid.ToString() + " ORDER BY consecutivo ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoCotizante entidad = new TipoCotizante();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORCENTAJE_SALUD"] != DBNull.Value) entidad.porcentaje_salud = Convert.ToDecimal(resultado["PORCENTAJE_SALUD"]);

                            if (resultado["PORCENTAJE_PENSION"] != DBNull.Value) entidad.porcentaje_pension = Convert.ToDecimal(resultado["PORCENTAJE_PENSION"]);
                            if (resultado["PAGASUBSIDIO"] != DBNull.Value) entidad.paga_subsidio = Convert.ToDecimal(resultado["PAGASUBSIDIO"]);



                            lstContratacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstContratacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCotizanteData", "ListarTipoCotizante", ex);
                        return null;
                    }
                }
            }
        }
       

    }
}