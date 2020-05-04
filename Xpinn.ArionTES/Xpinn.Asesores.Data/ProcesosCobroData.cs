using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla procesoscobro
    /// </summary>
    public class ProcesosCobroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla procesoscobro
        /// </summary>
        public ProcesosCobroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pProcesosCobro">Entidad ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro creada</returns>
        public ProcesosCobro CrearProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPROCESOCOBRO = cmdTransaccionFactory.CreateParameter();
                        pCODPROCESOCOBRO.ParameterName = "p_codprocesocobro";
                        pCODPROCESOCOBRO.Value = pProcesosCobro.codprocesocobro;
                        pCODPROCESOCOBRO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCODPROCESOPRECEDE = cmdTransaccionFactory.CreateParameter();
                        pCODPROCESOPRECEDE.ParameterName = "p_codprocesoprecede";
                        pCODPROCESOPRECEDE.Value = pProcesosCobro.codprocesoprecede;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pProcesosCobro.descripcion;

                        DbParameter pRANGOINICIAL = cmdTransaccionFactory.CreateParameter();
                        pRANGOINICIAL.ParameterName = "p_rangoinicial";
                        pRANGOINICIAL.Value = pProcesosCobro.rangoinicial;

                        DbParameter pRANGOFINAL = cmdTransaccionFactory.CreateParameter();
                        pRANGOFINAL.ParameterName = "p_rangofinal";
                        pRANGOFINAL.Value = pProcesosCobro.rangofinal;


                        cmdTransaccionFactory.Parameters.Add(pCODPROCESOCOBRO);
                        cmdTransaccionFactory.Parameters.Add(pCODPROCESOPRECEDE);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pRANGOINICIAL);
                        cmdTransaccionFactory.Parameters.Add(pRANGOFINAL);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_PROCESOSCOBRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesosCobro, "procesoscobro",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pProcesosCobro.codprocesocobro = Convert.ToInt64(pCODPROCESOCOBRO.Value);
                        return pProcesosCobro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "CrearProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ProcesosCobro modificada</returns>
        public ProcesosCobro ModificarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPROCESOCOBRO = cmdTransaccionFactory.CreateParameter();
                        pCODPROCESOCOBRO.ParameterName = "p_CODPROCESOCOBRO";
                        pCODPROCESOCOBRO.Value = pProcesosCobro.codprocesocobro;

                        DbParameter pCODPROCESOPRECEDE = cmdTransaccionFactory.CreateParameter();
                        pCODPROCESOPRECEDE.ParameterName = "p_CODPROCESOPRECEDE";
                        pCODPROCESOPRECEDE.Value = pProcesosCobro.codprocesoprecede;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pProcesosCobro.descripcion;

                        DbParameter pRANGOINICIAL = cmdTransaccionFactory.CreateParameter();
                        pRANGOINICIAL.ParameterName = "p_RANGOINICIAL";
                        pRANGOINICIAL.Value = pProcesosCobro.rangoinicial;

                        DbParameter pRANGOFINAL = cmdTransaccionFactory.CreateParameter();
                        pRANGOFINAL.ParameterName = "p_RANGOFINAL";
                        pRANGOFINAL.Value = pProcesosCobro.rangofinal;

                        cmdTransaccionFactory.Parameters.Add(pCODPROCESOCOBRO);
                        cmdTransaccionFactory.Parameters.Add(pCODPROCESOPRECEDE);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pRANGOINICIAL);
                        cmdTransaccionFactory.Parameters.Add(pRANGOFINAL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_PROCESOSCOBRO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesosCobro, "procesoscobro",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pProcesosCobro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ModificarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de procesoscobro</param>
        public void EliminarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ProcesosCobro pProcesosCobro = new ProcesosCobro();

                        //if (pUsuario.programaGeneraLog)
                        //    pProcesosCobro = ConsultarProcesosCobro(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODPROCESOCOBRO = cmdTransaccionFactory.CreateParameter();
                        pCODPROCESOCOBRO.ParameterName = "p_codprocesocobro";
                        pCODPROCESOCOBRO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODPROCESOCOBRO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_PROCESOSCOBRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pProcesosCobro, "procesoscobro", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "EliminarProcesosCobro", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla procesoscobro</param>
        /// <returns>Entidad ProcesosCobro consultado</returns>
        public ProcesosCobro ConsultarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            ProcesosCobro entidad = new ProcesosCobro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PROCESOSCOBRO WHERE codprocesocobro = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODPROCESOCOBRO"] != DBNull.Value) entidad.codprocesocobro = Convert.ToInt64(resultado["CODPROCESOCOBRO"]);
                            if (resultado["CODPROCESOPRECEDE"] != DBNull.Value) entidad.codprocesoprecede = Convert.ToInt64(resultado["CODPROCESOPRECEDE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RANGOINICIAL"] != DBNull.Value) entidad.rangoinicial = Convert.ToInt64(resultado["RANGOINICIAL"]);
                            if (resultado["RANGOFINAL"] != DBNull.Value) entidad.rangofinal = Convert.ToInt64(resultado["RANGOFINAL"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ConsultarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla procesoscobro</param>
        /// <returns>Entidad ProcesosCobro consultado</returns>
        public ProcesosCobro ConsultarProcesosCobroAbogados( Usuario pUsuario)
        {
            DbDataReader resultado;
            ProcesosCobro entidad = new ProcesosCobro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PROCESOSCOBRO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODPROCESOCOBRO"] != DBNull.Value) entidad.codprocesocobro = Convert.ToInt64(resultado["CODPROCESOCOBRO"]);
                            if (resultado["CODPROCESOPRECEDE"] != DBNull.Value) entidad.codprocesoprecede = Convert.ToInt64(resultado["CODPROCESOPRECEDE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RANGOINICIAL"] != DBNull.Value) entidad.rangoinicial = Convert.ToInt64(resultado["RANGOINICIAL"]);
                            if (resultado["RANGOFINAL"] != DBNull.Value) entidad.rangofinal = Convert.ToInt64(resultado["RANGOFINAL"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ConsultarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }




        /// <summary>
        /// Obtiene un registro en la tabla procesoscobro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla procesoscobro</param>
        /// <returns>Entidad ProcesosCobro consultado</returns>
        public ProcesosCobro ConsultarDatosProceso(Int64 numero_radicacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            ProcesosCobro entidad = new ProcesosCobro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.codprocesocobro, p.descripcion as descripcionproceso, u.codusuario, u.nombre, m.cod_motivo_cambio, m.descripcion as descripcionmotivo, c.abogado_encargado, c.numero_juzgado, c.observaciones, ciu.codciudad, c.ciudad 
                                        From cobros_credito c Left Join ciudades_juzgados ciu On ciu.codciudad = c.ciudad_juzgado Left Join usuarios u On c.encargado = u.codusuario Left Join motivos_cambios_procesos m On c.cod_motivo_cambio = m.cod_motivo_cambio, procesoscobro p 
                                        Where c.estado_proceso = p.codprocesocobro and c.numero_radicacion = " + numero_radicacion;
                                             
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODPROCESOCOBRO"] != DBNull.Value) entidad.codprocesocobro = Convert.ToInt64(resultado["CODPROCESOCOBRO"]);
                            if (resultado["DESCRIPCIONPROCESO"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCIONPROCESO"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreusuario = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_MOTIVO_CAMBIO"] != DBNull.Value) entidad.codmotivo = Convert.ToInt64(resultado["COD_MOTIVO_CAMBIO"]);
                            if (resultado["DESCRIPCIONMOTIVO"] != DBNull.Value) entidad.nombremotivo = Convert.ToString(resultado["DESCRIPCIONMOTIVO"]);
                            if (resultado["ABOGADO_ENCARGADO"] != DBNull.Value) entidad.codabogado = Convert.ToInt64(resultado["ABOGADO_ENCARGADO"]);
                            if (resultado["NUMERO_JUZGADO"] != DBNull.Value) entidad.numero_juzgado = Convert.ToString(resultado["NUMERO_JUZGADO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones= Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["CODCIUDAD"] != DBNull.Value) entidad.codciudadjuzgado = Convert.ToInt64(resultado["CODCIUDAD"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["CIUDAD"]);
                             
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ConsultarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla procesoscobro dados unos filtros
        /// </summary>
        /// <param name="pprocesoscobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProcesosCobro> lstProcesosCobro = new List<ProcesosCobro>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  PROCESOSCOBRO " + ObtenerFiltro(pProcesosCobro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesosCobro entidad = new ProcesosCobro();

                            if (resultado["CODPROCESOCOBRO"] != DBNull.Value) entidad.codprocesocobro = Convert.ToInt64(resultado["CODPROCESOCOBRO"]);
                            if (resultado["CODPROCESOPRECEDE"] != DBNull.Value) entidad.codprocesoprecede = Convert.ToInt64(resultado["CODPROCESOPRECEDE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RANGOINICIAL"] != DBNull.Value) entidad.rangoinicial = Convert.ToInt64(resultado["RANGOINICIAL"]);
                            if (resultado["RANGOFINAL"] != DBNull.Value) entidad.rangofinal = Convert.ToInt64(resultado["RANGOFINAL"]);

                            lstProcesosCobro.Add(entidad);
                        }

                        return lstProcesosCobro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ListarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla procesoscobro dados unos filtros
        /// </summary>
        /// <param name="pprocesoscobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobroSiguientes(ProcesosCobro pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProcesosCobro> lstProcesosCobro = new List<ProcesosCobro>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from procesoscobro where codprocesocobro in(select codprocesoprecede from procesoscobro where codprocesocobro<=" + pEntidad.codprocesocobro + ") union all select * from procesoscobro where codprocesoprecede=" + pEntidad.codprocesocobro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesosCobro entidad = new ProcesosCobro();

                            if (resultado["CODPROCESOCOBRO"] != DBNull.Value) entidad.codprocesocobro = Convert.ToInt64(resultado["CODPROCESOCOBRO"]);
                            if (resultado["CODPROCESOPRECEDE"] != DBNull.Value) entidad.codprocesoprecede = Convert.ToInt64(resultado["CODPROCESOPRECEDE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RANGOINICIAL"] != DBNull.Value) entidad.rangoinicial = Convert.ToInt64(resultado["RANGOINICIAL"]);
                            if (resultado["RANGOFINAL"] != DBNull.Value) entidad.rangofinal = Convert.ToInt64(resultado["RANGOFINAL"]);

                            lstProcesosCobro.Add(entidad);
                        }

                        return lstProcesosCobro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesosCobroData", "ListarProcesosCobro", ex);
                        return null;
                    }
                }
            }
        }
    }
}