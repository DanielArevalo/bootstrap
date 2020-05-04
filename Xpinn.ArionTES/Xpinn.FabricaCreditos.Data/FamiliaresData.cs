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
    /// Objeto de acceso a datos para la tabla FAMILIARES
    /// </summary>
    public class FamiliaresData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla FAMILIARES
        /// </summary>
        public FamiliaresData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla FAMILIARES de la base de datos
        /// </summary>
        /// <param name="pFamiliares">Entidad Familiares</param>
        /// <returns>Entidad Familiares creada</returns>
        public Familiares CrearFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODFAMILIAR = cmdTransaccionFactory.CreateParameter();
                        pCODFAMILIAR.ParameterName = "p_CODFAMILIAR";
                        pCODFAMILIAR.Value = pFamiliares.codfamiliar;
                        pCODFAMILIAR.Direction = ParameterDirection.InputOutput;
                        
                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pFamiliares.cod_persona;
                        

                        DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        pNOMBRES.ParameterName = "p_NOMBRES";
                        pNOMBRES.Value = pFamiliares.nombres;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";                       
                        if (pFamiliares.identificacion =="")
                           pIDENTIFICACION.Value = "0";
                        else 
                        {
                            pIDENTIFICACION.Value = pFamiliares.identificacion;
                        }

                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        pCODPARENTESCO.Value = pFamiliares.codparentesco;

                        DbParameter pSEXO = cmdTransaccionFactory.CreateParameter();
                        pSEXO.ParameterName = "p_SEXO";
                        pSEXO.Value = pFamiliares.sexo;

                        DbParameter pACARGO = cmdTransaccionFactory.CreateParameter();
                        pACARGO.ParameterName = "p_ACARGO";
                        pACARGO.Value = pFamiliares.acargo;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        pOBSERVACIONES.Value = pFamiliares.observaciones;

                        DbParameter pFECHANACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pFECHANACIMIENTO.ParameterName = "p_FECHANACIMIENTO";
                        pFECHANACIMIENTO.Value = pFamiliares.fechanacimiento;

                        DbParameter pESTUDIA = cmdTransaccionFactory.CreateParameter();
                        pESTUDIA.ParameterName = "p_ESTUDIA";
                        pESTUDIA.Value = pFamiliares.estudia;

                        cmdTransaccionFactory.Parameters.Add(pCODFAMILIAR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pSEXO);
                        cmdTransaccionFactory.Parameters.Add(pACARGO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pFECHANACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pESTUDIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_FAMIL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pFamiliares, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pFamiliares.codfamiliar = Convert.ToInt64(pCODFAMILIAR.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFamiliares;
                    }
                    catch (Exception ex)
                    {
                        string vError = ex.ToString();
                        //BOExcepcion.Throw("FamiliaresData", "CrearFamiliares", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla FAMILIARES de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Familiares modificada</returns>
        public Familiares ModificarFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODFAMILIAR = cmdTransaccionFactory.CreateParameter();
                        pCODFAMILIAR.ParameterName = "p_CODFAMILIAR";
                        pCODFAMILIAR.Value = pFamiliares.codfamiliar;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pFamiliares.cod_persona;

                        DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        pNOMBRES.ParameterName = "p_NOMBRES";
                        pNOMBRES.Value = pFamiliares.nombres;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "p_IDENTIFICACION";
                        pIDENTIFICACION.Value = pFamiliares.identificacion;

                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        pCODPARENTESCO.Value = pFamiliares.codparentesco;

                        DbParameter pSEXO = cmdTransaccionFactory.CreateParameter();
                        pSEXO.ParameterName = "p_SEXO";
                        pSEXO.Value = pFamiliares.sexo;

                        DbParameter pACARGO = cmdTransaccionFactory.CreateParameter();
                        pACARGO.ParameterName = "p_ACARGO";
                        pACARGO.Value = pFamiliares.acargo;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        pOBSERVACIONES.Value = pFamiliares.observaciones;

                        DbParameter pFECHANACIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pFECHANACIMIENTO.ParameterName = "p_FECHANACIMIENTO";
                        pFECHANACIMIENTO.Value = pFamiliares.fechanacimiento;

                        DbParameter pESTUDIA = cmdTransaccionFactory.CreateParameter();
                        pESTUDIA.ParameterName = "p_ESTUDIA";
                        pESTUDIA.Value = pFamiliares.estudia;

                        cmdTransaccionFactory.Parameters.Add(pCODFAMILIAR);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pSEXO);
                        cmdTransaccionFactory.Parameters.Add(pACARGO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pFECHANACIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pESTUDIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_FAMIL_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pFamiliares, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFamiliares;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresData", "ModificarFamiliares", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla FAMILIARES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de FAMILIARES</param>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Familiares pFamiliares = new Familiares();

                        //if (pUsuario.programaGeneraLog)
                        //    pFamiliares = ConsultarFamiliares(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCODFAMILIAR = cmdTransaccionFactory.CreateParameter();
                        pCODFAMILIAR.ParameterName = "p_CODFAMILIAR";
                        pCODFAMILIAR.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODFAMILIAR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_FAMIL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pFamiliares, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresData", "InsertarFamiliares", ex);
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla FAMILIARES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla FAMILIARES</param>
        /// <returns>Entidad Familiares consultado</returns>
        public Familiares ConsultarFamiliares(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Familiares entidad = new Familiares();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = "SELECT * FROM FAMILIARES where CODFAMILIAR=" + pId.ToString();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        connection.Open();
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODFAMILIAR"] != DBNull.Value) entidad.codfamiliar = Convert.ToInt64(resultado["CODFAMILIAR"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["ACARGO"] != DBNull.Value) entidad.acargo = Convert.ToInt64(resultado["ACARGO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ACARGO"] != DBNull.Value) entidad.acargo = Convert.ToInt64(resultado["ACARGO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["ESTUDIA"] != DBNull.Value) entidad.estudia = Convert.ToInt64(resultado["ESTUDIA"]);                            
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
                        BOExcepcion.Throw("FamiliaresData", "ConsultarFamiliares", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla FAMILIARES dados unos filtros
        /// </summary>
        /// <param name="pFAMILIARES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<Familiares> ListarFamiliares(Familiares Familiares, Usuario pUsuario, String Id)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Familiares> lstFamiliares = new List<Familiares>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        string sql = @"SELECT FAMILIARES.*, parentescos.descripcion FROM FAMILIARES, parentescos 
                                       where parentescos.codparentesco = familiares.codparentesco and COD_PERSONA=" + Id.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Familiares entidad = new Familiares();

                            if (resultado["CODFAMILIAR"] != DBNull.Value) entidad.codfamiliar = Convert.ToInt64(resultado["CODFAMILIAR"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["SEXO"] != DBNull.Value) entidad.sexo = Convert.ToString(resultado["SEXO"]);
                            if (resultado["ACARGO"] != DBNull.Value) entidad.acargo = Convert.ToInt64(resultado["ACARGO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHANACIMIENTO"] != DBNull.Value) entidad.fechanacimiento = Convert.ToDateTime(resultado["FECHANACIMIENTO"]);
                            if (resultado["ESTUDIA"] != DBNull.Value) entidad.estudia = Convert.ToInt64(resultado["ESTUDIA"]);

                            lstFamiliares.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstFamiliares;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("FamiliaresData", "ListarFamiliares", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene las listas de nuevo familiar
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<Familiares> ListasDesplegables(Familiares pEntidad, Usuario pUsuario, String ListaSolicitada)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Familiares> lstDatosSolicitud = new List<Familiares>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                       
                        sql = "select CODPARENTESCO as ListaId, DESCRIPCION as ListaDescripcion from PARENTESCOS ";
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Familiares entidad = new Familiares();
                            if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); 
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClientenData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }
        

    }
}