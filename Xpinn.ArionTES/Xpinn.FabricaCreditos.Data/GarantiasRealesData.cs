using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class GarantiasRealesData : GlobalData 
    {  
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public GarantiasRealesData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Cuentas_Garantias 
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cuentas_Garantias  obtenidos</returns>
        public List<GarantiasReales> ListarGarantiasReales(GarantiasReales pGarantias, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GarantiasReales> lstGarantiasReales = new List<GarantiasReales>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select consecutivo, nombre, tipogarantia,ctadebito,ctacredito from cuentas_garantias " + ObtenerFiltro(pGarantias) + "  order by consecutivo";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pGarantias = new GarantiasReales();
                            //Asociar todos los valores a la entidad
                            if (resultado["consecutivo"] != DBNull.Value) pGarantias.consecutivo = Convert.ToInt16(resultado["consecutivo"]);
                            if (resultado["nombre"] != DBNull.Value) pGarantias.Nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipogarantia"] != DBNull.Value) pGarantias.TipoGarantia = Convert.ToString(resultado["tipogarantia"]);
                            if (resultado["ctacredito"] != DBNull.Value) pGarantias.ctacredito = Convert.ToString(resultado["ctacredito"]);
                            if (resultado["ctadebito"] != DBNull.Value) pGarantias.ctadebito = Convert.ToString(resultado["ctadebito"]);
                            lstGarantiasReales.Add(pGarantias);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGarantiasReales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasRealesData", "ListaTGarantiasReales", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea una entidad Cuentas_Garantias  en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cuentas_Garantias </param>
        /// <returns>Entidad creada</returns>
        public GarantiasReales InsertarGarantiasReales(GarantiasReales pCuentasgarantias, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_linea = cmdTransaccionFactory.CreateParameter();
                        p_linea.ParameterName = "p_linea";
                        p_linea.Value = pCuentasgarantias.Nombre;
                        p_linea.DbType = DbType.String;
                        p_linea.Size = 80;
                        p_linea.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_garantia = cmdTransaccionFactory.CreateParameter();
                        p_tipo_garantia.ParameterName = "p_tipo_garantia";
                        p_tipo_garantia.Value = pCuentasgarantias.TipoGarantia;
                        p_tipo_garantia.DbType = DbType.String;
                        p_tipo_garantia.Size = 80;
                        p_tipo_garantia.Direction = ParameterDirection.Input;

                        DbParameter p_ctacredito = cmdTransaccionFactory.CreateParameter();
                        p_ctacredito.ParameterName = "p_ctacredito";
                        p_ctacredito.Value = pCuentasgarantias.ctacredito;
                        p_ctacredito.DbType = DbType.String;
                        p_ctacredito.Size = 80;
                        p_ctacredito.Direction = ParameterDirection.Input;

                        DbParameter p_ctadebito= cmdTransaccionFactory.CreateParameter();
                        p_ctadebito.ParameterName = "p_ctadebito";
                        p_ctadebito.Value = pCuentasgarantias.ctadebito;
                        p_ctadebito.DbType = DbType.String;
                        p_ctadebito.Size = 80;
                        p_ctadebito.Direction = ParameterDirection.Input;

                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Output;
                       
                        cmdTransaccionFactory.Parameters.Add(p_linea);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_garantia);                       
                        cmdTransaccionFactory.Parameters.Add(p_ctacredito);
                        cmdTransaccionFactory.Parameters.Add(p_ctadebito);
                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);
                                               
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GAR_REALES_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCuentasgarantias.consecutivo = Convert.ToInt64(p_consecutivo.Value);
                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.codigo = Convert.ToString(p_codigo.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCuentasgarantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasRealesData", "InsertarGarantiasReales", ex);
                        return null;
                    }

                }
                
            }
        }

        /// <summary>
        /// Crea una entidad Cuentas_Garantias  en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cuentas_Garantias </param>
        /// <returns>Entidad modificada</returns>
        public GarantiasReales ModificarGarantiasReales(GarantiasReales pCuentasgarantias, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pCuentasgarantias.consecutivo;
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 50;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        DbParameter p_linea = cmdTransaccionFactory.CreateParameter();
                        p_linea.ParameterName = "p_linea";
                        p_linea.Value = pCuentasgarantias.Nombre;
                        p_linea.DbType = DbType.String;
                        p_linea.Size = 80;
                        p_linea.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_garantia = cmdTransaccionFactory.CreateParameter();
                        p_tipo_garantia.ParameterName = "p_tipo_garantia";
                        p_tipo_garantia.Value = pCuentasgarantias.TipoGarantia;
                        p_tipo_garantia.DbType = DbType.String;
                        p_tipo_garantia.Size = 80;
                        p_tipo_garantia.Direction = ParameterDirection.Input;

                        DbParameter p_ctacredito = cmdTransaccionFactory.CreateParameter();
                        p_ctacredito.ParameterName = "p_ctacredito";
                        p_ctacredito.Value = pCuentasgarantias.ctacredito;
                        p_ctacredito.DbType = DbType.String;
                        p_ctacredito.Size = 80;
                        p_ctacredito.Direction = ParameterDirection.Input;

                        DbParameter p_ctadebito = cmdTransaccionFactory.CreateParameter();
                        p_ctadebito.ParameterName = "p_ctadebito";
                        p_ctadebito.Value = pCuentasgarantias.ctadebito;
                        p_ctadebito.DbType = DbType.String;
                        p_ctadebito.Size = 80;
                        p_ctadebito.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo); 
                        cmdTransaccionFactory.Parameters.Add(p_linea);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_garantia);
                        cmdTransaccionFactory.Parameters.Add(p_ctacredito);
                        cmdTransaccionFactory.Parameters.Add(p_ctadebito);
                      
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GAR_REALES_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                       
                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.codigo = Convert.ToString(p_codigo.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCuentasgarantias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasRealesData", "ModificarGarantiasReales", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Cuentas_Garantias para registro de cuentas contables de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Cuentas_Garantias  consultada</returns>
        public GarantiasReales ConsultarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            GarantiasReales entidad = new GarantiasReales();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select Nombre,tipogarantia,ctadebito,ctacredito from garantiasreales where codigo =" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.TipoGarantia = Convert.ToString(resultado["tipo"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["Nombre"]);
                                                    if (resultado["ctacredito"] != DBNull.Value) entidad.ctacredito = Convert.ToString(resultado["ctacredito"]);
                            if (resultado["ctadebito"] != DBNull.Value) entidad.ctadebito = Convert.ToString(resultado["ctadebito"]);
                           

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasRealesData", "ConsultarGarantiasReales", ex);
                        return null;
                    }

                }
            }
        }


        /// <summary>
        /// Elimina una Cuentas_Garantias  en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la Cuentas_Garantias </param>
        public void EliminarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        GarantiasReales pEntidad = new GarantiasReales();

                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pId;
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GAR_REALES_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //     if (pUsuario.programaGeneraLog)
                        //  DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.codigo), "GARANTIA",Accion.Eliminar.ToString(),connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GarantiasRealesData", "EliminarGarantiasReales", ex);
                    }
                }

            }
        }


    }
}