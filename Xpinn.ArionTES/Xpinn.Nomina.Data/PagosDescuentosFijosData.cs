using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Nomina.Entities;
using Xpinn.Util;

namespace Xpinn.Nomina.Data
{
    public class PagosDescuentosFijosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public PagosDescuentosFijosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public PagosDescuentosFijos CrearPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pPagosDescuentosFijos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        //  pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pPagosDescuentosFijos.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        //  pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pPagosDescuentosFijos.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        // pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pvalorcuota = cmdTransaccionFactory.CreateParameter();
                        pvalorcuota.ParameterName = "p_valorcuota";
                        if (pPagosDescuentosFijos.valorcuota == null)
                            pvalorcuota.Value = DBNull.Value;
                        else
                        {
                            pvalorcuota.Value = pPagosDescuentosFijos.valorcuota;
                        }
                        pvalorcuota.Direction = ParameterDirection.Input;
                        //   pvalorcuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorcuota);

                        DbParameter pvalortotal = cmdTransaccionFactory.CreateParameter();
                        pvalortotal.ParameterName = "p_valortotal";
                        if (pPagosDescuentosFijos.valortotal == null)
                            pvalortotal.Value = DBNull.Value;
                        else
                        {
                            pvalortotal.Value = pPagosDescuentosFijos.valortotal;
                        }
                        pvalortotal.Direction = ParameterDirection.Input;
                        //  pvalortotal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotal);
                        DbParameter pacumulado = cmdTransaccionFactory.CreateParameter();
                        pacumulado.ParameterName = "p_acumulado";
                        if (pPagosDescuentosFijos.acumulado == null)
                            pacumulado.Value = 0;
                        else
                        {
                            pacumulado.Value = 0;
                        }
                        pacumulado.Direction = ParameterDirection.Input;
                        // pacumulado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pacumulado);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pPagosDescuentosFijos.fecha == null)
                        {
                            pfecha.Value = DBNull.Value;
                        }
                        else
                        {
                            pfecha.Value = pPagosDescuentosFijos.fecha;
                        }
                        pfecha.Direction = ParameterDirection.Input;
                        // pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcontrolsaldos = cmdTransaccionFactory.CreateParameter();
                        pcontrolsaldos.ParameterName = "p_controlsaldos";
                        if (pPagosDescuentosFijos.controlsaldos == null)
                            pcontrolsaldos.Value = 0;
                        else
                        {
                            pcontrolsaldos.Value = pPagosDescuentosFijos.controlsaldos;
                        }
                        pcontrolsaldos.Direction = ParameterDirection.Input;
                        // pcontrolsaldos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcontrolsaldos);

                        DbParameter pliquidapagodefinitiva = cmdTransaccionFactory.CreateParameter();
                        pliquidapagodefinitiva.ParameterName = "p_liquidapagodefinitiva";
                        if (pPagosDescuentosFijos.liquidapagodefinitiva == null)
                            pliquidapagodefinitiva.Value = 0;
                        else
                        {
                            pliquidapagodefinitiva.Value = pPagosDescuentosFijos.liquidapagodefinitiva;
                        }
                        pliquidapagodefinitiva.Direction = ParameterDirection.Input;
                        // pliquidapagodefinitiva.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pliquidapagodefinitiva);

                        DbParameter pliquidapagoperiodica = cmdTransaccionFactory.CreateParameter();
                        pliquidapagoperiodica.ParameterName = "p_liquidapagoperiodica";
                        if (pPagosDescuentosFijos.liquidapagoperiodica == null)
                            pliquidapagoperiodica.Value = 0;
                        else
                        {
                            pliquidapagoperiodica.Value = pPagosDescuentosFijos.liquidapagoperiodica;
                        }
                        pliquidapagoperiodica.Direction = ParameterDirection.Input;
                        // pliquidapagoperiodica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pliquidapagoperiodica);

                        DbParameter pdescuentoperiocidad = cmdTransaccionFactory.CreateParameter();
                        pdescuentoperiocidad.ParameterName = "p_descuentoperiocidad";
                        if (pPagosDescuentosFijos.descuentoperiocidad == null)
                            pdescuentoperiocidad.Value = DBNull.Value;
                        else
                        {
                            pdescuentoperiocidad.Value = pPagosDescuentosFijos.descuentoperiocidad;
                        }
                        pdescuentoperiocidad.Direction = ParameterDirection.Input;
                        //  pdescuentoperiocidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdescuentoperiocidad);

