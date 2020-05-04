using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla OBComponente
    /// </summary>
    public class ComponenteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Componente
        /// </summary>
        public ComponenteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Componente de la base de datos
        /// </summary>
        /// <param name="pComponente">Entidad Componente</param>
        /// <returns>Entidad Componente creada</returns>
        public Componente CrearComponente(Componente pComponente, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCodComponente = cmdTransaccionFactory.CreateParameter();
                        pCodComponente.ParameterName = "p_CodComponente";
                        pCodComponente.Value = 0;
                        pCodComponente.Direction = ParameterDirection.InputOutput;

                        DbParameter pNombre = cmdTransaccionFactory.CreateParameter();
                        pNombre.ParameterName = "p_Nombre";
                        pNombre.Value = pComponente.NOMBRE;

                        DbParameter pCausa = cmdTransaccionFactory.CreateParameter();
                        pCausa.ParameterName = "p_Causa";
                        pCausa.Value = pComponente.CAUSA;

                        cmdTransaccionFactory.Parameters.Add(pCodComponente);
                        cmdTransaccionFactory.Parameters.Add(pNombre);
                        cmdTransaccionFactory.Parameters.Add(pCausa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMPONENTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pComponente.CODCOMPONENTE = Convert.ToInt64(pCodComponente.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pComponente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteData", "CrearComponente", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ComponenteS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Componente modificada</returns>
        public Componente ModificarComponente(Componente pComponente, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCodComponente = cmdTransaccionFactory.CreateParameter();
                        pCodComponente.ParameterName = "p_CodComponente";
                        pCodComponente.Value = pComponente.CODCOMPONENTE;

                        DbParameter pNombre = cmdTransaccionFactory.CreateParameter();
                        pNombre.ParameterName = "p_Nombre";
                        pNombre.Value = pComponente.NOMBRE;

                        DbParameter pCausa = cmdTransaccionFactory.CreateParameter();
                        pCausa.ParameterName = "p_Causa";
                        pCausa.Value = pComponente.CAUSA;

                        cmdTransaccionFactory.Parameters.Add(pCodComponente);
                        cmdTransaccionFactory.Parameters.Add(pNombre);
                        cmdTransaccionFactory.Parameters.Add(pCausa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_COMPONENTE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pComponente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteData", "ModificarComponente", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ComponenteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ComponenteS</param>
        public void EliminarComponente(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_CodComponente = cmdTransaccionFactory.CreateParameter();
                        p_CodComponente.ParameterName = "p_CodComponente";
                        p_CodComponente.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_CodComponente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPONENTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteData", "EliminarComponente", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ComponenteS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ComponenteS</param>
        /// <returns>Entidad Componente consultado</returns>
        public Componente ConsultarComponente(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Componente entidad = new Componente();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT CodComponente, Nombre, Causa FROM ObComponente" +
                                     " WHERE CodComponente = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CodComponente"] != DBNull.Value) entidad.CODCOMPONENTE = Convert.ToInt64(resultado["CodComponente"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["Nombre"]);
                            if (resultado["Causa"] != DBNull.Value) entidad.CAUSA = Convert.ToInt64(resultado["Causa"]);
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
                        BOExcepcion.Throw("ComponenteData", "ConsultarComponente", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla OBCOMPONENTE dados unos filtros
        /// </summary>
        /// <param name="pComponente">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Componente obtenidos</returns>
        public List<Componente> ListarComponentes(Componente pComponente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Componente> lstComponente = new List<Componente>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM OBCOMPONENTE WHERE visible = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Componente entidad = new Componente();

                            if (resultado["CODCOMPONENTE"] != DBNull.Value) entidad.CODCOMPONENTE= Convert.ToInt64(resultado["CODCOMPONENTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);

                            lstComponente.Add(entidad);
                        }

                        return lstComponente;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteData", "ListarComponentes", ex);
                        return null;
                    }
                }
            }
        }
    }
}
