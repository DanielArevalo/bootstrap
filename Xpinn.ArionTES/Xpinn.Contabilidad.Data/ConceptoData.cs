using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ConceptoS
    /// </summary>
    public class ConceptoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoS
        /// </summary>
        public ConceptoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ConceptoS de la base de datos
        /// </summary>
        /// <param name="pConcepto">Entidad Concepto</param>
        /// <returns>Entidad Concepto creada</returns>
        public Concepto CrearConcepto(Concepto pConcepto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = ObtenerSiguienteCodigo(vUsuario);
                        pCONCEPTO.Direction = ParameterDirection.InputOutput;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pConcepto.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CONCEPTO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConcepto.concepto = Convert.ToInt64(pCONCEPTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConcepto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "CrearConcepto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ConceptoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Concepto modificada</returns>
        public Concepto ModificarConcepto(Concepto pConcepto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = pConcepto.concepto;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_descripcion";
                        pDESCRIPCION.Value = pConcepto.descripcion;

                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CONCEPTO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConcepto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "ModificarConcepto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ConceptoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ConceptoS</param>
        public void EliminarConcepto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Concepto pConcepto = new Concepto();

                        DbParameter pCONCEPTO = cmdTransaccionFactory.CreateParameter();
                        pCONCEPTO.ParameterName = "p_concepto";
                        pCONCEPTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCONCEPTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CONCEPTO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ConceptoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ConceptoS</param>
        /// <returns>Entidad Concepto consultado</returns>
        public Concepto ConsultarConcepto(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Concepto entidad = new Concepto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT concepto, descripcion FROm concepto" +
                                     " WHERE concepto = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("ConceptoData", "ConsultarConcepto", ex);
                        return null;
                    }
                }
            }
        }

 

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla CONCEPTO dados unos filtros
        /// </summary>
        /// <param name="pCONCEPTO">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de conceptos obtenidos</returns>
        public List<Concepto> ListarConcepto(Concepto pCONCEPTO, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Concepto> lstCONCEPTO = new List<Concepto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  concepto " + ObtenerFiltro(pCONCEPTO) + " ORDER BY DESCRIPCION";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Concepto entidad = new Concepto();

                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstCONCEPTO.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCONCEPTO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "ListarConcepto", ex);
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
                        string sql = "SELECT MAX(concepto) + 1 FROM concepto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        string sDato = cmdTransaccionFactory.ExecuteScalar().ToString();
                        if (sDato != null && sDato != "")
                            resultado = Convert.ToInt64(sDato);
                        else
                            resultado = 1;
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Valida el Concepto que ingresa al sistema
        /// </summary>
        /// <param name="pConcepto"></param>
        /// <param name="pClave"></param>
        /// <returns></returns>
        public Concepto ValidarConcepto(Int64 pConcepto, Usuario pUsuario)
        {
            DbDataReader resultado;
            Concepto entidad = new Concepto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT concepto, descripcion " +
                                     " FROM concepto " +
                                     " WHERE concepto = '" + pConcepto.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("ConceptoData", "ConsultarConcepto", ex);
                        return null;
                    }
                }
            }
        }

    }
}