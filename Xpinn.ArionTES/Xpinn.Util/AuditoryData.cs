using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;

namespace Xpinn.Util
{
    /// <summary>
    /// Objeto para registro de auditoria del sistema
    /// </summary>
    public class AuditoryData
    {
        protected ConnectionDataBase dbConnectionFactory = new ConnectionDataBase();
        protected DbCommand cmdTransaccionFactory;
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        protected enum Accion { Crear = 1, Listar = 2, Detalle = 3, Modificar = 4, Eliminar = 5 };

         /// <summary>
        /// Constructor del objeto 
        /// </summary>
        public AuditoryData()
        {
            BOExcepcion = new ExcepcionBusiness();
        }

        /// <summary>
        /// Registra la transaccion realizada por un usuario en una tabla
        /// </summary>
        /// <param name="pEntidad">Conjunto de datos de la transaccion</param>
        /// <param name="pTabla">Nombre de la Tabla sobre la que se ejecuta la operacion</param>
        /// <param name="pUsuario">Datos del usuario que ejecuta la accion</param>
        /// <param name="pAccion">Accion que realiza (I) Insetar, (M) Modificar, (E) Eliminar, (C) Consultar</param>
        /// <param name="observacion">Observacion que se quiera dejar opcional para la auditoria</param>
        /// <param name="tipoAuditoria">Tipo de auditoria</param>
        /// <param name="informacionEntidadAnterior">Entidad que tiene los datos antes de que sean modificados, si no es modificacion llegara nulo</param>
        public void InsertarLog(Object pEntidad, string pTabla, Usuario pUsuario, string pAccion, TipoAuditoria tipoAuditoria = TipoAuditoria.SinTipoAuditoria, string observacion = " ", object informacionEntidadAnterior = null)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                       
                        DbParameter pCOD_AUDITORIA = cmdTransaccionFactory.CreateParameter();
                        pCOD_AUDITORIA.ParameterName = "p_cod_auditoria";
                        pCOD_AUDITORIA.Value = ObtenerSiguienteCodigo(pUsuario);
                        pCOD_AUDITORIA.Direction = ParameterDirection.InputOutput;

                        DbParameter pCODUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pCODUSUARIO.ParameterName = "p_codusuario";
                        pCODUSUARIO.Value = pUsuario.codusuario;

                        DbParameter pCODOPCION = cmdTransaccionFactory.CreateParameter();
                        pCODOPCION.ParameterName = "p_codOpcion";
                        pCODOPCION.Value = pUsuario.codOpcionActual;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_fecha";
                        pFECHA.Value = DateTime.Now;

                        DbParameter pIP = cmdTransaccionFactory.CreateParameter();
                        pIP.ParameterName = "p_ip";
                        pIP.Value = pUsuario.IP;

                        DbParameter pNAVEGADOR = cmdTransaccionFactory.CreateParameter();
                        pNAVEGADOR.ParameterName = "p_navegador";
                        pNAVEGADOR.Value = pUsuario.navegador;

                        DbParameter pACCION = cmdTransaccionFactory.CreateParameter();
                        pACCION.ParameterName = "p_accion";
                        pACCION.Value = pAccion;

                        DbParameter pTABLA = cmdTransaccionFactory.CreateParameter();
                        pTABLA.ParameterName = "p_tabla";
                        pTABLA.Value = pTabla;

                        DbParameter pDETALLE = cmdTransaccionFactory.CreateParameter();
                        pDETALLE.ParameterName = "p_detalle";
                        pDETALLE.Value = ObtenerDatos(pEntidad);

                        DbParameter p_tipoAuditoria = cmdTransaccionFactory.CreateParameter();
                        p_tipoAuditoria.ParameterName = "p_tipoAuditoria";
                        p_tipoAuditoria.Value = (int)tipoAuditoria;

                        DbParameter p_observacion = cmdTransaccionFactory.CreateParameter();
                        p_observacion.ParameterName = "p_observacion";
                        if (!string.IsNullOrWhiteSpace(observacion))
                        {
                            p_observacion.Value = observacion;
                        }
                        else
                        {
                            p_observacion.Value = DBNull.Value;
                        }

                        DbParameter p_informacionAnterior = cmdTransaccionFactory.CreateParameter();
                        p_informacionAnterior.ParameterName = "p_informacionAnterior";
                        if (informacionEntidadAnterior != null)
                        {
                            p_informacionAnterior.Value = ObtenerDatos(informacionEntidadAnterior);
                        }
                        else
                        {
                            p_informacionAnterior.Value = DBNull.Value;
                        }

