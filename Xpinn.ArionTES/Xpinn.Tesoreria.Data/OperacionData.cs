using System;
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
    public class OperacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public OperacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para crear una operación
        /// </summary>
        /// <param name="pOperacion"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Int64 CrearOperacion(Operacion pOperacion, ref string error, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        error = "";
                        
                        DbParameter pcodigooper = cmdTransaccionFactory.CreateParameter();
                        pcodigooper.ParameterName = "pcodigooper";
                        pcodigooper.Value = pOperacion.cod_ope;
                        pcodigooper.DbType = DbType.Int64;
                        pcodigooper.Direction = ParameterDirection.Output;

                        DbParameter ptipo_ope = cmdTransaccionFactory.CreateParameter();
                        ptipo_ope.ParameterName = "ptipo_ope";
                        ptipo_ope.Value = pOperacion.tipo_ope;
                        ptipo_ope.DbType = DbType.Int64;

                        DbParameter pcodigousuario = cmdTransaccionFactory.CreateParameter();
                        pcodigousuario.ParameterName = "pcodigousuario";
                        pcodigousuario.Value = pOperacion.cod_usu;
                        pcodigousuario.DbType = DbType.Int64;

                        DbParameter pcodigooficina = cmdTransaccionFactory.CreateParameter();
                        pcodigooficina.ParameterName = "pcodigooficina";
                        pcodigooficina.Value = pOperacion.cod_ofi;
                        pcodigooficina.DbType = DbType.Int64;

                        DbParameter pfechaoper = cmdTransaccionFactory.CreateParameter();
                        pfechaoper.ParameterName = "pfechaoper";
                        pfechaoper.Value = pOperacion.fecha_oper;
                        pfechaoper.DbType = DbType.DateTime;

                        DbParameter pfechacalc = cmdTransaccionFactory.CreateParameter();
                        pfechacalc.ParameterName = "pfechacalc";
                        pfechacalc.Value = pOperacion.fecha_calc;
                        pfechacalc.DbType = DbType.DateTime;

                        DbParameter pnumlista = cmdTransaccionFactory.CreateParameter();
                        pnumlista.ParameterName = "pnumlista";
                        if (pOperacion.num_lista != null)
                            pnumlista.Value = pOperacion.num_lista;
                        else
                            pnumlista.Value = DBNull.Value;
                        pnumlista.DbType = DbType.Int64;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        if (pUsuario.IP == null)
                            p_ip.Value = DBNull.Value;
                        else
                            p_ip.Value = pUsuario.IP;

                        cmdTransaccionFactory.Parameters.Add(pcodigooper);
                        cmdTransaccionFactory.Parameters.Add(ptipo_ope);
                        cmdTransaccionFactory.Parameters.Add(pcodigousuario);
                        cmdTransaccionFactory.Parameters.Add(pcodigooficina);
                        cmdTransaccionFactory.Parameters.Add(pfechaoper);
                        cmdTransaccionFactory.Parameters.Add(pfechacalc);
                        cmdTransaccionFactory.Parameters.Add(pnumlista);                  
                        cmdTransaccionFactory.Parameters.Add(p_ip);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_OPERACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return Convert.ToInt64(pcodigooper.Value);
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        return 0;
                    }
                }
            }
        }


        public List<AnulacionOperaciones> combooperacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from tipo_ope";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["descripcion"] != DBNull.Value) entidad.TIPO_OPE = (Convert.ToString(resultado["descripcion"]));
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OperacionData", "combooperacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para listar los tipos de comprobantes
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<AnulacionOperaciones> cobocomprobantes(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from tipo_comp order by 1 asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["descripcion"] != DBNull.Value) entidad.DESCRIPCION = (Convert.ToString(resultado["descripcion"]));
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.TIPO_COMP = (Convert.ToString(resultado["TIPO_COMP"]));
                            if (entidad.TIPO_COMP != null)
                            {
                                entidad.nomtipocomp = entidad.TIPO_COMP + "-" + entidad.DESCRIPCION;
                             }
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OperacionData", "cobocomprobantes", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Método para listar las transacciones de una operación
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<AnulacionOperaciones> listaranulacionesNuevo(string id, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_TRANSACCION where cod_ope =  " + id;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();

                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.TIPO_TRAN = (Convert.ToString(resultado["TIPO_TRAN"]));
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.TIPO_MOV = (Convert.ToString(resultado["TIPO_MOV"]));
                            if (resultado["ATRIBUTO"] != DBNull.Value) entidad.ATRIBUTO = (Convert.ToString(resultado["ATRIBUTO"]));
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR = (Convert.ToDouble(resultado["VALOR"]));
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE2 = (Convert.ToString(resultado["COD_OPE"]));
                            if (resultado["NUM_TRAN"] != DBNull.Value) entidad.NUM_TRAN = (Convert.ToString(resultado["NUM_TRAN"]));
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = (Convert.ToString(resultado["NUMERO_RADICACION"]));
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = (Convert.ToString(resultado["COD_LINEA_CREDITO"]));
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = (Convert.ToString(resultado["NOMBRE_LINEA"]));
                            if (resultado["CLIENTE"] != DBNull.Value) entidad.CLIENTE = (Convert.ToString(resultado["CLIENTE"]));
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));

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

        /// <summary>
        /// Método para anular una operación
        /// </summary>
        /// <param name="cod_ope"></param>
        /// <param name="fecha_anula"></param>
        /// <param name="cod_usuario"></param>
        /// <param name="cod_ofi"></param>
        /// <param name="cod_motivo_anula"></param>
        /// <param name="error"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public int RealizarAnulacion(Int64 cod_ope, DateTime fecha_anula, string cod_usuario, Int64 cod_ofi, long cod_motivo_anula, ref string error, Usuario pUsuario)
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
                        Pb_Resultado.ParameterName = "pb_Resultado";
                        Pb_Resultado.Direction = ParameterDirection.InputOutput;
                        Pb_Resultado.DbType = DbType.Int64;

                        DbParameter ps_error = cmdTransaccionFactory.CreateParameter();
                        ps_error.ParameterName = "ps_error";
                        ps_error.Value = "0";
                        ps_error.Direction = ParameterDirection.Output;
                        ps_error.DbType = DbType.StringFixedLength;
                        ps_error.Size = 400;

                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_anula);
                        cmdTransaccionFactory.Parameters.Add(pn_usuario);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ofi);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_motivo);
                        cmdTransaccionFactory.Parameters.Add(Pb_Resultado);
                        cmdTransaccionFactory.Parameters.Add(ps_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ANULAROPERACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        int result = Convert.ToInt32(Pb_Resultado.Value);
                        if (ps_error != null)
                            error = ps_error.Value.ToString().Trim();
                        if (error.Trim().Length > 0)
                            return 0;

                        AnulacionOperaciones anulacion = new AnulacionOperaciones
                        {
                            COD_OPE = cod_ope,
                            fecha_anulacion = fecha_anula,
                            cod_usuario_anulacion = pUsuario.codusuario,
                            COD_OFICINA = cod_ofi,
                            codigo_motivo_anulacion = cod_motivo_anula
                        };

                        DAauditoria.InsertarLog(anulacion, "Operacion", pUsuario, Accion.Crear.ToString(), TipoAuditoria.Tesoreria, "Creacion de anulacion de operacion con numero de operacion " + cod_ope);

                        return result;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        BOExcepcion.Throw("AnulacionOperacionesData", "RealizarAnulacion", ex);                        
                        return 0;
                    }
                }
            }

        }

        /// <summary>
        /// Método para listar las operaciones que se puedan anular
        /// </summary>
        /// <param name="ObtenerFiltro"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
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
                        string sql = "select  * from V_OPERACION " + ObtenerFiltro ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.TIPO_OPE = (Convert.ToString(resultado["TIPO_OPE"]));
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

        /// <summary>
        ///  Método para consultar los datos de una operación
        /// </summary>
        /// <param name="ObtenerFiltro"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
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
                        string sql = "select  * from V_OPERACION where cod_ope= " + ObtenerFiltro;

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

        /// <summary>
        /// Método para consultar el usuario que anuló una operación
        /// </summary>
        /// <param name="ObtenerFiltro"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public AnulacionOperaciones ListarOperacionAnula(string ObtenerFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select b.*, c.descripcion, b.cod_usu || '-' || u.nombre As nom_usuario from OPERACION_ANULADA a Left Join TIPO_MOTIVO_ANULACION c on a.cod_motivo = c.tipo_motivo, OPERACION b Left Join USUARIOS u On b.cod_usu = u.codusuario where a.cod_ope = b.cod_ope And a.cod_ope_anula = " + ObtenerFiltro;

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
                            if (resultado["NOM_USUARIO"] != DBNull.Value) entidad.NOM_USUARIO = (Convert.ToString(resultado["NOM_USUARIO"]));
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.MOTIVO_ANULA = (Convert.ToString(resultado["DESCRIPCION"]));
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

        /// <summary>
        /// Método para listar las operaciones
        /// </summary>
        /// <param name="poperacion"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Operacion> ListarOperacion(Operacion poperacion, Usuario pUsuario)
        {
            return ListarOperacion(poperacion, "", pUsuario);
        }


        public List<Operacion> ListarOperacion(Operacion poperacion, string pfiltro, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Operacion> lstoperacion = new List<Operacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT o.*, u.identificacion As iden_usuario, o.cod_usu || '-' || u.nombre As nom_usuario, o.cod_ofi AS cod_oficina,ofi.nombre As nom_oficina,
                                        Case o.estado When 1 Then 'Activa' When 2 Then 'Anulada' Else To_Char(o.estado) End As nomestado, t.descripcion As nom_tipo_ope,
                                        Nvl((Select Sum(t.valor) From tran_cred t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_APORTE  t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_AHORRO t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_PROGRAMADO t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_SERVICIOS t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_DEVOLUCION t Where t.cod_ope = o.cod_ope), 0) ,
                                        + Nvl((Select Sum(t.valor) From tran_otros t Where t.cod_ope = o.cod_ope), 0) As valor,
                                        o.cod_proceso, pc.cod_cuenta
                                        FROM Operacion o INNER JOIN tipo_ope t ON o.tipo_ope = t.tipo_ope LEFT JOIN Usuarios u ON o.cod_usu = u.codusuario LEFT JOIN Oficina ofi ON o.cod_ofi = ofi.cod_oficina                                      
                                        LEFT JOIN proceso_contable pc ON o.cod_proceso = pc.cod_proceso ";
                        string filtro =  ObtenerFiltro(poperacion, "o.");
                        if (pfiltro.Trim() != "")
                            filtro += (filtro.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + pfiltro;
                        sql +=  filtro + " ORDER BY o.cod_ope";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Operacion entidad = new Operacion();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["TIPO_OPE"]);
                            
                            if (entidad.tipo_ope==119)
                            {
                                if (resultado["NUM_LISTA"] != DBNull.Value) entidad.num_lista = Convert.ToInt32(resultado["NUM_LISTA"]);
                                entidad = listaEmpresa(entidad.num_lista, pUsuario);
                            }
                            else
                            {
                                entidad = lista(entidad.cod_ope, pUsuario);
                            }
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["TIPO_OPE"]);
                            if (resultado["NUM_LISTA"] != DBNull.Value) entidad.num_lista = Convert.ToInt32(resultado["NUM_LISTA"]);
                            if (resultado["NOM_TIPO_OPE"] != DBNull.Value) entidad.nom_tipo_ope = Convert.ToString(resultado["NOM_TIPO_OPE"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["FECHA_REAL"] != DBNull.Value) entidad.fecha_real = Convert.ToDateTime(resultado["FECHA_REAL"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            if (resultado["IDEN_USUARIO"] != DBNull.Value) entidad.iden_usuario = Convert.ToString(resultado["IDEN_USUARIO"]);
                            if (resultado["NOM_USUARIO"] != DBNull.Value) entidad.nom_usuario = Convert.ToString(resultado["NOM_USUARIO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_ofi = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_ofi = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt64(resultado["COD_PROCESO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            entidad.seleccionar = true;
                            lstoperacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstoperacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "Listaroperacion", ex);
                        return null;
                    }
                }
            }
        }
        public Operacion lista(Int64 cod_ope,Usuario pUsuario)
        {
            DbDataReader resultado;
            Operacion entidad = new Operacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT * FROM v_transaccion t JOIN operacion o ON o.cod_ope = t.cod_ope 
                                    where o.cod_ope= "+cod_ope+" and rownum=1";
                   
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {

                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                          
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "lista", ex);
                        return null;
                    }
                }
            }
        }
        public Operacion listaEmpresa(Int64? cod_ope, Usuario pUsuario)
        {
            DbDataReader resultado;
            Operacion entidad = new Operacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"   Select r.cod_empresa, e.nom_empresa, p.identificacion
                                from recaudo_Masivo r Join empresa_recaudo e on r.cod_empresa = e.cod_empresa
                                LEFT JOIN persona p ON e.cod_persona = p.cod_persona
                                 Where r.numero_Recaudo =" + cod_ope ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {

                          
                            if (resultado["cod_empresa"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["cod_empresa"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nom_empresa"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nom_empresa"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "lista", ex);
                        return null;
                    }
                }
            }
        }


        public Boolean GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64? pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter plnum_comp = cmdTransaccionFactory.CreateParameter();
                        DbParameter pltipo_comp = cmdTransaccionFactory.CreateParameter();
                        if (ptip_ope == 119 || ptip_ope == 132)
                        {
                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            plcod_proceso.Value = pcod_proceso;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                          
                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                           
                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_RECAUDOSMASIVOS";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        else
                        {


                            DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                            plcod_ope.ParameterName = "pcod_ope";
                            plcod_ope.Value = pcod_ope;

                            DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                            plcod_persona.ParameterName = "pcod_persona";
                            plcod_persona.Value = pcod_persona;

                            DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                            plcod_proceso.ParameterName = "pcod_proceso";
                            if (pcod_proceso == null)
                                plcod_proceso.Value = DBNull.Value;
                            else
                                plcod_proceso.Value = pcod_proceso;

                          
                            plnum_comp.ParameterName = "pnum_comp";
                            plnum_comp.Value = 0;
                            plnum_comp.Direction = ParameterDirection.Output;

                         
                            pltipo_comp.ParameterName = "ptipo_comp";
                            pltipo_comp.Value = 0;
                            pltipo_comp.Direction = ParameterDirection.Output;

                            cmdTransaccionFactory.Parameters.Add(plcod_ope);
                            cmdTransaccionFactory.Parameters.Add(plcod_persona);
                            cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                            cmdTransaccionFactory.Parameters.Add(plnum_comp);
                            cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_INTERFACE";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        if (plnum_comp != null) pnum_comp = Convert.ToInt64(plnum_comp.Value);
                        if (pltipo_comp != null) ptipo_comp = Convert.ToInt64(pltipo_comp.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "GenerarComprobane", ex);
                        return false;
                    }
                }
            }
        }

        public Boolean ReversarOperacion(GridView gvOperaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        CheckBox chkAnula;
                        int codOpe = 0;

                        // Recorre listado de operaciones determinando cuales fueron marcadas para anular
                        foreach (GridViewRow fila in gvOperaciones.Rows)
                        {
                            codOpe = int.Parse(fila.Cells[1].Text);
                            chkAnula = (CheckBox)fila.FindControl("chkAnula");

                            if (chkAnula.Checked == true)
                            {
                                // En el caso de operaciòn de pagos realiza la anulaciòn de los productos aplicados.
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                                pcod_ope.ParameterName = "pn_cod_ope";
                                pcod_ope.Value = long.Parse(codOpe.ToString());
                                pcod_ope.Size = 8;
                                pcod_ope.DbType = DbType.Int64;
                                pcod_ope.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                                pusuario.ParameterName = "pn_usuario";
                                pusuario.Value = pUsuario.codusuario;
                                pusuario.Size = 8;
                                pusuario.DbType = DbType.Int64;
                                pusuario.Direction = ParameterDirection.Input;

                                DbParameter pValor_Retorno = cmdTransaccionFactory.CreateParameter();
                                pValor_Retorno.ParameterName = "pb_resultado";
                                pValor_Retorno.Size = 8;
                                pValor_Retorno.Value = 0;
                                pValor_Retorno.DbType = DbType.Int64;
                                pValor_Retorno.Direction = ParameterDirection.InputOutput;

                                cmdTransaccionFactory.Parameters.Add(pcod_ope);
                                cmdTransaccionFactory.Parameters.Add(pusuario);
                                cmdTransaccionFactory.Parameters.Add(pValor_Retorno);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "USP_XPINN_TES_REVERSION";
                                cmdTransaccionFactory.ExecuteNonQuery();

                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "ReversarOperacion", ex);
                        return false;
                    }
                }
            }
        }


        public Operacion GrabarOperacion(Operacion pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pUsuario.cod_oficina;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        if (pEntidad.cod_caja != 0) pcodi_caja.Value = pEntidad.cod_caja; else pcodi_caja.Value = DBNull.Value;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        if (pEntidad.cod_cajero != 0) pcodi_cajero.Value = pEntidad.cod_cajero; else pcodi_cajero.Value = DBNull.Value;
                        pcodi_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_oper = cmdTransaccionFactory.CreateParameter();
                        pfecha_oper.ParameterName = "pfechaoper";
                        pfecha_oper.Value = pEntidad.fecha_oper;
                        pfecha_oper.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechacalc";
                        if (pEntidad.fecha_calc != DateTime.MinValue) pfecha_cal.Value = pEntidad.fecha_calc; else pfecha_cal.Value = DBNull.Value;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "pobservacion";
                        if (pEntidad.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pEntidad.observacion;
                        pobservacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "pcod_proceso";
                        if (pEntidad.cod_proceso == null)
                            pcod_proceso.Value = DBNull.Value;
                        else
                            pcod_proceso.Value = pEntidad.cod_proceso;
                        pobservacion.Direction = ParameterDirection.Input;

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "pnum_comp";
                        pnum_comp.Value = -2;
                        pnum_comp.Direction = ParameterDirection.Input;

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "ptipo_comp";
                        ptipo_comp.Value = -2;
                        ptipo_comp.Direction = ParameterDirection.Input;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;
                        p_ip.Direction = ParameterDirection.Input;


                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Value = "";
                        p_error.DbType = DbType.AnsiStringFixedLength;


                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_oper);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(pobservacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_TES_OPERACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        //cmdTransaccionFactory.Parameters.Add(p_error);

                        pEntidad.cod_ope = pcode_opera.Value != DBNull.Value ? Convert.ToInt64(pcode_opera.Value) : 1;
                        pEntidad.error = p_error.Value != DBNull.Value ? Convert.ToString(p_error.Value) : "";

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "Grabaroperacion", ex);
                        return null;
                    }
                }
            }
        }



        public List<AnulacionOperaciones> ListarUltimosMovimientosXpersona(Int64 id, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AnulacionOperaciones> lstComponenteAdicional = new List<AnulacionOperaciones>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"WITH ULTIMOS_MOVIMIENTOS AS (
                                SELECT V.COD_OPE, V.NUMERO_RADICACION, V.COD_LINEA_CREDITO, V.NOMBRE_LINEA, V.CLIENTE , V.TIPO_TRAN, 
                                V.TIPO_MOV, SUM(V.VALOR) VALOR, V.COD_CLIENTE, V.IDENTIFICACION, V.NOMBRE, V.TIPO_PRODUCTO,
                                V.COD_TIPO_PRODUCTO, O.FECHA_OPER, T.DESCRIPCION AS TIPO_OPE 
                                FROM V_TRANSACCION V LEFT JOIN OPERACION O ON O.COD_OPE = V.COD_OPE 
                                LEFT JOIN TIPO_OPE T ON T.TIPO_OPE = O.TIPO_OPE 
                                WHERE V.COD_CLIENTE = " + id + @" 
                                GROUP BY V.COD_OPE, V.NUMERO_RADICACION, V.COD_LINEA_CREDITO, V.NOMBRE_LINEA, V.CLIENTE , V.TIPO_TRAN, 
                                V.TIPO_MOV, V.COD_CLIENTE, V.IDENTIFICACION, V.NOMBRE, V.TIPO_PRODUCTO,
                                V.COD_TIPO_PRODUCTO, O.FECHA_OPER, T.DESCRIPCION
                                ORDER by o.fecha_oper desc)
                                SELECT * FROM ULTIMOS_MOVIMIENTOS WHERE ROWNUM <= 40";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AnulacionOperaciones entidad = new AnulacionOperaciones();
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE = (Convert.ToInt64(resultado["COD_OPE"]));
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.COD_OPE2 = (Convert.ToString(resultado["COD_OPE"]));
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = (Convert.ToString(resultado["NUMERO_RADICACION"]));
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = (Convert.ToString(resultado["COD_LINEA_CREDITO"]));
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = (Convert.ToString(resultado["NOMBRE_LINEA"]));
                            if (resultado["CLIENTE"] != DBNull.Value) entidad.CLIENTE = (Convert.ToString(resultado["CLIENTE"]));
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.TIPO_TRAN = (Convert.ToString(resultado["TIPO_TRAN"]));
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.TIPO_MOV = (Convert.ToString(resultado["TIPO_MOV"]));
                            if (resultado["VALOR"] != DBNull.Value) entidad.VALOR_STR = (Convert.ToDecimal(resultado["VALOR"]).ToString("n0")); else entidad.VALOR_STR = "0";
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.TIPO_PRODUCTO = Convert.ToString(resultado["TIPO_PRODUCTO"]); else entidad.TIPO_OPE = "";
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.FECHA_OPER = (Convert.ToDateTime(resultado["FECHA_OPER"]).ToString("dd/MM/yyyy")); else entidad.FECHA_OPER = "";
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.TIPO_OPE = Convert.ToString(resultado["TIPO_OPE"]); else entidad.TIPO_OPE = "";
                            //if (resultado["ATRIBUTO"] != DBNull.Value) entidad.ATRIBUTO = (Convert.ToString(resultado["ATRIBUTO"]));
                            //if (resultado["NUM_TRAN"] != DBNull.Value) entidad.NUM_TRAN = (Convert.ToString(resultado["NUM_TRAN"]));
                            
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "ListarUltimosMovimientosXpersona", ex);
                        return null;
                    }
                }
            }
        }


    }
}




