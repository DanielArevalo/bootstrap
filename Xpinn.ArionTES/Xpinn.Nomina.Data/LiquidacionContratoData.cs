using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class LiquidacionContratoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionContratoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public LiquidacionContrato CrearLiquidacionContrato(LiquidacionContrato pLiquidacionContrato, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionContrato.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionContrato.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionContrato.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pfecharetiro = cmdTransaccionFactory.CreateParameter();
                        pfecharetiro.ParameterName = "p_fecharetiro";
                        pfecharetiro.Value = pLiquidacionContrato.fecharetiro;
                        pfecharetiro.Direction = ParameterDirection.Input;
                        pfecharetiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetiro);

                        DbParameter pcodigotiporetirocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotiporetirocontrato.ParameterName = "p_codigotiporetirocontrato";
                        pcodigotiporetirocontrato.Value = pLiquidacionContrato.codigotiporetirocontrato;
                        pcodigotiporetirocontrato.Direction = ParameterDirection.Input;
                        pcodigotiporetirocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiporetirocontrato);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = vUsuario.codusuario;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = DateTime.Now;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionContrato.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter p_codigoingresopersonal = cmdTransaccionFactory.CreateParameter();
                        p_codigoingresopersonal.ParameterName = "p_codigoingresopersonal";
                        p_codigoingresopersonal.Value = pLiquidacionContrato.codigoingresopersonal;
                        p_codigoingresopersonal.Direction = ParameterDirection.Input;
                        p_codigoingresopersonal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoingresopersonal);

                        DbParameter p_primaCalculo = cmdTransaccionFactory.CreateParameter();
                        p_primaCalculo.ParameterName = "p_primaCalculo";
                        p_primaCalculo.Value = pLiquidacionContrato.primaCalculo;
                        p_primaCalculo.Direction = ParameterDirection.Input;
                        p_primaCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_primaCalculo);

                        DbParameter p_primaDias = cmdTransaccionFactory.CreateParameter();
                        p_primaDias.ParameterName = "p_primaDias";
                        p_primaDias.Value = pLiquidacionContrato.primaDias;
                        p_primaDias.Direction = ParameterDirection.Input;
                        p_primaDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_primaDias);

                        DbParameter p_cesantiasCalculo = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasCalculo.ParameterName = "p_cesantiasCalculo";
                        p_cesantiasCalculo.Value = pLiquidacionContrato.cesantiasCalculo;
                        p_cesantiasCalculo.Direction = ParameterDirection.Input;
                        p_cesantiasCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasCalculo);

                        DbParameter p_cesantiasDias = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasDias.ParameterName = "p_cesantiasDias";
                        p_cesantiasDias.Value = pLiquidacionContrato.cesantiasDias;
                        p_cesantiasDias.Direction = ParameterDirection.Input;
                        p_cesantiasDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasDias);

                        DbParameter p_vacacionesCalculo = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesCalculo.ParameterName = "p_vacacionesCalculo";
                        p_vacacionesCalculo.Value = pLiquidacionContrato.vacacionesCalculo;
                        p_vacacionesCalculo.Direction = ParameterDirection.Input;
                        p_vacacionesCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesCalculo);

                        DbParameter p_vacacionesDias = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesDias.ParameterName = "p_vacacionesDias";
                        p_vacacionesDias.Value = pLiquidacionContrato.vacacionesDias;
                        p_vacacionesDias.Direction = ParameterDirection.Input;
                        p_vacacionesDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesDias);



                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pLiquidacionContrato.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQCONTRATO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionContrato.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionContrato;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "CrearLiquidacionContrato", ex);
                        return null;
                    }
                }
            }
        }

        public Tuple<List<LiquidacionContratoDetalle>, LiquidacionContrato> GenerarLiquidacionDeContrato(LiquidacionContrato liquidacion, Usuario pusuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fecharetiro = cmdTransaccionFactory.CreateParameter();
                        p_fecharetiro.ParameterName = "p_fecharetiro";
                        p_fecharetiro.Value = liquidacion.fecharetiro;
                        p_fecharetiro.Direction = ParameterDirection.Input;
                        p_fecharetiro.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecharetiro);

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

                        DbParameter p_primaCalculo = cmdTransaccionFactory.CreateParameter();
                        p_primaCalculo.ParameterName = "p_primaCalculo";
                        p_primaCalculo.Value = liquidacion.primaCalculo;
                        p_primaCalculo.Direction = ParameterDirection.Output;
                        p_primaCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_primaCalculo);

                        DbParameter p_primaDias = cmdTransaccionFactory.CreateParameter();
                        p_primaDias.ParameterName = "p_primaDias";
                        p_primaDias.Value = liquidacion.primaDias;
                        p_primaDias.Direction = ParameterDirection.Output;
                        p_primaDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_primaDias);

                        DbParameter p_cesantiasCalculo = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasCalculo.ParameterName = "p_cesantiasCalculo";
                        p_cesantiasCalculo.Value = liquidacion.cesantiasCalculo;
                        p_cesantiasCalculo.Direction = ParameterDirection.Output;
                        p_cesantiasCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasCalculo);

                        DbParameter p_cesantiasDias = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasDias.ParameterName = "p_cesantiasDias";
                        p_cesantiasDias.Value = liquidacion.cesantiasDias;
                        p_cesantiasDias.Direction = ParameterDirection.Output;
                        p_cesantiasDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasDias);

                        DbParameter p_vacacionesCalculo = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesCalculo.ParameterName = "p_vacacionesCalculo";
                        p_vacacionesCalculo.Value = liquidacion.vacacionesCalculo;
                        p_vacacionesCalculo.Direction = ParameterDirection.Output;
                        p_vacacionesCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesCalculo);

                        DbParameter p_vacacionesDias = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesDias.ParameterName = "p_vacacionesDias";
                        p_vacacionesDias.Value = liquidacion.vacacionesDias;
                        p_vacacionesDias.Direction = ParameterDirection.Output;
                        p_vacacionesDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesDias);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_GENLIQCONTRATO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        liquidacion.primaCalculo = p_primaCalculo.Value != DBNull.Value ? Convert.ToDecimal(p_primaCalculo.Value) : 0;
                        liquidacion.primaDias = p_primaDias.Value != DBNull.Value ? Convert.ToInt64(p_primaDias.Value) : 0;
                        liquidacion.cesantiasCalculo = p_cesantiasCalculo.Value != DBNull.Value ? Convert.ToDecimal(p_cesantiasCalculo.Value) : 0;
                        liquidacion.cesantiasDias = p_cesantiasDias.Value != DBNull.Value ? Convert.ToInt64(p_cesantiasDias.Value) : 0;
                        liquidacion.vacacionesCalculo = p_vacacionesCalculo.Value != DBNull.Value ? Convert.ToDecimal(p_vacacionesCalculo.Value) : 0;
                        liquidacion.vacacionesDias = p_vacacionesDias.Value != DBNull.Value ? Convert.ToInt64(p_vacacionesDias.Value) : 0;

                        List<LiquidacionContratoDetalle> listaLiquidacionDetalle = new List<LiquidacionContratoDetalle>();

                        string sql = @"select * from TEMP_LIQCONCEPTOSCONTRATO ORDER BY CONSECUTIVOCONCEPTO ";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionContratoDetalle liquidacionDetalle = new LiquidacionContratoDetalle();

                            if (resultado["ConsecutivoConcepto"] != DBNull.Value) liquidacionDetalle.codigoconceptonomina = Convert.ToInt64(resultado["ConsecutivoConcepto"]);
                            if (resultado["ValorConcepto"] != DBNull.Value) liquidacionDetalle.valor = Convert.ToDecimal(resultado["ValorConcepto"]);
                            if (resultado["DescripcionConcepto"] != DBNull.Value) liquidacionDetalle.desc_conceptoNomina = Convert.ToString(resultado["DescripcionConcepto"]);
                            if (resultado["TIPO"] != DBNull.Value) liquidacionDetalle.tipoCalculo = Convert.ToInt64(resultado["TIPO"]);

                            listaLiquidacionDetalle.Add(liquidacionDetalle);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(listaLiquidacionDetalle, liquidacion);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "GenerarLiquidacionDeContrato", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionContratoDetalle CrearLiquidacionContratoDetalle(LiquidacionContratoDetalle pLiquidacionContratoDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionContratoDetalle.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacioncontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacioncontrato.ParameterName = "p_codigoliquidacioncontrato";
                        pcodigoliquidacioncontrato.Value = pLiquidacionContratoDetalle.codigoliquidacioncontrato;
                        pcodigoliquidacioncontrato.Direction = ParameterDirection.Input;
                        pcodigoliquidacioncontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacioncontrato);

                        DbParameter pcodigoconceptonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigoconceptonomina.ParameterName = "p_codigoconceptonomina";
                        pcodigoconceptonomina.Value = pLiquidacionContratoDetalle.codigoconceptonomina;
                        pcodigoconceptonomina.Direction = ParameterDirection.Input;
                        pcodigoconceptonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconceptonomina);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pLiquidacionContratoDetalle.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQCONTRADET_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionContratoDetalle.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionContratoDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "CrearLiquidacionContratoDetalle", ex);
                        return null;
                    }
                }
            }
        }
       public LiquidacionContrato CrearLiquidContratoNominaInterfaz(LiquidacionContrato pliquidacioncontrato,  Usuario vUsuario)
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
                        BOExcepcion.Throw("LiquidacionNominaData", "CrearLiquidContratoNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionContrato ModificarLiquidacionContrato(LiquidacionContrato pLiquidacionContrato, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionContrato.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionContrato.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionContrato.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pfecharetiro = cmdTransaccionFactory.CreateParameter();
                        pfecharetiro.ParameterName = "p_fecharetiro";
                        pfecharetiro.Value = pLiquidacionContrato.fecharetiro;
                        pfecharetiro.Direction = ParameterDirection.Input;
                        pfecharetiro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetiro);

                        DbParameter pcodigotiporetirocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotiporetirocontrato.ParameterName = "p_codigotiporetirocontrato";
                        pcodigotiporetirocontrato.Value = pLiquidacionContrato.codigotiporetirocontrato;
                        pcodigotiporetirocontrato.Direction = ParameterDirection.Input;
                        pcodigotiporetirocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiporetirocontrato);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = vUsuario.codusuario;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = DateTime.Today;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionContrato.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter p_codigoingresopersonal = cmdTransaccionFactory.CreateParameter();
                        p_codigoingresopersonal.ParameterName = "p_codigoingresopersonal";
                        p_codigoingresopersonal.Value = pLiquidacionContrato.codigoingresopersonal;
                        p_codigoingresopersonal.Direction = ParameterDirection.Input;
                        p_codigoingresopersonal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoingresopersonal);

                        DbParameter p_primaCalculo = cmdTransaccionFactory.CreateParameter();
                        p_primaCalculo.ParameterName = "p_primaCalculo";
                        p_primaCalculo.Value = pLiquidacionContrato.primaCalculo;
                        p_primaCalculo.Direction = ParameterDirection.Input;
                        p_primaCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_primaCalculo);

                        DbParameter p_primaDias = cmdTransaccionFactory.CreateParameter();
                        p_primaDias.ParameterName = "p_primaDias";
                        p_primaDias.Value = pLiquidacionContrato.primaDias;
                        p_primaDias.Direction = ParameterDirection.Input;
                        p_primaDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_primaDias);

                        DbParameter p_cesantiasCalculo = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasCalculo.ParameterName = "p_cesantiasCalculo";
                        p_cesantiasCalculo.Value = pLiquidacionContrato.cesantiasCalculo;
                        p_cesantiasCalculo.Direction = ParameterDirection.Input;
                        p_cesantiasCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasCalculo);

                        DbParameter p_cesantiasDias = cmdTransaccionFactory.CreateParameter();
                        p_cesantiasDias.ParameterName = "p_cesantiasDias";
                        p_cesantiasDias.Value = pLiquidacionContrato.cesantiasDias;
                        p_cesantiasDias.Direction = ParameterDirection.Input;
                        p_cesantiasDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cesantiasDias);

                        DbParameter p_vacacionesCalculo = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesCalculo.ParameterName = "p_vacacionesCalculo";
                        p_vacacionesCalculo.Value = pLiquidacionContrato.vacacionesCalculo;
                        p_vacacionesCalculo.Direction = ParameterDirection.Input;
                        p_vacacionesCalculo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesCalculo);

                        DbParameter p_vacacionesDias = cmdTransaccionFactory.CreateParameter();
                        p_vacacionesDias.ParameterName = "p_vacacionesDias";
                        p_vacacionesDias.Value = pLiquidacionContrato.vacacionesDias;
                        p_vacacionesDias.Direction = ParameterDirection.Input;
                        p_vacacionesDias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_vacacionesDias);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQCONTRATO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiquidacionContrato;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "ModificarLiquidacionContrato", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLiquidacionContrato(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LiquidacionContrato pLiquidacionContrato = new LiquidacionContrato();
                        pLiquidacionContrato = ConsultarLiquidacionContrato(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionContrato.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQCONTRATO_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "EliminarLiquidacionContrato", ex);
                    }
                }
            }
        }


        public LiquidacionContrato ConsultarLiquidacionContrato(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionContrato entidad = new LiquidacionContrato();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, ing.FECHAINGRESO, ing.CODIGOTIPOCONTRATO, per.identificacion, per.nombre, per.TIPO_IDENTIFICACION,fecDIFdia(ing.fechaingreso,liq.fecharetiro,1) as cantidad ,
                                        ING.CODIGOCENTROCOSTO                           
                                        FROM LiquidacionContrato liq
                                        JOIN IngresoPersonal ing on ing.consecutivo = liq.codigoingresopersonal
                                        JOIN EMPLEADOS emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                        JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                        WHERE    ING.CONSECUTIVO=liq.CODIGOINGRESOPERSONAL and liq.CONSECUTIVO = " + pId.ToString();

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
                            if (resultado["FECHARETIRO"] != DBNull.Value) entidad.fecharetiro = Convert.ToDateTime(resultado["FECHARETIRO"]);
                            if (resultado["CODIGOTIPORETIROCONTRATO"] != DBNull.Value) entidad.codigotiporetirocontrato= Convert.ToInt64(resultado["CODIGOTIPORETIROCONTRATO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["CODIGOINGRESOPERSONAL"] != DBNull.Value) entidad.codigoingresopersonal = Convert.ToInt64(resultado["CODIGOINGRESOPERSONAL"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigoTipoContrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);

                            if (resultado["PRIMAVALORPAGADO"] != DBNull.Value) entidad.primaCalculo = Convert.ToDecimal(resultado["PRIMAVALORPAGADO"]);
                            if (resultado["PRIMADIASPAGADO"] != DBNull.Value) entidad.primaDias = Convert.ToInt64(resultado["PRIMADIASPAGADO"]);
                            if (resultado["CESANTIASVALORPAGADO"] != DBNull.Value) entidad.cesantiasCalculo = Convert.ToDecimal(resultado["CESANTIASVALORPAGADO"]);
                            if (resultado["CESANTIASDIASPAGADO"] != DBNull.Value) entidad.cesantiasDias = Convert.ToInt64(resultado["CESANTIASDIASPAGADO"]);
                            if (resultado["VACACIONESVALORPAGADO"] != DBNull.Value) entidad.vacacionesCalculo = Convert.ToDecimal(resultado["VACACIONESVALORPAGADO"]);
                            if (resultado["VACACIONESDIASPAGADO"] != DBNull.Value) entidad.vacacionesDias = Convert.ToInt64(resultado["VACACIONESDIASPAGADO"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.dias = Convert.ToInt64(resultado["CANTIDAD"]);

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
                        BOExcepcion.Throw("LiquidacionContratoData", "ConsultarLiquidacionContrato", ex);
                        return null;
                    }
                }
            }
        }


        public List<LiquidacionContrato> ListarLiquidacionContrato(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionContrato> lstLiquidacionContrato = new List<LiquidacionContrato>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, ing.FechaIngreso,
                                        nom.descripcion as desc_nomina, usu.NOMBRE as desc_usuario,O.NUM_COMP,O.TIPO_COMP
                                        FROM LIQUIDACIONCONTRATO liq
                                        JOIN INGRESOPERSONAL ing on ing.consecutivo = liq.CodigoIngresoPersonal
                                        JOIN NOMINA_EMPLEADO nom on nom.consecutivo = liq.CodigoNomina
                                        JOIN USUARIOS usu on usu.codusuario = liq.CODIGOUSUARIOCREACION
                                        JOIN Empleados emp on emp.consecutivo = liq.CODIGOEMPLEADO
                                        JOIN v_persona per on per.COD_PERSONA = emp.COD_PERSONA
                                        JOIN OPERACION O ON O.COD_OPE=LIQ.COD_OPE " + filtro + " ORDER BY liq.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionContrato entidad = new LiquidacionContrato();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["FECHARETIRO"] != DBNull.Value) entidad.fecharetiro = Convert.ToDateTime(resultado["FECHARETIRO"]);
                            if (resultado["CODIGOTIPORETIROCONTRATO"] != DBNull.Value) entidad.codigotiporetirocontrato = Convert.ToInt64(resultado["CODIGOTIPORETIROCONTRATO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["CodigoIngresoPersonal"] != DBNull.Value) entidad.codigoingresopersonal = Convert.ToInt64(resultado["CodigoIngresoPersonal"]);

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_usuario"] != DBNull.Value) entidad.desc_usuario = Convert.ToString(resultado["desc_usuario"]);
                            if (resultado["FechaIngreso"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FechaIngreso"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);

                            lstLiquidacionContrato.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstLiquidacionContrato;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "ListarLiquidacionContrato", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionContratoDetalle> ListarLiquidacionContratoDetalle(long consecutivoLiquidacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<LiquidacionContratoDetalle> lstLiquidacionContrato = new List<LiquidacionContratoDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, con.descripcion as desc_conceptoNomina, con.TIPO as tipoCalculo
                                        FROM LIQUIDACIONCONTRATODETALLE liq
                                        JOIN CONCEPTO_NOMINA con on con.consecutivo = liq.CODIGOCONCEPTONOMINA
                                        WHERE liq.CODIGOLIQUIDACIONCONTRATO = " + consecutivoLiquidacion + "  ORDER BY   tipocalculo  asc,con.descripcion asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionContratoDetalle entidad = new LiquidacionContratoDetalle();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["codigoliquidacioncontrato"] != DBNull.Value) entidad.codigoliquidacioncontrato = Convert.ToInt64(resultado["codigoliquidacioncontrato"]);
                            if (resultado["codigoconceptonomina"] != DBNull.Value) entidad.codigoconceptonomina = Convert.ToInt64(resultado["codigoconceptonomina"]);
                            if (resultado["desc_conceptoNomina"] != DBNull.Value) entidad.desc_conceptoNomina = Convert.ToString(resultado["desc_conceptoNomina"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipoCalculo"] != DBNull.Value) entidad.tipoCalculo = Convert.ToInt64(resultado["tipoCalculo"]);
                            
                            lstLiquidacionContrato.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstLiquidacionContrato;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionContratoData", "ListarLiquidacionContrato", ex);
                        return null;
                    }
                }
            }
        }

    }
}