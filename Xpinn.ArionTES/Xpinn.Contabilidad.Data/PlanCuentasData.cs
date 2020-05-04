using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using Xpinn.NIIF.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentass
    /// </summary>    
    public class PlanCuentasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public PlanCuentasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Consultar un PlanCuentas
        /// </summary>
        /// <param name="pnum_comp"></param>
        /// <param name="ptipo_comp"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public PlanCuentas ConsultarPlanCuentas(String pcod_cuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            PlanCuentas entidad = new PlanCuentas();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.*, ofc.cod_auxiliar, ofc.cod_auxiliar_contra
                                        FROM v_plan_cuentas p 
                                        LEFT JOIN OFICINA_CUENTA_CONTABLE ofc on p.cod_cuenta = ofc.cod_cuenta 
                                        WHERE p.cod_cuenta = '" + pcod_cuenta + "' ORDER BY p.cod_cuenta ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["MANEJA_GIR"] != DBNull.Value) entidad.maneja_gir = Convert.ToInt64(resultado["MANEJA_GIR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE_NIIF"] != DBNull.Value) entidad.nombre_niif= Convert.ToString(resultado["NOMBRE_NIIF"]);
                            if (resultado["DEPENDE_DE_NIIF"] != DBNull.Value) entidad.depende_de_niif = Convert.ToString(resultado["DEPENDE_DE_NIIF"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["CORRIENTE"] != DBNull.Value) entidad.corriente = Convert.ToInt32(resultado["CORRIENTE"]);
                            if (resultado["NOCORRIENTE"] != DBNull.Value) entidad.nocorriente = Convert.ToInt32(resultado["NOCORRIENTE"]);
                            if (resultado["TIPO_DISTRIBUCION"] != DBNull.Value) entidad.tipo_distribucion = Convert.ToInt32(resultado["TIPO_DISTRIBUCION"]);
                            if (resultado["PORCENTAJE_DISTRIBUCION"] != DBNull.Value) entidad.porcentaje_distribucion = Convert.ToDecimal(resultado["PORCENTAJE_DISTRIBUCION"]);
                            if (resultado["VALOR_DISTRIBUCION"] != DBNull.Value) entidad.valor_distribucion = Convert.ToDecimal(resultado["VALOR_DISTRIBUCION"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);else entidad.cod_tipo_impuesto = -1;
                            if (resultado["NUM_IMPUESTO"] != DBNull.Value) entidad.num_impuesto = Convert.ToInt32(resultado["NUM_IMPUESTO"]);
                            if (resultado["ES_IMPUESTO"] != DBNull.Value) entidad.es_impuesto = Convert.ToString(resultado["ES_IMPUESTO"]);
                            if (resultado["cod_auxiliar"] != DBNull.Value) entidad.cod_cuenta_centro_costo = Convert.ToString(resultado["cod_auxiliar"]);
                            if (resultado["cod_auxiliar_contra"] != DBNull.Value) entidad.cod_cuenta_contrapartida = Convert.ToString(resultado["cod_auxiliar_contra"]);
                            if (resultado["MANEJA_TRASLADO"] != DBNull.Value) entidad.maneja_traslado = Convert.ToInt64(resultado["MANEJA_TRASLADO"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ConsultarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerficarAuxiliar(string Cod_Cuenta,Usuario pUsuario)
        {
            DbDataReader resultado;
            bool entidad;
            Int64 saldo = 0;
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select x.depende_de from plan_cuentas x where x.depende_de=" + Cod_Cuenta;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            entidad = true;
                        }
                        else
                        {
                            sql = @"select saldocuentacontable(to_date('" + DateTime.Now.ToShortDateString() + "','"+conf.ObtenerFormatoFecha()+"')," + Cod_Cuenta + ") as saldo from dual";
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            if (resultado.Read())
                            {
                                if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToInt64(resultado["saldo"]);
                            }
                            if (saldo == 0)
                            {
                                entidad = true;
                            }
                            else
                            {
                                entidad = false;
                            }
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "VerficarAuxiliar", ex);
                        return false;
                    }
                }
            }
        }
        public PlanCuentas CrearCuentaContableOficina(PlanCuentas pPlanCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidofcuenta = cmdTransaccionFactory.CreateParameter();
                        pidofcuenta.ParameterName = "p_idofcuenta";
                        pidofcuenta.Value = pPlanCuentas.idofcuenta;
                        pidofcuenta.Direction = ParameterDirection.Output;
                        pidofcuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidofcuenta);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pPlanCuentas.tipo_producto == 0)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pPlanCuentas.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPlanCuentas.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pPlanCuentas.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_auxiliar = cmdTransaccionFactory.CreateParameter();
                        pcod_auxiliar.ParameterName = "p_cod_auxiliar";
                        if (pPlanCuentas.cod_cuenta_centro_costo == null)
                            pcod_auxiliar.Value = DBNull.Value;
                        else
                            pcod_auxiliar.Value = pPlanCuentas.cod_cuenta_centro_costo;
                        pcod_auxiliar.Direction = ParameterDirection.Input;
                        pcod_auxiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_auxiliar);

                        DbParameter pcod_auxiliar_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_auxiliar_contra.ParameterName = "p_cod_auxiliar_contra";
                        if (pPlanCuentas.cod_cuenta_contrapartida == null)
                            pcod_auxiliar_contra.Value = DBNull.Value;
                        else
                            pcod_auxiliar_contra.Value = pPlanCuentas.cod_cuenta_contrapartida;
                        pcod_auxiliar_contra.Direction = ParameterDirection.Input;
                        pcod_auxiliar_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_auxiliar_contra);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPlanCuentas.cod_est_det == 0)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPlanCuentas.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_OFICINA_CU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPlanCuentas.idofcuenta = pidofcuenta.Value != null ? Convert.ToInt32(pidofcuenta.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return pPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "CrearPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }


        public int ConsultarIDCuentaContableOficina(String pcod_cuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            int idOfCuenta = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select IDOFCUENTA from OFICINA_CUENTA_CONTABLE where cod_cuenta = '" + pcod_cuenta + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDOFCUENTA"] != DBNull.Value) idOfCuenta = Convert.ToInt32(resultado["IDOFCUENTA"]);
                        }

                        return idOfCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ConsultarCuentaContableOficina", ex);
                        return 0;
                    }
                }
            }
        }


        public PlanCuentas ModificarCuentaContableOficina(PlanCuentas pPlanCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idofcuenta = cmdTransaccionFactory.CreateParameter();
                        p_idofcuenta.ParameterName = "p_idofcuenta";
                        p_idofcuenta.Value = pPlanCuentas.idofcuenta;
                        p_idofcuenta.Direction = ParameterDirection.Input;
                        p_idofcuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_idofcuenta);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pPlanCuentas.tipo_producto == 0)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pPlanCuentas.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPlanCuentas.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pPlanCuentas.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_auxiliar = cmdTransaccionFactory.CreateParameter();
                        pcod_auxiliar.ParameterName = "p_cod_auxiliar";
                        if (pPlanCuentas.cod_cuenta_centro_costo == null)
                            pcod_auxiliar.Value = DBNull.Value;
                        else
                            pcod_auxiliar.Value = pPlanCuentas.cod_cuenta_centro_costo;
                        pcod_auxiliar.Direction = ParameterDirection.Input;
                        pcod_auxiliar.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_auxiliar);

                        DbParameter pcod_auxiliar_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_auxiliar_contra.ParameterName = "p_cod_auxiliar_contra";
                        if (pPlanCuentas.cod_cuenta_contrapartida == null)
                            pcod_auxiliar_contra.Value = DBNull.Value;
                        else
                            pcod_auxiliar_contra.Value = pPlanCuentas.cod_cuenta_contrapartida;
                        pcod_auxiliar_contra.Direction = ParameterDirection.Input;
                        pcod_auxiliar_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_auxiliar_contra);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPlanCuentas.cod_est_det == 0)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPlanCuentas.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_OFICINA_CU_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ModificarCuentaContableOficina", ex);
                        return null;
                    }
                }
            }
        }

        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(String pcod_cuenta, Usuario pUsuario)
        {
            DbDataReader resultado;
            PlanCuentasNIIF entidad = new PlanCuentasNIIF();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from Plan_Cuentas_Niif where Cod_Cuenta_Niif = '" + pcod_cuenta + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);                            
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ConsultarPlanCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Traer listado de PlanCuentass
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<PlanCuentas> ListarPlanCuentasLocal(PlanCuentas pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas); 
                        if (string.Equals(filtro, "AUXILIARES"))
                        {
                            sql = @"Select plan_cuentas.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = plan_cuentas.cod_moneda) As moneda 
                                    from plan_cuentas " + filtroClase + " And plan_cuentas.cod_cuenta Not In (Select x.depende_de From plan_cuentas x Where x.depende_de = plan_cuentas.cod_cuenta) Order by plan_cuentas.cod_cuenta";
                        }
                        else
                        {
                            string antes = (filtro.Trim() == "" ? "" : (filtroClase.ToLower().Contains("where") ? " And " : " Where "));
                            filtro = antes + filtro;
                            sql = @"Select plan_cuentas.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = plan_cuentas.cod_moneda) As moneda 
                                    from plan_cuentas " + filtroClase + filtro + " Order by plan_cuentas.cod_cuenta";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

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
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.codigo_nombre = Convert.ToString(resultado["COD_CUENTA"]) + "-" + Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }


        public List<PlanCuentas> ListarPlanCuentasNif(PlanCuentas pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas);
                        if (string.Equals(filtroClase, ""))
                            filtroClase = " Where 1 = 1 ";

                        sql = @"Select plan_cuentas_niif.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = plan_cuentas_niif.cod_moneda) As moneda, 
                                p.cod_cuenta As cod_cuenta_local, p.nombre As nombre_local
                                From plan_cuentas_niif Left Join plan_cuentas p On p.cod_cuenta = plan_cuentas_niif.cod_cuenta 
                                " + filtroClase + filtro + " And plan_cuentas_niif.cod_cuenta_niif Is not Null " + 
                              " Order by plan_cuentas_niif.cod_cuenta_niif";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

                            if (resultado["COD_CUENTA_LOCAL"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA_LOCAL"]);
                            if (resultado["NOMBRE_LOCAL"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE_LOCAL"]);
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
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_niif = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de_niif = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["REPORTARMAYOR"] != DBNull.Value) entidad.reportarmayor = Convert.ToInt32(resultado["REPORTARMAYOR"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentas> ListarPlanCuentasAmbos(PlanCuentas pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas, "plan_cuentas.");
                        if (string.Equals(filtroClase, ""))
                            filtroClase = " Where 1 = 1 ";

                        sql = @"Select plan_cuentas.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = plan_cuentas.cod_moneda) As moneda,
                                plan_cuentas_niif.cod_cuenta_niif, plan_cuentas_niif.nombre As nombre_niif, plan_cuentas_niif.depende_de As depende_de_niif
                                From plan_cuentas Inner Join plan_cuentas_niif On plan_cuentas.cod_cuenta = plan_cuentas_niif.cod_cuenta " + filtroClase + filtro + 
                              " Order by plan_cuentas.cod_cuenta";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

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
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.codigo_nombre = Convert.ToString(resultado["COD_CUENTA"]) + "-" + Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE_NIIF"] != DBNull.Value) entidad.nombre_niif = Convert.ToString(resultado["NOMBRE_NIIF"]);
                            if (resultado["DEPENDE_DE_NIIF"] != DBNull.Value) entidad.depende_de_niif = Convert.ToString(resultado["DEPENDE_DE_NIIF"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas pPlanCuentas, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        string filtroClase = ObtenerFiltro(pPlanCuentas, "v_plan_cuentas.");
                        if (string.Equals(filtroClase, ""))
                            filtroClase = " Where 1 = 1 ";

                        sql = @"Select v_plan_cuentas.*, (Select t.descripcion From tipomoneda t Where t.cod_moneda = v_plan_cuentas.cod_moneda) As moneda,
                                v_plan_cuentas.cod_cuenta_niif, v_plan_cuentas.nombre_niif, v_plan_cuentas.depende_de_niif
                                From v_plan_cuentas " + filtroClase + filtro +
                              " Order by v_plan_cuentas.cod_cuenta";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

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
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.codigo_nombre = Convert.ToString(resultado["COD_CUENTA"]) + "-" + Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE_NIIF"] != DBNull.Value) entidad.nombre_niif = Convert.ToString(resultado["NOMBRE_NIIF"]);
                            if (resultado["DEPENDE_DE_NIIF"] != DBNull.Value) entidad.depende_de_niif = Convert.ToString(resultado["DEPENDE_DE_NIIF"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        
        /// <summary>
        /// Traer listado de PlanCuentas
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<PlanCuentas> ListarPlanCuentasxterc(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        sql = "Select * From plan_cuentas Where maneja_ter = 1 Order by plan_cuentas.cod_cuenta";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarPlanCuentasxterc", ex);
                        return null;
                    }
                }
            }
        }


        public PlanCuentas CrearPlanCuentas(PlanCuentas pPlanCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        pcod_cuenta.Value = pPlanCuentas.cod_cuenta;
                        pcod_cuenta.DbType = DbType.String;
                        pcod_cuenta.Direction = ParameterDirection.Input;

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.DbType = DbType.String;
                        pnombre.Value = pPlanCuentas.nombre;

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.DbType = DbType.String;
                        ptipo.Value = pPlanCuentas.tipo;

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "pnivel";
                        pnivel.DbType = DbType.Int64;
                        pnivel.Value = pPlanCuentas.nivel;

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "pdepende_de";
                        pnivel.DbType = DbType.String;
                        pdepende_de.Value = pPlanCuentas.depende_de;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcod_moneda";
                        pnivel.DbType = DbType.Int64;
                        pcod_moneda.Value = pPlanCuentas.cod_moneda;

                        DbParameter pmaneja_cc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_cc.ParameterName = "pmaneja_cc";
                        pmaneja_cc.DbType = DbType.Int64;
                        pmaneja_cc.Value = pPlanCuentas.maneja_cc;

                        DbParameter pmaneja_ter = cmdTransaccionFactory.CreateParameter();
                        pmaneja_ter.ParameterName = "pmaneja_ter";
                        pmaneja_ter.DbType = DbType.Int64;
                        pmaneja_ter.Value = pPlanCuentas.maneja_ter;

                        DbParameter pmaneja_sc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sc.ParameterName = "pmaneja_sc";
                        pmaneja_sc.DbType = DbType.Int64;
                        pmaneja_sc.Value = pPlanCuentas.maneja_sc;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.DbType = DbType.Int64;
                        pestado.Value = pPlanCuentas.estado;

                        DbParameter pimpuesto = cmdTransaccionFactory.CreateParameter();
                        pimpuesto.ParameterName = "pimpuesto";
                        pimpuesto.DbType = DbType.Int64;
                        pimpuesto.Value = pPlanCuentas.impuesto;

                        DbParameter pmaneja_gir = cmdTransaccionFactory.CreateParameter();
                        pmaneja_gir.ParameterName = "pmaneja_gir";
                        pmaneja_gir.DbType = DbType.Int64;
                        pmaneja_gir.Value = pPlanCuentas.maneja_gir;

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "pbase_minima";
                        pbase_minima.DbType = DbType.Decimal;
                        if (pPlanCuentas.base_minima == null)
                            pbase_minima.Value = DBNull.Value;
                        else
                            pbase_minima.Value = pPlanCuentas.base_minima;

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "pporcentaje_impuesto";
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        if (pPlanCuentas.porcentaje_impuesto == null)
                            pporcentaje_impuesto.Value = DBNull.Value;
                        else
                            pporcentaje_impuesto.Value = pPlanCuentas.porcentaje_impuesto;

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "pcod_cuenta_niif";
                        pcod_cuenta_niif.DbType = DbType.String;
                        if (pPlanCuentas.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pPlanCuentas.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;

                        DbParameter pnombre_niif = cmdTransaccionFactory.CreateParameter();
                        pnombre_niif.ParameterName = "pnombre_niif";
                        pnombre_niif.DbType = DbType.String;
                        if (pPlanCuentas.nombre_niif == null)
                            pnombre_niif.Value = DBNull.Value;
                        else
                            pnombre_niif.Value = pPlanCuentas.nombre_niif;
                        pnombre_niif.Direction = ParameterDirection.Input;

                        DbParameter pdepende_de_niif = cmdTransaccionFactory.CreateParameter();
                        pdepende_de_niif.ParameterName = "pdepende_de_niif";
                        pdepende_de_niif.DbType = DbType.String;
                        if (pPlanCuentas.depende_de_niif == null)
                            pdepende_de_niif.Value = DBNull.Value;
                        else
                            pdepende_de_niif.Value = pPlanCuentas.depende_de_niif;
                        pdepende_de_niif.Direction = ParameterDirection.Input;


                        DbParameter pcorriente = cmdTransaccionFactory.CreateParameter();
                        pcorriente.ParameterName = "p_corriente";
                        pcorriente.Value = pPlanCuentas.corriente;
                        pcorriente.Direction = ParameterDirection.Input;
                        pcorriente.DbType = DbType.Int32;


                        DbParameter pnocorriente = cmdTransaccionFactory.CreateParameter();
                        pnocorriente.ParameterName = "p_nocorriente";
                        pnocorriente.Value = pPlanCuentas.nocorriente;
                        pnocorriente.Direction = ParameterDirection.Input;
                        pnocorriente.DbType = DbType.Int32;


                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "p_tipo_distribucion";
                        if (pPlanCuentas.tipo_distribucion != -1)
                            ptipo_distribucion.Value = pPlanCuentas.tipo_distribucion;
                        else
                            ptipo_distribucion.Value = DBNull.Value;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;


                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        if (pPlanCuentas.porcentaje_distribucion != -1)
                            pporcentaje_distribucion.Value = pPlanCuentas.porcentaje_distribucion;
                        else
                            pporcentaje_distribucion.Value = DBNull.Value;
                        pporcentaje_distribucion.Direction = ParameterDirection.Input;
                        pporcentaje_distribucion.DbType = DbType.Decimal;

                        DbParameter pvalor_distribucion = cmdTransaccionFactory.CreateParameter();
                        pvalor_distribucion.ParameterName = "p_valor_distribucion";
                        if (pPlanCuentas.valor_distribucion != -1)
                            pvalor_distribucion.Value = pPlanCuentas.valor_distribucion;
                        else
                            pvalor_distribucion.Value = DBNull.Value;
                        pvalor_distribucion.Direction = ParameterDirection.Input;
                        pvalor_distribucion.DbType = DbType.Decimal;

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        if (pPlanCuentas.cod_tipo_impuesto != -1) pcod_tipo_impuesto.Value = pPlanCuentas.cod_tipo_impuesto; else pcod_tipo_impuesto.Value = DBNull.Value;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;

                        DbParameter pmaneja_traslado = cmdTransaccionFactory.CreateParameter();
                        pmaneja_traslado.ParameterName = "p_maneja_traslado";
                        pmaneja_traslado.Value = pPlanCuentas.maneja_traslado;
                        pmaneja_traslado.Direction = ParameterDirection.Input;
                        pmaneja_traslado.DbType = DbType.Int64;


                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pnombre);
                        cmdTransaccionFactory.Parameters.Add(ptipo);
                        cmdTransaccionFactory.Parameters.Add(pnivel);
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_cc);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_ter);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sc);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pimpuesto);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_gir);
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);
                        cmdTransaccionFactory.Parameters.Add(pnombre_niif);
                        cmdTransaccionFactory.Parameters.Add(pdepende_de_niif);
                        cmdTransaccionFactory.Parameters.Add(pcorriente);
                        cmdTransaccionFactory.Parameters.Add(pnocorriente);
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pvalor_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_traslado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLANCUE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPlanCuentas, "PLAN_CUENTAS", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "CrearPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }



        public PlanCuentas ModificarPlanCuentas(PlanCuentas pPlanCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        pcod_cuenta.Value = pPlanCuentas.cod_cuenta;
                        pcod_cuenta.DbType = DbType.String;
                        pcod_cuenta.Direction = ParameterDirection.Input;

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        pnombre.DbType = DbType.String;
                        pnombre.Value = pPlanCuentas.nombre;

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.DbType = DbType.String;
                        ptipo.Value = pPlanCuentas.tipo;

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "pnivel";
                        pnivel.DbType = DbType.Int64;
                        pnivel.Value = pPlanCuentas.nivel;

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "pdepende_de";
                        pnivel.DbType = DbType.String;
                        pdepende_de.Value = pPlanCuentas.depende_de;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcod_moneda";
                        pnivel.DbType = DbType.Int64;
                        pcod_moneda.Value = pPlanCuentas.cod_moneda;

                        DbParameter pmaneja_cc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_cc.ParameterName = "pmaneja_cc";
                        pmaneja_cc.DbType = DbType.Int64;
                        pmaneja_cc.Value = pPlanCuentas.maneja_cc;

                        DbParameter pmaneja_ter = cmdTransaccionFactory.CreateParameter();
                        pmaneja_ter.ParameterName = "pmaneja_ter";
                        pmaneja_ter.DbType = DbType.Int64;
                        pmaneja_ter.Value = pPlanCuentas.maneja_ter;

                        DbParameter pmaneja_sc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sc.ParameterName = "pmaneja_sc";
                        pmaneja_sc.DbType = DbType.Int64;
                        pmaneja_sc.Value = pPlanCuentas.maneja_sc;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.DbType = DbType.Int64;
                        pestado.Value = pPlanCuentas.estado;

                        DbParameter pimpuesto = cmdTransaccionFactory.CreateParameter();
                        pimpuesto.ParameterName = "pimpuesto";
                        pimpuesto.DbType = DbType.Int64;
                        pimpuesto.Value = pPlanCuentas.impuesto;

                        DbParameter pmaneja_gir = cmdTransaccionFactory.CreateParameter();
                        pmaneja_gir.ParameterName = "pmaneja_gir";
                        pmaneja_gir.DbType = DbType.Int64;
                        pmaneja_gir.Value = pPlanCuentas.maneja_gir;

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "pbase_minima";
                        pbase_minima.DbType = DbType.Decimal;
                        if (pPlanCuentas.base_minima == null)
                            pbase_minima.Value = DBNull.Value;
                        else
                            pbase_minima.Value = pPlanCuentas.base_minima;

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "pporcentaje_impuesto";
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        if (pPlanCuentas.porcentaje_impuesto == null)
                            pporcentaje_impuesto.Value = DBNull.Value;
                        else
                            pporcentaje_impuesto.Value = pPlanCuentas.porcentaje_impuesto;

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "pcod_cuenta_niif";
                        pcod_cuenta_niif.DbType = DbType.String;
                        if (pPlanCuentas.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pPlanCuentas.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;

                        DbParameter pnombre_niif = cmdTransaccionFactory.CreateParameter();
                        pnombre_niif.ParameterName = "pnombre_niif";
                        pnombre_niif.DbType = DbType.String;
                        if (pPlanCuentas.nombre_niif == null)
                            pnombre_niif.Value = DBNull.Value;
                        else
                            pnombre_niif.Value = pPlanCuentas.nombre_niif;
                        pnombre_niif.Direction = ParameterDirection.Input;

                        DbParameter pdepende_de_niif = cmdTransaccionFactory.CreateParameter();
                        pdepende_de_niif.ParameterName = "pdepende_de_niif";
                        pdepende_de_niif.DbType = DbType.String;
                        if (pPlanCuentas.depende_de_niif == null)
                            pdepende_de_niif.Value = DBNull.Value;
                        else
                            pdepende_de_niif.Value = pPlanCuentas.depende_de_niif;
                        pdepende_de_niif.Direction = ParameterDirection.Input;


                        DbParameter pcorriente = cmdTransaccionFactory.CreateParameter();
                        pcorriente.ParameterName = "p_corriente";
                        pcorriente.Value = pPlanCuentas.corriente;
                        pcorriente.Direction = ParameterDirection.Input;
                        pcorriente.DbType = DbType.Int32;


                        DbParameter pnocorriente = cmdTransaccionFactory.CreateParameter();
                        pnocorriente.ParameterName = "p_nocorriente";
                        pnocorriente.Value = pPlanCuentas.nocorriente;
                        pnocorriente.Direction = ParameterDirection.Input;
                        pnocorriente.DbType = DbType.Int32;


                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "p_tipo_distribucion";
                        if (pPlanCuentas.tipo_distribucion != -1)
                            ptipo_distribucion.Value = pPlanCuentas.tipo_distribucion;
                        else
                            ptipo_distribucion.Value = DBNull.Value;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;


                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        if (pPlanCuentas.porcentaje_distribucion != -1)
                            pporcentaje_distribucion.Value = pPlanCuentas.porcentaje_distribucion;
                        else
                            pporcentaje_distribucion.Value = DBNull.Value;
                        pporcentaje_distribucion.Direction = ParameterDirection.Input;
                        pporcentaje_distribucion.DbType = DbType.Decimal;

                        DbParameter pvalor_distribucion = cmdTransaccionFactory.CreateParameter();
                        pvalor_distribucion.ParameterName = "p_valor_distribucion";
                        if (pPlanCuentas.valor_distribucion != -1)
                            pvalor_distribucion.Value = pPlanCuentas.valor_distribucion;
                        else
                            pvalor_distribucion.Value = DBNull.Value;
                        pvalor_distribucion.Direction = ParameterDirection.Input;
                        pvalor_distribucion.DbType = DbType.Decimal;

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        if (pPlanCuentas.cod_tipo_impuesto != -1) pcod_tipo_impuesto.Value = pPlanCuentas.cod_tipo_impuesto; else pcod_tipo_impuesto.Value = DBNull.Value;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;

                        DbParameter pmaneja_traslado = cmdTransaccionFactory.CreateParameter();
                        pmaneja_traslado.ParameterName = "p_maneja_traslado";
                        pmaneja_traslado.Value = pPlanCuentas.maneja_traslado;
                        pmaneja_traslado.Direction = ParameterDirection.Input;
                        pmaneja_traslado.DbType = DbType.Int64;


                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pnombre);
                        cmdTransaccionFactory.Parameters.Add(ptipo);
                        cmdTransaccionFactory.Parameters.Add(pnivel);
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_cc);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_ter);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sc);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pimpuesto);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_gir);
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);
                        cmdTransaccionFactory.Parameters.Add(pnombre_niif);
                        cmdTransaccionFactory.Parameters.Add(pdepende_de_niif);
                        cmdTransaccionFactory.Parameters.Add(pcorriente);
                        cmdTransaccionFactory.Parameters.Add(pnocorriente);
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pvalor_distribucion);
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);
                        cmdTransaccionFactory.Parameters.Add(pmaneja_traslado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLANCUE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPlanCuentas, "PLAN_CUENTAS", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ModificarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarPlanCuentas(string pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PlanCuentas pPlanCuentas = new PlanCuentas();
                        pPlanCuentas = ConsultarPlanCuentas(pId, vUsuario);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPlanCuentas.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLANCUE_ELIMI";
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

        public List<PlanCuentas> ListarTipoImpuesto(PlanCuentas pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstTipoImpuestos = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from Tipoimpuesto order by Cod_Tipo_Impuesto";
                        


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nombre_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            
                            lstTipoImpuestos.Add(entidad);
                        }

                        return lstTipoImpuestos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarTipoImpuesto", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentas> ListarCuentasTraslado(string filtro, DateTime pFecha, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstCuentasTraslado = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = @"Select Cod_Cuenta, Nombre, Case Tipo When 'D' THEN 'Débito' When 'C' Then 'Crédito' Else Null END AS Tipo, Saldocuentacontable(To_Date('" + pFecha.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + @"'),Cod_Cuenta) As Saldo
                                        From Plan_Cuentas Where Maneja_Traslado = 1 ";
                        if (filtro != "")
                            sql += filtro+ " Order by Cod_Cuenta Desc ";
                        connection.Open();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);

                            if (entidad.saldo > 0)
                                lstCuentasTraslado.Add(entidad);
                        }

                        return lstCuentasTraslado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasData", "ListarCuentasTraslado", ex);
                        return null;
                    }
                }
            }
        }
        public bool EsPlanCuentasNIIF(Usuario pUsuario)
        {
            int cantidad = 0;
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select Count(*) As NIIF From par_cue_lincred Where cod_atr = 2 And tipo = 0 And cod_cuenta Like '14%' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NIIF"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (cantidad > 0)
                            return true;
                        else
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

    }
}
