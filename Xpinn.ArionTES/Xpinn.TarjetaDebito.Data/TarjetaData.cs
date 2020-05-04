using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;


namespace Xpinn.TarjetaDebito.Data
{
    public class TarjetaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TarjetaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Tarjeta ConsultarAsignacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA WHERE IDTARJETA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) entidad.cuenta_homologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt32(resultado["COD_CONVENIO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToDecimal(resultado["CUPO"]);
                            if (resultado["MAX_TRAN"] != DBNull.Value) entidad.max_tran = Convert.ToInt32(resultado["MAX_TRAN"]);
                            if (resultado["COBRA_CUOTA_MANEJO"] != DBNull.Value) entidad.cobra_cuota_manejo = Convert.ToInt32(resultado["COBRA_CUOTA_MANEJO"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
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
                        BOExcepcion.Throw("TarjetaData", "ConsultarAsignacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadas(int numeroDeDiasParaBloquearTarjetas, string ProductosenCuentaparaBloqueo, int tipo_bloqueo, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.TIPO_CUENTA, tar.ESTADO, per.nombre, per.identificacion, 
                                        ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) as DiasMora,
                                        CASE tar.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' END as desc_estado,
                                        CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                        FROM CREDITO cre
                                        JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                        JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                        JOIN V_PERSONA per ON per.COD_PERSONA = cre.COD_DEUDOR
                                        WHERE lin.TIPO_LINEA = 2 And tar.tipo_cuenta = 2 And cre.estado = 'C' And cre.SALDO_CAPITAL > 0 And cre.FECHA_PROXIMO_PAGO IS NOT NULL
                                        and ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) >= " + numeroDeDiasParaBloquearTarjetas;
                        if (tipo_bloqueo == 1)
                        {
                            sql += " And (tar.ESTADO_SALDO is null or tar.ESTADO_SALDO = 0)"; //" And tar.estado = 1"
                        }
                        else
                        {
                            sql += " And (tar.ESTADO_SALDO is null or tar.ESTADO_SALDO = 0) And tar.estado NOT IN (0,2)";
                        }

                        if (ProductosenCuentaparaBloqueo != null && ProductosenCuentaparaBloqueo != "" && ProductosenCuentaparaBloqueo != "0")
                        {
                            sql += @"   UNION 
                                        SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.TIPO_CUENTA, tar.ESTADO, per.nombre, per.identificacion, 
                                        ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) as DiasMora,
                                        CASE tar.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' END as desc_estado,
                                        CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                        FROM CREDITO cre
                                        JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                        JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                        JOIN V_PERSONA per ON per.COD_PERSONA = cre.COD_DEUDOR
                                        WHERE lin.TIPO_LINEA = 2 And tar.tipo_cuenta = 2 And tar.estado = 1  And cre.estado = 'C' And cre.FECHA_PROXIMO_PAGO IS NOT NULL
                                        and ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) < " + numeroDeDiasParaBloquearTarjetas +
                                        @"and cre.COD_DEUDOR IN (
                                        WITH resultado As (
                                            -- Aportes
                                            SELECT DISTINCT 1 tipo_producto, COD_PERSONA from APORTE
                                            WHERE NUMERODIASENTREDOSFECHAS(sysdate, FECHA_PROXIMO_PAGO) >=  " + numeroDeDiasParaBloquearTarjetas + @" AND ESTADO = 1
                                            AND COD_LINEA_APORTE IN (SELECT COD_LINEA_APORTE FROM LINEAAPORTE WHERE TIPO_APORTE = 1)
                                            --Creditos Normales
                                            UNION
                                            SELECT DISTINCT 2 tipo_producto, COD_DEUDOR from CREDITO
                                            WHERE NUMERODIASENTREDOSFECHAS(sysdate, FECHA_PROXIMO_PAGO) >=  " + numeroDeDiasParaBloquearTarjetas + @" AND ESTADO = 'C'
                                            AND COD_LINEA_CREDITO NOT IN ( select COD_LINEA_CREDITO FROM LINEASCREDITO where TIPO_LINEA = 2)
                                            -- Servicios
                                            UNION
                                            SELECT DISTINCT 4 tipo_producto, COD_PERSONA from SERVICIOS
                                            WHERE NUMERODIASENTREDOSFECHAS(sysdate, FECHA_PROXIMO_PAGO) >= 3 AND ESTADO = 'C')
                                            SELECT DISTINCT cod_persona FROM resultado WHERE tipo_producto IN (" + ProductosenCuentaparaBloqueo + @")
                                       )";
                        }


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();

                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.cod_tipocta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["DIASMORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIASMORA"]);
                            if (resultado["DESC_ESTADO"] != DBNull.Value) entidad.desc_estado = Convert.ToString(resultado["DESC_ESTADO"]);
                            if (resultado["DESC_TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["DESC_TIPO_CUENTA"]);

                            lstTarjeta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListarTarjetasEnMoraYNoBloqueadas", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarTarjetasBloqueadasYAlDia(int ptipo_bloqueo, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        
                        if (ptipo_bloqueo == 1)
                        {
                            sql = @"SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.tipo_cuenta, tar.estado, per.nombre, per.identificacion, 
                                            CASE tar.estado_saldo WHEN 0 THEN 'Cupo Activo' WHEN 1 THEN 'Bloqueada' END as desc_estado,
                                            CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                            FROM CREDITO cre
                                            JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                            JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                            JOIN v_persona per on per.COD_PERSONA = cre.COD_DEUDOR
                                            WHERE lin.tipo_linea = 2 and tar.tipo_cuenta = 2 and cre.estado = 'C' and cre.FECHA_PROXIMO_PAGO IS NOT NULL and TRUNC(cre.FECHA_PROXIMO_PAGO) >= TRUNC(sysdate)
                                            and tar.estado_saldo = 1";
                        }
                        else
                        {
                            sql = @"SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.tipo_cuenta, tar.estado, per.nombre, per.identificacion, 
                                            CASE tar.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' END as desc_estado,
                                            CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                            FROM CREDITO cre
                                            JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                            JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                            JOIN v_persona per on per.COD_PERSONA = cre.COD_DEUDOR
                                            WHERE lin.tipo_linea = 2 and tar.tipo_cuenta = 2 and cre.estado = 'C' and cre.FECHA_PROXIMO_PAGO IS NOT NULL and TRUNC(cre.FECHA_PROXIMO_PAGO) >= TRUNC(sysdate)
                                            and tar.estado = 3 ";

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();

                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.cod_tipocta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["desc_estado"] != DBNull.Value) entidad.desc_estado = Convert.ToString(resultado["desc_estado"]);
                            if (resultado["desc_tipo_cuenta"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["desc_tipo_cuenta"]);

                            lstTarjeta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListarTarjetasBloqueadasYAlDia", ex);
                        return null;
                    }
                }
            }
        }

        public void CambiarEstadoTarjeta(Tarjeta tarjeta, EstadoTarjetaEnpacto estado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDTARJETA = cmdTransaccionFactory.CreateParameter();
                        P_IDTARJETA.Direction = ParameterDirection.Input;
                        P_IDTARJETA.ParameterName = "P_IDTARJETA";
                        P_IDTARJETA.Value = tarjeta.idtarjeta;

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = (int)estado;

                        cmdTransaccionFactory.Parameters.Add(P_IDTARJETA);
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TARJ_MODESTADO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "CambiarEstadoTarjeta", ex);
                    }
                }
            }
        }

        public Tarjeta ConsultarNumTarjeta(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA WHERE NUMERO_TARJETA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ConsultarNumTarjeta", ex);
                        return null;
                    }
                }
            }
        }

        public Tarjeta ConsultarCuentas(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA WHERE NUMERO_TARJETA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Data", "ConsultarNumTarjeta", ex);
                        return null;
                    }
                }
            }
        }

        public Tarjeta ConsultarTarjetaDeUnaCuenta(string numeroCuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA WHERE NUMERO_CUENTA = '" + numeroCuenta + "' AND ESTADO = 1 ORDER BY IDTARJETA DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) entidad.cuenta_homologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt64(resultado["COD_CONVENIO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToDecimal(resultado["CUPO"]);
                            if (resultado["MAX_TRAN"] != DBNull.Value) entidad.max_tran = Convert.ToInt32(resultado["MAX_TRAN"]);
                            if (resultado["COBRA_CUOTA_MANEJO"] != DBNull.Value) entidad.cobra_cuota_manejo = Convert.ToInt32(resultado["COBRA_CUOTA_MANEJO"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_ASESOR"] != DBNull.Value) entidad.cod_asesor = Convert.ToInt64(resultado["COD_ASESOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ConsultarTarjetaDeUnaCuenta", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarAsignacion(String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText =
                            @"SELECT t.*,p.identificacion,p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido as Nombres , p.cod_nomina from tarjeta t inner join persona p on p.cod_persona=t.cod_persona " + filtro;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();
                            // AsignacionTarjeta entidad = new AsignacionTarjeta();
                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["CUENTA_HOMOLOGA"] != DBNull.Value) entidad.cuenta_homologa = Convert.ToString(resultado["CUENTA_HOMOLOGA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_asignacion = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToInt64(resultado["COD_CONVENIO"]);
                            if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["CUPO"] != DBNull.Value) entidad.cupo = Convert.ToDecimal(resultado["CUPO"]);
                            if (resultado["MAX_TRAN"] != DBNull.Value) entidad.max_tran = Convert.ToInt32(resultado["MAX_TRAN"]);
                            if (resultado["COBRA_CUOTA_MANEJO"] != DBNull.Value) entidad.cobra_cuota_manejo = Convert.ToInt32(resultado["COBRA_CUOTA_MANEJO"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            lstTarjeta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListarAsignacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarOficina(Tarjeta pEntOficina, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntOficina = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM OFICINA " + ObtenerFiltro(pEntOficina);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entityOficina = new Tarjeta();

                            if (reader["COD_OFICINA"] != DBNull.Value) entityOficina.cod_oficina = Convert.ToInt64(reader["COD_OFICINA"].ToString());
                            if (reader["NOMBRE"] != DBNull.Value) entityOficina.oficina = reader["NOMBRE"].ToString();

                            lstEntOficina.Add(entityOficina);
                        }
                        return lstEntOficina;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "ListarOficina", ex);
                        return null;
                    }
                }
            }
        }

        public List<Tarjeta> ListarTipoCuenta(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntCuenta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM TIPO_CTA " + ObtenerFiltro(pEntCuenta);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entityOficina = new Tarjeta();

                            if (reader["COD_TIPOCTA"] != DBNull.Value) entityOficina.cod_tipocta = Convert.ToInt32(reader["COD_TIPOCTA"].ToString());
                            if (reader["NOMCUE"] != DBNull.Value) entityOficina.nomcuenta = reader["NOMCUE"].ToString();

                            lstEntCuenta.Add(entityOficina);
                        }
                        return lstEntCuenta;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "ListarTipoCuenta", ex);
                        return null;
                    }
                }
            }
        }
        public List<Tarjeta> ListarConvenio(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntCuenta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM tarjeta_convenio " + ObtenerFiltro(pEntCuenta);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entityConvenio = new Tarjeta();

                            if (reader["COD_CONVENIO"] != DBNull.Value) entityConvenio.codconvenio = Convert.ToInt32(reader["COD_CONVENIO"].ToString());
                            if (reader["NOMBRE"] != DBNull.Value) entityConvenio.nom_convenio = reader["NOMBRE"].ToString();

                            lstEntCuenta.Add(entityConvenio);
                        }
                        return lstEntCuenta;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "ListarConvenio", ex);
                        return null;
                    }
                }
            }
        }
        public List<Tarjeta> Listartarjeta(Int64 filtro, Tarjeta pEntCuenta, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntCuenta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT * FROM TARJETA_PLASTICO  where cod_convenio=" + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entitytarjeta = new Tarjeta();

                            if (reader["IDPLASTICO"] != DBNull.Value) entitytarjeta.idplastico = Convert.ToInt32(reader["IDPLASTICO"].ToString());
                            if (reader["NUMERO_TARJETA"] != DBNull.Value) entitytarjeta.numtarjeta = Convert.ToString(reader["NUMERO_TARJETA"].ToString());
                            if (reader["COD_CONVENIO"] != DBNull.Value) entitytarjeta.cod_convenio = Convert.ToInt32(reader["COD_CONVENIO"].ToString());
                            if (reader["ESTADO"] != DBNull.Value) entitytarjeta.estado = reader["ESTADO"].ToString();



                            lstEntCuenta.Add(entitytarjeta);
                        }
                        return lstEntCuenta;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "Listartarjeta", ex);
                        return null;
                    }
                }
            }
        }
        public List<Tarjeta> ListarAhorros(Int64 filtro, Tarjeta pEntAhorros, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntCuenta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT COD_LINEA_AHORRO,numero_cuenta FROM ahorro_vista  where cod_persona=" + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entitytarjeta = new Tarjeta();

                            if (reader["COD_LINEA_AHORRO"] != DBNull.Value) entitytarjeta.cod_ahorro = Convert.ToInt32(reader["COD_LINEA_AHORRO"].ToString());
                            if (reader["NUMERO_CUENTA"] != DBNull.Value) entitytarjeta.numero_cuenta = Convert.ToString(reader["NUMERO_CUENTA"].ToString());



                            lstEntCuenta.Add(entitytarjeta);
                        }
                        return lstEntCuenta;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "ListarAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public List<Tarjeta> ListarCredito(Int64 filtro, Tarjeta pEntCredito, Usuario pUsuario)
        {
            DbDataReader reader = default(DbDataReader);
            List<Tarjeta> lstEntCuenta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string query = "SELECT COD_LINEA_CREDITO,NUMERO_RADICACION FROM credito  where estado ='C' and cod_deudor=" + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = query;
                        reader = cmdTransaccionFactory.ExecuteReader();

                        while (reader.Read())
                        {
                            Tarjeta entity = new Tarjeta();

                            if (reader["COD_LINEA_CREDITO"] != DBNull.Value) entity.cod_linea_credito = Convert.ToInt32(reader["COD_LINEA_CREDITO"].ToString());
                            if (reader["NUMERO_RADICACION"] != DBNull.Value) entity.num_radicacion = Convert.ToInt32(reader["NUMERO_RADICACION"].ToString());



                            lstEntCuenta.Add(entity);
                        }
                        return lstEntCuenta;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "ListarAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public Tarjeta ConsultarValoresCredito(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT monto_aprobado, (monto_aprobado - saldo_capital) as saldo_disponible from credito where numero_radicacion= " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["monto_aprobado"]);
                            if (resultado["saldo_disponible"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["saldo_disponible"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Data", "ConsultarSaldocredito", ex);
                        return null;
                    }
                }
            }
        }
        public void Eliminar(Int64 p_Idtarjeta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Tarjeta aseEntEjecutivo = new Tarjeta();

                        //if (pUsuario.programaGeneraLog)
                        // aseEntEjecutivo = ConsultarEjecutivo(pIdEjecutivo, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_idtarjeta = cmdTransaccionFactory.CreateParameter();
                        p_idtarjeta.Direction = ParameterDirection.Input;
                        p_idtarjeta.ParameterName = "p_idtarjeta";
                        p_idtarjeta.Value = p_Idtarjeta;

                        cmdTransaccionFactory.Parameters.Add(p_idtarjeta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TARJETA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //DAauditoria.InsertarLog(aseEntEjecutivo, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "EliminarTarjeta", ex);
                    }
                }
            }
        }
        public Tarjeta CrearAsignacionTarjeta(Tarjeta pAsignacionTarjeta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtarjeta = cmdTransaccionFactory.CreateParameter();
                        pidtarjeta.ParameterName = "p_idtarjeta";
                        pidtarjeta.Value = pAsignacionTarjeta.idtarjeta;
                        pidtarjeta.Direction = ParameterDirection.Output;
                        pidtarjeta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidtarjeta);

                        DbParameter pnumero_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pnumero_tarjeta.ParameterName = "p_numero_tarjeta";
                        pnumero_tarjeta.Value = pAsignacionTarjeta.numtarjeta;
                        pnumero_tarjeta.Direction = ParameterDirection.Input;
                        pnumero_tarjeta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_tarjeta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pAsignacionTarjeta.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pAsignacionTarjeta.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pcuenta_homologa = cmdTransaccionFactory.CreateParameter();
                        pcuenta_homologa.ParameterName = "p_cuenta_homologa";
                        if (pAsignacionTarjeta.cuenta_homologa == null)
                            pcuenta_homologa.Value = DBNull.Value;
                        else
                            pcuenta_homologa.Value = pAsignacionTarjeta.cuenta_homologa;
                        pcuenta_homologa.Direction = ParameterDirection.Input;
                        pcuenta_homologa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuenta_homologa);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAsignacionTarjeta.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_asignacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_asignacion.ParameterName = "p_fecha_asignacion";
                        if (pAsignacionTarjeta.fecha_asignacion == null)
                            pfecha_asignacion.Value = DBNull.Value;
                        else
                            pfecha_asignacion.Value = pAsignacionTarjeta.fecha_asignacion;
                        pfecha_asignacion.Direction = ParameterDirection.Input;
                        pfecha_asignacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_asignacion);

                        DbParameter pcod_convenio = cmdTransaccionFactory.CreateParameter();
                        pcod_convenio.ParameterName = "p_cod_convenio";
                        if (pAsignacionTarjeta.cod_convenio == null)
                            pcod_convenio.Value = DBNull.Value;
                        else
                            pcod_convenio.Value = pAsignacionTarjeta.cod_convenio;
                        pcod_convenio.Direction = ParameterDirection.Input;
                        pcod_convenio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_convenio);

                        DbParameter pfecha_proceso = cmdTransaccionFactory.CreateParameter();
                        pfecha_proceso.ParameterName = "p_fecha_proceso";
                        if (pAsignacionTarjeta.fecha_proceso == null)
                            pfecha_proceso.Value = DBNull.Value;
                        else
                            pfecha_proceso.Value = pAsignacionTarjeta.fecha_proceso;
                        pfecha_proceso.Direction = ParameterDirection.Input;
                        pfecha_proceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proceso);

                        DbParameter pfecha_activacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_activacion.ParameterName = "p_fecha_activacion";
                        if (pAsignacionTarjeta.fecha_activacion == null)
                            pfecha_activacion.Value = DBNull.Value;
                        else
                            pfecha_activacion.Value = pAsignacionTarjeta.fecha_activacion;
                        pfecha_activacion.Direction = ParameterDirection.Input;
                        pfecha_activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_activacion);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (pAsignacionTarjeta.cod_oficina == null)
                            pcod_oficina.Value = DBNull.Value;
                        else
                            pcod_oficina.Value = pAsignacionTarjeta.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter psaldo_total = cmdTransaccionFactory.CreateParameter();
                        psaldo_total.ParameterName = "p_saldo_total";
                        psaldo_total.Value = pAsignacionTarjeta.saldo_total;
                        psaldo_total.Direction = ParameterDirection.Input;
                        psaldo_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_total);

                        DbParameter psaldo_disponible = cmdTransaccionFactory.CreateParameter();
                        psaldo_disponible.ParameterName = "p_saldo_disponible";
                        if (pAsignacionTarjeta.saldo_disponible == null)
                            psaldo_disponible.Value = DBNull.Value;
                        else
                            psaldo_disponible.Value = pAsignacionTarjeta.saldo_disponible;
                        psaldo_disponible.Direction = ParameterDirection.Input;
                        psaldo_disponible.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_disponible);

                        DbParameter psaldo_canje = cmdTransaccionFactory.CreateParameter();
                        psaldo_canje.ParameterName = "p_saldo_canje";
                        if (pAsignacionTarjeta.saldo_canje == null)
                            psaldo_canje.Value = DBNull.Value;
                        else
                            psaldo_canje.Value = pAsignacionTarjeta.saldo_canje;
                        psaldo_canje.Direction = ParameterDirection.Input;
                        psaldo_canje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_canje);

                        DbParameter pcupo = cmdTransaccionFactory.CreateParameter();
                        pcupo.ParameterName = "p_cupo";
                        if (pAsignacionTarjeta.cupo == null)
                            pcupo.Value = DBNull.Value;
                        else
                            pcupo.Value = pAsignacionTarjeta.cupo;
                        pcupo.Direction = ParameterDirection.Input;
                        pcupo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcupo);

                        DbParameter pmax_tran = cmdTransaccionFactory.CreateParameter();
                        pmax_tran.ParameterName = "p_max_tran";
                        if (pAsignacionTarjeta.max_tran == null)
                            pmax_tran.Value = DBNull.Value;
                        else
                            pmax_tran.Value = pAsignacionTarjeta.max_tran;
                        pmax_tran.Direction = ParameterDirection.Input;
                        pmax_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmax_tran);

                        DbParameter pcobra_cuota_manejo = cmdTransaccionFactory.CreateParameter();
                        pcobra_cuota_manejo.ParameterName = "p_cobra_cuota_manejo";
                        if (pAsignacionTarjeta.cobra_cuota_manejo == null)
                            pcobra_cuota_manejo.Value = DBNull.Value;
                        else
                            pcobra_cuota_manejo.Value = pAsignacionTarjeta.cobra_cuota_manejo;
                        pcobra_cuota_manejo.Direction = ParameterDirection.Input;
                        pcobra_cuota_manejo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_cuota_manejo);

                        DbParameter pcuota_manejo = cmdTransaccionFactory.CreateParameter();
                        pcuota_manejo.ParameterName = "p_cuota_manejo";
                        if (pAsignacionTarjeta.cuota_manejo == null)
                            pcuota_manejo.Value = DBNull.Value;
                        else
                            pcuota_manejo.Value = pAsignacionTarjeta.cuota_manejo;
                        pcuota_manejo.Direction = ParameterDirection.Input;
                        pcuota_manejo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_manejo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAsignacionTarjeta.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter p_cod_asesor = cmdTransaccionFactory.CreateParameter();
                        p_cod_asesor.ParameterName = "p_cod_asesor";
                        if (pAsignacionTarjeta.cod_asesor.HasValue && pAsignacionTarjeta.cod_asesor != 0)
                        {
                            p_cod_asesor.Value = pAsignacionTarjeta.cod_asesor;
                        }
                        else
                        {
                            p_cod_asesor.Value = DBNull.Value;
                        }

                        p_cod_asesor.Direction = ParameterDirection.Input;
                        p_cod_asesor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_asesor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_TARJETA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAsignacionTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AsignacionTarjetaData", "CrearAsignacionTarjeta", ex);
                        return null;
                    }
                }
            }
        }
        public Tarjeta ConsultarValoresAhorros(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Tarjeta entidad = new Tarjeta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT saldo_total, saldo_canje from ahorro_vista where numero_cuenta= " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["saldo_total"]);
                            if (resultado["saldo_canje"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["saldo_canje"]);

                        }
                        else
                        {
                            //throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Data", "ConsultarValoresAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public Tarjeta ActualizarSaldoTarjeta(Tarjeta tarjeta, ref string error, Usuario usuario)
        {
            error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDTARJETA = cmdTransaccionFactory.CreateParameter();
                        P_IDTARJETA.Direction = ParameterDirection.Input;
                        P_IDTARJETA.ParameterName = "P_IDTARJETA";
                        P_IDTARJETA.Value = tarjeta.idtarjeta;
                        P_IDTARJETA.DbType = DbType.Int64;

                        DbParameter P_NUMERO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_CUENTA.Direction = ParameterDirection.Input;
                        P_NUMERO_CUENTA.ParameterName = "P_NUMERO_CUENTA";
                        P_NUMERO_CUENTA.Value = tarjeta.numero_cuenta;

                        DbParameter P_SALDO_DISPONIBLE = cmdTransaccionFactory.CreateParameter();
                        P_SALDO_DISPONIBLE.Direction = ParameterDirection.InputOutput;
                        P_SALDO_DISPONIBLE.ParameterName = "P_SALDO_DISPONIBLE";
                        if (tarjeta.saldo_disponible == null)
                            P_SALDO_DISPONIBLE.Value = DBNull.Value;
                        else
                            P_SALDO_DISPONIBLE.Value = tarjeta.saldo_disponible;
                        P_SALDO_DISPONIBLE.DbType = DbType.Decimal;

                        cmdTransaccionFactory.Parameters.Add(P_IDTARJETA);
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(P_SALDO_DISPONIBLE);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ACT_SALDOS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Tarjeta Entidad = new Tarjeta();
                        Entidad.idtarjeta = tarjeta.idtarjeta;
                        Entidad.numero_cuenta = tarjeta.numero_cuenta;
                        Entidad.tipo_cuenta = tarjeta.tipo_cuenta;

                        Configuracion conf = new Configuracion();
                        if (P_SALDO_DISPONIBLE.Value.ToString() != "")
                            Entidad.saldo_disponible = Convert.ToDecimal(P_SALDO_DISPONIBLE.Value.ToString().Replace(",", conf.ObtenerSeparadorDecimalConfig()));
                        else
                            Entidad.saldo_disponible = 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return Entidad;

                    }
                    catch (Exception ex)
                    {
                        error += " ERROR Id.Tarjeta:" + tarjeta.idtarjeta + " Saldo Disponible:" + tarjeta.saldo_disponible;
                        BOExcepcion.Throw("TarjetaData", "ActualizarSaldoTarjeta", ex);
                        return null;
                    }
                }

            }
        }
        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadasXSaldo(int numeroDeDiasParaBloquearTarjetas, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.TIPO_CUENTA, tar.ESTADO, per.nombre, per.identificacion, 
                                        ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) as DiasMora,
                                        CASE tar.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' END as desc_estado,
                                        CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                        FROM CREDITO cre
                                        JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                        JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                        JOIN V_PERSONA per ON per.COD_PERSONA = cre.COD_DEUDOR
                                        WHERE lin.TIPO_LINEA = 2 And tar.tipo_cuenta = 2 And tar.estado = 1  And cre.estado = 'C' And cre.FECHA_PROXIMO_PAGO IS NOT NULL
                                        And ESNULO(FECDIFDIA(cre.FECHA_PROXIMO_PAGO, TRUNC(sysdate), 2), 0) >= " + numeroDeDiasParaBloquearTarjetas + " And (tar.ESTADO_SALDO = 0 OR tar.ESTADO_SALDO Is Null)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();

                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.cod_tipocta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["DiasMora"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DiasMora"]);
                            if (resultado["desc_estado"] != DBNull.Value) entidad.desc_estado = Convert.ToString(resultado["desc_estado"]);
                            if (resultado["desc_tipo_cuenta"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["desc_tipo_cuenta"]);
                            lstTarjeta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListarTarjetasEnMoraYNoBloqueadasXSaldo", ex);
                        return null;
                    }
                }
            }
        }
        public List<Tarjeta> ListarTarjetasBloqueadasYAlDiaXSaldo(Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tarjeta> lstTarjeta = new List<Tarjeta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tar.idtarjeta, tar.numero_tarjeta, tar.numero_cuenta, tar.tipo_cuenta, tar.estado, per.nombre, per.identificacion, 
                                        CASE tar.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' END as desc_estado,
                                        CASE tar.tipo_cuenta WHEN 1 THEN 'Ahorro' WHEN 2 THEN 'Credito Rotativo' END as desc_tipo_cuenta
                                        FROM CREDITO cre
                                        JOIN LINEASCREDITO lin ON lin.COD_LINEA_CREDITO = cre.COD_LINEA_CREDITO
                                        JOIN TARJETA tar ON tar.numero_cuenta = cre.NUMERO_RADICACION
                                        JOIN v_persona per on per.COD_PERSONA = cre.COD_DEUDOR
                                        WHERE lin.tipo_linea = 2 and tar.tipo_cuenta = 2 and cre.estado = 'C' and cre.FECHA_PROXIMO_PAGO IS NOT NULL and TRUNC(cre.FECHA_PROXIMO_PAGO) >= TRUNC(sysdate)
                                        and tar.SALDO_DISPONIBLE = 0
                                        and tar.ESTADO_SALDO = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tarjeta entidad = new Tarjeta();

                            if (resultado["IDTARJETA"] != DBNull.Value) entidad.idtarjeta = Convert.ToInt32(resultado["IDTARJETA"]);
                            if (resultado["NUMERO_TARJETA"] != DBNull.Value) entidad.numtarjeta = Convert.ToString(resultado["NUMERO_TARJETA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.cod_tipocta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["desc_estado"] != DBNull.Value) entidad.desc_estado = Convert.ToString(resultado["desc_estado"]);
                            if (resultado["desc_tipo_cuenta"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToString(resultado["desc_tipo_cuenta"]);

                            lstTarjeta.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjeta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListarTarjetasBloqueadasYAlDia", ex);
                        return null;
                    }
                }
            }
        }


    }
}