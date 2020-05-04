using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ACHcampo
    /// </summary>
    public class ACHcampoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACHcampo
        /// </summary>
        public ACHcampoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ACHcampo de la base de datos
        /// </summary>
        /// <param name="pACHcampo">Entidad ACHcampo</param>
        /// <returns>Entidad ACHcampo creada</returns>
        public ACHcampo CrearACHcampo(ACHcampo pACHcampo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHcampo.codigo;
                        pcodigo.Direction = ParameterDirection.Output;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);



                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pACHcampo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pACHcampo.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pACHcampo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pACHcampo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pACHcampo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_dato = cmdTransaccionFactory.CreateParameter();
                        ptipo_dato.ParameterName = "p_tipo_dato";
                        if (pACHcampo.tipo_dato == null)
                            ptipo_dato.Value = DBNull.Value;
                        else
                            ptipo_dato.Value = pACHcampo.tipo_dato;
                        ptipo_dato.Direction = ParameterDirection.Input;
                        ptipo_dato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_dato);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        if (pACHcampo.justificacion == null)
                            pjustificacion.Value = DBNull.Value;
                        else
                            pjustificacion.Value = pACHcampo.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pformato = cmdTransaccionFactory.CreateParameter();
                        pformato.ParameterName = "p_formato";
                        if (pACHcampo.formato == null)
                            pformato.Value = DBNull.Value;
                        else
                            pformato.Value = pACHcampo.formato;
                        pformato.Direction = ParameterDirection.Input;
                        pformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato);

                        DbParameter ppunto = cmdTransaccionFactory.CreateParameter();
                        ppunto.ParameterName = "p_punto";
                        if (pACHcampo.punto == null)
                            ppunto.Value = DBNull.Value;
                        else
                            ppunto.Value = pACHcampo.punto;
                        ppunto.Direction = ParameterDirection.Input;
                        ppunto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppunto);

                        DbParameter pnum_dec = cmdTransaccionFactory.CreateParameter();
                        pnum_dec.ParameterName = "p_num_dec";
                        if (pACHcampo.num_dec == null)
                            pnum_dec.Value = DBNull.Value;
                        else
                            pnum_dec.Value = pACHcampo.num_dec;
                        pnum_dec.Direction = ParameterDirection.Input;
                        pnum_dec.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_dec);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pACHcampo.longitud == null)
                            plongitud.Value = DBNull.Value;
                        else
                            plongitud.Value = pACHcampo.longitud;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pllenado = cmdTransaccionFactory.CreateParameter();
                        pllenado.ParameterName = "p_llenado";
                        if (pACHcampo.llenado == null)
                            pllenado.Value = DBNull.Value;
                        else
                            pllenado.Value = pACHcampo.llenado;
                        pllenado.Direction = ParameterDirection.Input;
                        pllenado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pllenado);

                        DbParameter psuma = cmdTransaccionFactory.CreateParameter();
                        psuma.ParameterName = "p_suma";
                        if (pACHcampo.suma == null)
                            psuma.Value = DBNull.Value;
                        else
                            psuma.Value = pACHcampo.suma;
                        psuma.Direction = ParameterDirection.Input;
                        psuma.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psuma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHCAMPO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pACHcampo.codigo = Convert.ToInt64(pcodigo.Value);

                        return pACHcampo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHcampoData", "CrearACHcampo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ACHcampo de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ACHcampo modificada</returns>
        public ACHcampo ModificarACHcampo(ACHcampo pACHcampo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHcampo.codigo;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pACHcampo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pACHcampo.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pACHcampo.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pACHcampo.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pACHcampo.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo_dato = cmdTransaccionFactory.CreateParameter();
                        ptipo_dato.ParameterName = "p_tipo_dato";
                        if (pACHcampo.tipo_dato == null)
                            ptipo_dato.Value = DBNull.Value;
                        else
                            ptipo_dato.Value = pACHcampo.tipo_dato;
                        ptipo_dato.Direction = ParameterDirection.Input;
                        ptipo_dato.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_dato);

                        DbParameter pjustificacion = cmdTransaccionFactory.CreateParameter();
                        pjustificacion.ParameterName = "p_justificacion";
                        if (pACHcampo.justificacion == null)
                            pjustificacion.Value = DBNull.Value;
                        else
                            pjustificacion.Value = pACHcampo.justificacion;
                        pjustificacion.Direction = ParameterDirection.Input;
                        pjustificacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pjustificacion);

                        DbParameter pformato = cmdTransaccionFactory.CreateParameter();
                        pformato.ParameterName = "p_formato";
                        if (pACHcampo.formato == null)
                            pformato.Value = DBNull.Value;
                        else
                            pformato.Value = pACHcampo.formato;
                        pformato.Direction = ParameterDirection.Input;
                        pformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato);

                        DbParameter ppunto = cmdTransaccionFactory.CreateParameter();
                        ppunto.ParameterName = "p_punto";
                        if (pACHcampo.punto == null)
                            ppunto.Value = DBNull.Value;
                        else
                            ppunto.Value = pACHcampo.punto;
                        ppunto.Direction = ParameterDirection.Input;
                        ppunto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppunto);

                        DbParameter pnum_dec = cmdTransaccionFactory.CreateParameter();
                        pnum_dec.ParameterName = "p_num_dec";
                        if (pACHcampo.num_dec == null)
                            pnum_dec.Value = DBNull.Value;
                        else
                            pnum_dec.Value = pACHcampo.num_dec;
                        pnum_dec.Direction = ParameterDirection.Input;
                        pnum_dec.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_dec);

                        DbParameter plongitud = cmdTransaccionFactory.CreateParameter();
                        plongitud.ParameterName = "p_longitud";
                        if (pACHcampo.longitud == null)
                            plongitud.Value = DBNull.Value;
                        else
                            plongitud.Value = pACHcampo.longitud;
                        plongitud.Direction = ParameterDirection.Input;
                        plongitud.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(plongitud);

                        DbParameter pllenado = cmdTransaccionFactory.CreateParameter();
                        pllenado.ParameterName = "p_llenado";
                        if (pACHcampo.llenado == null)
                            pllenado.Value = DBNull.Value;
                        else
                            pllenado.Value = pACHcampo.llenado;
                        pllenado.Direction = ParameterDirection.Input;
                        pllenado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pllenado);

                        DbParameter psuma = cmdTransaccionFactory.CreateParameter();
                        psuma.ParameterName = "p_suma";
                        if (pACHcampo.suma == null)
                            psuma.Value = DBNull.Value;
                        else
                            psuma.Value = pACHcampo.suma;
                        psuma.Direction = ParameterDirection.Input;
                        psuma.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psuma);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHCAMPO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pACHcampo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHcampoData", "ModificarACHcampo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ACHcampo de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ACHcampo</param>
        public void EliminarACHcampo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ACHcampo pACHcampo = new ACHcampo();
                        pACHcampo = ConsultarACHcampo(pId, vUsuario);

                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHcampo.codigo;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHCAMPO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHcampoData", "EliminarACHcampo", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ACHcampo de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ACHcampoS</param>
        /// <returns>Entidad ACHcampo consultado</returns>
        public ACHcampo ConsultarACHcampo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ACHcampo entidad = new ACHcampo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_campo WHERE CODIGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["TIPO_DATO"] != DBNull.Value) entidad.tipo_dato = Convert.ToInt32(resultado["TIPO_DATO"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToInt32(resultado["JUSTIFICACION"]);
                            if (resultado["FORMATO"] != DBNull.Value) entidad.formato = Convert.ToString(resultado["FORMATO"]);
                            if (resultado["PUNTO"] != DBNull.Value) entidad.punto = Convert.ToString(resultado["PUNTO"]);
                            if (resultado["NUM_DEC"] != DBNull.Value) entidad.num_dec = Convert.ToInt32(resultado["NUM_DEC"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToDecimal(resultado["LONGITUD"]);
                            if (resultado["LLENADO"] != DBNull.Value) entidad.llenado = Convert.ToString(resultado["LLENADO"]);
                            if (resultado["SUMA"] != DBNull.Value) entidad.suma = Convert.ToDecimal(resultado["SUMA"]);
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
                        BOExcepcion.Throw("ACHcampoData", "ConsultarACHcampo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ACHcampo dados unos filtros
        /// </summary>
        /// <param name="pACHcampoS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHcampo obtenidos</returns>
        public List<ACHcampo> ListarACHcampo(ACHcampo pACHcampo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHcampo> lstACHcampo = new List<ACHcampo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_campo " + ObtenerFiltro(pACHcampo) + " ORDER BY 2 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ACHcampo entidad = new ACHcampo();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["TIPO_DATO"] != DBNull.Value) entidad.tipo_dato = Convert.ToInt32(resultado["TIPO_DATO"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToInt32(resultado["JUSTIFICACION"]);
                            if (resultado["FORMATO"] != DBNull.Value) entidad.formato = Convert.ToString(resultado["FORMATO"]);
                            if (resultado["PUNTO"] != DBNull.Value) entidad.punto = Convert.ToString(resultado["PUNTO"]);
                            if (resultado["NUM_DEC"] != DBNull.Value) entidad.num_dec = Convert.ToInt32(resultado["NUM_DEC"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToDecimal(resultado["LONGITUD"]);
                            if (resultado["LLENADO"] != DBNull.Value) entidad.llenado = Convert.ToString(resultado["LLENADO"]);
                            if (resultado["SUMA"] != DBNull.Value) entidad.suma = Convert.ToDecimal(resultado["SUMA"]);
                            lstACHcampo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstACHcampo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHcampoData", "ListarACHcampo", ex);
                        return null;
                    }
                }
            }
        }

        public List<ACHcampo> ListarACHcampo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHcampo> lstACHcampo = new List<ACHcampo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.*, b.orden FROM ach_campo a INNER JOIN ach_det_reg b ON a.codigo = b.campo WHERE b.registro = " + pId.ToString() + " ORDER BY b.orden ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ACHcampo entidad = new ACHcampo();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["TIPO_DATO"] != DBNull.Value) entidad.tipo_dato = Convert.ToInt32(resultado["TIPO_DATO"]);
                            if (resultado["JUSTIFICACION"] != DBNull.Value) entidad.justificacion = Convert.ToInt32(resultado["JUSTIFICACION"]);
                            if (resultado["FORMATO"] != DBNull.Value) entidad.formato = Convert.ToString(resultado["FORMATO"]);
                            if (resultado["PUNTO"] != DBNull.Value) entidad.punto = Convert.ToString(resultado["PUNTO"]);
                            if (resultado["NUM_DEC"] != DBNull.Value) entidad.num_dec = Convert.ToInt32(resultado["NUM_DEC"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToDecimal(resultado["LONGITUD"]);
                            if (resultado["LLENADO"] != DBNull.Value) entidad.llenado = Convert.ToString(resultado["LLENADO"]);
                            if (resultado["SUMA"] != DBNull.Value) entidad.suma = Convert.ToDecimal(resultado["SUMA"]);
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToInt32(resultado["ORDEN"]);
                            lstACHcampo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstACHcampo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHcampoData", "ListarACHcampo", ex);
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
                        string sql = "SELECT MAX(codigo) + 1 FROM ach_campo ";

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



    }
}