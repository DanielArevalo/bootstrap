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
    
    public class ImpuestoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public ImpuestoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Impuesto> ListarRetencion(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            return ListarRetencion("", filtro, pFechaIni, pFechaFin, vUsuario);
        }

        public List<Impuesto> ListarRetencion(string pCodTipoImpuesto, string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Impuesto> lstImpuestos = new List<Impuesto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "", sqlD = "";

                        string[] filtros = new string[2];
                        filtros = filtro.Split('|');

                        sql = @"Select Distinct e.iden_benef, e.nombre, c.nomciudad, e.direccion, e.telefono, e.email, "
                                + "(Case d.tipo When p.tipo Then 1 Else -1 End)* d.base_comp As base_comp,  d.porcentaje, sum(Case d.tipo When p.tipo Then d.valor Else -d.valor End) as valor, e.num_comp, e.fecha, d.cod_cuenta, d.nomcue "
                                + "From e_comprobante e Inner join d_comprobante d on e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp "
                                + "Inner join ciudades c on c.codciudad = e.ciudad "
                                + "Inner join tipo_comp t on t.tipo_comp = d.tipo_comp "
                                + "Inner Join Plan_Cuentas P On P.Cod_Cuenta = D.Cod_Cuenta "
                                + "Where d.tercero Is Null And d.cod_cuenta In (Select p.cod_cuenta_imp From plan_cuentas_impuesto p Where p.cod_cuenta_imp = d.cod_cuenta" + (pCodTipoImpuesto == "" ? "" : pCodTipoImpuesto) + ") " + (filtros.Count() >= 1 ? filtros[0] : "") + " ";

                        sqlD = @"Select Distinct d.identificacion, d.nom_tercero, c.nomciudad, d.direccion, d.telefono, d.email, "
                               + "(Case d.tipo When p.tipo Then 1 Else -1 End)*d.base_comp As base_comp,  d.porcentaje, sum(Case d.tipo When p.tipo Then d.valor Else -d.valor End) as valor, e.num_comp, e.fecha, d.cod_cuenta, d.nomcue "
                               + "From e_comprobante e Inner join d_comprobante d on e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp "
                               + "Inner join ciudades c on c.codciudad = e.ciudad "
                               + "Inner join tipo_comp t on t.tipo_comp = d.tipo_comp "
                               + "Inner Join Plan_Cuentas P On P.Cod_Cuenta = D.Cod_Cuenta "
                               + "Where d.tercero Is Not Null And d.cod_cuenta In (Select p.cod_cuenta_imp From plan_cuentas_impuesto p Where p.cod_cuenta_imp = d.cod_cuenta" + (pCodTipoImpuesto == "" ? " " : pCodTipoImpuesto) + ") " + (filtros.Count() >= 2 ? filtros[1] : "") + " ";

                        string sqlCondicion = " ";
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sqlCondicion += " And ";
                            else
                                sqlCondicion += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sqlCondicion += " e.fecha >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sqlCondicion += " e.fecha >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sqlCondicion += " And ";
                            else
                                sqlCondicion += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sqlCondicion += " e.fecha <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sqlCondicion += " e.fecha <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql  += sqlCondicion + " Group by e.iden_benef, e.nombre, c.nomciudad, e.direccion, e.telefono, e.email, (Case d.tipo When p.tipo Then 1 Else -1 End)*d.base_comp, d.porcentaje, e.num_comp, e.fecha, d.cod_cuenta, d.nomcue";

                        sqlD += sqlCondicion + " Group by d.identificacion, d.nom_tercero, c.nomciudad, d.direccion, d.telefono, d.email, (Case d.tipo When p.tipo Then 1 Else -1 End)*d.base_comp, d.porcentaje, e.num_comp, e.fecha, d.cod_cuenta, d.nomcue";
                        sqlD += " Order by 1, 12 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql + " UNION ALL " + sqlD;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Impuesto entidad = new Impuesto();

                            if (resultado["iden_benef"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["iden_benef"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["nomciudad"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["nomciudad"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);                                                        
                            if (resultado["base_comp"] != DBNull.Value) entidad.baseimp = Convert.ToDecimal(resultado["base_comp"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);                           
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comprobante = Convert.ToInt32(resultado["num_comp"]);
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nomcue"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["nomcue"]);
                            lstImpuestos.Add(entidad);
                        }

                        return lstImpuestos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarRetencion", ex);
                        return null;
                    }
                }
            }
        }


        public Impuesto ConsultaTelefonoEmpresa(int id, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Impuesto entidad = new Impuesto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        sql = @"SELECT E.TELEFONO,E.DIRECCION, C.NOMCIUDAD FROM EMPRESA E INNER JOIN CIUDADES C ON C.CODCIUDAD= E.CIUDAD WHERE E.COD_EMPRESA = " + id;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ConsultaTelefonoEmpresa", ex);
                        return null;
                    }
                }
            }
        }

        public List<Impuesto> getListaGridv(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Impuesto> lstImpuestos = new List<Impuesto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string[] filtros = new string[2];
                        filtros = filtro.Split('|');
                        string sql = "";

                        sql = @"   Select e.iden_benef, e.nombre , e.codciudad, e.direccion, e.telefono, e.email,'concepto' as Concepto , "
                                + " d.base_comp, d.porcentaje, sum(d.valor) as valor "
                                + " from e_comprobante e inner join d_comprobante d on e.num_comp = d.num_comp and e.tipo_comp = d.tipo_comp "
                                + " where  e.fecha between To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')"
                                + " and To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') "
                                + " and e.estado != 'N' And d.tercero Is Null " + (filtros.Count() >= 1 ? filtros[0] : "")  + (filtros.Count() >= 2 ? filtros[1] : "") + " group by e.iden_benef, e.nombre, e.codciudad, e.direccion, e.telefono, e.email, d.base_comp, d.porcentaje "
                                + "Union All "
                                + " Select e.iden_benef, e.nombre , e.codciudad, e.direccion, e.telefono, e.email,'concepto' as Concepto , "
                                + " d.base_comp, d.porcentaje, sum(d.valor) as valor "
                                + " from e_comprobante e inner join d_comprobante d on e.num_comp = d.num_comp and e.tipo_comp = d.tipo_comp "
                                + " where  e.fecha between To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')"
                                + " and To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') "
                                + " and e.estado != 'N' And d.tercero Is Not Null " + (filtros.Count() >= 1 ? filtros[0] : "") + (filtros.Count() >= 3 ? filtros[2] : "") + " group by e.iden_benef, e.nombre, e.codciudad,e.direccion, e.telefono,e.email, d.base_comp,d.porcentaje "
                                + " order by 1,7 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Impuesto entidad = new Impuesto();

                            if (resultado["iden_benef"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["iden_benef"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["codciudad"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["codciudad"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);
                            if (resultado["Concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["Concepto"]);
                            if (resultado["base_comp"] != DBNull.Value) entidad.baseimp = Convert.ToDecimal(resultado["base_comp"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["porcentaje"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            lstImpuestos.Add(entidad);
                        }

                        return lstImpuestos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteImpuestosData", "ListarRetencion", ex);
                        return null;
                    }
                }
            }
        }
    }
}
