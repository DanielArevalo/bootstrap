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
    public class FamiliaresPolizasData : GlobalData
    {

    protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public FamiliaresPolizasData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Obtiene la lista de Familiares dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<FamiliaresPolizas> ListarFamiliares(FamiliaresPolizas pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<FamiliaresPolizas> lstFamiliares = new List<FamiliaresPolizas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT consecutivo,cod_poliza,nombres, identificacion, sexo,parentesco,fecha_nacimiento,actividad FROM POLIZASEGUROSFAMILIARES ";// +ObtenerFiltro(pEntidad);
                        if (pEntidad.cod_poliza.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where cod_poliza =  " + pEntidad.cod_poliza.ToString();
                        } 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            FamiliaresPolizas entidad = new FamiliaresPolizas();
                            //Asociar todos los valores a la entidad
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                            if (resultado["parentesco"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["parentesco"]);
                            if (resultado["fecha_nacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fecha_nacimiento"]);
                            if (resultado["actividad"] != DBNull.Value) entidad.actividad = Convert.ToString(resultado["actividad"]);
                            lstFamiliares.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstFamiliares;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresPolizasData", "ListarFamiliares", ex);
                        return null;
                    }

                }
            }
        }
        /// <summary>
        /// Modifica una entidad Familiares en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Familiares</param>
        /// <returns>Entidad modificada</returns>
        public FamiliaresPolizas ModificarFamiliares(FamiliaresPolizas pEntidad, Usuario pUsuario)
        {


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pEntidad.consecutivo;
                        p_consecutivo.DbType = DbType.Int16;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Input;
                        
                        DbParameter p_nombres = cmdTransaccionFactory.CreateParameter();
                        p_nombres.ParameterName = "p_nombres";
                        p_nombres.Value = pEntidad.nombres;
                        p_nombres.DbType = DbType.String;
                        p_nombres.Size = 50;
                        p_nombres.Direction = ParameterDirection.Input;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pEntidad.identificacion;
                        p_identificacion.DbType = DbType.String;
                        p_identificacion.Size = 50;
                        p_identificacion.Direction = ParameterDirection.Input;
                                                                      
                        DbParameter p_sexo = cmdTransaccionFactory.CreateParameter();
                        p_sexo.ParameterName = "p_sexo";
                        p_sexo.Value = pEntidad.sexo;
                        p_sexo.DbType = DbType.String;
                        p_sexo.Size = 25;
                        p_sexo.Direction = ParameterDirection.Input;

                        DbParameter p_parentesco = cmdTransaccionFactory.CreateParameter();
                        p_parentesco.ParameterName = "p_parentesco";
                        p_parentesco.Value = pEntidad.parentesco;
                        p_parentesco.DbType = DbType.String;
                        p_parentesco.Size = 25;
                        p_parentesco.Direction = ParameterDirection.Input; 

                        DbParameter p_fecha_nacimiento = cmdTransaccionFactory.CreateParameter();
                        p_fecha_nacimiento.ParameterName = "p_fecha_nacimiento";
                        p_fecha_nacimiento.Value = pEntidad.fecha_nacimiento;
                        p_fecha_nacimiento.DbType = DbType.Date;
                       // p_fecha_nacimiento.Size = 18;
                        p_fecha_nacimiento.Direction = ParameterDirection.Input;

                        DbParameter p_actividad = cmdTransaccionFactory.CreateParameter();
                        p_actividad.ParameterName = "p_actividad";
                        p_actividad.Value = pEntidad.actividad;
                        p_actividad.DbType = DbType.String;
                        p_actividad.Size = 8;
                        p_actividad.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_sexo);
                        cmdTransaccionFactory.Parameters.Add(p_parentesco);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_nacimiento);
                        cmdTransaccionFactory.Parameters.Add(p_actividad);
                                             
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_FAM_POL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresPolizasData", "ModificarFamiliares", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Crea una entidad Familiares en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Familiares</param>
        /// <returns>Entidad modificada</returns>
        public FamiliaresPolizas InsertarFamiliares(FamiliaresPolizas pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                      
                        DbParameter p_cod_poliza = cmdTransaccionFactory.CreateParameter();
                        p_cod_poliza.ParameterName = "p_cod_poliza";
                        p_cod_poliza.Value = pEntidad.cod_poliza;
                        p_cod_poliza.DbType = DbType.Int64;
                        p_cod_poliza.Size = 8;
                        p_cod_poliza.Direction = ParameterDirection.Input;

                        DbParameter p_nombres = cmdTransaccionFactory.CreateParameter();
                        p_nombres.ParameterName = "p_nombres";
                        p_nombres.Value = pEntidad.nombres;
                        p_nombres.DbType = DbType.String;
                        p_nombres.Size = 58;
                        p_nombres.Direction = ParameterDirection.Input;

                        DbParameter p_identificacion = cmdTransaccionFactory.CreateParameter();
                        p_identificacion.ParameterName = "p_identificacion";
                        p_identificacion.Value = pEntidad.identificacion;
                        p_identificacion.DbType = DbType.Int64;
                        p_identificacion.Size = 50;
                        p_identificacion.Direction = ParameterDirection.Input;

                        DbParameter p_sexo = cmdTransaccionFactory.CreateParameter();
                        p_sexo.ParameterName = "p_sexo";
                        p_sexo.Value = pEntidad.sexo;
                        p_sexo.DbType = DbType.String;
                        p_sexo.Size = 25;
                        p_sexo.Direction = ParameterDirection.Input;
                        
                        DbParameter p_parentesco = cmdTransaccionFactory.CreateParameter();
                        p_parentesco.ParameterName = "p_parentesco";
                        p_parentesco.Value = pEntidad.parentesco;
                        p_parentesco.DbType = DbType.String;
                        p_parentesco.Size = 25;
                        p_parentesco.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_nacimiento = cmdTransaccionFactory.CreateParameter();
                        p_fecha_nacimiento.ParameterName = "p_fecha_nacimiento";
                        p_fecha_nacimiento.Value = pEntidad.fecha_nacimiento;
                        p_fecha_nacimiento.DbType = DbType.Date;
                       // p_fecha_nacimiento.Size = 8;
                        p_fecha_nacimiento.Direction = ParameterDirection.Input;

                        DbParameter p_actividad = cmdTransaccionFactory.CreateParameter();
                        p_actividad.ParameterName = "p_actividad";
                        p_actividad.Value = pEntidad.actividad;
                        p_actividad.DbType = DbType.String;
                        p_actividad.Size = 8;
                        p_actividad.Direction = ParameterDirection.Input;
                       
                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_sexo);
                        cmdTransaccionFactory.Parameters.Add(p_parentesco);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_nacimiento);
                        cmdTransaccionFactory.Parameters.Add(p_actividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_FAM_POL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresPolizasData", "InsertarFamiliares", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Elimina un Familiar en la base de datos
        /// </summary>
        /// <param name="pId">identificador de Familiares</param>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        FamiliaresPolizas pEntidad = new FamiliaresPolizas();

                        pEntidad = ConsultarFamiliaresPolizas(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_consecutivo = cmdTransaccionFactory.CreateParameter();
                        p_consecutivo.ParameterName = "p_consecutivo";
                        p_consecutivo.Value = pId;
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 8;
                        p_consecutivo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_consecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_FAM_POLIZ_ELIMI";

                        cmdTransaccionFactory.ExecuteNonQuery();

                        //     if (pUsuario.programaGeneraLog)
                        //  DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_poliza), "POLIZA",Accion.Eliminar.ToString(),connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresPolizasData", "EliminarFamiliares", ex);
                    }
                }

            }
        }


        /// <summary>
        /// Obtiene un registro de la tabla PolizaSegurosFamiliares de la base de datos relacionado
        /// Familiar, responsable
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Familiar consultado</returns>
        public FamiliaresPolizas ConsultarFamiliaresPolizas(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            FamiliaresPolizas entidad = new FamiliaresPolizas();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT consecutivo,cod_poliza, nombres, identificacion,sexo, parentesco,fecha_nacimiento,actividad FROM POLIZASEGUROSFAMILIARES where consecutivo =" + pId.ToString();
                        connection.Open();                       
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            //Asociar todos los valores a la entidad
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["sexo"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["sexo"]);
                            if (resultado["parentesco"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["parentesco"]);
                            if (resultado["fecha_nacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fecha_nacimiento"]);
                            if (resultado["actividad"] != DBNull.Value) entidad.actividad = Convert.ToString(resultado["actividad"]);


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
                        BOExcepcion.Throw("ListarFamiliaresPolizasData", "ConsultarFamiliares", ex);
                        return null;
                    }
                }
            }
        }
    }
}
