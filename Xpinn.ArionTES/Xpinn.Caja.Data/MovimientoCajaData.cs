using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla MOVIMIENTOCAJA
    /// </summary>
    public class MovimientoCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla MOVIMIENTOCAJA
        /// </summary>
        public MovimientoCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "pcodusuario";
                        pcodusuario.Value = pMovimientoCaja.cod_usuario;
                        pcodusuario.DbType = DbType.Int32;
                        pcodusuario.Size = 8;
                        pcodusuario.Direction = ParameterDirection.Input;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pMovimientoCaja.fechaCierre;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Size = 7;

                        cmdTransaccionFactory.Parameters.Add(pcodusuario);
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ARQUEOCAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                      //  List<Xpinn.Caja.Entities.MovimientoCaja> tempArqueoLst = new List<Xpinn.Caja.Entities.MovimientoCaja>();
                        //Xpinn.Caja.Entities.MovimientoCaja tempArqueoReg = new Xpinn.Caja.Entities.MovimientoCaja();

                       // tempArqueoReg.cod_cajero = pMovimientoCaja.cod_usuario;
                       // tempArqueoLst = ListarArqueoTemporalTesoreria(tempArqueoReg, pUsuario);
                        // Una vez generado el arqueo se consultan los datos de la tabla temporal

                        List<Xpinn.Caja.Entities.MovimientoCaja> tempArqueoLst = new List<Xpinn.Caja.Entities.MovimientoCaja>();
                        //Xpinn.Caja.Entities.MovimientoCaja tempArqueoReg = new Xpinn.Caja.Entities.MovimientoCaja();
                        //tempArqueoReg.cod_cajero = pMovimientoCaja.cod_cajero;
                        //tempArqueoLst = ListarArqueoTemporal(tempArqueoReg, pUsuario);
                        DbDataReader resultado = default(DbDataReader);
                        string sql = "SELECT orden, cod_caja, cod_cajero, cod_moneda, concepto, efectivo, cheque, total, cod_usuario, nom_moneda from TEMPARQUEOCAJA where cod_cajero = " + pMovimientoCaja.cod_cajero + " order by cod_moneda, orden asc";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToDecimal(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToDecimal(resultado["cheque"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);

                            tempArqueoLst.Add(entidad);
                        }
                        resultado.Dispose();

                        decimal efectivo = 0;
                        decimal cheque = 0;
                        decimal consignacion = 0;
                        decimal datafono = 0;
                        decimal total = 0;

                        foreach (MovimientoCaja fil in tempArqueoLst)
                        {
                            if (fil.orden == 1 || fil.orden == 2 || fil.orden == 3 || fil.orden == 4)
                            {
                                efectivo = efectivo + fil.efectivo;
                                cheque = cheque + fil.cheque;
                                datafono = datafono + fil.datafono;
                                consignacion = consignacion + fil.consignacion;
                                total = total + fil.total;
                            }
                            else
                            {
                                if (fil.orden != 8)
                                {
                                    efectivo = efectivo - fil.efectivo;
                                    cheque = cheque - fil.cheque;
                                    datafono = datafono - fil.datafono;
                                    consignacion = consignacion - fil.consignacion;
                                    total = total - fil.total;
                                }
                                else
                                {
                                    // Actualiza línea de totales del arqueo
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                                    pcod_usuario.ParameterName = "pcodigocajero";
                                    pcod_usuario.Value = fil.cod_cajero;
                                    pcod_usuario.DbType = DbType.Int32;
                                    pcod_usuario.Size = 8;
                                    pcod_usuario.Direction = ParameterDirection.Input;

                                    DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcod_moneda.ParameterName = "pcodigomoneda";
                                    pcod_moneda.Value = fil.cod_moneda;
                                    pcod_moneda.DbType = DbType.Int32;
                                    pcod_moneda.Size = 8;
                                    pcod_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pval_efectivo = cmdTransaccionFactory.CreateParameter();
                                    pval_efectivo.ParameterName = "pefectivo";
                                    pval_efectivo.Value = efectivo;
                                    pval_efectivo.DbType = DbType.Int64;
                                    pval_efectivo.Size = 8;
                                    pval_efectivo.Direction = ParameterDirection.Input;

                                    DbParameter pval_cheque = cmdTransaccionFactory.CreateParameter();
                                    pval_cheque.ParameterName = "pcheque";
                                    pval_cheque.Value = cheque;
                                    pval_cheque.DbType = DbType.Int64;
                                    pval_cheque.Size = 8;
                                    pval_cheque.Direction = ParameterDirection.Input;

                                    DbParameter pval_consignacion = cmdTransaccionFactory.CreateParameter();
                                    pval_consignacion.ParameterName = "pval_consignacion";
                                    pval_consignacion.Value = consignacion;
                                    pval_consignacion.DbType = DbType.Int64;
                                    pval_consignacion.Size = 8;
                                    pval_consignacion.Direction = ParameterDirection.Input;

                                    DbParameter pval_datafono = cmdTransaccionFactory.CreateParameter();
                                    pval_datafono.ParameterName = "pval_datafono";
                                    pval_datafono.Value = datafono;
                                    pval_datafono.DbType = DbType.Int64;
                                    pval_datafono.Size = 8;
                                    pval_datafono.Direction = ParameterDirection.Input;

                                    DbParameter pval_total = cmdTransaccionFactory.CreateParameter();
                                    pval_total.ParameterName = "ptotal";
                                    pval_total.Value = total;
                                    pval_total.DbType = DbType.Int64;
                                    pval_total.Size = 8;
                                    pval_total.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcod_usuario);
                                    cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pval_efectivo);
                                    cmdTransaccionFactory.Parameters.Add(pval_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pval_consignacion);
                                    cmdTransaccionFactory.Parameters.Add(pval_datafono);
                                    cmdTransaccionFactory.Parameters.Add(pval_total);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TEMPARQUEO_U";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }
                            }
                        }

                        return pMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "CrearTempArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro en la tabla MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        /// Generar el arqueo y dejar los registros en una tabla temporal.

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodcaja";
                        pcod_caja.Value = pMovimientoCaja.cod_caja;
                        pcod_caja.DbType = DbType.Int16;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodcajero";
                        pcod_cajero.Value = pMovimientoCaja.cod_cajero;
                        pcod_cajero.DbType = DbType.Int16;
                        pcod_cajero.Size = 8;
                        pcod_cajero.Direction = ParameterDirection.Input;


                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pMovimientoCaja.fechaCierre;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Size = 7;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TEMP_ARQUEOCAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // Una vez generado el arqueo se consultan los datos de la tabla temporal

                        List<Xpinn.Caja.Entities.MovimientoCaja> tempArqueoLst = new List<Xpinn.Caja.Entities.MovimientoCaja>();
                        //Xpinn.Caja.Entities.MovimientoCaja tempArqueoReg = new Xpinn.Caja.Entities.MovimientoCaja();
                        //tempArqueoReg.cod_cajero = pMovimientoCaja.cod_cajero;
                        //tempArqueoLst = ListarArqueoTemporal(tempArqueoReg, pUsuario);
                        DbDataReader resultado = default(DbDataReader);
                        string sql = "SELECT orden, cod_caja, cod_cajero, cod_moneda, concepto,CONSIGNACION ,efectivo, cheque, total,datafono, cod_usuario, nom_moneda from TEMPARQUEOCAJA where cod_cajero = " + pMovimientoCaja.cod_cajero + " order by cod_moneda, orden asc";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToDecimal(resultado["efectivo"]);
                            if (resultado["datafono"] != DBNull.Value) entidad.datafono = Convert.ToDecimal(resultado["datafono"]);
                            if (resultado["consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToDecimal(resultado["consignacion"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToDecimal(resultado["cheque"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);

                            tempArqueoLst.Add(entidad);
                        }
                        resultado.Dispose();

                        // Calcular los valores a pagar por cada forma de pago
                        decimal efectivo = 0;
                        decimal cheque = 0;
                        decimal total = 0;
                        decimal datafono = 0;
                        foreach (MovimientoCaja fil in tempArqueoLst)
                        {
                            if (fil.orden == 1 || fil.orden == 2 || fil.orden == 3 || fil.orden == 4)
                            {
                                datafono = datafono + fil.datafono;
                                efectivo = efectivo + fil.efectivo;
                                cheque = cheque + fil.cheque;
                                total = total + fil.total;
                            }
                            else
                            {
                                if (fil.orden != 8)
                                {
                                    datafono = datafono + fil.datafono;
                                    efectivo = efectivo - fil.efectivo;
                                    cheque = cheque - fil.cheque;
                                    total = total - fil.total;
                                }
                                else
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                                    pcod_usuario.ParameterName = "pcodigocajero";
                                    pcod_usuario.Value = fil.cod_cajero;
                                    pcod_usuario.DbType = DbType.Int16;
                                    pcod_usuario.Size = 8;
                                    pcod_usuario.Direction = ParameterDirection.Input;

                                    DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcod_moneda.ParameterName = "pcodigomoneda";
                                    pcod_moneda.Value = fil.cod_moneda;
                                    pcod_moneda.DbType = DbType.Int16;
                                    pcod_moneda.Size = 8;
                                    pcod_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pval_efectivo = cmdTransaccionFactory.CreateParameter();
                                    pval_efectivo.ParameterName = "pefectivo";
                                    pval_efectivo.Value = efectivo;
                                    pval_efectivo.DbType = DbType.Int64;
                                    pval_efectivo.Size = 8;
                                    pval_efectivo.Direction = ParameterDirection.Input;

                                    DbParameter pval_cheque = cmdTransaccionFactory.CreateParameter();
                                    pval_cheque.ParameterName = "pcheque";
                                    pval_cheque.Value = cheque;
                                    pval_cheque.DbType = DbType.Int64;
                                    pval_cheque.Size = 8;
                                    pval_cheque.Direction = ParameterDirection.Input;

                                    DbParameter pval_total = cmdTransaccionFactory.CreateParameter();
                                    pval_total.ParameterName = "ptotal";
                                    pval_total.Value = total;
                                    pval_total.DbType = DbType.Int64;
                                    pval_total.Size = 8;
                                    pval_total.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcod_usuario);
                                    cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pval_efectivo);
                                    cmdTransaccionFactory.Parameters.Add(pval_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pval_total);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TEMPARQUEO_U";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }
                            }
                        }


                        connection.Close();
                        return pMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "CrearTempArqueoCaja", ex);
                        return null;
                    }
                }
            }
        }

        
        /// <summary>
        /// Modifica un registro en la tabla MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad MovimientoCaja modificada</returns>
        public MovimientoCaja ModificarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INGEGR = cmdTransaccionFactory.CreateParameter();
                        pCOD_INGEGR.ParameterName = param + "COD_INGEGR";
                        pCOD_INGEGR.Value = pMovimientoCaja.cod_movimiento;

                        DbParameter pCOD_OPE = cmdTransaccionFactory.CreateParameter();
                        pCOD_OPE.ParameterName = param + "COD_OPE";
                        pCOD_OPE.Value = pMovimientoCaja.cod_ope;

                        DbParameter pFEC_OPE = cmdTransaccionFactory.CreateParameter();
                        pFEC_OPE.ParameterName = param + "FEC_OPE";
                        pFEC_OPE.Value = pMovimientoCaja.fec_ope;

                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pMovimientoCaja.cod_caja;

                        DbParameter pCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJERO.ParameterName = param + "COD_CAJERO";
                        pCOD_CAJERO.Value = pMovimientoCaja.cod_cajero;

                        DbParameter pTIPO_MOV = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MOV.ParameterName = param + "TIPO_MOV";
                        pTIPO_MOV.Value = pMovimientoCaja.tipo_mov;

                        DbParameter pCOD_TIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pCOD_TIPO_PAGO.ParameterName = param + "COD_TIPO_PAGO";
                        pCOD_TIPO_PAGO.Value = pMovimientoCaja.cod_tipo_pago;

                        DbParameter pCOD_BANCO = cmdTransaccionFactory.CreateParameter();
                        pCOD_BANCO.ParameterName = param + "COD_BANCO";
                        pCOD_BANCO.Value = pMovimientoCaja.cod_banco;

                        DbParameter pNUM_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pNUM_DOCUMENTO.ParameterName = param + "NUM_DOCUMENTO";
                        pNUM_DOCUMENTO.Value = pMovimientoCaja.num_documento;

                        DbParameter pTIPO_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_DOCUMENTO.ParameterName = param + "TIPO_DOCUMENTO";
                        pTIPO_DOCUMENTO.Value = pMovimientoCaja.tipo_documento;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = param + "COD_MONEDA";
                        pCOD_MONEDA.Value = pMovimientoCaja.cod_moneda;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = param + "VALOR";
                        pVALOR.Value = pMovimientoCaja.valor;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = param + "ESTADO";
                        pESTADO.Value = pMovimientoCaja.estado;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INGEGR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_OPE);
                        cmdTransaccionFactory.Parameters.Add(pFEC_OPE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_MOV);
                        cmdTransaccionFactory.Parameters.Add(pCOD_TIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BANCO);
                        cmdTransaccionFactory.Parameters.Add(pNUM_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_MOVIMIENTOCAJA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pMovimientoCaja, pUsuario, pMovimientoCaja.cod_movimiento, "MOVIMIENTOCAJA", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        return pMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ModificarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Temporal para Arqueo MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de MOVIMIENTOCAJA</param>
        public void EliminarTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pMovimientoCaja.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pMovimientoCaja.cod_cajero;
                        pcod_cajero.DbType = DbType.Int64;
                        pcod_cajero.Size = 8;
                        pcod_cajero.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TEMP_ARQUEOCAJA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "InsertarMovimientoCaja", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Elimina un registro en la tabla Temporal para Arqueo MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de MOVIMIENTOCAJA</param>
        public void EliminarTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "pcodusuario";
                        pcodusuario.Value = pMovimientoCaja.cod_usuario;
                        pcodusuario.DbType = DbType.Int64;
                        pcodusuario.Size = 8;
                        pcodusuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcodusuario);
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TEMP_ARQCAJA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "EliminarTempArqueoTesoreria", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla MOVIMIENTOCAJA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla MOVIMIENTOCAJA</param>
        /// <returns>Entidad MovimientoCaja consultado</returns>
        public MovimientoCaja ConsultarMovimientoCaja(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            MovimientoCaja entidad = new MovimientoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INGEGR = cmdTransaccionFactory.CreateParameter();
                        pCOD_INGEGR.ParameterName = param + "COD_INGEGR";
                        pCOD_INGEGR.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INGEGR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_&Modulo&_MOVIMIENTOCAJA_R";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_INGEGR"] == DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["COD_INGEGR"]);
                            if (resultado["COD_OPE"] == DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["FEC_OPE"] == DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["FEC_OPE"]);
                            if (resultado["COD_CAJA"] == DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] == DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["TIPO_MOV"] == DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO_MOV"]);
                            if (resultado["COD_TIPO_PAGO"] == DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                            if (resultado["COD_BANCO"] == DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["NUM_DOCUMENTO"] == DBNull.Value) entidad.num_documento = Convert.ToString(resultado["NUM_DOCUMENTO"]);
                            if (resultado["TIPO_DOCUMENTO"] == DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["TIPO_DOCUMENTO"]);
                            if (resultado["COD_MONEDA"] == DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR"] == DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["ESTADO"] == DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ConsultarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pMovimientoCaja.filtro = pMovimientoCaja.filtro == null ? " " : pMovimientoCaja.filtro;
                        string sql = @"SELECT a.cod_movimiento codmovimiento, a.cod_ope codope, a.fec_ope fecope, a.cod_caja codcaja, a.cod_cajero codcajero, a.tipo_mov tipomov, a.cod_tipo_pago codtipopago, 
                                        a.cod_banco codbanco, a.num_documento numdocumento, a.tipo_documento tipodocumento, a.cod_moneda codmoneda, a.valor valore, a.estado estadoi, 
                                        (select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda, 
                                        (select x.nombrebanco from bancos x where x.cod_banco=a.cod_banco) nombanco  FROM  MOVIMIENTOCAJA a WHERE a.cod_caja = " + pMovimientoCaja.cod_caja + 
                                       " And a.cod_cajero = " + pMovimientoCaja.cod_cajero + " And a.cod_moneda=" + pMovimientoCaja.cod_moneda + " And a.cod_tipo_pago=" + pMovimientoCaja.cod_tipo_pago + 
                                       " And a.estado = " + pMovimientoCaja.estado + " And a.tipo_mov = '" + pMovimientoCaja.tipo_mov + "' " + pMovimientoCaja.filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["codmovimiento"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["codmovimiento"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecope"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["codtipopago"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["codtipopago"]);
                            if (resultado["codbanco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["codbanco"]);
                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["numdocumento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["numdocumento"]);
                            if (resultado["tipodocumento"] != DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["tipodocumento"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["codmoneda"]);
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nommoneda"]);
                            if (resultado["valore"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valore"]);
                            if (resultado["estadoi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoi"]);


                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.cod_movimiento codmovimiento, a.cod_ope codope, a.fec_ope fecope, a.cod_caja codcaja, a.cod_cajero codcajero, a.tipo_mov tipomov, a.cod_tipo_pago codtipopago, 
                                        a.cod_banco codbanco, a.num_documento numdocumento, a.tipo_documento tipodocumento, a.cod_moneda codmoneda, a.valor valore, a.estado estadoi, 
                                        (select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda, 
                                        (select x.nombrebanco from bancos x where x.cod_banco=a.cod_banco) nombanco  FROM  MOVIMIENTOCAJA a WHERE a.cod_cajero = " + pMovimientoCaja.cod_cajero + " And a.cod_moneda=" + pMovimientoCaja.cod_moneda + " And a.cod_tipo_pago=" + pMovimientoCaja.cod_tipo_pago +
                                       " And a.estado = " + pMovimientoCaja.estado + " And a.tipo_mov = '" + pMovimientoCaja.tipo_mov + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["codmovimiento"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["codmovimiento"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecope"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["codtipopago"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["codtipopago"]);
                            if (resultado["codbanco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["codbanco"]);
                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["numdocumento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["numdocumento"]);
                            if (resultado["tipodocumento"] != DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["tipodocumento"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["codmoneda"]);
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nommoneda"]);
                            if (resultado["valore"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valore"]);
                            if (resultado["estadoi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoi"]);


                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarCheques(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.cod_movimiento codmovimiento,a.cod_ope codope,a.fec_ope fecope,a.cod_caja codcaja,a.cod_cajero codcajero,a.tipo_mov tipomov,a.cod_tipo_pago codtipopago ,a.cod_banco codbanco,a.num_documento numdocumento,a.tipo_documento tipodocumento,a.cod_moneda codmoneda,a.valor valore ,a.estado estadoi,(select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda,(select x.nombrebanco from bancos x where x.cod_banco=a.cod_banco) nombanco FROM  MOVIMIENTOCAJA a WHERE a.estado=0 and a.COD_CAJA=" + pMovimientoCaja.cod_caja + " and a.COD_CAJERO=" + pMovimientoCaja.cod_cajero + " and a.cod_tipo_pago=" + pMovimientoCaja.cod_tipo_pago;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["codmovimiento"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["codmovimiento"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecope"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["codtipopago"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["codtipopago"]);
                            if (resultado["codbanco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["codbanco"]);
                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["numdocumento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["numdocumento"]);
                            if (resultado["tipodocumento"] != DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["tipodocumento"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["codmoneda"]);
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nommoneda"]);
                            if (resultado["valore"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valore"]);
                            if (resultado["estadoi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoi"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldos(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " Select orden, cod_moneda,concepto,efectivo,cheque,Consignacion,datafono, total, nom_moneda from TEMPARQUEOCAJA where cod_caja=" + pMovimientoCaja.cod_caja + " and cod_cajero=" + pMovimientoCaja.cod_cajero + " order by cod_moneda,orden asc ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["datafono"] != DBNull.Value) entidad.datafono = Convert.ToInt64(resultado["datafono"]);
                            if (resultado["consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["consignacion"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);

                            if (int.Parse(resultado["orden"].ToString()) == 1)
                                if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldosTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " Select orden, cod_moneda, concepto, efectivo, cheque, consignacion, datafono, total, nom_moneda From TEMPARQUEOCAJA Where cod_cajero = " + pMovimientoCaja.cod_usuario + " Order by cod_moneda, orden asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["consignacion"]);
                            if (resultado["datafono"] != DBNull.Value) entidad.datafono = Convert.ToInt64(resultado["datafono"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);

                            if (int.Parse(resultado["orden"].ToString()) == 1)
                                if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarSaldosTesoreria", ex);
                        return null;
                    }
                }
            }
        }


        public List<MovimientoCaja> Listararqueo(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT FECHA, COD_CAJA, COD_CAJERO, CONCEPTO, EFECTIVO, CHEQUE, TOTAL," +
                        "(select nombre from usuarios u where u.codusuario = (select cod_persona from cajero c where cod_cajero = a.COD_CAJERO)) as nombre," +
                        "(select x.descripcion from tipomoneda x where x.cod_moneda=a.moneda) nommoneda" +
                        " FROM ARQUEOCAJA_DETALLE a where cod_cajero=" + cod_cajero + "  and to_char(fecha,'dd/MM/yyyy') = '" + fecha.ToShortDateString() + "' and concepto = 'Saldo Final'order by fecha asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["fecha"] != DBNull.Value) entidad.fechaCierre = Convert.ToDateTime(resultado["fecha"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["EFECTIVO"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["EFECTIVO"]);
                            if (resultado["CHEQUE"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["CHEQUE"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);



                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<MovimientoCaja> Listararqueodetalle(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "SELECT id_arqueo, FECHA, COD_CAJA, COD_CAJERO, CONCEPTO, EFECTIVO, CHEQUE, TOTAL," +
                        "(select nombre from usuarios u where u.codusuario = (select cod_persona from cajero c where c.cod_cajero = a.cod_cajero)) as nombre," +
                        "(select x.descripcion from tipomoneda x where x.cod_moneda=a.moneda) nommoneda" +
                        " FROM ARQUEOCAJA_DETALLE a where cod_cajero=" + cod_cajero + "  and to_char(fecha,'dd/MM/yyyy') = '" + fecha.ToShortDateString() + "'" + " order by id_arqueo asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["fecha"] != DBNull.Value) entidad.fechaCierre = Convert.ToDateTime(resultado["fecha"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["EFECTIVO"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["EFECTIVO"]);
                            if (resultado["CHEQUE"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["CHEQUE"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["TOTAL"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);


                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<MovimientoCaja> Listarcomprobante(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " Select orden, cod_moneda,concepto,efectivo,cheque, total, nom_moneda from TEMPARQUEOCAJA where cod_caja=" + pMovimientoCaja.cod_caja + " and cod_cajero=" + pMovimientoCaja.cod_cajero + "and concepto='Saldo Final' order by cod_moneda,orden desc ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);

                            if (int.Parse(resultado["orden"].ToString()) == 1)
                                if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesPendientes(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.cod_movimiento codmovimiento, a.cod_ope codope, a.fec_ope fecope, a.cod_caja codcaja, a.cod_cajero codcajero, a.tipo_mov tipomov, a.cod_tipo_pago codtipopago, a.cod_banco codbanco, a.num_documento numdocumento, a.tipo_documento tipodocumento, a.cod_moneda codmoneda, a.valor valore, a.estado estadoi, (select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda, (select x.nombrebanco from bancos x where x.cod_banco=a.cod_banco) nombanco, (Select (x.primer_nombre||' '||x.segundo_nombre||' '||x.primer_apellido||' '||x.segundo_apellido) from persona x where x.cod_persona=a.cod_persona) titular " +
                                     "FROM  MOVIMIENTOCAJA a WHERE a.estado = 0 AND a.cod_caja = " + pMovimientoCaja.cod_caja + " AND a.COD_CAJERO = " + pMovimientoCaja.cod_cajero + " AND a.cod_tipo_pago = 2 ORDER BY a.cod_movimiento asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["codmovimiento"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["codmovimiento"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecope"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["codtipopago"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["codtipopago"]);
                            if (resultado["codbanco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["codbanco"]);
                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["numdocumento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["numdocumento"]);
                            if (resultado["tipodocumento"] != DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["tipodocumento"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["codmoneda"]);
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nommoneda"]);
                            if (resultado["valore"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valore"]);
                            if (resultado["estadoi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoi"]);
                            if (resultado["titular"] != DBNull.Value) entidad.titular = Convert.ToString(resultado["titular"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesAsignados(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.cod_movimiento codmovimiento, a.cod_ope codope, a.fec_ope fecope, a.cod_caja codcaja, a.cod_cajero codcajero, a.tipo_mov tipomov, a.cod_tipo_pago codtipopago, a.cod_banco codbanco, a.num_documento numdocumento, a.tipo_documento tipodocumento, a.cod_moneda codmoneda, a.valor valore, a.estado estadoi, (select x.descripcion from tipomoneda x where x.cod_moneda=a.cod_moneda) nommoneda, (select x.nombrebanco from bancos x where x.cod_banco=a.cod_banco) nombanco, (Select (x.primer_nombre||' '||x.segundo_nombre||' '||x.primer_apellido||' '||x.segundo_apellido) from persona x where x.cod_persona=a.cod_persona) titular " +
                                     "FROM  MOVIMIENTOCAJA a WHERE a.estado = 1 AND a.cod_caja = " + pMovimientoCaja.cod_caja + " AND a.cod_cajero = " + pMovimientoCaja.cod_cajero + " AND a.cod_tipo_pago = 2  ORDER BY a.cod_movimiento ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["codmovimiento"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["codmovimiento"]);
                            if (resultado["codope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["codope"]);
                            if (resultado["fecope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fecope"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipomov"]);
                            if (resultado["codtipopago"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["codtipopago"]);
                            if (resultado["codbanco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["codbanco"]);
                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["numdocumento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["numdocumento"]);
                            if (resultado["tipodocumento"] != DBNull.Value) entidad.tipo_documento = Convert.ToString(resultado["tipodocumento"]);
                            if (resultado["codmoneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["codmoneda"]);
                            if (resultado["nommoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nommoneda"]);
                            if (resultado["valore"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valore"]);
                            if (resultado["estadoi"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estadoi"]);
                            if (resultado["titular"] != DBNull.Value) entidad.titular = Convert.ToString(resultado["titular"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TempArqueoCaja dados unos filtros
        /// </summary>
        /// <param name="TempArqueoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarArqueoTemporal(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT orden, cod_caja,cod_cajero,cod_moneda, concepto,efectivo,cheque, total, cod_usuario, nom_moneda from TEMPARQUEOCAJA where cod_cajero=" + pMovimientoCaja.cod_cajero + " order by cod_moneda,orden asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarArqueoTemporal", ex);
                        return null;
                    }
                }
            }
        }





        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TempArqueoCaja dados unos filtros
        /// </summary>
        /// <param name="TempArqueoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarArqueoTemporalTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT orden, cod_caja,cod_cajero,cod_moneda, concepto,efectivo,cheque,consignacion,datafono, total, cod_usuario, nom_moneda from TEMPARQUEOCAJA where cod_cajero=" + pMovimientoCaja.cod_cajero + " order by cod_moneda,orden asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["orden"] != DBNull.Value) entidad.orden = Convert.ToInt64(resultado["orden"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["cod_caja"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["cod_moneda"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["cod_moneda"]);
                            if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"]);
                            if (resultado["efectivo"] != DBNull.Value) entidad.efectivo = Convert.ToInt64(resultado["efectivo"]);
                            if (resultado["cheque"] != DBNull.Value) entidad.cheque = Convert.ToInt64(resultado["cheque"]);
                            if (resultado["consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["consignacion"]);
                            if (resultado["datafono"] != DBNull.Value) entidad.datafono = Convert.ToInt64(resultado["datafono"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["total"]);
                            if (resultado["cod_usuario"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["cod_usuario"]);

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarArqueoTemporalTesoreria", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesCanje(string[] pdata, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select c.cod_ingegr cod_consignacion , m.cod_ope, m.fec_ope, p.identificacion, p.nombre,bs.nombrebanco banco," +
                        "m.num_documento,  o.num_cuenta, m.cod_persona, m.valor, mn.descripcion,c.estado " +
                        "From movimientocaja m left Join consignacioncheque c On m.cod_movimiento = c.cod_ingegr "+
                        "left Join consignacion o on c.cod_consignacion = o.cod_consignacion " +
                        "inner join bancos bs on m.cod_banco = bs.cod_banco "+
                        "INNER JOIN tipomoneda mn ON m.cod_moneda = mn.cod_moneda "+
                        "Left Join v_persona p On m.cod_persona = p.cod_persona "+
                        "Where m.cod_tipo_pago = 2 And m.estado in(0,1) and c.estado not in(2,3) ";

                        string sFiltro = "";
                        Configuracion conf = new Configuracion();
                        if (pdata[0].Trim() != "" && pdata[0].Trim() != "Seleccionar item")
                        {
                            sFiltro = " and bs.cod_banco = " + pdata[0].Trim() + "";
                        }
                        if (pdata[1] != "" && pdata[2] == "")
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sFiltro = sFiltro + " and  Trunc(m.fec_ope) <= TO_DATE('" + pdata[1] + "','" + conf.ObtenerFormatoFecha() + "') ";
                            else
                                sFiltro = sFiltro + " and  m.fec_ope <= '" + pdata[1] + "' ";
                        }
                        else if (pdata[1] != "" && pdata[2] != "")
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sFiltro = sFiltro + " and  Trunc(m.fec_ope) between TO_DATE('" + pdata[1] + "','" + conf.ObtenerFormatoFecha() + "')  and TO_DATE('" + pdata[2] + "','" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sFiltro = sFiltro + " and  m.fec_ope between '" + pdata[1] + "' and '" + pdata[2] + "' ";
                        }
                        if (pdata[3].Trim() != "" && pdata[3].Trim() != "Seleccione un item")
                        {
                            sFiltro = sFiltro + " and o.num_cuenta = '" + pdata[3].Trim() + "'";
                        }
                        if (sFiltro != "")
                        {
                            sql = sql + sFiltro;
                        }
                        
                        connection.Open();
                        
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                       
                        
                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["cod_consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["cod_consignacion"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["fec_ope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fec_ope"]);
                            if (resultado["Identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["Identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["banco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["banco"]);
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.numcuenta = Convert.ToString(resultado["num_cuenta"]);
                            if (resultado["num_documento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["num_documento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["descripcion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado_cheque = Convert.ToString(resultado["estado"]);
                            if (entidad.estado_cheque == null) entidad.estado_cheque = "SIN CONSIGNAR";
                            else
                                entidad.estado_cheque = " CONSIGNADO";

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MOVIMIENTOCAJA dados unos filtros
        /// </summary>
        /// <param name="pMOVIMIENTOCAJA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesNoconsignados(string[] pdata, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select c.cod_ingegr cod_consignacion , m.cod_ope, m.fec_ope, p.identificacion, p.nombre,bs.nombrebanco banco," +
                        "m.num_documento, b.num_cuenta, m.cod_persona, m.valor, mn.descripcion,c.estado " +
                        "From movimientocaja m left Join consignacioncheque c On m.cod_movimiento = c.cod_ingegr " +
                        "inner join bancos bs on m.cod_banco = bs.cod_banco " +
                        "INNER JOIN tipomoneda mn ON m.cod_moneda = mn.cod_moneda " +
                        "Left Join v_persona p On m.cod_persona = p.cod_persona " +
                        "Left Join cuenta_bancaria b On m.idctabancaria = b.idctabancaria " +
                        "Where m.cod_tipo_pago = 2 And m.estado=0 ";

                        string sFiltro = "";
                        Configuracion conf = new Configuracion();
                        if (pdata[0].Trim() != "" && pdata[0].Trim() != "Seleccionar item")
                        {
                            sFiltro = " and bs.cod_banco = " + pdata[0].Trim() + "";
                        }
                        if (pdata[1] != "" && pdata[2] == "")
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sFiltro = sFiltro + " and  Trunc(m.fec_ope) <= TO_DATE('" + pdata[1] + "','" + conf.ObtenerFormatoFecha() + "') ";
                            else
                                sFiltro = sFiltro + " and  m.fec_ope <= '" + pdata[1] + "' ";
                        }
                        else if (pdata[1] != "" && pdata[2] != "")
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sFiltro = sFiltro + " and  Trunc(m.fec_ope) between TO_DATE('" + pdata[1] + "','" + conf.ObtenerFormatoFecha() + "')  and TO_DATE('" + pdata[2] + "','" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sFiltro = sFiltro + " and  m.fec_ope between '" + pdata[1] + "' and '" + pdata[2] + "' ";
                        }

                        if (sFiltro != "")
                        {
                            sql = sql + sFiltro;
                        }

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["cod_consignacion"] != DBNull.Value) entidad.consignacion = Convert.ToInt64(resultado["cod_consignacion"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["fec_ope"] != DBNull.Value) entidad.fec_ope = Convert.ToDateTime(resultado["fec_ope"]);
                            if (resultado["Identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["Identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["banco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["banco"]);
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.numcuenta = Convert.ToString(resultado["num_cuenta"]);
                            if (resultado["num_documento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["num_documento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["descripcion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado_cheque = Convert.ToString(resultado["estado"]);
                            if (entidad.estado_cheque == null) entidad.estado_cheque = "SIN CONSIGNAR";
                            else
                                entidad.estado_cheque = " CONSIGNADO";

                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarChequesNoconsignados", ex);
                        return null;
                    }
                }
            }
        }



        public MovimientoCaja ConsultarBoucher(Xpinn.Caja.Entities.MovimientoCaja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            MovimientoCaja entidad = new MovimientoCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select num_documento from movimientocaja where cod_ope= " + pEntidad.cod_ope;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["num_documento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["num_documento"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ConsultarBoucher", ex);
                        return null;
                    }
                }
            }
        }

        public List<MovimientoCaja> ListarChequesRecibidos(Int64 Poperacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoCaja> lstMovimientoCaja = new List<MovimientoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select B.NOMBREBANCO as nombanco,a.valor,a.num_documento from movimientocaja  a inner join bANCOS B ON B.COD_BANCO = A.COD_BANCO where cod_tipo_pago = 2 and cod_ope=" + Poperacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MovimientoCaja entidad = new MovimientoCaja();

                            if (resultado["nombanco"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["nombanco"]);
                            if (resultado["num_documento"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["num_documento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor"]);
                            
                            lstMovimientoCaja.Add(entidad);
                        }

                        return lstMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCajaData", "ListarChequesRecibidos", ex);
                        return null;
                    }
                }
            }
        }


    }
}