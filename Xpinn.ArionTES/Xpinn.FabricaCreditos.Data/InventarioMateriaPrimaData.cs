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
    /// Objeto de acceso a datos para la tabla INVENTARIOMATPRIMA
    /// </summary>
    public class InventarioMateriaPrimaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla INVENTARIOMATPRIMA
        /// </summary>
        public InventarioMateriaPrimaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla INVENTARIOMATPRIMA de la base de datos
        /// </summary>
        /// <param name="pInventarioMateriaPrima">Entidad InventarioMateriaPrima</param>
        /// <returns>Entidad InventarioMateriaPrima creada</returns>
        public InventarioMateriaPrima CrearInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MATPRIMA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MATPRIMA.ParameterName = "p_COD_MATPRIMA";
                        pCOD_MATPRIMA.Value = 0;
                        pCOD_MATPRIMA.Direction = ParameterDirection.Output;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInventarioMateriaPrima.cod_inffin;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInventarioMateriaPrima.descripcion;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pInventarioMateriaPrima.valor;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInventarioMateriaPrima.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MATPRIMA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVMA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pInventarioMateriaPrima.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pInventarioMateriaPrima.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();


                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioMateriaPrima, "INVENTARIOMATPRIMA",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pInventarioMateriaPrima.cod_matprima = Convert.ToInt64(pCOD_MATPRIMA.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInventarioMateriaPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "CrearInventarioMateriaPrima", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla INVENTARIOMATPRIMA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad InventarioMateriaPrima modificada</returns>
        public InventarioMateriaPrima ModificarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MATPRIMA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MATPRIMA.ParameterName = "p_COD_MATPRIMA";
                        pCOD_MATPRIMA.Value = pInventarioMateriaPrima.cod_matprima;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInventarioMateriaPrima.cod_inffin;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInventarioMateriaPrima.descripcion;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pInventarioMateriaPrima.valor;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInventarioMateriaPrima.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MATPRIMA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVMA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pInventarioMateriaPrima.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pInventarioMateriaPrima.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioMateriaPrima, "INVENTARIOMATPRIMA",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInventarioMateriaPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "ModificarInventarioMateriaPrima", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla INVENTARIOMATPRIMA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de INVENTARIOMATPRIMA</param>
        public void EliminarInventarioMateriaPrima(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        InventarioMateriaPrima pInventarioMateriaPrima = new InventarioMateriaPrima();

                        //if (pUsuario.programaGeneraLog)
                        //    pInventarioMateriaPrima = ConsultarInventarioMateriaPrima(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_MATPRIMA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MATPRIMA.ParameterName = "p_COD_MATPRIMA";
                        pCOD_MATPRIMA.Value = pId;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = Cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MATPRIMA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INVMA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = Cod_InfFin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = Cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInventarioMateriaPrima, "INVENTARIOMATPRIMA", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "EliminarInventarioMateriaPrima", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla INVENTARIOMATPRIMA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla INVENTARIOMATPRIMA</param>
        /// <returns>Entidad InventarioMateriaPrima consultado</returns>
        public InventarioMateriaPrima ConsultarInventarioMateriaPrima(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            InventarioMateriaPrima entidad = new InventarioMateriaPrima();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INVENTARIOMATPRIMA WHERE COD_MATPRIMA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MATPRIMA"] != DBNull.Value) entidad.cod_matprima = Convert.ToInt64(resultado["COD_MATPRIMA"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
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
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "ConsultarInventarioMateriaPrima", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla INVENTARIOMATPRIMA dados unos filtros
        /// </summary>
        /// <param name="pINVENTARIOMATPRIMA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrima(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InventarioMateriaPrima> lstInventarioMateriaPrima = new List<InventarioMateriaPrima>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  INVENTARIOMATPRIMA " ;

                        string sql = @"SELECT INVENTARIOMATPRIMA.* FROM INFORMACIONFINANCIERA, INVENTARIOMATPRIMA 
                                       WHERE 
                                       INVENTARIOMATPRIMA.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pInventarioMateriaPrima.cod_persona + 
                                       " and INVENTARIOMATPRIMA.COD_INFFIN = " + pInventarioMateriaPrima.cod_inffin ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InventarioMateriaPrima entidad = new InventarioMateriaPrima();

                            if (resultado["COD_MATPRIMA"] != DBNull.Value) entidad.cod_matprima = Convert.ToInt64(resultado["COD_MATPRIMA"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstInventarioMateriaPrima.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstInventarioMateriaPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "ListarInventarioMateriaPrima", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla INVENTARIOMATPRIMA dados unos filtros
        /// </summary>
        /// <param name="pINVENTARIOMATPRIMA">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InventarioMateriaPrima obtenidos</returns>
        public List<InventarioMateriaPrima> ListarInventarioMateriaPrimaRepo(InventarioMateriaPrima pInventarioMateriaPrima, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InventarioMateriaPrima> lstInventarioMateriaPrima = new List<InventarioMateriaPrima>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  INVENTARIOMATPRIMA " ;

                        string sql = @"SELECT INVENTARIOMATPRIMA.* FROM INFORMACIONFINANCIERA, INVENTARIOMATPRIMA 
                                       WHERE 
                                       INVENTARIOMATPRIMA.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pInventarioMateriaPrima.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InventarioMateriaPrima entidad = new InventarioMateriaPrima();

                            if (resultado["COD_MATPRIMA"] != DBNull.Value) entidad.cod_matprima = Convert.ToInt64(resultado["COD_MATPRIMA"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstInventarioMateriaPrima.Add(entidad);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInventarioMateriaPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioMateriaPrimaData", "ListarInventarioMateriaPrimaRepo", ex);
                        return null;
                    }
                }
            }
        }

    }
}