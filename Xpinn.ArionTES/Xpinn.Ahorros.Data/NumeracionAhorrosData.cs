using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla  numeracion Cuentas
    /// </summary>
    public class NumeracionAhorrosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  Numeracion_Cuentas
        /// </summary>
        public NumeracionAhorrosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public NumeracionAhorros CrearNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pNumeracionAhorros.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Output;
                        pidconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pNumeracionAhorros.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pNumeracionAhorros.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea_producto = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_producto.ParameterName = "p_cod_linea_producto";
                        if (pNumeracionAhorros.cod_linea_producto == null)
                            pcod_linea_producto.Value = DBNull.Value;
                        else
                            pcod_linea_producto.Value = pNumeracionAhorros.cod_linea_producto;
                        pcod_linea_producto.Direction = ParameterDirection.Input;
                        pcod_linea_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_producto);

                        DbParameter pposicion = cmdTransaccionFactory.CreateParameter();
                        pposicion.ParameterName = "p_posicion";
                        pposicion.Value = pNumeracionAhorros.posicion;
                        pposicion.Direction = ParameterDirection.Input;
                        pposicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion);

                        DbParameter ptipo_campo = cmdTransaccionFactory.CreateParameter();
                        ptipo_campo.ParameterName = "p_tipo_campo";
                        ptipo_campo.Value = pNumeracionAhorros.tipo_campo;
                        ptipo_campo.Direction = ParameterDirection.Input;
                        ptipo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_campo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pNumeracionAhorros.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pNumeracionAhorros.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pNumeracionAhorros.longitud == null)
                            plongitud.Value = DBNull.Value;
                        else
                            plongitud.Value = pNumeracionAhorros.longitud;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter palinear = cmdTransaccionFactory.CreateParameter();
                        palinear.ParameterName = "p_alinear";
                        if (pNumeracionAhorros.alinear == null)
                            palinear.Value = DBNull.Value;
                        else
                            palinear.Value = pNumeracionAhorros.alinear;
                        palinear.Direction = ParameterDirection.Input;
                        palinear.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(palinear);

                        DbParameter pcaracter_llenado = cmdTransaccionFactory.CreateParameter();
                        pcaracter_llenado.ParameterName = "p_caracter_llenado";
                        if (pNumeracionAhorros.caracter_llenado == null)
                            pcaracter_llenado.Value = DBNull.Value;
                        else
                            pcaracter_llenado.Value = pNumeracionAhorros.caracter_llenado;
                        pcaracter_llenado.Direction = ParameterDirection.Input;
                        pcaracter_llenado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcaracter_llenado);
                        
                       if (pNumeracionAhorros.posicion > 0)
                        {                        
                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_NUMERACION_CREAR";

                            cmdTransaccionFactory.ExecuteNonQuery();
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return pNumeracionAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NumeracionAhorrosData", "CrearNumeracionAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public NumeracionAhorros ModificarNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pNumeracionAhorros.idconsecutivo;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pNumeracionAhorros.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pNumeracionAhorros.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea_producto = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_producto.ParameterName = "p_cod_linea_producto";
                        if (pNumeracionAhorros.cod_linea_producto == null)
                            pcod_linea_producto.Value = DBNull.Value;
                        else
                            pcod_linea_producto.Value = pNumeracionAhorros.cod_linea_producto;
                        pcod_linea_producto.Direction = ParameterDirection.Input;
                        pcod_linea_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_producto);

                        DbParameter pposicion = cmdTransaccionFactory.CreateParameter();
                        pposicion.ParameterName = "p_posicion";
                        pposicion.Value = pNumeracionAhorros.posicion;
                        pposicion.Direction = ParameterDirection.Input;
                        pposicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pposicion);

                        DbParameter ptipo_campo = cmdTransaccionFactory.CreateParameter();
                        ptipo_campo.ParameterName = "p_tipo_campo";
                        ptipo_campo.Value = pNumeracionAhorros.tipo_campo;
                        ptipo_campo.Direction = ParameterDirection.Input;
                        ptipo_campo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_campo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pNumeracionAhorros.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pNumeracionAhorros.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pNumeracionAhorros.longitud == null)
                            plongitud.Value = DBNull.Value;
                        else
                            plongitud.Value = pNumeracionAhorros.longitud;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter palinear = cmdTransaccionFactory.CreateParameter();
                        palinear.ParameterName = "p_alinear";
                        if (pNumeracionAhorros.alinear == null)
                            palinear.Value = DBNull.Value;
                        else
                            palinear.Value = pNumeracionAhorros.alinear;
                        palinear.Direction = ParameterDirection.Input;
                        palinear.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(palinear);

                        DbParameter pcaracter_llenado = cmdTransaccionFactory.CreateParameter();
                        pcaracter_llenado.ParameterName = "p_caracter_llenado";
                        if (pNumeracionAhorros.caracter_llenado == null)
                            pcaracter_llenado.Value = DBNull.Value;
                        else
                            pcaracter_llenado.Value = pNumeracionAhorros.caracter_llenado;
                        pcaracter_llenado.Direction = ParameterDirection.Input;
                        pcaracter_llenado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcaracter_llenado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_NUMERACION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pNumeracionAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NumeracionAhorrosData", "ModificarNumeracionAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public NumeracionAhorros ConsultarNumeracionAhorros(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            NumeracionAhorros entidad = new NumeracionAhorros();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM NUMERACION_CUENTAS WHERE TIPO_PRODUCTO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt32(resultado["IDCONSECUTIVO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.cod_linea_producto = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["POSICION"] != DBNull.Value) entidad.posicion = Convert.ToInt32(resultado["POSICION"]);
                            if (resultado["TIPO_CAMPO"] != DBNull.Value) entidad.tipo_campo = Convert.ToInt32(resultado["TIPO_CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["ALINEAR"] != DBNull.Value) entidad.alinear = Convert.ToString(resultado["ALINEAR"]);
                            if (resultado["CARACTER_LLENADO"] != DBNull.Value) entidad.caracter_llenado = Convert.ToString(resultado["CARACTER_LLENADO"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NumeracionAhorrosData", "ConsultarNumeracionAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarNumeracionAhorros(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pidconsecutivo.ParameterName = "p_idconsecutivo";
                        pidconsecutivo.Value = pId;
                        pidconsecutivo.Direction = ParameterDirection.Input;
                        pidconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_NUMERACION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NumeracionAhorrosData", "EliminarNumeracionAhorros", ex);
                    }
                }
            }
        }     
       public List<NumeracionAhorros> ListarNumeracionAhorros(NumeracionAhorros pNumeracionAhorros, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<NumeracionAhorros> lstNumeracionAhorros = new List<NumeracionAhorros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM NUMERACION_CUENTAS " + ObtenerFiltro(pNumeracionAhorros) + " ORDER BY POSICION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            NumeracionAhorros entidad = new NumeracionAhorros();
                            if (resultado["IDCONSECUTIVO"] != DBNull.Value) entidad.idconsecutivo = Convert.ToInt32(resultado["IDCONSECUTIVO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.cod_linea_producto = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["POSICION"] != DBNull.Value) entidad.posicion = Convert.ToInt32(resultado["POSICION"]);
                            if (resultado["TIPO_CAMPO"] != DBNull.Value) entidad.tipo_campo = Convert.ToInt32(resultado["TIPO_CAMPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["ALINEAR"] != DBNull.Value) entidad.alinear = Convert.ToString(resultado["ALINEAR"]);
                            if (resultado["CARACTER_LLENADO"] != DBNull.Value) entidad.caracter_llenado = Convert.ToString(resultado["CARACTER_LLENADO"]);
                            lstNumeracionAhorros.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstNumeracionAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NumeracionAhorrosData", "ListarNumeracionAhorros", ex);
                        return null;
                    }
                }
            }
        }
        
    }
}
