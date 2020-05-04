using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Data
{
   
    public class LibroAuxiliarNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PERSONA
        /// </summary>
        public LibroAuxiliarNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


       public List<LibroAuxiliar> ListarAuxiliar(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, string sCuentas,Int32 moneda, string pOrdenar, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DbDataReader resultadoComp = default(DbDataReader);
            List<LibroAuxiliar> lstLibAux = new List<LibroAuxiliar>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();                        

                        string sCuentasPlan = sCuentas.Replace("d.", "dd.");
                        string sql = "";
                        sql = @"Select dd.cod_cuenta_niif, dd.nombre As nomcue, dd.tipo As naturaleza, dd.depende_de, dd.base_minima, dd.porcentaje_impuesto
                                    From plan_cuentas_niif dd 
                                    Where " + sCuentasPlan + @" And dd.cod_cuenta_niif Not In (Select p.depende_de From plan_cuentas_niif p Where p.depende_de = dd.cod_cuenta_niif) 
                                    And dd.cod_moneda = " + moneda.ToString() + " Order by dd.cod_cuenta_niif";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        string lcod_cuenta = "";
                        string lnombrecuenta = "";
                        string lnaturaleza = "";
                        string ldepende_de = "";
                        decimal lbase_minima = 0;
                        decimal lporcentaje_impuesto = 0;

                        while (resultado.Read())
                        {
                            if (resultado["cod_cuenta_niif"] != DBNull.Value) lcod_cuenta = Convert.ToString(resultado["cod_cuenta_niif"]);
                            if (resultado["NOMCUE"] != DBNull.Value) lnombrecuenta = Convert.ToString(resultado["NOMCUE"]);
                            if (resultado["NATURALEZA"] != DBNull.Value) lnaturaleza = Convert.ToString(resultado["NATURALEZA"]);
                            if (resultado["DEPENDE_DE"] != DBNull.Value) ldepende_de = Convert.ToString(resultado["DEPENDE_DE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) lbase_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) lporcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);

                            LibroAuxiliar entidadini = new LibroAuxiliar();
                            entidadini.fecha = null;
                            entidadini.cod_cuenta = lcod_cuenta;
                            entidadini.nombrecuenta = lnombrecuenta;
                            entidadini.naturaleza = lnaturaleza;
                            entidadini.depende_de = ldepende_de;
                            entidadini.detalle = "SALDO INICIAL";
                            entidadini.saldo = 0;  // El saldo se calcula en capa negocio
                            lstLibAux.Add(entidadini);

                            string sqlT = "";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                // Consultar datos de comprobantes                                
                                sqlT = @"Select e.fecha, e.num_comp, e.tipo_comp, e.n_documento, e.num_consig, d.detalle, d.tipo, d.valor, e.concepto, e.tipo_benef, 
                                            e.iden_benef, e.tipo_iden, e.nombre, d.tercero, d.identificacion, d.tipo_iden As tipo_iden_tercero, d.nom_tercero, 
                                            d.cod_cuenta_niif, d.nomcue_niif, d.naturaleza, d.depende_de, d.centro_costo, d.centro_gestion, d.codigo, r.tipo_regimen, d.base_comp, d.porcentaje 
                                            From e_comprobante e, d_comprobante d Left Join Regimen r On (d.tercero = r.codpersona ) 
                                            Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N'
                                            And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') " + @"
                                            And d.cod_cuenta_niif = '" + lcod_cuenta + "' And d.centro_costo Between " + CenIni.ToString() + " And " + CenFin.ToString();                            
                                if (pOrdenar == "1")        // Ordenar por tipo de comprobante
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), e.tipo_comp, e.num_comp";
                                else if (pOrdenar == "2")   // Ordenar por número de comprobante;
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), e.num_comp";
                                else if (pOrdenar == "3")   // Ordernar por tercero
                                    sqlT += " Order by to_char(e.fecha, 'MM/YYYY'), d.tercero";
                                else
                                    sqlT += " Order by e.fecha, e.num_comp, e.tipo_comp";
                            }
                            else
                            {
                                sqlT = "Select e.fecha, e.num_comp, e.tipo_comp, e.n_documento, e.num_consig, d.detalle, d.tipo, d.valor, e.concepto, e.tipo_benef, e.iden_benef, e.tipo_iden, " +
                                          "e.nombre, d.tercero, d.identificacion, d.tipo_iden, d.nom_tercero, d.cod_cuenta_niif, d.nomcue_niif, d.naturaleza, d.depende_de, d.centro_costo, d.centro_gestion, d.codigo, r.tipo_regimen " +
                                          "From e_comprobante e, d_comprobante d Left Join Regimen r On (d.tercero = r.codpersona ) " +
                                          "Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.estado != 'N' " +
                                          "And e.fecha between '" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "' and '" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "' " +
                                          "And d.cod_cuenta_niif = '" + lcod_cuenta + "' And d.centro_costo Between " + CenIni.ToString() + " And " + CenFin.ToString();
                                if (pOrdenar == "1")
                                    sqlT += " Order by e.fecha, e.tipo_comp, e.num_comp";
                                else if (pOrdenar == "2")
                                    sqlT += " Order by e.fecha, e.num_comp";
                                else if (pOrdenar == "3")
                                    sqlT += " Order by e.fecha, d.tercero";
                                else
                                    sqlT += " Order by e.fecha, e.num_comp, e.tipo_comp";
                            }
                                                
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sqlT;
                            resultadoComp = cmdTransaccionFactory.ExecuteReader();
                            while (resultadoComp.Read())
                            {
                                LibroAuxiliar entidad = new LibroAuxiliar();
                                if (resultadoComp["cod_cuenta_niif"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultadoComp["cod_cuenta_niif"]);
                                if (resultadoComp["nomcue_niif"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultadoComp["nomcue_niif"]);
                                entidad.naturaleza = lnaturaleza;
                                if (resultadoComp["DEPENDE_DE"] != DBNull.Value) entidad.depende_de = Convert.ToString(resultadoComp["DEPENDE_DE"]);
                                if (resultadoComp["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultadoComp["FECHA"]);
                                if (resultadoComp["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultadoComp["NUM_COMP"]);
                                if (resultadoComp["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultadoComp["TIPO_COMP"]);
                                if (resultadoComp["N_DOCUMENTO"] != DBNull.Value) entidad.sop_egreso = Convert.ToString(resultadoComp["N_DOCUMENTO"]);
                                if (resultadoComp["NUM_CONSIG"] != DBNull.Value) entidad.sop_ingreso = Convert.ToString(resultadoComp["NUM_CONSIG"]);
                                entidad.num_sop = entidad.sop_egreso != null && entidad.sop_egreso != "0" && entidad.sop_egreso.Trim() != "" ? entidad.sop_egreso : null;
                                if (entidad.sop_ingreso != null && entidad.sop_ingreso != "0" && entidad.sop_ingreso.Trim() != "")
                                {
                                    if (entidad.num_sop != null && entidad.num_sop != "0")
                                        entidad.num_sop += "/" + entidad.sop_ingreso;
                                    else
                                        entidad.num_sop = entidad.sop_ingreso;
                                }
                                if (resultadoComp["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultadoComp["DETALLE"]);
                                if (resultadoComp["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultadoComp["TIPO"]);
                                if (resultadoComp["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultadoComp["VALOR"]);
                                if (resultadoComp["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultadoComp["CONCEPTO"]);
                                if (resultadoComp["IDEN_BENEF"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultadoComp["IDEN_BENEF"]);
                                if (resultadoComp["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultadoComp["NOMBRE"]);
                                if (resultadoComp["IDENTIFICACION"] != DBNull.Value) entidad.identific_tercero = Convert.ToString(resultadoComp["IDENTIFICACION"]);
                                if (resultadoComp["NOM_TERCERO"] != DBNull.Value) entidad.nombre_tercero = Convert.ToString(resultadoComp["NOM_TERCERO"]);
                                if (resultadoComp["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultadoComp["CENTRO_COSTO"]);
                                if (resultadoComp["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToString(resultadoComp["CENTRO_GESTION"]);
                                if (resultadoComp["CODIGO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultadoComp["CODIGO"]);
                                if (resultadoComp["TIPO_REGIMEN"] != DBNull.Value) entidad.regimen = Convert.ToString(resultadoComp["TIPO_REGIMEN"]);                                
                                entidad.base_minima = lbase_minima;
                                entidad.porcentaje_impuesto = lporcentaje_impuesto;
                                lstLibAux.Add(entidad);
                            }
                        }

                        return lstLibAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarNIFData", "ListarAuxiliar", ex);
                        return null;
                    }
                }
            }
        }


       public string CargarCuentasNiif(string pcod_cuenta, Usuario pUsuario)
       {
           string sListadoCuentas = "";
           DbDataReader resultado = default(DbDataReader);
           using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
           {
               using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
               {
                   try
                   {
                       string cod_cuenta_niif = "";
                       string sql = "Select p.cod_cuenta_niif From plan_cuentas_niif p Where p.depende_de = '" + pcod_cuenta + "' ";
                       connection.Open();
                       cmdTransaccionFactory.Connection = connection;
                       cmdTransaccionFactory.CommandType = CommandType.Text;
                       cmdTransaccionFactory.CommandText = sql;
                       resultado = cmdTransaccionFactory.ExecuteReader();
                       while (resultado.Read())
                       {
                           if (resultado["cod_cuenta_niif"] != DBNull.Value) cod_cuenta_niif = Convert.ToString(resultado["cod_cuenta_niif"]);
                           string sCuentasHijas = CargarCuentasNiif(cod_cuenta_niif, pUsuario);
                           sListadoCuentas = sCuentasHijas + sListadoCuentas + ", '" + cod_cuenta_niif + "' ";
                       }
                       return sListadoCuentas;
                   }
                   catch (Exception ex)
                   {
                       BOExcepcion.Throw("LibroAuxiliarNIFData", "CargarCuentasNiif", ex);
                       return "";
                   }
               }
           }
       }



       public double SaldoCuentaNIIF(string cod_cuenta, DateTime fecha, Int64 cenini, Int64 cenfin, Usuario pUsuario)
       {
           Double saldo = 0;
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
                       pSALDO.DbType = DbType.Double;
                       pSALDO.Direction = ParameterDirection.Output;

                       cmdTransaccionFactory.Parameters.Add(pCOD_CUENTA);
                       cmdTransaccionFactory.Parameters.Add(pFECHA);
                       cmdTransaccionFactory.Parameters.Add(pCENINI);
                       cmdTransaccionFactory.Parameters.Add(pCENFIN);
                       cmdTransaccionFactory.Parameters.Add(pSALDO);

                       connection.Open();
                       cmdTransaccionFactory.Connection = connection;
                       cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                       cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOCUENTANIIF";
                       cmdTransaccionFactory.ExecuteNonQuery();

                       saldo = Convert.ToDouble(pSALDO.Value);

                       return saldo;

                   }
                   catch (Exception ex)
                   {
                       BOExcepcion.Throw("LibroAuxiliarNIFData", "SaldoCuentaNIIF", ex);
                       return 0;
                   }
               }
           }
       }
        
    }
}