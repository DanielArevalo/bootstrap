using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class LiquidacionNominaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public LiquidacionNomina CrearLiquidacionNomina(LiquidacionNomina pLiquidacionNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNomina.consecutivo;
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
                        pfechageneracion.Value = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionNomina.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionNomina.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionNomina.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionNomina.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);


                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLiquidacionNomina.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        pcodope.Value = pLiquidacionNomina.cod_ope;
                        pcodope.Direction = ParameterDirection.Input;
                        pcodope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodope);


                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.Value = pLiquidacionNomina.observaciones;
                        p_observaciones.Direction = ParameterDirection.Input;
                        p_observaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIDACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNomina.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNomina", ex);
                        return null;
                    }
                }
            }
        }

        public List<ParConceptosPlanillaLiq> ListarConceptosParametrizadosSegunColumna(int codigoColumna, Usuario usuario)
        {
            DbDataReader resultado;
            List<ParConceptosPlanillaLiq> lstParConceptosPlanillaLiq = new List<ParConceptosPlanillaLiq>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Par_ConceptosPlanillaLiq WHERE CODIGOCOLUMNA = " + codigoColumna;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParConceptosPlanillaLiq entidad = new ParConceptosPlanillaLiq();

                            if (resultado["CODIGOCOLUMNA"] != DBNull.Value) entidad.codigocolumna = Convert.ToInt64(resultado["CODIGOCOLUMNA"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt64(resultado["CODIGOCONCEPTO"]);

                            lstParConceptosPlanillaLiq.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstParConceptosPlanillaLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarConceptosParametrizadosSegunColumna", ex);
                        return null;
                    }
                }
            }
        }

        public ParColumnasPlanillaLiq ConsultarParametrizacionColumnas(int codigoColumna, Usuario usuario)
        {
            DbDataReader resultado;
            ParColumnasPlanillaLiq entidad = new ParColumnasPlanillaLiq();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_COLUMNASPLANILLALIQ WHERE codigocolumna = " + codigoColumna.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODIGOCOLUMNA"] != DBNull.Value) entidad.codigocolumna = Convert.ToInt64(resultado["CODIGOCOLUMNA"]);
                            if (resultado["NOMBRECOLUMNA"] != DBNull.Value) entidad.nombrecolumna = Convert.ToString(resultado["NOMBRECOLUMNA"]);
                            if (resultado["ESVISIBLE"] != DBNull.Value) entidad.esvisible = Convert.ToInt64(resultado["ESVISIBLE"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarParametrizacionColumnas", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarParConceptosPlanillaLiqDeUnaColumna(ParColumnasPlanillaLiq parametrizacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigocolumna = cmdTransaccionFactory.CreateParameter();
                        pcodigocolumna.ParameterName = "p_codigocolumna";
                        pcodigocolumna.Value = parametrizacion.codigocolumna;
                        pcodigocolumna.Direction = ParameterDirection.Input;
                        pcodigocolumna.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocolumna);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_ELMCONCEPTOS";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "EliminarParConceptosPlanillaLiqDeUnaColumna", ex);
                    }
                }
            }
        }

        public ParColumnasPlanillaLiq CrearParametrizacionColumnasLiquidacion(ParColumnasPlanillaLiq parametrizacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigocolumna = cmdTransaccionFactory.CreateParameter();
                        pcodigocolumna.ParameterName = "p_codigocolumna";
                        pcodigocolumna.Value = parametrizacion.codigocolumna;
                        pcodigocolumna.Direction = ParameterDirection.Input;
                        pcodigocolumna.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocolumna);

                        DbParameter pnombrecolumna = cmdTransaccionFactory.CreateParameter();
                        pnombrecolumna.ParameterName = "p_nombrecolumna";
                        pnombrecolumna.Value = parametrizacion.nombrecolumna;
                        pnombrecolumna.Direction = ParameterDirection.Input;
                        pnombrecolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombrecolumna);

                        DbParameter pesvisible = cmdTransaccionFactory.CreateParameter();
                        pesvisible.ParameterName = "p_esvisible";
                        pesvisible.Value = parametrizacion.esvisible;
                        pesvisible.Direction = ParameterDirection.Input;
                        pesvisible.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pesvisible);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PAR_COLUMN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return parametrizacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearParametrizacionColumnasLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        public ParConceptosPlanillaLiq CrearParConceptosPlanillaLiq(ParConceptosPlanillaLiq pParConceptosPlanillaLiq, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigocolumna = cmdTransaccionFactory.CreateParameter();
                        pcodigocolumna.ParameterName = "p_codigocolumna";
                        pcodigocolumna.Value = pParConceptosPlanillaLiq.codigocolumna;
                        pcodigocolumna.Direction = ParameterDirection.Input;
                        pcodigocolumna.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocolumna);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pParConceptosPlanillaLiq.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PAR_CONCEP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParConceptosPlanillaLiq;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearParConceptosPlanillaLiq", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarConceptoLiquidado(ConceptosOpcionesLiquidados conceptosLiquidados, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivoOpcion = cmdTransaccionFactory.CreateParameter();
                        p_consecutivoOpcion.ParameterName = "p_consecutivoOpcion";
                        p_consecutivoOpcion.Value = conceptosLiquidados.consecutivoOpcion;
                        p_consecutivoOpcion.Direction = ParameterDirection.Input;
                        p_consecutivoOpcion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_consecutivoOpcion);

                        DbParameter p_tipoOpcion = cmdTransaccionFactory.CreateParameter();
                        p_tipoOpcion.ParameterName = "p_tipoOpcion";
                        p_tipoOpcion.Value = conceptosLiquidados.tipoOpcion;
                        p_tipoOpcion.Direction = ParameterDirection.Input;
                        p_tipoOpcion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipoOpcion);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = conceptosLiquidados.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "P_FECHA";
                        p_fecha.Value = conceptosLiquidados.fecha;
                        p_fecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_APLICARCONCEPTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "AplicarConceptoLiquidado", ex);
                    }
                }
            }
        }

        public void CrearGirosDeLiquidacionNomina(LiquidacionNomina liquidacion, Usuario usuario)
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
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearGirosDeLiquidacionNomina", ex);
                    }
                }
            }
        }
        public void CrearGirosDeAnticiposNomina(LiquidacionNomina liquidacion, Usuario usuario)
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
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearGirosDeLiquidacionNomina", ex);
                    }
                }
            }
        }
        public bool ConsultarSiConceptoEsHoraExtra(long codigoConcepto, Usuario usuario)
        {
            DbDataReader resultado;
            bool esHoraExtra = false;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"SELECT consecutivo FROM concepto_nomina where tipoconcepto = 9 and consecutivo = {0}", codigoConcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value)
                            {
                                esHoraExtra = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return esHoraExtra;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarSiConceptoEsHoraExtra", ex);
                        return false;
                    }
                }
            }
        }

        public bool VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            DbDataReader resultado;
            bool hayLiquidacion = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"SELECT COUNT(*) as existe
                                                    FROM liquidacionnomina 
                                                    WHERE to_Date('{0}', 'dd/MM/yyyy') between fechainicio and fechaterminacion
                                                    OR to_Date('{0}', 'dd/MM/yyyy') between fechainicio and fechaterminacion", fechaInicio.ToShortDateString(), fechaFinal.ToShortDateString());

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["existe"] != DBNull.Value)
                            {
                                int numeroRegistros = Convert.ToInt32(resultado["existe"]);
                                hayLiquidacion = numeroRegistros > 0;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return hayLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar", ex);
                        return false;
                    }
                }
            }
        }

        public int ConsultarUnidadConceptoNomina(long codigoConcepto, Usuario usuario)
        {
            DbDataReader resultado;
            int unidadConcepto = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select UNIDAD_CONCEPTO FROM CONCEPTO_NOMINA WHERE CONSECUTIVO = {0}", codigoConcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["UNIDAD_CONCEPTO"] != DBNull.Value) unidadConcepto = Convert.ToInt32(resultado["UNIDAD_CONCEPTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return unidadConcepto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarUnidadConceptoNomina", ex);
                        return 0;
                    }
                }
            }
        }

        public LiquidacionNominaDetaEmpleado CalcularValorConceptoNominaDeUnEmpleado(int codigoconcepto, long codigoempleado, long codigoNomina, DateTime fechaInicio, DateTime fechaFin, decimal cantidad, decimal valor, Usuario usuario, Int16 origen)
        {
            DbDataReader resultado;
            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select CalcularConceptoNomina({0}, {1}, {2}, to_date('{3}', 'dd/MM/yyyy'), to_date('{4}', 'dd/MM/yyyy'), {5}, {6},{7}) as ValorConcepto, tipo from concepto_nomina where CONSECUTIVO = {0}", codigoconcepto, codigoempleado, codigoNomina, fechaInicio.ToShortDateString(), fechaFin.ToShortDateString(), cantidad, valor, origen);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ValorConcepto"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["ValorConcepto"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["tipo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CalcularValorConceptoNominaDeUnEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedades(long codigoEmpleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaNoveEmpleado> listaNovedades = new List<LiquidacionNominaNoveEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = @"SELECT * FROM LIQUIDACIONNOMINANOVEEMPLEADO WHERE ESTADO IN(0,2) AND CODIGOEMPLEADO= " + codigoEmpleado;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LiquidacionNominaNoveEmpleado entidad = new LiquidacionNominaNoveEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaNovedades", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaNovedadesRecibo(long codigoEmpleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaDetaEmpleado> listaNovedades = new List<LiquidacionNominaDetaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = @"SELECT liq.*,con.DESCRIPCION AS desc_concepto FROM LIQUIDACIONNOMINANOVEEMPLEADO liq JOIN CONCEPTO_NOMINA con on liq.CODIGOCONCEPTO = con.CONSECUTIVO WHERE liq.ESTADO IN(0,2) AND liq.CODIGOEMPLEADO= " + codigoEmpleado;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["desc_concepto"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["desc_concepto"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaNovedadesRecibo", ex);
                        return null;
                    }
                }
            }
        }


        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesAplicadas(long consecutivo, long codigoEmpleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaNoveEmpleado> listaNovedades = new List<LiquidacionNominaNoveEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = @"select  LN.* from  LIQUIDACIONNOMINANOVEEMPLEADO LN INNER JOIN liquidacionnominadetalle LD ON LD.CONSECUTIVO=LN.CODIGOLIQUIDACIONNOMINADETALLE INNER JOIN liquidacionnomina L ON L.CONSECUTIVO=LD.CODIGOLIQUIDACIONNOMINA where LN.estado=1 AND LN.CODIGOEMPLEADO= " + codigoEmpleado + " and L.consecutivo = " + consecutivo;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LiquidacionNominaNoveEmpleado entidad = new LiquidacionNominaNoveEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaNovedadesAplicadas", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodas(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaNoveEmpleado> listaNovedades = new List<LiquidacionNominaNoveEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = @"SELECT * FROM LIQUIDACIONNOMINANOVEEMPLEADO WHERE ESTADO IN(0,2)";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LiquidacionNominaNoveEmpleado entidad = new LiquidacionNominaNoveEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaNovedadesTodas", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodasAplicadas(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaNoveEmpleado> listaNovedades = new List<LiquidacionNominaNoveEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        string sql = @"select  LN.* from  LIQUIDACIONNOMINANOVEEMPLEADO LN INNER JOIN liquidacionnominadetalle LD ON LD.CONSECUTIVO=LN.CODIGOLIQUIDACIONNOMINADETALLE INNER JOIN liquidacionnomina L ON L.CONSECUTIVO=LD.CODIGOLIQUIDACIONNOMINA where  LN.estado=1";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LiquidacionNominaNoveEmpleado entidad = new LiquidacionNominaNoveEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaNovedadesTodasAplicadas", ex);
                        return null;
                    }
                }
            }
        }


        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaConceptos(LiquidacionNomina liquidacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaDetaEmpleado> listaNovedades = new List<LiquidacionNominaDetaEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, con.DESCRIPCION as desc_concepto, con.tipo
                                        FROM LIQUIDACIONNOMINADETAEMPLEADO liq
                                        JOIN CONCEPTO_NOMINA con on liq.CODIGOCONCEPTO = con.CONSECUTIVO
                                        WHERE liq.CODIGOLIQUIDACIONNOMINADETALLE IN(
                                        (
                                            SELECT CONSECUTIVO FROM LiquidacionNominaDetalle where CODIGOLIQUIDACIONNOMINA = " + liquidacion.consecutivo +
                                        " )) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINADETALLE"] != DBNull.Value) entidad.codigoliquidacionnominadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);
                            if (resultado["desc_concepto"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["desc_concepto"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["tipo"]);

                            listaNovedades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaNovedades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaConceptos", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaDetalle> ListarLiquidacionNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaDetalle> listaLiquidacion = new List<LiquidacionNominaDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select liq.*, per.nombre, per.identificacion, ing.CODIGOCARGO, car.DESCRIPCION as desc_cargo,ing.CODIGOCENTROCOSTO
                                        from LiquidacionNominaDetalle liq 
                                        JOIN empleados emp on liq.CodigoEmpleado = emp.consecutivo
                                        join v_persona per on emp.COD_PERSONA = per.cod_persona
                                        join ingresopersonal ing on emp.consecutivo = ing.CODIGOEMPLEADO AND ING.CONSECUTIVO=LIQ.CONSECUTIVOCONTRATO
                                             and ing.CODIGONOMINA =  
                                        (
                                            SELECT CODIGONOMINA FROM LIQUIDACIONNOMINA WHERE CONSECUTIVO = " + liquidacion.consecutivo +
                                     @" )
                                        join CARGO_NOMINA car on car.IDCARGO = ing.codigocargo
                                        where liq.CODIGOLIQUIDACIONNOMINA = " + liquidacion.consecutivo + " order by  CODIGOCENTROCOSTO ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionNominaDetalle entidad = new LiquidacionNominaDetalle();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONNOMINA"] != DBNull.Value) entidad.codigoliquidacionnomina = Convert.ToInt64(resultado["CODIGOLIQUIDACIONNOMINA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["VALORTOTALAPAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToDecimal(resultado["VALORTOTALAPAGAR"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["DiasLiquidar"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["DiasLiquidar"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.centrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);

                            listaLiquidacion.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<LiquidacionNominaDetalle, LiquidacionNominaDetaEmpleado> GenerarLiquidacionPorEmpleado(LiquidacionNomina liquidacion, Empleados empleados, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoEmpleado = cmdTransaccionFactory.CreateParameter();
                        p_codigoEmpleado.ParameterName = "p_codigoEmpleado";
                        p_codigoEmpleado.Value = empleados.consecutivo;
                        p_codigoEmpleado.Direction = ParameterDirection.Input;
                        p_codigoEmpleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoEmpleado);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_borrarTemporalesAntes = cmdTransaccionFactory.CreateParameter();
                        p_borrarTemporalesAntes.ParameterName = "p_borrarTemporalesAntes";
                        p_borrarTemporalesAntes.Value = 1;
                        p_borrarTemporalesAntes.Direction = ParameterDirection.Input;
                        p_borrarTemporalesAntes.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_borrarTemporalesAntes);

                        DbParameter p_liquidaTodoPendiente = cmdTransaccionFactory.CreateParameter();
                        p_liquidaTodoPendiente.ParameterName = "p_liquidaTodoPendiente";
                        p_liquidaTodoPendiente.Value = 0; // No
                        p_liquidaTodoPendiente.Direction = ParameterDirection.Input;
                        p_liquidaTodoPendiente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_liquidaTodoPendiente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CALCULARLIQUI_EMPLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        LiquidacionNominaDetalle liquidacionDef = new LiquidacionNominaDetalle();
                        LiquidacionNominaDetaEmpleado liquidacionDetalle = new LiquidacionNominaDetaEmpleado();

                        string sql = @"select t.*,ip.CODIGOCENTROCOSTO from TEMP_EMPLEADOLIQUIDACIOn  t left  join  v_persona  vp on vp.identificacion=t.identificacion inner join INGRESOPERSONAL ip on IP.CODIGOPERSONA=vp.cod_persona order by IP.CODIGOCENTROCOSTO asc"; 

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) liquidacionDef.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) liquidacionDef.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) liquidacionDef.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALARIO"] != DBNull.Value) liquidacionDef.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) liquidacionDef.valortotalapagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["DiasLiquidar"] != DBNull.Value) liquidacionDef.dias = Convert.ToInt64(resultado["DiasLiquidar"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) liquidacionDetalle.codigocentrocosto = Convert.ToInt32(resultado["CODIGOCENTROCOSTO"]);

                        }

                        sql = @"select TEMP_EMPLIQUIDACIONCONCEPTOS.*,IP.CODIGOCENTROCOSTO from TEMP_EMPLIQUIDACIONCONCEPTOS INNER JOIN CONCEPTO_NOMINA ON CONCEPTO_NOMINA.CONSECUTIVO=TEMP_EMPLIQUIDACIONCONCEPTOS.CODIGOCONCEPTO
                                        inner join INGRESOPERSONAL ip on IP.CODIGOEMPLEADO=TEMP_EMPLIQUIDACIONCONCEPTOS=CONSECUTIVOEMPLEADO ORDER BY order by IP.CODIGOCENTROCOSTO asc
                                  ";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) liquidacionDetalle.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) liquidacionDetalle.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) liquidacionDetalle.descripcion_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) liquidacionDetalle.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) liquidacionDetalle.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) liquidacionDetalle.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) liquidacionDetalle.codigocentrocosto = Convert.ToInt32(resultado["CODIGOCENTROCOSTO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionDef, liquidacionDetalle);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "GenerarLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacionDefinitiva(LiquidacionNomina liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);






                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CALCULARLIQ_DEF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                        List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();
                        List<ConceptosOpcionesLiquidados> listaConceptosLiquidadosOpciones = new List<ConceptosOpcionesLiquidados>();

                        string sql = @"select temp.*, ING.CODIGOCARGO, car.DESCRIPCION as desc_cargo,ING.CODIGOCENTROCOSTO
                                        from TEMP_EMPLEADOLIQUIDACION temp
                                        JOIN INGRESOPERSONAL ING on ING.CODIGOEMPLEADO = temp.CONSECUTIVOEMPLEADO and ING.CODIGONOMINA = " + liquidacion.codigonomina +
                                        " JOIN CARGO_nomina car on car.IDCARGO = ing.codigocargo where ING.ESTAACTIVOCONTRATO=1 order by ING.CODIGOCENTROCOSTO asc ";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetalle entidad = new LiquidacionNominaDetalle();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["DiasLiquidar"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["DiasLiquidar"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);

                            liquidacionDetalle.Add(entidad);
                        }

                        sql = @"select * from TEMP_EMPLIQUIDACIONCONCEPTOS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            liquidacionDetalleEmpleados.Add(entidad);
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

                        return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados, listaConceptosLiquidadosOpciones);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "GenerarLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNominaDetalle CrearLiquidacionNominaDetalle(LiquidacionNominaDetalle pLiquidacionNominaDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNominaDetalle.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacionnomina = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacionnomina.ParameterName = "p_codigoliquidacionnomina";
                        pcodigoliquidacionnomina.Value = pLiquidacionNominaDetalle.codigoliquidacionnomina;
                        pcodigoliquidacionnomina.Direction = ParameterDirection.Input;
                        pcodigoliquidacionnomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacionnomina);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaDetalle.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pvalortotalapagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalapagar.ParameterName = "p_valortotalapagar";
                        pvalortotalapagar.Value = pLiquidacionNominaDetalle.valortotalapagar;
                        pvalortotalapagar.Direction = ParameterDirection.Input;
                        pvalortotalapagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalapagar);

                        DbParameter p_salario = cmdTransaccionFactory.CreateParameter();
                        p_salario.ParameterName = "p_salario";
                        p_salario.Value = pLiquidacionNominaDetalle.salario;
                        p_salario.Direction = ParameterDirection.Input;
                        p_salario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_salario);

                        DbParameter p_dia = cmdTransaccionFactory.CreateParameter();
                        p_dia.ParameterName = "p_dia";
                        p_dia.Value = pLiquidacionNominaDetalle.dias;
                        p_dia.Direction = ParameterDirection.Input;
                        p_dia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_dia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIDETALL_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaDetalle.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNominaDetaEmpleado CrearLiquidacionNominaDetaEmpleado(LiquidacionNominaDetaEmpleado pLiquidacionNominaDetaEmpleado, Usuario vUsuario)
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

                        DbParameter p_codigoliquinominadet = cmdTransaccionFactory.CreateParameter();
                        p_codigoliquinominadet.ParameterName = "p_codigoliquinominadet";
                        p_codigoliquinominadet.Value = pLiquidacionNominaDetaEmpleado.codigoliquidacionnominadetalle;
                        p_codigoliquinominadet.Direction = ParameterDirection.Input;
                        p_codigoliquinominadet.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoliquinominadet);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaDetaEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pLiquidacionNominaDetaEmpleado.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.valorconcepto;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQDETEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaDetaEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaDetaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaDetaEmpleado", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionNominaNoveEmpleado CrearLiquidacionNominaDetaEmpleado2(LiquidacionNominaNoveEmpleado pLiquidacionNominaDetaEmpleado, Usuario vUsuario)
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

                        DbParameter p_codigoliquinominadet = cmdTransaccionFactory.CreateParameter();
                        p_codigoliquinominadet.ParameterName = "p_codigoliquinominadet";
                        p_codigoliquinominadet.Value = pLiquidacionNominaDetaEmpleado.codigoliquidacionnominadetalle;
                        p_codigoliquinominadet.Direction = ParameterDirection.Input;
                        p_codigoliquinominadet.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoliquinominadet);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaDetaEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pLiquidacionNominaDetaEmpleado.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.valorconcepto;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQDETEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaDetaEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaDetaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaDetaEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNominaDetaEmpleado CrearLiquidacionNominaInterfaz(LiquidacionNominaDetaEmpleado pLiquidacionNominaDetaEmpleado, LiquidacionNomina pLiquidacionNomina, Usuario vUsuario)
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
                        pcodigoconcepto.Value = pLiquidacionNominaDetaEmpleado.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.valorconcepto;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        if (pLiquidacionNominaDetaEmpleado.cod_ope == 0)
                        {
                            pcodope.Value = pLiquidacionNomina.cod_ope;
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
                        pcodigocentrocosto.Value = pLiquidacionNomina.codigocentrocosto;
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
                        porigen.Value = pLiquidacionNomina.codorigen;
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
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }


        public LiquidacionNomina CrearAnticiposNominaInterfaz(LiquidacionNomina panticiposNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = panticiposNomina.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);



                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = panticiposNomina.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = panticiposNomina.cod_concepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);


                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = panticiposNomina.valor_anticipo;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        pcodope.Value = panticiposNomina.cod_ope;
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
                        pcodigocentrocosto.Value = panticiposNomina.codigocentrocosto;
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
                        porigen.Value = panticiposNomina.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INTERFAZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        panticiposNomina.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return panticiposNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionNomina CrearAnticiposNominaInterfazSubsidio(LiquidacionNomina panticiposNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = panticiposNomina.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);



                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = panticiposNomina.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = panticiposNomina.cod_concepto_trans;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);


                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = panticiposNomina.valor_anticipo_sub;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        pcodope.Value = panticiposNomina.cod_ope;
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
                        pcodigocentrocosto.Value = panticiposNomina.codigocentrocosto;
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
                        porigen.Value = panticiposNomina.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INTERFAZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        panticiposNomina.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return panticiposNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearAnticiposNominaInterfazSubsidio", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNominaNoveEmpleado CrearLiquidacionNominaNoveEmpleado(LiquidacionNominaNoveEmpleado pLiquidacionNominaNoveEmpleado, Usuario vUsuario, Int64 codigoliquidacionnominadetalle)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNominaNoveEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter p_codigoliquinominadet = cmdTransaccionFactory.CreateParameter();
                        p_codigoliquinominadet.ParameterName = "p_codigoliquinominadet";
                        if (pLiquidacionNominaNoveEmpleado.codigoliquidacionnominadetalle == 0)
                            p_codigoliquinominadet.Value = codigoliquidacionnominadetalle;
                        else
                            p_codigoliquinominadet.Value = pLiquidacionNominaNoveEmpleado.codigoliquidacionnominadetalle;
                        p_codigoliquinominadet.Direction = ParameterDirection.Input;
                        p_codigoliquinominadet.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoliquinominadet);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaNoveEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pLiquidacionNominaNoveEmpleado.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pLiquidacionNominaNoveEmpleado.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pLiquidacionNominaNoveEmpleado.valorconcepto;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionNominaNoveEmpleado.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionNominaNoveEmpleado.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pLiquidacionNominaNoveEmpleado.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQNOVEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaNoveEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaNoveEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidacionNominaNoveEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNomina ModificarLiquidacionNomina(LiquidacionNomina pLiquidacionNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNomina.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = pLiquidacionNomina.codigousuariocreacion;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = pLiquidacionNomina.fechageneracion;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionNomina.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionNomina.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionNomina.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionNomina.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIDACIO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ModificarLiquidacionNomina", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLiquidacionNomina(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LiquidacionNomina pLiquidacionNomina = new LiquidacionNomina();
                        pLiquidacionNomina = ConsultarLiquidacionNomina(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNomina.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIDACIO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "EliminarLiquidacionNomina", ex);
                    }
                }
            }
        }
        public void EliminarNovedadesNomina(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pId;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_NOVEDADES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "EliminarNovedadesNomina", ex);
                    }
                }
            }
        }


        public LiquidacionNomina ConsultarLiquidacionNomina(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionNomina entidad = new LiquidacionNomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT  LiquidacionNomina.*,u.nombre  FROM LiquidacionNomina  left join usuarios u on u.codusuario=LiquidacionNomina.CODIGOUSUARIOCREACION  WHERE  LiquidacionNomina.CONSECUTIVO = " + pId.ToString();
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
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["nombre"]);
                            if (resultado["Observaciones"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["Observaciones"]);

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
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarLiquidacionNomina", ex);
                        return null;
                    }
                }
            }
        }


        public List<LiquidacionNomina> ListarLiquidacionNomina(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNomina> lstLiquidacionNomina = new List<LiquidacionNomina>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, nom.DESCRIPCION as desc_nomina, cen.DESCRIPCION as desc_centro_costo, usu.NOMBRE as usuario,E.NUM_COMP,E.TIPO_COMP
                                        FROM LIQUIDACIONNOMINA liq
                                        JOIN nomina_empleado nom on nom.Consecutivo = liq.CODIGONOMINA
                                        JOIN CENTRO_COSTO cen on cen.CENTRO_COSTO = liq.CODIGOCENTROCOSTO
                                        JOIN Usuarios usu on usu.CODUSUARIO = liq.CODIGOUSUARIOCREACION
                                        JOIN OPERACION o ON O.COD_OPE=liq.cod_ope
                                        LEFT JOIN E_COMPROBANTE E ON E.NUM_COMP=O.NUM_COMP AND E.TIPO_COMP=O.TIPO_COMP  " + filtro + " ORDER BY liq.fechainicio desc, liq.fechaterminacion desc, liq.consecutivo desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNomina entidad = new LiquidacionNomina();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAGENERACION"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHAGENERACION"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);

                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["usuario"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (entidad.estado == "P")
                            {
                                entidad.estado = "PRUEBA";
                            }
                            if (entidad.estado == "D")
                            {
                                entidad.estado = "DEFINITIVA";
                            }

                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToString(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);


                            lstLiquidacionNomina.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNomina", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNomina ConsultarUltimaFechaLiquidacionNomina(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionNomina entidad = new LiquidacionNomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT consecutivo,tiponomina,fecha_ult_liquidacion,FECHA_INI_LIQUIDACION_NOMINA(consecutivo) AS FECHA_INI,FECHA_FIN_LIQUIDACION_NOMINA(FECHA_INI_LIQUIDACION_NOMINA(consecutivo),(consecutivo)) AS FECHA_FIN FROM nomina_empleado WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA_ULT_LIQUIDACION"] != DBNull.Value) entidad.fechaultliquidacion = Convert.ToDateTime(resultado["FECHA_ULT_LIQUIDACION"]);
                            if (resultado["tiponomina"] != DBNull.Value) entidad.tiponomina = Convert.ToInt32(resultado["tiponomina"]);
                            if (resultado["FECHA_INI"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHA_INI"]);
                            if (resultado["FECHA_FIN"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHA_FIN"]);
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
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarUltimaFechaLiquidacionNomina", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticipos(LiquidacionNomina liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);


                        DbParameter p_codempleado = cmdTransaccionFactory.CreateParameter();
                        p_codempleado.ParameterName = "P_CODIGOEMPLEADO";
                        p_codempleado.Value = liquidacion.codigoempleado;
                        p_codempleado.Direction = ParameterDirection.Input;
                        p_codempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codempleado);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOMCALCULA_ANTICIPOS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                        List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();

                        string sql = @"select temp.*, ING.CODIGOCARGO, car.DESCRIPCION as desc_cargo
                                        from TEMP_EMPLIQANTICIPOS_NOMINA temp
                                        JOIN INGRESOPERSONAL ING on ING.CODIGOEMPLEADO = temp.CONSECUTIVOEMPLEADO and ING.CODIGONOMINA = " + liquidacion.codigonomina +
                                        " JOIN CARGO_nomina car on car.IDCARGO = ing.codigocargo  where ing.ESTAACTIVOCONTRATO=1 order by  ING.CODIGOEMPLEADO  asc";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetalle entidad = new LiquidacionNominaDetalle();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.porcentaje_anticipo = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO"]);
                            if (resultado["DIAS_LIQUIDADOS"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["DIAS_LIQUIDADOS"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["VALOR_ANTICIPO_SUB"] != DBNull.Value) entidad.valor_anticipo_sub = Convert.ToDecimal(resultado["VALOR_ANTICIPO_SUB"]);
                            if (resultado["PORCENTAJE_ANTICIPO_SUB"] != DBNull.Value) entidad.porcentaje_anticipo_sub = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO_SUB"]);
                            if (resultado["SUBSIDIO_TRANSPORTE"] != DBNull.Value) entidad.subsidio_transporte = Convert.ToDecimal(resultado["SUBSIDIO_TRANSPORTE"]);

                            liquidacionDetalle.Add(entidad);
                        }

                        sql = @"select * from TEMP_EMPLIQUIDACIONCONCEPTOS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            liquidacionDetalleEmpleados.Add(entidad);
                        }



                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "GenerarAnticipos", ex);
                        return null;
                    }
                }
            }
        }
        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticiposInd(LiquidacionNomina liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_codempleado = cmdTransaccionFactory.CreateParameter();
                        p_codempleado.ParameterName = "P_CODIGOEMPLEADO";
                        p_codempleado.Value = liquidacion.codigoempleado;
                        p_codempleado.Direction = ParameterDirection.Input;
                        p_codempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codempleado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOMCALCULA_ANTICIPOS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                        List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();

                        string sql = @"select temp.*, ING.CODIGOCARGO, car.DESCRIPCION as desc_cargo
                                        from TEMP_EMPLIQANTICIPOS_NOMINA temp
                                        JOIN INGRESOPERSONAL ING on ING.CODIGOEMPLEADO = temp.CONSECUTIVOEMPLEADO and ING.CODIGONOMINA = " + liquidacion.codigonomina +
                                        " JOIN CARGO_nomina car on car.IDCARGO = ing.codigocargo  where ing.ESTAACTIVOCONTRATO=1 order by  ING.CODIGOEMPLEADO  asc";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetalle entidad = new LiquidacionNominaDetalle();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.porcentaje_anticipo = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO"]);
                            if (resultado["DIAS_LIQUIDADOS"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["DIAS_LIQUIDADOS"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["VALOR_ANTICIPO_SUB"] != DBNull.Value) entidad.valor_anticipo_sub = Convert.ToDecimal(resultado["VALOR_ANTICIPO_SUB"]);
                            if (resultado["PORCENTAJE_ANTICIPO_SUB"] != DBNull.Value) entidad.porcentaje_anticipo_sub = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO_SUB"]);
                            if (resultado["SUBSIDIO_TRANSPORTE"] != DBNull.Value) entidad.subsidio_transporte = Convert.ToDecimal(resultado["SUBSIDIO_TRANSPORTE"]);

                            liquidacionDetalle.Add(entidad);
                        }

                        sql = @"select * from TEMP_EMPLIQUIDACIONCONCEPTOS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNominaDetaEmpleado entidad = new LiquidacionNominaDetaEmpleado();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) entidad.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);

                            liquidacionDetalleEmpleados.Add(entidad);
                        }



                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "GenerarAnticiposInd", ex);
                        return null;
                    }
                }
            }
        }
        public Tuple<LiquidacionNominaDetalle, LiquidacionNominaDetaEmpleado> GenerarAncticipsPorEmpleado(LiquidacionNomina liquidacion, Empleados empleados, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoEmpleado = cmdTransaccionFactory.CreateParameter();
                        p_codigoEmpleado.ParameterName = "p_codigoEmpleado";
                        p_codigoEmpleado.Value = empleados.consecutivo;
                        p_codigoEmpleado.Direction = ParameterDirection.Input;
                        p_codigoEmpleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoEmpleado);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_borrarTemporalesAntes = cmdTransaccionFactory.CreateParameter();
                        p_borrarTemporalesAntes.ParameterName = "p_borrarTemporalesAntes";
                        p_borrarTemporalesAntes.Value = 1;
                        p_borrarTemporalesAntes.Direction = ParameterDirection.Input;
                        p_borrarTemporalesAntes.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_borrarTemporalesAntes);

                        DbParameter p_liquidaTodoPendiente = cmdTransaccionFactory.CreateParameter();
                        p_liquidaTodoPendiente.ParameterName = "p_liquidaTodoPendiente";
                        p_liquidaTodoPendiente.Value = 0; // No
                        p_liquidaTodoPendiente.Direction = ParameterDirection.Input;
                        p_liquidaTodoPendiente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_liquidaTodoPendiente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CALCULARLIQUI_EMPLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        LiquidacionNominaDetalle liquidacionDef = new LiquidacionNominaDetalle();
                        LiquidacionNominaDetaEmpleado liquidacionDetalle = new LiquidacionNominaDetaEmpleado();

                        string sql = @"select * from TEMP_EMPLEADOLIQUIDACION order by CONSECUTIVOEMPLEADO asc";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) liquidacionDef.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) liquidacionDef.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) liquidacionDef.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALARIO"] != DBNull.Value) liquidacionDef.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) liquidacionDef.valortotalapagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["DiasLiquidar"] != DBNull.Value) liquidacionDef.dias = Convert.ToInt64(resultado["DiasLiquidar"]);
                        }

                        sql = @"select * from TEMP_EMPLIQUIDACIONCONCEPTOS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) liquidacionDetalle.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) liquidacionDetalle.codigoconcepto = Convert.ToInt32(resultado["CODIGOCONCEPTO"]);
                            if (resultado["DESCRIPCIONCONCEPTO"] != DBNull.Value) liquidacionDetalle.descripcion_concepto = Convert.ToString(resultado["DESCRIPCIONCONCEPTO"]);
                            if (resultado["TIPO"] != DBNull.Value) liquidacionDetalle.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALORCONCEPTO"] != DBNull.Value) liquidacionDetalle.valorconcepto = Convert.ToDecimal(resultado["VALORCONCEPTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionDef, liquidacionDetalle);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "GenerarAncticipsPorEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNomina CrearAnticiposNomina(LiquidacionNomina pLiquidacionNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNomina.consecutivo;
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
                        pfechageneracion.Value = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionNomina.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionNomina.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionNomina.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNomina.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionNomina.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);


                        DbParameter psalario = cmdTransaccionFactory.CreateParameter();
                        psalario.ParameterName = "p_salario";
                        psalario.Value = pLiquidacionNomina.salario;
                        psalario.Direction = ParameterDirection.Input;
                        psalario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psalario);

                        DbParameter pporcentajeanticipos = cmdTransaccionFactory.CreateParameter();
                        pporcentajeanticipos.ParameterName = "p_porcentaje_anticipo";
                        pporcentajeanticipos.Value = pLiquidacionNomina.porcentaje_anticipo;
                        pporcentajeanticipos.Direction = ParameterDirection.Input;
                        pporcentajeanticipos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentajeanticipos);

                        DbParameter pvaloranticipos = cmdTransaccionFactory.CreateParameter();
                        pvaloranticipos.ParameterName = "p_valor_anticipo";
                        pvaloranticipos.Value = pLiquidacionNomina.valor_anticipo;
                        pvaloranticipos.Direction = ParameterDirection.Input;
                        pvaloranticipos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvaloranticipos);

                        DbParameter p_porcentaje_anticipo_sub = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje_anticipo_sub.ParameterName = "p_porcentaje_anticipo_sub";
                        p_porcentaje_anticipo_sub.Value = pLiquidacionNomina.porcentaje_anticipo_sub;
                        p_porcentaje_anticipo_sub.Direction = ParameterDirection.Input;
                        p_porcentaje_anticipo_sub.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje_anticipo_sub);

                        DbParameter pvaloranticipos_sub = cmdTransaccionFactory.CreateParameter();
                        pvaloranticipos_sub.ParameterName = "p_valor_anticipo_sub";
                        pvaloranticipos_sub.Value = pLiquidacionNomina.valor_anticipo_sub;
                        pvaloranticipos_sub.Direction = ParameterDirection.Input;
                        pvaloranticipos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvaloranticipos_sub);



                        DbParameter pdias_liquidados = cmdTransaccionFactory.CreateParameter();
                        pdias_liquidados.ParameterName = "p_dias_liquidados";
                        pdias_liquidados.Value = pLiquidacionNomina.dias_liquidados;
                        pdias_liquidados.Direction = ParameterDirection.Input;
                        pdias_liquidados.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdias_liquidados);


                        DbParameter pfechaliquidacion = cmdTransaccionFactory.CreateParameter();
                        pfechaliquidacion.ParameterName = "p_fecha_liquidacion";
                        pfechaliquidacion.Value = pLiquidacionNomina.fechageneracion;
                        pfechaliquidacion.Direction = ParameterDirection.Input;
                        pfechaliquidacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaliquidacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pLiquidacionNomina.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);


                        DbParameter p_consecutivoanticipo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivoanticipo.ParameterName = "p_consecutivo_anticipo";
                        p_consecutivoanticipo.Value = pLiquidacionNomina.codigoanticipo;
                        p_consecutivoanticipo.Direction = ParameterDirection.Input;
                        p_consecutivoanticipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_consecutivoanticipo);


                        DbParameter P_TIPO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO.ParameterName = "P_TIPO";
                        P_TIPO.Value = pLiquidacionNomina.tipoanticipo;
                        P_TIPO.Direction = ParameterDirection.Input;
                        P_TIPO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO);

    
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_ANTICIPOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNomina.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearAnticiposNomina", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNomina> ListarAnticiposNomina(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNomina> lstLiquidacionNomina = new List<LiquidacionNomina>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT(CODIGO_ANTICIPO),SUM(VALOR_ANTICIPO) AS VALOR_ANTICIPO,SUM(SALARIO) AS SALARIO,CODIGOUSUARIOCREACION,PORCENTAJE_ANTICIPO,FECHAINICIO,FECHATERMINACION,
                                        an.COD_OPE,FECHA_LIQ_ANTICIPO,CODIGONOMINA,nom.DESCRIPCION as desc_nomina,codigocentrocosto,cen.DESCRIPCION as desc_centro_costo, usu.NOMBRE as usuario, O.NUM_COMP,O.TIPO_COMP
                                        FROM ANTICIPOS_NOMINA an
                                        INNER JOIN nomina_empleado nom on nom.Consecutivo =  an.CODIGONOMINA
                                        INNER JOIN CENTRO_COSTO cen on cen.CENTRO_COSTO = an.CODIGOCENTROCOSTO
                                        INNER JOIN Usuarios usu on usu.CODUSUARIO = an.CODIGOUSUARIOCREACION
                                        LEFT JOIN OPERACION O ON O.COD_OPE=AN.COD_OPE " + filtro +
                                        " GROUP BY CODIGO_ANTICIPO,CODIGOUSUARIOCREACION,PORCENTAJE_ANTICIPO,FECHAINICIO,FECHATERMINACION,an.COD_OPE,"+
                                        " FECHA_LIQ_ANTICIPO,CODIGONOMINA,nom.DESCRIPCION,codigocentrocosto,cen.DESCRIPCION, usu.NOMBRE, O.NUM_COMP,O.TIPO_COMP  ORDER BY an.CODIGO_ANTICIPO desc, an.fechainicio desc, an.fechaterminacion desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNomina entidad = new LiquidacionNomina();

                            if (resultado["CODIGO_ANTICIPO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CODIGO_ANTICIPO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["usuario"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.porcentaje_anticipo = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToInt64(resultado["VALOR_ANTICIPO"]);
                            if (resultado["FECHA_LIQ_ANTICIPO"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHA_LIQ_ANTICIPO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToString(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);

                            lstLiquidacionNomina.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarAnticiposNomina", ex);
                        return null;
                    }
                }
            }
        }
        public List<LiquidacionNomina> ListarAnticiposNominaInd(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionNomina> lstLiquidacionNomina = new List<LiquidacionNomina>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT(CODIGO_ANTICIPO),SUM(an.VALOR_ANTICIPO) AS VALOR_ANTICIPO,SUM(an.SALARIO) AS SALARIO,CODIGOUSUARIOCREACION,an.PORCENTAJE_ANTICIPO,an.FECHAINICIO,an.FECHATERMINACION,
                                        an.COD_OPE,FECHA_LIQ_ANTICIPO,an.CODIGONOMINA,nom.DESCRIPCION as desc_nomina,an.codigocentrocosto,cen.DESCRIPCION as desc_centro_costo, usu.NOMBRE as usuario, O.NUM_COMP,O.TIPO_COMP, VP.IDENTIFICACION
                                        FROM ANTICIPOS_NOMINA an
                                        INNER JOIN nomina_empleado nom on nom.Consecutivo =  an.CODIGONOMINA
                                        INNER JOIN CENTRO_COSTO cen on cen.CENTRO_COSTO = an.CODIGOCENTROCOSTO
                                        INNER JOIN Usuarios usu on usu.CODUSUARIO = an.CODIGOUSUARIOCREACION
                                        LEFT JOIN OPERACION O ON O.COD_OPE=AN.COD_OPE
                                        INNER JOIN INGRESOPERSONAL IP ON IP.CONSECUTIVO= an.CONSECUTIVOCONTRATO
                                        INNER JOIN V_PERSONA VP ON VP.COD_PERSONA=IP.CODIGOPERSONA " + filtro +
                                        " group by CODIGO_ANTICIPO,CODIGOUSUARIOCREACION,an.PORCENTAJE_ANTICIPO,an.FECHAINICIO,an.FECHATERMINACION,an.COD_OPE,"+
                                       " an.FECHA_LIQ_ANTICIPO,an.CODIGONOMINA,nom.DESCRIPCION, an.codigocentrocosto,cen.DESCRIPCION, usu.NOMBRE, O.NUM_COMP,O.TIPO_COMP, VP.IDENTIFICACION " + " ORDER BY an.CODIGO_ANTICIPO desc, an.fechainicio desc, an.fechaterminacion desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNomina entidad = new LiquidacionNomina();

                            if (resultado["CODIGO_ANTICIPO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CODIGO_ANTICIPO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["usuario"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["usuario"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.porcentaje_anticipo = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToInt64(resultado["VALOR_ANTICIPO"]);
                            if (resultado["FECHA_LIQ_ANTICIPO"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHA_LIQ_ANTICIPO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToString(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            lstLiquidacionNomina.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarAnticiposNominaInd", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionNomina ConsultarAnticiposNomina(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionNomina entidad = new LiquidacionNomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM anticipos_nomina WHERE codigo_anticipo = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHA_LIQ_ANTICIPO"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHA_LIQ_ANTICIPO"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            if (resultado["DIAS_LIQUIDADOS"] != DBNull.Value) entidad.dias_liquidados = Convert.ToInt64(resultado["DIAS_LIQUIDADOS"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.dias_liquidados = Convert.ToInt64(resultado["DIAS_LIQUIDADOS"]);


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
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarAnticiposNomina", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionNominaDetalle>  ListarAnticiposNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<LiquidacionNominaDetalle> listaLiquidacion = new List<LiquidacionNominaDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select an.*, per.nombre, per.identificacion, ing.CODIGOCARGO, car.DESCRIPCION as desc_cargo
                                        from ANTICIPOS_NOMINA an 
                                        JOIN empleados emp on an.CodigoEmpleado = emp.consecutivo
                                        join v_persona per on emp.COD_PERSONA = per.cod_persona
                                        join ingresopersonal ing on  AN.CONSECUTIVOCONTRATO=ing.consecutivo and  emp.consecutivo = ing.CODIGOEMPLEADO and ing.CODIGONOMINA =  
                                        (
                                        select DISTINCT(CODIGONOMINA) FROM ANTICIPOS_NOMINA WHERE CODIGONOMINA = an.CODIGONOMINA " +
                                     @" )
                                        join CARGO_NOMINA car on car.IDCARGO = ing.codigocargo
                                        where an.codigo_anticipo = " + liquidacion.consecutivo + "order by ing.codigoempleado asc  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;

                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionNominaDetalle entidad = new LiquidacionNominaDetalle();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGO_ANTICIPO"] != DBNull.Value) entidad.codigo_anticipo = Convert.ToInt64(resultado["CODIGO_ANTICIPO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["DIAS_LIQUIDADOS"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["DIAS_LIQUIDADOS"]);
                            if (resultado["PORCENTAJE_ANTICIPO"] != DBNull.Value) entidad.porcentaje_anticipo = Convert.ToInt64(resultado["PORCENTAJE_ANTICIPO"]);
                            if (resultado["VALOR_ANT_SUB_TRANS"] != DBNull.Value) entidad.valor_anticipo_sub = Convert.ToDecimal(resultado["VALOR_ANT_SUB_TRANS"]);
                            if (resultado["PORCENTAJE_ANT_SUB_TRANS"] != DBNull.Value) entidad.porcentaje_anticipo_sub = Convert.ToInt64(resultado["PORCENTAJE_ANT_SUB_TRANS"]);


                            listaLiquidacion.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarLiquidacionNominaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionNomina ConsultarUltimaFechaAnticiposNomina(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionNomina entidad = new LiquidacionNomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"sELECT DISTINCT(fechaterminacion),FECHA_INI_ANTICIPOS_NOMINA(codigonomina)AS FECHA_INI,FECHA_FIN_ANTICIPOS_NOMINA(FECHA_INI_ANTICIPOS_NOMINA(codigonomina),(codigonomina)) AS FECHA_FIN FROM anticipos_nomina WHERE codigonomina =" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["fechaterminacion"] != DBNull.Value) entidad.fechaultliquidacion = Convert.ToDateTime(resultado["fechaterminacion"]);
                            // if (resultado["tiponomina"] != DBNull.Value) entidad.tiponomina = Convert.ToInt32(resultado["tiponomina"]);
                            if (resultado["FECHA_INI"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHA_INI"]);
                            if (resultado["FECHA_FIN"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHA_FIN"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarUltimaFechaAnticiposNomina", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionNomina ConsultarUltimoAnticiposNomina(Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionNomina entidad = new LiquidacionNomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT max(CODIGO_ANTICIPO)+1 as CODIGO_ANTICIPO    FROM ANTICIPOS_NOMINA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_ANTICIPO"] != DBNull.Value) entidad.codigoanticipo = Convert.ToInt64(resultado["CODIGO_ANTICIPO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarUltimoAnticiposNomina", ex);
                        return null;
                    }
                }
            }
        }

        public Nomina_Entidad ConsultarDatosLiquidacion(Int64 pId, Usuario vUsuario, Int64 pCodEMpleado)
        {
            DbDataReader resultado;
            Nomina_Entidad entidad = new Nomina_Entidad();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"   select   LD.DIASLIQUIDAR,IP.FECHAINGRESO,IP.CODIGOPERSONA,LN.CODIGOEMPLEADO,IP.CODIGOCENTROCOSTO ,
                                           EMPRESANOMINAENTIDAD(1,IP.CODIGOFONDOSALUD)AS FONDOSALUD,EMPRESANOMINAENTIDAD(1,IP.CODIGOFONDOPENSION)AS FONDOPENSION,
                                            EMPRESANOMINAENTIDAD(1,IP.CODIGOFONDOCESANTIAS)AS FONDOCESANTIAS,EMPRESANOMINAENTIDAD(2,IP.CODIGOCAJACOMPENSACION)AS CAJACOMPENSACION,
                                            EMPRESANOMINAENTIDAD(1,IP.CODIGOARL)AS ARL,
                                            (case  IP.FormaPago when 1 then 'Efectivo' when 2 then 'Cheque' when 3 then 'Consignacion' end)As Forma_Pago,b.NOMBREBANCO,C.NOM_CENTRO AS CENTRO_COSTO
                                            from LIQUIDACIONNOMINADETAEMPLEADO LN 
                                            INNER JOIN liquidacionnominadetalle LD ON LD.CONSECUTIVO=LN.CODIGOLIQUIDACIONNOMINADETALLE
                                            INNER JOIN liquidacionnomina L ON L.CONSECUTIVO=LD.CODIGOLIQUIDACIONNOMINA
                                            INNER JOIN INGRESOPERSONAL IP on IP.CODIGOEMPLEADO=LN.CODIGOEMPLEADO
                                            LEFT JOIN bancos b on b.cod_banco=ip.codigobanco
                                            LEFT JOIN CENTRO_COSTO C on C.CENTRO_COSTO=IP.CODIGOCENTROCOSTO
                                            WHERE  IP.ESTAACTIVOCONTRATO=1 and  LN.CODIGOCONCEPTO in(select consecutivo from concepto_nomina  where tipoconcepto in(17,11))
                                            and  l.CONSECUTIVO = " + pId.ToString() + " and  LN.CODIGOEMPLEADO = " + pCodEMpleado.ToString()
                                            + " order by C.NOM_CENTRO  asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["DIASLIQUIDAR"] != DBNull.Value) entidad.dias_liquidar = Convert.ToInt64(resultado["DIASLIQUIDAR"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.Fecha_ingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigo_persona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigo_empleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FONDOSALUD"] != DBNull.Value) entidad.fondosalud = Convert.ToString(resultado["FONDOSALUD"]);
                            if (resultado["FONDOPENSION"] != DBNull.Value) entidad.fondopension = Convert.ToString(resultado["FONDOPENSION"]);
                            if (resultado["FONDOCESANTIAS"] != DBNull.Value) entidad.cesantias = Convert.ToString(resultado["FONDOCESANTIAS"]);
                            if (resultado["CAJACOMPENSACION"] != DBNull.Value) entidad.cajacompensacion = Convert.ToString(resultado["CAJACOMPENSACION"]);
                            if (resultado["ARL"] != DBNull.Value) entidad.arl = Convert.ToString(resultado["ARL"]);
                            if (resultado["Forma_Pago"] != DBNull.Value) entidad.formapago = Convert.ToString(resultado["Forma_Pago"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.entidadpago = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.CENTRO_COSTO_NOM = Convert.ToString(resultado["CENTRO_COSTO"]);

                        }
                        else
                        {
                            //     throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ConsultarDatosLiquidacion", ex);
                        return null;
                    }
                }
            }
        }





        public List<LiquidacionNomina> ListarReportesNomina(LiquidacionNomina liquidacion, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<LiquidacionNomina> lstReportes = new List<LiquidacionNomina>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;


                        DbParameter p_fechaInicio = cmdTransaccionFactory.CreateParameter();
                        p_fechaInicio.ParameterName = "p_fechaInicio";
                        p_fechaInicio.Value = liquidacion.fechainicio;
                        p_fechaInicio.Direction = ParameterDirection.Input;
                        p_fechaInicio.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaInicio);

                        DbParameter p_fechaFin = cmdTransaccionFactory.CreateParameter();
                        p_fechaFin.ParameterName = "p_fechaFin";
                        p_fechaFin.Value = liquidacion.fechaterminacion;
                        p_fechaFin.Direction = ParameterDirection.Input;
                        p_fechaFin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fechaFin);

                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = liquidacion.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = liquidacion.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);


                        DbParameter pORIGEN = cmdTransaccionFactory.CreateParameter();
                        pORIGEN.ParameterName = "LORIGEN";
                        pORIGEN.Value = liquidacion.origen;
                        pORIGEN.Direction = ParameterDirection.Input;
                        pORIGEN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pORIGEN);



                        //  connection.Open();
                        ////   cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_REPORTES";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }

                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_NOM_REPORTES order by 1";  //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionNomina entidad = new LiquidacionNomina();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstReportes.Add(entidad);
                        }

                        return lstReportes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionNominaData", "ListarReportesNomina", ex);
                        return null;
                    }
                }
            }
        }



    }
}