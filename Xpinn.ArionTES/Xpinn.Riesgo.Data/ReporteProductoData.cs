using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class ReporteProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ReporteProducto> ListarReporteProducto(ReporteProducto pReporte, DateTime? pFecIni, DateTime? pFecFin, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ReporteProducto> lstReporteProducto = new List<ReporteProducto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select p.cod_persona, p.identificacion, p.fechaexpedicion, (Case p.tipo_persona When 'N' Then QUITARESPACIOS(Substr(p.primer_nombre || ' ' || p.segundo_nombre || ' ' || p.primer_apellido || ' ' || p.segundo_apellido, 0, 240)) Else p.razon_social End) As nombre,
                                    Case p.sexo When 'M' Then 'Mascúlino' When 'F' Then 'Femenino' End As genero, p.cod_oficina, o.nombre As nom_oficina, p.fechanacimiento, f.fecha_afiliacion, ci.nomciudad as ciudadresidencia, p.direccion, p.telefono, p.email, t.descripcion As tipoidentificacion,
                                    ase.SNOMBRE1 || ' '||ase.SNOMBRE2|| ' '||ase.SAPELLIDO1|| ' '||ase.SAPELLIDO2 AS ASESOR, case h.estado when 'R' then 'Retirado' else 'Activo' end as estado,
                                    Sum(case vs.tipo_producto when 'Aportes' then vs.saldo else 0 end) as Saldo_Aportes,
                                    Sum(case vs.tipo_producto when 'Créditos' then vs.saldo else 0 end) as Saldo_Creditos,
                                    Sum(case vs.tipo_producto when 'Ahorros Vista' then vs.saldo else 0 end) as Saldo_Ahorro_Vista,
                                    Sum(case vs.tipo_producto when 'Servicios' then vs.saldo else 0 end) as Saldo_Servicios,
                                    Sum(case vs.tipo_producto when 'CDAT' then vs.saldo else 0 end) as Saldo_CDAT,
                                    Sum(case vs.tipo_producto when 'Ahorros Programado' then vs.saldo else 0 end) as Saldo_Ahorro_Programado
                                    From persona p Left Join persona_afiliacion f On p.cod_persona = f.cod_persona Left join ciudades ci on ci.codciudad = p.codciudadresidencia
                                    Left Join oficina o On p.cod_oficina = o.cod_oficina Left Join tipoidentificacion t On p.tipo_identificacion = t.codtipoidentificacion 
                                    Left Join asejecutivos ase on ase.iusuario=p.cod_asesor 
                                    left join historico_persona h on p.cod_persona = h.cod_persona
                                    left join VSarlaft_Producto vs on vs.cod_persona = p.cod_persona and vs.Fecha_Historico = h.fecha_historico";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " ") + " h.fecha_historico = TO_DATE('" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " f.fecha_afiliacion <= TO_DATE('" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        }
                        else
                        {
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " ") + " h.fecha_historico = '" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " f.fecha_afiliacion <= '" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += (!sql.ToUpper().Contains("WHERE") ? " WHERE p.cod_persona > 0 " : " ") + pFiltro;
                        sql += " GROUP BY  p.cod_persona, p.identificacion, p.fechaexpedicion, p.tipo_persona ,p.primer_nombre, p.segundo_nombre, p.primer_apellido, p.segundo_apellido, p.razon_social,p.sexo, p.cod_oficina, o.nombre, p.fechanacimiento, f.fecha_afiliacion, ci.nomciudad, p.direccion, p.telefono, p.email, t.descripcion, ase.SNOMBRE1,ase.SNOMBRE2,ase.SAPELLIDO1,ase.SAPELLIDO2, h.estado Order by 3 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteProducto entidad = new ReporteProducto();
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["fechaexpedicion"] != DBNull.Value) entidad.fecha_expedicion = Convert.ToDateTime(resultado["fechaexpedicion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["genero"] != DBNull.Value) entidad.genero = Convert.ToString(resultado["genero"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nom_oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["nom_oficina"]);
                            if (resultado["fechanacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fechanacimiento"]);
                            if (resultado["fecha_afiliacion"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["fecha_afiliacion"]);
                            if (resultado["ciudadresidencia"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["ciudadresidencia"]);
                            if (resultado["direccion"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["direccion"]);
                            if (resultado["telefono"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["telefono"]);
                            if (resultado["email"] != DBNull.Value) entidad.email = Convert.ToString(resultado["email"]);
                            if (resultado["tipoidentificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipoidentificacion"]);
                            if (resultado["asesor"] != DBNull.Value) entidad.Asesor = Convert.ToString(resultado["asesor"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["Saldo_Aportes"] != DBNull.Value) entidad.saldo_aportes = Convert.ToInt64(resultado["Saldo_Aportes"]);
                            if (resultado["Saldo_Creditos"] != DBNull.Value) entidad.saldo_creditos = Convert.ToInt64(resultado["Saldo_Creditos"]);
                            if (resultado["Saldo_Ahorro_vista"] != DBNull.Value) entidad.saldo_ahorroV = Convert.ToInt64(resultado["Saldo_Ahorro_vista"]);
                            if (resultado["Saldo_Servicios"] != DBNull.Value) entidad.saldo_servicios = Convert.ToInt64(resultado["Saldo_Servicios"]);
                            if (resultado["Saldo_CDAT"] != DBNull.Value) entidad.saldo_cdat = Convert.ToInt64(resultado["Saldo_CDAT"]);
                            if (resultado["Saldo_Ahorro_programado"] != DBNull.Value) entidad.saldo_ahorroP= Convert.ToInt64(resultado["Saldo_Ahorro_programado"]);
                            lstReporteProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SarlaftAlertaData", "ListarSarlaftAlerta", ex);
                        return null;
                    }
                }
            }
        }

        public List<producto> ListarProductoSaldo(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<producto> lstReporteProducto = new List<producto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select a.tipo_producto, Sum(a.saldo) As saldo, Count(*) As cantidad From VSarlaft_Producto a Where a.saldo != 0 And a.cod_persona = " + pCod_Persona;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += pFecIni == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " a.fecha_historico = TO_DATE('" + Convert.ToDateTime(pFecIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += pFecIni == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " a.fecha_historico = '" + Convert.ToDateTime(pFecIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " a.fecha_historico = TO_DATE('" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += pFecFin == null ? " " : (!sql.ToUpper().Contains("WHERE") ? " WHERE " : " AND ") + " a.fecha_historico = '" + Convert.ToDateTime(pFecFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Group by a.tipo_producto Order by 1, 3 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            producto entidad = new producto();
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["cantidad"] != DBNull.Value) entidad.cantidad = Convert.ToInt32(resultado["cantidad"]);
                            lstReporteProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteProducto;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }
        public List<producto> ListarProductoSaldoXAsociado(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<producto> lstReporteProducto = new List<producto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select a.tipo_producto, Sum(a.saldo) As saldo, Count(*) As cantidad,a.cuota From VREPORT_PRODUCTO a Where a.saldo != 0 and (a.estado='1' or a.estado='C') and a.cod_persona="+pCod_Persona+" Group by a.tipo_producto,a.cuota Order by 1, 3 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            producto entidad = new producto();
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["saldo"]);
                            if (resultado["cantidad"] != DBNull.Value) entidad.cantidad = Convert.ToInt32(resultado["cantidad"]);
                            if (resultado["cuota"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["cuota"]);
                            lstReporteProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporteProducto;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

    }

}
