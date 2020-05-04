using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AreasCaj
    /// </summary>
    public class ReporteMovimientoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public ReporteMovimientoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ReporteMovimientos> ListarReporteMovimientos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteMovimientos> lstData = new List<ReporteMovimientos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select A.Cod_Ope, Op.Cod_Ofi Coficina, D.Nombre NomOficina, Op.Cod_Cajero ccajero,X.Nombre ccaja, T.Descripcion as nommoneda, "
                                            +"op.fecha_oper fechamov, a.cod_persona codpersona,  p.nombre nom_cliente, a.num_documento, "
                                            + "p.identificacion iden_cliente, op.tipo_ope codtipoope, op.num_comp, op.tipo_comp, a.valor, " 
                                            +"u.nombre usuario, u.identificacion iden_usuario, "
                                            + "Op.Tipo_Comp ||'-'|| Tc.Descripcion as nomtipo_comp, L.Descripcion as nomTipo_Ope,Tp.Descripcion as nomTipo_Pago,tp.Cod_Tipo_Pago, "
                                            +"OP.ESTADO,CASE OP.ESTADO WHEN 1 THEN 'NORMAL' WHEN 2 THEN 'ANULADO' END AS NOM_ESTADO_OPE "
                                            + " From movimientocaja a Left Join operacion op On op.cod_ope = a.cod_ope "
                                            +"Left Join Tipo_Comp Tc On Tc.Tipo_Comp = Op.Tipo_Comp "
                                            +"Left join Tipo_Ope L on L.Tipo_Ope = Op.Tipo_Ope "
                                            +"left Join Tipo_Pago Tp on Tp.Cod_Tipo_Pago = A.Cod_Tipo_Pago "
                                            +"Left Join v_persona p On a.cod_persona = p.cod_persona "
                                            +"Left Join oficina d On op.cod_ofi = d.cod_oficina "
                                            +"Left Join Usuarios U On U.Codusuario = Op.Cod_Usu "
                                            +"Left join caja X on X.Cod_Caja = Op.Cod_Caja "
                                            +"Left join Tipomoneda t on T.Cod_Moneda = A.Cod_Moneda where 1 = 1 "
                                            + filtro;
                        if (pFechaIni != DateTime.MinValue && pFechaIni != null)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " and trunc(op.fecha_oper) >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " and op.fecha_oper >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " and trunc(op.fecha_oper) <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " and op.fecha_oper <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += "Order By 1 Desc"; 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteMovimientos entidad = new ReporteMovimientos();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COFICINA"] != DBNull.Value) entidad.cod_ofi = Convert.ToInt32(resultado["COFICINA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["CCAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt32(resultado["CCAJERO"]);
                            if (resultado["CCAJA"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["CCAJA"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["FECHAMOV"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHAMOV"]); 
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["IDEN_CLIENTE"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDEN_CLIENTE"]);
                            if (resultado["NOM_CLIENTE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_CLIENTE"]);
                            if (resultado["CODTIPOOPE"] != DBNull.Value) entidad.cod_tipoope = Convert.ToInt32(resultado["CODTIPOOPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["USUARIO"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO"]);
                            if (resultado["IDEN_USUARIO"] != DBNull.Value) entidad.iden_usuario = Convert.ToString(resultado["IDEN_USUARIO"]);
                            if (resultado["NOMTIPO_COMP"] != DBNull.Value) entidad.nomtipo_comp= Convert.ToString(resultado["NOMTIPO_COMP"]);
                            if (resultado["NOMTIPO_OPE"] != DBNull.Value) entidad.nomTipo_Ope = Convert.ToString(resultado["NOMTIPO_OPE"]);
                            if (resultado["NOMTIPO_PAGO"] != DBNull.Value) entidad.nomTipo_Pago = Convert.ToString(resultado["NOMTIPO_PAGO"]);
                            if (resultado["NUM_DOCUMENTO"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["NUM_DOCUMENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO_OPE"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO_OPE"]);
                            if (entidad.estado == 2)
                                entidad.valor = 0;
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoData", "ListarReporteMovimientos", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteMovimientos> ListarFormaPago(ReporteMovimientos  pEntformapago,Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteMovimientos> lstData = new List<ReporteMovimientos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"select * from Tipo_Pago where caja ='S'" + ObtenerFiltro(pEntformapago);
                        
                        sql += "   Order By 1 Desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteMovimientos entidad = new ReporteMovimientos();
                            if (resultado["COD_TIPO_PAGO"] != DBNull.Value) entidad.cod_tipo_pago = Convert.ToInt64(resultado["COD_TIPO_PAGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["DESCRIPCION"]);
                           
                            lstData.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstData;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoData", "ListarFormaPago", ex);
                        return null;
                    }
                }
            }
        }

    }
}