using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
   
    public class TipoActivoNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public TipoActivoNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public TipoActivoNIF CrearTipoActivoNIF(TipoActivoNIF pActivo, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_activo_nif = cmdTransaccionFactory.CreateParameter();
                        ptipo_activo_nif.ParameterName = "p_tipo_activo_nif";
                        ptipo_activo_nif.Value = pActivo.tipo_activo_nif;
                        if(opcion == 1)//crear
                            ptipo_activo_nif.Direction = ParameterDirection.Output;
                        else
                            ptipo_activo_nif.Direction = ParameterDirection.Input;
                        ptipo_activo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_activo_nif);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pActivo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcodclasificacion_nif = cmdTransaccionFactory.CreateParameter();
                        pcodclasificacion_nif.ParameterName = "p_codclasificacion_nif";
                        pcodclasificacion_nif.Value = pActivo.codclasificacion_nif;
                        pcodclasificacion_nif.Direction = ParameterDirection.Input;
                        pcodclasificacion_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodclasificacion_nif);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pActivo.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_deterioro = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_deterioro.ParameterName = "p_cod_cuenta_deterioro";
                        if (pActivo.cod_cuenta_deterioro != null) pcod_cuenta_deterioro.Value = pActivo.cod_cuenta_deterioro; else pcod_cuenta_deterioro.Value = DBNull.Value;
                        pcod_cuenta_deterioro.Direction = ParameterDirection.Input;
                        pcod_cuenta_deterioro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_deterioro);

                        DbParameter pcod_cuenta_revaluacion = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_revaluacion.ParameterName = "p_cod_cuenta_revaluacion";
                        if (pActivo.cod_cuenta_revaluacion != null) pcod_cuenta_revaluacion.Value = pActivo.cod_cuenta_revaluacion; else pcod_cuenta_revaluacion.Value = DBNull.Value;
                        pcod_cuenta_revaluacion.Direction = ParameterDirection.Input;
                        pcod_cuenta_revaluacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_revaluacion);

                        DbParameter pcod_cuenta_gastodet = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_gastodet.ParameterName = "p_cod_cuenta_gastodet";
                        if (pActivo.cod_cuenta_gastodet != null) pcod_cuenta_gastodet.Value = pActivo.cod_cuenta_gastodet; else pcod_cuenta_gastodet.Value = DBNull.Value;
                        pcod_cuenta_gastodet.Direction = ParameterDirection.Input;
                        pcod_cuenta_gastodet.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_gastodet);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1) // crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TIPOACTIVO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TIPOACTIVO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if(opcion == 1)// crear
                            pActivo.tipo_activo_nif = Convert.ToInt32(ptipo_activo_nif.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoNIFData", "CrearTipoActivoNIF", ex);
                        return null;
                    }
                }
            }
        }

       
        
       
        public void EliminarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_activo_nif = cmdTransaccionFactory.CreateParameter();
                        ptipo_activo_nif.ParameterName = "p_tipo_activo_nif";
                        ptipo_activo_nif.Value = pId;
                        ptipo_activo_nif.Direction = ParameterDirection.Input;
                        ptipo_activo_nif.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_activo_nif);                       

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TIPOACTIVO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoNIFData", "EliminarTipoActivo", ex);
                    }
                }
            }
        }

        
        public TipoActivoNIF ConsultarTipoActivo(Int32 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoActivoNIF entidad = new TipoActivoNIF();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select *  From Tipo_Activo_Nif where Tipo_Activo_Nif = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_ACTIVO_NIF"] != DBNull.Value) entidad.tipo_activo_nif = Convert.ToInt32(resultado["TIPO_ACTIVO_NIF"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODCLASIFICACION_NIF"] != DBNull.Value) entidad.codclasificacion_nif = Convert.ToInt32(resultado["CODCLASIFICACION_NIF"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_DETERIORO"] != DBNull.Value) entidad.cod_cuenta_deterioro = Convert.ToString(resultado["COD_CUENTA_DETERIORO"]);
                            if (resultado["COD_CUENTA_REVALUACION"] != DBNull.Value) entidad.cod_cuenta_revaluacion = Convert.ToString(resultado["COD_CUENTA_REVALUACION"]);
                            if (resultado["COD_CUENTA_GASTODET"] != DBNull.Value) entidad.cod_cuenta_gastodet = Convert.ToString(resultado["COD_CUENTA_GASTODET"]);
                        }                        
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoNIFData", "ConsultarTipoActivo", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoActivoNIF> ListarTipoActivo(String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoActivoNIF> lstParametro = new List<TipoActivoNIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Tipo_Activo_Nif.*, Clasificacion_Activo_Nif.Descripcion as nomclasificacion_nif "
                                            +"From Tipo_Activo_Nif Inner Join Clasificacion_Activo_Nif "
                                            +"On Tipo_Activo_Nif.Codclasificacion_Nif = Clasificacion_Activo_Nif.Codclasificacion_Nif "
                                            + "where 1 = 1" + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoActivoNIF entidad = new TipoActivoNIF();

                            if (resultado["TIPO_ACTIVO_NIF"] != DBNull.Value) entidad.tipo_activo_nif = Convert.ToInt32(resultado["TIPO_ACTIVO_NIF"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOMCLASIFICACION_NIF"] != DBNull.Value) entidad.nomclasificacion_nif = Convert.ToString(resultado["NOMCLASIFICACION_NIF"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_DETERIORO"] != DBNull.Value) entidad.cod_cuenta_deterioro = Convert.ToString(resultado["COD_CUENTA_DETERIORO"]);
                            if (resultado["COD_CUENTA_REVALUACION"] != DBNull.Value) entidad.cod_cuenta_revaluacion = Convert.ToString(resultado["COD_CUENTA_REVALUACION"]);
                            lstParametro.Add(entidad);
                        }
                        return lstParametro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoActivoNIFData", "ListarTipoActivo", ex);
                        return null;
                    }
                }
            }
        }

        
    }
}