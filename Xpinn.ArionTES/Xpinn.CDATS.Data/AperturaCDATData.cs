using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.Imagenes.Data;



namespace Xpinn.CDATS.Data
{
    public class AperturaCDATData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AperturaCDATData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Cdat CrearAperturaCDAT(Cdat pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Output;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "p_numero_cdat";
                        if (pCdat.numero_cdat != null) pnumero_cdat.Value = pCdat.numero_cdat; else pnumero_cdat.Value = DBNull.Value;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pnumero_fisico = cmdTransaccionFactory.CreateParameter();
                        pnumero_fisico.ParameterName = "p_numero_fisico";
                        if (pCdat.numero_fisico != null) pnumero_fisico.Value = pCdat.numero_fisico; else pnumero_fisico.Value = DBNull.Value;
                        pnumero_fisico.Direction = ParameterDirection.Input;
                        pnumero_fisico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_fisico);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pCdat.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        if (pCdat.cod_lineacdat != null) pcod_lineacdat.Value = pCdat.cod_lineacdat; else pcod_lineacdat.Value = DBNull.Value;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pcod_destinacion = cmdTransaccionFactory.CreateParameter();
                        pcod_destinacion.ParameterName = "p_cod_destinacion";
                        pcod_destinacion.Value = pCdat.cod_destinacion;
                        pcod_destinacion.Direction = ParameterDirection.Input;
                        pcod_destinacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destinacion);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pCdat.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        pmodalidad.Value = pCdat.modalidad;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        if (pCdat.codforma_captacion != 0) pcodforma_captacion.Value = pCdat.codforma_captacion; else pcodforma_captacion.Value = DBNull.Value;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        pplazo.Value = pCdat.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter ptipo_calendario = cmdTransaccionFactory.CreateParameter();
                        ptipo_calendario.ParameterName = "p_tipo_calendario";
                        ptipo_calendario.Value = pCdat.tipo_calendario;
                        ptipo_calendario.Direction = ParameterDirection.Input;
                        ptipo_calendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_calendario);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCdat.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pCdat.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pCdat.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pCdat.fecha_vencimiento != DateTime.MinValue) pfecha_vencimiento.Value = pCdat.fecha_vencimiento; else pfecha_vencimiento.Value = DBNull.Value;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pcod_asesor_com = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor_com.ParameterName = "p_cod_asesor_com";
                        pcod_asesor_com.Value = pCdat.cod_asesor_com;
                        pcod_asesor_com.Direction = ParameterDirection.Input;
                        pcod_asesor_com.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor_com);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pCdat.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pCdat.tasa_interes != 0)
                            ptasa_interes.Value = pCdat.tasa_interes;
                        else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pCdat.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pCdat.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pCdat.tipo_historico != 0)
                            ptipo_historico.Value = pCdat.tipo_historico;
                        else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pCdat.desviacion != 0) pdesviacion.Value = pCdat.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (!(pCdat.cod_periodicidad_int == 0 || pCdat.cod_periodicidad_int == null)) pcod_periodicidad_int.Value = pCdat.cod_periodicidad_int; else pcod_periodicidad_int.Value = DBNull.Value;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pmodalidad_int = cmdTransaccionFactory.CreateParameter();
                        pmodalidad_int.ParameterName = "p_modalidad_int";
                        pmodalidad_int.Value = pCdat.modalidad_int;
                        pmodalidad_int.Direction = ParameterDirection.Input;
                        pmodalidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad_int);

                        DbParameter pcapitalizar_int = cmdTransaccionFactory.CreateParameter();
                        pcapitalizar_int.ParameterName = "p_capitalizar_int";
                        pcapitalizar_int.Value = pCdat.capitalizar_int;
                        pcapitalizar_int.Direction = ParameterDirection.Input;
                        pcapitalizar_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcapitalizar_int);

                        DbParameter pcobra_retencion = cmdTransaccionFactory.CreateParameter();
                        pcobra_retencion.ParameterName = "p_cobra_retencion";
                        pcobra_retencion.Value = pCdat.cobra_retencion;
                        pcobra_retencion.Direction = ParameterDirection.Input;
                        pcobra_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_retencion);

                        DbParameter ptasa_nominal = cmdTransaccionFactory.CreateParameter();
                        ptasa_nominal.ParameterName = "p_tasa_nominal";
                        if (pCdat.tasa_nominal != 0) ptasa_nominal.Value = pCdat.tasa_nominal; else ptasa_nominal.Value = DBNull.Value;
                        ptasa_nominal.Direction = ParameterDirection.Input;
                        ptasa_nominal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_nominal);

                        DbParameter ptasa_efectiva = cmdTransaccionFactory.CreateParameter();
                        ptasa_efectiva.ParameterName = "p_tasa_efectiva";
                        if (pCdat.tasa_efectiva != 0) ptasa_efectiva.Value = pCdat.tasa_efectiva; else ptasa_efectiva.Value = DBNull.Value;
                        ptasa_efectiva.Direction = ParameterDirection.Input;
                        ptasa_efectiva.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_efectiva);

                        DbParameter pintereses_cap = cmdTransaccionFactory.CreateParameter();
                        pintereses_cap.ParameterName = "p_intereses_cap";
                        if (pCdat.intereses_cap != 0)
                            pintereses_cap.Value = pCdat.intereses_cap;
                        else
                            pintereses_cap.Value = 0;
                        pintereses_cap.Direction = ParameterDirection.Input;
                        pintereses_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses_cap);

                        DbParameter pretencion_cap = cmdTransaccionFactory.CreateParameter();
                        pretencion_cap.ParameterName = "p_retencion_cap";
                        if (pCdat.retencion_cap != 0)
                            pretencion_cap.Value = pCdat.retencion_cap;
                        else
                            pretencion_cap.Value = 0;
                        pretencion_cap.Direction = ParameterDirection.Input;
                        pretencion_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion_cap);

                        DbParameter pfecha_intereses = cmdTransaccionFactory.CreateParameter();
                        pfecha_intereses.ParameterName = "p_fecha_intereses";
                        if (pCdat.fecha_intereses != DateTime.MinValue)
                            pfecha_intereses.Value = pCdat.fecha_intereses;
                        else
                            pfecha_intereses.Value = DBNull.Value;
                        pfecha_intereses.Direction = ParameterDirection.Input;
                        pfecha_intereses.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_intereses);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCdat.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdesmaterializado = cmdTransaccionFactory.CreateParameter();
                        pdesmaterializado.ParameterName = "p_desmaterializado";
                        pdesmaterializado.Value = pCdat.desmaterializado;
                        pdesmaterializado.Direction = ParameterDirection.Input;
                        pdesmaterializado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdesmaterializado);

                        DbParameter popcion = cmdTransaccionFactory.CreateParameter();
                        popcion.ParameterName = "p_opcion";
                        popcion.Value = pCdat.origen;
                        popcion.Direction = ParameterDirection.Input;
                        popcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(popcion);

                        DbParameter P_CDAT_RENOVADO = cmdTransaccionFactory.CreateParameter();
                        P_CDAT_RENOVADO.ParameterName = "P_CDATRENOVADO";
                        P_CDAT_RENOVADO.Value = pCdat.cdat_renovado;
                        P_CDAT_RENOVADO.Direction = ParameterDirection.Input;
                        popcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_CDAT_RENOVADO);

                        DbParameter pcod_ope= cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "P_COD_OPE";
                        pcod_ope.Value = pCdat.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_CODPERSONA";
                        P_COD_PERSONA.Value = pCdat.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        popcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter p_capitalizar = cmdTransaccionFactory.CreateParameter();
                        p_capitalizar.ParameterName = "P_CAPITALIZA";
                        p_capitalizar.Value = pCdat.capitalizar_int_old;
                        p_capitalizar.Direction = ParameterDirection.Input;
                        p_capitalizar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_capitalizar);

                        DbParameter p_valorcapitalizar = cmdTransaccionFactory.CreateParameter();
                        p_valorcapitalizar.ParameterName = "P_VALOR_CAPITALIZA";
                        p_valorcapitalizar.Value = pCdat.valor_capitalizar;
                        p_valorcapitalizar.Direction = ParameterDirection.Input;
                        p_valorcapitalizar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_valorcapitalizar);

                        //Formas de Pago Cdat's

                        DbParameter pCod_Forma_Pago = cmdTransaccionFactory.CreateParameter();
                        pCod_Forma_Pago.ParameterName = "P_CODFORM";
                        pCod_Forma_Pago.Value = pCdat.forma_pago;
                        pCod_Forma_Pago.Direction = ParameterDirection.Input;
                        pCod_Forma_Pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCod_Forma_Pago);

                        DbParameter pNumero_Cuenta = cmdTransaccionFactory.CreateParameter();
                        pNumero_Cuenta.ParameterName = "P_NUMCUENTA";
                        pNumero_Cuenta.Value = pCdat.numero_cuenta;
                        pNumero_Cuenta.Direction = ParameterDirection.Input;
                        pNumero_Cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pNumero_Cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_APERTURA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCdat.codigo_cdat = Convert.ToInt64(pcodigo_cdat.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pCdat, "CDAT", vUsuario, Accion.Crear.ToString(), TipoAuditoria.CDAT, "Creacion de CDAT con numero de cdat " + pnumero_cdat.Value); //REGISTRO DE AUDITORIA

                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearAperturaCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public string CrearSolicitudCDAT(Cdat pCDAT, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_PRODUCTO.ParameterName = "P_ID_SOL_PRODUCTO";
                        P_ID_SOL_PRODUCTO.Value = 0;
                        P_ID_SOL_PRODUCTO.Direction = ParameterDirection.Output;
                        P_ID_SOL_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_PRODUCTO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pCDAT.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_COD_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_COD_TIPO_PRODUCTO.ParameterName = "P_COD_TIPO_PRODUCTO";
                        P_COD_TIPO_PRODUCTO.Value = pCDAT.cod_tipo_producto;
                        P_COD_TIPO_PRODUCTO.Direction = ParameterDirection.Input;
                        P_COD_TIPO_PRODUCTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_TIPO_PRODUCTO);

                        DbParameter P_LINEA = cmdTransaccionFactory.CreateParameter();
                        P_LINEA.ParameterName = "P_LINEA";
                        P_LINEA.Value = pCDAT.cod_lineacdat;
                        P_LINEA.Direction = ParameterDirection.Input;
                        P_LINEA.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_LINEA);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = pCDAT.plazo;
                        P_PLAZO.Direction = ParameterDirection.Input;
                        P_PLAZO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        DbParameter P_NUM_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUM_CUOTAS.ParameterName = "P_NUM_CUOTAS";
                        P_NUM_CUOTAS.Value = 0;
                        P_NUM_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUM_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NUM_CUOTAS);

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = pCDAT.valor;
                        P_VALOR.Direction = ParameterDirection.Input;
                        P_VALOR.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = 0;
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;
                        P_PERIODICIDAD.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = 0;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;
                        P_FORMA_PAGO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_COD_DESTINO = cmdTransaccionFactory.CreateParameter();
                        P_COD_DESTINO.ParameterName = "P_COD_DESTINO";
                        P_COD_DESTINO.Value = 0;
                        P_COD_DESTINO.Direction = ParameterDirection.Input;
                        P_COD_DESTINO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_COD_DESTINO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pCDAT.estado_modificacion1;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODUCTO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (Convert.ToInt32(P_ID_SOL_PRODUCTO.Value) != 0)
                        {
                            int salida = Convert.ToInt32(P_ID_SOL_PRODUCTO.Value);
                            if (pCDAT.lstBenef != null && pCDAT.lstBenef.Count > 0)
                            {

                                //CARGA LOS DOCUMENTOS 
                                /*******************ACTUALIZO CON DOCUMENTOS*********************/
                                ImagenesORAData DAImagenes = new ImagenesORAData();
                                if (pCDAT.consignacion != null || pCDAT.declaracion != null)
                                    DAImagenes.guardarDoctCDAT(salida.ToString(), pCDAT.consignacion, pCDAT.declaracion, pUsuario);

                                //Crea los registros de beneficiarios del CDAT
                                foreach (Beneficiarios item in pCDAT.lstBenef)
                                {
                                    DbParameter P_IDBENEFICIARIO = cmdTransaccionFactory.CreateParameter();
                                    P_IDBENEFICIARIO.ParameterName = "P_IDBENEFICIARIO ";
                                    P_IDBENEFICIARIO.Value = 0;
                                    P_IDBENEFICIARIO.Direction = ParameterDirection.Output;
                                    P_IDBENEFICIARIO.DbType = DbType.Int32;
                                    cmdTransaccionFactory.Parameters.Add(P_IDBENEFICIARIO);

                                    DbParameter P_NUMERO_SOL = cmdTransaccionFactory.CreateParameter();
                                    P_NUMERO_SOL.ParameterName = "P_NUMERO_SOL ";
                                    P_NUMERO_SOL.Value = salida;
                                    P_NUMERO_SOL.Direction = ParameterDirection.Input;
                                    P_NUMERO_SOL.DbType = DbType.Int32;
                                    cmdTransaccionFactory.Parameters.Add(P_NUMERO_SOL);

                                    DbParameter P_IDENTIFICACION_BEN = cmdTransaccionFactory.CreateParameter();
                                    P_IDENTIFICACION_BEN.ParameterName = "P_IDENTIFICACION_BEN";
                                    P_IDENTIFICACION_BEN.Value = item.identificacion_ben;
                                    P_IDENTIFICACION_BEN.Direction = ParameterDirection.Input;
                                    P_IDENTIFICACION_BEN.DbType = DbType.String;
                                    cmdTransaccionFactory.Parameters.Add(P_IDENTIFICACION_BEN);

                                    DbParameter P_NOMBRE_BEN = cmdTransaccionFactory.CreateParameter();
                                    P_NOMBRE_BEN.ParameterName = "P_NOMBRE_BEN";
                                    P_NOMBRE_BEN.Value = item.nombre_ben;
                                    P_NOMBRE_BEN.Direction = ParameterDirection.Input;
                                    P_NOMBRE_BEN.DbType = DbType.String;
                                    cmdTransaccionFactory.Parameters.Add(P_NOMBRE_BEN);

                                    DbParameter P_PARENTESCO = cmdTransaccionFactory.CreateParameter();
                                    P_PARENTESCO.ParameterName = "P_PARENTESCO";
                                    P_PARENTESCO.Value = item.parentesco;
                                    P_PARENTESCO.Direction = ParameterDirection.Input;
                                    P_PARENTESCO.DbType = DbType.Int32;
                                    cmdTransaccionFactory.Parameters.Add(P_PARENTESCO);

                                    DbParameter P_PORCENTAJE_BEN = cmdTransaccionFactory.CreateParameter();
                                    P_PORCENTAJE_BEN.ParameterName = "P_PORCENTAJE_BEN";
                                    P_PORCENTAJE_BEN.Value = 0;
                                    P_PORCENTAJE_BEN.Direction = ParameterDirection.Input;
                                    P_PORCENTAJE_BEN.DbType = DbType.Int32;
                                    cmdTransaccionFactory.Parameters.Add(P_PORCENTAJE_BEN);
                                    
                                    connection.Open();
                                    cmdTransaccionFactory.Connection = connection;
                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLPRODBENEF_CRE";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                    dbConnectionFactory.CerrarConexion(connection);
                                }
                            }

                            return P_ID_SOL_PRODUCTO.Value.ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearSolicitudCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public Cdat ModificarAperturaCDAT(Cdat pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Cdat cdatAnterior = ConsultarApertu(pCdat.codigo_cdat, vUsuario);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pnumero_cdat = cmdTransaccionFactory.CreateParameter();
                        pnumero_cdat.ParameterName = "p_numero_cdat";
                        if (pCdat.numero_cdat != null) pnumero_cdat.Value = pCdat.numero_cdat; else pnumero_cdat.Value = DBNull.Value;
                        pnumero_cdat.Direction = ParameterDirection.Input;
                        pnumero_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cdat);

                        DbParameter pnumero_fisico = cmdTransaccionFactory.CreateParameter();
                        pnumero_fisico.ParameterName = "p_numero_fisico";
                        if (pCdat.numero_fisico != null) pnumero_fisico.Value = pCdat.numero_fisico; else pnumero_fisico.Value = DBNull.Value;
                        pnumero_fisico.Direction = ParameterDirection.Input;
                      //  pnumero_fisico.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_fisico);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pCdat.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_lineacdat = cmdTransaccionFactory.CreateParameter();
                        pcod_lineacdat.ParameterName = "p_cod_lineacdat";
                        if (pCdat.cod_lineacdat != null) pcod_lineacdat.Value = pCdat.cod_lineacdat; else pcod_lineacdat.Value = DBNull.Value;
                        pcod_lineacdat.Direction = ParameterDirection.Input;
                        pcod_lineacdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_lineacdat);

                        DbParameter pcod_destinacion = cmdTransaccionFactory.CreateParameter();
                        pcod_destinacion.ParameterName = "p_cod_destinacion";
                        pcod_destinacion.Value = pCdat.cod_destinacion;
                        pcod_destinacion.Direction = ParameterDirection.Input;
                        pcod_destinacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destinacion);

                        DbParameter pfecha_apertura = cmdTransaccionFactory.CreateParameter();
                        pfecha_apertura.ParameterName = "p_fecha_apertura";
                        pfecha_apertura.Value = pCdat.fecha_apertura;
                        pfecha_apertura.Direction = ParameterDirection.Input;
                        pfecha_apertura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_apertura);

                        DbParameter pmodalidad = cmdTransaccionFactory.CreateParameter();
                        pmodalidad.ParameterName = "p_modalidad";
                        pmodalidad.Value = pCdat.modalidad;
                        pmodalidad.Direction = ParameterDirection.Input;
                        pmodalidad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad);

                        DbParameter pcodforma_captacion = cmdTransaccionFactory.CreateParameter();
                        pcodforma_captacion.ParameterName = "p_codforma_captacion";
                        if (pCdat.codforma_captacion != 0) pcodforma_captacion.Value = pCdat.codforma_captacion; else pcodforma_captacion.Value = DBNull.Value;
                        pcodforma_captacion.Direction = ParameterDirection.Input;
                        pcodforma_captacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodforma_captacion);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        pplazo.Value = pCdat.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter ptipo_calendario = cmdTransaccionFactory.CreateParameter();
                        ptipo_calendario.ParameterName = "p_tipo_calendario";
                        ptipo_calendario.Value = pCdat.tipo_calendario;
                        ptipo_calendario.Direction = ParameterDirection.Input;
                        ptipo_calendario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_calendario);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCdat.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pCdat.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pfecha_inicio = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio.ParameterName = "p_fecha_inicio";
                        pfecha_inicio.Value = pCdat.fecha_inicio;
                        pfecha_inicio.Direction = ParameterDirection.Input;
                        pfecha_inicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pCdat.fecha_vencimiento != DateTime.MinValue) pfecha_vencimiento.Value = pCdat.fecha_vencimiento; else pfecha_vencimiento.Value = DBNull.Value;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pcod_asesor_com = cmdTransaccionFactory.CreateParameter();
                        pcod_asesor_com.ParameterName = "p_cod_asesor_com";
                        pcod_asesor_com.Value = pCdat.cod_asesor_com;
                        pcod_asesor_com.Direction = ParameterDirection.Input;
                        pcod_asesor_com.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_asesor_com);

                        DbParameter ptipo_interes = cmdTransaccionFactory.CreateParameter();
                        ptipo_interes.ParameterName = "p_tipo_interes";
                        ptipo_interes.Value = pCdat.tipo_interes;
                        ptipo_interes.Direction = ParameterDirection.Input;
                        ptipo_interes.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_interes);

                        DbParameter ptasa_interes = cmdTransaccionFactory.CreateParameter();
                        ptasa_interes.ParameterName = "p_tasa_interes";
                        if (pCdat.tasa_interes != 0) ptasa_interes.Value = pCdat.tasa_interes; else ptasa_interes.Value = DBNull.Value;
                        ptasa_interes.Direction = ParameterDirection.Input;
                        ptasa_interes.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_interes);

                        DbParameter pcod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        if (pCdat.cod_tipo_tasa != 0) pcod_tipo_tasa.Value = pCdat.cod_tipo_tasa; else pcod_tipo_tasa.Value = DBNull.Value;
                        pcod_tipo_tasa.Direction = ParameterDirection.Input;
                        pcod_tipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_tasa);

                        DbParameter ptipo_historico = cmdTransaccionFactory.CreateParameter();
                        ptipo_historico.ParameterName = "p_tipo_historico";
                        if (pCdat.tipo_historico != 0) ptipo_historico.Value = pCdat.tipo_historico; else ptipo_historico.Value = DBNull.Value;
                        ptipo_historico.Direction = ParameterDirection.Input;
                        ptipo_historico.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_historico);

                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "p_desviacion";
                        if (pCdat.desviacion != 0) pdesviacion.Value = pCdat.desviacion; else pdesviacion.Value = DBNull.Value;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pcod_periodicidad_int = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad_int.ParameterName = "p_cod_periodicidad_int";
                        if (pCdat.cod_periodicidad_int != 0) pcod_periodicidad_int.Value = pCdat.cod_periodicidad_int; else pcod_periodicidad_int.Value = DBNull.Value;
                        pcod_periodicidad_int.Direction = ParameterDirection.Input;
                        pcod_periodicidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad_int);

                        DbParameter pmodalidad_int = cmdTransaccionFactory.CreateParameter();
                        pmodalidad_int.ParameterName = "p_modalidad_int";
                        pmodalidad_int.Value = pCdat.modalidad_int;
                        pmodalidad_int.Direction = ParameterDirection.Input;
                        pmodalidad_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmodalidad_int);

                        DbParameter pcapitalizar_int = cmdTransaccionFactory.CreateParameter();
                        pcapitalizar_int.ParameterName = "p_capitalizar_int";
                        pcapitalizar_int.Value = pCdat.capitalizar_int;
                        pcapitalizar_int.Direction = ParameterDirection.Input;
                        pcapitalizar_int.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcapitalizar_int);

                        DbParameter pcobra_retencion = cmdTransaccionFactory.CreateParameter();
                        pcobra_retencion.ParameterName = "p_cobra_retencion";
                        pcobra_retencion.Value = pCdat.cobra_retencion;
                        pcobra_retencion.Direction = ParameterDirection.Input;
                        pcobra_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_retencion);

                        DbParameter ptasa_nominal = cmdTransaccionFactory.CreateParameter();
                        ptasa_nominal.ParameterName = "p_tasa_nominal";
                        if (pCdat.tasa_nominal != 0) ptasa_nominal.Value = pCdat.tasa_nominal; else ptasa_nominal.Value = DBNull.Value;
                        ptasa_nominal.Direction = ParameterDirection.Input;
                        ptasa_nominal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_nominal);

                        DbParameter ptasa_efectiva = cmdTransaccionFactory.CreateParameter();
                        ptasa_efectiva.ParameterName = "p_tasa_efectiva";
                        if (pCdat.tasa_efectiva != 0) ptasa_efectiva.Value = pCdat.tasa_efectiva; else ptasa_efectiva.Value = DBNull.Value;
                        ptasa_efectiva.Direction = ParameterDirection.Input;
                        ptasa_efectiva.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa_efectiva);

                        DbParameter pintereses_cap = cmdTransaccionFactory.CreateParameter();
                        pintereses_cap.ParameterName = "p_intereses_cap";
                        if (pCdat.intereses_cap != 0) pintereses_cap.Value = pCdat.intereses_cap; else pintereses_cap.Value = DBNull.Value;
                        pintereses_cap.Direction = ParameterDirection.Input;
                        pintereses_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pintereses_cap);

                        DbParameter pretencion_cap = cmdTransaccionFactory.CreateParameter();
                        pretencion_cap.ParameterName = "p_retencion_cap";
                        if (pCdat.retencion_cap != 0) pretencion_cap.Value = pCdat.retencion_cap; else pretencion_cap.Value = DBNull.Value;
                        pretencion_cap.Direction = ParameterDirection.Input;
                        pretencion_cap.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pretencion_cap);

                        DbParameter pfecha_intereses = cmdTransaccionFactory.CreateParameter();
                        pfecha_intereses.ParameterName = "p_fecha_intereses";
                        if (pCdat.fecha_intereses != DateTime.MinValue) pfecha_intereses.Value = pCdat.fecha_intereses; else pfecha_intereses.Value = DBNull.Value;
                        pfecha_intereses.Direction = ParameterDirection.Input;
                        pfecha_intereses.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_intereses);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCdat.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pdesmaterializado = cmdTransaccionFactory.CreateParameter();
                        pdesmaterializado.ParameterName = "p_desmaterializado";
                        pdesmaterializado.Value = pCdat.desmaterializado;
                        pdesmaterializado.Direction = ParameterDirection.Input;
                        pdesmaterializado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdesmaterializado);


                        //NUEVOS PARAMETROS 

                        DbParameter pCod_Forma_Pago = cmdTransaccionFactory.CreateParameter();
                        pCod_Forma_Pago.ParameterName = "P_CODFORM";
                        pCod_Forma_Pago.Value = pCdat.forma_pago;
                        pCod_Forma_Pago.Direction = ParameterDirection.Input;
                        pCod_Forma_Pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCod_Forma_Pago);

                        DbParameter pNumero_Cuenta = cmdTransaccionFactory.CreateParameter();
                        pNumero_Cuenta.ParameterName = "P_NUMCUENTA";
                        pNumero_Cuenta.Value = pCdat.numero_cuenta;
                        pNumero_Cuenta.Direction = ParameterDirection.Input;
                      //  pNumero_Cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pNumero_Cuenta);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_APERTURA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        DAauditoria.InsertarLog(pCdat, "CDAT", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.CDAT, "Modificacion de CDAT con numero de cdat " + pnumero_cdat.Value, cdatAnterior); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);

                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ModificarAperturaCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarAperturaCdat(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pId;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_APERTURA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "EliminarAperturaCdat", ex);
                    }
                }
            }
        }

        public List<Cdat> Listardatos(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstDatos = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"select cdat.*,v_persona.identificacion,v_persona.nombre,credito.monto_desembolsado,credito.numero_radicacion from cdat c inner join v_persona  on cdat.codigo_cdat=v_persona.cod_persona inner join credito on cdat.cod_oficina=cdat.cod_oficina " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToInt32(resultado["TASA_NOMINAL"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToInt32(resultado["TASA_EFECTIVA"]);
                            if (resultado["MONTO_DESEMBOLSADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_DESEMBOLSADO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstDatos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CdatData", "Listardatos", ex);
                        return null;
                    }
                }
            }
        }


        public List<Cdat> Listarsimulacion(Cdat vapertura, DateTime FechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstDatos = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter Psimulacion = cmdTransaccionFactory.CreateParameter();
                        Psimulacion.ParameterName = "PFECHA_SIMULACION";
                        Psimulacion.Value = FechaApe;
                        Psimulacion.Direction = ParameterDirection.Input;
                        Psimulacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(Psimulacion);

                        DbParameter Pvalor = cmdTransaccionFactory.CreateParameter();
                        Pvalor.ParameterName = "PVALOR";
                        Pvalor.Value = vapertura.valor;
                        Pvalor.Direction = ParameterDirection.Input;
                        Pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(Pvalor);

                        DbParameter pmoneda = cmdTransaccionFactory.CreateParameter();
                        pmoneda.ParameterName = "PMONEDA";
                        pmoneda.Value = vapertura.cod_moneda;
                        pmoneda.Direction = ParameterDirection.Input;
                        pmoneda.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmoneda);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "PPLAZO";
                        pplazo.Value = vapertura.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pcalculo = cmdTransaccionFactory.CreateParameter();
                        pcalculo.ParameterName = "PCALCULO_TASA";
                        pcalculo.Value = vapertura.tipo_interes;
                        pcalculo.Direction = ParameterDirection.Input;
                        pcalculo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcalculo);


                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "PTASA_INTERES";
                        ptasa.Value = vapertura.tasa_interes;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipotasa = cmdTransaccionFactory.CreateParameter();
                        ptipotasa.ParameterName = "PCOD_TIPO_TASA";
                        ptipotasa.Value = vapertura.cod_tipo_tasa;
                        ptipotasa.Direction = ParameterDirection.Input;
                        ptipotasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipotasa);

                        DbParameter ptipohis = cmdTransaccionFactory.CreateParameter();
                        ptipohis.ParameterName = "PTIPO_HISTORICO";
                        ptipohis.Value = vapertura.tipo_historico;
                        ptipohis.Direction = ParameterDirection.Input;
                        ptipohis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipohis);


                        DbParameter pdesviacion = cmdTransaccionFactory.CreateParameter();
                        pdesviacion.ParameterName = "PDESVIACION";
                        pdesviacion.Value = vapertura.desviacion;
                        pdesviacion.Direction = ParameterDirection.Input;
                        pdesviacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdesviacion);

                        DbParameter pperiodicidad = cmdTransaccionFactory.CreateParameter();
                        pperiodicidad.ParameterName = "PCOD_PERIODICIDAD_INT";
                        if (vapertura.cod_periodicidad_int != null) pperiodicidad.Value = vapertura.cod_periodicidad_int; else pperiodicidad.Value = DBNull.Value;
                        pperiodicidad.Direction = ParameterDirection.Input;
                        pperiodicidad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pperiodicidad);

                        DbParameter pcapitaliza = cmdTransaccionFactory.CreateParameter();
                        pcapitaliza.ParameterName = "PCAPITALIZA";
                        pcapitaliza.Value = vapertura.capitalizar_int;
                        pcapitaliza.Direction = ParameterDirection.Input;
                        pcapitaliza.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcapitaliza);

                        DbParameter pcobra = cmdTransaccionFactory.CreateParameter();
                        pcobra.ParameterName = "PCOBRA_RETENCION";
                        pcobra.Value = vapertura.cobra_retencion;
                        pcobra.Direction = ParameterDirection.Input;
                        pcobra.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcobra);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_SIMULAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select * From TEMP_SIMULACDAT";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();

                            if (resultado["INTERES"] != DBNull.Value) entidad.intereses_cap = Convert.ToInt32(resultado["INTERES"].ToString());
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion_cap = Convert.ToDecimal(resultado["RETENCION"].ToString());
                            if (resultado["FECHA_INT"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INT"]);
                            if (resultado["VALOR_CAPITALIZAR"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["VALOR_CAPITALIZAR"]);
                            if (resultado["VALOR_PAGAR"] != DBNull.Value) entidad.valor = Convert.ToInt32(resultado["VALOR_PAGAR"]);
                            if (resultado["DIAS_LIQUIDA"] != DBNull.Value) entidad.dias_liquidacion = Convert.ToInt32(resultado["DIAS_LIQUIDA"]);





                            lstDatos.Add(entidad);
                        }
                        return lstDatos;
                    }


                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CdatData", "Listardatos", ex);
                        return null;
                    }
                }
            }
        }





        public List<Detalle_CDAT> Liquidar(AdministracionCDAT Liquidacion, Usuario vUsuario)
        {
            List<Cdat> lstDatos = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter Psimulacion = cmdTransaccionFactory.CreateParameter();
                        Psimulacion.ParameterName = "P_FECHA_LIQUIDACION";
                        Psimulacion.Value = Liquidacion.fecha_apertura;
                        Psimulacion.Direction = ParameterDirection.Input;
                        Psimulacion.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(Psimulacion);

                        DbParameter Pvalor = cmdTransaccionFactory.CreateParameter();
                        Pvalor.ParameterName = "PNUMERO_CDAT";
                        Pvalor.Value = Liquidacion.numero_cdat;
                        Pvalor.Direction = ParameterDirection.Input;
                        Pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(Pvalor);

                        DbParameter pmoneda = cmdTransaccionFactory.CreateParameter();
                        pmoneda.ParameterName = "PVALOR";
                        pmoneda.Value = Liquidacion.valor;
                        pmoneda.Direction = ParameterDirection.Output;
                        pmoneda.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pmoneda);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "PINTERES_CAPITALZADO";
                        pplazo.Value = Liquidacion.intereses_cap;
                        pplazo.Direction = ParameterDirection.Output;
                        pplazo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pcalculo = cmdTransaccionFactory.CreateParameter();
                        pcalculo.ParameterName = "PINTERES";
                        pcalculo.Value = Liquidacion.intereses;
                        pcalculo.Direction = ParameterDirection.Input;
                        pcalculo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcalculo);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "PRETENCION";
                        ptasa.Value = Liquidacion.retencion;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipotasa = cmdTransaccionFactory.CreateParameter();
                        ptipotasa.ParameterName = "PGMF";
                        ptipotasa.Value = Liquidacion.codigo_gmf;
                        ptipotasa.Direction = ParameterDirection.Input;
                        ptipotasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipotasa);

                        DbParameter ptipohis = cmdTransaccionFactory.CreateParameter();
                        ptipohis.ParameterName = "PVALOR_A_PAGAR";
                        ptipohis.Value = Liquidacion.valor;
                        ptipohis.Direction = ParameterDirection.Input;
                        ptipohis.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipohis);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_LIQUIDACDAT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;

                    }


                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CdatData", "Listardatos", ex);
                        return null;
                    }
                }
            }
        }



        public List<Cdat> ListarCdat(string filtro, DateTime FechaApe, Usuario vUsuario, DateTime FechaVencimi)
        {
            DbDataReader resultado;
            List<Cdat> lstCdat = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select C.CODIGO_CDAT, C.NUMERO_CDAT, C.NUMERO_FISICO, C.FECHA_APERTURA, O.NOMBRE AS NOMOFICINA, "
                                       + "CASE C.MODALIDAD When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTERNA' End As Modalidad, "
                                       + "C.VALOR, C.PLAZO, M.DESCRIPCION AS NOMMONEDA, C.FECHA_INICIO, C.FECHA_VENCIMIENTO, "
                                       + "CASE C.COBRA_RETENCION WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END AS RETENCION, "
                                       + "P.DESCRIPCION AS NOMPERIODICIDAD, L.DESCRIPCION AS NOMLINEA, C.ESTADO "
                                       + " FROM CDAT C INNER JOIN OFICINA O ON C.COD_OFICINA = O.COD_OFICINA "
                                       + " LEFT JOIN CDAT_TITULAR T ON C.CODIGO_CDAT = T.CODIGO_CDAT AND T.PRINCIPAL = '1' "
                                       + " LEFT JOIN TIPOMONEDA M ON C.COD_MONEDA = M.COD_MONEDA "
                                       + " LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = C.COD_PERIODICIDAD_INT "
                                       + " LEFT JOIN PERSONA p on p.cod_persona=t.cod_persona "
                                       + " LEFT JOIN LINEACDAT L ON L.COD_LINEACDAT = C.COD_LINEACDAT where 1 = 1 " + filtro;
                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura = To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (FechaVencimi != null && FechaVencimi != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Vencimiento = To_Date('" + Convert.ToDateTime(FechaVencimi).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Vencimiento = '" + Convert.ToDateTime(FechaVencimi).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " ORDER BY C.CODIGO_CDAT ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToString(resultado["RETENCION"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CdatData", "ListarCdat", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cdat> ListarCdats(string filtro, DateTime FechaApe, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstCdat = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select C.CODIGO_CDAT, C.NUMERO_CDAT, C.NUMERO_FISICO, C.FECHA_APERTURA, O.NOMBRE AS NOMOFICINA, "
                                       + "CASE C.MODALIDAD When 'IND' Then 'INDIVIDUAL' When 'CON' Then 'CONJUNTO' When 'ALT' Then 'ALTERNA' End As Modalidad, "
                                       + "C.VALOR, C.PLAZO, M.DESCRIPCION AS NOMMONEDA, C.FECHA_INICIO, C.FECHA_VENCIMIENTO, "
                                       + "CASE C.COBRA_RETENCION WHEN 0 THEN 'NO' WHEN 1 THEN 'SI' END AS RETENCION, "
                                       + "P.DESCRIPCION AS NOMPERIODICIDAD, L.DESCRIPCION AS NOMLINEA, C.COD_LINEACDAT, "
                                       + " CASE C.ESTADO WHEN 1 THEN 'APERTURA' WHEN 2 THEN 'ACTIVA' WHEN 3 THEN 'TERMINADO' WHEN 4 THEN 'ANULADO' WHEN 5 THEN 'EMBARGADO' END AS ESTADO,C.TASA_INTERES AS TASA_INTERES ,SALDO_ACUMULADO(5,C.NUMERO_CDAT) AS VALOR_ACUMULADO, "
                                       + " (select sum(valor) from tran_cdat  t inner join operacion o on o.cod_ope=t.cod_ope  where C.CODIGO_CDAT=t.CODIGO_CDAT and tipo_tran=301 and o.estado=1 and o.tipo_ope!=7 ) as VALOR_PARCIAL "
                                       + " FROM CDAT C INNER JOIN OFICINA O ON C.COD_OFICINA = O.COD_OFICINA "
                                       + " LEFT JOIN CDAT_TITULAR T ON C.CODIGO_CDAT = T.CODIGO_CDAT AND T.PRINCIPAL = '1' "
                                       + " LEFT JOIN TIPOMONEDA M ON C.COD_MONEDA = M.COD_MONEDA "
                                       + " LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = C.COD_PERIODICIDAD_INT "
                                       + " LEFT JOIN PERSONA p on p.cod_persona=t.cod_persona "
                                       + " LEFT JOIN LINEACDAT L ON L.COD_LINEACDAT = C.COD_LINEACDAT where 1 = 1 " + filtro;
                        if (FechaApe != null && FechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.Fecha_Apertura = To_Date('" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.Fecha_Apertura = '" + Convert.ToDateTime(FechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }


                        sql += " ORDER BY C.FECHA_APERTURA DESC, C.CODIGO_CDAT DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["NOMMONEDA"] != DBNull.Value) entidad.nommoneda = Convert.ToString(resultado["NOMMONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToString(resultado["RETENCION"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (entidad.nomperiodicidad == null || entidad.nomperiodicidad == "")
                            {
                                entidad.nomperiodicidad = "Al Vencimiento";
                            }
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (entidad.valor_acumulado > 0)
                            {
                                entidad.valor_total_acumu = entidad.valor + entidad.valor_acumulado;
                            }

                            if (resultado["VALOR_PARCIAL"] != DBNull.Value) entidad.valor_parcial = Convert.ToDecimal(resultado["VALOR_PARCIAL"]);



                            lstCdat.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CdatData", "ListarCdats", ex);
                        return null;
                    }
                }
            }
        }

        //Detalle

        public Detalle_CDAT CrearTitularCdat(Detalle_CDAT pCdat, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_usuario_cdat = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_cdat.ParameterName = "p_cod_usuario_cdat";
                        pcod_usuario_cdat.Value = pCdat.cod_usuario_cdat;
                        if (opcion == 1)//Crear
                            pcod_usuario_cdat.Direction = ParameterDirection.Output;
                        else //Modificar
                            pcod_usuario_cdat.Direction = ParameterDirection.Input;

                        pcod_usuario_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_cdat);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCdat.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pprincipal = cmdTransaccionFactory.CreateParameter();
                        pprincipal.ParameterName = "p_principal";
                        if (pCdat.principal != 0) pprincipal.Value = pCdat.principal; else pprincipal.Value = DBNull.Value;
                        pprincipal.Direction = ParameterDirection.Input;
                        pprincipal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pprincipal);

                        DbParameter pconjuncion = cmdTransaccionFactory.CreateParameter();
                        pconjuncion.ParameterName = "p_conjuncion";
                        if (pCdat.conjuncion != null) pconjuncion.Value = pCdat.conjuncion; else pconjuncion.Value = DBNull.Value;
                        pconjuncion.Direction = ParameterDirection.Input;
                        pconjuncion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconjuncion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        if (opcion == 1)//Crear
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_TITULARDET_CREAR";
                        else //Modificar
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_TITULARDET_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (opcion == 1)//Crear
                            pCdat.cod_usuario_cdat = Convert.ToInt64(pcod_usuario_cdat.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearTitularCdat", ex);
                        return null;
                    }
                }
            }
        }

        public Cdat CrearTranCdat(Cdat pCdat, Int64 pcod_ope, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnum_tran = cmdTransaccionFactory.CreateParameter();
                        pnum_tran.ParameterName = "pn_num_tran";
                        pnum_tran.Value = 0;
                        pnum_tran.Direction = ParameterDirection.Output;
                        pnum_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "pn_num_producto";
                        pnum_producto.Value = pCdat.codigo_cdat;
                        pnum_producto.Direction = ParameterDirection.Input;
                        pnum_producto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pn_cod_cliente";
                        pcod_cliente.Value = pCdat.lstDetalle[0].cod_persona;
                        pcod_cliente.Direction = ParameterDirection.Input;
                        pcod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);

                        DbParameter pcod_Ope = cmdTransaccionFactory.CreateParameter();
                        pcod_Ope.ParameterName = "Pn_Cod_Ope";
                        pcod_Ope.Value = pcod_ope;
                        pcod_Ope.Direction = ParameterDirection.Input;
                        pcod_Ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_Ope);

                        DbParameter Pfecha_pago = cmdTransaccionFactory.CreateParameter();
                        Pfecha_pago.ParameterName = "pf_fecha_pago";
                        Pfecha_pago.Value = DateTime.Now;
                        Pfecha_pago.Direction = ParameterDirection.Input;
                        Pfecha_pago.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(Pfecha_pago);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "pn_valor_pago";
                        pvalor_pago.Value = pCdat.valor;
                        pvalor_pago.Direction = ParameterDirection.Input;
                        pvalor_pago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "pn_tipo_tran";
                        ptipo_tran.Value = 301;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_REALIZARPAGO";

                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearTranCdat", ex);
                        return null;
                    }
                }
            }
        }

        public AdministracionCDAT cierrecdat(AdministracionCDAT traslado_cuenta, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_aud = cmdTransaccionFactory.CreateParameter();
                        pfecha_aud.ParameterName = "P_FECHA_CIERRE";
                        pfecha_aud.Value = traslado_cuenta.fecha_vencimiento;
                        pfecha_aud.Direction = ParameterDirection.Input;
                        pfecha_aud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aud);

                        DbParameter pcod_auditoria_cdat = cmdTransaccionFactory.CreateParameter();
                        pcod_auditoria_cdat.ParameterName = "PNUMERO_CDAT";
                        pcod_auditoria_cdat.Value = traslado_cuenta.numero_cdat;
                        pcod_auditoria_cdat.Direction = ParameterDirection.Input;
                        pcod_auditoria_cdat.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_auditoria_cdat);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "PCOD_OPE";
                        pcodigo_cdat.Value = poperacion.cod_ope;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter ptipo_registro_aud = cmdTransaccionFactory.CreateParameter();
                        ptipo_registro_aud.ParameterName = "PVALOR";
                        ptipo_registro_aud.Value = traslado_cuenta.valor;
                        ptipo_registro_aud.Direction = ParameterDirection.Input;
                        ptipo_registro_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_registro_aud);



                        DbParameter pcod_usuario_aud = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_aud.ParameterName = "PINTERES_CAPITALZADO";
                        pcod_usuario_aud.Value = traslado_cuenta.intereses_cap;
                        pcod_usuario_aud.Direction = ParameterDirection.Input;
                        pcod_usuario_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_aud);

                        DbParameter pip_aud = cmdTransaccionFactory.CreateParameter();
                        pip_aud.ParameterName = "PINTERES";
                        pip_aud.Value = traslado_cuenta.intereses;
                        pip_aud.Direction = ParameterDirection.Input;
                        pip_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pip_aud);


                        DbParameter ret = cmdTransaccionFactory.CreateParameter();
                        ret.ParameterName = "PRETENCION";
                        ret.Value = traslado_cuenta.retencion;
                        ret.Direction = ParameterDirection.Input;
                        ret.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ret);

                        DbParameter gmf = cmdTransaccionFactory.CreateParameter();
                        gmf.ParameterName = "PGMF";
                        gmf.Value = traslado_cuenta.codigo_gmf;
                        gmf.Direction = ParameterDirection.Input;
                        gmf.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(gmf);

                        DbParameter valor_pagar = cmdTransaccionFactory.CreateParameter();
                        valor_pagar.ParameterName = "PVALOR_A_PAGAR";
                        valor_pagar.Value = traslado_cuenta.valor;
                        valor_pagar.Direction = ParameterDirection.Input;
                        valor_pagar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(valor_pagar);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CIERRECDAT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "cierrecdat", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarTitularCdat(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_usuario_cdat = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_cdat.ParameterName = "p_cod_usuario_cdat";
                        pcod_usuario_cdat.Value = pId;
                        pcod_usuario_cdat.Direction = ParameterDirection.Input;
                        pcod_usuario_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_cdat);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_TITULARDET_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "EliminarTitularCdat", ex);
                    }
                }
            }
        }


        public List<Cdat> ListarUsuariosAsesores(Cdat pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstPerso = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Usuarios " + ObtenerFiltro(pPerso) + " ORDER BY CODUSUARIO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstPerso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarUsuariosAsesores", ex);
                        return null;
                    }
                }
            }
        }


        public List<Cdat> ListarOficinas(Cdat pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstPerso = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Oficina " + ObtenerFiltro(pPerso) + " ORDER BY COD_OFICINA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstPerso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarOficinas", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cdat> ListarFechaVencimiento(Cdat pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstPerso = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select * from cdat c join cdat_titular t on c.codigo_cdat=t.codigo_cdat join persona p on p.cod_persona=t.cod_persona where p.cod_persona= " + pPerso.cod_persona + " and estado in (1,2)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["Fecha_vencimiento"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["Fecha_vencimiento"]);
                            if (resultado["Numero_cdat"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["Numero_cdat"]);
                            lstPerso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarOficinas", ex);
                        return null;
                    }
                }
            }
        }
        public Cdat DiasAvisoCDAT(Cdat pPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat Entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from notificacion where tipo='C'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {

                            if (resultado["DiasAviso"] != DBNull.Value) Entidad.DiasAviso = Convert.ToInt32(resultado["DiasAviso"]);
                            if (resultado["texto"] != DBNull.Value) Entidad.mensajenotifi = Convert.ToString(resultado["texto"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "DiasAvisoCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public Detalle_CDAT ConsultarPersona(Detalle_CDAT pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            Detalle_CDAT entidad = new Detalle_CDAT();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Cod_Persona,Identificacion, Primer_Nombre||' '||Segundo_Nombre As Nombres, "
                                        + "Primer_Apellido||' '||Segundo_Apellido As Apellidos,Codciudadresidencia, C.Nomciudad, "
                                        + "P.Direccion,P.Telefono "
                                        + "From Persona P Inner Join Ciudades C On C.Codciudad = P.Codciudadresidencia "
                                        + "where Identificacion = '" + pPerso.identificacion + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }
        }



        public Cdat ConsultarApertura(Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cdat";
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
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ApertuData", "ConsultarApertu", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.FabricaCreditos.Entities.Credito ConsultarAperturas(Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.* from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ApertuData", "ConsultarApertu", ex);
                        return null;
                    }
                }
            }
        }


        //CONSULTAR DATOS PRINCIPALES

        public Cdat ConsultarApertu(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cdat WHERE CODIGO_CDAT = " + pId.ToString();
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
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ApertuData", "ConsultarApertu", ex);
                        return null;
                    }
                }
            }
        }


        public Cdat ConsultarApertuXnumcdat(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Cdat WHERE NUMERO_CDAT = " + pId.ToString();
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
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ApertuData", "ConsultarApertuXnumcdat", ex);
                        return null;
                    }
                }
            }
        }




        //CONSULTAR DETALLE TITULAR
        public List<Detalle_CDAT> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Detalle_CDAT> lstTitu = new List<Detalle_CDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.Cod_Usuario_Cdat,C.Codigo_Cdat,C.Cod_Persona,P.Identificacion, "
                                        + "P.Primer_Nombre ||' '|| P.Segundo_Nombre As Nombres, P.Primer_Apellido||' '|| P.Segundo_Apellido As Apellidos, "
                                        + "P.Codciudadresidencia, I.Nomciudad,P.Direccion,P.Telefono,C.Principal,C.Conjuncion "
                                        + "From Cdat_Titular C Inner Join Persona P On C.Cod_Persona = P.Cod_Persona "
                                        + "left join Ciudades i on I.Codciudad = P.Codciudadresidencia  where C.CODIGO_CDAT = " + pCod.ToString() + " ORDER BY C.COD_USUARIO_CDAT ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Detalle_CDAT entidad = new Detalle_CDAT();
                            if (resultado["COD_USUARIO_CDAT"] != DBNull.Value) entidad.cod_usuario_cdat = Convert.ToInt64(resultado["COD_USUARIO_CDAT"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["CONJUNCION"] != DBNull.Value) entidad.conjuncion = Convert.ToString(resultado["CONJUNCION"]);
                            //AGREGADO
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["CONJUNCION"] != DBNull.Value) entidad.conjuncion = Convert.ToString(resultado["CONJUNCION"]);
                            lstTitu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTitu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarDetalleTitulares", ex);
                        return null;
                    }
                }
            }
        }



        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarDetalles(Usuario vUsuario, string filtro)
        {

            DbDataReader resultado;
            List<Xpinn.FabricaCreditos.Entities.Credito> lstTitu = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select v_creditos.* from v_creditos " + filtro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt32(resultado["COD_DEUDOR"]);
                            if (resultado["DIAS_AJUSTE"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["DIAS_AJUSTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            lstTitu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTitu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarDetalle", ex);
                        return null;
                    }
                }
            }
        }
        public List<Detalle_CDAT> ListarDetalle(Usuario vUsuario)
        {

            DbDataReader resultado;
            List<Detalle_CDAT> lstTitu = new List<Detalle_CDAT>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select C.Cod_Usuario_Cdat,C.Codigo_Cdat,C.Cod_Persona,P.Identificacion, "
                                        + "P.Primer_Nombre ||' '|| P.Segundo_Nombre As Nombres, P.Primer_Apellido||' '|| P.Segundo_Apellido As Apellidos, "
                                        + "P.Codciudadresidencia, I.Nomciudad,P.Direccion,P.Telefono,C.Principal,C.Conjuncion "
                                        + "From Cdat_Titular C Inner Join Persona P On C.Cod_Persona = P.Cod_Persona "
                                        + "inner join Ciudades i on I.Codciudad = P.Codciudadresidencia  where C.CODIGO_CDAT = 21 " + " ORDER BY C.COD_USUARIO_CDAT ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Detalle_CDAT entidad = new Detalle_CDAT();
                            if (resultado["COD_USUARIO_CDAT"] != DBNull.Value) entidad.cod_usuario_cdat = Convert.ToInt64(resultado["COD_USUARIO_CDAT"]);
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt64(resultado["CODIGO_CDAT"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["CONJUNCION"] != DBNull.Value) entidad.conjuncion = Convert.ToString(resultado["CONJUNCION"]);
                            //AGREGADO
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["PRINCIPAL"] != DBNull.Value) entidad.principal = Convert.ToInt32(resultado["PRINCIPAL"]);
                            if (resultado["CONJUNCION"] != DBNull.Value) entidad.conjuncion = Convert.ToString(resultado["CONJUNCION"]);
                            lstTitu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTitu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarDetalle", ex);
                        return null;
                    }
                }
            }
        }







        public Cdat ConsultarDiasPeriodicidad(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Periodicidad WHERE Cod_Periodicidad = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) entidad.numdias = Convert.ToInt32(resultado["NUMERO_DIAS"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ConsultarDiasPeriodicidad", ex);
                        return null;
                    }
                }
            }
        }



        public Cdat ConsultarNumeracionCDATS(Cdat pCadt, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from General where Codigo = 580";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ConsultarNumeracionCDATS", ex);
                        return null;
                    }
                }
            }
        }



        //GRABAR AUDITORIA
        public CDAT_AUDITORIA CrearAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_auditoria_cdat = cmdTransaccionFactory.CreateParameter();
                        pcod_auditoria_cdat.ParameterName = "p_cod_auditoria_cdat";
                        pcod_auditoria_cdat.Value = pCdat.cod_auditoria_cdat;
                        pcod_auditoria_cdat.Direction = ParameterDirection.Output;
                        pcod_auditoria_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_auditoria_cdat);

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter ptipo_registro_aud = cmdTransaccionFactory.CreateParameter();
                        ptipo_registro_aud.ParameterName = "p_tipo_registro_aud";
                        ptipo_registro_aud.Value = pCdat.tipo_registro_aud;
                        ptipo_registro_aud.Direction = ParameterDirection.Input;
                        ptipo_registro_aud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_registro_aud);

                        DbParameter pfecha_aud = cmdTransaccionFactory.CreateParameter();
                        pfecha_aud.ParameterName = "p_fecha_aud";
                        pfecha_aud.Value = pCdat.fecha_aud;
                        pfecha_aud.Direction = ParameterDirection.Input;
                        pfecha_aud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aud);

                        DbParameter pcod_usuario_aud = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_aud.ParameterName = "p_cod_usuario_aud";
                        pcod_usuario_aud.Value = pCdat.cod_usuario_aud;
                        pcod_usuario_aud.Direction = ParameterDirection.Input;
                        pcod_usuario_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_aud);

                        DbParameter pip_aud = cmdTransaccionFactory.CreateParameter();
                        pip_aud.ParameterName = "p_ip_aud";
                        pip_aud.Value = pCdat.ip_aud;
                        pip_aud.Direction = ParameterDirection.Input;
                        pip_aud.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip_aud);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_AUDITORIA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCdat.cod_auditoria_cdat = Convert.ToInt64(pcod_auditoria_cdat.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearAuditoriaCdat", ex);
                        return null;
                    }
                }
            }
        }


        public CDAT_AUDITORIA ModificarAuditoriaCdat(CDAT_AUDITORIA pCdat, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "p_codigo_cdat";
                        pcodigo_cdat.Value = pCdat.codigo_cdat;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter ptipo_registro_aud = cmdTransaccionFactory.CreateParameter();
                        ptipo_registro_aud.ParameterName = "p_tipo_registro_aud";
                        ptipo_registro_aud.Value = pCdat.tipo_registro_aud;
                        ptipo_registro_aud.Direction = ParameterDirection.Input;
                        ptipo_registro_aud.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_registro_aud);

                        DbParameter pfecha_aud = cmdTransaccionFactory.CreateParameter();
                        pfecha_aud.ParameterName = "p_fecha_aud";
                        pfecha_aud.Value = pCdat.fecha_aud;
                        pfecha_aud.Direction = ParameterDirection.Input;
                        pfecha_aud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aud);

                        DbParameter pcod_usuario_aud = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_aud.ParameterName = "p_cod_usuario_aud";
                        pcod_usuario_aud.Value = pCdat.cod_usuario_aud;
                        pcod_usuario_aud.Direction = ParameterDirection.Input;
                        pcod_usuario_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_aud);

                        DbParameter pip_aud = cmdTransaccionFactory.CreateParameter();
                        pip_aud.ParameterName = "p_ip_aud";
                        pip_aud.Value = pCdat.ip_aud;
                        pip_aud.Direction = ParameterDirection.Input;
                        pip_aud.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pip_aud);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_AUDITORIA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pCdat;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ModificarAuditoriaCdat", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cdat> ListarTipoLineaCDAT(Cdat pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstTipo = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Lineacdat " + ObtenerFiltro(pPerso) + " ORDER BY COD_LINEACDAT ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarTipoLineaCDAT", ex);
                        return null;
                    }
                }
            }
        }




        public DateTime FecSumDia(DateTime fecha, int dias, int tipo_cal)
        {
            int dato = 0;
            if (fecha == null)
                return fecha;
            if (tipo_cal == 1)
            {
                int año_fec = fecha.Year;
                int mes_fec = fecha.Month;
                int dia_fec = fecha.Day;
                int dias_febrero = 28;
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                if (dia_fec > 30 || (mes_fec == 2 && dia_fec >= dias_febrero))
                    dia_fec = 30;
                dato = (dias / 360);
                if (dato > 1)
                {
                    año_fec = año_fec + dato;
                    dias = dias - (360 * dato);
                }
                dato = (dias / 30);
                if (dato > 1)
                {
                    mes_fec = mes_fec + dato;
                    if (mes_fec > 12)
                    {
                        año_fec = año_fec + 1;
                        mes_fec = mes_fec - 12;
                    }
                    dias = dias - (30 * dato);
                }
                if (dias > 0)
                {
                    if (30 - dia_fec < dias)
                    {
                        mes_fec = mes_fec + 1;
                        if (mes_fec > 12)
                        {
                            año_fec = año_fec + 1;
                            mes_fec = 1;
                        }
                        dia_fec = dias - (30 - dia_fec);
                    }
                    else
                    {
                        dia_fec = dia_fec + dias;
                    }
                }
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                if (mes_fec == 2 && (dia_fec > 30 && dia_fec <= 59))
                    dia_fec = dia_fec - (30 - dias_febrero);
                if (mes_fec == 4 || mes_fec == 6 || mes_fec == 9 || mes_fec == 11)
                    if (dia_fec == 31)
                        dia_fec = 30;
                fecha = new DateTime(año_fec, mes_fec, dia_fec, 0, 0, 0);
            }
            else
            {
                fecha = fecha.AddDays(dias);
            }
            return fecha;
        }

        public Xpinn.Comun.Entities.Cierea FechaUltimoCierre(Xpinn.Comun.Entities.Cierea pCierea, String filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from cierea" + ObtenerFiltro(pCierea) + " Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                            if (resultado["FECREA"] != DBNull.Value) entidad.fecrea = Convert.ToDateTime(resultado["FECREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["CODUSUARIO"]);

                            dbConnectionFactory.CerrarConexion(connection);

                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoCDAT", "FechaUltimoCierre", ex);
                        return null;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select valor From general Where codigo = 1911 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoCDAT", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreHistoricoCDAT", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }


        public Cdat ciCierreHistorico(Cdat pentidad,string valor, DateTime fechas, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            serror = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = fechas;
                        PFECHA.DbType = DbType.Date;

                        DbParameter PESTADO = cmdTransaccionFactory.CreateParameter();
                        PESTADO.ParameterName = "PESTADO";
                        PESTADO.Value = valor;

                        DbParameter PUSUARIO = cmdTransaccionFactory.CreateParameter();
                        PUSUARIO.ParameterName = "PUSUARIO";
                        PUSUARIO.Value = cod_usuario;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PESTADO);
                        cmdTransaccionFactory.Parameters.Add(PUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CIERREHISTORICO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        return pentidad; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearCierremensual", ex);
                        serror = ex.Message;
                        return null;
                    }
                }
            }
        }


        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCredito = new List<Xpinn.FabricaCreditos.Entities.Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.* from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);


                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReliquidacionData", "ListarCredito", ex);
                        return null;
                    }
                }

            }
        }

        public Xpinn.FabricaCreditos.Entities.Credito ListarCreditos(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v_creditos.* from v_creditos " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.FabricaCreditos.Entities.Credito entidad = new Xpinn.FabricaCreditos.Entities.Credito();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea_credito = Convert.ToString(resultado["LINEA"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);


                            return null;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReliquidacionData", "ListarCredito", ex);
                        return null;
                    }
                }

            }
        }



        public Xpinn.FabricaCreditos.Entities.Credito CrearAperturaCDAT(Xpinn.FabricaCreditos.Entities.Credito pAperturaCDAT, Usuario vUsuario, Xpinn.Tesoreria.Entities.Operacion poperacion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {




                        DbParameter pcodigo_cdat = cmdTransaccionFactory.CreateParameter();
                        pcodigo_cdat.ParameterName = "P_CONSECUTIVO";
                        pcodigo_cdat.Value = pAperturaCDAT.Codeudor;
                        pcodigo_cdat.Direction = ParameterDirection.Input;
                        pcodigo_cdat.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_cdat);

                        DbParameter ptipo_registro_aud = cmdTransaccionFactory.CreateParameter();
                        ptipo_registro_aud.ParameterName = "P_CODIGO_CDAT";
                        ptipo_registro_aud.Value = pAperturaCDAT.cod_deudor;
                        ptipo_registro_aud.Direction = ParameterDirection.Input;
                        ptipo_registro_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_registro_aud);



                        DbParameter pcod_usuario_aud = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_aud.ParameterName = "P_NUMERO_RADICACION";
                        pcod_usuario_aud.Value = pAperturaCDAT.numero_radicacion;
                        pcod_usuario_aud.Direction = ParameterDirection.Input;
                        pcod_usuario_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_aud);

                        DbParameter pfecha_aud = cmdTransaccionFactory.CreateParameter();
                        pfecha_aud.ParameterName = "P_FECHA_GARANTIA";
                        pfecha_aud.Value = pAperturaCDAT.fecha_aprobacion;
                        pfecha_aud.Direction = ParameterDirection.Input;
                        pfecha_aud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aud);

                        DbParameter pip_aud = cmdTransaccionFactory.CreateParameter();
                        pip_aud.ParameterName = "P_COD_OPE";
                        pip_aud.Value = poperacion.tipo_ope;
                        pip_aud.Direction = ParameterDirection.Input;
                        pip_aud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pip_aud);



                        DbParameter valor_garantia = cmdTransaccionFactory.CreateParameter();
                        valor_garantia.ParameterName = "P_VALOR_GARANTIA";
                        valor_garantia.Value = pAperturaCDAT.valor_cuota;
                        valor_garantia.Direction = ParameterDirection.Input;
                        valor_garantia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(valor_garantia);


                        DbParameter estado = cmdTransaccionFactory.CreateParameter();
                        estado.ParameterName = "P_ESTADO";
                        estado.Value = pAperturaCDAT.estados;
                        estado.Direction = ParameterDirection.Input;
                        estado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(estado);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_GARANTIA_C_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAperturaCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "CrearAperturaCDAT", ex);
                        return null;
                    }
                }
            }
        }


        public Int32 ObtenerConsecutivo(string pConsulta, Usuario vUsuario)
        {
            Int32 result = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = pConsulta;
                        result = Convert.ToInt32(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        return 1;
                    }
                }
            }
        }

        public Cdat ConsultarAfiliacion(String pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select a.estado,a.cod_persona From persona_afiliacion a Where a.cod_persona= " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["estado"] != DBNull.Value) entidad.estado_persona = Convert.ToString(resultado["estado"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);


                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ConsultarAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public Xpinn.Comun.Entities.Cierea Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter fecha = cmdTransaccionFactory.CreateParameter();
                        fecha.ParameterName = "p_fecha";
                        fecha.Value = pcierea.fecha;
                        fecha.Direction = ParameterDirection.Input;
                        fecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pcierea.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pcierea.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcampo1 = cmdTransaccionFactory.CreateParameter();
                        pcampo1.ParameterName = "p_campo1";
                        pcampo1.Value = pcierea.campo1;
                        pcampo1.Direction = ParameterDirection.Input;
                        pcampo1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo1);

                        DbParameter pcampo2 = cmdTransaccionFactory.CreateParameter();
                        pcampo2.ParameterName = "p_campo2";
                        if (pcierea.campo2 == null)
                            pcampo2.Value = DBNull.Value;
                        else
                            pcampo2.Value = pcierea.campo2;
                        pcampo2.Direction = ParameterDirection.Input;
                        pcampo2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcampo2);

                        DbParameter pfecrea = cmdTransaccionFactory.CreateParameter();
                        pfecrea.ParameterName = "p_fecrea";
                        if (pcierea.fecrea == DateTime.MinValue)
                            pfecrea.Value = DBNull.Value;
                        else
                            pfecrea.Value = pcierea.fecrea;
                        pfecrea.Direction = ParameterDirection.Input;
                        pfecrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pcierea.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pcierea.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_CIEREA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pcierea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ciereaData", "Crearcierea", ex);
                        return null;
                    }
                }
            }
        }


        public List<Cdat> ListartodosUsuarios(Cdat pUsuarioAse, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cdat> lstUsuario = new List<Cdat>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT u.codusuario, u.nombre, u.cod_oficina FROM USUARIOS u
                                        INNER JOIN asejecutivos ase on u.identificacion = ase.sidentificacion
                                        WHERE u.estado = 1 and cod_oficina = " + pUsuario.cod_oficina + " order by u.nombre";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();

                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstUsuario.Add(entidad);
                        }

                        return lstUsuario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCdatData", "ListarUsuario", ex);
                        return null;
                    }
                }
            }
        }

    }
}
