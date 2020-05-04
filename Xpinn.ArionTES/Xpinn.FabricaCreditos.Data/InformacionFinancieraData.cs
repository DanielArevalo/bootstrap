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
    /// Objeto de acceso a datos para la tabla InformacionFinanciera
    /// </summary>
    public class InformacionFinancieraData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla InformacionFinanciera
        /// </summary>
        public InformacionFinancieraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla InformacionFinanciera de la base de datos
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera creada</returns>
        public InformacionFinanciera CrearInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInformacionFinanciera.cod_inffin;
                        pCOD_INFFIN.Direction = ParameterDirection.InputOutput;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_FECHA";
                        pFECHA.Value = pInformacionFinanciera.fecha;
                        pFECHA.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInformacionFinanciera.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INFIN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionFinanciera, "InformacionFinanciera",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pInformacionFinanciera.cod_inffin = Convert.ToInt64(pCOD_INFFIN.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInformacionFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "CrearInformacionFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla InformacionFinanciera de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad InformacionFinanciera modificada</returns>
        public InformacionFinanciera ModificarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pInformacionFinanciera.cod_inffin;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "p_FECHA";
                        pFECHA.Value = pInformacionFinanciera.fecha;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInformacionFinanciera.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INFIN_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionFinanciera, "InformacionFinanciera",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInformacionFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ModificarInformacionFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla InformacionFinanciera de la base de datos
        /// </summary>
        /// <param name="pId">identificador de InformacionFinanciera</param>
        public void EliminarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        InformacionFinanciera pInformacionFinanciera = new InformacionFinanciera();

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_INFIN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionFinanciera, "InformacionFinanciera", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "EliminarInformacionFinanciera", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla InformacionFinanciera de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera consultado</returns>
        public InformacionFinanciera ConsultarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            InformacionFinanciera entidad = new InformacionFinanciera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INFORMACIONFINANCIERA WHERE COD_INFFIN = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ConsultarInformacionFinanciera", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinanciera = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  INFORMACIONFINANCIERA ";// +ObtenerFiltro(pInformacionFinanciera);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);

                            lstInformacionFinanciera.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinanciera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanciera", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanNegRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 1 and 18 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")"; 
                                            
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 20 and 26 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 39 and 52 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 53 and 64 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 65 and 68 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 69 and 73 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla InformacionFinanciera dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepoeg(InformacionFinanciera pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionFinanciera> lstInformacionFinancierarepo = new List<InformacionFinanciera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select  a.*, c.descripcion,b.valor,c.cod_cuenta from INFORMACIONFINANCIERA a inner join ESTADOSFINANCIEROS b on a.COD_INFFIN= b.COD_INFFIN   inner join ESTADOSFINANCIEROSestructura c on b.cod_cuenta=c.cod_cuenta where  c.cod_cuenta between 28 and 38 AND  A.COD_INFFIN =(SELECT MAX(A.COD_INFFIN) FROM INFORMACIONFINANCIERA A where  a.cod_persona = " + pId.cod_persona + ")";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            InformacionFinanciera entidad = new InformacionFinanciera();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            lstInformacionFinancierarepo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionFinancierarepo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionFinancieraData", "ListarInformacionFinanRepoNeg", ex);
                        return null;
                    }
                }
            }
        }
        
    }
}