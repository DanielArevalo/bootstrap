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
    public class TipoCupoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TIPO_TASA
        /// </summary>
        public TipoCupoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TIPO_TASA de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad tipo tasa</param>
        /// <returns>Entidad tipo_tasa creada</returns>
        public TipoCupo CrearTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_cupo = cmdTransaccionFactory.CreateParameter();
                        ptipo_cupo.ParameterName = "p_tipo_cupo";
                        ptipo_cupo.Value = pTipoCupo.tipo_cupo;
                        ptipo_cupo.Direction = ParameterDirection.Output;
                        ptipo_cupo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cupo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoCupo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter presta_creditos = cmdTransaccionFactory.CreateParameter();
                        presta_creditos.ParameterName = "p_resta_creditos";
                        presta_creditos.Value = pTipoCupo.resta_creditos;
                        presta_creditos.Direction = ParameterDirection.Input;
                        presta_creditos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(presta_creditos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOCUPO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTipoCupo.tipo_cupo = Convert.ToInt64(ptipo_cupo.Value);

                        return pTipoCupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCupoData", "CrearTipoCupo", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Modifica un registro en la tabla TIPO_TASA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoCupo modificada</returns>
        public TipoCupo ModificarTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_cupo = cmdTransaccionFactory.CreateParameter();
                        ptipo_cupo.ParameterName = "p_tipo_cupo";
                        ptipo_cupo.Value = pTipoCupo.tipo_cupo;
                        ptipo_cupo.Direction = ParameterDirection.Input;
                        ptipo_cupo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cupo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoCupo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter presta_creditos = cmdTransaccionFactory.CreateParameter();
                        presta_creditos.ParameterName = "p_resta_creditos";
                        presta_creditos.Value = pTipoCupo.resta_creditos;
                        presta_creditos.Direction = ParameterDirection.Input;
                        presta_creditos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(presta_creditos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOCUPO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoCupo, "TipoCupo", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoCupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCupoData", "ModificarTipoCupo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoCupo de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoCupo</param>
        public void EliminarTipoCupo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoCupo pTipoCupo = new TipoCupo();
                        pTipoCupo = ConsultarTipoCupo(pId, vUsuario);

                        DbParameter ptipo_cupo = cmdTransaccionFactory.CreateParameter();
                        ptipo_cupo.ParameterName = "p_tipo_cupo";
                        ptipo_cupo.Value = pTipoCupo.tipo_cupo;
                        ptipo_cupo.Direction = ParameterDirection.Input;
                        ptipo_cupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cupo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_TIPOCUPO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCupoData", "EliminarTipoCupo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoCupo de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoCupo</param>
        /// <returns>Entidad Usuario consultado</returns>
        public TipoCupo ConsultarTipoCupo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoCupo entidad = new TipoCupo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPOCUPO WHERE TIPO_CUPO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RESTA_CREDITOS"] != DBNull.Value) entidad.resta_creditos = Convert.ToInt32(resultado["RESTA_CREDITOS"]);
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
                        BOExcepcion.Throw("TipoCupoData", "ConsultarTipoCupo", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPO TASA dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos de tasa obtenidos</returns>
        public List<TipoCupo> ListarTipoCupo(TipoCupo pTipoCupo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipoCupo> lstTipoCupo = new List<TipoCupo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPOCUPO " + ObtenerFiltro(pTipoCupo) + " ORDER BY TIPO_CUPO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoCupo entidad = new TipoCupo();
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RESTA_CREDITOS"] != DBNull.Value) entidad.resta_creditos = Convert.ToInt32(resultado["RESTA_CREDITOS"]);
                            lstTipoCupo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoCupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCupoData", "ListarTipoCupo", ex);
                        return null;
                    }
                }
            }
        }

        public TipoCupo ConsultarTipoCupo(TipoCupo pTipoCupo, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoCupo entidad = new TipoCupo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPOCUPO " + ObtenerFiltro(pTipoCupo);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["RESTA_CREDITOS"] != DBNull.Value) entidad.resta_creditos = Convert.ToInt32(resultado["RESTA_CREDITOS"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public List<DetTipoCupo> ListarDetTipoCupo(int pTipoCupo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetTipoCupo> lstTipoCupo = new List<DetTipoCupo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DETTIPOCUPO WHERE TIPO_CUPO = " + pTipoCupo + " ORDER BY IDDETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetTipoCupo entidad = new DetTipoCupo();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt32(resultado["IDVARIABLE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            lstTipoCupo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoCupo;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        public Int64 CrearDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario vUsuario)
        {
            Int64 _iddetalle = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pDetTipoCupo.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter ptipo_cupo = cmdTransaccionFactory.CreateParameter();
                        ptipo_cupo.ParameterName = "p_tipo_cupo";
                        if (pDetTipoCupo.tipo_cupo == 0)
                            ptipo_cupo.Value = DBNull.Value;
                        else
                            ptipo_cupo.Value = pDetTipoCupo.tipo_cupo;
                        ptipo_cupo.Direction = ParameterDirection.Input;
                        ptipo_cupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cupo);

                        DbParameter pidvariable = cmdTransaccionFactory.CreateParameter();
                        pidvariable.ParameterName = "p_idvariable";
                        if (pDetTipoCupo.idvariable == 0)
                            pidvariable.Value = DBNull.Value;
                        else
                            pidvariable.Value = pDetTipoCupo.idvariable;
                        pidvariable.Direction = ParameterDirection.Input;
                        pidvariable.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidvariable);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pDetTipoCupo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pDetTipoCupo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DETTIPOCUP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (piddetalle.Value != null)
                            _iddetalle = Convert.ToInt64(piddetalle.Value);
                        return _iddetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoCupoData", "CrearDetTipoCupo", ex);
                        return _iddetalle;
                    }
                }
            }
        }

        public DetTipoCupo ModificarDetTipoCupo(DetTipoCupo pDetTipoCupo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pDetTipoCupo.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter ptipo_cupo = cmdTransaccionFactory.CreateParameter();
                        ptipo_cupo.ParameterName = "p_tipo_cupo";
                        if (pDetTipoCupo.tipo_cupo == 0)
                            ptipo_cupo.Value = DBNull.Value;
                        else
                            ptipo_cupo.Value = pDetTipoCupo.tipo_cupo;
                        ptipo_cupo.Direction = ParameterDirection.Input;
                        ptipo_cupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cupo);

                        DbParameter pidvariable = cmdTransaccionFactory.CreateParameter();
                        pidvariable.ParameterName = "p_idvariable";
                        if (pDetTipoCupo.idvariable == 0)
                            pidvariable.Value = DBNull.Value;
                        else
                            pidvariable.Value = pDetTipoCupo.idvariable;
                        pidvariable.Direction = ParameterDirection.Input;
                        pidvariable.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidvariable);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pDetTipoCupo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pDetTipoCupo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DETTIPOCUP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDetTipoCupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetTipoCupoData", "ModificarDetTipoCupo", ex);
                        return null;
                    }
                }
            }
        }

        public DetTipoCupo ConsultarDetTipoCupo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            DetTipoCupo entidad = new DetTipoCupo();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DETTIPOCUPO WHERE IDDETALLE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["TIPO_CUPO"] != DBNull.Value) entidad.tipo_cupo = Convert.ToInt32(resultado["TIPO_CUPO"]);
                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt32(resultado["IDVARIABLE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
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
                        BOExcepcion.Throw("DetTipoCupoData", "ConsultarDetTipoCupo", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDetTipoCupo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DetTipoCupo pDetTipoCupo = new DetTipoCupo();
                        pDetTipoCupo = ConsultarDetTipoCupo(pId, vUsuario);

                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pDetTipoCupo.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_DETTIPOCUP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetTipoCupoData", "EliminarDetTipoCupo", ex);
                    }
                }
            }
        }




    }

}