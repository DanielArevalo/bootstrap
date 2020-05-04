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
    public class TipoLiquidacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public TipoLiquidacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad TipoLiquidacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad TipoLiquidacion</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pTipoLiquidacion.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoLiquidacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "p_tipo_cuota";
                        ptipo_cuota.Value = pTipoLiquidacion.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        ptipo_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter ptipo_pago = cmdTransaccionFactory.CreateParameter();
                        ptipo_pago.ParameterName = "p_tipo_pago";
                        ptipo_pago.Value = pTipoLiquidacion.tipo_pago;
                        ptipo_pago.Direction = ParameterDirection.Input;
                        ptipo_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_pago);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pTipoLiquidacion.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptipo_intant = cmdTransaccionFactory.CreateParameter();
                        ptipo_intant.ParameterName = "p_tipo_intant";
                        ptipo_intant.Value = pTipoLiquidacion.tipo_intant;
                        ptipo_intant.Direction = ParameterDirection.Input;
                        ptipo_intant.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_intant);

                        DbParameter pvalor_gradiente = cmdTransaccionFactory.CreateParameter();
                        pvalor_gradiente.ParameterName = "p_valor_gradiente";
                        pvalor_gradiente.Value = pTipoLiquidacion.valor_gradiente;
                        pvalor_gradiente.Direction = ParameterDirection.Input;
                        pvalor_gradiente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_gradiente);

                        DbParameter ptip_gra = cmdTransaccionFactory.CreateParameter();
                        ptip_gra.ParameterName = "p_tip_gra";
                        ptip_gra.Value = pTipoLiquidacion.tip_gra;
                        ptip_gra.Direction = ParameterDirection.Input;
                        ptip_gra.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptip_gra);

                        DbParameter ptip_amo = cmdTransaccionFactory.CreateParameter();
                        ptip_amo.ParameterName = "p_tip_amo";
                        ptip_amo.Value = pTipoLiquidacion.tip_amo;
                        ptip_amo.Direction = ParameterDirection.Input;
                        ptip_amo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptip_amo);

                        DbParameter p_cob_intant_des = cmdTransaccionFactory.CreateParameter();
                        p_cob_intant_des.ParameterName = "p_cob_intant_des";
                        p_cob_intant_des.Value = pTipoLiquidacion.cob_intant_des;
                        p_cob_intant_des.Direction = ParameterDirection.Input;
                        p_cob_intant_des.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cob_intant_des);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOLIQUID_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoLiquidacion, "TIPOLIQUIDACION", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "CrearTipoLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad TipoLiquidacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad TipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pTipoLiquidacion.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoLiquidacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_cuota = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuota.ParameterName = "p_tipo_cuota";
                        ptipo_cuota.Value = pTipoLiquidacion.tipo_cuota;
                        ptipo_cuota.Direction = ParameterDirection.Input;
                        ptipo_cuota.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuota);

                        DbParameter ptipo_pago = cmdTransaccionFactory.CreateParameter();
                        ptipo_pago.ParameterName = "p_tipo_pago";
                        ptipo_pago.Value = pTipoLiquidacion.tipo_pago;
                        ptipo_pago.Direction = ParameterDirection.Input;
                        ptipo_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_pago);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pTipoLiquidacion.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptipo_intant = cmdTransaccionFactory.CreateParameter();
                        ptipo_intant.ParameterName = "p_tipo_intant";
                        ptipo_intant.Value = pTipoLiquidacion.tipo_intant;
                        ptipo_intant.Direction = ParameterDirection.Input;
                        ptipo_intant.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_intant);

                        DbParameter pvalor_gradiente = cmdTransaccionFactory.CreateParameter();
                        pvalor_gradiente.ParameterName = "p_valor_gradiente";
                        pvalor_gradiente.Value = pTipoLiquidacion.valor_gradiente;
                        pvalor_gradiente.Direction = ParameterDirection.Input;
                        pvalor_gradiente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_gradiente);

                        DbParameter ptip_gra = cmdTransaccionFactory.CreateParameter();
                        ptip_gra.ParameterName = "p_tip_gra";
                        ptip_gra.Value = pTipoLiquidacion.tip_gra;
                        ptip_gra.Direction = ParameterDirection.Input;
                        ptip_gra.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptip_gra);

                        DbParameter ptip_amo = cmdTransaccionFactory.CreateParameter();
                        ptip_amo.ParameterName = "p_tip_amo";
                        ptip_amo.Value = pTipoLiquidacion.tip_amo;
                        ptip_amo.Direction = ParameterDirection.Input;
                        ptip_amo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptip_amo);

                        DbParameter p_cob_intant_des = cmdTransaccionFactory.CreateParameter();
                        p_cob_intant_des.ParameterName = "p_cob_intant_des";
                        p_cob_intant_des.Value = pTipoLiquidacion.cob_intant_des;
                        p_cob_intant_des.Direction = ParameterDirection.Input;
                        p_cob_intant_des.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cob_intant_des);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOLIQUID_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoLiquidacion, "TIPOLIQUIDACION", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionaData", "ModificarTipoLiquidacion", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene la lista de TipoLiquidaciones
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoLiquidaciones obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TipoLiquidacion> lstTipoLiquidacion = new List<TipoLiquidacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT TipoLiquidacion.*, 
                                        Case TipoLiquidacion.tipo_cuota When 1 Then 'Pago Unico' When 2 Then 'Serie Uniforme' When 3 Then 'Gradiente' End As NomTipo_Cuota,
                                        Case TipoLiquidacion.tipo_pago When 1 Then 'Anticipado' When 2 Then 'Vencido' End As NomTipo_Pago,
                                        Case TipoLiquidacion.tipo_interes When 1 Then 'Simple' When 2 Then 'Compuesto' End As NomTipo_Interes,
                                        Case TipoLiquidacion.tipo_intant When 1 Then 'No Utiliza' When 2 Then 'Descuento del Desembolso' When 3 Then 'Lo Antes Posible' When 4 Then 'Todo Financiado' When 5 Then 'Financiado' End As NomTipo_IntAnt,
                                        Case TipoLiquidacion.tip_amo 
                                            When 1 Then 'Capital Fijo Interes Fijo' When 2 Then 'Capital Fijo Interes Variable' When 3 Then 'Capital Fijo Intere Fijo' When 4 Then 'Capital Variable Interes Variable' When 5 Then 'T.F.Prorrateados'When 6 Then 'Pago Unico' 
                                            When 7 Then 'Ajuste Periodico y Gradiente' When 8 Then 'Incremento Anual con Salario Minimo' When 9 Then 'Cuota Fija Crecimiento Periodico' When 10 Then 'Incremento Periodico' When 11 Then 'Gradiente sin capitalización de cuota' End As nomtip_amo,
                                        Case TipoLiquidacion.tip_gra When 1 Then 'No Utiliza' When 2 Then 'Escalonado' When 3 Then 'Geométrico' End As NomTip_Gra, TipoLiquidacion.cob_intant_des
                                        FROM TipoLiquidacion " + ObtenerFiltro(pTipoLiquidacion) + " ORDER BY TIPO_LIQUIDACION ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoLiquidacion entidad = new TipoLiquidacion();
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["NOMTIPO_CUOTA"] != DBNull.Value) entidad.nomtipo_cuota = Convert.ToString(resultado["NOMTIPO_CUOTA"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt32(resultado["TIPO_PAGO"]);
                            if (resultado["NOMTIPO_PAGO"] != DBNull.Value) entidad.nomtipo_pago = Convert.ToString(resultado["NOMTIPO_PAGO"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToInt32(resultado["TIPO_INTERES"]);
                            if (resultado["NOMTIPO_INTERES"] != DBNull.Value) entidad.nomtipo_interes = Convert.ToString(resultado["NOMTIPO_INTERES"]);
                            if (resultado["TIPO_INTANT"] != DBNull.Value) entidad.tipo_intant = Convert.ToInt32(resultado["TIPO_INTANT"]);
                            if (resultado["NOMTIPO_INTANT"] != DBNull.Value) entidad.nomtipo_intant = Convert.ToString(resultado["NOMTIPO_INTANT"]);
                            if (resultado["VALOR_GRADIENTE"] != DBNull.Value) entidad.valor_gradiente = Convert.ToDecimal(resultado["VALOR_GRADIENTE"]);
                            if (resultado["TIP_GRA"] != DBNull.Value) entidad.tip_gra = Convert.ToInt32(resultado["TIP_GRA"]);
                            if (resultado["NOMTIP_GRA"] != DBNull.Value) entidad.nomtip_gra = Convert.ToString(resultado["NOMTIP_GRA"]);
                            if (resultado["TIP_AMO"] != DBNull.Value) entidad.tip_amo = Convert.ToInt32(resultado["TIP_AMO"]);
                            if (resultado["NOMTIP_AMO"] != DBNull.Value) entidad.nomtip_amo = Convert.ToString(resultado["NOMTIP_AMO"]);
                            if (resultado["COB_INTANT_DES"] != DBNull.Value) entidad.cob_intant_des = Convert.ToInt32(resultado["COB_INTANT_DES"]);
                            lstTipoLiquidacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoLiquidacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "ListarTipoLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un TipoLiquidacion de datos
        /// </summary>
        /// <param name="pId">identificador del TipoLiquidacion</param>
        public void EliminarTipoLiquidacion(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoLiquidacion pTipoLiquidacion = new TipoLiquidacion();
                        DbParameter ptipo_liquidacion = cmdTransaccionFactory.CreateParameter();
                        ptipo_liquidacion.ParameterName = "p_tipo_liquidacion";
                        ptipo_liquidacion.Value = pTipoLiquidacion.tipo_liquidacion;
                        ptipo_liquidacion.Direction = ParameterDirection.Input;
                        ptipo_liquidacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_liquidacion);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_FAB_TIPOLIQUIDA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la informacion de un TipoLiquidacion
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Datos del TipoLiquidacion</returns>
        public TipoLiquidacion ConsultarTipoLiquidacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoLiquidacion entidad = new TipoLiquidacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TipoLiquidacion WHERE TIPO_LIQUIDACION = " + pId.ToString();
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.tipo_liquidacion = Convert.ToInt32(resultado["TIPO_LIQUIDACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CUOTA"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["TIPO_CUOTA"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt32(resultado["TIPO_PAGO"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToInt32(resultado["TIPO_INTERES"]);
                            if (resultado["TIPO_INTANT"] != DBNull.Value) entidad.tipo_intant = Convert.ToInt32(resultado["TIPO_INTANT"]);
                            if (resultado["VALOR_GRADIENTE"] != DBNull.Value) entidad.valor_gradiente = Convert.ToDecimal(resultado["VALOR_GRADIENTE"]);
                            if (resultado["TIP_GRA"] != DBNull.Value) entidad.tip_gra = Convert.ToInt32(resultado["TIP_GRA"]);
                            if (resultado["TIP_AMO"] != DBNull.Value) entidad.tip_amo = Convert.ToInt32(resultado["TIP_AMO"]);
                            if (resultado["COB_INTANT_DES"] != DBNull.Value) entidad.cob_intant_des = Convert.ToInt32(resultado["COB_INTANT_DES"]);
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
                        BOExcepcion.Throw("TipoLiquidacionData", "ConsultarTipoLiquidacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta el ultimo Id_TipoLiquidacion en la base de datos
        /// </summary>
        /// <param name="pEntidad">IdTipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public object UltimoIdTipoLiquidacion(Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select Tipo_Liquidacion from TipoLiquidacion order by Tipo_Liquidacion desc";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        return cmdTransaccionFactory.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoLiquidacionData", "UltimoIdTipoLiquidacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}
