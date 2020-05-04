using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipoComprobanteS
    /// </summary>
    public class TipoComprobanteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipoComprobanteS
        /// </summary>
        public TipoComprobanteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipoComprobanteS de la base de datos
        /// </summary>
        /// <param name="pTipoComprobante">Entidad TipoComprobante</param>
        /// <returns>Entidad TipoComprobante creada</returns>
        public TipoComprobante CrearTipoComprobante(TipoComprobante pTipoComprobante, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPOCOMP = cmdTransaccionFactory.CreateParameter();
                        pTIPOCOMP.ParameterName = "p_tipo_comp";
                        pTIPOCOMP.Value = ObtenerSiguienteCodigo(pUsuario);
                        pTIPOCOMP.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pTipoComprobante.descripcion;

                        DbParameter pTIPONORMA = cmdTransaccionFactory.CreateParameter();
                        pTIPONORMA.ParameterName = "p_tipo_norma";
                        if (pTipoComprobante.tipo_norma == null)
                            pTIPONORMA.Value = DBNull.Value;
                        else
                            pTIPONORMA.Value = pTipoComprobante.tipo_norma;
                        pTIPONORMA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pTIPOCOMP);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPONORMA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOCOMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pTipoComprobante.tipo_comprobante = Convert.ToInt64(pTIPOCOMP.Value);
                        return pTipoComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "CrearTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipoComprobanteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoComprobante modificada</returns>
        public TipoComprobante ModificarTipoComprobante(TipoComprobante pTipoComprobante, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPOCOMP = cmdTransaccionFactory.CreateParameter();
                        pTIPOCOMP.ParameterName = "p_tipo_comp";
                        pTIPOCOMP.Value = pTipoComprobante.tipo_comprobante;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pTipoComprobante.descripcion;

                        DbParameter pTIPONORMA = cmdTransaccionFactory.CreateParameter();
                        pTIPONORMA.ParameterName = "p_tipo_norma";
                        if (pTipoComprobante.tipo_norma == null)
                            pTIPONORMA.Value = DBNull.Value;
                        else
                            pTIPONORMA.Value = pTipoComprobante.tipo_norma;
                        pTIPONORMA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pTIPOCOMP);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pTIPONORMA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOCOMP_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pTipoComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ModificarTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipoComprobanteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipoComprobanteS</param>
        public void EliminarTipoComprobante(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoComprobante pTipoComprobante = new TipoComprobante(); 
                        pTipoComprobante = ConsultarTipoComprobante(pId, vUsuario);

                        DbParameter pTIPOCOMP = cmdTransaccionFactory.CreateParameter();
                        pTIPOCOMP.ParameterName = "p_tipo_comp";
                        pTIPOCOMP.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPOCOMP);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_TIPOCOMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "EliminarTipoComprobante", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipoComprobanteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipoComprobanteS</param>
        /// <returns>Entidad TipoComprobante consultado</returns>
        public TipoComprobante ConsultarTipoComprobante(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoComprobante entidad = new TipoComprobante();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM tipo_comp" +
                                     " WHERE tipo_comp = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_NORMA"] != DBNull.Value) entidad.tipo_norma = Convert.ToInt64(resultado["TIPO_NORMA"]);
                            if (entidad.tipo_norma == 0)
                                entidad.nomtipo_norma = "Local";
                            else if (entidad.tipo_norma == 1)
                                entidad.nomtipo_norma = "NIF";
                            else if (entidad.tipo_norma == 2)
                                entidad.nomtipo_norma = "Local/NIF";
                            else
                                entidad.nomtipo_norma = "Local";
                      }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ConsultarTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipoComprobanteS dados unos filtros
        /// </summary>
        /// <param name="pTipoComprobanteS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoComprobante obtenidos</returns>
        public List<TipoComprobante> ListarTipoComprobante(TipoComprobante pTipoComprobante,  string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoComprobante> lstTipoComprobante = new List<TipoComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT Tipo_Comp,Tipo_Comp ||'-'|| Descripcion as descripcion,Tipo_Norma FROM tipo_comp " + ObtenerFiltro(pTipoComprobante);
                        if (pFiltro.Trim() != "")
                        {
                            if (sql.ToLower().Contains("where") == true)
                                sql = sql + " And " + pFiltro;
                            else
                                sql = sql + " Where " + pFiltro;
                        }
                        sql += " ORDER BY Tipo_Comp";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoComprobante entidad = new TipoComprobante();

                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_NORMA"] != DBNull.Value) entidad.tipo_norma = Convert.ToInt64(resultado["TIPO_NORMA"]);
                            if (entidad.tipo_norma == 0)
                                entidad.nomtipo_norma = "Local";
                            else if (entidad.tipo_norma == 1)
                                entidad.nomtipo_norma = "NIF";
                            else if (entidad.tipo_norma == 2)
                                entidad.nomtipo_norma = "Local/NIF";
                            else
                                entidad.nomtipo_norma = "Local";

                            lstTipoComprobante.Add(entidad);
                        }

                        return lstTipoComprobante;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ListarTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla tipo comprobante dados unos filtros
        /// </summary>
        /// <param name="pTipoComp">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos de comprobantes obtenidos</returns>
        public List<TipoComprobante> ListarTipoComp(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoComprobante> lstTipoComp = new List<TipoComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  V_TIPO_COMP " + ObtenerFiltro(pTipoComp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoComprobante entidad = new TipoComprobante();

                            if (resultado["TIPO_COMPROBANTE"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToInt64(resultado["TIPO_COMPROBANTE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_NORMA"] != DBNull.Value) entidad.tipo_norma = Convert.ToInt64(resultado["TIPO_NORMA"]);
                            if (entidad.tipo_norma == 0)
                                entidad.nomtipo_norma = "Local";
                            else if (entidad.tipo_norma == 1)
                                entidad.nomtipo_norma = "NIF";
                            else if (entidad.tipo_norma == 2)
                                entidad.nomtipo_norma = "Local/NIF";
                            else
                                entidad.nomtipo_norma = "Local";
                            lstTipoComp.Add(entidad);
                        }

                        return lstTipoComp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ListarTipoComprobante", ex);
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

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(tipo_comp) + 1 FROM tipo_comp ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }

       
        /// <summary>
        /// Valida el TipoComprobante que ingresa al sistema
        /// </summary>
        /// <param name="pTipoComprobante"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public TipoComprobante ValidarTipoComprobante(Int64 pTipoComprobante, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoComprobante entidad = new TipoComprobante();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT tipo_comp, descripcion " +
                                     " FROM tipo_comp " +
                                     " WHERE tipo_comp = '" + pTipoComprobante.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ConsultarTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public List<TipoComprobante> ListarTipoCompTodos(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoComprobante> lstTipoComp = new List<TipoComprobante>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM TIPO_COMP " + ObtenerFiltro(pTipoComp) + " ORDER BY TIPO_COMP";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoComprobante entidad = new TipoComprobante();

                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comprobante = Convert.ToInt64(resultado["TIPO_COMP"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_NORMA"] != DBNull.Value) entidad.tipo_norma = Convert.ToInt64(resultado["TIPO_NORMA"]);
                            if (entidad.tipo_norma == 0)
                                entidad.nomtipo_norma = "Local";
                            else if (entidad.tipo_norma == 1)
                                entidad.nomtipo_norma = "NIF";
                            else if (entidad.tipo_norma == 2)
                                entidad.nomtipo_norma = "Local/NIF";
                            else
                                entidad.nomtipo_norma = "Local";
                            lstTipoComp.Add(entidad);
                        }

                        return lstTipoComp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoComprobanteData", "ListarTipoComprobante", ex);
                        return null;
                    }
                }
            }
        }


    }
}