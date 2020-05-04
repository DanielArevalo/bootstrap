using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla LineasCredito
    /// </summary>
    public class LineasCreditoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla LineasCredito
        /// </summary>
        public LineasCreditoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla LineasCredito de la base de datos
        /// </summary>
        /// <param name="pLineasCredito">Entidad LineasCredito</param>
        /// <returns>Entidad LineasCredito creada</returns>
        public LineasCredito CrearLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "p_COD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pLineasCredito.cod_linea_credito;
                        pCOD_LINEA_CREDITO.Direction = ParameterDirection.InputOutput;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pLineasCredito.nombre;

                        DbParameter pTIPO_LINEA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LINEA.ParameterName = "p_TIPO_LINEA";
                        pTIPO_LINEA.Value = pLineasCredito.tipo_linea;

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "p_TIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.Value = pLineasCredito.tipo_liquidacion;

                        DbParameter pTIPO_CUPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CUPO.ParameterName = "p_TIPO_CUPO";
                        pTIPO_CUPO.Value = pLineasCredito.tipo_cupo;

                        DbParameter pRECOGE_SALDOS = cmdTransaccionFactory.CreateParameter();
                        pRECOGE_SALDOS.ParameterName = "p_RECOGE_SALDOS";
                        pRECOGE_SALDOS.Value = pLineasCredito.recoge_saldos;

                        DbParameter pCOBRA_MORA = cmdTransaccionFactory.CreateParameter();
                        pCOBRA_MORA.ParameterName = "p_COBRA_MORA";
                        pCOBRA_MORA.Value = pLineasCredito.cobra_mora;

                        DbParameter pTIPO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_REFINANCIA.ParameterName = "p_TIPO_REFINANCIA";
                        pTIPO_REFINANCIA.Value = pLineasCredito.tipo_refinancia;

                        DbParameter pMINIMO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pMINIMO_REFINANCIA.ParameterName = "p_MINIMO_REFINANCIA";
                        pMINIMO_REFINANCIA.Value = pLineasCredito.minimo_refinancia;

                        DbParameter pMAXIMO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pMAXIMO_REFINANCIA.ParameterName = "p_MAXIMO_REFINANCIA";
                        pMAXIMO_REFINANCIA.Value = pLineasCredito.maximo_refinancia;

                        DbParameter pMANEJA_PERGRACIA = cmdTransaccionFactory.CreateParameter();
                        pMANEJA_PERGRACIA.ParameterName = "p_MANEJA_PERGRACIA";
                        pMANEJA_PERGRACIA.Value = pLineasCredito.maneja_pergracia;

                        DbParameter pPERIODO_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pPERIODO_GRACIA.ParameterName = "p_PERIODO_GRACIA";
                        pPERIODO_GRACIA.Value = pLineasCredito.periodo_gracia;

                        DbParameter pTIPO_PERIODIC_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PERIODIC_GRACIA.ParameterName = "p_TIPO_PERIODIC_GRACIA";
                        pTIPO_PERIODIC_GRACIA.Value = pLineasCredito.tipo_periodic_gracia;

                        DbParameter pMODIFICA_DATOS = cmdTransaccionFactory.CreateParameter();
                        pMODIFICA_DATOS.ParameterName = "p_MODIFICA_DATOS";
                        pMODIFICA_DATOS.Value = pLineasCredito.modifica_datos;

                        DbParameter pMODIFICA_FECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pMODIFICA_FECHA_PAGO.ParameterName = "p_MODIFICA_FECHA_PAGO";
                        pMODIFICA_FECHA_PAGO.Value = pLineasCredito.modifica_fecha_pago;

                        DbParameter pGARANTIA_REQUERIDA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA_REQUERIDA.ParameterName = "p_GARANTIA_REQUERIDA";
                        pGARANTIA_REQUERIDA.Value = pLineasCredito.garantia_requerida;

                        DbParameter pTIPO_CAPITALIZACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CAPITALIZACION.ParameterName = "p_TIPO_CAPITALIZACION";
                        pTIPO_CAPITALIZACION.Value = pLineasCredito.tipo_capitalizacion;

                        DbParameter pCUOTAS_EXTRAS = cmdTransaccionFactory.CreateParameter();
                        pCUOTAS_EXTRAS.ParameterName = "p_CUOTAS_EXTRAS";
                        pCUOTAS_EXTRAS.Value = pLineasCredito.cuotas_extras;

                        DbParameter pCOD_CLASIFICA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CLASIFICA.ParameterName = "p_COD_CLASIFICA";
                        pCOD_CLASIFICA.Value = pLineasCredito.cod_clasifica;

                        DbParameter pNUMERO_CODEUDORES = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_CODEUDORES.ParameterName = "p_NUMERO_CODEUDORES";
                        pNUMERO_CODEUDORES.Value = pLineasCredito.numero_codeudores;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = "p_COD_MONEDA";
                        pCOD_MONEDA.Value = pLineasCredito.cod_moneda;

                        DbParameter pPORC_CORTO = cmdTransaccionFactory.CreateParameter();
                        pPORC_CORTO.ParameterName = "p_PORC_CORTO";
                        pPORC_CORTO.Value = pLineasCredito.porc_corto;

                        DbParameter pTIPO_AMORTIZA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_AMORTIZA.ParameterName = "p_TIPO_AMORTIZA";
                        pTIPO_AMORTIZA.Value = pLineasCredito.tipo_amortiza;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pLineasCredito.estado;

                        DbParameter pPLAZO_DIFERIR = cmdTransaccionFactory.CreateParameter();
                        pPLAZO_DIFERIR.ParameterName = "p_plazo_a_diferir";
                        pPLAZO_DIFERIR.Value = pLineasCredito.plazo_diferir;

                        DbParameter pAVANCES_APROB = cmdTransaccionFactory.CreateParameter();
                        pAVANCES_APROB.ParameterName = "p_aprobar_avances";
                        pAVANCES_APROB.Value = pLineasCredito.avances_aprob;

                        DbParameter pDESEM_AHORROS = cmdTransaccionFactory.CreateParameter();
                        pDESEM_AHORROS.ParameterName = "p_desembolsar_a_ahorros";
                        pDESEM_AHORROS.Value = pLineasCredito.desem_ahorros;

                        DbParameter pAPLICA_TERCERO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_TERCERO.ParameterName = "p_aplica_tercero";
                        pAPLICA_TERCERO.Value = pLineasCredito.aplica_tercero;

                        DbParameter pAPLICA_ASOCIADO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_ASOCIADO.ParameterName = "p_aplica_asociado";
                        pAPLICA_ASOCIADO.Value = pLineasCredito.aplica_asociado;

                        DbParameter pAPLICA_EMPLEADO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_EMPLEADO.ParameterName = "p_aplica_empleado";
                        pAPLICA_EMPLEADO.Value = pLineasCredito.aplica_empleado;

                        DbParameter pMANEJA_EXCEPCION = cmdTransaccionFactory.CreateParameter();
                        pMANEJA_EXCEPCION.ParameterName = "p_maneja_excepcion";
                        pMANEJA_EXCEPCION.Value = pLineasCredito.maneja_excepcion;

                        DbParameter pcuotas_intajuste = cmdTransaccionFactory.CreateParameter();
                        pcuotas_intajuste.ParameterName = "p_cuotas_intajuste";
                        pcuotas_intajuste.Value = pLineasCredito.cuota_intajuste;

                        DbParameter pcredito_gerencial = cmdTransaccionFactory.CreateParameter();
                        pcredito_gerencial.ParameterName = "p_credito_gerencial";
                        pcredito_gerencial.Value = pLineasCredito.credito_gerencial;

                        DbParameter porden_servicio = cmdTransaccionFactory.CreateParameter();
                        porden_servicio.ParameterName = "p_orden_servicio";
                        porden_servicio.Value = pLineasCredito.orden_servicio;

                        DbParameter p_educativo = cmdTransaccionFactory.CreateParameter();
                        p_educativo.ParameterName = "p_educativo";
                        p_educativo.Value = pLineasCredito.credito_educativo;

                        DbParameter p_credito_x_linea = cmdTransaccionFactory.CreateParameter();
                        p_credito_x_linea.ParameterName = "p_credito_x_linea";
                        if (pLineasCredito.credito_x_linea != 0) p_credito_x_linea.Value = pLineasCredito.credito_x_linea; else p_credito_x_linea.Value = DBNull.Value;

                        DbParameter p_maneja_auxilio = cmdTransaccionFactory.CreateParameter();
                        p_maneja_auxilio.ParameterName = "p_maneja_auxilio";
                        p_maneja_auxilio.Value = pLineasCredito.maneja_auxilio;

                        DbParameter p_prioridad = cmdTransaccionFactory.CreateParameter();
                        p_prioridad.ParameterName = "p_prioridad";
                        p_prioridad.Value = pLineasCredito.prioridad;

                        DbParameter p_diferir = cmdTransaccionFactory.CreateParameter();
                        p_diferir.ParameterName = "p_diferir";
                        p_diferir.Value = pLineasCredito.diferir;

                        DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                        p_fecha_corte.ParameterName = "p_fecha_corte";
                        p_fecha_corte.Value = pLineasCredito.fecha_corte;

                        DbParameter p_cantidad_comision = cmdTransaccionFactory.CreateParameter();
                        p_cantidad_comision.ParameterName = "p_cantidad_comision";
                        p_cantidad_comision.Value = pLineasCredito.cantidad_comision;

                        DbParameter p_valor_comision = cmdTransaccionFactory.CreateParameter();
                        p_valor_comision.ParameterName = "p_valor_comision";
                        p_valor_comision.Value = pLineasCredito.valor_comision;

                        DbParameter p_signo_comision = cmdTransaccionFactory.CreateParameter();
                        p_signo_comision.ParameterName = "p_signo_comision";
                        p_signo_comision.Value = pLineasCredito.signo_comision;

                        DbParameter p_aporte_garantia = cmdTransaccionFactory.CreateParameter();
                        p_aporte_garantia.ParameterName = "p_aporte_garantia";
                        p_aporte_garantia.Value = pLineasCredito.aporte_garantia;

                        DbParameter p_meses_gracia = cmdTransaccionFactory.CreateParameter();
                        p_meses_gracia.ParameterName = "p_meses_gracia";
                        p_meses_gracia.Value = pLineasCredito.meses_gracia;

                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LINEA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CUPO);
                        cmdTransaccionFactory.Parameters.Add(pRECOGE_SALDOS);
                        cmdTransaccionFactory.Parameters.Add(pCOBRA_MORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMINIMO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMAXIMO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMANEJA_PERGRACIA);
                        cmdTransaccionFactory.Parameters.Add(pPERIODO_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PERIODIC_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICA_DATOS);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICA_FECHA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA_REQUERIDA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CAPITALIZACION);
                        cmdTransaccionFactory.Parameters.Add(pCUOTAS_EXTRAS);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CLASIFICA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_CODEUDORES);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pPORC_CORTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_AMORTIZA);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        //credito rotativo
                        cmdTransaccionFactory.Parameters.Add(pPLAZO_DIFERIR);
                        cmdTransaccionFactory.Parameters.Add(pAVANCES_APROB);
                        cmdTransaccionFactory.Parameters.Add(pDESEM_AHORROS);
                        cmdTransaccionFactory.Parameters.Add(p_diferir);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_corte);
                        //Tipo persona aplica
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_TERCERO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_ASOCIADO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_EMPLEADO);
                        cmdTransaccionFactory.Parameters.Add(pMANEJA_EXCEPCION);
                        //Otros
                        cmdTransaccionFactory.Parameters.Add(pcuotas_intajuste);
                        cmdTransaccionFactory.Parameters.Add(pcredito_gerencial);
                        cmdTransaccionFactory.Parameters.Add(porden_servicio);
                        cmdTransaccionFactory.Parameters.Add(p_educativo);
                        cmdTransaccionFactory.Parameters.Add(p_credito_x_linea);
                        cmdTransaccionFactory.Parameters.Add(p_maneja_auxilio);
                        cmdTransaccionFactory.Parameters.Add(p_prioridad);
                        cmdTransaccionFactory.Parameters.Add(p_cantidad_comision);
                        cmdTransaccionFactory.Parameters.Add(p_valor_comision);
                        cmdTransaccionFactory.Parameters.Add(p_signo_comision);
                        cmdTransaccionFactory.Parameters.Add(p_aporte_garantia);
                        cmdTransaccionFactory.Parameters.Add(p_meses_gracia);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LINEA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pLineasCredito, "LineasCredito", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pLineasCredito.cod_linea_credito = Convert.ToString(pCOD_LINEA_CREDITO.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return pLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearLineasCredito", ex);
                        return null;
                    }
                }
            }
        }

        public bool ConsultarLineasCreditosActivasPorClasificacion(string cod_clasificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            bool tengoLineas = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        string sLinRes = "";

                        string sqlRES = "Select valor From general Where codigo = 430";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlRES;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["valor"] != DBNull.Value) sLinRes = Convert.ToString(resultado["valor"]);

                        string sql = @"Select Count(*) as contador from lineascredito 
                                        where cod_clasifica In (" + cod_clasificacion + @") And estado = 1 And tipo_linea != 2 
                                        And Nvl(lineascredito.Credito_Gerencial, 0) != 1
                                        And cod_linea_credito Not In (" + sLinRes + @") 
                                        And Nvl(lineascredito.educativo, 0) != 1 ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            int contador = 0;
                            if (resultado["contador"] != DBNull.Value) contador = Convert.ToInt32(resultado["contador"]);

                            if (contador > 0)
                            {
                                tengoLineas = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return tengoLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCreditosActivasPorClasificacion", ex);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla LineasCredito de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad LineasCredito modificada</returns>
        public LineasCredito ModificarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "p_COD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pLineasCredito.cod_linea_credito;

                        DbParameter pNOMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOMBRE.ParameterName = "p_NOMBRE";
                        pNOMBRE.Value = pLineasCredito.nombre;

                        DbParameter pTIPO_LINEA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LINEA.ParameterName = "p_TIPO_LINEA";
                        pTIPO_LINEA.Value = pLineasCredito.tipo_linea;

                        DbParameter pTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_LIQUIDACION.ParameterName = "p_TIPO_LIQUIDACION";
                        pTIPO_LIQUIDACION.Value = pLineasCredito.tipo_liquidacion;

                        DbParameter pTIPO_CUPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CUPO.ParameterName = "p_TIPO_CUPO";
                        pTIPO_CUPO.Value = pLineasCredito.tipo_cupo;

                        DbParameter pRECOGE_SALDOS = cmdTransaccionFactory.CreateParameter();
                        pRECOGE_SALDOS.ParameterName = "p_RECOGE_SALDOS";
                        pRECOGE_SALDOS.Value = pLineasCredito.recoge_saldos;

                        DbParameter pCOBRA_MORA = cmdTransaccionFactory.CreateParameter();
                        pCOBRA_MORA.ParameterName = "p_COBRA_MORA";
                        pCOBRA_MORA.Value = pLineasCredito.cobra_mora;

                        DbParameter pTIPO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_REFINANCIA.ParameterName = "p_TIPO_REFINANCIA";
                        pTIPO_REFINANCIA.Value = pLineasCredito.tipo_refinancia;

                        DbParameter pMINIMO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pMINIMO_REFINANCIA.ParameterName = "p_MINIMO_REFINANCIA";
                        pMINIMO_REFINANCIA.Value = pLineasCredito.minimo_refinancia;

                        DbParameter pMAXIMO_REFINANCIA = cmdTransaccionFactory.CreateParameter();
                        pMAXIMO_REFINANCIA.ParameterName = "p_MAXIMO_REFINANCIA";
                        pMAXIMO_REFINANCIA.Value = pLineasCredito.maximo_refinancia;

                        DbParameter pMANEJA_PERGRACIA = cmdTransaccionFactory.CreateParameter();
                        pMANEJA_PERGRACIA.ParameterName = "p_MANEJA_PERGRACIA";
                        pMANEJA_PERGRACIA.Value = pLineasCredito.maneja_pergracia;

                        DbParameter pPERIODO_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pPERIODO_GRACIA.ParameterName = "p_PERIODO_GRACIA";
                        pPERIODO_GRACIA.Value = pLineasCredito.periodo_gracia;

                        DbParameter pTIPO_PERIODIC_GRACIA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PERIODIC_GRACIA.ParameterName = "p_TIPO_PERIODIC_GRACIA";
                        pTIPO_PERIODIC_GRACIA.Value = pLineasCredito.tipo_periodic_gracia;

                        DbParameter pMODIFICA_DATOS = cmdTransaccionFactory.CreateParameter();
                        pMODIFICA_DATOS.ParameterName = "p_MODIFICA_DATOS";
                        pMODIFICA_DATOS.Value = pLineasCredito.modifica_datos;

                        DbParameter pMODIFICA_FECHA_PAGO = cmdTransaccionFactory.CreateParameter();
                        pMODIFICA_FECHA_PAGO.ParameterName = "p_MODIFICA_FECHA_PAGO";
                        pMODIFICA_FECHA_PAGO.Value = pLineasCredito.modifica_fecha_pago;

                        DbParameter pGARANTIA_REQUERIDA = cmdTransaccionFactory.CreateParameter();
                        pGARANTIA_REQUERIDA.ParameterName = "p_GARANTIA_REQUERIDA";
                        pGARANTIA_REQUERIDA.Value = pLineasCredito.garantia_requerida;

                        DbParameter pTIPO_CAPITALIZACION = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CAPITALIZACION.ParameterName = "p_TIPO_CAPITALIZACION";
                        pTIPO_CAPITALIZACION.Value = pLineasCredito.tipo_capitalizacion;

                        DbParameter pCUOTAS_EXTRAS = cmdTransaccionFactory.CreateParameter();
                        pCUOTAS_EXTRAS.ParameterName = "p_CUOTAS_EXTRAS";
                        pCUOTAS_EXTRAS.Value = pLineasCredito.cuotas_extras;

                        DbParameter pCOD_CLASIFICA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CLASIFICA.ParameterName = "p_COD_CLASIFICA";
                        pCOD_CLASIFICA.Value = pLineasCredito.cod_clasifica;

                        DbParameter pNUMERO_CODEUDORES = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_CODEUDORES.ParameterName = "p_NUMERO_CODEUDORES";
                        pNUMERO_CODEUDORES.Value = pLineasCredito.numero_codeudores;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = "p_COD_MONEDA";
                        pCOD_MONEDA.Value = pLineasCredito.cod_moneda;

                        DbParameter pPORC_CORTO = cmdTransaccionFactory.CreateParameter();
                        pPORC_CORTO.ParameterName = "p_PORC_CORTO";
                        pPORC_CORTO.Value = pLineasCredito.porc_corto;

                        DbParameter pTIPO_AMORTIZA = cmdTransaccionFactory.CreateParameter();
                        pTIPO_AMORTIZA.ParameterName = "p_TIPO_AMORTIZA";
                        pTIPO_AMORTIZA.Value = pLineasCredito.tipo_amortiza;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pLineasCredito.estado;

                        DbParameter pPLAZO_DIFERIR = cmdTransaccionFactory.CreateParameter();
                        pPLAZO_DIFERIR.ParameterName = "p_plazo_a_diferir";
                        pPLAZO_DIFERIR.Value = pLineasCredito.plazo_diferir;

                        DbParameter pAVANCES_APROB = cmdTransaccionFactory.CreateParameter();
                        pAVANCES_APROB.ParameterName = "p_aprobar_avances";
                        pAVANCES_APROB.Value = pLineasCredito.avances_aprob;

                        DbParameter pDESEM_AHORROS = cmdTransaccionFactory.CreateParameter();
                        pDESEM_AHORROS.ParameterName = "p_desembolsar_a_ahorros";
                        pDESEM_AHORROS.Value = pLineasCredito.desem_ahorros;

                        DbParameter pAPLICA_TERCERO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_TERCERO.ParameterName = "p_aplica_tercero";
                        pAPLICA_TERCERO.Value = pLineasCredito.aplica_tercero;

                        DbParameter pAPLICA_ASOCIADO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_ASOCIADO.ParameterName = "p_aplica_asociado";
                        pAPLICA_ASOCIADO.Value = pLineasCredito.aplica_asociado;

                        DbParameter pAPLICA_EMPLEADO = cmdTransaccionFactory.CreateParameter();
                        pAPLICA_EMPLEADO.ParameterName = "p_aplica_empleado";
                        pAPLICA_EMPLEADO.Value = pLineasCredito.aplica_empleado;

                        DbParameter pMANEJA_EXCEPCION = cmdTransaccionFactory.CreateParameter();
                        pMANEJA_EXCEPCION.ParameterName = "p_maneja_excepcion";
                        pMANEJA_EXCEPCION.Value = pLineasCredito.maneja_excepcion;

                        DbParameter pcuotas_intajuste = cmdTransaccionFactory.CreateParameter();
                        pcuotas_intajuste.ParameterName = "p_cuotas_intajuste";
                        pcuotas_intajuste.Value = pLineasCredito.cuota_intajuste;

                        DbParameter pcredito_gerencial = cmdTransaccionFactory.CreateParameter();
                        pcredito_gerencial.ParameterName = "p_credito_gerencial";
                        pcredito_gerencial.Value = pLineasCredito.credito_gerencial;

                        DbParameter porden_servicio = cmdTransaccionFactory.CreateParameter();
                        porden_servicio.ParameterName = "p_orden_servicio";
                        porden_servicio.Value = pLineasCredito.orden_servicio;

                        DbParameter p_educativo = cmdTransaccionFactory.CreateParameter();
                        p_educativo.ParameterName = "p_educativo";
                        p_educativo.Value = pLineasCredito.credito_educativo;

                        DbParameter p_credito_x_linea = cmdTransaccionFactory.CreateParameter();
                        p_credito_x_linea.ParameterName = "p_credito_x_linea";
                        if (pLineasCredito.credito_x_linea != 0) p_credito_x_linea.Value = pLineasCredito.credito_x_linea; else p_credito_x_linea.Value = DBNull.Value;

                        DbParameter p_maneja_auxilio = cmdTransaccionFactory.CreateParameter();
                        p_maneja_auxilio.ParameterName = "p_maneja_auxilio";
                        p_maneja_auxilio.Value = pLineasCredito.maneja_auxilio;

                        DbParameter p_prioridad = cmdTransaccionFactory.CreateParameter();
                        p_prioridad.ParameterName = "p_prioridad";
                        p_prioridad.Value = pLineasCredito.prioridad;

                        DbParameter p_diferir = cmdTransaccionFactory.CreateParameter();
                        p_diferir.ParameterName = "p_diferir";
                        p_diferir.Value = pLineasCredito.diferir;

                        DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                        p_fecha_corte.ParameterName = "p_fecha_corte";
                        p_fecha_corte.Value = pLineasCredito.fecha_corte;

                        DbParameter p_cantidad_comision = cmdTransaccionFactory.CreateParameter();
                        p_cantidad_comision.ParameterName = "p_cantidad_comision";
                        p_cantidad_comision.Value = pLineasCredito.cantidad_comision;

                        DbParameter p_valor_comision = cmdTransaccionFactory.CreateParameter();
                        p_valor_comision.ParameterName = "p_valor_comision";
                        p_valor_comision.Value = pLineasCredito.valor_comision;

                        DbParameter p_signo_comision = cmdTransaccionFactory.CreateParameter();
                        p_signo_comision.ParameterName = "p_signo_comision";
                        p_signo_comision.Value = pLineasCredito.signo_comision;

                        DbParameter p_aporte_garantia = cmdTransaccionFactory.CreateParameter();
                        p_aporte_garantia.ParameterName = "p_aporte_garantia";
                        p_aporte_garantia.Value = pLineasCredito.aporte_garantia;

                        DbParameter p_meses_gracia = cmdTransaccionFactory.CreateParameter();
                        p_meses_gracia.ParameterName = "p_meses_gracia";
                        p_meses_gracia.Value = pLineasCredito.meses_gracia;


                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRE);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LINEA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CUPO);
                        cmdTransaccionFactory.Parameters.Add(pRECOGE_SALDOS);
                        cmdTransaccionFactory.Parameters.Add(pCOBRA_MORA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMINIMO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMAXIMO_REFINANCIA);
                        cmdTransaccionFactory.Parameters.Add(pMANEJA_PERGRACIA);
                        cmdTransaccionFactory.Parameters.Add(pPERIODO_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PERIODIC_GRACIA);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICA_DATOS);
                        cmdTransaccionFactory.Parameters.Add(pMODIFICA_FECHA_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pGARANTIA_REQUERIDA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CAPITALIZACION);
                        cmdTransaccionFactory.Parameters.Add(pCUOTAS_EXTRAS);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CLASIFICA);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_CODEUDORES);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pPORC_CORTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_AMORTIZA);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO_DIFERIR);
                        cmdTransaccionFactory.Parameters.Add(pAVANCES_APROB);
                        cmdTransaccionFactory.Parameters.Add(pDESEM_AHORROS);
                        //Tipo persona aplica
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_TERCERO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_ASOCIADO);
                        cmdTransaccionFactory.Parameters.Add(pAPLICA_EMPLEADO);
                        cmdTransaccionFactory.Parameters.Add(pMANEJA_EXCEPCION);
                        //Otros
                        cmdTransaccionFactory.Parameters.Add(pcuotas_intajuste);
                        cmdTransaccionFactory.Parameters.Add(pcredito_gerencial);
                        cmdTransaccionFactory.Parameters.Add(porden_servicio);
                        cmdTransaccionFactory.Parameters.Add(p_educativo);
                        cmdTransaccionFactory.Parameters.Add(p_credito_x_linea);
                        cmdTransaccionFactory.Parameters.Add(p_maneja_auxilio);
                        cmdTransaccionFactory.Parameters.Add(p_prioridad);
                        cmdTransaccionFactory.Parameters.Add(p_diferir);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_corte);
                        cmdTransaccionFactory.Parameters.Add(p_cantidad_comision);
                        cmdTransaccionFactory.Parameters.Add(p_valor_comision);
                        cmdTransaccionFactory.Parameters.Add(p_signo_comision);
                        cmdTransaccionFactory.Parameters.Add(p_aporte_garantia);
                        cmdTransaccionFactory.Parameters.Add(p_meses_gracia);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LINEA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pLineasCredito, "LineasCredito", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ModificarLineasCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla LineasCredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de LineasCredito</param>
        public void EliminarLineasCredito(string pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineasCredito pLineasCredito = new LineasCredito();

                        if (pUsuario.programaGeneraLog)
                            pLineasCredito = ConsultarLineasCredito(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "p_COD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_LINEA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pLineasCredito, "LineasCredito", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarLineasCredito", ex);
                    }
                }
            }
        }


        public LineasCredito modificardeducciones(LineasCredito entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_CREDITO.ParameterName = "PCOD_LINEA_CREDITO";
                        PCOD_LINEA_CREDITO.Value = entidad.cod_linea_credito;

                        DbParameter PCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ATR.ParameterName = "PCOD_ATR";
                        PCOD_ATR.Value = entidad.cod_atr;

                        DbParameter PTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        PTIPO_LIQUIDACION.ParameterName = "PTIPO_LIQUIDACION";
                        PTIPO_LIQUIDACION.Value = entidad.tipoliquidacion;

                        DbParameter PVALOR = cmdTransaccionFactory.CreateParameter();
                        PVALOR.ParameterName = "PVALOR";
                        PVALOR.Value = entidad.valor1;


                        DbParameter PCOBRA_MORA = cmdTransaccionFactory.CreateParameter();
                        PCOBRA_MORA.ParameterName = "PCOBRA_MORA";
                        PCOBRA_MORA.Value = entidad.cobra_mora;

                        DbParameter PNUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUOTAS.ParameterName = "PNUMERO_CUOTAS";
                        PNUMERO_CUOTAS.Value = entidad.numero_cuotas;

                        DbParameter PFORMA_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_DESCUENTO.ParameterName = "PFORMA_DESCUENTO";
                        PFORMA_DESCUENTO.Value = entidad.Formadescuento;

                        DbParameter PTIPO_IMPUESTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_IMPUESTO.ParameterName = "PTIPO_IMPUESTO";
                        PTIPO_IMPUESTO.Value = entidad.tipoimpuesto;


                        DbParameter PTIPO_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_DESCUENTO.ParameterName = "PTIPO_DESCUENTO";
                        PTIPO_DESCUENTO.Value = entidad.tiposdescuento;

                        DbParameter PMODIFICA = cmdTransaccionFactory.CreateParameter();
                        PMODIFICA.ParameterName = "PMODIFICA";
                        PMODIFICA.Value = entidad.modifica;


                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(PCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(PVALOR);
                        cmdTransaccionFactory.Parameters.Add(PCOBRA_MORA);
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUOTAS);
                        cmdTransaccionFactory.Parameters.Add(PFORMA_DESCUENTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_IMPUESTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_DESCUENTO);
                        cmdTransaccionFactory.Parameters.Add(PMODIFICA);
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESCUENTOLIN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);


                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "modificardeducciones", ex);

                    }
                    return entidad;
                }


            }
        }



        public void Creardeducciones(LineasCredito entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        PCOD_LINEA_CREDITO.ParameterName = "PCOD_LINEA_CREDITO";
                        PCOD_LINEA_CREDITO.Value = entidad.cod_linea_credito;

                        DbParameter PCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ATR.ParameterName = "PCOD_ATR";
                        PCOD_ATR.Value = entidad.cod_atr;

                        DbParameter PTIPO_LIQUIDACION = cmdTransaccionFactory.CreateParameter();
                        PTIPO_LIQUIDACION.ParameterName = "PTIPO_LIQUIDACION";
                        PTIPO_LIQUIDACION.Value = entidad.tipoliquidacion;

                        DbParameter PVALOR = cmdTransaccionFactory.CreateParameter();
                        PVALOR.ParameterName = "PVALOR";
                        PVALOR.Value = entidad.valor1;


                        DbParameter PCOBRA_MORA = cmdTransaccionFactory.CreateParameter();
                        PCOBRA_MORA.ParameterName = "PCOBRA_MORA";
                        PCOBRA_MORA.Value = entidad.cobra_mora;

                        DbParameter PNUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        PNUMERO_CUOTAS.ParameterName = "PNUMERO_CUOTAS";
                        PNUMERO_CUOTAS.Value = entidad.numero_cuotas;

                        DbParameter PFORMA_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_DESCUENTO.ParameterName = "PFORMA_DESCUENTO";
                        PFORMA_DESCUENTO.Value = entidad.Formadescuento;

                        DbParameter PTIPO_IMPUESTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_IMPUESTO.ParameterName = "PTIPO_IMPUESTO";
                        PTIPO_IMPUESTO.Value = entidad.tipoimpuesto;


                        DbParameter PTIPO_DESCUENTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_DESCUENTO.ParameterName = "PTIPO_DESCUENTO";
                        PTIPO_DESCUENTO.Value = entidad.tiposdescuento;

                        DbParameter PMODIFICA = cmdTransaccionFactory.CreateParameter();
                        PMODIFICA.ParameterName = "PMODIFICA";
                        PMODIFICA.Value = entidad.modifica;


                        cmdTransaccionFactory.Parameters.Add(PCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(PCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_LIQUIDACION);
                        cmdTransaccionFactory.Parameters.Add(PVALOR);
                        cmdTransaccionFactory.Parameters.Add(PCOBRA_MORA);
                        cmdTransaccionFactory.Parameters.Add(PNUMERO_CUOTAS);
                        cmdTransaccionFactory.Parameters.Add(PFORMA_DESCUENTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_IMPUESTO);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_DESCUENTO);
                        cmdTransaccionFactory.Parameters.Add(PMODIFICA);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DEDUC";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "Creardeducciones", ex);
                    }
                }
            }
        }


        public LineasCredito CrearRangosAtributos(LineasCredito entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = entidad.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_rango_atr.ParameterName = "p_cod_rango_atr";
                        pcod_rango_atr.Value = entidad.cod_rango_atr;
                        pcod_rango_atr.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango_atr);

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = entidad.nombre;
                        p_nombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_nombre);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RAN_ATR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        entidad.cod_rango_atr = Convert.ToInt64(pcod_rango_atr.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearRangosAtributos", ex);
                    }
                    return entidad;
                }
            }
        }

        public LineasCredito ModificarRangosAtributos(LineasCredito entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "UPDATE RANGOSATRIBUTOS SET nombre = '" + entidad.nombre + "'"
                                   + " WHERE cod_linea_credito = '" + entidad.cod_linea_credito + "' AND cod_rango_atr = " + entidad.cod_rango_atr;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ModificarRangosAtributos", ex);
                    }
                    return entidad;
                }
            }
        }


        public RangosTopes CrearRangosTopes(RangosTopes pTopes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtope = cmdTransaccionFactory.CreateParameter();
                        pidtope.ParameterName = "p_idtope";
                        pidtope.Value = pTopes.idtope;
                        pidtope.Direction = ParameterDirection.Output;
                        pidtope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtope);

                        DbParameter pcod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_rango_atr.ParameterName = "p_cod_rango_atr";
                        pcod_rango_atr.Value = pTopes.cod_rango_atr;
                        pcod_rango_atr.Direction = ParameterDirection.Input;
                        pcod_rango_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango_atr);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pTopes.minimo != null) pminimo.Value = pTopes.minimo; else pminimo.Value = DBNull.Value;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pTopes.maximo != null) pmaximo.Value = pTopes.maximo; else pmaximo.Value = DBNull.Value;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pTopes.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pTopes.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RANGOSTOPE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearRangosTopes", ex);
                        return null;
                    }
                }
            }
        }

        public RangosTopes ModificarRangosTopes(RangosTopes pTopes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtope = cmdTransaccionFactory.CreateParameter();
                        pidtope.ParameterName = "p_idtope";
                        pidtope.Value = pTopes.idtope;
                        pidtope.Direction = ParameterDirection.Input;
                        pidtope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtope);

                        DbParameter pcod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_rango_atr.ParameterName = "p_cod_rango_atr";
                        pcod_rango_atr.Value = pTopes.cod_rango_atr;
                        pcod_rango_atr.Direction = ParameterDirection.Input;
                        pcod_rango_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango_atr);

                        DbParameter pminimo = cmdTransaccionFactory.CreateParameter();
                        pminimo.ParameterName = "p_minimo";
                        if (pTopes.minimo != null) pminimo.Value = pTopes.minimo; else pminimo.Value = DBNull.Value;
                        pminimo.Direction = ParameterDirection.Input;
                        pminimo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pminimo);

                        DbParameter pmaximo = cmdTransaccionFactory.CreateParameter();
                        pmaximo.ParameterName = "p_maximo";
                        if (pTopes.maximo != null) pmaximo.Value = pTopes.maximo; else pmaximo.Value = DBNull.Value;
                        pmaximo.Direction = ParameterDirection.Input;
                        pmaximo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmaximo);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pTopes.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pTopes.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RANGOSTOPE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ModificarRangosTopes", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla LineasCredito de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla LineasCredito</param>
        /// <returns>Entidad LineasCredito consultado</returns>
        public LineasCredito ConsultarLineasCredito(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM LINEASCREDITO WHERE COD_LINEA_CREDITO = '" + pId.ToString() + "'";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt64(resultado["TIPO_CUPO"]);
                            if (resultado["RECOGE_SALDOS"] != DBNull.Value) entidad.recoge_saldos = Convert.ToInt64(resultado["RECOGE_SALDOS"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt64(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToDecimal(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToDecimal(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MANEJA_PERGRACIA"] != DBNull.Value) entidad.maneja_pergracia = Convert.ToString(resultado["MANEJA_PERGRACIA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["TIPO_PERIODIC_GRACIA"] != DBNull.Value) entidad.tipo_periodic_gracia = Convert.ToString(resultado["TIPO_PERIODIC_GRACIA"]);
                            if (resultado["MODIFICA_DATOS"] != DBNull.Value) entidad.modifica_datos = Convert.ToString(resultado["MODIFICA_DATOS"]);
                            if (resultado["MODIFICA_FECHA_PAGO"] != DBNull.Value) entidad.modifica_fecha_pago = Convert.ToString(resultado["MODIFICA_FECHA_PAGO"]);
                            if (resultado["GARANTIA_REQUERIDA"] != DBNull.Value) entidad.garantia_requerida = Convert.ToString(resultado["GARANTIA_REQUERIDA"]);
                            if (resultado["TIPO_CAPITALIZACION"] != DBNull.Value) entidad.tipo_capitalizacion = Convert.ToInt64(resultado["TIPO_CAPITALIZACION"]);
                            if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.cuotas_extras = Convert.ToInt64(resultado["CUOTAS_EXTRAS"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) entidad.numero_codeudores = Convert.ToInt64(resultado["NUMERO_CODEUDORES"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["PORC_CORTO"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_CORTO"]);
                            if (resultado["TIPO_AMORTIZA"] != DBNull.Value) entidad.tipo_amortiza = Convert.ToInt64(resultado["TIPO_AMORTIZA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.avances_aprob = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) entidad.desem_ahorros = Convert.ToInt64(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["APLICA_TERCERO"] != DBNull.Value) entidad.aplica_tercero = Convert.ToInt64(resultado["APLICA_TERCERO"]);
                            if (resultado["APLICA_ASOCIADO"] != DBNull.Value) entidad.aplica_asociado = Convert.ToInt64(resultado["APLICA_ASOCIADO"]);
                            if (resultado["APLICA_EMPLEADO"] != DBNull.Value) entidad.aplica_empleado = Convert.ToInt64(resultado["APLICA_EMPLEADO"]);
                            if (resultado["MANEJA_EXCEPCION"] != DBNull.Value) entidad.maneja_excepcion = Convert.ToString(resultado["MANEJA_EXCEPCION"]);
                            if (resultado["CUOTAS_INTAJUSTE"] != DBNull.Value) entidad.cuota_intajuste = Convert.ToInt32(resultado["CUOTAS_INTAJUSTE"]);
                            if (resultado["CREDITO_GERENCIAL"] != DBNull.Value) entidad.credito_gerencial = Convert.ToInt32(resultado["CREDITO_GERENCIAL"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.credito_educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["CREDITO_X_LINEA"] != DBNull.Value) entidad.credito_x_linea = Convert.ToInt32(resultado["CREDITO_X_LINEA"]);
                            if (resultado["MANEJA_AUXILIO"] != DBNull.Value) entidad.maneja_auxilio = Convert.ToInt32(resultado["MANEJA_AUXILIO"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt32(resultado["PRIORIDAD"]);
                            if (resultado["DIFERIR"] != DBNull.Value) entidad.diferir = Convert.ToInt32(resultado["DIFERIR"]);
                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.fecha_corte = Convert.ToDateTime(resultado["FECHA_CORTE"]);
                            if (resultado["CANTIDAD_COMISION"] != DBNull.Value) entidad.cantidad_comision = Convert.ToInt64(resultado["CANTIDAD_COMISION"]);
                            if (resultado["VALOR_COMISION"] != DBNull.Value) entidad.valor_comision = Convert.ToInt64(resultado["VALOR_COMISION"]);
                            if (resultado["SIGNO_COMISION"] != DBNull.Value) entidad.signo_comision = Convert.ToInt64(resultado["SIGNO_COMISION"]);
                            if (resultado["APORTE_GARANTIA"] != DBNull.Value) entidad.aporte_garantia = Convert.ToInt32(resultado["APORTE_GARANTIA"]);
                            if (resultado["MESES_GRACIA"] != DBNull.Value) entidad.meses_gracia = Convert.ToString(resultado["MESES_GRACIA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCredito", ex);
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public LineasCredito ConsultarTasaInteresLineaCredito(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineasCredito linea = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.tasa, a.tipo_tasa, t.nombre
                                        From atributoslinea a Left Join tipotasa t on a.tipo_tasa = t.cod_tipo_tasa
                                        Where cod_linea_credito = '" + pId + "' and cod_atr = AtrCorriente()";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TASA"] != DBNull.Value) linea.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) linea.tipotasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) linea.descripcion_tipo_tasa = Convert.ToString(resultado["NOMBRE"]);
                        }

                        return linea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarTasaInteresLineaCredito", ex);
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public Decimal obtenerTasaInteresEspecifica(string cod_linea_credito, int plazo, Usuario pUsuario)
        {
            DbDataReader resultado;
            Decimal tasaEspecifica = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select L.TASA
                                       From RANGOSATRIBUTOS A
                                       Inner Join RANGOSTOPES T On A.COD_RANGO_ATR = T.COD_RANGO_ATR
                                       Inner Join ATRIBUTOSLINEA L On A.COD_RANGO_ATR = L.COD_RANGO_ATR
                                       Inner Join TIPOTASA S On L.TIPO_TASA = S.COD_TIPO_TASA
                                       Where A.COD_LINEA_CREDITO = '" + cod_linea_credito + @"' 
                                       And (T.TIPO_TOPE = 2 And L.TASA Is Not Null And T.MINIMO <= " + plazo + " AND T.MAXIMO >= " + plazo + ")";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TASA"] != DBNull.Value) tasaEspecifica = Convert.ToDecimal(resultado["TASA"]);
                        }
                        return tasaEspecifica;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarTasaInteresLineaCredito", ex);
                        return 0;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }


        public List<LineasCredito> ListarLineasCreditoTasaInteres(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstLineas = null;
            string sql = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Obtener los parámetros pasados en el filtro
                        decimal tasaReal = 0;
                        string[] datosTasa = pFiltro.Split('|');
                        string cod_linea_credito, plazo, monto, cod_persona;
                        cod_linea_credito = "";
                        if (datosTasa.Length >= 5)
                        {
                            cod_linea_credito = datosTasa[1].Trim();
                            plazo = datosTasa[2].Trim();
                            monto = datosTasa[3].Trim();
                            cod_persona = datosTasa[4].Trim();
                            tasaReal = obtenerTasaInteresEspecifica(cod_linea_credito, Int32.Parse(plazo), pUsuario);
                            pFiltro = datosTasa[5].Trim();
                        }

                        sql = @"Select Distinct l.cod_linea_credito, l.nombre, l.tipo_liquidacion, max(a.tasa) tasa, a.tipo_tasa, t.nombre as descripcion_tipotasa, l.cuotas_extras, d.cod_linea_credito AFIANCOL,L.TIPO_LINEA
                                        From lineascredito l inner Join atributoslinea a on l.cod_linea_credito = a.cod_linea_credito
                                        Left Join tipotasa t on a.tipo_tasa = t.cod_tipo_tasa 
                                        Left Join Descuentoslinea d on d.cod_linea_credito = l.cod_linea_credito and d.cod_atr = 60
                                        Where a.cod_atr = AtrCorriente() " + pFiltro + @" 
                                        Group by l.cod_linea_credito, l.nombre, l.tipo_liquidacion, a.tipo_tasa, t.nombre, l.cuotas_extras, d.cod_linea_credito,L.TIPO_LINEA
                                        Order by 2";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            lstLineas = new List<LineasCredito>();
                            LineasCredito entidad;
                            while (resultado.Read())
                            {
                                entidad = new LineasCredito();
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToString(resultado["TIPO_LIQUIDACION"]);
                                if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                                if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                                if (resultado["DESCRIPCION_TIPOTASA"] != DBNull.Value) entidad.descripcion_tipo_tasa = Convert.ToString(resultado["DESCRIPCION_TIPOTASA"]);
                                if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.Cuotas_Extras = Convert.ToDouble(resultado["CUOTAS_EXTRAS"]);
                                if (resultado["AFIANCOL"] != DBNull.Value) entidad.afiancol = Convert.ToInt32(resultado["AFIANCOL"]);
                                if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt32(resultado["TIPO_LINEA"]);
                                lstLineas.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        // Asignar la tasa según el rango a la línea
                        if (!string.IsNullOrWhiteSpace(cod_linea_credito))
                        {
                            LineasCredito pLineaSeleccionada = lstLineas.Where(x => x.cod_linea_credito == cod_linea_credito).FirstOrDefault();
                            if (pLineaSeleccionada != null)
                            {
                                pLineaSeleccionada.tasa = tasaReal;
                            }
                        }
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarLineasCreditoTasaInteres" + sql, ex);
                        return null;
                    }
                }
            }
        }

        public long? ConsultarNumeroCodeudoresXLinea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            long? numeroCodeudores = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT NUMERO_CODEUDORES FROM LINEASCREDITO WHERE COD_LINEA_CREDITO = " + pId;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) numeroCodeudores = Convert.ToInt64(resultado["NUMERO_CODEUDORES"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroCodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarNumeroCodeudoresXLinea", ex);
                        return null;
                    }
                }
            }
        }

        public LineasCredito ConsultarLineasCreditoRotativo(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  LINEASCREDITO WHERE TIPO_LINEA = " + pId.ToString();

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt64(resultado["TIPO_CUPO"]);
                            if (resultado["RECOGE_SALDOS"] != DBNull.Value) entidad.recoge_saldos = Convert.ToInt64(resultado["RECOGE_SALDOS"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt64(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToDecimal(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToDecimal(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MANEJA_PERGRACIA"] != DBNull.Value) entidad.maneja_pergracia = Convert.ToString(resultado["MANEJA_PERGRACIA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["TIPO_PERIODIC_GRACIA"] != DBNull.Value) entidad.tipo_periodic_gracia = Convert.ToString(resultado["TIPO_PERIODIC_GRACIA"]);
                            if (resultado["MODIFICA_DATOS"] != DBNull.Value) entidad.modifica_datos = Convert.ToString(resultado["MODIFICA_DATOS"]);
                            if (resultado["MODIFICA_FECHA_PAGO"] != DBNull.Value) entidad.modifica_fecha_pago = Convert.ToString(resultado["MODIFICA_FECHA_PAGO"]);
                            if (resultado["GARANTIA_REQUERIDA"] != DBNull.Value) entidad.garantia_requerida = Convert.ToString(resultado["GARANTIA_REQUERIDA"]);
                            if (resultado["TIPO_CAPITALIZACION"] != DBNull.Value) entidad.tipo_capitalizacion = Convert.ToInt64(resultado["TIPO_CAPITALIZACION"]);
                            if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.cuotas_extras = Convert.ToInt64(resultado["CUOTAS_EXTRAS"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) entidad.numero_codeudores = Convert.ToInt64(resultado["NUMERO_CODEUDORES"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["PORC_CORTO"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_CORTO"]);
                            if (resultado["TIPO_AMORTIZA"] != DBNull.Value) entidad.tipo_amortiza = Convert.ToInt64(resultado["TIPO_AMORTIZA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.avances_aprob = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) entidad.desem_ahorros = Convert.ToInt64(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["APLICA_TERCERO"] != DBNull.Value) entidad.aplica_tercero = Convert.ToInt64(resultado["APLICA_TERCERO"]);
                            if (resultado["APLICA_ASOCIADO"] != DBNull.Value) entidad.aplica_asociado = Convert.ToInt64(resultado["APLICA_ASOCIADO"]);
                            if (resultado["APLICA_EMPLEADO"] != DBNull.Value) entidad.aplica_empleado = Convert.ToInt64(resultado["APLICA_EMPLEADO"]);
                            if (resultado["MANEJA_EXCEPCION"] != DBNull.Value) entidad.maneja_excepcion = Convert.ToString(resultado["MANEJA_EXCEPCION"]);
                            if (resultado["CUOTAS_INTAJUSTE"] != DBNull.Value) entidad.cuota_intajuste = Convert.ToInt32(resultado["CUOTAS_INTAJUSTE"]);
                            if (resultado["CREDITO_GERENCIAL"] != DBNull.Value) entidad.credito_gerencial = Convert.ToInt32(resultado["CREDITO_GERENCIAL"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.credito_educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["CREDITO_X_LINEA"] != DBNull.Value) entidad.credito_x_linea = Convert.ToInt32(resultado["CREDITO_X_LINEA"]);
                            if (resultado["MANEJA_AUXILIO"] != DBNull.Value) entidad.maneja_auxilio = Convert.ToInt32(resultado["MANEJA_AUXILIO"]);
                            if (resultado["CANTIDAD_COMISION"] != DBNull.Value) entidad.cantidad_comision = Convert.ToInt64(resultado["CANTIDAD_COMISION"]);
                            if (resultado["VALOR_COMISION"] != DBNull.Value) entidad.valor_comision = Convert.ToInt64(resultado["VALOR_COMISION"]);
                            if (resultado["SIGNO_COMISION"] != DBNull.Value) entidad.signo_comision = Convert.ToInt64(resultado["SIGNO_COMISION"]);

                        }
                        else
                        {

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCreditoRotativo", ex);
                        return null;
                    }
                }
            }
        }



        public List<LineasCredito> ddlliquidacion(Usuario pUsuario)
        {
            DbDataReader resultado;

            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select tipo_liquidacion,descripcion from tipoliquidacion ";


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["tipo_liquidacion"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["tipo_liquidacion"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["descripcion"]);


                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ddlliquidacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para obtener una listad de atributos
        /// </summary>
        /// <returns></returns>
        public List<LineasCredito> ddlatributo(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select cod_atr, nombre from atributos";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);


                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ddlatributo", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ddlimpuestos(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_tipo_impuesto,nombre_impuesto from  tipoimpuesto";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_tipo_impuesto"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_tipo_impuesto"]);
                            if (resultado["nombre_impuesto"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre_impuesto"]);


                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ddlimpuestos", ex);
                        return null;
                    }
                }
            }
        }

        public List<RangosTopes> ConsultarLineasCreditopes(string codigo, string atr, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RangosTopes> lstLineasCredito = new List<RangosTopes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select rnago.idtope, tipo.tipo_tope, tipo.descripcion, rnago.minimo, rnago.maximo 
                                          from tipo_tope tipo
                                          inner join rangostopes  rnago
                                          on tipo.tipo_tope=rnago.tipo_tope
                                      where rnago.cod_linea_credito = " + codigo + "  and rnago.cod_rango_atr =" + atr;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            RangosTopes entidad = new RangosTopes();
                            if (resultado["idtope"] != DBNull.Value) entidad.idtope = Convert.ToInt64(resultado["idtope"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["minimo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["maximo"]);
                            if (resultado["tipo_tope"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["tipo_tope"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCreditopes", ex);
                        return null;
                    }
                }
            }
        }
        public RangosTopes ConsultarTopestodos(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            RangosTopes entidad = new RangosTopes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select  rnago.idtope, tipo.tipo_tope, tipo.descripcion, rnago.minimo, rnago.maximo 
                                        From tipo_tope tipo inner join rangostopes rnago on tipo.tipo_tope = rnago.tipo_tope
                                        Where rnago.cod_linea_credito = '" + codigo + "' ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            //LineasCredito entidad = new LineasCredito();
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["minimo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["maximo"]);
                            if (resultado["tipo_tope"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["tipo_tope"]);
                            if (resultado["idtope"] != DBNull.Value) entidad.idtope = Convert.ToInt64(resultado["idtope"]);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarTopestodos", ex);
                        return null;
                    }
                }
            }
        }

        public RangosTopes ConsultarCreditoTopes(int codigo, int tope, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            RangosTopes entidad = new RangosTopes();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  rnago.idtope,tipo.tipo_tope,
                                          tipo.descripcion,
                                          rnago.minimo,
                                          rnago.maximo 
                                          from tipo_tope tipo
                                          inner join rangostopes  rnago
                                          on tipo.tipo_tope=rnago.tipo_tope
                                      where rnago.cod_linea_credito = " + codigo + " and rnago.idtope=" + tope;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            //LineasCredito entidad = new LineasCredito();
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["minimo"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["maximo"]);
                            if (resultado["tipo_tope"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["tipo_tope"]);
                            if (resultado["idtope"] != DBNull.Value) entidad.idtope = Convert.ToInt64(resultado["idtope"]);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarCreditoTopes", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ConsultarLineasCrediatributo(string codigo, int rango, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select atrilin.cod_linea_credito,
                                          atrilin.cod_atr,
                                          atrilin.cod_rango_atr,  
                                          atr.nombre as descripcion,  
                                          atrilin.calculo_atr as FormaCalculo,
                                          atrilin.tasa,tas.cod_tipo_tasa  as tipotasa,
                                          hist.tipo_historico as tipohistorico,
                                          atrilin.desviacion,
                                          atrilin.cobra_mora 
                                       from atributoslinea atrilin
                                       left join atributos atr on atr.cod_atr = atrilin.cod_atr
                                       left join tipotasa tas on atrilin.tipo_tasa = tas.cod_tipo_tasa
                                       left join tipotasahist hist on atrilin.tipo_historico = hist.tipo_historico  Where atrilin.cod_linea_credito = " + codigo + " And atrilin.cod_rango_atr = " + rango;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCrediatributo", ex);
                        return null;
                    }
                }
            }
        }
        public List<LineasCredito> ConsultarLineasCrediatributo2(string codigo, Usuario pUsuario, string numradic)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //Consulta modificada para cargar solamente los atributos correspondientes al crédito
                        string sql = @"select c.cod_linea_credito,
                                        atricred.cod_atr,
                                        atr.nombre as descripcion,  
                                        atricred.calculo_atr as FormaCalculo,
                                        atricred.tasa,
                                        tas.Nombre  as tipotasa,
                                        hist.tipo_historico as tipohistorico,
                                        atricred.desviacion,
                                        atricred.cobra_mora 
                                        from atributos atr 
                                        inner join atributoscredito atricred on atr.cod_atr = atricred.cod_atr
                                        inner join tipotasa tas on atricred.tipo_tasa = tas.cod_tipo_tasa
                                        left join tipotasahist hist on atricred.tipo_historico = hist.tipo_historico
                                        inner join credito c on c.numero_radicacion = atricred.numero_radicacion
                                        where c.numero_radicacion = " + numradic + " and c.cod_linea_credito = " + codigo + "";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasaNom = Convert.ToString(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCrediatributo2", ex);
                        return null;
                    }
                }
            }
        }

        public LineasCredito Consultar_atri_cred_rango(int codigo, int codatr, int rango, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select atrilin.cod_linea_credito,
                                          atrilin.cod_atr,
                                          atrilin.cod_rango_atr,  
                                          atr.nombre as descripcion,  
                                          atrilin.calculo_atr as FormaCalculo,
                                          atrilin.tasa,tas.cod_tipo_tasa  as tipotasa,
                                          hist.tipo_historico as tipohistorico,
                                          atrilin.desviacion,
                                          atrilin.cobra_mora 
                                       from atributoslinea atrilin
                                       left join atributos atr on atr.cod_atr = atrilin.cod_atr
                                       left join tipotasa tas on atrilin.tipo_tasa = tas.cod_tipo_tasa
                                       left join tipotasahist hist on atrilin.tipo_historico = hist.tipo_historico  Where atrilin.cod_linea_credito = " + codigo + " And atrilin.cod_rango_atr = " + rango + " And atrilin.cod_atr = " + codatr;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "Consultar_atri_cred_rango", ex);
                        return null;
                    }
                }
            }
        }

        public LineasCredito Consultar_atributos_credito(int codigo, int rango, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select atrilin.cod_linea_credito,
                                          atrilin.cod_atr,
                                          atrilin.cod_rango_atr,  
                                          atr.nombre as descripcion,  
                                          atrilin.calculo_atr as FormaCalculo,
                                          atrilin.tasa,tas.cod_tipo_tasa  as tipotasa,
                                          hist.tipo_historico as tipohistorico,
                                          atrilin.desviacion,
                                          atrilin.cobra_mora 
                                       from atributoslinea atrilin
                                       left join atributos atr on atr.cod_atr = atrilin.cod_atr
                                       left join tipotasa tas on atrilin.tipo_tasa = tas.cod_tipo_tasa
                                       left join tipotasahist hist on atrilin.tipo_historico = hist.tipo_historico  Where atrilin.cod_linea_credito = " + codigo + " And atrilin.cod_rango_atr = " + rango;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "Consultar_atributos_credito", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ConsultarLineasCrediatributo(int codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                          atrilin.cod_linea_credito,
                                          atrilin.cod_atr,
                                          atrilin.cod_rango_atr,  
                                          atr.nombre as descripcion,  
                                          (case
                                              WHEN  atrilin.calculo_atr=1 THEN 'Tasa Fija'
                                              WHEN  atrilin.calculo_atr=2 THEN 'Ponderada'
                                              WHEN  atrilin.calculo_atr=3 THEN 'Tasa Historico'
                                              WHEN  atrilin.calculo_atr=4 THEN 'Promedio Historico'
                                              WHEN  atrilin.calculo_atr=5 THEN 'Tasa Historico'
                                           END) as FormaCalculo,
                                          atrilin.tasa,tas.nombre as tipotasa,
                                          hist.descripcion as tipohistorico ,
                                          atrilin.desviacion,
                                          atrilin.cobra_mora 
                                       from atributoslinea atrilin
                                       left join atributos atr on atr.cod_atr = atrilin.cod_atr
                                       left join tipotasa tas on atrilin.tipo_tasa = tas.cod_tipo_tasa
                                       left join tipotasahist hist on atrilin.tipo_historico = hist.tipo_historico Where atrilin.cod_linea_credito = " + codigo;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipotasa"] != DBNull.Value) entidad.tipotasa = Convert.ToInt64(resultado["tipotasa"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["tasa"]);
                            if (resultado["FormaCalculo"] != DBNull.Value) entidad.formacalculo = Convert.ToString(resultado["FormaCalculo"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipohistorico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipohistorico"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCrediatributo", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineasCredito> ConsultarLineasCreditodeducciones(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_linea_credito, des.cod_atr, atri.nombre as descripcion, '' as tipoliquidacion, 
                                        (CASE
                                            WHEN des.tipo_descuento=0 THEN 'Constante'
                                            WHEN des.tipo_descuento=1 THEN 'Constante'
                                            WHEN  des.tipo_descuento=2 THEN 'Factor'
                                            WHEN  des.tipo_descuento=3 THEN 'Porcentaje'
                                            WHEN  des.tipo_descuento=4 THEN 'Rango'
                                        END) as tiposdescuento, 
                                        (CASE
                                            WHEN des.forma_descuento=1 THEN 'Descuento del Desembolso'
                                            WHEN  des.forma_descuento=2 THEN 'Financiado'
                                            WHEN  des.forma_descuento=3 THEN 'Sumado a la Cuota'
                                        END) as Formadescuento,
                                        des.valor, des.numero_cuotas, des.cobra_mora, des.modifica, impu.descripcion as tipoimpuesto 
                                        From descuentoslinea des
                                        Inner join atributos atri on des.cod_atr=atri.cod_atr
                                        Left join tipoimpuesto impu on des.tipo_impuesto = impu.cod_tipo_impuesto Where cod_linea_credito = '" + codigo + "' ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipoliquidacion"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToString(resultado["tipoliquidacion"]);
                            entidad.tipoliquidacion = NombreTipoLiquidacionDeduccion(entidad.tipoliquidacion);
                            if (resultado["tiposdescuento"] != DBNull.Value) entidad.tiposdescuento = Convert.ToString(resultado["tiposdescuento"]);
                            if (resultado["Formadescuento"] != DBNull.Value) entidad.Formadescuento = Convert.ToString(resultado["Formadescuento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["valor"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.numero_cuotas = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipoimpuesto"] != DBNull.Value) entidad.tipoimpuesto = Convert.ToString(resultado["tipoimpuesto"]);
                            if (resultado["modifica"] != DBNull.Value) entidad.modifica = Convert.ToInt64(resultado["modifica"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCreditodeducciones", ex);
                        return null;
                    }
                }
            }
        }

        public string NombreTipoLiquidacionDeduccion(string pTipoLiquidacion)
        {
            if (pTipoLiquidacion == "1") return "Factor (Plazo+1) * Monto";
            if (pTipoLiquidacion == "2") return "Factor * Monto";
            if (pTipoLiquidacion == "3") return "Factor * Plazo * Monto";
            if (pTipoLiquidacion == "4") return "Factor (Factor+1) ** Cuotas";
            if (pTipoLiquidacion == "5") return "Factor * (Monto - Valor)";
            if (pTipoLiquidacion == "6") return "Factor * Saldo";
            if (pTipoLiquidacion == "7") return "Factor * (Saldo*Interes)";
            if (pTipoLiquidacion == "8") return "Factor Según Categoria * Monto";
            if (pTipoLiquidacion == "9") return "FactorVeh * VrComercial";
            if (pTipoLiquidacion == "10") return "Timbres -> Factor * Vlr_Leasing";
            if (pTipoLiquidacion == "11") return "Factor * Vlr_Bien";
            if (pTipoLiquidacion == "12") return "Factor * ((Vlr_Bien/Dias_Tot)*Dias_Mes)";
            if (pTipoLiquidacion == "13") return "Factor * Plazo * Saldo";
            if (pTipoLiquidacion == "14") return "Factor * Cuota";
            if (pTipoLiquidacion == "15") return "(Canon-(Vlr_Bien*Dias_Mes/Factor))*IVA";
            if (pTipoLiquidacion == "16") return "Tasa * Saldo Credito Diferido";
            if (pTipoLiquidacion == "17") return "Factor * ( 1 + Codeudor ) * Saldo";
            if (pTipoLiquidacion == "18") return "Factor * ( 1 + Codeudor ) * Monto";
            if (pTipoLiquidacion == "19") return "Factor * Plazo";
            if (pTipoLiquidacion == "20") return "Factor por Millón * Monto * Plazo";
            if (pTipoLiquidacion == "21") return "Factor Anual * Monto * Plazo";
            if (pTipoLiquidacion == "22") return "Factor/Plazo * Monto";
            if (pTipoLiquidacion == "23") return "Comisión por Rango";
            if (pTipoLiquidacion == "24") return "Factor * (Monto - Valores Recogidos)";
            if (pTipoLiquidacion == "25") return "Factor * Cuota (Dias Mora)";

            return "";
        }

        public List<LineasCredito> ConsultarLineasCreditoatributos(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from rangosatributos where cod_linea_credito = '" + codigo + "' order by cod_linea_credito";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["cod_rango_atr"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt64(resultado["cod_rango_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);


                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarLineasCreditoatributos", ex);
                        return null;
                    }
                }
            }
        }
        public LineasCredito ConsultarAtributoGeneral(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from atributos where cod_atr = '" + codigo + "' order by 1";


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["causa"] != DBNull.Value) entidad.causa = Convert.ToInt32(resultado["causa"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarAtributos", ex);
                        return null;
                    }
                }
            }
        }
        public LineasCredito ConsultarAtributos(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineasCredito entidad = new LineasCredito();
            //List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from rangosatributos where cod_linea_credito = '" + codigo + "' order by cod_linea_credito";


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["cod_rango_atr"] != DBNull.Value) entidad.cod_rango_atr = Convert.ToInt64(resultado["cod_rango_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarAtributos", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ListarDeducciones(int codigo, int atributo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstDeduc = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_linea_credito, des.cod_atr, atri.nombre as descripcion,des.tipo_liquidacion,
                                       ti.descripcion as tipoliquidacion, (case
                                                WHEN des.tipo_descuento=0 THEN 'Constante'
                                                WHEN des.tipo_descuento=1 THEN 'Constante'
                                                WHEN  des.tipo_descuento=2 THEN 'Factor'
                                                WHEN  des.tipo_descuento=3 THEN 'Porcentaje'
                                                WHEN  des.tipo_descuento=4 THEN 'Rango'
                                               END) as tiposdescuento, des.tipo_descuento,
                                               (case
                                                WHEN des.forma_descuento=1 THEN 'Descuento del Desembolso'
                                                WHEN  des.forma_descuento=2 THEN 'Financiado'
                                                WHEN  des.forma_descuento=3 THEN 'Sumado a la Cuota'
                                               END) as Formadescuento,des.forma_descuento,
                                               des.valor,des.numero_cuotas,
                                               des.cobra_mora,
                                               impu.descripcion as tipoimpuesto, cod_tipo_impuesto from descuentoslinea des
                                               inner join atributos atri on des.cod_atr=atri.cod_atr
                                       inner join tipoliquidacion ti on des.tipo_liquidacion=ti.tipo_liquidacion
                                       left join tipoimpuesto impu on des.tipo_impuesto = impu.cod_tipo_impuesto  where cod_linea_credito=" + codigo;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipoliquidacion"] != DBNull.Value) entidad.tipoliquidacion = Convert.ToString(resultado["tipoliquidacion"]);
                            if (resultado["tipo_liquidacion"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["tipo_liquidacion"]);
                            if (resultado["tiposdescuento"] != DBNull.Value) entidad.tiposdescuento = Convert.ToString(resultado["tiposdescuento"]);
                            if (resultado["Formadescuento"] != DBNull.Value) entidad.Formadescuento = Convert.ToString(resultado["Formadescuento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["valor"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.numero_cuotas = Convert.ToString(resultado["numero_cuotas"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["tipoimpuesto"] != DBNull.Value) entidad.tipoimpuesto = Convert.ToString(resultado["tipoimpuesto"]);
                            if (resultado["forma_descuento"] != DBNull.Value) entidad.Forma_descuento = Convert.ToInt64(resultado["forma_descuento"]);
                            if (resultado["tipo_descuento"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt64(resultado["tipo_descuento"]);
                            if (resultado["cod_tipo_impuesto"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt64(resultado["cod_tipo_impuesto"]);
                            lstDeduc.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDeduc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarDeducciones", ex);
                        return null;
                    }
                }
            }
        }


        public LineasCredito ConsultarDeducciones(string cod_linea, int atributo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        if (atributo == null || atributo == 0)
                            sql = @"Select cod_linea_credito, null as cod_atr, null as tipo_liquidacion, null as tipo_descuento, null as forma_descuento, null as valor, null as numero_cuotas, null as cobra_mora, modifica,null as tipo_impuesto
                                      From lineascredito Where cod_linea_credito = '" + cod_linea + "' ";
                        else
                            sql = @"Select cod_linea_credito, cod_atr, tipo_liquidacion, tipo_descuento, forma_descuento, valor, numero_cuotas, cobra_mora, modifica,tipo_impuesto 
                                      From descuentoslinea Where cod_linea_credito = '" + cod_linea + "' And cod_atr = " + atributo;
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["tipo_liquidacion"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["tipo_liquidacion"]);
                            if (resultado["tipo_descuento"] != DBNull.Value) entidad.tipo_descuento = Convert.ToInt64(resultado["tipo_descuento"]);
                            if (resultado["forma_descuento"] != DBNull.Value) entidad.Forma_descuento = Convert.ToInt64(resultado["forma_descuento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor1 = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.numero_cuotas1 = Convert.ToInt32(resultado["numero_cuotas"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt64(resultado["TIPO_IMPUESTO"]);
                            if (resultado["MODIFICA"] != DBNull.Value) entidad.modifica = Convert.ToInt64(resultado["MODIFICA"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarDeducciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Listar todas las lineas de crédito
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCredito(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> listLineas = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM LINEASCREDITO";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            listLineas.Add(entidad);
                        }
                        return listLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarLineasCredito", ex);
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineasCredito dados unos filtros
        /// </summary>
        /// <param name="pLineasCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineasCredito obtenidos</returns>
        public List<LineasCredito> ListarLineasCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT LINEASCREDITO.*, cod_linea_credito || ' - ' || nombre AS NOM_LINEA_CREDITO FROM  LINEASCREDITO " + ObtenerFiltro(pLineasCredito) + " ORDER BY cod_linea_credito";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.Codigo = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOM_LINEA_CREDITO"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["NOM_LINEA_CREDITO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt64(resultado["TIPO_CUPO"]);
                            if (resultado["RECOGE_SALDOS"] != DBNull.Value) entidad.recoge_saldos = Convert.ToInt64(resultado["RECOGE_SALDOS"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt64(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToDecimal(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToDecimal(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MANEJA_PERGRACIA"] != DBNull.Value) entidad.maneja_pergracia = Convert.ToString(resultado["MANEJA_PERGRACIA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["TIPO_PERIODIC_GRACIA"] != DBNull.Value) entidad.tipo_periodic_gracia = Convert.ToString(resultado["TIPO_PERIODIC_GRACIA"]);
                            if (resultado["MODIFICA_DATOS"] != DBNull.Value) entidad.modifica_datos = Convert.ToString(resultado["MODIFICA_DATOS"]);
                            if (resultado["MODIFICA_FECHA_PAGO"] != DBNull.Value) entidad.modifica_fecha_pago = Convert.ToString(resultado["MODIFICA_FECHA_PAGO"]);
                            if (resultado["GARANTIA_REQUERIDA"] != DBNull.Value) entidad.garantia_requerida = Convert.ToString(resultado["GARANTIA_REQUERIDA"]);
                            if (resultado["TIPO_CAPITALIZACION"] != DBNull.Value) entidad.tipo_capitalizacion = Convert.ToInt64(resultado["TIPO_CAPITALIZACION"]);
                            if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.cuotas_extras = Convert.ToInt64(resultado["CUOTAS_EXTRAS"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) entidad.numero_codeudores = Convert.ToInt64(resultado["NUMERO_CODEUDORES"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["PORC_CORTO"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_CORTO"]);
                            if (resultado["TIPO_AMORTIZA"] != DBNull.Value) entidad.tipo_amortiza = Convert.ToInt64(resultado["TIPO_AMORTIZA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.avances_aprob = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) entidad.desem_ahorros = Convert.ToInt64(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarLineasCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla LineasCredito dados unos filtros
        /// </summary>
        /// <param name="pLineasCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineasCredito obtenidos</returns>
        public List<LineasCredito> ListarLineasCreditoSinAuxilio(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "SELECT LINEASCREDITO.*, cod_linea_credito || ' - ' || nombre AS NOM_LINEA_CREDITO FROM  LINEASCREDITO " + ObtenerFiltro(pLineasCredito);
                        string sql = sql1 + " and maneja_auxilio=0 " + " ORDER BY cod_linea_credito";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.Codigo = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOM_LINEA_CREDITO"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["NOM_LINEA_CREDITO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt64(resultado["TIPO_CUPO"]);
                            if (resultado["RECOGE_SALDOS"] != DBNull.Value) entidad.recoge_saldos = Convert.ToInt64(resultado["RECOGE_SALDOS"]);
                            if (resultado["COBRA_MORA"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["COBRA_MORA"]);
                            if (resultado["TIPO_REFINANCIA"] != DBNull.Value) entidad.tipo_refinancia = Convert.ToInt64(resultado["TIPO_REFINANCIA"]);
                            if (resultado["MINIMO_REFINANCIA"] != DBNull.Value) entidad.minimo_refinancia = Convert.ToDecimal(resultado["MINIMO_REFINANCIA"]);
                            if (resultado["MAXIMO_REFINANCIA"] != DBNull.Value) entidad.maximo_refinancia = Convert.ToDecimal(resultado["MAXIMO_REFINANCIA"]);
                            if (resultado["MANEJA_PERGRACIA"] != DBNull.Value) entidad.maneja_pergracia = Convert.ToString(resultado["MANEJA_PERGRACIA"]);
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.periodo_gracia = Convert.ToInt64(resultado["PERIODO_GRACIA"]);
                            if (resultado["TIPO_PERIODIC_GRACIA"] != DBNull.Value) entidad.tipo_periodic_gracia = Convert.ToString(resultado["TIPO_PERIODIC_GRACIA"]);
                            if (resultado["MODIFICA_DATOS"] != DBNull.Value) entidad.modifica_datos = Convert.ToString(resultado["MODIFICA_DATOS"]);
                            if (resultado["MODIFICA_FECHA_PAGO"] != DBNull.Value) entidad.modifica_fecha_pago = Convert.ToString(resultado["MODIFICA_FECHA_PAGO"]);
                            if (resultado["GARANTIA_REQUERIDA"] != DBNull.Value) entidad.garantia_requerida = Convert.ToString(resultado["GARANTIA_REQUERIDA"]);
                            if (resultado["TIPO_CAPITALIZACION"] != DBNull.Value) entidad.tipo_capitalizacion = Convert.ToInt64(resultado["TIPO_CAPITALIZACION"]);
                            if (resultado["CUOTAS_EXTRAS"] != DBNull.Value) entidad.cuotas_extras = Convert.ToInt64(resultado["CUOTAS_EXTRAS"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["NUMERO_CODEUDORES"] != DBNull.Value) entidad.numero_codeudores = Convert.ToInt64(resultado["NUMERO_CODEUDORES"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["PORC_CORTO"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_CORTO"]);
                            if (resultado["TIPO_AMORTIZA"] != DBNull.Value) entidad.tipo_amortiza = Convert.ToInt64(resultado["TIPO_AMORTIZA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) entidad.plazo_diferir = Convert.ToInt64(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) entidad.avances_aprob = Convert.ToInt64(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) entidad.desem_ahorros = Convert.ToInt64(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarLineasCreditoSinAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineasCredito> ListarParentesco(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from PARENTESCOS";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.Codigo = Convert.ToString(resultado["CODPARENTESCO"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarParentesco", ex);
                        return null;
                    }
                }
            }
        }



        public List<LineasCredito> MotivoCredito(LineasCredito pLineasCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from motivo_excepcion";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["cod_motivo_excepcion"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["cod_motivo_excepcion"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "MotivoCredito", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineasCredito> ddlLinea(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineasCredito = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From lineasauxilios l Where l.educativo = 1 and L.ESTADO = 1 order by L.DESCRIPCION";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();

                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORC_MATRICULA"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_MATRICULA"]);


                            lstLineasCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineasCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ddlLinea", ex);
                        return null;
                    }
                }
            }
        }
        public LineasCredito getPorcentajeMatricula(string CodLineaAuxilio, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineasCredito entidad = new LineasCredito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From lineasauxilios l Where l.educativo = 1 And l.cod_linea_auxilio = '" + CodLineaAuxilio + "' ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {


                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORC_MATRICULA"] != DBNull.Value) entidad.porc_corto = Convert.ToInt64(resultado["PORC_MATRICULA"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "getPorcentajeMatricula", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene las listas desplegables de la tabla Persona
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<Persona1> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Persona1> lstDatosSolicitud = new List<Persona1>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        switch (ListaSolicitada)
                        {

                            case "TipoLiquidacion":
                                sql = "SELECT TIPO_LIQUIDACION as ListaId, DESCRIPCION as ListaDescripcion FROM TIPOLIQUIDACION";
                                break;

                            case "PeriodicidadGracia":
                                sql = "SELECT COD_PERIODICIDAD as ListaId, DESCRIPCION as ListaDescripcion FROM PERIODICIDAD";
                                break;

                            case "Cod_clasifica":
                                sql = "SELECT COD_CLASIFICA as ListaId, DESCRIPCION as ListaDescripcion FROM CLASIFICACION where estado=1";
                                break;

                            case "Cod_moneda":
                                sql = "SELECT COD_MONEDA as ListaId, DESCRIPCION as ListaDescripcion FROM TIPOMONEDA";
                                break;

                            // CREDITO ROTATIVO
                            case "TipoLiquidacionRot":
                                sql = "SELECT TIPO_LIQUIDACION as ListaId, DESCRIPCION as ListaDescripcion FROM TIPOLIQUIDACION order by  1 asc ";
                                break;
                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Persona1 entidad = new Persona1();
                            if (ListaSolicitada == "PeriodicidadGracia")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            {
                                if (resultado["ListaId"] != DBNull.Value)
                                    entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]);
                            }
                            else
                            {
                                if (resultado["ListaId"] != DBNull.Value)
                                    entidad.ListaId = Convert.ToInt64(resultado["ListaId"]);
                            }
                            if (resultado["ListaDescripcion"] != DBNull.Value)
                                entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);

                            lstDatosSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClienteData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }


        public List<Atributos> ListarAtributos(Atributos entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Atributos> lstLineas = new List<Atributos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_atr, nombre as nombre, NOMBRE AS descripcion, causa from atributos Order by cod_atr";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new Atributos();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["nombre"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["causa"] != DBNull.Value) entidad.causa = Convert.ToInt32(resultado["causa"]);
                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarAtributos", ex);
                        return null;
                    }
                }
            }
        }

        public List<RangosTopes> ListarTopes(RangosTopes entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RangosTopes> lstLineas = new List<RangosTopes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from tipo_tope order by 1";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new RangosTopes();
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_tope"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["tipo_tope"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);

                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarTopes", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta de datos de la línea de crédito
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Lineas de credito obtenidos</returns>
        public LineasCredito ConsultaLineaCredito(String cod_linea_credito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            LineasCredito DatosLinea = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from lineascredito Where cod_linea_credito = '" + cod_linea_credito + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["cod_linea_credito"] != DBNull.Value) DatosLinea.Codigo = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre"] != DBNull.Value) DatosLinea.nombre = Convert.ToString(resultado["nombre"]); else DatosLinea.nombre = "";
                            if (resultado["tipo_linea"] != DBNull.Value) DatosLinea.tipo_linea = Convert.ToInt16(resultado["tipo_linea"]); else DatosLinea.tipo_linea = null;
                            if (resultado["tipo_liquidacion"] != DBNull.Value) DatosLinea.tipo_liquidacion = Convert.ToInt16(resultado["tipo_liquidacion"]); else DatosLinea.tipo_liquidacion = null;
                            if (resultado["tipo_cupo"] != DBNull.Value) DatosLinea.tipo_cupo = Convert.ToInt16(resultado["tipo_cupo"]); else DatosLinea.tipo_cupo = null;
                            if (resultado["recoge_saldos"] != DBNull.Value) DatosLinea.recoge_saldos = Convert.ToInt16(resultado["recoge_saldos"]); else DatosLinea.recoge_saldos = null;
                            if (resultado["cobra_mora"] != DBNull.Value) DatosLinea.cobra_mora = Convert.ToInt16(resultado["cobra_mora"]); else DatosLinea.cobra_mora = null;
                            if (resultado["tipo_refinancia"] != DBNull.Value) DatosLinea.tipo_refinancia = Convert.ToInt16(resultado["tipo_refinancia"]); else DatosLinea.tipo_refinancia = null;
                            if (resultado["minimo_refinancia"] != DBNull.Value) DatosLinea.minimo_refinancia = Convert.ToDecimal(resultado["minimo_refinancia"]); else DatosLinea.minimo_refinancia = null;
                            if (resultado["maximo_refinancia"] != DBNull.Value) DatosLinea.maximo_refinancia = Convert.ToDecimal(resultado["maximo_refinancia"]); else DatosLinea.maximo_refinancia = null;
                            if (resultado["maneja_pergracia"] != DBNull.Value) DatosLinea.Maneja_Pergracia = Convert.ToString(resultado["maneja_pergracia"]); else DatosLinea.Maneja_Pergracia = "";
                            if (resultado["periodo_gracia"] != DBNull.Value) DatosLinea.Periodo_Gracia = Convert.ToInt16(resultado["periodo_gracia"]); else DatosLinea.Periodo_Gracia = null;
                            if (resultado["tipo_periodic_gracia"] != DBNull.Value) DatosLinea.Tipo_Periodic_Gracia = Convert.ToInt16(resultado["tipo_periodic_gracia"]); else DatosLinea.Tipo_Periodic_Gracia = null;
                            if (resultado["modifica_datos"] != DBNull.Value) DatosLinea.Modifica_Datos = Convert.ToString(resultado["modifica_datos"]); else DatosLinea.Modifica_Datos = null;
                            if (resultado["modifica_fecha_pago"] != DBNull.Value) DatosLinea.Modifica_Fecha_Pago = Convert.ToString(resultado["modifica_fecha_pago"]); else DatosLinea.Modifica_Fecha_Pago = "";
                            if (resultado["garantia_requerida"] != DBNull.Value) DatosLinea.Garantia_Requerida = Convert.ToString(resultado["garantia_requerida"]); else DatosLinea.Garantia_Requerida = "";
                            if (resultado["tipo_capitalizacion"] != DBNull.Value) DatosLinea.Tipo_Capitalizacion = Convert.ToInt16(resultado["tipo_capitalizacion"]); else DatosLinea.Tipo_Capitalizacion = null;
                            if (resultado["cuotas_extras"] != DBNull.Value) DatosLinea.Cuotas_Extras = Convert.ToInt16(resultado["cuotas_extras"]); else DatosLinea.Cuotas_Extras = null;
                            if (resultado["cod_clasifica"] != DBNull.Value) DatosLinea.Cod_Clasifica = Convert.ToInt16(resultado["cod_clasifica"]); else DatosLinea.Cod_Clasifica = null;
                            if (resultado["numero_codeudores"] != DBNull.Value) DatosLinea.Numero_Codeudores = Convert.ToInt16(resultado["numero_codeudores"]); else DatosLinea.Numero_Codeudores = null;
                            if (resultado["cod_moneda"] != DBNull.Value) DatosLinea.Cod_Moneda = Convert.ToInt16(resultado["cod_moneda"]); else DatosLinea.Cod_Moneda = null;
                            if (resultado["porc_corto"] != DBNull.Value) DatosLinea.Porc_Corto = Convert.ToDouble(resultado["porc_corto"]); else DatosLinea.Porc_Corto = null;
                            if (resultado["tipo_amortiza"] != DBNull.Value) DatosLinea.Tipo_Amortiza = Convert.ToInt16(resultado["tipo_amortiza"]); else DatosLinea.Tipo_Amortiza = null;
                            if (resultado["estado"] != DBNull.Value) DatosLinea.Estado = Convert.ToInt16(resultado["estado"]); else DatosLinea.Estado = null;
                            if (resultado["PLAZO_A_DIFERIR"] != DBNull.Value) DatosLinea.plazo_diferir = Convert.ToInt16(resultado["PLAZO_A_DIFERIR"]);
                            if (resultado["APROBAR_AVANCES"] != DBNull.Value) DatosLinea.avances_aprob = Convert.ToInt16(resultado["APROBAR_AVANCES"]);
                            if (resultado["DESEMBOLSAR_A_AHORROS"] != DBNull.Value) DatosLinea.desem_ahorros = Convert.ToInt16(resultado["DESEMBOLSAR_A_AHORROS"]);
                            if (resultado["CREDITO_GERENCIAL"] != DBNull.Value) DatosLinea.credito_gerencial = Convert.ToInt16(resultado["CREDITO_GERENCIAL"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) DatosLinea.credito_educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) DatosLinea.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["MANEJA_AUXILIO"] != DBNull.Value) DatosLinea.maneja_auxilio = Convert.ToInt32(resultado["MANEJA_AUXILIO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return DatosLinea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ConsultaLineaCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Determinar el cupo de la línea de crédito
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public LineasCredito Calcular_Cupo(String pcod_linea_credito, Int64 pcod_persona, String pfecha, Usuario pUsuario)
        {
            LineasCredito entidad = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_LINEA_CREDITO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CREDITO.ParameterName = "pCOD_LINEA_CREDITO";
                        pCOD_LINEA_CREDITO.Value = pcod_linea_credito;
                        pCOD_LINEA_CREDITO.Direction = ParameterDirection.Input;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pcod_persona.ToString();
                        pCOD_PERSONA.Direction = ParameterDirection.Input;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "pFECHA";
                        pFECHA.Value = pfecha;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pMONTO_MAXIMO = cmdTransaccionFactory.CreateParameter();
                        pMONTO_MAXIMO.ParameterName = "pMONTO_MAXIMO";
                        pMONTO_MAXIMO.Value = 0;
                        pMONTO_MAXIMO.DbType = DbType.Decimal;
                        pMONTO_MAXIMO.Direction = ParameterDirection.Output;

                        DbParameter pPLAZO_MAXIMO = cmdTransaccionFactory.CreateParameter();
                        pPLAZO_MAXIMO.ParameterName = "pPLAZO_MAXIMO";
                        pPLAZO_MAXIMO.Value = 0;
                        pPLAZO_MAXIMO.DbType = DbType.Decimal;
                        pPLAZO_MAXIMO.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CREDITO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pMONTO_MAXIMO);
                        cmdTransaccionFactory.Parameters.Add(pPLAZO_MAXIMO);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CALCULARCUPO";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pMONTO_MAXIMO.Value.ToString() != null && !string.Equals(pMONTO_MAXIMO.Value.ToString(), "")) { entidad.Monto_Maximo = Convert.ToDouble(Convert.ToDecimal(pMONTO_MAXIMO.Value.ToString())); };
                        if (pPLAZO_MAXIMO.Value.ToString() != null && !string.Equals(pPLAZO_MAXIMO.Value.ToString(), "")) { entidad.Plazo_Maximo = Convert.ToDouble(Convert.ToDecimal(pPLAZO_MAXIMO.Value.ToString())); };
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Calcular_Cupo", "Calcular_Cupo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para listar las líneas de crédito re-estructuradas
        /// </summary>
        /// <param name="entidad"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<LineasCredito> ListarLineasCreditoRes(LineasCredito entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstLineas = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sLinRes = "";
                        string sqlRES = "Select valor From general Where codigo = 430";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlRES;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                            if (resultado["valor"] != DBNull.Value) sLinRes = Convert.ToString(resultado["valor"]);

                        string sql = "select cod_linea_credito as codigo, cod_linea_credito || ' - ' || nombre as nombre from lineascredito";
                        if (sLinRes.Trim() != "")
                            sql = sql + " where cod_linea_credito In (" + sLinRes + ") ";
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new LineasCredito();
                            //Asociar todos los valores a la entidad
                            if (resultado["codigo"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["codigo"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarLineasCreditoRes", ex);
                        return null;
                    }
                }
            }
        }


        public List<Int64> ListarRangos(LineasCredito entidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Int64> lstRangos = new List<Int64>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlRES = "Select cod_rango_atr From RangosAtributos Where cod_linea_credito = '" + entidad.cod_linea_credito + "' ";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlRES;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Int64 cod_rango = 0;
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_rango_atr"] != DBNull.Value) cod_rango = Convert.ToInt64(resultado["cod_rango_atr"]);
                            lstRangos.Add(cod_rango);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarRangos", ex);
                        return null;
                    }
                }
            }
        }


        public List<RangosTopes> ListarRangosTopes(LineasCredito pLineas, Int64 cod_rango, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RangosTopes> lstRangos = new List<RangosTopes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlRES = "Select tipo_tope, minimo, maximo From RangosTopes Where cod_rango_atr = " + cod_rango;
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlRES;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RangosTopes entidad = new RangosTopes();
                            if (resultado["tipo_tope"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["tipo_tope"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["maximo"]);
                            lstRangos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarRangosTopes", ex);
                        return null;
                    }
                }
            }
        }


        public List<Atributos> ListarRangosAtributos(LineasCredito pLineas, Int64 cod_rango, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Atributos> lstsAtributos = new List<Atributos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sqlRES = "Select a.cod_atr, b.nombre, a.calculo_atr, a.tasa, a.tipo_tasa, a.tipo_historico, a.desviacion, a.cobra_mora " +
                                        "From atributoslinea a Left Join atributos b On a.cod_atr = b.cod_atr Where cod_linea_credito = '" + pLineas.cod_linea_credito + "' And cod_rango_atr = " + cod_rango + " Order By 1";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlRES;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Atributos entidad = new Atributos();
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["nombre"]);
                            if (resultado["calculo_atr"] != DBNull.Value) entidad.calculo_atr = Convert.ToInt64(resultado["calculo_atr"]);
                            if (resultado["tasa"] != DBNull.Value) entidad.tasa = Convert.ToDouble(resultado["tasa"]);
                            if (resultado["tipo_tasa"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["tipo_tasa"]);
                            if (resultado["tipo_historico"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["tipo_historico"]);
                            if (resultado["desviacion"] != DBNull.Value) entidad.desviacion = Convert.ToDouble(resultado["desviacion"]);
                            if (resultado["cobra_mora"] != DBNull.Value) entidad.cobra_mora = Convert.ToInt64(resultado["cobra_mora"]);
                            lstsAtributos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstsAtributos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaCreditoData", "ListarRangosAtributos", ex);
                        return null;
                    }
                }
            }
        }

        public Boolean LineaEsFondoGarantiasComunitarias(String pId, Usuario pUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_LINEAS_GARCOM WHERE COD_LINEA_CREDITO = '" + pId.ToString() + "' ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            dbConnectionFactory.CerrarConexion(connection);
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "LineaEsFondoGarantiasComunitarias", ex);
                        return false;
                    }
                }
            }
        }

        public List<LineasCredito> LineasCastigo(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstlineas = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT L.COD_LINEA_CREDITO, L.NOMBRE FROM LINEASCREDITO L INNER JOIN PARAMETROS_LINEA P ON L.COD_LINEA_CREDITO = P.COD_LINEA_CREDITO AND P.COD_PARAMETRO = 320";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstlineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstlineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "LineasCastigo", ex);
                        return null;
                    }
                }
            }
        }


        public decimal ConsultarParametrosLinea(string cod_linea, string cod_parametro, Usuario pUsuario)
        {
            DbDataReader resultado;
            decimal entidad = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from parametros_linea Where cod_parametro = " + cod_parametro + " and cod_linea_credito = " + cod_linea;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) entidad = Convert.ToDecimal(resultado["Valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarParametrosLinea", ex);
                        return entidad;
                    }
                }
            }
        }

        public void EliminarAtributos(LineasCredito pAtributos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_credito.ParameterName = "p_cod_linea_credito";
                        p_cod_linea_credito.Value = pAtributos.cod_linea_credito;
                        p_cod_linea_credito.Direction = ParameterDirection.Input;
                        p_cod_linea_credito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_credito);

                        DbParameter p_cod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_rango_atr.ParameterName = "p_cod_rango_atr";
                        p_cod_rango_atr.Value = pAtributos.cod_rango_atr;
                        p_cod_rango_atr.Direction = ParameterDirection.Input;
                        p_cod_rango_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_rango_atr);

                        DbParameter p_cod_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_atr.ParameterName = "p_cod_atr";
                        p_cod_atr.Value = pAtributos.cod_atr;
                        p_cod_atr.Direction = ParameterDirection.Input;
                        p_cod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_atr);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ATRIBUTOSL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarAtributos", ex);
                    }
                }
            }
        }

        public void EliminarDeducciones(LineasCredito pDeducciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_credito.ParameterName = "p_cod_linea_credito";
                        p_cod_linea_credito.Value = pDeducciones.cod_linea_credito;
                        p_cod_linea_credito.Direction = ParameterDirection.Input;
                        //p_cod_linea_credito.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_credito);

                        DbParameter p_cod_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_atr.ParameterName = "p_cod_atr";
                        p_cod_atr.Value = pDeducciones.cod_atr;
                        p_cod_atr.Direction = ParameterDirection.Input;
                        //p_cod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_atr);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarDeducciones", ex);
                    }
                }
            }
        }

        public void EliminarTopes(RangosTopes pTopes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_idtope = cmdTransaccionFactory.CreateParameter();
                        p_idtope.ParameterName = "p_idtope";
                        p_idtope.Value = pTopes.idtope;
                        p_idtope.Direction = ParameterDirection.Input;
                        p_idtope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_idtope);


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RANGOSTOPE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarTopes", ex);
                    }
                }
            }
        }

        public LineasCredito CrearAtributos(LineasCredito pAtributos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pAtributos.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_rango_atr.ParameterName = "p_cod_rango_atr";
                        pcod_rango_atr.Value = pAtributos.cod_rango_atr;
                        pcod_rango_atr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_rango_atr);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pAtributos.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcalculo_atr = cmdTransaccionFactory.CreateParameter();
                        pcalculo_atr.ParameterName = "p_calculo_atr";
                        pcalculo_atr.Value = pAtributos.formacalculo;
                        pcalculo_atr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcalculo_atr);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pAtributos.tipo_historico != 0 && pAtributos.tipo_historico != null) ptipo_historico.Value = pAtributos.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pAtributos.desviacion != null) pdesviacion.Value = pAtributos.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        if (pAtributos.tipotasa != null) ptipo_tasa.Value = pAtributos.tipotasa; else ptipo_tasa.Value = DBNull.Value;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        if (pAtributos.tasa != null) ptasa.Value = pAtributos.tasa; else ptasa.Value = DBNull.Value;
                        ptasa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter pcobra_mora = cmdTransaccionFactory.CreateParameter();
                        pcobra_mora.ParameterName = "p_cobra_mora";
                        pcobra_mora.Value = pAtributos.cobra_mora;
                        pcobra_mora.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcobra_mora);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ATRIBUTOSL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAtributos;

                    }

                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearAtributos", ex);

                    }
                    return pAtributos;
                }
            }
        }


        public void EliminarALLAtributosXlinea(LineasCredito pAtributos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "delete from atributoslinea where cod_linea_credito = '" + pAtributos.cod_linea_credito + "' and cod_rango_atr = " + pAtributos.cod_rango_atr;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarALLAtributosXlinea", ex);
                    }
                }
            }
        }


        public void EliminarTodoElAtributo(LineasCredito pAtri, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_credito.ParameterName = "p_cod_linea_credito";
                        p_cod_linea_credito.Value = pAtri.cod_linea_credito;
                        p_cod_linea_credito.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_credito);

                        DbParameter p_cod_rango_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_rango_atr.ParameterName = "p_cod_rango_atr";
                        p_cod_rango_atr.Value = pAtri.cod_rango_atr;
                        p_cod_rango_atr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_rango_atr);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RANGOSATRI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarTodoElAtributo", ex);
                    }
                }
            }
        }

        //Agregado para crear un registro en la tabla RANVAL_ATRIBUTO
        public RangosTopes CrearRanValAtributo(RangosTopes pTopes, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pTopes.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_tope = cmdTransaccionFactory.CreateParameter();
                        ptipo_tope.ParameterName = "p_tipo_tope";
                        ptipo_tope.Value = pTopes.tipo_tope;
                        ptipo_tope.Direction = ParameterDirection.Input;
                        ptipo_tope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tope);

                        DbParameter pdesde = cmdTransaccionFactory.CreateParameter();
                        pdesde.ParameterName = "p_desde";
                        if (pTopes.minimo != null) pdesde.Value = Convert.ToInt64(pTopes.minimo); else pdesde.Value = 0;
                        pdesde.Direction = ParameterDirection.Input;
                        pdesde.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdesde);

                        DbParameter phasta = cmdTransaccionFactory.CreateParameter();
                        phasta.ParameterName = "p_hasta";
                        if (pTopes.maximo != null) phasta.Value = Convert.ToInt64(pTopes.maximo); else phasta.Value = 0;
                        phasta.Direction = ParameterDirection.Input;
                        phasta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(phasta);

                        DbParameter ptipo_valor = cmdTransaccionFactory.CreateParameter();
                        ptipo_valor.ParameterName = "p_tipo_valor";
                        ptipo_valor.Value = pTopes.tipo_valor;
                        ptipo_valor.Direction = ParameterDirection.Input;
                        ptipo_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_valor);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTopes.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_RANVATRIB_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearRanValAtributo", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para eliminar registros de la tabla RANVAL_ATRIBUTO
        public void EliminarRanValAtributo(Int64 cod_atributo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        String sql = "DELETE RANVAL_ATRIBUTO WHERE COD_ATR = " + cod_atributo;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        //return pTopes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarRanValAtributo", ex);
                        //return null;
                    }
                }
            }
        }


        //Adicionado para consultar rangos de atributos 
        public List<RangosTopes> ListarRangosAtributos(Int64 cod_atributo, Usuario pUsuario)
        {
            DbDataReader resultado;

            List<RangosTopes> lstRangos = new List<RangosTopes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT R.COD_ATR, R.TIPO_TOPE, R.DESDE, R.HASTA, CASE R.TIPO_VALOR WHEN 1 THEN 'Unidad' WHEN 2 THEN 'Porcentaje' ELSE TO_CHAR(R.TIPO_VALOR) END AS NOM_TIPO_VALOR, R.TIPO_VALOR, R.VALOR
                                        FROM RANVAL_ATRIBUTO R INNER JOIN ATRIBUTOS A ON R.COD_ATR = A.COD_ATR  WHERE R.COD_ATR = " + cod_atributo;

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RangosTopes entidad = new RangosTopes();

                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["TIPO_TOPE"] != DBNull.Value) entidad.tipo_tope = Convert.ToInt64(resultado["TIPO_TOPE"]);
                            if (resultado["DESDE"] != DBNull.Value) entidad.minimo = Convert.ToString(resultado["DESDE"]);
                            if (resultado["HASTA"] != DBNull.Value) entidad.maximo = Convert.ToString(resultado["HASTA"]);
                            if (resultado["NOM_TIPO_VALOR"] != DBNull.Value) entidad.nom_tipo_valor = Convert.ToString(resultado["NOM_TIPO_VALOR"]);
                            if (resultado["TIPO_VALOR"] != DBNull.Value) entidad.tipo_valor = Convert.ToInt64(resultado["TIPO_VALOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);

                            lstRangos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRangos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarRangosAtributos", ex);
                        return null;
                    }
                }
            }
        }


        #region Parametros de Componentes (Documentos)

        public List<LineasCredito> ListarDocumentos(LineasCredito pLinea, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstDocumentos = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (filtro == "RECUPERA_DATOS")
                        {
                            sql = @"SELECT T.TIPO_DOCUMENTO, T.DESCRIPCION,D.APLICA_CODEUDOR, D.COD_LINEA_CREDIO "
                                + "From tiposdocumento t left join docrequeridoslinea d on t.tipo_documento = d.tipo_documento And d.cod_linea_credio = '"
                                + pLinea.cod_linea_credito + "' Where t.tipo = 'R' Order by 1";
                        }
                        else
                        {
                            sql = "select Tipo_Documento,Descripcion,0 As aplica_codeudor,null as COD_LINEA_CREDIO "
                                    + "from tiposdocumento where tipo = 'R' order by 1";
                        }


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
                            entidad.aplica_codeudor = entidad.aplica_codeudor == null || entidad.aplica_codeudor == "0" ? "0" : "1";
                            if (resultado["COD_LINEA_CREDIO"] != DBNull.Value) entidad.checkbox = Convert.ToInt32(resultado["COD_LINEA_CREDIO"]);
                            lstDocumentos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarDocumentos", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineasCredito> ListarDocumentosXLinea(string pCod_linea_credito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstDocumentos = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        sql = @"SELECT T.TIPO_DOCUMENTO, T.DESCRIPCION,D.APLICA_CODEUDOR, D.COD_LINEA_CREDIO "
                                + "From docrequeridoslinea d left join tiposdocumento t on t.tipo_documento = d.tipo_documento "
                                + " Where t.tipo = 'R' And d.cod_linea_credio = '" + pCod_linea_credito + "' Order by 1";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["APLICA_CODEUDOR"] != DBNull.Value) entidad.aplica_codeudor = Convert.ToString(resultado["APLICA_CODEUDOR"]);
                            entidad.aplica_codeudor = entidad.aplica_codeudor == null || entidad.aplica_codeudor == "0" ? "0" : "1";
                            if (resultado["COD_LINEA_CREDIO"] != DBNull.Value) entidad.checkbox = Convert.ToInt32(resultado["COD_LINEA_CREDIO"]);
                            lstDocumentos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarDocumentosXLinea", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineasCredito> ListarComboTipoDocumentos(LineasCredito pLinea, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LineasCredito> lstDocumentos = new List<LineasCredito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;

                        sql = @"Select Tipo_Documento, Descripcion, es_orden From tiposdocumento Where tipo = 'G' Order by 1";


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt64(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ES_ORDEN"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ES_ORDEN"]);
                            lstDocumentos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDocumentos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarComboTipoDocumentos", ex);
                        return null;
                    }
                }
            }
        }

        //GRABAR LINEA DOCUMENTOS tabla docrequeridoslinea
        public LineasCredito CrearLineasDocumentos(LineasCredito pDocu, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credio.ParameterName = "p_cod_linea_credio";
                        pcod_linea_credio.Value = pDocu.cod_linea_credito;
                        pcod_linea_credio.Direction = ParameterDirection.Input;
                        pcod_linea_credio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credio);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "p_tipo_documento";
                        ptipo_documento.Value = pDocu.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        //ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        DbParameter paplica_codeudor = cmdTransaccionFactory.CreateParameter();
                        paplica_codeudor.ParameterName = "p_aplica_codeudor";
                        paplica_codeudor.Value = pDocu.aplica_codeudor;
                        paplica_codeudor.Direction = ParameterDirection.Input;
                        paplica_codeudor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(paplica_codeudor);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCREQUERI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearLineasDocumentos", ex);
                        return null;
                    }
                }
            }
        }


        //crear Datos de docgarantialinea
        public LineasCredito CrearGarantiaLineaDocumento(LineasCredito pDocu, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pDocu.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "p_tipo_documento";
                        if (pDocu.tipo_documento == null) return null;
                        ptipo_documento.Value = pDocu.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        DbParameter prequerido = cmdTransaccionFactory.CreateParameter();
                        prequerido.ParameterName = "p_requerido";
                        if (pDocu.requerido != null) prequerido.Value = pDocu.requerido; else prequerido.Value = 0;
                        prequerido.Direction = ParameterDirection.Input;
                        prequerido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prequerido);

                        DbParameter pplantilla = cmdTransaccionFactory.CreateParameter();
                        pplantilla.ParameterName = "p_plantilla";
                        if (pDocu.plantilla != null) pplantilla.Value = pDocu.plantilla; else pplantilla.Value = DBNull.Value;
                        pplantilla.Direction = ParameterDirection.Input;
                        pplantilla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pplantilla);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "P_COD_EMPRESA";
                        if (pDocu.cod_empresa != null) pcod_empresa.Value = pDocu.cod_empresa; else pcod_empresa.Value = DBNull.Value;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCGARANTI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDocu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearGarantiaLineaDocumento", ex);
                        return null;
                    }
                }
            }
        }



        public List<LineasCredito> ConsultarGarantiaDocumento(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstData = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DOCGARANTIALINEA WHERE COD_LINEA_CREDITO = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            if (resultado["PLANTILLA"] != DBNull.Value) entidad.plantilla = Convert.ToString(resultado["PLANTILLA"]);
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarGarantiaDocumento", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ConsultarGarantiasPorCredito(int pCreditoId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstData = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT d.TIPO_DOCUMENTO, d.REQUERIDO, t.DESCRIPCION, t.TEXTO FROM DOCGARANTIALINEA d
                                        inner join TIPOSDOCUMENTO t on d.TIPO_DOCUMENTO = t.TIPO_DOCUMENTO
                                        inner join CREDITO c on c.COD_LINEA_CREDITO = d.COD_LINEA_CREDITO
                                        WHERE c.NUMERO_RADICACION = " + pCreditoId;
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTO"] != DBNull.Value) entidad.plantilla = Convert.ToString(resultado["TEXTO"]);
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarGarantiasPorCredito", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarDocumentosRequerido(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from DOCREQUERIDOSLINEA where cod_linea_credio = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarDocumentosRequerido", ex);
                    }
                }
            }
        }

        public void EliminarDocumentosGarantia(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from DOCGARANTIALINEA where COD_LINEA_CREDITO = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarDocumentosGarantia", ex);
                    }
                }
            }
        }


        public void Eliminardocumentosdegarantia(string pId, string linea, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineasCredito pLineasCredito = new LineasCredito();
                        pLineasCredito = ConsultarGarantiaDOCS(Convert.ToString(pId), linea, pUsuario);

                        DbParameter plinea_credito = cmdTransaccionFactory.CreateParameter();
                        plinea_credito.ParameterName = "P_COD_LINEA_CREDITO";
                        plinea_credito.Value = pLineasCredito.cod_linea_credito;
                        plinea_credito.Direction = ParameterDirection.Input;
                        plinea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(plinea_credito);

                        DbParameter ptipo_documento = cmdTransaccionFactory.CreateParameter();
                        ptipo_documento.ParameterName = "P_TIPO_DOCUMENTO";
                        ptipo_documento.Value = pLineasCredito.tipo_documento;
                        ptipo_documento.Direction = ParameterDirection.Input;
                        ptipo_documento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_documento);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DOCGARANTI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "Eliminardocumentosdegarantia", ex);
                    }
                }
            }
        }

        public LineasCredito ConsultarGarantiaDOCS(string pId, string linea, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineasCredito entidad = new LineasCredito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DOCGARANTIALINEA WHERE tipo_documento = '" + pId.ToString() + "' and COD_LINEA_CREDITO= '" + linea.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            if (resultado["PLANTILLA"] != DBNull.Value) entidad.plantilla = Convert.ToString(resultado["PLANTILLA"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarGarantiaDOCS", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ConsultarGarantiacompleta(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstentidad = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DOCGARANTIALINEA";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["TIPO_DOCUMENTO"] != DBNull.Value) entidad.tipo_documento = Convert.ToInt32(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            if (resultado["PLANTILLA"] != DBNull.Value) entidad.plantilla = Convert.ToString(resultado["PLANTILLA"]);
                            lstentidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarGarantiacompleta", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region Procesos

        public List<ProcesoLineaCredito> ListarProcesos(ProcesoLineaCredito pProceso, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ProcesoLineaCredito> lstData = new List<ProcesoLineaCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (filtro == "RECUPERA_DATOS")
                        {
                            sql = "Select t.codtipoproceso, t.descripcion, l.cod_procesolinea "
                                     + " From tipoprocesos t Left Join lineascredito_proceso l On t.codtipoproceso = l.cod_proceso and l.cod_linea_credito = '" + pProceso.cod_lineacredito + "' "
                                     + " Where t.tipo_proceso = 1 ";
                        }
                        else
                        {
                            sql = "Select t.codtipoproceso, t.descripcion, '' As cod_procesolinea "
                                    + " From tipoprocesos t"
                                    + " Where t.tipo_proceso = 1 ";
                        }
                        if (filtro == "PARAMETROS")
                        {
                            sql = @"Select t.cod_parametro CODTIPOPROCESO, t.descripcion, p.valor COD_PROCESOLINEA
                                    From tipoparametroslinea t Left Join parametros_linea p On t.cod_parametro = p.cod_parametro And p.cod_linea_credito = '" + pProceso.cod_lineacredito + @"
                                    Order by 1; ";
                        }
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesoLineaCredito entidad = new ProcesoLineaCredito();
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.codtipoproceso = Convert.ToInt32(resultado["CODTIPOPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_PROCESOLINEA"] != DBNull.Value) entidad.cod_procesolinea = Convert.ToInt32(resultado["COD_PROCESOLINEA"]);
                            if (entidad.cod_procesolinea != 0)
                                entidad.checkbox = 1;

                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarProcesos", ex);
                        return null;
                    }
                }
            }
        }

        //GRABAR PROCESO LINEA
        public ProcesoLineaCredito CrearProcesoLinea(ProcesoLineaCredito pFAB, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_procesolinea = cmdTransaccionFactory.CreateParameter();
                        pcod_procesolinea.ParameterName = "p_cod_procesolinea";
                        pcod_procesolinea.Value = pFAB.cod_procesolinea;
                        pcod_procesolinea.Direction = ParameterDirection.Input;
                        pcod_procesolinea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_procesolinea);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pFAB.cod_lineacredito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "p_cod_proceso";
                        pcod_proceso.Value = pFAB.cod_procesolinea;
                        pcod_proceso.Direction = ParameterDirection.Input;
                        pcod_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PROCESOLIN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFAB;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearProcesoLinea", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarProcesoLinea(string pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from LINEASCREDITO_PROCESO where COD_LINEA_CREDITO = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarProcesoLinea", ex);
                    }
                }
            }
        }


        #endregion

        #region PRIORIDADES

        public LineasCredito CrearPrioridad_Linea(LineasCredito pPrioridad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pPrioridad.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPrioridad.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pnumero = cmdTransaccionFactory.CreateParameter();
                        pnumero.ParameterName = "p_numero";
                        pnumero.Value = pPrioridad.numero;
                        pnumero.Direction = ParameterDirection.Input;
                        pnumero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PRIORIDADL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPrioridad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearPrioridad_Linea", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPrioridad_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from PRIORIDAD_LIN where COD_LINEA_CREDITO = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarPrioridad_Linea", ex);
                    }
                }
            }
        }


        public List<LineasCredito> ConsultarPrioridad_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstData = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.cod_atr, a.nombre, p.numero From Atributos A Left Join Prioridad_Lin P On A.Cod_Atr = P.Cod_Atr And cod_linea_credito = '" + pId.ToString() + "'"
                                         + " ORDER  BY 1";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["COD_ATR"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUMERO"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["NUMERO"]);
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarPrioridad_Linea", ex);
                        return null;
                    }
                }
            }
        }


        #endregion

        #region DESTINCACION
        public LineasCredito CrearDestino_Linea(LineasCredito pDestino, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "P_COD_LINEA_CREDITO";
                        pcod_linea_credito.Value = pDestino.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = pDestino.cod_destino;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);


                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DESTINOL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDestino;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearDestino_Linea", ex);
                        return null;
                    }
                }
            }
        }

        public List<LineasCredito> ConsultarDestinacion_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineasCredito> lstData = new List<LineasCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from LINEACRED_DESTINACION where COD_LINEA_CREDITO ='" + pId.ToString() + "'"
                                         + " ORDER  BY 1";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineasCredito entidad = new LineasCredito();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            lstData.Add(entidad);
                            if (entidad.cod_destino != 0)
                                entidad.seleccionar = 1;
                            else
                                entidad.seleccionar = 0;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarDestinacion_Linea", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarDestinacion_Linea(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"delete from LINEACRED_DESTINACION where COD_LINEA_CREDITO = '" + pId.ToString() + "'";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "EliminarDestinacion_Linea", ex);
                    }
                }
            }
        }


        public List<LineaCred_Destinacion> ListaDestinacionCredito(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<LineaCred_Destinacion> lstData = new List<LineaCred_Destinacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT L.*, d.descripcion FROM LINEACRED_DESTINACION L INNER JOIN destinacion D ON d.cod_destino = l.cod_destino
                                       where l.cod_linea_credito = " + pId.ToString() + " ORDER  BY d.descripcion";
                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        LineaCred_Destinacion entidad;
                        while (resultado.Read())
                        {
                            entidad = new LineaCred_Destinacion();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToInt32(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListaDestinacionCredito", ex);
                        return null;
                    }
                }
            }
        }


        #endregion

        #region parametros

        public List<ProcesoLineaCredito> ListarParametrosLinea(string codLienaCredito, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ProcesoLineaCredito> lstData = new List<ProcesoLineaCredito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select t.cod_parametro CODTIPOPROCESO, t.descripcion, p.valor COD_PROCESOLINEA
                                    From tipoparametroslinea t Left Join parametros_linea p On t.cod_parametro = p.cod_parametro And p.cod_linea_credito = " + codLienaCredito + @"
                                    Order by 1 ";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesoLineaCredito entidad = new ProcesoLineaCredito();
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.codtipoproceso = Convert.ToInt32(resultado["CODTIPOPROCESO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_PROCESOLINEA"] != DBNull.Value) entidad.cod_lineacredito = Convert.ToString(resultado["COD_PROCESOLINEA"]);

                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarParametrosLinea", ex);
                        return null;
                    }
                }
            }
        }
        public bool CrearParametroLinea(ProcesoLineaCredito pFAB, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_procesolinea = cmdTransaccionFactory.CreateParameter();
                        pcod_procesolinea.ParameterName = "P_Cod_Linea_Credito";
                        pcod_procesolinea.Value = pFAB.cod_lineacredito;
                        pcod_procesolinea.Direction = ParameterDirection.Input;
                        pcod_procesolinea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_procesolinea);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "P_Cod_Parametro";
                        pcod_linea_credito.Value = pFAB.codtipoproceso;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "p_Valor";
                        if (pFAB.Valor == null && pFAB.Valor.Trim() == "")
                            pcod_proceso.Value = DBNull.Value;
                        else
                            pcod_proceso.Value = pFAB.Valor;
                        pcod_proceso.Direction = ParameterDirection.Input;
                        pcod_proceso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PARAMETLIN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "CrearParametroLinea", ex);
                        return false;
                    }
                }
            }
        }

        #endregion

        public List<Int64> RangosLinea(string pCodLinea, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Int64> lista = new List<Int64>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_rango_atr From RangosAtributos Where cod_linea_credito = '" + pCodLinea + "' Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Int64 cod_rango = Int64.MinValue;
                            if (resultado["COD_RANGO_ATR"] != DBNull.Value) cod_rango = Convert.ToInt64(resultado["COD_RANGO_ATR"]);
                            if (cod_rango == Int64.MinValue)
                                lista.Add(cod_rango);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch 
                    {
                        return lista;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }

        public bool CodRangoAtr(Int64 pCodRango, string pCodLinea, Int64 pCodPersona, decimal pMonto, decimal pPlazo, Usuario pUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select tipo_tope, minimo, maximo From RangosTopes Where cod_rango_atr = " + pCodRango + " And cod_linea_credito = '" + pCodLinea + "' Order by 1";
                                                
                        AbrirConexion(dbConnectionFactory, cmdTransaccionFactory, connection, pUsuario);
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        bool bEncontro = true;

                        while (resultado.Read())
                        {
                            Int64? tipo_tope = null;
                            string minimo = "", maximo = "";
                            if (resultado["TIPO_TOPE"] != DBNull.Value) tipo_tope = Convert.ToInt64(resultado["TIPO_TOPE"]);
                            if (resultado["MINIMO"] != DBNull.Value) minimo = Convert.ToString(resultado["MINIMO"]);
                            if (resultado["MAXIMO"] != DBNull.Value) maximo = Convert.ToString(resultado["MAXIMO"]);
                            // Verificar máximos
                            if (minimo != null || maximo != null)
                            {
                                if (minimo == null) minimo = maximo;
                                if (maximo == null) maximo = minimo;
                            }
                            // Verificar el tipo de tope
                            if (tipo_tope == 1)     // Fecha de solicitud
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 2)  // Plazo
                            {
                                if (minimo != null && maximo != null)
                                {
                                    if (pPlazo <= Convert.ToDecimal(minimo) && pPlazo > Convert.ToDecimal(maximo))
                                        bEncontro = false;
                                }
                            }
                            else if (tipo_tope == 3)  // Monto
                            {
                                if (minimo != null && maximo != null)
                                {
                                    if (pMonto < Convert.ToDecimal(minimo) && pMonto > Convert.ToDecimal(maximo))
                                        bEncontro = false;
                                }
                            }
                            else if (tipo_tope == 6)  // Antiguedad
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 7)  // Plazo en días
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 8)  // Veces de los Aportes/Ahorros
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 9)  // Salarios Mínimos Legales Mensuales Vigentes
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 10) // Capacidad de Endeudamiento
                            {
                                minimo = "";
                            }
                            else if (tipo_tope == 11) // Veces los Aportes y ahorros Permanentes
                            {
                                minimo = "";
                            }
                        }
                        if (bEncontro)
                            return true;
                    }
                    catch 
                    {                        
                        return false;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    return false;
                }
            }
        }

        public LineasCredito ConsultarTasaInteresLineaCredito(string pCodLinea, Int64 pCodPersona, decimal pMonto, decimal pPlazo, Usuario pUsuario)
        {
            List<Int64> lstRangos = RangosLinea(pCodLinea, pUsuario);
            if (lstRangos.Count <= 0)
                return null;
            Int64 codRango = 0;
            foreach (Int64 _codRango in lstRangos)
            {
                if (CodRangoAtr(_codRango, pCodLinea, pCodPersona, pMonto, pPlazo, pUsuario))
                {
                    codRango = _codRango;
                    break;
                }
            }

            DbDataReader resultado;
            LineasCredito linea = new LineasCredito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.tasa, a.tipo_tasa, t.nombre
                                        From atributoslinea a Left Join tipotasa t on a.tipo_tasa = t.cod_tipo_tasa
                                        Where cod_linea_credito = '" + pCodLinea + "' " + (codRango == 0 ? "": " And cod_rango_atr = " + codRango) + " And cod_atr = AtrCorriente()";

                        connection.Open(); GuardarConexion(cmdTransaccionFactory, dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario); dbConnectionFactory.CerrarConexion(connection); connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TASA"] != DBNull.Value) linea.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) linea.tipotasa = Convert.ToInt64(resultado["TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) linea.descripcion_tipo_tasa = Convert.ToString(resultado["NOMBRE"]);
                        }

                        return linea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ConsultarTasaInteresLineaCredito", ex);
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }





    }
}