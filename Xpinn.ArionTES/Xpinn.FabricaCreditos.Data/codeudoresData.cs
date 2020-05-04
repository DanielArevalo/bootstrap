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
    /// Objeto de acceso a datos para la tabla codeudores
    /// </summary>
    public class codeudoresData : GlobalData
    { 
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla codeudores
        /// </summary>
        public codeudoresData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla codeudores de la base de datos
        /// </summary>
        /// <param name="pcodeudores">Entidad codeudores</param>
        /// <returns>Entidad codeudores creada</returns>
        public codeudores Crearcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCODEUD = cmdTransaccionFactory.CreateParameter();
                        pIDCODEUD.ParameterName = "p_IDCODEUD";
                        pIDCODEUD.Value = pcodeudores.idcodeud;
                        pIDCODEUD.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pcodeudores.numero_radicacion;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pcodeudores.codpersona;

                        DbParameter pTIPO_CODEUDOR = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CODEUDOR.ParameterName = "p_TIPO_CODEUDOR";
                        pTIPO_CODEUDOR.Value = pcodeudores.tipo_codeudor;

                        DbParameter pPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pPARENTESCO.ParameterName = "p_PARENTESCO";
                        pPARENTESCO.Value = pcodeudores.parentesco;

                        DbParameter pOPINION = cmdTransaccionFactory.CreateParameter();
                        pOPINION.ParameterName = "p_OPINION";
                        pOPINION.Value = pcodeudores.opinion;

                        DbParameter pRESPONSABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pRESPONSABILIDAD.ParameterName = "p_RESPONSABILIDAD";
                        if (pcodeudores.responsabilidad != null) pRESPONSABILIDAD.Value = pcodeudores.responsabilidad; else pRESPONSABILIDAD.Value = DBNull.Value;

                        DbParameter pORDEN = cmdTransaccionFactory.CreateParameter();
                        pORDEN.ParameterName = "P_ORDEN";
                        pORDEN.Value = pcodeudores.orden;
                        
                        cmdTransaccionFactory.Parameters.Add(pIDCODEUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CODEUDOR);
                        cmdTransaccionFactory.Parameters.Add(pPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pOPINION);
                        cmdTransaccionFactory.Parameters.Add(pRESPONSABILIDAD);
                        cmdTransaccionFactory.Parameters.Add(pORDEN); 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CODEU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA


                        pcodeudores.idcodeud = Convert.ToInt64(pIDCODEUD.Value);
                        return pcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Crearcodeudores", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla codeudores de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad codeudores modificada</returns>
        public codeudores Modificarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCODEUD = cmdTransaccionFactory.CreateParameter();
                        pIDCODEUD.ParameterName = "p_IDCODEUD";
                        pIDCODEUD.Value = pcodeudores.idcodeud;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pcodeudores.numero_radicacion;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pcodeudores.codpersona;

                        DbParameter pTIPO_CODEUDOR = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CODEUDOR.ParameterName = "p_TIPO_CODEUDOR";
                        pTIPO_CODEUDOR.Value = pcodeudores.tipo_codeudor;

                        DbParameter pPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pPARENTESCO.ParameterName = "p_PARENTESCO";
                        pPARENTESCO.Value = pcodeudores.parentesco;

                        DbParameter pOPINION = cmdTransaccionFactory.CreateParameter();
                        pOPINION.ParameterName = "p_OPINION";
                        pOPINION.Value = pcodeudores.opinion;

                        DbParameter pRESPONSABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pRESPONSABILIDAD.ParameterName = "p_RESPONSABILIDAD";
                        pRESPONSABILIDAD.Value = pcodeudores.responsabilidad;

                        cmdTransaccionFactory.Parameters.Add(pIDCODEUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CODEUDOR);
                        cmdTransaccionFactory.Parameters.Add(pPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pOPINION);
                        cmdTransaccionFactory.Parameters.Add(pRESPONSABILIDAD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CODEU_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Modificarcodeudores", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla codeudores de la base de datos
        /// </summary>
        /// <param name="pId">identificador de codeudores</param>
        public void EliminarcodeudoresSol(Int64 pId, Int64 solicitud, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        codeudores pcodeudores = new codeudores();

                        //if (pUsuario.programaGeneraLog)
                        //    pcodeudores = Consultarcodeudores(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pIDCODEUD = cmdTransaccionFactory.CreateParameter();
                        pIDCODEUD.ParameterName = "p_IDCODEUD";
                        pIDCODEUD.Value = pId;

                        DbParameter p_NUM_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        p_NUM_SOLICITUD.ParameterName = "p_NUM_SOLICITUD";
                        p_NUM_SOLICITUD.Value = solicitud;


                        cmdTransaccionFactory.Parameters.Add(pIDCODEUD);
                        cmdTransaccionFactory.Parameters.Add(p_NUM_SOLICITUD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_CODEU_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Eliminarcodeudores", ex);
                    }
                }
            }
        }
        /// <summary>
        /// Elimina un registro en la tabla codeudores de la base de datos
        /// </summary>
        /// <param name="pId">identificador de codeudores</param>
        public void EliminarcodeudoresCred(Int64 pId, Int64 radicado, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        codeudores pcodeudores = new codeudores();

                        //if (pUsuario.programaGeneraLog)
                        //    pcodeudores = Consultarcodeudores(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_IDCODEUD = cmdTransaccionFactory.CreateParameter();
                        p_IDCODEUD.ParameterName = "p_IDCODEUD";
                        p_IDCODEUD.Value = pId;

                        DbParameter p_NUM_RADICADO = cmdTransaccionFactory.CreateParameter();
                        p_NUM_RADICADO.ParameterName = "P_NUM_RADICADO";
                        p_NUM_RADICADO.Value = radicado;


                        cmdTransaccionFactory.Parameters.Add(p_IDCODEUD);
                        cmdTransaccionFactory.Parameters.Add(p_NUM_RADICADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CODEUDOR_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Eliminarcodeudores", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla codeudores de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla codeudores</param>
        /// <returns>Entidad codeudores consultado</returns>
        public codeudores Consultarcodeudores(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            codeudores entidad = new codeudores();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDCODEUDORES WHERE CODPERSONA = " + pId.ToString() + " AND ROWNUM = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.idcodeud = Convert.ToInt64(resultado["IDCODEUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["TIPO_CODEUDOR"] != DBNull.Value) entidad.tipo_codeudor = Convert.ToString(resultado["TIPO_CODEUDOR"]);
                            if (resultado["PARENTESCO"] != DBNull.Value) entidad.parentesco = Convert.ToInt64(resultado["PARENTESCO"]);
                            if (resultado["OPINION"] != DBNull.Value) entidad.opinion = Convert.ToString(resultado["OPINION"]);
                            if (resultado["RESPONSABILIDAD"] != DBNull.Value) entidad.responsabilidad = Convert.ToString(resultado["RESPONSABILIDAD"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Consultarcodeudores", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla codeudores dados unos filtros
        /// </summary>
        /// <param name="pcodeudores">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de codeudores obtenidos</returns>
        public List<codeudores> Listarcodeudores(codeudores pcodeudores, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<codeudores> lstcodeudores = new List<codeudores>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDCODEUDORES ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            codeudores entidad = new codeudores();

                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.idcodeud = Convert.ToInt64(resultado["IDCODEUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["TIPO_CODEUDOR"] != DBNull.Value) entidad.tipo_codeudor = Convert.ToString(resultado["TIPO_CODEUDOR"]);

                            lstcodeudores.Add(entidad);
                        }

                        return lstcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Listarcodeudores", ex);
                        return null;
                    }
                }
            }
        }

        public List<codeudores> ListarCodeudoresPorCredito(codeudores pcodeudores, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<codeudores> lstcodeudores = new List<codeudores>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  SOLICITUDCREDCODEUDORES ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            codeudores entidad = new codeudores();

                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.idcodeud = Convert.ToInt64(resultado["IDCODEUD"]);
                            if (resultado["NUMEROSOLICITUD"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMEROSOLICITUD"]);
                            if (resultado["CODPERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["CODPERSONA"]);
                            if (resultado["TIPO_CODEUDOR"] != DBNull.Value) entidad.tipo_codeudor = Convert.ToString(resultado["TIPO_CODEUDOR"]);

                            lstcodeudores.Add(entidad);
                        }

                        return lstcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Listarcodeudores", ex);
                        return null;
                    }
                }
            }
        }

        public List<codeudores> ListarCodeudoresCredito(String radicado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<codeudores> lstcodeudores = new List<codeudores>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  v_codeudores  where numero_radicacion="+  radicado;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            codeudores entidad = new codeudores();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            entidad.nombres = entidad.primer_nombre + " " + entidad.segundo_nombre + " " + entidad.primer_apellido + " " + entidad.segundo_apellido;
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                          
                            lstcodeudores.Add(entidad);
                        }

                        return lstcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "ListarCodeudoresCredito", ex);
                        return null;
                    }
                }
            }
        }


        public codeudores CrearCodeudoresCredito(codeudores pcodeudores, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDCODEUD = cmdTransaccionFactory.CreateParameter();
                        pIDCODEUD.ParameterName = "p_IDCODEUD";
                        pIDCODEUD.Value = pcodeudores.idcodeud;
                        pIDCODEUD.Direction = ParameterDirection.InputOutput;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pcodeudores.numero_radicacion;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pcodeudores.codpersona;

                        DbParameter pTIPO_CODEUDOR = cmdTransaccionFactory.CreateParameter();
                        pTIPO_CODEUDOR.ParameterName = "p_TIPO_CODEUDOR";

                        if (!string.IsNullOrWhiteSpace(pcodeudores.tipo_codeudor))
                        {
                            pTIPO_CODEUDOR.Value = pcodeudores.tipo_codeudor;
                        }
                        else
                        {
                            pTIPO_CODEUDOR.Value = DBNull.Value;
                        }            

                        DbParameter pPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pPARENTESCO.ParameterName = "p_PARENTESCO";
                        pPARENTESCO.Value = pcodeudores.parentesco;

                        DbParameter pOPINION = cmdTransaccionFactory.CreateParameter();
                        pOPINION.ParameterName = "p_OPINION";

                        if (!string.IsNullOrWhiteSpace(pcodeudores.opinion))
                        {
                            pOPINION.Value = pcodeudores.opinion;
                        }
                        else
                        {
                            pOPINION.Value = DBNull.Value;
                        }

                        DbParameter pRESPONSABILIDAD = cmdTransaccionFactory.CreateParameter();
                        pRESPONSABILIDAD.ParameterName = "p_RESPONSABILIDAD";
                        if (pcodeudores.responsabilidad != null) pRESPONSABILIDAD.Value = pcodeudores.responsabilidad; else pRESPONSABILIDAD.Value = DBNull.Value;

                        DbParameter pORDEN = cmdTransaccionFactory.CreateParameter();
                        pORDEN.ParameterName = "P_ORDEN";
                        pORDEN.Value = pcodeudores.orden;

                        cmdTransaccionFactory.Parameters.Add(pIDCODEUD);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_CODEUDOR);
                        cmdTransaccionFactory.Parameters.Add(pPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pOPINION);
                        cmdTransaccionFactory.Parameters.Add(pRESPONSABILIDAD);
                        cmdTransaccionFactory.Parameters.Add(pORDEN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CODEUDOR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA


                        pcodeudores.idcodeud = Convert.ToInt64(pIDCODEUD.Value);
                        return pcodeudores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "Crearcodeudores", ex);
                        return null;
                    }
                }
            }
        }


        public codeudores ConsultarDatosCodeudor(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            codeudores entidad = new codeudores();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT V_PERSONA.*,CODEUDORES.IDCODEUD FROM V_PERSONA Left join CODEUDORES on CODEUDORES.CODPERSONA = V_PERSONA.COD_PERSONA  WHERE IDENTIFICACION = '" + pId.ToString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDCODEUD"] != DBNull.Value) entidad.idcodeud = Convert.ToInt64(resultado["IDCODEUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "ConsultarDatosCodeudor", ex);
                        return null;
                    }
                }
            }
        }

        public codeudores ConsultarDatosCodeudorRepo(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            codeudores entidad = new codeudores();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_codeudores WHERE NUMERO_RADICACION = '" + pId.ToString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.codpersona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion= Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre= Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            else
                                entidad.telefono = "0"; 
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToString(resultado["BARRIO"]);
                            if (resultado["FECHAEXPEDICION"] != DBNull.Value) entidad.fechaexpedicion = Convert.ToDateTime(resultado["FECHAEXPEDICION"]);
                            if (resultado["CIUDADEXPEDICION"] != DBNull.Value) entidad.ciudadexpedicion = Convert.ToString(resultado["CIUDADEXPEDICION"]);
                            if (resultado["ESTADOCIVIL"] != DBNull.Value) entidad.estadocivil = Convert.ToString(resultado["ESTADOCIVIL"]);
                            if (resultado["ESCOLARIDAD"] != DBNull.Value) entidad.escolaridad = Convert.ToString(resultado["ESCOLARIDAD"]);
                            if (resultado["NUMPERSONASACARGO"] != DBNull.Value) entidad.personascargo = Convert.ToString(resultado["NUMPERSONASACARGO"]);
                            else
                                entidad.personascargo = "0";

                            if (resultado["TIPOVIVIENDA"] != DBNull.Value) entidad.tipovivienda = Convert.ToString(resultado["TIPOVIVIENDA"]);
                            if (entidad.tipovivienda == "0")
                            {
                                entidad.tipovivienda = "PROPIA";
                            }
                            if (entidad.tipovivienda == "1")
                            {
                                entidad.tipovivienda = "ARRENDADO";
                            }
                            if (entidad.tipovivienda == "2")
                            {
                                entidad.tipovivienda = "FAMILIAR";
                            }
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            else
                                entidad.arrendador = ""; 
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            else
                                entidad.telefonoarrendador = ""; 
                            if (resultado["VALORARRIENDO"] != DBNull.Value) entidad.valorarriendo = Convert.ToInt64(resultado["VALORARRIENDO"]);
                            else
                                entidad.valorarriendo = 0; 
                            if (resultado["EMPRESA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA"]);
                            else
                                entidad.empresa = "";
                            if (resultado["CARGO"] != DBNull.Value) entidad.cargo = Convert.ToString(resultado["CARGO"]);
                            else
                                entidad.cargo = ""; 
                            if (resultado["TIPOCONTRATO"] != DBNull.Value) entidad.tipocontrato = Convert.ToString(resultado["TIPOCONTRATO"]);
                            else
                                entidad.tipocontrato = ""; 
                            if (resultado["ANTIGUEDADLUGAREMPRESA"] != DBNull.Value) entidad.antiguedadempresa = Convert.ToInt64(resultado["ANTIGUEDADLUGAREMPRESA"]);
                            else
                                entidad.antiguedadempresa = 0; 
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefonoempresa = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            else
                                entidad.telefonoempresa = "";
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccionempresa = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            else
                                entidad.direccionempresa = "";
                        }
                        //else
                     //   {
                           // throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                       // }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "ConsultarDatosCodeudorRepo", ex);
                        return null;
                    }
                }
            }
        }



        public Boolean BorrarCodeudoresCredito(Int64 pnumero_radicacion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pnumero_radicacion;
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CODEUDOR_BORRAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pcodeudores, "codeudores",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "BorrarCodeudoresCredito", ex);
                        return false;
                    }
                }
            }
        }

        public codeudores ValidarCodeudor(codeudores pEntidad, Usuario pUsuario, ref string sError)
        {
            sError = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "pcod_persona";
                        pCOD_PERSONA.DbType = DbType.Int64;
                        pCOD_PERSONA.Direction = ParameterDirection.InputOutput;
                        pCOD_PERSONA.Value = pEntidad.codpersona;

                        DbParameter pIDENTIFICACION = cmdTransaccionFactory.CreateParameter();
                        pIDENTIFICACION.ParameterName = "pidentificacion";
                        pIDENTIFICACION.DbType = DbType.String;
                        pIDENTIFICACION.Value = pEntidad.identificacion;

                        DbParameter pNUMERO_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_SOLICITUD.ParameterName = "pnumero_radicacion";
                        pNUMERO_SOLICITUD.DbType = DbType.Int64;
                        pNUMERO_SOLICITUD.Value = pEntidad.numero_radicacion;

                        DbParameter pTIPO = cmdTransaccionFactory.CreateParameter();
                        pTIPO.ParameterName = "ptipo_radicacion";
                        pTIPO.DbType = DbType.Int64;
                        pTIPO.Value = 0;

                        DbParameter pMENSAJE = cmdTransaccionFactory.CreateParameter();
                        pMENSAJE.ParameterName = "pmensaje";
                        pMENSAJE.DbType = DbType.String;
                        pMENSAJE.Size = 200;
                        pMENSAJE.Direction = ParameterDirection.InputOutput;
                        pMENSAJE.Value = pEntidad.mensaje;

                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pIDENTIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_SOLICITUD);
                        cmdTransaccionFactory.Parameters.Add(pTIPO);
                        cmdTransaccionFactory.Parameters.Add(pMENSAJE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VALIDACODEU";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pEntidad.codpersona == 0)
                            pEntidad.codpersona = Convert.ToInt64(pCOD_PERSONA.Value.ToString());

                        pEntidad.mensaje = pMENSAJE.Value.ToString();

                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        sError = ex.Message;
                        return pEntidad;
                    }
                }
            }
        }


        public codeudores ConsultarCantidadCodeudores(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            codeudores entidad = new codeudores();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Count(*) as CANTIDAD 
                                From credito c Inner Join codeudores r On c.numero_radicacion = r.numero_radicacion
                                INNER JOIN PERSONA P ON R.CODPERSONA = P.COD_PERSONA
                                Where c.estado Not In ('T', 'B')  and saldo_capital > 0 And p.identificacion = '" + pId.ToString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt32(resultado["CANTIDAD"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("codeudoresData", "ConsultarCantidadCodeudores", ex);
                        return null;
                    }
                }
            }
        }


    }
}