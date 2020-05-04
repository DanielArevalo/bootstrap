using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla BalanceNIIFS
    /// </summary>
    public class BalanceNIIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla BalanceNIIFS
        /// </summary>
        public BalanceNIIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla BalanceNIIFS de la base de datos
        /// </summary>
        /// <param name="pBalanceNIIF">Entidad BalanceNIIF</param>
        /// <returns>Entidad BalanceNIIF creada</returns>
        public BalanceNIIF CrearBalanceNIIF(BalanceNIIF pBalanceNIIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pBalanceNIIF.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pBalanceNIIF.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pBalanceNIIF.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter ptipo_moneda = cmdTransaccionFactory.CreateParameter();
                        ptipo_moneda.ParameterName = "p_tipo_moneda";
                        if (pBalanceNIIF.tipo_moneda == null)
                            ptipo_moneda.Value = DBNull.Value;
                        else
                            ptipo_moneda.Value = pBalanceNIIF.tipo_moneda;
                        ptipo_moneda.Direction = ParameterDirection.Input;
                        ptipo_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_moneda);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pBalanceNIIF.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pBalanceNIIF.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pBalanceNIIF.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_BALANCE_NI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBalanceNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "CrearBalanceNIIF", ex);
                        return null;
                    }
                }
            }
        }

        public string VerificarComprobantesYCuentasNIIF(DateTime fechaCorte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = fechaCorte;
                        pFecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
                        p_mensajeerror.ParameterName = "pMensajeError";
                        p_mensajeerror.Value = DBNull.Value;

                        // No quitar, molesta si lo quitas
                        p_mensajeerror.Size = 8000;
                        p_mensajeerror.DbType = DbType.String;
                        p_mensajeerror.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_VERIFIC_CUENIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        string error = p_mensajeerror.Value != DBNull.Value ? p_mensajeerror.Value.ToString() : string.Empty;

                        return error;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "VerificarComprobantesYCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla BalanceNIIFS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad BalanceNIIF modificada</returns>
        public BalanceNIIF ModificarBalanceNIIF(BalanceNIIF pBalanceNIIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {  
                        DbParameter pcodOrigen_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcodOrigen_cuenta_niif.ParameterName = "p_codOrigen_cuenta_niif";
                        pcodOrigen_cuenta_niif.Value = pBalanceNIIF.cod_cuentaOrigen_niif;
                        pcodOrigen_cuenta_niif.Direction = ParameterDirection.Input;
                        pcodOrigen_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodOrigen_cuenta_niif);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pBalanceNIIF.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        
                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pBalanceNIIF.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_BALANCENIIF_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pBalanceNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ModificarBalanceNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla BalanceNIIFS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de BalanceNIIFS</param>
        public void EliminarBalance_NIIF(DateTime pFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_BALANCE_NIIF_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "EliminarBalance_NIIF", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla BalanceNIIFS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla BalanceNIIFS</param>
        /// <returns>Entidad BalanceNIIF consultado</returns>
        public BalanceNIIF ConsultarBalanceNIIF(BalanceNIIF pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            BalanceNIIF entidad = new BalanceNIIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Balance_NIIF " + ObtenerFiltro(pEntidad);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["TIPO_MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToInt32(resultado["TIPO_MONEDA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
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
                        BOExcepcion.Throw("BalanceNIIFData", "ConsultarBalanceNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla BalanceNIIF dados unos filtros
        /// </summary>
        /// <param name="pBalanceNIIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BalanceNIIFs obtenidos</returns>
        public List<BalanceNIIF> ListarBalance_NIIF(DateTime pFecha, Usuario vUsuario) //BalanceNIIF pBalance_NIIF,
        {

            Configuracion conf = new Configuracion();
            DbDataReader resultado;
            List<BalanceNIIF> lstBalance_NIIF = new List<BalanceNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_Balance_Niif " ;
                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " fecha = To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " fecha = '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " order by cod_cuenta_niif ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            BalanceNIIF entidad = new BalanceNIIF();
                            //PlanCuentasNIIF enti= new PlanCuentasNIIF();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombre_tercero = Convert.ToString(resultado["NOMBRE_TERCERO"]);
                            if (resultado["SALDO_COLGAAP"] != DBNull.Value) entidad.saldo_colgaap = Convert.ToDecimal(resultado["SALDO_COLGAAP"]);
                            if (resultado["AJUSTE"] != DBNull.Value) entidad.ajuste = Convert.ToInt32(resultado["AJUSTE"]);
                            if (resultado["RECLASIFICACION"] != DBNull.Value) entidad.reclasificacion = Convert.ToInt32(resultado["RECLASIFICACION"]);
                            if (resultado["SALDO_IFRS"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO_IFRS"]);
                            lstBalance_NIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBalance_NIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "ListarBalance_NIIF", ex);
                        return null;
                    }
                }
            }
        }



        public Boolean GenerarBalance_NIIF(DateTime pFecha,ref int opcion,ref string pError, Usuario vUsuario)
        {
            opcion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pFecha;  
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_BALANCE_GENERAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        opcion = 1;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        
                        pError = ex.Message;
                        //BOExcepcion.Throw("Balance_NIIFData", "GenerarBalance_NIIF", ex);
                        return false;
                    }
                }
            }
        }




        public void ReclasificarBalanceNIIF(string pCodOrigen, string pCodDestino, Decimal pValor, int pTipoAjuste, string pObservacion, DateTime pfechafiltro, Int64 pCentroCosto, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodOrigen_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcodOrigen_cuenta_niif.ParameterName = "p_codOrigen_cuenta_niif";
                        pcodOrigen_cuenta_niif.Value = pCodOrigen;
                                                
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";                       
                        if (pCodDestino == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pCodDestino;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Value = pfechafiltro;

                        DbParameter p_centro_costo = cmdTransaccionFactory.CreateParameter();
                        p_centro_costo.ParameterName = "p_centro_costo";
                        p_centro_costo.DbType = DbType.Int64;
                        p_centro_costo.Value = pCentroCosto;

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.DbType = DbType.Decimal;
                        psaldo.Value = pValor;

                        DbParameter p_tipo_ajuste = cmdTransaccionFactory.CreateParameter();
                        p_tipo_ajuste.ParameterName = "p_tipo_ajuste";
                        p_tipo_ajuste.DbType = DbType.Int32;
                        p_tipo_ajuste.Value = pTipoAjuste;

                        DbParameter p_observaciones = cmdTransaccionFactory.CreateParameter();
                        p_observaciones.ParameterName = "p_observaciones";
                        p_observaciones.DbType = DbType.String;
                        p_observaciones.Value = pObservacion;

                        cmdTransaccionFactory.Parameters.Add(pcodOrigen_cuenta_niif);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);
                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(p_centro_costo);
                        cmdTransaccionFactory.Parameters.Add(psaldo);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_ajuste);
                        cmdTransaccionFactory.Parameters.Add(p_observaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_RECLASIFICAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                    }
                }
            }
        }


        public List<PlanCuentasNIIF> ListaPlan_Cuentas(PlanCuentasNIIF pPlanCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCuentasNIIF> lstPlan_cuenta = new List<PlanCuentasNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select cod_cuenta_niif,cod_cuenta_niif || ' - ' || nombre as nombre from plan_cuentas_niif order by cod_cuenta_niif asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanCuentasNIIF entidad = new PlanCuentasNIIF();                            
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstPlan_cuenta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlan_cuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "ListarPlanCuentas_NIIF", ex);
                        return null;
                    }
                }
            }
        }


        public void ModificarFechaNIIF(DateTime pfechafiltro,int tipo, Usuario vUsuario)
        {
           
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Value = pfechafiltro;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.DbType = DbType.Int32;
                        ptipo.Value = tipo;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_BALANCEFECHA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "MODIFICARFECHANIIF", ex);

                    }
                }
            }
        }


        public void EliminarFechaBalanceGeneradoNIIF(DateTime pFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_FECHA_F_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "EliminarFechaBalanceGeneradoNIIF", ex);
                    }
                }
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasLocal(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentasNIIF> lstPlanCuentas = new List<PlanCuentasNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas);
                        string antes = (filtro.Trim() == "" ? "" : (filtroClase.ToLower().Contains("where") ? " And " : " Where "));
                        filtro = antes + filtro;
                        sql = @"Select plan_cuentas.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = plan_cuentas.cod_moneda) As moneda 
                                from plan_cuentas " + filtroClase + filtro + " Order by plan_cuentas.cod_cuenta";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentasNIIF entidad = new PlanCuentasNIIF();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["MANEJA_GIR"] != DBNull.Value) entidad.maneja_gir = Convert.ToInt64(resultado["MANEJA_GIR"]);                            
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "ListarPlanCuentasLocal", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentasNIIF> lstPlanCuentas = new List<PlanCuentasNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas);
                        string antes = (filtro.Trim() == "" ? "" : (filtroClase.ToLower().Contains("where") ? " And " : " Where "));
                        filtro = antes + filtro;
                        sql = @"SELECT P.*,PH.COD_CUENTA AS COD_CUENTA_NEW, (SELECT T.DESCRIPCION FROM TIPOMONEDA T WHERE T.COD_MONEDA = P.COD_MONEDA) AS MONEDA
                                FROM PLAN_CUENTAS_NIIF P LEFT JOIN PLAN_CUENTAS_HOMOLOGA PH ON PH.COD_CUENTA_NIIF = P.COD_CUENTA_NIIF "
                                + filtro + " ORDER BY P.COD_CUENTA_NIIF";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentasNIIF entidad = new PlanCuentasNIIF();

                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["MANEJA_GIR"] != DBNull.Value) entidad.maneja_gir = Convert.ToInt64(resultado["MANEJA_GIR"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["COD_CUENTA_NEW"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA_NEW"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);

                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt32(resultado["CORRIENTE"]);
                            if (resultado["NOCORRIENTE"] != DBNull.Value) entidad.nocorriente = Convert.ToInt32(resultado["NOCORRIENTE"]);
                            if (resultado["TIPO_DISTRIBUCION"] != DBNull.Value) entidad.tipo_distribucion = Convert.ToInt32(resultado["TIPO_DISTRIBUCION"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["VALOR_DISTRIBUCION"] != DBNull.Value) entidad.valor_distribucion = Convert.ToDecimal(resultado["VALOR_DISTRIBUCION"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "ListarPlanCuentasLocal", ex);
                        return null;
                    }
                }
            }
        }


        public PlanCuentasNIIF CrearPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentasNIIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pPlanCuentasNIIF.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pPlanCuentasNIIF.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPlanCuentasNIIF.tipo == "")
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pPlanCuentasNIIF.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        if (pPlanCuentasNIIF.nivel == null)
                            pnivel.Value = DBNull.Value;
                        else
                            pnivel.Value = pPlanCuentasNIIF.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "p_depende_de";
                        if (pPlanCuentasNIIF.depende_de == null || pPlanCuentasNIIF.depende_de == "")
                            pdepende_de.Value = DBNull.Value;
                        else
                            pdepende_de.Value = pPlanCuentasNIIF.depende_de;
                        pdepende_de.Direction = ParameterDirection.Input;
                        pdepende_de.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (pPlanCuentasNIIF.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pPlanCuentasNIIF.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pmaneja_cc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_cc.ParameterName = "p_maneja_cc";
                        if (pPlanCuentasNIIF.maneja_cc == null)
                            pmaneja_cc.Value = DBNull.Value;
                        else
                            pmaneja_cc.Value = pPlanCuentasNIIF.maneja_cc;
                        pmaneja_cc.Direction = ParameterDirection.Input;
                        pmaneja_cc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_cc);

                        DbParameter pmaneja_ter = cmdTransaccionFactory.CreateParameter();
                        pmaneja_ter.ParameterName = "p_maneja_ter";
                        if (pPlanCuentasNIIF.maneja_ter == null)
                            pmaneja_ter.Value = DBNull.Value;
                        else
                            pmaneja_ter.Value = pPlanCuentasNIIF.maneja_ter;
                        pmaneja_ter.Direction = ParameterDirection.Input;
                        pmaneja_ter.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_ter);

                        DbParameter pmaneja_sc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sc.ParameterName = "p_maneja_sc";
                        if (pPlanCuentasNIIF.maneja_sc == null)
                            pmaneja_sc.Value = DBNull.Value;
                        else
                            pmaneja_sc.Value = pPlanCuentasNIIF.maneja_sc;
                        pmaneja_sc.Direction = ParameterDirection.Input;
                        pmaneja_sc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sc);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pPlanCuentasNIIF.estado == null)
                            pestado.Value = "0";
                        else
                            pestado.Value = pPlanCuentasNIIF.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pimpuesto = cmdTransaccionFactory.CreateParameter();
                        pimpuesto.ParameterName = "p_impuesto";
                        if (pPlanCuentasNIIF.impuesto == null)
                            pimpuesto.Value = DBNull.Value;
                        else
                            pimpuesto.Value = pPlanCuentasNIIF.impuesto;
                        pimpuesto.Direction = ParameterDirection.Input;
                        pimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pimpuesto);

                        DbParameter pmaneja_gir = cmdTransaccionFactory.CreateParameter();
                        pmaneja_gir.ParameterName = "p_maneja_gir";
                        if (pPlanCuentasNIIF.maneja_gir == null)
                            pmaneja_gir.Value = DBNull.Value;
                        else
                            pmaneja_gir.Value = pPlanCuentasNIIF.maneja_gir;
                        pmaneja_gir.Direction = ParameterDirection.Input;
                        pmaneja_gir.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_gir);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        if (pPlanCuentasNIIF.porcentaje_impuesto == null)
                            pporcentaje_impuesto.Value = DBNull.Value;
                        else
                            pporcentaje_impuesto.Value = pPlanCuentasNIIF.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPlanCuentasNIIF.cod_cuenta == null || pPlanCuentasNIIF.cod_cuenta == "")
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pPlanCuentasNIIF.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pPlanCuentasNIIF.base_minima == null)
                            pbase_minima.Value = DBNull.Value;
                        else
                            pbase_minima.Value = pPlanCuentasNIIF.base_minima;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcorriente = cmdTransaccionFactory.CreateParameter();
                        pcorriente.ParameterName = "p_corriente";
                        //if (pPlanCuentasNIIF.corriente == null)
                        //    pcorriente.Value = DBNull.Value;
                        //else
                            pcorriente.Value = pPlanCuentasNIIF.corriente;
                        pcorriente.Direction = ParameterDirection.Input;
                        pcorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcorriente);

                        DbParameter pnocorriente = cmdTransaccionFactory.CreateParameter();
                        pnocorriente.ParameterName = "p_nocorriente";
                        //if (pPlanCuentasNIIF.nocorriente == null)
                        //    pnocorriente.Value = DBNull.Value;
                        //else
                            pnocorriente.Value = pPlanCuentasNIIF.nocorriente;
                        pnocorriente.Direction = ParameterDirection.Input;
                        pnocorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnocorriente);

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "p_tipo_distribucion";
                        //if (pPlanCuentasNIIF.tipo_distribucion == null)
                        //    ptipo_distribucion.Value = DBNull.Value;
                        //else
                            ptipo_distribucion.Value = pPlanCuentasNIIF.tipo_distribucion;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);

                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        //if (pPlanCuentasNIIF.porcentaje_distribucion == null)
                        //    pporcentaje_distribucion.Value = DBNull.Value;
                        //else
                            pporcentaje_distribucion.Value = pPlanCuentasNIIF.porcentaje_distribucion;
                        pporcentaje_distribucion.Direction = ParameterDirection.Input;
                        pporcentaje_distribucion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distribucion);

                        DbParameter pvalor_distribucion = cmdTransaccionFactory.CreateParameter();
                        pvalor_distribucion.ParameterName = "p_valor_distribucion";
                        if (pPlanCuentasNIIF.valor_distribucion != -1)
                            pvalor_distribucion.Value = pPlanCuentasNIIF.valor_distribucion;
                        else
                            pvalor_distribucion.Value = DBNull.Value;
                        pvalor_distribucion.Direction = ParameterDirection.Input;
                        pvalor_distribucion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_distribucion);

                        DbParameter preportarmayor = cmdTransaccionFactory.CreateParameter();
                        preportarmayor.ParameterName = "p_reportarmayor";
                        preportarmayor.Value = pPlanCuentasNIIF.estado;
                        preportarmayor.Direction = ParameterDirection.Input;
                        preportarmayor.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(preportarmayor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_PLAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanCuentasNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Balance_NIIFData", "CrearPlanCuentasNIIF. Cuenta:" + pPlanCuentasNIIF.cod_cuenta_niif, ex);
                        return null;
                    }
                }
            }
        }


        public void Eliminarniff(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PlanCuentasNIIF pniff = new PlanCuentasNIIF();
                       
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta";
                        if (pniff.cod_cuenta == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pniff.cod_cuenta;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_PLAN_ELIMINAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "Eliminarniff", ex);
                    }
                }
            }
        }


        public List<BalanceNIIF> Consultardatosdecierea(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<BalanceNIIF> lstentidad = new List<BalanceNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select fecha from cierea; ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            BalanceNIIF entidad = new BalanceNIIF();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstentidad.Add(entidad);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ConsultarBalanceNIIF", ex);
                        return null;
                    }
                }
            }
        }

        public List<BalanceNIIF> listarbalancereporteXBLR(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "P_FECHA";
                        pcod_cuenta_niif.Value = entidad.fecha;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "P_CENTRO_COSTO";
                        if (entidad.centro_costo == 0 || entidad.centro_costo < 0) pnombre.Value = DBNull.Value; else pnombre.Value = entidad.centro_costo;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "P_TIPO_MONEDA";
                        if (entidad.tipo_moneda == 0 || entidad.tipo_moneda == null) ptipo.Value = DBNull.Value; else ptipo.Value = entidad.tipo_moneda;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "P_NIVEL";
                        if (entidad.nivel == 0 || entidad.nivel <= 0) pnivel.Value = DBNull.Value; else pnivel.Value = entidad.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pmostrarceros = cmdTransaccionFactory.CreateParameter();
                        pmostrarceros.ParameterName = "P_MOSTRAR_CEROS";
                        pmostrarceros.Value = entidad.mostrar_ceros;
                        pmostrarceros.Direction = ParameterDirection.Input;
                        pmostrarceros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmostrarceros);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_REPORTE";
                        cmdTransaccionFactory.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNiifData", "ListarBalanceNiif", ex);
                        return null;
                    }
                }

            }
            DbDataReader resultado;
            List<BalanceNIIF> lstBalanceNiif = new List<BalanceNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM TEMP_BALANCE WHERE valor > 0 ORDER BY FECHA, COD_CUENTA, CENTRO_COSTO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            BalanceNIIF entidades = new BalanceNIIF();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidades.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidades.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidades.nombre = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidades.saldo = Convert.ToInt64(resultado["VALOR"]);

                            lstBalanceNiif.Add(entidades);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBalanceNiif;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNiifData", "ListarBalanceNiif", ex);
                        return null;
                    }
                }
            }
        }


        public List<BalanceNIIF> listarbalancereporteXBLRConceros(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "P_FECHA";
                        pcod_cuenta_niif.Value = entidad.fecha;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "P_CENTRO_COSTO";
                        if (entidad.centro_costo == 0 || entidad.centro_costo == null) pnombre.Value = DBNull.Value; else pnombre.Value = entidad.centro_costo;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "P_TIPO_MONEDA";
                        if (entidad.tipo_moneda == 0 || entidad.tipo_moneda == null) ptipo.Value = DBNull.Value; else ptipo.Value = entidad.tipo_moneda;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "P_NIVEL";
                        if (entidad.nivel == 0 || entidad.nivel == null) pnivel.Value = DBNull.Value; else pnivel.Value = entidad.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pmostrarceros = cmdTransaccionFactory.CreateParameter();
                        pmostrarceros.ParameterName = "P_MOSTRAR_CEROS";
                        pmostrarceros.Value = entidad.mostrar_ceros;
                        pmostrarceros.Direction = ParameterDirection.Input;
                        pmostrarceros.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmostrarceros);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_REPORTE";
                        cmdTransaccionFactory.ExecuteNonQuery();


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNiifData", "ListarBalanceNiif", ex);
                        return null;
                    }
                }

            }

            DbDataReader resultado;
            List<BalanceNIIF> lstBalanceNiif = new List<BalanceNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM TEMP_BALANCE ORDER BY 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            BalanceNIIF entidades = new BalanceNIIF();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidades.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidades.nombre = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidades.saldo = Convert.ToInt64(resultado["VALOR"]);

                            lstBalanceNiif.Add(entidades);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBalanceNiif;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNiifData", "ListarBalanceNiif", ex);
                        return null;
                    }
                }
            }
        }

        public List<BalanceNIIF> ListarBalance(BalanceNIIF pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceNIIF> lstBalPru = new List<BalanceNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pEntidad.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        if (pEntidad.centro_costo == null)
                            PCENINI.Value = DBNull.Value;
                        else
                            PCENINI.Value = pEntidad.centro_costo;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        if (pEntidad.centro_costo_fin == null)
                            PCENFIN.Value = DBNull.Value;
                        else
                            PCENFIN.Value = pEntidad.centro_costo_fin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PCOMPARTIVOCC = cmdTransaccionFactory.CreateParameter();
                        PCOMPARTIVOCC.ParameterName = "PCOMPARTIVOCC";
                        PCOMPARTIVOCC.Value = pEntidad.comparativo;
                        PCOMPARTIVOCC.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCOMPARTIVOCC);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pEntidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Input;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);


                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "pMoneda";
                        PMONEDA.Value = pEntidad.tipo_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);


                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_BALPRU";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "USP_XPINN_NIF_BALPRU", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_BALANCE Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceNIIF entidad = new BalanceNIIF();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt32(resultado["NIVEL"]);
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }


        public List<BalanceNIIF> ListarFechaCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceNIIF> lstFechaCierre = new List<BalanceNIIF>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha from Balance_NIIF order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceNIIF entidad = new BalanceNIIF();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime FechaUltimoCierre(Usuario vUsuario)
        {
            DateTime fecha = DateTime.MinValue;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Max(fecha) As fecha From Balance_NIIF ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }
                        return fecha;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "FechaUltimoCierre", ex);
                        return fecha;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 4100 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierremensualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierremensualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }

        public BalanceNIIF CrearCierremensual(BalanceNIIF pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "pFecha";
                        p_fecha.Value = pEntidad.fecha;
                        p_fecha.DbType = DbType.Date;
                        p_fecha.Direction = ParameterDirection.Input;

                        DbParameter pCentroCosto = cmdTransaccionFactory.CreateParameter();
                        pCentroCosto.ParameterName = "pCentroCosto";
                        pCentroCosto.Value = pEntidad.centro_costo;
                        pCentroCosto.DbType = DbType.Int64;
                        pCentroCosto.Direction = ParameterDirection.Input;

                        DbParameter pPorTercero = cmdTransaccionFactory.CreateParameter();
                        pPorTercero.ParameterName = "pPorTercero";
                        pPorTercero.Value = pEntidad.tipo;
                        pPorTercero.DbType = DbType.Int64;
                        pPorTercero.Direction = ParameterDirection.Input;

                        DbParameter pEstado = cmdTransaccionFactory.CreateParameter();
                        pEstado.ParameterName = "pEstado";
                        pEstado.Value = pEntidad.estado;
                        pEstado.DbType = DbType.String;
                        pEstado.Direction = ParameterDirection.Input;

                        DbParameter pUsua = cmdTransaccionFactory.CreateParameter();
                        pUsua.ParameterName = "pUsuario";
                        pUsua.Value = pUsuario.codusuario;
                        pUsua.DbType = DbType.Int64;
                        pUsua.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_fecha);
                        cmdTransaccionFactory.Parameters.Add(pCentroCosto);
                        cmdTransaccionFactory.Parameters.Add(pPorTercero);
                        cmdTransaccionFactory.Parameters.Add(pEstado);
                        cmdTransaccionFactory.Parameters.Add(pUsua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_CIERRECCOSTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "CrearCierremensual", ex);
                        return null;
                    }

                }
            }
        }


        public PlanCtasHomologacionNIF CrearPlanCtasHomologacion(PlanCtasHomologacionNIF pPlanCtasHomologacionNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidhomologa = cmdTransaccionFactory.CreateParameter();
                        pidhomologa.ParameterName = "p_idhomologa";
                        pidhomologa.Value = pPlanCtasHomologacionNIF.idhomologa;
                        pidhomologa.Direction = ParameterDirection.Output;
                        pidhomologa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidhomologa);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPlanCtasHomologacionNIF.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pPlanCtasHomologacionNIF.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_CTAHOMOLOG_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pPlanCtasHomologacionNIF.idhomologa = Convert.ToInt64(pidhomologa.Value);

                        return pPlanCtasHomologacionNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "CrearPlanCtasHomologacionNIF", ex);
                        return null;
                    }
                }
            }
        }

        public PlanCtasHomologacionNIF ModificarPlanCtasHomologacion(PlanCtasHomologacionNIF pPlanCtasHomologacionNIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidhomologa = cmdTransaccionFactory.CreateParameter();
                        pidhomologa.ParameterName = "p_idhomologa";
                        pidhomologa.Value = pPlanCtasHomologacionNIF.idhomologa;
                        pidhomologa.Direction = ParameterDirection.Input;
                        pidhomologa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidhomologa);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPlanCtasHomologacionNIF.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pPlanCtasHomologacionNIF.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_CTAHOMOLOG_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPlanCtasHomologacionNIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ModificarPlanCtasHomologacionNIF", ex);
                        return null;
                    }
                }
            }
        }


        #region METODOS DE BALANCE DE COMPROBACION

        public Xpinn.Contabilidad.Entities.BalancePrueba ConsultarBalanceMes13(Xpinn.Contabilidad.Entities.BalancePrueba pDatos, Usuario pUsuario, string pTipo = "O")
        {
            DbDataReader resultado = default(DbDataReader);
            Xpinn.Contabilidad.Entities.BalancePrueba entidad = new Xpinn.Contabilidad.Entities.BalancePrueba();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select Distinct fecha from cierea where tipo='O'and  estado='D' and fecha= To_Date('" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = "Select Distinct fecha from cierea where tipo='O'and  estado='D' and fecha= '" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ConsultarBalanceMes13Niif", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.Contabilidad.Entities.BalancePrueba> ListarBalanceComprobacionNiif(Xpinn.Contabilidad.Entities.BalancePrueba pEntidad, ref Double TotDeb, ref Double TotCre, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Contabilidad.Entities.BalancePrueba> lstBalPru = new List<Xpinn.Contabilidad.Entities.BalancePrueba>();
            TotDeb = 0;
            TotCre = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pEntidad.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        if (pEntidad.centro_costo == null)
                            PCENINI.Value = DBNull.Value;
                        else
                            PCENINI.Value = pEntidad.centro_costo;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        if (pEntidad.centro_costo_fin == null)
                            PCENFIN.Value = DBNull.Value;
                        else
                            PCENFIN.Value = pEntidad.centro_costo_fin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pEntidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Input;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALCOMPROBACNIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBalanceComprobacionNiif", "USP_XPINN_CON_BALCOMPROBACNIIF", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";
                        else
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Contabilidad.Entities.BalancePrueba entidad = new Xpinn.Contabilidad.Entities.BalancePrueba();
                            double saldo_inicial_debito = 0, saldo_inicial_credito = 0;
                            double saldo_final_debito = 0, saldo_final_credito = 0;

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            //if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["SALDO_INICIAL_DEBITO"] != DBNull.Value) saldo_inicial_debito = Convert.ToDouble(resultado["SALDO_INICIAL_DEBITO"]);
                            if (resultado["SALDO_INICIAL_CREDITO"] != DBNull.Value) saldo_inicial_credito = Convert.ToDouble(resultado["SALDO_INICIAL_CREDITO"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debitos = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.creditos = Convert.ToDouble(resultado["CREDITO"]);
                            if (resultado["SALDO_FINAL_DEBITO"] != DBNull.Value) saldo_final_debito = Convert.ToDouble(resultado["SALDO_FINAL_DEBITO"]);
                            if (resultado["SALDO_FINAL_CREDITO"] != DBNull.Value) saldo_final_credito = Convert.ToDouble(resultado["SALDO_FINAL_CREDITO"]);
                            if (entidad.tipo == "D")
                            {
                                entidad.saldo_inicial = saldo_inicial_debito - saldo_inicial_credito;
                                entidad.saldo_final = saldo_final_debito - saldo_final_credito;
                            }
                            else
                            {
                                entidad.saldo_inicial = saldo_inicial_credito - saldo_inicial_debito;
                                entidad.saldo_final = saldo_final_credito - saldo_final_debito;
                            }
                            lstBalPru.Add(entidad);
                        }

                        string sqltot = "";
                        sqltot = "Select Sum(debito) As debito, Sum(credito) As credito from TEMP_LIBROMAYOR Where nivel=1 and fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') ";


                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DEBITO"] != DBNull.Value) TotDeb = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) TotCre = Convert.ToDouble(resultado["CREDITO"]);
                        }

                        Double utilidad = 0;

                        sqltot = @"Select Nvl(Calcular_Utilidad_Niif(t.fecha, " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0) -
                                    Nvl(Calcular_Utilidad_Niif((Select Max(c.fecha) From cierea c Where tipo = 'G' And c.fecha < t.fecha), " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0) As Utilidad  
                                    From TEMP_LIBROMAYOR t Where t.cod_cuenta = '3' And t.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by t.fecha";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["UTILIDAD"] != DBNull.Value) utilidad = Convert.ToDouble(resultado["UTILIDAD"]);
                            TotCre = TotCre - utilidad;
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceNIIFData", "ListarBalanceComprobacionNiif", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

        public BalanceNIIF ConsultarBalanceMes13(BalanceNIIF pDatos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalanceNIIF entidad = new BalanceNIIF();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select Distinct fecha from cierea where tipo='N'and  estado='D' and fecha= To_Date('" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = "Select Distinct fecha from cierea where tipo='N'and  estado='D' and fecha= '" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ConsultarBalance", ex);
                        return null;
                    }
                }
            }
        }


        public List<BalancePrueba> ListarBalanceComprobacionTerNiif(BalancePrueba pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstBalPru = new List<BalancePrueba>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pEntidad.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        if (pEntidad.centro_costo == null)
                            PCENINI.Value = DBNull.Value;
                        else
                            PCENINI.Value = pEntidad.centro_costo;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        if (pEntidad.centro_costo_fin == int.MinValue)
                            PCENFIN.Value = DBNull.Value;
                        else
                            PCENFIN.Value = pEntidad.centro_costo_fin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pEntidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Input;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_BALCOMPROBATER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBalanceComprobacion", "USP_XPINN_NIIF_BALCOMPROBATER", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta, essaldocuenta desc, cod_tercero";
                        else
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta, essaldocuenta desc, cod_tercero";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalancePrueba entidad = new BalancePrueba();
                            double saldo_inicial_debito = 0, saldo_inicial_credito = 0;
                            double saldo_final_debito = 0, saldo_final_credito = 0;

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            //if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["SALDO_INICIAL_DEBITO"] != DBNull.Value) saldo_inicial_debito = Convert.ToDouble(resultado["SALDO_INICIAL_DEBITO"]);
                            if (resultado["SALDO_INICIAL_CREDITO"] != DBNull.Value) saldo_inicial_credito = Convert.ToDouble(resultado["SALDO_INICIAL_CREDITO"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debitos = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.creditos = Convert.ToDouble(resultado["CREDITO"]);
                            if (resultado["SALDO_FINAL_DEBITO"] != DBNull.Value) saldo_final_debito = Convert.ToDouble(resultado["SALDO_FINAL_DEBITO"]);
                            if (resultado["SALDO_FINAL_CREDITO"] != DBNull.Value) saldo_final_credito = Convert.ToDouble(resultado["SALDO_FINAL_CREDITO"]);
                            if (entidad.tipo == "D")
                            {
                                entidad.saldo_inicial = saldo_inicial_debito - saldo_inicial_credito;
                                entidad.saldo_final = saldo_final_debito - saldo_final_credito;
                            }
                            else
                            {
                                entidad.saldo_inicial = saldo_inicial_credito - saldo_inicial_debito;
                                entidad.saldo_final = saldo_final_credito - saldo_final_debito;
                            }
                            if (resultado["IDEN_TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["IDEN_TERCERO"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nombreTercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarBalanceComprobacionTer", ex);
                        return null;
                    }
                }
            }


        }

    }
}