                        cmdTransaccionFactory.Parameters.Add(pCOD_AUDITORIA);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUARIO);
                        cmdTransaccionFactory.Parameters.Add(pCODOPCION);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pIP);
                        cmdTransaccionFactory.Parameters.Add(pNAVEGADOR);
                        cmdTransaccionFactory.Parameters.Add(pACCION);
                        cmdTransaccionFactory.Parameters.Add(pTABLA);
                        cmdTransaccionFactory.Parameters.Add(pDETALLE);
                        cmdTransaccionFactory.Parameters.Add(p_tipoAuditoria);
                        cmdTransaccionFactory.Parameters.Add(p_observacion);
                        cmdTransaccionFactory.Parameters.Add(p_informacionAnterior);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_AUDITORIA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch // (Exception ex)
                    {
                        // No deberia ser causante de excepciones, si fallo fallo da igual
                        //BOExcepcion.Throw("AuditoriaData", "InsertarLog", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Registra la transaccion realizada por un usuario en una tabla
        /// </summary>
        /// <param name="pEntidad">Conjunto de datos de la transaccion</param>
        /// <param name="pIdUsuario">Identificador del usuario que ejecuta la accion</param>
        /// <param name="pIdPrograma">Identificador del programa que ejecuta la accion</param>
        /// <param name="pIP">Direccion IP desde donde se ejecuto</param>
        /// <param name="pTable">Nombre de la tabla sobre la que se ejecuta la operacion</param>
        /// <param name="pAccion">Accion que realiza (I) Insetar, (M) Modificar, (E) Eliminar, (C) Consultar</param>
        public void InsertarLog(Object pEntidad, Usuario pUsuario, Int64 pIdPrograma, string pTable, string pAccion, DbConnection connection, DbCommand cmd)
        {
            try
            {
                cmdTransaccionFactory = cmd;
                cmdTransaccionFactory.Parameters.Clear();
                DbParameter pCodigoUsuario = cmdTransaccionFactory.CreateParameter();
                pCodigoUsuario.ParameterName = "pcodusuario";
                pCodigoUsuario.Value = pUsuario.codusuario;
                pCodigoUsuario.DbType = DbType.Int16;
                pCodigoUsuario.Size = 8;
                pCodigoUsuario.Direction = ParameterDirection.Input;

                DbParameter pCodigoPrograma = cmdTransaccionFactory.CreateParameter();
                pCodigoPrograma.ParameterName = "pcodprograma";
                pCodigoPrograma.Value = pIdPrograma;
                pCodigoPrograma.DbType = DbType.Int16;
                pCodigoPrograma.Size = 8;
                pCodigoPrograma.Direction = ParameterDirection.Input;

                DbParameter pNomIp = cmdTransaccionFactory.CreateParameter();
                pNomIp.ParameterName = "pnomip";
                pNomIp.Value = pUsuario.IP;
                pNomIp.DbType = DbType.AnsiString;
                pNomIp.Size = 100;
                pNomIp.Direction = ParameterDirection.Input;

                DbParameter pNomTabla = cmdTransaccionFactory.CreateParameter();
                pNomTabla.ParameterName = "pnomtabla";
                pNomTabla.Value = pTable;
                pNomTabla.DbType = DbType.AnsiString;
                pNomTabla.Size = 100;
                pNomTabla.Direction = ParameterDirection.Input;

                DbParameter pNomAccion = cmdTransaccionFactory.CreateParameter();
                pNomAccion.ParameterName = "pnomaccion";
                pNomAccion.Value = pAccion;
                pNomAccion.DbType = DbType.AnsiString;
                pNomAccion.Size = 100;
                pNomAccion.Direction = ParameterDirection.Input;

                cmdTransaccionFactory.Parameters.Add(pCodigoUsuario);
                cmdTransaccionFactory.Parameters.Add(pCodigoPrograma);
                cmdTransaccionFactory.Parameters.Add(pNomIp);
                cmdTransaccionFactory.Parameters.Add(pNomTabla);
                cmdTransaccionFactory.Parameters.Add(pNomAccion);

                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                cmdTransaccionFactory.CommandText = "XPINN.USP_XPINN_SEG_LOGINSERTAR";
                cmdTransaccionFactory.ExecuteNonQuery();
            }
            catch //(Exception ex)
            {
                // Un log no deberia soltar excepcion
                //BOExcepcion.Throw("AuditoryData", "InsertarLog", ex);
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            //Int64 resultado;
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_auditoria) + 1 FROM  AUDITORIA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                            if (resultado.GetValue(0) != DBNull.Value)
                                return Convert.ToInt64(resultado.GetInt64(0));
                            else
                                return 1;
                        else
                            throw new Exception("Error al obtener el siguiente id");
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoryData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene por reflexion los datos de una entidad
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Cadena de texto con todos los datos de la entidad</returns>
        public string ObtenerDatos(Object pEntidad)
        {
            try
            {
                Object obj = new object();
                String str = "=";

                if (pEntidad != null)
                {
                    obj = pEntidad;
                    FieldInfo[] propiedades = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                    foreach (FieldInfo f in propiedades)
                    {
                        String sCampo = f.Name;
                        if (sCampo.Contains(">") && sCampo.IndexOf('>', 0) > 1)
                        {
                            sCampo = sCampo.Substring(1, sCampo.IndexOf('>', 0)-1);
                        }
                        
                        object valorObject = f.GetValue(obj);

                        // Si no soy nulo
                        if (valorObject != null)
                        {
                            string valorString = valorObject.ToString();

                            // Si no estoy vacio
                            if (!string.IsNullOrWhiteSpace(valorString))
                            {
                                // Y si soy una fecha y soy valida
                                if (valorObject is DateTime)
                                {
                                    DateTime? fechaValidar = valorObject as DateTime?;
                                    if (fechaValidar.Value != DateTime.MinValue)
                                    {
                                        str += sCampo + ": " + f.GetValue(obj) + "|";
                                    }
                                }
                                else
                                {
                                    str += sCampo + ": " + f.GetValue(obj) + "|";
                                }
                            }
                        }
                    }
                }

                if (str == null)
                    str = " ";

                return str;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoryData", "ObtenerDatos", ex);
                return null;
            }
        }
    }
}