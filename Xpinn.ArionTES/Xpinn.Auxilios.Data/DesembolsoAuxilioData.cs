using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.Auxilios.Data
{   
    public class DesembolsoAuxilioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public DesembolsoAuxilioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<SolicitudAuxilio> ListarSolicitudAuxilio(string filtro, DateTime pFechaSol,string orden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SolicitudAuxilio> lstAuxilio = new List<SolicitudAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.numero_auxilio,a.fecha_solicitud ,a.fecha_aprobacion, l.descripcion  ,p.identificacion, " 
                                        +"p.primer_nombre||' '|| p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as nombre, p.cod_nomina, a.valor_solicitado, "
                                        + "a.valor_aprobado,(select Max(c.observaciones) as observaciones from controlauxilios c where c.numero_auxilios = a.numero_auxilio) as observaciones "
                                        +"from auxilios a left join lineasauxilios l on a.cod_linea_auxilio = l.cod_linea_auxilio "
                                        + "left join persona p on p.cod_persona = a.cod_persona where 1=1" + filtro;

                        Configuracion conf = new Configuracion();
                        if (pFechaSol != null && pFechaSol != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " a.fecha_solicitud = To_Date('" + Convert.ToDateTime(pFechaSol).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " a.fecha_solicitud = '" + Convert.ToDateTime(pFechaSol).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (orden != "")
                            sql += " ORDER BY" + orden;
                        else
                            sql += " ORDER BY a.numero_auxilio ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SolicitudAuxilio entidad = new SolicitudAuxilio();
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                            lstAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoAuxilioData", "ListarSolicitudAuxilio", ex);
                        return null;
                    }
                }
            }
        }



        public AprobacionAuxilio DesembolsarAuxilios(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pAuxilio.numero_auxilios;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);

                        DbParameter pfecha_desembolso = cmdTransaccionFactory.CreateParameter();
                        pfecha_desembolso.ParameterName = "p_fecha_desembolso";
                        pfecha_desembolso.Value = pAuxilio.fecha_desembolso;
                        pfecha_desembolso.Direction = ParameterDirection.Input;
                        pfecha_desembolso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_desembolso);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = "D";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_AUXILIOS_DESEM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoAuxilioData", "DesembolsarAuxilios", ex);
                        return null;
                    }
                }
            }
        }



        
        public DesembolsoAuxilio CrearTran_Auxilio(DesembolsoAuxilio pDesembolso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pDesembolso.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);
 
                        DbParameter pnumero_auxilio = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilio.ParameterName = "p_numero_auxilio";
                        pnumero_auxilio.Value = pDesembolso.numero_auxilio;
                        pnumero_auxilio.Direction = ParameterDirection.Input;
                        pnumero_auxilio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilio);
 
                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pDesembolso.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);
 
                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "p_cod_cliente";
                        pcod_cliente.Value = pDesembolso.cod_cliente;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);
 
                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pDesembolso.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);
 
                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pDesembolso.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);
 
                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDesembolso.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);
 
                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pDesembolso.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);
 
                        DbParameter pnum_tran_anula = cmdTransaccionFactory.CreateParameter();
                        pnum_tran_anula.ParameterName = "p_num_tran_anula";
                        if (pDesembolso.num_tran_anula != 0) pnum_tran_anula.Value = pDesembolso.num_tran_anula; else pnum_tran_anula.Value = DBNull.Value;
                        pnum_tran_anula.Direction = ParameterDirection.Input;
                        pnum_tran_anula.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran_anula);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_TRAN_AUXIL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDesembolso.numero_transaccion = Convert.ToInt64(pnumero_transaccion.Value);
                        return pDesembolso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DesembolsoAuxilioData", "CrearTran_Auxilio", ex);
                        return null;
                    }
                }
            }
        }


        public SolicitudAuxilio ConsultarAuxilioAprobado(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudAuxilio entidad = new SolicitudAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT auxilios.*,oficina.nombre as nombreoficina,persona.cod_oficina,persona.identificacion,persona.primer_nombre "
                                        + "||' '|| persona.segundo_nombre||' '|| persona.primer_apellido||' '||persona.segundo_apellido as Nombre, "
                                        +"(select Max(c.observaciones) as observaciones from controlauxilios c where c.numero_auxilios = auxilios.numero_auxilio) as observaciones "
                                        + "FROM auxilios inner join persona "
                                        + "on persona.cod_persona = auxilios.cod_persona  inner join oficina on persona.cod_oficina=oficina.cod_oficina where numero_auxilio = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.numero_auxilio = Convert.ToInt32(resultado["NUMERO_AUXILIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["VALOR_SOLICITADO"] != DBNull.Value) entidad.valor_solicitado = Convert.ToDecimal(resultado["VALOR_SOLICITADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR_APROBADO"] != DBNull.Value) entidad.valor_aprobado = Convert.ToDecimal(resultado["VALOR_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["NOMBREOFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBREOFICINA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACIONES"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudAuxilioData", "ConsultarAUXILIO", ex);
                        return null;
                    }
                }
            }
        }



        public CuentasBancarias ConsultarCuentasBancarias(CuentasBancarias pId, string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasBancarias entidad = new CuentasBancarias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PERSONA_CUENTASBANCARIAS WHERE COD_PERSONA = " + pId.cod_persona;
                        if (filtro != "")
                            sql += filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            
                            if (resultado["IDCUENTABANCARIA"] != DBNull.Value) entidad.idcuentabancaria = Convert.ToInt64(resultado["IDCUENTABANCARIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["SUCURSAL"] != DBNull.Value) entidad.sucursal = Convert.ToString(resultado["SUCURSAL"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPersonaData", "ConsultarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }


    }
}
