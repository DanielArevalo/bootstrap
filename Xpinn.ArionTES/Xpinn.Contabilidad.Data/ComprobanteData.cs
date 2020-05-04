using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para comprobantes
    /// </summary>    
    public class ComprobanteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        Configuracion global = new Configuracion();
        string FormatoFecha = " ";

        /// <summary>
        /// Constructor del objeto de acceso a datos para Comprobante
        /// </summary>
        public ComprobanteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

        /// <summary>
        /// Crear el encabezado del comprobante
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Comprobante CrearComprobante(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pComprobante.num_comp;
                        pNUM_COMP.Direction = ParameterDirection.InputOutput;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pComprobante.tipo_comp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pNUM_CONSIG = cmdTransaccionFactory.CreateParameter();
                        pNUM_CONSIG.ParameterName = "p_num_consig";
                        if (pComprobante.num_consig == null)
                            pNUM_CONSIG.Value = "";
                        else
                            pNUM_CONSIG.Value = pComprobante.num_consig;
                        pNUM_CONSIG.DbType = DbType.AnsiString;
                        pNUM_CONSIG.Direction = ParameterDirection.Input;
                        pNUM_CONSIG.Size = 50;

                        DbParameter pN_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pN_DOCUMENTO.ParameterName = "p_n_documento";
                        if (pComprobante.n_documento == null)
                            pN_DOCUMENTO.Value = "";
                        else
                            pN_DOCUMENTO.Value = pComprobante.n_documento;
                        pN_DOCUMENTO.DbType = DbType.AnsiString;
                        pN_DOCUMENTO.Direction = ParameterDirection.Input;
                        pN_DOCUMENTO.Size = 50;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.Value = pComprobante.fecha;
                        pFECHA.DbType = DbType.DateTime;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_hora";
                        pHORA.Value = pComprobante.hora;
                        pHORA.DbType = DbType.DateTime;

                        DbParameter pCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCIUDAD.ParameterName = "p_ciudad";
                        pCIUDAD.Value = pComprobante.ciudad;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = pComprobante.concepto;

                        DbParameter pTIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PAGO.ParameterName = "p_tipo_pago";
                        if (pComprobante.tipo_pago != null)
                            pTIPO_PAGO.Value = pComprobante.tipo_pago;
                        else
                            pTIPO_PAGO.Value = DBNull.Value;

                        DbParameter pENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pENTIDAD.ParameterName = "p_entidad";
                        if (pComprobante.entidad == null)
                            pENTIDAD.Value = DBNull.Value;
                        else
                            pENTIDAD.Value = pComprobante.entidad;

                        DbParameter pTOTALCOM = cmdTransaccionFactory.CreateParameter();
                        pTOTALCOM.ParameterName = "p_totalcom";
                        pTOTALCOM.Value = pComprobante.totalcom;

                        DbParameter pTIPO_BENEF = cmdTransaccionFactory.CreateParameter();
                        pTIPO_BENEF.ParameterName = "p_tipo_benef";
                        pTIPO_BENEF.Value = pComprobante.tipo_benef;

                        DbParameter pCOD_BENEF = cmdTransaccionFactory.CreateParameter();
                        pCOD_BENEF.ParameterName = "p_cod_benef";
                        pCOD_BENEF.Value = pComprobante.cod_benef;

                        DbParameter pCOD_ELABORO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ELABORO.ParameterName = "p_cod_elaboro";
                        pCOD_ELABORO.Value = pComprobante.cod_elaboro;

                        DbParameter pCOD_APROBO = cmdTransaccionFactory.CreateParameter();
                        pCOD_APROBO.ParameterName = "p_cod_aprobo";
                        if (pComprobante.cod_aprobo == null)
                        {
                            pCOD_APROBO.Value = 0;
                            pCOD_APROBO.DbType = DbType.Int64;
                        }
                        else
                        {
                            pCOD_APROBO.Value = pComprobante.cod_aprobo;
                        }

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        if (pComprobante.estado == null || pComprobante.estado == "")
                            pESTADO.Value = 'E';
                        else
                            pESTADO.Value = pComprobante.estado;
                        pESTADO.DbType = DbType.AnsiString;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.Size = 1;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        if (pComprobante.observaciones == null)
                            pOBSERVACIONES.Value = "";
                        else
                            pOBSERVACIONES.Value = pComprobante.observaciones;
                        pOBSERVACIONES.DbType = DbType.AnsiString;
                        pOBSERVACIONES.Size = 200;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        if (pComprobante.cuenta == null)
                            p_cuenta.Value = DBNull.Value;
                        else
                            p_cuenta.Value = pComprobante.cuenta;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pNUM_CONSIG);
                        cmdTransaccionFactory.Parameters.Add(pN_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pTOTALCOM);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ELABORO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_APROBO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pComprobante.num_comp = Convert.ToInt64(pNUM_COMP.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pComprobante, "e_comingres/e_comegres/e_comconta", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Comprobante, "Creacion de comprobante con numero de comprobante " + pComprobante.num_comp + " y tipo de comprobante " + pComprobante.tipo_comp);

                        connection.Dispose();

                        return pComprobante;
                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "CrearComprobante", ex);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "CrearComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        ///  Modifcar un comprobante
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Comprobante ModificarComprobante(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Comprobante comprobanteAnterior = ConsultarComprobante(pComprobante.num_comp, pComprobante.tipo_comp, vUsuario);

                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pComprobante.num_comp;
                        pNUM_COMP.Direction = ParameterDirection.Input;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pComprobante.tipo_comp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pNUM_CONSIG = cmdTransaccionFactory.CreateParameter();
                        pNUM_CONSIG.ParameterName = "p_num_consig";
                        if (pComprobante.num_consig == null)
                            pNUM_CONSIG.Value = "";
                        else
                            pNUM_CONSIG.Value = pComprobante.num_consig;
                        pNUM_CONSIG.DbType = DbType.AnsiString;
                        pNUM_CONSIG.Direction = ParameterDirection.Input;
                        pNUM_CONSIG.Size = 50;

                        DbParameter pN_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pN_DOCUMENTO.ParameterName = "p_n_documento";
                        if (pComprobante.n_documento == null)
                            pN_DOCUMENTO.Value = "";
                        else
                            pN_DOCUMENTO.Value = pComprobante.n_documento;
                        pN_DOCUMENTO.DbType = DbType.AnsiString;
                        pN_DOCUMENTO.Direction = ParameterDirection.Input;
                        pN_DOCUMENTO.Size = 50;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.Value = pComprobante.fecha;
                        pFECHA.DbType = DbType.DateTime;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_hora";
                        pHORA.Value = pComprobante.hora;
                        pHORA.DbType = DbType.DateTime;

                        DbParameter pCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCIUDAD.ParameterName = "p_ciudad";
                        pCIUDAD.Value = pComprobante.ciudad;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = pComprobante.concepto;

                        DbParameter pTIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PAGO.ParameterName = "p_tipo_pago";
                        pTIPO_PAGO.Value = pComprobante.tipo_pago;

                        DbParameter pENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pENTIDAD.ParameterName = "p_entidad";
                        if (pComprobante.entidad == null)
                            pENTIDAD.Value = DBNull.Value;
                        else
                            pENTIDAD.Value = pComprobante.entidad;

                        DbParameter pTOTALCOM = cmdTransaccionFactory.CreateParameter();
                        pTOTALCOM.ParameterName = "p_totalcom";
                        pTOTALCOM.Value = pComprobante.totalcom;

                        DbParameter pTIPO_BENEF = cmdTransaccionFactory.CreateParameter();
                        pTIPO_BENEF.ParameterName = "p_tipo_benef";
                        pTIPO_BENEF.Value = pComprobante.tipo_benef;

                        DbParameter pCOD_BENEF = cmdTransaccionFactory.CreateParameter();
                        pCOD_BENEF.ParameterName = "p_cod_benef";
                        pCOD_BENEF.Value = pComprobante.cod_benef;

                        DbParameter pCOD_ELABORO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ELABORO.ParameterName = "p_cod_elaboro";
                        pCOD_ELABORO.Value = pComprobante.cod_elaboro;

                        DbParameter pCOD_APROBO = cmdTransaccionFactory.CreateParameter();
                        pCOD_APROBO.ParameterName = "p_cod_aprobo";
                        if (pComprobante.cod_aprobo == null)
                        {
                            pCOD_APROBO.Value = 0;
                            pCOD_APROBO.DbType = DbType.Int64;
                        }
                        else
                        {
                            pCOD_APROBO.Value = pComprobante.cod_aprobo;
                        }

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        if (pComprobante.estado == null || pComprobante.estado == "")
                            pESTADO.Value = DBNull.Value;
                        else
                            pESTADO.Value = pComprobante.estado;
                        pESTADO.DbType = DbType.AnsiString;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.Size = 1;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        if (pComprobante.observaciones == null)
                            pOBSERVACIONES.Value = "";
                        else
                            pOBSERVACIONES.Value = pComprobante.observaciones;
                        pOBSERVACIONES.DbType = DbType.AnsiString;
                        pOBSERVACIONES.Size = 200;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        if (pComprobante.cuenta == null)
                            p_cuenta.Value = DBNull.Value;
                        else
                            p_cuenta.Value = pComprobante.cuenta;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pNUM_CONSIG);
                        cmdTransaccionFactory.Parameters.Add(pN_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pTOTALCOM);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ELABORO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_APROBO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPROBANTE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pComprobante, "e_comingres/e_comegres/e_comconta", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Comprobante, "Modificacion de comprobante con numero de comprobante " + pComprobante.num_comp + " y tipo de comprobante " + pComprobante.tipo_comp, comprobanteAnterior);

                        dbConnectionFactory.CerrarConexion(connection);

                        return pComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ModificarComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public void ModificarGiro(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pComprobante.num_comp;
                        pNUM_COMP.Direction = ParameterDirection.Input;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pComprobante.tipo_comp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pCUENTA = cmdTransaccionFactory.CreateParameter();
                        pCUENTA.ParameterName = "p_cuenta";
                        if (pComprobante.cuenta == null || pComprobante.cuenta == "")
                            pCUENTA.Value = DBNull.Value;
                        else
                            pCUENTA.Value = pComprobante.cuenta;
                        pCUENTA.DbType = DbType.Int64;
                        pCUENTA.Direction = ParameterDirection.Input;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_cod_usuario";
                        pCODUSUARIO.Value = vUsuario.codusuario;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pCUENTA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pComprobante, "GIRO", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ModificarGiro", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Consultar un comprobante
        /// </summary>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Comprobante ConsultarComprobante(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select E.*,T.Descripcion as nomTipo_iden,u.nombre as nomElaboro, o.COD_OPE
                                        From V_Comprobante E Left Join Tipoidentificacion T  ON E.Tipo_Iden = T.Codtipoidentificacion 
                                        left join Usuarios u on u.codusuario = e.cod_elaboro
                                        left join operacion o on E.NUM_COMP = o.NUM_COMP AND e.tipo_comp = o.tipo_comp                                   
                                        WHERE e.num_comp = " + pnum_comp.ToString() + " AND e.tipo_comp = " + ptipo_comp.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.n_documento = Convert.ToString(resultado["N_DOCUMENTO"]);
                            if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.num_consig = Convert.ToString(resultado["NUM_CONSIG"]);
                            if (resultado["ENTIDAD"] != DBNull.Value) entidad.entidad = Convert.ToInt64(resultado["ENTIDAD"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["TIPO_PAGO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["DESCRIPCION_CONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCION_CONCEPTO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.iden_benef = Convert.ToString(resultado["IDEN_BENEF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TOTALCOM"] != DBNull.Value) entidad.totalcom = Convert.ToDecimal(resultado["TOTALCOM"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["CUENTA_BANCARIA"] != DBNull.Value) entidad.cuenta = Convert.ToString(resultado["CUENTA_BANCARIA"]);
                            if (resultado["TIPO_IDEN"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDEN"]);
                            if (resultado["NOMTIPO_IDEN"] != DBNull.Value) entidad.nom_tipo_iden = Convert.ToString(resultado["NOMTIPO_IDEN"]);
                            if (resultado["NOMELABORO"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMELABORO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);                           
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public Comprobante ConsultarGiro(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT O.COD_OPE,c.num_cuenta as num_referencia FROM giro e Inner Join operacion o On e.cod_ope = o.cod_ope left join cuenta_bancaria c on c.idctabancaria = e.idctabancaria WHERE o.num_comp = " + pnum_comp.ToString() + " AND o.tipo_comp = " + ptipo_comp.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["NUM_REFERENCIA"] != DBNull.Value) entidad.cuenta = Convert.ToString(resultado["NUM_REFERENCIA"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarGiro", ex);
                        return null;
                    }
                }
            }
        }


        public Giro ConsultarGiroGeneral(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Giro entidad = new Giro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From Giro Where Cod_Ope = (select O.Cod_Ope from operacion O where O.num_comp = " + pnum_comp.ToString() + " and O.tipo_comp = " + ptipo_comp.ToString() + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarGiroGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public Giro ConsultarGiroRealizadoGeneral(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Giro entidad = new Giro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT G.* FROM GIRO_REALIZADO R LEFT JOIN GIRO G ON R.IDGIRO = G.IDGIRO "
                                    + "Where R.Cod_Ope = (select O.Cod_Ope from operacion O where O.num_comp = " + pnum_comp.ToString() + " and O.tipo_comp = " + ptipo_comp.ToString() + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarGiroGeneral", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarCuenta(Int64 pCodBanco, string pNumCuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            string codCuenta = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT e.cod_cuenta FROM cuenta_bancaria e WHERE e.cod_banco = " + pCodBanco.ToString() + " And e.num_cuenta = '" + pNumCuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) codCuenta = Convert.ToString(resultado["COD_CUENTA"]);
                        }
                        return codCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarCuenta", ex);
                        return codCuenta;
                    }
                }
            }
        }


        /// <summary>
        /// Traer listado de comprobantes
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Comprobante> ListarComprobante(Comprobante pComprobante, Usuario pUsuario, String filtro, String orden)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Comprobante> lstComprobante = new List<Comprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string condicion = ObtenerFiltro(pComprobante, "v_comprobante.");
                        string sql = @"Select v_comprobante.*, usuarios.nombre As elaboro, ua.nombre as Aprobo, 
                                        Decode(v_comprobante.tipo_comp, 5, (Select Sum(Decode(d.tipo, 'C', d.valor, -d.valor)) From d_comprobante d Where d.num_comp = v_comprobante.num_comp And d.tipo_comp = v_comprobante.tipo_comp And d.cod_cuenta Like '11%'), 0) AS desembolso
                                        from v_comprobante Left Join usuarios On v_comprobante.cod_elaboro = usuarios.codusuario 
                                        Left Join usuarios ua On v_comprobante.cod_aprobo = ua.codusuario ";
                        if (condicion.Trim() != "")
                        {
                            sql = sql + condicion;
                        }
                        if (filtro.Trim() != "")
                        {
                            if (condicion.Trim() == "")
                                sql = sql + " Where 1=1 ";
                            sql = sql + filtro;
                        }
                        if (orden != "")
                            sql += " Order by " + orden;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Comprobante entidad = new Comprobante();

                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION_CONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCION_CONCEPTO"]);
                            if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.n_documento = Convert.ToString(resultado["N_DOCUMENTO"]);
                            if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.num_consig = Convert.ToString(resultado["NUM_CONSIG"]);
                            if (entidad.tipo_comp == 5)
                            {
                                if (resultado["N_DOCUMENTO"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["N_DOCUMENTO"]);
                            }
                            else
                            {
                                if (resultado["NUM_CONSIG"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["NUM_CONSIG"]);
                            }
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.iden_benef = Convert.ToString(resultado["IDEN_BENEF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["ELABORO"] != DBNull.Value) entidad.elaboro = Convert.ToString(resultado["ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["APROBO"] != DBNull.Value) entidad.aprobo = Convert.ToString(resultado["APROBO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TOTALCOM"] != DBNull.Value) entidad.totalcom = Convert.ToDecimal(resultado["TOTALCOM"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["DESEMBOLSO"] != DBNull.Value) entidad.desembolso = Convert.ToDecimal(resultado["DESEMBOLSO"]);
                            lstComprobante.Add(entidad);
                        }

                        return lstComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ListarComprobante", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Traer listado de comprobantes
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<DetalleComprobante> ListarComprobantesreporte(DetalleComprobante pComprobante, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleComprobante> lstComprobante = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "select * FROM d_comprobante  " + ObtenerFiltro(pComprobante) + " Order by codigo Desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToInt64(resultado["MONEDA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);

                            lstComprobante.Add(entidad);
                        }

                        return lstComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ListarComprobantesreporte", ex);
                        return null;
                    }
                }
            }
        }

        public string Consultafecha(Usuario pUsuario, string tipoComprobante = null)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleComprobante> lstComprobante = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (tipoComprobante == null || tipoComprobante == ""  ) 
                        {
                            tipoComprobante = "'C'";
                        }

                        string sql = "select max(fecha) as fecha from cierea where tipo = " + tipoComprobante + " and estado = 'D'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string respuesta = "";
                        if (resultado.Read())
                        {
                            if (resultado["fecha"] != DBNull.Value) respuesta = Convert.ToString(resultado["fecha"]);
                        }

                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ListarComprobantesreporte", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Insertar un detalle de un comprobante
        /// </summary>
        /// <param name="pDetalleComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public DetalleComprobante CrearDetalleComprobante(DetalleComprobante pDetalleComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODIGO = cmdTransaccionFactory.CreateParameter();
                        pCODIGO.ParameterName = "p_codigo";
                        pCODIGO.Value = 0;
                        pCODIGO.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pDetalleComprobante.num_comp;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pDetalleComprobante.tipo_comp;

                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "p_cod_cuenta";
                        if (pDetalleComprobante.cod_cuenta != null)
                            pCOD_CUENTA.Value = pDetalleComprobante.cod_cuenta;
                        else
                            pCOD_CUENTA.Value = DBNull.Value;
                        pCOD_CUENTA.DbType = DbType.AnsiString;
                        pCOD_CUENTA.Size = 20;

                        DbParameter pCOD_CUENTA_NIIF = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA_NIIF.ParameterName = "p_cod_cuenta_niif";
                        if (pDetalleComprobante.cod_cuenta_niif != null)
                            pCOD_CUENTA_NIIF.Value = pDetalleComprobante.cod_cuenta_niif;
                        else
                            pCOD_CUENTA_NIIF.Value = DBNull.Value;
                        pCOD_CUENTA_NIIF.DbType = DbType.AnsiString;
                        pCOD_CUENTA_NIIF.Size = 30;

                        DbParameter pMONEDA = cmdTransaccionFactory.CreateParameter();
                        pMONEDA.ParameterName = "p_moneda";
                        pMONEDA.Value = pDetalleComprobante.moneda;

                        DbParameter pCENTRO_COSTO = cmdTransaccionFactory.CreateParameter();
                        pCENTRO_COSTO.ParameterName = "p_centro_costo";
                        pCENTRO_COSTO.Value = pDetalleComprobante.centro_costo;

                        DbParameter pCENTRO_GESTION = cmdTransaccionFactory.CreateParameter();
                        pCENTRO_GESTION.ParameterName = "p_centro_gestion";
                        if (pDetalleComprobante.centro_gestion != null)
                            pCENTRO_GESTION.Value = Convert.ToInt64(pDetalleComprobante.centro_gestion);
                        else
                            pCENTRO_GESTION.Value = DBNull.Value;

                        DbParameter pDETALLE = cmdTransaccionFactory.CreateParameter();
                        pDETALLE.ParameterName = "p_detalle";
                        if (pDetalleComprobante.detalle == null)
                            pDETALLE.Value = DBNull.Value;
                        else
                            pDETALLE.Value = pDetalleComprobante.detalle;
                        pDETALLE.DbType = DbType.AnsiString;
                        pDETALLE.Size = 200;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_tipo";
                        pTIPO.Value = pDetalleComprobante.tipo;
                        pTIPO.DbType = DbType.AnsiString;
                        pTIPO.Size = 1;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_valor";
                        pVALOR.Value = pDetalleComprobante.valor;

                        DbParameter pTERCERO = cmdTransaccionFactory.CreateParameter();
                        pTERCERO.ParameterName = "p_tercero";
                        if (pDetalleComprobante.tercero != null)
                            pTERCERO.Value = pDetalleComprobante.tercero;
                        else
                            pTERCERO.Value = DBNull.Value;

                        DbParameter pBASE_COMP = cmdTransaccionFactory.CreateParameter();
                        pBASE_COMP.ParameterName = "p_base_comp";
                        if (pDetalleComprobante.base_comp != null)
                            pBASE_COMP.Value = pDetalleComprobante.base_comp;
                        else
                            pBASE_COMP.Value = DBNull.Value;

                        DbParameter pPORCENTAJE = cmdTransaccionFactory.CreateParameter();
                        pPORCENTAJE.ParameterName = "p_porcentaje";
                        if (pDetalleComprobante.porcentaje != null)
                            pPORCENTAJE.Value = pDetalleComprobante.porcentaje;
                        else
                            pPORCENTAJE.Value = DBNull.Value;

                        DbParameter pCOD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_TIPO_PRODUCTO.ParameterName = "p_cod_tipo_producto";
                        if (pDetalleComprobante.cod_tipo_producto != 0)
                            pCOD_TIPO_PRODUCTO.Value = pDetalleComprobante.cod_tipo_producto;
                        else
                            pCOD_TIPO_PRODUCTO.Value = DBNull.Value;

                        DbParameter pNUMERO_TRANSACCION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_TRANSACCION.ParameterName = "p_numero_transaccion";
                        if (pDetalleComprobante.numero_transaccion != null)
                            pNUMERO_TRANSACCION.Value = pDetalleComprobante.numero_transaccion;
                        else
                            pNUMERO_TRANSACCION.Value = DBNull.Value;

                        cmdTransaccionFactory.Parameters.Add(pCODIGO);
                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA_NIIF);
                        cmdTransaccionFactory.Parameters.Add(pMONEDA);
                        cmdTransaccionFactory.Parameters.Add(pCENTRO_COSTO);
                        cmdTransaccionFactory.Parameters.Add(pCENTRO_GESTION);
                        cmdTransaccionFactory.Parameters.Add(pDETALLE);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pTERCERO);
                        cmdTransaccionFactory.Parameters.Add(pBASE_COMP);
                        cmdTransaccionFactory.Parameters.Add(pPORCENTAJE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_TIPO_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_TRANSACCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DETALLE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pDetalleComprobante.codigo = Convert.ToInt64(pCODIGO.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return pDetalleComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "CrearDetalleComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modificar el detalle de un comprobante
        /// </summary>
        /// <param name="pDetalleComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public DetalleComprobante ModificarDetalleComprobante(DetalleComprobante pDetalleComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODIGO = cmdTransaccionFactory.CreateParameter();
                        pCODIGO.ParameterName = "p_codigo";
                        pCODIGO.Value = pDetalleComprobante.codigo;
                        pCODIGO.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pDetalleComprobante.num_comp;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pDetalleComprobante.tipo_comp;

                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "p_cod_cuenta";
                        pCOD_CUENTA.Value = pDetalleComprobante.cod_cuenta;

                        DbParameter pMONEDA = cmdTransaccionFactory.CreateParameter();
                        pMONEDA.ParameterName = "p_moneda";
                        pMONEDA.Value = pDetalleComprobante.moneda;

                        DbParameter pCENTRO_COSTO = cmdTransaccionFactory.CreateParameter();
                        pCENTRO_COSTO.ParameterName = "p_centro_costo";
                        pCENTRO_COSTO.Value = pDetalleComprobante.centro_costo;

                        DbParameter pCENTRO_GESTION = cmdTransaccionFactory.CreateParameter();
                        pCENTRO_GESTION.ParameterName = "p_centro_gestion";
                        if (pDetalleComprobante.centro_gestion == null)
                            pCENTRO_GESTION.Value = Convert.ToInt64(pDetalleComprobante.centro_gestion);
                        else
                            pCENTRO_GESTION.Value = Convert.ToInt64("0");

                        DbParameter pDETALLE = cmdTransaccionFactory.CreateParameter();
                        pDETALLE.ParameterName = "p_detalle";
                        pDETALLE.Value = pDetalleComprobante.detalle;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_tipo";
                        pTIPO.Value = pDetalleComprobante.tipo;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_valor";
                        pVALOR.Value = pDetalleComprobante.valor;

                        DbParameter pTERCERO = cmdTransaccionFactory.CreateParameter();
                        pTERCERO.ParameterName = "p_tercero";
                        if (pDetalleComprobante.tercero == null)
                            pTERCERO.Value = Convert.ToInt64(pDetalleComprobante.tercero);
                        else
                            pTERCERO.Value = Convert.ToInt64("0");

                        cmdTransaccionFactory.Parameters.Add(pCODIGO);
                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pMONEDA);
                        cmdTransaccionFactory.Parameters.Add(pCENTRO_COSTO);
                        cmdTransaccionFactory.Parameters.Add(pCENTRO_GESTION);
                        cmdTransaccionFactory.Parameters.Add(pDETALLE);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pTERCERO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DETALLE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pDetalleComprobante.codigo = Convert.ToInt64(pNUM_COMP.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDetalleComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ModificarDetalleComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consultar el detalle de un comprobante
        /// </summary>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<DetalleComprobante> ConsultarDetalleComprobante(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<DetalleComprobante> LstDetComp = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM d_comprobante e WHERE e.num_comp = " + pnum_comp.ToString() + " AND e.tipo_comp = " + ptipo_comp.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE_NIIF"] != DBNull.Value) entidad.nombre_cuenta_nif = Convert.ToString(resultado["NOMCUE_NIIF"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToInt64(resultado["MONEDA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["BASE_COMP"] != DBNull.Value) entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]); 
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToString(resultado["NUMERO_TRANSACCION"]); 

                            LstDetComp.Add(entidad);
                        };

                        return LstDetComp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return null;
                    }
                }
            }
        }
        public List<DetalleComprobante> ConsultarDetalleCompro_Anulacion(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<DetalleComprobante> LstDetComp = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM d_comprobante e WHERE e.num_comp = " + pnum_comp.ToString() + " AND e.tipo_comp = " + ptipo_comp.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE_NIIF"] != DBNull.Value) entidad.nombre_cuenta_nif = Convert.ToString(resultado["NOMCUE_NIIF"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToInt64(resultado["MONEDA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (entidad.tipo =="C")                            
                                entidad.tipo = "D";   
                            else
                                entidad.tipo = "C";
                            
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["BASE_COMP"] != DBNull.Value) entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]); else entidad.base_comp = 0;
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToString(resultado["NUMERO_TRANSACCION"]); else entidad.porcentaje = 0;

                            LstDetComp.Add(entidad);
                        };

                        return LstDetComp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return null;
                    }
                }
            }
        }


        public string ConsultaUsuario(long cod, Usuario pUsuario)
        {
            string resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select nombre From usuarios Where codusuario =" + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = Convert.ToString(cmdTransaccionFactory.ExecuteScalar());

                    return resultado;
                }
            }
        }


        public Int64 ConsultaCodUsuario(string cod, Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select codusuario From usuarios Where identificacion = " + cod;

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                    return resultado;

                }
            }
        }


        public Boolean IniciarDetalleComprobante(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pnum_comp;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = ptipo_comp;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DETALLE_DEL";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return false;
                    }
                }
            }
        }


        public string CuentaEsAuxiliar(string cod, Usuario pUsuario)
        {
            string nombre = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select p.nombre From plan_cuentas p Where p.cod_cuenta = '" + cod + "' And p.cod_cuenta Not In (Select x.depende_de From plan_cuentas x Where x.depende_de = p.cod_cuenta)";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    nombre = Convert.ToString(cmdTransaccionFactory.ExecuteScalar());

                    return nombre;

                }
            }
        }

        public string CuentaNIFEsAuxiliar(string cod, Usuario pUsuario)
        {
            string nombre = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select p.nombre From plan_cuentas_niif p Where p.cod_cuenta_niif = '" + cod + "' And p.cod_cuenta_niif Not In (Select x.depende_de From plan_cuentas_niif x Where x.depende_de = p.cod_cuenta_niif)";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    nombre = Convert.ToString(cmdTransaccionFactory.ExecuteScalar());

                    return nombre;

                }
            }
        }


        public Boolean CuentaEsGiro(string cod, Usuario pUsuario)
        {
            DbDataReader resultado;
            string sql = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    sql = "Select p.cod_cuenta From cuenta_bancaria p Where p.cod_cuenta = '" + cod + "' ";
                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                        return true;

                    sql = "Select p.cod_cuenta From plan_cuentas p Where p.cod_cuenta = '" + cod + "' And maneja_gir = 1";
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();
                    if (resultado.Read())
                        return true;

                    return false;

                }
            }
        }


        /// <summary>
        /// Mètodo para determinar los procesos parametrizados para generar interfaz contable
        /// </summary>
        /// <param name="pcod_ope"></param>
        /// <param name="ptip_ope"></param>
        /// <param name="pfecha"></param>
        /// <returns></returns>
        public List<ProcesoContable> ConsultaProceso(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ProcesoContable> lstProceso = new List<ProcesoContable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = "";
                    if (((pcod_ope != 0) || !(ptip_ope != 0)) && ptip_ope != 7)
                    {
                        string sCodOpe = pcod_ope.ToString();
                        sql = "Select p.cod_proceso, p.tipo_comp, t.descripcion, c.descripcion As concepto From tipo_comp t, proceso_contable p Left Join concepto c On c.concepto = p.concepto, operacion o " +
                                     "Where t.tipo_comp = p.tipo_comp And p.tipo_ope = o.tipo_ope And o.cod_ope = " + sCodOpe + " And o.fecha_oper Between p.fecha_inicial And p.fecha_final";
                    }
                    else
                    {
                        string sTipOpe = ptip_ope.ToString();
                        sql = "Select p.cod_proceso, p.tipo_comp, t.descripcion, c.descripcion As concepto From tipo_comp t, proceso_contable p Left Join concepto c On c.concepto = p.concepto " +
                                        "Where t.tipo_comp = p.tipo_comp And p.tipo_ope = " + sTipOpe + " And p.fecha_inicial <= SYSDATE And p.fecha_final >= SYSDATE ";
                    }

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ProcesoContable Proceso = new ProcesoContable();
                        if (resultado["COD_PROCESO"] != DBNull.Value) Proceso.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                        if (resultado["TIPO_COMP"] != DBNull.Value) Proceso.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                        if (resultado["DESCRIPCION"] != DBNull.Value) Proceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        if (resultado["CONCEPTO"] != DBNull.Value) Proceso.nom_concepto = Convert.ToString(resultado["CONCEPTO"]);
                        lstProceso.Add(Proceso);
                    };

                    return lstProceso;

                }
            }
        }


        public List<ProcesoContable> ConsultaProcesoUlt(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ProcesoContable> lstProceso = new List<ProcesoContable>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = "";
                    if (((pcod_ope != 0) || !(ptip_ope != 0)) && ptip_ope != 7)
                    {
                        string sCodOpe = pcod_ope.ToString();
                        sql = "Select p.cod_proceso, p.tipo_comp, t.descripcion, c.descripcion As concepto From tipo_comp t, concepto c, proceso_contable p, operacion o " +
                                "Where t.tipo_comp = p.tipo_comp And c.concepto = p.concepto And p.tipo_ope = o.tipo_ope And o.cod_ope = " + sCodOpe + " And o.fecha_oper Between p.fecha_inicial And p.fecha_final";
                    }
                    else
                    {
                        string sTipOpe = ptip_ope.ToString();
                        sql = "Select p.cod_proceso, p.tipo_comp, t.descripcion, c.descripcion As concepto From tipo_comp t, concepto c, proceso_contable p " +
                                "Where t.tipo_comp = p.tipo_comp And c.concepto = p.concepto And p.tipo_ope = " + sTipOpe + " And p.fecha_inicial <= SYSDATE And p.fecha_final >= SYSDATE ";
                    }

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        ProcesoContable Proceso = new ProcesoContable();
                        if (resultado["COD_PROCESO"] != DBNull.Value) Proceso.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                        if (resultado["TIPO_COMP"] != DBNull.Value) Proceso.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                        if (resultado["DESCRIPCION"] != DBNull.Value) Proceso.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        if (resultado["CONCEPTO"] != DBNull.Value) Proceso.nom_concepto = Convert.ToString(resultado["CONCEPTO"]);
                        if (Proceso.nom_concepto != null)
                            Proceso.descripcion = Proceso.descripcion + " - " + Proceso.nom_concepto;
                        lstProceso.Add(Proceso);
                    };

                    return lstProceso;

                }
            }
        }


        /// <summary>
        /// Mètodo para generar la contabilizaciòn segùn el tipo de operaciòn correspondiente
        /// </summary>
        /// <param name="pcod_ope"></param>
        /// <param name="ptip_ope"></param>
        /// <param name="pfecha"></param>
        /// <param name="pcod_ofi"></param>
        /// <param name="pcod_persona"></param>
        /// <param name="pcod_proceso"></param>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Boolean GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            pError = "";            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    DbParameter plerror = cmdTransaccionFactory.CreateParameter();
                    try
                    {
                        DbParameter plnum_comp = cmdTransaccionFactory.CreateParameter();
                        DbParameter pltipo_comp = cmdTransaccionFactory.CreateParameter();                        

                        // Realizar el comprobante para las reclamaciones
                        if (ptip_ope == 3)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_RECLAMACIONES";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante para las anulaciones
                        else if (ptip_ope == 7)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plfecha_anula = cmdTransaccionFactory.CreateParameter();
                            plfecha_anula.ParameterName = "pfecha_anula";
                            plfecha_anula.DbType = DbType.Date;
                            plfecha_anula.Value = pfecha;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plfecha_anula);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_ANULACION";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }                        

                        // Realizar el comprobante de causación de cdat
                        else if (ptip_ope == 13)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 13;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUSACIONCDAT";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante para las depreciaciones de activos fijos
                        else if (ptip_ope == 23)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DEPRECIACION";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante para la venta de activos fijos
                        else if (ptip_ope == 22)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_VENTAACTIVOFIJ";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante para la baja de activos fijos
                        else if (ptip_ope == 26)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BAJAACTIVOFIJ";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de causación
                        else if (ptip_ope == 36)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 36;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUSACION";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de clasificación
                        else if (ptip_ope == 37)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 37;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CLASIFICACION";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de provisión
                        else if (ptip_ope == 38)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 38;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            plerror.ParameterName = "perror";
                            plerror.Value = "0";
                            plerror.Direction = ParameterDirection.Output;
                            plerror.DbType = DbType.String;
                            plerror.Size = 1000;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);
                            cmdTransaccionFactory.Parameters.Add(plerror);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PROVISION";
                            cmdTransaccionFactory.ExecuteNonQuery();

                            pError = plerror.Value.ToString();
                            if (pError.Trim() != "")
                                return false;
                        }

                        // Realizar el comprobante de pago de obligaciones & se añidio para genere el comprobante de desembolso de obligaciones
                        else if (ptip_ope == 41 ||ptip_ope == 42 || ptip_ope == 148  )
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = ptipo_comp;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_OBPAGO";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de provisión de obligaciones 
                        else if (ptip_ope == 43)
                        {
                            DbParameter plfecha_corte = cmdTransaccionFactory.CreateParameter();
                            plfecha_corte.ParameterName = "pfecha_corte";
                            plfecha_corte.DbType = DbType.Date;
                            plfecha_corte.Value = pfecha;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pcod_persona;

                            DbParameter plcod_ofi = cmdTransaccionFactory.CreateParameter();
                            plcod_ofi.ParameterName = "pcod_ofi";
                            plcod_ofi.Value = pcod_ofi;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha_corte);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plcod_ofi);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_OBPROVISION";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de causación de AHORROS
                        else if (ptip_ope == 60)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 60;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUSACIONAHORRO";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Provisiòn
                        else if (ptip_ope == 133)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 133;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUS_PROGRAMADO";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        else if (ptip_ope == 134)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 134;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CAUS_APORTES";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Realizar el comprobante de causación
                        else if (ptip_ope == 66)
                        {
                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha_corte";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter pltipo_ope = cmdTransaccionFactory.CreateParameter();
                            pltipo_ope.ParameterName = "ptipo_ope";
                            pltipo_ope.Value = 36;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(pltipo_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PROVIGENERAL";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Deterioro
                        else if (ptip_ope == 106)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            if (pcod_persona != 0)
                                plcod_persona.Value = pcod_persona;
                            else
                                plcod_persona.Value = DBNull.Value;
                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DETERIORONIF";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        
                        // Realizar el comprobante para las aplicaciones masivas
                        else if (ptip_ope == 119 || ptip_ope == 132)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_RECAUDOSMASIVOS";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Generar el comprobante para los movimientos de caja financiera
                        else if (ptip_ope == 120)
                        {
                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                            plfecha.ParameterName = "pfecha";
                            plfecha.DbType = DbType.DateTime;
                            plfecha.Value = pfecha;

                            DbParameter plcod_ofi = cmdTransaccionFactory.CreateParameter();
                            plcod_ofi.ParameterName = "pcod_ofi";
                            plcod_ofi.Value = pcod_ofi;

                            DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                            pcod_caja.ParameterName = "pcod_caja";
                            pcod_caja.Value = DBNull.Value;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                            plcod_usu.ParameterName = "pcod_usu";
                            plcod_usu.Value = pUsuario.codusuario;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plfecha);
                            cmdTransaccionFactory.Parameters.Add(plcod_ofi);
                            cmdTransaccionFactory.Parameters.Add(pcod_caja);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_usu);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_COMPROBANTE";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Reintegro caja menor
                        else if (ptip_ope == 136)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            if (pcod_persona != 0)
                                plcod_persona.Value = pcod_persona;
                            else
                                plcod_persona.Value = DBNull.Value;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;
                            
                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;
                            
                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);
                            
                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CONTACAJAMENOR";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        // Garantias
                        else if (ptip_ope == 137)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            if (pcod_persona != 0)
                                plcod_persona.Value = pcod_persona;
                            else
                                plcod_persona.Value = DBNull.Value;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plvalor = cmdTransaccionFactory.CreateParameter();
                            plvalor.ParameterName = "pvalor";
                            plvalor.Value = 0;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            DbParameter pradicado = cmdTransaccionFactory.CreateParameter();
                            pradicado.ParameterName = "PNUMERORADICACION";
                            pradicado.Value = 0;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plvalor);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);
                            cmdTransaccionFactory.Parameters.Add(pradicado);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_GARANTIAS";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        
                        // Realizar el comprobante para los desembolsos, condonaciones, castigos, refinanciacion
                        else
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            if (pcod_persona != 0)
                                plcod_persona.Value = pcod_persona;
                            else
                                plcod_persona.Value = DBNull.Value;
                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_INTERFACE";
                            cmdTransaccionFactory.ExecuteNonQuery();

                        }

                        if (plnum_comp.Value != DBNull.Value) pnum_comp = Convert.ToInt64(plnum_comp.Value);
                        if (pltipo_comp.Value != DBNull.Value) ptipo_comp = Convert.ToInt64(pltipo_comp.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        // BOExcepcion.Throw("ComprobanteData", "GenerarComprobante", ex);
                        if (plerror.Value != null)
                            pError = ex.Message + plerror.Value.ToString();
                        else
                            pError = ex.Message;
                        return false;
                    }
                }
            }
        }

        
        ///  crear un nuevo cheque
        /// </summary>
        /// <param name="pComprobante"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>

        public Comprobante Crearcheque(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        DbParameter p_num_comp = cmdTransaccionFactory.CreateParameter();
                        p_num_comp.ParameterName = "p_num_comp";
                        p_num_comp.Value = pComprobante.num_comp;
                        p_num_comp.DbType = DbType.Int64;
                        p_num_comp.Direction = ParameterDirection.InputOutput;

                        DbParameter p_entidad = cmdTransaccionFactory.CreateParameter();
                        p_entidad.ParameterName = "p_entidad";
                        p_entidad.Value = pComprobante.entidad;
                        p_entidad.DbType = DbType.Int64;
                        p_entidad.Direction = ParameterDirection.InputOutput;

                        DbParameter p_num_consig = cmdTransaccionFactory.CreateParameter();
                        p_num_consig.ParameterName = "p_num_consig";
                        if (pComprobante.num_consig == null)
                            p_num_consig.Value = "";
                        else
                            p_num_consig.Value = pComprobante.num_consig;
                        p_num_consig.DbType = DbType.String;
                        p_num_consig.Direction = ParameterDirection.Input;


                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pComprobante.n_documento;
                        p_identificacion.DbType = DbType.String;
                        p_identificacion.Direction = ParameterDirection.Input;


                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.DbType = DbType.String;
                        p_tipo.Value = pComprobante.cheque_tipo_identificacion;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.DbType = DbType.String;
                        p_nombre.Value = pComprobante.cheque_nombre;

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha";
                        p_fecha.DbType = DbType.Date;
                        p_fecha.Value = pComprobante.fecha.ToString(conf.ObtenerFormatoFecha());

                        DbParameter p_cod_elaboro = cmdTransaccionFactory.CreateParameter();
                        p_cod_elaboro.ParameterName = "p_cod_elaboro";
                        p_cod_elaboro.DbType = DbType.Int64;
                        p_cod_elaboro.Value = pComprobante.cod_elaboro;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        p_cuenta.DbType = DbType.String;
                        p_cuenta.Value = pComprobante.cuenta;

                        cmdTransaccionFactory.Parameters.Add(p_num_comp);
                        cmdTransaccionFactory.Parameters.Add(p_entidad);
                        cmdTransaccionFactory.Parameters.Add(p_num_consig);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_fecha);
                        cmdTransaccionFactory.Parameters.Add(p_cod_elaboro);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CHEQUECOMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return pComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "CrearComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public Comprobante ConsultarCheque(Int64 pNumComp, Int64 pTipoComp, Usuario vUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CHEQUE WHERE NUM_COMP = " + pNumComp.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCHEQUE"] != DBNull.Value) entidad.cheque_id = Convert.ToInt64(resultado["IDCHEQUE"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.cheque_iden_benef = Convert.ToString(resultado["IDEN_BENEF"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.cheque_tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE_BENEFICIARIO"] != DBNull.Value) entidad.cheque_nombre = Convert.ToString(resultado["NOMBRE_BENEFICIARIO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarCheque", ex);
                        return null;
                    }
                }
            }
        }

        public bool Validar(DetalleComprobante cargacomprobante, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_CUENTACONTABLE = cmdTransaccionFactory.CreateParameter();
                        P_CUENTACONTABLE.ParameterName = "P_CUENTACONTABLE";
                        P_CUENTACONTABLE.Value = cargacomprobante.cod_cuenta;
                        P_CUENTACONTABLE.DbType = DbType.Int64;
                        P_CUENTACONTABLE.Direction = ParameterDirection.Input;

                        DbParameter P_CENTRO_COSTO = cmdTransaccionFactory.CreateParameter();
                        P_CENTRO_COSTO.ParameterName = "P_CENTRO_COSTO";
                        P_CENTRO_COSTO.Value = cargacomprobante.centro_costo;
                        P_CENTRO_COSTO.DbType = DbType.Int64;
                        P_CENTRO_COSTO.Direction = ParameterDirection.Input;

                        DbParameter P_TIPO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO.ParameterName = "P_TIPO";
                        P_TIPO.Value = cargacomprobante.tipo;
                        P_TIPO.DbType = DbType.Int64;
                        P_TIPO.Direction = ParameterDirection.Input;

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = cargacomprobante.valor;
                        P_VALOR.DbType = DbType.Int64;
                        P_VALOR.Direction = ParameterDirection.Input;

                        DbParameter P_DETALLE = cmdTransaccionFactory.CreateParameter();
                        P_DETALLE.ParameterName = "P_DETALLE";
                        P_DETALLE.Value = cargacomprobante.detalle;
                        P_DETALLE.DbType = DbType.DateTime;
                        P_DETALLE.Direction = ParameterDirection.Input;

                        DbParameter P_CENTRO_GESTION = cmdTransaccionFactory.CreateParameter();
                        P_CENTRO_GESTION.ParameterName = "P_CENTRO_GESTION";
                        P_CENTRO_GESTION.Value = cargacomprobante.centro_gestion;
                        P_CENTRO_GESTION.DbType = DbType.Int64;
                        P_CENTRO_GESTION.Direction = ParameterDirection.Input;

                        DbParameter P_TERCERO = cmdTransaccionFactory.CreateParameter();
                        P_TERCERO.ParameterName = "P_TERCERO";
                        P_TERCERO.Value = cargacomprobante.tercero;
                        P_TERCERO.DbType = DbType.Int64;
                        P_TERCERO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_CUENTACONTABLE);
                        cmdTransaccionFactory.Parameters.Add(P_CENTRO_COSTO);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO);
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);
                        cmdTransaccionFactory.Parameters.Add(P_DETALLE);
                        cmdTransaccionFactory.Parameters.Add(P_CENTRO_GESTION);
                        cmdTransaccionFactory.Parameters.Add(P_TERCERO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_VALIDARDETALLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;

                    }
                    catch (Exception ex)
                    {
                        Error = cargacomprobante.cod_cuenta + " " + ex.Message;
                        return false;
                    }

                }
            }
        }

        public DetalleComprobante CrearCargaComprobanteDetalle(DetalleComprobante cargacomprobante, ref string error, Usuario pUsuario)
        {
            error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        P_COD_OPE.ParameterName = "P_COD_OPE";
                        P_COD_OPE.Value = 1;
                        P_COD_OPE.DbType = DbType.Int64;
                        P_COD_OPE.Direction = ParameterDirection.InputOutput;

                        DbParameter P_CUENTACONTABLE = cmdTransaccionFactory.CreateParameter();
                        P_CUENTACONTABLE.ParameterName = "P_CUENTACONTABLE";
                        P_CUENTACONTABLE.Value = cargacomprobante.cod_cuenta;
                        P_CUENTACONTABLE.DbType = DbType.Int64;
                        P_CUENTACONTABLE.Direction = ParameterDirection.Input;

                        DbParameter P_CENTRO_COSTO = cmdTransaccionFactory.CreateParameter();
                        P_CENTRO_COSTO.ParameterName = "P_CENTRO_COSTO";
                        if (cargacomprobante.centro_costo == null)
                            P_CENTRO_COSTO.Value = DBNull.Value;
                        else
                            P_CENTRO_COSTO.Value = cargacomprobante.centro_costo;
                        P_CENTRO_COSTO.DbType = DbType.Int64;
                        P_CENTRO_COSTO.Direction = ParameterDirection.Input;

                        DbParameter P_TIPO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO.ParameterName = "P_TIPO";
                        P_TIPO.Value = cargacomprobante.tipo;
                        P_TIPO.DbType = DbType.String;
                        P_TIPO.Direction = ParameterDirection.Input;

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        if (cargacomprobante.valor == null)
                            P_VALOR.Value = DBNull.Value;
                        else
                            P_VALOR.Value = cargacomprobante.valor;
                        P_VALOR.DbType = DbType.Int64;
                        P_VALOR.Direction = ParameterDirection.Input;

                        DbParameter P_DETALLE = cmdTransaccionFactory.CreateParameter();
                        P_DETALLE.ParameterName = "P_DETALLE";
                        P_DETALLE.Value = cargacomprobante.detalle;
                        P_DETALLE.DbType = DbType.String;
                        P_DETALLE.Direction = ParameterDirection.Input;

                        DbParameter P_CENTRO_GESTION = cmdTransaccionFactory.CreateParameter();
                        P_CENTRO_GESTION.ParameterName = "P_CENTRO_GESTION";
                        if (cargacomprobante.centro_gestion == null)
                            P_CENTRO_GESTION.Value = DBNull.Value;
                        else
                            P_CENTRO_GESTION.Value = cargacomprobante.centro_gestion;
                        P_CENTRO_GESTION.DbType = DbType.Int64;
                        P_CENTRO_GESTION.Direction = ParameterDirection.Input;

                        DbParameter P_TERCERO = cmdTransaccionFactory.CreateParameter();
                        P_TERCERO.ParameterName = "P_TERCERO";
                        if (cargacomprobante.tercero == null)
                            P_TERCERO.Value = DBNull.Value;
                        else
                            P_TERCERO.Value = cargacomprobante.tercero;
                        P_TERCERO.DbType = DbType.Int64;
                        P_TERCERO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_COD_OPE);
                        cmdTransaccionFactory.Parameters.Add(P_CUENTACONTABLE);
                        cmdTransaccionFactory.Parameters.Add(P_CENTRO_COSTO);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO);
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);
                        cmdTransaccionFactory.Parameters.Add(P_DETALLE);
                        cmdTransaccionFactory.Parameters.Add(P_CENTRO_GESTION);
                        cmdTransaccionFactory.Parameters.Add(P_TERCERO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TEMPCOMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cargacomprobante.operacion = Convert.ToString(P_COD_OPE.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return cargacomprobante;
                    }
                    catch (Exception ex)
                    {
                        // BOExcepcion.Throw("ComprobanteData", "CrearCargaComprobanteDetalle", ex);
                        error = ex.Message;
                        return null;
                    }
                }
            }
        }

        public void EliminarCargaComprobanteDetalle(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter P_COD_OPE = cmdTransaccionFactory.CreateParameter();
                        P_COD_OPE.ParameterName = "P_COD_OPE";
                        P_COD_OPE.Value = pId;
                        P_COD_OPE.DbType = DbType.Int64;
                        P_COD_OPE.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(P_COD_OPE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TEMPCOMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "EliminarCargaComprobanteDetalle", ex);
                        return;
                    }

                }
            }
        }

        public List<DetalleComprobante> ConsultarCargaComprobanteDetalle(Int64 operacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleComprobante> lstCargaComprobante = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select TEMP_COMPROBANTE.*, PLAN_CUENTAS.NOMBRE AS NOMBRE_CUENTA, V_PERSONA.IDENTIFICACION, V_PERSONA.NOMBRE AS NOM_TERCERO,
                                        PLAN_CUENTAS.MANEJA_TER, PLAN_CUENTAS.IMPUESTO,PLAN_CUENTAS_HOMOLOGA.COD_CUENTA_NIIF,PLAN_CUENTAS_NIIF.NOMBRE AS NOM_CUENTA_NIIF
                                        From TEMP_COMPROBANTE LEFT JOIN PLAN_CUENTAS ON TEMP_COMPROBANTE.COD_CUENTA = PLAN_CUENTAS.COD_CUENTA
                                        LEFT JOIN V_PERSONA ON TEMP_COMPROBANTE.TERCERO = V_PERSONA.COD_PERSONA
                                        LEFT JOIN PLAN_CUENTAS_HOMOLOGA ON PLAN_CUENTAS_HOMOLOGA.COD_CUENTA = TEMP_COMPROBANTE.COD_CUENTA
                                        LEFT JOIN PLAN_CUENTAS_NIIF ON PLAN_CUENTAS_NIIF.COD_CUENTA_NIIF = PLAN_CUENTAS_HOMOLOGA.COD_CUENTA_NIIF                                        
                                        Where COD_OPE  = " + operacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();

                            if (resultado["COD_OPE"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["COD_OPE"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                            if (resultado["BASE_COMP"] != DBNull.Value) entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOM_CUENTA_NIIF"] != DBNull.Value) entidad.nombre_cuenta_nif = Convert.ToString(resultado["NOM_CUENTA_NIIF"]);
                            entidad.moneda = 1;
                            lstCargaComprobante.Add(entidad);
                        }

                        return lstCargaComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarCargaComprobanteDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public List<DetalleComprobante> ConsultarCargaComprobanteNiifDetalle(Int64 operacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleComprobante> lstCargaComprobante = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT TEMP_COMPROBANTE.*, PLAN_CUENTAS_NIIF.NOMBRE AS NOM_CUENTA_NIIF, V_PERSONA.IDENTIFICACION, V_PERSONA.NOMBRE AS NOM_TERCERO,
                                        PLAN_CUENTAS_NIIF.MANEJA_TER, PLAN_CUENTAS_NIIF.IMPUESTO
                                        From TEMP_COMPROBANTE LEFT JOIN PLAN_CUENTAS_NIIF ON TEMP_COMPROBANTE.COD_CUENTA = PLAN_CUENTAS_NIIF.COD_CUENTA_NIIF
                                        LEFT JOIN V_PERSONA ON TEMP_COMPROBANTE.TERCERO = V_PERSONA.COD_PERSONA
                                        Where COD_OPE  = " + operacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();

                            if (resultado["COD_OPE"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["COD_OPE"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOM_CUENTA_NIIF"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOM_CUENTA_NIIF"]);                            
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                            if (resultado["BASE_COMP"] != DBNull.Value) entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;                           
                            entidad.moneda = 1;
                            lstCargaComprobante.Add(entidad);
                        }

                        return lstCargaComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarCargaComprobanteNiifDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public List<Comprobante> ListarComprobanteParaAprobar(Comprobante pComprobante, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Comprobante> lstComprobante = new List<Comprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string condicion = ObtenerFiltro(pComprobante, "v_comprobante.");
                        string sql = "Select v_comprobante.*, u.nombre As Elaboro, ua.nombre As Aprobo from v_comprobante Left Join usuarios u On v_comprobante.cod_elaboro = u.codusuario Left Join usuarios ua On v_comprobante.cod_aprobo = ua.codusuario ";
                        if (condicion.Trim() != "")
                            sql += condicion;
                        if (sql.ToUpper().Contains("WHERE"))
                        {
                            String ultimaCondicion = condicion.Substring(condicion.Length - 4, 4);
                            if (ultimaCondicion.Trim().ToLower() == "and")
                            {
                                if (pComprobante.rptaLista == true)
                                    sql += " v_comprobante.estado in ('E','A')";
                                else
                                    sql += " v_comprobante.estado = 'E'";
                            }
                            else
                            {
                                if (pComprobante.rptaLista == true)
                                    sql += " AND v_comprobante.estado in ('E','A')";
                                else
                                    sql += " AND v_comprobante.estado = 'E'";
                            }
                        }
                        else
                        {
                            if (pComprobante.rptaLista == true)
                                sql += " WHERE v_comprobante.estado in ('E','A')";
                            else
                                sql += " WHERE v_comprobante.estado = 'E'";
                        }

                        sql = sql + " and ( v_comprobante.num_comp ||'*'|| v_comprobante.tipo_comp not in(select  num_comp ||'*'|| tipo_comp  from COMPROBANTE_ANULADO  )  and  v_comprobante.num_comp || '*' || v_comprobante.tipo_comp not in(select  num_comp_anula || '*' || tipo_comp_anula  from COMPROBANTE_ANULADO)) ";

                        if (filtro.Trim() != "")
                            sql += filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Comprobante entidad = new Comprobante();

                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION_CONCEPTO"] != DBNull.Value) entidad.descripcion_concepto = Convert.ToString(resultado["DESCRIPCION_CONCEPTO"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToInt64(resultado["CIUDAD"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.iden_benef = Convert.ToString(resultado["IDEN_BENEF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["ELABORO"] != DBNull.Value) entidad.elaboro = Convert.ToString(resultado["ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["APROBO"] != DBNull.Value) entidad.aprobo = Convert.ToString(resultado["APROBO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TOTALCOM"] != DBNull.Value) entidad.totalcom = Convert.ToDecimal(resultado["TOTALCOM"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            lstComprobante.Add(entidad);
                        }

                        return lstComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ListarComprobanteParaAprobar", ex);
                        return null;
                    }
                }
            }
        }

        public Boolean AprobarAnularComprobante(Comprobante pComprobante, ref string Error, Usuario vUsuario)
        {
            Error = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pComprobante.num_comp;
                        pNUM_COMP.Direction = ParameterDirection.Input;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pComprobante.tipo_comp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pCOD_APROBO = cmdTransaccionFactory.CreateParameter();
                        pCOD_APROBO.ParameterName = "p_cod_aprobo";
                        if (pComprobante.cod_aprobo == null)
                            pCOD_APROBO.Value = vUsuario.codusuario;
                        else
                            pCOD_APROBO.Value = pComprobante.cod_aprobo;
                        pCOD_APROBO.DbType = DbType.Int64;
                        pCOD_APROBO.Direction = ParameterDirection.Input;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        pESTADO.Value = pComprobante.estado;
                        pESTADO.DbType = DbType.AnsiString;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.Size = 1;

                        DbParameter PTIPOMOTIVO = cmdTransaccionFactory.CreateParameter();
                        PTIPOMOTIVO.ParameterName = "P_TIPO_MOTIVO";
                        if (pComprobante.tipo_motivo > 0) PTIPOMOTIVO.Value = pComprobante.tipo_motivo; else PTIPOMOTIVO.Value = DBNull.Value;
                        PTIPOMOTIVO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pCOD_APROBO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(PTIPOMOTIVO);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPRO_APROANULA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        if (pComprobante.estado == "N")
                        {
                            DAauditoria.InsertarLog(pComprobante, "e_comingres/e_comegres/e_comconta", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Comprobante, "Creacion de anulacion de comprobante con numero de comprobante " + pComprobante.num_comp + " y tipo de comprobante " + pComprobante.tipo_comp);
                        }
                        else if (pComprobante.estado == "A")
                        {
                            DAauditoria.InsertarLog(pComprobante, "e_comingres/e_comegres/e_comconta", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Comprobante, "Creacion de aprobacion de comprobante con numero de comprobante " + pComprobante.num_comp + " y tipo de comprobante " + pComprobante.tipo_comp);
                        }

                        return true;
                    }
                    catch (Exception ex)
                    {
                        //BOExcepcion.Throw("ComprobanteData", "AprobarAnularComprobante", ex);
                        Error = ex.Message;
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Int64 pTipoComp, DateTime pFecha, Int64 pOficina, Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pTipoComp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        if (pFecha == null)
                            pFECHA.Value = DBNull.Value;
                        else
                            pFECHA.Value = pFecha;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pOFICINA = cmdTransaccionFactory.CreateParameter();
                        pOFICINA.ParameterName = "p_oficina";
                        if (pOficina == null)
                            pOFICINA.Value = DBNull.Value;
                        else
                            pOFICINA.Value = pOficina;
                        pOFICINA.DbType = DbType.Int64;
                        pOFICINA.Direction = ParameterDirection.Input;

                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = 0;
                        pNUM_COMP.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_NUMEROCOMPRO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        resultado = Convert.ToInt64(pNUM_COMP.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ObtenerSiguienteCodigo", ex);
                        return 0;
                    }
                }
            }
        }

        //AGREGADO
        public DetalleComprobante Identificacion_RETORNA_CodPersona(string pident, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DetalleComprobante entidad = new DetalleComprobante();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select Cod_Persona, (Case tipo_persona When 'N' Then QUITARESPACIOS(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)) Else razon_social End) AS nombre From Persona where Identificacion = '" + pident + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOMBRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "Identificacion_RETORNA_CodPersona", ex);
                        return null;
                    }
                }
            }
        }




        //AGREGADO


        public Comprobante ConsultarComprobanteTipoMotivoAnulacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_MOTIVO_ANULACION WHERE TIPO_MOTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt32(resultado["TIPO_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return null;
                    }
                }
            }
        }


        public List<Comprobante> ListarComprobanteTipoMotivoAnulacion(Comprobante pComprobante, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Comprobante> lstComprobante = new List<Comprobante>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_MOTIVO_ANULACION " + ObtenerFiltro(pComprobante) + " ORDER BY TIPO_MOTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Comprobante entidad = new Comprobante();
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt32(resultado["TIPO_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstComprobante.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ListarComprobante", ex);
                        return null;
                    }
                }
            }
        }




        public void Anularcomprobante(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDANULACION = cmdTransaccionFactory.CreateParameter();
                        P_IDANULACION.ParameterName = "P_IDANULACION";
                        P_IDANULACION.Value = pComprobante.idanulaicon;
                        P_IDANULACION.Direction = ParameterDirection.Input;

                        DbParameter P_TIPO_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_MOTIVO.ParameterName = "P_TIPO_MOTIVO";
                        P_TIPO_MOTIVO.Value = pComprobante.tipo_motivo;
                        P_TIPO_MOTIVO.Direction = ParameterDirection.Input;

                        DbParameter P_FECHA_ANULACION = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_ANULACION.ParameterName = "P_FECHA_ANULACION";
                        if (pComprobante.fecha == null)
                            P_FECHA_ANULACION.Value = DBNull.Value;
                        else
                            P_FECHA_ANULACION.Value = pComprobante.fecha;
                        P_FECHA_ANULACION.DbType = DbType.DateTime;
                        P_FECHA_ANULACION.Direction = ParameterDirection.Input;

                        DbParameter P_NUM_COMP = cmdTransaccionFactory.CreateParameter();
                        P_NUM_COMP.ParameterName = "P_NUM_COMP";
                        P_NUM_COMP.Value = pComprobante.num_comp;

                        DbParameter P_TIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_COMP.ParameterName = "P_TIPO_COMP";
                        P_TIPO_COMP.Value = pComprobante.tipo_comp;
                        
                        DbParameter P_NUM_COMP_ANULA= cmdTransaccionFactory.CreateParameter();
                        P_NUM_COMP_ANULA.ParameterName = "P_NUM_COMP_ANULA";
                        P_NUM_COMP_ANULA.Value = pComprobante.num_comp_anula;

                        DbParameter P_TIPO_COMP_ANULA = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_COMP_ANULA.ParameterName = "P_TIPO_COMP_ANULA";
                        P_TIPO_COMP_ANULA.Value = pComprobante.tipo_comp_anula;


                        DbParameter P_USUARIO = cmdTransaccionFactory.CreateParameter();
                        P_USUARIO.ParameterName = "P_USUARIO";
                        P_USUARIO.Value = pComprobante.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(P_IDANULACION);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_MOTIVO);
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_ANULACION);
                        cmdTransaccionFactory.Parameters.Add(P_NUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(P_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(P_NUM_COMP_ANULA);
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_COMP_ANULA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPANU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pComprobante, "e_comingres/e_comegres/e_comconta", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Comprobante, "Creacion de anulacion de comprobante con numero de comprobante " + pComprobante.num_comp + " y tipo de comprobante " + pComprobante.tipo_comp);

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ModificarGiro", ex);
                    }
                }
            }
        }
        ////detalle comprobante 


        public List<DetalleComprobante> ConsultarAnulaciondetalle(Int64 pnum_comp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<DetalleComprobante> LstDetComp = new List<DetalleComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * FROM d_comprobante e WHERE e.num_comp = " + pnum_comp.ToString() + " AND e.tipo_comp = " + ptipo_comp.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleComprobante entidad = new DetalleComprobante();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE_NIIF"] != DBNull.Value) entidad.nombre_cuenta_nif = Convert.ToString(resultado["NOMCUE_NIIF"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToInt64(resultado["MONEDA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["BASE_COMP"] != DBNull.Value) entidad.base_comp = Convert.ToDecimal(resultado["BASE_COMP"]); else entidad.base_comp = 0;
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]); else entidad.porcentaje = 0;
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]); else entidad.base_comp = 0;
                            if (resultado["NUMERO_TRANSACCION"] != DBNull.Value) entidad.numero_transaccion = Convert.ToString(resultado["NUMERO_TRANSACCION"]); else entidad.porcentaje = 0;

                            LstDetComp.Add(entidad);
                        };

                        return LstDetComp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarComprobante", ex);
                        return null;
                    }
                }
            }
        }


        public Comprobante crearanulacioncomprobante(Comprobante pComprobante, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUM_COMP = cmdTransaccionFactory.CreateParameter();
                        pNUM_COMP.ParameterName = "p_num_comp";
                        pNUM_COMP.Value = pComprobante.num_comp;
                        pNUM_COMP.Direction = ParameterDirection.InputOutput;

                        DbParameter pTIPO_COMP = cmdTransaccionFactory.CreateParameter();
                        pTIPO_COMP.ParameterName = "p_tipo_comp";
                        pTIPO_COMP.Value = pComprobante.tipo_comp;
                        pTIPO_COMP.Direction = ParameterDirection.Input;

                        DbParameter pNUM_CONSIG = cmdTransaccionFactory.CreateParameter();
                        pNUM_CONSIG.ParameterName = "p_num_consig";
                        if (pComprobante.num_consig == null)
                            pNUM_CONSIG.Value = "";
                        else
                            pNUM_CONSIG.Value = pComprobante.num_consig;
                        pNUM_CONSIG.DbType = DbType.AnsiString;
                        pNUM_CONSIG.Direction = ParameterDirection.Input;
                        pNUM_CONSIG.Size = 50;

                        DbParameter pN_DOCUMENTO = cmdTransaccionFactory.CreateParameter();
                        pN_DOCUMENTO.ParameterName = "p_n_documento";
                        if (pComprobante.n_documento == null)
                            pN_DOCUMENTO.Value = "";
                        else
                            pN_DOCUMENTO.Value = pComprobante.n_documento;
                        pN_DOCUMENTO.DbType = DbType.AnsiString;
                        pN_DOCUMENTO.Direction = ParameterDirection.Input;
                        pN_DOCUMENTO.Size = 50;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.Value = pComprobante.fecha;
                        pFECHA.DbType = DbType.DateTime;

                        DbParameter pHORA = cmdTransaccionFactory.CreateParameter();
                        pHORA.ParameterName = "p_hora";
                        pHORA.Value = pComprobante.hora;
                        pHORA.DbType = DbType.DateTime;

                        DbParameter pCIUDAD = cmdTransaccionFactory.CreateParameter();
                        pCIUDAD.ParameterName = "p_ciudad";
                        pCIUDAD.Value = pComprobante.ciudad;

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = pComprobante.concepto;

                        DbParameter pTIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PAGO.ParameterName = "p_tipo_pago";
                        if (pComprobante.tipo_pago != null)
                            pTIPO_PAGO.Value = pComprobante.tipo_pago;
                        else
                            pTIPO_PAGO.Value = DBNull.Value;

                        DbParameter pENTIDAD = cmdTransaccionFactory.CreateParameter();
                        pENTIDAD.ParameterName = "p_entidad";
                        if (pComprobante.entidad == null)
                            pENTIDAD.Value = DBNull.Value;
                        else
                            pENTIDAD.Value = pComprobante.entidad;

                        DbParameter pTOTALCOM = cmdTransaccionFactory.CreateParameter();
                        pTOTALCOM.ParameterName = "p_totalcom";
                        pTOTALCOM.Value = pComprobante.totalcom;

                        DbParameter pTIPO_BENEF = cmdTransaccionFactory.CreateParameter();
                        pTIPO_BENEF.ParameterName = "p_tipo_benef";
                        pTIPO_BENEF.Value = pComprobante.tipo_benef;

                        DbParameter pCOD_BENEF = cmdTransaccionFactory.CreateParameter();
                        pCOD_BENEF.ParameterName = "p_cod_benef";
                        pCOD_BENEF.Value = pComprobante.cod_benef;

                        DbParameter pCOD_ELABORO = cmdTransaccionFactory.CreateParameter();
                        pCOD_ELABORO.ParameterName = "p_cod_elaboro";
                        pCOD_ELABORO.Value = pComprobante.cod_elaboro;

                        DbParameter pCOD_APROBO = cmdTransaccionFactory.CreateParameter();
                        pCOD_APROBO.ParameterName = "p_cod_aprobo";
                        if (pComprobante.cod_aprobo == null)
                        {
                            pCOD_APROBO.Value = 0;
                            pCOD_APROBO.DbType = DbType.Int64;
                        }
                        else
                        {
                            pCOD_APROBO.Value = pComprobante.cod_aprobo;
                        }

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        if (pComprobante.estado == null || pComprobante.estado == "")
                            pESTADO.Value = 'E';
                        else
                            pESTADO.Value = pComprobante.estado;
                        pESTADO.DbType = DbType.AnsiString;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.Size = 1;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        if (pComprobante.observaciones == null)
                            pOBSERVACIONES.Value = "";
                        else
                            pOBSERVACIONES.Value = pComprobante.observaciones;
                        pOBSERVACIONES.DbType = DbType.AnsiString;
                        pOBSERVACIONES.Size = 200;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        if (pComprobante.cuenta == null)
                            p_cuenta.Value = DBNull.Value;
                        else
                            p_cuenta.Value = pComprobante.cuenta;

                        cmdTransaccionFactory.Parameters.Add(pNUM_COMP);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_COMP);
                        cmdTransaccionFactory.Parameters.Add(pNUM_CONSIG);
                        cmdTransaccionFactory.Parameters.Add(pN_DOCUMENTO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pHORA);
                        cmdTransaccionFactory.Parameters.Add(pCIUDAD);
                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pENTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pTOTALCOM);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BENEF);
                        cmdTransaccionFactory.Parameters.Add(pCOD_ELABORO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_APROBO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pComprobante.num_comp = Convert.ToInt64(pNUM_COMP.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return pComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "CrearComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 consultacod_persona(string cod, Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    string sql = "Select cod_persona From persona Where identificacion = '" + cod + "'";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                    return resultado;

                }
            }
        }

        public Int64 ConsultarOperacion(Int64 pnumComp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64 operacion = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select cod_ope from operacion where tipo_comp= " + ptipo_comp.ToString() + "and  num_comp = " + pnumComp.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_ope"] != DBNull.Value) operacion = Convert.ToInt64(resultado["cod_ope"]);

                        }

                        return operacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "Consultar_Operacion", ex);
                        return operacion;
                    }
                }
            }
        }


        public Comprobante ConsultarObservacionesAnulacion(Int64 pnumComp, Int64 ptipo_comp, Usuario pUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.*,t.descripcion,U.nombre FROM  comprobante_anulado c inner join TIPO_MOTIVO_ANULACION t on t.tipo_motivo=c.tipo_motivo inner join usuarios u on u.codusuario=C.codusuario WHERE c.num_comp = " + pnumComp.ToString() + "and c.tipo_comp=" + ptipo_comp.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt32(resultado["TIPO_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_ANULACION"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA_ANULACION"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["NUM_COMP_ANULA"] != DBNull.Value) entidad.num_comp_anula = Convert.ToInt64(resultado["NUM_COMP_ANULA"]);
                            if (resultado["TIPO_COMP_ANULA"] != DBNull.Value) entidad.tipo_comp_anula = Convert.ToInt64(resultado["TIPO_COMP_ANULA"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarObservacionesAnulacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo para generar la contabilizaciòn segùn el tipo de operaciòn correspondiente a la caja
        /// </summary>
        /// <param name="pcod_ope"></param>
        /// <param name="ptip_ope"></param>
        /// <param name="pfecha"></param>
        /// <param name="pcod_ofi"></param>
        /// <param name="pcod_caja"></param>
        /// <param name="pcod_persona"></param>
        /// <param name="pcod_proceso"></param>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Boolean GenerarComprobanteCaja(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_caja, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string pError, Usuario pUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter plnum_comp = cmdTransaccionFactory.CreateParameter();
                        DbParameter pltipo_comp = cmdTransaccionFactory.CreateParameter();
                        DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                        plcod_persona.ParameterName = "pcod_persona";
                        plcod_persona.Value = pcod_persona;

                        DbParameter plfecha = cmdTransaccionFactory.CreateParameter();
                        plfecha.ParameterName = "pfecha";
                        plfecha.DbType = DbType.DateTime;
                        plfecha.Value = pfecha;

                        DbParameter plcod_ofi = cmdTransaccionFactory.CreateParameter();
                        plcod_ofi.ParameterName = "pcod_ofi";
                        plcod_ofi.Value = pcod_ofi;

                        DbParameter plcod_caja = cmdTransaccionFactory.CreateParameter();
                        plcod_caja.ParameterName = "pcod_caja";
                        plcod_caja.Value = pcod_caja;

                        DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                        plcod_proceso.ParameterName = "pcod_proceso";
                        plcod_proceso.Value = pcod_proceso;

                        DbParameter plcod_usu = cmdTransaccionFactory.CreateParameter();
                        plcod_usu.ParameterName = "pcod_usu";
                        plcod_usu.Value = pUsuario.codusuario;

                        plnum_comp.ParameterName = "pnum_comp";
                        plnum_comp.Value = 0;
                        plnum_comp.Direction = ParameterDirection.Output;

                        pltipo_comp.ParameterName = "ptipo_comp";
                        pltipo_comp.Value = 0;
                        pltipo_comp.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(plcod_persona);
                        cmdTransaccionFactory.Parameters.Add(plfecha);
                        cmdTransaccionFactory.Parameters.Add(plcod_ofi);
                        cmdTransaccionFactory.Parameters.Add(plcod_caja);
                        cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                        cmdTransaccionFactory.Parameters.Add(plcod_usu);
                        cmdTransaccionFactory.Parameters.Add(plnum_comp);
                        cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_COMPROBANTE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (plnum_comp != null) pnum_comp = Convert.ToInt64(plnum_comp.Value);
                        if (pltipo_comp != null) ptipo_comp = Convert.ToInt64(pltipo_comp.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;

                    }
                    catch (Exception ex)
                    {
                        // BOExcepcion.Throw("ComprobanteData", "GenerarComprobanteCaja", ex);
                        pError = ex.Message;
                        return false;
                    }
                }
            } 
        }

        public List<DetalleComprobante> AsignarTercero(List<DetalleComprobante> pLstDetalle,string pTipoNorma, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;

                        foreach (DetalleComprobante entidad in pLstDetalle)
                        {
                            if (entidad.identificacion != "" && entidad.identificacion != null)
                            {
                                string sql = "select Cod_Persona, (Case tipo_persona When 'N' Then QUITARESPACIOS(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)) Else razon_social End) AS nombre From Persona where Identificacion = '" + entidad.identificacion + "'";
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                if (resultado.Read())
                                {
                                    if (resultado["COD_PERSONA"] != DBNull.Value) entidad.tercero = Convert.ToInt64(resultado["COD_PERSONA"]);
                                    if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOMBRE"]);
                                }
                                
                            }

                            if (pTipoNorma == "L")
                            {
                                if (entidad.cod_cuenta != "" && entidad.cod_cuenta != null)
                                {
                                    string sql = @"SELECT p.* FROM plan_cuentas p WHERE p.cod_cuenta = '" + entidad.cod_cuenta + "' ";
                                    cmdTransaccionFactory.CommandText = sql;
                                    resultado = cmdTransaccionFactory.ExecuteReader();

                                    if (resultado.Read())
                                    {
                                        if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMBRE"]);
                                        if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                                        if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                                    }
                                }
                            }
                            else
                            {
                                if (entidad.cod_cuenta_niif != "" && entidad.cod_cuenta_niif != null)
                                {
                                    string sql = @"SELECT p.* FROM plan_cuentas_niif p WHERE p.cod_cuenta_niif = '" + entidad.cod_cuenta_niif + "' ";
                                    cmdTransaccionFactory.CommandText = sql;
                                    resultado = cmdTransaccionFactory.ExecuteReader();

                                    if (resultado.Read())
                                    {
                                        if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMBRE"]);
                                        if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                                        if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                                    }
                                }
                            }
                         
                            
                        }

                        return pLstDetalle;
                    }
                    catch
                    {
                        return pLstDetalle;
                    }
                }
            }
        }
        //Agregado para validar cuentas contables en comprobantes
	    //Modificación para indicar si el comprobante es nuevo
        public Int32 ValidarCuentaContable(Int64 nuevo, string cod_cuenta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_validacion = cmdTransaccionFactory.CreateParameter();
                        p_validacion.ParameterName = "pvalidar";
                        p_validacion.Value = 0;
                        p_validacion.DbType = DbType.Int64;
                        p_validacion.Direction = ParameterDirection.Output;

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cod_cuenta";
                        p_cuenta.Value = cod_cuenta;
                        p_cuenta.DbType = DbType.String;
                        p_cuenta.Direction = ParameterDirection.Input;

			            DbParameter p_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_nuevo.ParameterName = "p_nuevo";
                        p_nuevo.Value = nuevo;
                        p_nuevo.DbType = DbType.Int64;
                        p_nuevo.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(p_validacion);
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);
			            cmdTransaccionFactory.Parameters.Add(p_nuevo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_VALIDAR_CUEN_COM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Int32 validar = Convert.ToInt32(p_validacion.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return validar;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ValidarCuentaContable", ex);
                        return 0;
                    }

                }
            }
        }

        public bool GenerarComprobanteTraslado(DateTime fecha_contabilizacion, Int64 cod_traslado, ref Int64 num_comp, ref Int64 tipo_comp, ref string error, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_fecha_comprobante = cmdTransaccionFactory.CreateParameter();
                        p_fecha_comprobante.ParameterName = "p_fecha_comprobante";
                        p_fecha_comprobante.Value = fecha_contabilizacion;
                        p_fecha_comprobante.DbType = DbType.DateTime;
                        p_fecha_comprobante.Direction = ParameterDirection.Input;

                        DbParameter p_cod_traslado = cmdTransaccionFactory.CreateParameter();
                        p_cod_traslado.ParameterName = "p_cod_traslado";
                        p_cod_traslado.Value = cod_traslado;
                        p_cod_traslado.DbType = DbType.Int64;
                        p_cod_traslado.Direction = ParameterDirection.Input;

                        DbParameter p_num_comp = cmdTransaccionFactory.CreateParameter();
                        p_num_comp.ParameterName = "p_num_comp";
                        p_num_comp.Value = 0;
                        p_num_comp.DbType = DbType.Int64;
                        p_num_comp.Direction = ParameterDirection.Output;

                        DbParameter p_tipo_comp = cmdTransaccionFactory.CreateParameter();
                        p_tipo_comp.ParameterName = "p_tipo_comp";
                        p_tipo_comp.Value = 0;
                        p_tipo_comp.DbType = DbType.Int64;
                        p_tipo_comp.Direction = ParameterDirection.Output;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Value = 0;
                        p_error.DbType = DbType.AnsiStringFixedLength;
                        p_error.Size = 200;
                        p_error.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(p_fecha_comprobante);
                        cmdTransaccionFactory.Parameters.Add(p_cod_traslado);
                        cmdTransaccionFactory.Parameters.Add(p_num_comp);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TRASLADOSALDOTER";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        try { num_comp = p_num_comp != null ? (p_num_comp.Value != null ? Convert.ToInt64(p_num_comp.Value.ToString()) : 0) : 0; } catch { }
                        try { tipo_comp = p_tipo_comp != null ? (p_tipo_comp.Value != null ? Convert.ToInt64(p_tipo_comp.Value.ToString()) : 0) : 0; } catch { }
                        if (p_error != null)
                            if (p_error.Value != null)
                                error = Convert.ToString(p_error.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ValidarCuentaContable", ex);
                        return false;
                    }

                }
            }
        }

        public String HomologarCuentaContable(Int64 ptipo_comp, string pcod_cuenta, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipo_comp = cmdTransaccionFactory.CreateParameter();
                        p_tipo_comp.ParameterName = "p_tipo_comp";
                        p_tipo_comp.Value = ptipo_comp;
                        p_tipo_comp.DbType = DbType.Int64;
                        p_tipo_comp.Direction = ParameterDirection.Input;

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "p_cod_cuenta";
                        p_cod_cuenta.Value = pcod_cuenta;
                        p_cod_cuenta.DbType = DbType.String;
                        p_cod_cuenta.Direction = ParameterDirection.Input;

                        DbParameter p_cod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        p_cod_cuenta_niif.Value = "12345678901234567890";
                        p_cod_cuenta_niif.DbType = DbType.String;
                        p_cod_cuenta_niif.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_HOMOLOGANIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string cuentaNIIF = Convert.ToString(p_cod_cuenta_niif.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return cuentaNIIF;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "HomologarCuentaContable", ex);
                        return "";
                    }

                }
            }
        }

        public String HomologarCuentaNIIF(string pcod_cuenta, Usuario pUsuario)
        {
            string lcod_cuenta_niif = "";
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select p.cod_cuenta_niif From PLAN_CUENTAS_HOMOLOGA p Where p.cod_cuenta = '" + pcod_cuenta + "' "; 
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["cod_cuenta_niif"] != DBNull.Value) lcod_cuenta_niif = Convert.ToString(resultado["cod_cuenta_niif"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lcod_cuenta_niif;
                    }
                    catch (Exception ex)
                    {
                        return lcod_cuenta_niif;
                    }

                }
            }
        }

        public List<Comprobante> ContabilizarOperacionSinComp(DateTime pFechaIni, DateTime pFechaFin, Int64 pTipoProducto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Comprobante> lstOperaciones = new List<Comprobante>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        DbParameter p_fecha_ini = cmdTransaccionFactory.CreateParameter();
                        p_fecha_ini.ParameterName = "p_fecha_ini";
                        p_fecha_ini.Value = pFechaIni.ToString(conf.ObtenerFormatoFecha());
                        p_fecha_ini.DbType = DbType.DateTime;

                        DbParameter p_fecha_fin = cmdTransaccionFactory.CreateParameter();
                        p_fecha_fin.ParameterName = "p_fecha_fin";
                        p_fecha_fin.Value = pFechaFin.ToString(conf.ObtenerFormatoFecha());
                        p_fecha_fin.DbType = DbType.DateTime;

                        DbParameter p_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        p_tipo_producto.ParameterName = "p_tipo_producto";
                        p_tipo_producto.Value = pTipoProducto;
                        p_tipo_producto.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(p_fecha_ini);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_fin);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_producto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_OPE_SCOMPROBANTE";
                        cmdTransaccionFactory.ExecuteNonQuery();                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ContabilizarOperacionSinComp", ex);
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "SELECT * FROM TEMP_OPE_CONTABILIZAR ORDER BY COD_OPE";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Comprobante pEntidad = new Comprobante();

                            if (resultado["COD_OPE"] != DBNull.Value) pEntidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) pEntidad.tipo_comp = Convert.ToInt64(resultado["TIPO_OPE"]);
                            if (resultado["DESC_TIPO_OPE"] != DBNull.Value) pEntidad.descripcion = Convert.ToString(resultado["DESC_TIPO_OPE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) pEntidad.nombre = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) pEntidad.fecha = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) pEntidad.estado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) pEntidad.cod_persona = Convert.ToInt64(resultado["COD_USUARIO"]);
                            if (resultado["NOM_USUARIO"] != DBNull.Value) pEntidad.usuario = Convert.ToString(resultado["NOM_USUARIO"]);

                            lstOperaciones.Add(pEntidad);
                        }

                        return lstOperaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ContabilizarOperacionSinComp", ex);
                        return null;
                    }
                }
            }
        }


        public Comprobante ConsultarDatosElaboro(Int64 pelaboro, Usuario pUsuario)
        {
            DbDataReader resultado;
            Comprobante entidad = new Comprobante();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select o.direccion,o.telefono from usuarios u inner join  oficina o on o.cod_oficina=u.cod_oficina where CODUSUARIO= " + pelaboro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);                        
                        }
                       
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComprobanteData", "ConsultarDatosElaboro", ex);
                        return null;
                    }
                }
            }
        }



    }
}
