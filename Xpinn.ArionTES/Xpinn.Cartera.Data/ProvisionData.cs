using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ProvisionData : GlobalData
    {

         protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
         public ProvisionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        

        #region PROVISION DE CREDITOS

         public List<Provision> ListarProvisiones(string filtro, Usuario pUsuario)
         {
             DbDataReader resultado = default(DbDataReader);
             List<Provision> lstPrograma = new List<Provision>();

             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         Configuracion conf = new Configuracion();
                         string sql = @"SELECT P.IDPROVISION,H.NUMERO_RADICACION,H.COD_OFICINA,O.NOMBRE AS NOMBRE_OFICINA, H.COD_CLIENTE,v.identificacion, V.NOMBRE,
                                    H.COD_LINEA_CREDITO || '-' || L.NOMBRE AS NOMBRE_LINEA,H.COD_CATEGORIA, P.COD_ATR || '-' || A.NOMBRE AS NOMBRE_ATRIBUTO,
                                    P.BASE_PROVISION,P.PORC_PROVISION, P.VALOR_PROVISION, P.APORTE_RESTA, P.DIFERENCIA_PROVISION, P.DIFERENCIA_ACTUAL, P.DIFERENCIA_ANTERIOR                                 
                                    FROM historico_cre h 
                                    INNER JOIN provision p On h.fecha_historico = p.fecha_corte And h.numero_radicacion = p.numero_radicacion 
                                    INNER JOIN atributos a On a.cod_atr = p.cod_atr
                                    INNER JOIN lineascredito l On h.cod_linea_credito = l.cod_linea_credito                                    
                                    INNER JOIN OFICINA O ON H.COD_OFICINA = O.COD_OFICINA                                    
                                    INNER JOIN v_persona v On h.cod_cliente = v.cod_persona ";
                         if (filtro.Trim() != "")
                             sql += filtro;
                         sql += " ORDER BY H.NUMERO_RADICACION,H.cod_oficina";

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.Text;
                         cmdTransaccionFactory.CommandText = sql;
                         resultado = cmdTransaccionFactory.ExecuteReader();

                         while (resultado.Read())
                         {
                             Provision entidad = new Provision();
                             if (resultado["IDPROVISION"] != DBNull.Value) entidad.idprovision = Convert.ToInt32(resultado["IDPROVISION"]);
                             if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                             if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                             if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                             if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                             if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                             if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);                             
                             if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                             if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                             if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                             if (resultado["BASE_PROVISION"] != DBNull.Value) entidad.base_provision = Convert.ToDecimal(resultado["BASE_PROVISION"]);
                             if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.porc_provision = Convert.ToDecimal(resultado["PORC_PROVISION"]);
                             if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.valor_provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
                             if (resultado["APORTE_RESTA"] != DBNull.Value) entidad.aporte_resta = Convert.ToDecimal(resultado["APORTE_RESTA"]);
                             if (resultado["DIFERENCIA_PROVISION"] != DBNull.Value) entidad.diferencia_provision = Convert.ToInt32(resultado["DIFERENCIA_PROVISION"]);
                             if (resultado["DIFERENCIA_ACTUAL"] != DBNull.Value) entidad.diferencia_actual = Convert.ToInt32(resultado["DIFERENCIA_ACTUAL"]);
                             if (resultado["DIFERENCIA_ANTERIOR"] != DBNull.Value) entidad.diferencia_anterior = Convert.ToInt32(resultado["DIFERENCIA_ANTERIOR"]);
                             lstPrograma.Add(entidad);
                         }
                         return lstPrograma;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProvisionData", "ListarProvisiones", ex);
                         return null;
                     }
                 }
             }
         }



         public Provision ModificarProvision(Provision pProvision, Usuario vUsuario)
         {
             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                         pidprovision.ParameterName = "p_idprovision";
                         pidprovision.Value = pProvision.idprovision;
                         pidprovision.Direction = ParameterDirection.Input;
                         pidprovision.DbType = DbType.Int32;
                         cmdTransaccionFactory.Parameters.Add(pidprovision);

                         DbParameter pvalor_provision = cmdTransaccionFactory.CreateParameter();
                         pvalor_provision.ParameterName = "p_valor_provision";
                         pvalor_provision.Value = pProvision.valor_provision;
                         pvalor_provision.Direction = ParameterDirection.Input;
                         pvalor_provision.DbType = DbType.Decimal;
                         cmdTransaccionFactory.Parameters.Add(pvalor_provision);
                         
                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                         cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_PROVISION_MOD";
                         cmdTransaccionFactory.ExecuteNonQuery();
                         dbConnectionFactory.CerrarConexion(connection);
                         return pProvision;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProvisionData", "ModificarProvision", ex);
                         return null;
                     }
                 }
             }
         }



         public Provision CrearAuditoriaProvision(Provision pProvision, Usuario vUsuario)
         {
             using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
             {
                 using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                 {
                     try
                     {
                         DbParameter pidprovision = cmdTransaccionFactory.CreateParameter();
                         pidprovision.ParameterName = "p_idprovision";
                         pidprovision.Value = pProvision.idprovision;
                         pidprovision.Direction = ParameterDirection.Input;
                         pidprovision.DbType = DbType.Int32;
                         cmdTransaccionFactory.Parameters.Add(pidprovision);

                         DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                         pfecha_corte.ParameterName = "p_fecha_corte";
                         if (pProvision.fecha_corte == null)
                             pfecha_corte.Value = DBNull.Value;
                         else
                             pfecha_corte.Value = pProvision.fecha_corte;
                         pfecha_corte.Direction = ParameterDirection.Input;
                         pfecha_corte.DbType = DbType.DateTime;
                         cmdTransaccionFactory.Parameters.Add(pfecha_corte);

                         DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                         pvalor.ParameterName = "p_valor";
                         pvalor.Value = pProvision.vr_provision_anterior;
                         pvalor.Direction = ParameterDirection.Input;
                         pvalor.DbType = DbType.Decimal;
                         cmdTransaccionFactory.Parameters.Add(pvalor);

                         DbParameter pusuario_cambio = cmdTransaccionFactory.CreateParameter();
                         pusuario_cambio.ParameterName = "p_usuario_cambio";
                         pusuario_cambio.Value = vUsuario.nombre;
                         pusuario_cambio.Direction = ParameterDirection.Input;
                         pusuario_cambio.DbType = DbType.String;
                         cmdTransaccionFactory.Parameters.Add(pusuario_cambio);

                         DbParameter pfecha_cambio = cmdTransaccionFactory.CreateParameter();
                         pfecha_cambio.ParameterName = "p_fecha_cambio";
                         pfecha_cambio.Value = DateTime.Now;
                         pfecha_cambio.Direction = ParameterDirection.Input;
                         pfecha_cambio.DbType = DbType.DateTime;
                         cmdTransaccionFactory.Parameters.Add(pfecha_cambio);

                         DbParameter pip = cmdTransaccionFactory.CreateParameter();
                         pip.ParameterName = "p_ip";
                         if (vUsuario.IP == null)
                             pip.Value = DBNull.Value;
                         else
                             pip.Value = vUsuario.IP;
                         pip.Direction = ParameterDirection.Input;
                         pip.DbType = DbType.String;
                         cmdTransaccionFactory.Parameters.Add(pip);

                         connection.Open();
                         cmdTransaccionFactory.Connection = connection;
                         cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                         cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_AUDPROVISI_CREAR";
                         cmdTransaccionFactory.ExecuteNonQuery();
                         dbConnectionFactory.CerrarConexion(connection);
                         return pProvision;
                     }
                     catch (Exception ex)
                     {
                         BOExcepcion.Throw("ProvisionData", "CrearAuditoriaProvision", ex);
                         return null;
                     }
                 }
             }
         }


        #endregion

        #region calificacion
        public List<Provision> ListarClasificaciones(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Provision> lstPrograma = new List<Provision>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT H.CONSECUTIVO, H.NUMERO_RADICACION, H.COD_OFICINA, O.NOMBRE AS NOMBRE_OFICINA, H.COD_CLIENTE, v.IDENTIFICACION, V.NOMBRE,
                                    H.COD_LINEA_CREDITO || '-' || L.NOMBRE AS NOMBRE_LINEA, H.COD_CATEGORIA, H.SALDO_CAPITAL, H.FECHA_PROXIMO_PAGO, H.DIAS_MORA, H.COD_CATEGORIA_CLI
                                    FROM historico_cre h                                     
                                    INNER JOIN lineascredito l On h.cod_linea_credito = l.cod_linea_credito                                    
                                    INNER JOIN oficina O ON H.cod_oficina = O.cod_oficina                                    
                                    INNER JOIN v_persona v On h.cod_cliente = v.cod_persona ";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY H.NUMERO_RADICACION,H.cod_oficina";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Provision entidad = new Provision();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIAS_MORA"]);
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionData", "ListarClasificaciones", ex);
                        return null;
                    }
                }
            }
        }

        public Provision ModificarClasificacion(Provision pProvision, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pProvision.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter p_cod_categoria = cmdTransaccionFactory.CreateParameter();
                        p_cod_categoria.ParameterName = "p_cod_categoria";
                        p_cod_categoria.Value = pProvision.cod_categoria;
                        p_cod_categoria.Direction = ParameterDirection.Input;
                        p_cod_categoria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_categoria);

                        DbParameter pusuario_cambio = cmdTransaccionFactory.CreateParameter();
                        pusuario_cambio.ParameterName = "p_usuario_cambio";
                        pusuario_cambio.Value = vUsuario.nombre;
                        pusuario_cambio.Direction = ParameterDirection.Input;
                        pusuario_cambio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuario_cambio);

                        DbParameter pfecha_cambio = cmdTransaccionFactory.CreateParameter();
                        pfecha_cambio.ParameterName = "p_fecha_cambio";
                        pfecha_cambio.Value = DateTime.Now;
                        pfecha_cambio.Direction = ParameterDirection.Input;
                        pfecha_cambio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_cambio);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        if (vUsuario.IP == null)
                            pip.Value = DBNull.Value;
                        else
                            pip.Value = vUsuario.IP;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CALIFICACION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProvisionData", "ModificarClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        #endregion



    }
}
