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
    /// Objeto de acceso a datos para la tabla PRODUCTOSPROCESO
    /// </summary>
    public class ProductosProcesoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PRODUCTOSPROCESO
        /// </summary>
        public ProductosProcesoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PRODUCTOSPROCESO de la base de datos
        /// </summary>
        /// <param name="pProductosProceso">Entidad ProductosProceso</param>
        /// <returns>Entidad ProductosProceso creada</returns>
        public ProductosProceso CrearProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRODPROC = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODPROC.ParameterName = "p_COD_PRODPROC";
                        pCOD_PRODPROC.Value = 0;
                        pCOD_PRODPROC.Direction = ParameterDirection.Output;

                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pProductosProceso.cod_balance;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pProductosProceso.cantidad;

                        DbParameter pPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pPRODUCTO.ParameterName = "p_PRODUCTO";
                        pPRODUCTO.Value = pProductosProceso.producto;

                        DbParameter pPORCPD = cmdTransaccionFactory.CreateParameter();
                        pPORCPD.ParameterName = "p_PORCPD";
                        pPORCPD.Value = pProductosProceso.porcpd;

                        DbParameter pVALUNITARIO = cmdTransaccionFactory.CreateParameter();
                        pVALUNITARIO.ParameterName = "p_VALUNITARIO";
                        pVALUNITARIO.Value = pProductosProceso.valunitario;

                        DbParameter pVALORTOTAL = cmdTransaccionFactory.CreateParameter();
                        pVALORTOTAL.ParameterName = "p_VALORTOTAL";
                        pVALORTOTAL.Value = pProductosProceso.valortotal;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pProductosProceso.cod_persona;


                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODPROC);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pPORCPD);
                        cmdTransaccionFactory.Parameters.Add(pVALUNITARIO);
                        cmdTransaccionFactory.Parameters.Add(pVALORTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pProductosProceso.cod_balance;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pProductosProceso.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();


                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProductosProceso, "PRODUCTOSPROCESO",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pProductosProceso.cod_prodproc = Convert.ToInt64(pCOD_PRODPROC.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProductosProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosProcesoData", "CrearProductosProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PRODUCTOSPROCESO de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ProductosProceso modificada</returns>
        public ProductosProceso ModificarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PRODPROC = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODPROC.ParameterName = "p_COD_PRODPROC";
                        pCOD_PRODPROC.Value = pProductosProceso.cod_prodproc;

                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pProductosProceso.cod_balance;

                        DbParameter pCANTIDAD = cmdTransaccionFactory.CreateParameter();
                        pCANTIDAD.ParameterName = "p_CANTIDAD";
                        pCANTIDAD.Value = pProductosProceso.cantidad;

                        DbParameter pPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pPRODUCTO.ParameterName = "p_PRODUCTO";
                        pPRODUCTO.Value = pProductosProceso.producto;

                        DbParameter pPORCPD = cmdTransaccionFactory.CreateParameter();
                        pPORCPD.ParameterName = "p_PORCPD";
                        pPORCPD.Value = pProductosProceso.porcpd;

                        DbParameter pVALUNITARIO = cmdTransaccionFactory.CreateParameter();
                        pVALUNITARIO.ParameterName = "p_VALUNITARIO";
                        pVALUNITARIO.Value = pProductosProceso.valunitario;

                        DbParameter pVALORTOTAL = cmdTransaccionFactory.CreateParameter();
                        pVALORTOTAL.ParameterName = "p_VALORTOTAL";
                        pVALORTOTAL.Value = pProductosProceso.valortotal;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pProductosProceso.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODPROC);
                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);
                        cmdTransaccionFactory.Parameters.Add(pCANTIDAD);
                        cmdTransaccionFactory.Parameters.Add(pPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pPORCPD);
                        cmdTransaccionFactory.Parameters.Add(pVALUNITARIO);
                        cmdTransaccionFactory.Parameters.Add(pVALORTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODP_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        // ACTUALIZA LOS ESTADOS FINANCIEROS                       

                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();

                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pProductosProceso.cod_balance;

                        DbParameter pCOD_PERSONA1 = cmdTransaccionFactory2.CreateParameter();
                        pCOD_PERSONA1.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA1.Value = pProductosProceso.cod_persona;

                        cmdTransaccionFactory2.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory2.Parameters.Add(pCOD_PERSONA1);

                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();


                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProductosProceso, "PRODUCTOSPROCESO",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProductosProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosProcesoData", "ModificarProductosProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PRODUCTOSPROCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PRODUCTOSPROCESO</param>
        public void EliminarProductosProceso(Int64 pId, Usuario pUsuario, Int64 Cod_persona, Int64 Cod_InfFin)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ProductosProceso pProductosProceso = new ProductosProceso();

                        //if (pUsuario.programaGeneraLog)
                        //    pProductosProceso = ConsultarProductosProceso(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_PRODPROC = cmdTransaccionFactory.CreateParameter();
                        pCOD_PRODPROC.ParameterName = "p_COD_PRODPROC";
                        pCOD_PRODPROC.Value = pId;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = Cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PRODPROC);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_PRODP_ELIMI";
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
                        //    DAauditoria.InsertarLog(pProductosProceso, "PRODUCTOSPROCESO", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosProcesoData", "EliminarProductosProceso", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PRODUCTOSPROCESO de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PRODUCTOSPROCESO</param>
        /// <returns>Entidad ProductosProceso consultado</returns>
        public ProductosProceso ConsultarProductosProceso(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ProductosProceso entidad = new ProductosProceso();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PRODUCTOSPROCESO WHERE COD_PRODPROC = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PRODPROC"] != DBNull.Value) entidad.cod_prodproc = Convert.ToInt64(resultado["COD_PRODPROC"]);
                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["PORCPD"] != DBNull.Value) entidad.porcpd = Convert.ToInt64(resultado["PORCPD"]);
                            if (resultado["VALUNITARIO"] != DBNull.Value) entidad.valunitario = Convert.ToInt64(resultado["VALUNITARIO"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToInt64(resultado["VALORTOTAL"]);
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
                        BOExcepcion.Throw("ProductosProcesoData", "ConsultarProductosProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PRODUCTOSPROCESO dados unos filtros
        /// </summary>
        /// <param name="pPRODUCTOSPROCESO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProceso(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductosProceso> lstProductosProceso = new List<ProductosProceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  PRODUCTOSPROCESO " ;
                        string sql = @"SELECT PRODUCTOSPROCESO.* FROM INFORMACIONFINANCIERA, PRODUCTOSPROCESO 
                                       WHERE 
                                       PRODUCTOSPROCESO.COD_BALANCE = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pProductosProceso.cod_persona +
                                       " and INFORMACIONFINANCIERA.COD_INFFIN = " + pProductosProceso.cod_balance;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductosProceso entidad = new ProductosProceso();

                            if (resultado["COD_PRODPROC"] != DBNull.Value) entidad.cod_prodproc = Convert.ToInt64(resultado["COD_PRODPROC"]);
                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["PORCPD"] != DBNull.Value) entidad.porcpd = Convert.ToInt64(resultado["PORCPD"]);
                            if (resultado["VALUNITARIO"] != DBNull.Value) entidad.valunitario = Convert.ToInt64(resultado["VALUNITARIO"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToInt64(resultado["VALORTOTAL"]);

                            lstProductosProceso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductosProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosProcesoData", "ListarProductosProceso", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla PRODUCTOSPROCESO dados unos filtros
        /// </summary>
        /// <param name="pPRODUCTOSPROCESO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProductosProceso obtenidos</returns>
        public List<ProductosProceso> ListarProductosProcesoRepo(ProductosProceso pProductosProceso, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductosProceso> lstProductosProceso = new List<ProductosProceso>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "SELECT * FROM  PRODUCTOSPROCESO " ;
                        string sql = @"SELECT PRODUCTOSPROCESO.* FROM INFORMACIONFINANCIERA, PRODUCTOSPROCESO 
                                       WHERE 
                                       PRODUCTOSPROCESO.COD_BALANCE = INFORMACIONFINANCIERA.COD_INFFIN AND
                                       INFORMACIONFINANCIERA.COD_PERSONA = " + pProductosProceso.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductosProceso entidad = new ProductosProceso();

                            if (resultado["COD_PRODPROC"] != DBNull.Value) entidad.cod_prodproc = Convert.ToInt64(resultado["COD_PRODPROC"]);
                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (resultado["PRODUCTO"] != DBNull.Value) entidad.producto = Convert.ToString(resultado["PRODUCTO"]);
                            if (resultado["PORCPD"] != DBNull.Value) entidad.porcpd = Convert.ToInt64(resultado["PORCPD"]);
                            if (resultado["VALUNITARIO"] != DBNull.Value) entidad.valunitario = Convert.ToInt64(resultado["VALUNITARIO"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToInt64(resultado["VALORTOTAL"]);

                            lstProductosProceso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductosProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosProcesoData", "ListarProductosProceso", ex);
                        return null;
                    }
                }
            }
        }

    }
}