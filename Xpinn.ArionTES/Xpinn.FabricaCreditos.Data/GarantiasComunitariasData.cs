using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class GarantiasComunitariasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public GarantiasComunitariasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasCartera(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (cod == 0)
                            sql = "select * from v_grantiascomunitariasCartera where fecha_pago >= to_date ('" + Convert.ToDateTime(fechaini).ToShortDateString() + "', 'dd/mm/yyyy') and fecha_pago <= to_date ('" + Convert.ToDateTime(fechafin).ToShortDateString() + "', 'dd/mm/yyyy')";
                        else
                            sql = "select * from v_grantiascomunitariasCartera where fecha_pago >=to_date ('" + Convert.ToDateTime(fechaini).ToShortDateString() + "', 'dd/mm/yyyy') and fecha_pago <= to_date ('" + Convert.ToDateTime(fechafin).ToShortDateString() + "', 'dd/mm/yyyy')  and cod_oficina=" + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["VALOR_PAGADO"] != DBNull.Value) entidad.VALOR_PAGADO = Convert.ToInt64(resultado["VALOR_PAGADO"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.FECHA_DESEMBOLSO = Convert.ToDateTime(resultado["FECHA_PAGO"]).ToString("dd/MM/yyyy");

                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamaciones(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "";
                        if (cod == 0)
                            sql = @"select cred.cod_oficina, (select nit from empresa)as nit, per.identificacion,cred.numero_radicacion,cred.fecha_proximo_pago, round(to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') - cred.fecha_proximo_pago) as diasmora From credito cred 
                                     Inner join persona per on cred.cod_deudor = per.cod_persona where cred.fecha_proximo_pago <= to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                   " And cred.estado = 'C' And cred.saldo_capital > 0 And (cred.cod_linea_credito In ('106', '306') Or (cred.cod_linea_credito In (110, 310) And cred.numero_radicacion In (Select d.numero_radicacion From descuentoscredito d Where d.cod_atr In (14, 15) And d.numero_radicacion = cred.numero_radicacion)))";
                        else
                            sql = @"select cred.cod_oficina, (select nit from empresa)as nit, per.identificacion,cred.numero_radicacion,cred.fecha_proximo_pago, round(to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') - cred.fecha_proximo_pago) as diasmora From credito cred  
                                     Inner join persona per on cred.cod_deudor = per.cod_persona where cred.fecha_proximo_pago <= to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                   " And cred.estado = 'C' And cred.saldo_capital > 0 And (cred.cod_linea_credito In ('106', '306') Or (cred.cod_linea_credito In (110, 310) And cred.numero_radicacion In (Select d.numero_radicacion From descuentoscredito d Where d.cod_atr In (14, 15) And d.numero_radicacion = cred.numero_radicacion))) And cred.cod_oficina = " + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.NITENTIDAD = Convert.ToString(resultado["NIT"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.FECHAPROXPAGO = Convert.ToDateTime(resultado["fecha_proximo_pago"]).ToString("dd/MM/yyyy");
                            if (resultado["diasmora"] != DBNull.Value) entidad.DIASMORA = Convert.ToString(resultado["diasmora"]);
                            if (entidad.NUMERO_RADICACION != null)
                            {
                                // Calculando los valores adeudados por cada atributo para el crédito correspondiente.
                                using (DbConnection connection2 = dbConnectionFactory.ObtenerConexion(pUsuario))
                                {
                                    using (DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                                    {
                                        DbParameter pnumero_radicacion = cmdTransaccionFactory2.CreateParameter();
                                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                                        pnumero_radicacion.Value = Convert.ToInt32(entidad.NUMERO_RADICACION);
                                        pnumero_radicacion.DbType = DbType.Int32;

                                        DbParameter pfecha = cmdTransaccionFactory2.CreateParameter();
                                        pfecha.ParameterName = "pfecha";
                                        pfecha.Value = Convert.ToDateTime(fechaini);
                                        pfecha.DbType = DbType.Date;

                                        DbParameter pcapital = cmdTransaccionFactory2.CreateParameter();
                                        pcapital.ParameterName = "pcapital";
                                        pcapital.Value = 0;
                                        pcapital.Direction = ParameterDirection.Output;
                                        pcapital.DbType = DbType.Int32;

                                        DbParameter pinteres_corriente = cmdTransaccionFactory2.CreateParameter();
                                        pinteres_corriente.ParameterName = "pinteres_corriente";
                                        pinteres_corriente.Value = 0;
                                        pinteres_corriente.Direction = ParameterDirection.Output;
                                        pinteres_corriente.DbType = DbType.Int32;

                                        DbParameter pinteres_mora = cmdTransaccionFactory2.CreateParameter();
                                        pinteres_mora.ParameterName = "pinteres_mora";
                                        pinteres_mora.Value = 0;
                                        pinteres_mora.Direction = ParameterDirection.Output;
                                        pinteres_mora.DbType = DbType.Int32;

                                        DbParameter potros = cmdTransaccionFactory2.CreateParameter();
                                        potros.ParameterName = "potros";
                                        potros.Value = 0;
                                        potros.Direction = ParameterDirection.Output;
                                        potros.DbType = DbType.Int32;

                                        DbParameter pcuotas = cmdTransaccionFactory2.CreateParameter();
                                        pcuotas.ParameterName = "pcuotas";
                                        pcuotas.Value = 0;
                                        pcuotas.Direction = ParameterDirection.Output;
                                        pcuotas.DbType = DbType.Int32;


                                        cmdTransaccionFactory2.Parameters.Add(pnumero_radicacion);
                                        cmdTransaccionFactory2.Parameters.Add(pfecha);
                                        cmdTransaccionFactory2.Parameters.Add(pcapital);
                                        cmdTransaccionFactory2.Parameters.Add(pinteres_corriente);
                                        cmdTransaccionFactory2.Parameters.Add(pinteres_mora);
                                        cmdTransaccionFactory2.Parameters.Add(potros);
                                        cmdTransaccionFactory2.Parameters.Add(pcuotas);

                                        connection2.Open();
                                        cmdTransaccionFactory2.Connection = connection2;
                                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory2.CommandText = "USP_XPINN_CRE_RECLAMACIONES";
                                        cmdTransaccionFactory2.ExecuteNonQuery();

                                        entidad.CAPITAL = Convert.ToDouble(pcapital.Value);
                                        entidad.INT_CORRIENTES = Convert.ToDouble(pinteres_corriente.Value) + Convert.ToDouble(potros.Value);
                                        entidad.INT_MORA = Convert.ToDouble(pinteres_mora.Value);
                                        entidad.CUOTAS_RECLAMAR = Convert.ToString(pcuotas.Value);
                                    }
                                }

                            }


                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitarias(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (cod == 0)
                            sql = "select * from v_grantiascomunitarias where fecha >= to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and fecha <= to_date ('" + Convert.ToDateTime(fechafin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and (estado='C' OR estado='T')";
                        else
                            sql = "select * from v_grantiascomunitarias where fecha >= to_date ('" + Convert.ToDateTime(fechaini).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and fecha <= to_date ('" + Convert.ToDateTime(fechafin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')  and (estado='C' OR estado='T')  and  cod_oficina = " + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["CONVENIO"] != DBNull.Value) entidad.CONVENIO = Convert.ToString(resultado["CONVENIO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.ENTIDAD = Convert.ToString(resultado["ENTIDAD"]);
                            if (resultado["NITENTIDAD"] != DBNull.Value) entidad.NITENTIDAD = Convert.ToString(resultado["NITENTIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.OFICINA = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["DESEMBOLSOFECHA"] != DBNull.Value) entidad.DESEMBOLSOFECHA = Convert.ToString(resultado["DESEMBOLSOFECHA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.NOMBRES = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.APELLIDOS = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["SUCURSALOFICINA"] != DBNull.Value) entidad.SUCURSALOFICINA = Convert.ToString(resultado["SUCURSALOFICINA"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.TELEFONO = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.NOMCIUDAD = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.BARRIO = Convert.ToString(resultado["BARRIO"]);
                            if (resultado["ACTIVIDAD"] != DBNull.Value) entidad.ACTIVIDAD = Convert.ToString(resultado["ACTIVIDAD"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.PAGARE = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["PAGARE2"] != DBNull.Value) entidad.PAGARE2 = Convert.ToString(resultado["PAGARE2"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.MONTO_APROBADO = Convert.ToDouble(resultado["MONTO_APROBADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.DESCRIPCION = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.VALOR_CUOTA = Convert.ToString(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.NUMERO_CUOTAS = Convert.ToString(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FECHA_DESEMBOLSO = Convert.ToString(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.FECHA_VENCIMIENTO = Convert.ToString(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TIPOPRODUCTO"] != DBNull.Value) entidad.TIPOPRODUCTO = Convert.ToString(resultado["TIPOPRODUCTO"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.PERIODO_GRACIA = Convert.ToString(resultado["PERIODO_GRACIA"]);
                            if (resultado["CAPITAL"] != DBNull.Value) entidad.CAPITAL = Convert.ToDouble(resultado["CAPITAL"]);
                            if (resultado["PERIODO"] != DBNull.Value) entidad.PERIODO = Convert.ToString(resultado["PERIODO"]);
                            if (resultado["ASMODALIDAD"] != DBNull.Value) entidad.ASMODALIDAD = Convert.ToString(resultado["ASMODALIDAD"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.TASA = Convert.ToString(resultado["TASA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.PORCENTAJE = Convert.ToString(resultado["PORCENTAJE"]);
                            if (resultado["MODADLIDADCOMISION"] != DBNull.Value) entidad.MODADLIDADCOMISION = Convert.ToString(resultado["MODADLIDADCOMISION"]);
                            if (resultado["TIPOCOMISION"] != DBNull.Value) entidad.TIPOCOMISION = Convert.ToString(resultado["TIPOCOMISION"]);
                            if (resultado["VALORCOMISION"] != DBNull.Value) entidad.VALORCOMISION = Convert.ToDouble(resultado["VALORCOMISION"]);
                            if (resultado["VALORIVACOMISION"] != DBNull.Value) entidad.VALORIVACOMISION = Convert.ToDouble(resultado["VALORIVACOMISION"]);
                            if (resultado["VALORTOTALCOMISION"] != DBNull.Value) entidad.VALORTOTALCOMISION = Convert.ToDouble(resultado["VALORTOTALCOMISION"]);
                            if (resultado["NOMCODEUDOR"] != DBNull.Value) entidad.NOMCODEUDOR = Convert.ToString(resultado["NOMCODEUDOR"]);
                            if (resultado["APELLIDOCODEUDOR"] != DBNull.Value) entidad.APELLIDOCODEUDOR = Convert.ToString(resultado["APELLIDOCODEUDOR"]);
                            if (resultado["IDENTIFICACIONCODEUDOR"] != DBNull.Value) entidad.IDENTIFICACIONCODEUDOR = Convert.ToString(resultado["IDENTIFICACIONCODEUDOR"]);
                            if (resultado["SUCURSAL"] != DBNull.Value) entidad.SUCURSAL = Convert.ToString(resultado["SUCURSAL"]);
                            if (resultado["TELEFONOCODEUDOR"] != DBNull.Value) entidad.TELEFONOCODEUDOR = Convert.ToString(resultado["TELEFONOCODEUDOR"]);
                            if (resultado["DIRECCIONCODEUDOR"] != DBNull.Value) entidad.DIRECCIONCODEUDOR = Convert.ToString(resultado["DIRECCIONCODEUDOR"]);
                            if (resultado["CIUCODEUDOR"] != DBNull.Value) entidad.CIUCODEUDOR = Convert.ToString(resultado["CIUCODEUDOR"]);
                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<GarantiasComunitarias> consultargarantiasconsultarReclamacion(string fechaini, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ID_RERCLAMCION, NOMBRE, FECHARECLAMACION from  GCRECLAMACION
                                        inner join  usuarios  on usuarios.codusuario = GCRECLAMACION.usuario ";
                        if (fechaini != "")
                            sql = sql + "WHERE FECHARECLAMACION = to_date ('" + Convert.ToDateTime(fechaini).ToShortDateString() + "', 'dd/mm/yyyy') "; 
                        sql = sql + "ORDER BY ID_RERCLAMCION";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["ID_RERCLAMCION"] != DBNull.Value) entidad.COD_IDENT = Convert.ToInt32(resultado["ID_RERCLAMCION"]);
                            if (resultado["FECHARECLAMACION"] != DBNull.Value) entidad.FECHARECLAMACION = Convert.ToDateTime(resultado["FECHARECLAMACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRES = Convert.ToString(resultado["NOMBRE"]);

                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamacionesdetalle(string fechaini, int reclamacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (reclamacion == 0)
                            sql = "select  * from  GCRECLAMACION_DETALLE WHERE fecha_proximo_pago = '" + Convert.ToDateTime(fechaini).ToShortDateString() + "'";
                        else
                            sql = "select  * from  GCRECLAMACION_DETALLE WHERE ID_RECLAMACION = " + reclamacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.NITENTIDAD = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.DIASMORA = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["TIPO_RECLAMACION"] != DBNull.Value)
                            {
                                switch (Convert.ToString(resultado["TIPO_RECLAMACION"]))
                                {
                                    case "1": entidad.RECLAMACION = "Reclamación en cuotas"; break;
                                    case "2": entidad.RECLAMACION = "Reclamación Total"; break;
                                }
                            }
                            if (resultado["CAPITAL"] != DBNull.Value) entidad.CAPITAL = Convert.ToDouble(resultado["CAPITAL"]);
                            if (resultado["INT_CORRIENTES"] != DBNull.Value) entidad.INT_CORRIENTES = Convert.ToDouble(resultado["INT_CORRIENTES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["INT_MORA"] != DBNull.Value) entidad.INT_MORA = Convert.ToDouble(resultado["INT_MORA"]);
                            if (resultado["CUOTAS_RECLAMAR"] != DBNull.Value) entidad.CUOTAS_RECLAMAR = Convert.ToString(resultado["CUOTAS_RECLAMAR"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.FECHAPROXPAGO = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);

                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public List<GarantiasComunitarias> ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(Int64 numero_reclamacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasComunitarias> lstGarantia = new List<GarantiasComunitarias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From GCRECLAMACION_DETALLE_PAGO Where id_reclamacion = " + numero_reclamacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GarantiasComunitarias entidad = new GarantiasComunitarias();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["NIT"] != DBNull.Value) entidad.NITENTIDAD = Convert.ToString(resultado["NIT"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.DIASMORA = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["TIPO_RECLAMACION"] != DBNull.Value) entidad.RECLAMACION = Convert.ToString(resultado["TIPO_RECLAMACION"]);
                            if (resultado["CAPITAL"] != DBNull.Value) entidad.CAPITAL = Convert.ToDouble(resultado["CAPITAL"]);
                            if (resultado["INT_CORRIENTES"] != DBNull.Value) entidad.INT_CORRIENTES = Convert.ToDouble(resultado["INT_CORRIENTES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["INT_MORA"] != DBNull.Value) entidad.INT_MORA = Convert.ToDouble(resultado["INT_MORA"]);
                            if (resultado["CUOTAS_RECLAMAR"] != DBNull.Value) entidad.CUOTAS_RECLAMAR = Convert.ToString(resultado["CUOTAS_RECLAMAR"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.FECHAPROXPAGO = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);

                            lstGarantia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReferenciaData", "ConsultarReferencia", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearReclamacion(Usuario pUsuario, string fecha)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_FECHARECLAMACION = cmdTransaccionFactory.CreateParameter();
                        P_FECHARECLAMACION.ParameterName = "P_FECHARECLAMACION";
                        P_FECHARECLAMACION.Value = fecha;
                        P_FECHARECLAMACION.DbType = DbType.Date;
                        P_FECHARECLAMACION.Direction = ParameterDirection.Input;

                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Value = pUsuario.codusuario;
                        P_USUARIO.DbType = DbType.String;
                        P_USUARIO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_FECHARECLAMACION);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECLAMACION_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("", "", ex);

                    }

                }
            }
        }

        public void CrearReclamacionDetalle(GarantiasComunitarias reclamaciones, Usuario pUsuario, string fecha)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RADICACION.ParameterName = "P_NUMERO_RADICACION";
                        P_NUMERO_RADICACION.Value = reclamaciones.NUMERO_RADICACION;
                        P_NUMERO_RADICACION.DbType = DbType.Int32;
                        P_NUMERO_RADICACION.Direction = ParameterDirection.Input;

                        DbParameter P_DIAS_MORA = cmdTransaccionFactory.CreateParameter();
                        P_DIAS_MORA.ParameterName = "P_DIAS_MORA";
                        P_DIAS_MORA.Value = reclamaciones.DIASMORA;
                        P_DIAS_MORA.DbType = DbType.Int64;
                        P_DIAS_MORA.Direction = ParameterDirection.Input;

                        DbParameter P_NIT = cmdTransaccionFactory.CreateParameter();
                        P_NIT.ParameterName = "P_NIT";
                        P_NIT.Value = reclamaciones.NITENTIDAD;
                        P_NIT.DbType = DbType.String;
                        P_NIT.Direction = ParameterDirection.Input;

                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = reclamaciones.IDENTIFICACION;
                        P_IDENTIFICACION.DbType = DbType.String;
                        P_IDENTIFICACION.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        P_FECHA_PROXIMO_PAGO.Value = reclamaciones.FECHAPROXPAGO;
                        P_FECHA_PROXIMO_PAGO.DbType = DbType.String;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;

                        DbParameter P_TIPO_RECLAMACION = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_RECLAMACION.ParameterName = "P_TIPO_RECLAMACION";
                        P_TIPO_RECLAMACION.Value = reclamaciones.RECLAMACION;
                        P_TIPO_RECLAMACION.DbType = DbType.Int64;
                        P_TIPO_RECLAMACION.Direction = ParameterDirection.Input;

                        DbParameter P_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        P_CAPITAL.ParameterName = "P_CAPITAL";
                        P_CAPITAL.Value = reclamaciones.CAPITAL;
                        P_CAPITAL.DbType = DbType.Int64;
                        P_CAPITAL.Direction = ParameterDirection.Input;

                        DbParameter P_INT_CORRIENTES = cmdTransaccionFactory.CreateParameter();
                        P_INT_CORRIENTES.ParameterName = "P_INT_CORRIENTES";
                        P_INT_CORRIENTES.Value = reclamaciones.INT_CORRIENTES;
                        P_INT_CORRIENTES.DbType = DbType.Int64;
                        P_INT_CORRIENTES.Direction = ParameterDirection.Input;

                        DbParameter P_INT_MORA = cmdTransaccionFactory.CreateParameter();
                        P_INT_MORA.ParameterName = "P_INT_MORA";
                        P_INT_MORA.Value = reclamaciones.INT_MORA;
                        P_INT_MORA.DbType = DbType.Int64;
                        P_INT_MORA.Direction = ParameterDirection.Input;

                        DbParameter P_CUOTAS_RECLAMAR = cmdTransaccionFactory.CreateParameter();
                        P_CUOTAS_RECLAMAR.ParameterName = "P_CUOTAS_RECLAMAR";
                        P_CUOTAS_RECLAMAR.Value = reclamaciones.CUOTAS_RECLAMAR;
                        P_CUOTAS_RECLAMAR.DbType = DbType.Int64;
                        P_CUOTAS_RECLAMAR.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_DIAS_MORA);
                        cmdTransaccionFactory.Parameters.Add(P_NIT);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_RECLAMACION);
                        cmdTransaccionFactory.Parameters.Add(P_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(P_INT_CORRIENTES);
                        cmdTransaccionFactory.Parameters.Add(P_INT_MORA);
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(P_CUOTAS_RECLAMAR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECLAMACION_CRE_DET";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("", "", ex);

                    }

                }
            }
        }

        public void CrearReclamacionPago(GarantiasComunitarias reclamaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_FECHARECLAMACION = cmdTransaccionFactory.CreateParameter();
                        P_FECHARECLAMACION.ParameterName = "P_FECHARECLAMACION";
                        P_FECHARECLAMACION.Value = reclamaciones.FECHAPROXPAGO;
                        P_FECHARECLAMACION.DbType = DbType.Date;
                        P_FECHARECLAMACION.Direction = ParameterDirection.Input;

                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Value = pUsuario.codusuario;
                        P_USUARIO.DbType = DbType.String;
                        P_USUARIO.Direction = ParameterDirection.Input;

                        DbParameter P_NUMERO_RECLAMACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RECLAMACION.ParameterName = "P_NUMERO_RECLAMACION";
                        P_NUMERO_RECLAMACION.Value = 0;
                        P_NUMERO_RECLAMACION.DbType = DbType.Int64;
                        P_NUMERO_RECLAMACION.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(P_FECHARECLAMACION);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RECLAMACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECLAMPAG_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        reclamaciones.numero_reclamacion = Convert.ToInt64(P_NUMERO_RECLAMACION.Value.ToString());
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("", "", ex);

                    }

                }
            }
        }

        public void CrearReclamacionPagoDetalle(GarantiasComunitarias reclamaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_RADICACION.ParameterName = "P_NUMERO_RADICACION";
                        P_NUMERO_RADICACION.Value = reclamaciones.NUMERO_RADICACION;
                        P_NUMERO_RADICACION.DbType = DbType.Int32;
                        P_NUMERO_RADICACION.Direction = ParameterDirection.Input;

                        DbParameter P_DIAS_MORA = cmdTransaccionFactory.CreateParameter();
                        P_DIAS_MORA.ParameterName = "P_DIAS_MORA";
                        P_DIAS_MORA.Value = reclamaciones.DIASMORA;
                        P_DIAS_MORA.DbType = DbType.Int64;
                        P_DIAS_MORA.Direction = ParameterDirection.Input;

                        DbParameter P_NIT = cmdTransaccionFactory.CreateParameter();
                        P_NIT.ParameterName = "P_NIT";
                        P_NIT.Value = reclamaciones.NITENTIDAD;
                        P_NIT.DbType = DbType.String;
                        P_NIT.Direction = ParameterDirection.Input;

                        DbParameter P_IDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        P_IDENTIFICACION.ParameterName = "P_IDENTIFICACION";
                        P_IDENTIFICACION.Value = reclamaciones.IDENTIFICACION;
                        P_IDENTIFICACION.DbType = DbType.String;
                        P_IDENTIFICACION.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_PROXIMO_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PROXIMO_PAGO.ParameterName = "P_FECHA_PROXIMO_PAGO";
                        P_FECHA_PROXIMO_PAGO.Value = reclamaciones.FECHAPROXPAGO;
                        P_FECHA_PROXIMO_PAGO.DbType = DbType.String;
                        P_FECHA_PROXIMO_PAGO.Direction = ParameterDirection.Input;

                        DbParameter P_TIPO_RECLAMACION = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_RECLAMACION.ParameterName = "P_TIPO_RECLAMACION";
                        P_TIPO_RECLAMACION.Value = reclamaciones.RECLAMACION;
                        P_TIPO_RECLAMACION.DbType = DbType.Int64;
                        P_TIPO_RECLAMACION.Direction = ParameterDirection.Input;

                        DbParameter P_CAPITAL = cmdTransaccionFactory.CreateParameter();
                        P_CAPITAL.ParameterName = "P_CAPITAL";
                        P_CAPITAL.Value = reclamaciones.CAPITAL;
                        P_CAPITAL.DbType = DbType.Int64;
                        P_CAPITAL.Direction = ParameterDirection.Input;

                        DbParameter P_INT_CORRIENTES = cmdTransaccionFactory.CreateParameter();
                        P_INT_CORRIENTES.ParameterName = "P_INT_CORRIENTES";
                        P_INT_CORRIENTES.Value = reclamaciones.INT_CORRIENTES;
                        P_INT_CORRIENTES.DbType = DbType.Int64;
                        P_INT_CORRIENTES.Direction = ParameterDirection.Input;

                        DbParameter P_INT_MORA = cmdTransaccionFactory.CreateParameter();
                        P_INT_MORA.ParameterName = "P_INT_MORA";
                        P_INT_MORA.Value = reclamaciones.INT_MORA;
                        P_INT_MORA.DbType = DbType.Int64;
                        P_INT_MORA.Direction = ParameterDirection.Input;

                        DbParameter P_CUOTAS_RECLAMAR = cmdTransaccionFactory.CreateParameter();
                        P_CUOTAS_RECLAMAR.ParameterName = "P_CUOTAS_RECLAMAR";
                        P_CUOTAS_RECLAMAR.Value = reclamaciones.CUOTAS_RECLAMAR;
                        P_CUOTAS_RECLAMAR.DbType = DbType.Int64;
                        P_CUOTAS_RECLAMAR.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(P_DIAS_MORA);
                        cmdTransaccionFactory.Parameters.Add(P_NIT);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PROXIMO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_RECLAMACION);
                        cmdTransaccionFactory.Parameters.Add(P_CAPITAL);
                        cmdTransaccionFactory.Parameters.Add(P_INT_CORRIENTES);
                        cmdTransaccionFactory.Parameters.Add(P_INT_MORA);
                        cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(P_CUOTAS_RECLAMAR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECLAMPAGDET_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("", "", ex);

                    }

                }
            }
        }


        public void AplicarPago(GarantiasComunitarias reclamaciones, Int64 pcod_ope, Usuario pUsuario, Int64 pnerror, ref string Error)
        {
            pnerror = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        if (reclamaciones.NUMERO_RADICACION == null)
                            pn_num_producto.Value = DBNull.Value;
                        else
                            pn_num_producto.Value = reclamaciones.NUMERO_RADICACION;
                        pn_num_producto.DbType = DbType.Int64;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        if (reclamaciones.COD_IDENT == null)
                            pn_cod_cliente.Value = DBNull.Value;
                        else
                            pn_cod_cliente.Value = reclamaciones.COD_IDENT;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = pcod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = reclamaciones.FECHARECLAMACION;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        if (reclamaciones.VALOR_PAGADO == null)
                            pn_valor_pago.Value = DBNull.Value;
                        else
                            pn_valor_pago.Value = reclamaciones.VALOR_PAGADO;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_num_cuotas = cmdTransaccionFactory.CreateParameter();
                        pn_num_cuotas.ParameterName = "pn_num_cuotas";
                        if (reclamaciones.RECLAMACION == null)
                            pn_num_cuotas.Value = DBNull.Value;
                        else
                            pn_num_cuotas.Value = reclamaciones.RECLAMACION;
                        pn_num_cuotas.DbType = DbType.Double;
                        pn_num_cuotas.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        if (reclamaciones.RECLAMACION == "2")
                            pn_tipo_tran.Value = 5;
                        else
                            pn_tipo_tran.Value = 4;
                        pn_tipo_tran.DbType = DbType.Int64;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_recuado = cmdTransaccionFactory.CreateParameter();
                        pn_cod_recuado.ParameterName = "pn_cod_recuado";
                        pn_cod_recuado.Value = DBNull.Value;
                        pn_cod_recuado.DbType = DbType.Int64;
                        pn_cod_recuado.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        if (reclamaciones.SOBRANTE == null)
                            rn_sobrante.Value = DBNull.Value;
                        else
                            rn_sobrante.Value = reclamaciones.SOBRANTE;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "n_error";
                        n_error.Value = pnerror;
                        n_error.DbType = DbType.Int64;
                        n_error.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_num_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_recuado);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_APLICARPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);                        
                    }

                }
            }
        }


        public void Validar(GarantiasComunitarias reclamaciones, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        pn_num_producto.Value = reclamaciones.NUMERO_RADICACION;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = reclamaciones.COD_IDENT;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = reclamaciones.FECHARECLAMACION;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = reclamaciones.VALOR_PAGADO;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        pn_tipo_tran.Value = 4;
                        pn_tipo_tran.DbType = DbType.Int64;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = reclamaciones.SOBRANTE;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_VALIDARPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        //BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }


    }
}