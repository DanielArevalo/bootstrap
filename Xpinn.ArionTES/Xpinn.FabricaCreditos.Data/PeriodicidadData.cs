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
    public class PeriodicidadData : GlobalData 
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public PeriodicidadData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Crea un registro en la tabla Periodicidad de la base de datos
        /// </summary>
        /// <param name="pTipoComprobante">Entidad Periodicidad</param>
        /// <returns>Entidad Periodicidad creada</returns>
        public Periodicidad CrearPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_cod_periodicidad";
                        pCODPERIODICIDAD.Value = pPeriodicidad.Codigo;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pPeriodicidad.Descripcion;

                        DbParameter pNUMERO_DIAS = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_DIAS.ParameterName = "p_numero_dias";
                        pNUMERO_DIAS.Value = pPeriodicidad.numero_dias;

                        DbParameter pNUMERO_MESES = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_MESES.ParameterName = "p_numero_meses";
                        pNUMERO_MESES.Value = pPeriodicidad.numero_meses;

                        DbParameter pPERIODOS_ANUALES = cmdTransaccionFactory.CreateParameter();
                        pPERIODOS_ANUALES.ParameterName = "p_periodos_anuales";
                        pPERIODOS_ANUALES.Value = pPeriodicidad.periodos_anuales;

                        DbParameter pTIPO_CALENDARIO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CALENDARIO.ParameterName = "p_tipo_calendario";
                        pTIPO_CALENDARIO.Value = pPeriodicidad.tipo_calendario;

                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_DIAS);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_MESES);
                        cmdTransaccionFactory.Parameters.Add(pPERIODOS_ANUALES);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CALENDARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERIODIC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPeriodicidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadData", "CrearPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Periodicidad de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Periodicidad modificada</returns>
        public Periodicidad ModificarPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_cod_periodicidad";
                        pCODPERIODICIDAD.Value = pPeriodicidad.Codigo;                        

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pPeriodicidad.Descripcion;

                        DbParameter pNUMERO_DIAS = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_DIAS.ParameterName = "p_numero_dias";
                        pNUMERO_DIAS.Value = pPeriodicidad.numero_dias;

                        DbParameter pNUMERO_MESES = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_MESES.ParameterName = "p_numero_meses";
                        pNUMERO_MESES.Value = pPeriodicidad.numero_meses;

                        DbParameter pPERIODOS_ANUALES = cmdTransaccionFactory.CreateParameter();
                        pPERIODOS_ANUALES.ParameterName = "p_periodos_anuales";
                        pPERIODOS_ANUALES.Value = pPeriodicidad.periodos_anuales;

                        DbParameter pTIPO_CALENDARIO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CALENDARIO.ParameterName = "p_tipo_calendario";
                        pTIPO_CALENDARIO.Value = pPeriodicidad.tipo_calendario;

                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_DIAS);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_MESES);
                        cmdTransaccionFactory.Parameters.Add(pPERIODOS_ANUALES);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CALENDARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERIODIC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPeriodicidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadData", "ModificarPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Periodicidad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Periodicidad</param>
        public void EliminarPeriodicidad(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Periodicidad pPeriodicidad = new Periodicidad();

                        DbParameter pCODPERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODPERIODICIDAD.ParameterName = "p_cod_periodicidad";
                        pCODPERIODICIDAD.Value = pPeriodicidad.Codigo;

                        cmdTransaccionFactory.Parameters.Add(pCODPERIODICIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_PERIODIC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadData", "EliminarPeriodicidad", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Periodicidad de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Periodicidad</param>
        /// <returns>Entidad Periodicidad consultado</returns>
        public Periodicidad ConsultarPeriodicidad(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Periodicidad entidad = new Periodicidad();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM Periodicidad" +
                                     " WHERE cod_periodicidad = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.Codigo = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) entidad.numero_dias = Convert.ToInt32(resultado["NUMERO_DIAS"]);
                            if (resultado["NUMERO_MESES"] != DBNull.Value) entidad.numero_meses = Convert.ToInt32(resultado["NUMERO_MESES"]);
                            if (resultado["PERIODOS_ANUALES"] != DBNull.Value) entidad.periodos_anuales = Convert.ToInt32(resultado["PERIODOS_ANUALES"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
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
                        BOExcepcion.Throw("PeriodicidadData", "ConsultarPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }


        public List<CreditoSolicitado> ListarTipoTasa(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreditoSolicitado> lstAnexos = new List<CreditoSolicitado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * from tipotasa ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreditoSolicitado pEntidad = new CreditoSolicitado();
                            //Asociar todos los valores a la entidad
                            if (resultado["nombre"] != DBNull.Value) pEntidad.tipotasa = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_tipo_tasa"] != DBNull.Value) pEntidad.cod_tipotasa = Convert.ToString(resultado["cod_tipo_tasa"]);
                            lstAnexos.Add(pEntidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadData", "ListarPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene la lista de anexos
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Aprobadores obtenidos</returns>
        public List<Periodicidad> ListarPeriodicidad(Periodicidad pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Periodicidad> lstAnexos = new List<Periodicidad>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from periodicidad order by cod_periodicidad";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            pEntidad = new Periodicidad();
                            //Asociar todos los valores a la entidad
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) pEntidad.Codigo = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) pEntidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) pEntidad.numero_dias = Convert.ToInt32(resultado["NUMERO_DIAS"]);
                            if (resultado["NUMERO_MESES"] != DBNull.Value) pEntidad.numero_meses = Convert.ToInt32(resultado["NUMERO_MESES"]);
                            if (resultado["PERIODOS_ANUALES"] != DBNull.Value) pEntidad.periodos_anuales = Convert.ToInt32(resultado["PERIODOS_ANUALES"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value)
                            {   
                                pEntidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                                if (pEntidad.tipo_calendario == 1)
                                    pEntidad.calendario = "Comercial";
                                if (pEntidad.tipo_calendario == 2)
                                    pEntidad.calendario = "Calendario";
                            }
                            lstAnexos.Add(pEntidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnexos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PeriodicidadData", "ListarPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }
    }
}
