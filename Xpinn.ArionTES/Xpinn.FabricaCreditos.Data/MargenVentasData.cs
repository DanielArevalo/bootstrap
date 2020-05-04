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
    /// Objeto de acceso a datos para la tabla MARGENVENTAS
    /// </summary>
    public class MargenVentasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla MARGENVENTAS
        /// </summary>
        public MargenVentasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla MARGENVENTAS de la base de datos
        /// </summary>
        /// <param name="pMargenVentas">Entidad MargenVentas</param>
        /// <returns>Entidad MargenVentas creada</returns>
        public MargenVentas CrearMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MARGEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_MARGEN.ParameterName = "p_COD_MARGEN";
                        pCOD_MARGEN.Value = 0;
                        pCOD_MARGEN.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pMargenVentas.cod_ventas;

                        DbParameter pTIPOPRODUCO = cmdTransaccionFactory.CreateParameter();
                        pTIPOPRODUCO.ParameterName = "p_TIPOPRODUCO";
                        pTIPOPRODUCO.Value = pMargenVentas.tipoproduco;

                        DbParameter pNOMBREPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pNOMBREPRODUCTO.ParameterName = "p_NOMBREPRODUCTO";
                        pNOMBREPRODUCTO.Value = pMargenVentas.nombreproducto;

                        DbParameter pUNIVENDIDA = cmdTransaccionFactory.CreateParameter();
                        pUNIVENDIDA.ParameterName = "p_UNIVENDIDA";
                        pUNIVENDIDA.Value = pMargenVentas.univendida;

                        DbParameter pCOSTOUNIDVEN = cmdTransaccionFactory.CreateParameter();
                        pCOSTOUNIDVEN.ParameterName = "p_COSTOUNIDVEN";
                        pCOSTOUNIDVEN.Value = pMargenVentas.costounidven;

                        DbParameter pPRECIOUNIDVEN = cmdTransaccionFactory.CreateParameter();
                        pPRECIOUNIDVEN.ParameterName = "p_PRECIOUNIDVEN";
                        pPRECIOUNIDVEN.Value = pMargenVentas.preciounidven;

                        DbParameter pCOSTOVENTA = cmdTransaccionFactory.CreateParameter();
                        pCOSTOVENTA.ParameterName = "p_COSTOVENTA";
                        pCOSTOVENTA.Value = pMargenVentas.costoventa;

                        DbParameter pVENTATOTAL = cmdTransaccionFactory.CreateParameter();
                        pVENTATOTAL.ParameterName = "p_VENTATOTAL";
                        pVENTATOTAL.Value = pMargenVentas.ventatotal;

                        DbParameter pMARGEN = cmdTransaccionFactory.CreateParameter();
                        pMARGEN.ParameterName = "p_MARGEN";
                        pMARGEN.Value = pMargenVentas.margen;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pMargenVentas.cod_persona;
                        
                        cmdTransaccionFactory.Parameters.Add(pCOD_MARGEN);
                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOPRODUCO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBREPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pUNIVENDIDA);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOUNIDVEN);
                        cmdTransaccionFactory.Parameters.Add(pPRECIOUNIDVEN);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOVENTA);
                        cmdTransaccionFactory.Parameters.Add(pVENTATOTAL);
                        cmdTransaccionFactory.Parameters.Add(pMARGEN);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_MARVE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pMargenVentas, "MARGENVENTAS",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pMargenVentas.cod_margen = Convert.ToInt64(pCOD_MARGEN.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMargenVentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MargenVentasData", "CrearMargenVentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla MARGENVENTAS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad MargenVentas modificada</returns>
        public MargenVentas ModificarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MARGEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_MARGEN.ParameterName = "p_COD_MARGEN";
                        pCOD_MARGEN.Value = pMargenVentas.cod_margen;

                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pMargenVentas.cod_ventas;

                        DbParameter pTIPOPRODUCO = cmdTransaccionFactory.CreateParameter();
                        pTIPOPRODUCO.ParameterName = "p_TIPOPRODUCO";
                        pTIPOPRODUCO.Value = pMargenVentas.tipoproduco;

                        DbParameter pNOMBREPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pNOMBREPRODUCTO.ParameterName = "p_NOMBREPRODUCTO";
                        pNOMBREPRODUCTO.Value = pMargenVentas.nombreproducto;

                        DbParameter pUNIVENDIDA = cmdTransaccionFactory.CreateParameter();
                        pUNIVENDIDA.ParameterName = "p_UNIVENDIDA";
                        pUNIVENDIDA.Value = pMargenVentas.univendida;

                        DbParameter pCOSTOUNIDVEN = cmdTransaccionFactory.CreateParameter();
                        pCOSTOUNIDVEN.ParameterName = "p_COSTOUNIDVEN";
                        pCOSTOUNIDVEN.Value = pMargenVentas.costounidven;

                        DbParameter pPRECIOUNIDVEN = cmdTransaccionFactory.CreateParameter();
                        pPRECIOUNIDVEN.ParameterName = "p_PRECIOUNIDVEN";
                        pPRECIOUNIDVEN.Value = pMargenVentas.preciounidven;

                        DbParameter pCOSTOVENTA = cmdTransaccionFactory.CreateParameter();
                        pCOSTOVENTA.ParameterName = "p_COSTOVENTA";
                        pCOSTOVENTA.Value = pMargenVentas.costoventa;

                        DbParameter pVENTATOTAL = cmdTransaccionFactory.CreateParameter();
                        pVENTATOTAL.ParameterName = "p_VENTATOTAL";
                        pVENTATOTAL.Value = pMargenVentas.ventatotal;

                        DbParameter pMARGEN = cmdTransaccionFactory.CreateParameter();
                        pMARGEN.ParameterName = "p_MARGEN";
                        pMARGEN.Value = pMargenVentas.margen;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pMargenVentas.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MARGEN);
                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOPRODUCO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBREPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pUNIVENDIDA);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOUNIDVEN);
                        cmdTransaccionFactory.Parameters.Add(pPRECIOUNIDVEN);
                        cmdTransaccionFactory.Parameters.Add(pCOSTOVENTA);
                        cmdTransaccionFactory.Parameters.Add(pVENTATOTAL);
                        cmdTransaccionFactory.Parameters.Add(pMARGEN);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_MARVE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pMargenVentas, "MARGENVENTAS",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pMargenVentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MargenVentasData", "ModificarMargenVentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla MARGENVENTAS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de MARGENVENTAS</param>
        public void EliminarMargenVentas(Int64 pId, Int64 persona, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        MargenVentas pMargenVentas = new MargenVentas();

                        DbParameter pCOD_MARGEN = cmdTransaccionFactory.CreateParameter();
                        pCOD_MARGEN.ParameterName = "p_COD_MARGEN";
                        pCOD_MARGEN.Value = pId;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MARGEN);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_MARVE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pMargenVentas, "MARGENVENTAS", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MargenVentasData", "InsertarMargenVentas", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla MARGENVENTAS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla MARGENVENTAS</param>
        /// <returns>Entidad MargenVentas consultado</returns>
        public MargenVentas ConsultarMargenVentas(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            MargenVentas entidad = new MargenVentas();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MARGENVENTAS WHERE COD_MARGEN = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MARGEN"] != DBNull.Value) entidad.cod_margen = Convert.ToInt64(resultado["COD_MARGEN"]);
                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOPRODUCO"] != DBNull.Value) entidad.tipoproduco = Convert.ToString(resultado["TIPOPRODUCO"]);
                            if (resultado["NOMBREPRODUCTO"] != DBNull.Value) entidad.nombreproducto = Convert.ToString(resultado["NOMBREPRODUCTO"]);
                            if (resultado["UNIVENDIDA"] != DBNull.Value) entidad.univendida = Convert.ToInt64(resultado["UNIVENDIDA"]);
                            if (resultado["COSTOUNIDVEN"] != DBNull.Value) entidad.costounidven = Convert.ToInt64(resultado["COSTOUNIDVEN"]);
                            if (resultado["PRECIOUNIDVEN"] != DBNull.Value) entidad.preciounidven = Convert.ToInt64(resultado["PRECIOUNIDVEN"]);
                            if (resultado["COSTOVENTA"] != DBNull.Value) entidad.costoventa = Convert.ToInt64(resultado["COSTOVENTA"]);
                            if (resultado["VENTATOTAL"] != DBNull.Value) entidad.ventatotal = Convert.ToInt64(resultado["VENTATOTAL"]);
                            if (resultado["MARGEN"] != DBNull.Value) entidad.margen = Convert.ToInt64(resultado["MARGEN"]);
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
                        BOExcepcion.Throw("MargenVentasData", "ConsultarMargenVentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla MARGENVENTAS dados unos filtros
        /// </summary>
        /// <param name="pMARGENVENTAS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MargenVentas obtenidos</returns>
        public List<MargenVentas> ListarMargenVentas(MargenVentas pMargenVentas, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MargenVentas> lstMargenVentas = new List<MargenVentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  MARGENVENTAS where cod_ventas = (select cod_ventas from analisisventas where cod_persona = " + pMargenVentas.cod_persona + ")"; //Obtiene solo el margenventas del determinado codventas

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            MargenVentas entidad = new MargenVentas();

                            if (resultado["COD_MARGEN"] != DBNull.Value) entidad.cod_margen = Convert.ToInt64(resultado["COD_MARGEN"]);
                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOPRODUCO"] != DBNull.Value) entidad.tipoproduco = Convert.ToString(resultado["TIPOPRODUCO"]);
                            if (resultado["NOMBREPRODUCTO"] != DBNull.Value) entidad.nombreproducto = Convert.ToString(resultado["NOMBREPRODUCTO"]);
                            if (resultado["UNIVENDIDA"] != DBNull.Value) entidad.univendida = Convert.ToInt64(resultado["UNIVENDIDA"]);
                            if (resultado["COSTOUNIDVEN"] != DBNull.Value) entidad.costounidven = Convert.ToInt64(resultado["COSTOUNIDVEN"]);
                            if (resultado["PRECIOUNIDVEN"] != DBNull.Value) entidad.preciounidven = Convert.ToInt64(resultado["PRECIOUNIDVEN"]);
                            if (resultado["COSTOVENTA"] != DBNull.Value) entidad.costoventa = Convert.ToInt64(resultado["COSTOVENTA"]);
                            if (resultado["VENTATOTAL"] != DBNull.Value) entidad.ventatotal = Convert.ToInt64(resultado["VENTATOTAL"]);
                            if (resultado["MARGEN"] != DBNull.Value) entidad.margen = Convert.ToInt64(resultado["MARGEN"]);

                            lstMargenVentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMargenVentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MargenVentasData", "ListarMargenVentas", ex);
                        return null;
                    }
                }
            }
        }

        

    }
}