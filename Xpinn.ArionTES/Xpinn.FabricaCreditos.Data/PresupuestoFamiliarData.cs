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
    /// Objeto de acceso a datos para la tabla PresupuestoFamiliar
    /// </summary>
    public class PresupuestoFamiliarData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PresupuestoFamiliar
        /// </summary>
        public PresupuestoFamiliarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PresupuestoFamiliar de la base de datos
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar creada</returns>
        public PresupuestoFamiliar CrearPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pPresupuestoFamiliar.cod_presupuesto;
                        pCOD_PRESUPUESTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPresupuestoFamiliar.cod_persona;

                        DbParameter pACTIVIDADPRINCIPAL = cmdTransaccionFactory.CreateParameter();
                        pACTIVIDADPRINCIPAL.ParameterName = "p_ACTIVIDADPRINCIPAL";
                        pACTIVIDADPRINCIPAL.Value = pPresupuestoFamiliar.actividadprincipal;

                        DbParameter pCONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCONYUGE.ParameterName = "p_CONYUGE";
                        pCONYUGE.Value = pPresupuestoFamiliar.conyuge;

                        DbParameter pOTROSINGRESOS = cmdTransaccionFactory.CreateParameter();
                        pOTROSINGRESOS.ParameterName = "p_OTROSINGRESOS";
                        pOTROSINGRESOS.Value = pPresupuestoFamiliar.otrosingresos;

                        DbParameter pCONSUMOFAMILIAR = cmdTransaccionFactory.CreateParameter();
                        pCONSUMOFAMILIAR.ParameterName = "p_CONSUMOFAMILIAR";
                        pCONSUMOFAMILIAR.Value = pPresupuestoFamiliar.consumofamiliar;

                        DbParameter pOBLIGACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBLIGACIONES.ParameterName = "p_OBLIGACIONES";
                        pOBLIGACIONES.Value = pPresupuestoFamiliar.obligaciones;

                        DbParameter pEXCEDENTE = cmdTransaccionFactory.CreateParameter();
                        pEXCEDENTE.ParameterName = "p_EXCEDENTE";
                        pEXCEDENTE.Value = pPresupuestoFamiliar.excedente;


                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pACTIVIDADPRINCIPAL);
                        cmdTransaccionFactory.Parameters.Add(pCONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pOTROSINGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pCONSUMOFAMILIAR);
                        cmdTransaccionFactory.Parameters.Add(pOBLIGACIONES);
                        cmdTransaccionFactory.Parameters.Add(pEXCEDENTE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoFamiliar, "PresupuestoFamiliar",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pPresupuestoFamiliar.cod_presupuesto = Convert.ToInt64(pCOD_PRESUPUESTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuestoFamiliar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "CrearPresupuestoFamiliar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PresupuestoFamiliar de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad PresupuestoFamiliar modificada</returns>
        public PresupuestoFamiliar ModificarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pPresupuestoFamiliar.cod_presupuesto;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pPresupuestoFamiliar.cod_persona;

                        DbParameter pACTIVIDADPRINCIPAL = cmdTransaccionFactory.CreateParameter();
                        pACTIVIDADPRINCIPAL.ParameterName = "p_ACTIVIDADPRINCIPAL";
                        pACTIVIDADPRINCIPAL.Value = pPresupuestoFamiliar.actividadprincipal;

                        DbParameter pCONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCONYUGE.ParameterName = "p_CONYUGE";
                        pCONYUGE.Value = pPresupuestoFamiliar.conyuge;

                        DbParameter pOTROSINGRESOS = cmdTransaccionFactory.CreateParameter();
                        pOTROSINGRESOS.ParameterName = "p_OTROSINGRESOS";
                        pOTROSINGRESOS.Value = pPresupuestoFamiliar.otrosingresos;

                        DbParameter pCONSUMOFAMILIAR = cmdTransaccionFactory.CreateParameter();
                        pCONSUMOFAMILIAR.ParameterName = "p_CONSUMOFAMILIAR";
                        pCONSUMOFAMILIAR.Value = pPresupuestoFamiliar.consumofamiliar;

                        DbParameter pOBLIGACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBLIGACIONES.ParameterName = "p_OBLIGACIONES";
                        pOBLIGACIONES.Value = pPresupuestoFamiliar.obligaciones;

                        DbParameter pEXCEDENTE = cmdTransaccionFactory.CreateParameter();
                        pEXCEDENTE.ParameterName = "p_EXCEDENTE";
                        pEXCEDENTE.Value = pPresupuestoFamiliar.excedente;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pACTIVIDADPRINCIPAL);
                        cmdTransaccionFactory.Parameters.Add(pCONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pOTROSINGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pCONSUMOFAMILIAR);
                        cmdTransaccionFactory.Parameters.Add(pOBLIGACIONES);
                        cmdTransaccionFactory.Parameters.Add(pEXCEDENTE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESF_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoFamiliar, "PresupuestoFamiliar",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuestoFamiliar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "ModificarPresupuestoFamiliar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PresupuestoFamiliar de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PresupuestoFamiliar</param>
        public void EliminarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PresupuestoFamiliar pPresupuestoFamiliar = new PresupuestoFamiliar();

                        //if (pUsuario.programaGeneraLog)
                        //    pPresupuestoFamiliar = ConsultarPresupuestoFamiliar(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRESUPUESTO.ParameterName = "p_COD_PRESUPUESTO";
                        pCOD_PRESUPUESTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRESUPUESTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRESF_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pPresupuestoFamiliar, "PresupuestoFamiliar", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "EliminarPresupuestoFamiliar", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PresupuestoFamiliar de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar consultado</returns>
        public PresupuestoFamiliar ConsultarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            PresupuestoFamiliar entidad = new PresupuestoFamiliar();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PRESUPUESTOFAMILIAR WHERE COD_PRESUPUESTO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ACTIVIDADPRINCIPAL"] != DBNull.Value) entidad.actividadprincipal = Convert.ToInt64(resultado["ACTIVIDADPRINCIPAL"]);
                            if (resultado["CONYUGE"] != DBNull.Value) entidad.conyuge = Convert.ToInt64(resultado["CONYUGE"]);
                            if (resultado["OTROSINGRESOS"] != DBNull.Value) entidad.otrosingresos = Convert.ToInt64(resultado["OTROSINGRESOS"]);
                            if (resultado["CONSUMOFAMILIAR"] != DBNull.Value) entidad.consumofamiliar = Convert.ToInt64(resultado["CONSUMOFAMILIAR"]);
                            if (resultado["OBLIGACIONES"] != DBNull.Value) entidad.obligaciones = Convert.ToInt64(resultado["OBLIGACIONES"]);
                            if (resultado["EXCEDENTE"] != DBNull.Value) entidad.excedente = Convert.ToInt64(resultado["EXCEDENTE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "ConsultarPresupuestoFamiliar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PresupuestoFamiliar dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PresupuestoFamiliar> lstPresupuestoFamiliar = new List<PresupuestoFamiliar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PRESUPUESTOFAMILIAR where cod_persona = " + pPresupuestoFamiliar.cod_persona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PresupuestoFamiliar entidad = new PresupuestoFamiliar();

                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ACTIVIDADPRINCIPAL"] != DBNull.Value) entidad.actividadprincipal = Convert.ToInt64(resultado["ACTIVIDADPRINCIPAL"]);
                            if (resultado["CONYUGE"] != DBNull.Value) entidad.conyuge = Convert.ToInt64(resultado["CONYUGE"]);
                            if (resultado["OTROSINGRESOS"] != DBNull.Value) entidad.otrosingresos = Convert.ToInt64(resultado["OTROSINGRESOS"]);
                            if (resultado["CONSUMOFAMILIAR"] != DBNull.Value) entidad.consumofamiliar = Convert.ToInt64(resultado["CONSUMOFAMILIAR"]);
                            if (resultado["OBLIGACIONES"] != DBNull.Value) entidad.obligaciones = Convert.ToInt64(resultado["OBLIGACIONES"]);
                            if (resultado["EXCEDENTE"] != DBNull.Value) entidad.excedente = Convert.ToInt64(resultado["EXCEDENTE"]);

                            lstPresupuestoFamiliar.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuestoFamiliar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "ListarPresupuestoFamiliar", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PresupuestoFamiliar dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliarRepo(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PresupuestoFamiliar> lstPresupuestoFamiliar = new List<PresupuestoFamiliar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PRESUPUESTOFAMILIAR where cod_persona = " + pPresupuestoFamiliar.cod_persona.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PresupuestoFamiliar entidad = new PresupuestoFamiliar();

                            if (resultado["COD_PRESUPUESTO"] != DBNull.Value) entidad.cod_presupuesto = Convert.ToInt64(resultado["COD_PRESUPUESTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ACTIVIDADPRINCIPAL"] != DBNull.Value) entidad.actividadprincipal = Convert.ToInt64(resultado["ACTIVIDADPRINCIPAL"]);
                            if (resultado["CONYUGE"] != DBNull.Value) entidad.conyuge = Convert.ToInt64(resultado["CONYUGE"]);
                            if (resultado["OTROSINGRESOS"] != DBNull.Value) entidad.otrosingresos = Convert.ToInt64(resultado["OTROSINGRESOS"]);
                            if (resultado["CONSUMOFAMILIAR"] != DBNull.Value) entidad.consumofamiliar = Convert.ToInt64(resultado["CONSUMOFAMILIAR"]);
                            if (resultado["OBLIGACIONES"] != DBNull.Value) entidad.obligaciones = Convert.ToInt64(resultado["OBLIGACIONES"]);
                            if (resultado["EXCEDENTE"] != DBNull.Value) entidad.excedente = Convert.ToInt64(resultado["EXCEDENTE"]);

                            lstPresupuestoFamiliar.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuestoFamiliar;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoFamiliarData", "ListarPresupuestoFamiliarRepo", ex);
                        return null;
                    }
                }
            }
        }

    }
}