using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Caja
    /// </summary>
    public class CajaData : GlobalData 
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public CajaData()
        {
           dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea una Caja Oficina en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Caja</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.Caja InsertarCaja(Xpinn.Caja.Entities.Caja pEntidad, GridView gvTopes, GridView gvOperaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = 0;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.InputOutput;
                        
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Size = 8;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pnom_caja = cmdTransaccionFactory.CreateParameter();
                        pnom_caja.ParameterName = "pnomcaja";
                        pnom_caja.Value = pEntidad.nombre;
                        pnom_caja.DbType = DbType.AnsiString;
                        pnom_caja.Direction = ParameterDirection.Input;
                        pnom_caja.Size = 50;

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "pfechacreacion";
                        pfecha_creacion.Value = pEntidad.fecha_creacion;
                        pfecha_creacion.DbType = DbType.DateTime;
                        pfecha_creacion.Direction = ParameterDirection.Input;
                        pfecha_creacion.Size = 7;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.estado;
                        p_estado.DbType = DbType.Int64;
                        p_estado.Size = 8;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter pes_principal = cmdTransaccionFactory.CreateParameter();
                        pes_principal.ParameterName = "pesprincipal";
                        pes_principal.Value = pEntidad.esprincipal;
                        pes_principal.DbType = DbType.Int64;
                        pes_principal.Size = 8;
                        pes_principal.Direction = ParameterDirection.Input;

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";

                        if (string.IsNullOrWhiteSpace(pEntidad.cod_cuenta_contable))
                        {
                            pcod_cuenta.Value = DBNull.Value;
                        }
                        else
                        {
                            pcod_cuenta.Value = pEntidad.cod_cuenta_contable;
                        }

                        pcod_cuenta.DbType = DbType.Int64;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        
                        DbParameter pcod_datafono = cmdTransaccionFactory.CreateParameter();
                        pcod_datafono.ParameterName = "pcod_datafono";
                        pcod_datafono.Value = pEntidad.cod_datafono;
                        pcod_datafono.DbType = DbType.AnsiString;
                        pcod_datafono.Direction = ParameterDirection.Input;
                        pcod_datafono.Size = 20;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pnom_caja);
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(pes_principal);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pcod_datafono);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CAJAINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJA", pUsuario, Accion.Crear.ToString(), TipoAuditoria.CajaFinanciera, "Insertar en caja");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_caja.ToString()), "CAJA", Accion.Crear.ToString(), connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        pEntidad.cod_caja = pcod_caja.Value.ToString();

                        //se inserta las opciones de la grilla en TipoOperacion
                        CheckBox chkOperacionPermitida;
                        
                        int tipoOper=0;//captura el valor del codigo de Tipo de Operacion
                        //se limpia los parametros de entrada

                        foreach (GridViewRow fila in gvOperaciones.Rows)
                        {
                            //se captura la opcion chequeda en el grid
                            tipoOper = int.Parse(fila.Cells[0].Text);
                            chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");

                            if (chkOperacionPermitida.Checked == true)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcodigo_caja = cmdTransaccionFactory.CreateParameter();
                                pcodigo_caja.ParameterName = "pcodigocaja";
                                pcodigo_caja.Value = pEntidad.cod_caja;
                                pcodigo_caja.DbType = DbType.Int64;
                                pcodigo_caja.Size = 8;
                                pcodigo_caja.Direction = ParameterDirection.Input;

                                DbParameter pcodigo_tipoOpe = cmdTransaccionFactory.CreateParameter();
                                pcodigo_tipoOpe.ParameterName = "pcodigotipope";
                                pcodigo_tipoOpe.Value = tipoOper;
                                pcodigo_tipoOpe.DbType = DbType.Int64;
                                pcodigo_tipoOpe.Size = 8;
                                pcodigo_tipoOpe.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcod_caja);
                                cmdTransaccionFactory.Parameters.Add(pcodigo_tipoOpe);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_ATRIB_CAJA_C";
                                cmdTransaccionFactory.ExecuteNonQuery();
                            }
                        }
                        //gvTopes
                        TextBox txtMaximo;
                        TextBox txtMinimo;
                        int tipoTope=0;
                        int moneda=0;

                        foreach (GridViewRow fila in gvTopes.Rows)
                        { 
                            tipoTope = int.Parse(fila.Cells[0].Text);
                            moneda = int.Parse(fila.Cells[2].Text);

                            txtMinimo = (TextBox)fila.FindControl("txtMinimotex");
                            txtMaximo = (TextBox)fila.FindControl("txtMaximotex");

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter p_tipoTope = cmdTransaccionFactory.CreateParameter();
                            p_tipoTope.ParameterName = "pcodigotope";
                            p_tipoTope.Value = tipoTope;
                            p_tipoTope.DbType = DbType.Int64;
                            p_tipoTope.Size = 8;
                            p_tipoTope.Direction = ParameterDirection.Input;

                            DbParameter p_moneda = cmdTransaccionFactory.CreateParameter();
                            p_moneda.ParameterName = "pcodigomoneda";
                            p_moneda.Value = moneda;
                            p_moneda.DbType = DbType.Int64;
                            p_moneda.Size = 8;
                            p_moneda.Direction = ParameterDirection.Input;

                            DbParameter p_caja = cmdTransaccionFactory.CreateParameter();
                            p_caja.ParameterName = "pcodigocaja";
                            p_caja.Value = pEntidad.cod_caja;
                            p_caja.DbType = DbType.Int64;
                            p_caja.Size = 8;
                            p_caja.Direction = ParameterDirection.Input;

                            DbParameter pval_minimo = cmdTransaccionFactory.CreateParameter();
                            pval_minimo.ParameterName = "pvalminimo";
                            if (txtMinimo == null)
                                pval_minimo.Value = DBNull.Value;
                            else
                                if (txtMinimo.Text.Trim() == "")
                                    pval_minimo.Value = DBNull.Value;
                                else
                                    pval_minimo.Value = txtMinimo.Text;
                            pval_minimo.DbType = DbType.Decimal;
                            pval_minimo.Direction = ParameterDirection.Input;

                            DbParameter pval_maximo = cmdTransaccionFactory.CreateParameter();
                            pval_maximo.ParameterName = "pvalmaximo";
                            if (txtMaximo == null)
                                pval_maximo.Value = DBNull.Value;
                            else
                                if (txtMaximo.Text.Trim() == "")
                                    pval_maximo.Value = DBNull.Value;
                                else
                                    pval_maximo.Value = txtMaximo.Text;
                            pval_maximo.DbType = DbType.Decimal;
                            pval_maximo.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(p_tipoTope);
                            cmdTransaccionFactory.Parameters.Add(p_moneda);
                            cmdTransaccionFactory.Parameters.Add(p_caja);
                            cmdTransaccionFactory.Parameters.Add(pval_minimo);
                            cmdTransaccionFactory.Parameters.Add(pval_maximo);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_TOPE_C";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "InsertarCaja", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica una entidad Caja en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Caja</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.Caja ModificarCaja(Caja.Entities.Caja pEntidad,GridView gvTopes, GridView gvOperaciones, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pEntidad.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Direction = ParameterDirection.InputOutput; 

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pnom_caja = cmdTransaccionFactory.CreateParameter();
                        pnom_caja.ParameterName = "pnomcaja";
                        pnom_caja.Value = pEntidad.nombre;
                        pnom_caja.DbType = DbType.String;
                        pnom_caja.Direction = ParameterDirection.Input;

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "pfechacreacion";
                        pfecha_creacion.Value = pEntidad.fecha_creacion;
                        pfecha_creacion.DbType = DbType.DateTime;
                        pfecha_creacion.Direction = ParameterDirection.Input;

                        DbParameter pes_principal = cmdTransaccionFactory.CreateParameter();
                        pes_principal.ParameterName = "pesprincipal";
                        pes_principal.Value = pEntidad.esprincipal;
                        pes_principal.DbType = DbType.Int64;
                        pes_principal.Direction = ParameterDirection.Input;

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "pestado";
                        p_estado.Value = pEntidad.estado;
                        p_estado.DbType = DbType.Int64;
                        p_estado.Direction = ParameterDirection.Input;

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        if (string.IsNullOrWhiteSpace(pEntidad.cod_cuenta_contable))
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pEntidad.cod_cuenta_contable;
                        pcod_cuenta.Direction = ParameterDirection.Input;

                        DbParameter pcod_datafono = cmdTransaccionFactory.CreateParameter();
                        pcod_datafono.ParameterName = "pcod_datafono";
                        if (pEntidad.cod_datafono == null)
                            pcod_datafono.Value = DBNull.Value;
                        else
                            pcod_datafono.Value = pEntidad.cod_datafono;
                        pcod_datafono.DbType = DbType.String;
                        pcod_datafono.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pnom_caja);
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);                        
                        cmdTransaccionFactory.Parameters.Add(pes_principal);
                        cmdTransaccionFactory.Parameters.Add(p_estado);
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(pcod_datafono);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_U";
                        cmdTransaccionFactory.ExecuteNonQuery(); 
                        

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJA", pUsuario, Accion.Modificar.ToString(), TipoAuditoria.CajaFinanciera, "Modificar registro caja");
                        //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_caja),"CAJA",Accion.Modificar.ToString(), connection,cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                        //se inserta las opciones de la grilla en TipoOperacion
                        CheckBox chkOperacionPermitida;

                        int tipoOper = 0;//captura el valor del codigo de Tipo de Operacion
                        //se limpia los parametros de entrada

                        foreach (GridViewRow fila in gvOperaciones.Rows)
                        {
                            //se captura la opcion chequeda en el grid
                            tipoOper = int.Parse(fila.Cells[0].Text);
                            chkOperacionPermitida = (CheckBox)fila.FindControl("chkPermiso");

                            //se borran todos los registros del tipo operacion asociado
                            // a la caja para poder simular el tema de la actualziacion de opcione                          
                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter pcode_caja = cmdTransaccionFactory.CreateParameter();
                            pcode_caja.ParameterName = "pcodigocaja";
                            pcode_caja.Value = pEntidad.cod_caja;
                            pcode_caja.DbType = DbType.Int64;
                            pcode_caja.Size = 8;
                            pcode_caja.Direction = ParameterDirection.Input;

                            DbParameter pcode_tipoOpe = cmdTransaccionFactory.CreateParameter();
                            pcode_tipoOpe.ParameterName = "pcodigotipope";
                            pcode_tipoOpe.Value = tipoOper;
                            pcode_tipoOpe.DbType = DbType.Int64;
                            pcode_tipoOpe.Size = 8;
                            pcode_tipoOpe.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(pcode_caja);
                            cmdTransaccionFactory.Parameters.Add(pcode_tipoOpe);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_ATRIB_CAJA_D";
                            cmdTransaccionFactory.ExecuteNonQuery();


                            if (chkOperacionPermitida.Checked == true)
                            {
                                cmdTransaccionFactory.Parameters.Clear();
                                DbParameter pcodigo_caja = cmdTransaccionFactory.CreateParameter();
                                pcodigo_caja.ParameterName = "pcodigocaja";
                                pcodigo_caja.Value = pEntidad.cod_caja;
                                pcodigo_caja.DbType = DbType.Int64;
                                pcodigo_caja.Size = 8;
                                pcodigo_caja.Direction = ParameterDirection.Input;

                                DbParameter pcodigo_tipoOpe = cmdTransaccionFactory.CreateParameter();
                                pcodigo_tipoOpe.ParameterName = "pcodigotipope";
                                pcodigo_tipoOpe.Value = tipoOper;
                                pcodigo_tipoOpe.DbType = DbType.Int64;
                                pcodigo_tipoOpe.Size = 8;
                                pcodigo_tipoOpe.Direction = ParameterDirection.Input;

                                cmdTransaccionFactory.Parameters.Add(pcod_caja);
                                cmdTransaccionFactory.Parameters.Add(pcodigo_tipoOpe);

                                cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                                cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_ATRIB_CAJA_C";
                                cmdTransaccionFactory.ExecuteNonQuery();
                            }
                        }

                          //gvTopes
                       
                        int tipoTope=0;
                        int moneda=0;
                        decimal txtMinimo;
                        decimal txtMaximo;

                        foreach (GridViewRow fila in gvTopes.Rows)
                        {
                            tipoTope = int.Parse(fila.Cells[0].Text);
                            moneda = int.Parse(fila.Cells[2].Text);
                            txtMinimo = pEntidad.valor_minimo;
                            txtMaximo = pEntidad.valor_maximo;

                            cmdTransaccionFactory.Parameters.Clear();
                            DbParameter p_tipoTope = cmdTransaccionFactory.CreateParameter();
                            p_tipoTope.ParameterName = "pcodigotope";
                            p_tipoTope.Value = tipoTope;
                            p_tipoTope.DbType = DbType.Int16;
                            p_tipoTope.Size = 8;
                            p_tipoTope.Direction = ParameterDirection.Input;

                            DbParameter p_moneda = cmdTransaccionFactory.CreateParameter();
                            p_moneda.ParameterName = "pcodigomoneda";
                            p_moneda.Value = moneda;
                            p_moneda.DbType = DbType.Int16;
                            p_moneda.Size = 8;
                            p_moneda.Direction = ParameterDirection.Input;

                            DbParameter p_caja = cmdTransaccionFactory.CreateParameter();
                            p_caja.ParameterName = "pcodigocaja";
                            p_caja.Value = pEntidad.cod_caja;
                            p_caja.DbType = DbType.Int16;
                            p_caja.Size = 8;
                            p_caja.Direction = ParameterDirection.Input;

                            DbParameter pval_minimo = cmdTransaccionFactory.CreateParameter();
                            pval_minimo.ParameterName = "pvalminimo";
                            pval_minimo.Value = txtMinimo;
                            pval_minimo.DbType = DbType.Decimal;
                            //pval_minimo.Size = 8;
                            pval_minimo.Direction = ParameterDirection.Input;

                            DbParameter pval_maximo = cmdTransaccionFactory.CreateParameter();
                            pval_maximo.ParameterName = "pvalmaximo";
                            pval_maximo.Value = txtMaximo;
                            pval_maximo.DbType = DbType.Decimal;
                            //pval_maximo.Size = 8;
                            pval_maximo.Direction = ParameterDirection.Input;

                            cmdTransaccionFactory.Parameters.Add(p_tipoTope);
                            cmdTransaccionFactory.Parameters.Add(p_moneda);
                            cmdTransaccionFactory.Parameters.Add(p_caja);
                            cmdTransaccionFactory.Parameters.Add(pval_minimo);
                            cmdTransaccionFactory.Parameters.Add(pval_maximo);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_TOPE_U";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "InsertarCaja", ex);
                        return null;
                    }
                
                }
            }
        }

        /// <summary>
        /// Elimina una Caja en la base de datos
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        public void EliminarCaja(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Xpinn.Caja.Entities.Caja pEntidad = new Xpinn.Caja.Entities.Caja();

                        if (pUsuario.programaGeneraLog)
                            pEntidad = ConsultarCaja(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pId;
                        pcod_caja.DbType = DbType.Int16;
                        pcod_caja.Size = 8;
                        pcod_caja.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_CAJA_D";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pEntidad, "CAJA", pUsuario, Accion.Eliminar.ToString(), TipoAuditoria.CajaFinanciera, "Eliminar registro caja");
                            //DAauditoria.InsertarLog(pEntidad, pUsuario, Convert.ToInt64(pEntidad.cod_caja),"CAJA", Accion.Eliminar.ToString(),connection, cmdTransaccionFactory); //REGISTRO DE AUDITORIA

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "EliminarCaja", ex);
                    }
                }
               
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Caja de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCaja(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Caja entidad = new Caja.Entities.Caja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT caja.*, plan_cuentas.nombre as desc_cuenta 
                                       FROM CAJA 
                                       LEFT JOIN plan_cuentas on plan_cuentas.cod_cuenta =  caja.cod_cuenta where cod_caja=" + pId.ToString();
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        //cmdTransaccionFactory.CommandText = "uspXpinnCajaFinOficinaConsultar";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["esprincipal"] != DBNull.Value) entidad.esprincipal = Convert.ToInt64(resultado["esprincipal"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_contable = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["desc_cuenta"] != DBNull.Value) entidad.desc_cuenta_contable = Convert.ToString(resultado["desc_cuenta"]);
                            if (resultado["cod_datafono"] != DBNull.Value) entidad.cod_datafono = Convert.ToString(resultado["cod_datafono"]);
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ConsultarCaja", ex);
                        return null;
                    }
                
                }
            }
        }

        /// <summary>
        /// Obtiene un registro de la tabla Caja de la base de datos
        /// </summary>
        /// <param name="pId">identificador del registro</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCajaPrincipal(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Caja.Entities.Caja entidad = new Caja.Entities.Caja();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT count(*) conteo, cod_caja FROM CAJA where esprincipal=1 and cod_oficina=" + pId.ToString() + " Group By cod_caja ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["conteo"] != DBNull.Value) entidad.escajaprincip = Convert.ToInt64(resultado["conteo"]);
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja_principal = Convert.ToInt64(resultado["cod_caja"]);
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ConsultarCajaPrincipal", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajaas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Caja obtenidas</returns>
        public List<Caja.Entities.Caja> ListarCaja(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.cod_caja, c.nombre, c.esprincipal, c.fecha_creacion, (case when c.estado=1 then 'Activa' when c.estado=0 then 'Inactiva' when c.estado=2 then 'Cerrada' end ) state, c.cod_cuenta, pl.nombre as desc_cuenta
                                        FROM CAJA c
                                        LEFT JOIN plan_cuentas pl on pl.cod_cuenta =  c.cod_cuenta " + ObtenerFiltro(pEntidad) + " Order By cod_caja asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value)  entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["state"] != DBNull.Value) entidad.state = Convert.ToString(resultado["state"]);
                            if (resultado["esprincipal"] != DBNull.Value) entidad.esprincipal = Convert.ToInt64(resultado["esprincipal"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta_contable = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["desc_cuenta"] != DBNull.Value) entidad.desc_cuenta_contable = Convert.ToString(resultado["desc_cuenta"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarCaja", ex);
                        return null;
                    }
                
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajaas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Caja obtenidas</returns>
        public List<Caja.Entities.Caja> ListarComboCaja(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA " + ObtenerFiltro(pEntidad) + " Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["cod_datafono"] != DBNull.Value) entidad.cod_datafono = Convert.ToString(resultado["cod_datafono"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarComboCaja", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajaas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Caja obtenidas</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXOficina(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA Where cod_oficina="+ pEntidad.cod_oficina  + " Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarComboCajaXOficina", ex);
                        return null;
                    }

                }
            }
        }



        public List<Caja.Entities.Caja> ListarComboCajaXOficinaycaja(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA Where cod_caja=" + pEntidad.cod_caja +  " and cod_oficina =" + pEntidad.cod_oficina + " Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarComboCajaXOficinayaja", ex);
                        return null;
                    }

                }
            }
        }

        public List<Caja.Entities.Caja> ListarComboCajaXOficinaActiva(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA Where estado=1 and cod_oficina=" + pEntidad.cod_oficina + " Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarComboCajaXOficina", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajaas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Caja obtenidas por todas las oficinas</returns>
        public List<Caja.Entities.Caja> ListarCajaAllOficinas(Caja.Entities.Caja pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select c.cod_caja,c.cod_oficina,c.Nombre nombre_caj,(select Nombre from oficina where cod_oficina=c.cod_oficina) as nombre_ofi,"
                            + "c.fecha_creacion,c.esprincipal,(case when c.estado=1 then 'Activa' when c.estado=0 then 'Inactiva' when c.estado=2 then 'Cerrada'  end ) estado from caja c order by c.cod_caja";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre_caj"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre_caj"]);
                            if (resultado["nombre_ofi"] != DBNull.Value) entidad.nombre_ofi = Convert.ToString(resultado["nombre_ofi"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["esprincipal"] != DBNull.Value) entidad.esprincipal = Convert.ToInt64(resultado["esprincipal"]);
                            if (resultado["estado"] != DBNull.Value) entidad.state = Convert.ToString(resultado["estado"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarCaja", ex);
                        return null;
                    }

                }
            }
        }

        /// <summary>
        /// Obtiene la lista de cajas que tenga datafono
        /// </summary>
        /// <returns>Conjunto de Caja obtenidas</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXDatafono( Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Caja.Entities.Caja> lstCaja = new List<Caja.Entities.Caja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT *  FROM CAJA Where cod_datafono is not null Order By cod_caja asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Caja.Entities.Caja entidad = new Caja.Entities.Caja();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_caja"] != DBNull.Value) entidad.cod_caja = Convert.ToString(resultado["cod_caja"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToInt64(resultado["estado"]);
                            if (resultado["cod_datafono"] != DBNull.Value) entidad.cod_datafono = Convert.ToString(resultado["cod_datafono"]);
                            lstCaja.Add(entidad);
                        }

                        return lstCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CajaData", "ListarComboCajaXOficina", ex);
                        return null;
                    }

                }
            }
        }
    }
}
