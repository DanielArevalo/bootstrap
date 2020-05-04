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
    public class BeneficiariosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public BeneficiariosData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Obtiene la lista de Beneficiarios dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Beneficiarios obtenidos</returns>
        public List<Beneficiarios> ListarBeneficiarios(Beneficiarios pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Beneficiarios> lstBeneficiarios = new List<Beneficiarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT consecutivo,cod_poliza, nombres, identificacion, parentesco,fecha_nacimiento,porcentaje FROM POLIZASEGUROBENEFICIARIOS ";// +ObtenerFiltro(pEntidad);
                        if (pEntidad.cod_poliza.ToString().Trim().Length >= 0)
                        {
                            sql = sql + " where cod_poliza =  " + pEntidad.cod_poliza.ToString();
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        //     cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //   cmdTransaccionFactory.CommandText = "XPF_AS_POLIZASEGUROS_CONSULTAR";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Beneficiarios entidad = new Beneficiarios();
                            //Asociar todos los valores a la entidad
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["cod_poliza"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["cod_poliza"]);
                            if (resultado["nombres"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombres"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["parentesco"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["parentesco"]);
                           // if (resultado["fecha_nacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fecha_nacimiento"]);
                            if (resultado["fecha_nacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fecha_nacimiento"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["porcentaje"]);
                            lstBeneficiarios.Add(entidad);
                        }

                        return lstBeneficiarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiariosData", "ListarBeneficiarios", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Modifica una entidad Beneficiarios en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad beneficiarios</param>
        /// <returns>Entidad modificada</returns>
        public Beneficiarios ModificarBeneficiarios(Beneficiarios pEntidad, Usuario pUsuario)
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
                        p_consecutivo.DbType = DbType.Int64;
                        p_consecutivo.Size = 50;
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
                        p_fecha_nacimiento.Direction = ParameterDirection.Input;

                        DbParameter p_porcentaje = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje.ParameterName = "p_porcentaje";
                        p_porcentaje.Value = pEntidad.porcentaje;
                        p_porcentaje.DbType = DbType.Int64;
                        p_porcentaje.Size = 8;
                        p_porcentaje.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(p_consecutivo); 
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_parentesco);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_nacimiento);
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_BENPOL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiariosData", "ModificarBeneficiarios", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Crea una entidad Beneficiarios en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Beneficiarios</param>
        /// <returns>Entidad modificada</returns>
        public Beneficiarios InsertarBeneficiarios(Beneficiarios pEntidad, Usuario pUsuario)
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
                        p_identificacion.DbType = DbType.String;
                        p_identificacion.Size = 50;
                        p_identificacion.Direction = ParameterDirection.Input;

                      
                        DbParameter p_parentesco = cmdTransaccionFactory.CreateParameter();
                        p_parentesco.ParameterName = "p_parentesco";
                        p_parentesco.Value = pEntidad.parentesco;
                        p_parentesco.DbType = DbType.String;
                        p_parentesco.Size = 25;
                        p_parentesco.Direction = ParameterDirection.Input;

                        DbParameter p_fecha_nacimiento = cmdTransaccionFactory.CreateParameter();
                        p_fecha_nacimiento.ParameterName = "p_fecha_nacimiento";
                        p_fecha_nacimiento.Value = pEntidad.fecha_nacimiento;
                       // p_fecha_nacimiento.DbType = DbType.Date;
                       // p_fecha_nacimiento.Size = 8;
                        p_fecha_nacimiento.Direction = ParameterDirection.Input;

                        DbParameter p_porcentaje = cmdTransaccionFactory.CreateParameter();
                        p_porcentaje.ParameterName = "p_porcentaje";
                        p_porcentaje.Value = pEntidad.porcentaje;
                        p_porcentaje.DbType = DbType.Int64;
                        p_porcentaje.Size = 8;
                        p_porcentaje.Direction = ParameterDirection.Input;


                      
                        cmdTransaccionFactory.Parameters.Add(p_cod_poliza);
                        cmdTransaccionFactory.Parameters.Add(p_nombres);
                        cmdTransaccionFactory.Parameters.Add(p_identificacion);
                        cmdTransaccionFactory.Parameters.Add(p_parentesco);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_nacimiento);
                        cmdTransaccionFactory.Parameters.Add(p_porcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_BENPOL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiariosData", "InsertarBeneficiarios", ex);
                        return null;
                    }

                }


            }
        }

        /// <summary>
        /// Elimina un Beneficiario en la base de datos
        /// </summary>
        /// <param name="pId">identificador de Beneficiarios</param>
        public void EliminarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       Beneficiarios pEntidad = new Beneficiarios();

                        //if (pUsuario.programaGeneraLog)
                      pEntidad = ConsultarBeneficiarios(pId, pUsuario); //REGISTRO DE AUDITORIA

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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_BENPOLIZ_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //     if (pUsuario.programaGeneraLog)
                        //  DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_poliza), "POLIZA",Accion.Eliminar.ToString(),connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BeneficiariosData", "EliminarBeneficiarios", ex);
                    }
                }

            }
        }

       
        /// <summary>
        /// Obtiene un registro de la tabla PolizaSegurosBeneficiarios de la base de datos relacionado
        /// Beneficiario, responsable
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Beneficiario consultado</returns>
        public Beneficiarios ConsultarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Beneficiarios entidad = new Beneficiarios();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT consecutivo,cod_poliza, nombres, identificacion, parentesco,fecha_nacimiento,porcentaje FROM POLIZASEGUROBENEFICIARIOS where consecutivo =" + pId.ToString();
                     

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
                            if (resultado["parentesco"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["parentesco"]);
                            if (resultado["fecha_nacimiento"] != DBNull.Value) entidad.fecha_nacimiento = Convert.ToDateTime(resultado["fecha_nacimiento"]);
                            if (resultado["porcentaje"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["porcentaje"]);
                          

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBeneficiariosData", "ConsultarBeneficiarios", ex);
                        return null;
                    }
                }
            }
        }

        public List<Beneficiarios> ConsultarBeneficiariosAUXILIOS(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Beneficiarios> lstentidad = new List<Beneficiarios>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select parentescos.descripcion,b.* from auxilios a inner join auxilios_beneficiarios b on a.numero_auxilio = b.numero_auxilio inner join parentescos on b.cod_parentesco= b.cod_parentesco Where a.numero_radicacion =" + pId.ToString() + "";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Beneficiarios entidad = new Beneficiarios();
                            //Asociar todos los valores a la entidad
                            if (resultado["CODBENEFICIARIOAUX"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CODBENEFICIARIOAUX"]);
                            if (resultado["NUMERO_AUXILIO"] != DBNull.Value) entidad.cod_poliza = Convert.ToInt64(resultado["NUMERO_AUXILIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.parentesco = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["PORCENTAJE_BENEFICIARIO"] != DBNull.Value) entidad.porcentaje = Convert.ToInt64(resultado["PORCENTAJE_BENEFICIARIO"]);
                            lstentidad.Add(entidad);
                        }

                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBeneficiariosData", "ConsultarBeneficiarios", ex);
                        return null;
                    }
                }
            }
        }
    }
}
