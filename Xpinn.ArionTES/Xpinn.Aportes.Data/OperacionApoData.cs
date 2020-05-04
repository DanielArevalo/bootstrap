using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using System.Web;
//using System.Web.UI.WebControls;

namespace Xpinn.Aportes.Data
{
    public class OperacionApoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public OperacionApoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Boolean GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 pcod_ofi, Int64 pcod_persona, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter plcod_ope = cmdTransaccionFactory.CreateParameter();
                        plcod_ope.ParameterName = "pcod_ope";
                        plcod_ope.Value = pcod_ope;

                        DbParameter plcod_persona = cmdTransaccionFactory.CreateParameter();
                        plcod_persona.ParameterName = "pcod_persona";
                        plcod_persona.Value = pcod_persona;

                        DbParameter plcod_proceso = cmdTransaccionFactory.CreateParameter();
                        plcod_proceso.ParameterName = "pcod_proceso";
                        plcod_proceso.Value = pcod_proceso;

                        DbParameter plnum_comp = cmdTransaccionFactory.CreateParameter();
                        plnum_comp.ParameterName = "pnum_comp";
                        plnum_comp.Value = 0;
                        plnum_comp.Direction = ParameterDirection.Output;

                        DbParameter pltipo_comp = cmdTransaccionFactory.CreateParameter();
                        pltipo_comp.ParameterName = "ptipo_comp";
                        pltipo_comp.Value = 0;
                        pltipo_comp.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(plcod_ope);
                        cmdTransaccionFactory.Parameters.Add(plcod_persona);
                        cmdTransaccionFactory.Parameters.Add(plcod_proceso);
                        cmdTransaccionFactory.Parameters.Add(plnum_comp);
                        cmdTransaccionFactory.Parameters.Add(pltipo_comp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_INTERFACE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (plnum_comp != null) pnum_comp = Convert.ToInt64(plnum_comp.Value);
                        if (pltipo_comp != null) ptipo_comp = Convert.ToInt64(pltipo_comp.Value);

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "GenerarComprobane", ex);
                        return false;
                    }
                }
            }
        }


        public OperacionApo GrabarOperacion(OperacionApo pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_ofi;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero;
                        pcodi_cajero.Direction = ParameterDirection.Input;


                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "pobservacion";
                        if (pEntidad.observacion == null)
                            pobservacion.Value = DBNull.Value;
                        else
                            pobservacion.Value = pEntidad.observacion;
                        pobservacion.Direction = ParameterDirection.Input;


                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "pcod_proceso";
                        if (pEntidad.cod_proceso == null)
                            pcod_proceso.Value = DBNull.Value;
                        else
                            pcod_proceso.Value = pEntidad.cod_proceso;
                        pcod_proceso.DbType = DbType.Int64;



                        DbParameter pfecha_oper = cmdTransaccionFactory.CreateParameter();
                        pfecha_oper.ParameterName = "pfechaoper";
                        pfecha_oper.Value = pEntidad.fecha_oper;
                        pfecha_oper.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechacalc";
                        pfecha_cal.Value = pEntidad.fecha_calc;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "pnum_comp";
                        pnum_comp.Value = -2;
                        pnum_comp.Direction = ParameterDirection.Input;

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "ptipo_comp";
                        ptipo_comp.Value = -2;
                        ptipo_comp.Direction = ParameterDirection.Input;

                        DbParameter P_IP = cmdTransaccionFactory.CreateParameter();
                        P_IP.ParameterName = "P_IP";
                        P_IP.Value = pUsuario.IP;
                        P_IP.Direction = ParameterDirection.Input;

                        DbParameter p_error = cmdTransaccionFactory.CreateParameter();
                        p_error.ParameterName = "p_error";
                        p_error.Direction = ParameterDirection.Output;
                        p_error.Value = "";
                        p_error.DbType = DbType.AnsiStringFixedLength;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_oper);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(pobservacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);
                        cmdTransaccionFactory.Parameters.Add(P_IP);
                        cmdTransaccionFactory.Parameters.Add(p_error);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_TES_OPERACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //pEntidad.cod_ope = Convert.ToInt64(pcod_rango_atr.Value);
                        if (p_error != null)
                            if (p_error.Value != null)
                                pEntidad.error = p_error.Value.ToString();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("operacionData", "Grabaroperacion", ex);
                    }
                    return pEntidad;
                }
            }
        }

    }
}




