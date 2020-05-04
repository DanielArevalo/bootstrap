using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class ExtractoData : GlobalData
    {
        Configuracion conf = new Configuracion();

        protected ConnectionDataBase dbConnectionFactory;

        public ExtractoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Extracto> ListarExtracto(string filtro, DateTime pFechaCorte, DateTime pFechaPago, DateTime pFecDetaPagoIni, DateTime pFecDetaPagoFin,
            DateTime pFecVenAporIni, DateTime pFecVenAporFin, DateTime pFecVenCredIni, DateTime pFecVenCredFin, Usuario pUsuario)

        {
            DbDataReader resultado = default(DbDataReader);
            List<Extracto> lstExtracto = new List<Extracto>();
            Configuracion conf = new Configuracion();


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    string sql = "Select distinct V.Cod_Persona,V.Identificacion,V.Tipo_Persona,V.Tipo_Identificacion,V.Nombres,V.Apellidos, "
                                      + "V.Direccion,V.Telefono,V.Email ,V.Codciudadresidencia, C.Nomciudad , "
                                      + "(Select Max(Z.Fecha_Proximo_Pago) From Aporte Z Where Z.Cod_Persona = V.Cod_Persona) As Fecha_Proxpago_Aporte, "
                                      + " (Select SUM(Calcular_VrAPagarAporte(Z.Numero_Aporte,To_Date('" + Convert.ToDateTime(pFechaCorte).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'))) From Aporte Z Where Z.Cod_Persona = V.Cod_Persona) As Vr_Totalpagar_Aporte, "
                                      + "(Select Max(X.Fecha_Proximo_Pago) From Credito X Where X.Cod_Deudor = V.Cod_Persona) As Fecha_Proxpago_Cred, "
                                      + "(Select SUM(Calcular_VrAPagar(X.Numero_Radicacion,To_Date('" + Convert.ToDateTime(pFechaCorte).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'))) From Credito X Where X.Cod_Deudor = V.Cod_Persona) As Vr_Totalpagar_Cred "
                                      + "From Vextractodatoscliente V Left Join Ciudades C On C.Codciudad = V.Codciudadresidencia "
                                      + "Where 1 = 1" + filtro;
                    string filtroAporte = " And v.cod_persona In (Select a.cod_persona From aporte a Where a.cod_persona = v.cod_persona And a.estado = 1 ";
                    if (pFecVenAporIni != null && pFecVenAporIni != DateTime.MinValue)
                    {
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            filtroAporte += " And a.fecha_proximo_pago >= To_Date('" + Convert.ToDateTime(pFecVenAporIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            filtroAporte += " And a.fecha_proximo_pago >= '" + Convert.ToDateTime(pFecVenAporIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                    }
                    if (pFecVenAporFin != null && pFecVenAporFin != DateTime.MinValue)
                    {
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            filtroAporte += " And a.fecha_proximo_pago <= To_Date('" + Convert.ToDateTime(pFecVenAporFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            filtroAporte += " And a.fecha_proximo_pago <= '" + Convert.ToDateTime(pFecVenAporFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                    }
                    if ((pFecVenAporIni != null && pFecVenAporIni != DateTime.MinValue) || (pFecVenAporFin != null && pFecVenAporFin != DateTime.MinValue))
                    {
                        sql += filtroAporte;
                    }

                    filtroAporte += ") ";
                    string filtroCredito = " And v.cod_persona In (Select c.cod_deudor From credito c Where c.cod_deudor = v.cod_persona And c.estado = 'C' ";
                    if (pFecVenCredIni != null && pFecVenCredIni != DateTime.MinValue)
                    {
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            filtroCredito += " And c.fecha_proximo_pago >= To_Date('" + Convert.ToDateTime(pFecVenCredIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            filtroCredito += " And c.fecha_proximo_pago >= '" + Convert.ToDateTime(pFecVenCredIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                    }
                    if (pFecVenCredFin != null && pFecVenCredFin != DateTime.MinValue)
                    {
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            filtroCredito += " And c.fecha_proximo_pago <= To_Date('" + Convert.ToDateTime(pFecVenCredFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            filtroCredito += " And c.fecha_proximo_pago <= '" + Convert.ToDateTime(pFecVenCredFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                    }
                    filtroCredito += ") ";
                    if ((pFecVenCredIni != null && pFecVenCredIni != DateTime.MinValue) || (pFecVenCredFin != null && pFecVenCredFin != DateTime.MinValue))
                    {
                        sql += filtroCredito;
                    }
                    sql += " ORDER BY 1";

                    connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    while (resultado.Read())
                    {
                        Extracto entidad = new Extracto();
                        if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                        if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                        if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                        if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                        if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                        if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                        if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                        if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                        if (resultado["CODCIUDADRESIDENCIA"] != DBNull.Value) entidad.codciudadresidencia = Convert.ToInt64(resultado["CODCIUDADRESIDENCIA"]);
                        if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["NOMCIUDAD"]);
                        if (resultado["Fecha_Proxpago_Aporte"] != DBNull.Value) entidad.fechaProx_pago_Aporte = Convert.ToDateTime(resultado["Fecha_Proxpago_Aporte"]);
                        if (resultado["Vr_Totalpagar_Aporte"] != DBNull.Value) entidad.vr_totalPagar_aporte = Convert.ToDecimal(resultado["Vr_Totalpagar_Aporte"]);
                        if (resultado["Fecha_Proxpago_Cred"] != DBNull.Value) entidad.fechaProx_pago_Credito = Convert.ToDateTime(resultado["Fecha_Proxpago_Cred"]);
                        if (resultado["Vr_Totalpagar_Cred"] != DBNull.Value) entidad.vr_totalPagar_credito = Convert.ToDecimal(resultado["Vr_Totalpagar_Cred"]);
                        lstExtracto.Add(entidad);
                    }
                    return lstExtracto;
                }
            }
        }

        public List<Extracto> ListarExtractoAnualAporte(int cod_persona, DateTime fechaInicial, DateTime fechaFinal, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();

                        string sql = @" with suma as( select a.numero_aporte as numero_aporte, l.NOMBRE as Nom_linea,
                        ESNULO((select saldo from historico_aporte hist 
                        where hist.FECHA_HISTORICO = (SELECT MIN(histAN.FECHA_HISTORICO) 
                        FROM historico_aporte histAN WHERE histAN.FECHA_HISTORICO 
                        BETWEEN to_date('" + fechaInicial.AddDays(-1).ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                        and histAN.NUMERO_APORTE = a.NUMERO_APORTE) and hist.NUMERO_APORTE = a.NUMERO_APORTE),0) as Saldo_Inicial,
                        ESNULO((select saldo from historico_aporte hist
                        where hist.FECHA_HISTORICO = (SELECT MAX(histAN.FECHA_HISTORICO)
                        FROM historico_aporte histAN WHERE histAN.FECHA_HISTORICO
                        BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy')  AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                        and histAN.NUMERO_APORTE = a.NUMERO_APORTE) and hist.NUMERO_APORTE = a.NUMERO_APORTE),0) as Saldo_Final,                        
                        ESNULO((select sum(valor) from tran_aporte ta inner   join operacion o on o.cod_ope = ta.cod_ope
                        where ta.tipo_tran = 103 and o.estado = 1 and o.fecha_oper     BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                        and ta.NUMERO_APORTE = a.NUMERO_APORTE),0) as Intereses,
                        ESNULO((select sum(valor) from tran_aporte ta1 inner   join operacion o on o.cod_ope = ta1.cod_ope
                        where ta1.tipo_tran  in(select  tipo_tran from tipo_tran where tipo_producto=1 
                        and tipo_mov=2 and tipo_tran not in(103))  and o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                        and ta1.NUMERO_APORTE = a.NUMERO_APORTE),0) as MovimientosCreditos,
                        ESNULO((select sum(valor) from tran_aporte ta2 inner   join operacion o on o.cod_ope = ta2.cod_ope
                        where ta2.tipo_tran  in(select  tipo_tran from tipo_tran where tipo_producto=1 
                        and tipo_mov=1 and tipo_tran not in(103))  and o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                        and ta2.NUMERO_APORTE = a.NUMERO_APORTE),0) as MovimientosDebitos
                        from aporte a
                        join LINEAAPORTE l on l.COD_LINEA_APORTE = a.COD_LINEA_APORTE
                        join HISTORICO_APORTE h on h.NUMERO_APORTE = a.NUMERO_APORTE
                        join TRAN_APORTE t on t.NUMERO_APORTE = a.NUMERO_APORTE
                        where a.COD_PERSONA = " + cod_persona + "and h.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')  and l.tipo_aporte != 3
                        group by  a.numero_aporte,l.nombre)
                        select  numero_aporte, Nom_linea,  Saldo_Inicial, Saldo_Final,Intereses,MovimientosCreditos,MovimientosDebitos from suma";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Extracto entidad = new Extracto();
                            if (resultado["numero_aporte"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_aporte"]);

                            if (resultado["Nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["Nom_linea"]);
                            //if (resultado["Movimiento"] != DBNull.Value) entidad.movimiento = Convert.ToDecimal(resultado["Movimiento"]);
                            if (resultado["Saldo_Inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["Saldo_Inicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Saldo_Final"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["Saldo_Final"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Intereses"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["Intereses"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosCreditos"] != DBNull.Value) entidad.movimientoC = Convert.ToDecimal(resultado["MovimientosCreditos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosDebitos"] != DBNull.Value) entidad.movimientoD = Convert.ToDecimal(resultado["MovimientosDebitos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));

                            //    if (entidad.saldo_inicial > 0)
                            lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarExtractoAnualAporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<Extracto> ListarExtractoAnualAhorros(int cod_persona, DateTime fechaInicial, DateTime fechaFinal, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();
                   
                        // Consulta los datos de productos. Se ajustó para que el saldo inicial sea el del cierre antes del ultimo día del año. FerOrt.
                        string sql = @"select  aho.numero_cuenta, lin.DESCRIPCION as NOM_LINEA, histo.NUMERO_CUENTA,
                                          ESNULO((select hist.SALDO_TOTAL from historico_ahorro hist where hist.FECHA_HISTORICO = to_date('" + fechaInicial.AddDays(-1).ToShortDateString() + @"', 'dd/MM/yyyy') And hist.NUMERO_CUENTA = histo.NUMERO_CUENTA),0) as Saldo_Inicial,
                                          ESNULO((select hist.SALDO_TOTAL from historico_ahorro hist where hist.FECHA_HISTORICO = to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') And hist.NUMERO_CUENTA = histo.NUMERO_CUENTA),0) as Saldo_Final,
                                          ESNULO((select SUM(tr.VALOR) from operacion o join tran_ahorro tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN  where o.FECHA_OPER BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tip.TIPO_MOV = 2 and tr.ESTADO != 2 and tr.TIPO_TRAN NOT IN (207,208) and tr.NUMERO_CUENTA = histo.NUMERO_CUENTA and tr.cod_ope not in(select cod_ope from operacion_anulada)),0) as MovimientosCreditos,
                                          ESNULO((select SUM(tr.VALOR) from operacion o join tran_ahorro tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN where o.FECHA_OPER BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tip.TIPO_MOV = 1 and tr.ESTADO != 2 and tr.TIPO_TRAN NOT IN (207,208) and tr.NUMERO_CUENTA = histo.NUMERO_CUENTA and tr.cod_ope not in(select cod_ope from operacion_anulada)),0) as MovimientosDebitos,
                                          ESNULO((select SUM(tr.VALOR) from operacion o join tran_ahorro tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN where o.FECHA_OPER BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.TIPO_TRAN = 207  and o.estado=1 and tr.NUMERO_CUENTA = histo.NUMERO_CUENTA),0) as Interes,
                                          ESNULO((select SUM(tr.VALOR) from operacion o join tran_ahorro tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN where o.FECHA_OPER BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.TIPO_TRAN = 208  and o.estado=1  and tr.NUMERO_CUENTA = histo.NUMERO_CUENTA),0) as Retencion,
                                          ESNULO((select SUM(tr.VALOR) from operacion o join tran_ahorro tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN where o.FECHA_OPER BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tip.TIPO_MOV = 2 and tr.TIPO_TRAN = 203 and tr.NUMERO_CUENTA = histo.NUMERO_CUENTA),0) as Abonos
                                        from HISTORICO_AHORRO histo
                                        join ahorro_vista aho on histo.NUMERO_CUENTA = aho.NUMERO_CUENTA
                                        join LINEAAHORRO lin on lin.COD_LINEA_AHORRO = aho.COD_LINEA_AHORRO
                                        where aho.cod_persona = " + cod_persona +
                                        @" and histo.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                                        GROUP BY aho.numero_cuenta, lin.DESCRIPCION, histo.NUMERO_CUENTA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                             Extracto entidad = new Extracto();
                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_cuenta"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["Saldo_Inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["Saldo_Inicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Saldo_Final"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["Saldo_Final"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosCreditos"] != DBNull.Value) entidad.movimientoC = Convert.ToDecimal(resultado["MovimientosCreditos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Interes"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["Interes"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Retencion"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["Retencion"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Abonos"] != DBNull.Value) entidad.saldo_pagado = Convert.ToDecimal(resultado["Abonos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosDebitos"] != DBNull.Value) entidad.movimientoD = Convert.ToDecimal(resultado["MovimientosDebitos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));

                            lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarExtractoAnualAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public List<Extracto> ListarExtractoAnualCreditos(int cod_persona, DateTime fechaInicial, DateTime fechaFinal, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();

                        string sql = @"WITH sums AS 
                                    (
                                    select l.nombre as nom_linea, histo.NUMERO_RADICACION,
                                    ESNULO(NVL((select hist.SALDO_CAPITAL from historico_cre hist where hist.FECHA_HISTORICO = (SELECT MIN(histAN.FECHA_HISTORICO) FROM historico_cre histAN WHERE histAN.FECHA_HISTORICO = to_date('" + fechaInicial.AddDays(-1).ToShortDateString() + "', 'dd/MM/yyyy')"+ @"and histAN.NUMERO_RADICACION = histo.NUMERO_RADICACION) and hist.NUMERO_RADICACION = histo.NUMERO_RADICACION), 0),0) as Saldo_Inicial,
                                    ESNULO(NVL((select hist.SALDO_CAPITAL from historico_cre hist where hist.FECHA_HISTORICO = (SELECT MAX(histAN.FECHA_HISTORICO) FROM historico_cre histAN WHERE histAN.FECHA_HISTORICO = to_date('" + fechaFinal.ToShortDateString() + "', 'dd/MM/yyyy') " + @" and histAN.NUMERO_RADICACION = histo.NUMERO_RADICACION) and hist.NUMERO_RADICACION = histo.NUMERO_RADICACION), 0),0) as Saldo_Final,
                                    ESNULO(NVL((select Sum(tr.valor) from operacion o join tran_cred tr on tr.COD_OPE = o.COD_OPE join tipo_tran tt on tt.tipo_tran =tr.tipo_tran   where  tt.tipo_mov=2 and  o.fecha_oper  BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')  and tr.COD_ATR = 1 and tr.Numero_radicacion = histo.NUMERO_RADICACION), 0),0) as MovimientosCreditos,         
                                    ESNULO(NVL((select Sum(tr.valor) from operacion o join tran_cred tr on tr.COD_OPE = o.COD_OPE join tipo_tran tt on tt.tipo_tran =tr.tipo_tran   where  tt.tipo_mov=1 and  o.fecha_oper  BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')  and tr.COD_ATR = 1 and tr.Numero_radicacion = histo.NUMERO_RADICACION), 0),0) as MovimientosDebitos,                         
                                    ESNULO(NVL((select Sum(tr.valor) from operacion o join tran_cred tr on tr.COD_OPE = o.COD_OPE where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.COD_ATR = 2 and o.estado=1 and tr.tipo_tran not in(9) and tr.Numero_radicacion = histo.NUMERO_RADICACION), 0),0) as Interes_Corrientes,
                                    ESNULO(NVL((select Sum(tr.valor) from operacion o join tran_cred tr on tr.COD_OPE = o.COD_OPE where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.COD_ATR = 3  and o.estado=1 and tr.tipo_tran not in(9) and tr.Numero_radicacion = histo.NUMERO_RADICACION), 0),0) as Interes_Mora,
                                    ESNULO(NVL((select Sum(tr.valor) from operacion o join tran_cred tr on tr.COD_OPE = o.COD_OPE where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.COD_ATR NOT IN(1,2,3) and tr.Numero_radicacion = histo.NUMERO_RADICACION), 0),0) as Otros_Pagos
                                    from historico_cre histo
                                    join LINEASCREDITO l on l.COD_LINEA_CREDITO = histo.COD_LINEA_CREDITO
                                    where histo.COD_CLIENTE = " + cod_persona + @"
                                    and histo.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + "', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                                    and histo.COD_LINEA_CREDITO Not In (Select x.cod_linea_credito From parametros_linea x Where x.cod_parametro = 601)
                                    group by l.nombre, histo.NUMERO_RADICACION
                                    )
                                    SELECT NUMERO_RADICACION, nom_linea, Saldo_Inicial, Saldo_Final,MovimientosCreditos,MovimientosDebitos,Interes_Corrientes, Interes_Mora, Otros_Pagos, Saldo_Inicial - Saldo_Final as Saldo_Pagado
                                    from sums ";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;


                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Extracto entidad = new Extracto();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["Nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["Nom_linea"]);
                            if (resultado["Saldo_Inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["Saldo_Inicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig())); ;
                            if (resultado["Saldo_Final"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["Saldo_Final"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Interes_Corrientes"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["Interes_Corrientes"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Interes_Mora"] != DBNull.Value) entidad.interes_mora = Convert.ToDecimal(resultado["Interes_Mora"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Otros_Pagos"] != DBNull.Value) entidad.otros_pagos = Convert.ToDecimal(resultado["Otros_Pagos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Saldo_Pagado"] != DBNull.Value) entidad.saldo_pagado = Convert.ToDecimal(resultado["Saldo_Pagado"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosCreditos"] != DBNull.Value) entidad.movimientoC = Convert.ToDecimal(resultado["MovimientosCreditos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosDebitos"] != DBNull.Value) entidad.movimientoD = Convert.ToDecimal(resultado["MovimientosDebitos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));

                            // if (entidad.saldo_inicial > 0)
                            lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarExtractoAnualCreditos", ex);
                        return null;
                    }
                }
            }
        }


        public List<Extracto> ListarExtractoAnualCDAT(int cod_persona, DateTime fechaInicial, DateTime fechaFinal, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();
                        string sql = @"SELECT distinct(c.numero_cdat) as  numero_cdat, l.descripcion as Nom_linea, 
                        ESNULO((select hist.valor from historico_cdat hist where hist.FECHA_HISTORICO = to_date('" + fechaInicial.AddDays(-1).ToShortDateString() + @"', 'dd/MM/yyyy') And hist.numero_cdat = histo.numero_cdat),0) as Saldo_Inicial,                                                       
                        ESNULO((select sum(t.VALOR) from tran_cdat t join historico_cdat hist on t.CODIGO_CDAT = hist.CODIGO_CDAT where hist.FECHA_HISTORICO = (SELECT MAX(histAN.FECHA_HISTORICO) FROM historico_cdat histAN WHERE histAN.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and histAN.CODIGO_CDAT = d.CODIGO_CDAT) and t.TIPO_TRAN IN(302) and hist.CODIGO_CDAT = d.CODIGO_CDAT),0) as Capitalizacion,
                        ESNULO((select sum(t.VALOR) from tran_cdat t join operacion o on t.cod_ope = o.cod_ope where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and t.tipo_tran In (301) and o.estado=1 and t.codigo_cdat = c.codigo_cdat), 0) as apertura,
                        ESNULO((select sum(t.VALOR) from tran_cdat t join operacion o on t.cod_ope = o.cod_ope where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and t.tipo_tran In (302, 305) and o.estado=1 and t.codigo_cdat = c.codigo_cdat), 0) as Movimiento,
                        ESNULO((select sum(t.VALOR) from tran_cdat t join operacion o on t.cod_ope = o.cod_ope where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and t.tipo_tran In (303, 307, 312) and o.estado=1 and  t.codigo_cdat = c.codigo_cdat), 0) as Intereses,
                        ESNULO((select sum(t.VALOR) from tran_cdat t join operacion o on t.cod_ope = o.cod_ope where o.fecha_oper BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and t.tipo_tran In (304) and o.estado=1 and  t.codigo_cdat = c.codigo_cdat), 0)      as Retencion,        
                        0 As Saldo_Final
                        FROM HISTORICO_cdat    histo    
                        JOIN cdat c  ON  histo.codigo_cdat = c.codigo_cdat     
                        JOIN LINEACDAT l on c.cod_lineacdat = l.cod_lineacdat
                        JOIN cdat_titular d ON c.codigo_cdat = d.codigo_cdat 
                        WHERE  d.principal=1 and c.estado not in(4) and d.cod_persona = " + cod_persona /*+ " AND c.FECHA_APERTURA >= to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') " */ + " AND c.FECHA_APERTURA <= to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')";                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Extracto entidad = new Extracto();
                            if (resultado["numero_cdat"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_cdat"]);
                            if (resultado["Nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["Nom_linea"]);
                            if (resultado["Saldo_Inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["Saldo_Inicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Saldo_Final"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["Saldo_Final"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Movimiento"] != DBNull.Value) entidad.movimiento = Convert.ToDecimal(resultado["Movimiento"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig())); ;
                            if (resultado["Intereses"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["Intereses"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Retencion"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["Retencion"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["apertura"] != DBNull.Value) entidad.apertura = Convert.ToDecimal(resultado["apertura"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));

                            // Calcular el saldo final
                            entidad.saldo_final = entidad.saldo_inicial - entidad.movimiento;

                            if (entidad.saldo_inicial == 0)
                            {
                                entidad.saldo_inicial = entidad.apertura;

                            }
                            entidad.saldo_final = entidad.saldo_inicial - entidad.movimiento;

                            if (entidad.saldo_inicial > 0)
                                lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarExtractoAnualCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public List<Extracto> ListarExtractoAnualProgramado(int cod_persona, DateTime fechaInicial, DateTime fechaFinal, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();

                        string sql = @"select aho.numero_programado as numero_programado, lin.NOMBRE as NOM_LINEA, histo.numero_programado,
                                        ESNULO((select hist.SALDO_TOTAL from historico_programado hist where hist.FECHA_HISTORICO = (SELECT MIN(histAN.FECHA_HISTORICO) 
                                        FROM historico_programado histAN WHERE histAN.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and histAN.numero_programado = histo.numero_programado) and hist.numero_programado = histo.numero_programado),0) as Saldo_Inicial,
                                        ESNULO((select hist.SALDO_TOTAL from historico_programado hist where hist.FECHA_HISTORICO = (SELECT MAX(histAN.FECHA_HISTORICO) 
                                        FROM historico_programado histAN WHERE histAN.FECHA_HISTORICO BETWEEN to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and histAN.numero_programado = histo.numero_programado) and hist.numero_programado = histo.numero_programado),0) as Saldo_Final,
                                        ESNULO((select SUM(tr.VALOR) from operacion o join tran_programado tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN 
                                        where o.FECHA_OPER BETWEEN  to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tip.TIPO_MOV = 2  and tr.TIPO_TRAN NOT IN (353,354,355,356,357) and tr.numero_programado = histo.numero_programado),0) as MovimientosCreditos,
                                         ESNULO((select SUM(tr.VALOR) from operacion o join tran_programado tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN 
                                        where o.FECHA_OPER BETWEEN  to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tip.TIPO_MOV = 1 and tr.TIPO_TRAN NOT IN (353,354,355,356,357) and tr.numero_programado = histo.numero_programado),0) as MovimientosDebitos,
                                     
                                        ESNULO((select SUM(tr.VALOR) from operacion o join tran_programado tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN 
                                        where o.FECHA_OPER BETWEEN  to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.TIPO_TRAN = 353  and o.estado=1  and tr.numero_programado = histo.numero_programado),0) as Interes,
                                        ESNULO((select SUM(tr.VALOR) from operacion o join tran_programado tr on tr.COD_OPE = o.COD_OPE join tipo_tran tip on tr.TIPO_TRAN = tip.TIPO_TRAN 
                                        where o.FECHA_OPER BETWEEN  to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy') and tr.TIPO_TRAN = 354  and o.estado=1 and tr.numero_programado = histo.numero_programado),0) as Retencion
                                        from historico_programado histo
                                        join ahorro_programado aho on histo.numero_programado = aho.numero_programado
                                        join lineaprogramado lin on lin.COD_LINEA_PROGRAMADO = aho.COD_LINEA_PROGRAMADO
                                        where aho.cod_persona = " + cod_persona +" and histo.FECHA_HISTORICO BETWEEN  to_date('" + fechaInicial.ToShortDateString() + @"', 'dd/MM/yyyy') AND to_date('" + fechaFinal.ToShortDateString() + @"', 'dd/MM/yyyy')
                                        GROUP BY aho.numero_programado, lin.NOMBRE, histo.numero_programado ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Extracto entidad = new Extracto();
                            if (resultado["numero_programado"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_programado"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["Saldo_Inicial"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["Saldo_Inicial"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Saldo_Final"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["Saldo_Final"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosCreditos"] != DBNull.Value) entidad.movimientoC = Convert.ToDecimal(resultado["MovimientosCreditos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Interes"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["Interes"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["Retencion"] != DBNull.Value) entidad.retencion = Convert.ToDecimal(resultado["Retencion"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["MovimientosDebitos"] != DBNull.Value) entidad.movimientoD = Convert.ToDecimal(resultado["MovimientosDebitos"].ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));

                            // if (entidad.saldo_inicial > 0)
                            lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarExtractoAnualAhorros", ex);
                        return null;
                    }
                }
            }
        }


        public List<Extracto> ListarDetalleExtracto(Int64 cod_pesona, DateTime pFechaPago, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbDataReader resultado = default(DbDataReader);
                        List<Extracto> lstExtracto = new List<Extracto>();
                        Configuracion global = new Configuracion();

                        string sql = @"Select Tipo_Producto, Numero_Radicacion, valor_cuota, Nombre, Fecha_Proximo_Pago, CodTipoProducto,
                                        Case CodTipoProducto 
                                            When 1 Then Calcular_VrAPagarAporte(Numero_Radicacion , To_Date('" + pFechaPago.ToString(global.ObtenerFormatoFecha()) + "', '" + global.ObtenerFormatoFecha() + @"'))
                                            When 2 Then Calcular_Vrapagar(Numero_Radicacion, To_Date('" + pFechaPago.ToString(global.ObtenerFormatoFecha()) + "', '" + global.ObtenerFormatoFecha() + @"')) 
                                        End As Total_A_Pagar "
                                     + "From Vextractoproductos where Cod_Deudor = " + cod_pesona.ToString() + " order by Tipo_Producto, Numero_Radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Extracto entidad = new Extracto();
                            if (resultado["CodTipoProducto"] != DBNull.Value) entidad.codtipo_producto = Convert.ToInt64(resultado["CodTipoProducto"]);
                            if (resultado["Tipo_Producto"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["Tipo_Producto"]);
                            if (resultado["Numero_Radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["Numero_Radicacion"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["Nombre"]);
                            if (resultado["Fecha_Proximo_Pago"] != DBNull.Value) entidad.fec_prox_pago = Convert.ToDateTime(resultado["Fecha_Proximo_Pago"]);
                            if (resultado["Total_A_Pagar"] != DBNull.Value) entidad.vr_apagar = Convert.ToDecimal(resultado["Total_A_Pagar"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.vr_totalPagar_aporte = Convert.ToDecimal(resultado["valor_cuota"]);
                            if (entidad.codtipo_producto != 2 || entidad.vr_apagar != 0)
                                lstExtracto.Add(entidad);
                        }
                        return lstExtracto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoData", "ListarDetalleExtracto", ex);
                        return null;
                    }
                }
            }
        }


    }
}