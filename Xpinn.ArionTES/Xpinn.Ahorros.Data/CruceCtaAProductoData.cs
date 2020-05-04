using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Destinacion
    /// </summary>
    public class CruceCtaAProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Destinacion
        /// </summary>
        public CruceCtaAProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Solicitud_cruce_ahorro CrearSolicitud_cruce_ahorro(Solicitud_cruce_ahorro pSolicitud_cruce, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcruceahorro = cmdTransaccionFactory.CreateParameter();
                        pidcruceahorro.ParameterName = "p_idcruceahorro";
                        pidcruceahorro.Value = pSolicitud_cruce.idcruceahorro;
                        pidcruceahorro.Direction = ParameterDirection.Output;
                        pidcruceahorro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcruceahorro);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pSolicitud_cruce.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "p_num_producto";
                        pnum_producto.Value = pSolicitud_cruce.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pSolicitud_cruce.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "p_fecha_pago";
                        if (pSolicitud_cruce.fecha_pago == null)
                            pfecha_pago.Value = DBNull.Value;
                        else
                            pfecha_pago.Value = pSolicitud_cruce.fecha_pago;
                        pfecha_pago.Direction = ParameterDirection.Input;
                        pfecha_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "p_valor_pago";
                        pvalor_pago.Value = pSolicitud_cruce.valor_pago;
                        pvalor_pago.Direction = ParameterDirection.Input;
                        pvalor_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pSolicitud_cruce.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pSolicitud_cruce.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pSolicitud_cruce.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (pSolicitud_cruce.concepto == null)
                            pconcepto.Value = DBNull.Value;
                        else
                            pconcepto.Value = pSolicitud_cruce.concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSolicitud_cruce.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSolicitud_cruce.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pSolicitud_cruce.cod_ope == null)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pSolicitud_cruce.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pSolicitud_cruce.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        if (pSolicitud_cruce.ip == null)
                            pip.Value = DBNull.Value;
                        else
                            pip.Value = pSolicitud_cruce.ip;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);

                        DbParameter pmensaje_error = cmdTransaccionFactory.CreateParameter();
                        pmensaje_error.ParameterName = "p_mensaje_error";
                        pmensaje_error.Value = DBNull.Value;
                        pmensaje_error.Size = 8000;
                        pmensaje_error.DbType = DbType.String;
                        pmensaje_error.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pmensaje_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLICCRUCE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSolicitud_cruce.mensaje_error = Convert.ToString(pmensaje_error.Value);
                        if (pSolicitud_cruce.mensaje_error != null)
                        {
                            pError = pSolicitud_cruce.mensaje_error;
                            return null;
                        }
                        pSolicitud_cruce.idcruceahorro = Convert.ToInt64(pidcruceahorro.Value);
                        return pSolicitud_cruce;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public void CambiarEstadoSolicitud_CruceAhorro(Solicitud_cruce_ahorro p_solicitud, Usuario _usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(_usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        DbParameter pidcruceahorro = cmdTransaccionFactory.CreateParameter();
                        pidcruceahorro.ParameterName = "p_idcruceahorro";
                        pidcruceahorro.Value = p_solicitud.idcruceahorro;
                        pidcruceahorro.Direction = ParameterDirection.Input;
                        pidcruceahorro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcruceahorro);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = p_solicitud.tipo_producto;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLCRUCE_EST_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CruceCtaAProductoData", "CambiarEstadoSolicitud_CruceAhorro", ex);
                    }
                }
            }
        }

        public Solicitud_cruce_ahorro ModificarSolicitud_CruceAhorro(Solicitud_cruce_ahorro pSolicitud_cruce, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcruceahorro = cmdTransaccionFactory.CreateParameter();
                        pidcruceahorro.ParameterName = "p_idcruceahorro";
                        pidcruceahorro.Value = pSolicitud_cruce.idcruceahorro;
                        pidcruceahorro.Direction = ParameterDirection.Input;
                        pidcruceahorro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcruceahorro);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        ptipo_producto.Value = pSolicitud_cruce.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "p_num_producto";
                        pnum_producto.Value = pSolicitud_cruce.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pSolicitud_cruce.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_pago.ParameterName = "p_fecha_pago";
                        if (pSolicitud_cruce.fecha_pago == null)
                            pfecha_pago.Value = DBNull.Value;
                        else
                            pfecha_pago.Value = pSolicitud_cruce.fecha_pago;
                        pfecha_pago.Direction = ParameterDirection.Input;
                        pfecha_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_pago);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "p_valor_pago";
                        pvalor_pago.Value = pSolicitud_cruce.valor_pago;
                        pvalor_pago.Direction = ParameterDirection.Input;
                        pvalor_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        ptipo_tran.Value = pSolicitud_cruce.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pSolicitud_cruce.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pSolicitud_cruce.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (pSolicitud_cruce.concepto == null)
                            pconcepto.Value = DBNull.Value;
                        else
                            pconcepto.Value = pSolicitud_cruce.concepto;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSolicitud_cruce.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSolicitud_cruce.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pSolicitud_cruce.cod_ope == null)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pSolicitud_cruce.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pSolicitud_cruce.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter pip = cmdTransaccionFactory.CreateParameter();
                        pip.ParameterName = "p_ip";
                        if (pSolicitud_cruce.ip == null)
                            pip.Value = DBNull.Value;
                        else
                            pip.Value = pSolicitud_cruce.ip;
                        pip.Direction = ParameterDirection.Input;
                        pip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLICCRUCE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pSolicitud_cruce;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }

        public void EliminarSolicitud_Cruce_ahorro(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_idcruceahorro";
                        pcod_destino.Value = pId;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLICCRUCE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CruceCtaAProductoData", "EliminarDestinacion", ex);
                    }
                }
            }
        }


        public Solicitud_cruce_ahorro ConsultarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            Solicitud_cruce_ahorro entidad = new Solicitud_cruce_ahorro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.*, P.IDENTIFICACION, P.NOMBRE, O.NOMBRE AS NOM_OFICINA , T.DESCRIPCION AS NOM_TIPO_PRODUCTO,
                                        R.DESCRIPCION AS NOM_TIPO_TRAN, 
                                        CASE S.ESTADO WHEN 1 THEN 'Pendiente' WHEN 2 THEN 'Aprobado' end as nom_estado
                                        FROM SOLICITUD_CRUCE_AHORRO S INNER JOIN V_PERSONA P ON P.COD_PERSONA = S.COD_PERSONA
                                        LEFT JOIN OFICINA O ON O.COD_OFICINA = P.COD_OFICINA
                                        LEFT JOIN TIPOPRODUCTO T ON T.COD_TIPO_PRODUCTO = S.TIPO_PRODUCTO
                                        LEFT JOIN TIPO_TRAN R ON R.TIPO_TRAN = S.TIPO_TRAN " + pFiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCRUCEAHORRO"] != DBNull.Value) entidad.idcruceahorro = Convert.ToInt64(resultado["IDCRUCEAHORRO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToDecimal(resultado["VALOR_PAGO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_TRAN"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["NOM_TIPO_TRAN"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Solicitud_cruceData", "ConsultarSolicitud_cruce", ex);
                        return null;
                    }
                }
            }
        }




        public List<Solicitud_cruce_ahorro> ListarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Solicitud_cruce_ahorro> lstSolicitud = new List<Solicitud_cruce_ahorro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.*, P.IDENTIFICACION, P.NOMBRE, O.NOMBRE AS NOM_OFICINA , T.DESCRIPCION AS NOM_TIPO_PRODUCTO,
                                        R.DESCRIPCION AS NOM_TIPO_TRAN, 
                                        CASE S.ESTADO WHEN 0 THEN 'Solicitado' WHEN 1 THEN 'Autorizado' WHEN 2 THEN 'Rechazado' end as NOM_ESTADO
                                        ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                        FROM SOLICITUD_CRUCE_AHORRO S INNER JOIN V_PERSONA P ON P.COD_PERSONA = S.COD_PERSONA
                                        LEFT JOIN OFICINA O ON O.COD_OFICINA = P.COD_OFICINA
                                        LEFT JOIN TIPOPRODUCTO T ON T.COD_TIPO_PRODUCTO = S.TIPO_PRODUCTO
                                        LEFT JOIN TIPO_TRAN R ON R.TIPO_TRAN = S.TIPO_TRAN
                                        left join Asejecutivos aj on Aj.Icodigo = p.Cod_Asesor
                                        WHERE S.ESTADO IS NOT NULL " + pFiltro.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Solicitud_cruce_ahorro entidad = new Solicitud_cruce_ahorro();
                            if (resultado["IDCRUCEAHORRO"] != DBNull.Value) entidad.idcruceahorro = Convert.ToInt64(resultado["IDCRUCEAHORRO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToDecimal(resultado["VALOR_PAGO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["IP"] != DBNull.Value) entidad.ip = Convert.ToString(resultado["IP"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_TRAN"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["NOM_TIPO_TRAN"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.otro_atributo = Convert.ToString(resultado["NOMBREEJE"]);
                            
                            lstSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Solicitud_cruceData", "ListarSolicitud_cruce", ex);
                        return null;
                    }
                }
            }
        }

        
    }
}
