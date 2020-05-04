using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class LiquidacionVacacionesEmpleadoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionVacacionesEmpleadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public LiquidacionVacacionesEmpleado CrearLiquidacionVacacionesEmpleado(LiquidacionVacacionesEmpleado pLiquidacionVacacionesEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionVacacionesEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionVacacionesEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionVacacionesEmpleado.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionVacacionesEmpleado.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionVacacionesEmpleado.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pdiasliquidados = cmdTransaccionFactory.CreateParameter();
                        pdiasliquidados.ParameterName = "p_diasliquidados";
                        pdiasliquidados.Value = pLiquidacionVacacionesEmpleado.diasliquidados;
                        pdiasliquidados.Direction = ParameterDirection.Input;
                        pdiasliquidados.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdiasliquidados);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionVacacionesEmpleado.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter p_centroCosto = cmdTransaccionFactory.CreateParameter();
                        p_centroCosto.ParameterName = "p_centroCosto";
                        p_centroCosto.Value = pLiquidacionVacacionesEmpleado.codigocentrocosto;
                        p_centroCosto.Direction = ParameterDirection.Input;
                        p_centroCosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_centroCosto);

                        DbParameter p_fechaPago = cmdTransaccionFactory.CreateParameter();
                        p_fechaPago.ParameterName = "p_fechaPago";
                        p_fechaPago.Value = pLiquidacionVacacionesEmpleado.fechaPago;
                        p_fechaPago.Direction = ParameterDirection.Input;
                        p_fechaPago.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaPago);


                        DbParameter P_FECHA_INICIOPERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_INICIOPERIODO.ParameterName = "P_FECHA_INICIO_PERIODO";
                        P_FECHA_INICIOPERIODO.Value = pLiquidacionVacacionesEmpleado.fechainicioperiodo;
                        P_FECHA_INICIOPERIODO.Direction = ParameterDirection.Input;
                        P_FECHA_INICIOPERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_INICIOPERIODO);


                        DbParameter P_FECHATERMINACION_PERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHATERMINACION_PERIODO.ParameterName = "P_FECHA_TERMINACION_PERIODO";
                        P_FECHATERMINACION_PERIODO.Value = pLiquidacionVacacionesEmpleado.fechaterminacionperiodo;
                        P_FECHATERMINACION_PERIODO.Direction = ParameterDirection.Input;
                        P_FECHATERMINACION_PERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHATERMINACION_PERIODO);

                        DbParameter P_FECHAREGRESO = cmdTransaccionFactory.CreateParameter();
                        P_FECHAREGRESO.ParameterName = "P_FECHA_REGRESO";
                        P_FECHAREGRESO.Value = pLiquidacionVacacionesEmpleado.fechafinvacaciones;
                        P_FECHAREGRESO.Direction = ParameterDirection.Input;
                        P_FECHAREGRESO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAREGRESO);

                        DbParameter P_DIAS_ADISFRUTAR = cmdTransaccionFactory.CreateParameter();
                        P_DIAS_ADISFRUTAR.ParameterName = "P_DIAS_A_DISFRUTAR";
                        P_DIAS_ADISFRUTAR.Value = pLiquidacionVacacionesEmpleado.diasdisfrutar;
                        P_DIAS_ADISFRUTAR.Direction = ParameterDirection.Input;
                        P_DIAS_ADISFRUTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DIAS_ADISFRUTAR);



                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pLiquidacionVacacionesEmpleado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter P_DIASPAGADOS= cmdTransaccionFactory.CreateParameter();
                        P_DIASPAGADOS.ParameterName = "P_DIAS_PAGADOS";
                        P_DIASPAGADOS.Value = pLiquidacionVacacionesEmpleado.diaspagados;
                        P_DIASPAGADOS.Direction = ParameterDirection.Input;
                        P_DIASPAGADOS.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DIASPAGADOS);


                     

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = vUsuario.codusuario;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);


                        DbParameter P_DIASPENDIENTES = cmdTransaccionFactory.CreateParameter();
                        P_DIASPENDIENTES.ParameterName = "P_DIAS_PENDIENTES";
                        P_DIASPENDIENTES.Value = pLiquidacionVacacionesEmpleado.diaspendientes;
                        P_DIASPENDIENTES.Direction = ParameterDirection.Input;
                        P_DIASPENDIENTES.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DIASPENDIENTES);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQVACAEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionVacacionesEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionVacacionesEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "CrearLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionVacacionesDetalleEmpleado CrearLiquidacionVacacionesDetalleEmpleado(LiquidacionVacacionesDetalleEmpleado detalleEmpleado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = detalleEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter p_codigoliqvacacionesemp = cmdTransaccionFactory.CreateParameter();
                        p_codigoliqvacacionesemp.ParameterName = "p_codigoliqvacacionesemp";
                        p_codigoliqvacacionesemp.Value = detalleEmpleado.codigoliquidacionvacacionesemp;
                        p_codigoliqvacacionesemp.Direction = ParameterDirection.Input;
                        p_codigoliqvacacionesemp.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoliqvacacionesemp);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = detalleEmpleado.codigoConcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = detalleEmpleado.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pesconceptonomina = cmdTransaccionFactory.CreateParameter();
                        pesconceptonomina.ParameterName = "p_esconceptonomina";
                        pesconceptonomina.Value = detalleEmpleado.esConceptoNomina;
                        pesconceptonomina.Direction = ParameterDirection.Input;
                        pesconceptonomina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesconceptonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQVACADETEMP_CR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        detalleEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return detalleEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "CrearLiquidacionVacacionesDetalleEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<List<LiquidacionVacacionesDetalleEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacionVacacionesParaUnEmpleado(LiquidacionVacacionesEmpleado liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigoEmpleado = cmdTransaccionFactory.CreateParameter();
                        p_codigoEmpleado.ParameterName = "p_codigoEmpleado";
                        p_codigoEmpleado.Value = liquidacion.codigoempleado;
                        p_codigoEmpleado.Direction = ParameterDirection.Input;
                        p_codigoEmpleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoEmpleado);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaTerminacion = cmdTransaccionFactory.CreateParameter();
                        p_fechaTerminacion.ParameterName = "p_fechaTerminacion";
                        p_fechaTerminacion.Value = liquidacion.fechaterminacion;
                        p_fechaTerminacion.Direction = ParameterDirection.Input;
                        p_fechaTerminacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaTerminacion);


                        DbParameter P_FECHAINICIO_PERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHAINICIO_PERIODO.ParameterName = "P_FECHA_INICIO_PERIODO";
                        P_FECHAINICIO_PERIODO.Value = liquidacion.fechainicioperiodo;
                        P_FECHAINICIO_PERIODO.Direction = ParameterDirection.Input;
                        P_FECHAINICIO_PERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAINICIO_PERIODO);

                        DbParameter P_FECHATERMINACION_PERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHATERMINACION_PERIODO.ParameterName = "P_FECHA_TERMINACION_PERIODO";
                        P_FECHATERMINACION_PERIODO.Value = liquidacion.fechaterminacionperiodo;
                        P_FECHATERMINACION_PERIODO.Direction = ParameterDirection.Input;
                        P_FECHATERMINACION_PERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHATERMINACION_PERIODO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_GENERARLIQVACA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionVacacionesDetalleEmpleado> liquidacionVacacionDetalleEmpleados = new List<LiquidacionVacacionesDetalleEmpleado>();
                        List<ConceptosOpcionesLiquidados> listaConceptosLiquidadosOpciones = new List<ConceptosOpcionesLiquidados>();

                        string sql = @"select temp.* from TEMP_LIQVACACIONESEMP temp ";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionVacacionesDetalleEmpleado entidad = new LiquidacionVacacionesDetalleEmpleado();

                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoConcepto = Convert.ToInt64(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) entidad.desc_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPOCALCULO"] != DBNull.Value) entidad.tipoCalculo = Convert.ToInt32(resultado["TIPOCALCULO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALORCONCEPTO"]);
                            if (resultado["ESCONCEPTONOMINA"] != DBNull.Value) entidad.esConceptoNomina = Convert.ToInt32(resultado["ESCONCEPTONOMINA"]);

                            liquidacionVacacionDetalleEmpleados.Add(entidad);
                        }

                        sql = @"select * from TEMP_NOVEDALIQUIOPCIONES";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            ConceptosOpcionesLiquidados entidad = new ConceptosOpcionesLiquidados();

                            if (resultado["ConsecutivoOpcion"] != DBNull.Value) entidad.consecutivoOpcion = Convert.ToInt64(resultado["ConsecutivoOpcion"]);
                            if (resultado["TipoOpcion"] != DBNull.Value) entidad.tipoOpcion = Convert.ToInt64(resultado["TipoOpcion"]);
                            if (resultado["Valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["Valor"]);

                            listaConceptosLiquidadosOpciones.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionVacacionDetalleEmpleados, listaConceptosLiquidadosOpciones);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "GenerarLiquidacionVacacionesParaUnEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarSiExisteVacacionesParaEstasFechas(long codigoEmpleado, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            DbDataReader resultado;
            bool existe = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"SELECT CONSECUTIVO
                                                    FROM LIQUIDACIONVACACIONESEMPLEADO
                                                    WHERE   codigoempleado="  + codigoEmpleado +
                                                    " and dias_pendientes<=0 and FECHA_INICIO_PERIODO BETWEEN to_date('{0}', 'dd/MM/yyyy') and to_date('{1}', 'dd/MM/yyyy')", fechaInicio.ToShortDateString(), fechaFinal.ToShortDateString());

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value)
                            {
                                existe = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "VerificarSiExisteVacacionesParaEstasFechas", ex);
                        return false;
                    }
                }
            }
        }

        public List<LiquidacionVacacionesDetalleEmpleado> ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(long codigoLiquidacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<LiquidacionVacacionesDetalleEmpleado> lista = new List<LiquidacionVacacionesDetalleEmpleado>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liqDet.valor, liqDet.ESCONCEPTONOMINA, liqDet.CODIGOCONCEPTO,
                                        CASE EsConceptoNomina WHEN 1 THEN con.DESCRIPCION WHEN 0 THEN con.descripcion END as desc_concepto,
                                        CASE EsConceptoNomina WHEN 1 THEN con.TIPO WHEN 0 THEN con.tipo END as tipoCalculo
                                        FROM LIQUIDACIONVACACIONESDETEMPLEA liqDet
                                        LEFT JOIN TipoNovedadVacaciones tip on tip.CONSECUTIVO = liqDet.CODIGOCONCEPTO
                                        LEFT JOIN CONCEPTO_NOMINA con on con.CONSECUTIVO = liqDet.CODIGOCONCEPTO
                                        WHERE liqDet.CODIGOLIQUIDACIONVACACIONESEMP = " + codigoLiquidacion + " order by tipocalculo asc,CON.DESCRIPCION asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionVacacionesDetalleEmpleado entidad = new LiquidacionVacacionesDetalleEmpleado();

                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipoCalculo"] != DBNull.Value) entidad.tipoCalculo = Convert.ToInt32(resultado["tipoCalculo"]);
                            if (resultado["desc_concepto"] != DBNull.Value) entidad.desc_concepto = Convert.ToString(resultado["desc_concepto"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoConcepto = Convert.ToInt64(resultado["CODIGOCONCEPTO"]);
                            if (resultado["ESCONCEPTONOMINA"] != DBNull.Value) entidad.esConceptoNomina = Convert.ToInt32(resultado["ESCONCEPTONOMINA"]);

                            lista.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionVacacionesEmpleado ModificarLiquidacionVacacionesEmpleado(LiquidacionVacacionesEmpleado pLiquidacionVacacionesEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionVacacionesEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionVacacionesEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionVacacionesEmpleado.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionVacacionesEmpleado.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionVacacionesEmpleado.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pdiasliquidados = cmdTransaccionFactory.CreateParameter();
                        pdiasliquidados.ParameterName = "p_diasliquidados";
                        pdiasliquidados.Value = pLiquidacionVacacionesEmpleado.diasliquidados;
                        pdiasliquidados.Direction = ParameterDirection.Input;
                        pdiasliquidados.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdiasliquidados);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionVacacionesEmpleado.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter p_centroCosto = cmdTransaccionFactory.CreateParameter();
                        p_centroCosto.ParameterName = "p_centroCosto";
                        p_centroCosto.Value = pLiquidacionVacacionesEmpleado.codigocentrocosto;
                        p_centroCosto.Direction = ParameterDirection.Input;
                        p_centroCosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_centroCosto);


                        DbParameter P_FECHA_INICIOPERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_INICIOPERIODO.ParameterName = "P_FECHA_INICIO_PERIODO";
                        P_FECHA_INICIOPERIODO.Value = pLiquidacionVacacionesEmpleado.fechainicioperiodo;
                        P_FECHA_INICIOPERIODO.Direction = ParameterDirection.Input;
                        P_FECHA_INICIOPERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_INICIOPERIODO);


                        DbParameter P_FECHATERMINACION_PERIODO = cmdTransaccionFactory.CreateParameter();
                        P_FECHATERMINACION_PERIODO.ParameterName = "P_FECHA_TERMINACION_PERIODO";
                        P_FECHATERMINACION_PERIODO.Value = pLiquidacionVacacionesEmpleado.fechaterminacionperiodo;
                        P_FECHATERMINACION_PERIODO.Direction = ParameterDirection.Input;
                        P_FECHATERMINACION_PERIODO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHATERMINACION_PERIODO);

                        DbParameter P_FECHAREGRESO = cmdTransaccionFactory.CreateParameter();
                        P_FECHAREGRESO.ParameterName = "P_FECHA_REGRESO";
                        P_FECHAREGRESO.Value = pLiquidacionVacacionesEmpleado.fechafinvacaciones;
                        P_FECHAREGRESO.Direction = ParameterDirection.Input;
                        P_FECHAREGRESO.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAREGRESO);

                        DbParameter P_DIAS_ADISFRUTAR = cmdTransaccionFactory.CreateParameter();
                        P_DIAS_ADISFRUTAR.ParameterName = "P_DIAS_A_DISFRUTAR";
                        P_DIAS_ADISFRUTAR.Value = pLiquidacionVacacionesEmpleado.diasdisfrutar;
                        P_DIAS_ADISFRUTAR.Direction = ParameterDirection.Input;
                        P_DIAS_ADISFRUTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_DIAS_ADISFRUTAR);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQVACAEMP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiquidacionVacacionesEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ModificarLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLiquidacionVacacionesEmpleado(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LiquidacionVacacionesEmpleado pLiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                        pLiquidacionVacacionesEmpleado = ConsultarLiquidacionVacacionesEmpleado(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionVacacionesEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQVACAEMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "EliminarLiquidacionVacacionesEmpleado", ex);
                    }
                }
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION,FECSUMDIA(fecha_terminacion_periodo,1,2) as fechainiprox,
                                     FECSUMDIA(fecha_terminacion_periodo,365,2) as fechaprox,u.nombre as usuario FROM LiquidacionVacacionesEmpleado liq
                                    JOIN EMPLEADOS emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                    JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                    LEFT JOIN USUARIOS u on u.codusuario = liq.CODIGOUSUARIOCREACION
                                    WHERE liq.CONSECUTIVO= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DIASLIQUIDADOS"] != DBNull.Value) entidad.diasliquidados = Convert.ToInt64(resultado["DIASLIQUIDADOS"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["fechaPago"] != DBNull.Value) entidad.fechaPago = Convert.ToDateTime(resultado["fechaPago"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);

                            if (resultado["FECHA_INICIO_PERIODO"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["FECHA_INICIO_PERIODO"]);
                            if (resultado["FECHA_TERMINACION_PERIODO"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["FECHA_TERMINACION_PERIODO"]);
                            if (resultado["FECHA_REGRESO"] != DBNull.Value) entidad.fechafinvacaciones = Convert.ToDateTime(resultado["FECHA_REGRESO"]);
                            if (resultado["DIAS_A_DISFRUTAR"] != DBNull.Value) entidad.diasdisfrutar = Convert.ToInt64(resultado["DIAS_A_DISFRUTAR"]);
                            if (resultado["fechainiprox"] != DBNull.Value) entidad.fechainicioproxperiodo = Convert.ToDateTime(resultado["fechainiprox"]);
                            if (resultado["fechaprox"] != DBNull.Value) entidad.fechaterminacionproxperiodo = Convert.ToDateTime(resultado["fechaprox"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.usuario_crea = Convert.ToString(resultado["usuario"]);

                            if (resultado["DIAS_PAGADOS"] != DBNull.Value) entidad.diaspagados = Convert.ToInt64(resultado["DIAS_PAGADOS"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["DIAS_PENDIENTES"] != DBNull.Value) entidad.diaspendientes = Convert.ToInt64(resultado["DIAS_PENDIENTES"]);
                            if (resultado["PROMCONCEPTOSAD"] != DBNull.Value) entidad.salario_ad = Convert.ToDecimal(resultado["PROMCONCEPTOSAD"]);


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
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }



        public List<LiquidacionVacacionesEmpleado> ListarLiquidacionVacacionesEmpleado(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionVacacionesEmpleado> lstLiquidacionVacacionesEmpleado = new List<LiquidacionVacacionesEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select liq.*, per.identificacion, per.nombre as nombre_empleado, nom.DESCRIPCION as desc_nomina, cst.DESCRIPCION as desc_centro_costo,O.NUM_COMP,O.TIPO_COMP
                                        from LiquidacionVacacionesEmpleado liq
                                        JOIN EMPLEADOS emp on liq.CODIGOEMPLEADO = emp.consecutivo
                                        JOIN v_persona per on emp.COD_PERSONA = per.COD_PERSONA
                                        JOIN NOMINA_EMPLEADO nom on liq.CODIGONOMINA = nom.consecutivo
                                        JOIN CENTRO_COSTO cst on liq.CODIGOCENTROCOSTO = cst.CENTRO_COSTO 
                                        LEFT JOIN OPERACION O ON O.COD_OPE=LIQ.COD_OPE  " + filtro + " ORDER BY liq.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DIASLIQUIDADOS"] != DBNull.Value) entidad.diasliquidados = Convert.ToInt64(resultado["DIASLIQUIDADOS"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre_empleado"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre_empleado"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);

                            if (resultado["FECHA_INICIO_PERIODO"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHA_TERMINACION_PERIODO"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["FECHA_REGRESO"] != DBNull.Value) entidad.fechafinvacaciones = Convert.ToDateTime(resultado["FECHA_REGRESO"]);
                            if (resultado["DIAS_A_DISFRUTAR"] != DBNull.Value) entidad.diasdisfrutar = Convert.ToInt64(resultado["DIAS_A_DISFRUTAR"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);

                            lstLiquidacionVacacionesEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionVacacionesEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ListarLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarUltLiquidacionVacacionesEmpleado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION
                                    FROM LiquidacionVacacionesEmpleado liq
                                    JOIN EMPLEADOS emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                    JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                    WHERE liq.CODIGOEMPLEADO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DIASLIQUIDADOS"] != DBNull.Value) entidad.diasliquidados = Convert.ToInt64(resultado["DIASLIQUIDADOS"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["fechaPago"] != DBNull.Value) entidad.fechaPago = Convert.ToDateTime(resultado["fechaPago"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);

                            if (resultado["FECHA_INICIO_PERIODO"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHA_TERMINACION_PERIODO"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["FECHA_REGRESO"] != DBNull.Value) entidad.fechafinvacaciones = Convert.ToDateTime(resultado["FECHA_REGRESO"]);
                            if (resultado["DIAS_A_DISFRUTAR"] != DBNull.Value) entidad.diasdisfrutar = Convert.ToInt64(resultado["DIAS_A_DISFRUTAR"]);


                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleadoXCodigo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION,FECSUMDIA(fecha_terminacion_periodo,1,2) as fechainiprox,
                                     FECSUMDIA(fecha_terminacion_periodo,365,2) as fechaprox FROM LiquidacionVacacionesEmpleado liq
                                    JOIN EMPLEADOS emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                    JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                    WHERE  liq.FECHA_TERMINACION_PERIODO  =(select max(LiquidacionVacacionesEmpleado.FECHA_TERMINACION_PERIODO) 
                                    from   LiquidacionVacacionesEmpleado where  LiquidacionVacacionesEmpleado.codigoempleado =liq.codigoempleado) 
                                    and liq.consecutivo in(select max(lq1.consecutivo) from LiquidacionVacacionesEmpleado lq1 where  lq1.codigoempleado =liq.codigoempleado)
                                    and liq.codigoempleado = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["DIASLIQUIDADOS"] != DBNull.Value) entidad.diasliquidados = Convert.ToInt64(resultado["DIASLIQUIDADOS"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["fechaPago"] != DBNull.Value) entidad.fechaPago = Convert.ToDateTime(resultado["fechaPago"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);

                            if (resultado["fechainiprox"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["fechainiprox"]);
                            if (resultado["fechaprox"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["fechaprox"]);
                            if (resultado["FECHA_REGRESO"] != DBNull.Value) entidad.fechafinvacaciones = Convert.ToDateTime(resultado["FECHA_REGRESO"]);
                            if (resultado["DIAS_A_DISFRUTAR"] != DBNull.Value) entidad.diasdisfrutar = Convert.ToInt64(resultado["DIAS_A_DISFRUTAR"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["DIAS_PENDIENTES"] != DBNull.Value) entidad.diaspendientes = Convert.ToInt64(resultado["DIAS_PENDIENTES"]);

                            if(entidad.diaspendientes >0)
                            { 
                            if (resultado["fecha_inicio_periodo"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["fecha_inicio_periodo"]);
                            if (resultado["fecha_terminacion_periodo"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["fecha_terminacion_periodo"]);

                            }
                        }
                        else
                        {
                         //   throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarLiquidacionVacacionesEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public LiquidacionVacacionesEmpleado CrearLiquidContratoNominaInterfaz(LiquidacionVacacionesEmpleado pliquidacioncontrato, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pliquidacioncontrato.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);



                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pliquidacioncontrato.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pliquidacioncontrato.cod_concepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);


                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pliquidacioncontrato.valor;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        pcodope.Value = pliquidacioncontrato.cod_ope;
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
                        pcodigocentrocosto.Value = pliquidacioncontrato.codigocentrocosto;
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
                        porigen.Value = pliquidacioncontrato.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INTERFAZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pliquidacioncontrato.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pliquidacioncontrato;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "CrearLiquidContratoNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionVacacionesEmpleado ConsultarPagaVacacionesAnticipadas(Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT VACACIONES_ANTICIPADAS from NOMINA_SEGURIDAD_SOCIAL  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VACACIONES_ANTICIPADAS"] != DBNull.Value) entidad.vacacionesanticipadas = Convert.ToInt64(resultado["VACACIONES_ANTICIPADAS"]);
                         
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarPagaVacacionesAnticipadas", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarDiasVacaciones(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION
                                    FROM NOVEDADESVACACIONESEMPLEADO liq
                                    JOIN EMPLEADOS emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                    JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                    WHERE liq.CONSECUTIVO= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CANTIDADDIAS"] != DBNull.Value) entidad.cantidaddias = Convert.ToInt64(resultado["CANTIDADDIAS"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHA_INICIO_PERIODO"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["FECHA_INICIO_PERIODO"]);
                            if (resultado["FECHA_TERMINACION_PERIODO"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["FECHA_TERMINACION_PERIODO"]);


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
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarDiasVacaciones", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionVacacionesEmpleado CrearDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "P_CONSECUTIVO";
                        pconsecutivo.Value = pDiasvacacionesEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "P_CODIGOEMPLEADO";
                        pcodigoempleado.Value = pDiasvacacionesEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);
                        

                        DbParameter pfechainicial = cmdTransaccionFactory.CreateParameter();
                        pfechainicial.ParameterName = "P_FECHA_INICIAL";
                        pfechainicial.Value = pDiasvacacionesEmpleados.fechainicio;
                        pfechainicial.Direction = ParameterDirection.Input;
                        pfechainicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicial);

                        DbParameter pfechafinal= cmdTransaccionFactory.CreateParameter();
                        pfechafinal.ParameterName = "P_FECHA_FINAL";
                        pfechafinal.Value = pDiasvacacionesEmpleados.fechaterminacion;
                        pfechafinal.Direction = ParameterDirection.Input;
                        pfechafinal.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechafinal);

                        DbParameter pcantidaddias = cmdTransaccionFactory.CreateParameter();
                        pcantidaddias.ParameterName = "P_CANTIDADDIAS";
                        pcantidaddias.Value = pDiasvacacionesEmpleados.cantidaddias;
                        pcantidaddias.Direction = ParameterDirection.Input;
                        pcantidaddias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidaddias);


                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "P_CODIGONOMINA";
                        pcodigonomina.Value = pDiasvacacionesEmpleados.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);


                        DbParameter pfechainicioperiodo = cmdTransaccionFactory.CreateParameter();
                        pfechainicioperiodo.ParameterName = "P_FECHA_INICIO_PER";
                        pfechainicioperiodo.Value = pDiasvacacionesEmpleados.fechainicioperiodo;
                        pfechainicioperiodo.Direction = ParameterDirection.Input;
                        pfechainicioperiodo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicioperiodo);

                        DbParameter pfechaterminacionperiodo = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacionperiodo.ParameterName = "P_FECHA_TERM_PER";
                        pfechaterminacionperiodo.Value = pDiasvacacionesEmpleados.fechaterminacionperiodo;
                        pfechaterminacionperiodo.Direction = ParameterDirection.Input;
                        pfechaterminacionperiodo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacionperiodo);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DIASVACAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDiasvacacionesEmpleados.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pDiasvacacionesEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "CrearDiasVacacionesEmpleados", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionVacacionesEmpleado ModificarDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "P_CONSECUTIVO";
                        pconsecutivo.Value = pDiasvacacionesEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "P_CODIGOEMPLEADO";
                        pcodigoempleado.Value = pDiasvacacionesEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pfechainicial = cmdTransaccionFactory.CreateParameter();
                        pfechainicial.ParameterName = "P_FECHA_INICIAL";
                        pfechainicial.Value = pDiasvacacionesEmpleados.fechainicio;
                        pfechainicial.Direction = ParameterDirection.Input;
                        pfechainicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicial);

                        DbParameter pfechafinal = cmdTransaccionFactory.CreateParameter();
                        pfechafinal.ParameterName = "P_FECHA_FINAL";
                        pfechafinal.Value = pDiasvacacionesEmpleados.fechaterminacion;
                        pfechafinal.Direction = ParameterDirection.Input;
                        pfechafinal.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechafinal);

                        DbParameter pcantidaddias = cmdTransaccionFactory.CreateParameter();
                        pcantidaddias.ParameterName = "P_CANTIDADDIAS";
                        pcantidaddias.Value = pDiasvacacionesEmpleados.cantidaddias;
                        pcantidaddias.Direction = ParameterDirection.Input;
                        pcantidaddias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcantidaddias);


                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "P_CODIGONOMINA";
                        pcodigonomina.Value = pDiasvacacionesEmpleados.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);


                        DbParameter pfechainicioperiodo = cmdTransaccionFactory.CreateParameter();
                        pfechainicioperiodo.ParameterName = "P_FECHA_INICIO_PER";
                        pfechainicioperiodo.Value = pDiasvacacionesEmpleados.fechainicioperiodo;
                        pfechainicioperiodo.Direction = ParameterDirection.Input;
                        pfechainicioperiodo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicioperiodo);

                        DbParameter pfechaterminacionperiodo = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacionperiodo.ParameterName = "P_FECHA_TERM_PER";
                        pfechaterminacionperiodo.Value = pDiasvacacionesEmpleados.fechaterminacionperiodo;
                        pfechaterminacionperiodo.Direction = ParameterDirection.Input;
                        pfechaterminacionperiodo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacionperiodo);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_DIASVACAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        
                        return pDiasvacacionesEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ModificarDiasVacacionesEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public List<LiquidacionVacacionesEmpleado> ListarDiasVacaciones(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionVacacionesEmpleado> lstDiasVacaciones = new List<LiquidacionVacacionesEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, per.nombre, per.identificacion, nomi.DESCRIPCION as desc_nomina
                                        FROM NOVEDADESVACACIONESEMPLEADO nov
                                        JOIN NOMINA_EMPLEADO nomi on nomi.CONSECUTIVO = nov.CODIGONOMINA
                                        JOIN EMPLEADOS emp on emp.consecutivo = nov.codigoempleado
                                        JOIN v_persona per on emp.COD_PERSONA = per.COD_PERSONA " + filtro + " ORDER BY nov.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                         //   if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);

                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CANTIDADDIAS"] != DBNull.Value) entidad.cantidaddias = Convert.ToInt64(resultado["CANTIDADDIAS"]);
                             if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);

                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["FECHA_INICIO_PERIODO"] != DBNull.Value) entidad.fechainicioperiodo = Convert.ToDateTime(resultado["FECHA_INICIO_PERIODO"]);
                            if (resultado["FECHA_TERMINACION_PERIODO"] != DBNull.Value) entidad.fechaterminacionperiodo = Convert.ToDateTime(resultado["FECHA_TERMINACION_PERIODO"]);


                            lstDiasVacaciones.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDiasVacaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ListarDiasVacaciones", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarDiasVacacionesNovedades(Int64 pId, DateTime fechainicial, DateTime fechafinal,Usuario vUsuario)
        {
            DbDataReader resultado;
            String Fecha_Inicial = fechainicial.ToShortDateString();
            String Fecha_Final = fechafinal.ToShortDateString();

            LiquidacionVacacionesEmpleado entidad = new LiquidacionVacacionesEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"SELECT sum(cantidaddias) as CANTIDADDIAS
                                    FROM NOVEDADESVACACIONESEMPLEADO 
                                    WHERE codigoempleado = " + pId.ToString()                                     
                                    + "  and  ( FECHA_INICIO_PERIODO >= To_Date('" + Convert.ToDateTime(fechainicial).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')"
                                    + " OR FECHA_TERMINACION_PERIODO <= To_Date('" + Convert.ToDateTime(fechafinal).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'))";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CANTIDADDIAS"] != DBNull.Value) entidad.cantidaddias = Convert.ToInt64(resultado["CANTIDADDIAS"]);
                            

                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionVacacionesEmpleadoData", "ConsultarDiasVacacionesNovedades", ex);
                        return null;
                    }
                }
            }
        }


    }
}