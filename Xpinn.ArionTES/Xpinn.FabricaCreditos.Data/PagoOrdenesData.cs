using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class PagoOrdenesFabricaCreditosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public PagoOrdenesFabricaCreditosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        

        public PagoOrdenes ModificarPagoOrdenes(PagoOrdenes pPagoOrdenes, ref string pError, Usuario vUsuario)
        {
            pError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidordenservicio = cmdTransaccionFactory.CreateParameter();
                        pidordenservicio.ParameterName = "p_idordenservicio";
                        pidordenservicio.Value = pPagoOrdenes.idordenservicio;
                        pidordenservicio.Direction = ParameterDirection.Input;
                        pidordenservicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidordenservicio);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        pnumero_radicacion.Value = pPagoOrdenes.numero_radicacion;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pPagoOrdenes.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pPagoOrdenes.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidproveedor = cmdTransaccionFactory.CreateParameter();
                        pidproveedor.ParameterName = "p_idproveedor";
                        if (pPagoOrdenes.idproveedor == null)
                            pidproveedor.Value = DBNull.Value;
                        else
                            pidproveedor.Value = pPagoOrdenes.idproveedor;
                        pidproveedor.Direction = ParameterDirection.Input;
                        pidproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidproveedor);

                        DbParameter pnomproveedor = cmdTransaccionFactory.CreateParameter();
                        pnomproveedor.ParameterName = "p_nomproveedor";
                        if (pPagoOrdenes.nomproveedor == null)
                            pnomproveedor.Value = DBNull.Value;
                        else
                            pnomproveedor.Value = pPagoOrdenes.nomproveedor;
                        pnomproveedor.Direction = ParameterDirection.Input;
                        pnomproveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnomproveedor);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        if (pPagoOrdenes.detalle == null)
                            pdetalle.Value = DBNull.Value;
                        else
                            pdetalle.Value = pPagoOrdenes.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        if (pPagoOrdenes.estado == null)
                            p_estado.Value = DBNull.Value;
                        else
                            p_estado.Value = pPagoOrdenes.estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDITOORDEN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pPagoOrdenes;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }


        public List<PagoOrdenes> ConsultarPagoOrdenes(string pFiltro,Usuario vUsuario)
        {
            DbDataReader resultado;
            
            List<PagoOrdenes> lstPagoOrdenes = new List<PagoOrdenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.COD_PERSONA,S.IDPROVEEDOR, S.NOMPROVEEDOR, S.IDORDENSERVICIO, C.NUMERO_RADICACION, C.COD_LINEA_CREDITO, 
                                        L.NOMBRE AS NOM_LINEA_CREDITO, P.IDENTIFICACION, P.NOMBRE, C.FECHA_SOLICITUD, C.FECHA_DESEMBOLSO, C.MONTO_SOLICITADO,
                                        C.MONTO_APROBADO, C.MONTO_DESEMBOLSADO, C.VALOR_CUOTA, O.NOMBRE AS OFICINA, C.ESTADO, S.NUMERO_PREIMPRESO,
                                        (SELECT SUM(A.VALOR_APROBADO) FROM AUXILIOS A WHERE A.NUMERO_RADICACION = C.NUMERO_RADICACION) AS VALOR_AUXILIO 
                                        FROM CREDITO C 
                                        INNER JOIN CREDITO_ORDEN_SERVICIO S ON C.NUMERO_RADICACION = S.NUMERO_RADICACION 
                                        INNER JOIN V_PERSONA P ON C.COD_DEUDOR = P.COD_PERSONA 
                                        INNER JOIN LINEASCREDITO L ON C.COD_LINEA_CREDITO = L.COD_LINEA_CREDITO 
                                        INNER JOIN OFICINA O ON C.COD_OFICINA = O.COD_OFICINA 
                                        WHERE C.ESTADO In ('C', 'T') AND (S.ESTADO IS NULL OR S.ESTADO = 0) " + pFiltro + " ORDER BY C.NUMERO_RADICACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PagoOrdenes entidad = new PagoOrdenes();
                            if (resultado["IDORDENSERVICIO"] != DBNull.Value) entidad.idordenservicio = Convert.ToInt64(resultado["IDORDENSERVICIO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idproveedor = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nomproveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);                     
                            if (resultado["NOM_LINEA_CREDITO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);                            
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);                            
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["VALOR_AUXILIO"] != DBNull.Value) entidad.valor_auxilio = Convert.ToDecimal(resultado["VALOR_AUXILIO"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.numero_preimpreso = Convert.ToString(resultado["NUMERO_PREIMPRESO"]);
                            if (entidad.estado == "C" && entidad.monto_aprobado == 0)
                                entidad.monto_aprobado = entidad.monto_solicitado;
                            entidad.monto_aprobado += entidad.valor_auxilio;
                            lstPagoOrdenes.Add(entidad);
                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagoOrdenes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoOrdenesData", "ConsultarPagoOrdenes", ex);
                        return null;
                    }
                }
            }
        }


        public List<PagoOrdenes> ListarPagoOrdenes(PagoOrdenes pPagoOrdenes, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PagoOrdenes> lstPagoOrdenes = new List<PagoOrdenes>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM credito_orden_servicio " + ObtenerFiltro(pPagoOrdenes) + " ORDER BY IDORDENSERVICIO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PagoOrdenes entidad = new PagoOrdenes();
                            if (resultado["IDORDENSERVICIO"] != DBNull.Value) entidad.idordenservicio = Convert.ToInt64(resultado["IDORDENSERVICIO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDPROVEEDOR"] != DBNull.Value) entidad.idproveedor = Convert.ToString(resultado["IDPROVEEDOR"]);
                            if (resultado["NOMPROVEEDOR"] != DBNull.Value) entidad.nomproveedor = Convert.ToString(resultado["NOMPROVEEDOR"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            lstPagoOrdenes.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagoOrdenes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoOrdenesData", "ListarPagoOrdenes", ex);
                        return null;
                    }
                }
            }
        }

        public string TipoConexion(Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    return dbConnectionFactory.TipoConexion();
                }
            }
        }


    }
}