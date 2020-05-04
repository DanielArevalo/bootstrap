using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    public class AnalisisCapacidadPagoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AnalisisCapacidadPagoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Analisis_Capacidad_Pago CrearAnalisis_Capacidad_Pago(Analisis_Capacidad_Pago pAnalisis_Capacidad_Pago, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcapacidadpago = cmdTransaccionFactory.CreateParameter();
                        pidcapacidadpago.ParameterName = "p_idcapacidadpago";
                        pidcapacidadpago.Value = pAnalisis_Capacidad_Pago.idcapacidadpago;
                        pidcapacidadpago.Direction = ParameterDirection.Input;
                        pidcapacidadpago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcapacidadpago);

                        DbParameter pidanalisis = cmdTransaccionFactory.CreateParameter();
                        pidanalisis.ParameterName = "p_idanalisis";
                        if (pAnalisis_Capacidad_Pago.idanalisis == null)
                            pidanalisis.Value = DBNull.Value;
                        else
                            pidanalisis.Value = pAnalisis_Capacidad_Pago.idanalisis;
                        pidanalisis.Direction = ParameterDirection.Input;
                        pidanalisis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidanalisis);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAnalisis_Capacidad_Pago.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAnalisis_Capacidad_Pago.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pingresos = cmdTransaccionFactory.CreateParameter();
                        pingresos.ParameterName = "p_ingresos";
                        if (pAnalisis_Capacidad_Pago.ingresos == null)
                            pingresos.Value = DBNull.Value;
                        else
                            pingresos.Value = pAnalisis_Capacidad_Pago.ingresos;
                        pingresos.Direction = ParameterDirection.Input;
                        pingresos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pingresos);

                        DbParameter potros_ingresos = cmdTransaccionFactory.CreateParameter();
                        potros_ingresos.ParameterName = "p_otros_ingresos";
                        if (pAnalisis_Capacidad_Pago.otros_ingresos == null)
                            potros_ingresos.Value = DBNull.Value;
                        else
                            potros_ingresos.Value = pAnalisis_Capacidad_Pago.otros_ingresos;
                        potros_ingresos.Direction = ParameterDirection.Input;
                        potros_ingresos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(potros_ingresos);

                        DbParameter pdeduccion_nom = cmdTransaccionFactory.CreateParameter();
                        pdeduccion_nom.ParameterName = "p_deduccion_nom";
                        if (pAnalisis_Capacidad_Pago.deduccion_nom == null)
                            pdeduccion_nom.Value = DBNull.Value;
                        else
                            pdeduccion_nom.Value = pAnalisis_Capacidad_Pago.deduccion_nom;
                        pdeduccion_nom.Direction = ParameterDirection.Input;
                        pdeduccion_nom.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdeduccion_nom);

                        DbParameter pcuotas_oblig = cmdTransaccionFactory.CreateParameter();
                        pcuotas_oblig.ParameterName = "p_cuotas_oblig";
                        if (pAnalisis_Capacidad_Pago.cuotas_oblig == null)
                            pcuotas_oblig.Value = DBNull.Value;
                        else
                            pcuotas_oblig.Value = pAnalisis_Capacidad_Pago.cuotas_oblig;
                        pcuotas_oblig.Direction = ParameterDirection.Input;
                        pcuotas_oblig.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_oblig);

                        DbParameter pcuotas_cod = cmdTransaccionFactory.CreateParameter();
                        pcuotas_cod.ParameterName = "p_cuotas_cod";
                        if (pAnalisis_Capacidad_Pago.cuotas_cod == null)
                            pcuotas_cod.Value = DBNull.Value;
                        else
                            pcuotas_cod.Value = pAnalisis_Capacidad_Pago.cuotas_cod;
                        pcuotas_cod.Direction = ParameterDirection.Input;
                        pcuotas_cod.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_cod);

                        DbParameter pgastos_fam = cmdTransaccionFactory.CreateParameter();
                        pgastos_fam.ParameterName = "p_gastos_fam";
                        if (pAnalisis_Capacidad_Pago.gastos_fam == null)
                            pgastos_fam.Value = DBNull.Value;
                        else
                            pgastos_fam.Value = pAnalisis_Capacidad_Pago.gastos_fam;
                        pgastos_fam.Direction = ParameterDirection.Input;
                        pgastos_fam.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pgastos_fam);


                        DbParameter parrendamientos = cmdTransaccionFactory.CreateParameter();
                        parrendamientos.ParameterName = "p_arrendamientos";
                        if (pAnalisis_Capacidad_Pago.arrendamientos == null)
                            parrendamientos.Value = DBNull.Value;
                        else
                            parrendamientos.Value = pAnalisis_Capacidad_Pago.arrendamientos;
                        parrendamientos.Direction = ParameterDirection.Input;
                        parrendamientos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(parrendamientos);

                        DbParameter phonorarios = cmdTransaccionFactory.CreateParameter();
                        phonorarios.ParameterName = "p_honorarios";
                        if (pAnalisis_Capacidad_Pago.honorarios == null)
                            phonorarios.Value = DBNull.Value;
                        else
                            phonorarios.Value = pAnalisis_Capacidad_Pago.honorarios;
                        phonorarios.Direction = ParameterDirection.Input;
                        phonorarios.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(phonorarios);

                        DbParameter paportes = cmdTransaccionFactory.CreateParameter();
                        paportes.ParameterName = "p_aportes";
                        if (pAnalisis_Capacidad_Pago.aportes == null)
                            paportes.Value = DBNull.Value;
                        else
                            paportes.Value = pAnalisis_Capacidad_Pago.aportes;
                        paportes.Direction = ParameterDirection.Input;
                        paportes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(paportes);

                        DbParameter pcreditos = cmdTransaccionFactory.CreateParameter();
                        pcreditos.ParameterName = "p_creditos";
                        if (pAnalisis_Capacidad_Pago.creditos == null)
                            pcreditos.Value = DBNull.Value;
                        else
                            pcreditos.Value = pAnalisis_Capacidad_Pago.creditos;
                        pcreditos.Direction = ParameterDirection.Input;
                        pcreditos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcreditos);

                        DbParameter pservicios = cmdTransaccionFactory.CreateParameter();
                        pservicios.ParameterName = "p_servicios";
                        if (pAnalisis_Capacidad_Pago.servicios == null)
                            pservicios.Value = DBNull.Value;
                        else
                            pservicios.Value = pAnalisis_Capacidad_Pago.servicios;
                        pservicios.Direction = ParameterDirection.Input;
                        pservicios.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pservicios);

                        DbParameter pproteccion_salarial = cmdTransaccionFactory.CreateParameter();
                        pproteccion_salarial.ParameterName = "p_proteccion_salarial";
                        if (pAnalisis_Capacidad_Pago.proteccion_salarial == null)
                            pproteccion_salarial.Value = DBNull.Value;
                        else
                            pproteccion_salarial.Value = pAnalisis_Capacidad_Pago.proteccion_salarial;
                        pproteccion_salarial.Direction = ParameterDirection.Input;
                        pproteccion_salarial.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pproteccion_salarial);

                        DbParameter potro_descuento = cmdTransaccionFactory.CreateParameter();
                        potro_descuento.ParameterName = "p_otro_descuento";
                        if (pAnalisis_Capacidad_Pago.otro_descuento == null)
                            potro_descuento.Value = DBNull.Value;
                        else
                            potro_descuento.Value = pAnalisis_Capacidad_Pago.otro_descuento;
                        potro_descuento.Direction = ParameterDirection.Input;
                        potro_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(potro_descuento);

                        DbParameter pdeuda_tercero = cmdTransaccionFactory.CreateParameter();
                        pdeuda_tercero.ParameterName = "p_deuda_tercero";
                        if (pAnalisis_Capacidad_Pago.deuda_tercero == null)
                            pdeuda_tercero.Value = DBNull.Value;
                        else
                            pdeuda_tercero.Value = pAnalisis_Capacidad_Pago.deuda_tercero;
                        pdeuda_tercero.Direction = ParameterDirection.Input;
                        pdeuda_tercero.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdeuda_tercero);

                        DbParameter pcapacidad_descuento = cmdTransaccionFactory.CreateParameter();
                        pcapacidad_descuento.ParameterName = "p_capacidad_descuento";
                        if (pAnalisis_Capacidad_Pago.capacidad_descuento == null)
                            pcapacidad_descuento.Value = DBNull.Value;
                        else
                            pcapacidad_descuento.Value = pAnalisis_Capacidad_Pago.capacidad_descuento;
                        pcapacidad_descuento.Direction = ParameterDirection.Input;
                        pcapacidad_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcapacidad_descuento);

                        DbParameter pcapacidad_pago = cmdTransaccionFactory.CreateParameter();
                        pcapacidad_pago.ParameterName = "p_capacidad_pago";
                        if (pAnalisis_Capacidad_Pago.capacidad_pago == null)
                            pcapacidad_pago.Value = DBNull.Value;
                        else
                            pcapacidad_pago.Value = pAnalisis_Capacidad_Pago.capacidad_pago;
                        pcapacidad_pago.Direction = ParameterDirection.Input;
                        pcapacidad_pago.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcapacidad_pago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAPPAGO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAnalisis_Capacidad_Pago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_Capacidad_PagoData", "CrearAnalisis_Capacidad_Pago", ex);
                        return null;
                    }
                }
            }
        }


        public Analisis_Capacidad_Pago ModificarAnalisis_Capacidad_Pago(Analisis_Capacidad_Pago pAnalisis_Capacidad_Pago, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcapacidadpago = cmdTransaccionFactory.CreateParameter();
                        pidcapacidadpago.ParameterName = "p_idcapacidadpago";
                        pidcapacidadpago.Value = pAnalisis_Capacidad_Pago.idcapacidadpago;
                        pidcapacidadpago.Direction = ParameterDirection.Input;
                        pidcapacidadpago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcapacidadpago);

                        DbParameter pidanalisis = cmdTransaccionFactory.CreateParameter();
                        pidanalisis.ParameterName = "p_idanalisis";
                        if (pAnalisis_Capacidad_Pago.idanalisis == null)
                            pidanalisis.Value = DBNull.Value;
                        else
                            pidanalisis.Value = pAnalisis_Capacidad_Pago.idanalisis;
                        pidanalisis.Direction = ParameterDirection.Input;
                        pidanalisis.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidanalisis);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAnalisis_Capacidad_Pago.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAnalisis_Capacidad_Pago.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pingresos = cmdTransaccionFactory.CreateParameter();
                        pingresos.ParameterName = "p_ingresos";
                        if (pAnalisis_Capacidad_Pago.ingresos == null)
                            pingresos.Value = DBNull.Value;
                        else
                            pingresos.Value = pAnalisis_Capacidad_Pago.ingresos;
                        pingresos.Direction = ParameterDirection.Input;
                        pingresos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pingresos);

                        DbParameter potros_ingresos = cmdTransaccionFactory.CreateParameter();
                        potros_ingresos.ParameterName = "p_otros_ingresos";
                        if (pAnalisis_Capacidad_Pago.otros_ingresos == null)
                            potros_ingresos.Value = DBNull.Value;
                        else
                            potros_ingresos.Value = pAnalisis_Capacidad_Pago.otros_ingresos;
                        potros_ingresos.Direction = ParameterDirection.Input;
                        potros_ingresos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(potros_ingresos);

                        DbParameter pdeduccion_nom = cmdTransaccionFactory.CreateParameter();
                        pdeduccion_nom.ParameterName = "p_deduccion_nom";
                        if (pAnalisis_Capacidad_Pago.deduccion_nom == null)
                            pdeduccion_nom.Value = DBNull.Value;
                        else
                            pdeduccion_nom.Value = pAnalisis_Capacidad_Pago.deduccion_nom;
                        pdeduccion_nom.Direction = ParameterDirection.Input;
                        pdeduccion_nom.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdeduccion_nom);

                        DbParameter pcuotas_oblig = cmdTransaccionFactory.CreateParameter();
                        pcuotas_oblig.ParameterName = "p_cuotas_oblig";
                        if (pAnalisis_Capacidad_Pago.cuotas_oblig == null)
                            pcuotas_oblig.Value = DBNull.Value;
                        else
                            pcuotas_oblig.Value = pAnalisis_Capacidad_Pago.cuotas_oblig;
                        pcuotas_oblig.Direction = ParameterDirection.Input;
                        pcuotas_oblig.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_oblig);

                        DbParameter pcuotas_cod = cmdTransaccionFactory.CreateParameter();
                        pcuotas_cod.ParameterName = "p_cuotas_cod";
                        if (pAnalisis_Capacidad_Pago.cuotas_cod == null)
                            pcuotas_cod.Value = DBNull.Value;
                        else
                            pcuotas_cod.Value = pAnalisis_Capacidad_Pago.cuotas_cod;
                        pcuotas_cod.Direction = ParameterDirection.Input;
                        pcuotas_cod.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_cod);

                        DbParameter pgastos_fam = cmdTransaccionFactory.CreateParameter();
                        pgastos_fam.ParameterName = "p_gastos_fam";
                        if (pAnalisis_Capacidad_Pago.gastos_fam == null)
                            pgastos_fam.Value = DBNull.Value;
                        else
                            pgastos_fam.Value = pAnalisis_Capacidad_Pago.gastos_fam;
                        pgastos_fam.Direction = ParameterDirection.Input;
                        pgastos_fam.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pgastos_fam);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAPPAGO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAnalisis_Capacidad_Pago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_Capacidad_PagoData", "ModificarAnalisis_Capacidad_Pago", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAnalisis_Capacidad_Pago(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Analisis_Capacidad_Pago pAnalisis_Capacidad_Pago = new Analisis_Capacidad_Pago();
                        pAnalisis_Capacidad_Pago = ConsultarAnalisis_Capacidad_Pago(pId, vUsuario);

                        DbParameter pidcapacidadpago = cmdTransaccionFactory.CreateParameter();
                        pidcapacidadpago.ParameterName = "p_idcapacidadpago";
                        pidcapacidadpago.Value = pAnalisis_Capacidad_Pago.idcapacidadpago;
                        pidcapacidadpago.Direction = ParameterDirection.Input;
                        pidcapacidadpago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcapacidadpago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CAPPAGO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_Capacidad_PagoData", "EliminarAnalisis_Capacidad_Pago", ex);
                    }
                }
            }
        }


        public Analisis_Capacidad_Pago ConsultarAnalisis_Capacidad_Pago(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Analisis_Capacidad_Pago entidad = new Analisis_Capacidad_Pago();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Analisis_Capacidad_Pago WHERE IDCAPACIDADPAGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCAPACIDADPAGO"] != DBNull.Value) entidad.idcapacidadpago = Convert.ToInt32(resultado["IDCAPACIDADPAGO"]);
                            if (resultado["IDANALISIS"] != DBNull.Value) entidad.idanalisis = Convert.ToInt32(resultado["IDANALISIS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["INGRESOS"] != DBNull.Value) entidad.ingresos = Convert.ToDecimal(resultado["INGRESOS"]);
                            if (resultado["OTROS_INGRESOS"] != DBNull.Value) entidad.otros_ingresos = Convert.ToDecimal(resultado["OTROS_INGRESOS"]);
                            if (resultado["DEDUCCION_NOM"] != DBNull.Value) entidad.deduccion_nom = Convert.ToDecimal(resultado["DEDUCCION_NOM"]);
                            if (resultado["CUOTAS_OBLIG"] != DBNull.Value) entidad.cuotas_oblig = Convert.ToDecimal(resultado["CUOTAS_OBLIG"]);
                            if (resultado["CUOTAS_COD"] != DBNull.Value) entidad.cuotas_cod = Convert.ToDecimal(resultado["CUOTAS_COD"]);
                            if (resultado["GASTOS_FAM"] != DBNull.Value) entidad.gastos_fam = Convert.ToDecimal(resultado["GASTOS_FAM"]);
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
                        BOExcepcion.Throw("Analisis_Capacidad_PagoData", "ConsultarAnalisis_Capacidad_Pago", ex);
                        return null;
                    }
                }
            }
        }


        public List<Analisis_Capacidad_Pago> ListarAnalisis_Capacidad_Pago(Analisis_Capacidad_Pago pAnalisis_Capacidad_Pago, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Analisis_Capacidad_Pago> lstAnalisis_Capacidad_Pago = new List<Analisis_Capacidad_Pago>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Analisis_Capacidad_Pago " + ObtenerFiltro(pAnalisis_Capacidad_Pago) + " ORDER BY IDCAPACIDADPAGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Analisis_Capacidad_Pago entidad = new Analisis_Capacidad_Pago();
                            if (resultado["IDCAPACIDADPAGO"] != DBNull.Value) entidad.idcapacidadpago = Convert.ToInt32(resultado["IDCAPACIDADPAGO"]);
                            if (resultado["IDANALISIS"] != DBNull.Value) entidad.idanalisis = Convert.ToInt32(resultado["IDANALISIS"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["INGRESOS"] != DBNull.Value) entidad.ingresos = Convert.ToDecimal(resultado["INGRESOS"]);
                            if (resultado["OTROS_INGRESOS"] != DBNull.Value) entidad.otros_ingresos = Convert.ToDecimal(resultado["OTROS_INGRESOS"]);
                            if (resultado["DEDUCCION_NOM"] != DBNull.Value) entidad.deduccion_nom = Convert.ToDecimal(resultado["DEDUCCION_NOM"]);
                            if (resultado["CUOTAS_OBLIG"] != DBNull.Value) entidad.cuotas_oblig = Convert.ToDecimal(resultado["CUOTAS_OBLIG"]);
                            if (resultado["CUOTAS_COD"] != DBNull.Value) entidad.cuotas_cod = Convert.ToDecimal(resultado["CUOTAS_COD"]);
                            if (resultado["GASTOS_FAM"] != DBNull.Value) entidad.gastos_fam = Convert.ToDecimal(resultado["GASTOS_FAM"]);
                            lstAnalisis_Capacidad_Pago.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAnalisis_Capacidad_Pago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Analisis_Capacidad_PagoData", "ListarAnalisis_Capacidad_Pago", ex);
                        return null;
                    }
                }
            }
        }


    }
}