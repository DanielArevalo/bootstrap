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
    /// Objeto de acceso a datos para la tabla Referncias
    /// </summary>
    public class RefernciasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Referncias
        /// </summary>
        public RefernciasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Referncias de la base de datos
        /// </summary>
        /// <param name="pReferncias">Entidad Referncias</param>
        /// <returns>Entidad Referncias creada</returns>
        public Referncias CrearReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODREFERENCIA.ParameterName = "p_CODREFERENCIA";
                        pCODREFERENCIA.Value = 0;
                        pCODREFERENCIA.Direction = ParameterDirection.Output;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pReferncias.cod_persona;

                        DbParameter pTIPOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPOREFERENCIA.ParameterName = "p_TIPOREFERENCIA";
                        pTIPOREFERENCIA.Value = pReferncias.tiporeferencia;

                        DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        pNOMBRES.ParameterName = "p_NOMBRES";
                        pNOMBRES.Value = pReferncias.nombres;

                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        if (pReferncias.codparentesco != 0)
                            pCODPARENTESCO.Value = pReferncias.codparentesco;
                        else
                            pCODPARENTESCO.Value = DBNull.Value;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        pDIRECCION.Value = pReferncias.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        pTELEFONO.Value = pReferncias.telefono;

                        DbParameter pTELOFICINA = cmdTransaccionFactory.CreateParameter();
                        pTELOFICINA.ParameterName = "p_TELOFICINA";
                        pTELOFICINA.Value = pReferncias.teloficina;

                        DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        pCELULAR.ParameterName = "p_CELULAR";
                        pCELULAR.Value = pReferncias.celular;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pReferncias.estado;

                        DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pCODUSUVERIFICA.ParameterName = "p_CODUSUVERIFICA";
                        pCODUSUVERIFICA.Value = pReferncias.codusuverifica;

                        DbParameter pFECHAVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pFECHAVERIFICA.ParameterName = "p_FECHAVERIFICA";
                        pFECHAVERIFICA.Value = pReferncias.fechaverifica;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = "p_CALIFICACION";
                        if (pReferncias.calificacion != null)
                            pCALIFICACION.Value = pReferncias.calificacion;
                        else
                            pCALIFICACION.Value = "";

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        if (pReferncias.observaciones != null)
                            pOBSERVACIONES.Value = pReferncias.observaciones;
                        else
                            pOBSERVACIONES.Value = "";

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        if (pReferncias.numero_radicacion != 0)
                            pNUMERO_RADICACION.Value = pReferncias.numero_radicacion;
                        else
                            pNUMERO_RADICACION.Value = DBNull.Value;


                        DbParameter p_NUMERO_SOLICITUD = cmdTransaccionFactory.CreateParameter();
                        p_NUMERO_SOLICITUD.ParameterName = "p_NUMERO_SOLICITUD";
                        if (pReferncias.numero_solicitud != null)
                            p_NUMERO_SOLICITUD.Value = pReferncias.numero_solicitud;
                        else
                            p_NUMERO_SOLICITUD.Value = DBNull.Value;


                        cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pTELOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);
                        cmdTransaccionFactory.Parameters.Add(p_NUMERO_SOLICITUD);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_REFER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferncias, "Referncias",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pReferncias.codreferencia = Convert.ToInt64(pCODREFERENCIA.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReferncias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefernciasData", "CrearReferncias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Referncias de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Referncias modificada</returns>
        public Referncias ModificarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODREFERENCIA.ParameterName = "p_CODREFERENCIA";
                        pCODREFERENCIA.Value = pReferncias.codreferencia;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pReferncias.cod_persona;

                        DbParameter pTIPOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pTIPOREFERENCIA.ParameterName = "p_TIPOREFERENCIA";
                        pTIPOREFERENCIA.Value = pReferncias.tiporeferencia;

                        DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        pNOMBRES.ParameterName = "p_NOMBRES";
                        pNOMBRES.Value = pReferncias.nombres;

                        DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        pCODPARENTESCO.Value = pReferncias.codparentesco;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        pDIRECCION.Value = pReferncias.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        pTELEFONO.Value = pReferncias.telefono;

                        DbParameter pTELOFICINA = cmdTransaccionFactory.CreateParameter();
                        pTELOFICINA.ParameterName = "p_TELOFICINA";
                        pTELOFICINA.Value = pReferncias.teloficina;

                        DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        pCELULAR.ParameterName = "p_CELULAR";
                        pCELULAR.Value = pReferncias.celular;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_ESTADO";
                        pESTADO.Value = pReferncias.estado;

                        DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pCODUSUVERIFICA.ParameterName = "p_CODUSUVERIFICA";
                        pCODUSUVERIFICA.Value = pReferncias.codusuverifica;

                        DbParameter pFECHAVERIFICA = cmdTransaccionFactory.CreateParameter();
                        pFECHAVERIFICA.ParameterName = "p_FECHAVERIFICA";
                        pFECHAVERIFICA.Value = pReferncias.fechaverifica;

                        DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        pCALIFICACION.ParameterName = "p_CALIFICACION";
                        pCALIFICACION.Value = pReferncias.calificacion;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        pOBSERVACIONES.Value = pReferncias.observaciones;

                        DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        pNUMERO_RADICACION.Value = pReferncias.numero_radicacion;

                        cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pTIPOREFERENCIA);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pTELOFICINA);
                        cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pFECHAVERIFICA);
                        cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_REFER_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferncias, "Referncias",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReferncias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefernciasData", "ModificarReferncias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Referncias de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Referncias</param>
        public void EliminarReferncias(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Referncias pReferncias = new Referncias();

                        DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        pCODREFERENCIA.ParameterName = "p_CODREFERENCIA";
                        pCODREFERENCIA.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_REFER_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pReferncias, "Referncias", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefernciasData", "EliminarReferncias", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Referncias de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Referncias</param>
        /// <returns>Entidad Referncias consultado</returns>
        public Referncias ConsultarReferncias(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Referncias entidad = new Referncias();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  REFERENCIAS WHERE CODREFERENCIA = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.codreferencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tiporeferencia = Convert.ToInt64(resultado["TIPOREFERENCIA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["TELOFICINA"] != DBNull.Value) entidad.teloficina = Convert.ToString(resultado["TELOFICINA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODUSUVERIFICA"] != DBNull.Value) entidad.codusuverifica = Convert.ToInt64(resultado["CODUSUVERIFICA"]);
                            if (resultado["FECHAVERIFICA"] != DBNull.Value) entidad.fechaverifica = Convert.ToDateTime(resultado["FECHAVERIFICA"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
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
                        BOExcepcion.Throw("RefernciasData", "ConsultarReferncias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Referncias dados unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referncias> lstReferncias = new List<Referncias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT REFERENCIAS.*, PARENTESCOS.DESCRIPCION  FROM  REFERENCIAS LEFT JOIN PARENTESCOS ON PARENTESCOS.CODPARENTESCO = REFERENCIAS.CODPARENTESCO WHERE REFERENCIAS.NUMERO_RADICACION = 0 and REFERENCIAS.cod_persona = " + Convert.ToInt64(pReferncias.cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referncias entidad = new Referncias();

                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.codreferencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tiporeferencia = Convert.ToInt64(resultado["TIPOREFERENCIA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["TELOFICINA"] != DBNull.Value) entidad.teloficina = Convert.ToString(resultado["TELOFICINA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODUSUVERIFICA"] != DBNull.Value) entidad.codusuverifica = Convert.ToInt64(resultado["CODUSUVERIFICA"]);
                            if (resultado["FECHAVERIFICA"] != DBNull.Value) entidad.fechaverifica = Convert.ToDateTime(resultado["FECHAVERIFICA"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstReferncias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferncias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefernciasData", "ListarReferncias", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Referncias dados unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferenciasRepo(Referncias pReferncias, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referncias> lstReferncias = new List<Referncias>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT REFERENCIAS.*, PARENTESCOS.DESCRIPCION  FROM  REFERENCIAS LEFT JOIN PARENTESCOS ON PARENTESCOS.CODPARENTESCO = REFERENCIAS.CODPARENTESCO WHERE  REFERENCIAS.cod_persona = " + Convert.ToInt64(pReferncias.cod_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Referncias entidad = new Referncias();

                            if (resultado["CODREFERENCIA"] != DBNull.Value) entidad.codreferencia = Convert.ToInt64(resultado["CODREFERENCIA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPOREFERENCIA"] != DBNull.Value) entidad.tiporeferencia = Convert.ToInt64(resultado["TIPOREFERENCIA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt64(resultado["CODPARENTESCO"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["TELOFICINA"] != DBNull.Value) entidad.teloficina = Convert.ToString(resultado["TELOFICINA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["CODUSUVERIFICA"] != DBNull.Value) entidad.codusuverifica = Convert.ToInt64(resultado["CODUSUVERIFICA"]);
                            if (resultado["FECHAVERIFICA"] != DBNull.Value) entidad.fechaverifica = Convert.ToDateTime(resultado["FECHAVERIFICA"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstReferncias.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReferncias;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RefernciasData", "ListarReferenciasRepo", ex);
                        return null;
                    }
                }
            }
        }
        

        /// <summary>
        /// Obtiene las listas desplegables de la tabla Persona
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<Referncias> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Referncias> lstDatosSolicitud = new List<Referncias>();

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
                        switch (ListaSolicitada)
                        {
                            case "Parentesco":
                                sql = "select codparentesco as ListaId, descripcion as ListaDescripcion from parentescos ";
                                break;
                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Referncias entidad = new Referncias();

                            if (ListaSolicitada == "TipoCredito" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            {
                                if (resultado["ListaId"] != DBNull.Value)
                                {
                                    entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]);
                                }
                            }
                            else
                            {
                                if (resultado["ListaId"] != DBNull.Value)
                                {
                                    entidad.ListaId = Convert.ToInt64(resultado["ListaId"]);
                                    entidad.codparentesco = entidad.ListaId;
                                }
                            }

                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosSolicitud;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosClienteData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }

    }
}