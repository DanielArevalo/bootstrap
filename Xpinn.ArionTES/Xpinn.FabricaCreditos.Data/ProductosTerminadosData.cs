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
    /// Objeto de acceso a datos para la tabla PRODUCTOSTERMINADOS
    /// </summary>
    public class ProductosTerminadosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PRODUCTOSTERMINADOS
        /// </summary>
        public ProductosTerminadosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PRODUCTOSTERMINADOS de la base de datos
        /// </summary>
        /// <param name="pProductosTerminados">Entidad ProductosTerminados</param>
        /// <returns>Entidad ProductosTerminados creada</returns>
        public ProductosTerminados CrearProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRODTER = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODTER.ParameterName = "p_COD_PRODTER";
                        pCOD_PRODTER.Value = 0;
                        pCOD_PRODTER.Direction = ParameterDirection.Output;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pProductosTerminados.cod_inffin;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pProductosTerminados.cantidad;

                        DbParameter pPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pPRODUCTO.ParameterName = "p_PRODUCTO";
                        pPRODUCTO.Value = pProductosTerminados.producto;

                        DbParameter pVRUNITARIO = cmdTransaccionFactory.CreateParameter();
                        pVRUNITARIO.ParameterName = "p_VRUNITARIO";
                        pVRUNITARIO.Value = pProductosTerminados.vrunitario;

                        DbParameter pVRTOTAL = cmdTransaccionFactory.CreateParameter();
                        pVRTOTAL.ParameterName = "p_VRTOTAL";
                        pVRTOTAL.Value = pProductosTerminados.vrtotal;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pProductosTerminados.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODTER);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pVRUNITARIO);
                        cmdTransaccionFactory.Parameters.Add(pVRTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODT_CREAR";                        
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pProductosTerminados.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pProductosTerminados.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProductosTerminados, "PRODUCTOSTERMINADOS",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pProductosTerminados.cod_prodter = Convert.ToInt64(pCOD_PRODTER.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProductosTerminados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosTerminadosData", "CrearProductosTerminados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PRODUCTOSTERMINADOS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ProductosTerminados modificada</returns>
        public ProductosTerminados ModificarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRODTER = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODTER.ParameterName = "p_COD_PRODTER";
                        pCOD_PRODTER.Value = pProductosTerminados.cod_prodter;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pProductosTerminados.cod_inffin;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pProductosTerminados.cantidad;

                        DbParameter pPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pPRODUCTO.ParameterName = "p_PRODUCTO";
                        pPRODUCTO.Value = pProductosTerminados.producto;

                        DbParameter pVRUNITARIO = cmdTransaccionFactory.CreateParameter();
                        pVRUNITARIO.ParameterName = "p_VRUNITARIO";
                        pVRUNITARIO.Value = pProductosTerminados.vrunitario;

                        DbParameter pVRTOTAL = cmdTransaccionFactory.CreateParameter();
                        pVRTOTAL.ParameterName = "p_VRTOTAL";
                        pVRTOTAL.Value = pProductosTerminados.vrtotal;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pProductosTerminados.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODTER);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pVRUNITARIO);
                        cmdTransaccionFactory.Parameters.Add(pVRTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODT_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pProductosTerminados.cod_inffin;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pProductosTerminados.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProductosTerminados, "PRODUCTOSTERMINADOS",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProductosTerminados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosTerminadosData", "ModificarProductosTerminados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PRODUCTOSTERMINADOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PRODUCTOSTERMINADOS</param>
        public void EliminarProductosTerminados(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ProductosTerminados pProductosTerminados = new ProductosTerminados();

                        //if (pUsuario.programaGeneraLog)
                        //    pProductosTerminados = ConsultarProductosTerminados(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PRODTER = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODTER.ParameterName = "p_COD_PRODTER";
                        pCOD_PRODTER.Value = pId;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = Cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODTER);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODT_ELIMI";
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
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosTerminadosData", "EliminarProductosTerminados", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PRODUCTOSTERMINADOS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PRODUCTOSTERMINADOS</param>
        /// <returns>Entidad ProductosTerminados consultado</returns>
        public ProductosTerminados ConsultarProductosTerminados(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ProductosTerminados entidad = new ProductosTerminados();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PRODUCTOSTERMINADOS WHERE COD_PRODTER = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PRODTER"] != DBNull.Value) entidad.cod_prodter = Convert.ToInt64(resultado["COD_PRODTER"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["VRUNITARIO"] != DBNull.Value) entidad.vrunitario = Convert.ToInt64(resultado["VRUNITARIO"]);
                            if (resultado["VRTOTAL"] != DBNull.Value) entidad.vrtotal = Convert.ToInt64(resultado["VRTOTAL"]);
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
                        BOExcepcion.Throw("ProductosTerminadosData", "ConsultarProductosTerminados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PRODUCTOSTERMINADOS dados unos filtros
        /// </summary>
        /// <param name="pPRODUCTOSTERMINADOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminados(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductosTerminados> lstProductosTerminados = new List<ProductosTerminados>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT PRODUCTOSTERMINADOS.* FROM INFORMACIONFINANCIERA, PRODUCTOSTERMINADOS 
                                       WHERE 
                                       PRODUCTOSTERMINADOS.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pProductosTerminados.cod_persona +
                                       " and INFORMACIONFINANCIERA.COD_INFFIN = " + pProductosTerminados.cod_inffin;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductosTerminados entidad = new ProductosTerminados();

                            if (resultado["COD_PRODTER"] != DBNull.Value) entidad.cod_prodter = Convert.ToInt64(resultado["COD_PRODTER"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["VRUNITARIO"] != DBNull.Value) entidad.vrunitario = Convert.ToInt64(resultado["VRUNITARIO"]);
                            if (resultado["VRTOTAL"] != DBNull.Value) entidad.vrtotal = Convert.ToInt64(resultado["VRTOTAL"]);

                            lstProductosTerminados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductosTerminados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosTerminadosData", "ListarProductosTerminados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PRODUCTOSTERMINADOS dados unos filtros
        /// </summary>
        /// <param name="pPRODUCTOSTERMINADOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosTerminados obtenidos</returns>
        public List<ProductosTerminados> ListarProductosTerminadosRepo(ProductosTerminados pProductosTerminados, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductosTerminados> lstProductosTerminados = new List<ProductosTerminados>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                    
                        string sql = @"SELECT PRODUCTOSTERMINADOS.* FROM INFORMACIONFINANCIERA, PRODUCTOSTERMINADOS 
                                       WHERE 
                                       PRODUCTOSTERMINADOS.COD_INFFIN = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pProductosTerminados.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductosTerminados entidad = new ProductosTerminados();

                            if (resultado["COD_PRODTER"] != DBNull.Value) entidad.cod_prodter = Convert.ToInt64(resultado["COD_PRODTER"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["VRUNITARIO"] != DBNull.Value) entidad.vrunitario = Convert.ToInt64(resultado["VRUNITARIO"]);
                            if (resultado["VRTOTAL"] != DBNull.Value) entidad.vrtotal = Convert.ToInt64(resultado["VRTOTAL"]);

                            lstProductosTerminados.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductosTerminados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosTerminadosData", "ListarProductosTerminadosRepo", ex);
                        return null;
                    }
                }
            }
        }

    }
}