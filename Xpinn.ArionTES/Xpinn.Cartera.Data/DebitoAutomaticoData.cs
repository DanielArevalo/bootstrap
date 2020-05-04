using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class DebitoAutomaticoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public DebitoAutomaticoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public DebitoAutomatico CrearDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDebitoAutomatico.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodpersona = cmdTransaccionFactory.CreateParameter();
                        pcodpersona.ParameterName = "p_cod_persona";
                        pcodpersona.Value = pDebitoAutomatico.cod_persona;
                        pcodpersona.Direction = ParameterDirection.Input;
                        pcodpersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodpersona);


                        DbParameter pcodproducto = cmdTransaccionFactory.CreateParameter();
                        pcodproducto.ParameterName = "P_COD_TIPO_PRODUCTO";
                        pcodproducto.Value = pDebitoAutomatico.cod_tipo_producto;
                        pcodproducto.Direction = ParameterDirection.Input;
                        pcodproducto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodproducto);


                        DbParameter pcodlinea= cmdTransaccionFactory.CreateParameter();
                        pcodlinea.ParameterName = "p_cod_linea";                        
                        pcodlinea.Value = pDebitoAutomatico.cod_linea;
                        pcodlinea.Direction = ParameterDirection.Input;
                        pcodlinea.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodlinea);


                        DbParameter pnumproducto = cmdTransaccionFactory.CreateParameter();
                        pnumproducto.ParameterName = "p_num_producto";
                        pnumproducto.Value = pDebitoAutomatico.numero_producto;
                        pnumproducto.Direction = ParameterDirection.Input;
                        pnumproducto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumproducto);




                        DbParameter p_numcuentaahorro = cmdTransaccionFactory.CreateParameter();
                        p_numcuentaahorro.ParameterName = "p_num_cuenta_ahorro";
                        p_numcuentaahorro.Value = pDebitoAutomatico.numero_cuenta_ahorro;
                        p_numcuentaahorro.Direction = ParameterDirection.Input;
                        p_numcuentaahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numcuentaahorro);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DEB_AUT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDebitoAutomatico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "CrearDebitoAutomatico", ex);
                        return null;
                    }
                }
            }
        }


        public DebitoAutomatico ModificarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pDebitoAutomatico.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);


                        DbParameter pcodpersona = cmdTransaccionFactory.CreateParameter();
                        pcodpersona.ParameterName = "p_cod_persona";
                        pcodpersona.Value = pDebitoAutomatico.cod_persona;
                        pcodpersona.Direction = ParameterDirection.Input;
                        pcodpersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodpersona);


                        DbParameter pcodproducto = cmdTransaccionFactory.CreateParameter();
                        pcodproducto.ParameterName = "P_COD_TIPO_PRODUCTO";
                        pcodproducto.Value = pDebitoAutomatico.cod_tipo_producto;
                        pcodproducto.Direction = ParameterDirection.Input;
                        pcodproducto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodproducto);


                        DbParameter pcodlinea = cmdTransaccionFactory.CreateParameter();
                        pcodlinea.ParameterName = "p_cod_linea";
                        pcodlinea.Value = pDebitoAutomatico.cod_linea;
                        pcodlinea.Direction = ParameterDirection.Input;
                        pcodlinea.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodlinea);


                        DbParameter pnumproducto = cmdTransaccionFactory.CreateParameter();
                        pnumproducto.ParameterName = "p_num_producto";
                        pnumproducto.Value = pDebitoAutomatico.numero_producto;
                        pnumproducto.Direction = ParameterDirection.Input;
                        pnumproducto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumproducto);


                        DbParameter p_numcuentaahorro = cmdTransaccionFactory.CreateParameter();
                        p_numcuentaahorro.ParameterName = "p_num_cuenta_ahorro";
                        p_numcuentaahorro.Value = pDebitoAutomatico.numero_cuenta_ahorro;
                        p_numcuentaahorro.Direction = ParameterDirection.Input;
                        p_numcuentaahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_numcuentaahorro);





                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DEB_AUT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDebitoAutomatico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ModificarDebitoAutomatico", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDebitoAutomatico(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DebitoAutomatico pDebitoAutomatico = new DebitoAutomatico();
                       // pDebitoAutomatico = ConsultarCategorias(pId, vUsuario);

                        DbParameter PCONSECUTIVO = cmdTransaccionFactory.CreateParameter();
                        PCONSECUTIVO.ParameterName = "P_CONSECUTIVO";
                        PCONSECUTIVO.Value = pId;
                        PCONSECUTIVO.Direction = ParameterDirection.Input;
                        PCONSECUTIVO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCONSECUTIVO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DEB_AUT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "EliminarDebitoAutomatico", ex);
                    }
                }
            }
        }


        public DebitoAutomatico ConsultarDebitoAutomatico(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            DebitoAutomatico entidad = new DebitoAutomatico();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CARTERA_DEBITO_AUTOMATICO WHERE COD_PERSONA = '" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PRODUCTO"] != DBNull.Value) entidad.cod_producto = Convert.ToInt64(resultado["COD_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToInt64(resultado["COD_LINEA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ConsultarDebitoAutomatico", ex);
                        return null;
                    }
                }
            }
        }

        public DebitoAutomatico ConsultarDatosCliente(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            DebitoAutomatico entidad = new DebitoAutomatico();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CARTERA_DEBITO_AUTOMATICO.consecutivo,v_persona.COD_PERSONA,IDENTIFICACION,NOMBRES|| ' '||APELLIDOS AS nOMBRES,COD_OFICINA,TI.DESCRIPCION AS TIPO_IDENTIFICACION, 
                        (select distinct(numero_cuenta_ahorro) from  CARTERA_DEBITO_AUTOMATICO where  cod_persona=v_persona.cod_persona) as  numero_cuenta_ahorro 
                        FROM v_persona LEFT JOIN TIPOIDENTIFICACION TI ON TI.CODTIPOIDENTIFICACION=v_persona.TIPO_IDENTIFICACION 
                        left join  CARTERA_DEBITO_AUTOMATICO on CARTERA_DEBITO_AUTOMATICO.cod_persona=v_persona.cod_persona  WHERE v_persona.cod_persona = '" + pId.ToString() + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipoidentificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["numero_cuenta_ahorro"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["numero_cuenta_ahorro"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ConsultarDatosCliente", ex);
                        return null;
                    }
                }
            }
        }

        public List<DebitoAutomatico> ListarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DebitoAutomatico> lstCategorias = new List<DebitoAutomatico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DebitoAutomatico " + ObtenerFiltro(pDebitoAutomatico) + " ORDER BY COD_PERSONA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DebitoAutomatico entidad = new DebitoAutomatico();
                            if (resultado["COD_PRODUCTO"] != DBNull.Value) entidad.cod_producto = Convert.ToInt64(resultado["COD_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToInt64(resultado["COD_LINEA"]);
                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ListarDebitoAutomatico", ex);
                        return null;
                    }
                }
            }
        }

        public List<DebitoAutomatico> ListarProductosClientes(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DebitoAutomatico> lstCategorias = new List<DebitoAutomatico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select tp.DESCRIPCION,vc.*,vp.identificacion,vp.nombres,vp.apellidos,(select distinct(numero_cuenta_ahorro) from  CARTERA_DEBITO_AUTOMATICO where  cod_persona=vc.cod_persona) as numero_cuenta_ahorro
                                        from v_productos_cliente vc
                                        inner join v_persona  vp on vp.cod_persona=vc.COD_PERSONA
                                        left join TIPOPRODUCTO tp on tp.COD_TIPO_PRODUCTO=vc.COD_TIPO_PRODUCTO where tp.cod_tipo_producto!=3 
                                        And NOT (tp.cod_tipo_producto = 1 And vc.linea In (Select g.cod_linea_aporte  From grupo_lineaaporte g Where g.cod_linea_aporte = vc.linea And g.principal = 0))    and  vp.identificacion= " + "'" + (pDebitoAutomatico.identificacion) + "'"+  " ORDER BY vp.identificacion ";


                          connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DebitoAutomatico entidad = new DebitoAutomatico();
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt64(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToInt64(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToInt64(resultado["LINEA"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["NOMBRE_LINEA"]);

                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_CUENTA_AHORRO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NUMERO_CUENTA_AHORRO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);

                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ListarClientes", ex);
                        return null;
                    }
                }
            }
        }

        public List<DebitoAutomatico> ListarProductosClientesDebAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DebitoAutomatico> lstCategorias = new List<DebitoAutomatico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /* string sql = @"select tp.DESCRIPCION as TIPO_PRODUCTO,ca.*,NOMBRELINEAPRODUCTO(ca.cod_tipo_producto,ca.linea)as NOMBRE_LINEA,vp.identificacion,vp.nombres,vp.apellidos
                                         from CARTERA_DEBITO_AUTOMATICO ca
                                         inner join v_persona  vp on vp.cod_persona=ca.COD_PERSONA
                                         left join TIPOPRODUCTO tp on tp.COD_TIPO_PRODUCTO=ca.COD_TIPO_PRODUCTO 
                                         where tp.cod_tipo_producto!=3 and  vp.identificacion= " + "'" + (pDebitoAutomatico.identificacion) + "'" + " ORDER BY vp.identificacion ";
                                         */

                        string sql = @"select ca.consecutivo,tp.DESCRIPCION as TIPO_PRODUCTO,v.*,ca.NUMERO_CUENTA_AHORRO,vp.identificacion,vp.nombres,vp.apellidos
                                        from v_productos_cliente v 
                                        Left Join CARTERA_DEBITO_AUTOMATICO ca On v.cod_tipo_producto = ca.cod_tipo_producto And v.numero_producto = ca.numero_producto
                                        Left join v_persona  vp on vp.cod_persona=v.COD_PERSONA
                                        left join TIPOPRODUCTO tp on tp.COD_TIPO_PRODUCTO=v.COD_TIPO_PRODUCTO where v.cod_tipo_producto!=3  And NOT (v.cod_tipo_producto = 1 And v.linea In (Select g.cod_linea_aporte 
                                        From grupo_lineaaporte g Where g.cod_linea_aporte = v.linea And g.principal = 0))    and   vp.identificacion= " + "'" + (pDebitoAutomatico.identificacion) + "'" + " ORDER BY vp.identificacion ";

                                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DebitoAutomatico entidad = new DebitoAutomatico();
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);

                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt64(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToInt64(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToInt64(resultado["LINEA"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["NOMBRE_LINEA"]);

                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_CUENTA_AHORRO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NUMERO_CUENTA_AHORRO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt64(resultado["CUOTA"]);

                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ListarProductosClientesDebAutomatico", ex);
                        return null;
                    }
                }
            }
        }



        public List<DebitoAutomatico> ListarProductosAhorrosClientes(DebitoAutomatico pDebitoAutomatico, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DebitoAutomatico> lstCategorias = new List<DebitoAutomatico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select vc.NUMERO_PRODUCTO,vc.LINEA from v_productos_cliente vc
                                        inner join v_persona  vp on vp.cod_persona=vc.COD_PERSONA
                                        left join TIPOPRODUCTO tp on tp.COD_TIPO_PRODUCTO=vc.COD_TIPO_PRODUCTO where  tp.cod_tipo_producto=3  and vp.identificacion= " + "'" + (pDebitoAutomatico.identificacion) + "'" + " ORDER BY vc.NUMERO_PRODUCTO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DebitoAutomatico entidad = new DebitoAutomatico();
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["LINEA"]);
                           
                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ListarClientes", ex);
                        return null;
                    }
                }
            }
        }
     
        public List<DebitoAutomatico> ListarDatosClientes(DebitoAutomatico pDebitoAutomatico,Int64 cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DebitoAutomatico> lstCategorias = new List<DebitoAutomatico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select vp.cod_persona,vp.identificacion,vp.nombres,vp.apellidos,vp.cod_oficina,(select distinct(numero_cuenta_ahorro)  from  CARTERA_DEBITO_AUTOMATICO where  cod_persona=vp.cod_persona) as numero_cuenta_ahorro,o.NOMBRE as OFICINA from v_persona  vp left join oficina o on o.cod_oficina=vp.cod_oficina 
                                        " + ObtenerFiltro(pDebitoAutomatico);

                        string sql1 = " where 1=1 and  (select distinct(numero_cuenta_ahorro) from CARTERA_DEBITO_AUTOMATICO where cod_persona = vp.cod_persona)>0 ";
                        string sql2 = " ORDER BY vp.identificacion";
                        if (cuenta == 1)
                          sql = sql + sql1+sql2;
                        else
                           sql = sql  + sql2;



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DebitoAutomatico entidad = new DebitoAutomatico();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);

                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["numero_cuenta_ahorro"] != DBNull.Value) entidad.numero_cuenta_ahorro = Convert.ToString(resultado["numero_cuenta_ahorro"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["OFICINA"]);

                            lstCategorias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCategorias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DebitoAutomaticoData", "ListarClientes", ex);
                        return null;
                    }
                }
            }
        }

    }
}
