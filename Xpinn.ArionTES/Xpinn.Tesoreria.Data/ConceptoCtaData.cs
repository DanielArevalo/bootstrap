using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class ConceptoCtaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ConceptoCtaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ConceptoCta CrearConceptoCta(ConceptoCta pConceptoCta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        pcod_concepto_fac.Value = pConceptoCta.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.InputOutput;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoCta.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pConceptoCta.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pConceptoCta.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pConceptoCta.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pConceptoCta.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pConceptoCta.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pConceptoCta.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter pcod_cuenta_desc = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_desc.ParameterName = "p_cod_cuenta_desc";
                        if (pConceptoCta.cod_cuenta_desc == null)
                            pcod_cuenta_desc.Value = DBNull.Value;
                        else
                            pcod_cuenta_desc.Value = pConceptoCta.cod_cuenta_desc;
                        pcod_cuenta_desc.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_desc);

                        DbParameter p_cod_cuenta_anti = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta_anti.ParameterName = "p_cod_cuenta_anti";
                        if (pConceptoCta.cod_cuenta_anticipos == null)
                            p_cod_cuenta_anti.Value = DBNull.Value;
                        else
                            p_cod_cuenta_anti.Value = pConceptoCta.cod_cuenta_anticipos;
                        p_cod_cuenta_anti.Direction = ParameterDirection.Input;
                        p_cod_cuenta_anti.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta_anti);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPCTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoCta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "CrearConceptoCta", ex);
                        return null;
                    }
                }
            }
        }


        public ConceptoCta ModificarConceptoCta(ConceptoCta pConceptoCta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        pcod_concepto_fac.Value = pConceptoCta.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoCta.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pConceptoCta.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pConceptoCta.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pConceptoCta.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pConceptoCta.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pConceptoCta.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pConceptoCta.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter pcod_cuenta_desc = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_desc.ParameterName = "p_cod_cuenta_desc";
                        if (pConceptoCta.cod_cuenta_desc == null)
                            pcod_cuenta_desc.Value = DBNull.Value;
                        else
                            pcod_cuenta_desc.Value = pConceptoCta.cod_cuenta_desc;
                        pcod_cuenta_desc.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_desc);


                        DbParameter p_cod_cuenta_anti = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta_anti.ParameterName = "p_cod_cuenta_anti";
                        if (pConceptoCta.cod_cuenta_anticipos == null)
                            p_cod_cuenta_anti.Value = DBNull.Value;
                        else
                            p_cod_cuenta_anti.Value = pConceptoCta.cod_cuenta_anticipos;
                        p_cod_cuenta_anti.Direction = ParameterDirection.Input;
                        p_cod_cuenta_anti.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta_anti);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPCTA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoCta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ModificarConceptoCta", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarConceptoCta(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ConceptoCta pConceptoCta = new ConceptoCta();
                        pConceptoCta = ConsultarConceptoCta(pId, vUsuario);

                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        pcod_concepto_fac.Value = pConceptoCta.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCEPCTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "EliminarConceptoCta", ex);
                    }
                }
            }
        }


        public ConceptoCta ConsultarConceptoCta(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConceptoCta entidad = new ConceptoCta();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONCEPTO_CUENTAXPAGAR WHERE COD_CONCEPTO_FAC = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) entidad.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["COD_CUENTA_DESC"] != DBNull.Value) entidad.cod_cuenta_desc = Convert.ToString(resultado["COD_CUENTA_DESC"]);
                            if (resultado["COD_CUENTA_ANTIC"] != DBNull.Value) entidad.cod_cuenta_anticipos = Convert.ToString(resultado["COD_CUENTA_ANTIC"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ConsultarConceptoCta", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConceptoCta> ListarConceptoCta(ConceptoCta pConceptoCta, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConceptoCta> lstConceptoCta = new List<ConceptoCta>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONCEPTO_CUENTAXPAGAR.*, CASE TIPO_MOV WHEN 1 THEN 'DÉBITO' WHEN 2 THEN 'CRÉDITO' END AS NOM_TIPO_MOV FROM CONCEPTO_CUENTAXPAGAR " + ObtenerFiltro(pConceptoCta) + " ORDER BY COD_CONCEPTO_FAC ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConceptoCta entidad = new ConceptoCta();
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) entidad.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["COD_CUENTA_ANTIC"] != DBNull.Value) entidad.cod_cuenta_anticipos = Convert.ToString(resultado["COD_CUENTA_ANTIC"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["NOM_TIPO_MOV"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["NOM_TIPO_MOV"]);
                            lstConceptoCta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConceptoCta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ListarConceptoCta", ex);
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
                        string sql = "SELECT MAX(cod_concepto_fac) + 1 FROM CONCEPTO_CUENTAXPAGAR ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }



        public Concepto_CuentasXpagarImp CrearConcepto_CtaXpagarImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconceptoimp = cmdTransaccionFactory.CreateParameter();
                        pidconceptoimp.ParameterName = "p_idconceptoimp";
                        pidconceptoimp.Value = pImpuesto.idconceptoimp;
                        pidconceptoimp.Direction = ParameterDirection.Output;
                        pidconceptoimp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconceptoimp);

                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        if (pImpuesto.cod_concepto_fac == null)
                            pcod_concepto_fac.Value = DBNull.Value;
                        else
                            pcod_concepto_fac.Value = pImpuesto.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        if (pImpuesto.cod_tipo_impuesto == null)
                            pcod_tipo_impuesto.Value = DBNull.Value;
                        else
                            pcod_tipo_impuesto.Value = pImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        if (pImpuesto.porcentaje_impuesto == null)
                            pporcentaje_impuesto.Value = DBNull.Value;
                        else
                            pporcentaje_impuesto.Value = pImpuesto.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pImpuesto.base_minima == null)
                            pbase_minima.Value = DBNull.Value;
                        else
                            pbase_minima.Value = pImpuesto.base_minima;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcod_cuenta_imp = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_imp.ParameterName = "p_cod_cuenta_imp";
                        if (pImpuesto.cod_cuenta_imp == null)
                            pcod_cuenta_imp.Value = DBNull.Value;
                        else
                            pcod_cuenta_imp.Value = pImpuesto.cod_cuenta_imp;
                        pcod_cuenta_imp.Direction = ParameterDirection.Input;
                        pcod_cuenta_imp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_imp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGIMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pImpuesto.idconceptoimp = Convert.ToInt32(pidconceptoimp.Value);
                        return pImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "CrearConcepto_CtaXpagarImpuesto", ex);
                        return null;
                    }
                }
            }
        }

        public Concepto_CuentasXpagarImp ModificarConcepto_CtaXpagarImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconceptoimp = cmdTransaccionFactory.CreateParameter();
                        pidconceptoimp.ParameterName = "p_idconceptoimp";
                        pidconceptoimp.Value = pImpuesto.idconceptoimp;
                        pidconceptoimp.Direction = ParameterDirection.Input;
                        pidconceptoimp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconceptoimp);

                        DbParameter pcod_concepto_fac = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto_fac.ParameterName = "p_cod_concepto_fac";
                        if (pImpuesto.cod_concepto_fac == null)
                            pcod_concepto_fac.Value = DBNull.Value;
                        else
                            pcod_concepto_fac.Value = pImpuesto.cod_concepto_fac;
                        pcod_concepto_fac.Direction = ParameterDirection.Input;
                        pcod_concepto_fac.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto_fac);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        if (pImpuesto.cod_tipo_impuesto == null)
                            pcod_tipo_impuesto.Value = DBNull.Value;
                        else
                            pcod_tipo_impuesto.Value = pImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        if (pImpuesto.porcentaje_impuesto == null)
                            pporcentaje_impuesto.Value = DBNull.Value;
                        else
                            pporcentaje_impuesto.Value = pImpuesto.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pImpuesto.base_minima == null)
                            pbase_minima.Value = DBNull.Value;
                        else
                            pbase_minima.Value = pImpuesto.base_minima;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcod_cuenta_imp = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_imp.ParameterName = "p_cod_cuenta_imp";
                        if (pImpuesto.cod_cuenta_imp == null)
                            pcod_cuenta_imp.Value = DBNull.Value;
                        else
                            pcod_cuenta_imp.Value = pImpuesto.cod_cuenta_imp;
                        pcod_cuenta_imp.Direction = ParameterDirection.Input;
                        pcod_cuenta_imp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_imp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGIMP_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ModificarConcepto_CtaXpagarImpuesto", ex);
                        return null;
                    }
                }
            }
        }


        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();
            string separador_decimal = "";
            string formato_fecha = "";
            ParametrosBaseDatos(ref separador_decimal, ref formato_fecha, vUsuario);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONCEPTO_CUENTAXPAGAR_IMP.*, TIPOIMPUESTO.NOMBRE_IMPUESTO, PLAN_CUENTAS.TIPO 
                                        FROM CONCEPTO_CUENTAXPAGAR_IMP 
                                        LEFT JOIN TIPOIMPUESTO ON CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO = TIPOIMPUESTO.COD_TIPO_IMPUESTO
                                        LEFT JOIN PLAN_CUENTAS ON CONCEPTO_CUENTAXPAGAR_IMP.COD_CUENTA_IMP = PLAN_CUENTAS.COD_CUENTA " + ObtenerFiltro(pImpuesto, "CONCEPTO_CUENTAXPAGAR_IMP.", true, separador_decimal) + " ORDER BY CONCEPTO_CUENTAXPAGAR_IMP.IDCONCEPTOIMP ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Concepto_CuentasXpagarImp entidad = new Concepto_CuentasXpagarImp();
                            if (resultado["IDCONCEPTOIMP"] != DBNull.Value) entidad.idconceptoimp = Convert.ToInt32(resultado["IDCONCEPTOIMP"]);
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) entidad.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["COD_CUENTA_IMP"] != DBNull.Value) entidad.cod_cuenta_imp = Convert.ToString(resultado["COD_CUENTA_IMP"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.naturaleza = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nom_tipo_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            lstImpuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ListarConceptoImpuesto", ex);
                        return null;
                    }
                }
            }
        }

        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalle(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        
                        string sql = @"SELECT DISTINCT CONCEPTO_CUENTAXPAGAR_IMP.COD_CONCEPTO_FAC, CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO, TIPOIMPUESTO.NOMBRE_IMPUESTO, CONCEPTO_CUENTAXPAGAR_IMP.IDCONCEPTOIMP
                                        FROM CONCEPTO_CUENTAXPAGAR_IMP
                                        LEFT JOIN TIPOIMPUESTO ON CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO = TIPOIMPUESTO.COD_TIPO_IMPUESTO " + ObtenerFiltro(pImpuesto, "CONCEPTO_CUENTAXPAGAR_IMP.", true);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Concepto_CuentasXpagarImp entidad = new Concepto_CuentasXpagarImp();
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) entidad.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) entidad.nom_tipo_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            if (resultado["IDCONCEPTOIMP"] != DBNull.Value) entidad.coddetalleimp = Convert.ToInt32(resultado["IDCONCEPTOIMP"]);

                            
                            if (entidad.cod_tipo_impuesto != null)
                            {
                                DbDataReader resultado1;
                                List<Concepto_CuentasXpagarImp> lstPorc = new List<Concepto_CuentasXpagarImp>();
                                sql = @"SELECT CONCEPTO_CUENTAXPAGAR_IMP.*, TIPOIMPUESTO.NOMBRE_IMPUESTO, PLAN_CUENTAS.TIPO 
                                        FROM CONCEPTO_CUENTAXPAGAR_IMP
                                        LEFT JOIN TIPOIMPUESTO ON CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO = TIPOIMPUESTO.COD_TIPO_IMPUESTO
                                        LEFT JOIN PLAN_CUENTAS ON CONCEPTO_CUENTAXPAGAR_IMP.COD_CUENTA_IMP = PLAN_CUENTAS.COD_CUENTA
                                        WHERE CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO = " + entidad.cod_tipo_impuesto + " AND CONCEPTO_CUENTAXPAGAR_IMP.COD_CONCEPTO_FAC = " +entidad.cod_concepto_fac;

                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sql + " AND CONCEPTO_CUENTAXPAGAR_IMP.COD_TIPO_IMPUESTO = "+ entidad.cod_tipo_impuesto;
                                resultado1 = cmdTransaccionFactory.ExecuteReader();

                                while (resultado1.Read())
                                {
                                    Concepto_CuentasXpagarImp impuesto = new Concepto_CuentasXpagarImp();
                                    if (resultado1["COD_TIPO_IMPUESTO"] != DBNull.Value) impuesto.cod_tipo_impuesto = Convert.ToInt32(resultado1["COD_TIPO_IMPUESTO"]);
                                    if (resultado1["NOMBRE_IMPUESTO"] != DBNull.Value) impuesto.nom_tipo_impuesto = Convert.ToString(resultado1["NOMBRE_IMPUESTO"]);
                                    if (resultado1["PORCENTAJE_IMPUESTO"] != DBNull.Value) impuesto.porcentaje_impuesto = Convert.ToDecimal(resultado1["PORCENTAJE_IMPUESTO"]);
                                    if (resultado1["BASE_MINIMA"] != DBNull.Value) impuesto.base_minima = Convert.ToDecimal(resultado1["BASE_MINIMA"]);
                                    if (resultado1["COD_CUENTA_IMP"] != DBNull.Value) impuesto.cod_cuenta_imp = Convert.ToString(resultado1["COD_CUENTA_IMP"]);
                                    if (resultado1["TIPO"] != DBNull.Value) impuesto.naturaleza = Convert.ToString(resultado1["TIPO"]);
                                    if (resultado1["IDCONCEPTOIMP"] != DBNull.Value) impuesto.coddetalleimp = Convert.ToInt32(resultado1["IDCONCEPTOIMP"]);

                                    lstPorc.Add(impuesto);
                                }
                                entidad.lstPorcentaje = lstPorc;
                            }
                            lstImpuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpuesto; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ListarConceptoImpuestoDetalle", ex);
                        return null;
                    }
                }
            }
        }
        public List<Concepto_CuentasXpagarImp> ListarConceptoImpuestoDetalleCxp(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Concepto_CuentasXpagarImp> lstImpuesto = new List<Concepto_CuentasXpagarImp>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        String sql = @"SELECT CUENTAXPAGAR_impuesto.*,CONCEPTO_CUENTAXPAGAR_IMP.COD_CONCEPTO_FAC,CONCEPTO_CUENTAXPAGAR_IMP.COD_CUENTA_IMP, CONCEPTO_CUENTAXPAGAR_IMP.BASE_MINIMA,TIPOIMPUESTO.NOMBRE_IMPUESTO, PLAN_CUENTAS.TIPO 
                                 FROM CUENTAXPAGAR_impuesto
                                 LEFT JOIN TIPOIMPUESTO ON CUENTAXPAGAR_impuesto.COD_TIPO_IMPUESTO = TIPOIMPUESTO.COD_TIPO_IMPUESTO
                                 LEFT JOIN CONCEPTO_CUENTAXPAGAR_IMP ON CONCEPTO_CUENTAXPAGAR_IMP.IDCONCEPTOIMP = CUENTAXPAGAR_IMPUESTO.IDCONCEPTOIMP
                                 LEFT JOIN PLAN_CUENTAS ON CONCEPTO_CUENTAXPAGAR_IMP.COD_CUENTA_IMP = PLAN_CUENTAS.COD_CUENTA
                                 WHERE  CUENTAXPAGAR_IMPUESTO.CODIGO_FACTURA = " + pImpuesto.cod_factura;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Concepto_CuentasXpagarImp impuesto = new Concepto_CuentasXpagarImp();
                            if (resultado["COD_CONCEPTO_FAC"] != DBNull.Value) impuesto.cod_concepto_fac = Convert.ToInt32(resultado["COD_CONCEPTO_FAC"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) impuesto.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["NOMBRE_IMPUESTO"] != DBNull.Value) impuesto.nom_tipo_impuesto = Convert.ToString(resultado["NOMBRE_IMPUESTO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) impuesto.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) impuesto.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["COD_CUENTA_IMP"] != DBNull.Value) impuesto.cod_cuenta_imp = Convert.ToString(resultado["COD_CUENTA_IMP"]);
                            if (resultado["TIPO"] != DBNull.Value) impuesto.naturaleza = Convert.ToString(resultado["TIPO"]);
                            if (resultado["IDCONCEPTOIMP"] != DBNull.Value) impuesto.coddetalleimp = Convert.ToInt32(resultado["IDCONCEPTOIMP"]);
                            if (resultado["CODDETALLEFAC"] != DBNull.Value) impuesto.CodDetalleFac = Convert.ToInt32(resultado["CODDETALLEFAC"]);

                            lstImpuesto.Add(impuesto); 
                        }
                           
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ListarConceptoImpuestoDetalleCxp", ex);
                        return null;
                    }
                }
            }
  }

        public bool ParametrosBaseDatos(ref string pSeparadorDecimal, ref string pFormatoFecha, Usuario vUsuario)
        {
            DbDataReader resultado;
            pSeparadorDecimal = "";
            pFormatoFecha = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = @"Select 
                                        (Select substr(value, 0, 1) As separador_decimal From v$nls_parameters Where parameter In ('NLS_NUMERIC_CHARACTERS')) As separador_decimal,
                                        (Select Trim(Substr(value, 0, Instr(value, ' '))) From v$nls_parameters Where parameter In ('NLS_DATE_FORMAT')) As formato_fecha
                                        From dual";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["SEPARADOR_DECIMAL"] != DBNull.Value) pSeparadorDecimal = Convert.ToString(resultado["SEPARADOR_DECIMAL"]);
                            if (resultado["FORMATO_FECHA"] != DBNull.Value) pFormatoFecha = Convert.ToString(resultado["FORMATO_FECHA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                }
            }
        }

        public void EliminarConceptoImpuesto(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconceptoimp = cmdTransaccionFactory.CreateParameter();
                        pidconceptoimp.ParameterName = "p_idconceptoimp";
                        pidconceptoimp.Value = pId;
                        pidconceptoimp.Direction = ParameterDirection.Input;
                        pidconceptoimp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconceptoimp);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CTAXPAGIMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "EliminarConceptoImpuesto", ex);
                    }
                }
            }
        }



        public Concepto_CuentasXpagarImp ModificarPlanCuentasImpuesto(Concepto_CuentasXpagarImp pImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidimpuesto = cmdTransaccionFactory.CreateParameter();
                        pidimpuesto.ParameterName = "p_idimpuesto";
                        pidimpuesto.Value = pImpuesto.idimpuesto;
                        pidimpuesto.Direction = ParameterDirection.Input;
                        pidimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidimpuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pImpuesto.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        pporcentaje_impuesto.Value = pImpuesto.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pImpuesto.base_minima != null) pbase_minima.Value = pImpuesto.base_minima; else pbase_minima.Value = DBNull.Value;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcod_cuenta_imp = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_imp.ParameterName = "p_cod_cuenta_imp";
                        pcod_cuenta_imp.Value = pImpuesto.cod_cuenta_imp;
                        pcod_cuenta_imp.Direction = ParameterDirection.Input;
                        pcod_cuenta_imp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_imp);


                        DbParameter pasumido = cmdTransaccionFactory.CreateParameter();
                        pasumido.ParameterName = "P_ASUMIDO";
                        pasumido.Value = pImpuesto.asumido;
                        pasumido.Direction = ParameterDirection.Input;
                        pasumido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasumido);

                        DbParameter P_COD_CUENTA_ASUMIDO = cmdTransaccionFactory.CreateParameter();
                        P_COD_CUENTA_ASUMIDO.ParameterName = "P_COD_CUENTA_ASUMIDO";
                        P_COD_CUENTA_ASUMIDO.Value = pImpuesto.cod_cuenta_asumido;
                        P_COD_CUENTA_ASUMIDO.Direction = ParameterDirection.Input;
                        P_COD_CUENTA_ASUMIDO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CUENTA_ASUMIDO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLAN_CUENT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pImpuesto.idimpuesto = Convert.ToInt32(pidimpuesto.Value);
                        return pImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoCtaData", "ModificarPlanCuentasImpuesto", ex);
                        return null;
                    }
                }
            }
        }




    }
}
