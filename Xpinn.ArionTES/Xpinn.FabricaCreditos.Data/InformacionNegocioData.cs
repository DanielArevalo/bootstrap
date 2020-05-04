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
    /// Objeto de acceso a datos para la tabla Negocio
    /// </summary>
    public class InformacionNegocioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Negocio
        /// </summary>
        public InformacionNegocioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Negocio de la base de datos
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio creada</returns>
        public InformacionNegocio CrearInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_NEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_NEGOCIO.ParameterName = "p_COD_NEGOCIO";
                        pCOD_NEGOCIO.Value = pInformacionNegocio.cod_negocio;
                        pCOD_NEGOCIO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInformacionNegocio.cod_persona;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        pDIRECCION.Value = pInformacionNegocio.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        pTELEFONO.Value = pInformacionNegocio.telefono;

                        DbParameter pLOCALIDAD = cmdTransaccionFactory.CreateParameter();
                        pLOCALIDAD.ParameterName = "p_LOCALIDAD";
                        pLOCALIDAD.Value = pInformacionNegocio.localidad;

                        DbParameter pNOMBRENEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pNOMBRENEGOCIO.ParameterName = "p_NOMBRENEGOCIO";
                        pNOMBRENEGOCIO.Value = pInformacionNegocio.nombrenegocio;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInformacionNegocio.descripcion;

                        DbParameter pANTIGUEDAD = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDAD.ParameterName = "p_ANTIGUEDAD";
                        pANTIGUEDAD.Value = pInformacionNegocio.antiguedad;

                        DbParameter pPROPIA = cmdTransaccionFactory.CreateParameter();
                        pPROPIA.ParameterName = "p_PROPIA";
                        pPROPIA.Value = pInformacionNegocio.propia;

                        DbParameter pARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pARRENDADOR.ParameterName = "p_ARRENDADOR";
                        pARRENDADOR.Value = pInformacionNegocio.arrendador;

                        DbParameter pTELEFONOARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOARRENDADOR.ParameterName = "p_TELEFONOARRENDADOR";
                        pTELEFONOARRENDADOR.Value = pInformacionNegocio.telefonoarrendador;

                        DbParameter pCODACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODACTIVIDAD.ParameterName = "p_CODACTIVIDAD";
                        pCODACTIVIDAD.Value = pInformacionNegocio.codactividad;

                        DbParameter pBARRIO = cmdTransaccionFactory.CreateParameter();
                        pBARRIO.ParameterName = "p_BARRIO";
                        pBARRIO.Value = pInformacionNegocio.barrio;

                        DbParameter pEXPERIENCIA = cmdTransaccionFactory.CreateParameter();
                        pEXPERIENCIA.ParameterName = "p_EXPERIENCIA";
                        pEXPERIENCIA.Value = pInformacionNegocio.experiencia;

                        DbParameter pEMPLPERM = cmdTransaccionFactory.CreateParameter();
                        pEMPLPERM.ParameterName = "p_EMPLPERM";
                        pEMPLPERM.Value = pInformacionNegocio.emplperm;

                        DbParameter pEMPLTEM = cmdTransaccionFactory.CreateParameter();
                        pEMPLTEM.ParameterName = "p_EMPLTEM";
                        pEMPLTEM.Value = pInformacionNegocio.empltem;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHACREACION.Value = pInformacionNegocio.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "p_USUARIOCREACION";
                        pUSUARIOCREACION.Value = pInformacionNegocio.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "p_FECULTMOD";
                        pFECULTMOD.Value = pInformacionNegocio.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        pUSUULTMOD.Value = pInformacionNegocio.usuultmod;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_Valor";
                        pvalor.Value = pInformacionNegocio.valor_arriendo;

                        DbParameter psector = cmdTransaccionFactory.CreateParameter();
                        psector.ParameterName = "p_sector";
                        psector.Value = pInformacionNegocio.sector;

                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(pCOD_NEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOCALIDAD);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRENEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDAD);
                        cmdTransaccionFactory.Parameters.Add(pPROPIA);
                        cmdTransaccionFactory.Parameters.Add(pARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pCODACTIVIDAD);
                        cmdTransaccionFactory.Parameters.Add(pBARRIO);
                        cmdTransaccionFactory.Parameters.Add(pEXPERIENCIA);
                        cmdTransaccionFactory.Parameters.Add(pEMPLPERM);
                        cmdTransaccionFactory.Parameters.Add(pEMPLTEM);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(psector);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_NEGOC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionNegocio, "Negocio",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pInformacionNegocio.cod_negocio = Convert.ToInt64(pCOD_NEGOCIO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInformacionNegocio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionNegocioData", "CrearInformacionNegocio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Negocio de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad InformacionNegocio modificada</returns>
        public InformacionNegocio ModificarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_NEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_NEGOCIO.ParameterName = "p_COD_NEGOCIO";
                        pCOD_NEGOCIO.Value = pInformacionNegocio.cod_negocio;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pInformacionNegocio.cod_persona;

                        DbParameter pDIRECCION = cmdTransaccionFactory.CreateParameter();
                        pDIRECCION.ParameterName = "p_DIRECCION";
                        pDIRECCION.Value = pInformacionNegocio.direccion;

                        DbParameter pTELEFONO = cmdTransaccionFactory.CreateParameter();
                        pTELEFONO.ParameterName = "p_TELEFONO";
                        pTELEFONO.Value = pInformacionNegocio.telefono;

                        DbParameter pLOCALIDAD = cmdTransaccionFactory.CreateParameter();
                        pLOCALIDAD.ParameterName = "p_LOCALIDAD";
                        pLOCALIDAD.Value = pInformacionNegocio.localidad;

                        DbParameter pBARRIO = cmdTransaccionFactory.CreateParameter();
                        pBARRIO.ParameterName = "p_BARRIO";
                        pBARRIO.Value = pInformacionNegocio.barrio;

                        DbParameter pNOMBRENEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pNOMBRENEGOCIO.ParameterName = "p_NOMBRENEGOCIO";
                        pNOMBRENEGOCIO.Value = pInformacionNegocio.nombrenegocio;

                        DbParameter pDESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        pDESCRIPCION.ParameterName = "p_DESCRIPCION";
                        pDESCRIPCION.Value = pInformacionNegocio.descripcion;

                        DbParameter pANTIGUEDAD = cmdTransaccionFactory.CreateParameter();
                        pANTIGUEDAD.ParameterName = "p_ANTIGUEDAD";
                        pANTIGUEDAD.Value = pInformacionNegocio.antiguedad;

                        DbParameter pPROPIA = cmdTransaccionFactory.CreateParameter();
                        pPROPIA.ParameterName = "p_PROPIA";
                        pPROPIA.Value = pInformacionNegocio.propia;

                        DbParameter pARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pARRENDADOR.ParameterName = "p_ARRENDADOR";
                        pARRENDADOR.Value = pInformacionNegocio.arrendador;

                        DbParameter pTELEFONOARRENDADOR = cmdTransaccionFactory.CreateParameter();
                        pTELEFONOARRENDADOR.ParameterName = "p_TELEFONOARRENDADOR";
                        pTELEFONOARRENDADOR.Value = pInformacionNegocio.telefonoarrendador;

                        DbParameter pCODACTIVIDAD = cmdTransaccionFactory.CreateParameter();
                        pCODACTIVIDAD.ParameterName = "p_CODACTIVIDAD";
                        pCODACTIVIDAD.Value = pInformacionNegocio.codactividad;

                        DbParameter pEXPERIENCIA = cmdTransaccionFactory.CreateParameter();
                        pEXPERIENCIA.ParameterName = "p_EXPERIENCIA";
                        pEXPERIENCIA.Value = pInformacionNegocio.experiencia;

                        DbParameter pEMPLPERM = cmdTransaccionFactory.CreateParameter();
                        pEMPLPERM.ParameterName = "p_EMPLPERM";
                        pEMPLPERM.Value = pInformacionNegocio.emplperm;

                        DbParameter pEMPLTEM = cmdTransaccionFactory.CreateParameter();
                        pEMPLTEM.ParameterName = "p_EMPLTEM";
                        pEMPLTEM.Value = pInformacionNegocio.empltem;

                        DbParameter pFECHACREACION = cmdTransaccionFactory.CreateParameter();
                        pFECHACREACION.ParameterName = "p_FECHACREACION";
                        pFECHACREACION.Value = pInformacionNegocio.fechacreacion;

                        DbParameter pUSUARIOCREACION = cmdTransaccionFactory.CreateParameter();
                        pUSUARIOCREACION.ParameterName = "p_USUARIOCREACION";
                        pUSUARIOCREACION.Value = pInformacionNegocio.usuariocreacion;

                        DbParameter pFECULTMOD = cmdTransaccionFactory.CreateParameter();
                        pFECULTMOD.ParameterName = "p_FECULTMOD";
                        pFECULTMOD.Value = pInformacionNegocio.fecultmod;

                        DbParameter pUSUULTMOD = cmdTransaccionFactory.CreateParameter();
                        pUSUULTMOD.ParameterName = "p_USUULTMOD";
                        pUSUULTMOD.Value = pInformacionNegocio.usuultmod;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_Valor";
                        pvalor.Value = pInformacionNegocio.valor_arriendo;

                        DbParameter psector = cmdTransaccionFactory.CreateParameter();
                        psector.ParameterName = "p_sector";
                        psector.Value = pInformacionNegocio.sector;
                        
                        cmdTransaccionFactory.Parameters.Add(pCOD_NEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pDIRECCION);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONO);
                        cmdTransaccionFactory.Parameters.Add(pLOCALIDAD);
                        cmdTransaccionFactory.Parameters.Add(pBARRIO);
                        cmdTransaccionFactory.Parameters.Add(pNOMBRENEGOCIO);
                        cmdTransaccionFactory.Parameters.Add(pDESCRIPCION);
                        cmdTransaccionFactory.Parameters.Add(pANTIGUEDAD);
                        cmdTransaccionFactory.Parameters.Add(pPROPIA);
                        cmdTransaccionFactory.Parameters.Add(pARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pTELEFONOARRENDADOR);
                        cmdTransaccionFactory.Parameters.Add(pCODACTIVIDAD);                        
                        cmdTransaccionFactory.Parameters.Add(pEXPERIENCIA);
                        cmdTransaccionFactory.Parameters.Add(pEMPLPERM);
                        cmdTransaccionFactory.Parameters.Add(pEMPLTEM);
                        cmdTransaccionFactory.Parameters.Add(pFECHACREACION);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIOCREACION);
                        cmdTransaccionFactory.Parameters.Add(pFECULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pUSUULTMOD);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(psector);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_NEGOC_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionNegocio, "Negocio",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pInformacionNegocio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionNegocioData", "ModificarInformacionNegocio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Negocio de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Negocio</param>
        public void EliminarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        InformacionNegocio pInformacionNegocio = new InformacionNegocio();

                        //if (pUsuario.programaGeneraLog)
                        //    pInformacionNegocio = ConsultarInformacionNegocio(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_NEGOCIO = cmdTransaccionFactory.CreateParameter();
                        pCOD_NEGOCIO.ParameterName = "p_COD_NEGOCIO";
                        pCOD_NEGOCIO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_NEGOCIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_NEGOC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pInformacionNegocio, "Negocio", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionNegocioData", "EliminarInformacionNegocio", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Negocio de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Negocio</param>
        /// <returns>Entidad InformacionNegocio consultado</returns>
        public InformacionNegocio ConsultarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            InformacionNegocio entidad = new InformacionNegocio();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT n.* FROM  NEGOCIO n inner join persona p on p.cod_persona=n.cod_persona WHERE COD_NEGOCIO = " + pId.ToString() + " ORDER BY n.cod_negocio DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_NEGOCIO"] != DBNull.Value) entidad.cod_negocio = Convert.ToInt64(resultado["COD_NEGOCIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["LOCALIDAD"] != DBNull.Value) entidad.localidad = Convert.ToString(resultado["LOCALIDAD"]);
                            if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToInt64(resultado["BARRIO"]);
                            if (resultado["NOMBRENEGOCIO"] != DBNull.Value) entidad.nombrenegocio = Convert.ToString(resultado["NOMBRENEGOCIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ANTIGUEDAD"] != DBNull.Value) entidad.antiguedad = Convert.ToInt64(resultado["ANTIGUEDAD"]);
                            if (resultado["PROPIA"] != DBNull.Value) entidad.propia = Convert.ToInt64(resultado["PROPIA"]);
                            if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                            if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                            if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                            if (resultado["EXPERIENCIA"] != DBNull.Value) entidad.experiencia = Convert.ToDecimal(resultado["EXPERIENCIA"]);
                            if (resultado["EMPLPERM"] != DBNull.Value) entidad.emplperm = Convert.ToInt64(resultado["EMPLPERM"]);
                            if (resultado["EMPLTEM"] != DBNull.Value) entidad.empltem = Convert.ToInt64(resultado["EMPLTEM"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);
                            if (resultado["VRARRIENDO"] != DBNull.Value) entidad.valor_arriendo = Convert.ToInt64(resultado["VRARRIENDO"]);
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
                        BOExcepcion.Throw("InformacionNegocioData", "ConsultarInformacionNegocio", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Negocio dados unos filtros
        /// </summary>
        /// <param name="pNegocio">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionNegocio obtenidos</returns>
        public List<InformacionNegocio> ListarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionNegocio> lstInformacionNegocio = new List<InformacionNegocio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;


                        if (pInformacionNegocio.ListaSolicitada != "Localidad" && pInformacionNegocio.ListaSolicitada != "Barrio" && pInformacionNegocio.ListaSolicitada != "BarrioLocalidad" && pInformacionNegocio.ListaSolicitada != "Actividad")                        
                        {   
                            sql = "Select * From  NEGOCIO Where cod_persona = " + pInformacionNegocio.cod_persona.ToString() + " Order by cod_negocio desc";
                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                InformacionNegocio entidad = new InformacionNegocio();
                                if (resultado["COD_NEGOCIO"] != DBNull.Value) entidad.cod_negocio = Convert.ToInt64(resultado["COD_NEGOCIO"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                                if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                                if (resultado["LOCALIDAD"] != DBNull.Value) entidad.localidad = Convert.ToString(resultado["LOCALIDAD"]);
                                if (resultado["BARRIO"] != DBNull.Value) entidad.barrio = Convert.ToInt64(resultado["BARRIO"]);
                                if (resultado["NOMBRENEGOCIO"] != DBNull.Value) entidad.nombrenegocio = Convert.ToString(resultado["NOMBRENEGOCIO"]);
                                if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                                if (resultado["ANTIGUEDAD"] != DBNull.Value) entidad.antiguedad = Convert.ToInt64(resultado["ANTIGUEDAD"]);
                                if (resultado["PROPIA"] != DBNull.Value) entidad.propia = Convert.ToInt64(resultado["PROPIA"]);
                                if (resultado["ARRENDADOR"] != DBNull.Value) entidad.arrendador = Convert.ToString(resultado["ARRENDADOR"]);
                                if (resultado["TELEFONOARRENDADOR"] != DBNull.Value) entidad.telefonoarrendador = Convert.ToString(resultado["TELEFONOARRENDADOR"]);
                                if (resultado["CODACTIVIDAD"] != DBNull.Value) entidad.codactividad = Convert.ToInt64(resultado["CODACTIVIDAD"]);
                                if (resultado["EXPERIENCIA"] != DBNull.Value) entidad.experiencia = Convert.ToDecimal(resultado["EXPERIENCIA"]);
                                if (resultado["EMPLPERM"] != DBNull.Value) entidad.emplperm = Convert.ToInt64(resultado["EMPLPERM"]);
                                if (resultado["EMPLTEM"] != DBNull.Value) entidad.empltem = Convert.ToInt64(resultado["EMPLTEM"]);
                                if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                                if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                                if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                                if (resultado["USUULTMOD"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["USUULTMOD"]);

                                lstInformacionNegocio.Add(entidad);                              
                            }
                        }
                        
                        else
                        {
                            switch (pInformacionNegocio.ListaSolicitada)
                            {
                                case "Localidad":
                                    sql = "select codlocalidad as ListaId, nombre as ListaDescripcion from localidad ";
                                    break;
                                
                                case "Barrio":                                  
                                    sql = "select codbarrio as ListaId, nombre as ListaDescripcion from barrio where codlocalidad = " + Convert.ToInt64(pInformacionNegocio.localidad.ToString());
                                    break;

                                case "BarrioLocalidad":
                                    sql = "select codbarrio as ListaId, nombre as ListaDescripcion from barrio where codlocalidad = " + Convert.ToInt64(pInformacionNegocio.localidad.ToString());
                                break;

                                case "Actividad":
                                sql = " select CODACTIVIDADNEGOCIO as ListaId, DESCRIPCION as ListaDescripcion from actividad_negocio";
                                break;
                            }
                            
                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                InformacionNegocio entidad = new InformacionNegocio();
                                if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); 
                                if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                                lstInformacionNegocio.Add(entidad);
                            }                          
                        
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstInformacionNegocio; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionNegocioData", "ListarInformacionNegocio", ex);
                        return null;
                    }
                }
            }
        }
       
        /// <summary>
        /// Obtiene las listas desplegables de la tabla InformacionNegocio
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Datos Solicitud obtenidas</returns>
        public List<InformacionNegocio> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<InformacionNegocio> lstDatosSolicitud = new List<InformacionNegocio>();

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
                            case "Localidad":
                                sql = "select codlocalidad as ListaId, nombre as ListaDescripcion from localidad ";                               
                                break;

                            case "Barrio":
                                sql = "select codbarrio as ListaId, nombre as ListaDescripcion from barrio ";
                                break;                            
                        }

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            InformacionNegocio entidad = new InformacionNegocio();
                            if (ListaSolicitada == "TipoCredito" || ListaSolicitada == "Periodicidad" || ListaSolicitada == "Medio" || ListaSolicitada == "Lugares")  //Diferencia entre los Ids de tabla, que pueden ser integer o varchar
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ListaId"]); }
                            else
                            { if (resultado["ListaId"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ListaId"]); }

                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["ListaDescripcion"]);
                            lstDatosSolicitud.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatosSolicitud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("InformacionNegocioData", "InformacionNegocio", ex);
                        return null;
                    }
                }
            }
        }
            
    }
}