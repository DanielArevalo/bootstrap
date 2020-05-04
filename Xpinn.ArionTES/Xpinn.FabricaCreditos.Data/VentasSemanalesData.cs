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
    /// Objeto de acceso a datos para la tabla VENTASSEMANALES
    /// </summary>
    public class VentasSemanalesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla VENTASSEMANALES
        /// </summary>
        public VentasSemanalesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla VENTASSEMANALES de la base de datos
        /// </summary>
        /// <param name="pVentasSemanales">Entidad VentasSemanales</param>
        /// <returns>Entidad VentasSemanales creada</returns>
        public VentasSemanales CrearVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = 0;
                        pCOD_VENTAS.Direction = ParameterDirection.InputOutput;

                        DbParameter pTIPOVENTAS = cmdTransaccionFactory.CreateParameter();
                        pTIPOVENTAS.ParameterName = "p_TIPOVENTAS";
                        pTIPOVENTAS.Value = pVentasSemanales.tipoventas;
                        
                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pVentasSemanales.valor;

                        DbParameter pLUNES = cmdTransaccionFactory.CreateParameter();
                        pLUNES.ParameterName = "p_LUNES";
                        pLUNES.Value = pVentasSemanales.lunes;

                        DbParameter pMARTES = cmdTransaccionFactory.CreateParameter();
                        pMARTES.ParameterName = "p_MARTES";
                        pMARTES.Value = pVentasSemanales.martes;

                        DbParameter pMIERCOLES = cmdTransaccionFactory.CreateParameter();
                        pMIERCOLES.ParameterName = "p_MIERCOLES";
                        pMIERCOLES.Value = pVentasSemanales.miercoles;

                        DbParameter pJUEVES = cmdTransaccionFactory.CreateParameter();
                        pJUEVES.ParameterName = "p_JUEVES";
                        pJUEVES.Value = pVentasSemanales.jueves;

                        DbParameter pVIERNES = cmdTransaccionFactory.CreateParameter();
                        pVIERNES.ParameterName = "p_VIERNES";
                        pVIERNES.Value = pVentasSemanales.viernes;

                        DbParameter pSABADOS = cmdTransaccionFactory.CreateParameter();
                        pSABADOS.ParameterName = "p_SABADOS";
                        pSABADOS.Value = pVentasSemanales.sabados;

                        DbParameter pDOMINGO = cmdTransaccionFactory.CreateParameter();
                        pDOMINGO.ParameterName = "p_DOMINGO";
                        pDOMINGO.Value = pVentasSemanales.domingo;

                        DbParameter pTOTAL = cmdTransaccionFactory.CreateParameter();
                        pTOTAL.ParameterName = "p_TOTAL";
                        pTOTAL.Value = pVentasSemanales.total;

                        DbParameter pPORCONTADO = cmdTransaccionFactory.CreateParameter();
                        pPORCONTADO.ParameterName = "p_PORCONTADO";
                        pPORCONTADO.Value = pVentasSemanales.porContado;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pVentasSemanales.codPersona;


                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVENTAS);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pLUNES);
                        cmdTransaccionFactory.Parameters.Add(pMARTES);
                        cmdTransaccionFactory.Parameters.Add(pMIERCOLES);
                        cmdTransaccionFactory.Parameters.Add(pJUEVES);
                        cmdTransaccionFactory.Parameters.Add(pVIERNES);
                        cmdTransaccionFactory.Parameters.Add(pSABADOS);
                        cmdTransaccionFactory.Parameters.Add(pDOMINGO);
                        cmdTransaccionFactory.Parameters.Add(pTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pPORCONTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENSE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVentasSemanales, pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pVentasSemanales.cod_ventas = Convert.ToInt64(pCOD_VENTAS.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pVentasSemanales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VentasSemanalesData", "CrearVentasSemanales", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla VENTASSEMANALES de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad VentasSemanales modificada</returns>
        public VentasSemanales ModificarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pVentasSemanales.cod_ventas;

                        DbParameter pTIPOVENTAS = cmdTransaccionFactory.CreateParameter();
                        pTIPOVENTAS.ParameterName = "p_TIPOVENTAS";
                        pTIPOVENTAS.Value = pVentasSemanales.tipoventas;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pVentasSemanales.valor;

                        DbParameter pLUNES = cmdTransaccionFactory.CreateParameter();
                        pLUNES.ParameterName = "p_LUNES";
                        pLUNES.Value = pVentasSemanales.lunes;

                        DbParameter pMARTES = cmdTransaccionFactory.CreateParameter();
                        pMARTES.ParameterName = "p_MARTES";
                        pMARTES.Value = pVentasSemanales.martes;

                        DbParameter pMIERCOLES = cmdTransaccionFactory.CreateParameter();
                        pMIERCOLES.ParameterName = "p_MIERCOLES";
                        pMIERCOLES.Value = pVentasSemanales.miercoles;

                        DbParameter pJUEVES = cmdTransaccionFactory.CreateParameter();
                        pJUEVES.ParameterName = "p_JUEVES";
                        pJUEVES.Value = pVentasSemanales.jueves;

                        DbParameter pVIERNES = cmdTransaccionFactory.CreateParameter();
                        pVIERNES.ParameterName = "p_VIERNES";
                        pVIERNES.Value = pVentasSemanales.viernes;

                        DbParameter pSABADOS = cmdTransaccionFactory.CreateParameter();
                        pSABADOS.ParameterName = "p_SABADOS";
                        pSABADOS.Value = pVentasSemanales.sabados;

                        DbParameter pDOMINGO = cmdTransaccionFactory.CreateParameter();
                        pDOMINGO.ParameterName = "p_DOMINGO";
                        pDOMINGO.Value = pVentasSemanales.domingo;

                        DbParameter pTOTAL = cmdTransaccionFactory.CreateParameter();
                        pTOTAL.ParameterName = "p_TOTAL";
                        pTOTAL.Value = pVentasSemanales.total;

                        DbParameter pPORCONTADO = cmdTransaccionFactory.CreateParameter();
                        pPORCONTADO.ParameterName = "p_PORCONTADO";
                        pPORCONTADO.Value = pVentasSemanales.porContado;

                        DbParameter pCODPERSONA = cmdTransaccionFactory.CreateParameter();
                        pCODPERSONA.ParameterName = "p_CODPERSONA";
                        pCODPERSONA.Value = pVentasSemanales.codPersona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);
                        cmdTransaccionFactory.Parameters.Add(pTIPOVENTAS);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);
                        cmdTransaccionFactory.Parameters.Add(pLUNES);
                        cmdTransaccionFactory.Parameters.Add(pMARTES);
                        cmdTransaccionFactory.Parameters.Add(pMIERCOLES);
                        cmdTransaccionFactory.Parameters.Add(pJUEVES);
                        cmdTransaccionFactory.Parameters.Add(pVIERNES);
                        cmdTransaccionFactory.Parameters.Add(pSABADOS);
                        cmdTransaccionFactory.Parameters.Add(pDOMINGO);
                        cmdTransaccionFactory.Parameters.Add(pTOTAL);
                        cmdTransaccionFactory.Parameters.Add(pPORCONTADO);
                        cmdTransaccionFactory.Parameters.Add(pCODPERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENSE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVentasSemanales, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pVentasSemanales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VentasSemanalesData", "ModificarVentasSemanales", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla VENTASSEMANALES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de VENTASSEMANALES</param>
        public void EliminarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        VentasSemanales pVentasSemanales = new VentasSemanales();

                        //if (pUsuario.programaGeneraLog)
                        //    pVentasSemanales = ConsultarVentasSemanales(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_VENTAS = cmdTransaccionFactory.CreateParameter();
                        pCOD_VENTAS.ParameterName = "p_COD_VENTAS";
                        pCOD_VENTAS.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VENTAS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VENSE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVentasSemanales, pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VentasSemanalesData", "InsertarVentasSemanales", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla VENTASSEMANALES de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla VENTASSEMANALES</param>
        /// <returns>Entidad VentasSemanales consultado</returns>
        public VentasSemanales ConsultarVentasSemanales(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            VentasSemanales entidad = new VentasSemanales();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VENTASSEMANALES WHERE COD_VENTAS = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventas = Convert.ToString(resultado["TIPOVENTAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["LUNES"] != DBNull.Value) entidad.lunes = Convert.ToInt64(resultado["LUNES"]);
                            if (resultado["MARTES"] != DBNull.Value) entidad.martes = Convert.ToInt64(resultado["MARTES"]);
                            if (resultado["MIERCOLES"] != DBNull.Value) entidad.miercoles = Convert.ToInt64(resultado["MIERCOLES"]);
                            if (resultado["JUEVES"] != DBNull.Value) entidad.jueves = Convert.ToInt64(resultado["JUEVES"]);
                            if (resultado["VIERNES"] != DBNull.Value) entidad.viernes = Convert.ToInt64(resultado["VIERNES"]);
                            if (resultado["SABADOS"] != DBNull.Value) entidad.sabados = Convert.ToInt64(resultado["SABADOS"]);
                            if (resultado["DOMINGO"] != DBNull.Value) entidad.domingo = Convert.ToInt64(resultado["DOMINGO"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["TOTAL"]);
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
                        BOExcepcion.Throw("VentasSemanalesData", "ConsultarVentasSemanales", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla VENTASSEMANALES dados unos filtros
        /// </summary>
        /// <param name="pVENTASSEMANALES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de VentasSemanales obtenidos</returns>
        public List<VentasSemanales> ListarVentasSemanales(VentasSemanales pVentasSemanales, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<VentasSemanales> lstVentasSemanales = new List<VentasSemanales>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * From  VENTASSEMANALES Where codpersona = " + pVentasSemanales.codPersona.ToString() + " Order By COD_VENTAS, TIPOVENTAS";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            VentasSemanales entidad = new VentasSemanales();

                            if (resultado["COD_VENTAS"] != DBNull.Value) entidad.cod_ventas = Convert.ToInt64(resultado["COD_VENTAS"]);
                            if (resultado["TIPOVENTAS"] != DBNull.Value) entidad.tipoventas = Convert.ToString(resultado["TIPOVENTAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["LUNES"] != DBNull.Value) entidad.lunes = Convert.ToInt64(resultado["LUNES"]);
                            if (resultado["MARTES"] != DBNull.Value) entidad.martes = Convert.ToInt64(resultado["MARTES"]);
                            if (resultado["MIERCOLES"] != DBNull.Value) entidad.miercoles = Convert.ToInt64(resultado["MIERCOLES"]);
                            if (resultado["JUEVES"] != DBNull.Value) entidad.jueves = Convert.ToInt64(resultado["JUEVES"]);
                            if (resultado["VIERNES"] != DBNull.Value) entidad.viernes = Convert.ToInt64(resultado["VIERNES"]);
                            if (resultado["SABADOS"] != DBNull.Value) entidad.sabados = Convert.ToInt64(resultado["SABADOS"]);
                            if (resultado["DOMINGO"] != DBNull.Value) entidad.domingo = Convert.ToInt64(resultado["DOMINGO"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total = Convert.ToInt64(resultado["TOTAL"]);
                            if (resultado["PORCONTADO"] != DBNull.Value) entidad.porContado = Convert.ToInt64(resultado["PORCONTADO"]);

                            switch (entidad.tipoventas)
                            {
                                case "1":
                                    entidad.tipoventastxt = "Buena";
                                    break;
                                case "2":
                                    entidad.tipoventastxt = "Regular";
                                    break;
                                case "3": 
                                    entidad.tipoventastxt = "Mala";
                                    break;                                
                                default:
                                    break;
                            }

                            lstVentasSemanales.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstVentasSemanales;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VentasSemanalesData", "ListarVentasSemanales", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene listas desplegables
        /// </summary>
        /// <param name="pVENTASMENSUALES">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstacionalidadMensual obtenidos</returns>
        public List<VentasSemanales> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<VentasSemanales> lstEstacionalidadMensual = new List<VentasSemanales>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM " + ListaSolicitada.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            VentasSemanales entidad = new VentasSemanales();

                            if (resultado["ID"] != DBNull.Value) entidad.ListaId = Convert.ToInt64(resultado["ID"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstEstacionalidadMensual.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstacionalidadMensual;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VentasSemanalesData", "ListarVentasSemanales", ex);
                        return null;
                    }
                }
            }
        }


    }
}