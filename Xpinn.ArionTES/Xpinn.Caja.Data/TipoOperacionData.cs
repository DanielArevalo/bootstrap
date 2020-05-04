using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoOperacion
    /// </summary>
    public class TipoOperacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TipoOperacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad procesoOficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public TipoOperacion InsertarFactura(TipoOperacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnum_factu = cmdTransaccionFactory.CreateParameter();
                        pnum_factu.ParameterName = "pnumfactu";
                        pnum_factu.Value = "0000000000";
                        pnum_factu.DbType = DbType.AnsiStringFixedLength;
                        pnum_factu.Size = 10;
                        pnum_factu.Direction = ParameterDirection.InputOutput;

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcodigoope";
                        pcod_ope.Value = pEntidad.cod_operacion;
                        pcod_ope.DbType = DbType.Int64;
                        pcod_ope.Size = 8;
                        pcod_ope.Direction = ParameterDirection.Input;

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcodigopersona";
                        pcod_usuario.Value = pUsuario.codusuario;
                        pcod_usuario.Size = 8;
                        pcod_usuario.DbType = DbType.Int64;
                        pcod_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnum_factu);
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_INSERTAR_FACTURA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.num_factura = pnum_factu.Value.ToString();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "InsertarFactura", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Verifica si una caja tiene tipos de operaciòn asignados
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public TipoOperacion ConsultarTipOpeCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion entidad = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo FROM atribuciones_caja WHERE tipo_operacion = " + pId.ToString() + " AND cod_caja = " + pCaja;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["conteo"] != DBNull.Value) entidad.conteo = long.Parse(resultado["conteo"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de Tipos de Transacciones dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoOperacion obtenidas</returns>
        public List<TipoOperacion> ListarTipoTransaccion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        //se cambia por que se debe utilizar la tabla TIPO_TRAN para este proceso
                        string sql = " SELECT tipo_tran cod_operacion, descripcion nombre FROM tipo_tran  WHERE tipo_caja = 1 ORDER BY tipo_tran asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_operacion"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["cod_operacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["nombre"]);
                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOperacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de tipos de transacciones dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Transaccion por Cajas obtenidas</returns>
        public List<TipoOperacion> ListarTipoOpeTransac(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.tipo_mov, a.tipo_tran cod_operacion, a.descripcion nombre FROM tipo_tran a, atribuciones_caja b WHERE a.tipo_caja = 1 AND a.tipo_tran = b.tipo_operacion AND b.cod_caja = " + pEntidad.cod_caja + " and a.tipo_mov= " + pEntidad.tipo_movimiento;
                        if (pEntidad.tipo_producto > 0)
                            sql = sql + " AND a.tipo_producto = " + pEntidad.tipo_producto;
                        sql = sql + " ORDER BY a.tipo_tran asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_operacion"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["cod_operacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                           
                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpeTransac", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVent(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.tipo_tran cod_operacion, a.descripcion nombre FROM tipo_tran a WHERE a.tipo_caja = 1 ";
                        if (pEntidad.tipo_producto > 0)
                            sql = sql + " AND a.tipo_producto = " + pEntidad.tipo_producto;
                        sql = sql + " ORDER BY a.tipo_tran asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_operacion"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["cod_operacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);

                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpeTransac", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla TipoOperacion asociado con e tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>TipoOperacion-TipoTransaccion Caja consultada</returns>
        public TipoOperacion ConsultarTipOpeTranCaja(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion entidad = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = "SELECT a.tipo_mov tipMov, a.tipo_producto tipoProd, (Select t.descripcion From TipoProductoCaja t WHERE t.codtipoproductocaja = a.tipo_producto) nomtipoprod FROM tipo_tran a WHERE a.tipo_tran = " + pEntidad.cod_operacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["tipoProd"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["tipoProd"].ToString());
                            if (resultado["nomtipoprod"] != DBNull.Value) entidad.nom_tipo_operacion = Convert.ToString(resultado["nomtipoprod"].ToString());
                            if (resultado["tipMov"] != DBNull.Value) entidad.tipo_movimiento = long.Parse(resultado["tipMov"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeTranCaja", ex);
                        return null;
                    }
                }
            }
        }

       
        public TipoOperacion ConsultarTipoOperacion(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion entidad = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select tipo_producto , tipo_mov, (select descripcion from tipoproductocaja where codtipoproductocaja = tipo_producto) nomtipoprod from tipo_tran t where tipo_caja = 1 and tipo_tran = " + pEntidad.cod_operacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["tipo_producto"].ToString());
                            if (resultado["nomtipoprod"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["nomtipoprod"].ToString());
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToInt64(resultado["tipo_mov"].ToString());
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeTranCaja", ex);
                        return null;
                    }
                }
            }
        }

        public string ParametroGeneral(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string valor = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //la anterior consulta ue cambiada por la vista que sigue
                        string sql = " select * from general where codigo = 9";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"].ToString());
                        }
                        return valor;
                        
                    }
                    catch 
                    {
                        return valor;
                    }
                }
            }

        }


        public List<TipoOperacion> ConsultarTranCreditosboucher(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTranCred = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   
                        //la anterior consulta ue cambiada por la vista que sigue
                        string sql = " select * from V_DATOS_FACTURA_CAJATOT where cod_ope=" + pEntidad.cod_operacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            entidad.cod_operacion = pEntidad.cod_operacion;
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["VALOR_TRAN"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR_TRAN"]);
                            if (resultado["RADICADO"] != DBNull.Value) entidad.nro_producto = Convert.ToString(resultado["RADICADO"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["MONEDA"]);
                            entidad = ConsultarDatosOperacion(entidad, pUsuario);

                            lstTranCred.Add(entidad);
                        }

                        return lstTranCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTranCred", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Tipo Producto asociado con e tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Tipo Tran -Tipo Producto </returns>
        public List<TipoOperacion> ConsultarTranCred(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTranCred = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        //la anterior consulta ue cambiada por la vista que sigue
                        string sql = " select * from v_datos_factura_caja where cod_ope=" + pEntidad.cod_operacion;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        bool bEncontro = false;
                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            entidad.cod_operacion = pEntidad.cod_operacion;
                            if (resultado["concepto"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["concepto"].ToString());
                            if (resultado["valor_tran"] != DBNull.Value) entidad.valor = long.Parse(resultado["valor_tran"].ToString());
                            if (resultado["radicado"] != DBNull.Value) entidad.nro_producto = Convert.ToString(resultado["radicado"].ToString());
                            if (resultado["moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["moneda"].ToString());
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"].ToString());
                            entidad = ConsultarDatosOperacion(entidad, pUsuario);
                            bEncontro = true;
                            lstTranCred.Add(entidad);
                        }
                        if (!bEncontro)
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            entidad.cod_operacion = pEntidad.cod_operacion;
                            entidad = ConsultarDatosOperacion(entidad, pUsuario);                            
                            lstTranCred.Add(entidad);
                        }
                        return lstTranCred;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTranCred", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla Tipo Producto asociado con e tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Tipo Tran -Tipo Producto </returns>
        public TipoOperacion ConsultarValIva(TipoOperacion pEntidad, Usuario pUsuario)
        {
            //DbDataReader resultado = default(DbDataReader);
            TipoOperacion entidad = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcod_ope";
                        pcod_ope.Value = pEntidad.cod_operacion;
                        pcod_ope.DbType = DbType.Int64;
                        pcod_ope.Size = 8;
                        pcod_ope.Direction = ParameterDirection.Input;

                        DbParameter pvalor_iva = cmdTransaccionFactory.CreateParameter();
                        pvalor_iva.ParameterName = "pvalor_iva";
                        pvalor_iva.Value = pEntidad.valor_iva;
                        pvalor_iva.DbType = DbType.Int64;
                        pvalor_iva.Size = 8;
                        pvalor_iva.Direction = ParameterDirection.InputOutput;

                        DbParameter pvalor_base = cmdTransaccionFactory.CreateParameter();
                        pvalor_base.ParameterName = "pvalor_base";
                        pvalor_base.Value = pEntidad.valor_base;
                        pvalor_base.DbType = DbType.Int64;
                        pvalor_base.Size = 8;
                        pvalor_base.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
                        cmdTransaccionFactory.Parameters.Add(pvalor_iva);
                        cmdTransaccionFactory.Parameters.Add(pvalor_base);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = " USP_XPINN_TES_VALORIVAFACTURA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        entidad.cod_operacion = Convert.ToString(pcod_ope.Value);
                        entidad.valor_iva = Convert.ToInt64(pvalor_iva.Value);
                        entidad.valor_base = Convert.ToInt64(pvalor_base.Value);

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarValIva", ex);
                        return null;
                    }
                }
            }
        }


        // ---------------------------------------------------------------------------------------------------------------------------------- //

        /// <summary>
        /// Obtiene un registro de la tabla Tipo Producto asociado con el tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Tipo Tran -Tipo Producto </returns>
        public List<TipoOperacion> ListarTipoProducto(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipoOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select tp.codtipoproductocaja, tp.descripcion From tipoproductocaja tp Order by codtipoproductocaja ";
                     
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["codtipoproductocaja"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["codtipoproductocaja"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeTranCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// Obtiene un registro de la tabla Tipo Producto asociado con el tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Tipo Tran -Tipo Producto </returns>
        public List<TipoOperacion> ListarTipoProductoCaja(Usuario pUsuario, Int64 caja)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipoOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql1 = "Select DISTINCT(tp.codtipoproductocaja), tp.descripcion From tipoproductocaja tp INNER join tipo_tran tt on tp.codtipoproductocaja=tt.TIPO_PRODUCTO where  tt.tipo_tran in(select tipo_operacion from atribuciones_caja where cod_caja= " + caja + ")";
                        string sql = sql1 + "  Order by codtipoproductocaja ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["codtipoproductocaja"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["codtipoproductocaja"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeTranCaja", ex);
                        return null;
                    }
                }
            }
        }

        public TipoOperacion ConsultarTipoProducto(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion eTipoOpe = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select codtipoproductocaja, descripcion From tipoproductocaja Where codtipoproductocaja = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["codtipoproductocaja"] != DBNull.Value) eTipoOpe.tipo_producto = long.Parse(resultado["codtipoproductocaja"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) eTipoOpe.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());     
                        }
                        return eTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipoProducto", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoOperacion> ListarTipoOpe(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //se cambia por que se debe utilizar la tabla TIPO_TRAN para este proceso
                        string sql = " SELECT tipo_ope, tipo_ope ||'-'|| descripcion as descripcion FROM tipo_ope ORDER BY tipo_ope asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_tipo_operacion = Convert.ToString(resultado["descripcion"]);
                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpe", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoOperacion> ListarTipoOpGrid(Usuario pUsuario , String Filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //se cambia por que se debe utilizar la tabla TIPO_TRAN para este proceso
                        string sql = "select * from tipo_tran t where 1 =1 "+ Filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToInt64(resultado["TIPO_MOV"]);
                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpGrid", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarTipoOpe(Int64 id, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value =id;
                        p_tipo_tran.DbType = DbType.Int64;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPOTRAN_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "EliminarTipoOpe", ex);
                    }

                }
            }
        }

        public void insertTipoOP(TipoOperacion entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = entidad.tipo_tran;
                        p_tipo_tran.DbType = DbType.Int64;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value =entidad.concepto ;
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        p_tipo_producto.ParameterName = "p_tipo_producto";
                        p_tipo_producto.Value = entidad.tipo_producto;
                        p_tipo_producto.DbType = DbType.Int64;
                        p_tipo_producto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_producto);

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        p_tipo_mov.Value = entidad.tipo_movimiento ;
                        p_tipo_mov.DbType = DbType.Int64;
                        p_tipo_mov.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);

                        DbParameter p_tipo_caja = cmdTransaccionFactory.CreateParameter();
                        p_tipo_caja.ParameterName = "p_tipo_caja";
                        if (entidad.cod_caja == 0)
                            p_tipo_caja.Value = DBNull.Value;
                        else
                            p_tipo_caja.Value = entidad.cod_caja;
                        p_tipo_caja.DbType = DbType.Int64;
                        p_tipo_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_caja);

                        DbParameter p_porc_imp = cmdTransaccionFactory.CreateParameter();
                        p_porc_imp.ParameterName = "p_porc_imp";
                        if (entidad.porc_imp == null)
                            p_porc_imp.Value = DBNull.Value;
                        else
                            p_porc_imp.Value = entidad.porc_imp;
                        p_porc_imp.DbType = DbType.Int64;
                        p_porc_imp.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_porc_imp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPOTRAN_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "EliminarTipoOpe", ex);
                    }

                }
            }
        }

        public void ModificaTipoOp(TipoOperacion entidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = entidad.tipo_tran;
                        p_tipo_tran.DbType = DbType.Int64;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = entidad.concepto;
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_tipo_producto = cmdTransaccionFactory.CreateParameter();
                        p_tipo_producto.ParameterName = "p_tipo_producto";
                        p_tipo_producto.Value = entidad.tipo_producto;
                        p_tipo_producto.DbType = DbType.Int64;
                        p_tipo_producto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_producto);

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        p_tipo_mov.Value = entidad.tipo_movimiento;
                        p_tipo_mov.DbType = DbType.Int64;
                        p_tipo_mov.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);

                        DbParameter p_tipo_caja = cmdTransaccionFactory.CreateParameter();
                        p_tipo_caja.ParameterName = "p_tipo_caja";
                        if (entidad.cod_caja == 0)
                            p_tipo_caja.Value = DBNull.Value;
                        else
                            p_tipo_caja.Value = entidad.cod_caja;
                        p_tipo_caja.DbType = DbType.Int64;
                        p_tipo_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_caja);

                        DbParameter p_porc_imp = cmdTransaccionFactory.CreateParameter();
                        p_porc_imp.ParameterName = "p_porc_imp";
                        p_porc_imp.Value = DBNull.Value;
                        p_porc_imp.DbType = DbType.Int64;
                        p_porc_imp.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_porc_imp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPOTRAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "EliminarTipoOpe", ex);
                    }

                }
            }
        }



        public List<TipoOperacion> validaDatos(Usuario pUsuario, Int64 TipoTra, String desc, Int64 opc)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();
            string sql = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //se cambia por que se debe utilizar la tabla TIPO_TRAN para este proceso
                        switch (opc)
                        {
                            case 1://modificar
                                sql = @"select * from tipo_tran t 
                                        where t.tipo_producto = 7 and t.tipo_tran >= 1000  and t.descripcion = '" + desc + "' ";
                                break;
                            case 2://Insertar
                                sql = @"select * from tipo_tran t 
                                        where t.tipo_producto = 7 and t.tipo_tran >= 1000  and t.descripcion = '" + desc + "' ";
                                break;
                            case 3 :
                                sql = @"select * from tipo_tran t 
                                        where t.tipo_tran = " + TipoTra;
                                break;
                        }
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpGrid", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Tipo Producto asociado con el tipo de transaccion de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Tipo Tran -Tipo Producto </returns>
        public List<TipoOperacion> ListarTipoProductoAhorros(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipoOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select tp.cod_tipo_producto, tp.descripcion From tipoproducto tp where  cod_tipo_producto not in (3)  Order by cod_tipo_producto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.tipo_producto = long.Parse(resultado["cod_tipo_producto"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());
                            lstTipoOpe.Add(entidad);
                        }
                        return lstTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipOpeTranCaja", ex);
                        return null;
                    }
                }
            }
        }

        public TipoOperacion ConsultarTipoProductoAhorros(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion eTipoOpe = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                    //    string sql = "Select codtipoproductocaja, descripcion From tipoproductocaja Where codtipoproductocaja = " + pId;
                        string sql = "Select tp.cod_tipo_producto, tp.descripcion From tipoproducto tp where  cod_tipo_producto not in (3) and  cod_tipo_producto = " + pId;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_tipo_producto"] != DBNull.Value) eTipoOpe.tipo_producto = long.Parse(resultado["cod_tipo_producto"].ToString());
                            if (resultado["descripcion"] != DBNull.Value) eTipoOpe.nom_tipo_producto = Convert.ToString(resultado["descripcion"].ToString());
                        }
                        return eTipoOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTipoProducto", ex);
                        return null;
                    }
                }
            }
        }

        public TipoOperacion ConsultarDatosOperacion(TipoOperacion pTipoOperacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //la anterior consulta ue cambiada por la vista que sigue
                        string sql = @"SELECT op.FECHA_OPER, ofi.NOMBRE as NOMBRE_OFICINA, caj.NOMBRE as NOMBRE_CAJA, 
                                        (Select u.nombre From  usuarios u  where op.COD_USU = U.codusuario) AS NOMBRE_CAJERO,
                                        (Select Max(cod_persona) From MOVIMIENTOCAJA trans Where trans.cod_ope = op.cod_ope) AS COD_PERSONA,
                                        op.OBSERVACION,op.num_comp,op.tipo_comp,tcom.DESCRIPCION
                                        FROM Operacion op
                                        LEFT JOIN Tipo_COMP tcom on tcom.tipo_comp = op.tipo_comp
                                        LEFT JOIN Oficina ofi on op.cod_ofi = ofi.cod_oficina
                                        LEFT JOIN caja caj on op.cod_caja = caj.cod_caja
                                        WHERE op.cod_ope = " + pTipoOperacion.cod_operacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FECHA_OPER"] != DBNull.Value) pTipoOperacion.fecha_operacion = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) pTipoOperacion.nombre_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["NOMBRE_CAJA"] != DBNull.Value) pTipoOperacion.nombre_caja = Convert.ToString(resultado["NOMBRE_CAJA"]);
                            if (resultado["NOMBRE_CAJERO"] != DBNull.Value) pTipoOperacion.nombre_cajero = Convert.ToString(resultado["NOMBRE_CAJERO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) pTipoOperacion.cod_persona = Convert.ToString(resultado["COD_PERSONA"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) pTipoOperacion.observaciones = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["num_comp"] != DBNull.Value) pTipoOperacion.num_comp = Convert.ToString(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) pTipoOperacion.tipo_comp = Convert.ToString(resultado["tipo_comp"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) pTipoOperacion.tipo_comp_desc = Convert.ToString(resultado["DESCRIPCION"]);
                        }

                        return pTipoOperacion;
                    }
                    catch 
                    {
                        return pTipoOperacion;
                    }
                }
            }

        }


        public string ConsultarFactura(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string num_factura = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT NUM_FACTURA, CODIGO_FACTURA FROM FACTURA WHERE COD_OPE = " + pId.ToString() + " ORDER BY CODIGO_FACTURA DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["NUM_FACTURA"] != DBNull.Value) num_factura = Convert.ToString(resultado["NUM_FACTURA"].ToString());
                        }

                        return num_factura;
                    }
                    catch 
                    {
                        return num_factura;
                    }
                }
            }
        }

        public List<TipoOperacion> ConsultarSaldoProductos(Int64 pCodOpe, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstProductos = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        string sql = "";
                        cmdTransaccionFactory.Connection = connection;
                        // Determinar productos de aportes
                        sql = "Select a.numero_aporte, a.saldo from aporte a where a.numero_aporte In (Select t.numero_aporte From tran_aporte t Where t.cod_ope = " + pCodOpe.ToString() + " And t.numero_aporte  = a.numero_aporte) ";                                                
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            entidad.nom_tipo_producto = "Aportes";
                            if (resultado["numero_aporte"] != DBNull.Value) entidad.nro_producto = Convert.ToString(resultado["numero_aporte"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["saldo"].ToString());
                            lstProductos.Add(entidad);
                        }
                        // Determinar productos de créditos
                        sql = "Select a.numero_radicacion, a.saldo_capital from credito a where a.numero_radicacion In (Select t.numero_radicacion From tran_cred t Where t.cod_ope = " + pCodOpe.ToString() + " And t.numero_radicacion  = a.numero_radicacion) ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            entidad.nom_tipo_producto = "Creditos";
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.nro_producto = Convert.ToString(resultado["numero_radicacion"].ToString());
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["saldo_capital"].ToString());
                            lstProductos.Add(entidad);
                        }
                        // Determinar productos de ahorros
                        sql = "Select a.numero_cuenta, a.saldo_total from ahorro_vista a where a.numero_cuenta In (Select t.numero_cuenta From tran_ahorro t Where t.cod_ope = " + pCodOpe.ToString() + " And t.numero_cuenta  = a.numero_cuenta) ";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            entidad.nom_tipo_producto = "Ahorros";
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.nro_producto = Convert.ToString(resultado["numero_cuenta"].ToString());
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["saldo_total"].ToString());
                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ConsultarTranCred", ex);
                        return null;
                    }
                }
            }
        }
        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.tipo_tran cod_operacion, a.descripcion nombre FROM tipo_tran a WHERE a.tipo_caja = 1 and a.TIPO_PRODUCTO = 2 and a.TIPO_TRAN In (2, 3, 6) ";
                        if (pEntidad.tipo_producto > 0)
                            sql = sql + " AND a.tipo_producto = " + pEntidad.tipo_producto;
                        sql = sql + " ORDER BY a.tipo_tran asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_operacion"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["cod_operacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);

                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpeTransacVentRotativo", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo1(TipoOperacion pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoOperacion> lstTipOpe = new List<TipoOperacion>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT a.tipo_tran cod_operacion, a.descripcion nombre FROM tipo_tran a WHERE a.tipo_caja = 1 and a.TIPO_PRODUCTO = 2 and a.tipo_tran in(2,3) ";                        if (pEntidad.tipo_producto > 0)
                            sql = sql + " AND a.tipo_producto = " + pEntidad.tipo_producto;
                        sql = sql + " ORDER BY a.tipo_tran asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoOperacion entidad = new TipoOperacion();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_operacion"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["cod_operacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);

                            lstTipOpe.Add(entidad);
                        }

                        return lstTipOpe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoOperacionData", "ListarTipoOpeTransacVentRotativo", ex);
                        return null;
                    }
                }
            }
        }



        public TipoOperacion ConsultarFacturaCompleta(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TipoOperacion entidad = new TipoOperacion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM FACTURA WHERE COD_OPE = " + pId.ToString() + " ORDER BY CODIGO_FACTURA DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.num_factura = Convert.ToString(resultado["CODIGO_FACTURA"].ToString());
                            if (resultado["NUM_FACTURA"] != DBNull.Value) entidad.num_factura = Convert.ToString(resultado["NUM_FACTURA"].ToString());
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_operacion = Convert.ToString(resultado["COD_OPE"].ToString());
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToString(resultado["COD_PERSONA"].ToString());
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fecha_operacion = Convert.ToDateTime(resultado["FECHA_FACTURA"].ToString());
                        }

                        return entidad;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

    }
}
