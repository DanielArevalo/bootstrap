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
    /// Objeto de acceso a datos para la tabla COSTEOPRODUCTOS
    /// </summary>
    public class CosteoProductosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla COSTEOPRODUCTOS
        /// </summary>
        public CosteoProductosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla COSTEOPRODUCTOS de la base de datos
        /// </summary>
        /// <param name="pCosteoProductos">Entidad CosteoProductos</param>
        /// <returns>Entidad CosteoProductos creada</returns>
        public CosteoProductos CrearCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_COSTEO = cmdTransaccionFactory.CreateParameter();
                        pCOD_COSTEO.ParameterName = "p_COD_COSTEO";
                        pCOD_COSTEO.Value = 0;
                        pCOD_COSTEO.Direction = ParameterDirection.Output;

                        DbParameter pCOD_MARGEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_MARGEN.ParameterName = "p_COD_MARGEN";
                        pCOD_MARGEN.Value = pCosteoProductos.cod_margen;

                        DbParameter pMATERIAPRIMA = cmdTransaccionFactory.CreateParameter();
                        pMATERIAPRIMA.ParameterName = "p_MATERIAPRIMA";
                        pMATERIAPRIMA.Value = pCosteoProductos.materiaprima;

                        DbParameter pUNIDADCOMPRA = cmdTransaccionFactory.CreateParameter();
                        pUNIDADCOMPRA.ParameterName = "p_UNIDADCOMPRA";
                        pUNIDADCOMPRA.Value = pCosteoProductos.unidadcompra;

                        DbParameter pCOSTOUNIDAD = cmdTransaccionFactory.CreateParameter();
                        pCOSTOUNIDAD.ParameterName = "p_COSTOUNIDAD";
                        pCOSTOUNIDAD.Value = pCosteoProductos.costounidad;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pCosteoProductos.cantidad;

                        DbParameter pCOSTO = cmdTransaccionFactory.CreateParameter();
                        pCOSTO.ParameterName = "p_COSTO";
                        pCOSTO.Value = pCosteoProductos.costo;


                        cmdTransaccionFactory.Parameters.Add(pCOD_COSTEO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MARGEN);
                        cmdTransaccionFactory.Parameters.Add(pMATERIAPRIMA);
                        cmdTransaccionFactory.Parameters.Add(pUNIDADCOMPRA);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOUNIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCOSTO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COSPR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCosteoProductos, "COSTEOPRODUCTOS",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pCosteoProductos.cod_costeo = Convert.ToInt64(pCOD_COSTEO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCosteoProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosteoProductosData", "CrearCosteoProductos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla COSTEOPRODUCTOS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad CosteoProductos modificada</returns>
        public CosteoProductos ModificarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_COSTEO = cmdTransaccionFactory.CreateParameter();
                        pCOD_COSTEO.ParameterName = "p_COD_COSTEO";
                        pCOD_COSTEO.Value = pCosteoProductos.cod_costeo;

                        DbParameter pCOD_MARGEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_MARGEN.ParameterName = "p_COD_MARGEN";
                        pCOD_MARGEN.Value = pCosteoProductos.cod_margen;

                        DbParameter pMATERIAPRIMA = cmdTransaccionFactory.CreateParameter();
                        pMATERIAPRIMA.ParameterName = "p_MATERIAPRIMA";
                        pMATERIAPRIMA.Value = pCosteoProductos.materiaprima;

                        DbParameter pUNIDADCOMPRA = cmdTransaccionFactory.CreateParameter();
                        pUNIDADCOMPRA.ParameterName = "p_UNIDADCOMPRA";
                        pUNIDADCOMPRA.Value = pCosteoProductos.unidadcompra;

                        DbParameter pCOSTOUNIDAD = cmdTransaccionFactory.CreateParameter();
                        pCOSTOUNIDAD.ParameterName = "p_COSTOUNIDAD";
                        pCOSTOUNIDAD.Value = pCosteoProductos.costounidad;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pCosteoProductos.cantidad;

                        DbParameter pCOSTO = cmdTransaccionFactory.CreateParameter();
                        pCOSTO.ParameterName = "p_COSTO";
                        pCOSTO.Value = pCosteoProductos.costo;

                        cmdTransaccionFactory.Parameters.Add(pCOD_COSTEO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MARGEN);
                        cmdTransaccionFactory.Parameters.Add(pMATERIAPRIMA);
                        cmdTransaccionFactory.Parameters.Add(pUNIDADCOMPRA);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOUNIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pCOSTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COSPR_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCosteoProductos, "COSTEOPRODUCTOS",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCosteoProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosteoProductosData", "ModificarCosteoProductos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla COSTEOPRODUCTOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de COSTEOPRODUCTOS</param>
        public void EliminarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CosteoProductos pCosteoProductos = new CosteoProductos();

                        //if (pUsuario.programaGeneraLog)
                        //    pCosteoProductos = ConsultarCosteoProductos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_COSTEO = cmdTransaccionFactory.CreateParameter();
                        pCOD_COSTEO.ParameterName = "p_COD_COSTEO";
                        pCOD_COSTEO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_COSTEO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_COSPR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pCosteoProductos, "COSTEOPRODUCTOS", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosteoProductosData", "InsertarCosteoProductos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla COSTEOPRODUCTOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla COSTEOPRODUCTOS</param>
        /// <returns>Entidad CosteoProductos consultado</returns>
        public CosteoProductos ConsultarCosteoProductos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            CosteoProductos entidad = new CosteoProductos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  COSTEOPRODUCTOS WHERE COD_COSTEO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_COSTEO"] != DBNull.Value) entidad.cod_costeo = Convert.ToInt64(resultado["COD_COSTEO"]);
                            if (resultado["COD_MARGEN"] != DBNull.Value) entidad.cod_margen = Convert.ToInt64(resultado["COD_MARGEN"]);
                            if (resultado["MATERIAPRIMA"] != DBNull.Value) entidad.materiaprima = Convert.ToString(resultado["MATERIAPRIMA"]);
                            if (resultado["UNIDADCOMPRA"] != DBNull.Value) entidad.unidadcompra = Convert.ToString(resultado["UNIDADCOMPRA"]);
                            if (resultado["COSTOUNIDAD"] != DBNull.Value) entidad.costounidad = Convert.ToInt64(resultado["COSTOUNIDAD"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["COSTO"] != DBNull.Value) entidad.costo = Convert.ToInt64(resultado["COSTO"]);
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
                        BOExcepcion.Throw("CosteoProductosData", "ConsultarCosteoProductos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla COSTEOPRODUCTOS dados unos filtros
        /// </summary>
        /// <param name="pCOSTEOPRODUCTOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CosteoProductos obtenidos</returns>
        public List<CosteoProductos> ListarCosteoProductos(CosteoProductos pCosteoProductos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CosteoProductos> lstCosteoProductos = new List<CosteoProductos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  COSTEOPRODUCTOS " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CosteoProductos entidad = new CosteoProductos();

                            if (resultado["COD_COSTEO"] != DBNull.Value) entidad.cod_costeo = Convert.ToInt64(resultado["COD_COSTEO"]);
                            if (resultado["COD_MARGEN"] != DBNull.Value) entidad.cod_margen = Convert.ToInt64(resultado["COD_MARGEN"]);
                            if (resultado["MATERIAPRIMA"] != DBNull.Value) entidad.materiaprima = Convert.ToString(resultado["MATERIAPRIMA"]);
                            if (resultado["UNIDADCOMPRA"] != DBNull.Value) entidad.unidadcompra = Convert.ToString(resultado["UNIDADCOMPRA"]);
                            if (resultado["COSTOUNIDAD"] != DBNull.Value) entidad.costounidad = Convert.ToInt64(resultado["COSTOUNIDAD"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["COSTO"] != DBNull.Value) entidad.costo = Convert.ToInt64(resultado["COSTO"]);

                            lstCosteoProductos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCosteoProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosteoProductosData", "ListarCosteoProductos", ex);
                        return null;
                    }
                }
            }
        }

    }
}