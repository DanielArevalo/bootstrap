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
    /// Objeto de acceso a datos para la tabla EstadosFinancieros
    /// </summary>
    public class EstadosFinancierosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla EstadosFinancieros
        /// </summary>
        public EstadosFinancierosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla EstadosFinancieros de la base de datos
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros creada</returns>
        public EstadosFinancieros CrearEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pINFFIN = cmdTransaccionFactory.CreateParameter();
                        pINFFIN.ParameterName = "pINFFIN";
                        pINFFIN.Value = pEstadosFinancieros.cod_inffin;

                        cmdTransaccionFactory.Parameters.Add(pINFFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_IFNE1_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "CrearEstadosFinancieros", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Modifica un registro en la tabla EstadosFinancieros de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad EstadosFinancieros modificada</returns>
        public EstadosFinancieros ModificarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pEstadosFinancieros.cod_balance;

                        DbParameter pCOD_INFFIN = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN.ParameterName = "p_COD_INFFIN";
                        pCOD_INFFIN.Value = pEstadosFinancieros.cod_inffin;

                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "p_COD_CUENTA";
                        pCOD_CUENTA.Value = pEstadosFinancieros.cod_cuenta;

                        DbParameter pVALOR = cmdTransaccionFactory.CreateParameter();
                        pVALOR.ParameterName = "p_VALOR";
                        pVALOR.Value = pEstadosFinancieros.valor;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);
                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_IFNEG_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEstadosFinancieros, "EstadosFinancieros", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ModificarEstadosFinancieros", ex);
                        return null;
                    }
                }
            }
        }

        public EstadosFinancieros RecalcularEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_INFFIN1 = cmdTransaccionFactory.CreateParameter();
                        pCOD_INFFIN1.ParameterName = "p_COD_INFFIN1";
                        pCOD_INFFIN1.Value = pEstadosFinancieros.cod_inffin;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = "p_COD_PERSONA";
                        pCOD_PERSONA.Value = pEstadosFinancieros.cod_persona;

                        cmdTransaccionFactory.Parameters.Add(pCOD_INFFIN1);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "RecalcularEstadosFinancieros", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla EstadosFinancieros de la base de datos
        /// </summary>
        /// <param name="pId">identificador de EstadosFinancieros</param>
        public void EliminarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        EstadosFinancieros pEstadosFinancieros = new EstadosFinancieros();

                        DbParameter pCOD_BALANCE = cmdTransaccionFactory.CreateParameter();
                        pCOD_BALANCE.ParameterName = "p_COD_BALANCE";
                        pCOD_BALANCE.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_BALANCE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SOLICRED_IFNEG_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // ACTUALIZA LOS ESTADOS FINANCIEROS
                        DbCommand cmdTransaccionFactory2 = dbConnectionFactory.dbProveedorFactory.CreateCommand();
                        cmdTransaccionFactory2.Connection = connection;
                        cmdTransaccionFactory2.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory2.CommandText = "USP_XPINN_SOLICRED_ESFIN_MODIF";
                        cmdTransaccionFactory2.ExecuteNonQuery();

                        //if (pUsuario.programaGeneraLog)
                        //    DAauditoria.InsertarLog(pEstadosFinancieros, "EstadosFinancieros", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "EliminarEstadosFinancieros", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla EstadosFinancieros de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla EstadosFinancieros</param>
        /// <returns>Entidad EstadosFinancieros consultado</returns>
        public EstadosFinancieros ConsultarEstadosFinancieros(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            EstadosFinancieros entidad = new EstadosFinancieros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  ESTADOSFINANCIEROS WHERE COD_BALANCE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
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
                        BOExcepcion.Throw("EstadosFinancierosData", "ConsultarEstadosFinancieros", ex);
                        return null;
                    }
                }
            }
        }

        public EstadosFinancieros listarperosnainfofin(long cod, Usuario pUsuario)
        {
            EstadosFinancieros entidad = new EstadosFinancieros();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        int conteo = 0;
                        string sql;
                        sql = "select count(*) as conteo from Informacion_Ingre_Egre where Cod_Persona=" + cod;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {

                            if (resultado["conteo"] != DBNull.Value) conteo = Convert.ToInt32(resultado["conteo"]);

                        }
                        connection.Close();

                        if (conteo <= 1)
                        {
                            sql = "select * from Informacion_Ingre_Egre where Cod_Persona=" + cod;

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            if (resultado.Read())
                            {
                                if (resultado["Sueldo_Persona"] != DBNull.Value) entidad.sueldo = Convert.ToInt64(resultado["Sueldo_Persona"]);
                                if (resultado["Sueldo_Conyuge"] != DBNull.Value) entidad.sueldoconyuge = Convert.ToInt64(resultado["Sueldo_Conyuge"]);
                                if (resultado["Honorarios"] != DBNull.Value) entidad.honorarios = Convert.ToInt64(resultado["Honorarios"]);
                                if (resultado["Honorarios_Conyuge"] != DBNull.Value) entidad.honorariosconyuge = Convert.ToInt64(resultado["Honorarios_Conyuge"]);
                                if (resultado["Otros_Ingresos"] != DBNull.Value) entidad.otrosingresos = Convert.ToInt64(resultado["Otros_Ingresos"]);
                                if (resultado["Otros_Ingresos_Conyuge"] != DBNull.Value) entidad.otrosingresosconyuge = Convert.ToInt64(resultado["Otros_Ingresos_Conyuge"]);
                                if (resultado["Total_Ingresos"] != DBNull.Value) entidad.totalingreso = Convert.ToInt64(resultado["Total_Ingresos"]);
                                if (resultado["Total_Ingresos_Conyuge"] != DBNull.Value) entidad.totalingresoconyuge = Convert.ToInt64(resultado["Total_Ingresos_Conyuge"]);
                                if (resultado["Hipoteca"] != DBNull.Value) entidad.hipoteca = Convert.ToInt64(resultado["Hipoteca"]);
                                if (resultado["Hipoteca_Conyuge"] != DBNull.Value) entidad.hipotecaconyuge = Convert.ToInt64(resultado["Hipoteca_Conyuge"]);
                                if (resultado["Trargetacred"] != DBNull.Value) entidad.targeta_credito = Convert.ToInt64(resultado["Trargetacred"]);
                                if (resultado["Trargetacred_Conyuge"] != DBNull.Value) entidad.targeta_creditoconyuge = Convert.ToInt64(resultado["Trargetacred_Conyuge"]);
                                if (resultado["Otros_Prestamos"] != DBNull.Value) entidad.otrosprestamos = Convert.ToInt64(resultado["Otros_Prestamos"]);
                                if (resultado["Otros_Prestamos_Conyuge"] != DBNull.Value) entidad.otrosprestamosconyuge = Convert.ToInt64(resultado["Otros_Prestamos_Conyuge"]);
                                if (resultado["Gastos_Familiares"] != DBNull.Value) entidad.gastofamiliar = Convert.ToInt64(resultado["Gastos_Familiares"]);
                                if (resultado["Gastos_Familiares_Conyuge"] != DBNull.Value) entidad.gastofamiliarconyuge = Convert.ToInt64(resultado["Gastos_Familiares_Conyuge"]);
                                if (resultado["Descuentos_Nomina"] != DBNull.Value) entidad.decunomina = Convert.ToInt64(resultado["Descuentos_Nomina"]);
                                if (resultado["Descuentos_Nomina_Conyuge"] != DBNull.Value) entidad.decunominaconyuge = Convert.ToInt64(resultado["Descuentos_Nomina_Conyuge"]);
                                if (resultado["Total_Egresos"] != DBNull.Value) entidad.totalegresos = Convert.ToInt64(resultado["Total_Egresos"]);
                                if (resultado["Total_Egresos_Conyuge"] != DBNull.Value) entidad.totalegresosconyuge = Convert.ToInt64(resultado["Total_Egresos_Conyuge"]);
                                if (resultado["ARRENDAMIENTO"] != DBNull.Value) entidad.arrendamientos = Convert.ToDecimal(resultado["ARRENDAMIENTO"]);
                                if (resultado["ARRENDAMIENTO_CONYUGUE"] != DBNull.Value) entidad.arrendamientosconyuge = Convert.ToDecimal(resultado["ARRENDAMIENTO_CONYUGUE"]);
                                if (resultado["CONCEPTO_OTROS"] != DBNull.Value) entidad.conceptootros = resultado["CONCEPTO_OTROS"].ToString();
                                if (resultado["CONCEPTO_OTROS_CONYUGE"] != DBNull.Value) entidad.conceptootrosconyuge = resultado["CONCEPTO_OTROS_CONYUGE"].ToString();
                                //Agregado para consultar información de activos, pasivos y patrimonio
                                if (resultado["TOTAL_ACTIVOS"] != DBNull.Value) entidad.TotAct = Convert.ToInt64(resultado["TOTAL_ACTIVOS"]);
                                if (resultado["TOTAL_ACTIVOS_CONYUGE"] != DBNull.Value) entidad.TotActConyuge = Convert.ToInt64(resultado["TOTAL_ACTIVOS_CONYUGE"]);
                                if (resultado["TOTAL_PASIVOS"] != DBNull.Value) entidad.TotPas = Convert.ToInt64(resultado["TOTAL_PASIVOS"]);
                                if (resultado["TOTAL_PASIVOS_CONYUGE"] != DBNull.Value) entidad.TotPasConyuge = Convert.ToInt64(resultado["TOTAL_PASIVOS_CONYUGE"]);
                                if (resultado["TOTAL_PATRIMONIO"] != DBNull.Value) entidad.TotPat = Convert.ToInt64(resultado["TOTAL_PATRIMONIO"]);
                                if (resultado["TOTAL_PATRIMONIO_CONYUGE"] != DBNull.Value) entidad.TotPatConyuge = Convert.ToInt64(resultado["TOTAL_PATRIMONIO_CONYUGE"]);
                            }
                            dbConnectionFactory.CerrarConexion(connection);
                            return entidad;
                        }
                        else
                        {
                            entidad.sueldo = 0;
                            entidad.sueldoconyuge = 0;
                            entidad.honorarios = 0;
                            entidad.honorariosconyuge = 0;
                            entidad.otrosingresos = 0;
                            entidad.otrosingresosconyuge = 0;
                            entidad.totalingreso = 0;
                            entidad.totalingresoconyuge = 0;
                            entidad.hipoteca = 0;
                            entidad.hipotecaconyuge = 0;
                            entidad.targeta_credito = 0;
                            entidad.targeta_creditoconyuge = 0;
                            entidad.otrosprestamos = 0;
                            entidad.otrosprestamosconyuge = 0;
                            entidad.gastofamiliar = 0;
                            entidad.gastofamiliarconyuge = 0;
                            entidad.decunomina = 0;
                            entidad.decunominaconyuge = 0;
                            entidad.totalegresos = 0;
                            entidad.totalegresosconyuge = 0;

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ConsultarEstadosFinancieros", ex);
                        return null;
                    }
                }
            }
        }


        public void guardarIngreEgre(EstadosFinancieros infromacionFinanciera, Usuario pUsuario)
        {
            EstadosFinancieros entidad = new EstadosFinancieros();
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        int conteo = 0;

                        string sql = "select count(*) as conteo from Informacion_Ingre_Egre where Cod_Persona=" + infromacionFinanciera.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["conteo"] != DBNull.Value) conteo = Convert.ToInt32(resultado["conteo"]);
                        }
                        connection.Close();

                        if (conteo < 1)
                        {

                            EstadosFinancieros pEstadosFinancieros = new EstadosFinancieros();

                            DbParameter P_Cod_Persona = cmdTransaccionFactory.CreateParameter();
                            P_Cod_Persona.ParameterName = "P_Cod_Persona";
                            P_Cod_Persona.Value = infromacionFinanciera.cod_persona;

                            DbParameter p_Cod_Conyuge = cmdTransaccionFactory.CreateParameter();
                            p_Cod_Conyuge.ParameterName = "p_Cod_Conyuge";
                            p_Cod_Conyuge.Value = infromacionFinanciera.cod_personaconyuge;

                            DbParameter P_Sueldo_Persona = cmdTransaccionFactory.CreateParameter();
                            P_Sueldo_Persona.ParameterName = "P_Sueldo_Persona";
                            P_Sueldo_Persona.Value = infromacionFinanciera.sueldo;

                            DbParameter P_Sueldo_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Sueldo_Conyuge.ParameterName = "P_Sueldo_Conyuge";
                            P_Sueldo_Conyuge.Value = infromacionFinanciera.sueldoconyuge;

                            DbParameter p_Honorarios = cmdTransaccionFactory.CreateParameter();
                            p_Honorarios.ParameterName = "p_Honorarios";
                            p_Honorarios.Value = infromacionFinanciera.honorarios;

                            DbParameter P_Honorarios_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Honorarios_Conyuge.ParameterName = "P_Honorarios_Conyuge";
                            P_Honorarios_Conyuge.Value = infromacionFinanciera.honorariosconyuge;

                            DbParameter P_Otros_Ingresos = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Ingresos.ParameterName = "P_Otros_Ingresos";
                            P_Otros_Ingresos.Value = infromacionFinanciera.otrosingresos;

                            DbParameter P_Otros_Ingresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Ingresos_Conyuge.ParameterName = "P_Otros_Ingresos_Conyuge";
                            P_Otros_Ingresos_Conyuge.Value = infromacionFinanciera.otrosingresosconyuge;

                            DbParameter P_Total_Ingresos = cmdTransaccionFactory.CreateParameter();
                            P_Total_Ingresos.ParameterName = "P_Total_Ingresos";
                            P_Total_Ingresos.Value = infromacionFinanciera.totalingreso;

                            DbParameter P_Total_Ingresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Total_Ingresos_Conyuge.ParameterName = "P_Total_Ingresos_Conyuge";
                            P_Total_Ingresos_Conyuge.Value = infromacionFinanciera.totalingresoconyuge;

                            DbParameter P_Hipoteca = cmdTransaccionFactory.CreateParameter();
                            P_Hipoteca.ParameterName = "P_Hipoteca";
                            P_Hipoteca.Value = infromacionFinanciera.hipoteca;

                            DbParameter P_Hipoteca_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Hipoteca_Conyuge.ParameterName = "P_Hipoteca_Conyuge";
                            P_Hipoteca_Conyuge.Value = infromacionFinanciera.hipotecaconyuge;

                            DbParameter P_Trargetacred = cmdTransaccionFactory.CreateParameter();
                            P_Trargetacred.ParameterName = "P_Trargetacred";
                            P_Trargetacred.Value = infromacionFinanciera.targeta_credito;

                            DbParameter P_Trargetacred_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Trargetacred_Conyuge.ParameterName = "P_Trargetacred_Conyuge";
                            P_Trargetacred_Conyuge.Value = infromacionFinanciera.targeta_creditoconyuge;

                            DbParameter P_Otros_Prestamos = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Prestamos.ParameterName = "P_Otros_Prestamos";
                            P_Otros_Prestamos.Value = infromacionFinanciera.otrosprestamos;

                            DbParameter P_Otros_Prestamos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Prestamos_Conyuge.ParameterName = "P_Otros_Prestamos_Conyuge";
                            P_Otros_Prestamos_Conyuge.Value = infromacionFinanciera.otrosprestamosconyuge;

                            DbParameter P_Gastos_Familiares = cmdTransaccionFactory.CreateParameter();
                            P_Gastos_Familiares.ParameterName = "P_Gastos_Familiares";
                            P_Gastos_Familiares.Value = infromacionFinanciera.gastofamiliar;

                            DbParameter P_Gastos_Familiares_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Gastos_Familiares_Conyuge.ParameterName = "P_Gastos_Familiares_Conyuge";
                            P_Gastos_Familiares_Conyuge.Value = infromacionFinanciera.gastofamiliarconyuge;

                            DbParameter P_Descuentos_Nomina = cmdTransaccionFactory.CreateParameter();
                            P_Descuentos_Nomina.ParameterName = "P_Descuentos_Nomina";
                            P_Descuentos_Nomina.Value = infromacionFinanciera.decunomina;

                            DbParameter P_Descuentos_Nomina_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Descuentos_Nomina_Conyuge.ParameterName = "P_Descuentos_Nomina_Conyuge";
                            P_Descuentos_Nomina_Conyuge.Value = infromacionFinanciera.decunominaconyuge;

                            DbParameter P_Total_Egresos = cmdTransaccionFactory.CreateParameter();
                            P_Total_Egresos.ParameterName = "P_Total_Egresos";
                            P_Total_Egresos.Value = infromacionFinanciera.totalegresos;

                            DbParameter p_Total_Egresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            p_Total_Egresos_Conyuge.ParameterName = "p_Total_Egresos_Conyuge";
                            p_Total_Egresos_Conyuge.Value = infromacionFinanciera.totalegresosconyuge;

                            DbParameter P_ARRENDAMIENTO = cmdTransaccionFactory.CreateParameter();
                            P_ARRENDAMIENTO.ParameterName = "P_ARRENDAMIENTO";
                            P_ARRENDAMIENTO.Value = infromacionFinanciera.arrendamientos;

                            DbParameter P_ARRENDAMIENTO_CONYUGUE = cmdTransaccionFactory.CreateParameter();
                            P_ARRENDAMIENTO_CONYUGUE.ParameterName = "P_ARRENDAMIENTO_CONYUGUE";
                            P_ARRENDAMIENTO_CONYUGUE.Value = infromacionFinanciera.arrendamientosconyuge;

                            DbParameter p_concepto_otros = cmdTransaccionFactory.CreateParameter();
                            p_concepto_otros.ParameterName = "P_CONCEPTO_OTROS";
                            p_concepto_otros.Value = infromacionFinanciera.conceptootros;

                            DbParameter p_concepto_otros_conyuge = cmdTransaccionFactory.CreateParameter();
                            p_concepto_otros_conyuge.ParameterName = "P_CONCEPTO_OTROS_CONYUGE";
                            p_concepto_otros_conyuge.Value = infromacionFinanciera.conceptootrosconyuge;

                            //Agregado para información de activos, pasivos y patrimonio

                            DbParameter P_Activos = cmdTransaccionFactory.CreateParameter();
                            P_Activos.ParameterName = "P_ACTIVOS";
                            P_Activos.Value = infromacionFinanciera.TotAct;

                            DbParameter P_Activos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Activos_Conyuge.ParameterName = "P_ACTIVOS_CONYUGE";
                            P_Activos_Conyuge.Value = infromacionFinanciera.TotActConyuge;

                            DbParameter P_Pasivos = cmdTransaccionFactory.CreateParameter();
                            P_Pasivos.ParameterName = "P_PASIVOS";
                            P_Pasivos.Value = infromacionFinanciera.TotPas;

                            DbParameter P_Pasivos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Pasivos_Conyuge.ParameterName = "P_PASIVOS_CONYUGE";
                            P_Pasivos_Conyuge.Value = infromacionFinanciera.TotPasConyuge;

                            DbParameter P_Patrimonio = cmdTransaccionFactory.CreateParameter();
                            P_Patrimonio.ParameterName = "P_PATRIMONIO";
                            P_Patrimonio.Value = infromacionFinanciera.TotPat;

                            DbParameter P_Patrimonio_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Patrimonio_Conyuge.ParameterName = "P_PATRIMONIO_CONYUGE";
                            P_Patrimonio_Conyuge.Value = infromacionFinanciera.TotPatConyuge;

                            cmdTransaccionFactory.Parameters.Add(P_Cod_Persona);
                            cmdTransaccionFactory.Parameters.Add(p_Cod_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Sueldo_Persona);
                            cmdTransaccionFactory.Parameters.Add(P_Sueldo_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(p_Honorarios);
                            cmdTransaccionFactory.Parameters.Add(P_Honorarios_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Ingresos);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Ingresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Ingresos);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Ingresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Hipoteca);
                            cmdTransaccionFactory.Parameters.Add(P_Hipoteca_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Trargetacred);
                            cmdTransaccionFactory.Parameters.Add(P_Trargetacred_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Prestamos);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Prestamos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Gastos_Familiares);
                            cmdTransaccionFactory.Parameters.Add(P_Gastos_Familiares_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Descuentos_Nomina);
                            cmdTransaccionFactory.Parameters.Add(P_Descuentos_Nomina_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Egresos);
                            cmdTransaccionFactory.Parameters.Add(p_Total_Egresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_ARRENDAMIENTO);
                            cmdTransaccionFactory.Parameters.Add(P_ARRENDAMIENTO_CONYUGUE);
                            cmdTransaccionFactory.Parameters.Add(p_concepto_otros);
                            cmdTransaccionFactory.Parameters.Add(p_concepto_otros_conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Activos);
                            cmdTransaccionFactory.Parameters.Add(P_Activos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Pasivos);
                            cmdTransaccionFactory.Parameters.Add(P_Pasivos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Patrimonio);
                            cmdTransaccionFactory.Parameters.Add(P_Patrimonio_Conyuge);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ING_EGR_CREAR";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        else
                        {

                            EstadosFinancieros pEstadosFinancieros = new EstadosFinancieros();

                            DbParameter P_Cod_Persona = cmdTransaccionFactory.CreateParameter();
                            P_Cod_Persona.ParameterName = "P_Cod_Persona";
                            P_Cod_Persona.Value = infromacionFinanciera.cod_persona;

                            DbParameter p_Cod_Conyuge = cmdTransaccionFactory.CreateParameter();
                            p_Cod_Conyuge.ParameterName = "p_Cod_Conyuge";
                            p_Cod_Conyuge.Value = infromacionFinanciera.cod_personaconyuge;

                            DbParameter P_Sueldo_Persona = cmdTransaccionFactory.CreateParameter();
                            P_Sueldo_Persona.ParameterName = "P_Sueldo_Persona";
                            P_Sueldo_Persona.Value = infromacionFinanciera.sueldo;

                            DbParameter P_Sueldo_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Sueldo_Conyuge.ParameterName = "P_Sueldo_Conyuge";
                            P_Sueldo_Conyuge.Value = infromacionFinanciera.sueldoconyuge;

                            DbParameter p_Honorarios = cmdTransaccionFactory.CreateParameter();
                            p_Honorarios.ParameterName = "p_Honorarios";
                            p_Honorarios.Value = infromacionFinanciera.honorarios;

                            DbParameter P_Honorarios_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Honorarios_Conyuge.ParameterName = "P_Honorarios_Conyuge";
                            P_Honorarios_Conyuge.Value = infromacionFinanciera.honorariosconyuge;

                            DbParameter P_Otros_Ingresos = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Ingresos.ParameterName = "P_Otros_Ingresos";
                            P_Otros_Ingresos.Value = infromacionFinanciera.otrosingresos;

                            DbParameter P_Otros_Ingresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Ingresos_Conyuge.ParameterName = "P_Otros_Ingresos_Conyuge";
                            P_Otros_Ingresos_Conyuge.Value = infromacionFinanciera.otrosingresosconyuge;

                            DbParameter P_Total_Ingresos = cmdTransaccionFactory.CreateParameter();
                            P_Total_Ingresos.ParameterName = "P_Total_Ingresos";
                            P_Total_Ingresos.Value = infromacionFinanciera.totalingreso;

                            DbParameter P_Total_Ingresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Total_Ingresos_Conyuge.ParameterName = "P_Total_Ingresos_Conyuge";
                            P_Total_Ingresos_Conyuge.Value = infromacionFinanciera.totalingresoconyuge;

                            DbParameter P_Hipoteca = cmdTransaccionFactory.CreateParameter();
                            P_Hipoteca.ParameterName = "P_Hipoteca";
                            P_Hipoteca.Value = infromacionFinanciera.hipoteca;

                            DbParameter P_Hipoteca_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Hipoteca_Conyuge.ParameterName = "P_Hipoteca_Conyuge";
                            P_Hipoteca_Conyuge.Value = infromacionFinanciera.hipotecaconyuge;

                            DbParameter P_Trargetacred = cmdTransaccionFactory.CreateParameter();
                            P_Trargetacred.ParameterName = "P_Trargetacred";
                            P_Trargetacred.Value = infromacionFinanciera.targeta_credito;

                            DbParameter P_Trargetacred_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Trargetacred_Conyuge.ParameterName = "P_Trargetacred_Conyuge";
                            P_Trargetacred_Conyuge.Value = infromacionFinanciera.targeta_creditoconyuge;

                            DbParameter P_Otros_Prestamos = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Prestamos.ParameterName = "P_Otros_Prestamos";
                            P_Otros_Prestamos.Value = infromacionFinanciera.otrosprestamos;

                            DbParameter P_Otros_Prestamos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Otros_Prestamos_Conyuge.ParameterName = "P_Otros_Prestamos_Conyuge";
                            P_Otros_Prestamos_Conyuge.Value = infromacionFinanciera.otrosprestamosconyuge;

                            DbParameter P_Gastos_Familiares = cmdTransaccionFactory.CreateParameter();
                            P_Gastos_Familiares.ParameterName = "P_Gastos_Familiares";
                            P_Gastos_Familiares.Value = infromacionFinanciera.gastofamiliar;

                            DbParameter P_Gastos_Familiares_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Gastos_Familiares_Conyuge.ParameterName = "P_Gastos_Familiares_Conyuge";
                            P_Gastos_Familiares_Conyuge.Value = infromacionFinanciera.gastofamiliarconyuge;

                            DbParameter P_Descuentos_Nomina = cmdTransaccionFactory.CreateParameter();
                            P_Descuentos_Nomina.ParameterName = "P_Descuentos_Nomina";
                            P_Descuentos_Nomina.Value = infromacionFinanciera.decunomina;

                            DbParameter P_Descuentos_Nomina_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Descuentos_Nomina_Conyuge.ParameterName = "P_Descuentos_Nomina_Conyuge";
                            P_Descuentos_Nomina_Conyuge.Value = infromacionFinanciera.decunominaconyuge;

                            DbParameter P_Total_Egresos = cmdTransaccionFactory.CreateParameter();
                            P_Total_Egresos.ParameterName = "P_Total_Egresos";
                            P_Total_Egresos.Value = infromacionFinanciera.totalegresos;

                            DbParameter p_Total_Egresos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            p_Total_Egresos_Conyuge.ParameterName = "p_Total_Egresos_Conyuge";
                            p_Total_Egresos_Conyuge.Value = infromacionFinanciera.totalegresosconyuge;

                            DbParameter P_ARRENDAMIENTO = cmdTransaccionFactory.CreateParameter();
                            P_ARRENDAMIENTO.ParameterName = "P_ARRENDAMIENTO";
                            if (infromacionFinanciera.arrendamientos == null)
                                P_ARRENDAMIENTO.Value = DBNull.Value;
                            else
                                P_ARRENDAMIENTO.Value = infromacionFinanciera.arrendamientos;

                            DbParameter P_ARRENDAMIENTO_CONYUGUE = cmdTransaccionFactory.CreateParameter();
                            P_ARRENDAMIENTO_CONYUGUE.ParameterName = "P_ARRENDAMIENTO_CONYUGUE";
                            if (infromacionFinanciera.arrendamientosconyuge == null)
                                P_ARRENDAMIENTO_CONYUGUE.Value = DBNull.Value;
                            else
                                P_ARRENDAMIENTO_CONYUGUE.Value = infromacionFinanciera.arrendamientosconyuge;

                            DbParameter p_concepto_otros = cmdTransaccionFactory.CreateParameter();
                            p_concepto_otros.ParameterName = "P_CONCEPTO_OTROS";
                            p_concepto_otros.Value = infromacionFinanciera.conceptootros;

                            DbParameter p_concepto_otros_conyuge = cmdTransaccionFactory.CreateParameter();
                            p_concepto_otros_conyuge.ParameterName = "P_CONCEPTO_OTROS_CONYUGE";
                            p_concepto_otros_conyuge.Value = infromacionFinanciera.conceptootrosconyuge;

                            //Agregado para información de activos, pasivos y patrimonio

                            DbParameter P_Activos = cmdTransaccionFactory.CreateParameter();
                            P_Activos.ParameterName = "P_ACTIVOS";
                            P_Activos.Value = infromacionFinanciera.TotAct;

                            DbParameter P_Activos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Activos_Conyuge.ParameterName = "P_ACTIVOS_CONYUGE";
                            P_Activos_Conyuge.Value = infromacionFinanciera.TotActConyuge;

                            DbParameter P_Pasivos = cmdTransaccionFactory.CreateParameter();
                            P_Pasivos.ParameterName = "P_PASIVOS";
                            P_Pasivos.Value = infromacionFinanciera.TotPas;

                            DbParameter P_Pasivos_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Pasivos_Conyuge.ParameterName = "P_PASIVOS_CONYUGE";
                            P_Pasivos_Conyuge.Value = infromacionFinanciera.TotPasConyuge;

                            DbParameter P_Patrimonio = cmdTransaccionFactory.CreateParameter();
                            P_Patrimonio.ParameterName = "P_PATRIMONIO";
                            P_Patrimonio.Value = infromacionFinanciera.TotPat;

                            DbParameter P_Patrimonio_Conyuge = cmdTransaccionFactory.CreateParameter();
                            P_Patrimonio_Conyuge.ParameterName = "P_PATRIMONIO_CONYUGE";
                            P_Patrimonio_Conyuge.Value = infromacionFinanciera.TotPatConyuge;

                            cmdTransaccionFactory.Parameters.Add(P_Cod_Persona);
                            cmdTransaccionFactory.Parameters.Add(p_Cod_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Sueldo_Persona);
                            cmdTransaccionFactory.Parameters.Add(P_Sueldo_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(p_Honorarios);
                            cmdTransaccionFactory.Parameters.Add(P_Honorarios_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Ingresos);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Ingresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Ingresos);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Ingresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Hipoteca);
                            cmdTransaccionFactory.Parameters.Add(P_Hipoteca_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Trargetacred);
                            cmdTransaccionFactory.Parameters.Add(P_Trargetacred_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Prestamos);
                            cmdTransaccionFactory.Parameters.Add(P_Otros_Prestamos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Gastos_Familiares);
                            cmdTransaccionFactory.Parameters.Add(P_Gastos_Familiares_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Descuentos_Nomina);
                            cmdTransaccionFactory.Parameters.Add(P_Descuentos_Nomina_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Total_Egresos);
                            cmdTransaccionFactory.Parameters.Add(p_Total_Egresos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_ARRENDAMIENTO);
                            cmdTransaccionFactory.Parameters.Add(P_ARRENDAMIENTO_CONYUGUE);
                            cmdTransaccionFactory.Parameters.Add(p_concepto_otros);
                            cmdTransaccionFactory.Parameters.Add(p_concepto_otros_conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Activos);
                            cmdTransaccionFactory.Parameters.Add(P_Activos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Pasivos);
                            cmdTransaccionFactory.Parameters.Add(P_Pasivos_Conyuge);
                            cmdTransaccionFactory.Parameters.Add(P_Patrimonio);
                            cmdTransaccionFactory.Parameters.Add(P_Patrimonio_Conyuge);

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_ING_EGR_MODIF";
                            cmdTransaccionFactory.ExecuteNonQuery();

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "guardarIngreEgre", ex);

                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla EstadosFinancieros dados unos filtros
        /// </summary>
        /// <param name="pEstadosFinancieros">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstadosFinancieros obtenidos</returns>
        public List<EstadosFinancieros> ListarEstadosFinancieros(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DbDataReader resultado2 = default(DbDataReader);
            List<EstadosFinancieros> lstEstadosFinancieros = new List<EstadosFinancieros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = null;
                        if (pEstadosFinancieros.filtro == "UtiNet")
                        {
                            sql = "SELECT * FROM  ESTADOSFINANCIEROS WHERE cod_cuenta = " + pEstadosFinancieros.cod_cuenta + " AND cod_inffin = " + pEstadosFinancieros.cod_inffin + " ORDER BY cod_cuenta";
                        }
                        else
                        {
                            sql = @"SELECT ESTADOSFINANCIEROS.*, ESTADOSFINANCIEROSestructura.descripcion
                                    FROM  ESTADOSFINANCIEROS, ESTADOSFINANCIEROSestructura 
                                    WHERE ESTADOSFINANCIEROS.cod_cuenta = ESTADOSFINANCIEROSestructura.cod_cuenta AND ESTADOSFINANCIEROS.cod_inffin = " + pEstadosFinancieros.cod_inffin +
                                   "AND   ESTADOSFINANCIEROSestructura.tipoinformacion = '" + pEstadosFinancieros.tipoInformacion + "' ORDER BY ESTADOSFINANCIEROSestructura.cod_cuenta";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();


                        while (resultado.Read())
                        {
                            EstadosFinancieros entidad = new EstadosFinancieros();

                            if (resultado["COD_BALANCE"] != DBNull.Value) entidad.cod_balance = Convert.ToInt64(resultado["COD_BALANCE"]);
                            if (resultado["COD_INFFIN"] != DBNull.Value) entidad.cod_inffin = Convert.ToInt64(resultado["COD_INFFIN"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);

                            if (entidad.cod_cuenta == 8)
                            {
                                // Colocar en el campo del valor arriendo el valor digitado en datos del negocio                                                                
                                if (entidad.valor == 0)
                                {
                                    cmdTransaccionFactory.Connection = connection;
                                    cmdTransaccionFactory.CommandType = CommandType.Text;
                                    if (pEstadosFinancieros.cod_persona.ToString() != "")
                                    {
                                        cmdTransaccionFactory.CommandText = "select VRARRIENDO from negocio Where cod_persona = " + pEstadosFinancieros.cod_persona + " Order by cod_negocio desc";
                                        resultado2 = cmdTransaccionFactory.ExecuteReader();
                                        if (resultado2.Read())
                                        {
                                            if (resultado2["VRARRIENDO"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado2["VRARRIENDO"]);
                                        }
                                        else
                                            entidad.valor = 0;
                                    }
                                }
                            }

                            lstEstadosFinancieros.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ListarEstadosFinancieros", ex);
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
        public List<EstadosFinancieros> ListarIngresosEgresosRepo(EstadosFinancieros pInformacionfinanciera, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EstadosFinancieros> lstIngresosEgresos = new List<EstadosFinancieros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Informacion_Ingre_Egre where COD_PERSONA = " + pInformacionfinanciera.cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EstadosFinancieros entidad = new EstadosFinancieros();


                            if (resultado["Sueldo_Persona"] != DBNull.Value) entidad.sueldo = Convert.ToInt64(resultado["Sueldo_Persona"]);
                            if (resultado["Sueldo_Conyuge"] != DBNull.Value) entidad.sueldoconyuge = Convert.ToInt64(resultado["Sueldo_Conyuge"]);
                            if (resultado["Honorarios"] != DBNull.Value) entidad.honorarios = Convert.ToInt64(resultado["Honorarios"]);
                            if (resultado["Honorarios_Conyuge"] != DBNull.Value) entidad.honorariosconyuge = Convert.ToInt64(resultado["Honorarios_Conyuge"]);
                            if (resultado["Otros_Ingresos"] != DBNull.Value) entidad.otrosingresos = Convert.ToInt64(resultado["Otros_Ingresos"]);
                            if (resultado["Otros_Ingresos_Conyuge"] != DBNull.Value) entidad.otrosingresosconyuge = Convert.ToInt64(resultado["Otros_Ingresos_Conyuge"]);
                            if (resultado["Total_Ingresos"] != DBNull.Value) entidad.totalingreso = Convert.ToInt64(resultado["Total_Ingresos"]);
                            if (resultado["Total_Ingresos_Conyuge"] != DBNull.Value) entidad.totalingresoconyuge = Convert.ToInt64(resultado["Total_Ingresos_Conyuge"]);
                            if (resultado["Hipoteca"] != DBNull.Value) entidad.hipoteca = Convert.ToInt64(resultado["Hipoteca"]);
                            if (resultado["Hipoteca_Conyuge"] != DBNull.Value) entidad.hipotecaconyuge = Convert.ToInt64(resultado["Hipoteca_Conyuge"]);
                            if (resultado["Trargetacred"] != DBNull.Value) entidad.targeta_credito = Convert.ToInt64(resultado["Trargetacred"]);
                            if (resultado["Trargetacred_Conyuge"] != DBNull.Value) entidad.targeta_creditoconyuge = Convert.ToInt64(resultado["Trargetacred_Conyuge"]);
                            if (resultado["Otros_Prestamos"] != DBNull.Value) entidad.otrosprestamos = Convert.ToInt64(resultado["Otros_Prestamos"]);
                            if (resultado["Otros_Prestamos_Conyuge"] != DBNull.Value) entidad.otrosprestamosconyuge = Convert.ToInt64(resultado["Otros_Prestamos_Conyuge"]);
                            if (resultado["Gastos_Familiares"] != DBNull.Value) entidad.gastofamiliar = Convert.ToInt64(resultado["Gastos_Familiares"]);
                            if (resultado["Gastos_Familiares_Conyuge"] != DBNull.Value) entidad.gastofamiliarconyuge = Convert.ToInt64(resultado["Gastos_Familiares_Conyuge"]);
                            if (resultado["Descuentos_Nomina"] != DBNull.Value) entidad.decunomina = Convert.ToInt64(resultado["Descuentos_Nomina"]);
                            if (resultado["Descuentos_Nomina_Conyuge"] != DBNull.Value) entidad.decunominaconyuge = Convert.ToInt64(resultado["Descuentos_Nomina_Conyuge"]);
                            if (resultado["Total_Egresos"] != DBNull.Value) entidad.totalegresos = Convert.ToInt64(resultado["Total_Egresos"]);
                            if (resultado["Total_Egresos_Conyuge"] != DBNull.Value) entidad.totalegresosconyuge = Convert.ToInt64(resultado["Total_Egresos_Conyuge"]);

                            lstIngresosEgresos.Add(entidad);
                        }

                        return lstIngresosEgresos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ListarIngresosEgresosRepo", ex);
                        return null;
                    }
                }
            }
        }
        //Agregado par manejo de moneda extranjera
        public EstadosFinancieros CrearMonedaExtranjera(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "P_COD_MONEDA";
                        pcod_moneda.Direction = ParameterDirection.Output;
                        pcod_moneda.DbType = DbType.Int64;
                        pcod_moneda.Value = pEstadosFinancieros.cod_moneda_ext;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "P_COD_PERSONA";
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        pcod_persona.Value = pEstadosFinancieros.cod_persona;

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "P_NUM_CUENTA";
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        if (pEstadosFinancieros.num_cuenta_ext != "" && pEstadosFinancieros.num_cuenta_ext != null)
                            pnum_cuenta.Value = pEstadosFinancieros.num_cuenta_ext;
                        else
                            pnum_cuenta.Value = DBNull.Value;

                        DbParameter pnom_banco = cmdTransaccionFactory.CreateParameter();
                        pnom_banco.ParameterName = "P_BANCO";
                        pnom_banco.Direction = ParameterDirection.Input;
                        pnom_banco.DbType = DbType.String;
                        if (pEstadosFinancieros.banco_ext != null) pnom_banco.Value = pEstadosFinancieros.banco_ext; else pnom_banco.Value = DBNull.Value;

                        DbParameter pnom_pais = cmdTransaccionFactory.CreateParameter();
                        pnom_pais.ParameterName = "P_PAIS";
                        pnom_pais.Direction = ParameterDirection.Input;
                        pnom_pais.DbType = DbType.String;
                        pnom_pais.Value = pEstadosFinancieros.pais;

                        DbParameter pnom_ciudad = cmdTransaccionFactory.CreateParameter();
                        pnom_ciudad.ParameterName = "P_CIUDAD";
                        pnom_ciudad.Direction = ParameterDirection.Input;
                        pnom_ciudad.DbType = DbType.String;
                        pnom_ciudad.Value = pEstadosFinancieros.ciudad;

                        DbParameter pnom_moneda = cmdTransaccionFactory.CreateParameter();
                        pnom_moneda.ParameterName = "P_MONEDA";
                        pnom_moneda.Direction = ParameterDirection.Input;
                        pnom_moneda.DbType = DbType.String;
                        pnom_moneda.Value = pEstadosFinancieros.moneda;

                        DbParameter pdesc_operacion = cmdTransaccionFactory.CreateParameter();
                        pdesc_operacion.ParameterName = "P_OPERACION";
                        pdesc_operacion.Direction = ParameterDirection.Input;
                        pdesc_operacion.DbType = DbType.String;
                        if (pEstadosFinancieros.desc_operacion != null) pdesc_operacion.Value = pEstadosFinancieros.desc_operacion; else pdesc_operacion.Value = DBNull.Value;
                        
                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "P_TIPO_PRODUCTO";
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.String;
                        if (pEstadosFinancieros.tipo_producto != null) ptipo_producto.Value = pEstadosFinancieros.tipo_producto; else ptipo_producto.Value = DBNull.Value;
                        
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pnom_banco);
                        cmdTransaccionFactory.Parameters.Add(pnom_pais);
                        cmdTransaccionFactory.Parameters.Add(pnom_ciudad);
                        cmdTransaccionFactory.Parameters.Add(pnom_moneda);
                        cmdTransaccionFactory.Parameters.Add(pdesc_operacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MONEDA_EXTRAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pcod_moneda.Value != DBNull.Value)
                            pEstadosFinancieros.cod_moneda_ext = Convert.ToInt64(pcod_moneda.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "CrearMonedaExtranjera", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para modificar información de manejo de moneda extranjera
        public EstadosFinancieros ModificarMonedaExtranjera(EstadosFinancieros pEstadosFinancieros, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "P_COD_MONEDA";
                        pcod_moneda.Value = pEstadosFinancieros.cod_moneda_ext;
                        pcod_moneda.Direction = ParameterDirection.Input;

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "P_COD_PERSONA";
                        pcod_persona.Value = pEstadosFinancieros.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "P_NUM_CUENTA";
                        if (pEstadosFinancieros.num_cuenta_ext != "" && pEstadosFinancieros.num_cuenta_ext != null)
                            pnum_cuenta.Value = pEstadosFinancieros.num_cuenta_ext;
                        else
                            pnum_cuenta.Value = DBNull.Value;
                        pnum_cuenta.Direction = ParameterDirection.Input;

                        DbParameter pnom_banco = cmdTransaccionFactory.CreateParameter();
                        pnom_banco.ParameterName = "P_BANCO";
                        if (pEstadosFinancieros.banco_ext != null && pEstadosFinancieros.banco_ext != "") pnom_banco.Value = pEstadosFinancieros.banco_ext; else pnom_banco.Value = DBNull.Value;
                        pnom_banco.Direction = ParameterDirection.Input;

                        DbParameter pnom_pais = cmdTransaccionFactory.CreateParameter();
                        pnom_pais.ParameterName = "P_PAIS";
                        pnom_pais.Value = pEstadosFinancieros.pais;
                        pnom_pais.Direction = ParameterDirection.Input;

                        DbParameter pnom_ciudad = cmdTransaccionFactory.CreateParameter();
                        pnom_ciudad.ParameterName = "P_CIUDAD";
                        pnom_ciudad.Value = pEstadosFinancieros.ciudad;
                        pnom_ciudad.Direction = ParameterDirection.Input;

                        DbParameter pnom_moneda = cmdTransaccionFactory.CreateParameter();
                        pnom_moneda.ParameterName = "P_MONEDA";
                        pnom_moneda.Value = pEstadosFinancieros.moneda;
                        pnom_moneda.Direction = ParameterDirection.Input;

                        DbParameter pdesc_operacion = cmdTransaccionFactory.CreateParameter();
                        pdesc_operacion.ParameterName = "P_OPERACION";
                        if (pEstadosFinancieros.desc_operacion != null && pEstadosFinancieros.desc_operacion != "") pdesc_operacion.Value = pEstadosFinancieros.desc_operacion; else pdesc_operacion.Value = DBNull.Value;
                        pdesc_operacion.Direction = ParameterDirection.Input;

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "P_TIPO_PRODUCTO";
                        if (pEstadosFinancieros.tipo_producto != null && pEstadosFinancieros.tipo_producto != "") ptipo_producto.Value = pEstadosFinancieros.tipo_producto; else ptipo_producto.Value = DBNull.Value;
                        ptipo_producto.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pnom_banco);
                        cmdTransaccionFactory.Parameters.Add(pnom_pais);
                        cmdTransaccionFactory.Parameters.Add(pnom_ciudad);
                        cmdTransaccionFactory.Parameters.Add(pnom_moneda);
                        cmdTransaccionFactory.Parameters.Add(pdesc_operacion);
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MONEDA_EXTRAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEstadosFinancieros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ModificarMonedaExtranjera", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para consultar información de moneda extranjera
        public List<EstadosFinancieros> ListarCuentasMonedaExtranjera(Int64 cod_persona, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<EstadosFinancieros> lstMonedaExtr = new List<EstadosFinancieros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM MONEDA_EXTRANJERA WHERE COD_PERSONA = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            EstadosFinancieros entidad = new EstadosFinancieros();
                            if (resultado["COD_MONEDA_EXT"] != DBNull.Value) entidad.cod_moneda_ext = Convert.ToInt64(resultado["COD_MONEDA_EXT"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta_ext = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["NOM_BANCO"] != DBNull.Value) entidad.banco_ext = Convert.ToString(resultado["NOM_BANCO"]);
                            if (resultado["PAIS"] != DBNull.Value) entidad.pais = Convert.ToString(resultado["PAIS"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["OPERACION"] != DBNull.Value) entidad.desc_operacion = Convert.ToString(resultado["OPERACION"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);

                            lstMonedaExtr.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstMonedaExtr;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "ListarCuentasMonedaExtranjera", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para eliminar información de moneda extranjera
        public void EliminarCuentasMonedaExtranjera(Int64 pCodMoneda, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_moneda_ext = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda_ext.ParameterName = "P_COD_MONEDA";
                        pcod_moneda_ext.Value = pCodMoneda;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda_ext);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MONEDA_EXTRAN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EstadosFinancierosData", "EliminarCuentasMonedaExtranjera", ex);
                    }
                }
            }
        }
    }
}