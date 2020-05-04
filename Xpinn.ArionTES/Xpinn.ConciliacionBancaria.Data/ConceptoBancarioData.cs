using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Entities;

namespace Xpinn.ConciliacionBancaria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ConceptoBancarioS
    /// </summary>
    public class ConceptoBancarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ConceptoBancarioS
        /// </summary>
        public ConceptoBancarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ConceptoBancarioS de la base de datos
        /// </summary>
        /// <param name="pConceptoBancario">Entidad ConceptoBancario</param>
        /// <returns>Entidad ConceptoBancario creada</returns>
        public ConceptoBancario CrearConceptoBancario(ConceptoBancario pConceptoBancario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pConceptoBancario.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pConceptoBancario.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoBancario.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_concepto = cmdTransaccionFactory.CreateParameter();
                        ptipo_concepto.ParameterName = "p_tipo_concepto";
                        ptipo_concepto.Value = pConceptoBancario.tipo_concepto;
                        ptipo_concepto.Direction = ParameterDirection.Input;
                        ptipo_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_concepto);


                        DbParameter PIDCONCEPTOBANCARIO = cmdTransaccionFactory.CreateParameter();
                        PIDCONCEPTOBANCARIO.ParameterName = "P_IDCONCEPTOBANCARIO";
                        if (pConceptoBancario.id_conceptobancario == null || pConceptoBancario.id_conceptobancario == 0)
                            PIDCONCEPTOBANCARIO.Value = DBNull.Value;
                        else
                            PIDCONCEPTOBANCARIO.Value = pConceptoBancario.id_conceptobancario;
                        PIDCONCEPTOBANCARIO.Direction = ParameterDirection.Input;
                        PIDCONCEPTOBANCARIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PIDCONCEPTOBANCARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPBAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoBancarioData", "CrearConceptoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ConceptoBancarioS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ConceptoBancario modificada</returns>
        public ConceptoBancario ModificarConceptoBancario(ConceptoBancario pConceptoBancario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pConceptoBancario.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pConceptoBancario.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoBancario.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_concepto = cmdTransaccionFactory.CreateParameter();
                        ptipo_concepto.ParameterName = "p_tipo_concepto";
                        ptipo_concepto.Value = pConceptoBancario.tipo_concepto;
                        ptipo_concepto.Direction = ParameterDirection.Input;
                        ptipo_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_concepto);

                        DbParameter PIDCONCEPTOBANCARIO = cmdTransaccionFactory.CreateParameter();
                        PIDCONCEPTOBANCARIO.ParameterName = "P_IDCONCEPTOBANCARIO";
                        if (pConceptoBancario.id_conceptobancario == null || pConceptoBancario.id_conceptobancario == 0)
                            PIDCONCEPTOBANCARIO.Value = DBNull.Value;
                        else
                            PIDCONCEPTOBANCARIO.Value = pConceptoBancario.id_conceptobancario;
                        PIDCONCEPTOBANCARIO.Direction = ParameterDirection.Input;
                        PIDCONCEPTOBANCARIO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PIDCONCEPTOBANCARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPBAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pConceptoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoBancarioData", "ModificarConceptoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ConceptoBancarioS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ConceptoBancarioS</param>
        public void EliminarConceptoBancario(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ConceptoBancario pConceptoBancario = new ConceptoBancario();
                        pConceptoBancario = ConsultarConceptoBancario(pId, vUsuario);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_idconceptobancario";
                        pcod_concepto.Value = pConceptoBancario.id_conceptobancario;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPBAN_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoBancarioData", "EliminarConceptoBancario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ConceptoBancarioS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ConceptoBancarioS</param>
        /// <returns>Entidad ConceptoBancario consultado</returns>
        public ConceptoBancario ConsultarConceptoBancario(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConceptoBancario entidad = new ConceptoBancario();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Concepto_Bancario WHERE idconceptobancario = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToString(resultado["TIPO_CONCEPTO"]);
                            if (resultado["IDCONCEPTOBANCARIO"] != DBNull.Value) entidad.id_conceptobancario = Convert.ToInt32(resultado["IDCONCEPTOBANCARIO"]);
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
                        BOExcepcion.Throw("ConceptoBancarioData", "ConsultarConceptoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ConceptoBancarioS dados unos filtros
        /// </summary>
        /// <param name="pConceptoBancarioS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ConceptoBancario obtenidos</returns>
        public List<ConceptoBancario> ListarConceptoBancario(String filtro,ConceptoBancario pConceptoBancario, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConceptoBancario> lstConceptoBancario = new List<ConceptoBancario>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Concepto_Bancario where 1=1" + ObtenerFiltro(pConceptoBancario) +filtro+ " ORDER BY COD_CONCEPTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConceptoBancario entidad = new ConceptoBancario();
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["IDCONCEPTOBANCARIO"] != DBNull.Value) entidad.id_conceptobancario = Convert.ToInt32(resultado["IDCONCEPTOBANCARIO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToString(resultado["TIPO_CONCEPTO"]);
                            lstConceptoBancario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConceptoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoBancarioData", "ListarConceptoBancario", ex);
                        return null;
                    }
                }
            }
        }
       
    }
}