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
    /// Objeto de acceso a datos para la tabla VENTASMENSUALES
    /// </summary>
    public class EstacionalidadMensualData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla VENTASMENSUALES
        /// </summary>
        public EstacionalidadMensualData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla VENTASMENSUALES de la base de datos
        /// </summary>
        /// <param name="pEstacionalidadMensual">Entidad EstacionalidadMensual</param>
        /// <returns>Entidad EstacionalidadMensual creada</returns>
        public EstacionalidadMensual CrearEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = 0;
                        pCOD_VENTAS.Direction = ParameterDirection.Output;

                        DbParameter pTIPOVENTAS = cmdTransaccionFactory.CreateParameter();
                        pTIPOVENTAS.ParameterName = "p_TIPOVENTAS";
                        pTIPOVENTAS.Value = pEstacionalidadMensual.tipoventas;
                   
                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pEstacionalidadMensual.valor;

                        DbParameter pENERO = cmdTransaccionFactory.CreateParameter();
                        pENERO.ParameterName = "p_ENERO";
                        pENERO.Value = pEstacionalidadMensual.enero;

                        DbParameter pFEBRERO = cmdTransaccionFactory.CreateParameter();
                        pFEBRERO.ParameterName = "p_FEBRERO";
                        pFEBRERO.Value = pEstacionalidadMensual.febrero;

                        DbParameter pMARZO = cmdTransaccionFactory.CreateParameter();
                        pMARZO.ParameterName = "p_MARZO";
                        pMARZO.Value = pEstacionalidadMensual.marzo;

                        DbParameter pABRIL = cmdTransaccionFactory.CreateParameter();
                        pABRIL.ParameterName = "p_ABRIL";
                        pABRIL.Value = pEstacionalidadMensual.abril;

                        DbParameter pMAYO = cmdTransaccionFactory.CreateParameter();
                        pMAYO.ParameterName = "p_MAYO";
                        pMAYO.Value = pEstacionalidadMensual.mayo;

                        DbParameter pJUNIO = cmdTransaccionFactory.CreateParameter();
                        pJUNIO.ParameterName = "p_JUNIO";
                        pJUNIO.Value = pEstacionalidadMensual.junio;

                        DbParameter pJULIO = cmdTransaccionFactory.CreateParameter();
                        pJULIO.ParameterName = "p_JULIO";
                        pJULIO.Value = pEstacionalidadMensual.julio;

                        DbParameter pAGOSTO = cmdTransaccionFactory.CreateParameter();
                        pAGOSTO.ParameterName = "p_AGOSTO";
                        pAGOSTO.Value = pEstacionalidadMensual.agosto;

                        DbParameter pSEPTIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pSEPTIEMBRE.ParameterName = "p_SEPTIEMBRE";
                        pSEPTIEMBRE.Value = pEstacionalidadMensual.septiembre;

                        DbParameter pOCTUBRE = cmdTransaccionFactory.CreateParameter();
                        pOCTUBRE.ParameterName = "p_OCTUBRE";
                        pOCTUBRE.Value = pEstacionalidadMensual.octubre;

                        DbParameter pNOVIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOVIEMBRE.ParameterName = "p_NOVIEMBRE";
                        pNOVIEMBRE.Value = pEstacionalidadMensual.noviembre;

                        DbParameter pDICIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pDICIEMBRE.ParameterName = "p_DICIEMBRE";
                        pDICIEMBRE.Value = pEstacionalidadMensual.diciembre;

                        DbParameter pTOTAL = cmdTransaccionFactory.CreateParameter();
                        pTOTAL.ParameterName = "p_TOTAL";
                        pTOTAL.Value = pEstacionalidadMensual.total;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pEstacionalidadMensual.codpersona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVENTAS);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pENERO);
                        cmdTransaccionFactory.Parameters.Add(pFEBRERO);
                        cmdTransaccionFactory.Parameters.Add(pMARZO);
                        cmdTransaccionFactory.Parameters.Add(pABRIL);
                        cmdTransaccionFactory.Parameters.Add(pMAYO);
                        cmdTransaccionFactory.Parameters.Add(pJUNIO);
                        cmdTransaccionFactory.Parameters.Add(pJULIO);
                        cmdTransaccionFactory.Parameters.Add(pAGOSTO);
                        cmdTransaccionFactory.Parameters.Add(pSEPTIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pOCTUBRE);
                        cmdTransaccionFactory.Parameters.Add(pNOVIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDICIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENME_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEstacionalidadMensual, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEstacionalidadMensual.cod_ventas = Convert.ToInt64(pCOD_VENTAS.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstacionalidadMensual;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstacionalidadMensualData", "CrearEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla VENTASMENSUALES de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad EstacionalidadMensual modificada</returns>
        public EstacionalidadMensual ModificarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pEstacionalidadMensual.cod_ventas;

                        DbParameter pTIPOVENTAS = cmdTransaccionFactory.CreateParameter();
                        pTIPOVENTAS.ParameterName = "p_TIPOVENTAS";
                        pTIPOVENTAS.Value = pEstacionalidadMensual.tipoventas;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pEstacionalidadMensual.valor;

                        DbParameter pENERO = cmdTransaccionFactory.CreateParameter();
                        pENERO.ParameterName = "p_ENERO";
                        pENERO.Value = pEstacionalidadMensual.enero;

                        DbParameter pFEBRERO = cmdTransaccionFactory.CreateParameter();
                        pFEBRERO.ParameterName = "p_FEBRERO";
                        pFEBRERO.Value = pEstacionalidadMensual.febrero;

                        DbParameter pMARZO = cmdTransaccionFactory.CreateParameter();
                        pMARZO.ParameterName = "p_MARZO";
                        pMARZO.Value = pEstacionalidadMensual.marzo;

                        DbParameter pABRIL = cmdTransaccionFactory.CreateParameter();
                        pABRIL.ParameterName = "p_ABRIL";
                        pABRIL.Value = pEstacionalidadMensual.abril;

                        DbParameter pMAYO = cmdTransaccionFactory.CreateParameter();
                        pMAYO.ParameterName = "p_MAYO";
                        pMAYO.Value = pEstacionalidadMensual.mayo;

                        DbParameter pJUNIO = cmdTransaccionFactory.CreateParameter();
                        pJUNIO.ParameterName = "p_JUNIO";
                        pJUNIO.Value = pEstacionalidadMensual.junio;

                        DbParameter pJULIO = cmdTransaccionFactory.CreateParameter();
                        pJULIO.ParameterName = "p_JULIO";
                        pJULIO.Value = pEstacionalidadMensual.julio;

                        DbParameter pAGOSTO = cmdTransaccionFactory.CreateParameter();
                        pAGOSTO.ParameterName = "p_AGOSTO";
                        pAGOSTO.Value = pEstacionalidadMensual.agosto;

                        DbParameter pSEPTIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pSEPTIEMBRE.ParameterName = "p_SEPTIEMBRE";
                        pSEPTIEMBRE.Value = pEstacionalidadMensual.septiembre;

                        DbParameter pOCTUBRE = cmdTransaccionFactory.CreateParameter();
                        pOCTUBRE.ParameterName = "p_OCTUBRE";
                        pOCTUBRE.Value = pEstacionalidadMensual.octubre;

                        DbParameter pNOVIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pNOVIEMBRE.ParameterName = "p_NOVIEMBRE";
                        pNOVIEMBRE.Value = pEstacionalidadMensual.noviembre;

                        DbParameter pDICIEMBRE = cmdTransaccionFactory.CreateParameter();
                        pDICIEMBRE.ParameterName = "p_DICIEMBRE";
                        pDICIEMBRE.Value = pEstacionalidadMensual.diciembre;

                        DbParameter pTOTAL = cmdTransaccionFactory.CreateParameter();
                        pTOTAL.ParameterName = "p_TOTAL";
                        pTOTAL.Value = pEstacionalidadMensual.total;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pEstacionalidadMensual.codpersona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVENTAS);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pENERO);
                        cmdTransaccionFactory.Parameters.Add(pFEBRERO);
                        cmdTransaccionFactory.Parameters.Add(pMARZO);
                        cmdTransaccionFactory.Parameters.Add(pABRIL);
                        cmdTransaccionFactory.Parameters.Add(pMAYO);
                        cmdTransaccionFactory.Parameters.Add(pJUNIO);
                        cmdTransaccionFactory.Parameters.Add(pJULIO);
                        cmdTransaccionFactory.Parameters.Add(pAGOSTO);
                        cmdTransaccionFactory.Parameters.Add(pSEPTIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pOCTUBRE);
                        cmdTransaccionFactory.Parameters.Add(pNOVIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pDICIEMBRE);
                        cmdTransaccionFactory.Parameters.Add(pTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENME_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEstacionalidadMensual, "VENTASMENSUALES",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstacionalidadMensual;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstacionalidadMensualData", "ModificarEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla VENTASMENSUALES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de VENTASMENSUALES</param>
        public void EliminarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EstacionalidadMensual pEstacionalidadMensual = new EstacionalidadMensual();

                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENME_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    //    if (pUsuario.programaGeneraLog)
                    //        DAauditoria.InsertarLog(pEstacionalidadMensual, "VENTASMENSUALES", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstacionalidadMensualData", "InsertarEstacionalidadMensual", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla VENTASMENSUALES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla VENTASMENSUALES</param>
        /// <returns>Entidad EstacionalidadMensual consultado</returns>
        public EstacionalidadMensual ConsultarEstacionalidadMensual(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            EstacionalidadMensual entidad = new EstacionalidadMensual();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VENTASMENSUALES WHERE COD_VENTAS = " + pId.ToString() + " ORDER BY COD_VENTAS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventas = Convert.ToString(resultado["TIPOVENTAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["ENERO"] != DBNull.Value) entidad.enero = Convert.ToInt64(resultado["ENERO"]);
                            if (resultado["FEBRERO"] != DBNull.Value) entidad.febrero = Convert.ToInt64(resultado["FEBRERO"]);
                            if (resultado["MARZO"] != DBNull.Value) entidad.marzo = Convert.ToInt64(resultado["MARZO"]);
                            if (resultado["ABRIL"] != DBNull.Value) entidad.abril = Convert.ToInt64(resultado["ABRIL"]);
                            if (resultado["MAYO"] != DBNull.Value) entidad.mayo = Convert.ToInt64(resultado["MAYO"]);
                            if (resultado["JUNIO"] != DBNull.Value) entidad.junio = Convert.ToInt64(resultado["JUNIO"]);
                            if (resultado["JULIO"] != DBNull.Value) entidad.julio = Convert.ToInt64(resultado["JULIO"]);
                            if (resultado["AGOSTO"] != DBNull.Value) entidad.agosto = Convert.ToInt64(resultado["AGOSTO"]);
                            if (resultado["SEPTIEMBRE"] != DBNull.Value) entidad.septiembre = Convert.ToInt64(resultado["SEPTIEMBRE"]);
                            if (resultado["OCTUBRE"] != DBNull.Value) entidad.octubre = Convert.ToInt64(resultado["OCTUBRE"]);
                            if (resultado["NOVIEMBRE"] != DBNull.Value) entidad.noviembre = Convert.ToInt64(resultado["NOVIEMBRE"]);
                            if (resultado["DICIEMBRE"] != DBNull.Value) entidad.diciembre = Convert.ToInt64(resultado["DICIEMBRE"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["TOTAL"]);
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
                        BOExcepcion.Throw("EstacionalidadMensualData", "ConsultarEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla VENTASMENSUALES dados unos filtros
        /// </summary>
        /// <param name="pVENTASMENSUALES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<EstacionalidadMensual> ListarEstacionalidadMensual(EstacionalidadMensual pEstacionalidadMensual, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EstacionalidadMensual> lstEstacionalidadMensual = new List<EstacionalidadMensual>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VENTASMENSUALES where codpersona = " + pEstacionalidadMensual.codpersona.ToString() + " order by TIPOVENTAS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstacionalidadMensual entidad = new EstacionalidadMensual();

                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventas = Convert.ToString(resultado["TIPOVENTAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["ENERO"] != DBNull.Value) entidad.enero = Convert.ToInt64(resultado["ENERO"]);
                            if (resultado["FEBRERO"] != DBNull.Value) entidad.febrero = Convert.ToInt64(resultado["FEBRERO"]);
                            if (resultado["MARZO"] != DBNull.Value) entidad.marzo = Convert.ToInt64(resultado["MARZO"]);
                            if (resultado["ABRIL"] != DBNull.Value) entidad.abril = Convert.ToInt64(resultado["ABRIL"]);
                            if (resultado["MAYO"] != DBNull.Value) entidad.mayo = Convert.ToInt64(resultado["MAYO"]);
                            if (resultado["JUNIO"] != DBNull.Value) entidad.junio = Convert.ToInt64(resultado["JUNIO"]);
                            if (resultado["JULIO"] != DBNull.Value) entidad.julio = Convert.ToInt64(resultado["JULIO"]);
                            if (resultado["AGOSTO"] != DBNull.Value) entidad.agosto = Convert.ToInt64(resultado["AGOSTO"]);
                            if (resultado["SEPTIEMBRE"] != DBNull.Value) entidad.septiembre = Convert.ToInt64(resultado["SEPTIEMBRE"]);
                            if (resultado["OCTUBRE"] != DBNull.Value) entidad.octubre = Convert.ToInt64(resultado["OCTUBRE"]);
                            if (resultado["NOVIEMBRE"] != DBNull.Value) entidad.noviembre = Convert.ToInt64(resultado["NOVIEMBRE"]);
                            if (resultado["DICIEMBRE"] != DBNull.Value) entidad.diciembre = Convert.ToInt64(resultado["DICIEMBRE"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["TOTAL"]);

                            lstEstacionalidadMensual.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstacionalidadMensual;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstacionalidadMensualData", "ListarEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }





        /// <summary>
        /// Obtiene listas desplegables
        /// </summary>
        /// <param name="pVENTASMENSUALES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<EstacionalidadMensual> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EstacionalidadMensual> lstEstacionalidadMensual = new List<EstacionalidadMensual>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM "  + ListaSolicitada.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstacionalidadMensual entidad = new EstacionalidadMensual();

                            if (resultado["ID"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ID"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["DESCRIPCION"]);
                           
                            lstEstacionalidadMensual.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstacionalidadMensual;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstacionalidadMensualData", "ListarEstacionalidadMensual", ex);
                        return null;
                    }
                }
            }
        }

    }
}