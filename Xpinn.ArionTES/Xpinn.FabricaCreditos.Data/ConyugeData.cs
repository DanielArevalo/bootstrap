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
    /// Objeto de acceso a datos para la tabla Conyuge
    /// </summary>
    public class ConyugeData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Conyuge
        /// </summary>
        public ConyugeData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Conyuge de la base de datos
        /// </summary>
        /// <param name="pConyuge">Entidad Conyuge</param>
        /// <returns>Entidad Conyuge creada</returns>
        public Conyuge CrearConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONYUGE.ParameterName = "p_COD_CONYUGE";
                        pCOD_CONYUGE.Value = pConyuge.cod_conyuge;
                        pCOD_CONYUGE.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pConyuge.cod_persona;
                        pCOD_PERSONA.Direction = ParameterDirection.InputOutput;


                        cmdTransaccionFactory.Parameters.Add(pCOD_CONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CONYU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pConyuge, "Conyuge",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //pConyuge.cod_conyuge = Convert.ToInt64(pCOD_CONYUGE.Value);
                        return pConyuge;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConyugeData", "CrearConyuge", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Conyuge de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Conyuge modificada</returns>
        public Conyuge ModificarConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONYUGE.ParameterName = "p_COD_CONYUGE";
                        pCOD_CONYUGE.Value = pConyuge.cod_conyuge;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pConyuge.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CONYUGE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CONYU_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pConyuge, "Conyuge",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pConyuge;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConyugeData", "ModificarConyuge", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Conyuge de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Conyuge</param>
        public void EliminarConyuge(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Conyuge pConyuge = new Conyuge();

                        //if (pUsuario.programaGeneraLog)
                        //    pConyuge = ConsultarConyuge(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_CONYUGE = cmdTransaccionFactory.CreateParameter();
                        pCOD_CONYUGE.ParameterName = "p_COD_CONYUGE";
                        pCOD_CONYUGE.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CONYUGE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CONYU_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pConyuge, "Conyuge", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConyugeData", "EliminarConyuge", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Conyuge de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Conyuge</param>
        /// <returns>Entidad Conyuge consultado</returns>
        public Conyuge ConsultarConyuge(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Conyuge entidad = new Conyuge();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONYUGE WHERE COD_PERSONA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CONYUGE"] != DBNull.Value) entidad.cod_conyuge = Convert.ToInt64(resultado["COD_CONYUGE"]);
                           
                        }
                        else
                        {
                            entidad.cod_conyuge = Int64.MinValue; // Retorna codigos en MinValue para no generar excepcion
                            
                        }                  


                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("ConyugeData", "ConsultarConyuge", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Conyuge dados unos filtros
        /// </summary>
        /// <param name="pConyuge">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Conyuge obtenidos</returns>
        public List<Conyuge> ListarConyuge(Conyuge pConyuge, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Conyuge> lstConyuge = new List<Conyuge>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  CONYUGE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Conyuge entidad = new Conyuge();

                            if (resultado["COD_CONYUGE"] != DBNull.Value) entidad.cod_conyuge = Convert.ToInt64(resultado["COD_CONYUGE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);

                            lstConyuge.Add(entidad);
                        }

                        return lstConyuge;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConyugeData", "ListarConyuge", ex);
                        return null;
                    }
                }
            }


        }
        public Conyuge ConsultarRefConyuge(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Conyuge entidad = new Conyuge();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  v_referenciaconyuge WHERE COD_PERSONA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                      
                            if (resultado.Read())
                        {
                           
                            entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            entidad.nombre = Convert.ToString(resultado["NOMBRES"]) + Convert.ToString(resultado["PRIMER_APELLIDO"]) + Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                             entidad.telefono = Convert.ToInt16(resultado["TELEFONO"]);
                             entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                             entidad.actividad = Convert.ToInt16(resultado["CODACTIVIDAD"]); 
                            //if (resultado["actividadempresa"] != DBNull.Value) entidad.actividad_empresa = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                             entidad.nombre_empresa = Convert.ToString(resultado["EMPRESA"]);
                            //if (resultado["NIT"] != DBNull.Value) entidad.nit_empresa = Convert.ToInt16(resultado["NIT"]);
                             entidad.cargo = Convert.ToInt16(resultado["CARGO"]);
                             entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                             entidad.antiguedad_empresa = Convert.ToInt16(resultado["ANTIGUEDADLUGAREMPRESA"]);
                             entidad.direccion_empresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);                           
                        } 
                        
                       

                        return entidad;
                    }
                    catch (Exception ex)
                    {

                        BOExcepcion.Throw("ConyugeData", "ConsultarConyuge", ex);
                        return null;
                    }
                }
            }
        }

        public Conyuge ConsultarRefConyugeRepo(Int64 pId, Usuario pUsuario)         

        {
            DbDataReader resultado;

            Conyuge entidad = new Conyuge();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                      //  string sql = null;

                        string sql = "SELECT * FROM V_CONYUGE_CODEUDOR  where CONYUGE = " + pId.ToString();


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if(resultado.Read())
                        {
                            //Conyuge entidad = new Conyuge();
                            entidad.identificacion = Convert.ToInt64(resultado["IDENTIFICACION"]);
                            entidad.primer_nombre= Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            entidad.primer_nombre = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            entidad.fechaexpedicion= Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            entidad.antiguedadempresa = Convert.ToString(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            entidad.direccion_empresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            entidad.barrio = Convert.ToString(resultado["BARRIO"]);
                            entidad.ciudadexpedicion = Convert.ToString(resultado["CIUDADEXPEDICION"]);
                            entidad.estadocivil = Convert.ToString(resultado["ESTADOCIVIL"]);
                            entidad.escolaridad = Convert.ToString(resultado["ESCOLARIDAD"]);
                            entidad.cargorepo = Convert.ToString(resultado["CARGO"]);
                            entidad.tipocontrato = Convert.ToString(resultado["TIPOCONTRATO"]);
                        }
                         
                           // lstconyuge.Add(entidad);
                            return entidad;
                        
                    
                       
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Persona1Data", "ListadoPersonas1ReporteConyuge", ex);
                        return null;
                    }

                }
            }
        }
    }
}