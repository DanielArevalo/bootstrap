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
    /// Objeto de acceso a datos para la tabla Vehiculos
    /// </summary>
    public class VehiculosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Vehiculos
        /// </summary>
        public VehiculosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Vehiculos de la base de datos
        /// </summary>
        /// <param name="pVehiculos">Entidad Vehiculos</param>
        /// <returns>Entidad Vehiculos creada</returns>
        public Vehiculos CrearVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VEHICULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_VEHICULO.ParameterName = "p_COD_VEHICULO";
                        pCOD_VEHICULO.Value = 0;
                        pCOD_VEHICULO.Direction = ParameterDirection.InputOutput;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pVehiculos.cod_persona;

                        DbParameter pMARCA = cmdTransaccionFactory.CreateParameter();
                        pMARCA.ParameterName = "p_MARCA";
                        pMARCA.Value = pVehiculos.marca;

                        DbParameter pPLACA = cmdTransaccionFactory.CreateParameter();
                        pPLACA.ParameterName = "p_PLACA";
                        pPLACA.Value = pVehiculos.placa;

                        DbParameter pMODELO = cmdTransaccionFactory.CreateParameter();
                        pMODELO.ParameterName = "p_MODELO";
                        pMODELO.Value = pVehiculos.modelo;

                        DbParameter pVALORCOMERCIAL = cmdTransaccionFactory.CreateParameter();
                        pVALORCOMERCIAL.ParameterName = "p_VALORCOMERCIAL";
                        pVALORCOMERCIAL.Value = pVehiculos.valorcomercial;

                        DbParameter pVALORPRENDA = cmdTransaccionFactory.CreateParameter();
                        pVALORPRENDA.ParameterName = "p_VALORPRENDA";
                        pVALORPRENDA.Value = pVehiculos.valorprenda;


                        cmdTransaccionFactory.Parameters.Add(pCOD_VEHICULO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMARCA);
                        cmdTransaccionFactory.Parameters.Add(pPLACA);
                        cmdTransaccionFactory.Parameters.Add(pMODELO);
                        cmdTransaccionFactory.Parameters.Add(pVALORCOMERCIAL);
                        cmdTransaccionFactory.Parameters.Add(pVALORPRENDA);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VEHIC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVehiculos, "Vehiculos",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pVehiculos.cod_vehiculo = Convert.ToInt64(pCOD_VEHICULO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pVehiculos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VehiculosData", "CrearVehiculos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla Vehiculos de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Vehiculos modificada</returns>
        public Vehiculos ModificarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_VEHICULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_VEHICULO.ParameterName = "p_COD_VEHICULO";
                        pCOD_VEHICULO.Value = pVehiculos.cod_vehiculo;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pVehiculos.cod_persona;

                        DbParameter pMARCA = cmdTransaccionFactory.CreateParameter();
                        pMARCA.ParameterName = "p_MARCA";
                        pMARCA.Value = pVehiculos.marca;

                        DbParameter pPLACA = cmdTransaccionFactory.CreateParameter();
                        pPLACA.ParameterName = "p_PLACA";
                        pPLACA.Value = pVehiculos.placa;

                        DbParameter pMODELO = cmdTransaccionFactory.CreateParameter();
                        pMODELO.ParameterName = "p_MODELO";
                        pMODELO.Value = pVehiculos.modelo;

                        DbParameter pVALORCOMERCIAL = cmdTransaccionFactory.CreateParameter();
                        pVALORCOMERCIAL.ParameterName = "p_VALORCOMERCIAL";
                        pVALORCOMERCIAL.Value = pVehiculos.valorcomercial;

                        DbParameter pVALORPRENDA = cmdTransaccionFactory.CreateParameter();
                        pVALORPRENDA.ParameterName = "p_VALORPRENDA";
                        pVALORPRENDA.Value = pVehiculos.valorprenda;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VEHICULO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pMARCA);
                        cmdTransaccionFactory.Parameters.Add(pPLACA);
                        cmdTransaccionFactory.Parameters.Add(pMODELO);
                        cmdTransaccionFactory.Parameters.Add(pVALORCOMERCIAL);
                        cmdTransaccionFactory.Parameters.Add(pVALORPRENDA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VEHIC_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVehiculos, "Vehiculos",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pVehiculos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VehiculosData", "ModificarVehiculos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla Vehiculos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de Vehiculos</param>
        public void EliminarVehiculos(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Vehiculos pVehiculos = new Vehiculos();

                        //if (pUsuario.programaGeneraLog)
                        //    pVehiculos = ConsultarVehiculos(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_VEHICULO = cmdTransaccionFactory.CreateParameter();
                        pCOD_VEHICULO.ParameterName = "p_COD_VEHICULO";
                        pCOD_VEHICULO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_VEHICULO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_VEHIC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pVehiculos, "Vehiculos", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VehiculosData", "EliminarVehiculos", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla Vehiculos de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla Vehiculos</param>
        /// <returns>Entidad Vehiculos consultado</returns>
        public Vehiculos ConsultarVehiculos(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Vehiculos entidad = new Vehiculos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VEHICULOS WHERE COD_VEHICULO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_VEHICULO"] != DBNull.Value) entidad.cod_vehiculo = Convert.ToInt64(resultado["COD_VEHICULO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["PLACA"] != DBNull.Value) entidad.placa = Convert.ToString(resultado["PLACA"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToInt64(resultado["MODELO"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORPRENDA"] != DBNull.Value) entidad.valorprenda = Convert.ToInt64(resultado["VALORPRENDA"]);
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
                        BOExcepcion.Throw("VehiculosData", "ConsultarVehiculos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Vehiculos dados unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculos(Vehiculos pVehiculos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Vehiculos> lstVehiculos = new List<Vehiculos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VEHICULOS " ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Vehiculos entidad = new Vehiculos();

                            if (resultado["COD_VEHICULO"] != DBNull.Value) entidad.cod_vehiculo = Convert.ToInt64(resultado["COD_VEHICULO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["PLACA"] != DBNull.Value) entidad.placa = Convert.ToString(resultado["PLACA"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToInt64(resultado["MODELO"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORPRENDA"] != DBNull.Value) entidad.valorprenda = Convert.ToInt64(resultado["VALORPRENDA"]);

                            lstVehiculos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstVehiculos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VehiculosData", "ListarVehiculos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Vehiculos dados unos filtros
        /// </summary>
        /// <param name="pVehiculos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Vehiculos obtenidos</returns>
        public List<Vehiculos> ListarVehiculosRepo(Vehiculos pVehiculos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Vehiculos> lstVehiculos = new List<Vehiculos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VEHICULOS where COD_PERSONA = " + pVehiculos.cod_persona; 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Vehiculos entidad = new Vehiculos();

                            if (resultado["COD_VEHICULO"] != DBNull.Value) entidad.cod_vehiculo = Convert.ToInt64(resultado["COD_VEHICULO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["MARCA"] != DBNull.Value) entidad.marca = Convert.ToString(resultado["MARCA"]);
                            if (resultado["PLACA"] != DBNull.Value) entidad.placa = Convert.ToString(resultado["PLACA"]);
                            if (resultado["MODELO"] != DBNull.Value) entidad.modelo = Convert.ToInt64(resultado["MODELO"]);
                            if (resultado["VALORCOMERCIAL"] != DBNull.Value) entidad.valorcomercial = Convert.ToInt64(resultado["VALORCOMERCIAL"]);
                            if (resultado["VALORPRENDA"] != DBNull.Value) entidad.valorprenda = Convert.ToInt64(resultado["VALORPRENDA"]);

                            lstVehiculos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstVehiculos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("VehiculosData", "ListarVehiculosRepo", ex);
                        return null;
                    }
                }
            }
        }

    }
}