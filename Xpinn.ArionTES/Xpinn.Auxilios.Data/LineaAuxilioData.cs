using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;

namespace Xpinn.Auxilios.Data
{   
    public class LineaAuxilioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public LineaAuxilioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public LineaAuxilio CrearLineaAuxilio(LineaAuxilio pLineaAuxilio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pLineaAuxilio.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pLineaAuxilio.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pLineaAuxilio.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        pcod_est_det.Value = pLineaAuxilio.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pLineaAuxilio.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pLineaAuxilio.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pLineaAuxilio.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pLineaAuxilio.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pLineaAuxilio.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pLineaAuxilio.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pLineaAuxilio.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_PAR_CUE_LI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "CrearLineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public LineaAuxilio ModificarLineaAuxilios(LineaAuxilio pLineaAuxilio, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pLineaAuxilio.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pLineaAuxilio.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pLineaAuxilio.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        pcod_est_det.Value = pLineaAuxilio.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pLineaAuxilio.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pLineaAuxilio.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pLineaAuxilio.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pLineaAuxilio.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pLineaAuxilio.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pLineaAuxilio.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pLineaAuxilio.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_PAR_CUE_LI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLineaAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "ModificarLineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaAuxilios(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LineaAuxilio pLineaAuxilio = new LineaAuxilio();
                        pLineaAuxilio = ConsultarLineaAuxilio(pId, pUsuario);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pLineaAuxilio.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_PAR_CUE_LI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "EliminarLineaAuxilio", ex);
                    }
                }
            }
        }

        public LineaAuxilio ConsultarLineaAuxilio(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaAuxilio entidad = new LineaAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_CUE_LINAUX WHERE IDPARAMETRO = " + pId.ToString();
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilios = Convert.ToInt32(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
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
                        BOExcepcion.Throw("LineaAuxilioData", "ConsultarLineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }

        
        public LineaAuxilio Crear_Modi_LineaAuxilio(LineaAuxilio pLinea, Usuario pUsuario,int Opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pLinea.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pLinea.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pLinea.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pmonto_maximo = cmdTransaccionFactory.CreateParameter();
                        pmonto_maximo.ParameterName = "p_monto_maximo";
                        pmonto_maximo.Value = pLinea.monto_maximo;
                        pmonto_maximo.Direction = ParameterDirection.Input;
                        pmonto_maximo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_maximo);

                        DbParameter pmonto_minimo = cmdTransaccionFactory.CreateParameter();
                        pmonto_minimo.ParameterName = "p_monto_minimo";
                        pmonto_minimo.Value = pLinea.monto_minimo;
                        pmonto_minimo.Direction = ParameterDirection.Input;
                        pmonto_minimo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pmonto_minimo);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pLinea.cod_periodicidad != 0) pcod_periodicidad.Value = pLinea.cod_periodicidad; else pcod_periodicidad.Value = DBNull.Value;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter ptipo_persona = cmdTransaccionFactory.CreateParameter();
                        ptipo_persona.ParameterName = "p_tipo_persona";
                        if (pLinea.tipo_persona != null) ptipo_persona.Value = pLinea.tipo_persona; else ptipo_persona.Value = DBNull.Value;
                        ptipo_persona.Direction = ParameterDirection.Input;
                        ptipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_persona);

                        DbParameter pnumero_auxilios = cmdTransaccionFactory.CreateParameter();
                        pnumero_auxilios.ParameterName = "p_numero_auxilios";
                        if (pLinea.numero_auxilios != 0) pnumero_auxilios.Value = pLinea.numero_auxilios; else pnumero_auxilios.Value = DBNull.Value;
                        pnumero_auxilios.Direction = ParameterDirection.Input;
                        pnumero_auxilios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_auxilios);

                        DbParameter pdias_desembolso = cmdTransaccionFactory.CreateParameter();
                        pdias_desembolso.ParameterName = "p_dias_desembolso";
                        if (pLinea.dias_desembolso != 0) pdias_desembolso.Value = pLinea.dias_desembolso; else pdias_desembolso.Value = DBNull.Value;
                        pdias_desembolso.Direction = ParameterDirection.Input;
                        pdias_desembolso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias_desembolso);

                        DbParameter pcobra_retencion = cmdTransaccionFactory.CreateParameter();
                        pcobra_retencion.ParameterName = "p_cobra_retencion";
                        pcobra_retencion.Value = pLinea.cobra_retencion;
                        pcobra_retencion.Direction = ParameterDirection.Input;
                        pcobra_retencion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_retencion);

                        DbParameter peducativo = cmdTransaccionFactory.CreateParameter();
                        peducativo.ParameterName = "p_educativo";
                        peducativo.Value = pLinea.educativo;
                        peducativo.Direction = ParameterDirection.Input;
                        peducativo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(peducativo);

                        DbParameter pporc_matricula = cmdTransaccionFactory.CreateParameter();
                        pporc_matricula.ParameterName = "p_porc_matricula";
                        if (pLinea.porc_matricula != 0) pporc_matricula.Value = pLinea.porc_matricula; else pporc_matricula.Value = DBNull.Value;
                        pporc_matricula.Direction = ParameterDirection.Input;
                        pporc_matricula.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporc_matricula);

                        DbParameter pordenServicio = cmdTransaccionFactory.CreateParameter();
                        pordenServicio.ParameterName = "p_orden_servicio";
                        pordenServicio.Value = pLinea.orden_servicio;
                        pordenServicio.Direction = ParameterDirection.Input;
                        pordenServicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pordenServicio);

                        DbParameter ppermite_mora = cmdTransaccionFactory.CreateParameter();
                        ppermite_mora.ParameterName = "p_permite_mora";
                        ppermite_mora.Value = pLinea.permite_mora;
                        ppermite_mora.Direction = ParameterDirection.Input;
                        ppermite_mora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_mora);

                        DbParameter ppermite_retirados = cmdTransaccionFactory.CreateParameter();
                        ppermite_retirados.ParameterName = "p_permite_retirados";
                        ppermite_retirados.Value = pLinea.permite_retirados;
                        ppermite_retirados.Direction = ParameterDirection.Input;
                        ppermite_retirados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppermite_retirados);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(Opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_LINEASAUXI_CREAR";//CREAR
                        else if(Opcion == 2)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_LINEASAUXI_MOD";//MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLinea;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "Crear_Modi_LineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public List<LineaAuxilio> ListarLineaAuxilio(LineaAuxilio pAuxilio, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado;
            List<LineaAuxilio> lstAuxilio = new List<LineaAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" select l.cod_linea_auxilio,l.descripcion, case l.estado when 0 then 'Inactivo' when 1 then 'Activo' end as estado, "
                                        +"l.monto_minimo, l.monto_maximo,p.descripcion as nomperiodicidad,case l.tipo_persona when '1' then 'Asociado' when '2' then 'Familiar' end as tipo_persona, "
                                        +"l.numero_auxilios,l.dias_desembolso,case l.cobra_retencion when 0 then 'NO' when 1 then 'SI' end as retencion "
                                        +"from lineasauxilios l left join periodicidad p on p.cod_periodicidad = l.cod_periodicidad "
                                        + "where 1=1" + filtro + " order by l.cod_linea_auxilio";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LineaAuxilio entidad = new LineaAuxilio();
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["MONTO_MAXIMO"] != DBNull.Value) entidad.monto_maximo = Convert.ToDecimal(resultado["MONTO_MAXIMO"]);
                            if (resultado["MONTO_MINIMO"] != DBNull.Value) entidad.monto_minimo = Convert.ToDecimal(resultado["MONTO_MINIMO"]);
                            if (resultado["NOMPERIODICIDAD"] != DBNull.Value) entidad.nomperiodicidad = Convert.ToString(resultado["NOMPERIODICIDAD"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["NUMERO_AUXILIOS"] != DBNull.Value) entidad.numero_auxilios = Convert.ToInt32(resultado["NUMERO_AUXILIOS"]);
                            if (resultado["DIAS_DESEMBOLSO"] != DBNull.Value) entidad.dias_desembolso = Convert.ToInt32(resultado["DIAS_DESEMBOLSO"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToString(resultado["RETENCION"]);

                            lstAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "ListarLineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLineaAuxilio(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pId;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_LINEASAUXI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "EliminarLineaAuxilio", ex);
                    }
                }
            }
        }

        public LineaAuxilio ConsultarLineaAUXILIO(string pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            LineaAuxilio entidad = new LineaAuxilio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM lineasauxilios WHERE COD_LINEA_AUXILIO = " + pId.ToString();
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["MONTO_MAXIMO"] != DBNull.Value) entidad.monto_maximo = Convert.ToDecimal(resultado["MONTO_MAXIMO"]);
                            if (resultado["MONTO_MINIMO"] != DBNull.Value) entidad.monto_minimo = Convert.ToDecimal(resultado["MONTO_MINIMO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["NUMERO_AUXILIOS"] != DBNull.Value) entidad.numero_auxilios = Convert.ToInt32(resultado["NUMERO_AUXILIOS"]);
                            if (resultado["DIAS_DESEMBOLSO"] != DBNull.Value) entidad.dias_desembolso = Convert.ToInt32(resultado["DIAS_DESEMBOLSO"]);
                            if (resultado["COBRA_RETENCION"] != DBNull.Value) entidad.cobra_retencion = Convert.ToInt32(resultado["COBRA_RETENCION"]);
                            if (resultado["EDUCATIVO"] != DBNull.Value) entidad.educativo = Convert.ToInt32(resultado["EDUCATIVO"]);
                            if (resultado["PORC_MATRICULA"] != DBNull.Value) entidad.porc_matricula = Convert.ToDecimal(resultado["PORC_MATRICULA"]);
                            if (resultado["ORDEN_SERVICIO"] != DBNull.Value) entidad.orden_servicio = Convert.ToInt32(resultado["ORDEN_SERVICIO"]);
                            if (resultado["permite_mora"] != DBNull.Value) entidad.permite_mora = Convert.ToInt32(resultado["permite_mora"]);
                            if (resultado["PERMITE_RETIRADOS"] != DBNull.Value) entidad.permite_retirados = Convert.ToInt32(resultado["PERMITE_RETIRADOS"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "ConsultarLineaAUXILIO", ex);
                        return null;
                    }
                }
            }
        }



        //Detalle

        public RequisitosLineaAuxilio CrearRequisitosDeLineaAux(RequisitosLineaAuxilio pRequisitos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrequisitoaux = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoaux.ParameterName = "p_codrequisitoaux";
                        pcodrequisitoaux.Value = pRequisitos.codrequisitoaux;
                        pcodrequisitoaux.Direction = ParameterDirection.Output;
                        pcodrequisitoaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoaux);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pRequisitos.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pRequisitos.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter prequerido = cmdTransaccionFactory.CreateParameter();
                        prequerido.ParameterName = "p_requerido";
                        prequerido.Value = pRequisitos.requerido;
                        prequerido.Direction = ParameterDirection.Input;
                        prequerido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prequerido);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_REQUILINEA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRequisitos.codrequisitoaux = Convert.ToInt64(pcodrequisitoaux.Value);
                        return pRequisitos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "CrearRequisitosDeLineaAux", ex);
                        return null;
                    }
                }
            }
        }


        public RequisitosLineaAuxilio ModificarRequisitosDeLineaAux(RequisitosLineaAuxilio pRequisitos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrequisitoaux = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoaux.ParameterName = "p_codrequisitoaux";
                        pcodrequisitoaux.Value = pRequisitos.codrequisitoaux;
                        pcodrequisitoaux.Direction = ParameterDirection.Input;
                        pcodrequisitoaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoaux);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pRequisitos.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pRequisitos.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter prequerido = cmdTransaccionFactory.CreateParameter();
                        prequerido.ParameterName = "p_requerido";
                        prequerido.Value = pRequisitos.requerido;
                        prequerido.Direction = ParameterDirection.Input;
                        prequerido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(prequerido);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_REQUILINEA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pRequisitos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "CrearRequisitosDeLineaAux", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDETALLELineaAux(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodrequisitoaux = cmdTransaccionFactory.CreateParameter();
                        pcodrequisitoaux.ParameterName = "p_codrequisitoaux";
                        pcodrequisitoaux.Value = pId;
                        pcodrequisitoaux.Direction = ParameterDirection.Input;
                        pcodrequisitoaux.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodrequisitoaux);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AUX_REQUILINEA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "EliminarDETALLELineaAux", ex);
                    }
                }
            }
        }


        public List<RequisitosLineaAuxilio> ConsultarDETALLELineaAux(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<RequisitosLineaAuxilio> LstDetalle = new List<RequisitosLineaAuxilio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" SELECT * FROM lineasauxilios_requisitos where COD_LINEA_AUXILIO = " + pId.ToString() + " order by CODREQUISITOAUX";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            RequisitosLineaAuxilio entidad = new RequisitosLineaAuxilio();
                            if (resultado["CODREQUISITOAUX"] != DBNull.Value) entidad.codrequisitoaux = Convert.ToInt64(resultado["CODREQUISITOAUX"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["REQUERIDO"] != DBNull.Value) entidad.requerido = Convert.ToInt32(resultado["REQUERIDO"]);
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "ConsultarDETALLEAuxilio", ex);
                        return null;
                    }
                }
            }
        }

       

        
       
    }
}
