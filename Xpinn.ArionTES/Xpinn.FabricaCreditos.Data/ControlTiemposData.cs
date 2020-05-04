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
    /// Objeto de acceso a datos para la tabla ControlTiempos
    /// </summary>
    public class ControlTiemposData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ControlTiempos
        /// </summary>
        public ControlTiemposData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ControlTiempos de la base de datos
        /// </summary>
        /// <param name="pControlTiempos">Entidad ControlTiempos</param>
        /// <returns>Entidad ControlTiempos creada</returns>
        public ControlTiempos CrearControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pNUMEROCREDIDO = cmdTransaccionFactory.CreateParameter();
                        //pNUMEROCREDIDO.ParameterName = "p_NumeroCredido";
                        //pNUMEROCREDIDO.Value = pControlTiempos.NumeroCredido;
                        //pNUMEROCREDIDO.Direction = ParameterDirection.InputOutput;

                        //DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        //pIDENTIFICACION.ParameterName = "p_Identificacion";
                        //pIDENTIFICACION.Value = pControlTiempos.Identificacion;

                        //DbParameter pNOMBREDEUDOR = cmdTransaccionFactory.CreateParameter();
                        //pNOMBREDEUDOR.ParameterName = "p_NombreDeudor";
                        //pNOMBREDEUDOR.Value = pControlTiempos.NombreDeudor;

                        //DbParameter pFECHAS = cmdTransaccionFactory.CreateParameter();
                        //pFECHAS.ParameterName = "p_FechaS";
                        //pFECHAS.Value = pControlTiempos.FechaS;

                        //DbParameter pASESOR = cmdTransaccionFactory.CreateParameter();
                        //pASESOR.ParameterName = "p_Asesor";
                        //pASESOR.Value = pControlTiempos.Asesor;

                        //DbParameter pMONTO = cmdTransaccionFactory.CreateParameter();
                        //pMONTO.ParameterName = "p_Monto";
                        //pMONTO.Value = pControlTiempos.Monto;

                        //DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        //pESTADO.ParameterName = "p_Estado";
                        //pESTADO.Value = pControlTiempos.Estado;

                        //DbParameter pFECHAU = cmdTransaccionFactory.CreateParameter();
                        //pFECHAU.ParameterName = "p_FechaU";
                        //pFECHAU.Value = pControlTiempos.FechaU;

                        //DbParameter pULTIMOPROCESO = cmdTransaccionFactory.CreateParameter();
                        //pULTIMOPROCESO.ParameterName = "p_UltimoProceso";
                        //pULTIMOPROCESO.Value = pControlTiempos.UltimoProceso;

                        //DbParameter pTIEMPOTOTAL = cmdTransaccionFactory.CreateParameter();
                        //pTIEMPOTOTAL.ParameterName = "p_TiempoTotal";
                        //pTIEMPOTOTAL.Value = pControlTiempos.TiempoTotal;

                        //DbParameter pENCARGADO = cmdTransaccionFactory.CreateParameter();
                        //pENCARGADO.ParameterName = "p_Encargado";
                        //pENCARGADO.Value = pControlTiempos.Encargado;


                        //cmdTransaccionFactory.Parameters.Add(pNUMEROCREDIDO);
                        //cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        //cmdTransaccionFactory.Parameters.Add(pNOMBREDEUDOR);
                        //cmdTransaccionFactory.Parameters.Add(pFECHAS);
                        //cmdTransaccionFactory.Parameters.Add(pASESOR);
                        //cmdTransaccionFactory.Parameters.Add(pMONTO);
                        //cmdTransaccionFactory.Parameters.Add(pESTADO);
                        //cmdTransaccionFactory.Parameters.Add(pFECHAU);
                        //cmdTransaccionFactory.Parameters.Add(pULTIMOPROCESO);
                        //cmdTransaccionFactory.Parameters.Add(pTIEMPOTOTAL);
                        //cmdTransaccionFactory.Parameters.Add(pENCARGADO);


                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_ControlTiempos_CREAR";
                        //cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pControlTiempos, "ControlTiempos",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //pControlTiempos.numerocredito = Convert.ToInt64(pNUMEROCREDIDO.Value);
                        return pControlTiempos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "CrearControlTiempos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ControlTiempos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ControlTiempos modificada</returns>
        public ControlTiempos ModificarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMEROCREDIDO = cmdTransaccionFactory.CreateParameter();
                        pNUMEROCREDIDO.ParameterName = "p_NUMEROCREDIDO";
                        pNUMEROCREDIDO.Value = pControlTiempos.numerocredito;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        pIDENTIFICACION.Value = pControlTiempos.identificacion;

                        DbParameter pNOMBREDEUDOR = cmdTransaccionFactory.CreateParameter();
                        pNOMBREDEUDOR.ParameterName = "p_NOMBREDEUDOR";
                        pNOMBREDEUDOR.Value = pControlTiempos.nombredeudor;

                        DbParameter pFECHAS = cmdTransaccionFactory.CreateParameter();
                        pFECHAS.ParameterName = "p_FECHAS";
                        pFECHAS.Value = pControlTiempos.fechas;

                        DbParameter pASESOR = cmdTransaccionFactory.CreateParameter();
                        pASESOR.ParameterName = "p_ASESOR";
                        pASESOR.Value = pControlTiempos.asesor;

                        DbParameter pMONTO = cmdTransaccionFactory.CreateParameter();
                        pMONTO.ParameterName = "p_MONTO";
                        pMONTO.Value = pControlTiempos.monto;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pControlTiempos.estado;

                        DbParameter pFECHAU = cmdTransaccionFactory.CreateParameter();
                        pFECHAU.ParameterName = "p_FECHAU";
                        pFECHAU.Value = pControlTiempos.fechau;

                        DbParameter pULTIMOPROCESO = cmdTransaccionFactory.CreateParameter();
                        pULTIMOPROCESO.ParameterName = "p_ULTIMOPROCESO";
                        pULTIMOPROCESO.Value = pControlTiempos.ultimoproceso;

                        DbParameter pTIEMPOTOTAL = cmdTransaccionFactory.CreateParameter();
                        pTIEMPOTOTAL.ParameterName = "p_TIEMPOTOTAL";
                        pTIEMPOTOTAL.Value = pControlTiempos.tiempototal;

                        DbParameter pENCARGADO = cmdTransaccionFactory.CreateParameter();
                        pENCARGADO.ParameterName = "p_ENCARGADO";
                        pENCARGADO.Value = pControlTiempos.encargado;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROCREDIDO);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNOMBREDEUDOR);
                        cmdTransaccionFactory.Parameters.Add(pFECHAS);
                        cmdTransaccionFactory.Parameters.Add(pASESOR);
                        cmdTransaccionFactory.Parameters.Add(pMONTO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pFECHAU);
                        cmdTransaccionFactory.Parameters.Add(pULTIMOPROCESO);
                        cmdTransaccionFactory.Parameters.Add(pTIEMPOTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pENCARGADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_ControlTiempos_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pControlTiempos, "ControlTiempos", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pControlTiempos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "ModificarControlTiempos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ControlTiempos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ControlTiempos</param>
        public void EliminarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ControlTiempos pControlTiempos = new ControlTiempos();

                        if (pUsuario.programaGeneraLog)
                            pControlTiempos = ConsultarControlTiempos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pNUMEROCREDIDO = cmdTransaccionFactory.CreateParameter();
                        pNUMEROCREDIDO.ParameterName = "p_NumeroCredido";
                        pNUMEROCREDIDO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pNUMEROCREDIDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_ControlTiempos_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pControlTiempos, "ControlTiempos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "EliminarControlTiempos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ControlTiempos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ControlTiempos</param>
        /// <returns>Entidad ControlTiempos consultado</returns>
        public ControlTiempos ConsultarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ControlTiempos entidad = new ControlTiempos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONTROLTIEMPOS WHERE NumeroCredito = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMEROCREDITO"] != DBNull.Value) entidad.numerocredito = Convert.ToString(resultado["NUMEROCREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBREDEUDOR"] != DBNull.Value) entidad.nombredeudor = Convert.ToString(resultado["NOMBREDEUDOR"]);
                            if (resultado["FECHAS"] != DBNull.Value) entidad.fechas = Convert.ToDateTime(resultado["FECHAS"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHAU"] != DBNull.Value) entidad.fechau = Convert.ToDateTime(resultado["FECHAU"]);
                            if (resultado["ULTIMOPROCESO"] != DBNull.Value) entidad.ultimoproceso = Convert.ToString(resultado["ULTIMOPROCESO"]);
                            //  if (resultado["TIEMPOTOTAL"] != DBNull.Value) entidad.tiempototal = Convert.ToInt64(resultado["TIEMPOTOTAL"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToString(resultado["ENCARGADO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "ConsultarControlTiempos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ControlTiempos dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiemposEfic(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ControlTiempos> lstControlTiempos = new List<ControlTiempos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar el filtro según datos escogidos
                        string filtro = " where ";
                        filtro = pControlTiempos.cod_oficina.ToString() != "0" ? filtro + "a.COD_OFICINA = " + pControlTiempos.cod_oficina.ToString() + " and " : filtro;
                        filtro = pControlTiempos.asesor != null ? filtro + "u.codusuario = " + "'" + pControlTiempos.asesor.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.fechaprocesoin != null ? filtro + "to_char(b.fechaproceso,'MM/dd/yyyy') >= " + "'" + pControlTiempos.fechaprocesoin.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.fechaprocesofin != null ? filtro + "to_char(b.fechaproceso,'MM/dd/yyyy') <= " + "'" + pControlTiempos.fechaprocesofin.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.numerocredito != null ? filtro + "a.numero_radicacion = " + "'" + pControlTiempos.numerocredito.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.identificacion != null ? filtro + "p.IDENTIFICACION = " + "'" + pControlTiempos.identificacion.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.proceso1 != null ? filtro + "b.codtipoproceso=" + pControlTiempos.proceso1.ToString() + " and " : filtro;
                        filtro = pControlTiempos.proceso2 != null ? filtro + "c.codtipoproceso=" + pControlTiempos.proceso2.ToString() + " and " : filtro;

                        // Determinar las condiciones
                        string condiciones = " a.estado not in('TERMINADO')  order by a.fecha_solicitud asc";

                        // Determinar la consulta de datos. 
                        string sql = @"Select CALCULAR_HORASHABILES(b.fecha_consulta_dat,f.fechaproceso) tiempodata,
                                        CALCULAR_HORASHABILES(b.fechaproceso, c.fechaproceso) TIEMPOPROCESOS, 
                                        a.numero_radicacion, p.identificacion, a.cod_deudor, a.monto_aprobado,
                                        a.cod_oficina,f.fechaproceso as fecha_solicitud, b.fecha_consulta_dat, 
                                        u.nombre as asesor,b.cod_persona, t.descripcion as proceso1, 
                                        b.observaciones as observaciones1, b.fechaproceso as fecha1,
                                        c.cod_persona,x.descripcion as proceso2, c.observaciones as observaciones2, 
                                        c.fechaproceso as fecha2, d.cod_persona as aprobador, usu.nombre as nombreaprobador,e.fechaproceso as fecha3 
                                        From credito a inner join usuarios u on a.cod_asesor_com=u.codusuario 
                                        inner join persona p on p.cod_persona=a.cod_deudor 
                                        inner join controlcreditos b on a.numero_radicacion=b.numero_radicacion 
                                        inner join tipoprocesos t on t.codtipoproceso=b.codtipoproceso 
                                        inner join controlcreditos c on a.numero_radicacion=c.numero_radicacion 
                                        inner join tipoprocesos x on x.codtipoproceso=c.codtipoproceso 
                                        left join controlcreditos d on c.numero_radicacion=d.numero_radicacion and d.codtipoproceso=5  
                                        left join usuarios usu on d.cod_persona=usu.codusuario
                                        left join controlcreditos e  on c.numero_radicacion=e.numero_radicacion and e.codtipoproceso=14
                                        left join controlcreditos f on c.numero_radicacion=f.numero_radicacion and f.codtipoproceso=1";

                        // Pegar los filtros y condiciones a la sentencia                       
                        sql = filtro == " where " ? sql + " where " + condiciones : sql + filtro + condiciones;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ControlTiempos entidad = new ControlTiempos();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numerocredito = Convert.ToString(resultado["numero_radicacion"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["PROCESO1"] != DBNull.Value) entidad.proceso1 = Convert.ToString(resultado["PROCESO1"]);
                            if (resultado["PROCESO2"] != DBNull.Value) entidad.proceso2 = Convert.ToString(resultado["PROCESO2"]);
                            if (resultado["OBSERVACIONES1"] != DBNull.Value) entidad.observaciones1 = Convert.ToString(resultado["OBSERVACIONES1"]);
                            if (resultado["OBSERVACIONES2"] != DBNull.Value) entidad.observaciones2 = Convert.ToString(resultado["OBSERVACIONES2"]);
                            if (resultado["FECHA1"] != DBNull.Value) entidad.fecha1 = Convert.ToDateTime(resultado["FECHA1"].ToString());
                            if (resultado["FECHA2"] != DBNull.Value) entidad.fecha2 = Convert.ToDateTime(resultado["FECHA2"].ToString());
                            if (resultado["APROBADOR"] != DBNull.Value) entidad.aprobador = Convert.ToString(resultado["APROBADOR"]);
                            if (resultado["NOMBREAPROBADOR"] != DBNull.Value) entidad.nombreaprobador = Convert.ToString(resultado["NOMBREAPROBADOR"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fechas = Convert.ToDateTime(resultado["FECHA_SOLICITUD"].ToString());
                            if (resultado["fecha_consulta_dat"] != DBNull.Value) entidad.fechadata = Convert.ToDateTime(resultado["fecha_consulta_dat"].ToString());
                            if (resultado["TIEMPOPROCESOS"] != DBNull.Value) entidad.tiempototal = Convert.ToString(resultado["TIEMPOPROCESOS"]);
                            if (resultado["TIEMPODATA"] != DBNull.Value) entidad.tiempodatacredito = Convert.ToString(resultado["TIEMPODATA"]);
                            if (resultado["FECHA3"] != DBNull.Value) entidad.fechaentrega = Convert.ToDateTime(resultado["FECHA3"]);

                            lstControlTiempos.Add(entidad);
                        }

                        return lstControlTiempos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "ListarControlTiempos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ControlTiempos dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ControlTiempos> lstControlTiempos = new List<ControlTiempos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar el filtro según datos escogidos
                        string filtro = " where ";
                        filtro = pControlTiempos.cod_oficina.ToString() != "0" ? filtro + "OFICINA = " + pControlTiempos.cod_oficina.ToString() + " and " : filtro;
                        //  filtro = pControlTiempos.asesor != null ? filtro + "NOMBREDEUDOR = " + "'" + pControlTiempos.asesor.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.ultimoproceso != null ? filtro + "ULTIMOPROCESO = " + "'" + pControlTiempos.ultimoproceso.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.encargado != null ? filtro + " TRIM(ASESOR) = " + "'" + pControlTiempos.encargado.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.tiempoMayor.ToString() != "0" ? filtro + "Round((SYSDATE - fecha_solicitud)) > " + "'" + pControlTiempos.tiempoMayor.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.tiempoMenor.ToString() != "0" ? filtro + "Round((SYSDATE - fecha_solicitud)) < " + "'" + pControlTiempos.tiempoMenor.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.numerocredito != null ? filtro + "NUMEROCREDITO = " + "'" + pControlTiempos.numerocredito.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.nombredeudor != null ? filtro + "NOMBREDEUDOR = " + "'" + pControlTiempos.nombredeudor.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.identificacion != null ? filtro + "IDENTIFICACION = " + "'" + pControlTiempos.identificacion.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.fechaproceso != null ? filtro + "to_char(FECHAU,'MM/dd/yyyy') = " + "'" + pControlTiempos.fechaproceso.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.cod_nomina != null ? filtro + "COD_NOMINA = " + "'" + pControlTiempos.cod_nomina.ToString() + "' and " : filtro;

                        // Determinar las condiciones
                        string condiciones = filtro.ToLower() == "where" ? "estado Not In ('Terminado', 'Borrado', 'Desembolsado', 'Negado') and nombreproceso not in ('Negado', 'Desembolsado') Order By fecha_solicitud desc" : "estado Not In ('Terminado', 'Borrado') and nombreproceso not in ('Negado') Order By fecha_solicitud desc";

                        // Determinar la consulta de datos
                        string sql = @"SELECT * FROM v_controlcredito";

                        // Pegar los filtros y condiciones a la sentencia
                        sql = filtro == " where " ? sql + " where " + condiciones : sql + filtro + condiciones;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ControlTiempos entidad = new ControlTiempos();

                            if (resultado["NUMEROCREDITO"] != DBNull.Value) entidad.numerocredito = Convert.ToString(resultado["NUMEROCREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBREDEUDOR"] != DBNull.Value) entidad.nombredeudor = Convert.ToString(resultado["NOMBREDEUDOR"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fechas = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHAU"] != DBNull.Value) entidad.fechau = Convert.ToDateTime(resultado["FECHAU"]);
                            if (resultado["ULTIMOPROCESO"] != DBNull.Value) entidad.ultimoproceso = Convert.ToString(resultado["ULTIMOPROCESO"]);
                            //if (resultado["TIEMPOTOTAL"] != DBNull.Value) entidad.tiempototal = Convert.ToInt64(resultado["TIEMPOTOTAL"]);
                            if (resultado["NOMBREENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToString(resultado["NOMBREENCARGADO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["OFICINA"]);
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["CODTIPOPROCESO"]);
                            if (resultado["NOMBREPROCESO"] != DBNull.Value) entidad.nom_proceso = Convert.ToString(resultado["NOMBREPROCESO"]);
                            if (resultado["PROCESO_SIGUIENTE"] != DBNull.Value) entidad.sig_proceso = Convert.ToString(resultado["PROCESO_SIGUIENTE"]);
                            if (resultado["NOMPROCESO_SIGUIENTE"] != DBNull.Value) entidad.sig_proceso_nom = Convert.ToString(resultado["NOMPROCESO_SIGUIENTE"]);
                            if (resultado["NOMTIPOPROCESO"] != DBNull.Value) entidad.nom_tipo_proceso = Convert.ToString(resultado["NOMTIPOPROCESO"]);
                            if (resultado["PROC_SIG_TIPO"] != DBNull.Value) entidad.sig_proceso_tipo = Convert.ToString(resultado["PROC_SIG_TIPO"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["FECHADATA"] != DBNull.Value) entidad.fechadata = Convert.ToDateTime(resultado["FECHADATA"].ToString());
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMLINEA"]);

                            lstControlTiempos.Add(entidad);
                        }

                        return lstControlTiempos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "ListarControlTiempos", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ControlTiempos dados unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarReporteMensajeria(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ControlTiempos> lstControlTiempos = new List<ControlTiempos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar el filtro según datos escogidos
                        string filtro = " where ";
                        filtro = pControlTiempos.cod_oficina.ToString() != "0" ? filtro + "OFICINA = " + pControlTiempos.cod_oficina.ToString() + " and " : filtro;
                        filtro = pControlTiempos.nom_proceso != null ? filtro + "NOMBREPROCESO = " + "'" + pControlTiempos.nom_proceso.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.encargado != null ? filtro + "NOMBREENCARGADO = " + "'" + pControlTiempos.encargado.ToString() + "'" + " and " : filtro;
                        filtro = pControlTiempos.fechaproceso != null ? filtro + "to_char(FECHAU,'MM/dd/yyyy') = " + "'" + pControlTiempos.fechaproceso.ToString() + "'" + " and " : filtro;

                        // Determinar las condiciones
                        string condiciones = " estado not in('TERMINADO')  order by nomoficina asc";

                        // Determinar la consulta de datos
                        string sql = @"SELECT * FROM v_controlcredito";

                        // Pegar los filtros y condiciones a la sentencia
                        sql = filtro == " where " ? sql + " where " + condiciones : sql + filtro + condiciones;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ControlTiempos entidad = new ControlTiempos();

                            if (resultado["NUMEROCREDITO"] != DBNull.Value) entidad.numerocredito = Convert.ToString(resultado["NUMEROCREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBREDEUDOR"] != DBNull.Value) entidad.nombredeudor = Convert.ToString(resultado["NOMBREDEUDOR"]);
                            if (resultado["ASESOR"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["ASESOR"]);
                            if (resultado["NOMBREENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToString(resultado["NOMBREENCARGADO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["OFICINA"]);
                            if (resultado["NOMBREPROCESO"] != DBNull.Value) entidad.nom_proceso = Convert.ToString(resultado["NOMBREPROCESO"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMOFICINA"]);

                            lstControlTiempos.Add(entidad);
                        }

                        return lstControlTiempos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlTiemposData", "ListarReporteMensajeria", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene las listas desplegables 
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<ControlTiempos> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ControlTiempos> lstDatosSolicitud = new List<ControlTiempos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        switch (ListaSolicitada)
                        {
                            case "Oficinas":
                                sql = "select COD_OFICINA as ListaId, NOMBRE as ListaDescripcion from oficina";
                                break;

                            case "Asesor":
                                sql = "select ICODIGO as ListaId, snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2 as ListaDescripcion from asejecutivos ORDER BY snombre1 ASC";
                                break;

                            case "Estado":
                                sql = "select ESTADO as ListaId, DESCRIPCION as ListaDescripcion from ESTADO_CREDITO";
                                break;

                            case "Accion":
                                sql = "SELECT CODTIPOPROCESO as ListaId, DESCRIPCION as ListaDescripcion FROM TIPOPROCESOS";
                                break;

                            case "Actividad":
                                sql = "SELECT CODACTIVIDAD as ListaId, DESCRIPCION as ListaDescripcion FROM actividad";
                                break;

                            case "Linea_Credito":
                                sql = "SELECT COD_LINEA_CREDITO as ListaId, NOMBRE as ListaDescripcion FROM LINEASCREDITO";
                                break;

                            case "EstadoProceso":
                                sql = "select codtipoproceso as ListaId, DESCRIPCION as ListaDescripcion from tipoprocesos order by listaid asc";
                                break;

                            case "EstadoProcesoAntecesor":
                                sql = "select codproantecede as ListaId, DESCRIPCION as ListaDescripcion from tipoprocesos";
                                break;

                            case "Encargado":
                                sql = "select CODUSUARIO as ListaId, NOMBRE as ListaDescripcion from USUARIOS  Where Codperfil = 13  and estado = 1";
                                break;

                            case "EstadoProcesoReporte":
                                sql = "select codtipoproceso as ListaId, DESCRIPCION as ListaDescripcion from tipoprocesos where  codtipoproceso in(6,12)order by listaid asc";
                                break;

                            case "TipoCuotaExtra":
                                sql = "select idtipo as ListaId, DESCRIPCION as ListaDescripcion from TIPO_CUOTAS_EXTRAS ";
                                break;

                            case "Periodicidad":
                                sql = "select cod_periodicidad as ListaId, DESCRIPCION as ListaDescripcion from PERIODICIDAD ";
                                break;

                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ControlTiempos entidad = new ControlTiempos();
                            if (ListaSolicitada == "Estado" || ListaSolicitada == "Accion" || ListaSolicitada == "Actividad" || ListaSolicitada == "Periodicidad")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]); }
                            else
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); }
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }
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

    }
}