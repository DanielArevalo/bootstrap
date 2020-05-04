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
    /// Objeto de acceso a datos para la tabla Ahorro_Vista
    /// </summary>
    public class CuentasExentasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Ahorro_Vista
        /// </summary>
        public CuentasExentasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CuentasExenta CrearCuentaExenta(CuentasExenta pExenta, Usuario vUsuario,Int32 opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidexenta = cmdTransaccionFactory.CreateParameter();
                        pidexenta.ParameterName = "p_idexenta";
                        pidexenta.Value = pExenta.idexenta;
                        pidexenta.Direction = ParameterDirection.Input;
                        pidexenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidexenta);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        if (pExenta.numero_cuenta == null)
                            pnumero_cuenta.Value = DBNull.Value;
                        else
                            pnumero_cuenta.Value = pExenta.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pExenta.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pfecha_exenta = cmdTransaccionFactory.CreateParameter();
                        pfecha_exenta.ParameterName = "p_fecha_exenta";
                        if (pExenta.fecha_exenta == null)
                            pfecha_exenta.Value = DBNull.Value;
                        else
                            pfecha_exenta.Value = pExenta.fecha_exenta;
                        pfecha_exenta.Direction = ParameterDirection.Input;
                        pfecha_exenta.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_exenta);

                        DbParameter pmonto = cmdTransaccionFactory.CreateParameter();
                        pmonto.ParameterName = "p_monto";
                        if (pExenta.monto == null)
                            pmonto.Value = DBNull.Value;
                        else
                            pmonto.Value = pExenta.monto;
                        pmonto.Direction = ParameterDirection.Input;
                        pmonto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pExenta.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pExenta.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1) // CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CTAEXENTA_CREAR";
                        else  //MODIFICAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_CTAEXENTA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pExenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "CrearCuentaExenta", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuentasExenta> ListarProductosControl(int pTipo, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasExenta> lstCuenta = new List<CuentasExenta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if ( pTipo == 1) // APORTES
                            sql = @"SELECT A.NUMERO_APORTE NUMERO_PRODUCTO, A.COD_LINEA_APORTE as COD_LINEA, L.NOMBRE NOMBRE_LINEA, A.COD_PERSONA, V.IDENTIFICACION, V.NOMBRE, V.COD_OFICINA
                                    FROM APORTE A INNER JOIN LINEAAPORTE L ON A.COD_LINEA_APORTE = L.COD_LINEA_APORTE
                                    INNER JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA
                                    WHERE SALDO > 0 " + pFiltro;
                        if ( pTipo == 2) // CREDITOS
                            sql = @"SELECT C.NUMERO_RADICACION NUMERO_PRODUCTO, C.COD_LINEA_CREDITO as COD_LINEA, L.NOMBRE NOMBRE_LINEA, C.COD_DEUDOR COD_PERSONA, V.IDENTIFICACION, V.NOMBRE, V.COD_OFICINA
                                    FROM CREDITO C INNER JOIN LINEASCREDITO L ON C.COD_LINEA_CREDITO = L.COD_LINEA_CREDITO
                                    INNER JOIN V_PERSONA V ON C.COD_DEUDOR = V.COD_PERSONA
                                    WHERE SALDO_CAPITAL > 0 " + pFiltro;
                        if ( pTipo == 3) // AHORRO VISTA
                            sql = @"SELECT A.NUMERO_CUENTA NUMERO_PRODUCTO, A.COD_LINEA_AHORRO as COD_LINEA, L.DESCRIPCION NOMBRE_LINEA, A.COD_PERSONA, V.IDENTIFICACION, V.NOMBRE, V.COD_OFICINA
                                    FROM AHORRO_VISTA A INNER JOIN LINEAAHORRO L ON A.COD_LINEA_AHORRO = L.COD_LINEA_AHORRO
                                    INNER JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA
                                    WHERE A.SALDO_TOTAL > 0 " + pFiltro;
                        if ( pTipo == 9) // AHORRO PROGRAMADO
                            sql = @"SELECT A.NUMERO_PROGRAMADO NUMERO_PRODUCTO, A.COD_LINEA_PROGRAMADO as COD_LINEA, L.NOMBRE NOMBRE_LINEA, A.COD_PERSONA, V.IDENTIFICACION, V.NOMBRE, V.COD_OFICINA
                                    FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON A.COD_LINEA_PROGRAMADO = L.COD_LINEA_PROGRAMADO
                                    INNER JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA
                                    WHERE A.SALDO > 0 " + pFiltro;
                        if ( pTipo == 5) // CDAT
                            sql = @"SELECT A.NUMERO_CDAT NUMERO_PRODUCTO, A.COD_LINEACDAT as COD_LINEA, L.DESCRIPCION NOMBRE_LINEA, T.COD_PERSONA, V.IDENTIFICACION, V.NOMBRE, V.COD_OFICINA
                                    FROM CDAT A INNER JOIN LINEACDAT L ON A.COD_LINEACDAT = L.COD_LINEACDAT
                                    INNER JOIN CDAT_TITULAR T ON A.CODIGO_CDAT = T.CODIGO_CDAT
                                    INNER JOIN V_PERSONA V ON T.COD_PERSONA = V.COD_PERSONA
                                    WHERE A.VALOR > 0 " + pFiltro;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            CuentasExenta entidad;
                            while (resultado.Read())
                            {
                                entidad = new CuentasExenta();
                                if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                                if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                                if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                                lstCuenta.Add(entidad);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "ListarProductosControl", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasExenta ConsultarCuentaExentaXNumeroCuenta(CuentasExenta pExenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasExenta entidad = new CuentasExenta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";


                         if(pExenta.tipo_cuenta == 1) //APORTES                        
                                sql =  @"SELECT A.COD_LINEA_APORTE AS COD_LINEA,L.NOMBRE AS NOM_LINEA,A.COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA "
                                        +" FROM APORTE A LEFT JOIN LINEAAPORTE L ON L.COD_LINEA_APORTE = A.COD_LINEA_APORTE "
                                        +" LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA "
                                        + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA "
                                        + "  WHERE A.NUMERO_APORTE = " + pExenta.numero_cuenta + "ORDER BY  A.NUMERO_APORTE ";

                         if(pExenta.tipo_cuenta == 2) //CREDITOS
                                 sql = @"SELECT A.COD_LINEA_CREDITO AS COD_LINEA,L.NOMBRE AS NOM_LINEA,A.COD_DEUDOR AS COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA "
                                        +" FROM CREDITO A LEFT JOIN LINEASCREDITO L ON L.COD_LINEA_CREDITO = A.COD_LINEA_CREDITO "
                                        +" LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_DEUDOR "
                                        +" LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA "
                                        + " WHERE A.NUMERO_RADICACION = " + pExenta.numero_cuenta + " ORDER BY  A.NUMERO_RADICACION ";

                            if (pExenta.tipo_cuenta == 3) //AHORRO VISTA
                                sql = @"SELECT A.COD_LINEA_AHORRO AS COD_LINEA,L.DESCRIPCION AS NOM_LINEA,A.COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA "
                                        +" FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO "
                                        +" LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA "
                                        + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA WHERE A.NUMERO_CUENTA = " + pExenta.numero_cuenta + " ORDER BY  A.NUMERO_CUENTA ";

                            if (pExenta.tipo_cuenta == 9)// AHORRO PROGRAMADO
                                sql = @"SELECT A.COD_LINEA_PROGRAMADO as COD_LINEA,L.NOMBRE AS NOM_LINEA,A.COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA "
                                        + " FROM AHORRO_PROGRAMADO A LEFT JOIN LINEAPROGRAMADO L ON L.COD_LINEA_PROGRAMADO = A.COD_LINEA_PROGRAMADO "
                                        + " LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA "
                                        + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA WHERE A.NUMERO_PROGRAMADO = " + pExenta.numero_cuenta + " ORDER BY  A.NUMERO_PROGRAMADO ";

                            if (pExenta.tipo_cuenta == 5)//CDATS
                                sql = @" SELECT A.COD_LINEACDAT AS COD_LINEA,L.descripcion AS NOM_LINEA,t.COD_persona AS COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA  "
                                          + " FROM CDAT A LEFT JOIN LINEACDAT L ON L.COD_LINEACDAT = A.COD_LINEACDAT   "
                                          + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA  "
                                          + " left join cdat_titular t on t.codigo_cdat=a.codigo_cdat "
                                          + " LEFT JOIN V_PERSONA V ON V.COD_PERSONA = t.COD_persona "
                                          + " WHERE t.principal=1 and A.numero_cdat = " + pExenta.numero_cuenta + " ORDER BY  A.numero_cdat ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "ConsultarCuentaExentaXNumeroCuenta", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCuentasExentas(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM CUENTAS_EXENTAS WHERE IDEXENTA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "EliminarCuentasExentas", ex);
                    }
                }
            }
        }

        public void EliminarCuentasExentasNumeroCuenta(Int64 pNumeroCuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM CUENTAS_EXENTAS WHERE NUMERO_CUENTA = " + pNumeroCuenta.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "EliminarCuentasExentasNumeroCuenta", ex);
                    }
                }
            }
        }

        public List<CuentasExenta> ListarCuentaExenta(CuentasExenta pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasExenta> lstCuenta = new List<CuentasExenta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT V.* FROM V_CUENTAS_EXENTAS V " + ObtenerFiltro(pCuenta, "V.") + " ORDER BY V.IDEXENTA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasExenta entidad = new CuentasExenta();
                            if (resultado["IDEXENTA"] != DBNull.Value) entidad.idexenta = Convert.ToInt64(resultado["IDEXENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.cod_tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);

                            if (entidad.cod_tipo_cuenta == 1)
                            {
                                entidad.nom_tipocuenta = "Aportes";
                            }
                            if (entidad.cod_tipo_cuenta == 2)
                            {
                                entidad.nom_tipocuenta = "Créditos";
                            }
                            if (entidad.cod_tipo_cuenta == 3)
                            {
                                entidad.nom_tipocuenta = "Ahorros Vista";
                            }
                            if(entidad.cod_tipo_cuenta == 4)
                            {
                                entidad.nom_tipocuenta = "Servicios";
                            }
                            if (entidad.cod_tipo_cuenta == 5)
                            {
                                entidad.nom_tipocuenta = "CDATS";
                            }
                            if (entidad.cod_tipo_cuenta == 9)
                            {
                                entidad.nom_tipocuenta = "Ahorros Programado";
                            }
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["FECHA_EXENTA"] != DBNull.Value) entidad.fecha_exenta = Convert.ToDateTime(resultado["FECHA_EXENTA"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_PRODUCTO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_PRODUCTO"]);
                            if (resultado["NOM_LINEA_PRODUCTO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_PRODUCTO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            lstCuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasExentasData", "ListarCuentaExenta", ex);
                        return null;
                    }
                }
            }
        }

        public int? ConvertirAIntN(string pdato)
        {
            int? dato = null;
            try { dato = Convert.ToInt16(pdato.Trim()); } catch { }
            return dato;
        }

        public int ConvertirAInt(string pdato)
        {
            int dato = 0;
            try { dato = Convert.ToInt16(pdato.Trim()); } catch { }
            return dato;
        }

        public Int32 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int32 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(IDEXENTA) + 1 FROM CUENTAS_EXENTAS";

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
                        return 1;
                    }
                }
            }
        }


    }
}
