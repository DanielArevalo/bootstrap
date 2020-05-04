using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;


namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TransaccionesCaja
    /// </summary>
    public class TransaccionCajaData : GlobalData
    {
        Configuracion conf = new Configuracion();
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TransaccionesCaja
        /// </summary>
        public TransaccionCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TransaccionesCaja de la base de datos
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja CrearTransaccionCajaReversion(TransaccionCaja pTransaccionCaja, GridView gvOperaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;


                        CheckBox chkAnula;
                        int codOpe = 0;

                        List<Xpinn.Caja.Entities.TransaccionCaja> transacCajaLst = new List<Xpinn.Caja.Entities.TransaccionCaja>();
                        Xpinn.Caja.Entities.TransaccionCaja transacCaja1 = new Xpinn.Caja.Entities.TransaccionCaja();

                        // Recorre listado de operaciones determinando cuales fueron marcadas para anular

                        long codMotivo = 0;
                        foreach (GridViewRow fila in gvOperaciones.Rows)
                        {
                            codOpe = int.Parse(fila.Cells[1].Text);
                            chkAnula = (CheckBox)fila.FindControl("chkAnula");

                            if (chkAnula.Checked == true)
                            {
                                transacCaja1.cod_ope = codOpe;
                                codMotivo = transacCaja1.cod_motivo_reversion;

                                // Carga listado de transacciones de caja de la operaciòn
                                transacCajaLst = ListarTransaccionesPendientes(transacCaja1, pUsuario);
                                transacCaja1.cod_motivo_reversion = codMotivo;

                                // Para cada una de las transacciones de caja realiza la anulaciòn y actualiza saldo de caja
                                foreach (TransaccionCaja fil in transacCajaLst)
                                {
                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pcod_tipo_anulacion = cmdTransaccionFactory.CreateParameter();
                                    pcod_tipo_anulacion.ParameterName = "pcodigomotivoreversion";
                                    pcod_tipo_anulacion.Value = pTransaccionCaja.cod_motivo_reversion;
                                    pcod_tipo_anulacion.Size = 8;
                                    pcod_tipo_anulacion.DbType = DbType.Int64;
                                    pcod_tipo_anulacion.Direction = ParameterDirection.Input;

                                    DbParameter pcod_transac = cmdTransaccionFactory.CreateParameter();
                                    pcod_transac.ParameterName = "pcodigotransaccion";
                                    pcod_transac.Value = fil.cod_movimiento;
                                    pcod_transac.Size = 8;
                                    pcod_transac.DbType = DbType.Int64;
                                    pcod_transac.Direction = ParameterDirection.Input;

                                    DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                                    pcod_usuario.ParameterName = "pcodigousuario";
                                    pcod_usuario.Value = pUsuario.codusuario;
                                    pcod_usuario.Size = 8;
                                    pcod_usuario.DbType = DbType.Int64;
                                    pcod_usuario.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcod_tipo_anulacion);
                                    cmdTransaccionFactory.Parameters.Add(pcod_transac);
                                    cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_REVER_TRANCAJA_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                    TransaccionCaja transaccion = new TransaccionCaja
                                    {
                                        cod_motivo_reversion = pTransaccionCaja.cod_motivo_reversion,
                                        cod_movimiento = fil.cod_movimiento,
                                        codigo_usuario = pUsuario.codusuario
                                    };

                                    DAauditoria.InsertarLog(transaccion, "Todos los productos", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Creacion de reversion de transaccion con numero " + fil.cod_movimiento);
                                }

                                // En el caso de operaciòn de pagos realiza la anulaciòn de los productos aplicados.
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                                pcod_ope.ParameterName = "pn_cod_ope";
                                pcod_ope.Value = long.Parse(codOpe.ToString());
                                pcod_ope.Size = 8;
                                pcod_ope.DbType = DbType.Int64;
                                pcod_ope.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                                pusuario.ParameterName = "pn_usuario";
                                pusuario.Value = pUsuario.codusuario;
                                pusuario.Size = 8;
                                pusuario.DbType = DbType.Int64;
                                pusuario.Direction = ParameterDirection.Input;

                                DbParameter pValor_Retorno = cmdTransaccionFactory.CreateParameter();
                                pValor_Retorno.ParameterName = "pb_resultado";
                                pValor_Retorno.Size = 8;
                                pValor_Retorno.Value = 0;
                                pValor_Retorno.DbType = DbType.Int64;
                                pValor_Retorno.Direction = ParameterDirection.InputOutput;

                                cmdTransaccionFactory.Parameters.Add(pcod_ope);
                                cmdTransaccionFactory.Parameters.Add(pusuario);
                                cmdTransaccionFactory.Parameters.Add(pValor_Retorno);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REVERSION";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                if (!string.Equals(pValor_Retorno.Value.ToString(), '0'))
                                {
                                    pTransaccionCaja.resultado = Convert.ToInt16(pValor_Retorno.Value);
                                }

                            }
                        }

                        //pTransaccionCaja.cod_movimiento = Convert.ToInt64(pCOD_MOVIMIENTO.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "CrearTransaccionCajaReversion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TransaccionesCaja de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TransaccionCaja modificada</returns>
        public TransaccionCaja ModificarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOVIMIENTO.ParameterName = param + "COD_MOVIMIENTO";
                        pCOD_MOVIMIENTO.Value = pTransaccionCaja.cod_movimiento;

                        DbParameter pCOD_CAJA = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJA.ParameterName = param + "COD_CAJA";
                        pCOD_CAJA.Value = pTransaccionCaja.cod_caja;

                        DbParameter pCOD_CAJERO = cmdTransaccionFactory.CreateParameter();
                        pCOD_CAJERO.ParameterName = param + "COD_CAJERO";
                        pCOD_CAJERO.Value = pTransaccionCaja.cod_cajero;

                        DbParameter pFECHA_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_MOVIMIENTO.ParameterName = param + "FECHA_MOVIMIENTO";
                        pFECHA_MOVIMIENTO.Value = pTransaccionCaja.fecha_movimiento;

                        DbParameter pFECHA_APLICA = cmdTransaccionFactory.CreateParameter();
                        pFECHA_APLICA.ParameterName = param + "FECHA_APLICA";
                        pFECHA_APLICA.Value = pTransaccionCaja.fecha_aplica;

                        DbParameter pTIPO_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MOVIMIENTO.ParameterName = param + "TIPO_MOVIMIENTO";
                        pTIPO_MOVIMIENTO.Value = pTransaccionCaja.tipo_movimiento;

                        DbParameter pCOD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        pCOD_PERSONA.ParameterName = param + "COD_PERSONA";
                        pCOD_PERSONA.Value = pTransaccionCaja.cod_persona;

                        DbParameter pNUM_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pNUM_PRODUCTO.ParameterName = param + "NUM_PRODUCTO";
                        pNUM_PRODUCTO.Value = pTransaccionCaja.num_producto;

                        DbParameter pTIPO_PAGO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_PAGO.ParameterName = param + "TIPO_PAGO";
                        pTIPO_PAGO.Value = pTransaccionCaja.tipo_pago;

                        DbParameter pCOD_MONEDA = cmdTransaccionFactory.CreateParameter();
                        pCOD_MONEDA.ParameterName = param + "COD_MONEDA";
                        pCOD_MONEDA.Value = pTransaccionCaja.cod_moneda;

                        DbParameter pVALOR_PAGO = cmdTransaccionFactory.CreateParameter();
                        pVALOR_PAGO.ParameterName = param + "VALOR_PAGO";
                        pVALOR_PAGO.Value = pTransaccionCaja.valor_pago;

                        DbParameter pTASA_CAMBIO = cmdTransaccionFactory.CreateParameter();
                        pTASA_CAMBIO.ParameterName = param + "TASA_CAMBIO";
                        pTASA_CAMBIO.Value = pTransaccionCaja.tasa_cambio;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOVIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJA);
                        cmdTransaccionFactory.Parameters.Add(pCOD_CAJERO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_MOVIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_APLICA);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_MOVIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(pNUM_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pTIPO_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MONEDA);
                        cmdTransaccionFactory.Parameters.Add(pVALOR_PAGO);
                        cmdTransaccionFactory.Parameters.Add(pTASA_CAMBIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_TransaccionesCaja_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTransaccionCaja, pUsuario, pTransaccionCaja.cod_movimiento, "TRANSACCIONESCAJA", Accion.Modificar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        dbConnectionFactory.CerrarConexion(connection);
                        return pTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ModificarTransaccionCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TransaccionesCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TransaccionesCaja</param>
        public void EliminarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TransaccionCaja pTransaccionCaja = new TransaccionCaja();

                        if (pUsuario.programaGeneraLog)
                            pTransaccionCaja = ConsultarTransaccionCaja(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pCOD_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOVIMIENTO.ParameterName = param + "COD_MOVIMIENTO";
                        pCOD_MOVIMIENTO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOVIMIENTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "Xpinn_Caja_TransaccionesCaja_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTransaccionCaja, pUsuario, pTransaccionCaja.cod_movimiento, "TRANSACCIONESCAJA", Accion.Eliminar.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "InsertarTransaccionCaja", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TransaccionesCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TransaccionesCaja</param>
        /// <returns>Entidad TransaccionCaja consultado</returns>
        public TransaccionCaja ConsultarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TransaccionCaja entidad = new TransaccionCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TRANSACCIONESCAJA WHERE cod_movimiento = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_MOVIMIENTO"] == DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["COD_MOVIMIENTO"]);
                            if (resultado["COD_CAJA"] == DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] == DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_MOVIMIENTO"] == DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["FECHA_APLICA"] == DBNull.Value) entidad.fecha_aplica = Convert.ToDateTime(resultado["FECHA_APLICA"]);
                            if (resultado["TIPO_MOVIMIENTO"] == DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["TIPO_MOVIMIENTO"]);
                            if (resultado["COD_PERSONA"] == DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUM_PRODUCTO"] == DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["NUM_PRODUCTO"]);
                            if (resultado["TIPO_PAGO"] == DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["TIPO_PAGO"]);
                            if (resultado["COD_MONEDA"] == DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_PAGO"] == DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (resultado["TASA_CAMBIO"] == DBNull.Value) entidad.tasa_cambio = Convert.ToInt64(resultado["TASA_CAMBIO"]);
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
                        BOExcepcion.Throw("TransaccionCajaData", "ConsultarTransaccionCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene un registro en la tabla TransaccionesCaja de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TransaccionesCaja</param>
        /// <returns>Entidad TransaccionCaja consultado</returns>
        public TransaccionCaja ConsultarParametroCastigos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TransaccionCaja entidad = new TransaccionCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select COD_LINEA_CREDITO from parametros_linea where cod_parametro = 320";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {

                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.parametro = Convert.ToString(resultado["cod_linea_credito"]);


                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ConsultarParametroCastigos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TRANSACCIONESCAJA " + ObtenerFiltro(pTransaccionCaja);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["COD_MOVIMIENTO"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["COD_MOVIMIENTO"]);
                            if (resultado["COD_CAJA"] != DBNull.Value) entidad.cod_caja = Convert.ToInt64(resultado["COD_CAJA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["FECHA_APLICA"] != DBNull.Value) entidad.fecha_aplica = Convert.ToDateTime(resultado["FECHA_APLICA"]);
                            if (resultado["TIPO_MOVIMIENTO"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["TIPO_MOVIMIENTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["NUM_PRODUCTO"]);
                            if (resultado["TIPO_PAGO"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["TIPO_PAGO"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt64(resultado["COD_MONEDA"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (resultado["TASA_CAMBIO"] != DBNull.Value) entidad.tasa_cambio = Convert.ToInt64(resultado["TASA_CAMBIO"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTransaccionCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransacciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.num_trancaja, a.cod_ope, a.tipo_tran, a.cod_cajero, (Select u.nombre From usuarios u, cajero c Where u.codusuario = c.cod_persona And c.cod_cajero = a.cod_cajero) As nom_cajero,
                                        (select s.descripcion from tipoproductocaja s where s.codtipoproductocaja = (select y.tipo_producto from tipo_tran y where y.tipo_tran=a.tipo_tran)) tipoprod,
                                        (select x.tipo_ope from operacion x where x.cod_ope = a.cod_ope) codtipoope,
                                        (select y.descripcion from tipo_ope y where y.tipo_ope = (select x.tipo_ope from operacion x where x.cod_ope=a.cod_ope)) nomtipope,
                                        a.fecha_movimiento, a.tipo_movimiento, (select x.identificacion from persona x where x.cod_persona = a.cod_persona) iden_persona,
                                        (select x.primer_nombre||' '||x.segundo_nombre||' '||x.primer_apellido||' '||x.segundo_apellido from persona x where x.cod_persona = a.cod_persona) nom_persona,
                                        a.num_producto, (valor_pago- nvl((select x.valor from movimientocaja x where x.cod_ope=a.cod_ope and x.cod_tipo_pago not in(1,2)),0)) valor_total,
                                        (select x.nombre from usuarios x where x.codusuario = a.cod_persona) usuario,
                                        (select x.identificacion from usuarios x where x.codusuario = a.cod_persona) iden_usuario 
                                        FROM TRANSACCIONESCAJA a, CAJA b WHERE a.cod_caja = b.cod_caja AND a.cod_caja = " + pTransaccionCaja.cod_caja + " AND b.cod_oficina = " + pTransaccionCaja.cod_oficina + " AND to_char(a.fecha_movimiento,'dd/MM/yyyy') = '" + pTransaccionCaja.fecha_cierre.ToShortDateString() + "' order by a.fecha_movimiento asc, a.cod_ope asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["num_trancaja"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["num_trancaja"]);
                            if (resultado["fecha_movimiento"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fecha_movimiento"]);
                            if (resultado["tipo_movimiento"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipo_movimiento"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["tipoprod"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tipoprod"]);
                            if (resultado["codtipoope"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["codtipoope"]);
                            if (resultado["nomtipope"] != DBNull.Value) entidad.nom_tipo_ope = Convert.ToString(resultado["nomtipope"]);

                            //se valida que todo lo que sea registro de operaciones aparezca el cliente sino saldra el usuario quien realizo la operacion
                            if (entidad.tipo_ope == 120)
                            {
                                if (resultado["iden_persona"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_persona"]);
                                if (resultado["nom_persona"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["nom_persona"]);
                            }
                            else
                            {
                                if (resultado["iden_usuario"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_usuario"]);
                                if (resultado["usuario"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["usuario"]);
                            }

                            if (resultado["num_producto"] != DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["num_producto"]);
                            if (resultado["valor_total"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor_total"]);
                            if (resultado["cod_cajero"] != DBNull.Value) entidad.cod_cajero = Convert.ToInt64(resultado["cod_cajero"]);
                            if (resultado["nom_cajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["nom_cajero"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTransacciones", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesComprobante(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = " Select moneda, fecha_oper, tipo_ope, tipo_movimiento, NOMBRE_EJE, tipo_producto, cod_linea_credito, nombre_atributo, tipo_cta, numero_radicacion, valor, cod_ope " +
                                  " From VCAJATRANSACCIONES " +
                                  " where num_comp =- 1  and cod_tipo_ope != 2 and cod_ofi=" + pTransaccionCaja.cod_oficina + " and trunc(fecha_oper) = To_Date('" + pTransaccionCaja.fecha_cierre.ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = " Select moneda, fecha_oper, tipo_ope, tipo_movimiento, NOMBRE_EJE, tipo_producto, cod_linea_credito, nombre_atributo, tipo_cta, numero_radicacion, valor, cod_ope " +
                                  " From VCAJATRANSACCIONES " +
                                  " where num_comp =- 1  and cod_tipo_ope != 2 and cod_ofi=" + pTransaccionCaja.cod_oficina + " and trunc(fecha_oper) = '" + pTransaccionCaja.fecha_cierre.ToShortDateString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["moneda"]);
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["tipo_movimiento"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipo_movimiento"]);
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.nom_tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["nombre_atributo"] != DBNull.Value) entidad.nombre_atributo = Convert.ToString(resultado["nombre_atributo"]);
                            if (resultado["tipo_cta"] != DBNull.Value) entidad.tipo_cta = Convert.ToString(resultado["tipo_cta"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToString(resultado["numero_radicacion"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);
                            if (resultado["NOMBRE_EJE"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["NOMBRE_EJE"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTransaccionesComprobante", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo para mostrar la grilla de total de transacciones
        /// </summary>
        /// <param name="pTransaccionCaja"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<TransaccionCaja> ListarTransaccionesComprobanteTot(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = " Select moneda, tipo_movimiento, Sum(valor) as valor " +
                                     " From VCAJATRANSACCIONES " +
                                     " Where num_comp = -1 And cod_ofi=" + pTransaccionCaja.cod_oficina + " And to_char(fecha_oper,'dd/MM/yyyy') = '" + pTransaccionCaja.fecha_cierre.ToShortDateString() + "' " +
                                     " Group by moneda, tipo_movimiento Order by 1, 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["moneda"]);
                            if (resultado["tipo_movimiento"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipo_movimiento"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTransaccionesComprobanteTot", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// Movimientos de Caja
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarMovimientosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql =
                                " Select tipo_tran.DESCRIPCION   as tipo_tran,tipoproducto.descripcion as nom_tipo_producto, tipo_tran.tipo_producto,a.cod_ope, " +
                                " d.cod_oficina coficina, d.nombre nomficina, cj.nombre ncaja, e.descripcion nmoneda, a.cod_moneda moneda, a.cod_caja ccaja, a.cod_cajero ccajero, a.fecha_movimiento fechamov, a.tipo_movimiento tipomov, a.cod_persona codpersona, a.num_producto producto, " +
                                " (a.valor_pago - nvl((select x.valor from movimientocaja x where x.cod_ope = a.cod_ope and x.cod_tipo_pago Not In (1,2)),0) ) valor, " +
                                " x.primer_nombre || ' ' || x.segundo_nombre || ' ' || x.primer_apellido || ' ' || x.segundo_apellido nom_cliente, x.identificacion iden_cliente, " +
                                " op.tipo_ope codtipoope, op.num_comp, op.tipo_comp, u.nombre usuario, u.identificacion iden_usuario " +
                                " From caja cj Left Join oficina d On d.cod_oficina = cj.cod_oficina, transaccionescaja a left join tipo_tran on a.tipo_tran=tipo_tran.tipo_tran  left join tipoproducto on tipoproducto.cod_tipo_producto=tipo_tran.tipo_producto  Left Join operacion op On op.cod_ope = a.cod_ope Left Join tipomoneda e On e.cod_moneda = a.cod_moneda Left Join persona x On x.cod_persona = a.cod_persona Left Join usuarios u On u.codusuario = op.cod_usu, caja b " +
                                " Where cj.cod_caja = a.cod_caja And a.cod_caja = (Case When " + pTransaccionCaja.cod_caja.ToString() + " = 0 Then a.cod_caja else " + pTransaccionCaja.cod_caja.ToString() + " end) And a.cod_cajero = (Case When " + pTransaccionCaja.cod_cajero.ToString() + " = 0 Then a.cod_cajero else " + pTransaccionCaja.cod_cajero.ToString() + " End) And a.cod_moneda = " + pTransaccionCaja.cod_moneda.ToString() +
                                " And a.cod_caja = b.cod_caja ";

                        // Parametro 28 define la caja que es super auxiliar, si no lo es filtro por oficina, si no dejo libertad
                        if (!pTransaccionCaja.es_super_auxiliar)
                        {
                            sql += " And b.cod_oficina = " + pUsuario.cod_oficina;
                        }

                        sql += @" and a.cod_ope != 0    And trunc(FECHA_MOVIMIENTO) between To_Date('" + pTransaccionCaja.fecha_consulta_inicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + pTransaccionCaja.fecha_consulta_final.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";
                        sql += " ORDER BY a.cod_ope ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["nomficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["nomficina"]);
                            if (resultado["ncaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["ncaja"]);
                            if (resultado["fechamov"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fechamov"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipomov"]);
                            if (entidad.tipo_movimiento == "INGRESO")
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);
                            }
                            else
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]) * -1;
                            }
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);
                            if (resultado["codtipoope"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["codtipoope"]);

                            //se valida que todo lo que sea registro de operaciones aparezca el cliente sino saldra el usuario quien realizo la operacion
                            if (entidad.tipo_ope == 120)
                            {
                                if (resultado["iden_cliente"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_cliente"]);
                                if (resultado["nom_cliente"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["nom_cliente"]);
                            }
                            else
                            {
                                if (resultado["iden_usuario"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_usuario"]);
                                if (resultado["usuario"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["usuario"]);
                            }
                            if (resultado["nom_tipo_producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["nom_tipo_producto"]);
                            if (resultado["producto"] != DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["producto"]);
                            if (resultado["nmoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nmoneda"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["tipo_tran"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarMovimientosCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<TransaccionCaja> ListarTrasladosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql =
                                " Select tipo_tran.DESCRIPCION   as tipo_tran,tipoproducto.descripcion as nom_tipo_producto, tipo_tran.tipo_producto,a.cod_ope, " +
                                " d.cod_oficina coficina, d.nombre nomficina, cj.nombre ncaja, e.descripcion nmoneda, a.cod_moneda moneda, a.cod_caja ccaja, a.cod_cajero ccajero, a.fecha_movimiento fechamov, a.tipo_movimiento tipomov, a.cod_persona codpersona, a.num_producto producto, " +
                                " (a.valor_pago - nvl((select x.valor from movimientocaja x where x.cod_ope = a.cod_ope and x.cod_tipo_pago Not In (1,2)),0) ) valor, " +
                                " x.primer_nombre || ' ' || x.segundo_nombre || ' ' || x.primer_apellido || ' ' || x.segundo_apellido nom_cliente, x.identificacion iden_cliente, " +
                                " op.tipo_ope codtipoope, op.num_comp, op.tipo_comp, u.nombre usuario, u.identificacion iden_usuario " +
                                " From caja cj Left Join oficina d On d.cod_oficina = cj.cod_oficina, transaccionescaja a left join tipo_tran on a.tipo_tran=tipo_tran.tipo_tran  left join tipoproducto on tipoproducto.cod_tipo_producto=tipo_tran.tipo_producto  Left Join operacion op On op.cod_ope = a.cod_ope Left Join tipomoneda e On e.cod_moneda = a.cod_moneda Left Join persona x On x.cod_persona = a.cod_persona   inner join trasladocaja tc on tc.COD_OPE_TRASLADO=a.cod_ope and tc.COD_OPE_RECEPCION is null Left Join usuarios u On u.codusuario = op.cod_usu, caja b " +
                                " Where cj.cod_caja = a.cod_caja And a.cod_caja = (Case When " + pTransaccionCaja.cod_caja.ToString() + " = 0 Then a.cod_caja else " + pTransaccionCaja.cod_caja.ToString() + " end) And a.cod_cajero = (Case When " + pTransaccionCaja.cod_cajero.ToString() + " = 0 Then a.cod_cajero else " + pTransaccionCaja.cod_cajero.ToString() + " End) " +
                                " And a.cod_caja = b.cod_caja  and tipo_tran.tipo_tran=902 ";

                        // Parametro 28 define la caja que es super auxiliar, si no lo es filtro por oficina, si no dejo libertad
                        if (!pTransaccionCaja.es_super_auxiliar)
                        {
                            sql += " And b.cod_oficina = " + pUsuario.cod_oficina;
                        }

                        sql += @" and a.cod_ope != 0    And trunc(FECHA_MOVIMIENTO) between To_Date('" + pTransaccionCaja.fecha_consulta_final.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + pTransaccionCaja.fecha_consulta_final.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";
                        sql += " ORDER BY a.cod_ope ";
                            
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["nomficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["nomficina"]);
                            if (resultado["ncaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["ncaja"]);
                            if (resultado["fechamov"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fechamov"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipomov"]);
                            //if (entidad.tipo_movimiento == "INGRESO")
                          //  {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);
                            //}
                           // else
                          //  {
                              //  if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]) * -1;
                          //  }
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);
                            if (resultado["codtipoope"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["codtipoope"]);

                            //se valida que todo lo que sea registro de operaciones aparezca el cliente sino saldra el usuario quien realizo la operacion
                            if (entidad.tipo_ope == 120)
                            {
                                if (resultado["iden_cliente"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_cliente"]);
                                if (resultado["nom_cliente"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["nom_cliente"]);
                            }
                            else
                            {
                                if (resultado["iden_usuario"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_usuario"]);
                                if (resultado["usuario"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["usuario"]);
                            }
                            if (resultado["nom_tipo_producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["nom_tipo_producto"]);
                            if (resultado["producto"] != DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["producto"]);
                            if (resultado["nmoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nmoneda"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["tipo_tran"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTrasladosCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<TransaccionCaja> ListarSumaMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql =
                   "  Select Sum(  (a.valor_pago - nvl((select x.valor from movimientocaja x where x.cod_ope = a.cod_ope and x.cod_tipo_pago Not In (1,2)),0) ) )valor, a.tipo_movimiento as tipomov " +
                   " From caja cj Left Join oficina d On d.cod_oficina = cj.cod_oficina, transaccionescaja a left join tipo_tran on a.tipo_tran=tipo_tran.tipo_tran Left Join operacion op On op.cod_ope = a.cod_ope Left Join tipomoneda e On e.cod_moneda = a.cod_moneda Left Join persona x On x.cod_persona = a.cod_persona Left Join usuarios u On u.codusuario = op.cod_usu, caja b " +
                   " Where cj.cod_caja = a.cod_caja And a.cod_caja = (Case When " + pTransaccionCaja.cod_caja.ToString() + " = 0 Then a.cod_caja else " + pTransaccionCaja.cod_caja.ToString() + " end) And a.cod_cajero = (Case When " + pTransaccionCaja.cod_cajero.ToString() + " = 0 Then a.cod_cajero else " + pTransaccionCaja.cod_cajero.ToString() + " End) And a.cod_moneda = " + pTransaccionCaja.cod_moneda.ToString() +
                   " And a.cod_caja = b.cod_caja And b.cod_oficina = " + pUsuario.cod_oficina +
                   @" and a.cod_ope != 0 ";

                        if (pFechaInicial != null && pFechaInicial != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql += " A.FECHA_MOVIMIENTO  >= TO_DATE('" + Convert.ToDateTime(pFechaInicial).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";



                            }
                            else
                            {
                                sql += " A.FECHA_MOVIMIENTO >= '" + Convert.ToDateTime(pFechaInicial).ToString(conf.ObtenerFormatoFecha()) + "' ";

                            }
                        }

                        sql += "   group by  a.tipo_movimiento ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipomov"]);
                            if (entidad.tipo_movimiento == "INGRESO")
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);
                            }
                            else
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]) * -1;
                            }

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarMovimientosCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<TransaccionCaja> ListarTodosMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql =
                   " Select tipo_tran.tipo_producto,a.cod_ope, " +
                   " d.cod_oficina coficina, d.nombre nomficina, cj.nombre ncaja, e.descripcion nmoneda, a.cod_moneda moneda, a.cod_caja ccaja, a.cod_cajero ccajero, a.fecha_movimiento fechamov, a.tipo_movimiento tipomov, a.cod_persona codpersona, a.num_producto producto, " +
                   " (a.valor_pago - nvl((select x.valor from movimientocaja x where x.cod_ope = a.cod_ope and x.cod_tipo_pago Not In (1,2)),0) ) valor, " +
                   " x.primer_nombre || ' ' || x.segundo_nombre || ' ' || x.primer_apellido || ' ' || x.segundo_apellido nom_cliente, x.identificacion iden_cliente, " +
                   " op.tipo_ope codtipoope, op.num_comp, op.tipo_comp, u.nombre usuario, u.identificacion iden_usuario " +
                   " From caja cj Left Join oficina d On d.cod_oficina = cj.cod_oficina, transaccionescaja a left join tipo_tran on a.tipo_tran=tipo_tran.tipo_tran Left Join operacion op On op.cod_ope = a.cod_ope Left Join tipomoneda e On e.cod_moneda = a.cod_moneda Left Join persona x On x.cod_persona = a.cod_persona Left Join usuarios u On u.codusuario = op.cod_usu, caja b " +
                   " Where cj.cod_caja = a.cod_caja And a.cod_caja = (Case When " + pTransaccionCaja.cod_caja.ToString() + " = 0 Then a.cod_caja else " + pTransaccionCaja.cod_caja.ToString() + " end)  And a.cod_moneda = " + pTransaccionCaja.cod_moneda.ToString() +
                   " And a.cod_caja = b.cod_caja And b.cod_oficina = " + pUsuario.cod_oficina +
                   @" and a.cod_ope != 0 ";

                        if (pFechaInicial != null && pFechaInicial != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " AND ";
                            else
                                sql += " WHERE ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql += " A.FECHA_MOVIMIENTO  >= TO_DATE('" + Convert.ToDateTime(pFechaInicial).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";



                            }
                            else
                            {
                                sql += " A.FECHA_MOVIMIENTO >= '" + Convert.ToDateTime(pFechaInicial).ToString(conf.ObtenerFormatoFecha()) + "' ";

                            }
                        }

                        sql += " ORDER BY a.cod_ope ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["nomficina"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["nomficina"]);
                            if (resultado["ncaja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["ncaja"]);
                            if (resultado["fechamov"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fechamov"]);
                            if (resultado["tipomov"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["tipomov"]);
                            if (entidad.tipo_movimiento == "INGRESO")
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]);
                            }
                            else
                            {
                                if (resultado["valor"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["valor"]) * -1;
                            }
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);
                            if (resultado["codtipoope"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt64(resultado["codtipoope"]);

                            //se valida que todo lo que sea registro de operaciones aparezca el cliente sino saldra el usuario quien realizo la operacion
                            if (entidad.tipo_ope == 120)
                            {
                                if (resultado["iden_cliente"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_cliente"]);
                                if (resultado["nom_cliente"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["nom_cliente"]);
                            }
                            else
                            {
                                if (resultado["iden_usuario"] != DBNull.Value) entidad.iden_cliente = Convert.ToString(resultado["iden_usuario"]);
                                if (resultado["usuario"] != DBNull.Value) entidad.nom_cliente = Convert.ToString(resultado["usuario"]);
                            }
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["producto"] != DBNull.Value) entidad.num_producto = Convert.ToInt64(resultado["producto"]);
                            if (resultado["nmoneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nmoneda"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarMovimientosCaja", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// Transacciones de Caja Pendientes por pasar a contabilidad
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesPendientes(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select num_trancaja, fecha_movimiento " +
                                    " from transaccionescaja " +
                                     " where cod_ope=" + pTransaccionCaja.cod_ope;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["num_trancaja"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["num_trancaja"]);
                            if (resultado["fecha_movimiento"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fecha_movimiento"]);

                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarTransaccionPendientes", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// Transacciones de Caja Pendientes por pasar a contabilidad
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarOperaciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        Int64 codcajero_responsable = CajeroResponsableOficina(Convert.ToInt64(pTransaccionCaja.cod_oficina), pUsuario);
                        //Agregado para validar si el usuario es responsable de oficina
                        List<Int64> lstOficinas = OficinasUsuario(pUsuario);
                        Configuracion conf = new Configuracion();
                        string sql = "";

                        //Se hizo modificación de la consulta para validar el tipo de usuario y de acuerdo a ello listar las transacciones
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = " Select distinct a.cod_ope, a.fecha_oper, b.nombre caja, d.nombre cajero, (select s.descripcion from tipo_ope s where s.tipo_ope=a.tipo_ope) tip_ope, (select sum(valor_pago) from transaccionescaja x where x.cod_ope = a.cod_ope and x.tipo_movimiento='INGRESO') valor_ope_ing, (select sum(valor_pago) from transaccionescaja x where x.cod_ope=a.cod_ope and x.tipo_movimiento='EGRESO') valor_ope_egr,  (select min(cod_tipo_pago) from movimientocaja x where x.cod_ope=a.cod_ope) tipo_pago " +
                                    " From operacion a, caja b Join cajero c On b.cod_caja = c.cod_caja Join usuarios d On c.cod_persona = d.codusuario " +
                                    " Where a.cod_caja = b.cod_caja And a.cod_cajero = c.cod_cajero And a.tipo_ope In (30, 31, 32, 33, 120) And a.num_comp = -1 And Trunc(a.fecha_oper) = To_Date('" + pTransaccionCaja.fecha_cierre.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " +
                                    (pUsuario.codperfil == 1 ? " " : (lstOficinas.Count > 0 ? " And b.cod_oficina in (select c.cod_oficina from oficina c inner join persona p on c.responsable = p.cod_persona where p.identificacion = '" + pUsuario.identificacion + "' )" : (pTransaccionCaja.cod_cajero != 0 ? " And a.cod_cajero = " + pTransaccionCaja.cod_cajero + " And b.cod_oficina = " + pTransaccionCaja.cod_oficina : " ") ) ) +
                                    " And a.estado = 1 " +
                                    " Order By a.cod_ope asc ";
                        
                        //(codcajero_responsable != pTransaccionCaja.cod_cajero ? " And a.cod_cajero = " + pTransaccionCaja.cod_cajero : " " ) +
                        else
                            sql = " Select distinct a.cod_ope, a.fecha_oper, b.nombre caja, d.nombre cajero, (select s.descripcion from tipo_ope s where s.tipo_ope=a.tipo_ope) tip_ope, (select sum(valor_pago) from transaccionescaja x where x.cod_ope = a.cod_ope and x.tipo_movimiento='INGRESO') valor_ope_ing, (select sum(valor_pago) from transaccionescaja x where x.cod_ope=a.cod_ope and x.tipo_movimiento='EGRESO') valor_ope_egr,  (select min(cod_tipo_pago) from movimientocaja x where x.cod_ope=a.cod_ope) tipo_pago " +
                                    " From operacion a, caja b Join cajero c On b.cod_caja = c.cod_caja Join usuarios d On c.cod_persona = d.codusuario " +
                                    " Where a.cod_caja = b.cod_caja And a.cod_cajero = c.cod_cajero And a.tipo_ope In (30, 31, 32, 33, 120) And a.num_comp = -1 And a.fecha_oper = '" + pTransaccionCaja.fecha_cierre.ToString(conf.ObtenerFormatoFecha()) + "' " +
                                    (pUsuario.codperfil == 1 ? " " : (lstOficinas.Count > 0 ? " And b.cod_oficina In (select c.cod_oficina from oficina c inner join persona p on c.responsable = p.cod_persona where p.identificacion = '" + pUsuario.identificacion + "' )" : (pTransaccionCaja.cod_cajero != 0 ? " And a.cod_cajero = " + pTransaccionCaja.cod_cajero + " And b.cod_oficina = " + pTransaccionCaja.cod_oficina : " "))) +
                                    " And b.cod_oficina = " + pTransaccionCaja.cod_oficina + " And a.estado = 1 " +
                                    " Order By a.cod_ope asc ";
                        //(codcajero_responsable != pTransaccionCaja.cod_cajero ? " And a.cod_cajero = " + pTransaccionCaja.cod_cajero : " ") +

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();

                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["fecha_oper"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fecha_oper"]);
                            if (resultado["caja"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["caja"]);
                            if (resultado["cajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["cajero"]);
                            if (resultado["tip_ope"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tip_ope"]);
                            if (resultado["valor_ope_ing"] != DBNull.Value) entidad.valor_pago_ing = Convert.ToInt64(resultado["valor_ope_ing"]);
                            if (resultado["valor_ope_egr"] != DBNull.Value) entidad.valor_pago_egr = Convert.ToInt64(resultado["valor_ope_egr"]);
                            if (resultado["tipo_pago"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["tipo_pago"]);
                            if (entidad.tipo_pago == 1)
                                entidad.nomtipo_pago = "EFECTIVO";
                            if (entidad.tipo_pago == 2)
                                entidad.nomtipo_pago = "CHEQUE";
                            if (entidad.tipo_pago == null)
                                entidad.nomtipo_pago = " ";



                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarOperaciones", ex);
                        return null;
                    }
                }
            }
        }

        // <summary>
        /// Crea un registro en la tabla CONSIGNACION de la base de datos
        /// </summary>
        /// <param name="pConsignacion">Entidad Consignacion</param>
        /// <returns>Entidad Consignacion creada</returns>
        public TransaccionCaja CrearTransaccionCajaOperacion(TransaccionCaja pEntidad, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            Error = "";
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

                        cmdTransaccionFactory.Parameters.Clear();

                        int? idcuentabancaria = null;
                        string numeroboucher = "";


                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero;
                        pcodi_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pEntidad.fecha_cierre;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";

                        if (string.IsNullOrWhiteSpace(pEntidad.observacion))
                        {
                            pobservaciones.Value = DBNull.Value;
                        }
                        else
                        {
                            pobservaciones.Value = pEntidad.observacion;
                        }
                        pobservaciones.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        Int64 pCod_Ope = Convert.ToInt64(pcode_opera.Value);
                        //----------------------------------------------------------------------------------------------//

                        long moneda = 0;
                        long moneda2 = 0;
                        long tipomov = 0;
                        string nomtipomov = "";
                        string nroprod = "";
                        long tipotran = 0;
                        long tipopago = 0;
                        decimal valor = 0;
                        decimal valor2 = 0;
                        string nroRef = "0";
                        string tippago = "";
                        string referencia = "";
                        string idavances = "";

                        foreach (GridViewRow fila in gvTransacciones.Rows)
                        {
                            moneda = long.Parse(fila.Cells[2].Text);
                            tipotran = fila.Cells[12].Text != "&nbsp;" ? long.Parse(fila.Cells[12].Text) : 0;
                            tipomov = long.Parse(fila.Cells[5].Text);
                            nroprod = Convert.ToString(fila.Cells[8].Text);
                            nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";

                            valor2 = decimal.Parse(fila.Cells[9].Text);
                            nroRef = fila.Cells[8].Text;
                            tippago = fila.Cells[12].Text != "&nbsp;" ? fila.Cells[12].Text : null;                                                   
                            referencia = fila.Cells[13].Text;

                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            // Se deja registro de las transacciones registradas por el CAJERO para ser aplicadas
                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            if (valor2 > 0)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                pfecha_mov.ParameterName = "pfechamov";
                                pfecha_mov.Value = pEntidad.fecha_cierre;
                                pfecha_mov.Direction = ParameterDirection.Input;

                                DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                                ptipo_mov.ParameterName = "ptipomov";
                                ptipo_mov.Value = nomtipomov;
                                ptipo_mov.Direction = ParameterDirection.Input;

                                DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                                pcode_caja.ParameterName = "pcodigocaja";
                                pcode_caja.Value = pEntidad.cod_caja;
                                pcode_caja.Direction = ParameterDirection.Input;

                                DbParameter pcode_oper = cmdTransaccionFactory.CreateParameter();
                                pcode_oper.ParameterName = "pcodigooper";
                                pcode_oper.Value = pCod_Ope;
                                pcode_oper.Direction = ParameterDirection.Input;

                                DbParameter pcode_tran = cmdTransaccionFactory.CreateParameter();
                                pcode_tran.ParameterName = "pcodigotipotran";
                                if (tippago != null) pcode_tran.Value = tippago; else pcode_tran.Value = DBNull.Value;
                                pcode_tran.Direction = ParameterDirection.Input;

                                DbParameter pcode_cajero = cmdTransaccionFactory.CreateParameter();
                                pcode_cajero.ParameterName = "pcodigocajero";
                                pcode_cajero.Value = pEntidad.cod_cajero;
                                pcode_cajero.Direction = ParameterDirection.Input;

                                DbParameter pcode_usuario = cmdTransaccionFactory.CreateParameter();
                                pcode_usuario.ParameterName = "pcodigousuario";
                                pcode_usuario.Value = pEntidad.cod_persona;
                                pcode_usuario.Direction = ParameterDirection.Input;

                                DbParameter pcode_moneda = cmdTransaccionFactory.CreateParameter();
                                pcode_moneda.ParameterName = "pcodigomoneda";
                                pcode_moneda.Value = moneda;
                                pcode_moneda.Direction = ParameterDirection.Input;

                                DbParameter pval_pago = cmdTransaccionFactory.CreateParameter();
                                pval_pago.ParameterName = "pvalorpago";
                                pval_pago.Value = valor2;
                                pval_pago.Direction = ParameterDirection.Input;

                                DbParameter pcode_fpago = cmdTransaccionFactory.CreateParameter();
                                pcode_fpago.ParameterName = "pformapago";
                                pcode_fpago.Value = 1;//Efectivo
                                pcode_fpago.Direction = ParameterDirection.Input;

                                DbParameter pnro_prod = cmdTransaccionFactory.CreateParameter();
                                pnro_prod.ParameterName = "pnroprod";
                                pnro_prod.Value = nroprod;
                                pnro_prod.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                cmdTransaccionFactory.Parameters.Add(ptipo_mov);
                                cmdTransaccionFactory.Parameters.Add(pcode_caja);
                                cmdTransaccionFactory.Parameters.Add(pcode_oper);
                                cmdTransaccionFactory.Parameters.Add(pcode_tran);
                                cmdTransaccionFactory.Parameters.Add(pcode_cajero);
                                cmdTransaccionFactory.Parameters.Add(pcode_usuario);
                                cmdTransaccionFactory.Parameters.Add(pcode_moneda);
                                cmdTransaccionFactory.Parameters.Add(pval_pago);
                                cmdTransaccionFactory.Parameters.Add(pcode_fpago);
                                cmdTransaccionFactory.Parameters.Add(pnro_prod);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_TRAN_OPE_CAJA_C";
                                cmdTransaccionFactory.ExecuteNonQuery();
                            }
                        }

                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        // Ingresar los valores que van a registrarse en caja según la forma de pago
                        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        foreach (GridViewRow fila2 in gvFormaPago.Rows)
                        {
                            moneda2 = long.Parse(fila2.Cells[0].Text);
                            tipopago = long.Parse(fila2.Cells[1].Text);
                            valor = decimal.Parse(fila2.Cells[4].Text);
                            tipomov = long.Parse(fila2.Cells[5].Text);

                            if (valor > 0)// se valida que el valor de la forma de pago sea mayor que cero para no insertar datos basura
                            {
                                if (tipopago == 1)// Tipo de Pago : Efectivo
                                {
                                    //---------------------------------Saldo-de-Caja-------------------------------------------------//
                                    //en esta porcion de codigo se va a enviar los saldos realizados 
                                    //por el cajero en la caja especifica en una fecha

                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                                    pfecha.ParameterName = "pfecha";
                                    pfecha.Value = pEntidad.fecha_cierre;
                                    pfecha.Direction = ParameterDirection.Input;

                                    DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                                    pcoda_caja.ParameterName = "pcodigocaja";
                                    pcoda_caja.Value = pEntidad.cod_caja;
                                    pcoda_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcoda_cajero.ParameterName = "pcodigocajero";
                                    pcoda_cajero.Value = pEntidad.cod_cajero;
                                    pcoda_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodi_moneda.ParameterName = "pcodigomoneda";
                                    pcodi_moneda.Value = moneda2;
                                    pcodi_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                    pvalor.ParameterName = "pvalor";
                                    pvalor.Value = valor;
                                    pvalor.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodi_tipomov.ParameterName = "ptipomov";
                                    pcodi_tipomov.Value = tipomov;
                                    pcodi_tipomov.DbType = DbType.Int64;
                                    pcodi_tipomov.Size = 8;
                                    pcodi_tipomov.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pfecha);
                                    cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalor);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_tipomov);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_OPE";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                    cmdTransaccionFactory.Parameters.Clear();
                                    
                                    if (tipopago == 5)
                                        // Esto es para las consignaciones de Pagos por Ventanilla
                                        idcuentabancaria = int.Parse(fila2.Cells[6].Text);
                                    else if (tipopago == 10)
                                        // Esto es para las consignaciones de Pagos por Ventanilla
                                        numeroboucher = Convert.ToString(pEntidad.baucher);
                                    else
                                        idcuentabancaria = null;

                                    DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                    pcode_ope.ParameterName = "pcodigoope";
                                    pcode_ope.Value = pCod_Ope;
                                    pcode_ope.Direction = ParameterDirection.Input;

                                    DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                    pfecha_mov.ParameterName = "pfechaope";
                                    pfecha_mov.Value = pEntidad.fecha_cierre;
                                    pfecha_mov.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                    pcodx_caja.ParameterName = "pcodigocaja";
                                    pcodx_caja.Value = pEntidad.cod_caja;
                                    pcodx_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcodx_cajero.ParameterName = "pcodigocajero";
                                    pcodx_cajero.Value = pEntidad.cod_cajero;
                                    pcodx_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                    pcodx_banco.ParameterName = "pcodigobanco";
                                    pcodx_banco.Value = 0;// nose a quien asignarle
                                    pcodx_banco.Direction = ParameterDirection.Input;

                                    
                                    DbParameter pnrox_cheque = cmdTransaccionFactory.CreateParameter();
                                    pnrox_cheque.ParameterName = "pnumdoc";
                                    if (numeroboucher != "")
                                        pnrox_cheque.Value = numeroboucher;
                                    else
                                        pnrox_cheque.Value = DBNull.Value;
                                    pnrox_cheque.Direction = ParameterDirection.Input;

                                    DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                                    pcod_persona.ParameterName = "pcodpersona";
                                    pcod_persona.Value = pEntidad.cod_persona;
                                    pcod_persona.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodx_moneda.ParameterName = "pcodigomoneda";
                                    pcodx_moneda.Value = moneda2;
                                    pcodx_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalorx = cmdTransaccionFactory.CreateParameter();
                                    pvalorx.ParameterName = "pvalor";
                                    pvalorx.Value = valor;
                                    pvalorx.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_tipopay = cmdTransaccionFactory.CreateParameter();
                                    pcodi_tipopay.ParameterName = "ptipopago";
                                    pcodi_tipopay.Value = tipopago;
                                    pcodi_tipopay.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodx_tipomov.ParameterName = "ptipomov";
                                    pcodx_tipomov.Value = tipomov;
                                    pcodx_tipomov.Direction = ParameterDirection.Input;

                                    DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                    pidctabancaria.ParameterName = "pidctabancaria";
                                    pidctabancaria.Value = DBNull.Value;
                                    pidctabancaria.Direction = ParameterDirection.Input;

                              

                                    cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                    cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                    cmdTransaccionFactory.Parameters.Add(pnrox_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pcod_persona);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalorx);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_tipopay);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_tipomov);
                                    cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                }
                                else if (tipopago == 2)//cheque
                                {
                                    long moneda3 = 0;
                                    decimal valor3 = 0;
                                    long banco = 0;
                                    string numcheque = "";

                                    foreach (GridViewRow fila3 in gvCheques.Rows)
                                    {
                                        banco = long.Parse(fila3.Cells[1].Text);
                                        moneda3 = long.Parse(fila3.Cells[2].Text);
                                        numcheque = fila3.Cells[3].Text;
                                        valor3 = decimal.Parse(fila3.Cells[5].Text);

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                        pcode_ope.ParameterName = "pcodigoope";
                                        pcode_ope.Value = pCod_Ope;
                                        pcode_ope.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Clear();
                                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                        pfecha_mov.ParameterName = "pfechaope";
                                        pfecha_mov.Value = pEntidad.fecha_cierre;
                                        pfecha_mov.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                        pcodx_caja.ParameterName = "pcodigocaja";
                                        pcodx_caja.Value = pEntidad.cod_caja;
                                        pcodx_caja.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                        pcodx_cajero.ParameterName = "pcodigocajero";
                                        pcodx_cajero.Value = pEntidad.cod_cajero;
                                        pcodx_cajero.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                        pcodx_banco.ParameterName = "pcodigobanco";
                                        pcodx_banco.Value = banco;
                                        pcodx_banco.Direction = ParameterDirection.Input;

                                        DbParameter pnro_cheque = cmdTransaccionFactory.CreateParameter();
                                        pnro_cheque.ParameterName = "pnumdoc";
                                        pnro_cheque.Value = numcheque;
                                        pnro_cheque.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_persona = cmdTransaccionFactory.CreateParameter();
                                        pcodx_persona.ParameterName = "pcodpersona";
                                        pcodx_persona.Value = pEntidad.cod_persona;
                                        pcodx_persona.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                        pcodx_moneda.ParameterName = "pcodigomoneda";
                                        pcodx_moneda.Value = moneda3;
                                        pcodx_moneda.Direction = ParameterDirection.Input;

                                        DbParameter pvalory = cmdTransaccionFactory.CreateParameter();
                                        pvalory.ParameterName = "pvalor";
                                        pvalory.Value = valor3;
                                        pvalory.Direction = ParameterDirection.Input;

                                        DbParameter pcodis_tipopay = cmdTransaccionFactory.CreateParameter();
                                        pcodis_tipopay.ParameterName = "ptipopago";
                                        pcodis_tipopay.Value = tipopago;
                                        pcodis_tipopay.Direction = ParameterDirection.Input;

                                        DbParameter pcody_tipomov = cmdTransaccionFactory.CreateParameter();
                                        pcody_tipomov.ParameterName = "ptipomov";
                                        pcody_tipomov.Value = 2;
                                        pcody_tipomov.Direction = ParameterDirection.Input;

                                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                        pidctabancaria.ParameterName = "pidctabancaria";
                                        pidctabancaria.Value = DBNull.Value;
                                        pidctabancaria.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                        cmdTransaccionFactory.Parameters.Add(pnro_cheque);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_persona);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                        cmdTransaccionFactory.Parameters.Add(pvalory);
                                        cmdTransaccionFactory.Parameters.Add(pcodis_tipopay);
                                        cmdTransaccionFactory.Parameters.Add(pcody_tipomov);
                                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();

                                    }
                                }
                                else
                                {
                                  
                                   if (tipopago == 10)
                                        //pagos por datafano
                                        numeroboucher = Convert.ToString(pEntidad.baucher);
                                    

                                    cmdTransaccionFactory.Parameters.Clear();

                                    DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                    pcode_ope.ParameterName = "pcodigoope";
                                    pcode_ope.Value = pCod_Ope;
                                    pcode_ope.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                    pfecha_mov.ParameterName = "pfechaope";
                                    pfecha_mov.Value = pEntidad.fecha_cierre;
                                    pfecha_mov.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                    pcodx_caja.ParameterName = "pcodigocaja";
                                    pcodx_caja.Value = pEntidad.cod_caja;
                                    pcodx_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcodx_cajero.ParameterName = "pcodigocajero";
                                    pcodx_cajero.Value = pEntidad.cod_cajero;
                                    pcodx_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                    pcodx_banco.ParameterName = "pcodigobanco";
                                    pcodx_banco.Value = 0;// nose a quien asignarle
                                    pcodx_banco.Direction = ParameterDirection.Input;

                                    DbParameter pnrox_cheque = cmdTransaccionFactory.CreateParameter();
                                    pnrox_cheque.ParameterName = "pnumdoc";
                                    if (numeroboucher != "")
                                        pnrox_cheque.Value = numeroboucher;
                                    else
                                        pnrox_cheque.Value = DBNull.Value;
                                    pnrox_cheque.Direction = ParameterDirection.Input;


                                    DbParameter pcody_persona = cmdTransaccionFactory.CreateParameter();
                                    pcody_persona.ParameterName = "pcodpersona";
                                    pcody_persona.Value = pEntidad.cod_persona;
                                    pcody_persona.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodx_moneda.ParameterName = "pcodigomoneda";
                                    pcodx_moneda.Value = moneda2;
                                    pcodx_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalorz = cmdTransaccionFactory.CreateParameter();
                                    pvalorz.ParameterName = "pvalor";
                                    pvalorz.Value = valor;
                                    pvalorz.Direction = ParameterDirection.Input;

                                    DbParameter pcodiz_tipopay = cmdTransaccionFactory.CreateParameter();
                                    pcodiz_tipopay.ParameterName = "ptipopago";
                                    pcodiz_tipopay.Value = tipopago;
                                    pcodiz_tipopay.Direction = ParameterDirection.Input;

                                    DbParameter pcodz_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodz_tipomov.ParameterName = "ptipomov";
                                    pcodz_tipomov.Value = tipomov;
                                    pcodz_tipomov.Direction = ParameterDirection.Input;

                                    DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                    pidctabancaria.ParameterName = "pidctabancaria";
                                    if (idcuentabancaria != 0 || idcuentabancaria != null)
                                        pidctabancaria.Value = idcuentabancaria;
                                    if (idcuentabancaria == null)
                                        pidctabancaria.Value = DBNull.Value;                                    
                                    pidctabancaria.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                    cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                    cmdTransaccionFactory.Parameters.Add(pnrox_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pcody_persona);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalorz);
                                    cmdTransaccionFactory.Parameters.Add(pcodiz_tipopay);
                                    cmdTransaccionFactory.Parameters.Add(pcodz_tipomov);
                                   cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }
                            }
                        }

                        foreach (GridViewRow fila in gvTransacciones.Rows)
                        {
                            moneda = long.Parse(fila.Cells[2].Text);
                            tipotran = fila.Cells[12].Text != "&nbsp;" ? long.Parse(fila.Cells[12].Text) : 0;
                            tipomov = long.Parse(fila.Cells[5].Text);
                            nroprod = Convert.ToString(fila.Cells[8].Text);
                            nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";

                            valor2 = decimal.Parse(fila.Cells[9].Text);
                            nroRef = fila.Cells[8].Text;
                            tippago = fila.Cells[12].Text != "&nbsp;" ? fila.Cells[12].Text : null;
                            referencia = fila.Cells[13].Text;
                            try
                            {
                                if (fila.Cells.Count >= 14)
                                    if (fila.Cells[14] != null)
                                        idavances = fila.Cells[14].Text;
                                if ((tipotran == 2 || tipotran == 3 || tipotran == 6) && idavances != "&nbsp;")
                                    referencia = idavances;
                            }
                            catch
                            {
                                idavances = "";
                            }

                            if (tippago == "3")
                            {
                                pEntidad.tipo_pago = 1;
                            }
                            if (tippago == "6")
                            {
                                pEntidad.tipo_pago = 3;
                            }
                            if (tippago == "2")
                            {
                                pEntidad.tipo_pago = 2;
                            }

                            // se llama a la funcion para APLICAR PRODUCTOS para generar la transaccion
                            if (valor2 != 0)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pn_radic = cmdTransaccionFactory.CreateParameter();
                                pn_radic.ParameterName = "pn_num_producto";
                                pn_radic.Value = nroRef;
                                pn_radic.DbType = DbType.Int64;
                                pn_radic.Direction = ParameterDirection.Input;
                                pn_radic.Size = 8;

                                DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                                pn_cod_cliente.ParameterName = "pn_cod_cliente";
                                pn_cod_cliente.Value = pEntidad.cod_persona;
                                pn_cod_cliente.DbType = DbType.Int64;
                                pn_cod_cliente.Direction = ParameterDirection.Input;
                                pn_cod_cliente.Size = 8;

                                DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                                pn_cod_ope.ParameterName = "pn_cod_ope";
                                pn_cod_ope.Value = pCod_Ope;
                                pn_cod_ope.DbType = DbType.Int64;
                                pn_cod_ope.Direction = ParameterDirection.Input;
                                pn_cod_ope.Size = 8;

                                DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                                pf_fecha_pago.ParameterName = "pf_fecha_pago";
                                pf_fecha_pago.Value = pEntidad.fecha_cierre;
                                pf_fecha_pago.DbType = DbType.Date;
                                pf_fecha_pago.Direction = ParameterDirection.Input;
                                pf_fecha_pago.Size = 7;

                                DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                                pn_valor_pago.ParameterName = "pn_valor_pago";
                                pn_valor_pago.Value = valor2;
                                pn_valor_pago.DbType = DbType.Decimal;
                                pn_valor_pago.Direction = ParameterDirection.Input;
                                pn_valor_pago.Size = 8;

                                DbParameter ps_tipo_tran = cmdTransaccionFactory.CreateParameter();
                                ps_tipo_tran.ParameterName = "pn_tipo_tran";
                                if (tipotran > 0) ps_tipo_tran.Value = tipotran; else ps_tipo_tran.Value = -999;
                                ps_tipo_tran.DbType = DbType.Int64;
                                ps_tipo_tran.Direction = ParameterDirection.Input;
                                ps_tipo_tran.Size = 1;

                                DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                                pn_cod_usu.ParameterName = "pn_cod_usu";
                                pn_cod_usu.Value = pUsuario.codusuario;
                                pn_cod_usu.DbType = DbType.Int64;
                                pn_cod_usu.Direction = ParameterDirection.Input;
                                pn_cod_usu.Size = 8;

                                DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                                rn_sobrante.ParameterName = "rn_sobrante";
                                rn_sobrante.Value = -1;
                                rn_sobrante.DbType = DbType.Int64;
                                rn_sobrante.Direction = ParameterDirection.InputOutput;
                                rn_sobrante.Size = 8;

                                DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                                n_error.ParameterName = "n_error";
                                n_error.Value = 0;
                                n_error.DbType = DbType.Int64;
                                n_error.Direction = ParameterDirection.InputOutput;
                                n_error.Size = 8;

                                DbParameter pn_documento = cmdTransaccionFactory.CreateParameter();
                                pn_documento.ParameterName = "pn_documento";
                                pn_documento.Value = referencia;
                                pn_documento.DbType = DbType.String;
                                pn_documento.Direction = ParameterDirection.Input;

                                DbParameter ppagorotativo = cmdTransaccionFactory.CreateParameter();
                                ppagorotativo.ParameterName = "ppagorotativo";
                                ppagorotativo.Value =  pEntidad.tipo_pago;
                                ppagorotativo.DbType = DbType.Int16;
                                ppagorotativo.Direction = ParameterDirection.Input;

                                DbParameter pn_salida = cmdTransaccionFactory.CreateParameter();
                                pn_salida.ParameterName = "pn_salida";
                                pn_salida.Value = "  ";
                                pn_salida.DbType = DbType.String;
                                pn_salida.Direction = ParameterDirection.Output;

                                cmdTransaccionFactory.Parameters.Add(pn_radic);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                                cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                                cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                                cmdTransaccionFactory.Parameters.Add(ps_tipo_tran);
                                cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                                cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                                cmdTransaccionFactory.Parameters.Add(n_error);
                                cmdTransaccionFactory.Parameters.Add(pn_documento);
                                cmdTransaccionFactory.Parameters.Add(ppagorotativo);
                                cmdTransaccionFactory.Parameters.Add(pn_salida);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_REGISOPER";
                                cmdTransaccionFactory.ExecuteNonQuery();

                                string sobrante = rn_sobrante.Value != DBNull.Value ? Convert.ToString(rn_sobrante.Value) : string.Empty;
                                string error = n_error.Value != DBNull.Value ? Convert.ToString(n_error) : string.Empty;

                                DetallePagos detalle = new DetallePagos
                                {
                                    NumeroProducto = nroRef,
                                    CodigoCliente = pEntidad.cod_persona,
                                    CodigoOperacion = pCod_Ope,
                                    FechaPago = pEntidad.fecha_cierre,
                                    ValorPago = valor2,
                                    TipoTran = tipotran,
                                    CodigoUsuarioRealizoTransaccion = pUsuario.codusuario,
                                    Documento = referencia,
                                    Sobrante = sobrante,
                                    Error = error
                                };

                                DAauditoria.InsertarLog(detalle, "Todos los productos", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Creacion de transaccion para producto con numero " + nroRef);
                            }

                        }


                        pEntidad.cod_ope = pCod_Ope;
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        //BOExcepcion.Throw("TransaccionCajaData", "CrearTransaccionCajaOperacion", ex);
                        Error = ex.Message;
                        return null;
                    }
                }

            }
        }

        #region GIRO_MONEDA



        public TransaccionCaja CrearTransaccionGiroMoneda(TransaccionCaja pEntidad, List<GiroMoneda> lstGiros, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            Error = "";
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

                        cmdTransaccionFactory.Parameters.Clear();


                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        p_ip.Value = pUsuario.IP;

                        DbParameter pcode_opera = cmdTransaccionFactory.CreateParameter();
                        pcode_opera.ParameterName = "pcodigooper";
                        pcode_opera.Value = pEntidad.cod_ope;
                        pcode_opera.Direction = ParameterDirection.Output;

                        DbParameter pcode_tope = cmdTransaccionFactory.CreateParameter();
                        pcode_tope.ParameterName = "pcodigotipoope";
                        pcode_tope.Value = pEntidad.tipo_ope;
                        pcode_tope.Direction = ParameterDirection.Input;

                        DbParameter pcode_usuari = cmdTransaccionFactory.CreateParameter();
                        pcode_usuari.ParameterName = "pcodigousuario";
                        pcode_usuari.Value = pUsuario.codusuario;
                        pcode_usuari.Direction = ParameterDirection.Input;

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcodi_caja = cmdTransaccionFactory.CreateParameter();
                        pcodi_caja.ParameterName = "pcodigocaja";
                        pcodi_caja.Value = pEntidad.cod_caja;
                        pcodi_caja.Direction = ParameterDirection.Input;

                        DbParameter pcodi_cajero = cmdTransaccionFactory.CreateParameter();
                        pcodi_cajero.ParameterName = "pcodigocajero";
                        pcodi_cajero.Value = pEntidad.cod_cajero;
                        pcodi_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_cal = cmdTransaccionFactory.CreateParameter();
                        pfecha_cal.ParameterName = "pfechaoper";
                        pfecha_cal.Value = pEntidad.fecha_cierre;
                        pfecha_cal.Direction = ParameterDirection.Input;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = "  ";

                        cmdTransaccionFactory.Parameters.Add(pcode_opera);
                        cmdTransaccionFactory.Parameters.Add(pcode_tope);
                        cmdTransaccionFactory.Parameters.Add(pcode_usuari);
                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcodi_caja);
                        cmdTransaccionFactory.Parameters.Add(pcodi_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_cal);
                        cmdTransaccionFactory.Parameters.Add(p_ip);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OPERACION_CAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        Int64 pCod_Ope = Convert.ToInt64(pcode_opera.Value);

                        if (pEntidad.tipo_movimiento == "I")
                        {
                            //GRABANDO EN LA TABLA GIRO_MONEDA
                            GiroMoneda pEntiGiro = new GiroMoneda();
                            pEntiGiro = lstGiros[0];
                            pEntiGiro.cod_ope = pCod_Ope;
                            pEntiGiro = CrearYModificarGiroMoneda(pEntiGiro, pUsuario, 1);
                        }
                        else
                        {
                            foreach (GiroMoneda nGiroMoneda in lstGiros)
                            {
                                GiroMoneda pEntiGiro = new GiroMoneda();
                                pEntiGiro = ConsultarGiroMoneda(nGiroMoneda.idgiromoneda, pUsuario);
                                if (pEntiGiro.idgiromoneda > 0)
                                {
                                    pEntiGiro.valor = nGiroMoneda.valor;
                                    pEntiGiro.estado = nGiroMoneda.estado;
                                    pEntiGiro.cod_usuario_entrega = Convert.ToInt32(pUsuario.codusuario);
                                    pEntiGiro = CrearYModificarGiroMoneda(pEntiGiro, pUsuario, 2);
                                }
                            }
                        }

                        //----------------------------------------------------------------------------------------------//

                        long moneda2 = 0;
                        long tipomov = 0;
                        long tipopago = 0;
                        decimal valor = 0;

                        foreach (GridViewRow fila2 in gvFormaPago.Rows)
                        {
                            moneda2 = long.Parse(fila2.Cells[0].Text);
                            tipopago = long.Parse(fila2.Cells[1].Text);
                            valor = decimal.Parse(fila2.Cells[4].Text);
                            tipomov = long.Parse(fila2.Cells[5].Text);

                            if (valor > 0)// se valida que el valor de la forma de pago sea mayor que cero para no insertar datos basura
                            {

                                if (tipopago == 1)// Tipo de Pago : Efectivo
                                {
                                    //---------------------------------Saldo-de-Caja-------------------------------------------------//
                                    //en esta porcion de codigo se va a enviar los saldos realizados 
                                    //por el cajero en la caja especifica en una fecha

                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                                    pfecha.ParameterName = "pfecha";
                                    pfecha.Value = pEntidad.fecha_cierre;
                                    pfecha.Direction = ParameterDirection.Input;

                                    DbParameter pcoda_caja = cmdTransaccionFactory.CreateParameter();
                                    pcoda_caja.ParameterName = "pcodigocaja";
                                    pcoda_caja.Value = pEntidad.cod_caja;
                                    pcoda_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcoda_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcoda_cajero.ParameterName = "pcodigocajero";
                                    pcoda_cajero.Value = pEntidad.cod_cajero;
                                    pcoda_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodi_moneda.ParameterName = "pcodigomoneda";
                                    pcodi_moneda.Value = moneda2;
                                    pcodi_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                                    pvalor.ParameterName = "pvalor";
                                    pvalor.Value = valor;
                                    pvalor.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodi_tipomov.ParameterName = "ptipomov";
                                    pcodi_tipomov.Value = tipomov;
                                    pcodi_tipomov.DbType = DbType.Int64;
                                    pcodi_tipomov.Size = 8;
                                    pcodi_tipomov.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pfecha);
                                    cmdTransaccionFactory.Parameters.Add(pcoda_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcoda_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalor);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_tipomov);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_SALDO_CAJA_OPE";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                    cmdTransaccionFactory.Parameters.Clear();

                                    DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                    pcode_ope.ParameterName = "pcodigoope";
                                    pcode_ope.Value = pCod_Ope;
                                    pcode_ope.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                    pfecha_mov.ParameterName = "pfechaope";
                                    pfecha_mov.Value = pEntidad.fecha_cierre;
                                    pfecha_mov.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                    pcodx_caja.ParameterName = "pcodigocaja";
                                    pcodx_caja.Value = pEntidad.cod_caja;
                                    pcodx_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcodx_cajero.ParameterName = "pcodigocajero";
                                    pcodx_cajero.Value = pEntidad.cod_cajero;
                                    pcodx_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                    pcodx_banco.ParameterName = "pcodigobanco";
                                    pcodx_banco.Value = 0;// nose a quien asignarle
                                    pcodx_banco.Direction = ParameterDirection.Input;

                                    DbParameter pnrox_cheque = cmdTransaccionFactory.CreateParameter();
                                    pnrox_cheque.ParameterName = "pnumdoc";
                                    pnrox_cheque.Value = "";
                                    pnrox_cheque.Direction = ParameterDirection.Input;

                                    DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                                    pcod_persona.ParameterName = "pcodpersona";
                                    pcod_persona.Value = pEntidad.cod_persona;
                                    pcod_persona.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodx_moneda.ParameterName = "pcodigomoneda";
                                    pcodx_moneda.Value = moneda2;
                                    pcodx_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalorx = cmdTransaccionFactory.CreateParameter();
                                    pvalorx.ParameterName = "pvalor";
                                    pvalorx.Value = valor;
                                    pvalorx.Direction = ParameterDirection.Input;

                                    DbParameter pcodi_tipopay = cmdTransaccionFactory.CreateParameter();
                                    pcodi_tipopay.ParameterName = "ptipopago";
                                    pcodi_tipopay.Value = tipopago;
                                    pcodi_tipopay.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodx_tipomov.ParameterName = "ptipomov";
                                    pcodx_tipomov.Value = tipomov;
                                    pcodx_tipomov.Direction = ParameterDirection.Input;

                                    DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                    pidctabancaria.ParameterName = "pidctabancaria";
                                    pidctabancaria.Value = DBNull.Value;
                                    pidctabancaria.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                    cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                    cmdTransaccionFactory.Parameters.Add(pnrox_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pcod_persona);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalorx);
                                    cmdTransaccionFactory.Parameters.Add(pcodi_tipopay);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_tipomov);
                                    cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();

                                }
                                else if (tipopago == 2)//cheque
                                {
                                    long moneda3 = 0;
                                    decimal valor3 = 0;
                                    long banco = 0;
                                    string numcheque = "";

                                    foreach (GridViewRow fila3 in gvCheques.Rows)
                                    {
                                        banco = long.Parse(fila3.Cells[1].Text);
                                        moneda3 = long.Parse(fila3.Cells[2].Text);
                                        numcheque = fila3.Cells[3].Text;
                                        valor3 = decimal.Parse(fila3.Cells[5].Text);

                                        cmdTransaccionFactory.Parameters.Clear();

                                        DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                        pcode_ope.ParameterName = "pcodigoope";
                                        pcode_ope.Value = pCod_Ope;
                                        pcode_ope.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Clear();
                                        DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                        pfecha_mov.ParameterName = "pfechaope";
                                        pfecha_mov.Value = pEntidad.fecha_cierre;
                                        pfecha_mov.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                        pcodx_caja.ParameterName = "pcodigocaja";
                                        pcodx_caja.Value = pEntidad.cod_caja;
                                        pcodx_caja.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                        pcodx_cajero.ParameterName = "pcodigocajero";
                                        pcodx_cajero.Value = pEntidad.cod_cajero;
                                        pcodx_cajero.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                        pcodx_banco.ParameterName = "pcodigobanco";
                                        pcodx_banco.Value = banco;
                                        pcodx_banco.Direction = ParameterDirection.Input;

                                        DbParameter pnro_cheque = cmdTransaccionFactory.CreateParameter();
                                        pnro_cheque.ParameterName = "pnumdoc";
                                        pnro_cheque.Value = numcheque;
                                        pnro_cheque.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_persona = cmdTransaccionFactory.CreateParameter();
                                        pcodx_persona.ParameterName = "pcodpersona";
                                        pcodx_persona.Value = pEntidad.cod_persona;
                                        pcodx_persona.Direction = ParameterDirection.Input;

                                        DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                        pcodx_moneda.ParameterName = "pcodigomoneda";
                                        pcodx_moneda.Value = moneda3;
                                        pcodx_moneda.Direction = ParameterDirection.Input;

                                        DbParameter pvalory = cmdTransaccionFactory.CreateParameter();
                                        pvalory.ParameterName = "pvalor";
                                        pvalory.Value = valor3;
                                        pvalory.Direction = ParameterDirection.Input;

                                        DbParameter pcodis_tipopay = cmdTransaccionFactory.CreateParameter();
                                        pcodis_tipopay.ParameterName = "ptipopago";
                                        pcodis_tipopay.Value = tipopago;
                                        pcodis_tipopay.Direction = ParameterDirection.Input;

                                        DbParameter pcody_tipomov = cmdTransaccionFactory.CreateParameter();
                                        pcody_tipomov.ParameterName = "ptipomov";
                                        pcody_tipomov.Value = 2;
                                        pcody_tipomov.Direction = ParameterDirection.Input;

                                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                        pidctabancaria.ParameterName = "pidctabancaria";
                                        pidctabancaria.Value = DBNull.Value;
                                        pidctabancaria.Direction = ParameterDirection.Input;

                                        cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                        cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                        cmdTransaccionFactory.Parameters.Add(pnro_cheque);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_persona);
                                        cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                        cmdTransaccionFactory.Parameters.Add(pvalory);
                                        cmdTransaccionFactory.Parameters.Add(pcodis_tipopay);
                                        cmdTransaccionFactory.Parameters.Add(pcody_tipomov);
                                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                        cmdTransaccionFactory.ExecuteNonQuery();

                                    }
                                }
                                else
                                {

                                    cmdTransaccionFactory.Parameters.Clear();

                                    DbParameter pcode_ope = cmdTransaccionFactory.CreateParameter();
                                    pcode_ope.ParameterName = "pcodigoope";
                                    pcode_ope.Value = pCod_Ope;
                                    pcode_ope.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Clear();
                                    DbParameter pfecha_mov = cmdTransaccionFactory.CreateParameter();
                                    pfecha_mov.ParameterName = "pfechaope";
                                    pfecha_mov.Value = pEntidad.fecha_cierre;
                                    pfecha_mov.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_caja = cmdTransaccionFactory.CreateParameter();
                                    pcodx_caja.ParameterName = "pcodigocaja";
                                    pcodx_caja.Value = pEntidad.cod_caja;
                                    pcodx_caja.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_cajero = cmdTransaccionFactory.CreateParameter();
                                    pcodx_cajero.ParameterName = "pcodigocajero";
                                    pcodx_cajero.Value = pEntidad.cod_cajero;
                                    pcodx_cajero.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_banco = cmdTransaccionFactory.CreateParameter();
                                    pcodx_banco.ParameterName = "pcodigobanco";
                                    pcodx_banco.Value = 0;// nose a quien asignarle
                                    pcodx_banco.Direction = ParameterDirection.Input;

                                    DbParameter pnro_cheque = cmdTransaccionFactory.CreateParameter();
                                    pnro_cheque.ParameterName = "pnumdoc";
                                    pnro_cheque.Value = "";// no se a quien asignarle
                                    pnro_cheque.Direction = ParameterDirection.Input;

                                    DbParameter pcody_persona = cmdTransaccionFactory.CreateParameter();
                                    pcody_persona.ParameterName = "pcodpersona";
                                    pcody_persona.Value = pEntidad.cod_persona;
                                    pcody_persona.Direction = ParameterDirection.Input;

                                    DbParameter pcodx_moneda = cmdTransaccionFactory.CreateParameter();
                                    pcodx_moneda.ParameterName = "pcodigomoneda";
                                    pcodx_moneda.Value = moneda2;
                                    pcodx_moneda.Direction = ParameterDirection.Input;

                                    DbParameter pvalorz = cmdTransaccionFactory.CreateParameter();
                                    pvalorz.ParameterName = "pvalor";
                                    pvalorz.Value = valor;
                                    pvalorz.Direction = ParameterDirection.Input;

                                    DbParameter pcodiz_tipopay = cmdTransaccionFactory.CreateParameter();
                                    pcodiz_tipopay.ParameterName = "ptipopago";
                                    pcodiz_tipopay.Value = tipopago;
                                    pcodiz_tipopay.Direction = ParameterDirection.Input;

                                    DbParameter pcodz_tipomov = cmdTransaccionFactory.CreateParameter();
                                    pcodz_tipomov.ParameterName = "ptipomov";
                                    pcodz_tipomov.Value = tipomov;
                                    pcodz_tipomov.Direction = ParameterDirection.Input;

                                    DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                                    pidctabancaria.ParameterName = "pidctabancaria";
                                    pidctabancaria.Value = DBNull.Value;
                                    pidctabancaria.Direction = ParameterDirection.Input;

                                    cmdTransaccionFactory.Parameters.Add(pcode_ope);
                                    cmdTransaccionFactory.Parameters.Add(pfecha_mov);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_caja);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_cajero);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_banco);
                                    cmdTransaccionFactory.Parameters.Add(pnro_cheque);
                                    cmdTransaccionFactory.Parameters.Add(pcody_persona);
                                    cmdTransaccionFactory.Parameters.Add(pcodx_moneda);
                                    cmdTransaccionFactory.Parameters.Add(pvalorz);
                                    cmdTransaccionFactory.Parameters.Add(pcodiz_tipopay);
                                    cmdTransaccionFactory.Parameters.Add(pcodz_tipomov);
                                    cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                                    cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                    cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_C";
                                    cmdTransaccionFactory.ExecuteNonQuery();
                                }

                            }
                        }

                        pEntidad.cod_ope = long.Parse(pcode_opera.Value.ToString());
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        return null;
                    }
                }

            }
        }

        public GiroMoneda CrearYModificarGiroMoneda(GiroMoneda pGiroMoneda, Usuario vUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiromoneda = cmdTransaccionFactory.CreateParameter();
                        pidgiromoneda.ParameterName = "p_idgiromoneda";
                        pidgiromoneda.Value = pGiroMoneda.idgiromoneda;
                        if (pOpcion == 1)
                            pidgiromoneda.Direction = ParameterDirection.Output;
                        else
                            pidgiromoneda.Direction = ParameterDirection.Input;
                        pidgiromoneda.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidgiromoneda);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pGiroMoneda.cod_ope == null)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pGiroMoneda.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pGiroMoneda.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pGiroMoneda.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        if (pGiroMoneda.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pGiroMoneda.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pcod_oficina_recibe = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina_recibe.ParameterName = "p_cod_oficina_recibe";
                        if (pGiroMoneda.cod_oficina_recibe == null)
                            pcod_oficina_recibe.Value = DBNull.Value;
                        else
                            pcod_oficina_recibe.Value = pGiroMoneda.cod_oficina_recibe;
                        pcod_oficina_recibe.Direction = ParameterDirection.Input;
                        pcod_oficina_recibe.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina_recibe);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        if (pGiroMoneda.identificacion == null)
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = pGiroMoneda.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pGiroMoneda.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pGiroMoneda.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pGiroMoneda.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pGiroMoneda.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pGiroMoneda.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pGiroMoneda.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pGiroMoneda.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pGiroMoneda.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter pcod_usuario_entrega = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario_entrega.ParameterName = "p_cod_usuario_entrega";
                        if (pOpcion == 1 || pGiroMoneda.cod_usuario_entrega == null)
                            pcod_usuario_entrega.Value = DBNull.Value;
                        else
                            pcod_usuario_entrega.Value = pGiroMoneda.cod_usuario_entrega;
                        pcod_usuario_entrega.Direction = ParameterDirection.Input;
                        pcod_usuario_entrega.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario_entrega);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_GIRO_MONED_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAJ_GIRO_MONED_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        if (pOpcion == 1)
                            pGiroMoneda.idgiromoneda = Convert.ToInt64(pidgiromoneda.Value);
                        dbConnectionFactory.CerrarConexion(connection);

                        return pGiroMoneda;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "CrearGiroMoneda", ex);
                        return null;
                    }
                }
            }
        }

        public GiroMoneda ConsultarGiroMoneda(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            GiroMoneda entidad = new GiroMoneda();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GIRO_MONEDA WHERE IDGIROMONEDA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGIROMONEDA"] != DBNull.Value) entidad.idgiromoneda = Convert.ToInt64(resultado["IDGIROMONEDA"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["COD_OFICINA_RECIBE"] != DBNull.Value) entidad.cod_oficina_recibe = Convert.ToInt32(resultado["COD_OFICINA_RECIBE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["COD_USUARIO_ENTREGA"] != DBNull.Value) entidad.cod_usuario_entrega = Convert.ToInt32(resultado["COD_USUARIO_ENTREGA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ConsultarGiroMoneda", ex);
                        return null;
                    }
                }
            }
        }


        public List<GiroMoneda> ListarGiroMoneda(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<GiroMoneda> lstGiroMoneda = new List<GiroMoneda>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT G.*,M.DESCRIPCION AS NOM_MONEDA FROM GIRO_MONEDA G LEFT JOIN TIPOMONEDA M ON G.COD_MONEDA = M.COD_MONEDA " + pFiltro + " ORDER BY G.IDGIROMONEDA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            GiroMoneda entidad = new GiroMoneda();
                            if (resultado["IDGIROMONEDA"] != DBNull.Value) entidad.idgiromoneda = Convert.ToInt64(resultado["IDGIROMONEDA"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["COD_OFICINA_RECIBE"] != DBNull.Value) entidad.cod_oficina_recibe = Convert.ToInt32(resultado["COD_OFICINA_RECIBE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                            if (resultado["COD_USUARIO_ENTREGA"] != DBNull.Value) entidad.cod_usuario_entrega = Convert.ToInt32(resultado["COD_USUARIO_ENTREGA"]);
                            if (resultado["NOM_MONEDA"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["NOM_MONEDA"]);
                            lstGiroMoneda.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiroMoneda;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarGiroMoneda", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

        public string BuscarGeneral(int pCodigo, int pTipoDato, Usuario pUsuario)
        {
            string resultado = "";
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        sql = @"SELECT valor FROM general WHERE codigo = " + pCodigo;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = (string)cmdTransaccionFactory.ExecuteScalar();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (pTipoDato == 1)         // No valida nada
                            return resultado;
                        else if (pTipoDato == 2)    // Se valida si corresponde a un número
                            return Convert.ToString(Convert.ToInt64(resultado));
                        else if (pTipoDato == 3)    // Se valida si corresponde a una fecha
                            return Convert.ToString(DateTime.ParseExact(resultado, conf.ObtenerFormatoFecha(), null));
                        else
                            return resultado;
                    }
                    catch
                    {
                        if (pTipoDato == 2)
                            return "0";
                        else
                            return resultado;
                    }
                }
            }
        }

        public Decimal ConsultarTotalTransaccionDia(Int64 pCod_Persona, decimal pvalortransaccion, DateTime pFecha, Usuario vUsuario)
        {
            Decimal resultado = 0;

            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT (+ " + pvalortransaccion + @")) valor from movimientocaja m left join operacion o on m.cod_ope = o.cod_ope
                                    WHERE o.tipo_ope not in (32,33,34) and m.cod_persona = " + pCod_Persona + " and m.tipo_mov ='I' AND m.COD_TIPO_PAGO = 1  and TRUNC(m.FEC_OPE) = TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + @"')
                                    HAVING (" + pvalortransaccion + ") >= (select valor from general where codigo = 16)";
                        }
                        else
                        {
                            sql = @"SELECT (+ " + pvalortransaccion + @") valor from movimientocaja m left join operacion o on m.cod_ope = o.cod_ope
                                    WHERE o.tipo_ope not in (32,33,34) and m.cod_persona = " + pCod_Persona + " and m.tipo_mov ='I' AND m.COD_TIPO_PAGO = 1  and TRUNC(m.FEC_OPE) = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + @"'
                                    HAVING  (" + pvalortransaccion + " ) >= (select valor from general where codigo = 16)";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        return resultado = (Decimal)cmdTransaccionFactory.ExecuteScalar();
                        dbConnectionFactory.CerrarConexion(connection);
                       return resultado;
                    }
                    catch (Exception ex)
                    {
                        //   BOExcepcion.Throw("TransaccionCajaData", "ConsultarGiroMoneda", ex);
                        return resultado;
                    }
                }
            }
        }

        public Decimal ConsultarTotalTransaccionMes(Int64 pCod_Persona, decimal pvalortransaccion, int Mes, int Año, Usuario vUsuario)
        {
            Decimal resultado = 0;
            
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"SELECT (sum(m.valor)  + " + pvalortransaccion + @") valor from movimientocaja m left join operacion o on m.cod_ope = o.cod_ope
                                    where o.tipo_ope not in (32,33,34) and m.cod_persona = " + pCod_Persona
                                    + " and m.tipo_mov ='I'  AND m.COD_TIPO_PAGO = 1  and TO_CHAR(m.FEC_OPE,'MM') = " + Mes + " and TO_CHAR(m.FEC_OPE,'YYYY') = " + Año
                                    + "  having  (sum(m.valor) + " + pvalortransaccion + " ) >= (select valor from general where codigo = 17)";
                        }
                        else
                        {
                            sql = @"SELECT (sum(m.valor)  + " + pvalortransaccion + @") valor from movimientocaja m left join operacion o on m.cod_ope = o.cod_ope
                                    where o.tipo_ope not in (32,33,34) and m.cod_persona = " + pCod_Persona
                                   + " and m.tipo_mov ='I' AND m.COD_TIPO_PAGO = 1 and MONTH(m.FEC_OPE) = " + Mes + " and YEAR(m.FEC_OPE) = " + Año
                                   + "  having  (sum(m.valor) + " + pvalortransaccion + " ) >= (select valor from general where codigo = 17)";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        return  resultado = (Decimal)cmdTransaccionFactory.ExecuteScalar();
                        dbConnectionFactory.CerrarConexion(connection);
                       return resultado;
                    }

                    catch (Exception ex)
                    {
                        //   BOExcepcion.Throw("TransaccionCajaData", "ConsultarGiroMoneda", ex);
                        return resultado;
                    }
                 
                }
          
            }
        }



        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TransaccionesCaja dados unos filtros
        /// Transacciones de Caja Pendientes por pasar a contabilidad
        /// </summary>
        /// <param name="pTransaccionesCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarOperacionesAnuladas(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select T .*,CAJA.NOMBRE,U.NOMBRE AS CAJERO,p.identificacion,tp.descripcion from REVERSIONTRANSACCIONESCAJA T INNER JOIN CAJERO ON CAJERO.COD_CAJERO = T.COD_CAJERO LEFT JOIN CAJA ON CAJA.COD_CAJA = CAJERO.COD_CAJA LEFT JOIN USUARIOS U ON U.CODUSUARIO = CAJERO.COD_PERSONA inner join persona  p on p.cod_persona=t.cod_persona left join tipoproducto tp on tp.cod_tipo_producto=t.COD_TIPO_PRODUCTO where  CAJERO.cod_cajero = " + pTransaccionCaja.cod_cajero + " And caja.cod_oficina = " + pTransaccionCaja.cod_oficina ;
                            // "   And trunc(FECHA_MOVIMIENTO) = To_Date('" + pTransaccionCaja.fecha_movimiento.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        sql += @"    And trunc(FECHA_MOVIMIENTO) between To_Date('" + pTransaccionCaja.fecha_movimiento.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + pTransaccionCaja.fecha_movimientofinal.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();



                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["tipo_pago"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["tipo_pago"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["cajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["cajero"]);
                           // if (resultado["tip_ope"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tip_ope"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (entidad.tipo_pago == 1)
                                entidad.nomtipo_pago = "EFECTIVO";
                            if (entidad.tipo_pago == 2)
                                entidad.nomtipo_pago = "CHEQUE";
                            if (entidad.tipo_pago == null)
                                entidad.nomtipo_pago = " ";
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.nom_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);



                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarOperacionesAnuladas", ex);
                        return null;
                    }
                }
            }
        }

        public List<TransaccionCaja> ListarOperacionesAnuladasSincajero(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TransaccionCaja> lstTransaccionCaja = new List<TransaccionCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select T .*,CAJA.NOMBRE,U.NOMBRE AS CAJERO,p.identificacion,tp.descripcion from REVERSIONTRANSACCIONESCAJA T INNER JOIN CAJERO ON CAJERO.COD_CAJERO = T.COD_CAJERO LEFT JOIN CAJA ON CAJA.COD_CAJA = CAJERO.COD_CAJA LEFT JOIN USUARIOS U ON U.CODUSUARIO = CAJERO.COD_PERSONA inner join persona  p on p.cod_persona=t.cod_persona left join tipoproducto tp on tp.cod_tipo_producto=t.COD_TIPO_PRODUCTO where caja.cod_oficina = " + pTransaccionCaja.cod_oficina;

                        //sql+ + "   And trunc(FECHA_MOVIMIENTO) = To_Date('" + pTransaccionCaja.fecha_movimiento.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                        sql += @"    And trunc(FECHA_MOVIMIENTO) between To_Date('" + pTransaccionCaja.fecha_movimiento.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + pTransaccionCaja.fecha_movimientofinal.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') " + " order by U.NOMBRE,t.cod_ope  asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionCaja entidad = new TransaccionCaja();



                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["tipo_pago"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["tipo_pago"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["cajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["cajero"]);
                            // if (resultado["tip_ope"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["tip_ope"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (entidad.tipo_pago == 1)
                                entidad.nomtipo_pago = "EFECTIVO";
                            if (entidad.tipo_pago == 2)
                                entidad.nomtipo_pago = "CHEQUE";
                            if (entidad.tipo_pago == null)
                                entidad.nomtipo_pago = " ";
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.nom_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);



                            lstTransaccionCaja.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ListarOperacionesAnuladas", ex);
                        return null;
                    }
                }
            }
        }



        public TransaccionCaja ConsultarOperacionesAnuladas(Int64 cod_ope, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            TransaccionCaja entidad = new TransaccionCaja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select T.*,CAJA.NOMBRE,U.NOMBRE AS CAJERO,p.identificacion,tp.descripcion,p.PRIMER_NOMBRE || ' ' || p.SEGUNDO_NOMBRE || ' ' || p.PRIMER_APELLIDO || ' ' || p.SEGUNDO_APELLIDO AS NOMBRECLIENTE, o.nombre as OFICINA from REVERSIONTRANSACCIONESCAJA T INNER JOIN CAJERO ON CAJERO.COD_CAJERO = T.COD_CAJERO LEFT JOIN CAJA ON CAJA.COD_CAJA = CAJERO.COD_CAJA LEFT JOIN USUARIOS U ON U.CODUSUARIO = CAJERO.COD_PERSONA inner join persona p on p.cod_persona = t.cod_persona left join oficina o on o.cod_oficina = CAJA.cod_oficina left join tipoproducto tp on tp.cod_tipo_producto = t.COD_TIPO_PRODUCTO where t.cod_ope = " + cod_ope ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {                            
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_movimiento = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["tipo_pago"] != DBNull.Value) entidad.tipo_pago = Convert.ToInt64(resultado["tipo_pago"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_caja = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["cajero"] != DBNull.Value) entidad.nom_cajero = Convert.ToString(resultado["cajero"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (entidad.tipo_pago == 1)
                                entidad.nomtipo_pago = "EFECTIVO";
                            if (entidad.tipo_pago == 2)
                                entidad.nomtipo_pago = "CHEQUE";
                            if (entidad.tipo_pago == 0)
                                entidad.nomtipo_pago = " ";
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipoproducto = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.nom_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["NOMBRECLIENTE"] != DBNull.Value) entidad.nom_Cliente = Convert.ToString(resultado["NOMBRECLIENTE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["OFICINA"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                         return entidad;;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionCajaData", "ConsultarOperacionesAnuladas", ex);
                        return entidad;
                    }
                }
            }
        }

        public Int64 CajeroResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            Int64 codcajeroresponsable = 0;
            Int64? codresponsable = null;
            string responsable = "";
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select o.responsable From oficina o Where o.cod_oficina = " + cod_oficina; ;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["RESPONSABLE"] != DBNull.Value) responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            try { codresponsable = Convert.ToInt64(responsable); } catch { codresponsable = null; }
                            if (codresponsable != null)
                            {
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = "Select c.cod_cajero From usuarios u Left Join cajero c On u.codusuario = c.cod_persona Where u.identificacion = (Select p.identificacion From persona p Where p.cod_persona = " + codresponsable + ") ";
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                if (resultado.Read())
                                {
                                    if (resultado["COD_CAJERO"] != DBNull.Value) codcajeroresponsable = Convert.ToInt64(resultado["COD_CAJERO"]);
                                }
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return codcajeroresponsable; ;
                    }
                    catch 
                    {
                        return codcajeroresponsable;
                    }
                }
            }
        }

        public Int64 UsuarioResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            Int64 codusuarioresponsable = 0;
            Int64? codresponsable = null;
            string responsable = "";
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = "Select o.responsable From oficina o Where o.cod_oficina = " + cod_oficina; ;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["RESPONSABLE"] != DBNull.Value) responsable = Convert.ToString(resultado["RESPONSABLE"]);
                            try { codresponsable = Convert.ToInt64(responsable); } catch { codresponsable = null; }
                            if (codresponsable != null)
                            {
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = "Select u.codusuario From usuarios u Where u.identificacion = (Select p.identificacion From persona p Where p.cod_persona = " + codresponsable + ") ";
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                if (resultado.Read())
                                {
                                    if (resultado["CODUSUARIO"] != DBNull.Value) codusuarioresponsable = Convert.ToInt64(resultado["CODUSUARIO"]);
                                }
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return codusuarioresponsable; ;
                    }
                    catch
                    {
                        return codusuarioresponsable;
                    }
                }
            }
        }

        //Agregado para listar los códigos de las oficinas de las cuales el usuario sea responsable 
        public List<Int64> OficinasUsuario(Usuario pUsuario)
        {
            
            DbDataReader resultado = default(DbDataReader);
            List<Int64> lstOficinas = new List<long>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select c.cod_oficina from oficina c inner join persona p on c.responsable = p.cod_persona where p.identificacion = '" + pUsuario.identificacion + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Int64 responsable = 0;
                            if (resultado["COD_OFICINA"] != DBNull.Value) responsable = Convert.ToInt64(resultado["COD_OFICINA"]);
                            lstOficinas.Add(responsable);                            
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficinas;
                    }
                    catch(Exception ex)
                    {
                        return null;
                    }
                }
            }
        }


    }
}

