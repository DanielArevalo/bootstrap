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
    /// Objeto de acceso a datos para la tabla UsuarioAtribuciones
    /// </summary>
    public class UsuarioAtribucionesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla UsuarioAtribuciones
        /// </summary>
        public UsuarioAtribucionesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla UsuarioAtribuciones de la base de datos
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones creada</returns>
        public UsuarioAtribuciones CrearUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        //pCODREFERENCIA.ParameterName = "p_CODREFERENCIA";
                        //pCODREFERENCIA.Value = 0;
                        //pCODREFERENCIA.Direction = ParameterDirection.Output;

                        //DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        //pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        //pCOD_PERSONA.Value = pUsuarioAtribuciones.cod_persona;

                        //DbParameter pTIPOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        //pTIPOREFERENCIA.ParameterName = "p_TIPOREFERENCIA";
                        //pTIPOREFERENCIA.Value = pUsuarioAtribuciones.tiporeferencia;

                        //DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        //pNOMBRES.ParameterName = "p_NOMBRES";
                        //pNOMBRES.Value = pUsuarioAtribuciones.nombres;

                        //DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        //pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        //pCODPARENTESCO.Value = pUsuarioAtribuciones.codparentesco;

                        //DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        //pDIRECCION.ParameterName = "p_DIRECCION";
                        //pDIRECCION.Value = pUsuarioAtribuciones.direccion;

                        //DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        //pTELEFONO.ParameterName = "p_TELEFONO";
                        //pTELEFONO.Value = pUsuarioAtribuciones.telefono;

                        //DbParameter pTELOFICINA = cmdTransaccionFactory.CreateParameter();
                        //pTELOFICINA.ParameterName = "p_TELOFICINA";
                        //pTELOFICINA.Value = pUsuarioAtribuciones.teloficina;

                        //DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        //pCELULAR.ParameterName = "p_CELULAR";
                        //pCELULAR.Value = pUsuarioAtribuciones.celular;

                        //DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        //pESTADO.ParameterName = "p_ESTADO";
                        //pESTADO.Value = pUsuarioAtribuciones.estado;

                        //DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        //pCODUSUVERIFICA.ParameterName = "p_CODUSUVERIFICA";
                        //pCODUSUVERIFICA.Value = pUsuarioAtribuciones.codusuverifica;

                        //DbParameter pFECHAVERIFICA = cmdTransaccionFactory.CreateParameter();
                        //pFECHAVERIFICA.ParameterName = "p_FECHAVERIFICA";
                        //pFECHAVERIFICA.Value = pUsuarioAtribuciones.fechaverifica;

                        //DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        //pCALIFICACION.ParameterName = "p_CALIFICACION";
                        //pCALIFICACION.Value = pUsuarioAtribuciones.calificacion;

                        //DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        //pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        //pOBSERVACIONES.Value = pUsuarioAtribuciones.observaciones;

                        //DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        //pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        //pNUMERO_RADICACION.Value = pUsuarioAtribuciones.numero_radicacion;


                        //cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);
                        //cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        //cmdTransaccionFactory.Parameters.Add(pTIPOREFERENCIA);
                        //cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        //cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        //cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        //cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        //cmdTransaccionFactory.Parameters.Add(pTELOFICINA);
                        //cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        //cmdTransaccionFactory.Parameters.Add(pESTADO);
                        //cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        //cmdTransaccionFactory.Parameters.Add(pFECHAVERIFICA);
                        //cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        //cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        //cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);


                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_REFER_CREAR";
                        //cmdTransaccionFactory.ExecuteNonQuery();

                        ////if (pUsuario.programaGeneraLog)
                        ////    DAauditoria.InsertarLog(pUsuarioAtribuciones, "UsuarioAtribuciones",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        //pUsuarioAtribuciones.codreferencia = Convert.ToInt64(pCODREFERENCIA.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuarioAtribuciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "CrearUsuarioAtribuciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla UsuarioAtribuciones de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad UsuarioAtribuciones modificada</returns>
        public UsuarioAtribuciones ModificarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //DbParameter pCODREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        //pCODREFERENCIA.ParameterName = "p_CODREFERENCIA";
                        //pCODREFERENCIA.Value = pUsuarioAtribuciones.codreferencia;

                        //DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        //pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        //pCOD_PERSONA.Value = pUsuarioAtribuciones.cod_persona;

                        //DbParameter pTIPOREFERENCIA = cmdTransaccionFactory.CreateParameter();
                        //pTIPOREFERENCIA.ParameterName = "p_TIPOREFERENCIA";
                        //pTIPOREFERENCIA.Value = pUsuarioAtribuciones.tiporeferencia;

                        //DbParameter pNOMBRES = cmdTransaccionFactory.CreateParameter();
                        //pNOMBRES.ParameterName = "p_NOMBRES";
                        //pNOMBRES.Value = pUsuarioAtribuciones.nombres;

                        //DbParameter pCODPARENTESCO = cmdTransaccionFactory.CreateParameter();
                        //pCODPARENTESCO.ParameterName = "p_CODPARENTESCO";
                        //pCODPARENTESCO.Value = pUsuarioAtribuciones.codparentesco;

                        //DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        //pDIRECCION.ParameterName = "p_DIRECCION";
                        //pDIRECCION.Value = pUsuarioAtribuciones.direccion;

                        //DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        //pTELEFONO.ParameterName = "p_TELEFONO";
                        //pTELEFONO.Value = pUsuarioAtribuciones.telefono;

                        //DbParameter pTELOFICINA = cmdTransaccionFactory.CreateParameter();
                        //pTELOFICINA.ParameterName = "p_TELOFICINA";
                        //pTELOFICINA.Value = pUsuarioAtribuciones.teloficina;

                        //DbParameter pCELULAR = cmdTransaccionFactory.CreateParameter();
                        //pCELULAR.ParameterName = "p_CELULAR";
                        //pCELULAR.Value = pUsuarioAtribuciones.celular;

                        //DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        //pESTADO.ParameterName = "p_ESTADO";
                        //pESTADO.Value = pUsuarioAtribuciones.estado;

                        //DbParameter pCODUSUVERIFICA = cmdTransaccionFactory.CreateParameter();
                        //pCODUSUVERIFICA.ParameterName = "p_CODUSUVERIFICA";
                        //pCODUSUVERIFICA.Value = pUsuarioAtribuciones.codusuverifica;

                        //DbParameter pFECHAVERIFICA = cmdTransaccionFactory.CreateParameter();
                        //pFECHAVERIFICA.ParameterName = "p_FECHAVERIFICA";
                        //pFECHAVERIFICA.Value = pUsuarioAtribuciones.fechaverifica;

                        //DbParameter pCALIFICACION = cmdTransaccionFactory.CreateParameter();
                        //pCALIFICACION.ParameterName = "p_CALIFICACION";
                        //pCALIFICACION.Value = pUsuarioAtribuciones.calificacion;

                        //DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        //pOBSERVACIONES.ParameterName = "p_OBSERVACIONES";
                        //pOBSERVACIONES.Value = pUsuarioAtribuciones.observaciones;

                        //DbParameter pNUMERO_RADICACION = cmdTransaccionFactory.CreateParameter();
                        //pNUMERO_RADICACION.ParameterName = "p_NUMERO_RADICACION";
                        //pNUMERO_RADICACION.Value = pUsuarioAtribuciones.numero_radicacion;

                        //cmdTransaccionFactory.Parameters.Add(pCODREFERENCIA);
                        //cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        //cmdTransaccionFactory.Parameters.Add(pTIPOREFERENCIA);
                        //cmdTransaccionFactory.Parameters.Add(pNOMBRES);
                        //cmdTransaccionFactory.Parameters.Add(pCODPARENTESCO);
                        //cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        //cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        //cmdTransaccionFactory.Parameters.Add(pTELOFICINA);
                        //cmdTransaccionFactory.Parameters.Add(pCELULAR);
                        //cmdTransaccionFactory.Parameters.Add(pESTADO);
                        //cmdTransaccionFactory.Parameters.Add(pCODUSUVERIFICA);
                        //cmdTransaccionFactory.Parameters.Add(pFECHAVERIFICA);
                        //cmdTransaccionFactory.Parameters.Add(pCALIFICACION);
                        //cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        //cmdTransaccionFactory.Parameters.Add(pNUMERO_RADICACION);

                        //connection.Open();
                        //cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        //cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_REFER_MODIF";
                        //cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pUsuarioAtribuciones, "UsuarioAtribuciones",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuarioAtribuciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "ModificarUsuarioAtribuciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla UsuarioAtribuciones de la base de datos
        /// </summary>
        /// <param name="pId">identificador de UsuarioAtribuciones</param>
        public void EliminarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        UsuarioAtribuciones pUsuarioAtribuciones = new UsuarioAtribuciones();

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
                        //    DAauditoria.InsertarLog(pUsuarioAtribuciones, "UsuarioAtribuciones", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "EliminarUsuarioAtribuciones", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla UsuarioAtribuciones de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones consultado</returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            UsuarioAtribuciones entidad = new UsuarioAtribuciones();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM USUARIO_ATRIBUCIONES WHERE CODUSUARIO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["TIPOATRIBUCION"] != DBNull.Value) entidad.tipoatribucion = Convert.ToInt64(resultado["TIPOATRIBUCION"]);
                            if (resultado["ACTIVO"] != DBNull.Value) entidad.activo = Convert.ToInt64(resultado["ACTIVO"]);                           
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
                        // BOExcepcion.Throw("UsuarioAtribucionesData", "ConsultarUsuarioAtribuciones", ex);
                        return null;
                    }
                }
            }
        }
        

        /// <summary>
        /// Consultar si el usuario un tipo de atribuciòn especifica
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Int64 pTi, Usuario pUsuario)
        {
            DbDataReader resultado;
            UsuarioAtribuciones entidad = new UsuarioAtribuciones();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM USUARIO_ATRIBUCIONES WHERE CODUSUARIO = " + pId.ToString() + " AND TIPOATRIBUCION = " + pTi.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["TIPOATRIBUCION"] != DBNull.Value) entidad.tipoatribucion = Convert.ToInt64(resultado["TIPOATRIBUCION"]);
                            if (resultado["ACTIVO"] != DBNull.Value) entidad.activo = Convert.ToInt64(resultado["ACTIVO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El usuario " + pId.ToString() + " no tiene atributos definidas. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "ConsultarUsuarioAtribuciones", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla UsuarioAtribuciones dados unos filtros
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de UsuarioAtribuciones obtenidos</returns>
        public List<UsuarioAtribuciones> ListarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAtribuciones> lstUsuarioAtribuciones = new List<UsuarioAtribuciones>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM USUARIO_ATRIBUCIONES " + ObtenerFiltro(pUsuarioAtribuciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuarioAtribuciones entidad = new UsuarioAtribuciones();
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);
                            if (resultado["TIPOATRIBUCION"] != DBNull.Value) entidad.tipoatribucion = Convert.ToInt64(resultado["TIPOATRIBUCION"]);
                            if (resultado["ACTIVO"] != DBNull.Value) entidad.activo = Convert.ToInt64(resultado["ACTIVO"]);
                            lstUsuarioAtribuciones.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUsuarioAtribuciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "ListarUsuarioAtribuciones", ex);
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
        public List<UsuarioAtribuciones> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuarioAtribuciones> lstDatosSolicitud = new List<UsuarioAtribuciones>();

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
                            //UsuarioAtribuciones entidad = new UsuarioAtribuciones();
                            //if (ListaSolicitada == "TipoCredito" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            //{ if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]); }
                            //else
                            //{ if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); }

                            //if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            //lstDatosSolicitud.Add(entidad);
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