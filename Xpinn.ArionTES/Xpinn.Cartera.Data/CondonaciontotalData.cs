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
    /// <summary>
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class CondonaciontotalData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public CondonaciontotalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

     
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Credito dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CondonacionTotal> ListarCredito(CondonacionTotal pCredito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CondonacionTotal> lstCredito = new List<CondonacionTotal>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From v_creditos " + ObtenerFiltro(pCredito);
                        if (sql.ToUpper().Contains("WHERE"))
                            sql += " And estado = 'C'";
                        else
                            sql += " Where estado = 'C'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CondonacionTotal entidad = new CondonacionTotal();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);                                                        
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);

                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondonacionTotalData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea un registro de la condonacion  total de la base de datos
        /// </summary>
        /// <param name="pDiligencia">Entidad Condonacion</param>
        /// <returns>Entidad Condonacion creada</returns>
        public CondonacionTotal CrearCondonacion(CondonacionTotal pcondonacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODIGO_OPERACION = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_OPERACION.ParameterName = "pcod_ope";
                        pCODIGO_OPERACION.Value = pcondonacion.codigo;
                        pCODIGO_OPERACION.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHA_CONDONACION = cmdTransaccionFactory.CreateParameter();
                        pFECHA_CONDONACION.ParameterName = "pfecha_condonacion";
                        pFECHA_CONDONACION.Value = pcondonacion.fecha_condonacion;
                       

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERO_RADICACION.Value = pcondonacion.numero_radicacion;
                        
                                  
                        DbParameter pCODIGO_USUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_USUARIO.ParameterName = "pcod_usu";
                        pCODIGO_USUARIO.Value = pcondonacion.codigo_usuario;

                        DbParameter pCODIGO_OFICINA= cmdTransaccionFactory.CreateParameter();
                        pCODIGO_OFICINA.ParameterName = "pcod_ofi";
                        pCODIGO_OFICINA.Value = pcondonacion.codigo_oficina;

                        cmdTransaccionFactory.Parameters.Add(pCODIGO_OPERACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_CONDONACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                              cmdTransaccionFactory.Parameters.Add(pCODIGO_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_OFICINA);
                      

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CONDONATOTAL";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pDiligencia, "Diligencia",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pcondonacion.codigo = Convert.ToInt64(pCODIGO_OPERACION.Value);
                        return pcondonacion;
                    }                                        
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondonacionototalData", "CrearCondonacion", ex);
                        return null;
                    }
                }
            }
        }

    }
}