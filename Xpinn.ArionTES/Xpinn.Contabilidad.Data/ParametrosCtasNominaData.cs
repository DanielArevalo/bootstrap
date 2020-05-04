using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    public class ParametrosCtasNominaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ParametrosCtasNominaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Par_Cue_Nomina CrearPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_Nomina.idparametro;
                        pidparametro.Direction = ParameterDirection.Output;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pPar_Cue_Nomina.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPar_Cue_Nomina.cod_est_det == null)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPar_Cue_Nomina.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPar_Cue_Nomina.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pPar_Cue_Nomina.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pPar_Cue_Nomina.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pPar_Cue_Nomina.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_Nomina.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_Nomina.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pPar_Cue_Nomina.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pPar_Cue_Nomina.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pcod_tercero = cmdTransaccionFactory.CreateParameter();
                        pcod_tercero.ParameterName = "p_cod_tercero";
                        if (pPar_Cue_Nomina.cod_tercero == null)
                            pcod_tercero.Value = DBNull.Value;
                        else
                            pcod_tercero.Value = pPar_Cue_Nomina.cod_tercero;
                        pcod_tercero.Direction = ParameterDirection.Input;
                        pcod_tercero.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_tercero);

                        DbParameter ptipo_tercero = cmdTransaccionFactory.CreateParameter();
                        ptipo_tercero.ParameterName = "p_tipo_tercero";
                        if (pPar_Cue_Nomina.tipo_tercero == null)
                            ptipo_tercero.Value = DBNull.Value;
                        else
                            ptipo_tercero.Value = pPar_Cue_Nomina.tipo_tercero;
                        ptipo_tercero.Direction = ParameterDirection.Input;
                        ptipo_tercero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tercero);

                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pPar_Cue_Nomina.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pPar_Cue_Nomina.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_NO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pPar_Cue_Nomina.idparametro = pidparametro.Value != DBNull.Value ? Convert.ToInt64(pidparametro.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pPar_Cue_Nomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_NominaData", "CrearPar_Cue_Nomina", ex);
                        return null;
                    }
                }
            }
        }


        public Par_Cue_Nomina ModificarPar_Cue_Nomina(Par_Cue_Nomina pPar_Cue_Nomina, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_Nomina.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pPar_Cue_Nomina.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pPar_Cue_Nomina.cod_est_det == null)
                            pcod_est_det.Value = DBNull.Value;
                        else
                            pcod_est_det.Value = pPar_Cue_Nomina.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pPar_Cue_Nomina.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pPar_Cue_Nomina.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pPar_Cue_Nomina.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pPar_Cue_Nomina.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_Nomina.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_Nomina.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pPar_Cue_Nomina.cod_cuenta_niif == null)
                            pcod_cuenta_niif.Value = DBNull.Value;
                        else
                            pcod_cuenta_niif.Value = pPar_Cue_Nomina.cod_cuenta_niif;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter pcod_tercero = cmdTransaccionFactory.CreateParameter();
                        pcod_tercero.ParameterName = "p_cod_tercero";
                        if (pPar_Cue_Nomina.cod_tercero == null)
                            pcod_tercero.Value = DBNull.Value;
                        else
                            pcod_tercero.Value = pPar_Cue_Nomina.cod_tercero;
                        pcod_tercero.Direction = ParameterDirection.Input;
                        pcod_tercero.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_tercero);

                        DbParameter ptipo_tercero = cmdTransaccionFactory.CreateParameter();
                        ptipo_tercero.ParameterName = "p_tipo_tercero";
                        if (pPar_Cue_Nomina.tipo_tercero == null)
                            ptipo_tercero.Value = DBNull.Value;
                        else
                            ptipo_tercero.Value = pPar_Cue_Nomina.tipo_tercero;
                        ptipo_tercero.Direction = ParameterDirection.Input;
                        ptipo_tercero.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tercero);

                        DbParameter pcod_cuenta_contra = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_contra.ParameterName = "p_cod_cuenta_contra";
                        if (pPar_Cue_Nomina.cod_cuenta_contra == null)
                            pcod_cuenta_contra.Value = DBNull.Value;
                        else
                            pcod_cuenta_contra.Value = pPar_Cue_Nomina.cod_cuenta_contra;
                        pcod_cuenta_contra.Direction = ParameterDirection.Input;
                        pcod_cuenta_contra.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_contra);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_NO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPar_Cue_Nomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_NominaData", "ModificarPar_Cue_Nomina", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPar_Cue_Nomina(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Par_Cue_Nomina pPar_Cue_Nomina = new Par_Cue_Nomina();
                        pPar_Cue_Nomina = ConsultarPar_Cue_Nomina(pId, vUsuario);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_Nomina.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_NO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_NominaData", "EliminarPar_Cue_Nomina", ex);
                    }
                }
            }
        }


        public Par_Cue_Nomina ConsultarPar_Cue_Nomina(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_Nomina entidad = new Par_Cue_Nomina();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.*, PCN.nombre, t.descripcion as nomtipo_tran, pc.nombre as nomCuenta, 
                                        case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, 
                                        e.detalle as nomestructura, pc2.NOMBRE as Nombre_Nomina, cop.DESCRIPCION as Desc_concepto,
                                        per.identificacion, per.primer_nombre || ' ' || per.primer_apellido as nombre_tercero, per.razon_social
                                        from PAR_CUE_NOMINA p 
                                        LEFT join plan_cuentas pc on pc.cod_cuenta = p.cod_cuenta 
                                        LEFT join plan_cuentas pc2 on pc2.cod_cuenta = p.COD_CUENTA_CONTRA
                                        LEFT join tipo_tran t on t.tipo_tran = p.tipo_tran 
                                        left join persona per on p.cod_tercero = per.cod_persona
                                        left join estructura_detalle e on p.cod_est_det = e.cod_est_det 
                                        left join concepto_nomina cop on cop.CONSECUTIVO = p.COD_CONCEPTO
                                        left join Plan_Cuentas_Niif PCN on PCN.Cod_Cuenta_Niif = p.cod_cuenta_NIIF 
                                        WHERE p.IDPARAMETRO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["Desc_concepto"] != DBNull.Value) entidad.nom_concepto = Convert.ToString(resultado["Desc_concepto"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nom_est_det = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nom_cue_local = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cue_niff = Convert.ToString(resultado["nombre"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_tercero = Convert.ToInt64(resultado["COD_TERCERO"]);
                            if (resultado["TIPO_TERCERO"] != DBNull.Value) entidad.tipo_tercero = Convert.ToInt32(resultado["TIPO_TERCERO"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);
                            if (resultado["Nombre_Nomina"] != DBNull.Value) entidad.nom_cue_contra = Convert.ToString(resultado["Nombre_Nomina"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_tercero = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre_tercero"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["nombre_tercero"]);
                            if (resultado["razon_social"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["razon_social"]);
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
                        BOExcepcion.Throw("Par_Cue_NominaData", "ConsultarPar_Cue_Nomina", ex);
                        return null;
                    }
                }
            }
        }


        public List<Par_Cue_Nomina> ListarPar_Cue_Nomina(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_Nomina> lstPar_Cue_Nomina = new List<Par_Cue_Nomina>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.*, PCN.nombre, t.descripcion as nomtipo_tran, pc.nombre as nomCuenta, 
                                        case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, 
                                        e.detalle as nomestructura, pc2.nombre as Nombre_Nomina, cop.DESCRIPCION as Desc_concepto
                                        from PAR_CUE_NOMINA p 
                                        left join plan_cuentas pc on pc.cod_cuenta = p.cod_cuenta 
                                        left join plan_cuentas pc2 on pc2.cod_cuenta = p.cod_cuenta
                                        left join tipo_tran t on t.tipo_tran = p.tipo_tran 
                                        left join estructura_detalle e on p.cod_est_det = e.cod_est_det 
                                        left join concepto_nomina cop on cop.CONSECUTIVO = p.COD_CONCEPTO
                                        left join Plan_Cuentas_Niif PCN on PCN.Cod_Cuenta_Niif = p.cod_cuenta_NIIF    where 1=1 "
                                        + filtro + " ORDER BY IDPARAMETRO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_Nomina entidad = new Par_Cue_Nomina();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["Desc_concepto"] != DBNull.Value) entidad.nom_concepto = Convert.ToString(resultado["Desc_concepto"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nom_est_det = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nom_cue_local = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt64(resultado["TIPO_TRAN"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_cue_niff = Convert.ToString(resultado["nombre"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_tercero = Convert.ToInt64(resultado["COD_TERCERO"]);
                            if (resultado["TIPO_TERCERO"] != DBNull.Value) entidad.tipo_tercero = Convert.ToInt32(resultado["TIPO_TERCERO"]);
                            if (resultado["COD_CUENTA_CONTRA"] != DBNull.Value) entidad.cod_cuenta_contra = Convert.ToString(resultado["COD_CUENTA_CONTRA"]);
                            if (resultado["Nombre_Nomina"] != DBNull.Value) entidad.nom_cue_contra = Convert.ToString(resultado["Nombre_Nomina"]);
                            lstPar_Cue_Nomina.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_Nomina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_NominaData", "ListarPar_Cue_Nomina", ex);
                        return null;
                    }
                }
            }
        }


    }
}