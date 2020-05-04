using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Data
{

    public class InventariosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Giro
        /// </summary>
        public InventariosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ivcategoria> ListarCategoriasProductos(ivcategoria pIVProducto, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivcategoria> lstProducto = new List<ivcategoria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.*, c.nombre As nombre_categoria FROM IV_CATEGORIA g LEFT JOIN IV_CATEGORIA c ON g.id_padre = c.id_categoria " + ObtenerFiltro(pIVProducto, "g.") + " ORDER BY g.ID_CATEGORIA desc ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivcategoria entidad = new ivcategoria();
                            if (resultado["ID_CATEGORIA"] != DBNull.Value) entidad.id_categoria = Convert.ToInt64(resultado["ID_CATEGORIA"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToString(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ID_PADRE"] != DBNull.Value) entidad.id_padre = Convert.ToInt64(resultado["ID_PADRE"]);
                            if (resultado["NOMBRE_CATEGORIA"] != DBNull.Value) entidad.nombre_categoria = Convert.ToString(resultado["NOMBRE_CATEGORIA"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarCategoriasProductos", ex);
                        return null;
                    }
                }
            }
         }

         public void ConsultarParametroContable(Int64 pID_categoria, ref string pCod_Cuenta, ref string pCod_Cuenta_Niif, ref string pCod_Cuenta_Ingreso, ref string pCod_Cuenta_Gasto, Usuario pUsuario)
         {
            DbDataReader resultado;
            pCod_Cuenta = "";
            pCod_Cuenta_Ingreso = "";
            pCod_Cuenta_Gasto = "";
            pCod_Cuenta_Niif = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.* FROM par_cue_inventario  g WHERE g.id_categoria = " + pID_categoria;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) pCod_Cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) pCod_Cuenta_Niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["COD_CUENTA_INGRESO"] != DBNull.Value) pCod_Cuenta_Ingreso = Convert.ToString(resultado["COD_CUENTA_INGRESO"]);
                            if (resultado["COD_CUENTA_GASTO"] != DBNull.Value) pCod_Cuenta_Gasto = Convert.ToString(resultado["COD_CUENTA_GASTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarParametroContable", "ConsultarParametroContable", ex);
                        return;
                    }
                }
            }
        }

        public ivcategoria ConsultarCategoriasProductos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ivcategoria entidad = new ivcategoria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.* FROM IV_CATEGORIA g WHERE g.id_categoria = " + pId;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_CATEGORIA"] != DBNull.Value) entidad.id_categoria = Convert.ToInt64(resultado["ID_CATEGORIA"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToString(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["ID_PADRE"] != DBNull.Value) entidad.id_padre = Convert.ToInt64(resultado["ID_PADRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ConsultarCategoriasProductos", ex);
                        return null;
                    }
                }
            }
        }

        public ParCueInventarios  ConsultarParCueInventarios(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ParCueInventarios entidad = new ParCueInventarios();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_CUE_INVENTARIO WHERE id_categoria = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["ID_CATEGORIA"] != DBNull.Value) entidad.id_categoria = Convert.ToInt64(resultado["ID_CATEGORIA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["COD_CUENTA_INGRESO"] != DBNull.Value) entidad.cod_cuenta_ingreso = Convert.ToString(resultado["COD_CUENTA_INGRESO"]);
                            if (resultado["COD_CUENTA_GASTO"] != DBNull.Value) entidad.cod_cuenta_gasto = Convert.ToString(resultado["COD_CUENTA_GASTO"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarParametroContable", "ConsultarParCueInventarios", ex);
                        return null;
                    }
                }
            }
        }

        public ParCueInventarios CrearParCueInventarios(ParCueInventarios pParCueInventarios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParCueInventarios.idparametro;
                        pidparametro.Direction = ParameterDirection.InputOutput;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pid_categoria = cmdTransaccionFactory.CreateParameter();
                        pid_categoria.ParameterName = "p_id_categoria";
                        pid_categoria.Value = pParCueInventarios.id_categoria;
                        pid_categoria.Direction = ParameterDirection.Input;
                        pid_categoria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_categoria);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pParCueInventarios.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pParCueInventarios.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.AnsiStringFixedLength;
                        pcod_cuenta.Size = 20;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        
                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pParCueInventarios.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pParCueInventarios.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);
                        
                        DbParameter pcod_cuenta_ingreso = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_ingreso.ParameterName = "p_cod_cuenta_ingreso";
                        if (pParCueInventarios.cod_cuenta_ingreso == null)
                            pcod_cuenta_ingreso.Value = DBNull.Value;
                        else
                            pcod_cuenta_ingreso.Value = pParCueInventarios.cod_cuenta_ingreso;
                        pcod_cuenta_ingreso.Direction = ParameterDirection.Input;
                        pcod_cuenta_ingreso.DbType = DbType.AnsiStringFixedLength;
                        pcod_cuenta_ingreso.Size = 20;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_ingreso);
                        
                        DbParameter pcod_cuenta_gasto = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_gasto.ParameterName = "p_cod_cuenta_gasto";
                        if (pParCueInventarios.cod_cuenta_gasto == null)
                            pcod_cuenta_gasto.Value = DBNull.Value;
                        else
                            pcod_cuenta_gasto.Value = pParCueInventarios.cod_cuenta_gasto;
                        pcod_cuenta_gasto.Direction = ParameterDirection.Input;
                        pcod_cuenta_gasto.DbType = DbType.AnsiStringFixedLength;
                        pcod_cuenta_gasto.Size = 20;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_gasto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PARCUEINV_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParCueInventarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventariosData", "CrearParCueInventarios", ex);
                        return null;
                    }
                }
            }
        }


        public ParCueInventarios ModificarParCueInventarios(ParCueInventarios pParCueInventarios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParCueInventarios.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pid_categoria = cmdTransaccionFactory.CreateParameter();
                        pid_categoria.ParameterName = "p_id_categoria";
                        pid_categoria.Value = pParCueInventarios.id_categoria;
                        pid_categoria.Direction = ParameterDirection.Input;
                        pid_categoria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pid_categoria);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pParCueInventarios.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pParCueInventarios.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pParCueInventarios.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pParCueInventarios.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pcod_cuenta_ingreso = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_ingreso.ParameterName = "pcod_cuenta_ingreso";
                        if (pParCueInventarios.cod_cuenta_ingreso == null)
                            pcod_cuenta_ingreso.Value = DBNull.Value;
                        else
                            pcod_cuenta_ingreso.Value = pParCueInventarios.cod_cuenta_ingreso;
                        pcod_cuenta_ingreso.Direction = ParameterDirection.Input;
                        pcod_cuenta_ingreso.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_ingreso);

                        DbParameter pcod_cuenta_gasto = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_gasto.ParameterName = "p_cod_cuenta";
                        if (pParCueInventarios.cod_cuenta_gasto == null)
                            pcod_cuenta_gasto.Value = DBNull.Value;
                        else
                            pcod_cuenta_gasto.Value = pParCueInventarios.cod_cuenta_gasto;
                        pcod_cuenta_gasto.Direction = ParameterDirection.Input;
                        pcod_cuenta_gasto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_gasto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PARCUEINV_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParCueInventarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventariosData", "ModificarParCueInventarios", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarParCueInventarios(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ParCueInventarios pParCueInventarios = new ParCueInventarios();
                        pParCueInventarios = ConsultarParCueInventarios(pId, vUsuario);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParCueInventarios.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PARCUEINV_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParCueInventariosData", "EliminarParCueInventarios", ex);
                    }
                }
            }
        }

        public List<ivimpuesto> ListarImpuestos(ivimpuesto pIVProducto, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivimpuesto> lstProducto = new List<ivimpuesto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.* FROM IV_IMPUESTO g " + ObtenerFiltro(pIVProducto, "g.") + " ORDER BY g.ID_IMPUESTO ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivimpuesto entidad = new ivimpuesto();
                            if (resultado["ID_IMPUESTO"] != DBNull.Value) entidad.id_impuesto = Convert.ToInt64(resultado["ID_IMPUESTO"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToString(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_VALOR"] != DBNull.Value) entidad.tipo_valor = Convert.ToInt32(resultado["TIPO_VALOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarImpuestos", ex);
                        return null;
                    }
                }
            }
        }

        public ParCueIvimpuesto ModificarParCueIvimpuesto(ParCueIvimpuesto pParCueIvimpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParCueIvimpuesto.idparametro;
                        pidparametro.Direction = ParameterDirection.InputOutput;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pid_impuesto = cmdTransaccionFactory.CreateParameter();
                        pid_impuesto.ParameterName = "p_id_impuesto";
                        if (pParCueIvimpuesto.id_impuesto == 0)
                            pid_impuesto.Value = DBNull.Value;
                        else
                            pid_impuesto.Value = pParCueIvimpuesto.id_impuesto;
                        pid_impuesto.Direction = ParameterDirection.Input;
                        pid_impuesto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pid_impuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pParCueIvimpuesto.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pParCueIvimpuesto.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pParCueIvimpuesto.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pParCueIvimpuesto.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pParCueIvimpuesto.tipo == int.MinValue)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pParCueIvimpuesto.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PARCUEIMP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParCueIvimpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventariosData", "ModificarParCueIvimpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public void ConsultarParametroImpContable(Int64 pID_impuesto, ref string pCod_Cuenta, ref string pCod_Cuenta_Niif, Usuario pUsuario)
        {
            DbDataReader resultado;
            pCod_Cuenta = "";
            pCod_Cuenta_Niif = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.* FROM par_cue_ivimpuestos  g WHERE g.id_impuesto = " + pID_impuesto + " AND (g.tipo = 1 OR g.tipo IS NULL)";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) pCod_Cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) pCod_Cuenta_Niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarParametroContable", "ConsultarParametroImpContable", ex);
                        return;
                    }
                }
            }
        }

        public List<ivmovimiento> ListarMovimiento(ivmovimiento pMovimiento, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivmovimiento> lstProducto = new List<ivmovimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With TablaValores As (
                                        Select d.id_movimiento, D.tipo_Movimiento, D.id_Almacen, a.almacenname, D.fecha_Movimiento, d.id_proveedor, 0 As costo_promedio, a.centro_costos, Sum(m.Precio_Total + NVL(m.valor_impuesto, 0)) As total, Sum(m.valor_impuesto) As total_impuesto, ' ' As numero_factura, d.idconinventario
                                        From iv_Movimiento d Inner Join Iv_movimiento_detalle m on M.id_Movimiento = d.id_movimiento
                                        Inner Join iv_producto p on p.Productoid = m.id_producto Inner Join iv_almacen a On d.id_almacen = a.almacenid
                                        Group By d.id_movimiento, D.tipo_Movimiento, D.id_Almacen, a.almacenname, D.fecha_Movimiento, d.id_proveedor, a.centro_costos, d.idconinventario
                                        Union all
                                        Select D.ordencompra_Id, o.tipo_movimiento*10, O.almacenid, a.almacenname, O.fecha_Facturacion, o.per_documento, 0, a.centro_costos, Sum(D.precio_total), Sum(Nvl(Round(D.precio_total*im.valor/100), 0)), To_Char(o.numero_factura), o.idconinventario
                                        From Iv_Ordencompra o Inner Join Iv_Ordencompra_Detalle d on O.ordencompra_Id = D.ordencompra_Id 
                                        Inner Join iv_producto p on p.Productoid = D.id_Producto Inner Join iv_almacen a On O.almacenid = a.almacenid Left Join iv_impuesto im On p.id_impuesto_compra = im.id_impuesto
                                        Group By D.ordencompra_Id, o.tipo_movimiento, O.almacenid, a.almacenname, O.fecha_Facturacion, o.per_documento, a.centro_costos, To_Char(o.numero_factura), o.idconinventario
                                        Union all
                                        Select x.id_consignacion, -1, x.almacenid_consignacion, a.almacenname, x.fecha_consignacion, u.usuario, 0, 1, x.valor_consignacion, 0, ' ', 0
                                        From iv_consignacion x Join iv_almacen a On x.almacenid_consignacion = a.almacenid Join iv_usuarios u On u.id_usuario = x.id_usuario_consignacion
                                        )  
                                        Select t.*, pe.per_nombre, Case When t.tipo_movimiento In (-1) Then 'Consignaciòn' When t.tipo_movimiento In (2, 6) Then 'Entrada' When t.tipo_movimiento In (3) Then 'Venta' When t.tipo_movimiento In (4) Then 'Traslado' When t.tipo_movimiento In (5) Then 'Salida' When t.tipo_movimiento In (20) Then 'Orden de Compra' End As nom_tipo_movimiento
                                        From TablaValores t  Left Join iv_personas pe On pe.per_documento = t.id_proveedor " + ObtenerFiltro(pMovimiento, "t.") + @"
                                        Order by t.fecha_movimiento, Nvl(t.idconinventario, 0) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivmovimiento entidad = new ivmovimiento();
                            if (resultado["ID_MOVIMIENTO"] != DBNull.Value) entidad.id_movimiento = Convert.ToInt64(resultado["ID_MOVIMIENTO"]);
                            if (resultado["TIPO_MOVIMIENTO"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToInt32(resultado["TIPO_MOVIMIENTO"]);
                            if (resultado["ID_ALMACEN"] != DBNull.Value) entidad.id_almacen = Convert.ToInt32(resultado["ID_ALMACEN"]);
                            if (resultado["ALMACENNAME"] != DBNull.Value) entidad.almacenname = Convert.ToString(resultado["ALMACENNAME"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["COSTO_PROMEDIO"] != DBNull.Value) entidad.total_costo = Convert.ToDecimal(resultado["COSTO_PROMEDIO"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["TOTAL"]);
                            if (resultado["TOTAL_IMPUESTO"] != DBNull.Value) entidad.total_impuesto = Convert.ToDecimal(resultado["TOTAL_IMPUESTO"]);
                            if (resultado["NOM_TIPO_MOVIMIENTO"] != DBNull.Value) entidad.nom_tipo_movimiento = Convert.ToString(resultado["NOM_TIPO_MOVIMIENTO"]);
                            if (resultado["ID_PROVEEDOR"] != DBNull.Value) entidad.id_persona = Convert.ToString(resultado["ID_PROVEEDOR"]);
                            if (resultado["PER_NOMBRE"] != DBNull.Value) entidad.nombre_persona = Convert.ToString(resultado["PER_NOMBRE"]);
                            if (resultado["CENTRO_COSTOS"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTOS"]);
                            if (resultado["NUMERO_FACTURA"] != DBNull.Value) entidad.numero_factura = Convert.ToString(resultado["NUMERO_FACTURA"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarMovimiento", ex);
                        return null;
                    }
                }
            }
        }

        public List<ivalmacen> ListarAlmacen(ivalmacen pIVAlmacen, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivalmacen> lstProducto = new List<ivalmacen>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.ALMACENID, g.ALMACENNAME, g.DIRECCION FROM IV_ALMACEN g " + ObtenerFiltro(pIVAlmacen, "g.") + " ORDER BY g.ALMACENID ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivalmacen entidad = new ivalmacen();
                            if (resultado["ALMACENID"] != DBNull.Value) entidad.almacenid = Convert.ToInt64(resultado["ALMACENID"]);
                            if (resultado["ALMACENNAME"] != DBNull.Value) entidad.almacenname = Convert.ToString(resultado["ALMACENNAME"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarAlmacen", ex);
                        return null;
                    }
                }
            }
        }

        public List<ivmovimientodetalle> ListarDetalleMovimiento(Int64 pIDMovimiento, Int32 pTipoMovimiento, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivmovimientodetalle> lstProducto = new List<ivmovimientodetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "";
                            
                        if (pTipoMovimiento == 20)
                            sql = @"SELECT g.ID_DET_ORDENC As ID_DET_MOV, g.ID_PRODUCTO, g.CANTIDAD, g.PRECIO_UNITARIO, g.PRECIO_TOTAL, p.ID_IMPUESTO_COMPRA As IMPUESTO, Nvl(Round(g.PRECIO_TOTAL*im.VALOR/100), 0) As VALOR_IMPUESTO, p.ID_CATEGORIA, 0 As COSTO_PROMEDIO, p.ID_IMPUESTO_COMPRA As ID_IMPUESTO
                                        FROM IV_ORDENCOMPRA_DETALLE g LEFT JOIN IV_PRODUCTO p ON g.ID_PRODUCTO = p.PRODUCTOID LEFT JOIN IV_IMPUESTO IM ON p.ID_IMPUESTO_COMPRA = im.ID_IMPUESTO
                                        WHERE g.ORDENCOMPRA_ID = " + pIDMovimiento.ToString() + " ORDER BY g.ID_DET_ORDENC ";                        
                        else
                            sql = @"SELECT g.ID_DET_MOV, g.ID_PRODUCTO, g.CANTIDAD, g.PRECIO_UNITARIO, g.PRECIO_TOTAL, g.IMPUESTO, g.VALOR_IMPUESTO, p.ID_CATEGORIA, g.COSTO_PROMEDIO*g.CANTIDAD As COSTO_PROMEDIO, g.ID_IMPUESTO
                                        FROM IV_MOVIMIENTO_DETALLE g LEFT JOIN IV_MOVIMIENTO d ON d.ID_MOVIMIENTO = g.ID_MOVIMIENTO LEFT JOIN IV_PRODUCTO p ON g.ID_PRODUCTO = p.PRODUCTOID 
                                        WHERE g.ID_MOVIMIENTO = " + pIDMovimiento.ToString() + " ORDER BY g.ID_DET_MOV ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivmovimientodetalle entidad = new ivmovimientodetalle();
                            if (resultado["ID_DET_MOV"] != DBNull.Value) entidad.id_det_mov = Convert.ToInt64(resultado["ID_DET_MOV"]);
                            if (resultado["ID_PRODUCTO"] != DBNull.Value) entidad.id_producto = Convert.ToString(resultado["ID_PRODUCTO"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToDouble(resultado["CANTIDAD"]);
                            if (resultado["PRECIO_UNITARIO"] != DBNull.Value) entidad.precio_unitario = Convert.ToDecimal(resultado["PRECIO_UNITARIO"]);
                            if (resultado["PRECIO_TOTAL"] != DBNull.Value) entidad.precio_total = Convert.ToDecimal(resultado["PRECIO_TOTAL"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToDecimal(resultado["IMPUESTO"]);
                            if (resultado["VALOR_IMPUESTO"] != DBNull.Value) entidad.valor_impuesto = Convert.ToDecimal(resultado["VALOR_IMPUESTO"]);
                            if (resultado["ID_CATEGORIA"] != DBNull.Value) entidad.id_categoria = Convert.ToInt64(resultado["ID_CATEGORIA"]);
                            if (resultado["COSTO_PROMEDIO"] != DBNull.Value) entidad.costo_promedio = Convert.ToDecimal(resultado["COSTO_PROMEDIO"]);
                            if (resultado["ID_IMPUESTO"] != DBNull.Value) entidad.id_impuesto = Convert.ToInt64(resultado["ID_IMPUESTO"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarAlmacen", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 ContabilizaMovimiento(ivmovimientodetalle pMovimiento, Usuario vUsuario)
        {
            Int64 _codigo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_num_comp = cmdTransaccionFactory.CreateParameter();
                        p_num_comp.ParameterName = "p_num_comp";
                        p_num_comp.Value = pMovimiento.num_comp;
                        p_num_comp.Direction = ParameterDirection.Input;
                        p_num_comp.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_num_comp);

                        DbParameter p_tipo_comp = cmdTransaccionFactory.CreateParameter();
                        p_tipo_comp.ParameterName = "p_tipo_comp";
                        if (pMovimiento.id_movimiento == Int64.MinValue)
                            p_tipo_comp.Value = DBNull.Value;
                        else
                            p_tipo_comp.Value = pMovimiento.tipo_comp;
                        p_tipo_comp.Direction = ParameterDirection.Input;
                        p_tipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_comp);

                        DbParameter p_id_det_mov = cmdTransaccionFactory.CreateParameter();
                        p_id_det_mov.ParameterName = "p_id_det_mov";
                        p_id_det_mov.Value = pMovimiento.id_det_mov;
                        p_id_det_mov.Direction = ParameterDirection.Input;
                        p_id_det_mov.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_id_det_mov);

                        DbParameter p_id_movimiento = cmdTransaccionFactory.CreateParameter();
                        p_id_movimiento.ParameterName = "p_id_movimiento";
                        if (pMovimiento.id_movimiento == Int64.MinValue)
                            p_id_movimiento.Value = DBNull.Value;
                        else
                            p_id_movimiento.Value = pMovimiento.id_movimiento;
                        p_id_movimiento.Direction = ParameterDirection.Input;
                        p_id_movimiento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_id_movimiento);

                        DbParameter p_id_tipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        p_id_tipo_movimiento.ParameterName = "p_id_tipo_movimiento";
                        if (pMovimiento.id_tipo_movimiento == Int64.MinValue)
                            p_id_tipo_movimiento.Value = DBNull.Value;
                        else
                            p_id_tipo_movimiento.Value = pMovimiento.id_tipo_movimiento;
                        p_id_tipo_movimiento.Direction = ParameterDirection.Input;
                        p_id_tipo_movimiento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_id_tipo_movimiento);

                        DbParameter p_tercero = cmdTransaccionFactory.CreateParameter();
                        p_tercero.ParameterName = "p_tercero";
                        if (pMovimiento.id_categoria == null)
                            p_tercero.Value = DBNull.Value;
                        else
                            p_tercero.Value = pMovimiento.tercero;
                        p_tercero.Direction = ParameterDirection.Input;
                        p_tercero.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tercero);

                        DbParameter p_id_categoria = cmdTransaccionFactory.CreateParameter();
                        p_id_categoria.ParameterName = "p_id_categoria";
                        if (pMovimiento.id_categoria == null)
                            p_id_categoria.Value = DBNull.Value;
                        else
                            p_id_categoria.Value = pMovimiento.id_categoria;
                        p_id_categoria.Direction = ParameterDirection.Input;
                        p_id_categoria.DbType = DbType.AnsiStringFixedLength;
                        cmdTransaccionFactory.Parameters.Add(p_id_categoria);

                        DbParameter p_id_producto = cmdTransaccionFactory.CreateParameter();
                        p_id_producto.ParameterName = "p_id_producto";
                        if (pMovimiento.id_producto.Trim() == "")
                            p_id_producto.Value = DBNull.Value;
                        else
                            p_id_producto.Value = pMovimiento.id_producto;
                        p_id_producto.Direction = ParameterDirection.Input;
                        p_id_producto.DbType = DbType.AnsiStringFixedLength;
                        p_id_producto.Size = 50;
                        cmdTransaccionFactory.Parameters.Add(p_id_producto);

                        DbParameter p_cantidad = cmdTransaccionFactory.CreateParameter();
                        p_cantidad.ParameterName = "p_cantidad";
                        if (pMovimiento.cantidad == double.MinValue)
                            p_cantidad.Value = DBNull.Value;
                        else
                            p_cantidad.Value = pMovimiento.cantidad;
                        p_cantidad.Direction = ParameterDirection.Input;
                        p_cantidad.DbType = DbType.Double;
                        cmdTransaccionFactory.Parameters.Add(p_cantidad);

                        DbParameter p_precio_unitario = cmdTransaccionFactory.CreateParameter();
                        p_precio_unitario.ParameterName = "p_precio_unitario";
                        if (pMovimiento.precio_unitario == decimal.MinValue)
                            p_precio_unitario.Value = DBNull.Value;
                        else
                            p_precio_unitario.Value = pMovimiento.precio_unitario;
                        p_precio_unitario.Direction = ParameterDirection.Input;
                        p_precio_unitario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_precio_unitario);

                        DbParameter p_precio_total = cmdTransaccionFactory.CreateParameter();
                        p_precio_total.ParameterName = "p_precio_total";
                        if (pMovimiento.precio_total == decimal.MinValue)
                            p_precio_total.Value = DBNull.Value;
                        else
                            p_precio_total.Value = pMovimiento.precio_total;
                        p_precio_total.Direction = ParameterDirection.Input;
                        p_precio_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_precio_total);

                        DbParameter p_id_impuesto = cmdTransaccionFactory.CreateParameter();
                        p_id_impuesto.ParameterName = "p_id_impuesto";
                        if (pMovimiento.impuesto == Int64.MinValue)
                            p_id_impuesto.Value = DBNull.Value;
                        else
                            p_id_impuesto.Value = pMovimiento.id_impuesto;
                        p_id_impuesto.Direction = ParameterDirection.Input;
                        p_id_impuesto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_id_impuesto);


                        DbParameter p_impuesto = cmdTransaccionFactory.CreateParameter();
                        p_impuesto.ParameterName = "p_impuesto";
                        if (pMovimiento.impuesto == decimal.MinValue)
                            p_impuesto.Value = DBNull.Value;
                        else
                            p_impuesto.Value = pMovimiento.impuesto;
                        p_impuesto.Direction = ParameterDirection.Input;
                        p_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_impuesto);

                        DbParameter p_valor_impuesto = cmdTransaccionFactory.CreateParameter();
                        p_valor_impuesto.ParameterName = "p_valor_impuesto";
                        if (pMovimiento.valor_impuesto == decimal.MinValue)
                            p_valor_impuesto.Value = DBNull.Value;
                        else
                            p_valor_impuesto.Value = pMovimiento.valor_impuesto;
                        p_valor_impuesto.Direction = ParameterDirection.Input;
                        p_valor_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valor_impuesto);

                        DbParameter p_costo_promedio = cmdTransaccionFactory.CreateParameter();
                        p_costo_promedio.ParameterName = "p_costo_promedio";
                        if (pMovimiento.costo_promedio == decimal.MinValue)
                            p_costo_promedio.Value = DBNull.Value;
                        else
                            p_costo_promedio.Value = pMovimiento.costo_promedio;
                        p_costo_promedio.Direction = ParameterDirection.Input;
                        p_costo_promedio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_costo_promedio);

                        DbParameter p_id_factura = cmdTransaccionFactory.CreateParameter();
                        p_id_factura.ParameterName = "p_factura";
                        if (pMovimiento.factura == null)
                            p_id_factura.Value = DBNull.Value;
                        else
                            p_id_factura.Value = pMovimiento.factura;
                        p_id_factura.Direction = ParameterDirection.Input;
                        p_id_factura.DbType = DbType.AnsiStringFixedLength;
                        p_id_factura.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(p_id_factura);

                        DbParameter p_codigo = cmdTransaccionFactory.CreateParameter();
                        p_codigo.ParameterName = "p_codigo";
                        p_codigo.Value = pMovimiento.codigo;
                        p_codigo.Direction = ParameterDirection.InputOutput;
                        p_codigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_INVENTARIOS";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_codigo.Value != null)
                            pMovimiento.codigo = Convert.ToInt64(p_codigo.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return _codigo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventariosData", "ContabilizaMovimiento", ex);
                        return _codigo;
                    }
                }
            }
        }


        public Int64? ConsultarPersona(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64? cod_persona = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.cod_persona FROM persona g WHERE g.identificacion = '" + pIdentificacion.Trim() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_persona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarParametroContable", "ConsultarPersona", ex);
                        return cod_persona;
                    }
                }
            }
        }

        public Xpinn.Contabilidad.Entities.ProcesoContable ProcesoContable(Int64 pCodProceso, Usuario pUsuario)
        {
            DbDataReader resultado;
            Xpinn.Contabilidad.Entities.ProcesoContable _proceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.TIPO_COMP, g.CONCEPTO, g.COD_CUENTA, g.TIPO_MOV FROM PROCESO_CONTABLE g WHERE g.COD_PROCESO = " + pCodProceso + " ORDER BY g.COD_PROCESO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_COMP"] != DBNull.Value) _proceso.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) _proceso.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) _proceso.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) _proceso.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return _proceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ProcesoContable", ex);
                        return null;
                    }
                }
            }
        }

        public Int64? DeterminarCiudad(Int64 pCodOfi, Usuario pUsuario)
        {
            DbDataReader resultado;
            Int64? _ciudad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.COD_CIUDAD FROM OFICINA g WHERE g.COD_OFICINA = " + pCodOfi;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CIUDAD"] != DBNull.Value) _ciudad = Convert.ToInt64(resultado["COD_CIUDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return _ciudad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ProcesoContable", ex);
                        return null;
                    }
                }
            }
        }

        public List<ivpersonas_autoret> ListarRetencion(string pPerDocumento, decimal pBase, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivpersonas_autoret> lstProducto = new List<ivpersonas_autoret>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.TIPO_RETENCION, g.DESCRIPCION, g.PORCENTAJE FROM IV_PERSONAS_AUTORET g  WHERE g.PER_DOCUMENTO = '" + pPerDocumento + "' ORDER BY g.TIPO_RETENCION ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivpersonas_autoret entidad = new ivpersonas_autoret();
                            entidad.per_documento = pPerDocumento;
                            if (resultado["TIPO_RETENCION"] != DBNull.Value) entidad.tipo_retencion = Convert.ToInt32(resultado["TIPO_RETENCION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            entidad.valor = Math.Round(pBase * (entidad.porcentaje / 100));
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarRetencion", ex);
                        return null;
                    }
                }
            }
        }

        public List<ivconceptos> ListarConceptos(ivconceptos pConceptos, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivconceptos> lstProducto = new List<ivconceptos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.ID_CONCEPTO, g.NOMBRE, g.BASE_MINIMA, g.PORCENTAJE_CALCULO FROM IV_CONCEPTOS g " + ObtenerFiltro(pConceptos, "g.") + " ORDER BY g.ID_CONCEPTO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivconceptos entidad = new ivconceptos();
                            if (resultado["ID_CONCEPTO"] != DBNull.Value) entidad.id_concepto = Convert.ToInt64(resultado["ID_CONCEPTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_CALCULO"] != DBNull.Value) entidad.porcentaje_calculo = Convert.ToDecimal(resultado["PORCENTAJE_CALCULO"]);
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarConceptos", ex);
                        return null;
                    }
                }
            }
        }

        public void ConsultarParametroConContable(Int64 pID_concepto, ref string pCod_Cuenta, ref string pCod_Cuenta_Niif, Usuario pUsuario)
        {
            DbDataReader resultado;
            pCod_Cuenta = "";
            pCod_Cuenta_Niif = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT g.* FROM par_cue_ivimpuestos  g WHERE g.id_impuesto = " + pID_concepto + " AND g.tipo = 0 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) pCod_Cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) pCod_Cuenta_Niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConsultarParametroContable", "ConsultarParametroConContable", ex);
                        return;
                    }
                }
            }
        }


        public List<ivordenconcepto> ListarOrdenConceptos(Int64 pIdOrdenCompra, double pBase, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivordenconcepto> lstProducto = new List<ivordenconcepto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"SELECT g.ID_ORDCON, g.ORDENCOMPRA_ID, g.ID_CONCEPTO, h.NOMBRE, h.BASE_MINIMA, h.PORCENTAJE_CALCULO, h.TIPO 
                                        FROM IV_ORDENCOMPRA_CONCEPTO g JOIN IV_CONCEPTOS h On g.ID_CONCEPTO = h.ID_CONCEPTO WHERE g.ORDENCOMPRA_ID = '" + pIdOrdenCompra + "' ORDER BY g.ID_ORDCON ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ivordenconcepto entidad = new ivordenconcepto();
                            entidad.ordencompra_id = pIdOrdenCompra;
                            if (resultado["ID_ORDCON"] != DBNull.Value) entidad.id_ordcon = Convert.ToInt32(resultado["ID_ORDCON"]);
                            if (resultado["ORDENCOMPRA_ID"] != DBNull.Value) entidad.ordencompra_id = Convert.ToInt32(resultado["ORDENCOMPRA_ID"]);
                            if (resultado["ID_CONCEPTO"] != DBNull.Value) entidad.id_concepto = Convert.ToInt32(resultado["ID_CONCEPTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDouble(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_CALCULO"] != DBNull.Value) entidad.porcentaje_calculo = Convert.ToDouble(resultado["PORCENTAJE_CALCULO"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (pBase > entidad.base_minima)
                                entidad.valor = Math.Round(Convert.ToDouble(pBase * (entidad.porcentaje_calculo / 100)));
                            else
                                entidad.valor = 0;
                            lstProducto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarOrdenConceptos", ex);
                        return null;
                    }
                }
            }
        }

        public List<ivdatospago> ListarDatosPago(Int64 pIdMovimiento, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<ivdatospago> lstLista = new List<ivdatospago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select d.id_venta, d.id_forma_pago, d.valor, d.id_venta_realizada 
                                        From iv_datos_pago d Where d.id_venta_realizada In (Select x.id_venta_realizada From iv_movimiento x Where x.id_movimiento = " + pIdMovimiento + ") ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {

                            ivdatospago entidad = new ivdatospago();
                            if (resultado["id_venta"] != DBNull.Value) entidad.id_venta = Convert.ToInt64(resultado["id_venta"]);
                            if (resultado["id_forma_pago"] != DBNull.Value) entidad.id_forma_pago = Convert.ToInt64(resultado["id_forma_pago"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["valor"]);
                            lstLista.Add(entidad);
                        }

                        return lstLista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ListarDatosPago", ex);
                        return null;
                    }
                }
            }
        }

        public bool ConsultarParamtroFPagContable(Int64 pProcesoContable, Int64 pFormaPago, ref string pcod_cuenta, ref string pcod_cuenta_niif, Usuario vUsuario)
        {
            DbDataReader resultado;
            pcod_cuenta = "";
            pcod_cuenta_niif = "";
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT proc.idprocesogiro, proc.forma_pago, proc.cod_cuenta, plan.nombre 
                                        FROM Proceso_Tipo_Giro proc JOIN plan_cuentas plan ON proc.cod_cuenta = plan.cod_cuenta WHERE proc.forma_pago = " + pFormaPago.ToString() + (pProcesoContable == 0 ? "" : " AND proc.cod_proceso = " + pProcesoContable.ToString()) + " ORDER BY proc.idprocesogiro ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                            
                            if (resultado["cod_cuenta"] != DBNull.Value) pcod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InventarioData", "ConsultarParamtroFPagContable", ex);
                        return false;
                    }
                }
            }
        }




    }
}
