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
    /// Objeto de acceso a datos para la tabla ControlCreditos
    /// </summary>
    public class ControlCreditosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ControlCreditos
        /// </summary>
        public ControlCreditosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos creada</returns>
        public ControlCreditos CrearControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCONTROL = cmdTransaccionFactory.CreateParameter();
                        pIDCONTROL.ParameterName = "pIDCONTROL";
                        pIDCONTROL.Value = pControlCreditos.idcontrol;
                        pIDCONTROL.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pNUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pControlCreditos.numero_radicacion;

                        DbParameter pCODTIPOPROCESO = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOPROCESO.ParameterName = "pCODTIPOPROCESO";
                        if (pControlCreditos.codtipoproceso == null || pControlCreditos.codtipoproceso == "")
                            pCODTIPOPROCESO.Value = DBNull.Value;
                        else
                            pCODTIPOPROCESO.Value = pControlCreditos.codtipoproceso;           

                        DbParameter pFECHAPROCESO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROCESO.ParameterName = "pFECHAPROCESO";
                        pFECHAPROCESO.Value = pControlCreditos.fechaproceso;
                        pFECHAPROCESO.DbType = DbType.DateTime;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pCOD_PERSONA";
                        pCOD_PERSONA.Value = pControlCreditos.cod_persona;

                        DbParameter pCOD_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO.ParameterName = "pCOD_MOTIVO";
                        pCOD_MOTIVO.Value = pControlCreditos.cod_motivo;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "pOBSERVACIONES";
                        if (pControlCreditos.observaciones == null || pControlCreditos.observaciones == "")
                            pOBSERVACIONES.Value = DBNull.Value;
                        else
                            pOBSERVACIONES.Value = pControlCreditos.observaciones;

                        DbParameter pANEXOS = cmdTransaccionFactory.CreateParameter();
                        pANEXOS.ParameterName = "pANEXOS";
                        if (pControlCreditos.anexos == null || pControlCreditos.anexos == "")
                            pANEXOS.Value = DBNull.Value;
                        else
                            pANEXOS.Value = pControlCreditos.anexos;

                        DbParameter pNIVEL = cmdTransaccionFactory.CreateParameter();
                        pNIVEL.ParameterName = "pNIVEL";
                        pNIVEL.Value = pControlCreditos.nivel;

                        DbParameter pFECHACONSULTA_DAT = cmdTransaccionFactory.CreateParameter();
                        pFECHACONSULTA_DAT.ParameterName = "pFECHACONSULTA_DAT";
                        if (pControlCreditos.fechaconsulta_dat == DateTime.MinValue)
                            pFECHACONSULTA_DAT.Value = DBNull.Value;
                        else
                            pFECHACONSULTA_DAT.Value = pControlCreditos.fechaconsulta_dat;
                        pFECHACONSULTA_DAT.DbType = DbType.DateTime;


                        cmdTransaccionFactory.Parameters.Add(pIDCONTROL);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODTIPOPROCESO);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pANEXOS);
                        cmdTransaccionFactory.Parameters.Add(pNIVEL);
                        cmdTransaccionFactory.Parameters.Add(pFECHACONSULTA_DAT);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CTRLC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                       // if (pUsuario.programaGeneraLog)
                         //   DAauditoria.InsertarLog(pControlCreditos, "ControlCreditos",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pControlCreditos.idcontrol = Convert.ToInt64(pIDCONTROL.Value);
                        return pControlCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "CrearControlCreditos", ex);
                        return null;
                    }
                }
            }
        }

        public string obtenerCodTipoProceso(string estado, Usuario pUsuario)
        {
            DbDataReader resultado;
            string codigo = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(codtipoproceso) as CODIGO from tipoprocesos where estado = '" + estado.ToUpper() +"'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())                        
                            if (resultado["CODIGO"] != DBNull.Value) codigo = Convert.ToString(resultado["CODIGO"]);
                        else
                        {
                            throw new ExceptionBusiness("No se encontró código de proceso. Verifique por favor.");
                        }
                        return codigo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "obtenerCodTipoProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos ModificarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCONTROL = cmdTransaccionFactory.CreateParameter();
                        pIDCONTROL.ParameterName = "p_IDCONTROL";
                        pIDCONTROL.Value = pControlCreditos.idcontrol;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pControlCreditos.numero_radicacion;

                        DbParameter pCODTIPOPROCESO = cmdTransaccionFactory.CreateParameter();
                        pCODTIPOPROCESO.ParameterName = "p_CODTIPOPROCESO";
                        pCODTIPOPROCESO.Value = pControlCreditos.codtipoproceso;

                        DbParameter pFECHAPROCESO = cmdTransaccionFactory.CreateParameter();
                        pFECHAPROCESO.ParameterName = "p_FECHAPROCESO";
                        pFECHAPROCESO.Value = pControlCreditos.fechaproceso;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pControlCreditos.cod_persona;

                        DbParameter pCOD_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO.ParameterName = "p_COD_MOTIVO";
                        pCOD_MOTIVO.Value = pControlCreditos.cod_motivo;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        pOBSERVACIONES.Value = pControlCreditos.observaciones;

                        DbParameter pANEXOS = cmdTransaccionFactory.CreateParameter();
                        pANEXOS.ParameterName = "p_ANEXOS";
                        pANEXOS.Value = pControlCreditos.anexos;

                        DbParameter pNIVEL = cmdTransaccionFactory.CreateParameter();
                        pNIVEL.ParameterName = "p_NIVEL";
                        pNIVEL.Value = pControlCreditos.nivel;

                        cmdTransaccionFactory.Parameters.Add(pIDCONTROL);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODTIPOPROCESO);
                        cmdTransaccionFactory.Parameters.Add(pFECHAPROCESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pANEXOS);
                        cmdTransaccionFactory.Parameters.Add(pNIVEL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_ControlCreditos_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pControlCreditos, "ControlCreditos",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pControlCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "ModificarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ControlCreditos</param>
        public void EliminarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ControlCreditos pControlCreditos = new ControlCreditos();

                        if (pUsuario.programaGeneraLog)
                            pControlCreditos = ConsultarControlCreditos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDCONTROL = cmdTransaccionFactory.CreateParameter();
                        pIDCONTROL.ParameterName = "p_IDCONTROL";
                        pIDCONTROL.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIDCONTROL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_FabricaCreditos_ControlCreditos_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pControlCreditos, "ControlCreditos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "EliminarControlCreditos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ControlCreditos</param>
        /// <returns>Entidad ControlCreditos consultado</returns>
        public ControlCreditos ConsultarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ControlCreditos entidad = new ControlCreditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONTROLCREDITOS WHERE IDCONTROL = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDCONTROL"] != DBNull.Value) entidad.idcontrol = Convert.ToInt64(resultado["IDCONTROL"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.codtipoproceso = Convert.ToString(resultado["CODTIPOPROCESO"]);
                            if (resultado["FECHAPROCESO"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["FECHAPROCESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["COD_MOTIVO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ANEXOS"] != DBNull.Value) entidad.anexos = Convert.ToString(resultado["ANEXOS"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "ConsultarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ControlCreditos</param>
        /// <returns>Entidad ControlCreditos consultado</returns>
        public ControlCreditos ConsultarControl_Procesos(Int64 pId,String Radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            ControlCreditos entidad = new ControlCreditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONTROLCREDITOS WHERE CODTIPOPROCESO = '" + pId.ToString() +"'"  +" and  NUMERO_RADICACION ="+ Radicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDCONTROL"] != DBNull.Value) entidad.idcontrol = Convert.ToInt64(resultado["IDCONTROL"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.codtipoproceso = Convert.ToString(resultado["CODTIPOPROCESO"]);
                            if (resultado["FECHAPROCESO"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["FECHAPROCESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["COD_MOTIVO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ANEXOS"] != DBNull.Value) entidad.anexos = Convert.ToString(resultado["ANEXOS"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            //if (resultado["fecha_consulta_dat"] != DBNull.Value) entidad.fechadata = Convert.ToString(resultado["fecha_consulta_dat"].ToString());
                             
                    
                        }
                        //else
                        //{
                        //    throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        //}
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "ConsultarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ControlCreditos dados unos filtros
        /// </summary>
        /// <param name="pControlCreditos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlCreditos obtenidos</returns>
        public List<ControlCreditos> ListarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ControlCreditos> lstControlCreditos = new List<ControlCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  CONTROLCREDITOS where NUMERO_RADICACION = " + pControlCreditos.numero_radicacion + " order by idcontrol desc";


                        string sql = "SELECT cc.*, tp.descripcion, u.nombre  FROM  CONTROLCREDITOS cc, tipoprocesos tp, usuarios u where NUMERO_RADICACION = " + pControlCreditos.numero_radicacion + " and cc.codtipoproceso = tp.codtipoproceso and u.codusuario = cc.cod_persona order by idcontrol desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ControlCreditos entidad = new ControlCreditos();

                            if (resultado["IDCONTROL"] != DBNull.Value) entidad.idcontrol = Convert.ToInt64(resultado["IDCONTROL"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["CODTIPOPROCESO"] != DBNull.Value) entidad.codtipoproceso = Convert.ToString(resultado["CODTIPOPROCESO"]);
                            if (resultado["FECHAPROCESO"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["FECHAPROCESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_MOTIVO"] != DBNull.Value) entidad.cod_motivo = Convert.ToInt64(resultado["COD_MOTIVO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ANEXOS"] != DBNull.Value) entidad.anexos = Convert.ToString(resultado["ANEXOS"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                           // if (resultado["MOTIVO"] != DBNull.Value) entidad.motivo = Convert.ToString(resultado["MOTIVO"]);
                            //if (resultado["FECHADATA"] != DBNull.Value) entidad.fechadata = Convert.ToString(resultado["FECHADATA"].ToString());

                          //  if (resultado["fecha_consulta_dat"] != DBNull.Value) entidad.fechadata = Convert.ToString(resultado["fecha_consulta_dat"].ToString());
                     
                            lstControlCreditos.Add(entidad);
                        }

                        return lstControlCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "ListarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla ControlCreditos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos Modificarfechadatcredito(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pControlCreditos.numero_radicacion;

                        DbParameter pFECHADATA = cmdTransaccionFactory.CreateParameter();
                        pFECHADATA.ParameterName = "pFECHADATA";
                        pFECHADATA.Value = pControlCreditos.fechadata;


                      
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHADATA);
                     

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CTRLC_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                 
                        return pControlCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ControlCreditosData", "ModificarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }

    }
}