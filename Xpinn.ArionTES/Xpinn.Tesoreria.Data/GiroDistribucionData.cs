using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class GiroDistribucionTesoreriaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public GiroDistribucionTesoreriaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public GiroDistribucion CrearGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetgiro = cmdTransaccionFactory.CreateParameter();
                        piddetgiro.ParameterName = "p_iddetgiro";
                        piddetgiro.Value = pGiroDistribucion.iddetgiro;
                        piddetgiro.Direction = ParameterDirection.Input;
                        piddetgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetgiro);

                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiroDistribucion.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pfecha_distribucion = cmdTransaccionFactory.CreateParameter();
                        pfecha_distribucion.ParameterName = "p_fecha_distribucion";
                        pfecha_distribucion.Value = pGiroDistribucion.fecha_distribucion;
                        pfecha_distribucion.Direction = ParameterDirection.Input;
                        pfecha_distribucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_distribucion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pGiroDistribucion.cod_persona == 0 || pGiroDistribucion.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pGiroDistribucion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiroDistribucion.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pGiroDistribucion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pGiroDistribucion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pGiroDistribucion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pGiroDistribucion.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pGiroDistribucion.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pGiroDistribucion.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pGiroDistribucion.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIRO_DISTR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return pGiroDistribucion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "CrearGiroDistribucion", ex);
                        return null;
                    }
                }
            }
        }

        public void updateGiro(Int64 pId, Int64 estado , Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_idgiro = cmdTransaccionFactory.CreateParameter();
                        p_idgiro.ParameterName = "p_idgiro";
                        p_idgiro.Value = pId;
                        p_idgiro.Direction = ParameterDirection.Input;
                        p_idgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_idgiro);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "EliminarGiroDistribucion", ex);
                    }
                }
            }
        }


        public GiroDistribucion ModificarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetgiro = cmdTransaccionFactory.CreateParameter();
                        piddetgiro.ParameterName = "p_iddetgiro";
                        piddetgiro.Value = pGiroDistribucion.iddetgiro;
                        piddetgiro.Direction = ParameterDirection.Input;
                        piddetgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetgiro);

                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiroDistribucion.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pfecha_distribucion = cmdTransaccionFactory.CreateParameter();
                        pfecha_distribucion.ParameterName = "p_fecha_distribucion";
                        pfecha_distribucion.Value = pGiroDistribucion.fecha_distribucion;
                        pfecha_distribucion.Direction = ParameterDirection.Input;
                        pfecha_distribucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_distribucion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pGiroDistribucion.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pGiroDistribucion.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiroDistribucion.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pGiroDistribucion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pGiroDistribucion.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pGiroDistribucion.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIRO_DISTR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGiroDistribucion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "ModificarGiroDistribucion", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarGiroDistribucion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        GiroDistribucion pGiroDistribucion = new GiroDistribucion();
                        pGiroDistribucion = ConsultarGiroDistribucion(pId, vUsuario);

                        DbParameter piddetgiro = cmdTransaccionFactory.CreateParameter();
                        piddetgiro.ParameterName = "p_iddetgiro";
                        piddetgiro.Value = pGiroDistribucion.iddetgiro;
                        piddetgiro.Direction = ParameterDirection.Input;
                        piddetgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetgiro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIRO_DISTR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "EliminarGiroDistribucion", ex);
                    }
                }
            }
        }


        public GiroDistribucion ConsultarGiroDistribucion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            GiroDistribucion entidad = new GiroDistribucion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select g.idgiro, g.fec_reg, ta.descripcion, g.valor, g.cod_persona, g.cod_ope, g.numero_radicacion, g.tipo_acto,
                                       (Select Count(*) From giro_distribucion d Where d.iddetgiro = g.idgiro) As distribucion,
                                       g.num_comp, g.tipo_comp from giro g inner join tipo_acto_giro ta on g.tipo_acto = ta.tipo_acto where g.idgiro =  " + pId;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["idgiro"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["idgiro"]);
                            if (resultado["fec_reg"] != DBNull.Value) entidad.fecha_distribucion = Convert.ToDateTime(resultado["fec_reg"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_Operacion = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.numero_Com = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_Comp = Convert.ToString(resultado["tipo_comp"]);
                            if (resultado["tipo_acto"] != DBNull.Value) entidad.IdTipoActo = Convert.ToInt16(resultado["tipo_acto"]);
                            if (resultado["distribucion"] != DBNull.Value) entidad.distribucion = Convert.ToInt32(resultado["distribucion"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "ConsultarGiroDistribucion", ex);
                        return null;
                    }
                }
            }
        }


        public List<GiroDistribucion> ListarGiroDistribucion(GiroDistribucion pGiroDistribucion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<GiroDistribucion> lstGiroDistribucion = new List<GiroDistribucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GIRO_DISTRIBUCION " + ObtenerFiltro(pGiroDistribucion) + " ORDER BY IDDETGIRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GiroDistribucion entidad = new GiroDistribucion();
                            if (resultado["IDDETGIRO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["IDDETGIRO"]);
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["FECHA_DISTRIBUCION"] != DBNull.Value) entidad.fecha_distribucion = Convert.ToDateTime(resultado["FECHA_DISTRIBUCION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstGiroDistribucion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiroDistribucion;
                    }
                    catch // (Exception ex)
                    {
                        // BOExcepcion.Throw("GiroDistribucionData", "ListarGiroDistribucion", ex);
                        return null;
                    }
                }
            }
        }


        public List<GiroDistribucion> getListaGiro(Usuario pUsuario, string pFiltro)
        {
            DbDataReader resultado;
            List<GiroDistribucion> listobj = new List<GiroDistribucion>();
            using (DbConnection conecction = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        String sql = @"Select idgiro, g.fec_reg, g.cod_persona, p.identificacion, p.nombre, g.cod_ope, g.num_comp, g.tipo_comp,
                                      ta.descripcion, g.forma_pago, b.nombrebanco, cb.num_cuenta As num_cuenta_origen, ba.nombrebanco As banco_origen, g.num_cuenta, g.valor, g.estado
                                      From giro g 
                                      Inner join v_persona p on g.cod_persona = p.cod_persona 
                                      Inner join tipo_acto_giro ta on g.tipo_acto = ta.tipo_acto 
                                      Left join bancos ba On g.cod_banco = ba.cod_banco
                                      Left join cuenta_bancaria cb on g.idctabancaria = cb.idctabancaria 
                                      Left join bancos b on cb.cod_banco = b.cod_banco 
                                      Where (g.estado = 0 or g.estado = 1) and g.forma_pago != 4 " + pFiltro;

                        conecction.Open();
                        cmdTransaccionFactory.Connection = conecction;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read()) 
                        {
                            GiroDistribucion entidad = new GiroDistribucion();

                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["fec_reg"] != DBNull.Value) entidad.fecha_distribucion = Convert.ToDateTime(resultado["fec_reg"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_Operacion = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.numero_Com = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_Comp = Convert.ToString(resultado["tipo_comp"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_Pago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.banco = Convert.ToString(resultado["nombrebanco"]);
                            if (resultado["num_cuenta_origen"] != DBNull.Value) entidad.cuenta = Convert.ToString(resultado["num_cuenta_origen"]);
                            if (resultado["banco_origen"] != DBNull.Value) entidad.banc = Convert.ToString(resultado["banco_origen"]);
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.Numcue_des = Convert.ToString(resultado["num_cuenta"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["estado"]);

                            listobj.Add(entidad);
                        }
                        return listobj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "ListarGiroDistribucion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista Todos los Combos segun la OPcion  que se necesite y se envie como argumento 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<GiroDistribucion> listarDDlTipoCom(Usuario pUsuario, int opcion)
        {
            DbDataReader resultado;
            string sql = "";
            List<GiroDistribucion> lstGiroDistribucion = new List<GiroDistribucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        switch (opcion)
                        {
                            case 1:
                                sql = @"select * from tipo_comp"; // se carga el ddlTipoCom
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    GiroDistribucion entidad = new GiroDistribucion();
                                    if (resultado["TIPO_COMP"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["TIPO_COMP"]);
                                    if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]); ;
                                    lstGiroDistribucion.Add(entidad);
                                }

                                break;

                            case 2:
                                sql = @"select * from tipo_acto_giro"; // se carga el ddlGeneradoEn
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    GiroDistribucion entidad = new GiroDistribucion();
                                    if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["TIPO_ACTO"]);
                                    if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]); ;
                                    lstGiroDistribucion.Add(entidad);
                                }
                                break;

                            case 3:
                                sql = @"select * from bancos";// se carga el ddlBancos
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    GiroDistribucion entidad = new GiroDistribucion();
                                    if (resultado["COD_BANCO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["COD_BANCO"]);
                                    if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["NOMBREBANCO"]); ;
                                    lstGiroDistribucion.Add(entidad);
                                }
                                break;

                            case 4:
                                sql = @"select * from cuenta_bancaria";// se carga el ddl
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    GiroDistribucion entidad = new GiroDistribucion();
                                    if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                                    if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["NUM_CUENTA"]); ;
                                    lstGiroDistribucion.Add(entidad);
                                }
                                break;
                            case 5:
                                sql = @"select * from usuarios";
                                connection.Open();
                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql;
                                resultado = cmdTransaccionFactory.ExecuteReader();
                                while (resultado.Read())
                                {
                                    GiroDistribucion entidad = new GiroDistribucion();
                                    if (resultado["CODUSUARIO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["CODUSUARIO"]);
                                    if (resultado["NOMBRE"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["NOMBRE"]); ;
                                    lstGiroDistribucion.Add(entidad);
                                }
                                break;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiroDistribucion;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "listarDDlTipoCom", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// cargar el ddl Generado en consultando la tabla tipo_acto_giro
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<GiroDistribucion> listarDDlGeneradoEn(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<GiroDistribucion> lstGiroDistribucion = new List<GiroDistribucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from tipo_acto_giro";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GiroDistribucion entidad = new GiroDistribucion();
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]); ;
                            lstGiroDistribucion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiroDistribucion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "listarDDlGeneradoEn", ex);
                        return null;
                    }
                }
            }
        }

        public List<GiroDistribucion> listarDDlFormaPagoInv(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<GiroDistribucion> lstGiroDistribucion = new List<GiroDistribucion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexionInventarios(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select ID_FORMA_PAGO, DESCRIPCION from iv_forma_pago";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GiroDistribucion entidad = new GiroDistribucion();
                            if (resultado["ID_FORMA_PAGO"] != DBNull.Value) entidad.iddetgiro = Convert.ToInt32(resultado["ID_FORMA_PAGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]); ;
                            lstGiroDistribucion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiroDistribucion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroDistribucionData", "listarDDlGeneradoEn", ex);
                        return null;
                    }
                }
            }
        }


    }
}
