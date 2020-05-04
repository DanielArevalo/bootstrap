using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    public class RepVencimientoCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public RepVencimientoCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public RepVencimientoCDAT CrearRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pRepVencimientoCDAT.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "p_numero_cdat";
                        pnumero_cdat.Value = pRepVencimientoCDAT.numero_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pnumero_fisico = cmdTransaccionFactory.CreateParameter();
                        pnumero_fisico.ParameterName = "p_numero_fisico";
                        if (pRepVencimientoCDAT.numero_fisico == null)
                            pnumero_fisico.Value = DBNull.Value;
                        else
                            pnumero_fisico.Value = pRepVencimientoCDAT.numero_fisico;
                        pnumero_fisico.Direction = ParameterDirection.Input;
                        pnumero_fisico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_fisico);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pRepVencimientoCDAT.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        if (pRepVencimientoCDAT.cod_lineacdat == null)
                            pcod_lineacdat.Value = DBNull.Value;
                        else
                            pcod_lineacdat.Value = pRepVencimientoCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pcod_destinacion = cmdTransaccionFactory.CreateParameter();
                        pcod_destinacion.ParameterName = "p_cod_destinacion";
                        if (pRepVencimientoCDAT.cod_destinacion == null)
                            pcod_destinacion.Value = DBNull.Value;
                        else
                            pcod_destinacion.Value = pRepVencimientoCDAT.cod_destinacion;
                        pcod_destinacion.Direction = ParameterDirection.Input;
                        pcod_destinacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destinacion);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pRepVencimientoCDAT.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        if (pRepVencimientoCDAT.modalidad == null)
                            pmodalidad.Value = DBNull.Value;
                        else
                            pmodalidad.Value = pRepVencimientoCDAT.modalidad;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        if (pRepVencimientoCDAT.codforma_captacion == null)
                            pcodforma_captacion.Value = DBNull.Value;
                        else
                            pcodforma_captacion.Value = pRepVencimientoCDAT.codforma_captacion;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        pplazo.Value = pRepVencimientoCDAT.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter ptipo_calendario = cmdTransaccionFactory.CreateParameter();
                        ptipo_calendario.ParameterName = "p_tipo_calendario";
                        ptipo_calendario.Value = pRepVencimientoCDAT.tipo_calendario;
                        ptipo_calendario.Direction = ParameterDirection.Input;
                        ptipo_calendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_calendario);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pRepVencimientoCDAT.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pRepVencimientoCDAT.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pRepVencimientoCDAT.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pRepVencimientoCDAT.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pRepVencimientoCDAT.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pcod_asesor_com = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor_com.ParameterName = "p_cod_asesor_com";
                        if (pRepVencimientoCDAT.cod_asesor_com == null)
                            pcod_asesor_com.Value = DBNull.Value;
                        else
                            pcod_asesor_com.Value = pRepVencimientoCDAT.cod_asesor_com;
                        pcod_asesor_com.Direction = ParameterDirection.Input;
                        pcod_asesor_com.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor_com);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        if (pRepVencimientoCDAT.tipo_interes == null)
                            ptipo_interes.Value = DBNull.Value;
                        else
                            ptipo_interes.Value = pRepVencimientoCDAT.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pRepVencimientoCDAT.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = pRepVencimientoCDAT.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pRepVencimientoCDAT.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pRepVencimientoCDAT.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pRepVencimientoCDAT.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pRepVencimientoCDAT.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pRepVencimientoCDAT.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pRepVencimientoCDAT.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (pRepVencimientoCDAT.cod_periodicidad_int == null)
                            pcod_periodicidad_int.Value = DBNull.Value;
                        else
                            pcod_periodicidad_int.Value = pRepVencimientoCDAT.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pmodalidad_int = cmdTransaccionFactory.CreateParameter();
                        pmodalidad_int.ParameterName = "p_modalidad_int";
                        if (pRepVencimientoCDAT.modalidad_int == null)
                            pmodalidad_int.Value = DBNull.Value;
                        else
                            pmodalidad_int.Value = pRepVencimientoCDAT.modalidad_int;
                        pmodalidad_int.Direction = ParameterDirection.Input;
                        pmodalidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad_int);

                        DbParameter pcapitalizar_int = cmdTransaccionFactory.CreateParameter();
                        pcapitalizar_int.ParameterName = "p_capitalizar_int";
                        if (pRepVencimientoCDAT.capitalizar_int == null)
                            pcapitalizar_int.Value = DBNull.Value;
                        else
                            pcapitalizar_int.Value = pRepVencimientoCDAT.capitalizar_int;
                        pcapitalizar_int.Direction = ParameterDirection.Input;
                        pcapitalizar_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcapitalizar_int);

                        DbParameter pcobra_retencion = cmdTransaccionFactory.CreateParameter();
                        pcobra_retencion.ParameterName = "p_cobra_retencion";
                        if (pRepVencimientoCDAT.cobra_retencion == null)
                            pcobra_retencion.Value = DBNull.Value;
                        else
                            pcobra_retencion.Value = pRepVencimientoCDAT.cobra_retencion;
                        pcobra_retencion.Direction = ParameterDirection.Input;
                        pcobra_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_retencion);

                        DbParameter ptasa_nominal = cmdTransaccionFactory.CreateParameter();
                        ptasa_nominal.ParameterName = "p_tasa_nominal";
                        if (pRepVencimientoCDAT.tasa_nominal == null)
                            ptasa_nominal.Value = DBNull.Value;
                        else
                            ptasa_nominal.Value = pRepVencimientoCDAT.tasa_nominal;
                        ptasa_nominal.Direction = ParameterDirection.Input;
                        ptasa_nominal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_nominal);

                        DbParameter ptasa_efectiva = cmdTransaccionFactory.CreateParameter();
                        ptasa_efectiva.ParameterName = "p_tasa_efectiva";
                        if (pRepVencimientoCDAT.tasa_efectiva == null)
                            ptasa_efectiva.Value = DBNull.Value;
                        else
                            ptasa_efectiva.Value = pRepVencimientoCDAT.tasa_efectiva;
                        ptasa_efectiva.Direction = ParameterDirection.Input;
                        ptasa_efectiva.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_efectiva);

                        DbParameter pintereses_cap = cmdTransaccionFactory.CreateParameter();
                        pintereses_cap.ParameterName = "p_intereses_cap";
                        if (pRepVencimientoCDAT.intereses_cap == null)
                            pintereses_cap.Value = DBNull.Value;
                        else
                            pintereses_cap.Value = pRepVencimientoCDAT.intereses_cap;
                        pintereses_cap.Direction = ParameterDirection.Input;
                        pintereses_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses_cap);

                        DbParameter pretencion_cap = cmdTransaccionFactory.CreateParameter();
                        pretencion_cap.ParameterName = "p_retencion_cap";
                        if (pRepVencimientoCDAT.retencion_cap == null)
                            pretencion_cap.Value = DBNull.Value;
                        else
                            pretencion_cap.Value = pRepVencimientoCDAT.retencion_cap;
                        pretencion_cap.Direction = ParameterDirection.Input;
                        pretencion_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion_cap);

                        DbParameter pfecha_intereses = cmdTransaccionFactory.CreateParameter();
                        pfecha_intereses.ParameterName = "p_fecha_intereses";
                        if (pRepVencimientoCDAT.fecha_intereses == null)
                            pfecha_intereses.Value = DBNull.Value;
                        else
                            pfecha_intereses.Value = pRepVencimientoCDAT.fecha_intereses;
                        pfecha_intereses.Direction = ParameterDirection.Input;
                        pfecha_intereses.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_intereses);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pRepVencimientoCDAT.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pRepVencimientoCDAT.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdesmaterializado = cmdTransaccionFactory.CreateParameter();
                        pdesmaterializado.ParameterName = "p_desmaterializado";
                        if (pRepVencimientoCDAT.desmaterializado == null)
                            pdesmaterializado.Value = DBNull.Value;
                        else
                            pdesmaterializado.Value = pRepVencimientoCDAT.desmaterializado;
                        pdesmaterializado.Direction = ParameterDirection.Input;
                        pdesmaterializado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdesmaterializado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pRepVencimientoCDAT.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pRepVencimientoCDAT.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CDAT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pRepVencimientoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepVencimientoCDATData", "CrearRepVencimientoCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public RepVencimientoCDAT ModificarRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pRepVencimientoCDAT.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "p_numero_cdat";
                        pnumero_cdat.Value = pRepVencimientoCDAT.numero_cdat;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pnumero_fisico = cmdTransaccionFactory.CreateParameter();
                        pnumero_fisico.ParameterName = "p_numero_fisico";
                        if (pRepVencimientoCDAT.numero_fisico == null)
                            pnumero_fisico.Value = DBNull.Value;
                        else
                            pnumero_fisico.Value = pRepVencimientoCDAT.numero_fisico;
                        pnumero_fisico.Direction = ParameterDirection.Input;
                        pnumero_fisico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_fisico);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pRepVencimientoCDAT.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        if (pRepVencimientoCDAT.cod_lineacdat == null)
                            pcod_lineacdat.Value = DBNull.Value;
                        else
                            pcod_lineacdat.Value = pRepVencimientoCDAT.cod_lineacdat;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pcod_destinacion = cmdTransaccionFactory.CreateParameter();
                        pcod_destinacion.ParameterName = "p_cod_destinacion";
                        if (pRepVencimientoCDAT.cod_destinacion == null)
                            pcod_destinacion.Value = DBNull.Value;
                        else
                            pcod_destinacion.Value = pRepVencimientoCDAT.cod_destinacion;
                        pcod_destinacion.Direction = ParameterDirection.Input;
                        pcod_destinacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destinacion);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pRepVencimientoCDAT.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        if (pRepVencimientoCDAT.modalidad == null)
                            pmodalidad.Value = DBNull.Value;
                        else
                            pmodalidad.Value = pRepVencimientoCDAT.modalidad;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        if (pRepVencimientoCDAT.codforma_captacion == null)
                            pcodforma_captacion.Value = DBNull.Value;
                        else
                            pcodforma_captacion.Value = pRepVencimientoCDAT.codforma_captacion;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        pplazo.Value = pRepVencimientoCDAT.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter ptipo_calendario = cmdTransaccionFactory.CreateParameter();
                        ptipo_calendario.ParameterName = "p_tipo_calendario";
                        ptipo_calendario.Value = pRepVencimientoCDAT.tipo_calendario;
                        ptipo_calendario.Direction = ParameterDirection.Input;
                        ptipo_calendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_calendario);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pRepVencimientoCDAT.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pRepVencimientoCDAT.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pRepVencimientoCDAT.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pRepVencimientoCDAT.fecha_vencimiento == null)
                            pfecha_vencimiento.Value = DBNull.Value;
                        else
                            pfecha_vencimiento.Value = pRepVencimientoCDAT.fecha_vencimiento;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pcod_asesor_com = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor_com.ParameterName = "p_cod_asesor_com";
                        if (pRepVencimientoCDAT.cod_asesor_com == null)
                            pcod_asesor_com.Value = DBNull.Value;
                        else
                            pcod_asesor_com.Value = pRepVencimientoCDAT.cod_asesor_com;
                        pcod_asesor_com.Direction = ParameterDirection.Input;
                        pcod_asesor_com.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor_com);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        if (pRepVencimientoCDAT.tipo_interes == null)
                            ptipo_interes.Value = DBNull.Value;
                        else
                            ptipo_interes.Value = pRepVencimientoCDAT.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pRepVencimientoCDAT.tasa_interes == null)
                            ptasa_interes.Value = DBNull.Value;
                        else
                            ptasa_interes.Value = pRepVencimientoCDAT.tasa_interes;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pRepVencimientoCDAT.cod_tipo_tasa == null)
                            pcod_tipo_tasa.Value = DBNull.Value;
                        else
                            pcod_tipo_tasa.Value = pRepVencimientoCDAT.cod_tipo_tasa;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pRepVencimientoCDAT.tipo_historico == null)
                            ptipo_historico.Value = DBNull.Value;
                        else
                            ptipo_historico.Value = pRepVencimientoCDAT.tipo_historico;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pRepVencimientoCDAT.desviacion == null)
                            pdesviacion.Value = DBNull.Value;
                        else
                            pdesviacion.Value = pRepVencimientoCDAT.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (pRepVencimientoCDAT.cod_periodicidad_int == null)
                            pcod_periodicidad_int.Value = DBNull.Value;
                        else
                            pcod_periodicidad_int.Value = pRepVencimientoCDAT.cod_periodicidad_int;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pmodalidad_int = cmdTransaccionFactory.CreateParameter();
                        pmodalidad_int.ParameterName = "p_modalidad_int";
                        if (pRepVencimientoCDAT.modalidad_int == null)
                            pmodalidad_int.Value = DBNull.Value;
                        else
                            pmodalidad_int.Value = pRepVencimientoCDAT.modalidad_int;
                        pmodalidad_int.Direction = ParameterDirection.Input;
                        pmodalidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad_int);

                        DbParameter pcapitalizar_int = cmdTransaccionFactory.CreateParameter();
                        pcapitalizar_int.ParameterName = "p_capitalizar_int";
                        if (pRepVencimientoCDAT.capitalizar_int == null)
                            pcapitalizar_int.Value = DBNull.Value;
                        else
                            pcapitalizar_int.Value = pRepVencimientoCDAT.capitalizar_int;
                        pcapitalizar_int.Direction = ParameterDirection.Input;
                        pcapitalizar_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcapitalizar_int);

                        DbParameter pcobra_retencion = cmdTransaccionFactory.CreateParameter();
                        pcobra_retencion.ParameterName = "p_cobra_retencion";
                        if (pRepVencimientoCDAT.cobra_retencion == null)
                            pcobra_retencion.Value = DBNull.Value;
                        else
                            pcobra_retencion.Value = pRepVencimientoCDAT.cobra_retencion;
                        pcobra_retencion.Direction = ParameterDirection.Input;
                        pcobra_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_retencion);

                        DbParameter ptasa_nominal = cmdTransaccionFactory.CreateParameter();
                        ptasa_nominal.ParameterName = "p_tasa_nominal";
                        if (pRepVencimientoCDAT.tasa_nominal == null)
                            ptasa_nominal.Value = DBNull.Value;
                        else
                            ptasa_nominal.Value = pRepVencimientoCDAT.tasa_nominal;
                        ptasa_nominal.Direction = ParameterDirection.Input;
                        ptasa_nominal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_nominal);

                        DbParameter ptasa_efectiva = cmdTransaccionFactory.CreateParameter();
                        ptasa_efectiva.ParameterName = "p_tasa_efectiva";
                        if (pRepVencimientoCDAT.tasa_efectiva == null)
                            ptasa_efectiva.Value = DBNull.Value;
                        else
                            ptasa_efectiva.Value = pRepVencimientoCDAT.tasa_efectiva;
                        ptasa_efectiva.Direction = ParameterDirection.Input;
                        ptasa_efectiva.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_efectiva);

                        DbParameter pintereses_cap = cmdTransaccionFactory.CreateParameter();
                        pintereses_cap.ParameterName = "p_intereses_cap";
                        if (pRepVencimientoCDAT.intereses_cap == null)
                            pintereses_cap.Value = DBNull.Value;
                        else
                            pintereses_cap.Value = pRepVencimientoCDAT.intereses_cap;
                        pintereses_cap.Direction = ParameterDirection.Input;
                        pintereses_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses_cap);

                        DbParameter pretencion_cap = cmdTransaccionFactory.CreateParameter();
                        pretencion_cap.ParameterName = "p_retencion_cap";
                        if (pRepVencimientoCDAT.retencion_cap == null)
                            pretencion_cap.Value = DBNull.Value;
                        else
                            pretencion_cap.Value = pRepVencimientoCDAT.retencion_cap;
                        pretencion_cap.Direction = ParameterDirection.Input;
                        pretencion_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion_cap);

                        DbParameter pfecha_intereses = cmdTransaccionFactory.CreateParameter();
                        pfecha_intereses.ParameterName = "p_fecha_intereses";
                        if (pRepVencimientoCDAT.fecha_intereses == null)
                            pfecha_intereses.Value = DBNull.Value;
                        else
                            pfecha_intereses.Value = pRepVencimientoCDAT.fecha_intereses;
                        pfecha_intereses.Direction = ParameterDirection.Input;
                        pfecha_intereses.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_intereses);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pRepVencimientoCDAT.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pRepVencimientoCDAT.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdesmaterializado = cmdTransaccionFactory.CreateParameter();
                        pdesmaterializado.ParameterName = "p_desmaterializado";
                        if (pRepVencimientoCDAT.desmaterializado == null)
                            pdesmaterializado.Value = DBNull.Value;
                        else
                            pdesmaterializado.Value = pRepVencimientoCDAT.desmaterializado;
                        pdesmaterializado.Direction = ParameterDirection.Input;
                        pdesmaterializado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdesmaterializado);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pRepVencimientoCDAT.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pRepVencimientoCDAT.observacion;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CDAT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pRepVencimientoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepVencimientoCDATData", "ModificarRepVencimientoCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarRepVencimientoCDAT(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        RepVencimientoCDAT pRepVencimientoCDAT = new RepVencimientoCDAT();
                        pRepVencimientoCDAT = ConsultarRepVencimientoCDAT(pId, vUsuario);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pRepVencimientoCDAT.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CDAT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepVencimientoCDATData", "EliminarRepVencimientoCDAT", ex);
                    }
                }
            }
        }


        public RepVencimientoCDAT ConsultarRepVencimientoCDAT(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            RepVencimientoCDAT entidad = new RepVencimientoCDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CDAT WHERE CODIGO_CDAT = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["COD_DESTINACION"] != DBNull.Value) entidad.cod_destinacion = Convert.ToInt64(resultado["COD_DESTINACION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToString(resultado["TIPO_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["MODALIDAD_INT"] != DBNull.Value) entidad.modalidad_int = Convert.ToInt32(resultado["MODALIDAD_INT"]);
                            if (resultado["CAPITALIZAR_INT"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["CAPITALIZAR_INT"]);
                            if (resultado["COBRA_RETENCION"] != DBNull.Value) entidad.cobra_retencion = Convert.ToInt32(resultado["COBRA_RETENCION"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToDecimal(resultado["TASA_NOMINAL"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToDecimal(resultado["TASA_EFECTIVA"]);
                            if (resultado["INTERESES_CAP"] != DBNull.Value) entidad.intereses_cap = Convert.ToDecimal(resultado["INTERESES_CAP"]);
                            if (resultado["RETENCION_CAP"] != DBNull.Value) entidad.retencion_cap = Convert.ToDecimal(resultado["RETENCION_CAP"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DESMATERIALIZADO"] != DBNull.Value) entidad.desmaterializado = Convert.ToInt32(resultado["DESMATERIALIZADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
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
                        BOExcepcion.Throw("RepVencimientoCDATData", "ConsultarRepVencimientoCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public List<RepVencimientoCDAT> ListarRepVencimientoCDAT(String[] pfiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<RepVencimientoCDAT> lstRepVencimientoCDAT = new List<RepVencimientoCDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.CODIGO_CDAT, C.NUMERO_CDAT,C.FECHA_APERTURA,C.FECHA_VENCIMIENTO,C.VALOR,C.PLAZO,"
                                     +"P.IDENTIFICACION,P.NOMBRES,P.APELLIDOS,P.DIRECCION,P.TELEFONO,C.TASA_EFECTIVA,"
                                     + "C.TASA_NOMINAL,CASE C.MODALIDAD_INT WHEN 1 THEN 'Vencido' WHEN 2 THEN 'Anticipado' ELSE '' END MODALIDAD,NVL(PE.DESCRIPCION,'Al Vencimiento') DESCRIPCION "
                                     + "FROM CDAT C LEFT JOIN CDAT_TITULAR CT ON C.CODIGO_CDAT = CT.CODIGO_CDAT "
                                     + "LEFT JOIN PERIODICIDAD PE ON C.COD_PERIODICIDAD_INT=COD_PERIODICIDAD "
                                     + "LEFT JOIN V_PERSONA P ON CT.COD_PERSONA = P.COD_PERSONA "
                                     +"WHERE C.ESTADO=2 AND CT.PRINCIPAL=1 ";

                        string sFiltro = "";
                        Configuracion conf = new Configuracion();
                        
                        
                        if (pfiltro[0] != "" && pfiltro[1] != "")
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sFiltro = sFiltro + " and  Trunc(C.FECHA_VENCIMIENTO) between TO_DATE('" + pfiltro[0] + "','" + conf.ObtenerFormatoFecha() + "')  and TO_DATE('" + pfiltro[1] + "','" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sFiltro = sFiltro + " and  C.FECHA_VENCIMIENTO '" + pfiltro[0] + "' and '" + pfiltro[1] + "' ";
                        }
                        if (pfiltro[2].Trim() != "" && pfiltro[2].Trim() != "0")
                        {
                            sFiltro = " and C.COD_OFICINA  = " + pfiltro[2].Trim() + "";
                        }
                        if (sFiltro != "")
                        {
                            sql = sql + sFiltro + " order by  C.FECHA_VENCIMIENTO";
                        }
                        else
                        {
                            sql = sql + " order by  C.FECHA_VENCIMIENTO";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RepVencimientoCDAT entidad = new RepVencimientoCDAT();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                             if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToDecimal(resultado["TASA_EFECTIVA"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToDecimal(resultado["TASA_NOMINAL"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.nom_modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_periodo = Convert.ToString(resultado["DESCRIPCION"]);

                            lstRepVencimientoCDAT.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstRepVencimientoCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RepVencimientoCDATData", "ListarRepVencimientoCDAT", ex);
                        return null;
                    }
                }
            }
        }


    }
}
/*namespace Xpinn.CDATS.Data
{
    class RepVencimientoCDATData
    {
    }
}*/
