using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentas
    /// </summary>    
    public class LibroAuxiliarData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public LibroAuxiliarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string sCuentas,Int32 moneda, string pOrdenar, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DbDataReader resultadoComp = default(DbDataReader);
            List<LibroAuxiliar> lstLibAux = new List<LibroAuxiliar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();                        

                        string sCuentasPlan = sCuentas.Replace("d.", "dd.");
                        string sql = "";
                        sql = @"Select dd.cod_cuenta, dd.nombre As nomcue, dd.tipo As naturaleza, dd.depende_de, dd.base_minima, dd.porcentaje_impuesto
                                    From Plan_Cuentas dd 
                                    Where " + sCuentasPlan + @" And dd.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de = dd.cod_cuenta) 
                                    And dd.cod_moneda = " + moneda.ToString() + " Order by dd.cod_cuenta";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string lcod_cuenta = "";
                        string lnombrecuenta = "";
                        string lnaturaleza = "";
                        string ldepende_de = "";
                        decimal lbase_minima = 0;
                        decimal lporcentaje_impuesto = 0;

                        while (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) lnombrecuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) lnaturaleza = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) ldepende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) lbase_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) lporcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);

                            LibroAuxiliar entidadini = new LibroAuxiliar();
                            entidadini.fecha = null;
                            entidadini.cod_cuenta = lcod_cuenta;
                            entidadini.nombrecuenta = lnombrecuenta;
                            entidadini.naturaleza = lnaturaleza;
                            entidadini.depende_de = ldepende_de;
                            entidadini.detalle = "SALDO INICIAL";
                            entidadini.saldo = 0;  // El saldo se calcula en capa negocio
                            lstLibAux.Add(entidadini);

                            string sqlT = "";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                // Consultar datos de comprobantes                                
                                sqlT = @"Select e.fecha, e.num_comp, e.tipo_comp, e.n_documento, e.num_consig, d.detalle, d.tipo, d.valor, e.concepto, e.tipo_benef, 
                                            e.iden_benef, e.tipo_iden, e.nombre, d.tercero, d.identificacion, d.tipo_iden As tipo_iden_tercero, d.nom_tercero, 
                                            d.cod_cuenta, d.nomcue, d.naturaleza, d.depende_de, d.centro_costo, d.centro_gestion, d.codigo, r.tipo_regimen, d.base_comp, d.porcentaje ,e.observaciones
                                            From e_comprobante e, d_comprobante d Left Join Regimen r On (d.tercero = r.codpersona ) 
                                            Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N'
                                            And Trunc(e.fecha) between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') " + @"
                                            And d.cod_cuenta = '" + lcod_cuenta + "' And d.centro_costo Between " + CenIni.ToString() + " And " + CenFin.ToString();                            
                                if (pOrdenar == "1")        // Ordenar por tipo de comprobante
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), e.tipo_comp, e.num_comp,d.valor";
                                else if (pOrdenar == "2")   // Ordenar por número de comprobante;
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), e.num_comp,d.valor";
                                else if (pOrdenar == "3")   // Ordernar por tercero
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), d.tercero,d.valor";
                                else
                                    sqlT += " Order by e.fecha, e.num_comp, e.tipo_comp,d.valor";
                            }
                            else
                            {
                                sqlT = "Select e.fecha, e.num_comp, e.tipo_comp, e.n_documento, e.num_consig, d.detalle, d.tipo, d.valor, e.concepto, e.tipo_benef, e.iden_benef, e.tipo_iden, " +
                                          "e.nombre, d.tercero, d.identificacion, d.tipo_iden, d.nom_tercero, d.cod_cuenta, d.nomcue, d.naturaleza, d.depende_de, d.centro_costo, d.centro_gestion, d.codigo, r.tipo_regimen,e.observaciones " +
                                          "From e_comprobante e, d_comprobante d Left Join Regimen r On (d.tercero = r.codpersona ) " +
                                          "Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N' " +
                                          "And e.fecha between '" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "' and '" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "' " +
                                          "And d.cod_cuenta = '" + lcod_cuenta + "' And d.centro_costo Between " + CenIni.ToString() + " And " + CenFin.ToString();
                                if (pOrdenar == "1")
                                    sqlT += " Order by e.fecha, e.tipo_comp, e.num_comp,d.valor";
                                else if (pOrdenar == "2")
                                    sqlT += " Order by e.fecha, e.num_comp,d.valor";
                                else if (pOrdenar == "3")
                                    sqlT += " Order by e.fecha, d.tercero,d.valor";
                                else
                                    sqlT += " Order by e.fecha, e.num_comp, e.tipo_comp,d.valor";
                            }
                                                
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sqlT;
                            resultadoComp = cmdTransaccionFactory.ExecuteReader();
                            while (resultadoComp.Read())
                            {
                                LibroAuxiliar entidad = new LibroAuxiliar();
                                if (resultadoComp["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultadoComp["COD_CUENTA"]);
                                if (resultadoComp["NOMCUE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultadoComp["NOMCUE"]);
                                entidad.naturaleza = lnaturaleza;
                                if (resultadoComp["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultadoComp["DEPENDE_DE"]);
                                if (resultadoComp["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultadoComp["FECHA"]);
                                if (resultadoComp["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultadoComp["NUM_COMP"]);
                                if (resultadoComp["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultadoComp["TIPO_COMP"]);
                                if (resultadoComp["N_DOCUMENTO"] != DBNull.Value) entidad.sop_egreso = Convert.ToString(resultadoComp["N_DOCUMENTO"]);
                                if (resultadoComp["NUM_CONSIG"] != DBNull.Value) entidad.sop_ingreso = Convert.ToString(resultadoComp["NUM_CONSIG"]);
                                entidad.num_sop = entidad.sop_egreso != null && entidad.sop_egreso != "0" && entidad.sop_egreso.Trim() != "" ? entidad.sop_egreso : null;
                                if (entidad.sop_ingreso != null && entidad.sop_ingreso != "0" && entidad.sop_ingreso.Trim() != "")
                                {
                                    if (entidad.num_sop != null && entidad.num_sop != "0")
                                        entidad.num_sop += "/" + entidad.sop_ingreso;
                                    else
                                        entidad.num_sop = entidad.sop_ingreso;
                                }
                                if (resultadoComp["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultadoComp["DETALLE"]);
                                if (resultadoComp["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultadoComp["TIPO"]);
                                if (resultadoComp["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultadoComp["VALOR"]);
                                if (resultadoComp["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultadoComp["CONCEPTO"]);
                                if (resultadoComp["IDEN_BENEF"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultadoComp["IDEN_BENEF"]);
                                if (resultadoComp["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultadoComp["NOMBRE"]);
                                if (resultadoComp["IDENTIFICACION"] != DBNull.Value) entidad.identific_tercero = Convert.ToString(resultadoComp["IDENTIFICACION"]);
                                if (resultadoComp["NOM_TERCERO"] != DBNull.Value) entidad.nombre_tercero = Convert.ToString(resultadoComp["NOM_TERCERO"]);
                                if (resultadoComp["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultadoComp["CENTRO_COSTO"]);
                                if (resultadoComp["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToString(resultadoComp["CENTRO_GESTION"]);
                                if (resultadoComp["CODIGO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultadoComp["CODIGO"]);
                                if (resultadoComp["TIPO_REGIMEN"] != DBNull.Value) entidad.regimen = Convert.ToString(resultadoComp["TIPO_REGIMEN"]);
                                if (resultadoComp["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultadoComp["OBSERVACIONES"]);

                                entidad.base_minima = lbase_minima;
                                entidad.porcentaje_impuesto = lporcentaje_impuesto;
                                lstLibAux.Add(entidad);
                            }
                        }

                        return lstLibAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "ListarAuxiliar", ex);
                        return null;
                    }
                }
            }
        }

        public bool GenerarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string sCuentas, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sCuentasPlan = sCuentas.Replace("d.", "dd.");
                        string sqlT = "";
                        sqlT = @"Insert Into TEMP_LIBROAUXILIAR (fecha, num_comp, tipo_comp, n_documento, num_consig, detalle, tipo, valor, concepto, tipo_benef, 
                                    iden_benef, tipo_iden, nombre, tercero, identificacion, tipo_iden_tercero, nom_tercero, 
                                    cod_cuenta, nomcue, naturaleza, depende_de, centro_costo, centro_gestion, codigo, tipo_regimen, base_impuesto, porcentaje_impuesto)
                                    Select e.fecha, e.num_comp, e.tipo_comp, e.n_documento, e.num_consig, d.detalle, d.tipo, d.valor, e.concepto, e.tipo_benef, 
                                        e.iden_benef, e.tipo_iden, e.nombre, d.tercero, d.identificacion, d.tipo_iden As tipo_iden_tercero, d.nom_tercero, 
                                        d.cod_cuenta, d.nomcue, d.naturaleza, d.depende_de, d.centro_costo, d.centro_gestion, d.codigo, r.tipo_regimen, d.base_comp, d.porcentaje 
                                        From e_comprobante e, d_comprobante d Left Join Regimen r On (d.tercero = r.codpersona ) 
                                        Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N'
                                        And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') " + @"
                                        And " + sCuentas + " And d.centro_costo Between " + CenIni.ToString() + " And " + CenFin.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlT;
                        resultado = cmdTransaccionFactory.ExecuteReader();                        

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "GenerarAuxiliar", ex);
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el plan de cuentas
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<PlanCuentas> ListarPlanCuentas(Usuario pUsuario, String filtro)
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
                        if (string.Equals(filtro, "AUXILIARES"))
                        {
                            sql = "Select * from plan_cuentas Where plan_cuentas.cod_cuenta Not In (Select x.depende_de From plan_cuentas x Where x.depende_de = plan_cuentas.cod_cuenta) Order by plan_cuentas.cod_cuenta";
                        }
                        else
                        {
                            sql = "Select * from plan_cuentas " + filtro;
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
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiroAuxiliarData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para obtener todas las cuentas hijas de una cuenta dada
        /// </summary>
        /// <param name="pcod_cuenta"></param>
        /// <returns></returns>
        public string CargarCuentas(string pcod_cuenta, Usuario pUsuario)
        {
            string sListadoCuentas = "";
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string cod_cuenta = "";
                        string sql = "Select p.cod_cuenta From plan_cuentas p Where p.depende_de = '" + pcod_cuenta + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            string sCuentasHijas = CargarCuentas(cod_cuenta, pUsuario);
                            sListadoCuentas = sCuentasHijas + sListadoCuentas + ", '" + cod_cuenta + "' ";
                        }
                        return sListadoCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "CargarCuentas", ex);
                        return "";
                    }
                }
            }
        }


        /// <summary>
        /// Determinar la fecha de cierre inicial para un tipo de cierre dato
        /// </summary>
        /// <param name="ptipocierre">el tipo de cierre a verificar</param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string ptipocierre, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecha_cierre = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Min(fecha) As fecha From cierea Where tipo = '" + ptipocierre + "' And estado = 'D' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        return fecha_cierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "CargarCuentas", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }


        public double SaldoCuenta(string cod_cuenta, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
        {
            Double saldo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "pcod_cuenta";
                        pCOD_CUENTA.Value = cod_cuenta;
                        pCOD_CUENTA.Direction = ParameterDirection.Input;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "pfecha";
                        pFECHA.Value = fecha;
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pCENINI = cmdTransaccionFactory.CreateParameter();
                        pCENINI.ParameterName = "pcenini";
                        pCENINI.Value = cenini;
                        pCENINI.DbType = DbType.Int64;
                        pCENINI.Direction = ParameterDirection.Input;

                        DbParameter pCENFIN = cmdTransaccionFactory.CreateParameter();
                        pCENFIN.ParameterName = "pcenfin";
                        pCENFIN.Value = cenfin;
                        pCENFIN.DbType = DbType.Int64;
                        pCENFIN.Direction = ParameterDirection.Input;

                        DbParameter pSALDO = cmdTransaccionFactory.CreateParameter();
                        pSALDO.ParameterName = "psaldo";
                        pSALDO.Value = 0;
                        pSALDO.DbType = DbType.Double;
                        pSALDO.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCENINI);
                        cmdTransaccionFactory.Parameters.Add(pCENFIN);
                        cmdTransaccionFactory.Parameters.Add(pSALDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOCUENTA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        saldo = Convert.ToDouble(pSALDO.Value);

                        return saldo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "SaldoCuenta", ex);
                        return 0;
                    }
                }
            }
        }

    }
}
