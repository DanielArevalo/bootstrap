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
    /// Objeto de acceso a datos para la tabla EgresosFamilia
    /// </summary>
    public class EgresosFamiliaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla EgresosFamilia
        /// </summary>
        public EgresosFamiliaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla EgresosFamilia de la base de datos
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia creada</returns>
        public EgresosFamilia CrearEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_EGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_EGRESO.ParameterName = "p_COD_EGRESO";
                        pCOD_EGRESO.Value = 0;
                        pCOD_EGRESO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pEgresosFamilia.cod_persona;

                        DbParameter pEGRESOS = cmdTransaccionFactory.CreateParameter();
                        pEGRESOS.ParameterName = "p_EGRESOS";
                        pEGRESOS.Value = pEgresosFamilia.egresos;

                        DbParameter pALIMENTACION = cmdTransaccionFactory.CreateParameter();
                        pALIMENTACION.ParameterName = "p_ALIMENTACION";
                        pALIMENTACION.Value = pEgresosFamilia.alimentacion;

                        DbParameter pVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        pVIVIENDA.ParameterName = "p_VIVIENDA";
                        pVIVIENDA.Value = pEgresosFamilia.vivienda;

                        DbParameter pEDUCACION = cmdTransaccionFactory.CreateParameter();
                        pEDUCACION.ParameterName = "p_EDUCACION";
                        pEDUCACION.Value = pEgresosFamilia.educacion;

                        DbParameter pSERVICIOSPUBLICOS = cmdTransaccionFactory.CreateParameter();
                        pSERVICIOSPUBLICOS.ParameterName = "p_SERVICIOSPUBLICOS";
                        pSERVICIOSPUBLICOS.Value = pEgresosFamilia.serviciospublicos;

                        DbParameter pTRANSPORTE = cmdTransaccionFactory.CreateParameter();
                        pTRANSPORTE.ParameterName = "p_TRANSPORTE";
                        pTRANSPORTE.Value = pEgresosFamilia.transporte;

                        DbParameter pPAGODEUDAS = cmdTransaccionFactory.CreateParameter();
                        pPAGODEUDAS.ParameterName = "p_PAGODEUDAS";
                        pPAGODEUDAS.Value = pEgresosFamilia.pagodeudas;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pEgresosFamilia.otros;

                        cmdTransaccionFactory.Parameters.Add(pCOD_EGRESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pEGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pALIMENTACION);
                        cmdTransaccionFactory.Parameters.Add(pVIVIENDA);
                        cmdTransaccionFactory.Parameters.Add(pEDUCACION);
                        cmdTransaccionFactory.Parameters.Add(pSERVICIOSPUBLICOS);
                        cmdTransaccionFactory.Parameters.Add(pTRANSPORTE);
                        cmdTransaccionFactory.Parameters.Add(pPAGODEUDAS);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_EGRFA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEgresosFamilia, "EgresosFamilia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEgresosFamilia.cod_egreso = Convert.ToInt64(pCOD_EGRESO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEgresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EgresosFamiliaData", "CrearEgresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla EgresosFamilia de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad EgresosFamilia modificada</returns>
        public EgresosFamilia ModificarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_EGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_EGRESO.ParameterName = "p_COD_EGRESO";
                        pCOD_EGRESO.Value = pEgresosFamilia.cod_egreso;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pEgresosFamilia.cod_persona;

                        DbParameter pEGRESOS = cmdTransaccionFactory.CreateParameter();
                        pEGRESOS.ParameterName = "p_EGRESOS";
                        pEGRESOS.Value = pEgresosFamilia.egresos;

                        DbParameter pALIMENTACION = cmdTransaccionFactory.CreateParameter();
                        pALIMENTACION.ParameterName = "p_ALIMENTACION";
                        pALIMENTACION.Value = pEgresosFamilia.alimentacion;

                        DbParameter pVIVIENDA = cmdTransaccionFactory.CreateParameter();
                        pVIVIENDA.ParameterName = "p_VIVIENDA";
                        pVIVIENDA.Value = pEgresosFamilia.vivienda;

                        DbParameter pEDUCACION = cmdTransaccionFactory.CreateParameter();
                        pEDUCACION.ParameterName = "p_EDUCACION";
                        pEDUCACION.Value = pEgresosFamilia.educacion;

                        DbParameter pSERVICIOSPUBLICOS = cmdTransaccionFactory.CreateParameter();
                        pSERVICIOSPUBLICOS.ParameterName = "p_SERVICIOSPUBLICOS";
                        pSERVICIOSPUBLICOS.Value = pEgresosFamilia.serviciospublicos;

                        DbParameter pTRANSPORTE = cmdTransaccionFactory.CreateParameter();
                        pTRANSPORTE.ParameterName = "p_TRANSPORTE";
                        pTRANSPORTE.Value = pEgresosFamilia.transporte;

                        DbParameter pPAGODEUDAS = cmdTransaccionFactory.CreateParameter();
                        pPAGODEUDAS.ParameterName = "p_PAGODEUDAS";
                        pPAGODEUDAS.Value = pEgresosFamilia.pagodeudas;

                        DbParameter pOTROS = cmdTransaccionFactory.CreateParameter();
                        pOTROS.ParameterName = "p_OTROS";
                        pOTROS.Value = pEgresosFamilia.otros;

                        cmdTransaccionFactory.Parameters.Add(pCOD_EGRESO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pEGRESOS);
                        cmdTransaccionFactory.Parameters.Add(pALIMENTACION);
                        cmdTransaccionFactory.Parameters.Add(pVIVIENDA);
                        cmdTransaccionFactory.Parameters.Add(pEDUCACION);
                        cmdTransaccionFactory.Parameters.Add(pSERVICIOSPUBLICOS);
                        cmdTransaccionFactory.Parameters.Add(pTRANSPORTE);
                        cmdTransaccionFactory.Parameters.Add(pPAGODEUDAS);
                        cmdTransaccionFactory.Parameters.Add(pOTROS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_EGRFA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEgresosFamilia, "EgresosFamilia",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEgresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EgresosFamiliaData", "ModificarEgresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla EgresosFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de EgresosFamilia</param>
        public void EliminarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EgresosFamilia pEgresosFamilia = new EgresosFamilia();

                        //if (pUsuario.programaGeneraLog)
                        //    pEgresosFamilia = ConsultarEgresosFamilia(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_EGRESO = cmdTransaccionFactory.CreateParameter();
                        pCOD_EGRESO.ParameterName = "p_COD_EGRESO";
                        pCOD_EGRESO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_EGRESO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_EGRFA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEgresosFamilia, "EgresosFamilia", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EgresosFamiliaData", "EliminarEgresosFamilia", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla EgresosFamilia de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia consultado</returns>
        public EgresosFamilia ConsultarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            EgresosFamilia entidad = new EgresosFamilia();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  EGRESOSFAMILIA WHERE COD_EGRESO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_EGRESO"] != DBNull.Value) entidad.cod_egreso = Convert.ToInt64(resultado["COD_EGRESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["EGRESOS"] != DBNull.Value) entidad.egresos = Convert.ToInt64(resultado["EGRESOS"]);
                            if (resultado["ALIMENTACION"] != DBNull.Value) entidad.alimentacion = Convert.ToInt64(resultado["ALIMENTACION"]);
                            if (resultado["VIVIENDA"] != DBNull.Value) entidad.vivienda = Convert.ToInt64(resultado["VIVIENDA"]);
                            if (resultado["EDUCACION"] != DBNull.Value) entidad.educacion = Convert.ToInt64(resultado["EDUCACION"]);
                            if (resultado["SERVICIOSPUBLICOS"] != DBNull.Value) entidad.serviciospublicos = Convert.ToInt64(resultado["SERVICIOSPUBLICOS"]);
                            if (resultado["TRANSPORTE"] != DBNull.Value) entidad.transporte = Convert.ToInt64(resultado["TRANSPORTE"]);
                            if (resultado["PAGODEUDAS"] != DBNull.Value) entidad.pagodeudas = Convert.ToInt64(resultado["PAGODEUDAS"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);
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
                        BOExcepcion.Throw("EgresosFamiliaData", "ConsultarEgresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EgresosFamilia dados unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public List<EgresosFamilia> ListarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EgresosFamilia> lstEgresosFamilia = new List<EgresosFamilia>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  EGRESOSFAMILIA " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EgresosFamilia entidad = new EgresosFamilia();

                            if (resultado["COD_EGRESO"] != DBNull.Value) entidad.cod_egreso = Convert.ToInt64(resultado["COD_EGRESO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["EGRESOS"] != DBNull.Value) entidad.egresos = Convert.ToInt64(resultado["EGRESOS"]);
                            if (resultado["ALIMENTACION"] != DBNull.Value) entidad.alimentacion = Convert.ToInt64(resultado["ALIMENTACION"]);
                            if (resultado["VIVIENDA"] != DBNull.Value) entidad.vivienda = Convert.ToInt64(resultado["VIVIENDA"]);
                            if (resultado["EDUCACION"] != DBNull.Value) entidad.educacion = Convert.ToInt64(resultado["EDUCACION"]);
                            if (resultado["SERVICIOSPUBLICOS"] != DBNull.Value) entidad.serviciospublicos = Convert.ToInt64(resultado["SERVICIOSPUBLICOS"]);
                            if (resultado["TRANSPORTE"] != DBNull.Value) entidad.transporte = Convert.ToInt64(resultado["TRANSPORTE"]);
                            if (resultado["PAGODEUDAS"] != DBNull.Value) entidad.pagodeudas = Convert.ToInt64(resultado["PAGODEUDAS"]);
                            if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToInt64(resultado["OTROS"]);

                            lstEgresosFamilia.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEgresosFamilia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EgresosFamiliaData", "ListarEgresosFamilia", ex);
                        return null;
                    }
                }
            }
        }

    }
}