using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ReestructuracionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public ReestructuracionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Listar créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Listar personas con créditos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarPersonas(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select cod_persona, identificacion, tipo_identificacion, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido , cod_nomina from persona " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);

                            lstCredito.Add(entidad);
                        }

                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefinanciacionData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Método para guardar datos de la re-estructuración
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="tipo"></param>
        /// <param name="oficina"></param>
        /// <param name="ciudad"></param>
        /// <param name="codigo"></param>
        /// <param name="archivo"></param>
        public void CrearReestructurar(Reestructuracion vReestructuracion, ref Int64 numero_radicacion, ref string error, Usuario pusuario)
        {
            error = "";
            numero_radicacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "pfecha_solicitud";
                        pfecha_solicitud.Value = vReestructuracion.fecha;
                        pfecha_solicitud.DbType = DbType.Date;


                        DbParameter pcod_deudor = cmdTransaccionFactory.CreateParameter();
                        pcod_deudor.ParameterName = "pcod_deudor";
                        pcod_deudor.Value = vReestructuracion.cod_deudor;
                        pcod_deudor.DbType = DbType.Int64;

                        DbParameter pmonto_solicitado = cmdTransaccionFactory.CreateParameter();
                        pmonto_solicitado.ParameterName = "pmonto_solicitado";
                        pmonto_solicitado.Value = vReestructuracion.monto_solicitado;
                        pmonto_solicitado.DbType = DbType.Double;

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "pnumero_cuotas";
                        pnumero_cuotas.Value = vReestructuracion.numero_cuotas;
                        pnumero_cuotas.DbType = DbType.Int64;

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "pcod_linea_credito";
                        pcod_linea_credito.Value = vReestructuracion.cod_linea_credito;
                        pcod_linea_credito.DbType = DbType.String;

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "pcod_periodicidad";
                        pcod_periodicidad.Value = vReestructuracion.cod_periodicidad;
                        pcod_periodicidad.DbType = DbType.Int64;

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcod_oficina";
                        pcod_oficina.Value = vReestructuracion.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "pforma_pago";
                        pforma_pago.Value = vReestructuracion.forma_pago;
                        pforma_pago.DbType = DbType.String;

                        DbParameter pfecha_primerpago = cmdTransaccionFactory.CreateParameter();
                        pfecha_primerpago.ParameterName = "pfecha_primerpago";
                        pfecha_primerpago.Value = vReestructuracion.fecha_primer_pago;
                        pfecha_primerpago.DbType = DbType.DateTime;

                        DbParameter pmonto_nocapitaliza = cmdTransaccionFactory.CreateParameter();
                        pmonto_nocapitaliza.ParameterName = "pmonto_nocapitaliza";
                        pmonto_nocapitaliza.Value = vReestructuracion.monto_nocapitaliza;
                        pmonto_nocapitaliza.DbType = DbType.Double;

                        DbParameter pnum_cuo_nocap = cmdTransaccionFactory.CreateParameter();
                        pnum_cuo_nocap.ParameterName = "pnum_cuo_nocap";
                        pnum_cuo_nocap.Value = vReestructuracion.num_cuo_nocap;
                        pnum_cuo_nocap.DbType = DbType.Int64;

                        DbParameter pgarantias = cmdTransaccionFactory.CreateParameter();
                        pgarantias.ParameterName = "pgarantias";
                        if (vReestructuracion.bGarantias == true)
                            pgarantias.Value = 1;
                        else
                            pgarantias.Value = 0;
                        pgarantias.DbType = DbType.Int64;

                        DbParameter pcod_asesor = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor.ParameterName = "pcod_asesor";
                        pcod_asesor.Value = vReestructuracion.cod_asesor;
                        pcod_asesor.DbType = DbType.Int64;

                        DbParameter phonorarios = cmdTransaccionFactory.CreateParameter();
                        phonorarios.ParameterName = "phonorarios";
                        phonorarios.Value = vReestructuracion.honorarios;
                        phonorarios.DbType = DbType.Double;

                        DbParameter pdatacredito = cmdTransaccionFactory.CreateParameter();
                        pdatacredito.ParameterName = "pdatacredito";
                        pdatacredito.Value = vReestructuracion.datacredito;
                        pdatacredito.DbType = DbType.Double;

                        DbParameter pusuariores = cmdTransaccionFactory.CreateParameter();
                        pusuariores.ParameterName = "pusuario";
                        pusuariores.Value = pusuario.codusuario;
                        pusuariores.DbType = DbType.String;

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.InputOutput;
                        pnumero_radicacion.DbType = DbType.Int64;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        if (vReestructuracion.cod_empresa != 0) pcod_empresa.Value = vReestructuracion.cod_empresa; else pcod_empresa.Value = DBNull.Value; 
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;

                        DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
                        p_mensajeerror.ParameterName = "p_mensajeerror";
                        p_mensajeerror.Value = DBNull.Value;
                        p_mensajeerror.Direction = ParameterDirection.Output;
                        p_mensajeerror.Size = 400;

                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);
                        cmdTransaccionFactory.Parameters.Add(pcod_deudor);
                        cmdTransaccionFactory.Parameters.Add(pmonto_solicitado);
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);
                        cmdTransaccionFactory.Parameters.Add(pfecha_primerpago);
                        cmdTransaccionFactory.Parameters.Add(pmonto_nocapitaliza);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuo_nocap);
                        cmdTransaccionFactory.Parameters.Add(pgarantias);
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor);
                        cmdTransaccionFactory.Parameters.Add(phonorarios);
                        cmdTransaccionFactory.Parameters.Add(pdatacredito);
                        cmdTransaccionFactory.Parameters.Add(pusuariores);
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_REESTRUCTURAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        numero_radicacion = Convert.ToInt64(pnumero_radicacion.Value.ToString());

                        if (p_mensajeerror.Value != DBNull.Value)
                        {
                            throw new Exception(p_mensajeerror.Value.ToString());
                        }
                    }
                    catch (Exception ex)
                    {                        
                        error = ex.Message;
                    }
                }
            }

        }



        public void ActualizarAtributos(Int64 pnum_radic, Xpinn.FabricaCreditos.Entities.Atributos pAtributos, ref string error, Usuario pusuario)
        {
            error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pnum_radic;
                        pnumero_radicacion.Direction = ParameterDirection.InputOutput;
                        pnumero_radicacion.DbType = DbType.Int64;

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "pcod_atr";
                        pcod_atr.Value = pAtributos.cod_atr;
                        pcod_atr.DbType = DbType.Int64;

                        DbParameter pcalculo_atr = cmdTransaccionFactory.CreateParameter();
                        pcalculo_atr.ParameterName = "pcalculo_atr";
                        pcalculo_atr.Value = pAtributos.calculo_atr;
                        pcalculo_atr.DbType = DbType.Int64;

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "ptipo_historico";
                        if (pAtributos.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pAtributos.tipo_historico;
                        ptipo_historico.DbType = DbType.Int64;

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "pdesviacion";
                        pdesviacion.Value = pAtributos.desviacion;
                        pdesviacion.DbType = DbType.Double;

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "ptipo_tasa";
                        if (pAtributos.tipo_tasa == null)
                            ptipo_tasa.Value = DBNull.Value;
                        else                            
                            ptipo_tasa.Value = pAtributos.tipo_tasa;
                        ptipo_tasa.DbType = DbType.Int64;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.Value = pAtributos.tasa;
                        ptasa.DbType = DbType.Double;

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "pcobra_mora";
                        pcobra_mora.Value = pAtributos.cobra_mora;
                        pcobra_mora.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);
                        cmdTransaccionFactory.Parameters.Add(pcalculo_atr);
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ATRCRE_ACT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                }
            }

        }


        public void LiquidarCredito(Int64 pnum_radic, ref string error, Usuario pusuario)
        {
            error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "pnumero_radicacion";
                        pnumero_radicacion.Value = pnum_radic;
                        pnumero_radicacion.Direction = ParameterDirection.InputOutput;
                        pnumero_radicacion.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RECALCULARCRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                }
            }

        }



    }
}
