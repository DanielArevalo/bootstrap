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
    /// Objeto de acceso a datos para la tabla TipoListaRecaudoS
    /// </summary>
    public class AnulacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoListaRecaudoS
        /// </summary>
        public AnulacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<AnulacionOperaciones> listaranulaciones(string ObtenerFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  * from V_OPERACION_GRAL " + ObtenerFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["NOMTIPO_OPE"] != DBNull.Value) entidad.TIPO_OPE = (Convert.ToString(resultado["NOMTIPO_OPE"]));
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.FECHA_OPER = (Convert.ToDateTime(resultado["FECHA_OPER"]).ToString("dd/MM/yyyy"));
                            if (resultado["FECHA_REAL"] != DBNull.Value) entidad.FECHA_REAL = (Convert.ToDateTime(resultado["FECHA_REAL"]).ToString("dd/MM/yyyy"));
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.NUM_COMP = (Convert.ToInt64(resultado["NUM_COMP"]));
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.TIPO_COMP = (Convert.ToString(resultado["TIPO_COMP"]));
                            if (resultado["NUM_LISTA"] != DBNull.Value) entidad.NUM_LISTA = (Convert.ToInt64(resultado["NUM_LISTA"]));
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.NOMESTADO = (Convert.ToString(resultado["NOMESTADO"]));
                            if (resultado["ESTADO"] != DBNull.Value) entidad.ESTADO = (Convert.ToInt64(resultado["ESTADO"]));
                            if (resultado["IDEN_USUARIO"] != DBNull.Value) entidad.IDEN_USUARIO = (Convert.ToString(resultado["IDEN_USUARIO"]));
                            if (resultado["NOM_USUARIO"] != DBNull.Value) entidad.NOM_USUARIO = (Convert.ToString(resultado["NOM_USUARIO"]));
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.COD_OFICINA = (Convert.ToInt64(resultado["COD_OFICINA"]));
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.NOM_OFICINA = (Convert.ToString(resultado["NOM_OFICINA"]));
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.COD_TIPO_OPE = (Convert.ToInt32(resultado["TIPO_OPE"]));
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AnulacionData", "listaranulaciones", ex);
                        return null;
                    }
                }
            }
        }




        public List<AnulacionOperaciones> listaranulacionesNuevo(string[] pfiltros, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_ope,Numero_Radicacion,Cod_Linea_Credito,Nombre_Linea, Cliente, Tipo_Tran, Tipo_Mov, Tipo_Producto, SUM(valor) valor "
                                      + "From V_Transaccion where cod_ope = " + pfiltros[0];
                        if (pfiltros[1] != "")
                        {
                            sql = sql + " and Numero_Radicacion = "+ pfiltros[1];
                        }
                        if (pfiltros[2] != "")
                        {
                            sql = sql + " and Cliente = '" + pfiltros[2] + "'";
                        }
                        sql = sql + " Group By cod_ope, Numero_Radicacion, Cod_Linea_Credito, Nombre_Linea, Cliente, Tipo_Tran, Tipo_Mov, Tipo_Producto ";
                        if (pfiltros[3] != "")
                        {
                            sql = sql + " ORDER BY " + pfiltros[3] + " ";
                        }
                        else
                        {
                            sql = sql + " ORDER BY CLIENTE ";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();

                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = (Convert.ToString(resultado["NUMERO_RADICACION"]));
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = (Convert.ToString(resultado["COD_LINEA_CREDITO"]));
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = (Convert.ToString(resultado["NOMBRE_LINEA"]));
                            if (resultado["CLIENTE"] != DBNull.Value) entidad.CLIENTE = (Convert.ToString(resultado["CLIENTE"]));
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.TIPO_TRAN = (Convert.ToString(resultado["TIPO_TRAN"]));
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.TIPO_MOV = (Convert.ToString(resultado["TIPO_MOV"]));
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.TIPO_PRODUCTO = (Convert.ToString(resultado["TIPO_PRODUCTO"]));
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR = (Convert.ToDouble(resultado["VALOR"]));
                            /*
                            if (resultado["ATRIBUTO"] != DBNull.Value) entidad.ATRIBUTO = (Convert.ToString(resultado["ATRIBUTO"]));
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE2 = (Convert.ToString(resultado["COD_OPE"]));
                            if (resultado["NUM_TRAN"] != DBNull.Value) entidad.NUM_TRAN = (Convert.ToString(resultado["NUM_TRAN"]));
                            */
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteAdicionalData", "ListarComponenteAdicional", ex);
                        return null;
                    }
                }
            }
        }



        public AnulacionOperaciones listaranulacionesentidadnuevo(string ObtenerFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  * from V_Operacion_Gral where cod_ope= " + ObtenerFiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        AnulacionOperaciones entidad = new AnulacionOperaciones();

                        if (resultado.Read())
                        {
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.TIPO_OPE = (Convert.ToString(resultado["TIPO_OPE"]));
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.FECHA_OPER = (Convert.ToDateTime(resultado["FECHA_OPER"]).ToString("dd/MM/yyyy"));
                            if (resultado["FECHA_REAL"] != DBNull.Value) entidad.FECHA_REAL = (Convert.ToDateTime(resultado["FECHA_REAL"]).ToString("dd/MM/yyyy"));
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.NUM_COMP = (Convert.ToInt64(resultado["NUM_COMP"]));
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.TIPO_COMP = (Convert.ToString(resultado["TIPO_COMP"]));
                            if (resultado["NUM_LISTA"] != DBNull.Value) entidad.NUM_LISTA = (Convert.ToInt64(resultado["NUM_LISTA"]));
                            if (resultado["ESTADO"] != DBNull.Value) entidad.ESTADO = (Convert.ToInt64(resultado["ESTADO"]));
                            if (resultado["IDEN_USUARIO"] != DBNull.Value) entidad.IDEN_USUARIO = (Convert.ToString(resultado["IDEN_USUARIO"]));
                            if (resultado["NOM_USUARIO"] != DBNull.Value) entidad.NOM_USUARIO = (Convert.ToString(resultado["NOM_USUARIO"]));
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.COD_OFICINA = (Convert.ToInt64(resultado["COD_OFICINA"]));
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.NOM_OFICINA = (Convert.ToString(resultado["NOM_OFICINA"]));
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteAdicionalData", "ListarComponenteAdicional", ex);
                        return null;
                    }
                }
            }
        }




        public int RealizarAnulacion(Int64 cod_ope, Int64 num_radicacion, DateTime fecha_anula, string cod_usuario, Int64 cod_ofi, long cod_motivo_anula, ref string error, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        error = "";

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        pn_cod_ope.Value = cod_ope;
                        pn_cod_ope.DbType = DbType.Int64;

                        DbParameter pn_num_radicacion = cmdTransaccionFactory.CreateParameter();
                        pn_num_radicacion.ParameterName = "pn_num_radicacion";
                        pn_num_radicacion.Value = num_radicacion;
                        pn_num_radicacion.DbType = DbType.Int64;

                        DbParameter pf_fecha_anula = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_anula.ParameterName = "pf_fecha_anula";
                        pf_fecha_anula.Value = fecha_anula;
                        pf_fecha_anula.DbType = DbType.Date;

                        DbParameter pn_usuario = cmdTransaccionFactory.CreateParameter();
                        pn_usuario.ParameterName = "pn_usuario";
                        pn_usuario.Value = pUsuario.codusuario;
                        pn_usuario.DbType = DbType.Int64;

                        DbParameter pn_cod_ofi = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ofi.ParameterName = "pn_cod_ofi";
                        pn_cod_ofi.Value = cod_ofi;
                        pn_cod_ofi.DbType = DbType.Int64;

                        DbParameter pn_cod_motivo = cmdTransaccionFactory.CreateParameter();
                        pn_cod_motivo.ParameterName = "pn_cod_motivo_anula";
                        pn_cod_motivo.Value = cod_motivo_anula;
                        pn_cod_motivo.DbType = DbType.Int64;

                        DbParameter Pb_Resultado = cmdTransaccionFactory.CreateParameter();
                        Pb_Resultado.ParameterName = "Pb_Resultado";
                        Pb_Resultado.Direction = ParameterDirection.InputOutput;
                        Pb_Resultado.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pn_num_radicacion);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_anula);
                        cmdTransaccionFactory.Parameters.Add(pn_usuario);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ofi);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_motivo);
                        cmdTransaccionFactory.Parameters.Add(Pb_Resultado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ANULAROPE_REC";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return Convert.ToInt32(Pb_Resultado.Value);
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        //BOExcepcion.Throw("AnulacionOperacionesData", "RealizarAnulacion", ex);                        
                        return 0;
                    }
                }
            }

        }

        public List<AnulacionOperaciones> ListarTransacciones(Int64 pcod_ope, string pTipoProducto, string pNumeroProdcuto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select t.*, CodigoProducto(Upper(t.tipo_producto)) As COD_PRODUCTO From V_Transaccion t Where t.cod_ope = " + pcod_ope + " And t.tipo_producto = '" + pTipoProducto + "' And t.numero_radicacion = " + pNumeroProdcuto + " ";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                       
                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.CLIENTE = (Convert.ToString(resultado["COD_CLIENTE"]));
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = (Convert.ToString(resultado["NUMERO_RADICACION"]));
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = (Convert.ToString(resultado["COD_LINEA_CREDITO"]));
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = (Convert.ToString(resultado["NOMBRE_LINEA"]));
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.TIPO_TRAN = (Convert.ToString(resultado["TIPO_TRAN"]));
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.TIPO_MOV = (Convert.ToString(resultado["TIPO_MOV"]));
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR = (Convert.ToDouble(resultado["VALOR"]));
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.ATRIBUTO = (Convert.ToString(resultado["COD_ATR"]));
                            if (resultado["COD_PRODUCTO"] != DBNull.Value) entidad.TIPO_PRODUCTO = (Convert.ToString(resultado["COD_PRODUCTO"]));
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteAdicionalData", "ListarComponenteAdicional", ex);
                        return null;
                    }
                }
            }
        }


    }
}