using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Presupuesto.Entities;

namespace Xpinn.Presupuesto.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PresupuestoS
    /// </summary>
    public class PresupuestoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PresupuestoS
        /// </summary>
        public PresupuestoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla Presupuestos de la base de datos
        /// </summary>
        /// <param name="pPresupuesto">Entidad Presupuesto</param>
        /// <returns>Entidad Presupuesto creada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto CrearPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = ObtenerSiguienteCodigo(vUsuario);
                        pIdPresupuesto.Direction = ParameterDirection.InputOutput;

                        DbParameter pDescripcion = cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_descripcion";
                        pDescripcion.DbType = DbType.String;
                        pDescripcion.Value = pPresupuesto.descripcion;

                        DbParameter pFecha_Elaboracion = cmdTransaccionFactory.CreateParameter();
                        pFecha_Elaboracion.ParameterName = "pFecha_Elaboracion";
                        pFecha_Elaboracion.DbType = DbType.Date;
                        pFecha_Elaboracion.Value = pPresupuesto.fecha_elaboracion;

                        DbParameter pFecha_Aprobacion = cmdTransaccionFactory.CreateParameter();
                        pFecha_Aprobacion.ParameterName = "pFecha_Aprobacion";
                        pFecha_Aprobacion.DbType = DbType.Date;
                        pFecha_Aprobacion.Value = pPresupuesto.fecha_aprobacion;

                        DbParameter pCod_Elaboro = cmdTransaccionFactory.CreateParameter();
                        pCod_Elaboro.ParameterName = "pCod_Elaboro";
                        pCod_Elaboro.DbType = DbType.Int64;
                        pCod_Elaboro.Value = pPresupuesto.cod_elaboro;

                        DbParameter pCod_Aprobo = cmdTransaccionFactory.CreateParameter();
                        pCod_Aprobo.ParameterName = "pCod_Aprobo";
                        pCod_Aprobo.DbType = DbType.Int64;
                        pCod_Aprobo.Value = pPresupuesto.cod_aprobo;

                        DbParameter pTipo_Presupuesto = cmdTransaccionFactory.CreateParameter();
                        pTipo_Presupuesto.ParameterName = "pTipo_Presupuesto";
                        pTipo_Presupuesto.DbType = DbType.Int32;
                        pTipo_Presupuesto.Value = pPresupuesto.tipo_presupuesto;

                        DbParameter pNum_Periodos = cmdTransaccionFactory.CreateParameter();
                        pNum_Periodos.ParameterName = "pNum_Periodos";
                        pNum_Periodos.DbType = DbType.Int32;
                        pNum_Periodos.Value = pPresupuesto.num_periodos;

                        DbParameter pCod_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        pCod_Periodicidad.ParameterName = "pCod_Periodicidad";
                        pCod_Periodicidad.DbType = DbType.Int32;
                        pCod_Periodicidad.Value = pPresupuesto.cod_periodicidad;

                        DbParameter pPeriodo_inicial = cmdTransaccionFactory.CreateParameter();
                        pPeriodo_inicial.ParameterName = "pPeriodo_inicial";
                        pPeriodo_inicial.DbType = DbType.Date;
                        pPeriodo_inicial.Value = pPresupuesto.periodo_inicial;

                        DbParameter pCentro_Costo = cmdTransaccionFactory.CreateParameter();
                        pCentro_Costo.ParameterName = "pCentro_Costo";
                        pCentro_Costo.DbType = DbType.Int32;
                        pCentro_Costo.Value = pPresupuesto.centro_costo;

                        DbParameter pporPolizasVencidas = cmdTransaccionFactory.CreateParameter();
                        pporPolizasVencidas.ParameterName = "pporPolizasVencidas";
                        pporPolizasVencidas.DbType = DbType.Double;
                        pporPolizasVencidas.Value = pPresupuesto.porPolizasVencidas;

                        DbParameter pvalorUnitPoliza = cmdTransaccionFactory.CreateParameter();
                        pvalorUnitPoliza.ParameterName = "pvalorUnitPoliza";
                        pvalorUnitPoliza.DbType = DbType.Double;
                        pvalorUnitPoliza.Value = pPresupuesto.valorUnitPoliza;

                        DbParameter pcomisionPoliza = cmdTransaccionFactory.CreateParameter();
                        pcomisionPoliza.ParameterName = "pcomisionPoliza";
                        pcomisionPoliza.DbType = DbType.Double;
                        pcomisionPoliza.Value = pPresupuesto.comisionPoliza;

                        DbParameter pporLeyMiPyme = cmdTransaccionFactory.CreateParameter();
                        pporLeyMiPyme.ParameterName = "pporLeyMiPyme";
                        pporLeyMiPyme.DbType = DbType.Double;
                        pporLeyMiPyme.Value = pPresupuesto.porLeyMiPyme;

                        DbParameter pporProvision = cmdTransaccionFactory.CreateParameter();
                        pporProvision.ParameterName = "pporProvision";
                        pporProvision.DbType = DbType.Double;
                        pporProvision.Value = pPresupuesto.porProvision;

                        DbParameter pporProvisionGen = cmdTransaccionFactory.CreateParameter();
                        pporProvisionGen.ParameterName = "pporProvisionGen";
                        pporProvisionGen.DbType = DbType.Double;
                        pporProvisionGen.Value = pPresupuesto.porProvisionGen;

                        DbParameter pvalorPromedioCredito = cmdTransaccionFactory.CreateParameter();
                        pvalorPromedioCredito.ParameterName = "pvalorPromedioCredito";
                        pvalorPromedioCredito.DbType = DbType.Double;
                        pvalorPromedioCredito.Value = pPresupuesto.valorPromedioCredito;

                        DbParameter pflujoinicial = cmdTransaccionFactory.CreateParameter();
                        pflujoinicial.ParameterName = "pflujoinicial";
                        pflujoinicial.DbType = DbType.Double;
                        pflujoinicial.Value = pPresupuesto.flujoinicial;

                        DbParameter pfechacorte = cmdTransaccionFactory.CreateParameter();
                        pfechacorte.ParameterName = "pfechacorte";
                        pfechacorte.DbType = DbType.Date;
                        pfechacorte.Value = pPresupuesto.fechacorte;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);
                        cmdTransaccionFactory.Parameters.Add(pFecha_Elaboracion);
                        cmdTransaccionFactory.Parameters.Add(pFecha_Aprobacion);
                        cmdTransaccionFactory.Parameters.Add(pCod_Elaboro);
                        cmdTransaccionFactory.Parameters.Add(pCod_Aprobo);
                        cmdTransaccionFactory.Parameters.Add(pTipo_Presupuesto);
                        cmdTransaccionFactory.Parameters.Add(pNum_Periodos);
                        cmdTransaccionFactory.Parameters.Add(pCod_Periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pPeriodo_inicial);
                        cmdTransaccionFactory.Parameters.Add(pCentro_Costo);

                        cmdTransaccionFactory.Parameters.Add(pporPolizasVencidas);
                        cmdTransaccionFactory.Parameters.Add(pvalorUnitPoliza);
                        cmdTransaccionFactory.Parameters.Add(pcomisionPoliza);
                        cmdTransaccionFactory.Parameters.Add(pporLeyMiPyme);
                        cmdTransaccionFactory.Parameters.Add(pporProvision);
                        cmdTransaccionFactory.Parameters.Add(pporProvisionGen);
                        cmdTransaccionFactory.Parameters.Add(pvalorPromedioCredito);
                        cmdTransaccionFactory.Parameters.Add(pflujoinicial);
                        cmdTransaccionFactory.Parameters.Add(pfechacorte); 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PRESUP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPresupuesto.idpresupuesto = Convert.ToInt64(pIdPresupuesto.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla PresupuestoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Presupuesto modificada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ModificarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = pPresupuesto.idpresupuesto;

                        DbParameter pDescripcion = cmdTransaccionFactory.CreateParameter();
                        pDescripcion.ParameterName = "p_descripcion";
                        pDescripcion.DbType = DbType.String;
                        pDescripcion.Value = pPresupuesto.descripcion;

                        DbParameter pFecha_Elaboracion = cmdTransaccionFactory.CreateParameter();
                        pFecha_Elaboracion.ParameterName = "pFecha_Elaboracion";
                        pFecha_Elaboracion.DbType = DbType.Date;
                        pFecha_Elaboracion.Value = pPresupuesto.fecha_elaboracion;

                        DbParameter pFecha_Aprobacion = cmdTransaccionFactory.CreateParameter();
                        pFecha_Aprobacion.ParameterName = "pFecha_Aprobacion";
                        pFecha_Aprobacion.DbType = DbType.Date;
                        pFecha_Aprobacion.Value = pPresupuesto.fecha_aprobacion;

                        DbParameter pCod_Elaboro = cmdTransaccionFactory.CreateParameter();
                        pCod_Elaboro.ParameterName = "pCod_Elaboro";
                        pCod_Elaboro.DbType = DbType.Int64;
                        pCod_Elaboro.Value = pPresupuesto.cod_elaboro;

                        DbParameter pCod_Aprobo = cmdTransaccionFactory.CreateParameter();
                        pCod_Aprobo.ParameterName = "pCod_Aprobo";
                        pCod_Aprobo.DbType = DbType.Int64;
                        pCod_Aprobo.Value = pPresupuesto.cod_aprobo;

                        DbParameter pTipo_Presupuesto = cmdTransaccionFactory.CreateParameter();
                        pTipo_Presupuesto.ParameterName = "pTipo_Presupuesto";
                        pTipo_Presupuesto.DbType = DbType.Int32;
                        pTipo_Presupuesto.Value = pPresupuesto.tipo_presupuesto;

                        DbParameter pNum_Periodos = cmdTransaccionFactory.CreateParameter();
                        pNum_Periodos.ParameterName = "pNum_Periodos";
                        pNum_Periodos.DbType = DbType.Int32;
                        pNum_Periodos.Value = pPresupuesto.num_periodos;

                        DbParameter pCod_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        pCod_Periodicidad.ParameterName = "pCod_Periodicidad";
                        pCod_Periodicidad.DbType = DbType.Int32;
                        pCod_Periodicidad.Value = pPresupuesto.cod_periodicidad;

                        DbParameter pPeriodo_inicial = cmdTransaccionFactory.CreateParameter();
                        pPeriodo_inicial.ParameterName = "pPeriodo_inicial";
                        pPeriodo_inicial.DbType = DbType.Date;
                        pPeriodo_inicial.Value = pPresupuesto.periodo_inicial;

                        DbParameter pCentro_Costo = cmdTransaccionFactory.CreateParameter();
                        pCentro_Costo.ParameterName = "pCentro_Costo";
                        pCentro_Costo.DbType = DbType.Int32;
                        pCentro_Costo.Value = pPresupuesto.centro_costo;

                        DbParameter pporPolizasVencidas = cmdTransaccionFactory.CreateParameter();
                        pporPolizasVencidas.ParameterName = "pporPolizasVencidas";
                        pporPolizasVencidas.DbType = DbType.Double;
                        pporPolizasVencidas.Value = pPresupuesto.porPolizasVencidas;

                        DbParameter pvalorUnitPoliza = cmdTransaccionFactory.CreateParameter();
                        pvalorUnitPoliza.ParameterName = "pvalorUnitPoliza";
                        pvalorUnitPoliza.DbType = DbType.Double;
                        pvalorUnitPoliza.Value = pPresupuesto.valorUnitPoliza;

                        DbParameter pcomisionPoliza = cmdTransaccionFactory.CreateParameter();
                        pcomisionPoliza.ParameterName = "pcomisionPoliza";
                        pcomisionPoliza.DbType = DbType.Double;
                        pcomisionPoliza.Value = pPresupuesto.comisionPoliza;

                        DbParameter pporLeyMiPyme = cmdTransaccionFactory.CreateParameter();
                        pporLeyMiPyme.ParameterName = "pporLeyMiPyme";
                        pporLeyMiPyme.DbType = DbType.Double;
                        pporLeyMiPyme.Value = pPresupuesto.porLeyMiPyme;

                        DbParameter pporProvision = cmdTransaccionFactory.CreateParameter();
                        pporProvision.ParameterName = "pporProvision";
                        pporProvision.DbType = DbType.Double;
                        pporProvision.Value = pPresupuesto.porProvision;

                        DbParameter pporProvisionGen = cmdTransaccionFactory.CreateParameter();
                        pporProvisionGen.ParameterName = "pporProvisionGen";
                        pporProvisionGen.DbType = DbType.Double;
                        pporProvisionGen.Value = pPresupuesto.porProvisionGen;

                        DbParameter pvalorPromedioCredito = cmdTransaccionFactory.CreateParameter();
                        pvalorPromedioCredito.ParameterName = "pvalorPromedioCredito";
                        pvalorPromedioCredito.DbType = DbType.Double;
                        pvalorPromedioCredito.Value = pPresupuesto.valorPromedioCredito;

                        DbParameter pflujoinicial = cmdTransaccionFactory.CreateParameter();
                        pflujoinicial.ParameterName = "pflujoinicial";
                        pflujoinicial.DbType = DbType.Double;
                        pflujoinicial.Value = pPresupuesto.flujoinicial;

                        DbParameter pfechacorte = cmdTransaccionFactory.CreateParameter();
                        pfechacorte.ParameterName = "pfechacorte";
                        pfechacorte.DbType = DbType.Date;
                        pfechacorte.Value = pPresupuesto.fechacorte;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pDescripcion);
                        cmdTransaccionFactory.Parameters.Add(pFecha_Elaboracion);
                        cmdTransaccionFactory.Parameters.Add(pFecha_Aprobacion);
                        cmdTransaccionFactory.Parameters.Add(pCod_Elaboro);
                        cmdTransaccionFactory.Parameters.Add(pCod_Aprobo);
                        cmdTransaccionFactory.Parameters.Add(pTipo_Presupuesto);
                        cmdTransaccionFactory.Parameters.Add(pNum_Periodos);
                        cmdTransaccionFactory.Parameters.Add(pCod_Periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pPeriodo_inicial);
                        cmdTransaccionFactory.Parameters.Add(pCentro_Costo);

                        cmdTransaccionFactory.Parameters.Add(pporPolizasVencidas);
                        cmdTransaccionFactory.Parameters.Add(pvalorUnitPoliza);
                        cmdTransaccionFactory.Parameters.Add(pcomisionPoliza);
                        cmdTransaccionFactory.Parameters.Add(pporLeyMiPyme);
                        cmdTransaccionFactory.Parameters.Add(pporProvision);
                        cmdTransaccionFactory.Parameters.Add(pporProvisionGen);
                        cmdTransaccionFactory.Parameters.Add(pvalorPromedioCredito);
                        cmdTransaccionFactory.Parameters.Add(pflujoinicial);
                        cmdTransaccionFactory.Parameters.Add(pfechacorte); 

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PRESUP_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla PresupuestoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de PresupuestoS</param>
        public void EliminarPresupuesto(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto = new Xpinn.Presupuesto.Entities.Presupuesto();

                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "p_IdPresupuesto";
                        pIdPresupuesto.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PRESUP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarPresupuesto", ex);
                    }
                }
             }
        }

        /// <summary>
        /// Obtiene un registro en la tabla PresupuestoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla PresupuestoS</param>
        /// <returns>Entidad Presupuesto consultado</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ConsultarPresupuesto(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Xpinn.Presupuesto.Entities.Presupuesto entidad = new Xpinn.Presupuesto.Entities.Presupuesto();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "SELECT p.*, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 1) As porPolizasVencidas, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 2) As valorUnitPoliza, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 3) As comisionPoliza, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 4) As porLeyMiPyme, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 5) As porProvision, " +
                                     " (Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 6) As porProvisionGen, " +                                     
                                     " Nvl((Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 7), 0) As valorPromedioCredito, " +
                                     " Nvl((Select t.valor From presupuesto_datentrada t Where idPresupuesto = p.idpresupuesto and codentrada = 8), 0) As flujoinicial " + 
                                     " FROm Presupuesto p" +
                                     " WHERE p.IDPresupuesto = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPresupuesto"] != DBNull.Value) entidad.idpresupuesto = Convert.ToInt64(resultado["IDPresupuesto"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_ELABORACION"] != DBNull.Value) entidad.fecha_elaboracion = Convert.ToDateTime(resultado["FECHA_ELABORACION"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["TIPO_PRESUPUESTO"] != DBNull.Value) entidad.tipo_presupuesto = Convert.ToInt32(resultado["TIPO_PRESUPUESTO"]);
                            if (resultado["NUM_PERIODOS"] != DBNull.Value) entidad.num_periodos = Convert.ToInt32(resultado["NUM_PERIODOS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["PERIODO_INICIAL"] != DBNull.Value) entidad.periodo_inicial = Convert.ToDateTime(resultado["PERIODO_INICIAL"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);

                            try
                            {
                                if (resultado["porPolizasVencidas"] != DBNull.Value) entidad.porPolizasVencidas = Convert.ToDouble(resultado["porPolizasVencidas"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["valorUnitPoliza"] != DBNull.Value) entidad.valorUnitPoliza = Convert.ToDouble(resultado["valorUnitPoliza"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["comisionPoliza"] != DBNull.Value) entidad.comisionPoliza = Convert.ToDouble(resultado["comisionPoliza"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["porLeyMiPyme"] != DBNull.Value) entidad.porLeyMiPyme = Convert.ToDouble(resultado["porLeyMiPyme"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["porProvision"] != DBNull.Value) entidad.porProvision = Convert.ToDouble(resultado["porProvision"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["porProvisionGen"] != DBNull.Value) entidad.porProvisionGen = Convert.ToDouble(resultado["porProvisionGen"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["valorPromedioCredito"] != DBNull.Value) entidad.valorPromedioCredito = Convert.ToDouble(resultado["valorPromedioCredito"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                                if (resultado["flujoinicial"] != DBNull.Value) entidad.flujoinicial = Convert.ToDouble(resultado["flujoinicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla Presupuesto dados unos filtros
        /// </summary>
        /// <param name="pPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Presupuestos obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Presupuesto.Entities.Presupuesto> lstPresupuesto = new List<Xpinn.Presupuesto.Entities.Presupuesto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  Presupuesto " + ObtenerFiltro(pPresupuesto) + " ORDER BY descripcion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Presupuesto.Entities.Presupuesto entidad = new Xpinn.Presupuesto.Entities.Presupuesto();

                            if (resultado["IDPresupuesto"] != DBNull.Value) entidad.idpresupuesto = Convert.ToInt64(resultado["IDPresupuesto"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_ELABORACION"] != DBNull.Value) entidad.fecha_elaboracion = Convert.ToDateTime(resultado["FECHA_ELABORACION"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_ELABORO"] != DBNull.Value) entidad.cod_elaboro = Convert.ToInt64(resultado["COD_ELABORO"]);
                            if (resultado["COD_APROBO"] != DBNull.Value) entidad.cod_aprobo = Convert.ToInt64(resultado["COD_APROBO"]);
                            if (resultado["TIPO_PRESUPUESTO"] != DBNull.Value) entidad.tipo_presupuesto = Convert.ToInt32(resultado["TIPO_PRESUPUESTO"]);
                            if (resultado["NUM_PERIODOS"] != DBNull.Value) entidad.num_periodos = Convert.ToInt32(resultado["NUM_PERIODOS"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["PERIODO_INICIAL"] != DBNull.Value) entidad.periodo_inicial = Convert.ToDateTime(resultado["PERIODO_INICIAL"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);

                            lstPresupuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPeriodosPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Presupuesto.Entities.Presupuesto> lstPresupuesto = new List<Xpinn.Presupuesto.Entities.Presupuesto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT numero_periodo, fecha_final, Count(*) As numero FROM Presupuesto_Detalle " + ObtenerFiltro(pPresupuesto) + " GROUP BY numero_periodo, fecha_final ORDER BY 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Presupuesto.Entities.Presupuesto entidad = new Xpinn.Presupuesto.Entities.Presupuesto();

                            if (resultado["NUMERO_PERIODO"] != DBNull.Value) entidad.numero_periodo = Convert.ToInt64(resultado["NUMERO_PERIODO"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_periodo = Convert.ToDateTime(resultado["FECHA_FINAL"]).ToString(conf.ObtenerFormatoFecha());

                            lstPresupuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarPeriodosPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Insertar registro del detalle del presupuesto
        /// </summary>
        /// <param name="pPresupuesto"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Xpinn.Presupuesto.Entities.Presupuesto CrearDetallePresupuesto(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = pPresupuesto.idpresupuesto;
                        pIdPresupuesto.DbType = DbType.Int64;

                        DbParameter pIdDetalle = cmdTransaccionFactory.CreateParameter();
                        pIdDetalle.ParameterName = "pIdDetalle";
                        pIdDetalle.Value = pPresupuesto.iddetalle;
                        pIdDetalle.Direction = ParameterDirection.InputOutput;

                        DbParameter pnumero_periodo = cmdTransaccionFactory.CreateParameter();
                        pnumero_periodo.ParameterName = "pnumero_periodo";
                        pnumero_periodo.Value = pPresupuesto.numero_periodo;
                        pnumero_periodo.DbType = DbType.Int64;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pPresupuesto.fecha_inicial;
                        pfecha_inicial.DbType = DbType.Date;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = pPresupuesto.fecha_final;
                        pfecha_final.DbType = DbType.Date;

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        pcod_cuenta.Value = pPresupuesto.cod_cuenta;
                        pcod_cuenta.DbType = DbType.String;

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "pcentro_costo";
                        pcentro_costo.Value = pPresupuesto.dcentro_costo;
                        pcentro_costo.DbType = DbType.Int64;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pPresupuesto.valor;
                        pvalor.DbType = DbType.Double;

                        DbParameter pincremento = cmdTransaccionFactory.CreateParameter();
                        pincremento.ParameterName = "pincremento";
                        pincremento.Value = pPresupuesto.incremento;
                        pincremento.DbType = DbType.Decimal;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pIdDetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_periodo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(pincremento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PREDETALLE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPresupuesto.iddetalle = Convert.ToInt64(pIdDetalle.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDetallePresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Presupuesto CrearDetalleFlujo(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = pPresupuesto.idpresupuesto;
                        pIdPresupuesto.DbType = DbType.Int64;

                        DbParameter pIdDetalle = cmdTransaccionFactory.CreateParameter();
                        pIdDetalle.ParameterName = "pIdDetalle";
                        pIdDetalle.Value = pPresupuesto.iddetalle;
                        pIdDetalle.Direction = ParameterDirection.InputOutput;

                        DbParameter pnumero_periodo = cmdTransaccionFactory.CreateParameter();
                        pnumero_periodo.ParameterName = "pnumero_periodo";
                        pnumero_periodo.Value = pPresupuesto.numero_periodo;
                        pnumero_periodo.DbType = DbType.Int64;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pPresupuesto.fecha_inicial;
                        pfecha_inicial.DbType = DbType.Date;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = pPresupuesto.fecha_final;
                        pfecha_final.DbType = DbType.Date;

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        pcod_cuenta.Value = pPresupuesto.cod_cuenta;
                        pcod_cuenta.DbType = DbType.String;

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "pcentro_costo";
                        pcentro_costo.Value = pPresupuesto.dcentro_costo;
                        pcentro_costo.DbType = DbType.Int64;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pPresupuesto.valor;
                        pvalor.DbType = DbType.Double;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pIdDetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_periodo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PREFLUJO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPresupuesto.iddetalle = Convert.ToInt64(pIdDetalle.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDetalleFlujo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para grabar la información ingresada de colocaciones en el presupuesto
        /// </summary>
        /// <param name="pPresupuesto"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Xpinn.Presupuesto.Entities.Presupuesto CrearDetalleColocacion(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = pPresupuesto.idpresupuesto;
                        pIdPresupuesto.DbType = DbType.Int64;

                        DbParameter pIdDetalle = cmdTransaccionFactory.CreateParameter();
                        pIdDetalle.ParameterName = "pIdDetalle";
                        pIdDetalle.Value = pPresupuesto.iddetallecolocacion;
                        pIdDetalle.Direction = ParameterDirection.InputOutput;

                        DbParameter pnumero_periodo = cmdTransaccionFactory.CreateParameter();
                        pnumero_periodo.ParameterName = "pnumero_periodo";
                        pnumero_periodo.Value = pPresupuesto.numero_periodo;
                        pnumero_periodo.DbType = DbType.Int64;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pPresupuesto.fecha_inicial;
                        pfecha_inicial.DbType = DbType.Date;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = pPresupuesto.fecha_final;
                        pfecha_final.DbType = DbType.Date;

                        DbParameter pitem = cmdTransaccionFactory.CreateParameter();
                        pitem.ParameterName = "pitem";
                        pitem.Value = pPresupuesto.item;
                        pitem.DbType = DbType.String;

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "pcentro_costo";
                        pcentro_costo.Value = pPresupuesto.dcentro_costo;
                        pcentro_costo.DbType = DbType.Int64;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pPresupuesto.colocacion;
                        pvalor.DbType = DbType.Double;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pIdDetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_periodo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pitem);
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PRECOLOCA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPresupuesto.iddetallecolocacion = Convert.ToInt64(pIdDetalle.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDetalleColocacion", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Presupuesto CrearNumeroEjecutivos(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdPresupuesto = cmdTransaccionFactory.CreateParameter();
                        pIdPresupuesto.ParameterName = "pIdPresupuesto";
                        pIdPresupuesto.Value = pPresupuesto.idpresupuesto;
                        pIdPresupuesto.DbType = DbType.Int64;

                        DbParameter poficina = cmdTransaccionFactory.CreateParameter();
                        poficina.ParameterName = "poficina";
                        poficina.Value = pPresupuesto.oficina;
                        poficina.DbType = DbType.Int64;

                        DbParameter pnumero_ejecutivos = cmdTransaccionFactory.CreateParameter();
                        pnumero_ejecutivos.ParameterName = "pnumero_ejecutivos";
                        pnumero_ejecutivos.Value = pPresupuesto.numero_ejecutivos;
                        pnumero_ejecutivos.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pIdPresupuesto);
                        cmdTransaccionFactory.Parameters.Add(poficina);
                        cmdTransaccionFactory.Parameters.Add(pnumero_ejecutivos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PRENUMEJE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDetalleColocacion", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Presupuesto CrearDetalleObligacion(Xpinn.Presupuesto.Entities.Presupuesto pPresupuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidobligacion = cmdTransaccionFactory.CreateParameter();
                        pidobligacion.ParameterName = "pidobligacion";
                        pidobligacion.DbType = DbType.Int64;
                        pidobligacion.Value = 0;
                        pidobligacion.Direction = ParameterDirection.InputOutput;

                        DbParameter pidpresupuesto = cmdTransaccionFactory.CreateParameter();
                        pidpresupuesto.ParameterName = "pidpresupuesto";
                        pidpresupuesto.DbType = DbType.Int64;
                        pidpresupuesto.Value = pPresupuesto.idpresupuesto;
                        pidpresupuesto.Direction = ParameterDirection.Input;

                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "pcodigo";
                        pcodigo.DbType = DbType.Int64;
                        pcodigo.Value = pPresupuesto.codigo_obl;

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "pdescripcion";
                        pdescripcion.DbType = DbType.String;
                        pdescripcion.Value = pPresupuesto.descripcion_obl;

                        DbParameter pcupo = cmdTransaccionFactory.CreateParameter();
                        pcupo.ParameterName = "pcupo";
                        pcupo.DbType = DbType.Double;
                        pcupo.Value = pPresupuesto.cupo_obl;

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "ptasa";
                        ptasa.DbType = DbType.Double;
                        ptasa.Value = pPresupuesto.tasa_obl;

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "pplazo";
                        pplazo.DbType = DbType.Double;
                        pplazo.Value = pPresupuesto.plazo_obl;

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "pcod_periodicidad";
                        pcod_periodicidad.DbType = DbType.Double;
                        pcod_periodicidad.Value = pPresupuesto.cod_periodicidad_obl;

                        DbParameter pgracia = cmdTransaccionFactory.CreateParameter();
                        pgracia.ParameterName = "pgracia";
                        pgracia.DbType = DbType.Double;
                        pgracia.Value = pPresupuesto.gracia_obl;

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcod_oficina";
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Value = pPresupuesto.oficina_obl;

                        DbParameter pnumero_periodo = cmdTransaccionFactory.CreateParameter();
                        pnumero_periodo.ParameterName = "pnumero_periodo";
                        pnumero_periodo.Value = pPresupuesto.numero_periodo;
                        pnumero_periodo.DbType = DbType.Int64;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pPresupuesto.fecha_inicial;
                        pfecha_inicial.DbType = DbType.Date;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = pPresupuesto.fecha_final;
                        pfecha_final.DbType = DbType.Date;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pPresupuesto.valor;
                        pvalor.DbType = DbType.Double;

                        cmdTransaccionFactory.Parameters.Add(pidobligacion);
                        cmdTransaccionFactory.Parameters.Add(pidpresupuesto);
                        cmdTransaccionFactory.Parameters.Add(pcodigo);
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);
                        cmdTransaccionFactory.Parameters.Add(pcupo);
                        cmdTransaccionFactory.Parameters.Add(ptasa);
                        cmdTransaccionFactory.Parameters.Add(pplazo);
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(pgracia);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pnumero_periodo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_PREOBLIGA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPresupuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDetalleObligacion", ex);
                        return pPresupuesto;
                    }
                }
            }
        }

         /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario vUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(IDPresupuesto) + 1 FROM Presupuesto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch 
                    {
                        return 1;
                    }
                }
            }
        }

        /// <summary>
        ///  Método para mostrar los rubros a presupuestar
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public DataTable ListarCuentas(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtCuentas = new DataTable();
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_cuenta, nombre, depende_de, tipo, 0 as saldo_promedio, 0 as saldo_final, 0.0 Incremento FROM  Plan_Cuentas WHERE (cod_cuenta Like '4%' Or cod_cuenta Like '5%'  Or cod_cuenta Like '6%') And nivel <= Nvl(BuscarGeneral(1100, 2), 4)" + filtro + " ORDER BY cod_cuenta";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtCuentas.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtCuentas, ref Error);

                        // Adicionar la utilidad del ejercicio
                        DataRow drUtil = dtCuentas.NewRow();
                        drUtil[0] = "3505";
                        drUtil[1] = "UTILIDAD O PERDIDA DEL EJERCICIO";
                        drUtil[2] = "35";
                        drUtil[3] = "C";
                        drUtil[4] = "0";
                        drUtil[5] = "0";
                        dtCuentas.Rows.Add(drUtil);
                        dtCuentas.AcceptChanges();

                        // Adicionar la utilidad acumulada
                        DataRow drUtilAcum = dtCuentas.NewRow();
                        drUtilAcum[0] = "35";
                        drUtilAcum[1] = "UTILIDAD ACUMULADA";
                        drUtilAcum[2] = "3";
                        drUtilAcum[3] = "C";
                        drUtilAcum[4] = "0";
                        drUtilAcum[5] = "0";
                        dtCuentas.Rows.Add(drUtilAcum);
                        dtCuentas.AcceptChanges();

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return dtCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarEjecucionPresupuesto(Int64 pIdPresupuesto, Int64 pNumeroPeriodo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtCuentas = new DataTable();
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT d.cod_cuenta, d.numero_periodo, d.fecha_inicial, d.fecha_final, d.centro_costo, p.nombre, p.depende_de, p.tipo, valor As valor_presupuestado, 0 As valor_ejecutado, 0 As diferencia, 0 As porcentaje, 0 As acumulado_presupuestado, 0 As acumulado_ejecutado, 0 As acumulado_diferencia, 0 As acumulado_porcentaje FROM Presupuesto_Detalle d Left Join Plan_Cuentas p On d.cod_cuenta = p.cod_cuenta WHERE d.idPresupuesto = " + pIdPresupuesto + " And d.numero_periodo = " + pNumeroPeriodo + " ORDER BY d.iddetalle ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtCuentas.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtCuentas, ref Error);

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return dtCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarEjecucionPresupuesto", ex);
                        return null;
                    }
                }
            }
        }

        public decimal SaldoPromedioCuenta(string pcod_cuenta, DateTime pfecha_inicial, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal saldo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT Round(Sum(b.saldo - b.saldoant)/12) As saldo " +
                                     " FROM  v_saldo_cuenta b WHERE b.cod_cuenta = '" + pcod_cuenta + "' And b.fecha Between to_date('" + pfecha_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pfecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + pfiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToDecimal(resultado["saldo"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return saldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "SaldoPromedioCuenta", ex);
                        return saldo;
                    }
                }
            }
        }

        public decimal SaldoFinalCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal saldo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT Sum(b.valor) As saldo " +
                                     " FROM  balance b WHERE b.cod_cuenta = '" + pcod_cuenta + "' And b.fecha = to_date('" + pfecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + pfiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToDecimal(resultado["saldo"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return saldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "SaldoFinalCuenta", ex);
                        return saldo;
                    }
                }
            }
        }

        public decimal SaldoPeriodoCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal saldo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT Sum(b.saldo - b.saldoant) As saldo " +
                                     " FROM  v_saldo_cuenta b WHERE b.cod_cuenta = '" + pcod_cuenta + "' And b.fecha = to_date('" + pfecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + pfiltro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["saldo"] != DBNull.Value) saldo = Convert.ToDecimal(resultado["saldo"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return saldo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "SaldoFinalCuenta", ex);
                        return saldo;
                    }
                }
            }
        }

        public string CuentaDependeDe(string cod_cuenta, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string depende_de = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT depende_de FROM  Plan_Cuentas WHERE cod_cuenta = '" + cod_cuenta + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["depende_de"] != DBNull.Value) depende_de = Convert.ToString(resultado["depende_de"]);                            
                        }

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return depende_de;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CuentaDependeDe", ex);
                        return null;
                    }
                }
            }
        }

        public int Estructura(DbDataReader resultado, ref DataTable dtCuentas, ref string[] aColumnas, ref System.Type[] aTipos, ref string Error)
        {
            Error = "";
            DataTable schemaTable;
            schemaTable = resultado.GetSchemaTable();
            int numerocolumna = 0;
            foreach (DataRow myField in schemaTable.Rows)
            {
                string NombreColumna = "";
                string TipoColumna = "";
                System.Type Tipo = null;
                foreach (DataColumn myProperty in schemaTable.Columns)
                {
                    if (myProperty.ColumnName.Trim() == "ColumnName")
                        NombreColumna = myField[myProperty].ToString();
                    if (myProperty.ColumnName.Trim() == "DataType")
                    {
                        TipoColumna = myField[myProperty].ToString();
                        Tipo = System.Type.GetType(TipoColumna);
                    }
                }
                try
                {
                    Array.Resize(ref aColumnas, numerocolumna + 1);
                    aColumnas.SetValue(NombreColumna, numerocolumna);
                    Array.Resize(ref aTipos, numerocolumna + 1);
                    aTipos.SetValue(Tipo, numerocolumna);
                    dtCuentas.Columns.Add(NombreColumna, Tipo);
                    dtCuentas.Columns[numerocolumna].AllowDBNull = true;
                    numerocolumna = numerocolumna + 1;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return 0;
                }
            }
            return numerocolumna;
        }

        public Boolean TraerResultados(DbDataReader resultado, ref DataTable dtCuentas, ref string Error)
        {
            string[] aColumnas = new string[] { };
            System.Type[] aTipos = new System.Type[] { };
            int numerocolumna = 0;

            numerocolumna = Estructura(resultado, ref dtCuentas, ref aColumnas, ref aTipos, ref Error);

            try
            {
                while (resultado.Read())
                {
                    DataRow drFila;
                    drFila = dtCuentas.NewRow();
                    for (int i = 0; i < numerocolumna; i++)
                    {
                        if (resultado[aColumnas[i]] != DBNull.Value)
                        {
                            drFila[i] = Convert.ChangeType(resultado[aColumnas[i]], aTipos[i]);
                        }
                    }
                    dtCuentas.Rows.Add(drFila);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public decimal ConsultarValorPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select p.valor " +
                                     " From Presupuesto_Detalle p " +
                                     " Where p.idpresupuesto = " + idPresupuesto + " And p.numero_periodo = " + pnumeroPeriodo + " And p.cod_cuenta = '" + pcod_cuenta_pre + "' ";
                        if (pcentro_costo != 0)
                            sql = sql + " And p.centro_costo = " + pcentro_costo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorPresupuesto", ex);
                        return 0;
                    }
                }
            }
        }

        public Boolean ConsultarValorEjecutado(Int64 idPresupuesto, Int64 pnumeroPeriodo, DateTime pfecha_inicial, DateTime pfecha_final, string pcod_cuenta_pre, Int64 pcentro_costo, ref decimal valor_presupuestado, ref decimal valor_ejecutado, ref decimal valor_presupuestado_acumulado, ref decimal valor_ejecutado_acumulado, Usuario pUsuario)
        {
            valor_presupuestado = 0;
            valor_ejecutado = 0;
            valor_presupuestado_acumulado = 0;
            valor_ejecutado_acumulado = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIDPRESUPUESTO = cmdTransaccionFactory.CreateParameter();
                        pIDPRESUPUESTO.ParameterName = "pIDPRESUPUESTO";
                        pIDPRESUPUESTO.Value = idPresupuesto;
                        pIDPRESUPUESTO.DbType = DbType.Int64;

                        DbParameter pNUMERO_PERIODO = cmdTransaccionFactory.CreateParameter();
                        pNUMERO_PERIODO.ParameterName = "pNUMERO_PERIODO";
                        pNUMERO_PERIODO.Value = pnumeroPeriodo;
                        pNUMERO_PERIODO.DbType = DbType.Int64;

                        DbParameter pFECHA_INICIAL = cmdTransaccionFactory.CreateParameter();
                        pFECHA_INICIAL.ParameterName = "pfecha_inicial";
                        pFECHA_INICIAL.Value = pfecha_inicial;
                        pFECHA_INICIAL.DbType = DbType.Date;

                        DbParameter pFECHA_FINAL = cmdTransaccionFactory.CreateParameter();
                        pFECHA_FINAL.ParameterName = "pfecha_final";
                        pFECHA_FINAL.Value = pfecha_final;
                        pFECHA_FINAL.DbType = DbType.Date;

                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "pCOD_CUENTA";
                        pCOD_CUENTA.Value = pcod_cuenta_pre;
                        pCOD_CUENTA.DbType = DbType.String;

                        DbParameter pCENTRO_COSTO = cmdTransaccionFactory.CreateParameter();
                        pCENTRO_COSTO.ParameterName = "pCENTRO_COSTO";
                        pCENTRO_COSTO.Value = pcentro_costo;
                        pCENTRO_COSTO.DbType = DbType.Int64;

                        DbParameter pVALOR_PRESUPUESTADO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_PRESUPUESTADO.ParameterName = "pVALOR_PRESUPUESTADO";
                        pVALOR_PRESUPUESTADO.Value = 0;
                        pVALOR_PRESUPUESTADO.DbType = DbType.Decimal;
                        pVALOR_PRESUPUESTADO.Direction = ParameterDirection.Output;

                        DbParameter pVALOR_EJECUTADO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_EJECUTADO.ParameterName = "pVALOR_EJECUTADO";
                        pVALOR_EJECUTADO.Value = 0;
                        pVALOR_EJECUTADO.DbType = DbType.Decimal;
                        pVALOR_EJECUTADO.Direction = ParameterDirection.Output;

                        DbParameter pVALOR_PRESUPUESTADO_ACUMULADO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_PRESUPUESTADO_ACUMULADO.ParameterName = "pVALOR_PRESUPUESTADO_ACUMULADO";
                        pVALOR_PRESUPUESTADO_ACUMULADO.Value = 0;
                        pVALOR_PRESUPUESTADO_ACUMULADO.DbType = DbType.Decimal;
                        pVALOR_PRESUPUESTADO_ACUMULADO.Direction = ParameterDirection.Output;

                        DbParameter pVALOR_EJECUTADO_ACUMULADO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_EJECUTADO_ACUMULADO.ParameterName = "pVALOR_EJECUTADO_ACUMULADO";
                        pVALOR_EJECUTADO_ACUMULADO.Value = 0;
                        pVALOR_EJECUTADO_ACUMULADO.DbType = DbType.Decimal;
                        pVALOR_EJECUTADO_ACUMULADO.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pIDPRESUPUESTO);
                        cmdTransaccionFactory.Parameters.Add(pNUMERO_PERIODO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_INICIAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_FINAL);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pCENTRO_COSTO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_PRESUPUESTADO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_EJECUTADO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_PRESUPUESTADO_ACUMULADO);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_EJECUTADO_ACUMULADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_EJECUCION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        valor_presupuestado = Convert.ToDecimal(pVALOR_PRESUPUESTADO.Value);
                        valor_ejecutado = Convert.ToDecimal(pVALOR_EJECUTADO.Value);
                        valor_presupuestado_acumulado = Convert.ToDecimal(pVALOR_PRESUPUESTADO_ACUMULADO.Value);
                        valor_ejecutado_acumulado = Convert.ToDecimal(pVALOR_EJECUTADO_ACUMULADO.Value);                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorEjecutado", ex);
                        return false;
                    }
                }
            }
        }

        public decimal ConsultarIncrementoPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select p.incremento " +
                                     " From Presupuesto_Detalle p " +
                                     " Where p.idpresupuesto = " + idPresupuesto + " And p.numero_periodo = " + pnumeroPeriodo + " And p.cod_cuenta = '" + pcod_cuenta_pre + "' ";
                        if (pcentro_costo != 0)
                            sql = sql + " And p.centro_costo = " + pcentro_costo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["incremento"] != DBNull.Value) valor = Convert.ToDecimal(resultado["incremento"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarIncrementoPresupuesto", ex);
                        return 0;
                    }
                }
            }
        }

        public decimal ConsultarValorPresupuestoColocacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select p.valor " +
                                     " From presupuesto_colocacion p " +
                                     " Where p.idpresupuesto = " + idPresupuesto + " And p.item = '" + pitem + "' And p.numero_periodo = " + pnumeroPeriodo;
                        if (pcentro_costo != 0)
                            sql = sql + " And p.centro_costo = " + pcentro_costo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorPresupuestoColocacion", ex);
                        return 0;
                    }
                }
            }
        }

        public decimal ConsultarValorPresupuestoObligacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select p.valor " +
                                     " From presupuesto_obligacion p " +
                                     " Where p.idpresupuesto = " + idPresupuesto + " And p.codigo = '" + pitem + "' And p.numero_periodo = " + pnumeroPeriodo;
                        if (pcentro_costo != 0)
                            sql = sql + " And p.cod_oficina = " + pcentro_costo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorPresupuestoObligacion", ex);
                        return 0;
                    }
                }
            }
        }

        public Int64 ConsultarNumeroEjecutivos(Int64 idPresupuesto, Int64 pcod_oficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Int64 valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select p.numeroejecutivos " +
                                     " From presupuesto_ejecutivos p " +
                                     " Where p.idpresupuesto = " + idPresupuesto + " And p.cod_oficina = " + pcod_oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numeroejecutivos"] != DBNull.Value) valor = Convert.ToInt64(resultado["numeroejecutivos"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarNumeroEjecutivos", ex);
                        return 0;
                    }
                }
            }
        }

        public string ConsultarParametroCuenta(Int64 pcodigo, Usuario vUsuario)
        {
            DbDataReader resultado;
            string cod_cuenta = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT p.cod_cuenta " +
                                     " FROM presupuesto_cuentas p" +
                                     " WHERE p.codigo = " + pcodigo.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_cuenta"] != DBNull.Value) cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_cuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarParametroCuenta", ex);
                        return cod_cuenta;
                    }
                }
            }
        }

        public Boolean EsParametroCuenta(string cod_cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT p.cod_cuenta " +
                                     " FROM presupuesto_cuentas p" +
                                     " WHERE p.cod_cuenta = '" + cod_cuenta.ToString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            return true;
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EsParametroCuenta", ex);
                        return false;
                    }
                }
            }
        }

        #region Cartera

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// CARTERA
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Método para listar las clasificaciones de cartera existentes
        /// </summary>
        /// <param name="pfechahistorico"></param>
        /// <returns></returns>
        public DataTable ListarClasificacion(DateTime pfechahistorico, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtCuentas = new DataTable();
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_clasifica, descripcion FROM  clasificacion ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtCuentas.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtCuentas, ref Error);

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return dtCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarClasificacionOficinas(DateTime pfechahistorico, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtCuentas = new DataTable();
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "SELECT a.cod_clasifica, a.descripcion, b.cod_oficina, b.nombre, Sum(h.saldo_capital) As Saldo " +
                                     " FROM  historico_cre h Left Join clasificacion a ON h.cod_clasifica = a.cod_clasifica Left Join oficina b On h.cod_oficina = b.cod_oficina " +
                                     " WHERE h.fecha_historico = to_date('" + pfechahistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND h.saldo_capital != 0 " +
                                     " AND h.cod_linea_credito NOT IN (Select cod_linea_credito from parametros_linea where cod_parametro = 320) " + filtro + 
                                     " GROUP BY a.cod_clasifica, a.descripcion, b.cod_oficina, b.nombre " + 
                                     " ORDER BY 1, 3 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtCuentas.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtCuentas, ref Error);

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return dtCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarClasificacionOficinas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para determinar el valor de interés corriente proyectado en un período dado
        /// </summary>
        /// <param name="fechahistorico"></param>
        /// <param name="cod_clasifica"></param>
        /// <param name="fecha_inicial"></param>
        /// <param name="fecha_final"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public decimal ConsultarValorCartera(DateTime fechahistorico, int cod_clasifica, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Sum(h.valor) As valor " +
                                     " From historico_amortiza h Inner Join credito c On h.numero_radicacion = c.numero_radicacion Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito " +
                                     " Where h.fecha_historico = to_date('" + fechahistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And l.cod_clasifica = " + cod_clasifica + " And h.cod_atr = 2 " + filtro + 
                                     " And h.fecha_cuota Between to_date('" + fecha_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + fecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);                            
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorCartera", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Método para calcular el valor a recuperar de un atributo dato en un período de tiempo
        /// </summary>
        /// <param name="fechahistorico"></param>
        /// <param name="cod_clasifica"></param>
        /// <param name="cod_atr"></param>
        /// <param name="fecha_inicial"></param>
        /// <param name="fecha_final"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public decimal ConsultarValorRecuperacion(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro_clasifica = "";
                        if (cod_clasifica != int.MinValue)
                            filtro_clasifica = " And l.cod_clasifica = " + cod_clasifica;
                        Configuracion conf = new Configuracion();
                        string sql = "Select Sum(h.valor) As valor " +
                                     " From historico_amortiza h Inner Join credito c On h.numero_radicacion = c.numero_radicacion Left Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito " +
                                     " Where h.fecha_historico = to_date('" + fechahistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + filtro_clasifica + filtro + " And h.cod_atr = " + cod_atr +
                                     " And h.fecha_cuota Between to_date('" + fecha_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + fecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                     " And c.cod_linea_credito Not In (Select p.cod_linea_credito From parametros_linea p where p.cod_parametro = 320 And p.cod_linea_credito = c.cod_linea_credito) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorRecuperacion", ex);
                        return 0;
                    }
                }
            }
        }

        public decimal ConsultarValorRecuperacionCausado(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro_clasifica = "";
                        if (cod_clasifica != int.MinValue)
                            filtro_clasifica = " And l.cod_clasifica = " + cod_clasifica;
                        Configuracion conf = new Configuracion();
                        string sql = "Select Sum(h.valor) As valor " +
                                     " From pre_historico_amortiza h Inner Join credito c On h.numero_radicacion = c.numero_radicacion Left Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito " +
                                     " Where h.fecha_historico = to_date('" + fechahistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + filtro_clasifica + filtro + " And h.cod_atr = " + cod_atr +
                                     " And h.fecha_causa Between to_date('" + fecha_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + fecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                     " And c.cod_linea_credito Not In (Select p.cod_linea_credito From parametros_linea p where p.cod_parametro = 320 And p.cod_linea_credito = c.cod_linea_credito) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorRecuperacionCausado", ex);
                        return 0;
                    }
                }
            }
        }

        public double TasaPromedioColocacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            double valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select Round(Avg(Case a.calculo_atr When '1' Then CalTasMod(a.tasa, t.efectiva_nomina, t.cod_periodicidad, t.modalidad, t.cod_periodicidad_cap, 1, 3, t.modalidad, 3, 1)
                                        When '3' Then CalTasMod(h.valor, p.efectiva_nomina, p.cod_periodicidad, p.modalidad, p.cod_periodicidad_cap, 1, 3, p.modalidad, 3, 1)
                                        End), 2) As Tasa_Efectiva
                                        from atributoslinea a Left Join tipotasa t On a.tipo_tasa = t.cod_tipo_tasa 
                                        Left Join historicotasa h On h.tipo_historico = a.tipo_historico And SYSDATE Between fecha_inicial And fecha_final Left Join tipotasahist th On h.tipo_historico = th.tipo_historico
                                        Left Join tipotasa p On th.tipo_tasa = p.cod_tipo_tasa
                                        where a.cod_atr = 2 and a.cod_linea_credito Not In (Select p.cod_linea_credito From parametros_linea p Where p.cod_parametro = 320)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["Tasa_Efectiva"] != DBNull.Value) valor = Convert.ToDouble(resultado["Tasa_Efectiva"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "TasaPromedioColocacion", ex);
                        return 0;
                    }
                }
            }
        }

        public double PlazoPromedioColocacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            double valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select Round(avg(maximo), 0) as plazo_promedio from rangostopes where tipo_tope = 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["plazo_promedio"] != DBNull.Value) valor = Convert.ToDouble(resultado["plazo_promedio"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "PlazoPromedioColocacion", ex);
                        return 0;
                    }
                }
            }
        }

        public int NumeroEjecutivosOficina(int cod_oficina, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            int valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select count(*) As numero from asejecutivos a, usuarios u
                                        where a.iusuario = u.codusuario and a.iestado = 1 and u.codperfil = 5 and u.estado = 1 and a.ioficina = " + cod_oficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["numero"] != DBNull.Value) valor = Convert.ToInt32(resultado["numero"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "NumeroEjecutivosOficina", ex);
                        return 0;
                    }
                }
            }
        }

        public DateTime FechaUltimoHistorico(DateTime pFechaInicial, Usuario vUsuario)
        {
            DbDataReader resultado;
            DateTime fechahistorico = new DateTime(1900, 1, 1);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "SELECT MAX(fecha_historico) AS fecha_historico FROM historico_amortiza ";
                        if (pFechaInicial != DateTime.MaxValue)
                            sql = sql + " WHERE fecha_historico <= To_Date('" + pFechaInicial.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_historico"] != DBNull.Value) fechahistorico = Convert.ToDateTime(resultado["fecha_historico"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return fechahistorico;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "FechaUltimoHistorico", ex);
                        return System.DateTime.MinValue;
                    }
                }
            }
        }

        #endregion Cartera

        #region Nomina

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// NOMINA
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataTable ListarNomina(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtNomina = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = pFechaCorte;
                        pFecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFecha);
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_NOMINA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "USP_XPINN_PRE_NOMINA", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.CODIGO, e.NOMBRE, e.FECHA_INGRESO, Nvl(e.COD_OFICINA, 0) As COD_OFICINA, e.SALARIO, Nvl(e.CARGO, 0) As CARGO, NVL(e.INCREMENTO, 0) AS INCREMENTO, e.SALARIO_NUEVO, e.AUX_TRANS, " +
                                     " e.CUMPLIMIENTO, Nvl(e.COMISIONES, 0) As COMISIONES, Nvl(e.AUX_TEL, 0) As AUX_TEL, Nvl(e.AUX_GAS, 0) As AUX_GAS, e.CESANTIAS, e.INT_CES, e.PRIMA, e.VACACIONES, e.DOTACION, e.SALUD, e.PENSION, " +
                                     " e.ARP, e.CAJA_COMP, e.TOTAL, o.NOMBRE as OFICINA, c.NOM_CARGO, e.TIPO_SALARIO, CASE e.TIPO_SALARIO WHEN 1 THEN 'Normal' WHEN 2 THEN 'Integral' END AS NOM_TIPO_SALARIO " +
                                     " From pre_empleado e Left Join oficina o On e.cod_oficina = o.cod_oficina Left Join pre_cargo c On e.cargo = c.cod_cargo";
                        if (filtro.Trim() != "")
                            sql = sql + " Where (" + filtro + ") Or e.cod_oficina Is Null";
                        sql = sql + " Order by e.COD_OFICINA, e.CODIGO ";


                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtNomina.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtNomina, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarNomina", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarTotalesNomina(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtTotNomina = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.COD_OFICINA, o.NOMBRE as OFICINA, Sum(e.SALARIO) As SALARIO, Round(Avg(NVL(e.INCREMENTO, 0)), 2) AS INCREMENTO, Sum(e.SALARIO_NUEVO) AS SALARIO_NUEVO, Sum(e.AUX_TRANS) As AUX_TRANS, " +
                                     " Round(Avg(e.CUMPLIMIENTO), 2) As CUMPLIMIENTO, Sum(e.COMISIONES) As COMISIONES, Sum(e.AUX_TEL) As AUX_TEL, Sum(e.AUX_GAS) As AUX_GAS, Sum(e.CESANTIAS) As CESANTIAS, Sum(e.INT_CES) As INT_CES, Sum(e.PRIMA) As PRIMA, Sum(e.VACACIONES) As VACACIONES, Sum(e.DOTACION) As DOTACION, Sum(e.SALUD) As SALUD, Sum(e.PENSION) As PENSION, Sum(e.ARP) As ARP, Sum(e.CAJA_COMP) As CAJA_COMP, Sum(e.TOTAL) As TOTAL " +
                                     " From pre_empleado e Left Join oficina o On e.cod_oficina = o.cod_oficina ";
                        if (filtro.Trim() != "")
                            sql = sql + " Where (" + filtro + ") Or e.cod_oficina Is Null";
                        sql = sql + " Group by e.COD_OFICINA, o.NOMBRE Order by e.COD_OFICINA ";


                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtTotNomina.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtTotNomina, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtTotNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarTotalesNomina", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina CrearEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pNomina.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_Nombre = cmdTransaccionFactory.CreateParameter();
                        p_Nombre.ParameterName = "p_Nombre";
                        p_Nombre.DbType = DbType.String;
                        p_Nombre.Value = pNomina.nombre;

                        DbParameter p_Fecha_Ingreso = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Ingreso.ParameterName = "p_Fecha_Ingreso";
                        p_Fecha_Ingreso.DbType = DbType.Date;
                        p_Fecha_Ingreso.Value = pNomina.fecha_ingreso;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pNomina.cod_oficina;

                        DbParameter p_Salario = cmdTransaccionFactory.CreateParameter();
                        p_Salario.ParameterName = "p_Salario";
                        p_Salario.DbType = DbType.Decimal;
                        p_Salario.Value = pNomina.salario;

                        DbParameter p_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Cargo.ParameterName = "p_Cargo";
                        p_Cargo.Value = pNomina.cargo;

                        DbParameter p_Incremento = cmdTransaccionFactory.CreateParameter();
                        p_Incremento.ParameterName = "p_incremento";
                        p_Incremento.Value = pNomina.incremento;
                        p_Incremento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_salario_nuevo = cmdTransaccionFactory.CreateParameter(); 
                        p_salario_nuevo.ParameterName = "p_salario_nuevo"; 
                        p_salario_nuevo.Value = pNomina.salario_nuevo;
                        p_salario_nuevo.Direction = ParameterDirection.Output;

                        DbParameter p_aux_trans = cmdTransaccionFactory.CreateParameter(); 
                        p_aux_trans.ParameterName = "p_aux_trans"; 
                        p_aux_trans.Value = pNomina.aux_trans;
                        p_aux_trans.Direction = ParameterDirection.Output;

                        DbParameter p_cumplimiento = cmdTransaccionFactory.CreateParameter();
                        p_cumplimiento.ParameterName = "p_cumplimiento";
                        p_cumplimiento.Value = pNomina.cumplimiento;
                        p_cumplimiento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_comisiones = cmdTransaccionFactory.CreateParameter(); 
                        p_comisiones.ParameterName = "p_comisiones"; 
                        p_comisiones.Value = pNomina.comisiones;
                        p_comisiones.Direction = ParameterDirection.InputOutput;

                        DbParameter p_aux_gas = cmdTransaccionFactory.CreateParameter(); 
                        p_aux_gas.ParameterName = "p_aux_gas"; 
                        p_aux_gas.Value = pNomina.aux_gas;
                        p_aux_gas.Direction = ParameterDirection.Output;

                        DbParameter p_cesantias = cmdTransaccionFactory.CreateParameter(); 
                        p_cesantias.ParameterName = "p_cesantias"; 
                        p_cesantias.Value = pNomina.cesantias;
                        p_cesantias.Direction = ParameterDirection.Output;

                        DbParameter p_int_ces = cmdTransaccionFactory.CreateParameter(); 
                        p_int_ces.ParameterName = "p_int_ces"; 
                        p_int_ces.Value = pNomina.int_ces;
                        p_int_ces.Direction = ParameterDirection.Output;

                        DbParameter p_prima = cmdTransaccionFactory.CreateParameter(); 
                        p_prima.ParameterName = "p_prima"; 
                        p_prima.Value = pNomina.prima;
                        p_prima.Direction = ParameterDirection.Output;

                        DbParameter p_vacaciones = cmdTransaccionFactory.CreateParameter(); 
                        p_vacaciones.ParameterName = "p_vacaciones"; 
                        p_vacaciones.Value = pNomina.vacaciones;
                        p_vacaciones.Direction = ParameterDirection.Output;

                        DbParameter p_dotacion = cmdTransaccionFactory.CreateParameter(); 
                        p_dotacion.ParameterName = "p_dotacion"; 
                        p_dotacion.Value = pNomina.dotacion;
                        p_dotacion.Direction = ParameterDirection.Output;

                        DbParameter p_salud = cmdTransaccionFactory.CreateParameter(); 
                        p_salud.ParameterName = "p_salud"; 
                        p_salud.Value = pNomina.salud;
                        p_salud.Direction = ParameterDirection.Output;

                        DbParameter p_pension = cmdTransaccionFactory.CreateParameter(); 
                        p_pension.ParameterName = "p_pension"; 
                        p_pension.Value = pNomina.pension;
                        p_pension.Direction = ParameterDirection.Output;

                        DbParameter p_arp = cmdTransaccionFactory.CreateParameter(); 
                        p_arp.ParameterName = "p_arp"; 
                        p_arp.Value = pNomina.arp;
                        p_arp.Direction = ParameterDirection.Output;

                        DbParameter p_caja_comp = cmdTransaccionFactory.CreateParameter(); 
                        p_caja_comp.ParameterName = "p_caja_comp"; 
                        p_caja_comp.Value = pNomina.caja_comp;
                        p_caja_comp.Direction = ParameterDirection.Output;

                        DbParameter p_total = cmdTransaccionFactory.CreateParameter(); 
                        p_total.ParameterName = "p_total"; 
                        p_total.Value = pNomina.total;
                        p_total.Direction = ParameterDirection.Output;

                        DbParameter p_aux_tel = cmdTransaccionFactory.CreateParameter(); 
                        p_aux_tel.ParameterName = "p_aux_tel"; 
                        p_aux_tel.Value = pNomina.aux_tel;
                        p_aux_tel.Direction = ParameterDirection.Output;

                        DbParameter p_tipo_salario = cmdTransaccionFactory.CreateParameter();
                        p_tipo_salario.ParameterName = "p_tipo_salario";
                        p_tipo_salario.Value = pNomina.tipo_salario;
                        p_tipo_salario.Direction = ParameterDirection.InputOutput;
                        
                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_Nombre);
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Ingreso);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_Salario);
                        cmdTransaccionFactory.Parameters.Add(p_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Incremento);
                        cmdTransaccionFactory.Parameters.Add(p_salario_nuevo);
                        cmdTransaccionFactory.Parameters.Add(p_aux_trans);
                        cmdTransaccionFactory.Parameters.Add(p_cumplimiento);
                        cmdTransaccionFactory.Parameters.Add(p_comisiones);
                        cmdTransaccionFactory.Parameters.Add(p_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_cesantias);
                        cmdTransaccionFactory.Parameters.Add(p_int_ces);
                        cmdTransaccionFactory.Parameters.Add(p_prima);
                        cmdTransaccionFactory.Parameters.Add(p_vacaciones);
                        cmdTransaccionFactory.Parameters.Add(p_dotacion);
                        cmdTransaccionFactory.Parameters.Add(p_salud);
                        cmdTransaccionFactory.Parameters.Add(p_pension);
                        cmdTransaccionFactory.Parameters.Add(p_arp);
                        cmdTransaccionFactory.Parameters.Add(p_caja_comp);
                        cmdTransaccionFactory.Parameters.Add(p_total);
                        cmdTransaccionFactory.Parameters.Add(p_aux_tel);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_salario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_EMPLEADO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Incremento.Value != null) pNomina.incremento = Convert.ToDecimal(p_Incremento.Value);
                        if (p_cumplimiento.Value != null) pNomina.cumplimiento = Convert.ToDecimal(p_cumplimiento.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_salario_nuevo.Value != null) pNomina.salario_nuevo = Convert.ToDecimal(p_salario_nuevo.Value);
                        if (p_aux_trans.Value != null) pNomina.aux_trans = Convert.ToDecimal(p_aux_trans.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_aux_gas.Value != null) pNomina.aux_gas = Convert.ToDecimal(p_aux_gas.Value);
                        if (p_cesantias.Value != null) pNomina.cesantias = Convert.ToDecimal(p_cesantias.Value);
                        if (p_int_ces.Value != null) pNomina.int_ces = Convert.ToDecimal(p_int_ces.Value);
                        if (p_prima.Value != null) pNomina.prima = Convert.ToDecimal(p_prima.Value);
                        if (p_vacaciones.Value != null) pNomina.vacaciones = Convert.ToDecimal(p_vacaciones.Value);
                        if (p_dotacion.Value != null) pNomina.dotacion = Convert.ToDecimal(p_dotacion.Value);
                        if (p_salud.Value != null) pNomina.salud = Convert.ToDecimal(p_salud.Value);
                        if (p_pension.Value != null) pNomina.pension = Convert.ToDecimal(p_pension.Value);
                        if (p_arp.Value != null) pNomina.arp = Convert.ToDecimal(p_arp.Value);
                        if (p_caja_comp.Value != null) pNomina.caja_comp = Convert.ToDecimal(p_caja_comp.Value);
                        if (p_total.Value != null) pNomina.total = Convert.ToDecimal(p_total.Value);
                        if (p_aux_tel.Value != null) pNomina.aux_tel = Convert.ToDecimal(p_aux_tel.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearEmpleado", ex);
                        return pNomina;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ModificarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pNomina.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_Nombre = cmdTransaccionFactory.CreateParameter();
                        p_Nombre.ParameterName = "p_Nombre";
                        p_Nombre.DbType = DbType.String;
                        p_Nombre.Value = pNomina.nombre;

                        DbParameter p_Fecha_Ingreso = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Ingreso.ParameterName = "p_Fecha_Ingreso";
                        p_Fecha_Ingreso.DbType = DbType.Date;
                        p_Fecha_Ingreso.Value = pNomina.fecha_ingreso;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pNomina.cod_oficina;

                        DbParameter p_Salario = cmdTransaccionFactory.CreateParameter();
                        p_Salario.ParameterName = "p_Salario";
                        p_Salario.DbType = DbType.Decimal;
                        p_Salario.Value = pNomina.salario;

                        DbParameter p_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Cargo.ParameterName = "p_Cargo";
                        p_Cargo.Value = pNomina.cargo;

                        DbParameter p_Incremento = cmdTransaccionFactory.CreateParameter();
                        p_Incremento.ParameterName = "p_incremento";
                        p_Incremento.Value = pNomina.incremento;
                        p_Incremento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_salario_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_salario_nuevo.ParameterName = "p_salario_nuevo";
                        p_salario_nuevo.Value = pNomina.salario_nuevo;
                        p_salario_nuevo.Direction = ParameterDirection.Output;

                        DbParameter p_aux_trans = cmdTransaccionFactory.CreateParameter();
                        p_aux_trans.ParameterName = "p_aux_trans";
                        p_aux_trans.Value = pNomina.aux_trans;
                        p_aux_trans.Direction = ParameterDirection.Output;

                        DbParameter p_cumplimiento = cmdTransaccionFactory.CreateParameter();
                        p_cumplimiento.ParameterName = "p_cumplimiento";
                        p_cumplimiento.Value = pNomina.cumplimiento;
                        p_cumplimiento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_comisiones = cmdTransaccionFactory.CreateParameter();
                        p_comisiones.ParameterName = "p_comisiones";
                        p_comisiones.Value = pNomina.comisiones;
                        p_comisiones.Direction = ParameterDirection.InputOutput;

                        DbParameter p_aux_gas = cmdTransaccionFactory.CreateParameter();
                        p_aux_gas.ParameterName = "p_aux_gas";
                        p_aux_gas.Value = pNomina.aux_gas;
                        p_aux_gas.Direction = ParameterDirection.InputOutput;

                        DbParameter p_cesantias = cmdTransaccionFactory.CreateParameter();
                        p_cesantias.ParameterName = "p_cesantias";
                        p_cesantias.Value = pNomina.cesantias;
                        p_cesantias.Direction = ParameterDirection.Output;

                        DbParameter p_int_ces = cmdTransaccionFactory.CreateParameter();
                        p_int_ces.ParameterName = "p_int_ces";
                        p_int_ces.Value = pNomina.int_ces;
                        p_int_ces.Direction = ParameterDirection.Output;

                        DbParameter p_prima = cmdTransaccionFactory.CreateParameter();
                        p_prima.ParameterName = "p_prima";
                        p_prima.Value = pNomina.prima;
                        p_prima.Direction = ParameterDirection.Output;

                        DbParameter p_vacaciones = cmdTransaccionFactory.CreateParameter();
                        p_vacaciones.ParameterName = "p_vacaciones";
                        p_vacaciones.Value = pNomina.vacaciones;
                        p_vacaciones.Direction = ParameterDirection.Output;

                        DbParameter p_dotacion = cmdTransaccionFactory.CreateParameter();
                        p_dotacion.ParameterName = "p_dotacion";
                        p_dotacion.Value = pNomina.dotacion;
                        p_dotacion.Direction = ParameterDirection.Output;

                        DbParameter p_salud = cmdTransaccionFactory.CreateParameter();
                        p_salud.ParameterName = "p_salud";
                        p_salud.Value = pNomina.salud;
                        p_salud.Direction = ParameterDirection.Output;

                        DbParameter p_pension = cmdTransaccionFactory.CreateParameter();
                        p_pension.ParameterName = "p_pension";
                        p_pension.Value = pNomina.pension;
                        p_pension.Direction = ParameterDirection.Output;

                        DbParameter p_arp = cmdTransaccionFactory.CreateParameter();
                        p_arp.ParameterName = "p_arp";
                        p_arp.Value = pNomina.arp;
                        p_arp.Direction = ParameterDirection.Output;

                        DbParameter p_caja_comp = cmdTransaccionFactory.CreateParameter();
                        p_caja_comp.ParameterName = "p_caja_comp";
                        p_caja_comp.Value = pNomina.caja_comp;
                        p_caja_comp.Direction = ParameterDirection.Output;

                        DbParameter p_total = cmdTransaccionFactory.CreateParameter();
                        p_total.ParameterName = "p_total";
                        p_total.Value = pNomina.total;
                        p_total.Direction = ParameterDirection.Output;

                        DbParameter p_aux_tel = cmdTransaccionFactory.CreateParameter();
                        p_aux_tel.ParameterName = "p_aux_tel";
                        p_aux_tel.Value = pNomina.aux_tel;
                        p_aux_tel.Direction = ParameterDirection.InputOutput;

                        DbParameter p_tipo_salario = cmdTransaccionFactory.CreateParameter();
                        p_tipo_salario.ParameterName = "p_tipo_salario";
                        p_tipo_salario.Value = pNomina.tipo_salario;
                        p_tipo_salario.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_Nombre);
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Ingreso);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_Salario);
                        cmdTransaccionFactory.Parameters.Add(p_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Incremento);
                        cmdTransaccionFactory.Parameters.Add(p_salario_nuevo);
                        cmdTransaccionFactory.Parameters.Add(p_aux_trans);
                        cmdTransaccionFactory.Parameters.Add(p_cumplimiento);
                        cmdTransaccionFactory.Parameters.Add(p_comisiones);
                        cmdTransaccionFactory.Parameters.Add(p_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_cesantias);
                        cmdTransaccionFactory.Parameters.Add(p_int_ces);
                        cmdTransaccionFactory.Parameters.Add(p_prima);
                        cmdTransaccionFactory.Parameters.Add(p_vacaciones);
                        cmdTransaccionFactory.Parameters.Add(p_dotacion);
                        cmdTransaccionFactory.Parameters.Add(p_salud);
                        cmdTransaccionFactory.Parameters.Add(p_pension);
                        cmdTransaccionFactory.Parameters.Add(p_arp);
                        cmdTransaccionFactory.Parameters.Add(p_caja_comp);
                        cmdTransaccionFactory.Parameters.Add(p_total);
                        cmdTransaccionFactory.Parameters.Add(p_aux_tel);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_salario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_EMPLEADO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Incremento.Value != null) pNomina.incremento = Convert.ToDecimal(p_Incremento.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_salario_nuevo.Value != null) pNomina.salario_nuevo = Convert.ToDecimal(p_salario_nuevo.Value);
                        if (p_aux_trans.Value != null) pNomina.aux_trans = Convert.ToDecimal(p_aux_trans.Value);
                        if (p_cumplimiento.Value != null) pNomina.cumplimiento = Convert.ToDecimal(p_cumplimiento.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_aux_gas.Value != null) pNomina.aux_gas = Convert.ToDecimal(p_aux_gas.Value);
                        if (p_cesantias.Value != null) pNomina.cesantias = Convert.ToDecimal(p_cesantias.Value);
                        if (p_int_ces.Value != null) pNomina.int_ces = Convert.ToDecimal(p_int_ces.Value);
                        if (p_prima.Value != null) pNomina.prima = Convert.ToDecimal(p_prima.Value);
                        if (p_vacaciones.Value != null) pNomina.vacaciones = Convert.ToDecimal(p_vacaciones.Value);
                        if (p_dotacion.Value != null) pNomina.dotacion = Convert.ToDecimal(p_dotacion.Value);
                        if (p_salud.Value != null) pNomina.salud = Convert.ToDecimal(p_salud.Value);
                        if (p_pension.Value != null) pNomina.pension = Convert.ToDecimal(p_pension.Value);
                        if (p_arp.Value != null) pNomina.arp = Convert.ToDecimal(p_arp.Value);
                        if (p_caja_comp.Value != null) pNomina.caja_comp = Convert.ToDecimal(p_caja_comp.Value);
                        if (p_total.Value != null) pNomina.total = Convert.ToDecimal(p_total.Value);
                        if (p_aux_tel.Value != null) pNomina.aux_tel = Convert.ToDecimal(p_aux_tel.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarEmpleado", ex);
                        return pNomina;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ActualizarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pNomina.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_Nombre = cmdTransaccionFactory.CreateParameter();
                        p_Nombre.ParameterName = "p_Nombre";
                        p_Nombre.DbType = DbType.String;
                        p_Nombre.Value = pNomina.nombre;

                        DbParameter p_Fecha_Ingreso = cmdTransaccionFactory.CreateParameter();
                        p_Fecha_Ingreso.ParameterName = "p_Fecha_Ingreso";
                        p_Fecha_Ingreso.DbType = DbType.Date;
                        p_Fecha_Ingreso.Value = pNomina.fecha_ingreso;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pNomina.cod_oficina;

                        DbParameter p_Salario = cmdTransaccionFactory.CreateParameter();
                        p_Salario.ParameterName = "p_Salario";
                        p_Salario.DbType = DbType.Decimal;
                        p_Salario.Value = pNomina.salario;

                        DbParameter p_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Cargo.ParameterName = "p_Cargo";
                        p_Cargo.Value = pNomina.cargo;

                        DbParameter p_Incremento = cmdTransaccionFactory.CreateParameter();
                        p_Incremento.ParameterName = "p_incremento";
                        p_Incremento.Value = pNomina.incremento;
                        p_Incremento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_salario_nuevo = cmdTransaccionFactory.CreateParameter();
                        p_salario_nuevo.ParameterName = "p_salario_nuevo";
                        p_salario_nuevo.Value = pNomina.salario_nuevo;
                        p_salario_nuevo.Direction = ParameterDirection.Output;

                        DbParameter p_aux_trans = cmdTransaccionFactory.CreateParameter();
                        p_aux_trans.ParameterName = "p_aux_trans";
                        p_aux_trans.Value = pNomina.aux_trans;
                        p_aux_trans.Direction = ParameterDirection.Output;

                        DbParameter p_cumplimiento = cmdTransaccionFactory.CreateParameter();
                        p_cumplimiento.ParameterName = "p_cumplimiento";
                        p_cumplimiento.Value = pNomina.cumplimiento;
                        p_cumplimiento.Direction = ParameterDirection.InputOutput;

                        DbParameter p_comisiones = cmdTransaccionFactory.CreateParameter();
                        p_comisiones.ParameterName = "p_comisiones";
                        p_comisiones.Value = pNomina.comisiones;
                        p_comisiones.Direction = ParameterDirection.InputOutput;

                        DbParameter p_aux_gas = cmdTransaccionFactory.CreateParameter();
                        p_aux_gas.ParameterName = "p_aux_gas";
                        p_aux_gas.Value = pNomina.aux_gas;
                        p_aux_gas.Direction = ParameterDirection.Output;

                        DbParameter p_cesantias = cmdTransaccionFactory.CreateParameter();
                        p_cesantias.ParameterName = "p_cesantias";
                        p_cesantias.Value = pNomina.cesantias;
                        p_cesantias.Direction = ParameterDirection.Output;

                        DbParameter p_int_ces = cmdTransaccionFactory.CreateParameter();
                        p_int_ces.ParameterName = "p_int_ces";
                        p_int_ces.Value = pNomina.int_ces;
                        p_int_ces.Direction = ParameterDirection.Output;

                        DbParameter p_prima = cmdTransaccionFactory.CreateParameter();
                        p_prima.ParameterName = "p_prima";
                        p_prima.Value = pNomina.prima;
                        p_prima.Direction = ParameterDirection.Output;

                        DbParameter p_vacaciones = cmdTransaccionFactory.CreateParameter();
                        p_vacaciones.ParameterName = "p_vacaciones";
                        p_vacaciones.Value = pNomina.vacaciones;
                        p_vacaciones.Direction = ParameterDirection.Output;

                        DbParameter p_dotacion = cmdTransaccionFactory.CreateParameter();
                        p_dotacion.ParameterName = "p_dotacion";
                        p_dotacion.Value = pNomina.dotacion;
                        p_dotacion.Direction = ParameterDirection.Output;

                        DbParameter p_salud = cmdTransaccionFactory.CreateParameter();
                        p_salud.ParameterName = "p_salud";
                        p_salud.Value = pNomina.salud;
                        p_salud.Direction = ParameterDirection.Output;

                        DbParameter p_pension = cmdTransaccionFactory.CreateParameter();
                        p_pension.ParameterName = "p_pension";
                        p_pension.Value = pNomina.pension;
                        p_pension.Direction = ParameterDirection.Output;

                        DbParameter p_arp = cmdTransaccionFactory.CreateParameter();
                        p_arp.ParameterName = "p_arp";
                        p_arp.Value = pNomina.arp;
                        p_arp.Direction = ParameterDirection.Output;

                        DbParameter p_caja_comp = cmdTransaccionFactory.CreateParameter();
                        p_caja_comp.ParameterName = "p_caja_comp";
                        p_caja_comp.Value = pNomina.caja_comp;
                        p_caja_comp.Direction = ParameterDirection.Output;

                        DbParameter p_total = cmdTransaccionFactory.CreateParameter();
                        p_total.ParameterName = "p_total";
                        p_total.Value = pNomina.total;
                        p_total.Direction = ParameterDirection.Output;

                        DbParameter p_aux_tel = cmdTransaccionFactory.CreateParameter();
                        p_aux_tel.ParameterName = "p_aux_tel";
                        p_aux_tel.Value = pNomina.aux_tel;
                        p_aux_tel.Direction = ParameterDirection.Output;

                        DbParameter p_tipo_salario = cmdTransaccionFactory.CreateParameter();
                        p_tipo_salario.ParameterName = "p_tipo_salario";
                        p_tipo_salario.Value = pNomina.tipo_salario;
                        p_tipo_salario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_Nombre);
                        cmdTransaccionFactory.Parameters.Add(p_Fecha_Ingreso);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_Salario);
                        cmdTransaccionFactory.Parameters.Add(p_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Incremento);
                        cmdTransaccionFactory.Parameters.Add(p_salario_nuevo);
                        cmdTransaccionFactory.Parameters.Add(p_aux_trans);
                        cmdTransaccionFactory.Parameters.Add(p_cumplimiento);
                        cmdTransaccionFactory.Parameters.Add(p_comisiones);
                        cmdTransaccionFactory.Parameters.Add(p_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_cesantias);
                        cmdTransaccionFactory.Parameters.Add(p_int_ces);
                        cmdTransaccionFactory.Parameters.Add(p_prima);
                        cmdTransaccionFactory.Parameters.Add(p_vacaciones);
                        cmdTransaccionFactory.Parameters.Add(p_dotacion);
                        cmdTransaccionFactory.Parameters.Add(p_salud);
                        cmdTransaccionFactory.Parameters.Add(p_pension);
                        cmdTransaccionFactory.Parameters.Add(p_arp);
                        cmdTransaccionFactory.Parameters.Add(p_caja_comp);
                        cmdTransaccionFactory.Parameters.Add(p_total);
                        cmdTransaccionFactory.Parameters.Add(p_aux_tel);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_salario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_EMPLEADO_ACT";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (p_Incremento.Value != null) pNomina.incremento = Convert.ToDecimal(p_Incremento.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_salario_nuevo.Value != null) pNomina.salario_nuevo = Convert.ToDecimal(p_salario_nuevo.Value);
                        if (p_aux_trans.Value != null) pNomina.aux_trans = Convert.ToDecimal(p_aux_trans.Value);
                        if (p_cumplimiento.Value != null) pNomina.cumplimiento = Convert.ToDecimal(p_cumplimiento.Value);
                        if (p_comisiones.Value != null) pNomina.comisiones = Convert.ToDecimal(p_comisiones.Value);
                        if (p_aux_gas.Value != null) pNomina.aux_gas = Convert.ToDecimal(p_aux_gas.Value);
                        if (p_cesantias.Value != null) pNomina.cesantias = Convert.ToDecimal(p_cesantias.Value);
                        if (p_int_ces.Value != null) pNomina.int_ces = Convert.ToDecimal(p_int_ces.Value);
                        if (p_prima.Value != null) pNomina.prima = Convert.ToDecimal(p_prima.Value);
                        if (p_vacaciones.Value != null) pNomina.vacaciones = Convert.ToDecimal(p_vacaciones.Value);
                        if (p_dotacion.Value != null) pNomina.dotacion = Convert.ToDecimal(p_dotacion.Value);
                        if (p_salud.Value != null) pNomina.salud = Convert.ToDecimal(p_salud.Value);
                        if (p_pension.Value != null) pNomina.pension = Convert.ToDecimal(p_pension.Value);
                        if (p_arp.Value != null) pNomina.arp = Convert.ToDecimal(p_arp.Value);
                        if (p_caja_comp.Value != null) pNomina.caja_comp = Convert.ToDecimal(p_caja_comp.Value);
                        if (p_total.Value != null) pNomina.total = Convert.ToDecimal(p_total.Value);
                        if (p_aux_tel.Value != null) pNomina.aux_tel = Convert.ToDecimal(p_aux_tel.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pNomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarEmpleado", ex);
                        return pNomina;
                    }
                }
            }
        }

        public void EliminarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pNomina.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_EMPLEADO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarEmpleado", ex);
                        return;
                    }
                }
            }
        }

        public DataTable ListarCargos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtCargos = new DataTable();
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT cod_cargo, nom_cargo, comision_colocacion_ant, incremento_colocacion, comision_colocacion, incremento_cartera, comision_cartera, comision_cartera_ant, aux_gas_ant, incremento_aux_gas, aux_gas, aux_tel_ant, incremento_aux_tel, aux_tel FROM pre_cargo ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtCargos.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtCargos, ref Error);

                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);

                        return dtCargos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarCargos", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos CrearCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pCargos.comision_colocacion = (pCargos.comision_colocacion_ant * (1 + pCargos.incremento_colocacion/100));
                        pCargos.comision_cartera = (pCargos.comision_cartera_ant * (1 + pCargos.incremento_cartera / 100));
                        pCargos.aux_tel = (pCargos.aux_tel_ant * (1 + pCargos.incremento_aux_tel / 100));
                        pCargos.aux_gas = (pCargos.aux_gas_ant * (1 + pCargos.incremento_aux_gas / 100));

                        DbParameter p_Cod_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Cargo.ParameterName = "p_Cod_Cargo";
                        p_Cod_Cargo.DbType = DbType.Int64;
                        p_Cod_Cargo.Value = pCargos.cod_cargo;
                        p_Cod_Cargo.Direction = ParameterDirection.Input;

                        DbParameter p_Nom_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Nom_Cargo.ParameterName = "p_Nom_Cargo";
                        p_Nom_Cargo.DbType = DbType.String;
                        p_Nom_Cargo.Value = pCargos.nom_cargo;

                        DbParameter p_Comision_Colocacion_ant = cmdTransaccionFactory.CreateParameter();
                        p_Comision_Colocacion_ant.ParameterName = "p_Comision_Colocacion_ant";
                        p_Comision_Colocacion_ant.DbType = DbType.Decimal;
                        p_Comision_Colocacion_ant.Value = pCargos.comision_colocacion_ant;

                        DbParameter p_Comision_Cartera_ant = cmdTransaccionFactory.CreateParameter();
                        p_Comision_Cartera_ant.ParameterName = "p_Comision_Cartera_ant";
                        p_Comision_Cartera_ant.DbType = DbType.Decimal;
                        p_Comision_Cartera_ant.Value = pCargos.comision_cartera_ant;

                        DbParameter p_Aux_Gas_ant = cmdTransaccionFactory.CreateParameter();
                        p_Aux_Gas_ant.ParameterName = "p_Aux_Gas_ant";
                        p_Aux_Gas_ant.DbType = DbType.Decimal;
                        p_Aux_Gas_ant.Value = pCargos.aux_gas_ant;

                        DbParameter p_Aux_Tel_ant = cmdTransaccionFactory.CreateParameter();
                        p_Aux_Tel_ant.ParameterName = "p_Aux_Tel_ant";
                        p_Aux_Tel_ant.DbType = DbType.Decimal;
                        p_Aux_Tel_ant.Value = pCargos.aux_tel_ant;

                        DbParameter p_incremento_colocacion = cmdTransaccionFactory.CreateParameter();
                        p_incremento_colocacion.ParameterName = "p_incremento_colocacion";
                        p_incremento_colocacion.DbType = DbType.Decimal;
                        p_incremento_colocacion.Value = pCargos.incremento_colocacion;

                        DbParameter p_incremento_cartera = cmdTransaccionFactory.CreateParameter();
                        p_incremento_cartera.ParameterName = "p_incremento_cartera";
                        p_incremento_cartera.DbType = DbType.Decimal;
                        p_incremento_cartera.Value = pCargos.incremento_cartera;

                        DbParameter p_incremento_aux_gas = cmdTransaccionFactory.CreateParameter();
                        p_incremento_aux_gas.ParameterName = "p_incremento_aux_gas";
                        p_incremento_aux_gas.DbType = DbType.Decimal;
                        p_incremento_aux_gas.Value = pCargos.incremento_aux_gas;

                        DbParameter p_incremento_aux_tel = cmdTransaccionFactory.CreateParameter();
                        p_incremento_aux_tel.ParameterName = "p_incremento_aux_tel";
                        p_incremento_aux_tel.DbType = DbType.Decimal;
                        p_incremento_aux_tel.Value = pCargos.incremento_aux_tel;

                        DbParameter p_Comision_Colocacion = cmdTransaccionFactory.CreateParameter();
                        p_Comision_Colocacion.ParameterName = "p_Comision_Colocacion";
                        p_Comision_Colocacion.DbType = DbType.Decimal;
                        p_Comision_Colocacion.Value = pCargos.comision_colocacion;
                        p_Comision_Colocacion.Direction = ParameterDirection.InputOutput;

                        DbParameter p_Comision_Cartera = cmdTransaccionFactory.CreateParameter();
                        p_Comision_Cartera.ParameterName = "p_Comision_Cartera";
                        p_Comision_Cartera.DbType = DbType.Decimal;
                        p_Comision_Cartera.Value = pCargos.comision_cartera;
                        p_Comision_Cartera.Direction = ParameterDirection.InputOutput;

                        DbParameter p_Aux_Gas = cmdTransaccionFactory.CreateParameter();
                        p_Aux_Gas.ParameterName = "p_Aux_Gas";
                        p_Aux_Gas.DbType = DbType.Decimal;
                        p_Aux_Gas.Value = pCargos.aux_gas;
                        p_Aux_Gas.Direction = ParameterDirection.InputOutput;

                        DbParameter p_Aux_Tel = cmdTransaccionFactory.CreateParameter();
                        p_Aux_Tel.ParameterName = "p_Aux_Tel";
                        p_Aux_Tel.DbType = DbType.Decimal;
                        p_Aux_Tel.Value = pCargos.aux_tel;
                        p_Aux_Tel.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(p_Cod_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Nom_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Comision_Colocacion_ant);
                        cmdTransaccionFactory.Parameters.Add(p_Comision_Cartera_ant);
                        cmdTransaccionFactory.Parameters.Add(p_Aux_Gas_ant);
                        cmdTransaccionFactory.Parameters.Add(p_Aux_Tel_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_colocacion);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_cartera);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_aux_tel);
                        cmdTransaccionFactory.Parameters.Add(p_Comision_Colocacion);
                        cmdTransaccionFactory.Parameters.Add(p_Comision_Cartera);
                        cmdTransaccionFactory.Parameters.Add(p_Aux_Gas);
                        cmdTransaccionFactory.Parameters.Add(p_Aux_Tel);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_CARGO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCargos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearCargo", ex);
                        return pCargos;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ModificarCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        pCargos.comision_colocacion = (pCargos.comision_colocacion_ant * (1 + pCargos.incremento_colocacion / 100));
                        pCargos.comision_cartera = (pCargos.comision_cartera_ant * (1 + pCargos.incremento_cartera / 100));
                        pCargos.aux_tel = (pCargos.aux_tel_ant * (1 + pCargos.incremento_aux_tel / 100));
                        pCargos.aux_gas = (pCargos.aux_gas_ant * (1 + pCargos.incremento_aux_gas / 100));

                        DbParameter p_Cod_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Cargo.ParameterName = "p_Cod_Cargo";
                        p_Cod_Cargo.DbType = DbType.Int64;
                        p_Cod_Cargo.Value = pCargos.cod_cargo;
                        p_Cod_Cargo.Direction = ParameterDirection.Input;

                        DbParameter p_Nom_Cargo = cmdTransaccionFactory.CreateParameter();
                        p_Nom_Cargo.ParameterName = "p_Nom_Cargo";
                        p_Nom_Cargo.DbType = DbType.String;
                        p_Nom_Cargo.Value = pCargos.nom_cargo;

                        DbParameter p_comision_colocacion_ant = cmdTransaccionFactory.CreateParameter();
                        p_comision_colocacion_ant.ParameterName = "p_comision_colocacion_ant";
                        p_comision_colocacion_ant.DbType = DbType.Decimal;
                        p_comision_colocacion_ant.Value = pCargos.comision_colocacion_ant;

                        DbParameter p_comision_cartera_ant = cmdTransaccionFactory.CreateParameter();
                        p_comision_cartera_ant.ParameterName = "p_comision_cartera_ant";
                        p_comision_cartera_ant.DbType = DbType.Decimal;
                        p_comision_cartera_ant.Value = pCargos.comision_cartera_ant;

                        DbParameter p_aux_gas_ant = cmdTransaccionFactory.CreateParameter();
                        p_aux_gas_ant.ParameterName = "p_aux_gas_ant";
                        p_aux_gas_ant.DbType = DbType.Decimal;
                        p_aux_gas_ant.Value = pCargos.aux_gas_ant;

                        DbParameter p_aux_tel_ant = cmdTransaccionFactory.CreateParameter();
                        p_aux_tel_ant.ParameterName = "p_aux_tel_ant";
                        p_aux_tel_ant.DbType = DbType.Decimal;
                        p_aux_tel_ant.Value = pCargos.aux_tel_ant;

                        DbParameter p_incremento_colocacion = cmdTransaccionFactory.CreateParameter();
                        p_incremento_colocacion.ParameterName = "p_incremento_colocacion";
                        p_incremento_colocacion.DbType = DbType.Decimal;
                        p_incremento_colocacion.Value = pCargos.incremento_colocacion;

                        DbParameter p_incremento_cartera = cmdTransaccionFactory.CreateParameter();
                        p_incremento_cartera.ParameterName = "p_incremento_cartera";
                        p_incremento_cartera.DbType = DbType.Decimal;
                        p_incremento_cartera.Value = pCargos.incremento_cartera;

                        DbParameter p_incremento_aux_gas = cmdTransaccionFactory.CreateParameter();
                        p_incremento_aux_gas.ParameterName = "p_incremento_aux_gas";
                        p_incremento_aux_gas.DbType = DbType.Decimal;
                        p_incremento_aux_gas.Value = pCargos.incremento_aux_gas;

                        DbParameter p_incremento_aux_tel = cmdTransaccionFactory.CreateParameter();
                        p_incremento_aux_tel.ParameterName = "p_incremento_aux_tel";
                        p_incremento_aux_tel.DbType = DbType.Decimal;
                        p_incremento_aux_tel.Value = pCargos.incremento_aux_tel;

                        DbParameter p_comision_colocacion = cmdTransaccionFactory.CreateParameter();
                        p_comision_colocacion.ParameterName = "p_comision_colocacion";
                        p_comision_colocacion.DbType = DbType.Decimal;
                        p_comision_colocacion.Value = pCargos.comision_colocacion;
                        p_comision_colocacion.Direction = ParameterDirection.InputOutput;

                        DbParameter p_comision_cartera = cmdTransaccionFactory.CreateParameter();
                        p_comision_cartera.ParameterName = "p_comision_cartera";
                        p_comision_cartera.DbType = DbType.Decimal;
                        p_comision_cartera.Value = pCargos.comision_cartera;
                        p_comision_cartera.Direction = ParameterDirection.InputOutput;

                        DbParameter p_aux_gas = cmdTransaccionFactory.CreateParameter();
                        p_aux_gas.ParameterName = "p_aux_gas";
                        p_aux_gas.DbType = DbType.Decimal;
                        p_aux_gas.Value = pCargos.aux_gas;
                        p_aux_gas.Direction = ParameterDirection.InputOutput;

                        DbParameter p_aux_tel = cmdTransaccionFactory.CreateParameter();
                        p_aux_tel.ParameterName = "p_aux_tel";
                        p_aux_tel.DbType = DbType.Decimal;
                        p_aux_tel.Value = pCargos.aux_tel;
                        p_aux_tel.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(p_Cod_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_Nom_Cargo);
                        cmdTransaccionFactory.Parameters.Add(p_comision_colocacion_ant);
                        cmdTransaccionFactory.Parameters.Add(p_comision_cartera_ant);
                        cmdTransaccionFactory.Parameters.Add(p_aux_gas_ant);
                        cmdTransaccionFactory.Parameters.Add(p_aux_tel_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_colocacion);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_cartera);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_aux_tel);
                        cmdTransaccionFactory.Parameters.Add(p_comision_colocacion);
                        cmdTransaccionFactory.Parameters.Add(p_comision_cartera);
                        cmdTransaccionFactory.Parameters.Add(p_aux_gas);
                        cmdTransaccionFactory.Parameters.Add(p_aux_tel);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_CARGO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCargos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarCargo", ex);
                        return pCargos;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ConsultarCargo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Xpinn.Presupuesto.Entities.Cargos entidad = new Xpinn.Presupuesto.Entities.Cargos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "SELECT p.cod_cargo, p.nom_cargo, p.comision_colocacion, p.comision_cartera, p.aux_gas, p.aux_tel, p.comision_colocacion_ant, p.comision_cartera_ant, p.aux_gas_ant, p.aux_tel_ant FROM pre_cargo p " +
                                     " WHERE p.cod_cargo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_cargo"] != DBNull.Value) entidad.cod_cargo = Convert.ToInt64(resultado["cod_cargo"]);
                            if (resultado["nom_cargo"] != DBNull.Value) entidad.nom_cargo = Convert.ToString(resultado["nom_cargo"]);
                            if (resultado["comision_colocacion"] != DBNull.Value) entidad.comision_colocacion = Convert.ToDecimal(resultado["comision_colocacion"]);
                            if (resultado["comision_cartera"] != DBNull.Value) entidad.comision_cartera = Convert.ToDecimal(resultado["comision_cartera"]);
                            if (resultado["aux_gas"] != DBNull.Value) entidad.aux_gas = Convert.ToDecimal(resultado["aux_gas"]);
                            if (resultado["aux_tel"] != DBNull.Value) entidad.aux_tel = Convert.ToDecimal(resultado["aux_tel"]);
                            if (resultado["incremento_colocacion"] != DBNull.Value) entidad.incremento_colocacion = Convert.ToDecimal(resultado["incremento_colocacion"]);
                            if (resultado["incremento_cartera"] != DBNull.Value) entidad.incremento_cartera = Convert.ToDecimal(resultado["incremento_cartera"]);
                            if (resultado["incremento_aux_gas"] != DBNull.Value) entidad.incremento_aux_gas = Convert.ToDecimal(resultado["incremento_aux_gas"]);
                            if (resultado["incremento_aux_tel"] != DBNull.Value) entidad.incremento_aux_tel = Convert.ToDecimal(resultado["incremento_aux_tel"]);
                            if (resultado["comision_colocacion_ant"] != DBNull.Value) entidad.comision_colocacion_ant = Convert.ToDecimal(resultado["comision_colocacion_ant"]);
                            if (resultado["comision_cartera_ant"] != DBNull.Value) entidad.comision_cartera_ant = Convert.ToDecimal(resultado["comision_cartera_ant"]);
                            if (resultado["aux_gas_ant"] != DBNull.Value) entidad.aux_gas_ant = Convert.ToDecimal(resultado["aux_gas_ant"]);
                            if (resultado["aux_tel_ant"] != DBNull.Value) entidad.aux_tel_ant = Convert.ToDecimal(resultado["aux_tel_ant"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarCargo", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Nomina

        #region ActivosFijos

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ACTIVOS FIJOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataTable ListarActivosFijos(DateTime pFechaCorte, ref string Error, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtActivosFij = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.CODIGO, e.DESCRIPCION, Nvl(e.COD_OFICINA, 1) As COD_OFICINA, e.VRCOMPRA, e.FECHA_COMPRA, e.TIPO_ACTIVO, " +
                                     "Case e.TIPO_ACTIVO WHEN 1 THEN 'EDIFICACIONES' WHEN 2 THEN 'MUEBLES Y EQUIPOS DE OFICINA' WHEN 3 THEN 'EQUIPO DE COMPUTO Y COMUNICACION' WHEN 4 THEN 'VEHICULOS' End AS NOM_TIPO_ACTIVO, o.NOMBRE as OFICINA " +
                                     "From pre_activos_fijos e Left Join oficina o On e.cod_oficina = o.cod_oficina Order by e.CODIGO ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtActivosFij.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtActivosFij, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtActivosFij;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarActivosFijos", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos CrearActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pActivosFijos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pActivosFijos.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pActivosFijos.cod_oficina;

                        DbParameter p_vrcompra = cmdTransaccionFactory.CreateParameter();
                        p_vrcompra.ParameterName = "p_vrcompra";
                        p_vrcompra.DbType = DbType.Decimal;
                        p_vrcompra.Value = pActivosFijos.vrcompra;

                        DbParameter p_fecha_compra = cmdTransaccionFactory.CreateParameter();
                        p_fecha_compra.ParameterName = "p_fecha_compra";
                        p_fecha_compra.DbType = DbType.Date;
                        p_fecha_compra.Value = pActivosFijos.fecha_compra;

                        DbParameter p_tipo_activo = cmdTransaccionFactory.CreateParameter();
                        p_tipo_activo.ParameterName = "p_tipo_activo";
                        p_tipo_activo.DbType = DbType.Int64;
                        p_tipo_activo.Value = pActivosFijos.tipo_activo;
  
                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_vrcompra);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_compra);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_activo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_ACTFIJO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearActivoFijo", ex);
                        return pActivosFijos;
                    }
                }
            }
        }

        public void EliminarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pActivosFijos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_ACTFIJO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarActivoFijo", ex);
                        return;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos ModificarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pActivosFijos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pActivosFijos.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pActivosFijos.cod_oficina;

                        DbParameter p_vrcompra = cmdTransaccionFactory.CreateParameter();
                        p_vrcompra.ParameterName = "p_vrcompra";
                        p_vrcompra.DbType = DbType.Decimal;
                        p_vrcompra.Value = pActivosFijos.vrcompra;

                        DbParameter p_fecha_compra = cmdTransaccionFactory.CreateParameter();
                        p_fecha_compra.ParameterName = "p_fecha_compra";
                        p_fecha_compra.DbType = DbType.Date;
                        p_fecha_compra.Value = pActivosFijos.fecha_compra;

                        DbParameter p_tipo_activo = cmdTransaccionFactory.CreateParameter();
                        p_tipo_activo.ParameterName = "p_tipo_activo";
                        p_tipo_activo.DbType = DbType.Int64;
                        p_tipo_activo.Value = pActivosFijos.tipo_activo;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_vrcompra);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_compra);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_activo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_ACTFIJO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pActivosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarActivoFijo", ex);
                        return pActivosFijos;
                    }
                }
            }
        }

        #endregion ActivosFijos

        #region Diferidos

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// DIFERIDOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Método para crear los diferidos
        /// </summary>
        /// <param name="pDiferidos"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Xpinn.Presupuesto.Entities.Diferidos CrearDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pDiferidos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pDiferidos.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pDiferidos.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pDiferidos.valor;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.DbType = DbType.Decimal;
                        p_plazo.Value = pDiferidos.plazo;

                        DbParameter p_fecha_diferido = cmdTransaccionFactory.CreateParameter();
                        p_fecha_diferido.ParameterName = "p_fecha_diferido";
                        p_fecha_diferido.DbType = DbType.DateTime;
                        p_fecha_diferido.Value = pDiferidos.fecha_diferido;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_diferido);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_DIFERIDO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDiferidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDiferido", ex);
                        return pDiferidos;
                    }
                }
            }
        }

        public void EliminarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pDiferidos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_DIFERIDO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarDiferido", ex);
                        return;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Diferidos ModificarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pDiferidos.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pDiferidos.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pDiferidos.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pDiferidos.valor;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.DbType = DbType.Decimal;
                        p_plazo.Value = pDiferidos.plazo;

                        DbParameter p_fecha_diferido = cmdTransaccionFactory.CreateParameter();
                        p_fecha_diferido.ParameterName = "p_fecha_diferido";
                        p_fecha_diferido.DbType = DbType.DateTime;
                        p_fecha_diferido.Value = pDiferidos.fecha_diferido;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_diferido);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_DIFERIDO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pDiferidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarDiferido", ex);
                        return pDiferidos;
                    }
                }
            }
        }

        public DataTable ListarDiferidos(DateTime pFechaCorte, ref string Error, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtDiferidos = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.CODIGO, e.DESCRIPCION, e.COD_OFICINA, e.VALOR, e.PLAZO, e.FECHA_DIFERIDO, o.NOMBRE as OFICINA " +
                                     " From pre_diferidos e Left Join oficina o On e.cod_oficina = o.cod_oficina Order by e.CODIGO ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtDiferidos.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtDiferidos, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtDiferidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarDiferidos", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Diferidos

        #region Otros

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// OTROS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Xpinn.Presupuesto.Entities.Otros CrearOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pOtros.arriendo = pOtros.arriendo_ant * (1 + (pOtros.incremento_arriendo / 100));
                        pOtros.servicios = pOtros.servicios_ant * (1 + (pOtros.incremento_servicios / 100));
                        pOtros.vigilancia = pOtros.vigilancia_ant * (1 + (pOtros.incremento_vigilancia / 100));

                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pOtros.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pOtros.descripcion;

                        DbParameter p_arriendo_ant = cmdTransaccionFactory.CreateParameter();
                        p_arriendo_ant.ParameterName = "p_arriendo_ant";
                        p_arriendo_ant.DbType = DbType.Decimal;
                        p_arriendo_ant.Value = pOtros.arriendo_ant;

                        DbParameter p_servicios_ant = cmdTransaccionFactory.CreateParameter();
                        p_servicios_ant.ParameterName = "p_servicios_ant";
                        p_servicios_ant.DbType = DbType.Decimal;
                        p_servicios_ant.Value = pOtros.servicios_ant;

                        DbParameter p_vigilancia_ant = cmdTransaccionFactory.CreateParameter();
                        p_vigilancia_ant.ParameterName = "p_vigilancia_ant";
                        p_vigilancia_ant.DbType = DbType.Decimal;
                        p_vigilancia_ant.Value = pOtros.vigilancia_ant;

                        DbParameter p_incremento_arriendo = cmdTransaccionFactory.CreateParameter();
                        p_incremento_arriendo.ParameterName = "p_incremento_arriendo";
                        p_incremento_arriendo.DbType = DbType.Decimal;
                        p_incremento_arriendo.Value = pOtros.incremento_arriendo;

                        DbParameter p_incremento_servicios = cmdTransaccionFactory.CreateParameter();
                        p_incremento_servicios.ParameterName = "p_incremento_servicios";
                        p_incremento_servicios.DbType = DbType.Decimal;
                        p_incremento_servicios.Value = pOtros.incremento_servicios;

                        DbParameter p_incremento_vigilancia = cmdTransaccionFactory.CreateParameter();
                        p_incremento_vigilancia.ParameterName = "p_incremento_vigilancia";
                        p_incremento_vigilancia.DbType = DbType.Decimal;
                        p_incremento_vigilancia.Value = pOtros.incremento_vigilancia;

                        DbParameter p_arriendo = cmdTransaccionFactory.CreateParameter();
                        p_arriendo.ParameterName = "p_arriendo";
                        p_arriendo.DbType = DbType.Decimal;
                        p_arriendo.Value = pOtros.arriendo;

                        DbParameter p_servicios = cmdTransaccionFactory.CreateParameter();
                        p_servicios.ParameterName = "p_servicios";
                        p_servicios.DbType = DbType.Decimal;
                        p_servicios.Value = pOtros.servicios;

                        DbParameter p_vigilancia = cmdTransaccionFactory.CreateParameter();
                        p_vigilancia.ParameterName = "p_vigilancia";
                        p_vigilancia.DbType = DbType.Decimal;
                        p_vigilancia.Value = pOtros.vigilancia;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_arriendo_ant);
                        cmdTransaccionFactory.Parameters.Add(p_servicios_ant);
                        cmdTransaccionFactory.Parameters.Add(p_vigilancia_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_arriendo);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_servicios);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_vigilancia);
                        cmdTransaccionFactory.Parameters.Add(p_arriendo);
                        cmdTransaccionFactory.Parameters.Add(p_servicios);
                        cmdTransaccionFactory.Parameters.Add(p_vigilancia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OTROS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pOtros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearOtro", ex);
                        return pOtros;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Otros ModificarOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pOtros.arriendo = pOtros.arriendo_ant * (1 + (pOtros.incremento_arriendo / 100));
                        pOtros.servicios = pOtros.servicios_ant * (1 + (pOtros.incremento_servicios / 100));
                        pOtros.vigilancia = pOtros.vigilancia_ant * (1 + (pOtros.incremento_vigilancia / 100));

                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pOtros.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pOtros.descripcion;

                        DbParameter p_arriendo_ant = cmdTransaccionFactory.CreateParameter();
                        p_arriendo_ant.ParameterName = "p_arriendo_ant";
                        p_arriendo_ant.DbType = DbType.Decimal;
                        p_arriendo_ant.Value = pOtros.arriendo_ant;

                        DbParameter p_servicios_ant = cmdTransaccionFactory.CreateParameter();
                        p_servicios_ant.ParameterName = "p_servicios_ant";
                        p_servicios_ant.DbType = DbType.Decimal;
                        p_servicios_ant.Value = pOtros.servicios_ant;

                        DbParameter p_vigilancia_ant = cmdTransaccionFactory.CreateParameter();
                        p_vigilancia_ant.ParameterName = "p_vigilancia_ant";
                        p_vigilancia_ant.DbType = DbType.Decimal;
                        p_vigilancia_ant.Value = pOtros.vigilancia_ant;

                        DbParameter p_incremento_arriendo = cmdTransaccionFactory.CreateParameter();
                        p_incremento_arriendo.ParameterName = "p_incremento_arriendo";
                        p_incremento_arriendo.DbType = DbType.Decimal;
                        p_incremento_arriendo.Value = pOtros.incremento_arriendo;

                        DbParameter p_incremento_servicios = cmdTransaccionFactory.CreateParameter();
                        p_incremento_servicios.ParameterName = "p_incremento_servicios";
                        p_incremento_servicios.DbType = DbType.Decimal;
                        p_incremento_servicios.Value = pOtros.incremento_servicios;

                        DbParameter p_incremento_vigilancia = cmdTransaccionFactory.CreateParameter();
                        p_incremento_vigilancia.ParameterName = "p_incremento_vigilancia";
                        p_incremento_vigilancia.DbType = DbType.Decimal;
                        p_incremento_vigilancia.Value = pOtros.incremento_vigilancia;

                        DbParameter p_arriendo = cmdTransaccionFactory.CreateParameter();
                        p_arriendo.ParameterName = "p_arriendo";
                        p_arriendo.DbType = DbType.Decimal;
                        p_arriendo.Value = pOtros.arriendo;

                        DbParameter p_servicios = cmdTransaccionFactory.CreateParameter();
                        p_servicios.ParameterName = "p_servicios";
                        p_servicios.DbType = DbType.Decimal;
                        p_servicios.Value = pOtros.servicios;

                        DbParameter p_vigilancia = cmdTransaccionFactory.CreateParameter();
                        p_vigilancia.ParameterName = "p_vigilancia";
                        p_vigilancia.DbType = DbType.Decimal;
                        p_vigilancia.Value = pOtros.vigilancia;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_arriendo_ant);
                        cmdTransaccionFactory.Parameters.Add(p_servicios_ant);
                        cmdTransaccionFactory.Parameters.Add(p_vigilancia_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_arriendo);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_servicios);
                        cmdTransaccionFactory.Parameters.Add(p_incremento_vigilancia);
                        cmdTransaccionFactory.Parameters.Add(p_arriendo);
                        cmdTransaccionFactory.Parameters.Add(p_servicios);
                        cmdTransaccionFactory.Parameters.Add(p_vigilancia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OTROS_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pOtros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarOtro", ex);
                        return pOtros;
                    }
                }
            }
        }

        public DataTable ListarOtros(DateTime pFechaCorte, ref string Error, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtOtros = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select o.COD_OFICINA AS CODIGO, o.NOMBRE AS DESCRIPCION, e.ARRIENDO_ANT, e.SERVICIOS_ANT, e.VIGILANCIA_ANT, e.INCREMENTO_ARRIENDO, e.INCREMENTO_SERVICIOS, e.INCREMENTO_VIGILANCIA, e.ARRIENDO, e.SERVICIOS, e.VIGILANCIA" +
                                     " From oficina o Left Join pre_otros e On e.cod_oficina = o.cod_oficina Order by o.COD_OFICINA ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtOtros.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtOtros, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtOtros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarOtros", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Otros

        #region Honorarios

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// HONORARIOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Xpinn.Presupuesto.Entities.Honorarios CrearHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pHonorarios.valor = pHonorarios.valor_ant * (1 + pHonorarios.incremento/100);

                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pHonorarios.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pHonorarios.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int32;
                        p_Cod_Oficina.Value = pHonorarios.cod_oficina;

                        DbParameter p_valor_ant = cmdTransaccionFactory.CreateParameter();
                        p_valor_ant.ParameterName = "p_valor_ant";
                        p_valor_ant.DbType = DbType.Decimal;
                        p_valor_ant.Value = pHonorarios.valor_ant;

                        DbParameter p_incremento = cmdTransaccionFactory.CreateParameter();
                        p_incremento.ParameterName = "p_incremento";
                        p_incremento.DbType = DbType.Decimal;
                        p_incremento.Value = pHonorarios.incremento;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pHonorarios.valor;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento);
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_HONORARIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pHonorarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearHonorario", ex);
                        return pHonorarios;
                    }
                }
            }
        }

        public void EliminarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pHonorarios.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_HONORARIO_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarHonorario", ex);
                        return;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Honorarios ModificarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        pHonorarios.valor = pHonorarios.valor_ant * (1 + pHonorarios.incremento/100);

                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pHonorarios.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pHonorarios.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int32;
                        p_Cod_Oficina.Value = pHonorarios.cod_oficina;

                        DbParameter p_valor_ant = cmdTransaccionFactory.CreateParameter();
                        p_valor_ant.ParameterName = "p_valor_ant";
                        p_valor_ant.DbType = DbType.Decimal;
                        p_valor_ant.Value = pHonorarios.valor_ant;

                        DbParameter p_incremento = cmdTransaccionFactory.CreateParameter();
                        p_incremento.ParameterName = "p_incremento";
                        p_incremento.DbType = DbType.Decimal;
                        p_incremento.Value = pHonorarios.incremento;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pHonorarios.valor;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor_ant);
                        cmdTransaccionFactory.Parameters.Add(p_incremento);
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_HONORARIO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pHonorarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarHonorario", ex);
                        return pHonorarios;
                    }
                }
            }
        }

        public DataTable ListarHonorarios(DateTime pFechaCorte, ref string Error, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtHonorarios = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.CODIGO, e.DESCRIPCION, E.VALOR_ANT, E.INCREMENTO, e.VALOR, o.NOMBRE as OFICINA " +
                                     " From pre_honorarios e Left Join oficina o On e.cod_oficina = o.cod_oficina Order by e.CODIGO ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtHonorarios.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtHonorarios, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtHonorarios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarHonorarios", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Honorarios

        #region Obligaciones

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// OBLIGACIONES
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataTable ListarObligaciones(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtObligaciones = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select o.codobligacion As codigo, b.nombrebanco As descripcion, o.saldocapital As saldo From obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where o.saldocapital != 0 Order by o.codobligacion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtObligaciones.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtObligaciones, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtObligaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarObligaciones", ex);
                        return null;
                    }
                }
            }
        }
        
        public DataTable ListarObligacionesTotal(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtObligaciones = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select b.nombrebanco As descripcion, Sum(o.saldocapital) As saldo From obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where o.saldocapital != 0 Group by b.nombrebanco Order by 1";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtObligaciones.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtObligaciones, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtObligaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarObligacionesTotal", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarObligacionesTotalPagos(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtObligaciones = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select b.nombrebanco As descripcion, a.codcomponente || '-' || a.nombre As componente From obComponente a, obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where a.codcomponente In (1, 2) And o.saldocapital != 0 Group by b.nombrebanco, a.codcomponente || '-' || a.nombre " +
                                     "Union " +
                                     "Select b.nombrebanco As descripcion, -a.codcomponente || '- Causación ' || a.nombre As componente From obComponente a, obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where a.codcomponente = 2 And o.saldocapital != 0 Group by b.nombrebanco, -a.codcomponente || '- Causación ' || a.nombre " +
                                     "Order by 1, 2";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtObligaciones.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtObligaciones, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtObligaciones;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarObligacionesTotal", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarObligacionesNuevas(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtObligacionesNuevas = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        //string sql = "Select o.codigo, o.descripcion, o.cod_oficina, o.valor, o.plazo, o.tasa From pre_obligaciones o Left Join bancos b On o.codentidad = b.cod_banco Order by o.codigo";
                        string sql = "Select o.codigo, o.descripcion, o.cod_oficina, o.valor as cupo, o.plazo, o.tasa, o.cod_periodicidad, o.gracia From pre_obligaciones o Order by o.codigo";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtObligacionesNuevas.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtObligacionesNuevas, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtObligacionesNuevas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarObligacionesNuevas", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ListarObligacionesPagos(DateTime pFechaCorte, ref string Error, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtObligacionesPagos = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select o.codobligacion As codigo, b.nombrebanco As descripcion, a.codcomponente || '-' || a.nombre As componente From obComponente a, obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where a.codcomponente In (1, 2) And o.saldocapital != 0 "  +
                                     "Union " + 
                                     "Select o.codobligacion As codigo, b.nombrebanco As descripcion, -a.codcomponente || '- Causación ' || a.nombre As componente From obComponente a, obCredito o Left Join bancos b On o.codentidad = b.cod_banco Where a.codcomponente = 2 And o.saldocapital != 0 " +
                                     "Order by 3, 1";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtObligacionesPagos.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtObligacionesPagos, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtObligacionesPagos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarObligacionesPagos", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Obligacion CrearObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pObligacion.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pObligacion.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pObligacion.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pObligacion.valor;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.DbType = DbType.Decimal;
                        p_plazo.Value = pObligacion.plazo;

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "p_tasa";
                        p_tasa.DbType = DbType.Decimal;
                        p_tasa.Value = pObligacion.tasa;

                        DbParameter p_Cod_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Periodicidad.ParameterName = "p_Cod_Periodicidad";
                        p_Cod_Periodicidad.DbType = DbType.Int64;
                        p_Cod_Periodicidad.Value = pObligacion.cod_periodicidad;

                        DbParameter p_Gracia = cmdTransaccionFactory.CreateParameter();
                        p_Gracia.ParameterName = "p_Gracia";
                        p_Gracia.DbType = DbType.Decimal;
                        p_Gracia.Value = pObligacion.gracia;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_tasa);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Periodicidad);
                        cmdTransaccionFactory.Parameters.Add(p_Gracia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OBLIGACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pObligacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearObligacion", ex);
                        return pObligacion;
                    }
                }
            }
        }

        public void EliminarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pObligacion.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OBLIGACION_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarObligacion", ex);
                        return;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Obligacion ModificarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pObligacion.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pObligacion.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pObligacion.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pObligacion.valor;

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.DbType = DbType.Decimal;
                        p_plazo.Value = pObligacion.plazo;

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "p_tasa";
                        p_tasa.DbType = DbType.Decimal;
                        p_tasa.Value = pObligacion.tasa;

                        DbParameter p_Cod_Periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Periodicidad.ParameterName = "p_Cod_Periodicidad";
                        p_Cod_Periodicidad.DbType = DbType.Int64;
                        p_Cod_Periodicidad.Value = pObligacion.cod_periodicidad;

                        DbParameter p_Gracia = cmdTransaccionFactory.CreateParameter();
                        p_Gracia.ParameterName = "p_Gracia";
                        p_Gracia.DbType = DbType.Decimal;
                        p_Gracia.Value = pObligacion.gracia;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_plazo);
                        cmdTransaccionFactory.Parameters.Add(p_tasa);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Periodicidad);
                        cmdTransaccionFactory.Parameters.Add(p_Gracia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OBLIGACION_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pObligacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarObligacion", ex);
                        return pObligacion;
                    }
                }
            }
        }

        public decimal ConsultarValorAPagarObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, int codcomponente, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Sum(a.valorpendiente) As valor From obplanpagos a " +
                                     " Where a.codobligacion = " + cod_obligacion +
                                     " And a.fechacuota Between to_date('" + fecha_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + fecha_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                     " And a.codcomponente = " + codcomponente;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["valor"] != DBNull.Value) valor = Convert.ToDecimal(resultado["valor"]);
                        }
                        resultado.Close();
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorAPagarObligacion", ex);
                        return 0;
                    }
                }
            }
        }

        public decimal ConsultarValorProvisionObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            decimal valor = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha_Corte";
                        pFecha.Value = fecha_final;
                        pFecha.Direction = ParameterDirection.Input;
                        pFecha.DbType = DbType.Date;
                        DbParameter pcod_obligacion = cmdTransaccionFactory.CreateParameter();
                        pcod_obligacion.ParameterName = "pcod_obligacion";
                        pcod_obligacion.Value = cod_obligacion;
                        pcod_obligacion.Direction = ParameterDirection.Input;
                        DbParameter pintereses = cmdTransaccionFactory.CreateParameter();
                        pintereses.ParameterName = "pintereses";
                        pintereses.Value = valor;
                        pintereses.Direction = ParameterDirection.InputOutput;
                        DbParameter pdiascausados = cmdTransaccionFactory.CreateParameter();
                        pdiascausados.ParameterName = "pdiascausados";
                        pdiascausados.Value = valor;
                        pdiascausados.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(pFecha);
                        cmdTransaccionFactory.Parameters.Add(pcod_obligacion);
                        cmdTransaccionFactory.Parameters.Add(pintereses);
                        cmdTransaccionFactory.Parameters.Add(pdiascausados);
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_OB_PROVISION";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        valor = Convert.ToDecimal(pintereses.Value.ToString());
                        return valor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ConsultarValorProvisionObligacion", ex);
                        return 0;
                    }
                };

            }
        }
        
        public Xpinn.Presupuesto.Entities.Obligacion CrearDesembolsoObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdDetalle = cmdTransaccionFactory.CreateParameter();
                        pIdDetalle.ParameterName = "pIdDetalle";
                        pIdDetalle.Value = pObligacion.iddetalle;
                        pIdDetalle.Direction = ParameterDirection.InputOutput;

                        DbParameter pnumero_periodo = cmdTransaccionFactory.CreateParameter();
                        pnumero_periodo.ParameterName = "pnumero_periodo";
                        pnumero_periodo.Value = pObligacion.numero_periodo;
                        pnumero_periodo.DbType = DbType.Int64;

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pObligacion.fecha_inicial;
                        pfecha_inicial.DbType = DbType.Date;

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "pfecha_final";
                        pfecha_final.Value = pObligacion.fecha_final;
                        pfecha_final.DbType = DbType.Date;

                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "pcodigo";
                        pcodigo.Value = pObligacion.codigo;
                        pcodigo.DbType = DbType.String;

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "pcentro_costo";
                        pcentro_costo.Value = pObligacion.centro_costo;
                        pcentro_costo.DbType = DbType.Int64;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pObligacion.monto;
                        pvalor.DbType = DbType.Double;

                        cmdTransaccionFactory.Parameters.Add(pIdDetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_periodo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);
                        cmdTransaccionFactory.Parameters.Add(pcodigo);
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_OBLIGACION_DET";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pObligacion.iddetalle = Convert.ToInt64(pIdDetalle.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pObligacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearDesembolsoObligacion", ex);
                        return null;
                    }
                }
            }
        }

        #endregion Obligaciones

        #region Tecnologia

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// TECNOLOGIA
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public DataTable ListarTecnologia(DateTime pFechaCorte, ref string Error, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtTecnologia = new DataTable();
            Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = "Select e.CODIGO, e.DESCRIPCION, Nvl(e.COD_OFICINA, 1) As COD_OFICINA, e.VALOR, e.FECHA_COMPRA, e.TIPO_CONCEPTO, " +
                                     "Case e.TIPO_CONCEPTO WHEN 1 THEN 'Licencias (nuevas)' WHEN 2 THEN 'Renovacion Software' WHEN 3 THEN 'Equipos y Gastos' WHEN 4 THEN 'Equipos y Gastos' WHEN 5 THEN 'Comunicaciones' WHEN 6 THEN 'Cursos y eventos especiales' WHEN 7 THEN 'Otros Gastos' End AS NOM_TIPO_CONCEPTO, o.NOMBRE as OFICINA " +
                                     "From pre_tecnologia e Left Join oficina o On e.cod_oficina = o.cod_oficina Order by e.CODIGO ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtTecnologia.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtTecnologia, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtTecnologia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ListarTecnologia", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia CrearTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pTecnologia.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pTecnologia.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pTecnologia.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pTecnologia.valor;

                        DbParameter p_fecha_compra = cmdTransaccionFactory.CreateParameter();
                        p_fecha_compra.ParameterName = "p_fecha_compra";
                        p_fecha_compra.DbType = DbType.Date;
                        p_fecha_compra.Value = pTecnologia.fecha_compra;

                        DbParameter p_tipo_concepto = cmdTransaccionFactory.CreateParameter();
                        p_tipo_concepto.ParameterName = "p_tipo_concepto";
                        p_tipo_concepto.DbType = DbType.Int64;
                        p_tipo_concepto.Value = pTecnologia.tipo_concepto;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_compra);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TECNOLOGIA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTecnologia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "CrearTecnologia", ex);
                        return pTecnologia;
                    }
                }
            }
        }

        public void EliminarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.Value = pTecnologia.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TECNOLOGIA_ELIM";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "EliminarTecnologia", ex);
                        return;
                    }
                }
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia ModificarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Codigo = cmdTransaccionFactory.CreateParameter();
                        p_Codigo.ParameterName = "p_Codigo";
                        p_Codigo.DbType = DbType.Int64;
                        p_Codigo.Value = pTecnologia.codigo;
                        p_Codigo.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pTecnologia.descripcion;

                        DbParameter p_Cod_Oficina = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Oficina.ParameterName = "p_Cod_Oficina";
                        p_Cod_Oficina.DbType = DbType.Int64;
                        p_Cod_Oficina.Value = pTecnologia.cod_oficina;

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.DbType = DbType.Decimal;
                        p_valor.Value = pTecnologia.valor;

                        DbParameter p_fecha_compra = cmdTransaccionFactory.CreateParameter();
                        p_fecha_compra.ParameterName = "p_fecha_compra";
                        p_fecha_compra.DbType = DbType.Date;
                        p_fecha_compra.Value = pTecnologia.fecha_compra;

                        DbParameter p_tipo_concepto = cmdTransaccionFactory.CreateParameter();
                        p_tipo_concepto.ParameterName = "p_tipo_concepto";
                        p_tipo_concepto.DbType = DbType.Int64;
                        p_tipo_concepto.Value = pTecnologia.tipo_concepto;

                        cmdTransaccionFactory.Parameters.Add(p_Codigo);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Oficina);
                        cmdTransaccionFactory.Parameters.Add(p_valor);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_compra);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRE_TECNOLOGIA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTecnologia;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PresupuestoData", "ModificarTecnologia", ex);
                        return pTecnologia;
                    }
                }
            }
        }

        #endregion Tecnologia    
    
    }
}