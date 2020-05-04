using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Par_Cue_LinCredS
    /// </summary>
    public class ParametroCtasCreditosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Par_Cue_LinCredS
        /// </summary>
        public ParametroCtasCreditosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Par_Cue_LinCredS de la base de datos
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred creada</returns>
        public Par_Cue_LinCred CrearPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinCred.idparametro;
                        pidparametro.Direction = ParameterDirection.InputOutput;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pPar_Cue_LinCred.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPar_Cue_LinCred.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pPar_Cue_LinCred.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        if (pPar_Cue_LinCred.cod_categoria == null)
                            pcod_categoria.Value = DBNull.Value;
                        else
                            pcod_categoria.Value = pPar_Cue_LinCred.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        DbParameter plibranza = cmdTransaccionFactory.CreateParameter();
                        plibranza.ParameterName = "p_libranza";
                        if (pPar_Cue_LinCred.libranza == null)
                            plibranza.Value = DBNull.Value;
                        else
                            plibranza.Value = pPar_Cue_LinCred.libranza;
                        plibranza.Direction = ParameterDirection.Input;
                        plibranza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plibranza);

                        DbParameter pgarantia = cmdTransaccionFactory.CreateParameter();
                        pgarantia.ParameterName = "p_garantia";
                        if (pPar_Cue_LinCred.garantia == null)
                            pgarantia.Value = DBNull.Value;
                        else
                            pgarantia.Value = pPar_Cue_LinCred.garantia;
                        pgarantia.Direction = ParameterDirection.Input;
                        pgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pgarantia);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPar_Cue_LinCred.cod_est_det == null)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPar_Cue_LinCred.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPar_Cue_LinCred.cod_cuenta != null) 
                            pcod_cuenta.Value = pPar_Cue_LinCred.cod_cuenta; 
                        else 
                            pcod_cuenta.Value = DBNull.Value;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pPar_Cue_LinCred.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPar_Cue_LinCred.tipo >= 0) ptipo.Value = pPar_Cue_LinCred.tipo; else ptipo.Value = DBNull.Value;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_LinCred.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else 
                            ptipo_tran.Value = pPar_Cue_LinCred.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pPar_Cue_LinCred.cod_cuenta_niif != null) pcod_cuenta_niif.Value = pPar_Cue_LinCred.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_LI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pPar_Cue_LinCred.idparametro = Convert.ToInt32(pidparametro.Value);

                        return pPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "CrearPar_Cue_LinCred", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro o actualizar registro en la tabla PAR_CUE_LINCRED de la base de datos
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad Par_Cue_LinCred</param>
        /// <returns>Entidad Par_Cue_LinCred creada</returns>
        public void CrearParametrizacionCuenta(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinCred.idparametro;
                        pidparametro.Direction = ParameterDirection.InputOutput;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pPar_Cue_LinCred.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPar_Cue_LinCred.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pPar_Cue_LinCred.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        if (pPar_Cue_LinCred.cod_categoria == null)
                            pcod_categoria.Value = DBNull.Value;
                        else
                            pcod_categoria.Value = pPar_Cue_LinCred.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        DbParameter plibranza = cmdTransaccionFactory.CreateParameter();
                        plibranza.ParameterName = "p_libranza";
                        if (pPar_Cue_LinCred.libranza == null)
                            plibranza.Value = DBNull.Value;
                        else
                            plibranza.Value = pPar_Cue_LinCred.libranza;
                        plibranza.Direction = ParameterDirection.Input;
                        plibranza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plibranza);

                        DbParameter pgarantia = cmdTransaccionFactory.CreateParameter();
                        pgarantia.ParameterName = "p_garantia";
                        if (pPar_Cue_LinCred.garantia == null)
                            pgarantia.Value = DBNull.Value;
                        else
                            pgarantia.Value = pPar_Cue_LinCred.garantia;
                        pgarantia.Direction = ParameterDirection.Input;
                        pgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pgarantia);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPar_Cue_LinCred.cod_est_det == null)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPar_Cue_LinCred.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPar_Cue_LinCred.cod_cuenta != null)
                            pcod_cuenta.Value = pPar_Cue_LinCred.cod_cuenta;
                        else
                            pcod_cuenta.Value = DBNull.Value;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pPar_Cue_LinCred.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPar_Cue_LinCred.tipo >= 0) ptipo.Value = pPar_Cue_LinCred.tipo; else ptipo.Value = DBNull.Value;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_LinCred.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_LinCred.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pPar_Cue_LinCred.cod_cuenta_niif != null) pcod_cuenta_niif.Value = pPar_Cue_LinCred.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_LINCRED";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "CrearParametrizacionCuenta", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Par_Cue_LinCredS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Par_Cue_LinCred modificada</returns>
        public Par_Cue_LinCred ModificarPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinCred.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_credito = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_credito.ParameterName = "p_cod_linea_credito";
                        pcod_linea_credito.Value = pPar_Cue_LinCred.cod_linea_credito;
                        pcod_linea_credito.Direction = ParameterDirection.Input;
                        pcod_linea_credito.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_credito);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPar_Cue_LinCred.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pPar_Cue_LinCred.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcod_categoria = cmdTransaccionFactory.CreateParameter();
                        pcod_categoria.ParameterName = "p_cod_categoria";
                        if (pPar_Cue_LinCred.cod_categoria == null)
                            pcod_categoria.Value = DBNull.Value;
                        else
                            pcod_categoria.Value = pPar_Cue_LinCred.cod_categoria;
                        pcod_categoria.Direction = ParameterDirection.Input;
                        pcod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_categoria);

                        DbParameter plibranza = cmdTransaccionFactory.CreateParameter();
                        plibranza.ParameterName = "p_libranza";
                        if (pPar_Cue_LinCred.libranza == null)
                            plibranza.Value = DBNull.Value;
                        else
                            plibranza.Value = pPar_Cue_LinCred.libranza;
                        plibranza.Direction = ParameterDirection.Input;
                        plibranza.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(plibranza);

                        DbParameter pgarantia = cmdTransaccionFactory.CreateParameter();
                        pgarantia.ParameterName = "p_garantia";
                        if (pPar_Cue_LinCred.garantia == null)
                            pgarantia.Value = DBNull.Value;
                        else
                            pgarantia.Value = pPar_Cue_LinCred.garantia;
                        pgarantia.Direction = ParameterDirection.Input;
                        pgarantia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pgarantia);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPar_Cue_LinCred.cod_est_det == null)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPar_Cue_LinCred.cod_est_det;                        
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPar_Cue_LinCred.cod_cuenta != null) pcod_cuenta.Value = pPar_Cue_LinCred.cod_cuenta; else pcod_cuenta.Value = DBNull.Value;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pPar_Cue_LinCred.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPar_Cue_LinCred.tipo >= 0) ptipo.Value = pPar_Cue_LinCred.tipo; else ptipo.Value = DBNull.Value;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_LinCred.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_LinCred.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pPar_Cue_LinCred.cod_cuenta_niif != null) pcod_cuenta_niif.Value = pPar_Cue_LinCred.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_LI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pPar_Cue_LinCred.idparametro = Convert.ToInt32(pidparametro.Value);

                        return pPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ModificarPar_Cue_LinCred", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Par_Cue_LinCredS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Par_Cue_LinCredS</param>
        public void EliminarPar_Cue_LinCred(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_LI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "EliminarPar_Cue_LinCred", ex);
                    }
                }
            }
        }

        public string ValidarCuentaCredito(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            DbDataReader resultado;
            string error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pPar_Cue_LinCred.idparametro = 0;
                        pPar_Cue_LinCred.nom_atr = null;
                        string sql = @"SELECT PAR_CUE_LINCRED.IDPARAMETRO FROM PAR_CUE_LINCRED " + ObtenerFiltro(pPar_Cue_LinCred, "PAR_CUE_LINCRED.");

                        sql = sql +" AND PAR_CUE_LINCRED.COD_LINEA_CREDITO = " + pPar_Cue_LinCred.cod_linea_credito;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value)
                            {
                                error = "La parametrización ingresada ya se encuentra registrada.Código: "+Convert.ToInt32(resultado["IDPARAMETRO"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return error;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ValidarCuentaCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Par_Cue_LinCredS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Par_Cue_LinCredS</param>
        /// <returns>Entidad Par_Cue_LinCred consultado</returns>
        public Par_Cue_LinCred ConsultarPar_Cue_LinCred(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM par_cue_lincred WHERE idparametro = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["LIBRANZA"] != DBNull.Value) entidad.libranza = Convert.ToInt32(resultado["LIBRANZA"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
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
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ConsultarPar_Cue_LinCred", ex);
                        return null;
                    }
                }
            }
        }

 

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Par_Cue_LinCred dados unos filtros
        /// </summary>
        /// <param name="pPar_Cue_LinCred">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Par_Cue_LinCreds obtenidos</returns>
        public List<Par_Cue_LinCred> ListarPar_Cue_LinCred(Par_Cue_LinCred pPar_Cue_LinCred, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro = ObtenerFiltro(pPar_Cue_LinCred,"p.");
                        string sql = @"SELECT p.*, t.descripcion As nom_libranza, g.descripcion As nom_garantia, "
                                     + "(Select a.cod_atr || '-' || a.nombre From atributos a Where p.cod_atr = a.cod_atr) As nom_atr, "
                                     + "tr.descripcion As nom_tipo_tran,L.Nombre "
                                     + "FROM par_cue_lincred p Left Join tip_for_pag t On p.libranza = t.codigo "
                                     + "Left Join tipo_garantia g On p.garantia = g.cod_tipo_gar "
                                     + "Left Join tipo_tran tr On p.tipo_tran = tr.tipo_tran "
                                     + "Left Join Lineascredito l on L.Cod_Linea_Credito = P.Cod_Linea_Credito "
                                     + filtro;
                        if (pPar_Cue_LinCred.cod_linea_credito != null)
                        {
                            if (!string.IsNullOrWhiteSpace(filtro))
                                sql = sql + " AND p.cod_linea_credito = '" + pPar_Cue_LinCred.cod_linea_credito + "'";
                            else
                                sql = sql + " WHERE p.cod_linea_credito = '" + pPar_Cue_LinCred.cod_linea_credito + "'";
                            
                        }
                        if (pPar_Cue_LinCred.tipo == 0)
                            if (filtro.Trim() != "")
                                sql = sql + " AND (p.tipo = 0 Or p.tipo Is Null)";
                            else
                                sql = sql + " WHERE (p.tipo = 0 Or p.tipo Is Null)";
                        sql = sql + " ORDER BY p.cod_linea_credito, p.cod_atr, p.tipo, p.tipo_cuenta, p.cod_categoria, p.libranza, p.garantia ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["LIBRANZA"] != DBNull.Value) entidad.libranza = Convert.ToInt32(resultado["LIBRANZA"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["NOM_LIBRANZA"] != DBNull.Value) entidad.nom_libranza = Convert.ToString(resultado["NOM_LIBRANZA"]);
                            if (resultado["NOM_GARANTIA"] != DBNull.Value) entidad.nom_garantia = Convert.ToString(resultado["NOM_GARANTIA"]);
                            if (resultado["NOM_ATR"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOM_ATR"]);
                            if (resultado["NOM_TIPO_TRAN"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["NOM_TIPO_TRAN"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["NOMBRE"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarPar_Cue_LinCred", ex);
                        return null;
                    }
                }
            }
        }

        
        public List<Par_Cue_LinCred> ListarClasificacion(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Categorias ORDER BY cod_categoria";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Par_Cue_LinCred> ListarTransaccion(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipo_tran ORDER BY tipo_tran";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["DESCRIPCION"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarTransaccion", ex);
                        return null;
                    }
                }
            }
        }

        public List<Par_Cue_LinCred> ListarEstructura(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM estructura_detalle ORDER BY cod_est_det";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.nom_est_det = Convert.ToString(resultado["DETALLE"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarEstructura", ex);
                        return null;
                    }
                }
            }
        }

        public List<Par_Cue_LinCred> ListarLibranza(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tip_for_pag ORDER BY codigo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.libranza = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_libranza = Convert.ToString(resultado["DESCRIPCION"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarLibranza", ex);
                        return null;
                    }
                }
            }
        }

        public List<Par_Cue_LinCred> ListarGarantia(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinCred> lstPar_Cue_LinCred = new List<Par_Cue_LinCred>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM tipo_garantia ORDER BY cod_tipo_gar";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                            if (resultado["COD_TIPO_GAR"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["COD_TIPO_GAR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_garantia = Convert.ToString(resultado["DESCRIPCION"]);
                            lstPar_Cue_LinCred.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ListarGarantia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(idParametro) + 1 FROM Par_Cue_LinCred ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        string sDato = cmdTransaccionFactory.ExecuteScalar().ToString();                        
                        if (sDato != null && sDato.Trim() != "")
                            resultado = Convert.ToInt64(sDato);
                        else
                            resultado = 1;
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public void CopiarPar_Cue_LinCred(String lineaOrigen, String LineaDestino, int? cod_atr, int? tipo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_origen = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_origen.ParameterName = "p_cod_linea_origen";
                        pcod_linea_origen.Value = lineaOrigen;
                        pcod_linea_origen.Direction = ParameterDirection.Input;
                        pcod_linea_origen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_origen);

                        DbParameter pcod_linea_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_destino.ParameterName = "p_cod_Linea_Destino";
                        pcod_linea_destino.Value = LineaDestino;
                        pcod_linea_destino.Direction = ParameterDirection.Input;
                        pcod_linea_destino.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_destino);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        if (cod_atr == null)
                            pcod_atr.Value = DBNull.Value;
                        else
                            pcod_atr.Value = cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        if (tipo == null)
                            p_tipo.Value = DBNull.Value;
                        else
                            p_tipo.Value = tipo;
                        p_tipo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COPI_PACUE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "CopiarPar_Cue_LinCred", ex);
                    }
                }
            }
        }


        public Par_Cue_LinCred ConsultarPAR_CUE_LINAPO(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinCred entidad = new Par_Cue_LinCred();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDPARAMETRO FROM PAR_CUE_LINAPO " + ObtenerFiltro(pPar_Cue_LinCred) + " " + pfiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "PAR_CUE_LINAPO", ex);
                        return null;
                    }
                }
            }
        }

        public Par_Cue_LinCred ConsultarPAR_CUE_OTROS(Par_Cue_LinCred pPar_Cue_LinCred, string pfiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinCred entidad = new Par_Cue_LinCred();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDPARAMETRO FROM PAR_CUE_OTROS " + pfiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "PAR_CUE_LINAPO", ex);
                        return null;
                    }
                }
            }
        }


        public Par_Cue_LinCred ConsultarPar_Cue_LinCred2(Par_Cue_LinCred pPar_Cue_LinCred,string pfiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinCred entidad = new Par_Cue_LinCred();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM par_cue_lincred " + ObtenerFiltro(pPar_Cue_LinCred) + " " + pfiltro.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["LIBRANZA"] != DBNull.Value) entidad.libranza = Convert.ToInt32(resultado["LIBRANZA"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.garantia = Convert.ToInt32(resultado["GARANTIA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinCredData", "ConsultarPar_Cue_LinCred2", ex);
                        return null;
                    }
                }
            }
        }


        public string ParametroGeneral(Int64 pCodigo, Usuario vUsuario)
        {
            DbDataReader resultado;
            string entidad= "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"SELECT * FROM General WHERE codigo = " + pCodigo; 
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) entidad = Convert.ToString(resultado["VALOR"]);                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }

    }
}