using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class LiquidacionCesantiasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionCesantiasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Tuple<List<LiquidacionCesantiasDetalle>, List<LiquidacionCesantiasDetEmpleado>, List<NovedadCesantias>> GenerarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = pLiquidacionCesantias.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_anio = cmdTransaccionFactory.CreateParameter();
                        p_anio.ParameterName = "p_anio";
                        p_anio.Value = pLiquidacionCesantias.anio;
                        p_anio.Direction = ParameterDirection.Input;
                        p_anio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_anio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_GENLIQCESANTIAS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionCesantiasDetalle> LiquidacionCesantiasDetalle = new List<LiquidacionCesantiasDetalle>();
                        List<LiquidacionCesantiasDetEmpleado> liquidacionCesantiasDetalleEmpleados = new List<LiquidacionCesantiasDetEmpleado>();
                        List<NovedadCesantias> novedadesLiquidadas = new List<NovedadCesantias>();

                        string sql = @"select temp.*, ING.CODIGOCARGO, car.DESCRIPCION as desc_cargo
                                        from TEMP_CESANTIASEMP temp
                                        JOIN INGRESOPERSONAL ING on ING.CODIGOEMPLEADO = temp.CONSECUTIVOEMPLEADO   AND ING.ESTAACTIVOCONTRATO = 1 
                                        and Ing.Escontratoprestacional=0 and ING.ESSALARIOINTEGRAL=0  and ING.TIPOCOTIZANTE not in(4,5)  and ING.CODIGONOMINA = " + pLiquidacionCesantias.codigonomina +
                                        " JOIN CARGO_nomina car on car.idcargo = ing.codigocargo order by ING.CODIGOEMPLEADO asc";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetalle entidad = new LiquidacionCesantiasDetalle();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DIASLIQUIDAR"] != DBNull.Value) entidad.diasliquidar = Convert.ToInt64(resultado["DIASLIQUIDAR"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["INTERES"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["INTERES"]);

                            LiquidacionCesantiasDetalle.Add(entidad);
                        }

                        sql = @"select * from TEMP_CESANTIASEMPDET";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetEmpleado entidad = new LiquidacionCesantiasDetEmpleado();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["DESCRIPCIONNOVEDAD"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCIONNOVEDAD"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["TIPOCALCULONOVEDAD"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPOCALCULONOVEDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);

                            liquidacionCesantiasDetalleEmpleados.Add(entidad);
                        }

                        sql = @"select * from TEMP_CESANTIASNOVLIQUIDADAS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            NovedadCesantias entidad = new NovedadCesantias();

                            if (resultado["CONSECUTIVONOVEDAD"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVONOVEDAD"]);

                            novedadesLiquidadas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(LiquidacionCesantiasDetalle, liquidacionCesantiasDetalleEmpleados, novedadesLiquidadas);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "GenerarLiquidacionCesantias", ex);
                        return null;
                    }
                }
            }
        }

        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            DbDataReader resultado;
            int? tipoCalculo = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tipo FROM concepto_nomina WHERE CONSECUTIVO = " + codigoTipoNovedad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo"] != DBNull.Value) tipoCalculo = Convert.ToInt32(resultado["tipo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return tipoCalculo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionCesantias CrearLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionCesantias.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = vUsuario.codusuario;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = pLiquidacionCesantias.fechageneracion = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionCesantias.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionCesantias.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);


                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        pfechapago.Value = pLiquidacionCesantias.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pLiquidacionCesantias.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);


                        DbParameter P_LIQUIDOINTCESANTIAS = cmdTransaccionFactory.CreateParameter();
                        P_LIQUIDOINTCESANTIAS.ParameterName = "P_LIQUIDOINTCESANTIAS";
                        P_LIQUIDOINTCESANTIAS.Value = pLiquidacionCesantias.liquidainteres;
                        P_LIQUIDOINTCESANTIAS.Direction = ParameterDirection.Input;
                        P_LIQUIDOINTCESANTIAS.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_LIQUIDOINTCESANTIAS);




                        DbParameter PLIQUIDOCESANTIAS = cmdTransaccionFactory.CreateParameter();
                        PLIQUIDOCESANTIAS.ParameterName = "PLIQUIDOCESANTIAS";
                        PLIQUIDOCESANTIAS.Value = pLiquidacionCesantias.liquidacesantias;
                        PLIQUIDOCESANTIAS.Direction = ParameterDirection.Input;
                        PLIQUIDOCESANTIAS.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PLIQUIDOCESANTIAS);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUICES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionCesantias.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionCesantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearLiquidacionCesantias", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarNovedadPagada(NovedadPrima novedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = novedad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_APLICARNOVEDCESAN";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "AplicarNovedadPagada", ex);
                    }
                }
            }
        }


        public LiquidacionCesantiasDetalle CrearLiquidacionCesantiasDetalle(LiquidacionCesantiasDetalle pLiquidacionCesantiasDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionCesantiasDetalle.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacionprima = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacionprima.ParameterName = "p_codigoliquidacioncesantias";
                        pcodigoliquidacionprima.Value = pLiquidacionCesantiasDetalle.codigoliquidacionCesantias;
                        pcodigoliquidacionprima.Direction = ParameterDirection.Input;
                        pcodigoliquidacionprima.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacionprima);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionCesantiasDetalle.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);



                        DbParameter pdiasliquidar = cmdTransaccionFactory.CreateParameter();
                        pdiasliquidar.ParameterName = "p_diasliquidar";
                        pdiasliquidar.Value = pLiquidacionCesantiasDetalle.diasliquidar;
                        pdiasliquidar.Direction = ParameterDirection.Input;
                        pdiasliquidar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdiasliquidar);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionCesantiasDetalle.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionCesantiasDetalle.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter p_salario = cmdTransaccionFactory.CreateParameter();
                        p_salario.ParameterName = "p_salario";
                        p_salario.Value = pLiquidacionCesantiasDetalle.salario;
                        p_salario.Direction = ParameterDirection.Input;
                        p_salario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_salario);

                        DbParameter PCOD_OPE = cmdTransaccionFactory.CreateParameter();
                        PCOD_OPE.ParameterName = "P_COD_OPE";
                        PCOD_OPE.Value = pLiquidacionCesantiasDetalle.cod_ope;
                        PCOD_OPE.Direction = ParameterDirection.Input;
                        PCOD_OPE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCOD_OPE);




                        DbParameter P_CESANTIAS = cmdTransaccionFactory.CreateParameter();
                        P_CESANTIAS.ParameterName = "P_CESANTIAS";
                        if (pLiquidacionCesantiasDetalle.liquidaCesantias== 1)
                        {
                            P_CESANTIAS.Value = pLiquidacionCesantiasDetalle.valortotalpagar;
                        }
                        else
                        {
                            P_CESANTIAS.Value = 0;
                        }

                        P_CESANTIAS.Direction = ParameterDirection.Input;
                        P_CESANTIAS.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_CESANTIAS);



                        DbParameter P_INTERESES = cmdTransaccionFactory.CreateParameter();
                        P_INTERESES.ParameterName = "P_INTERESES";
                        if (pLiquidacionCesantiasDetalle.liquidainteres == 1)
                        {
                            P_INTERESES.Value = pLiquidacionCesantiasDetalle.interes;
                        }
                        else
                        {
                            P_INTERESES.Value = 0;
                        }

                        P_INTERESES.Direction = ParameterDirection.Input;
                        P_INTERESES.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_INTERESES);



                        DbParameter P_BASE_CESANTIAS = cmdTransaccionFactory.CreateParameter();
                        P_BASE_CESANTIAS.ParameterName = "P_BASE_CESANTIAS";
                        P_BASE_CESANTIAS.Value = pLiquidacionCesantiasDetalle.valortotalpagar;
                        P_BASE_CESANTIAS.Direction = ParameterDirection.Input;
                        P_BASE_CESANTIAS.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_BASE_CESANTIAS);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQCESADET_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionCesantiasDetalle.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionCesantiasDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearLiquidacionCesantiasDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionCesantiasDetEmpleado CrearLiquidacionCesantiasDetEmpleado(LiquidacionCesantiasDetEmpleado pLiquidacionCesantiasDetEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionCesantiasDetEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacionnprimadet = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacionnprimadet.ParameterName = "p_codigoliquidacionncesdet";
                        pcodigoliquidacionnprimadet.Value = pLiquidacionCesantiasDetEmpleado.codigoliquidacioncesantiasdetalle;
                        pcodigoliquidacionnprimadet.Direction = ParameterDirection.Input;
                        pcodigoliquidacionnprimadet.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacionnprimadet);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionCesantiasDetEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigotiponovedad = cmdTransaccionFactory.CreateParameter();
                        pcodigotiponovedad.ParameterName = "p_codigotiponovedad";
                        if (pLiquidacionCesantiasDetEmpleado.liquidainteres == 1)
                        {
                            pcodigotiponovedad.Value = 25;
                        }
                        else
                        {
                            pcodigotiponovedad.Value = pLiquidacionCesantiasDetEmpleado.codigotiponovedad;
                        }
                        pcodigotiponovedad.Direction = ParameterDirection.Input;
                        pcodigotiponovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiponovedad);

                        DbParameter pesnovedadcreadamanual = cmdTransaccionFactory.CreateParameter();
                        pesnovedadcreadamanual.ParameterName = "p_esnovedadcreadamanual";
                        pesnovedadcreadamanual.Value = pLiquidacionCesantiasDetEmpleado.esnovedadcreadamanual;
                        pesnovedadcreadamanual.Direction = ParameterDirection.Input;
                        pesnovedadcreadamanual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesnovedadcreadamanual);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pLiquidacionCesantiasDetEmpleado.liquidainteres == 1)
                        {
                            pvalor.Value = pLiquidacionCesantiasDetEmpleado.interes;
                        }
                        if (pLiquidacionCesantiasDetEmpleado.liquidaCesantias == 1)
                        { 
                        pvalor.Value = pLiquidacionCesantiasDetEmpleado.valor;

                        }
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CESDETEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionCesantiasDetEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionCesantiasDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearLiquidacionCesantiasDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionCesantias ModificarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionCesantias.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = pLiquidacionCesantias.codigousuariocreacion;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = pLiquidacionCesantias.fechageneracion;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionCesantias.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionCesantias.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionCesantias.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        pfechapago.Value = pLiquidacionCesantias.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pLiquidacionCesantias.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUICESAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiquidacionCesantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ModificarLiquidacionCesantias", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLiquidacionCesantias(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LiquidacionCesantias pLiquidacionCesantias = new LiquidacionCesantias();
                        pLiquidacionCesantias = ConsultarLiquidacionCesantias(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionCesantias.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUICESA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "EliminarLiquidacionCesantias", ex);
                    }
                }
            }
        }


        public LiquidacionCesantias ConsultarLiquidacionCesantias(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionCesantias entidad = new LiquidacionCesantias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT l.*,u.nombre FROM LiquidacionCesantias l left join usuarios u on u.CODUSUARIO= l.CODIGOUSUARIOCREACION  WHERE l.CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAGENERACION"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHAGENERACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_usuario = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LIQUIDOINTCESANTIAS"] != DBNull.Value) entidad.liquidainteres = Convert.ToInt64(resultado["LIQUIDOINTCESANTIAS"]);
                            if (entidad.liquidainteres == 1)
                                entidad.liquido_interes = "SI";
                            else
                                entidad.liquido_interes = "NO";                 

                            if (resultado["LIQUIDOCESANTIAS"] != DBNull.Value) entidad.liquidacesantias = Convert.ToInt64(resultado["LIQUIDOCESANTIAS"]);

                            if (entidad.liquidacesantias == 1)
                                entidad.liquido_cesantias = "SI";
                            else
                                entidad.liquido_cesantias = "NO";

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
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ConsultarLiquidacionCesantias", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionCesantias ConsultarUltpago(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionCesantias entidad = new LiquidacionCesantias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select max(ANIO) AS YEAR,fechapago from LiquidacionCesantias where LIQUIDOCESANTIAS=1 and codigonomina= " + pId.ToString() + " GROUP BY ANIO,fechapago order by ANIO desc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["YEAR"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["YEAR"]);
                            if (resultado["fechapago"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["fechapago"]);


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
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ConsultarUltpago", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionCesantias liquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            bool existe = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONSECUTIVO 
                                        FROM LiquidacionCesantias
                                        WHERE  LIQUIDOCESANTIAS=1 and CODIGONOMINA = " + liquidacion.codigonomina +
                                        " AND ANIO = " + liquidacion.anio;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value)
                                existe = true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo", ex);
                        return false;
                    }
                }
            }
        }

        public List<LiquidacionCesantias> ListarLiquidacionCesantias(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCesantias> lstLiquidacionPrima = new List<LiquidacionCesantias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"sELECT liq.*, nom.DESCRIPCION as desc_nomina, usu.NOMBRE as desc_usuario, cst.DESCRIPCION as desc_centro_costo,o.num_comp,o.tipo_comp
                                        FROM LiquidacionCesantias liq
                                        left JOIN NOMINA_EMPLEADO nom on nom.consecutivo = liq.CodigoNomina                                      
                                        left JOIN USUARIOS usu on usu.CODUSUARIO = liq.CODIGOUSUARIOCREACION
                                        left JOIN CENTRO_COSTO cst on cst.CENTRO_COSTO = liq.CODIGOCENTROCOSTO 
                                        LEFT JOIN (SELECT cod_ope,CODIGOLIQUIDACIONCESANTIAS 
                                                 FROM  LIQUIDACIONCESANTIASDETALLE GROUP BY cod_ope,CODIGOLIQUIDACIONCESANTIAS) A ON A.codigoliquidacioncesantias = liq.consecutivo
                                        LEFT JOIN OPERACION o on o.cod_ope = a.cod_ope "
                                        + filtro + " ORDER BY liq.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionCesantias entidad = new LiquidacionCesantias();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAGENERACION"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHAGENERACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_usuario"] != DBNull.Value) entidad.desc_usuario = Convert.ToString(resultado["desc_usuario"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);

                            if (resultado["LIQUIDOINTCESANTIAS"] != DBNull.Value) entidad.liquidainteres = Convert.ToInt64(resultado["LIQUIDOINTCESANTIAS"]);
                            if (entidad.liquidainteres == 1)
                                entidad.liquido_interes = "SI";
                            else
                                entidad.liquido_interes = "NO";

                            if (resultado["LIQUIDOCESANTIAS"] != DBNull.Value) entidad.liquidacesantias = Convert.ToInt64(resultado["LIQUIDOCESANTIAS"]);

                            if (entidad.liquidacesantias == 1)
                                entidad.liquido_cesantias = "SI";
                            else
                                entidad.liquido_cesantias = "NO";
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);




                            lstLiquidacionPrima.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ListarLiquidacionCesantias", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionCesantiasDetalle> ListarLiquidacionCesantiasDetalle(long codigoLiquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCesantiasDetalle> lstLiquidacionPrimaDetalle = new List<LiquidacionCesantiasDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, ing.CODIGOCARGO, car.DESCRIPCION as desc_cargo,lc.LIQUIDOINTCESANTIAS,lc.LIQUIDOCESANTIAS
                                        FROM LIQUIDACIONCESANTIASDETALLE liq
                                        JOIN LIQUIDACIONCESANTIAS lc on lc.consecutivo = LIQ.CODIGOLIQUIDACIONCESANTIAS
                                        JOIN EMPLEADOS emp on liq.CODIGOEMPLEADO = emp.consecutivo
                                        JOIN V_PERSONA per on per.cod_persona = emp.COD_PERSONA
                                        join ingresopersonal ing on emp.consecutivo = ing.CODIGOEMPLEADO and LIQ.CONSECUTIVOCONTRATO=ing.consecutivo and ing.CODIGONOMINA =  
                                        (
                                            SELECT CODIGONOMINA FROM LiquidacionCesantias WHERE CONSECUTIVO = " + codigoLiquidacion +
                                     @" )
                                        join CARGO_NOMINA car on car.IDCARGO = ing.codigocargo
                                        WHERE liq.codigoliquidacionCesantias = " + codigoLiquidacion + " order by liq.CODIGOEMPLEADO asc  ";
                                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetalle entidad = new LiquidacionCesantiasDetalle();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONCESANTIAS"] != DBNull.Value) entidad.codigoliquidacionCesantias = Convert.ToInt64(resultado["CODIGOLIQUIDACIONCESANTIAS"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CESANTIAS"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["CESANTIAS"]);
                            if (resultado["DIASLIQUIDAR"] != DBNull.Value) entidad.diasliquidar = Convert.ToInt64(resultado["DIASLIQUIDAR"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["salario"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["salario"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["intereses"] != DBNull.Value) entidad.interes = Convert.ToDecimal(resultado["intereses"]);
                            if (resultado["LIQUIDOINTCESANTIAS"] != DBNull.Value) entidad.liquidainteres = Convert.ToInt64(resultado["LIQUIDOINTCESANTIAS"]);   
                            if (resultado["LIQUIDOCESANTIAS"] != DBNull.Value) entidad.liquidaCesantias = Convert.ToInt64(resultado["LIQUIDOCESANTIAS"]);
                            if (resultado["SUBSISIDIOTRANSPORTE"] != DBNull.Value) entidad.subsidio = Convert.ToDecimal(resultado["SUBSISIDIOTRANSPORTE"]);
                            if (resultado["BASECESANTIAS"] != DBNull.Value) entidad.basecesantias = Convert.ToDecimal(resultado["BASECESANTIAS"]);



                            lstLiquidacionPrimaDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "ListarLiquidacionCesantiasDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarLiquidacionCesantiasDetEmpleado(long codigoLiquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCesantiasDetEmpleado> lstLiquidacionCesantiasDetEmpleado = new List<LiquidacionCesantiasDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liqDetEmp.*, tip.DESCRIPCION,tip.TIPO,lc.LIQUIDOINTCESANTIAS,lc.LIQUIDOCESANTIAS
                                        FROM LiquidacionCesantiasDetalle liqDet 
                                        JOIN LIQUIDACIONCESANTIASDETEMPLEAD liqDetEmp ON liqDet.CONSECUTIVO = liqDetEmp.CODIGOLIQUIDACIONCESANTIASDET
                                        JOIN LIQUIDACIONCESANTIAS lc on lc.consecutivo = liqDet.CODIGOLIQUIDACIONCESANTIAS
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = liqDetEmp.CODIGOTIPONOVEDAD
                                        WHERE liqDet.codigoliquidacionCesantias = " + codigoLiquidacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetEmpleado entidad = new LiquidacionCesantiasDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONCESANTIASDET"] != DBNull.Value) entidad.codigoliquidacioncesantiasdetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONCESANTIASDET"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            if (resultado["LIQUIDOINTCESANTIAS"] != DBNull.Value) entidad.liquidainteres = Convert.ToInt64(resultado["LIQUIDOINTCESANTIAS"]);


                            if (resultado["LIQUIDOCESANTIAS"] != DBNull.Value) entidad.liquidaCesantias = Convert.ToInt64(resultado["LIQUIDOCESANTIAS"]);


                            lstLiquidacionCesantiasDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionCesantiasDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasDetEmpleadoData", "ListarLiquidacionCesantiasDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleado(long paño, long pempleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCesantiasDetEmpleado> lstLiquidacionCesantiasDetEmpleado = new List<LiquidacionCesantiasDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADCESANTIAS nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=0  and nov.codigoempleado =" + pempleado
                                        + " and nov.anio =" + paño;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetEmpleado entidad = new LiquidacionCesantiasDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            // if (resultado["codigoliquidacioncesantiasdetalle"] != DBNull.Value) entidad.codigoliquidacioncesantiasdetalle = Convert.ToInt64(resultado["codigoliquidacioncesantiasdetalle"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionCesantiasDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionCesantiasDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasDetEmpleadoData", "ListarNovedadesCesantiasDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public List<NovedadPrima> ListarCesantiasNovedades(long paño, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<NovedadPrima> lstLiquidacionCesantiasDetEmpleado = new List<NovedadPrima>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADCESANTIAS nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=0   and nov.anio =" + paño;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            NovedadPrima entidad = new NovedadPrima();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            // if (resultado["codigoliquidacioncesantiasdetalle"] != DBNull.Value) entidad.codigoliquidacioncesantiasdetalle = Convert.ToInt64(resultado["codigoliquidacioncesantiasdetalle"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionCesantiasDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionCesantiasDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasDetEmpleadoData", "ListarCesantiasPrima", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleadoAplicada(long paño, long pempleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionCesantiasDetEmpleado> lstLiquidacionCesantiasDetEmpleado = new List<LiquidacionCesantiasDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADCESANTIAS nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=1  and nov.codigoempleado =" + pempleado
                                        + " and nov.anio =" + paño;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionCesantiasDetEmpleado entidad = new LiquidacionCesantiasDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            // if (resultado["codigoliquidacioncesantiasdetalle"] != DBNull.Value) entidad.codigoliquidacioncesantiasdetalle = Convert.ToInt64(resultado["codigoliquidacioncesantiasdetalle"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionCesantiasDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionCesantiasDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasDetEmpleadoData", "ListarNovedadesCesantiasDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearGirosDeLiquidacionNomina(LiquidacionCesantias liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter porigen = cmdTransaccionFactory.CreateParameter();
                        porigen.ParameterName = "PORIGEN";
                        porigen.Value = liquidacion.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        DbParameter pCod_ope = cmdTransaccionFactory.CreateParameter();
                        pCod_ope.ParameterName = "P_COD_OPE";
                        pCod_ope.Value = liquidacion.cod_ope;
                        pCod_ope.Direction = ParameterDirection.Input;
                        pCod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCod_ope);


                        DbParameter pCodigoNominaGirar = cmdTransaccionFactory.CreateParameter();
                        pCodigoNominaGirar.ParameterName = "pCodigoNominaGirar";
                        pCodigoNominaGirar.Value = liquidacion.consecutivo;
                        pCodigoNominaGirar.Direction = ParameterDirection.Input;
                        pCodigoNominaGirar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNominaGirar);

                        DbParameter pCodigoUsuario = cmdTransaccionFactory.CreateParameter();
                        pCodigoUsuario.ParameterName = "pCodigoUsuario";
                        pCodigoUsuario.Value = usuario.codusuario;
                        pCodigoUsuario.Direction = ParameterDirection.Input;
                        pCodigoUsuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoUsuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQGIROCREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearGirosDeLiquidacionNomina", ex);
                    }
                }
            }
        }


        public LiquidacionCesantiasDetEmpleado CrearLiquidacionNominaInterfaz(LiquidacionCesantiasDetEmpleado pLiquidacionNominaDetaEmpleado, LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNominaDetaEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);



                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaDetaEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        if(pLiquidacionNominaDetaEmpleado.liquidainteres==1)
                        {
                            pcodigoconcepto.Value = 25;
                        }
                        else
                        { 
                        pcodigoconcepto.Value = pLiquidacionNominaDetaEmpleado.codigotiponovedad;
                        }
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";

                        if (pLiquidacionNominaDetaEmpleado.liquidainteres == 1)
                        {
                            pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.interes;
                        }
                        else
                        {
                            pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.valor;

                        }
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        if (pLiquidacionNominaDetaEmpleado.cod_ope == 0)
                        {
                            pcodope.Value = pLiquidacionCesantias.cod_ope;
                        }
                        else
                        {
                            pcodope.Value = pLiquidacionNominaDetaEmpleado.cod_ope;
                        }
                        pcodope.Direction = ParameterDirection.Input;
                        pcodope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodope);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);


                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionCesantias.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);



                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pusuario);


                        DbParameter porigen = cmdTransaccionFactory.CreateParameter();
                        porigen.ParameterName = "p_origen";
                        porigen.Value = pLiquidacionCesantias.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INTERFAZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaDetaEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaDetaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearLiquidacionNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }

        public NovedadCesantias CrearNovedadCesantias(NovedadCesantias pNovedadCesantias, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pNovedadCesantias.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pNovedadCesantias.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                      

                        DbParameter pcodigotiponovedad = cmdTransaccionFactory.CreateParameter();
                        pcodigotiponovedad.ParameterName = "p_codigotiponovedad";
                        pcodigotiponovedad.Value = pNovedadCesantias.codigotiponovedad;
                        pcodigotiponovedad.Direction = ParameterDirection.Input;
                        pcodigotiponovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiponovedad);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pNovedadCesantias.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pnovedadfuepagada = cmdTransaccionFactory.CreateParameter();
                        pnovedadfuepagada.ParameterName = "p_novedadfuepagada";
                        pnovedadfuepagada.Value = pNovedadCesantias.novedadfuepagada;
                        pnovedadfuepagada.Direction = ParameterDirection.Input;
                        pnovedadfuepagada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnovedadfuepagada);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pNovedadCesantias.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pNovedadCesantias.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOV_CESA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pNovedadCesantias.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pNovedadCesantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionCesantiasData", "CrearNovedadCesantias", ex);
                        return null;
                    }
                }
            }
        }

    }
}