                        DbParameter pcodigoconceptonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigoconceptonomina.ParameterName = "p_codigoconceptonomina";
                        if (pPagosDescuentosFijos.codigoconceptonomina == null)
                            pcodigoconceptonomina.Value = DBNull.Value;
                        else
                        {
                            pcodigoconceptonomina.Value = pPagosDescuentosFijos.codigoconceptonomina;
                        }
                        pcodigoconceptonomina.Direction = ParameterDirection.Input;
                        //  pcodigoconceptonomina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconceptonomina);

                        DbParameter pcodigocentrocostos = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocostos.ParameterName = "p_codigocentrocostos";
                        if (pPagosDescuentosFijos.codigocentrocostos == null)
                            pcodigocentrocostos.Value = DBNull.Value;
                        else
                        {
                            pcodigocentrocostos.Value = pPagosDescuentosFijos.codigocentrocostos;
                        }
                        pcodigocentrocostos.Direction = ParameterDirection.Input;
                        // pcodigocentrocostos.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocostos);

                        DbParameter pmotivos = cmdTransaccionFactory.CreateParameter();
                        pmotivos.ParameterName = "p_motivos";
                        if (pPagosDescuentosFijos.motivos == null)
                            pmotivos.Value = DBNull.Value;
                        else
                        {
                            pmotivos.Value = pPagosDescuentosFijos.motivos;
                        }

