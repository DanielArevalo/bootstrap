using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Programa
    /// </summary>
    public class PreAnalisisData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PreAnalisisData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Parametro en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Parametro</param>
        /// <returns>Entidad creada</returns> 
        public Parametrizar CrearPrograma(Parametrizar pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pId = cmdTransaccionFactory.CreateParameter();
                        pId.ParameterName = "pIdp";
                        pId.Value = 0;
                        pId.Direction = ParameterDirection.InputOutput;

                        DbParameter pMinimo = cmdTransaccionFactory.CreateParameter();
                        pMinimo.ParameterName = "pMinimo";
                        pMinimo.Value = pEntidad.minimo;

                        DbParameter pMaximo = cmdTransaccionFactory.CreateParameter();
                        pMaximo.ParameterName = "pMaximo";
                        pMaximo.Value = pEntidad.maximo;

                        DbParameter pAprueba = cmdTransaccionFactory.CreateParameter();
                        pAprueba.ParameterName = "pAprueba";
                        pAprueba.Value = pEntidad.aprueba;

                        DbParameter pMuestra = cmdTransaccionFactory.CreateParameter();
                        pMuestra.ParameterName = "pMuestra";
                        pMuestra.Value = pEntidad.muestra;

                        DbParameter pMensaje = cmdTransaccionFactory.CreateParameter();
                        pMensaje.ParameterName = "pMensaje";
                        pMensaje.Value = pEntidad.mensaje;
                      

                        cmdTransaccionFactory.Parameters.Add(pId);
                        cmdTransaccionFactory.Parameters.Add(pMinimo);
                        cmdTransaccionFactory.Parameters.Add(pMaximo);
                        cmdTransaccionFactory.Parameters.Add(pAprueba);
                        cmdTransaccionFactory.Parameters.Add(pMuestra);
                        cmdTransaccionFactory.Parameters.Add(pMensaje);
                   
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PARAMETR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntidad.idp = Convert.ToInt64(pId.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "CrearParametr", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crea una entidad Central en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Central</param>
        /// <returns>Entidad creada</returns> 
        public Parametrizar CrearCentral(Parametrizar pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdc = cmdTransaccionFactory.CreateParameter();
                        pIdc.ParameterName = "pIdc";
                        pIdc.Value = 0;
                        pIdc.Direction = ParameterDirection.InputOutput;

                        DbParameter pCentral = cmdTransaccionFactory.CreateParameter();
                        pCentral.ParameterName = "pCentral";
                        pCentral.Value = pEntidad.central;

                        DbParameter pValor = cmdTransaccionFactory.CreateParameter();
                        pValor.ParameterName = "pValor";
                        pValor.Value = pEntidad.valor;

                        DbParameter pCobra = cmdTransaccionFactory.CreateParameter();
                        pCobra.ParameterName = "pCobra";
                        pCobra.Value = pEntidad.cobra;

                        DbParameter pPorcentaje = cmdTransaccionFactory.CreateParameter();
                        pPorcentaje.ParameterName = "pPorcentaje";
                        pPorcentaje.Value = pEntidad.porcentaje;

                        DbParameter pValoriva = cmdTransaccionFactory.CreateParameter();
                        pValoriva.ParameterName = "pValoriva";
                        pValoriva.Value = pEntidad.valoriva;


                        cmdTransaccionFactory.Parameters.Add(pIdc);
                        cmdTransaccionFactory.Parameters.Add(pCentral);
                        cmdTransaccionFactory.Parameters.Add(pValor);
                        cmdTransaccionFactory.Parameters.Add(pCobra);
                        cmdTransaccionFactory.Parameters.Add(pPorcentaje);
                        cmdTransaccionFactory.Parameters.Add(pValoriva);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_CENTRAL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pEntidad.idc = Convert.ToInt64(pIdc.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentralData", "CrearCentral", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad Parametro en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Paarmetro</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarPrograma(Parametrizar pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pId = cmdTransaccionFactory.CreateParameter();
                        pId.ParameterName = "pIdp";
                        pId.Value = pEntidad.idp;

                        DbParameter pMinimo = cmdTransaccionFactory.CreateParameter();
                        pMinimo.ParameterName = "pMinimo";
                        pMinimo.Value = pEntidad.minimo;

                        DbParameter pMaximo = cmdTransaccionFactory.CreateParameter();
                        pMaximo.ParameterName = "pMaximo";
                        pMaximo.Value = pEntidad.maximo;

                        DbParameter pAprueba = cmdTransaccionFactory.CreateParameter();
                        pAprueba.ParameterName = "pAprueba";
                        pAprueba.Value = pEntidad.aprueba;

                        DbParameter pMuestra = cmdTransaccionFactory.CreateParameter();
                        pMuestra.ParameterName = "pMuestra";
                        pMuestra.Value = pEntidad.muestra;

                        DbParameter pMensaje = cmdTransaccionFactory.CreateParameter();
                        pMensaje.ParameterName = "pMensaje";
                        pMensaje.Value = pEntidad.mensaje;
                        
                        cmdTransaccionFactory.Parameters.Add(pId);
                        cmdTransaccionFactory.Parameters.Add(pMinimo);
                        cmdTransaccionFactory.Parameters.Add(pMaximo);
                        cmdTransaccionFactory.Parameters.Add(pAprueba);
                        cmdTransaccionFactory.Parameters.Add(pMuestra);
                        cmdTransaccionFactory.Parameters.Add(pMensaje);
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PARAMETR_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProgramaData", "ModificarParametr", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Modifica una entidad Central en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Central</param>
        /// <returns>Entidad modificada</returns>
        public Parametrizar ModificarCentral(Parametrizar pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdc = cmdTransaccionFactory.CreateParameter();
                        pIdc.ParameterName = "pIdc";
                        pIdc.Value = pEntidad.idc;

                        DbParameter pCentral = cmdTransaccionFactory.CreateParameter();
                        pCentral.ParameterName = "pCentral";
                        pCentral.Value = pEntidad.central;

                        DbParameter pValor = cmdTransaccionFactory.CreateParameter();
                        pValor.ParameterName = "pValor";
                        pValor.Value = pEntidad.valor;

                        DbParameter pCobra = cmdTransaccionFactory.CreateParameter();
                        pCobra.ParameterName = "pCobra";
                        pCobra.Value = pEntidad.cobra;

                        DbParameter pPorcentaje = cmdTransaccionFactory.CreateParameter();
                        pPorcentaje.ParameterName = "pPorcentaje";
                        pPorcentaje.Value = pEntidad.porcentaje;

                        DbParameter pValoriva = cmdTransaccionFactory.CreateParameter();
                        pValoriva.ParameterName = "pValoriva";
                        pValoriva.Value = pEntidad.valoriva;

                        cmdTransaccionFactory.Parameters.Add(pIdc);
                        cmdTransaccionFactory.Parameters.Add(pCentral);
                        cmdTransaccionFactory.Parameters.Add(pValor);
                        cmdTransaccionFactory.Parameters.Add(pCobra);
                        cmdTransaccionFactory.Parameters.Add(pPorcentaje);
                        cmdTransaccionFactory.Parameters.Add(pValoriva);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_CENTRAL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentralData", "ModificarCentral", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Elimina un Parametro en la base de datos
        /// </summary>
        /// <param name="pId">Identificador del Parametro</param>
        public void EliminarPrograma(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Parametrizar pEntidad = new Parametrizar();

                        if (pUsuario.programaGeneraLog)
                            pEntidad = ConsultarPrograma(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdParametrizacion = cmdTransaccionFactory.CreateParameter();
                        pIdParametrizacion.ParameterName = "pIdp";
                        pIdParametrizacion.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIdParametrizacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PARAMETR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PreAnalisisData", "ElimiPreAnalisis", ex);
                    }
                }
            }
        }



        /// <summary>
        /// Elimina una Central en la base de datos
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        public void EliminarCentral(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Parametrizar pEntidad = new Parametrizar();

                        if (pUsuario.programaGeneraLog)
                            pEntidad = ConsultarCentral(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIdc = cmdTransaccionFactory.CreateParameter();
                        pIdc.ParameterName = "pIdc";
                        pIdc.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIdc);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_CENTRAL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CentralData", "EliminarCentral", ex);
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Parametrización de la base de datos
        /// </summary>
        /// <param name="pId">Identificador del registro</param>
        /// <returns>Programa consultado</returns>
        public Parametrizar ConsultarPrograma(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Parametrizar entidad = new Parametrizar();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM PARAMETRIZACION where IDP=" + pId.ToString();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        connection.Open();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["idp"] != DBNull.Value) entidad.idp = Convert.ToInt64(resultado["idp"]);
                            if (resultado["minimo"] != DBNull.Value) entidad.minimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["maximo"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["aprueba"] != DBNull.Value) entidad.aprueba = Convert.ToString(resultado["aprueba"]);
                            if (resultado["muestra"] != DBNull.Value) entidad.muestra = Convert.ToString(resultado["muestra"]);
                            if (resultado["mensaje"] != DBNull.Value) entidad.mensaje = Convert.ToString(resultado["mensaje"]);
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
                        BOExcepcion.Throw("PreAnalisisData", "ConsultarPreAnalisis", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene un registro de la tabla Central de la base de datos
        /// </summary>
        /// <param name="pIdc">Identificador del registro</param>
        /// <returns>Central consultada</returns>
        public Parametrizar ConsultarCentral(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Parametrizar entidad = new Parametrizar();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CENTRAL where IDC=" + pId.ToString();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        connection.Open();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["idc"] != DBNull.Value) entidad.idc = Convert.ToInt64(resultado["idc"]);
                            if (resultado["central"] != DBNull.Value) entidad.central = Convert.ToString(resultado["central"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor"]);
                            if (resultado["cobra"] != DBNull.Value) entidad.cobra = Convert.ToString(resultado["cobra"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["porcentaje"]);
                            if (resultado["valoriva"] != DBNull.Value) entidad.valoriva = Convert.ToInt64(resultado["valoriva"]);
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
                        BOExcepcion.Throw("PreAnalisisData", "ConsultarCentral", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Programas obtenidos</returns>
        public List<Parametrizar> ListarPrograma(Parametrizar pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Parametrizar> lstPrograma = new List<Parametrizar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM Parametrizacion " ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Parametrizar entidad = new Parametrizar();
                            if (resultado["IDP"] != DBNull.Value) entidad.idp = Convert.ToInt64(resultado["idp"]);// Convert.ToString(resultado["idp"]);
                            if (resultado["MINIMO"] != DBNull.Value) entidad.minimo = Convert.ToInt64(resultado["minimo"]);
                            if (resultado["MAXIMO"] != DBNull.Value) entidad.maximo = Convert.ToInt64(resultado["maximo"]);
                            if (resultado["APRUEBA"] != DBNull.Value) entidad.aprueba = Convert.ToString(resultado["aprueba"]);
                            if (resultado["MUESTRA"] != DBNull.Value) entidad.muestra = Convert.ToString(resultado["muestra"]);
                            if (resultado["MENSAJE"] != DBNull.Value) entidad.mensaje = Convert.ToString(resultado["mensaje"]);
                          
                            lstPrograma.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrizacionData", "ListarParametrizacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene la lista de Centrales dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centrales obtenidas</returns>
        public List<Parametrizar> ListarCentrales(Parametrizar pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Parametrizar> lstPrograma = new List<Parametrizar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        if (pEntidad.central != "")
                        {
                            sql = "SELECT * FROM CENTRAL WHERE CENTRAL = 'Datacredito'";
                        }
                        else
                        {
                            sql = "SELECT * FROM CENTRAL ";
                        }
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Parametrizar entidad = new Parametrizar();
                            if (resultado["idc"] != DBNull.Value) entidad.idc = Convert.ToInt64(resultado["idc"]);      // Convert.ToString(resultado["idp"]);
                            if (resultado["central"] != DBNull.Value) entidad.central = Convert.ToString(resultado["central"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["valor"]);
                            if (resultado["cobra"] != DBNull.Value) entidad.cobra = Convert.ToString(resultado["cobra"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["porcentaje"]);
                            if (resultado["valoriva"] != DBNull.Value) entidad.valoriva = Convert.ToInt64(resultado["valoriva"]);

                            lstPrograma.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PreAnalisisData", "ListarCentrales", ex);
                        return null;
                    }
                }
            }
        }


        public Credito ConsultarPreAnalisis_credito(Credito pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado;
            Credito entidad = new Credito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PREANALISIS_CREDITO " + ObtenerFiltro(pEntidad) + " ORDER BY IDPREANALISIS DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPREANALISIS"] != DBNull.Value) entidad.idpreanalisis = Convert.ToInt64(resultado["IDPREANALISIS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["SALDO_DISPONIBLE"] != DBNull.Value) entidad.saldo_disponible = Convert.ToDecimal(resultado["SALDO_DISPONIBLE"]);
                            if (resultado["CUOTA_CREDITO"] != DBNull.Value) entidad.cuota_credito = Convert.ToDecimal(resultado["CUOTA_CREDITO"]);
                            if (resultado["CUOTA_SERVICIOS"] != DBNull.Value) entidad.cuota_servicios = Convert.ToDecimal(resultado["CUOTA_SERVICIOS"]);
                            if (resultado["PAGO_TERCEROS"] != DBNull.Value) entidad.pago_terceros = Convert.ToDecimal(resultado["PAGO_TERCEROS"]);
                            if (resultado["CUOTA_OTROS"] != DBNull.Value) entidad.cuota_otros = Convert.ToDecimal(resultado["CUOTA_OTROS"]);
                            if (resultado["INGRESOS_ADICIONALES"] != DBNull.Value) entidad.ingresos_adicionales = Convert.ToDecimal(resultado["INGRESOS_ADICIONALES"]);
                            if (resultado["MENOS_SMLMV"] != DBNull.Value) entidad.menos_smlmv = Convert.ToDecimal(resultado["MENOS_SMLMV"]);
                            if (resultado["TOTAL_DISPONIBLE"] != DBNull.Value) entidad.total_disponible = Convert.ToDecimal(resultado["TOTAL_DISPONIBLE"]);
                            if (resultado["APORTES"] != DBNull.Value) entidad.aportes = Convert.ToDecimal(resultado["APORTES"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.creditos = Convert.ToDecimal(resultado["CREDITOS"]);
                            if (resultado["CAPITALIZACION"] != DBNull.Value) entidad.capitalizacion = Convert.ToDecimal(resultado["CAPITALIZACION"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["MONTO_SOLICITADO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO_SOLICITADO"]);
                            if (resultado["PLAZO_SOLICITADO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO_SOLICITADO"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                        }
                        else
                        {
                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PreAnalisisData", "ConsultarPreAnalisis_credito", ex);
                        return null;
                    }
                }
            }
        }


    }
}