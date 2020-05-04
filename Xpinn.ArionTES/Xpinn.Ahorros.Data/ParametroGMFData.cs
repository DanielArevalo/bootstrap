using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    public class ParametroGMFData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ParametroGMFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ParametroGMF CrearGMF(ParametroGMF pGMF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametrogmf = cmdTransaccionFactory.CreateParameter();
                        pidparametrogmf.ParameterName = "p_idparametrogmf";
                        pidparametrogmf.Value = pGMF.idparametrogmf;
                        pidparametrogmf.Direction = ParameterDirection.Input;
                        pidparametrogmf.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametrogmf);

                        DbParameter ptipo_ope = cmdTransaccionFactory.CreateParameter();
                        ptipo_ope.ParameterName = "p_tipo_ope";
                        ptipo_ope.Value = pGMF.tipo_ope;
                        ptipo_ope.Direction = ParameterDirection.Input;
                        ptipo_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_ope);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pGMF.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pGMF.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pafecta_producto = cmdTransaccionFactory.CreateParameter();
                        pafecta_producto.ParameterName = "p_afecta_producto";
                        if (pGMF.afecta_producto == null)
                            pafecta_producto.Value = DBNull.Value;
                        else
                            pafecta_producto.Value = pGMF.afecta_producto;
                        pafecta_producto.Direction = ParameterDirection.Input;
                        pafecta_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pafecta_producto);

                        DbParameter pasume = cmdTransaccionFactory.CreateParameter();
                        pasume.ParameterName = "p_asume";
                        if (pGMF.asume == null)
                            pasume.Value = 0;
                        else
                            pasume.Value = pGMF.asume;
                        pasume.Direction = ParameterDirection.Input;
                        pasume.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasume);

                        DbParameter pporasume_cliente = cmdTransaccionFactory.CreateParameter();
                        pporasume_cliente.ParameterName = "p_porasume_cliente";
                        if (pGMF.porasume_cliente == null)
                            pporasume_cliente.Value = DBNull.Value;
                        else
                            pporasume_cliente.Value = pGMF.porasume_cliente;
                        pporasume_cliente.Direction = ParameterDirection.Input;
                        pporasume_cliente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporasume_cliente);

                        DbParameter pmaneja_exentas = cmdTransaccionFactory.CreateParameter();
                        pmaneja_exentas.ParameterName = "p_maneja_exentas";
                        if (pGMF.maneja_exentas == null)
                            pmaneja_exentas.Value = DBNull.Value;
                        else
                            pmaneja_exentas.Value = pGMF.maneja_exentas;
                        pmaneja_exentas.Direction = ParameterDirection.Input;
                        pmaneja_exentas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_exentas);

                        DbParameter ppago_cheque = cmdTransaccionFactory.CreateParameter();
                        ppago_cheque.ParameterName = "p_pago_cheque";
                        if (pGMF.pago_cheque == null)
                            ppago_cheque.Value = DBNull.Value;
                        else
                            ppago_cheque.Value = pGMF.pago_cheque;
                        ppago_cheque.Direction = ParameterDirection.Input;
                        ppago_cheque.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_cheque);

                        DbParameter ppago_efectivo = cmdTransaccionFactory.CreateParameter();
                        ppago_efectivo.ParameterName = "p_pago_efectivo";
                        if (pGMF.pago_efectivo == null)
                            ppago_efectivo.Value = DBNull.Value;
                        else
                            ppago_efectivo.Value = pGMF.pago_efectivo;
                        ppago_efectivo.Direction = ParameterDirection.Input;
                        ppago_efectivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_efectivo);

                        DbParameter ppago_traslado = cmdTransaccionFactory.CreateParameter();
                        ppago_traslado.ParameterName = "p_pago_traslado";
                        if (pGMF.pago_traslado == null)
                            ppago_traslado.Value = DBNull.Value;
                        else
                            ppago_traslado.Value = pGMF.pago_traslado;
                        ppago_traslado.Direction = ParameterDirection.Input;
                        ppago_traslado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_traslado);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pGMF.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pGMF.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pGMF.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pGMF.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PARAMGMF_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGMF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GMFData", "CrearGMF", ex);
                        return null;
                    }
                }
            }
        }


        public ParametroGMF ModificarGMF(Int64 idobjeto, ParametroGMF pGMF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametrogmf = cmdTransaccionFactory.CreateParameter();
                        pidparametrogmf.ParameterName = "p_idparametrogmf";
                        pidparametrogmf.Value = idobjeto;
                        pidparametrogmf.Direction = ParameterDirection.Input;
                        pidparametrogmf.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametrogmf);

                        DbParameter ptipo_ope = cmdTransaccionFactory.CreateParameter();
                        ptipo_ope.ParameterName = "p_tipo_ope";
                        ptipo_ope.Value = pGMF.tipo_ope;
                        ptipo_ope.Direction = ParameterDirection.Input;
                        ptipo_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_ope);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pGMF.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pGMF.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pafecta_producto = cmdTransaccionFactory.CreateParameter();
                        pafecta_producto.ParameterName = "p_afecta_producto";
                        if (pGMF.afecta_producto == null)
                            pafecta_producto.Value = DBNull.Value;
                        else
                            pafecta_producto.Value = pGMF.afecta_producto;
                        pafecta_producto.Direction = ParameterDirection.Input;
                        pafecta_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pafecta_producto);

                        DbParameter pasume = cmdTransaccionFactory.CreateParameter();
                        pasume.ParameterName = "p_asume";
                        if (pGMF.asume == null)
                            pasume.Value = 0;
                        else
                            pasume.Value = pGMF.asume;
                        pasume.Direction = ParameterDirection.Input;
                        pasume.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasume);

                        DbParameter pporasume_cliente = cmdTransaccionFactory.CreateParameter();
                        pporasume_cliente.ParameterName = "p_porasume_cliente";
                        if (pGMF.porasume_cliente == null)
                            pporasume_cliente.Value = DBNull.Value;
                        else
                            pporasume_cliente.Value = pGMF.porasume_cliente;
                        pporasume_cliente.Direction = ParameterDirection.Input;
                        pporasume_cliente.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporasume_cliente);

                        DbParameter pmaneja_exentas = cmdTransaccionFactory.CreateParameter();
                        pmaneja_exentas.ParameterName = "p_maneja_exentas";
                        if (pGMF.maneja_exentas == null)
                            pmaneja_exentas.Value = DBNull.Value;
                        else
                            pmaneja_exentas.Value = pGMF.maneja_exentas;
                        pmaneja_exentas.Direction = ParameterDirection.Input;
                        pmaneja_exentas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmaneja_exentas);

                        DbParameter ppago_cheque = cmdTransaccionFactory.CreateParameter();
                        ppago_cheque.ParameterName = "p_pago_cheque";
                        if (pGMF.pago_cheque == null)
                            ppago_cheque.Value = DBNull.Value;
                        else
                            ppago_cheque.Value = pGMF.pago_cheque;
                        ppago_cheque.Direction = ParameterDirection.Input;
                        ppago_cheque.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_cheque);

                        DbParameter ppago_efectivo = cmdTransaccionFactory.CreateParameter();
                        ppago_efectivo.ParameterName = "p_pago_efectivo";
                        if (pGMF.pago_efectivo == null)
                            ppago_efectivo.Value = DBNull.Value;
                        else
                            ppago_efectivo.Value = pGMF.pago_efectivo;
                        ppago_efectivo.Direction = ParameterDirection.Input;
                        ppago_efectivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_efectivo);

                        DbParameter ppago_traslado = cmdTransaccionFactory.CreateParameter();
                        ppago_traslado.ParameterName = "p_pago_traslado";
                        if (pGMF.pago_traslado == null)
                            ppago_traslado.Value = DBNull.Value;
                        else
                            ppago_traslado.Value = pGMF.pago_traslado;
                        ppago_traslado.Direction = ParameterDirection.Input;
                        ppago_traslado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ppago_traslado);

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "p_tipo_producto";
                        if (pGMF.tipo_producto == null)
                            ptipo_producto.Value = DBNull.Value;
                        else
                            ptipo_producto.Value = pGMF.tipo_producto;
                        ptipo_producto.Direction = ParameterDirection.Input;
                        ptipo_producto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);

                        DbParameter pcod_linea = cmdTransaccionFactory.CreateParameter();
                        pcod_linea.ParameterName = "p_cod_linea";
                        if (pGMF.cod_linea == null)
                            pcod_linea.Value = DBNull.Value;
                        else
                            pcod_linea.Value = pGMF.cod_linea;
                        pcod_linea.Direction = ParameterDirection.Input;
                        pcod_linea.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PARAMGMF_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGMF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GMFData", "ModificarGMF", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarGMF(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ParametroGMF pGMF = new ParametroGMF();
                        pGMF = ConsultarGMF(pId, vUsuario);

                        DbParameter pidparametrogmf = cmdTransaccionFactory.CreateParameter();
                        pidparametrogmf.ParameterName = "p_idparametrogmf";
                        pidparametrogmf.Value = pGMF.idparametrogmf;
                        pidparametrogmf.Direction = ParameterDirection.Input;
                        pidparametrogmf.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametrogmf);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_PARAMGMF_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GMFData", "EliminarGMF", ex);
                    }
                }
            }
        }


        public ParametroGMF ConsultarGMF(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametroGMF entidad = new ParametroGMF();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PARAMETRO_GMF WHERE IDPARAMETROGMF = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETROGMF"] != DBNull.Value) entidad.idparametrogmf = Convert.ToInt64(resultado["IDPARAMETROGMF"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["AFECTA_PRODUCTO"] != DBNull.Value) entidad.afecta_producto = Convert.ToInt32(resultado["AFECTA_PRODUCTO"]);
                            if (resultado["ASUME"] != DBNull.Value) entidad.asume = Convert.ToInt32(resultado["ASUME"]);
                            if (resultado["PORASUME_CLIENTE"] != DBNull.Value) entidad.porasume_cliente = Convert.ToDecimal(resultado["PORASUME_CLIENTE"]);
                            if (resultado["MANEJA_EXENTAS"] != DBNull.Value) entidad.maneja_exentas = Convert.ToInt32(resultado["MANEJA_EXENTAS"]);
                            if (resultado["PAGO_CHEQUE"] != DBNull.Value) entidad.pago_cheque = Convert.ToInt32(resultado["PAGO_CHEQUE"]);
                            if (resultado["PAGO_EFECTIVO"] != DBNull.Value) entidad.pago_efectivo = Convert.ToInt32(resultado["PAGO_EFECTIVO"]);
                            if (resultado["PAGO_TRASLADO"] != DBNull.Value) entidad.pago_traslado = Convert.ToInt32(resultado["PAGO_TRASLADO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
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
                        BOExcepcion.Throw("GMFData", "ConsultarGMF", ex);
                        return null;
                    }
                }
            }
        }


        public List<ParametroGMF> combooperacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ParametroGMF> lstComponenteAdicional = new List<ParametroGMF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from tipo_ope";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametroGMF entidad = new ParametroGMF();
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = (Convert.ToString(resultado["descripcion"]));
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = (Convert.ToInt32(resultado["TIPO_OPE"]));
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ComponenteAdicionalData", "ListarComponenteAdicional", ex);
                        return null;
                    }
                }
            }
        }



        public List<ParametroGMF> ListarGMF(String filtro, ParametroGMF pGMF, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametroGMF> lstGMF = new List<ParametroGMF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.*,t.descripcion as operacion,tt.descripcion as transaccion, Case tt.tipo_producto When 3 Then (Select l.descripcion From lineaahorro l Where l.cod_linea_ahorro = p.cod_linea)
                      When 5 Then (Select l.descripcion From lineacdat l Where l.cod_lineacdat = p.cod_linea) End AS nom_linea FROM PARAMETRO_GMF p  left join tipo_ope t on t.tipo_ope=p.tipo_ope left join tipo_tran tt on tt.tipo_tran=p.tipo_tran" + filtro + " ORDER BY IDPARAMETROGMF ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ParametroGMF entidad = new ParametroGMF();
                            if (resultado["IDPARAMETROGMF"] != DBNull.Value) entidad.idparametrogmf = Convert.ToInt64(resultado["IDPARAMETROGMF"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["AFECTA_PRODUCTO"] != DBNull.Value) entidad.afecta_producto = Convert.ToInt32(resultado["AFECTA_PRODUCTO"]);
                            if (resultado["ASUME"] != DBNull.Value) entidad.asume = Convert.ToInt32(resultado["ASUME"]);
                            if (resultado["PORASUME_CLIENTE"] != DBNull.Value) entidad.porasume_cliente = Convert.ToDecimal(resultado["PORASUME_CLIENTE"]);
                            if (resultado["MANEJA_EXENTAS"] != DBNull.Value) entidad.maneja_exentas = Convert.ToInt32(resultado["MANEJA_EXENTAS"]);
                            if (resultado["PAGO_CHEQUE"] != DBNull.Value) entidad.pago_cheque = Convert.ToInt32(resultado["PAGO_CHEQUE"]);
                            if (resultado["PAGO_EFECTIVO"] != DBNull.Value) entidad.pago_efectivo = Convert.ToInt32(resultado["PAGO_EFECTIVO"]);
                            if (resultado["PAGO_TRASLADO"] != DBNull.Value) entidad.pago_traslado = Convert.ToInt32(resultado["PAGO_TRASLADO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                            if (resultado["operacion"] != DBNull.Value) entidad.operacion = Convert.ToString(resultado["operacion"]);
                            if (resultado["transaccion"] != DBNull.Value) entidad.transaccion = Convert.ToString(resultado["transaccion"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nom_linea"]); 
                            lstGMF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGMF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GMFData", "ListarGMF", ex);
                        return null;
                    }
                }
            }
        }

        
        public ParametroGMF ModificarEstadoTranGmf(ParametroGMF pGMF, DateTime fecha,DateTime fechafinal, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "update tran_gmf set estado=1 where estado=0 and fecha_oper between To_Date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + "and" + " To_Date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = "update tran_gmf set estado=1 where estado=0 and fecha_oper between '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pGMF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParamteroGmfData", "ModificarEstadoTranGmf", ex);
                        return null;
                    }
                }
            }
        }

        public int ModificarEstadoTranGmf(ParametroGMF Entidad, Usuario pUsaurio, DateTime fecha, DateTime fechafinal)
        {
            Int32 resultado = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsaurio))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                      
                        DbParameter pRespuesta = cmdTransaccionFactory.CreateParameter();
                        pRespuesta.ParameterName = "pRespuesta";
                        pRespuesta.Value = 0;
                        pRespuesta.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pRespuesta);

                        DbParameter PFECHAINICIAL = cmdTransaccionFactory.CreateParameter();
                        PFECHAINICIAL.ParameterName = "PFECHAINICIAL";
                        PFECHAINICIAL.Value = fecha;
                        PFECHAINICIAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PFECHAINICIAL);

                        DbParameter PFECHAFINAL = cmdTransaccionFactory.CreateParameter();
                        PFECHAFINAL.ParameterName = "PFECHAFINAL";
                        PFECHAFINAL.Value = fechafinal;
                        PFECHAFINAL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PFECHAFINAL);

                        DbParameter PCOD_OPE = cmdTransaccionFactory.CreateParameter();
                        PCOD_OPE.ParameterName = "PCOD_OPE";
                        PCOD_OPE.Value = Entidad.operacion;
                        PCOD_OPE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PCOD_OPE);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_GMFMOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pRespuesta.Value != null)
                            resultado = Convert.ToInt32(pRespuesta.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroGMFData", "ModificarEstadoTranGmf", ex);
                        return resultado;
                    }
                }
            }
        }


    }
}
