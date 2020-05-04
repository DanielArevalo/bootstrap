using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;

namespace Xpinn.TarjetaDebito.Data
{
    public class TarjetaConvenioData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public TarjetaConvenioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public TarjetaConvenio CrearTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_convenio = cmdTransaccionFactory.CreateParameter();
                        pcod_convenio.ParameterName = "p_cod_convenio";
                        pcod_convenio.Value = pTarjetaConvenio.cod_convenio;
                        pcod_convenio.Direction = ParameterDirection.Input;
                        pcod_convenio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_convenio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTarjetaConvenio.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcodigo_bin = cmdTransaccionFactory.CreateParameter();
                        pcodigo_bin.ParameterName = "p_codigo_bin";
                        if (pTarjetaConvenio.codigo_bin == null)
                            pcodigo_bin.Value = DBNull.Value;
                        else
                            pcodigo_bin.Value = pTarjetaConvenio.codigo_bin;
                        pcodigo_bin.Direction = ParameterDirection.Input;
                        pcodigo_bin.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_bin);

                        DbParameter pip_switch = cmdTransaccionFactory.CreateParameter();
                        pip_switch.ParameterName = "p_ip_switch";
                        if (pTarjetaConvenio.ip_switch == null)
                            pip_switch.Value = DBNull.Value;
                        else
                            pip_switch.Value = pTarjetaConvenio.ip_switch;
                        pip_switch.Direction = ParameterDirection.Input;
                        pip_switch.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip_switch);

                        DbParameter ppuerto_switch = cmdTransaccionFactory.CreateParameter();
                        ppuerto_switch.ParameterName = "p_puerto_switch";
                        if (pTarjetaConvenio.puerto_switch == null)
                            ppuerto_switch.Value = DBNull.Value;
                        else
                            ppuerto_switch.Value = pTarjetaConvenio.puerto_switch;
                        ppuerto_switch.Direction = ParameterDirection.Input;
                        ppuerto_switch.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppuerto_switch);

                        DbParameter pfecha_convenio = cmdTransaccionFactory.CreateParameter();
                        pfecha_convenio.ParameterName = "p_fecha_convenio";
                        if (pTarjetaConvenio.fecha_convenio == null)
                            pfecha_convenio.Value = DBNull.Value;
                        else
                            pfecha_convenio.Value = pTarjetaConvenio.fecha_convenio;
                        pfecha_convenio.Direction = ParameterDirection.Input;
                        pfecha_convenio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_convenio);

                        DbParameter pencargado = cmdTransaccionFactory.CreateParameter();
                        pencargado.ParameterName = "p_encargado";
                        if (pTarjetaConvenio.encargado == null)
                            pencargado.Value = DBNull.Value;
                        else
                            pencargado.Value = pTarjetaConvenio.encargado;
                        pencargado.Direction = ParameterDirection.Input;
                        pencargado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pencargado);

                        DbParameter pe_mail = cmdTransaccionFactory.CreateParameter();
                        pe_mail.ParameterName = "p_e_mail";
                        if (pTarjetaConvenio.e_mail == null)
                            pe_mail.Value = DBNull.Value;
                        else
                            pe_mail.Value = pTarjetaConvenio.e_mail;
                        pe_mail.Direction = ParameterDirection.Input;
                        pe_mail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_mail);

                        DbParameter pe_cc_1 = cmdTransaccionFactory.CreateParameter();
                        pe_cc_1.ParameterName = "p_e_cc_1";
                        if (pTarjetaConvenio.e_cc_1 == null)
                            pe_cc_1.Value = DBNull.Value;
                        else
                            pe_cc_1.Value = pTarjetaConvenio.e_cc_1;
                        pe_cc_1.Direction = ParameterDirection.Input;
                        pe_cc_1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_cc_1);

                        DbParameter pe_cc_2 = cmdTransaccionFactory.CreateParameter();
                        pe_cc_2.ParameterName = "p_e_cc_2";
                        if (pTarjetaConvenio.e_cc_2 == null)
                            pe_cc_2.Value = DBNull.Value;
                        else
                            pe_cc_2.Value = pTarjetaConvenio.e_cc_2;
                        pe_cc_2.Direction = ParameterDirection.Input;
                        pe_cc_2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_cc_2);

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "p_comision";
                        if (pTarjetaConvenio.comision == null)
                            pcomision.Value = DBNull.Value;
                        else
                            pcomision.Value = pTarjetaConvenio.comision;
                        pcomision.Direction = ParameterDirection.Input;
                        pcomision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcomision);

                        DbParameter pcuota_manejo = cmdTransaccionFactory.CreateParameter();
                        pcuota_manejo.ParameterName = "p_cuota_manejo";
                        if (pTarjetaConvenio.cuota_manejo == null)
                            pcuota_manejo.Value = DBNull.Value;
                        else
                            pcuota_manejo.Value = pTarjetaConvenio.cuota_manejo;
                        pcuota_manejo.Direction = ParameterDirection.Input;
                        pcuota_manejo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_manejo);

                        DbParameter ptipo_procesamiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_procesamiento.ParameterName = "p_tipo_procesamiento";
                        if (pTarjetaConvenio.tipo_procesamiento == null)
                            ptipo_procesamiento.Value = DBNull.Value;
                        else
                            ptipo_procesamiento.Value = pTarjetaConvenio.tipo_procesamiento;
                        ptipo_procesamiento.Direction = ParameterDirection.Input;
                        ptipo_procesamiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_procesamiento);

                        DbParameter pcupo_cajero = cmdTransaccionFactory.CreateParameter();
                        pcupo_cajero.ParameterName = "p_cupo_cajero";
                        if (pTarjetaConvenio.cupo_cajero == null)
                            pcupo_cajero.Value = DBNull.Value;
                        else
                            pcupo_cajero.Value = pTarjetaConvenio.cupo_cajero;
                        pcupo_cajero.Direction = ParameterDirection.Input;
                        pcupo_cajero.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcupo_cajero);

                        DbParameter ptransacciones_cajero = cmdTransaccionFactory.CreateParameter();
                        ptransacciones_cajero.ParameterName = "p_transacciones_cajero";
                        if (pTarjetaConvenio.transacciones_cajero == null)
                            ptransacciones_cajero.Value = DBNull.Value;
                        else
                            ptransacciones_cajero.Value = pTarjetaConvenio.transacciones_cajero;
                        ptransacciones_cajero.Direction = ParameterDirection.Input;
                        ptransacciones_cajero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptransacciones_cajero);

                        DbParameter pcupo_datafono = cmdTransaccionFactory.CreateParameter();
                        pcupo_datafono.ParameterName = "p_cupo_datafono";
                        if (pTarjetaConvenio.cupo_datafono == null)
                            pcupo_datafono.Value = DBNull.Value;
                        else
                            pcupo_datafono.Value = pTarjetaConvenio.cupo_datafono;
                        pcupo_datafono.Direction = ParameterDirection.Input;
                        pcupo_datafono.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcupo_datafono);

                        DbParameter ptransacciones_datafono = cmdTransaccionFactory.CreateParameter();
                        ptransacciones_datafono.ParameterName = "p_transacciones_datafono";
                        if (pTarjetaConvenio.transacciones_datafono == null)
                            ptransacciones_datafono.Value = DBNull.Value;
                        else
                            ptransacciones_datafono.Value = pTarjetaConvenio.transacciones_datafono;
                        ptransacciones_datafono.Direction = ParameterDirection.Input;
                        ptransacciones_datafono.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptransacciones_datafono);

                        DbParameter pvalor_cancela_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pvalor_cancela_tarjeta.ParameterName = "p_valor_cancela_tarjeta";
                        if (pTarjetaConvenio.valor_cancela_tarjeta == null)
                            pvalor_cancela_tarjeta.Value = DBNull.Value;
                        else
                            pvalor_cancela_tarjeta.Value = pTarjetaConvenio.valor_cancela_tarjeta;
                        pvalor_cancela_tarjeta.Direction = ParameterDirection.Input;
                        pvalor_cancela_tarjeta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cancela_tarjeta);

                        DbParameter pcobro_cancela_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pcobro_cancela_tarjeta.ParameterName = "p_cobro_cancela_tarjeta";
                        if (pTarjetaConvenio.cobro_cancela_tarjeta == null)
                            pcobro_cancela_tarjeta.Value = DBNull.Value;
                        else
                            pcobro_cancela_tarjeta.Value = pTarjetaConvenio.cobro_cancela_tarjeta;
                        pcobro_cancela_tarjeta.Direction = ParameterDirection.Input;
                        pcobro_cancela_tarjeta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobro_cancela_tarjeta);

                        DbParameter pcobra_tarjeta_bloqueada = cmdTransaccionFactory.CreateParameter();
                        pcobra_tarjeta_bloqueada.ParameterName = "p_cobra_tarjeta_bloqueada";
                        if (pTarjetaConvenio.cobra_tarjeta_bloqueada == null)
                            pcobra_tarjeta_bloqueada.Value = DBNull.Value;
                        else
                            pcobra_tarjeta_bloqueada.Value = pTarjetaConvenio.cobra_tarjeta_bloqueada;
                        pcobra_tarjeta_bloqueada.Direction = ParameterDirection.Input;
                        pcobra_tarjeta_bloqueada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_tarjeta_bloqueada);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TAR_TARJETA_CO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTarjetaConvenio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaConvenioData", "CrearTarjetaConvenio", ex);
                        return null;
                    }
                }
            }
        }


        public TarjetaConvenio ModificarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_convenio = cmdTransaccionFactory.CreateParameter();
                        pcod_convenio.ParameterName = "p_cod_convenio";
                        pcod_convenio.Value = pTarjetaConvenio.cod_convenio;
                        pcod_convenio.Direction = ParameterDirection.Input;
                        pcod_convenio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_convenio);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTarjetaConvenio.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcodigo_bin = cmdTransaccionFactory.CreateParameter();
                        pcodigo_bin.ParameterName = "p_codigo_bin";
                        if (pTarjetaConvenio.codigo_bin == null)
                            pcodigo_bin.Value = DBNull.Value;
                        else
                            pcodigo_bin.Value = pTarjetaConvenio.codigo_bin;
                        pcodigo_bin.Direction = ParameterDirection.Input;
                        pcodigo_bin.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_bin);

                        DbParameter pip_switch = cmdTransaccionFactory.CreateParameter();
                        pip_switch.ParameterName = "p_ip_switch";
                        if (pTarjetaConvenio.ip_switch == null)
                            pip_switch.Value = DBNull.Value;
                        else
                            pip_switch.Value = pTarjetaConvenio.ip_switch;
                        pip_switch.Direction = ParameterDirection.Input;
                        pip_switch.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip_switch);

                        DbParameter ppuerto_switch = cmdTransaccionFactory.CreateParameter();
                        ppuerto_switch.ParameterName = "p_puerto_switch";
                        if (pTarjetaConvenio.puerto_switch == null)
                            ppuerto_switch.Value = DBNull.Value;
                        else
                            ppuerto_switch.Value = pTarjetaConvenio.puerto_switch;
                        ppuerto_switch.Direction = ParameterDirection.Input;
                        ppuerto_switch.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppuerto_switch);

                        DbParameter pfecha_convenio = cmdTransaccionFactory.CreateParameter();
                        pfecha_convenio.ParameterName = "p_fecha_convenio";
                        if (pTarjetaConvenio.fecha_convenio == null)
                            pfecha_convenio.Value = DBNull.Value;
                        else
                            pfecha_convenio.Value = pTarjetaConvenio.fecha_convenio;
                        pfecha_convenio.Direction = ParameterDirection.Input;
                        pfecha_convenio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_convenio);

                        DbParameter pencargado = cmdTransaccionFactory.CreateParameter();
                        pencargado.ParameterName = "p_encargado";
                        if (pTarjetaConvenio.encargado == null)
                            pencargado.Value = DBNull.Value;
                        else
                            pencargado.Value = pTarjetaConvenio.encargado;
                        pencargado.Direction = ParameterDirection.Input;
                        pencargado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pencargado);

                        DbParameter pe_mail = cmdTransaccionFactory.CreateParameter();
                        pe_mail.ParameterName = "p_e_mail";
                        if (pTarjetaConvenio.e_mail == null)
                            pe_mail.Value = DBNull.Value;
                        else
                            pe_mail.Value = pTarjetaConvenio.e_mail;
                        pe_mail.Direction = ParameterDirection.Input;
                        pe_mail.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_mail);

                        DbParameter pe_cc_1 = cmdTransaccionFactory.CreateParameter();
                        pe_cc_1.ParameterName = "p_e_cc_1";
                        if (pTarjetaConvenio.e_cc_1 == null)
                            pe_cc_1.Value = DBNull.Value;
                        else
                            pe_cc_1.Value = pTarjetaConvenio.e_cc_1;
                        pe_cc_1.Direction = ParameterDirection.Input;
                        pe_cc_1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_cc_1);

                        DbParameter pe_cc_2 = cmdTransaccionFactory.CreateParameter();
                        pe_cc_2.ParameterName = "p_e_cc_2";
                        if (pTarjetaConvenio.e_cc_2 == null)
                            pe_cc_2.Value = DBNull.Value;
                        else
                            pe_cc_2.Value = pTarjetaConvenio.e_cc_2;
                        pe_cc_2.Direction = ParameterDirection.Input;
                        pe_cc_2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pe_cc_2);

                        DbParameter pcomision = cmdTransaccionFactory.CreateParameter();
                        pcomision.ParameterName = "p_comision";
                        if (pTarjetaConvenio.comision == null)
                            pcomision.Value = DBNull.Value;
                        else
                            pcomision.Value = pTarjetaConvenio.comision;
                        pcomision.Direction = ParameterDirection.Input;
                        pcomision.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcomision);

                        DbParameter pcuota_manejo = cmdTransaccionFactory.CreateParameter();
                        pcuota_manejo.ParameterName = "p_cuota_manejo";
                        if (pTarjetaConvenio.cuota_manejo == null)
                            pcuota_manejo.Value = DBNull.Value;
                        else
                            pcuota_manejo.Value = pTarjetaConvenio.cuota_manejo;
                        pcuota_manejo.Direction = ParameterDirection.Input;
                        pcuota_manejo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcuota_manejo);

                        DbParameter ptipo_procesamiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_procesamiento.ParameterName = "p_tipo_procesamiento";
                        if (pTarjetaConvenio.tipo_procesamiento == null)
                            ptipo_procesamiento.Value = DBNull.Value;
                        else
                            ptipo_procesamiento.Value = pTarjetaConvenio.tipo_procesamiento;
                        ptipo_procesamiento.Direction = ParameterDirection.Input;
                        ptipo_procesamiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_procesamiento);

                        DbParameter pcupo_cajero = cmdTransaccionFactory.CreateParameter();
                        pcupo_cajero.ParameterName = "p_cupo_cajero";
                        if (pTarjetaConvenio.cupo_cajero == null)
                            pcupo_cajero.Value = DBNull.Value;
                        else
                            pcupo_cajero.Value = pTarjetaConvenio.cupo_cajero;
                        pcupo_cajero.Direction = ParameterDirection.Input;
                        pcupo_cajero.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcupo_cajero);

                        DbParameter ptransacciones_cajero = cmdTransaccionFactory.CreateParameter();
                        ptransacciones_cajero.ParameterName = "p_transacciones_cajero";
                        if (pTarjetaConvenio.transacciones_cajero == null)
                            ptransacciones_cajero.Value = DBNull.Value;
                        else
                            ptransacciones_cajero.Value = pTarjetaConvenio.transacciones_cajero;
                        ptransacciones_cajero.Direction = ParameterDirection.Input;
                        ptransacciones_cajero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptransacciones_cajero);

                        DbParameter pcupo_datafono = cmdTransaccionFactory.CreateParameter();
                        pcupo_datafono.ParameterName = "p_cupo_datafono";
                        if (pTarjetaConvenio.cupo_datafono == null)
                            pcupo_datafono.Value = DBNull.Value;
                        else
                            pcupo_datafono.Value = pTarjetaConvenio.cupo_datafono;
                        pcupo_datafono.Direction = ParameterDirection.Input;
                        pcupo_datafono.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcupo_datafono);

                        DbParameter ptransacciones_datafono = cmdTransaccionFactory.CreateParameter();
                        ptransacciones_datafono.ParameterName = "p_transacciones_datafono";
                        if (pTarjetaConvenio.transacciones_datafono == null)
                            ptransacciones_datafono.Value = DBNull.Value;
                        else
                            ptransacciones_datafono.Value = pTarjetaConvenio.transacciones_datafono;
                        ptransacciones_datafono.Direction = ParameterDirection.Input;
                        ptransacciones_datafono.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptransacciones_datafono);

                        DbParameter pvalor_cancela_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pvalor_cancela_tarjeta.ParameterName = "p_valor_cancela_tarjeta";
                        if (pTarjetaConvenio.valor_cancela_tarjeta == null)
                            pvalor_cancela_tarjeta.Value = DBNull.Value;
                        else
                            pvalor_cancela_tarjeta.Value = pTarjetaConvenio.valor_cancela_tarjeta;
                        pvalor_cancela_tarjeta.Direction = ParameterDirection.Input;
                        pvalor_cancela_tarjeta.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cancela_tarjeta);

                        DbParameter pcobro_cancela_tarjeta = cmdTransaccionFactory.CreateParameter();
                        pcobro_cancela_tarjeta.ParameterName = "p_cobro_cancela_tarjeta";
                        if (pTarjetaConvenio.cobro_cancela_tarjeta == null)
                            pcobro_cancela_tarjeta.Value = DBNull.Value;
                        else
                            pcobro_cancela_tarjeta.Value = pTarjetaConvenio.cobro_cancela_tarjeta;
                        pcobro_cancela_tarjeta.Direction = ParameterDirection.Input;
                        pcobro_cancela_tarjeta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobro_cancela_tarjeta);

                        DbParameter pcobra_tarjeta_bloqueada = cmdTransaccionFactory.CreateParameter();
                        pcobra_tarjeta_bloqueada.ParameterName = "p_cobra_tarjeta_bloqueada";
                        if (pTarjetaConvenio.cobra_tarjeta_bloqueada == null)
                            pcobra_tarjeta_bloqueada.Value = DBNull.Value;
                        else
                            pcobra_tarjeta_bloqueada.Value = pTarjetaConvenio.cobra_tarjeta_bloqueada;
                        pcobra_tarjeta_bloqueada.Direction = ParameterDirection.Input;
                        pcobra_tarjeta_bloqueada.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_tarjeta_bloqueada);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TAR_TARJETA_CO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTarjetaConvenio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaConvenioData", "ModificarTarjetaConvenio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarTarjetaConvenio(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TarjetaConvenio pTarjetaConvenio = new TarjetaConvenio();
                        pTarjetaConvenio = ConsultarTarjetaConvenio(pId, vUsuario);

                        DbParameter pcod_convenio = cmdTransaccionFactory.CreateParameter();
                        pcod_convenio.ParameterName = "p_cod_convenio";
                        pcod_convenio.Value = pTarjetaConvenio.cod_convenio;
                        pcod_convenio.Direction = ParameterDirection.Input;
                        pcod_convenio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_convenio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TAR_TARJETA_CO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaConvenioData", "EliminarTarjetaConvenio", ex);
                    }
                }
            }
        }


        public TarjetaConvenio ConsultarTarjetaConvenio(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TarjetaConvenio entidad = new TarjetaConvenio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TARJETA_CONVENIO WHERE COD_CONVENIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToString(resultado["COD_CONVENIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CODIGO_BIN"] != DBNull.Value) entidad.codigo_bin = Convert.ToString(resultado["CODIGO_BIN"]);
                            if (resultado["IP_SWITCH"] != DBNull.Value) entidad.ip_switch = Convert.ToString(resultado["IP_SWITCH"]);
                            if (resultado["PUERTO_SWITCH"] != DBNull.Value) entidad.puerto_switch = Convert.ToString(resultado["PUERTO_SWITCH"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToString(resultado["ENCARGADO"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["E_CC_1"] != DBNull.Value) entidad.e_cc_1 = Convert.ToString(resultado["E_CC_1"]);
                            if (resultado["E_CC_2"] != DBNull.Value) entidad.e_cc_2 = Convert.ToString(resultado["E_CC_2"]);
                            if (resultado["COMISION"] != DBNull.Value) entidad.comision = Convert.ToDecimal(resultado["COMISION"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["TIPO_PROCESAMIENTO"] != DBNull.Value) entidad.tipo_procesamiento = Convert.ToInt32(resultado["TIPO_PROCESAMIENTO"]);
                            if (resultado["CUPO_CAJERO"] != DBNull.Value) entidad.cupo_cajero = Convert.ToDecimal(resultado["CUPO_CAJERO"]);
                            if (resultado["TRANSACCIONES_CAJERO"] != DBNull.Value) entidad.transacciones_cajero = Convert.ToInt32(resultado["TRANSACCIONES_CAJERO"]);
                            if (resultado["CUPO_DATAFONO"] != DBNull.Value) entidad.cupo_datafono = Convert.ToDecimal(resultado["CUPO_DATAFONO"]);
                            if (resultado["TRANSACCIONES_DATAFONO"] != DBNull.Value) entidad.transacciones_datafono = Convert.ToInt32(resultado["TRANSACCIONES_DATAFONO"]);
                            if (resultado["VALOR_CANCELA_TARJETA"] != DBNull.Value) entidad.valor_cancela_tarjeta = Convert.ToDecimal(resultado["VALOR_CANCELA_TARJETA"]);
                            if (resultado["COBRO_CANCELA_TARJETA"] != DBNull.Value) entidad.cobro_cancela_tarjeta = Convert.ToInt32(resultado["COBRO_CANCELA_TARJETA"]);
                            if (resultado["COBRA_TARJETA_BLOQUEADA"] != DBNull.Value) entidad.cobra_tarjeta_bloqueada = Convert.ToInt32(resultado["COBRA_TARJETA_BLOQUEADA"]);
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
                        BOExcepcion.Throw("TarjetaConvenioData", "ConsultarTarjetaConvenio", ex);
                        return null;
                    }
                }
            }
        }


        public List<TarjetaConvenio> ListarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TarjetaConvenio> lstTarjetaConvenio = new List<TarjetaConvenio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro;
                        filtro = ObtenerFiltro(pTarjetaConvenio);
                        if (pTarjetaConvenio != null)
                            if (pTarjetaConvenio.tipo_convenio == 0)
                                filtro = (filtro.ToUpper().Contains("WHERE") ? " AND " : " WHERE ") + " (TIPO_CONVENIO = 0 OR TIPO_CONVENIO IS NULL)";
                        string sql = @"SELECT * FROM TARJETA_CONVENIO " + filtro + " ORDER BY COD_CONVENIO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TarjetaConvenio entidad = new TarjetaConvenio();
                            if (resultado["COD_CONVENIO"] != DBNull.Value) entidad.cod_convenio = Convert.ToString(resultado["COD_CONVENIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CODIGO_BIN"] != DBNull.Value) entidad.codigo_bin = Convert.ToString(resultado["CODIGO_BIN"]);
                            if (resultado["IP_SWITCH"] != DBNull.Value) entidad.ip_switch = Convert.ToString(resultado["IP_SWITCH"]);
                            if (resultado["IP_APPLIANCE"] != DBNull.Value) entidad.ip_appliance = Convert.ToString(resultado["IP_APPLIANCE"]);
                            if (resultado["PUERTO_SWITCH"] != DBNull.Value) entidad.puerto_switch = Convert.ToString(resultado["PUERTO_SWITCH"]);
                            if (resultado["FECHA_CONVENIO"] != DBNull.Value) entidad.fecha_convenio = Convert.ToDateTime(resultado["FECHA_CONVENIO"]);
                            if (resultado["ENCARGADO"] != DBNull.Value) entidad.encargado = Convert.ToString(resultado["ENCARGADO"]);
                            if (resultado["E_MAIL"] != DBNull.Value) entidad.e_mail = Convert.ToString(resultado["E_MAIL"]);
                            if (resultado["E_CC_1"] != DBNull.Value) entidad.e_cc_1 = Convert.ToString(resultado["E_CC_1"]);
                            if (resultado["E_CC_2"] != DBNull.Value) entidad.e_cc_2 = Convert.ToString(resultado["E_CC_2"]);
                            if (resultado["COMISION"] != DBNull.Value) entidad.comision = Convert.ToDecimal(resultado["COMISION"]);
                            if (resultado["CUOTA_MANEJO"] != DBNull.Value) entidad.cuota_manejo = Convert.ToDecimal(resultado["CUOTA_MANEJO"]);
                            if (resultado["TIPO_PROCESAMIENTO"] != DBNull.Value) entidad.tipo_procesamiento = Convert.ToInt32(resultado["TIPO_PROCESAMIENTO"]);
                            if (resultado["CUPO_CAJERO"] != DBNull.Value) entidad.cupo_cajero = Convert.ToDecimal(resultado["CUPO_CAJERO"]);
                            if (resultado["TRANSACCIONES_CAJERO"] != DBNull.Value) entidad.transacciones_cajero = Convert.ToInt32(resultado["TRANSACCIONES_CAJERO"]);
                            if (resultado["CUPO_DATAFONO"] != DBNull.Value) entidad.cupo_datafono = Convert.ToDecimal(resultado["CUPO_DATAFONO"]);
                            if (resultado["TRANSACCIONES_DATAFONO"] != DBNull.Value) entidad.transacciones_datafono = Convert.ToInt32(resultado["TRANSACCIONES_DATAFONO"]);
                            if (resultado["VALOR_CANCELA_TARJETA"] != DBNull.Value) entidad.valor_cancela_tarjeta = Convert.ToDecimal(resultado["VALOR_CANCELA_TARJETA"]);
                            if (resultado["COBRO_CANCELA_TARJETA"] != DBNull.Value) entidad.cobro_cancela_tarjeta = Convert.ToInt32(resultado["COBRO_CANCELA_TARJETA"]);
                            if (resultado["COBRA_TARJETA_BLOQUEADA"] != DBNull.Value) entidad.cobra_tarjeta_bloqueada = Convert.ToInt32(resultado["COBRA_TARJETA_BLOQUEADA"]);
                            if (resultado["USUARIO_APPLIANCE"] != DBNull.Value) entidad.usuario_appliance = Convert.ToString(resultado["USUARIO_APPLIANCE"]);
                            if (resultado["CLAVE_APPLIANCE"] != DBNull.Value) entidad.clave_appliance = Convert.ToString(resultado["CLAVE_APPLIANCE"]);
                            if (resultado["TIPO_CONVENIO"] != DBNull.Value) entidad.tipo_convenio = Convert.ToInt32(resultado["TIPO_CONVENIO"]);
                            lstTarjetaConvenio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTarjetaConvenio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaConvenioData", "ListarTarjetaConvenio", ex);
                        return null;
                    }
                }
            }
        }


        public List<Email> ListaEmailAlerta(Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Email> lstEmail = new List<Email>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from EMAIL_ALERTA"; //  union select e_mail as email, null, null, 2, 1 from empresa where COD_EMPRESA = 1

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Email entidad = new Email();

                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["PROVEEDOR"] != DBNull.Value) entidad.proveedor = Convert.ToInt32(resultado["PROVEEDOR"]);
                            if (resultado["CLAVE"] != DBNull.Value) entidad.Clave = Convert.ToString(resultado["CLAVE"]);
                            if (resultado["TIPO_EMAIL"] != DBNull.Value) entidad.tipo_email = Convert.ToInt32(resultado["TIPO_EMAIL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstEmail.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmail;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TarjetaData", "ListaEmailAlerta", ex);
                        return null;
                    }
                }
            }
        }

    }
}
