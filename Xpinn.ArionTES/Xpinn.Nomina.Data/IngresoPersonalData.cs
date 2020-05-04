using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class IngresoPersonalData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public IngresoPersonalData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public IngresoPersonal CrearIngresoPersonal(IngresoPersonal pIngresoPersonal, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pIngresoPersonal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pIngresoPersonal.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pIngresoPersonal.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        if (pIngresoPersonal.codigonomina == null)
                            pcodigonomina.Value = DBNull.Value;
                        else
                            pcodigonomina.Value = pIngresoPersonal.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotipocontrato.ParameterName = "p_codigotipocontrato";
                        if (pIngresoPersonal.codigotipocontrato == null)
                            pcodigotipocontrato.Value = DBNull.Value;
                        else
                            pcodigotipocontrato.Value = pIngresoPersonal.codigotipocontrato;
                        pcodigotipocontrato.Direction = ParameterDirection.Input;
                        pcodigotipocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipocontrato);

                        DbParameter pcodigocargo = cmdTransaccionFactory.CreateParameter();
                        pcodigocargo.ParameterName = "p_codigocargo";
                        if (pIngresoPersonal.codigocargo == null)
                            pcodigocargo.Value = DBNull.Value;
                        else
                            pcodigocargo.Value = pIngresoPersonal.codigocargo;
                        pcodigocargo.Direction = ParameterDirection.Input;
                        pcodigocargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocargo);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        if (pIngresoPersonal.codigocentrocosto == null)
                            pcodigocentrocosto.Value = DBNull.Value;
                        else
                            pcodigocentrocosto.Value = pIngresoPersonal.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pfechaingreso = cmdTransaccionFactory.CreateParameter();
                        pfechaingreso.ParameterName = "p_fechaingreso";
                        if (pIngresoPersonal.fechaingreso == null)
                            pfechaingreso.Value = DBNull.Value;
                        else
                            pfechaingreso.Value = pIngresoPersonal.fechaingreso;
                        pfechaingreso.Direction = ParameterDirection.Input;
                        pfechaingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaingreso);

                        DbParameter pfechainicioperiodoprueba = cmdTransaccionFactory.CreateParameter();
                        pfechainicioperiodoprueba.ParameterName = "p_fechainicioperiodoprueba";
                        if (pIngresoPersonal.fechainicioperiodoprueba == null)
                            pfechainicioperiodoprueba.Value = DBNull.Value;
                        else
                            pfechainicioperiodoprueba.Value = pIngresoPersonal.fechainicioperiodoprueba;
                        pfechainicioperiodoprueba.Direction = ParameterDirection.Input;
                        pfechainicioperiodoprueba.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicioperiodoprueba);

                        DbParameter p_fechaterminaprueba = cmdTransaccionFactory.CreateParameter();
                        p_fechaterminaprueba.ParameterName = "p_fechaterminaprueba";
                        if (pIngresoPersonal.fechaterminacionperiodoprueba == null)
                            p_fechaterminaprueba.Value = DBNull.Value;
                        else
                            p_fechaterminaprueba.Value = pIngresoPersonal.fechaterminacionperiodoprueba;
                        p_fechaterminaprueba.Direction = ParameterDirection.Input;
                        p_fechaterminaprueba.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fechaterminaprueba);

                        DbParameter ptieneley50 = cmdTransaccionFactory.CreateParameter();
                        ptieneley50.ParameterName = "p_tieneley50";
                        if (pIngresoPersonal.tieneley50 == null)
                            ptieneley50.Value = DBNull.Value;
                        else
                            ptieneley50.Value = pIngresoPersonal.tieneley50;
                        ptieneley50.Direction = ParameterDirection.Input;
                        ptieneley50.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptieneley50);

                        DbParameter pesextranjero = cmdTransaccionFactory.CreateParameter();
                        pesextranjero.ParameterName = "p_esextranjero";
                        if (pIngresoPersonal.esextranjero == null)
                            pesextranjero.Value = DBNull.Value;
                        else
                            pesextranjero.Value = pIngresoPersonal.esextranjero;
                        pesextranjero.Direction = ParameterDirection.Input;
                        pesextranjero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesextranjero);

                        DbParameter psalario = cmdTransaccionFactory.CreateParameter();
                        psalario.ParameterName = "p_salario";
                        if (pIngresoPersonal.salario == null)
                            psalario.Value = DBNull.Value;
                        else
                            psalario.Value = pIngresoPersonal.salario;
                        psalario.Direction = ParameterDirection.Input;
                        psalario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psalario);

                        DbParameter pessueldovariable = cmdTransaccionFactory.CreateParameter();
                        pessueldovariable.ParameterName = "p_essueldovariable";
                        if (pIngresoPersonal.essueldovariable == null)
                            pessueldovariable.Value = DBNull.Value;
                        else
                            pessueldovariable.Value = pIngresoPersonal.essueldovariable;
                        pessueldovariable.Direction = ParameterDirection.Input;
                        pessueldovariable.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pessueldovariable);

                        DbParameter pauxiliotransporte = cmdTransaccionFactory.CreateParameter();
                        pauxiliotransporte.ParameterName = "p_auxiliotransporte";
                        if (pIngresoPersonal.auxiliotransporte == null)
                            pauxiliotransporte.Value = DBNull.Value;
                        else
                            pauxiliotransporte.Value = pIngresoPersonal.auxiliotransporte;
                        pauxiliotransporte.Direction = ParameterDirection.Input;
                        pauxiliotransporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pauxiliotransporte);

                        DbParameter pformapago = cmdTransaccionFactory.CreateParameter();
                        pformapago.ParameterName = "p_formapago";
                        if (pIngresoPersonal.formapago == null)
                            pformapago.Value = DBNull.Value;
                        else
                            pformapago.Value = pIngresoPersonal.formapago;
                        pformapago.Direction = ParameterDirection.Input;
                        pformapago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pformapago);

                        DbParameter ptipocuenta = cmdTransaccionFactory.CreateParameter();
                        ptipocuenta.ParameterName = "p_tipocuenta";
                        if (pIngresoPersonal.tipocuenta == null)
                            ptipocuenta.Value = DBNull.Value;
                        else
                            ptipocuenta.Value = pIngresoPersonal.tipocuenta;
                        ptipocuenta.Direction = ParameterDirection.Input;
                        ptipocuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipocuenta);

                        DbParameter pcodigobanco = cmdTransaccionFactory.CreateParameter();
                        pcodigobanco.ParameterName = "p_codigobanco";
                        if (pIngresoPersonal.codigobanco == null)
                            pcodigobanco.Value = DBNull.Value;
                        else
                            pcodigobanco.Value = pIngresoPersonal.codigobanco;
                        pcodigobanco.Direction = ParameterDirection.Input;
                        pcodigobanco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigobanco);

                        DbParameter pnumerocuentabancaria = cmdTransaccionFactory.CreateParameter();
                        pnumerocuentabancaria.ParameterName = "p_numerocuentabancaria";
                        if (pIngresoPersonal.numerocuentabancaria == null)
                            pnumerocuentabancaria.Value = DBNull.Value;
                        else
                            pnumerocuentabancaria.Value = pIngresoPersonal.numerocuentabancaria;
                        pnumerocuentabancaria.Direction = ParameterDirection.Input;
                        pnumerocuentabancaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumerocuentabancaria);

                        DbParameter pcodigofondosalud = cmdTransaccionFactory.CreateParameter();
                        pcodigofondosalud.ParameterName = "p_codigofondosalud";
                        if (pIngresoPersonal.codigofondosalud == null)
                            pcodigofondosalud.Value = DBNull.Value;
                        else
                            pcodigofondosalud.Value = pIngresoPersonal.codigofondosalud;
                        pcodigofondosalud.Direction = ParameterDirection.Input;
                        pcodigofondosalud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondosalud);

                        DbParameter pcodigofondopension = cmdTransaccionFactory.CreateParameter();
                        pcodigofondopension.ParameterName = "p_codigofondopension";
                        if (pIngresoPersonal.codigofondopension == null)
                            pcodigofondopension.Value = DBNull.Value;
                        else
                            pcodigofondopension.Value = pIngresoPersonal.codigofondopension;
                        pcodigofondopension.Direction = ParameterDirection.Input;
                        pcodigofondopension.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondopension);

                        DbParameter pcodigofondocesantias = cmdTransaccionFactory.CreateParameter();
                        pcodigofondocesantias.ParameterName = "p_codigofondocesantias";
                        if (pIngresoPersonal.codigofondocesantias == null)
                            pcodigofondocesantias.Value = DBNull.Value;
                        else
                            pcodigofondocesantias.Value = pIngresoPersonal.codigofondocesantias;
                        pcodigofondocesantias.Direction = ParameterDirection.Input;
                        pcodigofondocesantias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondocesantias);

                        DbParameter pcodigocajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pcodigocajacompensacion.ParameterName = "p_codigocajacompensacion";
                        if (pIngresoPersonal.codigocajacompensacion == null)
                            pcodigocajacompensacion.Value = DBNull.Value;
                        else
                            pcodigocajacompensacion.Value = pIngresoPersonal.codigocajacompensacion;
                        pcodigocajacompensacion.Direction = ParameterDirection.Input;
                        pcodigocajacompensacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocajacompensacion);

                        DbParameter pcodigoarl = cmdTransaccionFactory.CreateParameter();
                        pcodigoarl.ParameterName = "p_codigoarl";
                        if (pIngresoPersonal.codigoarl == null)
                            pcodigoarl.Value = DBNull.Value;
                        else
                            pcodigoarl.Value = pIngresoPersonal.codigoarl;
                        pcodigoarl.Direction = ParameterDirection.Input;
                        pcodigoarl.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoarl);

                        DbParameter pcodigopensionvoluntaria = cmdTransaccionFactory.CreateParameter();
                        pcodigopensionvoluntaria.ParameterName = "p_codigopensionvoluntaria";
                        if (pIngresoPersonal.codigopensionvoluntaria == null)
                            pcodigopensionvoluntaria.Value = DBNull.Value;
                        else
                            pcodigopensionvoluntaria.Value = pIngresoPersonal.codigopensionvoluntaria;
                        pcodigopensionvoluntaria.Direction = ParameterDirection.Input;
                        pcodigopensionvoluntaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopensionvoluntaria);

                        DbParameter pfechaafiliacionsalud = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacionsalud.ParameterName = "p_fechaafiliacionsalud";
                        if (pIngresoPersonal.fechaafiliacionsalud == null)
                            pfechaafiliacionsalud.Value = DBNull.Value;
                        else
                            pfechaafiliacionsalud.Value = pIngresoPersonal.fechaafiliacionsalud;
                        pfechaafiliacionsalud.Direction = ParameterDirection.Input;
                        pfechaafiliacionsalud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacionsalud);

                        DbParameter pfechaafiliacionpension = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacionpension.ParameterName = "p_fechaafiliacionpension";
                        if (pIngresoPersonal.fechaafiliacionpension == null)
                            pfechaafiliacionpension.Value = DBNull.Value;
                        else
                            pfechaafiliacionpension.Value = pIngresoPersonal.fechaafiliacionpension;
                        pfechaafiliacionpension.Direction = ParameterDirection.Input;
                        pfechaafiliacionpension.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacionpension);

                        DbParameter pfechaafiliacioncesantias = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacioncesantias.ParameterName = "p_fechaafiliacioncesantias";
                        if (pIngresoPersonal.fechaafiliacioncesantias == null)
                            pfechaafiliacioncesantias.Value = DBNull.Value;
                        else
                            pfechaafiliacioncesantias.Value = pIngresoPersonal.fechaafiliacioncesantias;
                        pfechaafiliacioncesantias.Direction = ParameterDirection.Input;
                        pfechaafiliacioncesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacioncesantias);

                        DbParameter pfechaafiliacajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacajacompensacion.ParameterName = "p_fechaafiliacajacompensacion";
                        if (pIngresoPersonal.fechaafiliacajacompensacion == null)
                            pfechaafiliacajacompensacion.Value = DBNull.Value;
                        else
                            pfechaafiliacajacompensacion.Value = pIngresoPersonal.fechaafiliacajacompensacion;
                        pfechaafiliacajacompensacion.Direction = ParameterDirection.Input;
                        pfechaafiliacajacompensacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacajacompensacion);

                        DbParameter pfecharetirosalud = cmdTransaccionFactory.CreateParameter();
                        pfecharetirosalud.ParameterName = "p_fecharetirosalud";
                        if (pIngresoPersonal.fecharetirosalud == null)
                            pfecharetirosalud.Value = DBNull.Value;
                        else
                            pfecharetirosalud.Value = pIngresoPersonal.fecharetirosalud;
                        pfecharetirosalud.Direction = ParameterDirection.Input;
                        pfecharetirosalud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirosalud);

                        DbParameter pfecharetiropension = cmdTransaccionFactory.CreateParameter();
                        pfecharetiropension.ParameterName = "p_fecharetiropension";
                        if (pIngresoPersonal.fecharetiropension == null)
                            pfecharetiropension.Value = DBNull.Value;
                        else
                            pfecharetiropension.Value = pIngresoPersonal.fecharetiropension;
                        pfecharetiropension.Direction = ParameterDirection.Input;
                        pfecharetiropension.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetiropension);

                        DbParameter pfecharetirocesantias = cmdTransaccionFactory.CreateParameter();
                        pfecharetirocesantias.ParameterName = "p_fecharetirocesantias";
                        if (pIngresoPersonal.fecharetirocesantias == null)
                            pfecharetirocesantias.Value = DBNull.Value;
                        else
                            pfecharetirocesantias.Value = pIngresoPersonal.fecharetirocesantias;
                        pfecharetirocesantias.Direction = ParameterDirection.Input;
                        pfecharetirocesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirocesantias);

                        DbParameter pfecharetirocajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pfecharetirocajacompensacion.ParameterName = "p_fecharetirocajacompensacion";
                        if (pIngresoPersonal.fecharetirocajacompensacion == null)
                            pfecharetirocajacompensacion.Value = DBNull.Value;
                        else
                            pfecharetirocajacompensacion.Value = pIngresoPersonal.fecharetirocajacompensacion;
                        pfecharetirocajacompensacion.Direction = ParameterDirection.Input;
                        pfecharetirocajacompensacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirocajacompensacion);

                        DbParameter ptipocotizante = cmdTransaccionFactory.CreateParameter();
                        ptipocotizante.ParameterName = "p_tipocotizante";
                        if (pIngresoPersonal.tipocotizante == null)
                            ptipocotizante.Value = DBNull.Value;
                        else
                            ptipocotizante.Value = pIngresoPersonal.tipocotizante;
                        ptipocotizante.Direction = ParameterDirection.Input;
                        ptipocotizante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipocotizante);

                        DbParameter pespensionadoporvejez = cmdTransaccionFactory.CreateParameter();
                        pespensionadoporvejez.ParameterName = "p_espensionadoporvejez";
                        if (pIngresoPersonal.espensionadoporvejez == null)
                            pespensionadoporvejez.Value = DBNull.Value;
                        else
                            pespensionadoporvejez.Value = pIngresoPersonal.espensionadoporvejez;
                        pespensionadoporvejez.Direction = ParameterDirection.Input;
                        pespensionadoporvejez.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pespensionadoporvejez);

                        DbParameter pespensionadoporinvalidez = cmdTransaccionFactory.CreateParameter();
                        pespensionadoporinvalidez.ParameterName = "p_espensionadoporinvalidez";
                        if (pIngresoPersonal.espensionadoporinvalidez == null)
                            pespensionadoporinvalidez.Value = DBNull.Value;
                        else
                            pespensionadoporinvalidez.Value = pIngresoPersonal.espensionadoporinvalidez;
                        pespensionadoporinvalidez.Direction = ParameterDirection.Input;
                        pespensionadoporinvalidez.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pespensionadoporinvalidez);



                        DbParameter pfechaultimopagoperiodica = cmdTransaccionFactory.CreateParameter();
                        pfechaultimopagoperiodica.ParameterName = "p_fechaultimopagoperiodica";
                        if (pIngresoPersonal.fechaultimopagoperiodica == null)
                            pfechaultimopagoperiodica.Value = DBNull.Value;
                        else
                            pfechaultimopagoperiodica.Value = pIngresoPersonal.fechaultimopagoperiodica;
                        pfechaultimopagoperiodica.Direction = ParameterDirection.Input;
                        pfechaultimopagoperiodica.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaultimopagoperiodica);

                        DbParameter pfechacausacionprima = cmdTransaccionFactory.CreateParameter();
                        pfechacausacionprima.ParameterName = "p_fechacausacionprima";
                        if (pIngresoPersonal.fechacausacionprima == null)
                            pfechacausacionprima.Value = DBNull.Value;
                        else
                            pfechacausacionprima.Value = pIngresoPersonal.fechacausacionprima;
                        pfechacausacionprima.Direction = ParameterDirection.Input;
                        pfechacausacionprima.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacionprima);

                        DbParameter pfechacausacioncesantias = cmdTransaccionFactory.CreateParameter();
                        pfechacausacioncesantias.ParameterName = "p_fechacausacioncesantias";
                        if (pIngresoPersonal.fechacausacioncesantias == null)
                            pfechacausacioncesantias.Value = DBNull.Value;
                        else
                            pfechacausacioncesantias.Value = pIngresoPersonal.fechacausacioncesantias;
                        pfechacausacioncesantias.Direction = ParameterDirection.Input;
                        pfechacausacioncesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacioncesantias);

                        DbParameter pfechacausainterescesa = cmdTransaccionFactory.CreateParameter();
                        pfechacausainterescesa.ParameterName = "p_fechacausainterescesa";
                        if (pIngresoPersonal.fechacausainterescesa == null)
                            pfechacausainterescesa.Value = DBNull.Value;
                        else
                            pfechacausainterescesa.Value = pIngresoPersonal.fechacausainterescesa;
                        pfechacausainterescesa.Direction = ParameterDirection.Input;
                        pfechacausainterescesa.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausainterescesa);

                        DbParameter pfechacausacionvacaciones = cmdTransaccionFactory.CreateParameter();
                        pfechacausacionvacaciones.ParameterName = "p_fechacausacionvacaciones";
                        if (pIngresoPersonal.fechacausacionvacaciones == null)
                            pfechacausacionvacaciones.Value = DBNull.Value;
                        else
                            pfechacausacionvacaciones.Value = pIngresoPersonal.fechacausacionvacaciones;
                        pfechacausacionvacaciones.Direction = ParameterDirection.Input;
                        pfechacausacionvacaciones.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacionvacaciones);

                        DbParameter pcuentaprovision = cmdTransaccionFactory.CreateParameter();
                        pcuentaprovision.ParameterName = "p_cuentaprovision";
                        if (pIngresoPersonal.cuentaprovision == null)
                            pcuentaprovision.Value = DBNull.Value;
                        else
                            pcuentaprovision.Value = pIngresoPersonal.cuentaprovision;
                        pcuentaprovision.Direction = ParameterDirection.Input;
                        pcuentaprovision.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuentaprovision);

                        DbParameter pcuentacontable = cmdTransaccionFactory.CreateParameter();
                        pcuentacontable.ParameterName = "p_cuentacontable";
                        if (pIngresoPersonal.cuentacontable == null)
                            pcuentacontable.Value = DBNull.Value;
                        else
                            pcuentacontable.Value = pIngresoPersonal.cuentacontable;
                        pcuentacontable.Direction = ParameterDirection.Input;
                        pcuentacontable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuentacontable);

                        DbParameter pescontratoprestacional = cmdTransaccionFactory.CreateParameter();
                        pescontratoprestacional.ParameterName = "p_escontratoprestacional";
                        if (pIngresoPersonal.escontratoprestacional == null)
                            pescontratoprestacional.Value = DBNull.Value;
                        else
                            pescontratoprestacional.Value = pIngresoPersonal.escontratoprestacional;
                        pescontratoprestacional.Direction = ParameterDirection.Input;
                        pescontratoprestacional.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pescontratoprestacional);

                        DbParameter P_AREA = cmdTransaccionFactory.CreateParameter();
                        P_AREA.ParameterName = "P_AREA";
                        if (pIngresoPersonal.area == 0)
                            P_AREA.Value = DBNull.Value;
                        else
                            P_AREA.Value = pIngresoPersonal.area;
                        P_AREA.Direction = ParameterDirection.Input;
                        P_AREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_AREA);



                        DbParameter ptiporiesgoaarl = cmdTransaccionFactory.CreateParameter();
                        ptiporiesgoaarl.ParameterName = "p_tiporiesgoarl";
                        if (pIngresoPersonal.tipo_riesgo == null)
                            ptiporiesgoaarl.Value = DBNull.Value;
                        else
                            ptiporiesgoaarl.Value = pIngresoPersonal.tipo_riesgo;
                        ptiporiesgoaarl.Direction = ParameterDirection.Input;
                        ptiporiesgoaarl.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptiporiesgoaarl);




                        DbParameter pporcentajearl = cmdTransaccionFactory.CreateParameter();
                        pporcentajearl.ParameterName = "p_porcentajearl";
                        if (pIngresoPersonal.porcentajearl == null)
                            pporcentajearl.Value = DBNull.Value;
                        else
                            pporcentajearl.Value = pIngresoPersonal.porcentajearl;
                        pporcentajearl.Direction = ParameterDirection.Input;
                        pporcentajearl.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentajearl);

                        DbParameter PPROCESORETENCION = cmdTransaccionFactory.CreateParameter();
                        PPROCESORETENCION.ParameterName = "P_PROCESORETENCION";
                        if (pIngresoPersonal.procesoretencion == null)
                            PPROCESORETENCION.Value = DBNull.Value;
                        else
                            PPROCESORETENCION.Value = pIngresoPersonal.procesoretencion;
                        PPROCESORETENCION.Direction = ParameterDirection.Input;
                        PPROCESORETENCION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PPROCESORETENCION);


                        DbParameter psalariointegral= cmdTransaccionFactory.CreateParameter();
                        psalariointegral.ParameterName = "p_salariointegral";
                        if (pIngresoPersonal.essalariointegral == null)
                            psalariointegral.Value = DBNull.Value;
                        else
                            psalariointegral.Value = pIngresoPersonal.essalariointegral;
                        psalariointegral.Direction = ParameterDirection.Input;
                        psalariointegral.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psalariointegral);


                        DbParameter pdia_habil = cmdTransaccionFactory.CreateParameter();
                        pdia_habil.ParameterName = "p_dia_habil";
                        if (pIngresoPersonal.dia_habil == null)
                            pdia_habil.Value = DBNull.Value;
                        else
                            pdia_habil.Value = pIngresoPersonal.dia_habil;
                        pdia_habil.Direction = ParameterDirection.Input;
                        pdia_habil.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdia_habil);


                        
                        DbParameter p_inactividad = cmdTransaccionFactory.CreateParameter();
                        p_inactividad.ParameterName = "p_inactividad";
                        if (pIngresoPersonal.inactivacion == null)
                            p_inactividad.Value = DBNull.Value;
                        else
                            p_inactividad.Value = pIngresoPersonal.inactivacion;
                        p_inactividad.Direction = ParameterDirection.Input;
                        p_inactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_inactividad);


                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pIngresoPersonal.cod_empresa == null)
                            p_cod_empresa.Value = DBNull.Value;
                        else
                            p_cod_empresa.Value = pIngresoPersonal.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INGRESOPER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pIngresoPersonal.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pIngresoPersonal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "CrearIngresoPersonal", ex);
                        return null;
                    }
                }
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(long codigoempleado, long codigonomina, Usuario usuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select ing.consecutivo, ing.FECHAINGRESO, ing.SALARIO, car.DESCRIPCION as desc_cargo, ing.CODIGOTIPOCONTRATO, ing.CODIGOCENTROCOSTO
                                                    FROM INGRESOPERSONAL ing
                                                    JOIN CARGO_NOMINA car on car.IDCARGO = ing.CODIGOCARGO
                                                    WHERE ing.CODIGOEMPLEADO = {0} 
                                                    AND ing.CODIGONOMINA = {1} 
                                                    ", codigoempleado, codigonomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina", ex);
                        return null;
                    }
                }
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoPorCodigoIngreso(long codigoIngreso, Usuario usuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select ing.consecutivo, ing.FECHAINGRESO, ing.SALARIO, car.DESCRIPCION as desc_cargo, ing.CODIGOTIPOCONTRATO, ing.CODIGOCENTROCOSTO
                                                    FROM INGRESOPERSONAL ing
                                                    JOIN CARGO_nomina car on car.idcargo = ing.CODIGOCARGO
                                                    WHERE ing.CONSECUTIVO = {0} ", codigoIngreso);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["consecutivo"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["consecutivo"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarInformacionDeContratoPorCodigoIngreso", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? ConsultarFechaIngresoSegunNominaYEmpleado(long codigoNomina, long codigoEmpleado, Usuario usuario)
        {
            DbDataReader resultado;
            DateTime? fechaIngreso = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select FECHAINGRESO 
                                                    FROM INGRESOPERSONAL 
                                                    WHERE CODIGOEMPLEADO = {0} 
                                                    AND CODIGONOMINA = {1} 
                                                    AND ESTAACTIVOCONTRATO = 1 ", codigoEmpleado, codigoNomina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["FECHAINGRESO"] != DBNull.Value)
                            {
                                fechaIngreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return fechaIngreso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarFechaIngresoSegunNominaYEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(IngresoPersonal empleado, Usuario usuario)
        {
            DbDataReader resultado;
            bool existe = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"select CONSECUTIVO
                                                    FROM INGRESOPERSONAL
                                                    WHERE CODIGONOMINA = {0}
                                                    AND CODIGOEMPLEADO = {1} 
                                                    AND ESTAACTIVOCONTRATO = 1 ", empleado.codigonomina, empleado.codigoempleado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value)
                            {
                                existe = true;
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina", ex);
                        return false;
                    }
                }
            }
        }

        public List<IngresoPersonal> ListarContratosDeUnEmpleado(long codigoEmpleado, Usuario usuario)
        {
            DbDataReader resultado;
            List<IngresoPersonal> lstIngresoPersonal = new List<IngresoPersonal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select Ing.Codigotipocontrato, Tip.Descripcion as desc_contrato 
                                    from ingresopersonal ing 
                                    join Tipocontrato tip on Ing.Codigotipocontrato = Tip.Codtipocontrato
                                    WHERE Ing.Codigoempleado =  " + codigoEmpleado;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IngresoPersonal entidad = new IngresoPersonal();

                            if (resultado["Codigotipocontrato"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["Codigotipocontrato"]);
                            if (resultado["desc_contrato"] != DBNull.Value) entidad.desc_contrato = Convert.ToString(resultado["desc_contrato"]);

                            lstIngresoPersonal.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIngresoPersonal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ListarContratosDeUnEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public IngresoPersonal ModificarIngresoPersonal(IngresoPersonal pIngresoPersonal, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pIngresoPersonal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pIngresoPersonal.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pIngresoPersonal.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        if (pIngresoPersonal.codigonomina == null)
                            pcodigonomina.Value = DBNull.Value;
                        else
                            pcodigonomina.Value = pIngresoPersonal.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigotipocontrato = cmdTransaccionFactory.CreateParameter();
                        pcodigotipocontrato.ParameterName = "p_codigotipocontrato";
                        if (pIngresoPersonal.codigotipocontrato == null)
                            pcodigotipocontrato.Value = DBNull.Value;
                        else
                            pcodigotipocontrato.Value = pIngresoPersonal.codigotipocontrato;
                        pcodigotipocontrato.Direction = ParameterDirection.Input;
                        pcodigotipocontrato.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotipocontrato);

                        DbParameter pcodigocargo = cmdTransaccionFactory.CreateParameter();
                        pcodigocargo.ParameterName = "p_codigocargo";
                        if (pIngresoPersonal.codigocargo == null)
                            pcodigocargo.Value = DBNull.Value;
                        else
                            pcodigocargo.Value = pIngresoPersonal.codigocargo;
                        pcodigocargo.Direction = ParameterDirection.Input;
                        pcodigocargo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocargo);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        if (pIngresoPersonal.codigocentrocosto == null)
                            pcodigocentrocosto.Value = DBNull.Value;
                        else
                            pcodigocentrocosto.Value = pIngresoPersonal.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pfechaingreso = cmdTransaccionFactory.CreateParameter();
                        pfechaingreso.ParameterName = "p_fechaingreso";
                        if (pIngresoPersonal.fechaingreso == null)
                            pfechaingreso.Value = DBNull.Value;
                        else
                            pfechaingreso.Value = pIngresoPersonal.fechaingreso;
                        pfechaingreso.Direction = ParameterDirection.Input;
                        pfechaingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaingreso);

                        DbParameter pfechainicioperiodoprueba = cmdTransaccionFactory.CreateParameter();
                        pfechainicioperiodoprueba.ParameterName = "p_fechainicioperiodoprueba";
                        if (pIngresoPersonal.fechainicioperiodoprueba == null)
                            pfechainicioperiodoprueba.Value = DBNull.Value;
                        else
                            pfechainicioperiodoprueba.Value = pIngresoPersonal.fechainicioperiodoprueba;
                        pfechainicioperiodoprueba.Direction = ParameterDirection.Input;
                        pfechainicioperiodoprueba.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicioperiodoprueba);

                        DbParameter p_fechaterminaprueba = cmdTransaccionFactory.CreateParameter();
                        p_fechaterminaprueba.ParameterName = "p_fechaterminaprueba";
                        if (pIngresoPersonal.fechaterminacionperiodoprueba == null)
                            p_fechaterminaprueba.Value = DBNull.Value;
                        else
                            p_fechaterminaprueba.Value = pIngresoPersonal.fechaterminacionperiodoprueba;
                        p_fechaterminaprueba.Direction = ParameterDirection.Input;
                        p_fechaterminaprueba.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(p_fechaterminaprueba);

                        DbParameter ptieneley50 = cmdTransaccionFactory.CreateParameter();
                        ptieneley50.ParameterName = "p_tieneley50";
                        if (pIngresoPersonal.tieneley50 == null)
                            ptieneley50.Value = DBNull.Value;
                        else
                            ptieneley50.Value = pIngresoPersonal.tieneley50;
                        ptieneley50.Direction = ParameterDirection.Input;
                        ptieneley50.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptieneley50);

                        DbParameter pesextranjero = cmdTransaccionFactory.CreateParameter();
                        pesextranjero.ParameterName = "p_esextranjero";
                        if (pIngresoPersonal.esextranjero == null)
                            pesextranjero.Value = DBNull.Value;
                        else
                            pesextranjero.Value = pIngresoPersonal.esextranjero;
                        pesextranjero.Direction = ParameterDirection.Input;
                        pesextranjero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesextranjero);

                        DbParameter psalario = cmdTransaccionFactory.CreateParameter();
                        psalario.ParameterName = "p_salario";
                        if (pIngresoPersonal.salario == null)
                            psalario.Value = DBNull.Value;
                        else
                            psalario.Value = pIngresoPersonal.salario;
                        psalario.Direction = ParameterDirection.Input;
                        psalario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psalario);

                        DbParameter pessueldovariable = cmdTransaccionFactory.CreateParameter();
                        pessueldovariable.ParameterName = "p_essueldovariable";
                        if (pIngresoPersonal.essueldovariable == null)
                            pessueldovariable.Value = DBNull.Value;
                        else
                            pessueldovariable.Value = pIngresoPersonal.essueldovariable;
                        pessueldovariable.Direction = ParameterDirection.Input;
                        pessueldovariable.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pessueldovariable);

                        DbParameter pauxiliotransporte = cmdTransaccionFactory.CreateParameter();
                        pauxiliotransporte.ParameterName = "p_auxiliotransporte";
                        if (pIngresoPersonal.auxiliotransporte == null)
                            pauxiliotransporte.Value = DBNull.Value;
                        else
                            pauxiliotransporte.Value = pIngresoPersonal.auxiliotransporte;
                        pauxiliotransporte.Direction = ParameterDirection.Input;
                        pauxiliotransporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pauxiliotransporte);

                        DbParameter pformapago = cmdTransaccionFactory.CreateParameter();
                        pformapago.ParameterName = "p_formapago";
                        if (pIngresoPersonal.formapago == null)
                            pformapago.Value = DBNull.Value;
                        else
                            pformapago.Value = pIngresoPersonal.formapago;
                        pformapago.Direction = ParameterDirection.Input;
                        pformapago.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pformapago);

                        DbParameter ptipocuenta = cmdTransaccionFactory.CreateParameter();
                        ptipocuenta.ParameterName = "p_tipocuenta";
                        if (pIngresoPersonal.tipocuenta == null)
                            ptipocuenta.Value = DBNull.Value;
                        else
                            ptipocuenta.Value = pIngresoPersonal.tipocuenta;
                        ptipocuenta.Direction = ParameterDirection.Input;
                        ptipocuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipocuenta);

                        DbParameter pcodigobanco = cmdTransaccionFactory.CreateParameter();
                        pcodigobanco.ParameterName = "p_codigobanco";
                        if (pIngresoPersonal.codigobanco == null)
                            pcodigobanco.Value = DBNull.Value;
                        else
                            pcodigobanco.Value = pIngresoPersonal.codigobanco;
                        pcodigobanco.Direction = ParameterDirection.Input;
                        pcodigobanco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigobanco);

                        DbParameter pnumerocuentabancaria = cmdTransaccionFactory.CreateParameter();
                        pnumerocuentabancaria.ParameterName = "p_numerocuentabancaria";
                        if (pIngresoPersonal.numerocuentabancaria == null)
                            pnumerocuentabancaria.Value = DBNull.Value;
                        else
                            pnumerocuentabancaria.Value = pIngresoPersonal.numerocuentabancaria;
                        pnumerocuentabancaria.Direction = ParameterDirection.Input;
                        pnumerocuentabancaria.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumerocuentabancaria);

                        DbParameter pcodigofondosalud = cmdTransaccionFactory.CreateParameter();
                        pcodigofondosalud.ParameterName = "p_codigofondosalud";
                        if (pIngresoPersonal.codigofondosalud == null)
                            pcodigofondosalud.Value = DBNull.Value;
                        else
                            pcodigofondosalud.Value = pIngresoPersonal.codigofondosalud;
                        pcodigofondosalud.Direction = ParameterDirection.Input;
                        pcodigofondosalud.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondosalud);

                        DbParameter pcodigofondopension = cmdTransaccionFactory.CreateParameter();
                        pcodigofondopension.ParameterName = "p_codigofondopension";
                        if (pIngresoPersonal.codigofondopension == null)
                            pcodigofondopension.Value = DBNull.Value;
                        else
                            pcodigofondopension.Value = pIngresoPersonal.codigofondopension;
                        pcodigofondopension.Direction = ParameterDirection.Input;
                        pcodigofondopension.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondopension);

                        DbParameter pcodigofondocesantias = cmdTransaccionFactory.CreateParameter();
                        pcodigofondocesantias.ParameterName = "p_codigofondocesantias";
                        if (pIngresoPersonal.codigofondocesantias == null)
                            pcodigofondocesantias.Value = DBNull.Value;
                        else
                            pcodigofondocesantias.Value = pIngresoPersonal.codigofondocesantias;
                        pcodigofondocesantias.Direction = ParameterDirection.Input;
                        pcodigofondocesantias.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigofondocesantias);

                        DbParameter pcodigocajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pcodigocajacompensacion.ParameterName = "p_codigocajacompensacion";
                        if (pIngresoPersonal.codigocajacompensacion == null)
                            pcodigocajacompensacion.Value = DBNull.Value;
                        else
                            pcodigocajacompensacion.Value = pIngresoPersonal.codigocajacompensacion;
                        pcodigocajacompensacion.Direction = ParameterDirection.Input;
                        pcodigocajacompensacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocajacompensacion);

                        DbParameter pcodigoarl = cmdTransaccionFactory.CreateParameter();
                        pcodigoarl.ParameterName = "p_codigoarl";
                        if (pIngresoPersonal.codigoarl == null)
                            pcodigoarl.Value = DBNull.Value;
                        else
                            pcodigoarl.Value = pIngresoPersonal.codigoarl;
                        pcodigoarl.Direction = ParameterDirection.Input;
                        pcodigoarl.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoarl);

                        DbParameter pcodigopensionvoluntaria = cmdTransaccionFactory.CreateParameter();
                        pcodigopensionvoluntaria.ParameterName = "p_codigopensionvoluntaria";
                        if (pIngresoPersonal.codigopensionvoluntaria == null)
                            pcodigopensionvoluntaria.Value = DBNull.Value;
                        else
                            pcodigopensionvoluntaria.Value = pIngresoPersonal.codigopensionvoluntaria;
                        pcodigopensionvoluntaria.Direction = ParameterDirection.Input;
                        pcodigopensionvoluntaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopensionvoluntaria);

                        DbParameter pfechaafiliacionsalud = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacionsalud.ParameterName = "p_fechaafiliacionsalud";
                        if (pIngresoPersonal.fechaafiliacionsalud == null)
                            pfechaafiliacionsalud.Value = DBNull.Value;
                        else
                            pfechaafiliacionsalud.Value = pIngresoPersonal.fechaafiliacionsalud;
                        pfechaafiliacionsalud.Direction = ParameterDirection.Input;
                        pfechaafiliacionsalud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacionsalud);

                        DbParameter pfechaafiliacionpension = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacionpension.ParameterName = "p_fechaafiliacionpension";
                        if (pIngresoPersonal.fechaafiliacionpension == null)
                            pfechaafiliacionpension.Value = DBNull.Value;
                        else
                            pfechaafiliacionpension.Value = pIngresoPersonal.fechaafiliacionpension;
                        pfechaafiliacionpension.Direction = ParameterDirection.Input;
                        pfechaafiliacionpension.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacionpension);

                        DbParameter pfechaafiliacioncesantias = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacioncesantias.ParameterName = "p_fechaafiliacioncesantias";
                        if (pIngresoPersonal.fechaafiliacioncesantias == null)
                            pfechaafiliacioncesantias.Value = DBNull.Value;
                        else
                            pfechaafiliacioncesantias.Value = pIngresoPersonal.fechaafiliacioncesantias;
                        pfechaafiliacioncesantias.Direction = ParameterDirection.Input;
                        pfechaafiliacioncesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacioncesantias);

                        DbParameter pfechaafiliacajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pfechaafiliacajacompensacion.ParameterName = "p_fechaafiliacajacompensacion";
                        if (pIngresoPersonal.fechaafiliacajacompensacion == null)
                            pfechaafiliacajacompensacion.Value = DBNull.Value;
                        else
                            pfechaafiliacajacompensacion.Value = pIngresoPersonal.fechaafiliacajacompensacion;
                        pfechaafiliacajacompensacion.Direction = ParameterDirection.Input;
                        pfechaafiliacajacompensacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaafiliacajacompensacion);

                        DbParameter pfecharetirosalud = cmdTransaccionFactory.CreateParameter();
                        pfecharetirosalud.ParameterName = "p_fecharetirosalud";
                        if (pIngresoPersonal.fecharetirosalud == null)
                            pfecharetirosalud.Value = DBNull.Value;
                        else
                            pfecharetirosalud.Value = pIngresoPersonal.fecharetirosalud;
                        pfecharetirosalud.Direction = ParameterDirection.Input;
                        pfecharetirosalud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirosalud);

                        DbParameter pfecharetiropension = cmdTransaccionFactory.CreateParameter();
                        pfecharetiropension.ParameterName = "p_fecharetiropension";
                        if (pIngresoPersonal.fecharetiropension == null)
                            pfecharetiropension.Value = DBNull.Value;
                        else
                            pfecharetiropension.Value = pIngresoPersonal.fecharetiropension;
                        pfecharetiropension.Direction = ParameterDirection.Input;
                        pfecharetiropension.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetiropension);

                        DbParameter pfecharetirocesantias = cmdTransaccionFactory.CreateParameter();
                        pfecharetirocesantias.ParameterName = "p_fecharetirocesantias";
                        if (pIngresoPersonal.fecharetirocesantias == null)
                            pfecharetirocesantias.Value = DBNull.Value;
                        else
                            pfecharetirocesantias.Value = pIngresoPersonal.fecharetirocesantias;
                        pfecharetirocesantias.Direction = ParameterDirection.Input;
                        pfecharetirocesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirocesantias);

                        DbParameter pfecharetirocajacompensacion = cmdTransaccionFactory.CreateParameter();
                        pfecharetirocajacompensacion.ParameterName = "p_fecharetirocajacompensacion";
                        if (pIngresoPersonal.fecharetirocajacompensacion == null)
                            pfecharetirocajacompensacion.Value = DBNull.Value;
                        else
                            pfecharetirocajacompensacion.Value = pIngresoPersonal.fecharetirocajacompensacion;
                        pfecharetirocajacompensacion.Direction = ParameterDirection.Input;
                        pfecharetirocajacompensacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharetirocajacompensacion);

                        DbParameter ptipocotizante = cmdTransaccionFactory.CreateParameter();
                        ptipocotizante.ParameterName = "p_tipocotizante";
                        if (pIngresoPersonal.tipocotizante == null)
                            ptipocotizante.Value = DBNull.Value;
                        else
                            ptipocotizante.Value = pIngresoPersonal.tipocotizante;
                        ptipocotizante.Direction = ParameterDirection.Input;
                        ptipocotizante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipocotizante);

                        DbParameter pespensionadoporvejez = cmdTransaccionFactory.CreateParameter();
                        pespensionadoporvejez.ParameterName = "p_espensionadoporvejez";
                        if (pIngresoPersonal.espensionadoporvejez == null)
                            pespensionadoporvejez.Value = DBNull.Value;
                        else
                            pespensionadoporvejez.Value = pIngresoPersonal.espensionadoporvejez;
                        pespensionadoporvejez.Direction = ParameterDirection.Input;
                        pespensionadoporvejez.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pespensionadoporvejez);




                        DbParameter pespensionadoporinvalidez = cmdTransaccionFactory.CreateParameter();
                        pespensionadoporinvalidez.ParameterName = "p_espensionadoporinvalidez";
                        if (pIngresoPersonal.espensionadoporinvalidez == null)
                            pespensionadoporinvalidez.Value = DBNull.Value;
                        else
                            pespensionadoporinvalidez.Value = pIngresoPersonal.espensionadoporinvalidez;
                        pespensionadoporinvalidez.Direction = ParameterDirection.Input;
                        pespensionadoporinvalidez.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pespensionadoporinvalidez);



                        DbParameter pfechaultimopagoperiodica = cmdTransaccionFactory.CreateParameter();
                        pfechaultimopagoperiodica.ParameterName = "p_fechaultimopagoperiodica";
                        if (pIngresoPersonal.fechaultimopagoperiodica == null)
                            pfechaultimopagoperiodica.Value = DBNull.Value;
                        else
                            pfechaultimopagoperiodica.Value = pIngresoPersonal.fechaultimopagoperiodica;
                        pfechaultimopagoperiodica.Direction = ParameterDirection.Input;
                        pfechaultimopagoperiodica.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaultimopagoperiodica);

                        DbParameter pfechacausacionprima = cmdTransaccionFactory.CreateParameter();
                        pfechacausacionprima.ParameterName = "p_fechacausacionprima";
                        if (pIngresoPersonal.fechacausacionprima == null)
                            pfechacausacionprima.Value = DBNull.Value;
                        else
                            pfechacausacionprima.Value = pIngresoPersonal.fechacausacionprima;
                        pfechacausacionprima.Direction = ParameterDirection.Input;
                        pfechacausacionprima.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacionprima);

                        DbParameter pfechacausacioncesantias = cmdTransaccionFactory.CreateParameter();
                        pfechacausacioncesantias.ParameterName = "p_fechacausacioncesantias";
                        if (pIngresoPersonal.fechacausacioncesantias == null)
                            pfechacausacioncesantias.Value = DBNull.Value;
                        else
                            pfechacausacioncesantias.Value = pIngresoPersonal.fechacausacioncesantias;
                        pfechacausacioncesantias.Direction = ParameterDirection.Input;
                        pfechacausacioncesantias.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacioncesantias);

                        DbParameter pfechacausainterescesa = cmdTransaccionFactory.CreateParameter();
                        pfechacausainterescesa.ParameterName = "p_fechacausainterescesa";
                        if (pIngresoPersonal.fechacausainterescesa == null)
                            pfechacausainterescesa.Value = DBNull.Value;
                        else
                            pfechacausainterescesa.Value = pIngresoPersonal.fechacausainterescesa;
                        pfechacausainterescesa.Direction = ParameterDirection.Input;
                        pfechacausainterescesa.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausainterescesa);

                        DbParameter pfechacausacionvacaciones = cmdTransaccionFactory.CreateParameter();
                        pfechacausacionvacaciones.ParameterName = "p_fechacausacionvacaciones";
                        if (pIngresoPersonal.fechacausacionvacaciones == null)
                            pfechacausacionvacaciones.Value = DBNull.Value;
                        else
                            pfechacausacionvacaciones.Value = pIngresoPersonal.fechacausacionvacaciones;
                        pfechacausacionvacaciones.Direction = ParameterDirection.Input;
                        pfechacausacionvacaciones.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacausacionvacaciones);

                        DbParameter pcuentaprovision = cmdTransaccionFactory.CreateParameter();
                        pcuentaprovision.ParameterName = "p_cuentaprovision";
                        if (pIngresoPersonal.cuentaprovision == null)
                            pcuentaprovision.Value = DBNull.Value;
                        else
                            pcuentaprovision.Value = pIngresoPersonal.cuentaprovision;
                        pcuentaprovision.Direction = ParameterDirection.Input;
                        pcuentaprovision.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuentaprovision);

                        DbParameter pcuentacontable = cmdTransaccionFactory.CreateParameter();
                        pcuentacontable.ParameterName = "p_cuentacontable";
                        if (pIngresoPersonal.cuentacontable == null)
                            pcuentacontable.Value = DBNull.Value;
                        else
                            pcuentacontable.Value = pIngresoPersonal.cuentacontable;
                        pcuentacontable.Direction = ParameterDirection.Input;
                        pcuentacontable.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcuentacontable);

                        DbParameter pescontratoprestacional = cmdTransaccionFactory.CreateParameter();
                        pescontratoprestacional.ParameterName = "p_escontratoprestacional";
                        if (pIngresoPersonal.escontratoprestacional == null)
                            pescontratoprestacional.Value = DBNull.Value;
                        else
                            pescontratoprestacional.Value = pIngresoPersonal.escontratoprestacional;
                        pescontratoprestacional.Direction = ParameterDirection.Input;
                        pescontratoprestacional.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pescontratoprestacional);

                        DbParameter P_AREA = cmdTransaccionFactory.CreateParameter();
                        P_AREA.ParameterName = "P_AREA";
                        if (pIngresoPersonal.area == 0)
                            P_AREA.Value = DBNull.Value;
                        else
                            P_AREA.Value = pIngresoPersonal.area;
                        P_AREA.Direction = ParameterDirection.Input;
                        P_AREA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_AREA);

                        DbParameter ptiporiesgoaarl = cmdTransaccionFactory.CreateParameter();
                        ptiporiesgoaarl.ParameterName = "p_tiporiesgoarl";
                        if (pIngresoPersonal.tipo_riesgo == null)
                            ptiporiesgoaarl.Value = DBNull.Value;
                        else
                            ptiporiesgoaarl.Value = pIngresoPersonal.tipo_riesgo;
                        ptiporiesgoaarl.Direction = ParameterDirection.Input;
                        ptiporiesgoaarl.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptiporiesgoaarl);




                        DbParameter pporcentajearl = cmdTransaccionFactory.CreateParameter();
                        pporcentajearl.ParameterName = "p_porcentajearl";
                        if (pIngresoPersonal.porcentajearl == null)
                            pporcentajearl.Value = DBNull.Value;
                        else
                            pporcentajearl.Value = pIngresoPersonal.porcentajearl;
                        pporcentajearl.Direction = ParameterDirection.Input;
                        pporcentajearl.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pporcentajearl);



                        DbParameter PPROCESORETENCION = cmdTransaccionFactory.CreateParameter();
                        PPROCESORETENCION.ParameterName = "P_PROCESORETENCION";
                        if (pIngresoPersonal.procesoretencion == null)
                            PPROCESORETENCION.Value = DBNull.Value;
                        else
                            PPROCESORETENCION.Value = pIngresoPersonal.procesoretencion;
                        PPROCESORETENCION.Direction = ParameterDirection.Input;
                        PPROCESORETENCION.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PPROCESORETENCION);

                        DbParameter psalariointegral = cmdTransaccionFactory.CreateParameter();
                        psalariointegral.ParameterName = "p_salariointegral";
                        if (pIngresoPersonal.essalariointegral == null)
                            psalariointegral.Value = DBNull.Value;
                        else
                            psalariointegral.Value = pIngresoPersonal.essalariointegral;
                        psalariointegral.Direction = ParameterDirection.Input;
                        psalariointegral.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psalariointegral);

                        DbParameter pdia_habil = cmdTransaccionFactory.CreateParameter();
                        pdia_habil.ParameterName = "p_dia_habil";
                        if (pIngresoPersonal.dia_habil == null)
                            pdia_habil.Value = DBNull.Value;
                        else
                            pdia_habil.Value = pIngresoPersonal.dia_habil;
                        pdia_habil.Direction = ParameterDirection.Input;
                        pdia_habil.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdia_habil);



                        DbParameter p_inactividad = cmdTransaccionFactory.CreateParameter();
                        p_inactividad.ParameterName = "p_inactividad";
                        if (pIngresoPersonal.inactivacion == null)
                            p_inactividad.Value = DBNull.Value;
                        else
                            p_inactividad.Value = pIngresoPersonal.inactivacion;
                        p_inactividad.Direction = ParameterDirection.Input;
                        p_inactividad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_inactividad);



                        DbParameter p_cod_empresa = cmdTransaccionFactory.CreateParameter();
                        p_cod_empresa.ParameterName = "p_cod_empresa";
                        if (pIngresoPersonal.cod_empresa == null)
                            p_cod_empresa.Value = DBNull.Value;
                        else
                            p_cod_empresa.Value = pIngresoPersonal.cod_empresa;
                        p_cod_empresa.Direction = ParameterDirection.Input;
                        p_cod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_empresa);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INGRESOPER_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pIngresoPersonal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ModificarIngresoPersonal", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarIngresoPersonal(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        IngresoPersonal pIngresoPersonal = new IngresoPersonal();
                        pIngresoPersonal = ConsultarIngresoPersonal(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pIngresoPersonal.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INGRESOPER_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "EliminarIngresoPersonal", ex);
                    }
                }
            }
        }


        public IngresoPersonal ConsultarIngresoPersonal(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT int.*, per.identificacion, PER.TIPO_IDENTIFICACION, PER.NOMBRE, NOM.DESCRIPCION AS DESC_NOMINA
                                        FROM INGRESOPERSONAL INT
                                        JOIN EMPLEADOS EMP ON EMP.CONSECUTIVO = INT.CODIGOEMPLEADO
                                        JOIN V_PERSONA PER ON INT.CODIGOPERSONA = PER.COD_PERSONA
                                        JOIN NOMINA_EMPLEADO NOM ON NOM.CONSECUTIVO = INT.CODIGONOMINA
                                        WHERE INT.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigocargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["FECHAINICIOPERIODOPRUEBA"] != DBNull.Value) entidad.fechainicioperiodoprueba = Convert.ToDateTime(resultado["FECHAINICIOPERIODOPRUEBA"]);
                            if (resultado["FECHATERMINACIONPERIODOPRUEBA"] != DBNull.Value) entidad.fechaterminacionperiodoprueba = Convert.ToDateTime(resultado["FECHATERMINACIONPERIODOPRUEBA"]);
                            if (resultado["TIENELEY50"] != DBNull.Value) entidad.tieneley50 = Convert.ToInt32(resultado["TIENELEY50"]);
                            if (resultado["ESEXTRANJERO"] != DBNull.Value) entidad.esextranjero = Convert.ToInt32(resultado["ESEXTRANJERO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["ESSUELDOVARIABLE"] != DBNull.Value) entidad.essueldovariable = Convert.ToInt64(resultado["ESSUELDOVARIABLE"]);
                            if (resultado["AUXILIOTRANSPORTE"] != DBNull.Value) entidad.auxiliotransporte = Convert.ToInt64(resultado["AUXILIOTRANSPORTE"]);
                            if (resultado["FORMAPAGO"] != DBNull.Value) entidad.formapago = Convert.ToInt64(resultado["FORMAPAGO"]);
                            if (resultado["TIPOCUENTA"] != DBNull.Value) entidad.tipocuenta = Convert.ToInt64(resultado["TIPOCUENTA"]);
                            if (resultado["CODIGOBANCO"] != DBNull.Value) entidad.codigobanco = Convert.ToInt64(resultado["CODIGOBANCO"]);
                            if (resultado["NUMEROCUENTABANCARIA"] != DBNull.Value) entidad.numerocuentabancaria = Convert.ToString(resultado["NUMEROCUENTABANCARIA"]);
                            if (resultado["CODIGOFONDOSALUD"] != DBNull.Value) entidad.codigofondosalud = Convert.ToInt64(resultado["CODIGOFONDOSALUD"]);
                            if (resultado["CODIGOFONDOPENSION"] != DBNull.Value) entidad.codigofondopension = Convert.ToInt64(resultado["CODIGOFONDOPENSION"]);
                            if (resultado["CODIGOFONDOCESANTIAS"] != DBNull.Value) entidad.codigofondocesantias = Convert.ToInt64(resultado["CODIGOFONDOCESANTIAS"]);
                            if (resultado["CODIGOCAJACOMPENSACION"] != DBNull.Value) entidad.codigocajacompensacion = Convert.ToInt64(resultado["CODIGOCAJACOMPENSACION"]);
                            if (resultado["CODIGOARL"] != DBNull.Value) entidad.codigoarl = Convert.ToInt64(resultado["CODIGOARL"]);
                            if (resultado["CODIGOPENSIONVOLUNTARIA"] != DBNull.Value) entidad.codigopensionvoluntaria = Convert.ToInt64(resultado["CODIGOPENSIONVOLUNTARIA"]);
                            if (resultado["FECHAAFILIACIONSALUD"] != DBNull.Value) entidad.fechaafiliacionsalud = Convert.ToDateTime(resultado["FECHAAFILIACIONSALUD"]);
                            if (resultado["FECHAAFILIACIONPENSION"] != DBNull.Value) entidad.fechaafiliacionpension = Convert.ToDateTime(resultado["FECHAAFILIACIONPENSION"]);
                            if (resultado["FECHAAFILIACIONCESANTIAS"] != DBNull.Value) entidad.fechaafiliacioncesantias = Convert.ToDateTime(resultado["FECHAAFILIACIONCESANTIAS"]);
                            if (resultado["FECHAAFILIACAJACOMPENSACION"] != DBNull.Value) entidad.fechaafiliacajacompensacion = Convert.ToDateTime(resultado["FECHAAFILIACAJACOMPENSACION"]);
                            if (resultado["FECHARETIROSALUD"] != DBNull.Value) entidad.fecharetirosalud = Convert.ToDateTime(resultado["FECHARETIROSALUD"]);
                            if (resultado["FECHARETIROPENSION"] != DBNull.Value) entidad.fecharetiropension = Convert.ToDateTime(resultado["FECHARETIROPENSION"]);
                            if (resultado["FECHARETIROCESANTIAS"] != DBNull.Value) entidad.fecharetirocesantias = Convert.ToDateTime(resultado["FECHARETIROCESANTIAS"]);
                            if (resultado["FECHARETIROCAJACOMPENSACION"] != DBNull.Value) entidad.fecharetirocajacompensacion = Convert.ToDateTime(resultado["FECHARETIROCAJACOMPENSACION"]);
                            if (resultado["TIPOCOTIZANTE"] != DBNull.Value) entidad.tipocotizante = Convert.ToInt64(resultado["TIPOCOTIZANTE"]);
                            if (resultado["ESPENSIONADOPORVEJEZ"] != DBNull.Value) entidad.espensionadoporvejez = Convert.ToInt64(resultado["ESPENSIONADOPORVEJEZ"]);
                            if (resultado["ESPENSIONADOPORINVALIDEZ"] != DBNull.Value) entidad.espensionadoporinvalidez = Convert.ToInt64(resultado["ESPENSIONADOPORINVALIDEZ"]);
                            if (resultado["FECHAULTIMOPAGOPERIODICA"] != DBNull.Value) entidad.fechaultimopagoperiodica = Convert.ToDateTime(resultado["FECHAULTIMOPAGOPERIODICA"]);
                            if (resultado["FECHACAUSACIONPRIMA"] != DBNull.Value) entidad.fechacausacionprima = Convert.ToDateTime(resultado["FECHACAUSACIONPRIMA"]);
                            if (resultado["FECHACAUSACIONCESANTIAS"] != DBNull.Value) entidad.fechacausacioncesantias = Convert.ToDateTime(resultado["FECHACAUSACIONCESANTIAS"]);
                            if (resultado["FECHACAUSAINTERESCESA"] != DBNull.Value) entidad.fechacausainterescesa = Convert.ToDateTime(resultado["FECHACAUSAINTERESCESA"]);
                            if (resultado["FECHACAUSACIONVACACIONES"] != DBNull.Value) entidad.fechacausacionvacaciones = Convert.ToDateTime(resultado["FECHACAUSACIONVACACIONES"]);
                            if (resultado["CUENTAPROVISION"] != DBNull.Value) entidad.cuentaprovision = Convert.ToString(resultado["CUENTAPROVISION"]);
                            if (resultado["CUENTACONTABLE"] != DBNull.Value) entidad.cuentacontable = Convert.ToString(resultado["CUENTACONTABLE"]);
                            if (resultado["ESCONTRATOPRESTACIONAL"] != DBNull.Value) entidad.escontratoprestacional = Convert.ToInt64(resultado["ESCONTRATOPRESTACIONAL"]);
                            if (resultado["AREA"] != DBNull.Value) entidad.area = Convert.ToInt64(resultado["AREA"]);
                         
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DESC_NOMINA"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["DESC_NOMINA"]);
                            if (resultado["TIPORIESGOARL"] != DBNull.Value) entidad.tipo_riesgo = Convert.ToInt64(resultado["TIPORIESGOARL"]);
                            if (resultado["PORCENTAJEARL"] != DBNull.Value) entidad.porcentajearl = Convert.ToDecimal(resultado["PORCENTAJEARL"]);
                            if (resultado["PROCESORETENCION"] != DBNull.Value) entidad.procesoretencion = Convert.ToInt64(resultado["PROCESORETENCION"]);
                            if (resultado["ESSALARIOINTEGRAL"] != DBNull.Value) entidad.essalariointegral = Convert.ToInt64(resultado["ESSALARIOINTEGRAL"]);
                            if (resultado["estaactivocontrato"] != DBNull.Value) entidad.estaactivocontrato = Convert.ToInt64(resultado["estaactivocontrato"]);
                            if (resultado["sabado_habil"] != DBNull.Value) entidad.dia_habil = Convert.ToString(resultado["sabado_habil"]);
                            if (resultado["INACTIVIDAD"] != DBNull.Value) entidad.inactivacion = Convert.ToInt32(resultado["INACTIVIDAD"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);




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
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarIngresoPersonal", ex);
                        return null;
                    }
                }
            }
        }


        public List<IngresoPersonal> ListarIngresoPersonal(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<IngresoPersonal> lstIngresoPersonal = new List<IngresoPersonal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select int.*, per.nombre, per.identificacion, cst.DESCRIPCION as desc_centro_costo, nom.DESCRIPCION as desc_nomina,
                                        CASE int.estaactivocontrato WHEN 1 THEN 'Activo' WHEN 0 THEN 'Cerrado' END as desc_estaactivocontrato
                                        From IngresoPersonal int
                                        JOIN EMPLEADOS emp on emp.consecutivo = int.codigoempleado
                                        JOIN v_persona per on per.cod_persona = int.codigopersona
                                        JOIN NOMINA_EMPLEADO nom on int.codigonomina = nom.consecutivo
                                        LEFT JOIN CENTRO_COSTO cst on int.codigocentrocosto = cst.CENTRO_COSTO " + filtro + " ORDER BY int.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IngresoPersonal entidad = new IngresoPersonal();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOTIPOCONTRATO"] != DBNull.Value) entidad.codigotipocontrato = Convert.ToInt64(resultado["CODIGOTIPOCONTRATO"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigocargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["FECHAINGRESO"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["FECHAINGRESO"]);
                            if (resultado["FECHAINICIOPERIODOPRUEBA"] != DBNull.Value) entidad.fechainicioperiodoprueba = Convert.ToDateTime(resultado["FECHAINICIOPERIODOPRUEBA"]);
                            if (resultado["FECHATERMINACIONPERIODOPRUEBA"] != DBNull.Value) entidad.fechaterminacionperiodoprueba = Convert.ToDateTime(resultado["FECHATERMINACIONPERIODOPRUEBA"]);
                            if (resultado["TIENELEY50"] != DBNull.Value) entidad.tieneley50 = Convert.ToInt32(resultado["TIENELEY50"]);
                            if (resultado["ESEXTRANJERO"] != DBNull.Value) entidad.esextranjero = Convert.ToInt32(resultado["ESEXTRANJERO"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["SALARIO"]);
                            if (resultado["ESSUELDOVARIABLE"] != DBNull.Value) entidad.essueldovariable = Convert.ToInt64(resultado["ESSUELDOVARIABLE"]);
                            if (resultado["AUXILIOTRANSPORTE"] != DBNull.Value) entidad.auxiliotransporte = Convert.ToInt64(resultado["AUXILIOTRANSPORTE"]);
                            if (resultado["FORMAPAGO"] != DBNull.Value) entidad.formapago = Convert.ToInt64(resultado["FORMAPAGO"]);
                            if (resultado["TIPOCUENTA"] != DBNull.Value) entidad.tipocuenta = Convert.ToInt64(resultado["TIPOCUENTA"]);
                            if (resultado["CODIGOBANCO"] != DBNull.Value) entidad.codigobanco = Convert.ToInt64(resultado["CODIGOBANCO"]);
                            if (resultado["NUMEROCUENTABANCARIA"] != DBNull.Value) entidad.numerocuentabancaria = Convert.ToString(resultado["NUMEROCUENTABANCARIA"]);
                            if (resultado["CODIGOFONDOSALUD"] != DBNull.Value) entidad.codigofondosalud = Convert.ToInt64(resultado["CODIGOFONDOSALUD"]);
                            if (resultado["CODIGOFONDOPENSION"] != DBNull.Value) entidad.codigofondopension = Convert.ToInt64(resultado["CODIGOFONDOPENSION"]);
                            if (resultado["CODIGOFONDOCESANTIAS"] != DBNull.Value) entidad.codigofondocesantias = Convert.ToInt64(resultado["CODIGOFONDOCESANTIAS"]);
                            if (resultado["CODIGOCAJACOMPENSACION"] != DBNull.Value) entidad.codigocajacompensacion = Convert.ToInt64(resultado["CODIGOCAJACOMPENSACION"]);
                            if (resultado["CODIGOARL"] != DBNull.Value) entidad.codigoarl = Convert.ToInt64(resultado["CODIGOARL"]);
                            if (resultado["CODIGOPENSIONVOLUNTARIA"] != DBNull.Value) entidad.codigopensionvoluntaria = Convert.ToInt64(resultado["CODIGOPENSIONVOLUNTARIA"]);
                            if (resultado["FECHAAFILIACIONSALUD"] != DBNull.Value) entidad.fechaafiliacionsalud = Convert.ToDateTime(resultado["FECHAAFILIACIONSALUD"]);
                            if (resultado["FECHAAFILIACIONPENSION"] != DBNull.Value) entidad.fechaafiliacionpension = Convert.ToDateTime(resultado["FECHAAFILIACIONPENSION"]);
                            if (resultado["FECHAAFILIACIONCESANTIAS"] != DBNull.Value) entidad.fechaafiliacioncesantias = Convert.ToDateTime(resultado["FECHAAFILIACIONCESANTIAS"]);
                            if (resultado["FECHAAFILIACAJACOMPENSACION"] != DBNull.Value) entidad.fechaafiliacajacompensacion = Convert.ToDateTime(resultado["FECHAAFILIACAJACOMPENSACION"]);
                            if (resultado["FECHARETIROSALUD"] != DBNull.Value) entidad.fecharetirosalud = Convert.ToDateTime(resultado["FECHARETIROSALUD"]);
                            if (resultado["FECHARETIROPENSION"] != DBNull.Value) entidad.fecharetiropension = Convert.ToDateTime(resultado["FECHARETIROPENSION"]);
                            if (resultado["FECHARETIROCESANTIAS"] != DBNull.Value) entidad.fecharetirocesantias = Convert.ToDateTime(resultado["FECHARETIROCESANTIAS"]);
                            if (resultado["FECHARETIROCAJACOMPENSACION"] != DBNull.Value) entidad.fecharetirocajacompensacion = Convert.ToDateTime(resultado["FECHARETIROCAJACOMPENSACION"]);
                            if (resultado["TIPOCOTIZANTE"] != DBNull.Value) entidad.tipocotizante = Convert.ToInt64(resultado["TIPOCOTIZANTE"]);
                            if (resultado["ESPENSIONADOPORVEJEZ"] != DBNull.Value) entidad.espensionadoporvejez = Convert.ToInt64(resultado["ESPENSIONADOPORVEJEZ"]);
                            if (resultado["ESPENSIONADOPORINVALIDEZ"] != DBNull.Value) entidad.espensionadoporinvalidez = Convert.ToInt64(resultado["ESPENSIONADOPORINVALIDEZ"]);

                            if (resultado["FECHAULTIMOPAGOPERIODICA"] != DBNull.Value) entidad.fechaultimopagoperiodica = Convert.ToDateTime(resultado["FECHAULTIMOPAGOPERIODICA"]);
                            if (resultado["FECHACAUSACIONPRIMA"] != DBNull.Value) entidad.fechacausacionprima = Convert.ToDateTime(resultado["FECHACAUSACIONPRIMA"]);
                            if (resultado["FECHACAUSACIONCESANTIAS"] != DBNull.Value) entidad.fechacausacioncesantias = Convert.ToDateTime(resultado["FECHACAUSACIONCESANTIAS"]);
                            if (resultado["FECHACAUSAINTERESCESA"] != DBNull.Value) entidad.fechacausainterescesa = Convert.ToDateTime(resultado["FECHACAUSAINTERESCESA"]);
                            if (resultado["FECHACAUSACIONVACACIONES"] != DBNull.Value) entidad.fechacausacionvacaciones = Convert.ToDateTime(resultado["FECHACAUSACIONVACACIONES"]);
                            if (resultado["CUENTAPROVISION"] != DBNull.Value) entidad.cuentaprovision = Convert.ToString(resultado["CUENTAPROVISION"]);
                            if (resultado["CUENTACONTABLE"] != DBNull.Value) entidad.cuentacontable = Convert.ToString(resultado["CUENTACONTABLE"]);
                            if (resultado["ESCONTRATOPRESTACIONAL"] != DBNull.Value) entidad.escontratoprestacional = Convert.ToInt64(resultado["ESCONTRATOPRESTACIONAL"]);

                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_estaactivocontrato"] != DBNull.Value) entidad.desc_estaactivocontrato = Convert.ToString(resultado["desc_estaactivocontrato"]);
                            if (resultado["ESSALARIOINTEGRAL"] != DBNull.Value) entidad.essalariointegral = Convert.ToInt64(resultado["ESSALARIOINTEGRAL"]);

                            lstIngresoPersonal.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIngresoPersonal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ListarIngresoPersonal", ex);
                        return null;
                    }
                }
            }
        }

        public ConceptosFijosNominaEmpleados CrearConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pConceptosFijosNominaEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pConceptosFijosNominaEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pConceptosFijosNominaEmpleados.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter p_codigoIngresoPersonal = cmdTransaccionFactory.CreateParameter();
                        p_codigoIngresoPersonal.ParameterName = "p_codigoIngresoPersonal";
                        p_codigoIngresoPersonal.Value = pConceptosFijosNominaEmpleados.codigoIngresoPersonal;
                        p_codigoIngresoPersonal.Direction = ParameterDirection.Input;
                        p_codigoIngresoPersonal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoIngresoPersonal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEPTOSF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pConceptosFijosNominaEmpleados.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pConceptosFijosNominaEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "CrearConceptosFijosNominaEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public ConceptosFijosNominaEmpleados ModificarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pConceptosFijosNominaEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pConceptosFijosNominaEmpleados.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pConceptosFijosNominaEmpleados.codigoconcepto;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter p_codigoIngresoPersonal = cmdTransaccionFactory.CreateParameter();
                        p_codigoIngresoPersonal.ParameterName = "p_codigoIngresoPersonal";
                        p_codigoIngresoPersonal.Value = pConceptosFijosNominaEmpleados.codigoIngresoPersonal;
                        p_codigoIngresoPersonal.Direction = ParameterDirection.Input;
                        p_codigoIngresoPersonal.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoIngresoPersonal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEPTOSF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptosFijosNominaEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ModificarConceptosFijosNominaEmpleados", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarConceptosFijosNominaEmpleados(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados = new ConceptosFijosNominaEmpleados();
                        pConceptosFijosNominaEmpleados = ConsultarConceptosFijosNominaEmpleados(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pConceptosFijosNominaEmpleados.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_CONCEPTOSF_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "EliminarConceptosFijosNominaEmpleados", ex);
                    }
                }
            }
        }


        public ConceptosFijosNominaEmpleados ConsultarConceptosFijosNominaEmpleados(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConceptosFijosNominaEmpleados entidad = new ConceptosFijosNominaEmpleados();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ConceptosFijosNominaEmpleados WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt64(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CodigoIngresoPersonal"] != DBNull.Value) entidad.codigoIngresoPersonal = Convert.ToInt64(resultado["CodigoIngresoPersonal"]);
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
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarConceptosFijosNominaEmpleados", ex);
                        return null;
                    }
                }
            }
        }

        public List<IngresoPersonal> ListarAreas(IngresoPersonal pAreas, Usuario pusuario)
        {
            DbDataReader resultado;
            List<IngresoPersonal> lstareas = new List<IngresoPersonal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Area " + ObtenerFiltro(pAreas) + " ORDER BY 1 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IngresoPersonal entidad = new IngresoPersonal();

                            if (resultado["idarea"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["idarea"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["nombre"]);
                       
                            lstareas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstareas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptosFijosNominaEmpleadosData", "ListarAreas", ex);
                        return null;
                    }
                }
            }
        }

        public List<IngresoPersonal> ListarContratacion(IngresoPersonal pcontrato, Usuario pusuario)
        {
            DbDataReader resultado;
            List<IngresoPersonal> lstareas = new List<IngresoPersonal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select COD_contratacion as ListaId,tipo_contrato as ListaDescripcion from CONTRATACION";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IngresoPersonal entidad = new IngresoPersonal();

                            if (resultado["ListaId"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["ListaId"]);
                            if (resultado["ListaDescripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["ListaDescripcion"]);

                            lstareas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstareas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptosFijosNominaEmpleadosData", "ListarContratacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<ConceptosFijosNominaEmpleados> ListarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConceptosFijosNominaEmpleados> lstConceptosFijosNominaEmpleados = new List<ConceptosFijosNominaEmpleados>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ConceptosFijosNominaEmpleados " + ObtenerFiltro(pConceptosFijosNominaEmpleados) + " ORDER BY CONSECUTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ConceptosFijosNominaEmpleados entidad = new ConceptosFijosNominaEmpleados();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOCONCEPTO"] != DBNull.Value) entidad.codigoconcepto = Convert.ToInt64(resultado["CODIGOCONCEPTO"]);
                            if (resultado["CodigoIngresoPersonal"] != DBNull.Value) entidad.codigoIngresoPersonal = Convert.ToInt64(resultado["CodigoIngresoPersonal"]);

                            lstConceptosFijosNominaEmpleados.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConceptosFijosNominaEmpleados;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptosFijosNominaEmpleadosData", "ListarConceptosFijosNominaEmpleados", ex);
                        return null;
                    }
                }
            }
        }
        public IngresoPersonal ConsultarInformacionFechaFinvacaciones(String  pfechainicial, Int64 dias, Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();
            DateTime fecha = Convert.ToDateTime(pfechainicial.ToString()) ;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  COD_EMPRESA, FECHA_FIN_LIQUID_VACACIONES(" + "'"+ pfechainicial + "',"   + dias + ","   + pId +") as fecha from empresa";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechafinvacaciones = Convert.ToDateTime(resultado["FECHA"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarInformacionFechaFinvacaciones", ex);
                        return null;
                    }
                }
            }
        }

        public IngresoPersonal ConsultarInformacionFechaRegresovacaciones(String pfechainicial,  Int64 pId, Int64 pdias,  Usuario vUsuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();
            DateTime fecha = Convert.ToDateTime(pfechainicial.ToString());
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select  COD_EMPRESA, FECHA_REGRESO_VACACIONES(" + "'" + pfechainicial + "'," + pId  + "," +  pdias + " ) as fecha from empresa";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecharegresovacaciones = Convert.ToDateTime(resultado["FECHA"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarInformacionFechaRegresovacaciones", ex);
                        return null;
                    }
                }
            }
        }
        public IngresoPersonal ConsultarInformacionDiaslegales(Usuario vUsuario)
        {
            DbDataReader resultado;
            IngresoPersonal entidad = new IngresoPersonal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select dias_vacaciones ,  VACACIONES_ANTICIPADAS from nomina_seguridad_social ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["dias_vacaciones"] != DBNull.Value) entidad.diasvacaciones = Convert.ToInt32(resultado["dias_vacaciones"]);
                            if (resultado["VACACIONES_ANTICIPADAS"] != DBNull.Value) entidad.pagavacacionesant = Convert.ToInt32(resultado["VACACIONES_ANTICIPADAS"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ConsultarInformacionDiaslegales", ex);
                        return null;
                    }
                }
            }
        }


        public List<IngresoPersonal> ListarEmpresaRecaudoPersona(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<IngresoPersonal> lstEmpresaRecaudo = new List<IngresoPersonal>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_persona_empresa_recaudo WHERE cod_persona = " + pId.ToString() + " ORDER BY COD_EMPRESA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            IngresoPersonal entidad = new IngresoPersonal();
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            lstEmpresaRecaudo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IngresoPersonalData", "ListarEmpresaRecaudoPersona", ex);
                        return null;
                    }
                }
            }
        }



    }
}