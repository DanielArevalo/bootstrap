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
    public class ParametroCtasOtrosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public ParametroCtasOtrosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Par_Cue_Otros> ListarPar_Cue_Otros(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_Otros> lstConsulta = new List<Par_Cue_Otros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.cod_cuenta_NIIF,PCN.nombre,p.idparametro,t.descripcion as nomtipo_tran,p.cod_cuenta,pc.nombre as nomCuenta, "
                                        +"case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, "
                                        +"e.detalle as nomestructura,ti.nombre_impuesto as nomimpuesto "
                                        +"from PAR_CUE_OTROS p left join estructura_detalle e on p.cod_est_det = e.cod_est_det "
                                        +"left join Plan_Cuentas_Niif PCN on PCN.Cod_Cuenta_Niif = p.cod_cuenta_NIIF "
                                        +"left join plan_cuentas pc on pc.cod_cuenta = p.cod_cuenta "
                                        +"left join tipo_tran t on t.tipo_tran = p.tipo_tran "
                                        +"left join tipoimpuesto ti on ti.cod_tipo_impuesto = p.tipo_impuesto where 1 = 1"
                                        + filtro + " order by p.idparametro";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_Otros entidad = new Par_Cue_Otros();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nomtipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["cod_cuenta_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["cod_cuenta_NIIF"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_cuenta_niif = Convert.ToString(resultado["NOMBRE"]);                            
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nomCuenta = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nomestructura = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["nomimpuesto"] != DBNull.Value) entidad.nomimpuesto = Convert.ToString(resultado["nomimpuesto"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "ListarPar_Cue_Otros", ex);
                        return null;
                    }
                }
            }
        }
        
        public Par_Cue_Otros ConsultarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_Otros entidad = new Par_Cue_Otros();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_CUE_OTROS WHERE IDPARAMETRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["TIPO_IMPUESTO"] != DBNull.Value) entidad.tipo_impuesto = Convert.ToInt32(resultado["TIPO_IMPUESTO"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "ConsultarParametroCtasOtros", ex);
                        return null;
                    }
                }
            }
        }

        public Par_Cue_Otros CrearPar_Cue_Otros(Par_Cue_Otros pParam, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParam.idparametro;
                        if (opcion == 1)
                            pidparametro.Direction = ParameterDirection.Output;
                        else
                            pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pParam.tipo_tran != 0) ptipo_tran.Value = pParam.tipo_tran; else ptipo_tran.Value = DBNull.Value;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pParam.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pParam.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pParam.cod_est_det != 0) pcod_est_det.Value = pParam.cod_est_det; else pcod_est_det.Value = DBNull.Value;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter ptipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        ptipo_impuesto.ParameterName = "p_tipo_impuesto";
                        if (pParam.tipo_impuesto != 0) ptipo_impuesto.Value = pParam.tipo_impuesto; else ptipo_impuesto.Value = DBNull.Value;
                        ptipo_impuesto.Direction = ParameterDirection.Input;
                        ptipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_impuesto);

                        DbParameter pcod_cuenta_NIIF = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_NIIF.ParameterName = "p_cod_cuenta_NIIF";
                        if (pParam.cod_cuenta_niif != null) pcod_cuenta_NIIF.Value = pParam.cod_cuenta_niif; else pcod_cuenta_NIIF.Value = DBNull.Value;
                        pcod_cuenta_NIIF.Direction = ParameterDirection.Input;
                        pcod_cuenta_NIIF.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_NIIF);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARCTAOTRO_CREAR"; // CREAR
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARCTAOTRO_MOD"; // MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1)
                            pParam.idparametro = Convert.ToInt32(pidparametro.Value);
                        return pParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "CrearPar_Cue_Otros", ex);
                        return null;
                    }
                }
            }
        }
        
        public void EliminarParametroCtasOtros(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM PAR_CUE_OTROS WHERE idparametro = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "EliminarParametroCtasOtros", ex);
                    }
                }
            }
        }



        public List<Par_Cue_LinAho> getListParametros(Usuario pusuario, String filtro) 
        {
            DbDataReader resultado;
            List<Par_Cue_LinAho> lstConsulta = new List<Par_Cue_LinAho>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select pl.idparametro, pl.tipo_ahorro, Case pl.tipo_ahorro When 3 Then 'Ahorro Vista' When 9 Then 'Ahorro Programado' When 5 Then  'CDATS' End as nomtipo_ahorro,"
                                     +" pl.cod_linea, "
                                     +" Case pl.tipo_ahorro "
                                     +" When 3 Then (Select l.descripcion From lineaahorro l Where l.cod_linea_ahorro = pl.cod_linea) "
                                     +" When 9 Then (Select l.nombre From lineaprogramado l Where l.cod_linea_programado = pl.cod_linea) "
                                     +" When 5 Then (Select l.descripcion From lineacdat l Where l.cod_lineacdat = pl.cod_linea) End As nomlinea,"
                                     +" pl.tipo_tran, t.descripcion, pl.cod_cuenta, p.nombre, pl.tipo, Case pl.tipo When 1 Then 'Débito' When 2 Then 'Crédito' End As nomtipo"
                                     +" From  par_cue_linaho pl "
                                     +" Left Join tipo_tran t On pl.tipo_tran = t.tipo_tran "
                                     +" Left Join plan_cuentas p On pl.cod_cuenta = p.cod_cuenta where 1=1 " + filtro + " Order by pl.cod_linea, pl.tipo_tran, pl.tipo, pl.idparametro ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Par_Cue_LinAho entidad = new Par_Cue_LinAho();
                            if (resultado["idparametro"] != DBNull.Value) entidad.Codigo = Convert.ToInt64(resultado["idparametro"]);
                            if (resultado["nomtipo_ahorro"] != DBNull.Value) entidad.TipoAhorro = Convert.ToString(resultado["nomtipo_ahorro"]);
                            if (resultado["nomlinea"] != DBNull.Value) entidad.LineaAhorro = Convert.ToString(resultado["nomlinea"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["tipo_tran"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.tipoTrasaccion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.CodigoCuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.NombreCuenta = Convert.ToString(resultado["nombre"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["tipo"]);
                            if (resultado["nomtipo"] != DBNull.Value) entidad.nomtipo_mov = Convert.ToString(resultado["nomtipo"]);

                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "getListParametros", ex);
                        return null;
                    }
                }
            }
        }

        // insertar parametros nuevos
        public void InsertParametro(Usuario pusuario,Par_Cue_LinAho entidadcrea) 
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = entidadcrea.Codigo;
                        p_idparametro.Direction = ParameterDirection.Output;
                        p_idparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);

                        DbParameter p_tipo_ahorro = cmdTransaccionFactory.CreateParameter();
                        p_tipo_ahorro.ParameterName = "p_tipo_ahorro";
                        p_tipo_ahorro.Value =Convert.ToInt64(entidadcrea.TipoAhorro);
                        p_tipo_ahorro.Direction = ParameterDirection.Input;
                        p_tipo_ahorro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_ahorro);

                        DbParameter p_cod_linea = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea.ParameterName = "p_cod_linea";
                        p_cod_linea.Value = entidadcrea.LineaAhorro;
                        p_cod_linea.Direction = ParameterDirection.Input;
                        p_cod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        if (entidadcrea.tipoTrasaccion != null) p_tipo_tran.Value = entidadcrea.tipoTrasaccion; else p_tipo_tran.Value = DBNull.Value;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "p_cod_cuenta";
                        if (entidadcrea.CodigoCuenta != null) p_cod_cuenta.Value = entidadcrea.CodigoCuenta; else p_cod_cuenta.Value = DBNull.Value;
                        p_cod_cuenta.Direction = ParameterDirection.Input;
                        p_cod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (entidadcrea.cod_cuenta_niif != null) pcod_cuenta_niif.Value = entidadcrea.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        if (entidadcrea.tipo_mov != null) p_tipo.Value = entidadcrea.tipo_mov; else p_tipo.Value = DBNull.Value;
                        p_tipo.Direction = ParameterDirection.Input;
                        p_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AH_CREAR"; // CREAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "InsertParametro", ex);
                    }
                }
            }
        }

        public void EliminarParametro(Usuario pUsuario,Int64 idcodigo) 
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = idcodigo;
                        p_idparametro.Direction = ParameterDirection.Input;
                        p_idparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AH_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarParametro", ex);
                    }
                }
            }
        }

        public Par_Cue_LinAho getParametroById(Usuario pusuario, Int64 idParametro) 
        {
            DbDataReader resultado;
            Par_Cue_LinAho entidad = new Par_Cue_LinAho();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pusuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM par_cue_linaho p WHERE p.IDPARAMETRO = " + idParametro + " ORDER BY cod_linea, tipo_tran, tipo, idparametro" ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_AHORRO"] != DBNull.Value) entidad.TipoAhorro = Convert.ToString(resultado["TIPO_AHORRO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.LineaAhorro = Convert.ToString(resultado["COD_LINEA"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipoTrasaccion = Convert.ToString(resultado["TIPO_TRAN"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.CodigoCuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "ConsultarParametroCtasOtros", ex);
                        return null;
                    }
                }
            }
        }

        public void updateParametro(Usuario pUsuario, Par_Cue_LinAho entidadcrea) 
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = entidadcrea.Codigo;
                        p_idparametro.Direction = ParameterDirection.Input;
                        p_idparametro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);

                        DbParameter p_tipo_ahorro = cmdTransaccionFactory.CreateParameter();
                        p_tipo_ahorro.ParameterName = "p_tipo_ahorro";
                        p_tipo_ahorro.Value = entidadcrea.TipoAhorro;
                        p_tipo_ahorro.Direction = ParameterDirection.Input;
                        p_tipo_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_ahorro);

                        DbParameter p_cod_linea = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea.ParameterName = "p_cod_linea";
                        p_cod_linea.Value = entidadcrea.LineaAhorro;
                        p_cod_linea.Direction = ParameterDirection.Input;
                        p_cod_linea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        if (entidadcrea.tipoTrasaccion != null) p_tipo_tran.Value = entidadcrea.tipoTrasaccion; else p_tipo_tran.Value = DBNull.Value;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "p_cod_cuenta";
                        if (entidadcrea.CodigoCuenta != null) p_cod_cuenta.Value = entidadcrea.CodigoCuenta; else p_cod_cuenta.Value = DBNull.Value;
                        p_cod_cuenta.Direction = ParameterDirection.Input;
                        p_cod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (entidadcrea.cod_cuenta_niif != null) pcod_cuenta_niif.Value = entidadcrea.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        if (entidadcrea.tipo_mov != null) p_tipo.Value = entidadcrea.tipo_mov; else p_tipo.Value = DBNull.Value;
                        p_tipo.Direction = ParameterDirection.Input;
                        p_tipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AH_MOD"; // Modifica
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "updateParametro", ex);
                    }
                }
            }
        }


        public List<Par_Cue_LinAho> llenarLista(Usuario pUsuario,String codigo) 
        {
            DbDataReader resultado;
            List<Par_Cue_LinAho> Listentidad = new List<Par_Cue_LinAho>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select t.tipo_tran, t.descripcion from tipo_tran t where t.tipo_producto = " + Convert.ToInt64(codigo);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinAho entidad = new Par_Cue_LinAho();
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.tipoTrasaccion = Convert.ToString(resultado["tipo_tran"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.NombreCuenta = Convert.ToString(resultado["descripcion"]);

                            Listentidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return Listentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasOtrosData", "ConsultarParametroCtasOtros", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo_ParCue_LinAHO(Usuario vUsuario)
        {
            Int64 resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT MAX(IDPARAMETRO) + 1 FROM PAR_CUE_LINAHO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }


    }
}

