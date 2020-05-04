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
    /// Objeto de acceso a datos para la tabla BienesRaices
    /// </summary>
    public class BienesRaicesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla BienesRaices
        /// </summary>
        public BienesRaicesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla BienesRaices de la base de datos
        /// </summary>
        /// <param name="pBienesRaices">Entidad BienesRaices</param>
        /// <returns>Entidad BienesRaices creada</returns>
        public BienesRaices CrearBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_BIEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_BIEN.ParameterName = "p_COD_BIEN";
                        pCOD_BIEN.Value = 0;
                        pCOD_BIEN.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pBienesRaices.cod_persona;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_TIPO";
                        pTIPO.Value = pBienesRaices.tipo;

                        DbParameter pMATRICULA = cmdTransaccionFactory.CreateParameter();
                        pMATRICULA.ParameterName = "p_MATRICULA";
                        pMATRICULA.Value = pBienesRaices.matricula;

                        DbParameter pVALORCOMERCIAL = cmdTransaccionFactory.CreateParameter();
                        pVALORCOMERCIAL.ParameterName = "p_VALORCOMERCIAL";
                        pVALORCOMERCIAL.Value = pBienesRaices.valorcomercial;

                        DbParameter pVALORHIPOTECA = cmdTransaccionFactory.CreateParameter();
                        pVALORHIPOTECA.ParameterName = "p_VALORHIPOTECA";
                        pVALORHIPOTECA.Value = pBienesRaices.valorhipoteca;


                        cmdTransaccionFactory.Parameters.Add(pCOD_BIEN);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pMATRICULA);
                        cmdTransaccionFactory.Parameters.Add(pVALORCOMERCIAL);
                        cmdTransaccionFactory.Parameters.Add(pVALORHIPOTECA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BIENE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBienesRaices, "BienesRaices",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pBienesRaices.cod_bien = Convert.ToInt64(pCOD_BIEN.Value);
                        return pBienesRaices;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "CrearBienesRaices", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla BienesRaices de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad BienesRaices modificada</returns>
        public BienesRaices ModificarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_BIEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_BIEN.ParameterName = "p_COD_BIEN";
                        pCOD_BIEN.Value = pBienesRaices.cod_bien;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pBienesRaices.cod_persona;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "p_TIPO";
                        pTIPO.Value = pBienesRaices.tipo;

                        DbParameter pMATRICULA = cmdTransaccionFactory.CreateParameter();
                        pMATRICULA.ParameterName = "p_MATRICULA";
                        pMATRICULA.Value = pBienesRaices.matricula;

                        DbParameter pVALORCOMERCIAL = cmdTransaccionFactory.CreateParameter();
                        pVALORCOMERCIAL.ParameterName = "p_VALORCOMERCIAL";
                        pVALORCOMERCIAL.Value = pBienesRaices.valorcomercial;

                        DbParameter pVALORHIPOTECA = cmdTransaccionFactory.CreateParameter();
                        pVALORHIPOTECA.ParameterName = "p_VALORHIPOTECA";
                        pVALORHIPOTECA.Value = pBienesRaices.valorhipoteca;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BIEN);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pMATRICULA);
                        cmdTransaccionFactory.Parameters.Add(pVALORCOMERCIAL);
                        cmdTransaccionFactory.Parameters.Add(pVALORHIPOTECA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BIENE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBienesRaices, "BienesRaices",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pBienesRaices;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "ModificarBienesRaices", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla BienesRaices de la base de datos
        /// </summary>
        /// <param name="pId">identificador de BienesRaices</param>
        public void EliminarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        BienesRaices pBienesRaices = new BienesRaices();

                        //if (pUsuario.programaGeneraLog)
                        //    pBienesRaices = ConsultarBienesRaices(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_BIEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_BIEN.ParameterName = "p_COD_BIEN";
                        pCOD_BIEN.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BIEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_BIENE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pBienesRaices, "BienesRaices", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "EliminarBienesRaices", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla BienesRaices de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla BienesRaices</param>
        /// <returns>Entidad BienesRaices consultado</returns>
        public BienesRaices ConsultarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            BienesRaices entidad = new BienesRaices();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  BIENESRAICES WHERE COD_BIEN = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_BIEN"] != DBNull.Value) entidad.cod_bien = Convert.ToInt64(resultado["COD_BIEN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["MATRICULA"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["MATRICULA"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORHIPOTECA"] != DBNull.Value) entidad.valorhipoteca = Convert.ToInt64(resultado["VALORHIPOTECA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "ConsultarBienesRaices", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla BienesRaices dados unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BienesRaices> lstBienesRaices = new List<BienesRaices>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  BIENESRAICES ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BienesRaices entidad = new BienesRaices();

                            if (resultado["COD_BIEN"] != DBNull.Value) entidad.cod_bien = Convert.ToInt64(resultado["COD_BIEN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["MATRICULA"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["MATRICULA"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORHIPOTECA"] != DBNull.Value) entidad.valorhipoteca = Convert.ToInt64(resultado["VALORHIPOTECA"]);

                            lstBienesRaices.Add(entidad);
                        }

                        return lstBienesRaices;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "ListarBienesRaices", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla BienesRaices dados unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaicesRepo(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BienesRaices> lstBienesRaices = new List<BienesRaices>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  BIENESRAICES where COD_PERSONA = " + pBienesRaices.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BienesRaices entidad = new BienesRaices();

                            if (resultado["COD_BIEN"] != DBNull.Value) entidad.cod_bien = Convert.ToInt64(resultado["COD_BIEN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["MATRICULA"] != DBNull.Value) entidad.matricula = Convert.ToString(resultado["MATRICULA"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORHIPOTECA"] != DBNull.Value) entidad.valorhipoteca = Convert.ToInt64(resultado["VALORHIPOTECA"]);

                            lstBienesRaices.Add(entidad);
                        }

                        return lstBienesRaices;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BienesRaicesData", "ListarBienesRaicesRepo", ex);
                        return null;
                    }
                }
            }
        }

    }
}