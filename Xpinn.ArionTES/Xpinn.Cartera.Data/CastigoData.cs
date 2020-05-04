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
    public class CastigoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Castigo
        /// </summary>
        public CastigoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Listar créditos a Castigo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

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
                        BOExcepcion.Throw("CastigoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }

        public Castigo CrearCastigo(Castigo pcastigo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "pnumero_radicacion";
                        pNUMERO_RADICACION.Value = pcastigo.numero_radicacion;

                        DbParameter pFECHA_CASTIGO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_CASTIGO.ParameterName = "pfecha_castigo";
                        pFECHA_CASTIGO.Value = pcastigo.fecha_castigo;

                        DbParameter pCOD_LINEA_CASTIGO = cmdTransaccionFactory.CreateParameter();
                        pCOD_LINEA_CASTIGO.ParameterName = "pcod_linea_castigo";
                        pCOD_LINEA_CASTIGO.Value = pcastigo.cod_linea_castigo;

                        DbParameter pCODIGO_USUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_USUARIO.ParameterName = "pcod_usu";
                        pCODIGO_USUARIO.Value = pUsuario.codusuario;

                        DbParameter pCODIGO_OFICINA = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_OFICINA.ParameterName = "pcod_ofi";
                        pCODIGO_OFICINA.Value = pUsuario.cod_oficina;

                        DbParameter pCODIGO_OPERACION = cmdTransaccionFactory.CreateParameter();
                        pCODIGO_OPERACION.ParameterName = "pcod_ope";
                        pCODIGO_OPERACION.Value = pcastigo.cod_ope;
                        pCODIGO_OPERACION.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_CASTIGO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_LINEA_CASTIGO);                                               
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_USUARIO);
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_OFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCODIGO_OPERACION); 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CASTIGO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pcastigo, "CASTIGO", pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pcastigo.cod_ope = Convert.ToInt64(pCODIGO_OPERACION.Value);

                        return pcastigo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CastigoData", "CrearCastigo", ex);
                        return null;
                    }
                }
            }
        }

    }
}