                        pmotivos.Direction = ParameterDirection.Input;
                        //    pmotivos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmotivos);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pPagosDescuentosFijos.codigotiponomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                        {
                            pCodigoNomina.Value = pPagosDescuentosFijos.codigotiponomina;
                        }
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        // pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);


                        DbParameter P_CODIGOTERCERO = cmdTransaccionFactory.CreateParameter();
                        P_CODIGOTERCERO.ParameterName = "PCODIGO_TERCERO";
                        if (pPagosDescuentosFijos.cod_proveedor == null)
                            P_CODIGOTERCERO.Value = DBNull.Value;
                        else
                        {
                            P_CODIGOTERCERO.Value = pPagosDescuentosFijos.cod_proveedor;
                        }
                        P_CODIGOTERCERO.Direction = ParameterDirection.Input;
                        //   PCODIGOTERCERO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CODIGOTERCERO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PAGOS_DESC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pPagosDescuentosFijos.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "CrearPagosDescuentosFijos", ex);
                        return null;
                    }
                }
            }
        }


        public PagosDescuentosFijos ModificarPagosDescuentosFijos(PagosDescuentosFijos pPagosDescuentosFijos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pPagosDescuentosFijos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pPagosDescuentosFijos.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pPagosDescuentosFijos.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pvalorcuota = cmdTransaccionFactory.CreateParameter();
                        pvalorcuota.ParameterName = "p_valorcuota";
                        if (pPagosDescuentosFijos.valorcuota == null)
                            pvalorcuota.Value = DBNull.Value;
                        else
                            pvalorcuota.Value = pPagosDescuentosFijos.valorcuota;
                        pvalorcuota.Direction = ParameterDirection.Input;
                        pvalorcuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorcuota);

                        DbParameter pvalortotal = cmdTransaccionFactory.CreateParameter();
                        pvalortotal.ParameterName = "p_valortotal";
                        if (pPagosDescuentosFijos.valortotal == null)
                            pvalortotal.Value = DBNull.Value;
                        else
                            pvalortotal.Value = pPagosDescuentosFijos.valortotal;
                        pvalortotal.Direction = ParameterDirection.Input;
                        pvalortotal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotal);

                        DbParameter pacumulado = cmdTransaccionFactory.CreateParameter();
                        pacumulado.ParameterName = "p_acumulado";
                        if (pPagosDescuentosFijos.acumulado == null)
                            pacumulado.Value = DBNull.Value;
                        else
                            pacumulado.Value = pPagosDescuentosFijos.acumulado;
                        pacumulado.Direction = ParameterDirection.Input;
                        pacumulado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pacumulado);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pPagosDescuentosFijos.fecha == null)
                            pfecha.Value = DBNull.Value;
                        else
                            pfecha.Value = pPagosDescuentosFijos.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcontrolsaldos = cmdTransaccionFactory.CreateParameter();
                        pcontrolsaldos.ParameterName = "p_controlsaldos";
                        if (pPagosDescuentosFijos.controlsaldos == null)
                            pcontrolsaldos.Value = DBNull.Value;
                        else
                            pcontrolsaldos.Value = pPagosDescuentosFijos.controlsaldos;
                        pcontrolsaldos.Direction = ParameterDirection.Input;
                        pcontrolsaldos.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcontrolsaldos);

                        DbParameter pliquidapagodefinitiva = cmdTransaccionFactory.CreateParameter();
                        pliquidapagodefinitiva.ParameterName = "p_liquidapagodefinitiva";
                        if (pPagosDescuentosFijos.liquidapagodefinitiva == null)
                            pliquidapagodefinitiva.Value = DBNull.Value;
                        else
                            pliquidapagodefinitiva.Value = pPagosDescuentosFijos.liquidapagodefinitiva;
                        pliquidapagodefinitiva.Direction = ParameterDirection.Input;
                        pliquidapagodefinitiva.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pliquidapagodefinitiva);

                        DbParameter pliquidapagoperiodica = cmdTransaccionFactory.CreateParameter();
                        pliquidapagoperiodica.ParameterName = "p_liquidapagoperiodica";
                        if (pPagosDescuentosFijos.liquidapagoperiodica == null)
                            pliquidapagoperiodica.Value = DBNull.Value;
                        else
                            pliquidapagoperiodica.Value = pPagosDescuentosFijos.liquidapagoperiodica;
                        pliquidapagoperiodica.Direction = ParameterDirection.Input;
                        pliquidapagoperiodica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pliquidapagoperiodica);

                        DbParameter pdescuentoperiocidad = cmdTransaccionFactory.CreateParameter();
                        pdescuentoperiocidad.ParameterName = "p_descuentoperiocidad";
                        if (pPagosDescuentosFijos.descuentoperiocidad == null)
                            pdescuentoperiocidad.Value = DBNull.Value;
                        else
                            pdescuentoperiocidad.Value = pPagosDescuentosFijos.descuentoperiocidad;
                        pdescuentoperiocidad.Direction = ParameterDirection.Input;
                        pdescuentoperiocidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdescuentoperiocidad);

                        DbParameter pcodigoconceptonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigoconceptonomina.ParameterName = "p_codigoconceptonomina";
                        if (pPagosDescuentosFijos.codigoconceptonomina == null)
                            pcodigoconceptonomina.Value = DBNull.Value;
                        else
                            pcodigoconceptonomina.Value = pPagosDescuentosFijos.codigoconceptonomina;
                        pcodigoconceptonomina.Direction = ParameterDirection.Input;
                        pcodigoconceptonomina.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconceptonomina);

                        DbParameter pcodigocentrocostos = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocostos.ParameterName = "p_codigocentrocostos";
                        if (pPagosDescuentosFijos.codigocentrocostos == null)
                            pcodigocentrocostos.Value = DBNull.Value;
                        else
                            pcodigocentrocostos.Value = pPagosDescuentosFijos.codigocentrocostos;
                        pcodigocentrocostos.Direction = ParameterDirection.Input;
                        pcodigocentrocostos.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocostos);

                        DbParameter pmotivos = cmdTransaccionFactory.CreateParameter();
                        pmotivos.ParameterName = "p_motivos";
                        if (pPagosDescuentosFijos.motivos == null)
                            pmotivos.Value = DBNull.Value;
                        else
                            pmotivos.Value = pPagosDescuentosFijos.motivos;
                        pmotivos.Direction = ParameterDirection.Input;
                        pmotivos.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmotivos);

                        DbParameter pCodigoNomina = cmdTransaccionFactory.CreateParameter();
                        pCodigoNomina.ParameterName = "pCodigoNomina";
                        if (pPagosDescuentosFijos.codigotiponomina == null)
                            pCodigoNomina.Value = DBNull.Value;
                        else
                            pCodigoNomina.Value = pPagosDescuentosFijos.codigotiponomina;
                        pCodigoNomina.Direction = ParameterDirection.Input;
                        pCodigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNomina);


                        DbParameter P_CODIGOTERCERO = cmdTransaccionFactory.CreateParameter();
                        P_CODIGOTERCERO.ParameterName = "PCODIGO_TERCERO";
                        if (pPagosDescuentosFijos.cod_proveedor == null)
                            P_CODIGOTERCERO.Value = DBNull.Value;
                        else
                        {
                            P_CODIGOTERCERO.Value = pPagosDescuentosFijos.cod_proveedor;
                        }
                        P_CODIGOTERCERO.Direction = ParameterDirection.Input;
                        //   PCODIGOTERCERO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CODIGOTERCERO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PAGOS_DESC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ModificarPagosDescuentosFijos", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPagosDescuentosFijos(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        PagosDescuentosFijos pPagosDescuentosFijos = new PagosDescuentosFijos();
                        pPagosDescuentosFijos = ConsultarPagosDescuentosFijos(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pPagosDescuentosFijos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PAGOS_DESC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "EliminarPagosDescuentosFijos", ex);
                    }
                }
            }
        }


        public PagosDescuentosFijos ConsultarPagosDescuentosFijos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            PagosDescuentosFijos entidad = new PagosDescuentosFijos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT pago.*, per.NOMBRE, per.identificacion, per.tipo_identificacion, pago.CodigoNomina
                                        FROM PAGOS_DESCUENTOS_FIJOS pago 
                                        JOIN v_persona per on per.cod_persona = pago.CodigoPersona
                                        JOIN EMPLEADOS emp on emp.CONSECUTIVO = pago.CODIGOEMPLEADO
                                        WHERE pago.CONSECUTIVO = " + pId.ToString();

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
                            if (resultado["CodigoNomina"] != DBNull.Value) entidad.codigotiponomina = Convert.ToInt64(resultado["CodigoNomina"]);
                            if (resultado["VALORCUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToDecimal(resultado["VALORCUOTA"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToDecimal(resultado["VALORTOTAL"]);
                            if (resultado["ACUMULADO"] != DBNull.Value) entidad.acumulado = Convert.ToDecimal(resultado["ACUMULADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CONTROLSALDOS"] != DBNull.Value) entidad.controlsaldos = Convert.ToInt64(resultado["CONTROLSALDOS"]);
                            if (resultado["LIQUIDAPAGODEFINITIVA"] != DBNull.Value) entidad.liquidapagodefinitiva = Convert.ToInt32(resultado["LIQUIDAPAGODEFINITIVA"]);
                            if (resultado["LIQUIDAPAGOPERIODICA"] != DBNull.Value) entidad.liquidapagoperiodica = Convert.ToInt32(resultado["LIQUIDAPAGOPERIODICA"]);
                            if (resultado["DESCUENTOPERIOCIDAD"] != DBNull.Value) entidad.descuentoperiocidad = Convert.ToInt32(resultado["DESCUENTOPERIOCIDAD"]);
                            if (resultado["CODIGOCONCEPTONOMINA"] != DBNull.Value) entidad.codigoconceptonomina = Convert.ToInt32(resultado["CODIGOCONCEPTONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTOS"] != DBNull.Value) entidad.codigocentrocostos = Convert.ToInt64(resultado["CODIGOCENTROCOSTOS"]);
                            if (resultado["MOTIVOS"] != DBNull.Value) entidad.motivos = Convert.ToString(resultado["MOTIVOS"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_TERCERO"]);

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
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ConsultarPagosDescuentosFijos", ex);
                        return null;
                    }
                }
            }
        }


        public List<PagosDescuentosFijos> ListarPagosDescuentosFijos(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PagosDescuentosFijos> lstPagosDescuentosFijos = new List<PagosDescuentosFijos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT pago.*, per.NOMBRE, per.identificacion, per.tipo_identificacion, 
                                        cen.DESCRIPCION as desc_centro_costo, con.DESCRIPCION as desc_concepto_nomina, nomi.DESCRIPCION as desc_nomina,
                                        CASE pago.DESCUENTOPERIOCIDAD WHEN 1 THEN '1er Periodo' WHEN 2 THEN '2do Periodo' WHEN 3 THEN '3er Periodo' WHEN 4 THEN '4to Periodo' WHEN 5 THEN 'Todos los Periodos' END as desc_descuento_periocidad
                                        FROM PAGOS_DESCUENTOS_FIJOS pago 
                                        JOIN v_persona per on per.cod_persona = pago.CodigoPersona
                                        JOIN EMPLEADOS emp on emp.CONSECUTIVO = pago.CODIGOEMPLEADO
                                        LEFT JOIN CENTRO_COSTO cen on cen.CENTRO_COSTO = pago.CODIGOCENTROCOSTOS
                                        LEFT JOIN CONCEPTO_NOMINA con on con.CONSECUTIVO = pago.CODIGOCONCEPTONOMINA
                                        LEFT JOIN NOMINA_EMPLEADO nomi on nomi.consecutivo = pago.CodigoNomina " + filtro + " ORDER BY pago.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PagosDescuentosFijos entidad = new PagosDescuentosFijos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["VALORCUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToDecimal(resultado["VALORCUOTA"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToDecimal(resultado["VALORTOTAL"]);
                            if (resultado["ACUMULADO"] != DBNull.Value) entidad.acumulado = Convert.ToDecimal(resultado["ACUMULADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CONTROLSALDOS"] != DBNull.Value) entidad.controlsaldos = Convert.ToInt64(resultado["CONTROLSALDOS"]);
                            if (resultado["LIQUIDAPAGODEFINITIVA"] != DBNull.Value) entidad.liquidapagodefinitiva = Convert.ToInt32(resultado["LIQUIDAPAGODEFINITIVA"]);
                            if (resultado["LIQUIDAPAGOPERIODICA"] != DBNull.Value) entidad.liquidapagoperiodica = Convert.ToInt32(resultado["LIQUIDAPAGOPERIODICA"]);
                            if (resultado["DESCUENTOPERIOCIDAD"] != DBNull.Value) entidad.descuentoperiocidad = Convert.ToInt32(resultado["DESCUENTOPERIOCIDAD"]);
                            if (resultado["CODIGOCONCEPTONOMINA"] != DBNull.Value) entidad.codigoconceptonomina = Convert.ToInt32(resultado["CODIGOCONCEPTONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTOS"] != DBNull.Value) entidad.codigocentrocostos = Convert.ToInt64(resultado["CODIGOCENTROCOSTOS"]);
                            if (resultado["MOTIVOS"] != DBNull.Value) entidad.motivos = Convert.ToString(resultado["MOTIVOS"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["desc_concepto_nomina"] != DBNull.Value) entidad.desc_concepto_nomina = Convert.ToString(resultado["desc_concepto_nomina"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_descuento_periocidad"] != DBNull.Value) entidad.desc_descuento_periocidad = Convert.ToString(resultado["desc_descuento_periocidad"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_TERCERO"]);

                            lstPagosDescuentosFijos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ListarPagosDescuentosFijos", ex);
                        return null;
                    }
                }
            }
        }
        public List<PagosDescuentosFijos> ListarConceptosNomina(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PagosDescuentosFijos> lstPagosDescuentosFijos = new List<PagosDescuentosFijos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT  * FROM CONCEPTO_NOMINA a where 1 =1  " + filtro + " ORDER BY a.CONSECUTIVO ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PagosDescuentosFijos entidad = new PagosDescuentosFijos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstPagosDescuentosFijos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ListarConceptosNomina", ex);
                        return null;
                    }
                }
            }
        }

        public PagosDescuentosFijos ConsultarTipoConceptosNomina(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            PagosDescuentosFijos entidad = new PagosDescuentosFijos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT  * FROM CONCEPTO_NOMINA a where 1 =1  " + " and a.consecutivo =" + filtro + " ORDER BY a.CONSECUTIVO ASC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ConsultarTipoConceptosNomina", ex);
                        return null;
                    }
                }
            }
        }
        public List<PagosDescuentosFijos> ListarProveedorDescuentos(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PagosDescuentosFijos> lstPagosDescuentosFijos = new List<PagosDescuentosFijos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT distinct(per.identificacion) AS identificacion_tercero, per.NOMBRE AS NOMBRE_TERCERO
                                        FROM PAGOS_DESCUENTOS_FIJOS pago 
                                        JOIN v_persona per on per.cod_persona = pago.COD_TERCERO  order by NOMBRE asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PagosDescuentosFijos entidad = new PagosDescuentosFijos();
                            if (resultado["identificacion_tercero"] != DBNull.Value) entidad.identificacion_tercero = Convert.ToString(resultado["identificacion_tercero"]);

                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombre_tercero= Convert.ToString(resultado["NOMBRE_TERCERO"]);
          
                            lstPagosDescuentosFijos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ListarProveedorDescuentos", ex);
                        return null;
                    }
                }
            }
        }
        public List<PagosDescuentosFijos> ListarDescuentosFijosReporte(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PagosDescuentosFijos> lstPagosDescuentosFijos = new List<PagosDescuentosFijos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT pago.*, per.NOMBRE, per.identificacion, per.tipo_identificacion, 
                                        cen.DESCRIPCION as desc_centro_costo, con.DESCRIPCION as desc_concepto_nomina, nomi.DESCRIPCION as desc_nomina,
                                        CASE pago.DESCUENTOPERIOCIDAD WHEN 1 THEN '1er Periodo' WHEN 2 THEN '2do Periodo' WHEN 3 THEN '3er Periodo' WHEN 4 THEN '4to Periodo' WHEN 5 THEN 'Todos los Periodos' END as desc_descuento_periocidad
                                        FROM PAGOS_DESCUENTOS_FIJOS pago 
                                        JOIN v_persona per on per.cod_persona = pago.CodigoPersona
                                        JOIN EMPLEADOS emp on emp.CONSECUTIVO = pago.CODIGOEMPLEADO
                                        LEFT JOIN CENTRO_COSTO cen on cen.CENTRO_COSTO = pago.CODIGOCENTROCOSTOS
                                        LEFT JOIN CONCEPTO_NOMINA con on con.CONSECUTIVO = pago.CODIGOCONCEPTONOMINA
                                        LEFT JOIN NOMINA_EMPLEADO nomi on nomi.consecutivo = pago.CodigoNomina " + filtro + " ORDER BY pago.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PagosDescuentosFijos entidad = new PagosDescuentosFijos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["VALORCUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToDecimal(resultado["VALORCUOTA"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToDecimal(resultado["VALORTOTAL"]);
                            if (resultado["ACUMULADO"] != DBNull.Value) entidad.acumulado = Convert.ToDecimal(resultado["ACUMULADO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CONTROLSALDOS"] != DBNull.Value) entidad.controlsaldos = Convert.ToInt64(resultado["CONTROLSALDOS"]);
                            if (resultado["LIQUIDAPAGODEFINITIVA"] != DBNull.Value) entidad.liquidapagodefinitiva = Convert.ToInt32(resultado["LIQUIDAPAGODEFINITIVA"]);
                            if (resultado["LIQUIDAPAGOPERIODICA"] != DBNull.Value) entidad.liquidapagoperiodica = Convert.ToInt32(resultado["LIQUIDAPAGOPERIODICA"]);
                            if (resultado["DESCUENTOPERIOCIDAD"] != DBNull.Value) entidad.descuentoperiocidad = Convert.ToInt32(resultado["DESCUENTOPERIOCIDAD"]);
                            if (resultado["CODIGOCONCEPTONOMINA"] != DBNull.Value) entidad.codigoconceptonomina = Convert.ToInt32(resultado["CODIGOCONCEPTONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTOS"] != DBNull.Value) entidad.codigocentrocostos = Convert.ToInt64(resultado["CODIGOCENTROCOSTOS"]);
                            if (resultado["MOTIVOS"] != DBNull.Value) entidad.motivos = Convert.ToString(resultado["MOTIVOS"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["tipo_identificacion"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToString(resultado["tipo_identificacion"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["desc_concepto_nomina"] != DBNull.Value) entidad.desc_concepto_nomina = Convert.ToString(resultado["desc_concepto_nomina"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_descuento_periocidad"] != DBNull.Value) entidad.desc_descuento_periocidad = Convert.ToString(resultado["desc_descuento_periocidad"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_proveedor = Convert.ToInt64(resultado["COD_TERCERO"]);

                            lstPagosDescuentosFijos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPagosDescuentosFijos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagosDescuentosFijosData", "ListarDescuentosFijosReporte", ex);
                        return null;
                    }
                }
            }
        }


    }
}