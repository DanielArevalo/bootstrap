using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentas
    /// </summary>    
    public class LibroTercerosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public LibroTercerosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroTerceros> ListarAuxiliarTerceros(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            List<LibroTerceros> lstControl = new List<LibroTerceros>();
            string lcod_cuenta = "";
            string lnomcue = "";
            Int64? lcodigo = null;
            string ltipocue = "";
            string lidentificacion = "";
            string lnombre = "";
            string ldepende_de = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "";
                        sql = "Select d.cod_cuenta, d.nomcue, d.naturaleza, d.tercero, d.identificacion, d.tipo_iden, t.descripcion, d.nom_tercero, d.tipo_persona, '' as Regimen, e.fecha, e.num_comp, e.tipo_comp, d.detalle, d.tipo, d.valor, 0 As saldo, e.concepto, 'P' As tipo_benef, e.N_DOCUMENTO As num_sop, d.centro_costo,d.depende_de " +
                                    " From e_comprobante e, d_comprobante d Left Join tipoidentificacion t On t.codtipoidentificacion = d.tipo_iden" +
                                    " Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N' And d.maneja_ter = 1 And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";
                        if (CodCueIni.Trim() != "")
                            sql = sql + " And d.idcuenta >= " + CodCueIni + Repetir("0", 10-CodCueIni.Length);
                        if (CodCueFin.Trim() != "")
                            sql = sql + " And d.idcuenta <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        if (IdenIni.Trim() != "")
                            sql = sql + " And d.identificacion >= '" + IdenIni + "' ";
                        if (IdenFin.Trim() != "")
                            sql = sql + " And d.identificacion <= '" + IdenFin + "' ";
                        if (CenIni.ToString() != "" && CenFin.ToString() != "" && CenIni == CenFin)
                        {
	                        sql = sql + " And d.centro_costo = " + CenIni;
                        }
                        else
                        {
                            if (CenIni.ToString() != "")
		                        sql = sql + " And d.centro_costo >= " + CenIni;
                            if (CenFin.ToString() != "")
                                sql = sql + " And d.centro_costo <= " + CenFin;
                        }
                        
                        if (bCuenta == true)
                            sql = sql + " Order by d.cod_cuenta, d.tercero, e.fecha";
                        else
                            sql = sql + " Order by d.tercero, d.cod_cuenta, e.fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Int64? tercero = null;                        
                        string cuenta = "";
                        string nombrecuenta = "";
                        string tipocuenta = "";
                        decimal SaldoAnt = 0;
                        string depende_de = "";

                        while (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) lnomcue = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) ltipocue = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["TERCERO"] != DBNull.Value) lcodigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) lidentificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) lnombre = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) ldepende_de = Convert.ToString(resultado["DEPENDE_DE"]);

                            // Insertando registro para mostrar el saldo inicial
                            if (lcod_cuenta != cuenta || lcodigo != tercero)
                            {
                                cuenta = lcod_cuenta;
                                nombrecuenta = lnomcue;
                                tipocuenta = ltipocue;
                                tercero = lcodigo;
                                LibroTerceros entidadini = new LibroTerceros();
                                entidadini.codigo = tercero;
                                entidadini.identificacion = lidentificacion;
                                entidadini.nombre = lnombre;
                                entidadini.cod_cuenta = cuenta;
                                entidadini.nombre_cuenta = lnomcue;
                                entidadini.tipo = ltipocue;
                                entidadini.fecha = null;
                                SaldoAnt = SaldoCuenta(entidadini.cod_cuenta, Convert.ToInt64(tercero), FecIni, CenIni, CenFin, vUsuario);
                                entidadini.saldo = SaldoAnt;
                                entidadini.valor = SaldoAnt;
                                if (bCuenta == true)
                                    entidadini.detalle = "SALDO INICIAL " + cuenta;
                                else
                                    entidadini.detalle = "SALDO INICIAL ";

                                entidadini.depende_de = ldepende_de;

                                lstLibAux.Add(entidadini);
                                lstControl.Add(entidadini);
                            }

                            LibroTerceros entidad = new LibroTerceros();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDEN"] != DBNull.Value) entidad.tipo_iden = Convert.ToInt32(resultado["TIPO_IDEN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_iden = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["REGIMEN"] != DBNull.Value) entidad.regimen = Convert.ToString(resultado["REGIMEN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["TIPO_BENEF"] != DBNull.Value) entidad.tipo_benef = Convert.ToString(resultado["TIPO_BENEF"]);
                            if (resultado["NUM_SOP"] != DBNull.Value) entidad.num_sop = Convert.ToString(resultado["NUM_SOP"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);

                            if (entidad.tipo == "Débito" || entidad.tipo == "D")
                            {
                                if (entidad.tipo_mov == "D")
                                    entidad.saldo = SaldoAnt + entidad.valor;
                                else
                                    entidad.saldo = SaldoAnt - entidad.valor;
                            }
                            else
                            {
                                if (entidad.tipo_mov == "D")
                                    entidad.saldo = SaldoAnt - entidad.valor;
                                else
                                    entidad.saldo = SaldoAnt + entidad.valor;
                            }
                            SaldoAnt = Convert.ToDecimal(entidad.saldo);


                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);


                            lstLibAux.Add(entidad);
                        }

                        // Insertar cuentas y terceros que no tuvieron movimiento en el período
                        string condicionfecha = "";
                        DateTime? fechabal = UltimaFechaBalanceTer(FecIni, vUsuario);
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {                            
                            if (fechabal != null)
                                condicionfecha = " And b.fecha = To_Date('" + Convert.ToDateTime(fechabal).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            sql = @"Select Distinct p.cod_cuenta, p.nombre As nomcue, p.tipo, b.cod_ter As tercero, Case tipo_persona When 'N' Then Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)) Else razon_social End As nom_tercero, c.identificacion,p.depende_de
                                From plan_cuentas p Left Join balance_ter b On b.cod_cuenta = p.cod_cuenta " + condicionfecha + @" And (b.saldo_ini != 0 Or b.saldo_fin != 0)
                                Left Join v_persona c On b.cod_ter = c.cod_persona
                                Where p.maneja_ter = 1 ";
                            if (CodCueIni.Trim() != "")
                                sql = sql + " And Power(10, 10-length(p.cod_cuenta))*to_number(p.cod_cuenta) >= " + CodCueIni + Repetir("0", 10 - CodCueIni.Length);
                            if (CodCueFin.Trim() != "")
                                sql = sql + " And Power(10, 10-length(p.cod_cuenta))*to_number(p.cod_cuenta) <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        }
                        else
                        {
                            if (fechabal != null)
                                condicionfecha = " And b.fecha = '" + Convert.ToDateTime(fechabal).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql = @"Select Distinct p.cod_cuenta, p.nombre As nomcue, p.tipo, b.cod_ter As tercero, Decode(tipo_persona, 'N', Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)), razon_social) As nom_tercero, c.identificacion,p.depende_de
                                From plan_cuentas p Left Join balance_ter b On b.cod_cuenta = p.cod_cuenta " + condicionfecha + @" And (b.saldo_ini != 0 Or b.saldo_fin != 0)
                                Left Join v_persona c On b.cod_ter = c.cod_persona
                                Where p.maneja_ter = 1 ";
                            if (CodCueIni.Trim() != "")
                                sql = sql + " And p.cod_cuenta >= " + CodCueIni + Repetir("0", 10 - CodCueIni.Length);
                            if (CodCueFin.Trim() != "")
                                sql = sql + " And p.cod_cuenta <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        }
                        if (IdenIni.Trim() != "")
                            sql = sql + " And c.identificacion >= '" + IdenIni + "' ";
                        if (IdenFin.Trim() != "")
                            sql = sql + " And c.identificacion <= '" + IdenFin + "' ";
                        if (CenIni.ToString() != "" && CenFin.ToString() != "" && CenIni == CenFin)
                        {
                            sql = sql + " And b.centro_costo = " + CenIni;
                        }
                        else
                        {
                            if (CenIni.ToString() != "")
                                sql = sql + " And b.centro_costo >= " + CenIni;
                            if (CenFin.ToString() != "")
                                sql = sql + " And b.centro_costo <= " + CenFin;
                        }

                        if (bCuenta == true)
                            sql = sql + " Order by p.cod_cuenta, b.cod_ter";
                        else
                            sql = sql + " Order by b.cod_ter, p.cod_cuenta";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUE"] != DBNull.Value) lnomcue = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["TIPO"] != DBNull.Value) ltipocue = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TERCERO"] != DBNull.Value) lcodigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) lidentificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) lnombre = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) ldepende_de = Convert.ToString(resultado["DEPENDE_DE"]);

                            bool bTieneMovimientos = false;
                            foreach (LibroTerceros liber in lstControl)
                            {
                                if (liber.cod_cuenta == lcod_cuenta && liber.codigo == lcodigo)
                                    bTieneMovimientos = true;
                            }
                            if (bTieneMovimientos == false)
                            {
                                cuenta = lcod_cuenta;
                                nombrecuenta = lnomcue;
                                tipocuenta = ltipocue;
                                tercero = lcodigo;
                                LibroTerceros entidadini = new LibroTerceros();
                                entidadini.codigo = tercero;
                                entidadini.identificacion = lidentificacion;
                                entidadini.nombre = lnombre;
                                entidadini.cod_cuenta = cuenta;
                                entidadini.nombre_cuenta = lnomcue;
                                entidadini.tipo = ltipocue;
                                entidadini.fecha = null;
                                SaldoAnt = SaldoCuenta(entidadini.cod_cuenta, Convert.ToInt64(tercero), FecIni, CenIni, CenFin, vUsuario);
                                entidadini.saldo = SaldoAnt;
                                entidadini.valor = SaldoAnt;
                                if (bCuenta == true)
                                    entidadini.detalle = "SALDO INICIAL " + cuenta;
                                else
                                    entidadini.detalle = "SALDO INICIAL ";

                                entidadini.depende_de = ldepende_de;
                                lstLibAux.Add(entidadini);
                            }
                        }
            
                        connection.Close();

                        return lstLibAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroTercerosData", "ListarAuxiliarTerceros", ex);
                        return null;
                    }
                }
            }
        }

        private string Repetir(string sLetra, int nveces)
        {
            string scadena = "";
            for (int i = 1; i <= nveces; i++)
            {
                scadena = scadena + sLetra;
            }
            return scadena;
        }



        public List<LibroTerceros> ListarAuxiliarTercerosNIIF(string CodCueIni, string CodCueFin, string IdenIni, string IdenFin, Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroTerceros> lstLibAux = new List<LibroTerceros>();
            List<LibroTerceros> lstControl = new List<LibroTerceros>();
            string lcod_cuenta = "";
            string lnomcue = "";
            Int64? lcodigo = null;
            string ltipocue = "";
            string lidentificacion = "";
            string lnombre = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "";
                        sql = "Select D.COD_CUENTA_NIIF, D.NOMCUE_NIIF, d.naturaleza, d.tercero, d.identificacion, d.tipo_iden, t.descripcion, d.nom_tercero, d.tipo_persona, '' as Regimen, e.fecha, e.num_comp, e.tipo_comp, d.detalle, d.tipo, d.valor, 0 As saldo, e.concepto, 'P' As tipo_benef, e.N_DOCUMENTO As num_sop, d.centro_costo " +
                                    " From e_comprobante e, d_comprobante d Left Join tipoidentificacion t On t.codtipoidentificacion = d.tipo_iden" +
                                    " Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N' And d.maneja_ter_niif = 1 And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";
                        if (CodCueIni.Trim() != "")
                            sql = sql + " And d.idcuenta_niif >= " + CodCueIni + Repetir("0", 10 - CodCueIni.Length);
                        if (CodCueFin.Trim() != "")
                            sql = sql + " And d.idcuenta_niif <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        if (IdenIni.Trim() != "")
                            sql = sql + " And d.identificacion >= '" + IdenIni + "' ";
                        if (IdenFin.Trim() != "")
                            sql = sql + " And d.identificacion <= '" + IdenFin + "' ";
                        if (CenIni.ToString() != "" && CenFin.ToString() != "" && CenIni == CenFin)
                        {
                            sql = sql + " And d.centro_costo = " + CenIni;
                        }
                        else
                        {
                            if (CenIni.ToString() != "")
                                sql = sql + " And d.centro_costo >= " + CenIni;
                            if (CenFin.ToString() != "")
                                sql = sql + " And d.centro_costo <= " + CenFin;
                        }

                        if (bCuenta == true)
                            sql = sql + " Order by D.COD_CUENTA_NIIF, d.tercero, e.fecha";
                        else
                            sql = sql + " Order by d.tercero, D.COD_CUENTA_NIIF, e.fecha";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Int64? tercero = null;
                        string cuenta = "";
                        string nombrecuenta = "";
                        string tipocuenta = "";
                        decimal SaldoAnt = 0;

                        while (resultado.Read())
                        {
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE_NIIF"] != DBNull.Value) lnomcue = Convert.ToString(resultado["NOMCUE_NIIF"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) ltipocue = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["TERCERO"] != DBNull.Value) lcodigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) lidentificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) lnombre = Convert.ToString(resultado["NOM_TERCERO"]);

                            // Insertando registro para mostrar el saldo inicial
                            if (lcod_cuenta != cuenta || lcodigo != tercero)
                            {
                                cuenta = lcod_cuenta;
                                nombrecuenta = lnomcue;
                                tipocuenta = ltipocue;
                                tercero = lcodigo;
                                LibroTerceros entidadini = new LibroTerceros();
                                entidadini.codigo = tercero;
                                entidadini.identificacion = lidentificacion;
                                entidadini.nombre = lnombre;
                                entidadini.cod_cuenta = cuenta;
                                entidadini.nombre_cuenta = lnomcue;
                                entidadini.tipo = ltipocue;
                                entidadini.fecha = null;
                                SaldoAnt = SaldoCuentaNIIF(entidadini.cod_cuenta, Convert.ToInt64(tercero), FecIni, CenIni, CenFin, vUsuario);
                                entidadini.saldo = SaldoAnt;
                                entidadini.valor = SaldoAnt;
                                if (bCuenta == true)
                                    entidadini.detalle = "SALDO INICIAL " + cuenta;
                                else
                                    entidadini.detalle = "SALDO INICIAL ";
                                lstLibAux.Add(entidadini);
                                lstControl.Add(entidadini);
                            }

                            LibroTerceros entidad = new LibroTerceros();
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE_NIIF"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMCUE_NIIF"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDEN"] != DBNull.Value) entidad.tipo_iden = Convert.ToInt32(resultado["TIPO_IDEN"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_iden = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["REGIMEN"] != DBNull.Value) entidad.regimen = Convert.ToString(resultado["REGIMEN"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["TIPO_BENEF"] != DBNull.Value) entidad.tipo_benef = Convert.ToString(resultado["TIPO_BENEF"]);
                            if (resultado["NUM_SOP"] != DBNull.Value) entidad.num_sop = Convert.ToString(resultado["NUM_SOP"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);

                            if (entidad.tipo == "Débito" || entidad.tipo == "D")
                            {
                                if (entidad.tipo_mov == "D")
                                    entidad.saldo = SaldoAnt + entidad.valor;
                                else
                                    entidad.saldo = SaldoAnt - entidad.valor;
                            }
                            else
                            {
                                if (entidad.tipo_mov == "D")
                                    entidad.saldo = SaldoAnt - entidad.valor;
                                else
                                    entidad.saldo = SaldoAnt + entidad.valor;
                            }
                            SaldoAnt = Convert.ToDecimal(entidad.saldo);

                            lstLibAux.Add(entidad);
                        }

                        // Insertar cuentas y terceros que no tuvieron movimiento en el período
                        string condicionfecha = "";
                        DateTime? fechabal = UltimaFechaBalanceTer(FecIni, vUsuario);
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            if (fechabal != null)
                                condicionfecha = " And b.fecha = To_Date('" + Convert.ToDateTime(fechabal).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            sql = @"Select Distinct P.COD_CUENTA_NIIF, p.nombre As nomcue, p.tipo, b.cod_ter As tercero, Case tipo_persona When 'N' Then Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)) Else razon_social End As nom_tercero, c.identificacion
                                From PLAN_CUENTAS_NIIF p Left Join BALANCE_TER_NIIF b On b.COD_CUENTA_NIIF = p.COD_CUENTA_NIIF " + condicionfecha + @" And (b.saldo_ini != 0 Or b.saldo_fin != 0)
                                Left Join v_persona c On b.cod_ter = c.cod_persona
                                Where p.maneja_ter = 1 ";
                            if (CodCueIni.Trim() != "")
                                sql = sql + " And Power(10, 10-length(p.COD_CUENTA_NIIF))*to_number(p.COD_CUENTA_NIIF) >= " + CodCueIni + Repetir("0", 10 - CodCueIni.Length);
                            if (CodCueFin.Trim() != "")
                                sql = sql + " And Power(10, 10-length(p.COD_CUENTA_NIIF))*to_number(p.COD_CUENTA_NIIF) <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        }
                        else
                        {
                            if (fechabal != null)
                                condicionfecha = " And b.fecha = '" + Convert.ToDateTime(fechabal).ToString(conf.ObtenerFormatoFecha()) + "' ";
                            sql = @"Select Distinct P.COD_CUENTA_NIIF, p.nombre As nomcue, p.tipo, b.cod_ter As tercero, Decode(tipo_persona, 'N', Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' || primer_apellido || ' ' || segundo_apellido, 0, 240)), razon_social) As nom_tercero, c.identificacion
                                From PLAN_CUENTAS_NIIF p Left Join BALANCE_TER_NIIF b On b.COD_CUENTA_NIIF = p.COD_CUENTA_NIIF " + condicionfecha + @" And (b.saldo_ini != 0 Or b.saldo_fin != 0)
                                Left Join v_persona c On b.cod_ter = c.cod_persona
                                Where p.maneja_ter = 1 ";
                            if (CodCueIni.Trim() != "")
                                sql = sql + " And p.COD_CUENTA_NIIF >= " + CodCueIni + Repetir("0", 10 - CodCueIni.Length);
                            if (CodCueFin.Trim() != "")
                                sql = sql + " And p.COD_CUENTA_NIIF <= " + CodCueFin + Repetir("9", 10 - CodCueFin.Length);
                        }
                        if (IdenIni.Trim() != "")
                            sql = sql + " And c.identificacion >= '" + IdenIni + "' ";
                        if (IdenFin.Trim() != "")
                            sql = sql + " And c.identificacion <= '" + IdenFin + "' ";
                        if (CenIni.ToString() != "" && CenFin.ToString() != "" && CenIni == CenFin)
                        {
                            sql = sql + " And b.centro_costo = " + CenIni;
                        }
                        else
                        {
                            if (CenIni.ToString() != "")
                                sql = sql + " And b.centro_costo >= " + CenIni;
                            if (CenFin.ToString() != "")
                                sql = sql + " And b.centro_costo <= " + CenFin;
                        }

                        if (bCuenta == true)
                            sql = sql + " Order by p.COD_CUENTA_NIIF, b.cod_ter";
                        else
                            sql = sql + " Order by b.cod_ter, p.COD_CUENTA_NIIF";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            lcod_cuenta = "";
                            lcodigo = null;
                            lidentificacion = "";
                            lnombre = "";
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["NOMCUE"] != DBNull.Value) lnomcue = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["TIPO"] != DBNull.Value) ltipocue = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TERCERO"] != DBNull.Value) lcodigo = Convert.ToInt64(resultado["TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) lidentificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) lnombre = Convert.ToString(resultado["NOM_TERCERO"]);
                            bool bTieneMovimientos = false;
                            foreach (LibroTerceros liber in lstControl)
                            {
                                if (liber.cod_cuenta == lcod_cuenta && liber.codigo == lcodigo)
                                    bTieneMovimientos = true;
                            }
                            if (bTieneMovimientos == false)
                            {
                                cuenta = lcod_cuenta;
                                nombrecuenta = lnomcue;
                                tipocuenta = ltipocue;
                                tercero = lcodigo;
                                LibroTerceros entidadini = new LibroTerceros();
                                entidadini.codigo = tercero;
                                entidadini.identificacion = lidentificacion;
                                entidadini.nombre = lnombre;
                                entidadini.cod_cuenta = cuenta;
                                entidadini.nombre_cuenta = lnomcue;
                                entidadini.tipo = ltipocue;
                                entidadini.fecha = null;
                                SaldoAnt = SaldoCuentaNIIF(entidadini.cod_cuenta, Convert.ToInt64(tercero), FecIni, CenIni, CenFin, vUsuario);
                                entidadini.saldo = SaldoAnt;
                                entidadini.valor = SaldoAnt;
                                if (bCuenta == true)
                                    entidadini.detalle = "SALDO INICIAL " + cuenta;
                                else
                                    entidadini.detalle = "SALDO INICIAL ";
                                lstLibAux.Add(entidadini);
                            }
                        }

                        connection.Close();

                        return lstLibAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroTercerosData", "ListarAuxiliarTercerosNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el plan de cuentas
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Tercero> ListarTerceros(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Tercero> lstTercero = new List<Tercero>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = "Select * from v_persona Order by cod_persona";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Tercero entidad = new Tercero();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstTercero.Add(entidad);
                        }

                        return lstTercero;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroTercerosData", "ListarTerceros", ex);
                        return null;
                    }
                }
            }
        }

        public List<PlanCuentas> ListarPlanCuentas(Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PlanCuentas> lstPlanCuentas = new List<PlanCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (string.Equals(filtro, "AUXILIARES"))
                        {
                            sql = "Select * from plan_cuentas Where plan_cuentas.cod_cuenta Not In (Select x.depende_de From plan_cuentas x Where x.depende_de = plan_cuentas.cod_cuenta) Order by plan_cuentas.cod_cuenta";
                        }
                        else
                        {
                            sql = "Select * from plan_cuentas " + filtro;
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PlanCuentas entidad = new PlanCuentas();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["MANEJA_CC"] != DBNull.Value) entidad.maneja_cc = Convert.ToInt64(resultado["MANEJA_CC"]);
                            if (resultado["MANEJA_TER"] != DBNull.Value) entidad.maneja_ter = Convert.ToInt64(resultado["MANEJA_TER"]);
                            if (resultado["MANEJA_SC"] != DBNull.Value) entidad.maneja_sc = Convert.ToInt64(resultado["MANEJA_SC"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["ESTADO"]);
                            if (resultado["IMPUESTO"] != DBNull.Value) entidad.impuesto = Convert.ToInt64(resultado["IMPUESTO"]);
                            lstPlanCuentas.Add(entidad);
                        }

                        return lstPlanCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroTercerosData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Determinar la fecha de cierre inicial para un tipo de cierre dato
        /// </summary>
        /// <param name="ptipocierre">el tipo de cierre a verificar</param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string ptipocierre, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecha_cierre = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Min(fecha) As fecha From cierea Where tipo = '" + ptipocierre + "' And estado = 'D' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        return fecha_cierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroTercerosData", "CargarCuentas", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }


        public decimal SaldoCuenta(string cod_cuenta, Int64 cod_tercero, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
        {
            decimal saldo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "pcod_cuenta";
                        pCOD_CUENTA.Value = cod_cuenta;
                        pCOD_CUENTA.Direction = ParameterDirection.Input;

                        DbParameter pCODTER = cmdTransaccionFactory.CreateParameter();
                        pCODTER.ParameterName = "pcodter";
                        pCODTER.Value = cod_tercero;
                        pCODTER.Direction = ParameterDirection.Input;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "pfecha";
                        pFECHA.Value = fecha;
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pCENINI = cmdTransaccionFactory.CreateParameter();
                        pCENINI.ParameterName = "pcenini";
                        pCENINI.Value = cenini;
                        pCENINI.DbType = DbType.Int64;
                        pCENINI.Direction = ParameterDirection.Input;

                        DbParameter pCENFIN = cmdTransaccionFactory.CreateParameter();
                        pCENFIN.ParameterName = "pcenfin";
                        pCENFIN.Value = cenfin;
                        pCENFIN.DbType = DbType.Int64;
                        pCENFIN.Direction = ParameterDirection.Input;

                        DbParameter pSALDO = cmdTransaccionFactory.CreateParameter();
                        pSALDO.ParameterName = "psaldo";
                        pSALDO.Value = 0;
                        pSALDO.DbType = DbType.Decimal;
                        pSALDO.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pCODTER);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCENINI);
                        cmdTransaccionFactory.Parameters.Add(pCENFIN);
                        cmdTransaccionFactory.Parameters.Add(pSALDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOCUENTATER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        saldo = Convert.ToDecimal(pSALDO.Value);

                        return saldo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "SaldoCuenta", ex);
                        return 0;
                    }
                }
            }
        }


        public decimal SaldoCuentaNIIF(string cod_cuenta_niif, Int64 cod_tercero, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
        {
            decimal saldo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_CUENTA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CUENTA.ParameterName = "pcod_cuenta";
                        pCOD_CUENTA.Value = cod_cuenta_niif;
                        pCOD_CUENTA.Direction = ParameterDirection.Input;

                        DbParameter pCODTER = cmdTransaccionFactory.CreateParameter();
                        pCODTER.ParameterName = "pcodter";
                        pCODTER.Value = cod_tercero;
                        pCODTER.Direction = ParameterDirection.Input;

                        DbParameter pFECHA = cmdTransaccionFactory.CreateParameter();
                        pFECHA.ParameterName = "pfecha";
                        pFECHA.Value = fecha;
                        pFECHA.DbType = DbType.Date;
                        pFECHA.Direction = ParameterDirection.Input;

                        DbParameter pCENINI = cmdTransaccionFactory.CreateParameter();
                        pCENINI.ParameterName = "pcenini";
                        pCENINI.Value = cenini;
                        pCENINI.DbType = DbType.Int64;
                        pCENINI.Direction = ParameterDirection.Input;

                        DbParameter pCENFIN = cmdTransaccionFactory.CreateParameter();
                        pCENFIN.ParameterName = "pcenfin";
                        pCENFIN.Value = cenfin;
                        pCENFIN.DbType = DbType.Int64;
                        pCENFIN.Direction = ParameterDirection.Input;

                        DbParameter pSALDO = cmdTransaccionFactory.CreateParameter();
                        pSALDO.ParameterName = "psaldo";
                        pSALDO.Value = 0;
                        pSALDO.DbType = DbType.Decimal;
                        pSALDO.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                        cmdTransaccionFactory.Parameters.Add(pCODTER);
                        cmdTransaccionFactory.Parameters.Add(pFECHA);
                        cmdTransaccionFactory.Parameters.Add(pCENINI);
                        cmdTransaccionFactory.Parameters.Add(pCENFIN);
                        cmdTransaccionFactory.Parameters.Add(pSALDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_SALDOCUENTATER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        saldo = Convert.ToDecimal(pSALDO.Value);

                        return saldo;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "SaldoCuentaNIIF", ex);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// Determinar fecha del último balance por terceros
        /// </summary>
        /// <param name="ptipocierre"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public DateTime? UltimaFechaBalanceTer(DateTime? pFecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime? fecha_cierre = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select max(x.fecha) From balance_ter x";
                        if (pFecha != null)
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " Where x.fecha <= To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            else
                                sql += " Where x.fecha <= '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        return fecha_cierre;
                    }
                    catch 
                    {
                        return fecha_cierre;
                    }
                }
            }
        }


    }
}
