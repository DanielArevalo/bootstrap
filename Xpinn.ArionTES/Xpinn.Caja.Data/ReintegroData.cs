using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Reintegro
    /// </summary>      
    public class ReintegroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Reintegro
        /// </summary>
        public ReintegroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una entidad Reintegro en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Reintegro</param>
        /// <returns>Entidad creada</returns>
        public Reintegro InsertarReintegro(Reintegro pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // en esta porcion de codigo se inserta primero la operacion realizada con el fin de ir alimentar la operacion cod_ope
                        // en la tabla TransaccionesCaja

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "P_IP";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pEntidad.fechaarqueo;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);
                        
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_ope = pcode_opera.Value != DBNull.Value ? Convert.ToInt64(pcode_opera.Value) : 0;
                        //en esta porcion de codigo se cera el reintegro y si tiene permisos el usuario
                        //se guarda la auditoria de ese registro

                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pcodex_opera = cmdTransaccionFactory.CreateParameter();
                        pcodex_opera.ParameterName = "pcodigooper";
                        pcodex_opera.Value = pEntidad.cod_ope;
                        pcodex_opera.Size = 8;
                        pcodex_opera.DbType = DbType.Int64;
                        pcodex_opera.Direction = ParameterDirection.Input;

                        DbParameter pfecha_reintegro = cmdTransaccionFactory.CreateParameter();
                        pfecha_reintegro.ParameterName = "pfechareintegro";
                        pfecha_reintegro.Value = pEntidad.fechareintegro;
                        pfecha_reintegro.DbType = DbType.DateTime;
                        pfecha_reintegro.Direction = ParameterDirection.Input;
                        pfecha_reintegro.Size = 7;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pEntidad.cod_caja;
                        pcod_caja.Size = 8;
                        pcod_caja.DbType = DbType.Int16;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pEntidad.cod_cajero;
                        pcod_cajero.Size = 8;
                        pcod_cajero.DbType = DbType.Int16;
                        pcod_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcodigobanco";
                        pcod_banco.Value = pEntidad.cod_banco;
                        pcod_banco.Size = 8;
                        pcod_banco.DbType = DbType.Int64;
                        pcod_banco.Direction = ParameterDirection.Input;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        pcod_moneda.Value = pEntidad.cod_moneda;
                        pcod_moneda.Size = 8;
                        pcod_moneda.DbType = DbType.Int16;
                        pcod_moneda.Direction = ParameterDirection.Input;

                        DbParameter pval_reintegro = cmdTransaccionFactory.CreateParameter();
                        pval_reintegro.ParameterName = "pvalorreintegro";
                        pval_reintegro.Value = pEntidad.valor_reintegro;
                        //pval_reintegro.Size = 8;
                        pval_reintegro.DbType = DbType.Decimal;
                        pval_reintegro.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcodex_opera);
                        cmdTransaccionFactory.Parameters.Add(pfecha_reintegro);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pval_reintegro);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REINTEGROS_C";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "REINTEGRO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Crear Reintegro");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_caja.ToString()), "REINTEGRO", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        //--------------------------------------------------------------------------------------//

                        //en esta porcion de codigo se inserta la transaccion realizada
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                        pfecha_mov.ParameterName = "pfechamov";
                        pfecha_mov.Value = pEntidad.fechaarqueo;

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "ptipomov";
                        ptipo_mov.Value = pEntidad.tipo_movimiento;

                        DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                        pcode_caja.ParameterName = "pcodigocaja";
                        pcode_caja.Value = pEntidad.cod_caja;

                        DbParameter pcode_oper = cmdTransaccionFactory.CreateParameter();
                        pcode_oper.ParameterName = "pcodigooper";
                        pcode_oper.Value = pcode_opera.Value;

                        DbParameter pcode_cajero = cmdTransaccionFactory.CreateParameter();
                        pcode_cajero.ParameterName = "pcodigocajero";
                        pcode_cajero.Value = pEntidad.cod_cajero;

                        DbParameter pcode_usuario = cmdTransaccionFactory.CreateParameter();
                        pcode_usuario.ParameterName = "pcodigousuario";
                        pcode_usuario.Value = pUsuario.codusuario;

                        DbParameter pcode_moneda = cmdTransaccionFactory.CreateParameter();
                        pcode_moneda.ParameterName = "pcodigomoneda";
                        pcode_moneda.Value = pEntidad.cod_moneda;

                        DbParameter pval_pago = cmdTransaccionFactory.CreateParameter();
                        pval_pago.ParameterName = "pvalorpago";
                        pval_pago.Value = pEntidad.valor_reintegro;

                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);
                        cmdTransaccionFactory.Parameters.Add(pcode_caja);
                        cmdTransaccionFactory.Parameters.Add(pcode_oper);
                        cmdTransaccionFactory.Parameters.Add(pcode_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuario);
                        cmdTransaccionFactory.Parameters.Add(pcode_moneda);
                        cmdTransaccionFactory.Parameters.Add(pval_pago);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRANSAC_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        //en esta porcion de codigo se va a enviar los saldos realiados 
                        //por el cajero en la caja especifica en una fecha
                        cmdTransaccionFactory.Parameters.Clear();
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Value = pEntidad.fechaarqueo;
                        pfecha.DbType = DbType.DateTime;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Size = 7;

                        DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                        pcoda_caja.ParameterName = "pcodigocaja";
                        pcoda_caja.Value = pEntidad.cod_caja;
                        pcoda_caja.Size = 8;
                        pcoda_caja.DbType = DbType.Int16;
                        pcoda_caja.Direction = ParameterDirection.Input;

                        DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                        pcoda_cajero.ParameterName = "pcodigocajero";
                        pcoda_cajero.Value = pEntidad.cod_cajero;
                        pcoda_cajero.Size = 8;
                        pcoda_cajero.DbType = DbType.Int16;
                        pcoda_cajero.Direction = ParameterDirection.Input;

                        DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                        pcodi_moneda.ParameterName = "pcodigomoneda";
                        pcodi_moneda.Value = pEntidad.cod_moneda;
                        pcodi_moneda.Size = 8;
                        pcodi_moneda.DbType = DbType.Int16;
                        pcodi_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = pEntidad.valor_reintegro;
                        //pvalor.Size = 8;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                        cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                        cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_REIN";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReintegroData", "InsertarReintegro", ex);
                        return null;
                    }

                }
            }
        }



        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public Reintegro ConsultarFecUltCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Reintegro entidad = new Reintegro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(fecha_proceso) as fecha_proceso from procesooficina WHERE cod_oficina=" + pUsuario.cod_oficina.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["fecha_proceso"] != DBNull.Value) entidad.fechaarqueo = Convert.ToDateTime(resultado["fecha_proceso"]);
                                                }
                       

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReintegroData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Cajero, Caja y Oficina de la base de datos
        /// </summary>
        /// <param name="pUsuario">identificador del usuario</param>
        /// <returns>Oficina consultada</returns>
        public Reintegro ConsultarCajero(Usuario pUsuario, bool pEsResponsableOficina=false)
        {
            DbDataReader resultado = default(DbDataReader);
            Reintegro entidad = new Reintegro();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT b.cod_cajero codcajero, a.nombre nomcajero,  c.cod_caja codcaja, c.nombre nomcaja,c.esprincipal principal, d.cod_oficina codoficina,d.nombre nomoficina,(select decode(max(x.fecha_proceso),null,sysdate,max(x.fecha_proceso)) from procesooficina x where x.cod_oficina=d.cod_oficina) fechaproceso, (Select x.nomciudad from ciudades x where x.codciudad=d.cod_ciudad) ciudad FROM USUARIOS a, CAJERO b, CAJA c, OFICINA d WHERE a.CODUSUARIO=" + pUsuario.codusuario.ToString() + " and a.codusuario=b.cod_persona and b.cod_caja=c.cod_caja and C.COD_OFICINA=d.cod_oficina ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["nomcajero"] != DBNull.Value) entidad.nomcajero = Convert.ToString(resultado["nomcajero"]);
                            if (resultado["codcajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["codcajero"]);
                            if (resultado["codcaja"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["codcaja"]);
                            if (resultado["nomcaja"] != DBNull.Value) entidad.nomcaja = Convert.ToString(resultado["nomcaja"]);
                            if (resultado["codoficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["codoficina"]);
                            if (resultado["nomoficina"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["nomoficina"]);
                            if (resultado["principal"] != DBNull.Value) entidad.esprincipal = Convert.ToInt64(resultado["principal"]);
                            if (resultado["fechaproceso"] != DBNull.Value) entidad.fechareintegro = Convert.ToDateTime(resultado["fechaproceso"]);
                            if (resultado["ciudad"] != DBNull.Value) entidad.nomciudad = Convert.ToString(resultado["ciudad"]);
                        }


                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReintegroData", "ConsultarCajero", ex);
                        return null;
                    }

                }

            }
        }


    }
}
