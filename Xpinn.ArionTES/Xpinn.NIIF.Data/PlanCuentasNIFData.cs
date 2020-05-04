using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PlanCuentasNIIFS
    /// </summary>
    public class PlanCuentasNIIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PlanCuentasNIIFS
        /// </summary>
        public PlanCuentasNIIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla PlanCuentasNIIFS de la base de datos
        /// </summary>
        /// <param name="pPlanCuentasNIIF">Entidad PlanCuentasNIIF</param>
        /// <returns>Entidad PlanCuentasNIIF creada</returns>        
        public Xpinn.Contabilidad.Entities.PlanCuentas CrearPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF, Usuario vUsuario)
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
                        pnombre.Value = pPlanCuentasNIIF.nombre_niif;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pPlanCuentasNIIF.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        pnivel.Value = pPlanCuentasNIIF.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "p_depende_de";
                        pdepende_de.Value = pPlanCuentasNIIF.depende_de_niif;
                        pdepende_de.Direction = ParameterDirection.Input;
                        pdepende_de.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pPlanCuentasNIIF.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pmaneja_cc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_cc.ParameterName = "p_maneja_cc";
                        pmaneja_cc.Value = pPlanCuentasNIIF.maneja_cc;
                        pmaneja_cc.Direction = ParameterDirection.Input;
                        pmaneja_cc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_cc);

                        DbParameter pmaneja_ter = cmdTransaccionFactory.CreateParameter();
                        pmaneja_ter.ParameterName = "p_maneja_ter";
                        pmaneja_ter.Value = pPlanCuentasNIIF.maneja_ter;
                        pmaneja_ter.Direction = ParameterDirection.Input;
                        pmaneja_ter.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_ter);

                        DbParameter pmaneja_sc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sc.ParameterName = "p_maneja_sc";
                        pmaneja_sc.Value = pPlanCuentasNIIF.maneja_sc;
                        pmaneja_sc.Direction = ParameterDirection.Input;
                        pmaneja_sc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sc);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPlanCuentasNIIF.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pimpuesto = cmdTransaccionFactory.CreateParameter();
                        pimpuesto.ParameterName = "p_impuesto";
                        pimpuesto.Value = pPlanCuentasNIIF.impuesto;
                        pimpuesto.Direction = ParameterDirection.Input;
                        pimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pimpuesto);

                        DbParameter pmaneja_gir = cmdTransaccionFactory.CreateParameter();
                        pmaneja_gir.ParameterName = "p_maneja_gir";
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
                        if (pPlanCuentasNIIF.cod_cuenta == null)
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
                        pcorriente.Value = pPlanCuentasNIIF.corriente;
                        pcorriente.Direction = ParameterDirection.Input;
                        pcorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcorriente);

                        DbParameter pnocorriente = cmdTransaccionFactory.CreateParameter();
                        pnocorriente.ParameterName = "p_nocorriente";
                        pnocorriente.Value = pPlanCuentasNIIF.nocorriente;
                        pnocorriente.Direction = ParameterDirection.Input;
                        pnocorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnocorriente);

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "p_tipo_distribucion";
                        if (pPlanCuentasNIIF.tipo_distribucion != -1)
                            ptipo_distribucion.Value = pPlanCuentasNIIF.tipo_distribucion;
                        else
                            ptipo_distribucion.Value = DBNull.Value;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);

                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        if (pPlanCuentasNIIF.porcentaje_distribucion != -1)
                            pporcentaje_distribucion.Value = pPlanCuentasNIIF.porcentaje_distribucion;
                        else
                            pporcentaje_distribucion.Value = DBNull.Value;
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
                        preportarmayor.Value = pPlanCuentasNIIF.estado == null ? 1 : pPlanCuentasNIIF.estado;
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
                        BOExcepcion.Throw("PlanCuentasNIIFData", "CrearPlanCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla PlanCuentasNIIFS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad PlanCuentasNIIF modificada</returns>
        public Xpinn.Contabilidad.Entities.PlanCuentas ModificarPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF, Usuario vUsuario)
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
                        pnombre.Value = pPlanCuentasNIIF.nombre_niif;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pPlanCuentasNIIF.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnivel = cmdTransaccionFactory.CreateParameter();
                        pnivel.ParameterName = "p_nivel";
                        pnivel.Value = pPlanCuentasNIIF.nivel;
                        pnivel.Direction = ParameterDirection.Input;
                        pnivel.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnivel);

                        DbParameter pdepende_de = cmdTransaccionFactory.CreateParameter();
                        pdepende_de.ParameterName = "p_depende_de";
                        pdepende_de.Value = pPlanCuentasNIIF.depende_de_niif;
                        pdepende_de.Direction = ParameterDirection.Input;
                        pdepende_de.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdepende_de);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pPlanCuentasNIIF.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pmaneja_cc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_cc.ParameterName = "p_maneja_cc";
                        pmaneja_cc.Value = pPlanCuentasNIIF.maneja_cc;
                        pmaneja_cc.Direction = ParameterDirection.Input;
                        pmaneja_cc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_cc);

                        DbParameter pmaneja_ter = cmdTransaccionFactory.CreateParameter();
                        pmaneja_ter.ParameterName = "p_maneja_ter";
                        pmaneja_ter.Value = pPlanCuentasNIIF.maneja_ter;
                        pmaneja_ter.Direction = ParameterDirection.Input;
                        pmaneja_ter.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_ter);

                        DbParameter pmaneja_sc = cmdTransaccionFactory.CreateParameter();
                        pmaneja_sc.ParameterName = "p_maneja_sc";
                        pmaneja_sc.Value = pPlanCuentasNIIF.maneja_sc;
                        pmaneja_sc.Direction = ParameterDirection.Input;
                        pmaneja_sc.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_sc);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pPlanCuentasNIIF.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pimpuesto = cmdTransaccionFactory.CreateParameter();
                        pimpuesto.ParameterName = "p_impuesto";
                        pimpuesto.Value = pPlanCuentasNIIF.impuesto;
                        pimpuesto.Direction = ParameterDirection.Input;
                        pimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pimpuesto);

                        DbParameter pmaneja_gir = cmdTransaccionFactory.CreateParameter();
                        pmaneja_gir.ParameterName = "p_maneja_gir";
                        pmaneja_gir.Value = pPlanCuentasNIIF.maneja_gir;
                        pmaneja_gir.Direction = ParameterDirection.Input;
                        pmaneja_gir.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_gir);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        if (pPlanCuentasNIIF.porcentaje_impuesto != null) pporcentaje_impuesto.Value = pPlanCuentasNIIF.porcentaje_impuesto; else pporcentaje_impuesto.Value = DBNull.Value;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPlanCuentasNIIF.cod_cuenta != "" && pPlanCuentasNIIF.cod_cuenta != null) pcod_cuenta.Value = pPlanCuentasNIIF.cod_cuenta; else pcod_cuenta.Value = DBNull.Value;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pPlanCuentasNIIF.base_minima != null) pbase_minima.Value = pPlanCuentasNIIF.base_minima; else pbase_minima.Value = DBNull.Value;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcorriente = cmdTransaccionFactory.CreateParameter();
                        pcorriente.ParameterName = "p_corriente";
                        pcorriente.Value = pPlanCuentasNIIF.corriente;
                        pcorriente.Direction = ParameterDirection.Input;
                        pcorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcorriente);

                        DbParameter pnocorriente = cmdTransaccionFactory.CreateParameter();
                        pnocorriente.ParameterName = "p_nocorriente";
                        pnocorriente.Value = pPlanCuentasNIIF.nocorriente;
                        pnocorriente.Direction = ParameterDirection.Input;
                        pnocorriente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnocorriente);

                        DbParameter ptipo_distribucion = cmdTransaccionFactory.CreateParameter();
                        ptipo_distribucion.ParameterName = "p_tipo_distribucion";
                        if (pPlanCuentasNIIF.tipo_distribucion != -1)
                            ptipo_distribucion.Value = pPlanCuentasNIIF.tipo_distribucion;
                        else
                            ptipo_distribucion.Value = DBNull.Value;
                        ptipo_distribucion.Direction = ParameterDirection.Input;
                        ptipo_distribucion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_distribucion);

                        DbParameter pporcentaje_distribucion = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_distribucion.ParameterName = "p_porcentaje_distribucion";
                        if (pPlanCuentasNIIF.porcentaje_distribucion != -1)
                            pporcentaje_distribucion.Value = pPlanCuentasNIIF.porcentaje_distribucion;
                        else
                            pporcentaje_distribucion.Value = DBNull.Value;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_PLAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPlanCuentasNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ModificarPlanCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PlanCuentasNIIFS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PlanCuentasNIIFS</param>
        public void EliminarHomologacionNIIFLocal(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidhomologa = cmdTransaccionFactory.CreateParameter();
                        pidhomologa.ParameterName = "p_idhomologa";
                        pidhomologa.Value = pId;
                        pidhomologa.Direction = ParameterDirection.Input;
                        pidhomologa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidhomologa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_PLANHOMOLOGAELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "EliminarHomologacionNIIF", ex);
                    }
                }
            }
        }


        public void EliminarPlanCuentasNIIF(string pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        pcod_cuenta_niif.Value = pId;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIIF_PLANNIIF_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "EliminarPlanCuentasNIIF", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PlanCuentasNIIFS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PlanCuentasNIIFS</param>
        /// <returns>Entidad PlanCuentasNIIF consultado</returns>
        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PlanCuentasNIIF entidad = new PlanCuentasNIIF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Plan_Cuentas_NIIF WHERE COD_CUENTA_NIIF = '" + pId.ToString() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt32(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt32(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt32(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                            if (resultado["MANEJA_GIR"] != DBNull.Value) entidad.maneja_gir = Convert.ToInt32(resultado["MANEJA_GIR"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["REPORTARMAYOR"] != DBNull.Value) entidad.reportarmayor = Convert.ToInt32(resultado["REPORTARMAYOR"]);
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
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ConsultarPlanCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }

                /// <summary>
        /// Obtiene una lista de Entidades de la tabla PlanCuentasNIIF dados unos filtros
        /// </summary>
        /// <param name="pPlanCuentasNIIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanCuentasNIIFs obtenidos</returns>
        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentasNIIF, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCuentasNIIF> lstPlanCuentasNIIF = new List<PlanCuentasNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Plan_Cuentas_NIIF " + ObtenerFiltro(pPlanCuentasNIIF) + " ORDER BY COD_CUENTA_NIIF ";
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
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt32(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt32(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt32(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt32(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt32(resultado["IMPUESTO"]);
                            if (resultado["MANEJA_GIR"] != DBNull.Value) entidad.maneja_gir = Convert.ToInt32(resultado["MANEJA_GIR"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["REPORTARMAYOR"] != DBNull.Value) entidad.reportarmayor = Convert.ToInt32(resultado["REPORTARMAYOR"]);
                            lstPlanCuentasNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanCuentasNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ListarPlanCuentasNIIF", ex);
                        return null;
                    }
                }
            }
        }


        public void GenerarHomologacionMovimientos(ref string pError, DateTime pFechaIni, DateTime pFechaFin,Int64 pTipoComp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfechaini = cmdTransaccionFactory.CreateParameter();
                        pfechaini.ParameterName = "PFECHAINI";
                        pfechaini.Value = pFechaIni;
                        pfechaini.Direction = ParameterDirection.Input;
                        pfechaini.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaini);

                        DbParameter pfechafin = cmdTransaccionFactory.CreateParameter();
                        pfechafin.ParameterName = "PFECHAFIN";
                        pfechafin.Value = pFechaFin;
                        pfechafin.Direction = ParameterDirection.Input;
                        pfechafin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechafin);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "PTIPO_COMP";
                        if (pTipoComp != 0) ptipo_comp.Value = pTipoComp; else ptipo_comp.Value = DBNull.Value;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "PCOD_USUARIO";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_HOMOLOG_MOVIMIEN";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return;
                    }
                }
            }
        }


        public List<PlanCuentasNIIF> ListarReporteComparativoNIIF(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCuentasNIIF> lstPlanCuentasNIIF = new List<PlanCuentasNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.COD_CUENTA_NIIF, P.NOMBRE, P.TIPO, SUM(BN.SALDO) AS SALDONIIF
                                        FROM PLAN_CUENTAS_NIIF P INNER JOIN BALANCE_NIIF BN ON P.COD_CUENTA_NIIF = BN.COD_CUENTA_NIIF " + pFiltro
                                        + @" GROUP BY P.COD_CUENTA_NIIF, P.NOMBRE, P.TIPO
                                        ORDER BY P.COD_CUENTA_NIIF";

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
                            if (resultado["SALDONIIF"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDONIIF"]);

                            // Listar los saldos locales
                            int control = 0;
                            List<PlanCuentasNIIF> lstSaldosLocal = new List<PlanCuentasNIIF>();
                            lstSaldosLocal = ListarSaldosLocal(pFiltro + " AND PH.COD_CUENTA_NIIF = '" + entidad.cod_cuenta_niif + "' ", vUsuario);
                            foreach (PlanCuentasNIIF entidLoc in lstSaldosLocal)
                            {
                                PlanCuentasNIIF entidadNIIF = new PlanCuentasNIIF();
                                entidadNIIF.cod_cuenta_niif = entidad.cod_cuenta_niif;
                                entidadNIIF.nombre = entidad.nombre;
                                entidadNIIF.tipo = entidad.tipo;
                                entidadNIIF.cod_cuenta = entidLoc.cod_cuenta;
                                entidadNIIF.nombre_local = entidLoc.nombre_local;
                                entidadNIIF.saldo_local = entidLoc.saldo_local;
                                if (control == 0)
                                    // Si la cuenta NIIF tiene varias homologaciones entonces solamente mostrar saldo NIIF en el primer registro                                    
                                    entidadNIIF.saldo = entidad.saldo;
                                else
                                    entidadNIIF.saldo = 0;
                                entidadNIIF.diferencia = entidadNIIF.saldo - entidadNIIF.saldo_local;     
                                lstPlanCuentasNIIF.Add(entidadNIIF);
                                control += 1;
                            }

                            if (control == 0)
                            {
                                // Si no hay homologaciones mostrar la cuenta NIIF
                                entidad.diferencia = entidad.saldo;
                                lstPlanCuentasNIIF.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanCuentasNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ListarReporteComparativoNIIF", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentasNIIF> ListarSaldosLocal(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCuentasNIIF> lstPlanCuentasNIIF = new List<PlanCuentasNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.COD_CUENTA, P.NOMBRE AS NOMBRE_LOCAL, P.TIPO, SUM(BN.VALOR) AS SALDOLOCAL
                                        FROM PLAN_CUENTAS P INNER JOIN BALANCE BN ON P.COD_CUENTA = BN.COD_CUENTA INNER JOIN PLAN_CUENTAS_HOMOLOGA PH ON PH.COD_CUENTA = P.COD_CUENTA " + pFiltro
                                        + @" GROUP BY P.COD_CUENTA, P.NOMBRE, P.TIPO
                                        ORDER BY P.COD_CUENTA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanCuentasNIIF entidad = new PlanCuentasNIIF();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_LOCAL"] != DBNull.Value) entidad.nombre_local = Convert.ToString(resultado["NOMBRE_LOCAL"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);                                                        
                            if (resultado["SALDOLOCAL"] != DBNull.Value) entidad.saldo_local = Convert.ToDecimal(resultado["SALDOLOCAL"]);

                            lstPlanCuentasNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlanCuentasNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ListarReporteComparativoNIIF", ex);
                        return null;
                    }
                }
            }
        }


        public List<PlanCtasHomologacionNIF> ListarCuentasHomologadas(string pFiltro, string pOpcion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCtasHomologacionNIF> lstHomologadas = new List<PlanCtasHomologacionNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT P.*, X.NOMBRE AS NOM_LOCAL, N.NOMBRE AS NOM_NIIF FROM PLAN_CUENTAS_HOMOLOGA P 
                                    LEFT JOIN PLAN_CUENTAS X ON X.COD_CUENTA = P.COD_CUENTA 
                                    LEFT JOIN PLAN_CUENTAS_NIIF N ON N.COD_CUENTA_NIIF = P.COD_CUENTA_NIIF " + pFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanCtasHomologacionNIF entidad = new PlanCtasHomologacionNIF();
                            if (resultado["IDHOMOLOGA"] != DBNull.Value) entidad.idhomologa = Convert.ToInt64(resultado["IDHOMOLOGA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (pOpcion == "N")
                            {
                                if (resultado["NOM_LOCAL"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOM_LOCAL"]);
                            }
                            else
                            {
                                if (resultado["NOM_NIIF"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOM_NIIF"]);
                            }
                            lstHomologadas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstHomologadas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasNIIFData", "ListarReporteComparativoNIIF", ex);
                        return null;
                    }
                }
            }
        }



    }
}