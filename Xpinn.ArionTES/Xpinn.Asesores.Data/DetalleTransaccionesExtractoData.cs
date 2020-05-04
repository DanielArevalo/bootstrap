using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class DetalleTransaccionesExtractoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        
        public DetalleTransaccionesExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

       
        public List<DetalleTransaccionesExtracto> ListarDetalleTransaccionesExtractos(DetalleTransaccionesExtracto entidad,Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleTransaccionesExtracto> lstDetalleTransaccionesExtractos = new List<DetalleTransaccionesExtracto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select o.fecha_oper As FechaTransaccion, t.numero_radicacion As Referencia, r.descripcion  as Descripcion, 0 as cuotas, t.valor as ValorTransaccion, 0 as SaldoPorPagar "
                                +"From tipo_tran r, tran_cred t, operacion o "
                                +"Where r.tipo_tran = t.tipo_tran And o.cod_ope = t.cod_ope And t.numero_radicacion = " + entidad.numero_radicacion;
                        if (entidad.fecha_Inicial != null && entidad.fecha_Inicial != DateTime.MinValue && entidad.fecha_Final != null && entidad.fecha_Final != DateTime.MinValue)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql += " and o.fecha_oper  Between To_Date('" + Convert.ToDateTime(entidad.fecha_Inicial).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')"
                                       + " and To_Date('" + Convert.ToDateTime(entidad.fecha_Final).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            }
                            else
                                sql += " o.fecha_oper Between '" + Convert.ToDateTime(entidad.fecha_Inicial).ToString(conf.ObtenerFormatoFecha()) + "' "
                                    + " and '" + Convert.ToDateTime(entidad.fecha_Final).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;                            
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            entidad = new DetalleTransaccionesExtracto();
                            //Asociar todos los valores a la entidad
                            if (resultado["FechaTransaccion"] != DBNull.Value) entidad.FechaTransaccion = Convert.ToDateTime(resultado["FechaTransaccion"]);
                            if (resultado["Referencia"] != DBNull.Value) entidad.Referencia = Convert.ToInt64(resultado["Referencia"]);
                            if (resultado["Descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["Descripcion"]);
                            if (resultado["Cuotas"] != DBNull.Value) entidad.Cuotas = Convert.ToInt32(resultado["Cuotas"]);
                            if (resultado["ValorTransaccion"] != DBNull.Value) entidad.ValorTransaccion = Convert.ToDouble(resultado["ValorTransaccion"]);
                            if (resultado["SaldoPorPagar"] != DBNull.Value) entidad.SaldoPorPagar = Convert.ToDouble(resultado["SaldoPorPagar"]);
                            lstDetalleTransaccionesExtractos.Add(entidad);
                        }

                        connection.Close();

                        return lstDetalleTransaccionesExtractos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetalleTransaccionesExtractoData", "ListarDetalleTransaccionesExtractos", ex);                        
                        return null;
                    }
                }
            }
        }


    }
}
