using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class MonederoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MonederoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Entities.Monedero consultarMonedero(int cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado;
            Entities.Monedero entidad = new Entities.Monedero();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from int_monedero where cod_persona = " + cod_persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ID_MONEDERO"] != DBNull.Value) entidad.id_monedero = Convert.ToInt32(resultado["ID_MONEDERO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "consultarMonedero", ex);
                        return null;
                    }
                }
            }
        }

        public Entities.PersonaMonedero consultarPersonaMonedero(string identificacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            Entities.PersonaMonedero entidad = new Entities.PersonaMonedero();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.cod_persona, p.identificacion, p.nombre, P.Celular, P.Email,
                                        M.Id_Monedero, M.Estado
                                        from v_persona p
                                        left join int_monedero m on p.cod_persona = m.cod_persona
                                        where p.identificacion = '"+identificacion+"'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["ID_MONEDERO"] != DBNull.Value) entidad.id_monedero = Convert.ToInt32(resultado["ID_MONEDERO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "consultarPersonaMonedero", ex);
                        return null;
                    }
                }
            }
        }

        public List<Operaciones> consultarOperaciones(bool soloActivos, Usuario pUsuario)
        {
            //INSERT INTO INT_OPERACIONES(Id_Operacion, Nombre, Estado) VALUES(2,'Transferencias',1);
            DbDataReader resultado;
            List<Operaciones> lstResultado = new List<Operaciones>();            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from INT_OPERACIONES";
                        if (soloActivos)
                            sql = sql + " where estado = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Operaciones entidad = new Operaciones();
                            if (resultado["Id_Operacion"] != DBNull.Value) entidad.id_operacion = Convert.ToInt32(resultado["Id_Operacion"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["Estado"]);
                            lstResultado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "consultarOperaciones", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProductoOrigen> consultarProductosOrigen(long cod_persona, Usuario pUsuario)
        {
            //INSERT INTO INT_OPERACIONES(Id_Operacion, Nombre, Estado) VALUES(2,'Transferencias',1);
            DbDataReader resultado;
            List<ProductoOrigen> lstResultado = new List<ProductoOrigen>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select* from
                                      (select '2' as tipo_producto, TO_CHAR(C.Numero_Radicacion) as referencia, L.Nombre as Nombre,
                                      c.Monto_Aprobado - c.Saldo_Capital as Saldo from Credito c
                                      inner join Lineascredito l on C.Cod_Linea_Credito = L.Cod_Linea_Credito
                                      where L.Tipo_Linea = 2
                                      AND C.Estado = 'C'
                                      and(c.Monto_Aprobado - c.Saldo_Capital) > 0
                                      and C.Cod_Deudor = " + cod_persona + @")
                                      Union All
                                      (select '3' as tipo_producto, TO_CHAR(A.Numero_Cuenta) as referencia, H.Descripcion as Nombre, A.Saldo_Total as Saldo
                                      from Ahorro_Vista A
                                      inner
                                      join Lineaahorro h on H.Cod_Linea_Ahorro = A.Cod_Linea_Ahorro
                                      where A.ESTADO NOT IN(4,3,2) AND A.COD_PERSONA = "+ cod_persona + " and A.Saldo_Total > 0)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ProductoOrigen entidad = new ProductoOrigen();
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["tipo_producto"]);
                            if (resultado["referencia"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["referencia"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = entidad.referencia +"-"+ Convert.ToString(resultado["Nombre"]);
                            if (resultado["Saldo"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["Saldo"]);
                            lstResultado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "consultarProductosOrigen", ex);
                        return null;
                    }
                }
            }
        }        

        public Int32 crearMonedero(int cod_persona, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //P_ID_MONEDERO OUT INT_MONEDERO.ID_MONEDERO % TYPE                        
                        DbParameter P_ID_MONEDERO = cmdTransaccionFactory.CreateParameter();
                        P_ID_MONEDERO.ParameterName = "P_ID_MONEDERO";
                        P_ID_MONEDERO.Value = 0;
                        P_ID_MONEDERO.Size = 15;
                        P_ID_MONEDERO.DbType = DbType.Int32;
                        P_ID_MONEDERO.Direction = ParameterDirection.Output;
                                                                        

                        //P_COD_PERSONA INT_MONEDERO.COD_PERSONA % TYPE
                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_ID_MONEDERO);
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_MONEDERO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(P_ID_MONEDERO.Value != null)
                        {
                            return Convert.ToInt32(P_ID_MONEDERO.Value);
                        }                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "crearMonedero", ex);
                        return 0;
                    }
                }
            }
            return 0;
        }       

        public TranMonedero guardarTransaccionMonedero(TranMonedero transac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //P_NUM_TRAN INT_TRAN_MONEDERO.NUM_TRAN % TYPE,
                        DbParameter P_NUM_TRAN = cmdTransaccionFactory.CreateParameter();
                        P_NUM_TRAN.ParameterName = "P_NUM_TRAN";
                        P_NUM_TRAN.DbType = DbType.Int32;
                        if(transac.num_tran != null && transac.num_tran > 0)
                        {
                            P_NUM_TRAN.Direction = ParameterDirection.Input;
                            P_NUM_TRAN.Value = transac.num_tran;
                        }
                        else
                        {
                            P_NUM_TRAN.Direction = ParameterDirection.Output;
                            P_NUM_TRAN.Value = 0;
                        }
                        cmdTransaccionFactory.Parameters.Add(P_NUM_TRAN);

                        //P_COD_PERSONA INT_TRAN_MONEDERO.COD_PERSONA % TYPE,
                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = transac.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        //P_ID_MONEDERO INT_TRAN_MONEDERO.ID_MONEDERO % TYPE,
                        DbParameter P_ID_MONEDERO = cmdTransaccionFactory.CreateParameter();
                        P_ID_MONEDERO.ParameterName = "P_ID_MONEDERO";
                        P_ID_MONEDERO.Value = transac.id_monedero;
                        P_ID_MONEDERO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_MONEDERO);

                        //P_TIPO_TRAN INT_TRAN_MONEDERO.TIPO_TRAN % TYPE,
                        DbParameter P_TIPO_TRAN = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_TRAN.ParameterName = "P_TIPO_TRAN";
                        P_TIPO_TRAN.Value = transac.tipo_tran;
                        P_TIPO_TRAN.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_TRAN);

                        //P_VALOR INT_TRAN_MONEDERO.VALOR % TYPE,
                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = transac.valor;
                        P_VALOR.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        //P_ESTADO INT_TRAN_MONEDERO.ESTADO % TYPE,
                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = transac.estado;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        //P_FECHA INT_TRAN_MONEDERO.FECHA % TYPE,
                        DbParameter P_FECHA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA.ParameterName = "P_FECHA";
                        P_FECHA.Value = transac.fecha == null ? DateTime.Now : transac.fecha;
                        P_FECHA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA);

                        //P_DESCRIPCION INT_TRAN_MONEDERO.DESCRIPCION % TYPE,
                        DbParameter P_DESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION.ParameterName = "P_DESCRIPCION";
                        if (transac.descripcion != null)
                            P_DESCRIPCION.Value = transac.descripcion;
                        else
                            P_DESCRIPCION.Value = DBNull.Value;
                        P_DESCRIPCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION);

                        //P_ID_OPERACION INT_TRAN_MONEDERO.ID_OPERACION % TYPE,
                        DbParameter P_ID_OPERACION = cmdTransaccionFactory.CreateParameter();
                        P_ID_OPERACION.ParameterName = "P_ID_OPERACION";
                        if (transac.id_operacion > 0)
                            P_ID_OPERACION.Value = transac.id_operacion;
                        else
                            P_ID_OPERACION.Value = DBNull.Value;
                        P_ID_OPERACION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_ID_OPERACION);

                        //P_TIPO_CREDITO INT_TRAN_MONEDERO.TIPO_CREDITO % TYPE,
                        DbParameter P_TIPO_CREDITO = cmdTransaccionFactory.CreateParameter();
                        P_TIPO_CREDITO.ParameterName = "P_TIPO_CREDITO";
                        if (transac.tipo_credito > 0)
                            P_TIPO_CREDITO.Value = transac.tipo_credito;
                        else
                            P_TIPO_CREDITO.Value = DBNull.Value;
                        P_TIPO_CREDITO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_TIPO_CREDITO);


                        //P_COD_TIPO_PRODUCTO INT_TRAN_MONEDERO.COD_TIPO_PRODUCTO % TYPE,
                        DbParameter P_COD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_PRODUCTO.ParameterName = "P_COD_TIPO_PRODUCTO";
                        if (transac.cod_tipo_producto > 0)
                            P_COD_TIPO_PRODUCTO.Value = transac.cod_tipo_producto;
                        else
                            P_COD_TIPO_PRODUCTO.Value = DBNull.Value;
                        P_COD_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_PRODUCTO);

                        //P_REFERENCIA varchar            
                        DbParameter P_REFERENCIA = cmdTransaccionFactory.CreateParameter();
                        P_REFERENCIA.ParameterName = "P_REFERENCIA";
                        if (transac.referencia != null)
                            P_REFERENCIA.Value = transac.referencia;
                        else
                            P_REFERENCIA.Value = DBNull.Value;
                        P_REFERENCIA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_REFERENCIA);
                       

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (transac.num_tran != null && transac.num_tran > 0)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_INT_TRAN_MONE_MOD";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_INT_TRAN_MONE_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        transac.num_tran = Convert.ToInt32(P_NUM_TRAN.Value);

                        return transac;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MonederoData", "guardarTransaccion", ex);
                        return null;
                    }
                }
            }
        }        

        public List<TranMonedero> listarTranMonederoPersona(string cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TranMonedero> lista = new List<TranMonedero>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from INT_TRAN_MONEDERO where cod_persona = " + cod_persona+ " order by Fecha desc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TranMonedero entidad = new TranMonedero();
                            if (resultado["NUM_TRAN"] != DBNull.Value) entidad.num_tran = Convert.ToInt32(resultado["NUM_TRAN"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ID_MONEDERO"] != DBNull.Value) entidad.id_monedero = Convert.ToInt32(resultado["ID_MONEDERO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ID_OPERACION"] != DBNull.Value) entidad.id_operacion = Convert.ToInt32(resultado["ID_OPERACION"]);
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.tipo_credito = Convert.ToInt32(resultado["TIPO_CREDITO"]);
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);                                                       
                            lista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IntegracionData", "listarTranMonederoPersona", ex);
                        return null;
                    }
                }
            }
        }

    }
}

