using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;
using System.Data.Common;

namespace Xpinn.FabricaCreditos.Data
{
    public class InformacionAdicionalData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public InformacionAdicionalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        #region PERSONA INFORMACION ADICIONAL

        public InformacionAdicional CrearPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidinfadicional = cmdTransaccionFactory.CreateParameter();
                        pidinfadicional.ParameterName = "p_idinfadicional";
                        pidinfadicional.Value = pAdicional.idinfadicional;
                        pidinfadicional.Direction = ParameterDirection.Output;
                        pidinfadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidinfadicional);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAdicional.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_infadicional = cmdTransaccionFactory.CreateParameter();
                        pcod_infadicional.ParameterName = "p_cod_infadicional";
                        pcod_infadicional.Value = pAdicional.cod_infadicional;
                        pcod_infadicional.Direction = ParameterDirection.Input;
                        pcod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_infadicional);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (string.IsNullOrWhiteSpace(pAdicional.valor))
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pAdicional.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERINFOADI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAdicional.idinfadicional = Convert.ToInt32(pidinfadicional.Value);
                        return pAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "CrearPersona_InfoAdicional", ex);
                        return null;
                    }
                }
            }
        }


        public InformacionAdicional ModificarPersona_InfoAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidinfadicional = cmdTransaccionFactory.CreateParameter();
                        pidinfadicional.ParameterName = "p_idinfadicional";
                        pidinfadicional.Value = pAdicional.idinfadicional;
                        pidinfadicional.Direction = ParameterDirection.Input;
                        pidinfadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidinfadicional);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pAdicional.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_infadicional = cmdTransaccionFactory.CreateParameter();
                        pcod_infadicional.ParameterName = "p_cod_infadicional";
                        pcod_infadicional.Value = pAdicional.cod_infadicional;
                        pcod_infadicional.Direction = ParameterDirection.Input;
                        pcod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_infadicional);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pAdicional.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERINFOADI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAdicional.idinfadicional = Convert.ToInt32(pidinfadicional.Value);
                        return pAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ModificarPersona_InfoAdicional", ex);
                        return null;
                    }
                }
            }
        }

        public void ModificarPersonaInformacionAdicional(InformacionAdicional pAdicional, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = pAdicional.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        p_cod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_cod_infadicional = cmdTransaccionFactory.CreateParameter();
                        p_cod_infadicional.ParameterName = "p_cod_infadicional";
                        p_cod_infadicional.Value = pAdicional.cod_infadicional;
                        p_cod_infadicional.Direction = ParameterDirection.Input;
                        p_cod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_infadicional);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pAdicional.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERINFOADIC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ModificarPersonaInformacionAdicional", ex);
                    }
                }
            }
        }


        #endregion

        #region PERSONA INFORMACION ADICIONAL

        public InformacionAdicional CrearTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_infadicional = cmdTransaccionFactory.CreateParameter();
                        pcod_infadicional.ParameterName = "p_cod_infadicional";
                        pcod_infadicional.Value = pAdicional.cod_infadicional;
                        pcod_infadicional.Direction = ParameterDirection.Output;
                        pcod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_infadicional);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pAdicional.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pAdicional.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pitems_lista = cmdTransaccionFactory.CreateParameter();
                        pitems_lista.ParameterName = "p_items_lista";
                        pitems_lista.Value = pAdicional.items_lista;
                        pitems_lista.Direction = ParameterDirection.Input;
                        pitems_lista.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pitems_lista);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pAdicional.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_INFADICION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAdicional.cod_infadicional = Convert.ToInt32(pcod_infadicional.Value);
                        return pAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "CrearTipo_InforAdicional", ex);
                        return null;
                    }
                }
            }
        }


        public InformacionAdicional ModificarTipo_InforAdicional(InformacionAdicional pAdicional, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_infadicional = cmdTransaccionFactory.CreateParameter();
                        pcod_infadicional.ParameterName = "p_cod_infadicional";
                        pcod_infadicional.Value = pAdicional.cod_infadicional;
                        pcod_infadicional.Direction = ParameterDirection.Input;
                        pcod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_infadicional);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pAdicional.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pAdicional.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pitems_lista = cmdTransaccionFactory.CreateParameter();
                        pitems_lista.ParameterName = "p_items_lista";
                        pitems_lista.Value = pAdicional.items_lista;
                        pitems_lista.Direction = ParameterDirection.Input;
                        pitems_lista.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pitems_lista);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        ptipo_persona.Value = pAdicional.tipo_persona;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_INFADICION_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ModificarTipo_InforAdicional", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarInformacionAdicional(Int64 pIdActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_infadicional = cmdTransaccionFactory.CreateParameter();
                        pcod_infadicional.ParameterName = "p_cod_infadicional";
                        pcod_infadicional.Value = pIdActividad;
                        pcod_infadicional.Direction = ParameterDirection.Input;
                        pcod_infadicional.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_infadicional);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_INFADICION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "EliminarInformacionAdicional", ex);
                    }
                }
            }
        }

        #endregion

        public List<InformacionAdicional> ListarInformacionAdicional(InformacionAdicional pInformacion,string tipo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select  Cod_Infadicional,Descripcion,Tipo,Items_Lista, "
                                            + "case Tipo_Persona when 'N' then 'NATURAL' WHEN 'J' THEN 'JURIDICA' WHEN 'M' THEN 'MENOR DE EDAD' END AS Tipo_Persona "
                                            + "From Tipo_Infadicional where Tipo_Persona = '" + tipo +"' "  
                                            +"ORDER BY COD_INFADICIONAL ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            InformacionAdicional entidad = new InformacionAdicional();
                            if (resultado["COD_INFADICIONAL"] != DBNull.Value) entidad.cod_infadicional = Convert.ToInt32(resultado["COD_INFADICIONAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["ITEMS_LISTA"] != DBNull.Value) entidad.items_lista = Convert.ToString(resultado["ITEMS_LISTA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            lstInformacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ListarInformacionAdicional", ex);
                        return null;
                    }
                }
            }
        }


        public List<InformacionAdicional> ListarInformacionAdicionalGeneral(InformacionAdicional pInformacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select  Cod_Infadicional,Descripcion,Tipo,Items_Lista, "
                                            + "case Tipo_Persona when 'N' then 'NATURAL' WHEN 'J' THEN 'JURIDICA' WHEN 'M' THEN 'MENOR DE EDAD' END AS Tipo_Persona "
                                            + "From Tipo_Infadicional ORDER BY COD_INFADICIONAL ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            InformacionAdicional entidad = new InformacionAdicional();
                            if (resultado["COD_INFADICIONAL"] != DBNull.Value) entidad.cod_infadicional = Convert.ToInt32(resultado["COD_INFADICIONAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["ITEMS_LISTA"] != DBNull.Value) entidad.items_lista = Convert.ToString(resultado["ITEMS_LISTA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            lstInformacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ListarInformacionAdicional", ex);
                        return null;
                    }
                }
            }
        }



        public List<InformacionAdicional> ConsultarInformacionAdicional(InformacionAdicional pInfo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<InformacionAdicional> lstInfo = new List<InformacionAdicional>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from tipo_infadicional";

                        if (pInfo.descripcion == "N" || pInfo.descripcion == "J" || pInfo.descripcion == "M")
                        {
                            sql += " WHERE TIPO_PERSONA = '" + pInfo.descripcion + "'";
                        }
                        else
                        {
                            sql += " WHERE COD_INFADICIONAL = " + pInfo.cod_infadicional;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            InformacionAdicional entidad = new InformacionAdicional();
                            if (resultado["COD_INFADICIONAL"] != DBNull.Value) entidad.cod_infadicional = Convert.ToInt32(resultado["COD_INFADICIONAL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["ITEMS_LISTA"] != DBNull.Value) entidad.items_lista = Convert.ToString(resultado["ITEMS_LISTA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            lstInfo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInfo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ConsultarInformacionAdicional", ex);
                        return null;
                    }
                }
            }
        }

        public string ConsultarInformacionPersonalDeUnaPersona(long codigoPersona, long codigoTipoInformacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            string valor = string.Empty;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select valor from PERSONA_INFADICIONAL where cod_persona = " + codigoPersona + " and cod_infadicional = " + codigoTipoInformacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToString(resultado["valor"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ConsultarInformacionPersonalDeUnaPersona", ex);
                        return null;
                    }
                }
            }
        }

        public List<InformacionAdicional> ListarPersonaInformacion(Int64 pCod,string tipo_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<InformacionAdicional> lstInformacion = new List<InformacionAdicional>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.idinfadicional, p.cod_persona, t.cod_infadicional, T.ITEMS_LISTA, p.valor, t.tipo, t.descripcion "
                                       + " From TIPO_INFADICIONAL T Left Outer Join PERSONA_INFADICIONAL P On P.Cod_Infadicional = T.Cod_Infadicional And p.cod_persona = " + pCod
                                       + " Where (t.tipo_persona = '" + tipo_persona + "' Or t.tipo_persona Is Null)  Order By t.cod_infadicional ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            InformacionAdicional entidad = new InformacionAdicional();
                            if (resultado["IDINFADICIONAL"] != DBNull.Value) entidad.idinfadicional = Convert.ToInt32(resultado["IDINFADICIONAL"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_INFADICIONAL"] != DBNull.Value) entidad.cod_infadicional = Convert.ToInt32(resultado["COD_INFADICIONAL"]);
                            if (resultado["ITEMS_LISTA"] != DBNull.Value) entidad.items_lista = Convert.ToString(resultado["ITEMS_LISTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstInformacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ListarPersonaInformacion", ex);
                        return null;
                    }
                }
            }
        }
        public InformacionAdicional ActualizacionMasiva(InformacionAdicional pEntidad,Usuario pUsuario)
        {
            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        PIDENTIFICACION.ParameterName = "PIDENTIFICACION";
                        PIDENTIFICACION.Value = pEntidad.identificacion;
                        PIDENTIFICACION.Direction = ParameterDirection.Input;
                        PIDENTIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PIDENTIFICACION);

                        DbParameter PCLASIFICACION = cmdTransaccionFactory.CreateParameter();
                        PCLASIFICACION.ParameterName = "PCLASIFICACION";
                        PCLASIFICACION.Value = pEntidad.valor;
                        PCLASIFICACION.Direction = ParameterDirection.Input;
                        PCLASIFICACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PCLASIFICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PER_INFADICIONAL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "ActualizacionMasiva", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarActividadPersona(Int64 pIdActividad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {       
                        DbParameter pidactividad = cmdTransaccionFactory.CreateParameter();
                        pidactividad.ParameterName = "p_idactividad";
                        pidactividad.Value = pIdActividad;
                        pidactividad.Direction = ParameterDirection.Input;
                        pidactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidactividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_APO_PERSONAACT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionAdicionalData", "EliminarActividadPersona", ex);
                    }
                }
            }
        }




    }
}
