using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class TrasladoPagaduriasData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public TrasladoPagaduriasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Productos_Persona ModificarTrasladoPagadurias(Productos_Persona pProducto, Int64 cod_persona , Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_producto.ParameterName = "p_cod_tipo_producto";
                        p_cod_tipo_producto.Value = pProducto.cod_tipo_producto;
                        p_cod_tipo_producto.Direction = ParameterDirection.Input;
                        p_cod_tipo_producto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_producto);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "p_num_producto";
                        pnum_producto.Value = pProducto.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        pcod_linea.Value = pProducto.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        DbParameter p_cod_forma_pago = cmdTransaccionFactory.CreateParameter();
                        p_cod_forma_pago.ParameterName = "p_cod_forma_pago";
                        p_cod_forma_pago.Value = pProducto.forma_pago;
                        p_cod_forma_pago.Direction = ParameterDirection.Input;
                        p_cod_forma_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_forma_pago);

                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        p_cod_empresa.Value = pProducto.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pProducto.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_TRASLADOPAG";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoPagaduriasData", "ModificarTrasladoPagadurias", ex);
                        return null;
                    }
                }
            }
        }
   

        public TrasladoPagadurias ConsultarTrasladoPagadurias(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TrasladoPagadurias entidad = new TrasladoPagadurias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.cod_persona,p.tipo_persona,p.Identificacion,p.Nombre,e.cod_empresa from v_persona p left join PERSONA_EMPRESA_RECAUDO e on p.cod_persona = e.cod_persona  where p.cod_persona= " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion= Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);

                         }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        sql = @"SELECT * FROM V_PRODUCTOSPERSONA WHERE COD_PERSONA=" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Productos_Persona productos = new Productos_Persona();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) productos.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) productos.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) productos.num_producto = Convert.ToInt64(resultado["NUM_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) productos.cod_linea = Convert.ToInt64(resultado["COD_LINEA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) productos.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) productos.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) productos.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) productos.porcentaje = Convert.ToInt32(resultado["PORCENTAJE"]);
                            entidad.Lista_Producto.Add(productos);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TrasladoPagaduriasData", "ConsultarTrasladoPagadurias", ex);
                        return null;
                    }
                }
            }
        }


    }
}