using System;
using System.Configuration;
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
    /// Objeto de acceso a datos para la tabla USUARIOS
    /// </summary>
    public class TipoTasaHistData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TIPOTASAHIST
        /// </summary>
        public TipoTasaHistData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TIPOTASAHIST de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad tipo tasa HIST</param>
        /// <returns>Entidad TIPOTASAHIST creada</returns>
        public TipoTasaHist CrearTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipo_historico = cmdTransaccionFactory.CreateParameter();
                        p_tipo_historico.ParameterName = "p_tipo_historico";
                        p_tipo_historico.Value = 0;
                        p_tipo_historico.Direction = ParameterDirection.InputOutput;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTipoTasaHist.descripcion;

                        DbParameter p_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tasa.ParameterName = "p_tipo_tasa";
                        p_tipo_tasa.Value = pTipoTasaHist.tipo_tasa;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_historico);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASAHIST_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasaHist, "TIPOTASAHIST", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTipoTasaHist.tipo_historico = Convert.ToInt64(p_tipo_historico.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoTasaHist;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasahISTData", "CrearTipoTasaHist", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TIPOTASAHIST de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad tipotasahist modificada</returns>
        public TipoTasaHist ModificarTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_tipo_historico = cmdTransaccionFactory.CreateParameter();
                        p_tipo_historico.ParameterName = "p_tipo_historico";
                        p_tipo_historico.Value = 0;
                        p_tipo_historico.Direction = ParameterDirection.InputOutput;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTipoTasaHist.descripcion;

                        DbParameter p_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tasa.ParameterName = "p_tipo_tasa";
                        p_tipo_tasa.Value = pTipoTasaHist.tipo_tasa;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_historico);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASAHIST_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasaHist, "TIPOTASAHIST", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoTasaHist;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaHistData", "ModificarTipoTasaHist", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TIPOTASAHIST de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TIPOTASAHIST</param>
        public void EliminarTipoTasaHist(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoTasaHist pTipoTasaHist = new TipoTasaHist();

                        DbParameter p_tipo_historico = cmdTransaccionFactory.CreateParameter();
                        p_tipo_historico.ParameterName = "p_tipo_historico";
                        p_tipo_historico.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_tipo_historico);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASAHIST_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasaHist, "TIPOTASAHIST", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaHistData", "EliminarTipoTasaHist", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TIPOTASAHIST de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TIPOTASAHIST</param>
        /// <returns>Entidad TIPOTASAHIST consultado</returns>
        public TipoTasaHist ConsultarTipoTasaHist(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoTasaHist entidad = new TipoTasaHist();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT tipotasahist.*" +
                                     " FROM tipotasahist " +
                                     " WHERE tipo_historico = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);
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
                        BOExcepcion.Throw("TipoTasaHistData", "ConsultarTipoTasaHist", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPOTASAHIST dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos de tasa histórico obtenidos</returns>
        public List<TipoTasaHist> ListarTipoTasaHist(TipoTasaHist pTipoTasaHist, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOTASAHIST " + ObtenerFiltro(pTipoTasaHist) + "order by descripcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoTasaHist entidad = new TipoTasaHist();

                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt64(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt64(resultado["TIPO_TASA"]);

                            lstTipoTasaHist.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoTasaHist;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaHistData", "ListarTipoTasaHist", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;
            TipoTasaHist entidad = new TipoTasaHist();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(tipo_historico) + 1 FROM  TIPOTASAHIST ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaHistData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


    }

}