using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{

    public class CuentasPorPagarData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public CuentasPorPagarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CUENTAXPAGAR_ANTICIPO CrearCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidanticipo = cmdTransaccionFactory.CreateParameter();
                        pidanticipo.ParameterName = "p_idanticipo";
                        if (pCUENTAXPAGAR_ANTICIPO.idanticipo != 0) pidanticipo.Value = pCUENTAXPAGAR_ANTICIPO.idanticipo; else pidanticipo.Value = DBNull.Value;
                        pidanticipo.Direction = ParameterDirection.Input;
                        pidanticipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidanticipo);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCUENTAXPAGAR_ANTICIPO.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pfecha_anticipo = cmdTransaccionFactory.CreateParameter();
                        pfecha_anticipo.ParameterName = "p_fecha_anticipo";
                        pfecha_anticipo.Value = pCUENTAXPAGAR_ANTICIPO.fecha_anticipo;
                        pfecha_anticipo.Direction = ParameterDirection.Input;
                        pfecha_anticipo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_anticipo);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pCUENTAXPAGAR_ANTICIPO.fecha_aprobacion == null)
                            pfecha_aprobacion.Value = DBNull.Value;
                        else
                            pfecha_aprobacion.Value = pCUENTAXPAGAR_ANTICIPO.fecha_aprobacion;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCUENTAXPAGAR_ANTICIPO.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pCUENTAXPAGAR_ANTICIPO.saldo == null)
                            psaldo.Value = DBNull.Value;
                        else
                            psaldo.Value = pCUENTAXPAGAR_ANTICIPO.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCUENTAXPAGAR_ANTICIPO.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTAXPAG_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCUENTAXPAGAR_ANTICIPO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOData", "CrearCUENTAXPAGAR_ANTICIPO", ex);
                        return null;
                    }
                }
            }
        }
        public Xpinn.Tesoreria.Entities.Giro CrearGiro(Xpinn.Tesoreria.Entities.Giro pGiro, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiro.idgiro;
                        if (opcion == 1)
                            pidgiro.Direction = ParameterDirection.Output;
                        else
                            pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pGiro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pGiro.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pfec_reg = cmdTransaccionFactory.CreateParameter();
                        pfec_reg.ParameterName = "p_fec_reg";
                        pfec_reg.Value = pGiro.fec_reg;
                        pfec_reg.Direction = ParameterDirection.Input;
                        pfec_reg.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_reg);

                        DbParameter pfec_giro = cmdTransaccionFactory.CreateParameter();
                        pfec_giro.ParameterName = "p_fec_giro";
                        if (pGiro.fec_giro != DateTime.MinValue) pfec_giro.Value = pGiro.fec_giro; else pfec_giro.Value = DBNull.Value;
                        pfec_giro.Direction = ParameterDirection.Input;
                        pfec_giro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_giro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pGiro.numero_radicacion != 0) pnumero_radicacion.Value = pGiro.numero_radicacion; else pnumero_radicacion.Value = DBNull.Value;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pGiro.cod_ope != 0) pcod_ope.Value = pGiro.cod_ope; else pcod_ope.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        pnum_comp.Value = -1;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        ptipo_comp.Value = -1;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pusu_gen = cmdTransaccionFactory.CreateParameter();
                        pusu_gen.ParameterName = "p_usu_gen";
                        if (pGiro.usu_gen != null) pusu_gen.Value = pGiro.usu_gen; else pusu_gen.Value = DBNull.Value;
                        pusu_gen.Direction = ParameterDirection.Input;
                        pusu_gen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_gen);

                        DbParameter pusu_apli = cmdTransaccionFactory.CreateParameter();
                        pusu_apli.ParameterName = "p_usu_apli";
                        if (pGiro.usu_apli != null) pusu_apli.Value = pGiro.usu_apli; else pusu_apli.Value = DBNull.Value;
                        pusu_apli.Direction = ParameterDirection.Input;
                        pusu_apli.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apli);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pGiro.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusu_apro = cmdTransaccionFactory.CreateParameter();
                        pusu_apro.ParameterName = "p_usu_apro";
                        if (pGiro.usu_apro != null) pusu_apro.Value = pGiro.usu_apro; else pusu_apro.Value = DBNull.Value;
                        pusu_apro.Direction = ParameterDirection.Input;
                        pusu_apro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apro);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (pGiro.idctabancaria != 0) pidctabancaria.Value = pGiro.idctabancaria; else pidctabancaria.Value = DBNull.Value;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pGiro.cod_banco != 0) pcod_banco.Value = pGiro.cod_banco; else pcod_banco.Value = DBNull.Value;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (pGiro.num_cuenta != null) pnum_cuenta.Value = pGiro.num_cuenta; else pnum_cuenta.Value = DBNull.Value;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (pGiro.tipo_cuenta != -1) ptipo_cuenta.Value = pGiro.tipo_cuenta; else ptipo_cuenta.Value = DBNull.Value;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pfec_apro = cmdTransaccionFactory.CreateParameter();
                        pfec_apro.ParameterName = "p_fec_apro";
                        if (pGiro.fec_apro != DateTime.MinValue) pfec_apro.Value = pGiro.fec_apro; else pfec_apro.Value = DBNull.Value;
                        pfec_apro.Direction = ParameterDirection.Input;
                        pfec_apro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_apro);

                        DbParameter pcob_comision = cmdTransaccionFactory.CreateParameter();
                        pcob_comision.ParameterName = "p_cob_comision";
                        if (pGiro.cob_comision != 0) pcob_comision.Value = pGiro.cob_comision; else pcob_comision.Value = DBNull.Value;
                        pcob_comision.Direction = ParameterDirection.Input;
                        pcob_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcob_comision);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiro.valor != 0) pvalor.Value = pGiro.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (opcion == 1)
                            pGiro.idgiro = Convert.ToInt32(pidgiro.Value);
                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearGiro", ex);
                        return null;
                    }
                }
            }
        }

        public CUENTAXPAGAR_ANTICIPO ModificarCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidanticipo = cmdTransaccionFactory.CreateParameter();
                        pidanticipo.ParameterName = "p_idanticipo";
                        pidanticipo.Value = pCUENTAXPAGAR_ANTICIPO.idanticipo;
                        pidanticipo.Direction = ParameterDirection.Input;
                        pidanticipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidanticipo);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCUENTAXPAGAR_ANTICIPO.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pfecha_anticipo = cmdTransaccionFactory.CreateParameter();
                        pfecha_anticipo.ParameterName = "p_fecha_anticipo";
                        pfecha_anticipo.Value = pCUENTAXPAGAR_ANTICIPO.fecha_anticipo;
                        pfecha_anticipo.Direction = ParameterDirection.Input;
                        pfecha_anticipo.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_anticipo);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pCUENTAXPAGAR_ANTICIPO.fecha_aprobacion == null)
                            pfecha_aprobacion.Value = DBNull.Value;
                        else
                            pfecha_aprobacion.Value = pCUENTAXPAGAR_ANTICIPO.fecha_aprobacion;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCUENTAXPAGAR_ANTICIPO.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pCUENTAXPAGAR_ANTICIPO.saldo == null)
                            psaldo.Value = DBNull.Value;
                        else
                            psaldo.Value = pCUENTAXPAGAR_ANTICIPO.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCUENTAXPAGAR_ANTICIPO.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTAXPAG_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCUENTAXPAGAR_ANTICIPO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOData", "ModificarCUENTAXPAGAR_ANTICIPO", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO = new CUENTAXPAGAR_ANTICIPO();
                        pCUENTAXPAGAR_ANTICIPO = ConsultarCUENTAXPAGAR_ANTICIPO(pId, vUsuario);

                        DbParameter pidanticipo = cmdTransaccionFactory.CreateParameter();
                        pidanticipo.ParameterName = "p_idanticipo";
                        pidanticipo.Value = pCUENTAXPAGAR_ANTICIPO.idanticipo;
                        pidanticipo.Direction = ParameterDirection.Input;
                        pidanticipo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidanticipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTAXPAG_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOData", "EliminarCUENTAXPAGAR_ANTICIPO", ex);
                    }
                }
            }
        }


        public CUENTAXPAGAR_ANTICIPO ConsultarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CUENTAXPAGAR_ANTICIPO entidad = new CUENTAXPAGAR_ANTICIPO();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUENTAXPAGAR_ANTICIPO WHERE codigo_factura = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDANTICIPO"] != DBNull.Value) entidad.idanticipo = Convert.ToInt64(resultado["IDANTICIPO"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["FECHA_ANTICIPO"] != DBNull.Value) entidad.fecha_anticipo = Convert.ToDateTime(resultado["FECHA_ANTICIPO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
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
                        BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOData", "ConsultarCUENTAXPAGAR_ANTICIPO", ex);
                        return null;
                    }
                }
            }
        }


        public List<CUENTAXPAGAR_ANTICIPO> ListarCUENTAXPAGAR_ANTICIPO(CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CUENTAXPAGAR_ANTICIPO> lstCUENTAXPAGAR_ANTICIPO = new List<CUENTAXPAGAR_ANTICIPO>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUENTAXPAGAR_ANTICIPO " + ObtenerFiltro(pCUENTAXPAGAR_ANTICIPO) + " ORDER BY IDANTICIPO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CUENTAXPAGAR_ANTICIPO entidad = new CUENTAXPAGAR_ANTICIPO();
                            if (resultado["IDANTICIPO"] != DBNull.Value) entidad.idanticipo = Convert.ToInt64(resultado["IDANTICIPO"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["FECHA_ANTICIPO"] != DBNull.Value) entidad.fecha_anticipo = Convert.ToDateTime(resultado["FECHA_ANTICIPO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            lstCUENTAXPAGAR_ANTICIPO.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCUENTAXPAGAR_ANTICIPO;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOData", "ListarCUENTAXPAGAR_ANTICIPO", ex);
                        return null;
                    }
                }
            }
        }




        public CuentasPorPagar CrearCuentasXpagar(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Output;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pnumero_factura = cmdTransaccionFactory.CreateParameter();
                        pnumero_factura.ParameterName = "p_numero_factura";
                        if (pCuentas.numero_factura != null) pnumero_factura.Value = pCuentas.numero_factura; else pnumero_factura.Value = DBNull.Value;
                        pnumero_factura.Direction = ParameterDirection.Input;
                        pnumero_factura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_factura);

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "p_fecha_ingreso";
                        pfecha_ingreso.Value = pCuentas.fecha_ingreso;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);

                        DbParameter pfecha_factura = cmdTransaccionFactory.CreateParameter();
                        pfecha_factura.ParameterName = "p_fecha_factura";
                        if (pCuentas.fecha_factura != DateTime.MinValue) pfecha_factura.Value = pCuentas.fecha_factura; else pfecha_factura.Value = DBNull.Value;
                        pfecha_factura.Direction = ParameterDirection.Input;
                        pfecha_factura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_factura);

                        DbParameter pfecha_radicacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_radicacion.ParameterName = "p_fecha_radicacion";
                        if (pCuentas.fecha_radicacion != DateTime.MinValue) pfecha_radicacion.Value = pCuentas.fecha_radicacion; else pfecha_radicacion.Value = DBNull.Value;
                        pfecha_radicacion.Direction = ParameterDirection.Input;
                        pfecha_radicacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_radicacion);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pCuentas.fecha_vencimiento != DateTime.MinValue) pfecha_vencimiento.Value = pCuentas.fecha_vencimiento; else pfecha_vencimiento.Value = DBNull.Value;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pidtipo_cta_por_pagar = cmdTransaccionFactory.CreateParameter();
                        pidtipo_cta_por_pagar.ParameterName = "p_idtipo_cta_por_pagar";
                        pidtipo_cta_por_pagar.Value = pCuentas.idtipo_cta_por_pagar;
                        pidtipo_cta_por_pagar.Direction = ParameterDirection.Input;
                        pidtipo_cta_por_pagar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_cta_por_pagar);

                        DbParameter pdoc_equivalente = cmdTransaccionFactory.CreateParameter();
                        pdoc_equivalente.ParameterName = "p_doc_equivalente";
                        if (pCuentas.doc_equivalente != 0) pdoc_equivalente.Value = pCuentas.doc_equivalente; else pdoc_equivalente.Value = DBNull.Value;
                        pdoc_equivalente.Direction = ParameterDirection.Input;
                        pdoc_equivalente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdoc_equivalente);

                        DbParameter pnum_contrato = cmdTransaccionFactory.CreateParameter();
                        pnum_contrato.ParameterName = "p_num_contrato";
                        if (pCuentas.num_contrato != null) pnum_contrato.Value = pCuentas.num_contrato; else pnum_contrato.Value = DBNull.Value;
                        pnum_contrato.Direction = ParameterDirection.Input;
                        pnum_contrato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_contrato);

                        DbParameter ppoliza = cmdTransaccionFactory.CreateParameter();
                        ppoliza.ParameterName = "p_poliza";
                        if (pCuentas.poliza != null) ppoliza.Value = pCuentas.poliza; else ppoliza.Value = DBNull.Value;
                        ppoliza.Direction = ParameterDirection.Input;
                        ppoliza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppoliza);

                        DbParameter pvence_contrato = cmdTransaccionFactory.CreateParameter();
                        pvence_contrato.ParameterName = "p_vence_contrato";
                        if (pCuentas.vence_contrato != DateTime.MinValue) pvence_contrato.Value = pCuentas.vence_contrato; else pvence_contrato.Value = DBNull.Value;
                        pvence_contrato.Direction = ParameterDirection.Input;
                        pvence_contrato.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvence_contrato);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentas.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pmaneja_descuentos = cmdTransaccionFactory.CreateParameter();
                        pmaneja_descuentos.ParameterName = "p_maneja_descuentos";
                        pmaneja_descuentos.Value = pCuentas.maneja_descuentos;
                        pmaneja_descuentos.Direction = ParameterDirection.Input;
                        pmaneja_descuentos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_descuentos);

                        DbParameter pmaneja_anticipos = cmdTransaccionFactory.CreateParameter();
                        pmaneja_anticipos.ParameterName = "p_maneja_anticipos";
                        pmaneja_anticipos.Value = pCuentas.maneja_anticipos;
                        pmaneja_anticipos.Direction = ParameterDirection.Input;
                        pmaneja_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_anticipos);

                        DbParameter pvalor_anticipo = cmdTransaccionFactory.CreateParameter();
                        pvalor_anticipo.ParameterName = "p_valor_anticipo";
                        if (pCuentas.valor_anticipo != 0) pvalor_anticipo.Value = pCuentas.valor_anticipo; else pvalor_anticipo.Value = DBNull.Value;
                        pvalor_anticipo.Direction = ParameterDirection.Input;
                        pvalor_anticipo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_anticipo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pCuentas.observaciones != null) pobservaciones.Value = pCuentas.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCuentas.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pfecha_crea = cmdTransaccionFactory.CreateParameter();
                        pfecha_crea.ParameterName = "p_fecha_crea";
                        pfecha_crea.Value = DateTime.Today;
                        pfecha_crea.Direction = ParameterDirection.Input;
                        pfecha_crea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_crea);

                        DbParameter PSALDO = cmdTransaccionFactory.CreateParameter();
                        PSALDO.ParameterName = "P_SALDO";
                        if (pCuentas.saldo != null && pCuentas.saldo != 0) PSALDO.Value = pCuentas.saldo; else PSALDO.Value = DBNull.Value;
                        PSALDO.Direction = ParameterDirection.Input;
                        PSALDO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PSALDO);


                        DbParameter PFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        if (pCuentas.formapago != null && pCuentas.formapago != 0) PFORMA_PAGO.Value = pCuentas.formapago; else PFORMA_PAGO.Value = DBNull.Value;
                        PFORMA_PAGO.Direction = ParameterDirection.Input;
                        PFORMA_PAGO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PFORMA_PAGO);

                        DbParameter PIDCTABANCARIA = cmdTransaccionFactory.CreateParameter();
                        PIDCTABANCARIA.ParameterName = "P_IDCTABANCARIA";
                        if (pCuentas.idctabancaria != null && pCuentas.idctabancaria != 0) PIDCTABANCARIA.Value = pCuentas.idctabancaria; else PIDCTABANCARIA.Value = DBNull.Value;
                        PIDCTABANCARIA.Direction = ParameterDirection.Input;
                        PIDCTABANCARIA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PIDCTABANCARIA);

                        DbParameter _COD_BANCO = cmdTransaccionFactory.CreateParameter();
                        _COD_BANCO.ParameterName = "P_COD_BANCO";
                        if (pCuentas.cod_banco != null && pCuentas.cod_banco != 0) _COD_BANCO.Value = pCuentas.cod_banco; else _COD_BANCO.Value = DBNull.Value;
                        _COD_BANCO.Direction = ParameterDirection.Input;
                        _COD_BANCO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(_COD_BANCO);

                        DbParameter PNUM_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PNUM_CUENTA.ParameterName = "P_NUM_CUENTA";
                        if (pCuentas.num_cuenta != null && pCuentas.num_cuenta != "") PNUM_CUENTA.Value = pCuentas.num_cuenta; else PNUM_CUENTA.Value = DBNull.Value;
                        PNUM_CUENTA.Direction = ParameterDirection.Input;
                        PNUM_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PNUM_CUENTA);

                        DbParameter PTIPO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PTIPO_CUENTA.ParameterName = "P_TIPO_CUENTA";
                        if (pCuentas.tipo_cuenta != null && pCuentas.tipo_cuenta != 0) PTIPO_CUENTA.Value = pCuentas.tipo_cuenta; else PTIPO_CUENTA.Value = DBNull.Value;
                        PTIPO_CUENTA.Direction = ParameterDirection.Input;
                        PTIPO_CUENTA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO_CUENTA);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pCuentas.cod_ope != null && pCuentas.cod_ope != 0) pcod_ope.Value = pCuentas.cod_ope; else PTIPO_CUENTA.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGAR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCuentas.codigo_factura = Convert.ToInt32(pcodigo_factura.Value);
                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "CrearCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }



        public CuentasPorPagar ModificarCuentasXpagar(CuentasPorPagar pCuentas,  Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pnumero_factura = cmdTransaccionFactory.CreateParameter();
                        pnumero_factura.ParameterName = "p_numero_factura";
                        if (pCuentas.numero_factura != null) pnumero_factura.Value = pCuentas.numero_factura; else pnumero_factura.Value = DBNull.Value;
                        pnumero_factura.Direction = ParameterDirection.Input;
                        pnumero_factura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_factura);

                        DbParameter pfecha_ingreso = cmdTransaccionFactory.CreateParameter();
                        pfecha_ingreso.ParameterName = "p_fecha_ingreso";
                        pfecha_ingreso.Value = pCuentas.fecha_ingreso;
                        pfecha_ingreso.Direction = ParameterDirection.Input;
                        pfecha_ingreso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ingreso);

                        DbParameter pfecha_factura = cmdTransaccionFactory.CreateParameter();
                        pfecha_factura.ParameterName = "p_fecha_factura";
                        if (pCuentas.fecha_factura != DateTime.MinValue) pfecha_factura.Value = pCuentas.fecha_factura; else pfecha_factura.Value = DBNull.Value;
                        pfecha_factura.Direction = ParameterDirection.Input;
                        pfecha_factura.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_factura);

                        DbParameter pfecha_radicacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_radicacion.ParameterName = "p_fecha_radicacion";
                        if (pCuentas.fecha_radicacion != DateTime.MinValue) pfecha_radicacion.Value = pCuentas.fecha_radicacion; else pfecha_radicacion.Value = DBNull.Value;
                        pfecha_radicacion.Direction = ParameterDirection.Input;
                        pfecha_radicacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_radicacion);

                        DbParameter pfecha_vencimiento = cmdTransaccionFactory.CreateParameter();
                        pfecha_vencimiento.ParameterName = "p_fecha_vencimiento";
                        if (pCuentas.fecha_vencimiento != DateTime.MinValue) pfecha_vencimiento.Value = pCuentas.fecha_vencimiento; else pfecha_vencimiento.Value = DBNull.Value;
                        pfecha_vencimiento.Direction = ParameterDirection.Input;
                        pfecha_vencimiento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_vencimiento);

                        DbParameter pidtipo_cta_por_pagar = cmdTransaccionFactory.CreateParameter();
                        pidtipo_cta_por_pagar.ParameterName = "p_idtipo_cta_por_pagar";
                        pidtipo_cta_por_pagar.Value = pCuentas.idtipo_cta_por_pagar;
                        pidtipo_cta_por_pagar.Direction = ParameterDirection.Input;
                        pidtipo_cta_por_pagar.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtipo_cta_por_pagar);

                        DbParameter pdoc_equivalente = cmdTransaccionFactory.CreateParameter();
                        pdoc_equivalente.ParameterName = "p_doc_equivalente";
                        if (pCuentas.doc_equivalente != 0) pdoc_equivalente.Value = pCuentas.doc_equivalente; else pdoc_equivalente.Value = DBNull.Value;
                        pdoc_equivalente.Direction = ParameterDirection.Input;
                        pdoc_equivalente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdoc_equivalente);

                        DbParameter pnum_contrato = cmdTransaccionFactory.CreateParameter();
                        pnum_contrato.ParameterName = "p_num_contrato";
                        if (pCuentas.num_contrato != null) pnum_contrato.Value = pCuentas.num_contrato; else pnum_contrato.Value = DBNull.Value;
                        pnum_contrato.Direction = ParameterDirection.Input;
                        pnum_contrato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_contrato);

                        DbParameter ppoliza = cmdTransaccionFactory.CreateParameter();
                        ppoliza.ParameterName = "p_poliza";
                        if (pCuentas.poliza != null) ppoliza.Value = pCuentas.poliza; else ppoliza.Value = DBNull.Value;
                        ppoliza.Direction = ParameterDirection.Input;
                        ppoliza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ppoliza);

                        DbParameter pvence_contrato = cmdTransaccionFactory.CreateParameter();
                        pvence_contrato.ParameterName = "p_vence_contrato";
                        if (pCuentas.vence_contrato != DateTime.MinValue) pvence_contrato.Value = pCuentas.vence_contrato; else pvence_contrato.Value = DBNull.Value;
                        pvence_contrato.Direction = ParameterDirection.Input;
                        pvence_contrato.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pvence_contrato);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pCuentas.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pmaneja_descuentos = cmdTransaccionFactory.CreateParameter();
                        pmaneja_descuentos.ParameterName = "p_maneja_descuentos";
                        pmaneja_descuentos.Value = pCuentas.maneja_descuentos;
                        pmaneja_descuentos.Direction = ParameterDirection.Input;
                        pmaneja_descuentos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_descuentos);

                        DbParameter pmaneja_anticipos = cmdTransaccionFactory.CreateParameter();
                        pmaneja_anticipos.ParameterName = "p_maneja_anticipos";
                        pmaneja_anticipos.Value = pCuentas.maneja_anticipos;
                        pmaneja_anticipos.Direction = ParameterDirection.Input;
                        pmaneja_anticipos.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_anticipos);

                        DbParameter pvalor_anticipo = cmdTransaccionFactory.CreateParameter();
                        pvalor_anticipo.ParameterName = "p_valor_anticipo";
                        if (pCuentas.valor_anticipo != 0) pvalor_anticipo.Value = pCuentas.valor_anticipo; else pvalor_anticipo.Value = DBNull.Value;
                        pvalor_anticipo.Direction = ParameterDirection.Input;
                        pvalor_anticipo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_anticipo);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pCuentas.observaciones != null) pobservaciones.Value = pCuentas.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCuentas.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter PSALDO = cmdTransaccionFactory.CreateParameter();
                        PSALDO.ParameterName = "P_SALDO";
                        if (pCuentas.saldo != null && pCuentas.saldo != 0) PSALDO.Value = pCuentas.saldo; else PSALDO.Value = DBNull.Value;
                        PSALDO.Direction = ParameterDirection.Input;
                        PSALDO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PSALDO);


                        DbParameter PFORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        PFORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        if (pCuentas.formapago != null && pCuentas.formapago != 0) PFORMA_PAGO.Value = pCuentas.formapago; else PFORMA_PAGO.Value = DBNull.Value;
                        PFORMA_PAGO.Direction = ParameterDirection.Input;
                        PFORMA_PAGO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PFORMA_PAGO);

                        DbParameter PIDCTABANCARIA = cmdTransaccionFactory.CreateParameter();
                        PIDCTABANCARIA.ParameterName = "P_IDCTABANCARIA";
                        if (pCuentas.idctabancaria != null && pCuentas.idctabancaria != 0) PIDCTABANCARIA.Value = pCuentas.idctabancaria; else PIDCTABANCARIA.Value = DBNull.Value;
                        PIDCTABANCARIA.Direction = ParameterDirection.Input;
                        PIDCTABANCARIA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PIDCTABANCARIA);

                        DbParameter _COD_BANCO = cmdTransaccionFactory.CreateParameter();
                        _COD_BANCO.ParameterName = "P_COD_BANCO";
                        if (pCuentas.cod_banco != null && pCuentas.cod_banco != 0) _COD_BANCO.Value = pCuentas.cod_banco; else _COD_BANCO.Value = DBNull.Value;
                        _COD_BANCO.Direction = ParameterDirection.Input;
                        _COD_BANCO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(_COD_BANCO);

                        DbParameter PNUM_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PNUM_CUENTA.ParameterName = "P_NUM_CUENTA";
                        if (pCuentas.num_cuenta != null && pCuentas.num_cuenta != "") PNUM_CUENTA.Value = pCuentas.num_cuenta; else PNUM_CUENTA.Value = DBNull.Value;
                        PNUM_CUENTA.Direction = ParameterDirection.Input;
                        PNUM_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(PNUM_CUENTA);

                        DbParameter PTIPO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        PTIPO_CUENTA.ParameterName = "P_TIPO_CUENTA";
                        if (pCuentas.tipo_cuenta != null && pCuentas.tipo_cuenta != 0) PTIPO_CUENTA.Value = pCuentas.tipo_cuenta; else PTIPO_CUENTA.Value = DBNull.Value;
                        PTIPO_CUENTA.Direction = ParameterDirection.Input;
                        PTIPO_CUENTA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO_CUENTA);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pCuentas.cod_ope != null && pCuentas.cod_ope != 0) pcod_ope.Value = pCuentas.cod_ope; else PTIPO_CUENTA.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGAR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(CODIGO_FACTURA) + 1 FROM cuentasxpagar ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteEquivalente(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(DOC_EQUIVALENTE) + 1 FROM cuentasxpagar ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public void EliminarCuentasXpagar(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pId;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGAR_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "EliminarCuentasXpagar", ex);
                    }
                }
            }
        }



        public List<CuentasPorPagar> ListarCuentasXpagar(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, DateTime pVencIni, DateTime pVencFin, Usuario vUsuario, String filtro)
        {
            DbDataReader resultado;
            List<CuentasPorPagar> lstCuentas = new List<CuentasPorPagar>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.codigo_factura,c.numero_factura,c.fecha_ingreso,c.fecha_factura,c.fecha_radicacion, "
                                     +"c.fecha_vencimiento,c.doc_equivalente, "
                                     +"c.cod_persona,p.identificacion,p.primer_nombre | | p.segundo_nombre | | "
                                     +"p.primer_apellido | | p.segundo_apellido as nombre, "
                                     +"case c.IDTIPO_CTA_POR_PAGAR when 1 then 'Factura' "
                                     +"when 2 then 'Orden de Pago' when 3 then 'Orden de Compra' when 4 then 'Orden de Servicio' "
                                     +"when 5 then 'Contrato de Servicio' end as TipoNombre, "
                                     +"case c.MANEJA_DESCUENTOS when 0 then 'NO' when 1 then 'SI' end as manejaDscto, "
                                     +"case c.MANEJA_ANTICIPOS when 0 then 'NO' when 1 then 'SI' end as manejaAnti, "
                                     +"sum(d.valor_total)as valorTotal, sum(d.VALOR_NETO) as valorNeto,"
                                     + " case c.estado when 0 then 'Pendiente' when  1 then 'Contabilizada' when 2 then 'Aprobada' when 3 then 'Pagada' when 4 then 'Anulada' end as Estado,   "
                                     + "case c.IDTIPO_CTA_POR_PAGAR when 1 then 'Factura' when 2 then 'Orden de Pago' when 3 then 'Orden de Compra' when 4 then 'Orden de Servicio' when 5 then 'Contrato de Servicio' end "
                                     + "FROM CUENTASXPAGAR c Left Join persona p On p.cod_persona = c.cod_persona "
                                     +"Left Join cuentaxpagar_detalle d On d.codigo_factura = c.codigo_factura "
                                     +"WHERE 1 = 1 " + filtro;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_INGRESO >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_INGRESO >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_INGRESO <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_INGRESO <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pVencIni != null && pVencIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_VENCIMIENTO >= To_Date('" + Convert.ToDateTime(pVencIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_VENCIMIENTO >= '" + Convert.ToDateTime(pVencIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pVencFin != null && pVencFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_VENCIMIENTO <= To_Date('" + Convert.ToDateTime(pVencFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_VENCIMIENTO <= '" + Convert.ToDateTime(pVencFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " group by  c.codigo_factura,c.numero_factura,c.fecha_ingreso,c.fecha_factura, "
                            + "c.fecha_radicacion, c.fecha_vencimiento,c.doc_equivalente, c.cod_persona,p.identificacion, "
                            +"p.primer_nombre | | p.segundo_nombre | | "
                            +"p.primer_apellido | | p.segundo_apellido, "
                            +"case c.IDTIPO_CTA_POR_PAGAR when 1 then 'Factura' "
                            +"when 2 then 'Orden de Pago' when 3 then 'Orden de Compra' when 4 then 'Orden de Servicio' "
                            +"when 5 then 'Contrato de Servicio' end,case c.MANEJA_DESCUENTOS when 0 then 'NO' when 1 then 'SI' end, "
                            +"case c.MANEJA_ANTICIPOS when 0 then 'NO' when 1 then 'SI' end, "
                            + " case c.estado when 0 then 'Pendiente' when  1 then 'Contabilizada' when 2 then 'Aprobada' when 3 then 'Pagada' when 4 then 'Anulada' end "
                            + "ORDER BY c.CODIGO_FACTURA  desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasPorPagar entidad = new CuentasPorPagar();
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["NUMERO_FACTURA"] != DBNull.Value) entidad.numero_factura = Convert.ToString(resultado["NUMERO_FACTURA"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fecha_factura = Convert.ToDateTime(resultado["FECHA_FACTURA"]);
                            if (resultado["FECHA_RADICACION"] != DBNull.Value) entidad.fecha_radicacion = Convert.ToDateTime(resultado["FECHA_RADICACION"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            //if (resultado["IDTIPO_CTA_POR_PAGAR"] != DBNull.Value) entidad.idtipo_cta_por_pagar = Convert.ToInt32(resultado["IDTIPO_CTA_POR_PAGAR"]);
                            if (resultado["DOC_EQUIVALENTE"] != DBNull.Value) entidad.doc_equivalente = Convert.ToDecimal(resultado["DOC_EQUIVALENTE"]);
                            //if (resultado["NUM_CONTRATO"] != DBNull.Value) entidad.num_contrato = Convert.ToString(resultado["NUM_CONTRATO"]);
                            //if (resultado["POLIZA"] != DBNull.Value) entidad.poliza = Convert.ToString(resultado["POLIZA"]);
                            //if (resultado["VENCE_CONTRATO"] != DBNull.Value) entidad.vence_contrato = Convert.ToDateTime(resultado["VENCE_CONTRATO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            //if (resultado["MANEJA_DESCUENTOS"] != DBNull.Value) entidad.maneja_descuentos = Convert.ToInt32(resultado["MANEJA_DESCUENTOS"]);
                            //if (resultado["MANEJA_ANTICIPOS"] != DBNull.Value) entidad.maneja_anticipos = Convert.ToInt32(resultado["MANEJA_ANTICIPOS"]);
                            //if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            //if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["ESTADO"]);
                            //if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            //if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPONOMBRE"] != DBNull.Value) entidad.tiponom = Convert.ToString(resultado["TIPONOMBRE"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToDecimal(resultado["VALORTOTAL"]);
                            if (resultado["VALORNETO"] != DBNull.Value) entidad.valorneto = Convert.ToDecimal(resultado["VALORNETO"]);
                            if (resultado["MANEJADSCTO"] != DBNull.Value) entidad.manejadscto = Convert.ToString(resultado["MANEJADSCTO"]);
                            if (resultado["MANEJAANTI"] != DBNull.Value) entidad.manejaanti = Convert.ToString(resultado["MANEJAANTI"]);
                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ListarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }





        public List<CuentasPorPagar> ListarAnticipos(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario, String filtro)
        {
            DbDataReader resultado;
            List<CuentasPorPagar> lstCuentas = new List<CuentasPorPagar>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"  SELECT n.*,c.codigo_factura, c.fecha_factura, p.identificacion, Decode(tipo_persona, 'N', Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' ||  primer_apellido || ' ' || segundo_apellido, 0, 240)), razon_social) As nombre,  c.valor_neto, c.saldo   FROM cuentaxpagar_anticipo n INNER JOIN CUENTASXPAGAR c ON n.codigo_factura = c.codigo_factura LEFT JOIN persona p on p.cod_persona = c.cod_persona "+filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasPorPagar entidad = new CuentasPorPagar();
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fecha_factura = Convert.ToDateTime(resultado["FECHA_FACTURA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valorneto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["IDANTICIPO"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["IDANTICIPO"]);
                            if (resultado["FECHA_ANTICIPO"] != DBNull.Value) entidad.fecha_radicacion = Convert.ToDateTime(resultado["FECHA_ANTICIPO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ListarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }



        public List<CuentaXpagar_Detalle> ConsultarDetalleCuentasXpagar(CuentaXpagar_Detalle pCuentas, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentaXpagar_Detalle> lstDetalle = new List<CuentaXpagar_Detalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUENTAXPAGAR_DETALLE WHERE CODIGO_FACTURA = " + pCuentas.codigo_factura+ " ORDER BY CODDETALLEFAC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentaXpagar_Detalle entidad = new CuentaXpagar_Detalle();
                            if (resultado["CODDETALLEFAC"] != DBNull.Value) entidad.coddetallefac = Convert.ToInt32(resultado["CODDETALLEFAC"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) entidad.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToInt32(resultado["CANTIDAD"]);
                            if (resultado["VALOR_UNITARIO"] != DBNull.Value) entidad.valor_unitario = Convert.ToDecimal(resultado["VALOR_UNITARIO"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["PORC_DESCUENTO"] != DBNull.Value) entidad.porc_descuento = Convert.ToDecimal(resultado["PORC_DESCUENTO"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            lstDetalle.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarDetalleCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }


        public List<CuentaXpagar_Pago> ConsultarDetalleFormaPago(CuentaXpagar_Pago pCuentas, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentaXpagar_Pago> lstCuentas = new List<CuentaXpagar_Pago>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CUENTAXPAGAR_PAGO WHERE CODIGO_FACTURA = " + pCuentas.codigo_factura + " ORDER BY CODPAGOFAC ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentaXpagar_Pago entidad = new CuentaXpagar_Pago();
                            if (resultado["CODPAGOFAC"] != DBNull.Value) entidad.codpagofac = Convert.ToInt32(resultado["CODPAGOFAC"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["NUMERO"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["NUMERO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["PORC_DESCUENTO"] != DBNull.Value) entidad.porc_descuento = Convert.ToDecimal(resultado["PORC_DESCUENTO"]);
                            if (resultado["VALOR_DESCUENTO"] != DBNull.Value) entidad.valor_descuento = Convert.ToDecimal(resultado["VALOR_DESCUENTO"]);
                            if (resultado["FECHA_DESCUENTO"] != DBNull.Value) entidad.fecha_descuento = Convert.ToDateTime(resultado["FECHA_DESCUENTO"]);
                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarDetalleFormaPago", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasPorPagar CONSULTARANTICIPOS (CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasPorPagar entidad = new CuentasPorPagar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT n.*,c.cod_persona,p.tipo_identificacion,c.codigo_factura, c.fecha_factura, p.identificacion, Decode(tipo_persona, 'N', Trim(Substr(primer_nombre || ' ' || segundo_nombre || ' ' ||  primer_apellido || ' ' || segundo_apellido, 0, 240)), razon_social) As nombre,  c.valor_neto, c.saldo   FROM cuentaxpagar_anticipo n INNER JOIN CUENTASXPAGAR c ON n.codigo_factura = c.codigo_factura LEFT JOIN persona p on p.cod_persona = c.cod_persona where c.codigo_factura= " + pCuentas.codigo_factura + " ORDER BY idanticipo ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.idtipo_cta_por_pagar = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fecha_factura = Convert.ToDateTime(resultado["FECHA_FACTURA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valorneto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["IDANTICIPO"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["IDANTICIPO"]);
                            if (resultado["FECHA_ANTICIPO"] != DBNull.Value) entidad.fecha_radicacion = Convert.ToDateTime(resultado["FECHA_ANTICIPO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }




        public CuentasPorPagar ConsultarCuentasXpagar(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasPorPagar entidad = new CuentasPorPagar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CUENTASXPAGAR.*,persona.identificacion,persona.primer_nombre | | persona.segundo_nombre | | persona.primer_apellido | | persona.segundo_apellido as nombre "
                                       + "FROM CUENTASXPAGAR left join persona "
                                       + "on persona.cod_persona = cuentasxpagar.cod_persona where CODIGO_FACTURA = " + pCuentas.codigo_factura + " ORDER BY CODIGO_FACTURA ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["NUMERO_FACTURA"] != DBNull.Value) entidad.numero_factura = Convert.ToString(resultado["NUMERO_FACTURA"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fec_fact = Convert.ToDateTime(resultado["FECHA_FACTURA"]); else entidad.fecha_factura = DateTime.MinValue;
                            if (resultado["FECHA_RADICACION"] != DBNull.Value) entidad.fec_radi = Convert.ToDateTime(resultado["FECHA_RADICACION"]); else entidad.fecha_radicacion = DateTime.MinValue;
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["IDTIPO_CTA_POR_PAGAR"] != DBNull.Value) entidad.idtipo_cta_por_pagar = Convert.ToInt32(resultado["IDTIPO_CTA_POR_PAGAR"]);
                            if (resultado["DOC_EQUIVALENTE"] != DBNull.Value) entidad.doc_equivalente = Convert.ToDecimal(resultado["DOC_EQUIVALENTE"]);
                            if (resultado["NUM_CONTRATO"] != DBNull.Value) entidad.num_contrato = Convert.ToString(resultado["NUM_CONTRATO"]);
                            if (resultado["POLIZA"] != DBNull.Value) entidad.poliza = Convert.ToString(resultado["POLIZA"]);
                            if (resultado["VENCE_CONTRATO"] != DBNull.Value) entidad.vence_contrato = Convert.ToDateTime(resultado["VENCE_CONTRATO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["MANEJA_DESCUENTOS"] != DBNull.Value) entidad.maneja_descuentos = Convert.ToInt32(resultado["MANEJA_DESCUENTOS"]);
                            if (resultado["MANEJA_ANTICIPOS"] != DBNull.Value) entidad.maneja_anticipos = Convert.ToInt32(resultado["MANEJA_ANTICIPOS"]);
                            if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if(entidad.estado == 1)
                            {
                                entidad.nomestado = Convert.ToString("Contabilizada");
                            }
                            if (entidad.estado == 2)
                            {
                                entidad.nomestado = Convert.ToString("Aprobada");
                            }
                            if (entidad.estado == 3)
                            {
                                entidad.nomestado = Convert.ToString("Pagada");
                            }

                            if (entidad.estado == 4)
                            {
                                entidad.nomestado = Convert.ToString("Anulada");
                            }
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);

                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt32(resultado["SALDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.formapago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasPorPagar ConsultarDatosReporte(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasPorPagar entidad = new CuentasPorPagar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.CODIGO_FACTURA,C.NUMERO_FACTURA,C.FECHA_INGRESO,C.FECHA_VENCIMIENTO,C.COD_PERSONA,P.IDENTIFICACION, "
                                        +"P.PRIMER_NOMBRE | | P.SEGUNDO_NOMBRE | | P.PRIMER_APELLIDO | | P.SEGUNDO_APELLIDO AS NOMBRE, "
                                        +"SUM(D.VALOR_TOTAL) AS VALOR_TOTAL,SUM(D.PORC_DESCUENTO) AS PORC_DESCUENTO,SUM(D.PORC_IVA) AS PORC_IVA, "
                                        +"SUM(D.PORC_RETENCION) AS PORC_RETENCION,SUM(D.PORC_RETEIVA) AS PORC_RETEIVA, 0 AS PORC_TIMBRE,SUM(D.VALOR_NETO) AS VALOR_NETO "
                                        +"FROM CUENTASXPAGAR C INNER JOIN CUENTAXPAGAR_DETALLE D "
                                        +"ON C.CODIGO_FACTURA = D.CODIGO_FACTURA "
                                        +"INNER JOIN PERSONA P "
                                        +"ON P.COD_PERSONA = C.COD_PERSONA "
                                        + "WHERE C.CODIGO_FACTURA = " + pCuentas.codigo_factura + " GROUP BY C.CODIGO_FACTURA,C.NUMERO_FACTURA,C.FECHA_INGRESO,C.FECHA_VENCIMIENTO,C.COD_PERSONA,P.IDENTIFICACION, "
                                        +"P.PRIMER_NOMBRE | | P.SEGUNDO_NOMBRE | | P.PRIMER_APELLIDO | | P.SEGUNDO_APELLIDO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["NUMERO_FACTURA"] != DBNull.Value) entidad.numero_factura = Convert.ToString(resultado["NUMERO_FACTURA"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            //if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fec_fact = Convert.ToDateTime(resultado["FECHA_FACTURA"]); else entidad.fecha_factura = DateTime.MinValue;
                            //if (resultado["FECHA_RADICACION"] != DBNull.Value) entidad.fec_radi = Convert.ToDateTime(resultado["FECHA_RADICACION"]); else entidad.fecha_radicacion = DateTime.MinValue;
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            //if (resultado["IDTIPO_CTA_POR_PAGAR"] != DBNull.Value) entidad.idtipo_cta_por_pagar = Convert.ToInt32(resultado["IDTIPO_CTA_POR_PAGAR"]);
                            //if (resultado["DOC_EQUIVALENTE"] != DBNull.Value) entidad.doc_equivalente = Convert.ToDecimal(resultado["DOC_EQUIVALENTE"]);
                            //if (resultado["NUM_CONTRATO"] != DBNull.Value) entidad.num_contrato = Convert.ToString(resultado["NUM_CONTRATO"]);
                            //if (resultado["POLIZA"] != DBNull.Value) entidad.poliza = Convert.ToString(resultado["POLIZA"]);
                            //if (resultado["VENCE_CONTRATO"] != DBNull.Value) entidad.vence_contrato = Convert.ToDateTime(resultado["VENCE_CONTRATO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["PORC_DESCUENTO"] != DBNull.Value) entidad.porc_descuento = Convert.ToDecimal(resultado["PORC_DESCUENTO"]);
                            if (resultado["PORC_IVA"] != DBNull.Value) entidad.porc_iva = Convert.ToDecimal(resultado["PORC_IVA"]);
                            if (resultado["PORC_RETENCION"] != DBNull.Value) entidad.porc_retencion = Convert.ToDecimal(resultado["PORC_RETENCION"]);
                            if (resultado["PORC_RETEIVA"] != DBNull.Value) entidad.porc_reteiva = Convert.ToDecimal(resultado["PORC_RETEIVA"]);
                            if (resultado["PORC_TIMBRE"] != DBNull.Value) entidad.porc_timbre = Convert.ToDecimal(resultado["PORC_TIMBRE"]);
                            if (resultado["VALOR_NETO"] != DBNull.Value) entidad.valor_neto = Convert.ToDecimal(resultado["VALOR_NETO"]);
                            //if (resultado["MANEJA_DESCUENTOS"] != DBNull.Value) entidad.maneja_descuentos = Convert.ToInt32(resultado["MANEJA_DESCUENTOS"]);
                            //if (resultado["MANEJA_ANTICIPOS"] != DBNull.Value) entidad.maneja_anticipos = Convert.ToInt32(resultado["MANEJA_ANTICIPOS"]);
                            //if (resultado["VALOR_ANTICIPO"] != DBNull.Value) entidad.valor_anticipo = Convert.ToDecimal(resultado["VALOR_ANTICIPO"]);
                            //if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            //if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            //if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            //if (resultado["FECHA_CREA"] != DBNull.Value) entidad.fecha_crea = Convert.ToDateTime(resultado["FECHA_CREA"]);                           
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarDatosReporte", ex);
                        return null;
                    }
                }
            }
        }


        public CuentaXpagar_Detalle CrearCuentasxPagarDetalle(CuentaXpagar_Detalle pCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcoddetallefac = cmdTransaccionFactory.CreateParameter();
                        pcoddetallefac.ParameterName = "p_coddetallefac";
                        pcoddetallefac.Value = pCuentas.coddetallefac;
                        pcoddetallefac.Direction = ParameterDirection.Output;
                        pcoddetallefac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetallefac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        pcod_concepto_fac.Value = pCuentas.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        pdetalle.Value = pCuentas.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pCuentas.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pcantidad = cmdTransaccionFactory.CreateParameter();
                        pcantidad.ParameterName = "p_cantidad";
                        pcantidad.Value = pCuentas.cantidad;
                        pcantidad.Direction = ParameterDirection.Input;
                        pcantidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcantidad);

                        DbParameter pvalor_unitario = cmdTransaccionFactory.CreateParameter();
                        pvalor_unitario.ParameterName = "p_valor_unitario";
                        pvalor_unitario.Value = pCuentas.valor_unitario;
                        pvalor_unitario.Direction = ParameterDirection.Input;
                        pvalor_unitario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_unitario);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pCuentas.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pporc_descuento = cmdTransaccionFactory.CreateParameter();
                        pporc_descuento.ParameterName = "p_porc_descuento";
                        if (pCuentas.porc_descuento != null) pporc_descuento.Value = pCuentas.porc_descuento; else pporc_descuento.Value = DBNull.Value;
                        pporc_descuento.Direction = ParameterDirection.Input;
                        pporc_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporc_descuento);

                        DbParameter pvalor_neto = cmdTransaccionFactory.CreateParameter();
                        pvalor_neto.ParameterName = "p_valor_neto";
                        pvalor_neto.Value = pCuentas.valor_neto;
                        pvalor_neto.Direction = ParameterDirection.Input;
                        pvalor_neto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_neto);

                        DbParameter pvalor_impuestos = cmdTransaccionFactory.CreateParameter();
                        pvalor_impuestos.ParameterName = "p_valor_impuestos";
                        pvalor_impuestos.Value = pCuentas.valor_impuesto;
                        pvalor_impuestos.Direction = ParameterDirection.Input;
                        pvalor_impuestos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_impuestos);




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPDETAL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCuentas.coddetallefac = Convert.ToInt32(pcoddetallefac.Value);
                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "CrearCuentasxPagarDetalle", ex);
                        return null;
                    }
                }
            }
        }


        public CuentaXpagar_Detalle ModificarCuentasxPagarDetalle(CuentaXpagar_Detalle pCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcoddetallefac = cmdTransaccionFactory.CreateParameter();
                        pcoddetallefac.ParameterName = "p_coddetallefac";
                        pcoddetallefac.Value = pCuentas.coddetallefac;
                        pcoddetallefac.Direction = ParameterDirection.Input;
                        pcoddetallefac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetallefac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        pcod_concepto_fac.Value = pCuentas.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pdetalle = cmdTransaccionFactory.CreateParameter();
                        pdetalle.ParameterName = "p_detalle";
                        pdetalle.Value = pCuentas.detalle;
                        pdetalle.Direction = ParameterDirection.Input;
                        pdetalle.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdetalle);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pCuentas.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        pcentro_costo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pcantidad = cmdTransaccionFactory.CreateParameter();
                        pcantidad.ParameterName = "p_cantidad";
                        pcantidad.Value = pCuentas.cantidad;
                        pcantidad.Direction = ParameterDirection.Input;
                        pcantidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcantidad);

                        DbParameter pvalor_unitario = cmdTransaccionFactory.CreateParameter();
                        pvalor_unitario.ParameterName = "p_valor_unitario";
                        pvalor_unitario.Value = pCuentas.valor_unitario;
                        pvalor_unitario.Direction = ParameterDirection.Input;
                        pvalor_unitario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_unitario);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pCuentas.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pporc_descuento = cmdTransaccionFactory.CreateParameter();
                        pporc_descuento.ParameterName = "p_porc_descuento";
                        if (pCuentas.porc_descuento != null) pporc_descuento.Value = pCuentas.porc_descuento; else pporc_descuento.Value = DBNull.Value;
                        pporc_descuento.Direction = ParameterDirection.Input;
                        pporc_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporc_descuento);

                        DbParameter pvalor_neto = cmdTransaccionFactory.CreateParameter();
                        pvalor_neto.ParameterName = "p_valor_neto";
                        pvalor_neto.Value = pCuentas.valor_neto;
                        pvalor_neto.Direction = ParameterDirection.Input;
                        pvalor_neto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_neto);

                        DbParameter pvalor_impuestos = cmdTransaccionFactory.CreateParameter();
                        pvalor_impuestos.ParameterName = "p_valor_impuestos";
                        pvalor_impuestos.Value = pCuentas.valor_impuesto;
                        pvalor_impuestos.Direction = ParameterDirection.Input;
                        pvalor_impuestos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_impuestos);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPDETAL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarCuentasxPagarDetalle", ex);
                        return null;
                    }
                }
            }
        }



        public CuentaXpagar_Pago CrearCuentasXpagar_Pago(CuentaXpagar_Pago pCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodpagofac = cmdTransaccionFactory.CreateParameter();
                        pcodpagofac.ParameterName = "p_codpagofac";
                        pcodpagofac.Value = pCuentas.codpagofac;
                        pcodpagofac.Direction = ParameterDirection.Output;
                        pcodpagofac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodpagofac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pnumero = cmdTransaccionFactory.CreateParameter();
                        pnumero.ParameterName = "p_numero";
                        pnumero.Value = pCuentas.numero;
                        pnumero.Direction = ParameterDirection.Input;
                        pnumero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCuentas.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pCuentas.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCuentas.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pporc_descuento = cmdTransaccionFactory.CreateParameter();
                        pporc_descuento.ParameterName = "p_porc_descuento";
                        if (pCuentas.porc_descuento != null) pporc_descuento.Value = pCuentas.porc_descuento; else pporc_descuento.Value = DBNull.Value;
                        pporc_descuento.Direction = ParameterDirection.Input;
                        pporc_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporc_descuento);

                        DbParameter pvalor_descuento = cmdTransaccionFactory.CreateParameter();
                        pvalor_descuento.ParameterName = "p_valor_descuento";
                        if (pCuentas.valor_descuento != null) pvalor_descuento.Value = pCuentas.valor_descuento; else pvalor_descuento.Value = DBNull.Value;
                        pvalor_descuento.Direction = ParameterDirection.Input;
                        pvalor_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_descuento);

                        DbParameter pfecha_descuento = cmdTransaccionFactory.CreateParameter();
                        pfecha_descuento.ParameterName = "p_fecha_descuento";
                        if (pCuentas.fecha_descuento != null) pfecha_descuento.Value = pCuentas.fecha_descuento; else pfecha_descuento.Value = DBNull.Value;
                        pfecha_descuento.Direction = ParameterDirection.Input;
                        pfecha_descuento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_descuento);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCuentas.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPFORMA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCuentas.codpagofac = Convert.ToInt32(pcodpagofac.Value);
                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "CrearCuentasXpagar_Pago", ex);
                        return null;
                    }
                }
            }
        }


        public CuentaXpagar_Pago ModificarCuentasXpagar_Pago(CuentaXpagar_Pago pCuentas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodpagofac = cmdTransaccionFactory.CreateParameter();
                        pcodpagofac.ParameterName = "p_codpagofac";
                        pcodpagofac.Value = pCuentas.codpagofac;
                        pcodpagofac.Direction = ParameterDirection.Input;
                        pcodpagofac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodpagofac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pCuentas.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pnumero = cmdTransaccionFactory.CreateParameter();
                        pnumero.ParameterName = "p_numero";
                        pnumero.Value = pCuentas.numero;
                        pnumero.Direction = ParameterDirection.Input;
                        pnumero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pCuentas.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pCuentas.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pCuentas.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pporc_descuento = cmdTransaccionFactory.CreateParameter();
                        pporc_descuento.ParameterName = "p_porc_descuento";
                        if (pCuentas.porc_descuento != null) pporc_descuento.Value = pCuentas.porc_descuento; else pporc_descuento.Value = DBNull.Value;
                        pporc_descuento.Direction = ParameterDirection.Input;
                        pporc_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporc_descuento);

                        DbParameter pvalor_descuento = cmdTransaccionFactory.CreateParameter();
                        pvalor_descuento.ParameterName = "p_valor_descuento";
                        if (pCuentas.valor_descuento != null) pvalor_descuento.Value = pCuentas.valor_descuento; else pvalor_descuento.Value = DBNull.Value;
                        pvalor_descuento.Direction = ParameterDirection.Input;
                        pvalor_descuento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_descuento);

                        DbParameter pfecha_descuento = cmdTransaccionFactory.CreateParameter();
                        pfecha_descuento.ParameterName = "p_fecha_descuento";
                        if (pCuentas.fecha_descuento != null) pfecha_descuento.Value = pCuentas.fecha_descuento; else pfecha_descuento.Value = DBNull.Value;
                        pfecha_descuento.Direction = ParameterDirection.Input;
                        pfecha_descuento.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_descuento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPFORMA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarCuentasXpagar_Pago", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCuentasXpagarDetalles(Int32 pId, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigoDetalle";
                        pcodigo.Value = pId;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter popcion = cmdTransaccionFactory.CreateParameter();
                        popcion.ParameterName = "p_opcion";
                        popcion.Value = opcion;
                        popcion.Direction = ParameterDirection.Input;
                        popcion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(popcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPDETAL_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "EliminarCuentasXpagar", ex);
                    }
                }
            }
        }


        public CuentasPorPagar ConsultarGiro(CuentasPorPagar pOperacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasPorPagar entidad = new CuentasPorPagar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select v_giro.cod_persona,v_giro.forma_pago,v_giro.idctabancaria,v_giro.num_referencia,v_giro.cod_banco,v_giro.tipo_cuenta,v_giro.NUM_REFERENCIA1,v_giro.COD_BANCO1,cxp.codigo_factura from v_giro left join CUENTASXPAGAR cxp on  cxp.cod_ope=v_giro.cod_ope where v_giro.cod_ope =" + pOperacion.cod_ope + " ORDER BY 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["forma_pago"]);
                            if (resultado["idctabancaria"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["idctabancaria"]);
                            if (resultado["num_referencia"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["num_referencia"]);
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["cod_banco"]);
                            if (resultado["tipo_cuenta"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["tipo_cuenta"]);
                            if (resultado["num_referencia1"] != DBNull.Value) entidad.num_cuenta_destino = Convert.ToString(resultado["num_referencia1"]);
                            if (resultado["cod_banco1"] != DBNull.Value) entidad.cod_bancodestino = Convert.ToInt32(resultado["cod_banco1"]);
                            if (resultado["codigo_factura"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["codigo_factura"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarGiro", ex);
                        return null;
                    }
                }
            }
        }

        public CuentasPorPagar ConsultarGiroXfactura(CuentasPorPagar pCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasPorPagar entidad = new CuentasPorPagar();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select v_giro.cod_persona,v_giro.forma_pago,v_giro.idctabancaria,v_giro.num_referencia,v_giro.cod_banco,v_giro.tipo_cuenta,v_giro.NUM_REFERENCIA1,v_giro.COD_BANCO1,cxp.codigo_factura from v_giro left join CUENTASXPAGAR cxp on  cxp.cod_ope=v_giro.cod_ope where cxp.codigo_factura =" + pCuenta.codigo_factura + " ORDER BY 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_PERSONA"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["forma_pago"]);
                            if (resultado["idctabancaria"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["idctabancaria"]);
                            if (resultado["num_referencia"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["num_referencia"]);
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["cod_banco"]);
                            if (resultado["tipo_cuenta"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["tipo_cuenta"]);
                            if (resultado["num_referencia1"] != DBNull.Value) entidad.num_cuenta_destino = Convert.ToString(resultado["num_referencia1"]);
                            if (resultado["cod_banco1"] != DBNull.Value) entidad.cod_bancodestino = Convert.ToInt32(resultado["cod_banco1"]);
                            if (resultado["codigo_factura"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["codigo_factura"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarGiro", ex);
                        return null;
                    }
                }
            }
        }



        #region DATOS DE LOS IMPUESTOS POR CADA CONCEPTO

        public CuentasXpagarImpuesto CrearCuentaXpagarImpuesto(CuentasXpagarImpuesto pDetaImp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcoddetalleimp = cmdTransaccionFactory.CreateParameter();
                        pcoddetalleimp.ParameterName = "p_coddetalleimp";
                        pcoddetalleimp.Value = pDetaImp.coddetalleimp;
                        pcoddetalleimp.Direction = ParameterDirection.Output;
                        pcoddetalleimp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetalleimp);

                        DbParameter pcoddetallefac = cmdTransaccionFactory.CreateParameter();
                        pcoddetallefac.ParameterName = "p_coddetallefac";
                        pcoddetallefac.Value = pDetaImp.coddetallefac;
                        pcoddetallefac.Direction = ParameterDirection.Input;
                        pcoddetallefac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetallefac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pDetaImp.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pDetaImp.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pDetaImp.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pDetaImp.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pbase = cmdTransaccionFactory.CreateParameter();
                        pbase.ParameterName = "p_base";
                        if (pDetaImp.base_vr == null)
                            pbase.Value = DBNull.Value;
                        else
                            pbase.Value = pDetaImp.base_vr;
                        pbase.Direction = ParameterDirection.Input;
                        pbase.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pDetaImp.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pDetaImp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);


                        DbParameter P_ID_CUENTA_IMP = cmdTransaccionFactory.CreateParameter();
                        P_ID_CUENTA_IMP.ParameterName = "P_ID_CUENTA_IMP";
                        if (pDetaImp.idcuentadetalleimp == null)
                            P_ID_CUENTA_IMP.Value = DBNull.Value;
                        else
                            P_ID_CUENTA_IMP.Value = pDetaImp.idcuentadetalleimp;
                        P_ID_CUENTA_IMP.Direction = ParameterDirection.Input;
                       // P_ID_CUENTA_IMP.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_CUENTA_IMP);

                        



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAIMPXCON_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDetaImp.coddetalleimp = Convert.ToInt32(pcoddetalleimp.Value);
                        return pDetaImp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "CrearCuentaXpagarImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasXpagarImpuesto ModificarCuentaXpagarImpuesto(CuentasXpagarImpuesto pDetaImp, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcoddetalleimp = cmdTransaccionFactory.CreateParameter();
                        pcoddetalleimp.ParameterName = "p_coddetalleimp";
                        pcoddetalleimp.Value = pDetaImp.coddetalleimp;
                        pcoddetalleimp.Direction = ParameterDirection.Input;
                        pcoddetalleimp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetalleimp);

                        DbParameter pcoddetallefac = cmdTransaccionFactory.CreateParameter();
                        pcoddetallefac.ParameterName = "p_coddetallefac";
                        pcoddetallefac.Value = pDetaImp.coddetallefac;
                        pcoddetallefac.Direction = ParameterDirection.Input;
                        pcoddetallefac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcoddetallefac);

                        DbParameter pcodigo_factura = cmdTransaccionFactory.CreateParameter();
                        pcodigo_factura.ParameterName = "p_codigo_factura";
                        pcodigo_factura.Value = pDetaImp.codigo_factura;
                        pcodigo_factura.Direction = ParameterDirection.Input;
                        pcodigo_factura.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_factura);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pDetaImp.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        if (pDetaImp.porcentaje == null)
                            pporcentaje.Value = DBNull.Value;
                        else
                            pporcentaje.Value = pDetaImp.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        DbParameter pbase = cmdTransaccionFactory.CreateParameter();
                        pbase.ParameterName = "p_base";
                        if (pDetaImp.base_vr == null)
                            pbase.Value = DBNull.Value;
                        else
                            pbase.Value = pDetaImp.base_vr;
                        pbase.Direction = ParameterDirection.Input;
                        pbase.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pDetaImp.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pDetaImp.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAIMPXCON_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pDetaImp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarCuentaXpagarImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public List<CuentasXpagarImpuesto> ConsultarDetImpuestosXConcepto(CuentasXpagarImpuesto pImpuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasXpagarImpuesto> lstImpuesto = new List<CuentasXpagarImpuesto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.*, T.DESCRIPCION, D.COD_CONCEPTO_FAC,CI.COD_CUENTA_IMP
                                        FROM CUENTAXPAGAR_IMPUESTO C 
                                        LEFT JOIN CUENTAXPAGAR_DETALLE D ON D.CODDETALLEFAC = C.CODDETALLEFAC
                                        LEFT JOIN CONCEPTO_CUENTAXPAGAR_IMP CI ON CI.IDCONCEPTOIMP = C.IDCONCEPTOIMP
                                        LEFT JOIN TIPOIMPUESTO T ON T.COD_TIPO_IMPUESTO = C.COD_TIPO_IMPUESTO " + ObtenerFiltro(pImpuesto, "C.") + " ORDER BY CODDETALLEIMP ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasXpagarImpuesto entidad = new CuentasXpagarImpuesto();
                            if (resultado["CODDETALLEIMP"] != DBNull.Value) entidad.coddetalleimp = Convert.ToInt32(resultado["CODDETALLEIMP"]);
                            if (resultado["CODDETALLEFAC"] != DBNull.Value) entidad.coddetallefac = Convert.ToInt32(resultado["CODDETALLEFAC"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["BASE"] != DBNull.Value) entidad.base_vr = Convert.ToDecimal(resultado["BASE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_tipo_impuesto = Convert.ToString(resultado["DESCRIPCION"]);
                            entidad.cod_cuenta = ConsultarCodCuentaImpuesto(0, entidad.cod_tipo_impuesto, vUsuario);
                            entidad.naturaleza = ConsultarNaturalezaCuenta(entidad.cod_cuenta, vUsuario);

                            if (resultado["IDCONCEPTOIMP"] != DBNull.Value) entidad.idcuentadetalleimp = Convert.ToInt32(resultado["IDCONCEPTOIMP"]);
                            if (resultado["COD_CUENTA_IMP"] != DBNull.Value) entidad.cod_cuenta_imp = Convert.ToString(resultado["COD_CUENTA_IMP"]);


                            lstImpuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ConsultarDetImpuestosXConcepto", ex);
                        return null;
                    }
                }
            }
        }

        public bool ConsultarCodCuenta(Int64 pCod_Concepto_Fac, ref string pCod_Cuenta, ref string pTipo_Mov, Usuario vUsuario)
        {
            DbDataReader resultado;
            pCod_Cuenta = "";
            pTipo_Mov = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_CUENTA, C.TIPO_MOV
                                        FROM CONCEPTO_CUENTAXPAGAR C
                                        WHERE C.COD_CONCEPTO_FAC = " + pCod_Concepto_Fac;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) pCod_Cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) pTipo_Mov = Convert.ToString(resultado["TIPO_MOV"]);
                            dbConnectionFactory.CerrarConexion(connection);
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }

        public string ConsultarCodCuentaImpuesto(Int64 pCod_Concepto_Fac, Int64 pCod_Tipo_Impuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            string sCod_Cuenta_Imp = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_CUENTA_IMP
                                        FROM CONCEPTO_CUENTAXPAGAR_IMP C
                                        WHERE C.COD_CONCEPTO_FAC = " + pCod_Concepto_Fac + " AND C.COD_TIPO_IMPUESTO = " + pCod_Tipo_Impuesto;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA_IMP"] != DBNull.Value) sCod_Cuenta_Imp = Convert.ToString(resultado["COD_CUENTA_IMP"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return sCod_Cuenta_Imp;
                    }
                    catch
                    {
                        return sCod_Cuenta_Imp;
                    }
                }
            }
        }

        public string ConsultarNaturalezaCuenta(string pCod_Cuenta_Imp, Usuario vUsuario)
        {
            DbDataReader resultado;
            string sNaturaleza = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.TIPO FROM PLAN_CUENTAS C WHERE C.COD_CUENTA = '" + pCod_Cuenta_Imp.Trim() + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO"] != DBNull.Value) sNaturaleza = Convert.ToString(resultado["TIPO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return sNaturaleza;
                    }
                    catch
                    {
                        return sNaturaleza;
                    }
                }
            }
        }

        #endregion


    }
}