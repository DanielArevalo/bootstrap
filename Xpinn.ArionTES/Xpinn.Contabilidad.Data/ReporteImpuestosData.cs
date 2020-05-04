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

    public class ReporteImpuestosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public ReporteImpuestosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<ReporteImpuestos> ListarImpuestos(string filtro, DateTime pFechaIni, DateTime pFechaFin, string pOrdenar, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ReporteImpuestos> lstImpuestos = new List<ReporteImpuestos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";

                        sql = @"Select distinct NVL(d.identificacion, e.iden_benef) as identificacion, NVL(d.nom_tercero, e.nombre) as nombre, c.nomciudad, e.direccion, e.telefono, e.email, e.fecha, "
                                + "e.num_comp, t.descripcion, d.cod_cuenta, d.base_comp, d.porcentaje, max(d.valor) as valor, d.nomcue "
                                + "From e_comprobante e inner join d_comprobante d on e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp "
                                + "Inner Join tipo_comp t on t.tipo_comp = d.tipo_comp "
                                + "Inner Join Plan_Cuentas P On P.Cod_Cuenta = D.Cod_Cuenta "
                                + "Left Join Plan_Cuentas_Impuesto Pi On P.Cod_Cuenta = Pi.Cod_Cuenta_Imp "
                                + "Left Join ciudades c on c.codciudad = e.ciudad "
                                + "Left Join operacion o on o.num_comp=d.num_comp and o.tipo_comp=d.tipo_comp  and o.estado=1 and o.tipo_ope!=7  "
                                + "Where ( d.num_comp || '*' || d.tipo_comp Not In (select  num_comp || '*' || tipo_comp  From COMPROBANTE_ANULADO  ) "
                                + " Or d.num_comp || '*' || d.tipo_comp Not In (select  num_comp_anula || '*' || tipo_comp_anula From COMPROBANTE_ANULADO))   " + filtro;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " e.fecha >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " e.fecha >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " e.fecha <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " e.fecha <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " Group by d.identificacion, e.iden_benef, d.nom_tercero,e.nombre, c.nomciudad, e.direccion, e.telefono, e.email, e.fecha, "
                              + "e.num_comp, t.descripcion, d.cod_cuenta, d.base_comp, d.porcentaje, d.nomcue ";

                        if (pOrdenar != "" && pOrdenar != null)
                            sql += " Order By " + pOrdenar;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteImpuestos entidad = new ReporteImpuestos();

                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["nomciudad"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["telefono"]);
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comprobante = Convert.ToInt32(resultado["num_comp"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToString(resultado["descripcion"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nomcue"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["nomcue"]);
                            if (resultado["base_comp"] != DBNull.Value) entidad.baseimp = Convert.ToDecimal(resultado["base_comp"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);

                            lstImpuestos.Add(entidad);
                        }

                        return lstImpuestos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarImpuestos", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteImpuestos> ListarImpuestosCombo(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteImpuestos> lstImpu = new List<ReporteImpuestos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_TIPO_IMPUESTO,NOMBRE_IMPUESTO, PRINCIPAL, DEPEN_DE FROM Tipoimpuesto " + ObtenerFiltro(pImpu) + " ORDER BY COD_TIPO_IMPUESTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteImpuestos entidad = new ReporteImpuestos();
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nombre_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToString(resultado["PRINCIPAL"]);
                            if (resultado["DEPEN_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPEN_DE"]);
                            lstImpu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarImpuestosCombo", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteImpuestos> ListarCuentasConImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteImpuestos> lstImpu = new List<ReporteImpuestos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select Cod_Cuenta,Nombre From Plan_Cuentas where Impuesto = 1 order by Cod_Cuenta";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteImpuestos entidad = new ReporteImpuestos();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstImpu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarCuentasConImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteImpuestos> ListarCuentasImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteImpuestos> lstImpu = new List<ReporteImpuestos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT PI.COD_CUENTA_IMP AS COD_CUENTA,P.NOMBRE FROM PLAN_CUENTAS_IMPUESTO PI "
                                        + "LEFT JOIN PLAN_CUENTAS P ON PI.COD_CUENTA_IMP = P.COD_CUENTA " + ObtenerFiltro(pImpu, "PI.")
                                        + "ORDER BY COD_CUENTA_IMP";
                        if (pImpu.nombre_impuesto != null)
                        {
                            if(string.IsNullOrEmpty(pImpu.nombre_impuesto.ToString()))
                            {
                                sql = @"SELECT DISTINCT PI.COD_CUENTA_IMP AS COD_CUENTA,P.NOMBRE FROM PLAN_CUENTAS_IMPUESTO PI "
                                            + "LEFT JOIN PLAN_CUENTAS P ON PI.COD_CUENTA_IMP = P.COD_CUENTA " + pImpu.nombre_impuesto.ToString()
                                            + "ORDER BY COD_CUENTA_IMP";
                            }
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteImpuestos entidad = new ReporteImpuestos();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstImpu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarCuentasConImpuesto", ex);
                        return null;
                    }
                }
            }
        }


    }
}
