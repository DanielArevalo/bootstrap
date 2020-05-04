using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncOficinaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public SyncOficinaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        #region METODOS DE OFICINAS
        public Oficina CrearModSyncOficina(Oficina pSync_Oficina, int pOpcion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        pcod_oficina.Value = pSync_Oficina.cod_oficina;
                        pcod_oficina.Direction = pOpcion == 1 ? ParameterDirection.Output : ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        pcod_empresa.Value = pSync_Oficina.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pSync_Oficina.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pSync_Oficina.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pfecha_creacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_creacion.ParameterName = "p_fecha_creacion";
                        if (pSync_Oficina.fecha_creacion == null)
                            pfecha_creacion.Value = DBNull.Value;
                        else
                            pfecha_creacion.Value = pSync_Oficina.fecha_creacion;
                        pfecha_creacion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_creacion);

                        DbParameter pcodciudad = cmdTransaccionFactory.CreateParameter();
                        pcodciudad.ParameterName = "p_codciudad";
                        if (pSync_Oficina.codciudad == null)
                            pcodciudad.Value = DBNull.Value;
                        else
                            pcodciudad.Value = pSync_Oficina.codciudad;
                        pcodciudad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcodciudad);

                        DbParameter pdireccion = cmdTransaccionFactory.CreateParameter();
                        pdireccion.ParameterName = "p_direccion";
                        if (pSync_Oficina.direccion == null)
                            pdireccion.Value = DBNull.Value;
                        else
                            pdireccion.Value = pSync_Oficina.direccion;
                        pdireccion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pdireccion);

                        DbParameter ptelefono = cmdTransaccionFactory.CreateParameter();
                        ptelefono.ParameterName = "p_telefono";
                        if (pSync_Oficina.telefono == null)
                            ptelefono.Value = DBNull.Value;
                        else
                            ptelefono.Value = pSync_Oficina.telefono;
                        ptelefono.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptelefono);

                        DbParameter presponsable = cmdTransaccionFactory.CreateParameter();
                        presponsable.ParameterName = "p_responsable";
                        if (pSync_Oficina.responsable == null)
                            presponsable.Value = DBNull.Value;
                        else
                            presponsable.Value = pSync_Oficina.responsable;
                        presponsable.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(presponsable);

                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        if (pSync_Oficina.centro_costo == null)
                            pcentro_costo.Value = DBNull.Value;
                        else
                            pcentro_costo.Value = pSync_Oficina.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pSync_Oficina.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pSync_Oficina.estado;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pSync_Oficina.cod_cuenta == null)
                            pcod_cuenta.Value = DBNull.Value;
                        else
                            pcod_cuenta.Value = pSync_Oficina.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter psede_propia = cmdTransaccionFactory.CreateParameter();
                        psede_propia.ParameterName = "p_sede_propia";
                        if (pSync_Oficina.sede_propia == null)
                            psede_propia.Value = DBNull.Value;
                        else
                            psede_propia.Value = pSync_Oficina.sede_propia;
                        psede_propia.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(psede_propia);

                        DbParameter pindicador_corresponsal = cmdTransaccionFactory.CreateParameter();
                        pindicador_corresponsal.ParameterName = "p_indicador_corresponsal";
                        if (pSync_Oficina.indicador_corresponsal == null)
                            pindicador_corresponsal.Value = DBNull.Value;
                        else
                            pindicador_corresponsal.Value = pSync_Oficina.indicador_corresponsal;
                        pindicador_corresponsal.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pindicador_corresponsal);

                        DbParameter ptipo_negocio = cmdTransaccionFactory.CreateParameter();
                        ptipo_negocio.ParameterName = "p_tipo_negocio";
                        if (pSync_Oficina.tipo_negocio == null)
                            ptipo_negocio.Value = DBNull.Value;
                        else
                            ptipo_negocio.Value = pSync_Oficina.tipo_negocio;
                        ptipo_negocio.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_negocio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_CAJ_OFICINASYN_CREAR" : "XPF_CAJAFIN_OFICINMODIFICAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pOpcion == 1)
                            pSync_Oficina.cod_oficina = Convert.ToInt64(pcod_oficina.Value);
                        return pSync_Oficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "CrearSyncOficina", ex);
                        return null;
                    }
                }
            }
        }


        public EntityGlobal EliminarSyncOficina(Oficina pOficina, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pOficina.cod_oficina;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_OFICINAELIMINAR";
                        pResult.NroRegisterAffected = cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pResult.Success = pResult.NroRegisterAffected > 0 ? true : false;
                    }
                    catch (Exception ex)
                    {
                        pResult.Message = ex.Message;
                        pResult.Success = false;
                    }
                    return pResult;
                }
            }
        }

        public List<Oficina> ListarSyncOficina(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Oficina> lstOficina = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Oficina " + pFiltro + " ORDER BY COD_OFICINA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstOficina = new List<Oficina>();
                            Oficina entidad;
                            while (resultado.Read())
                            {
                                entidad = new Oficina();
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["FECHA_CREACION"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["FECHA_CREACION"]);
                                if (resultado["COD_CIUDAD"] != DBNull.Value) entidad.codciudad = Convert.ToInt64(resultado["COD_CIUDAD"]);
                                if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                                if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                                if (resultado["RESPONSABLE"] != DBNull.Value) entidad.responsable = Convert.ToString(resultado["RESPONSABLE"]);
                                if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                                if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                                if (resultado["ESTADO_CAJA"] != DBNull.Value) entidad.estado_caja = Convert.ToInt32(resultado["ESTADO_CAJA"]);
                                lstOficina.Add(entidad);
                            }
                            resultado.Close();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOficina;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "ListarSyncOficina", ex);
                        return null;
                    }
                }
            }
        }
        

        #endregion

        #region HORARIO DE OFICINAS
        public List<SyncHorarioOficina> ListarSyncHorarioOficina(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncHorarioOficina> lstHorarioOfi = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT H.* FROM HORARIOOFICINA H INNER JOIN OFICINA O ON H.COD_OFICINA = O.COD_OFICINA " + pFiltro + " ORDER BY H.COD_OFICINA,COD_HORARIO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstHorarioOfi = new List<SyncHorarioOficina>();
                            SyncHorarioOficina entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncHorarioOficina();
                                if (resultado["COD_HORARIO"] != DBNull.Value) entidad.cod_horario = Convert.ToInt32(resultado["COD_HORARIO"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                                if (resultado["TIPO_HORARIO"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt32(resultado["TIPO_HORARIO"]);
                                if (resultado["DIA"] != DBNull.Value) entidad.dia = Convert.ToInt32(resultado["DIA"]);
                                if (resultado["HORA_INICIAL"] != DBNull.Value) entidad.hora_inicial = Convert.ToDateTime(resultado["HORA_INICIAL"]);
                                if (resultado["HORA_FINAL"] != DBNull.Value) entidad.hora_final = Convert.ToDateTime(resultado["HORA_FINAL"]);
                                lstHorarioOfi.Add(entidad);
                            }
                            resultado.Close();
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstHorarioOfi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "ListarSyncHorarioOficina", ex);
                        return null;
                    }
                }
            }
        }

        #endregion

        #region PROCESOS DE OFICINA

        public string GenerarValidacionProcesoOficina(SyncProcesoOficina pEntidad, Usuario pUsuario)
        {
            string pMensaje = string.Empty;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "pcodigoencargado";
                        pcod_encargado.Value = pEntidad.cod_usuario;
                        pcod_encargado.DbType = DbType.Int64;
                        pcod_encargado.Direction = ParameterDirection.Input;

                        DbParameter pfecha_proceso = cmdTransaccionFactory.CreateParameter();
                        pfecha_proceso.ParameterName = "pfechaproceso";
                        pfecha_proceso.Value = pEntidad.fecha_proceso;
                        pfecha_proceso.DbType = DbType.DateTime;
                        pfecha_proceso.Direction = ParameterDirection.Input;

                        DbParameter ptipo_proceso = cmdTransaccionFactory.CreateParameter();
                        ptipo_proceso.ParameterName = "ptipoproceso";
                        ptipo_proceso.Value = pEntidad.tipo_proceso;
                        ptipo_proceso.DbType = DbType.Int32;
                        ptipo_proceso.Direction = ParameterDirection.Input;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = pEntidad.tipo_horario;
                        ptipo_horario.DbType = DbType.Int32;
                        ptipo_horario.Direction = ParameterDirection.Input;

                        DbParameter pmensaje = cmdTransaccionFactory.CreateParameter();
                        pmensaje.ParameterName = "pmensaje";
                        pmensaje.Value = " ";
                        pmensaje.DbType = DbType.String;
                        pmensaje.Direction = ParameterDirection.Output;
                        pmensaje.Size = 1000;

                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);
                        cmdTransaccionFactory.Parameters.Add(pfecha_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);
                        cmdTransaccionFactory.Parameters.Add(pmensaje);
                         
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_VALIDAPROCOFICI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pMensaje = pmensaje.Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "GenerarValidacionProcesoOficina", ex);
                        return null;
                    }
                }
            }
            return pMensaje;
        }


        public SyncProcesoOficina CrearProcesoOficina(SyncProcesoOficina pEntidad, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcodigooficina";
                        pcod_oficina.Value = pEntidad.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcod_encargado = cmdTransaccionFactory.CreateParameter();
                        pcod_encargado.ParameterName = "pcodigoencargado";
                        pcod_encargado.Value = pEntidad.cod_usuario;
                        pcod_encargado.DbType = DbType.Int64;
                        pcod_encargado.Direction = ParameterDirection.Input;

                        DbParameter pfecha_proceso = cmdTransaccionFactory.CreateParameter();
                        pfecha_proceso.ParameterName = "pfechaproceso";
                        pfecha_proceso.Value = pEntidad.fecha_proceso;
                        pfecha_proceso.DbType = DbType.DateTime;
                        pfecha_proceso.Direction = ParameterDirection.Input;

                        DbParameter ptipo_proceso = cmdTransaccionFactory.CreateParameter();
                        ptipo_proceso.ParameterName = "ptipoproceso";
                        ptipo_proceso.Value = pEntidad.tipo_proceso;
                        ptipo_proceso.DbType = DbType.Int32;
                        ptipo_proceso.Direction = ParameterDirection.Input;

                        DbParameter ptipo_horario = cmdTransaccionFactory.CreateParameter();
                        ptipo_horario.ParameterName = "ptipohorario";
                        ptipo_horario.Value = pEntidad.tipo_horario;
                        ptipo_horario.DbType = DbType.Int32;
                        ptipo_horario.Direction = ParameterDirection.Input;
                        
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcod_encargado);
                        cmdTransaccionFactory.Parameters.Add(pfecha_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_proceso);
                        cmdTransaccionFactory.Parameters.Add(ptipo_horario);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_PROCOFIINSERTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        
                        int estadoOp = 0;
                        if (pEntidad.tipo_proceso == 1) //Apertura
                            estadoOp = 1;
                        else //Cierre de Oficina
                            estadoOp = 0;

                        cmdTransaccionFactory.Parameters.Clear();

                        DbParameter pcode_oficina = cmdTransaccionFactory.CreateParameter();
                        pcode_oficina.ParameterName = "pcodigooficina";
                        pcode_oficina.Value = pEntidad.cod_oficina;
                        pcode_oficina.DbType = DbType.Int16;
                        pcode_oficina.Size = 8;
                        pcode_oficina.Direction = ParameterDirection.Input;

                        DbParameter pcode_estado = cmdTransaccionFactory.CreateParameter();
                        pcode_estado.ParameterName = "pestado";
                        pcode_estado.Value = estadoOp;
                        pcode_estado.DbType = DbType.Int16;
                        pcode_estado.Size = 8;
                        pcode_estado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcode_oficina);
                        cmdTransaccionFactory.Parameters.Add(pcode_estado);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_PROCOFI_U";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "CrearProcesoOficina", ex);
                        return null;
                    }
                }
            }
        }
        

        public List<SyncProcesoOficina> ListarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SyncProcesoOficina> lstProceso = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROCESOOFICINA " + pFiltro + " ORDER BY CONSECUTIVO DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstProceso = new List<SyncProcesoOficina>();
                            SyncProcesoOficina entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncProcesoOficina();
                                if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                                if (resultado["TIPO_HORARIO"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt32(resultado["TIPO_HORARIO"]);
                                if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt32(resultado["TIPO_PROCESO"]);
                                if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                                lstProceso.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "ListarSyncProcesoOficina", ex);
                        return null;
                    }
                }
            }
        }

        public SyncProcesoOficina ConsultarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            SyncProcesoOficina entidad = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PROCESOOFICINA " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            while (resultado.Read())
                            {
                                entidad = new SyncProcesoOficina();
                                if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                                if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                                if (resultado["FECHA_PROCESO"] != DBNull.Value) entidad.fecha_proceso = Convert.ToDateTime(resultado["FECHA_PROCESO"]);
                                if (resultado["TIPO_HORARIO"] != DBNull.Value) entidad.tipo_horario = Convert.ToInt32(resultado["TIPO_HORARIO"]);
                                if (resultado["TIPO_PROCESO"] != DBNull.Value) entidad.tipo_proceso = Convert.ToInt32(resultado["TIPO_PROCESO"]);
                                if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt64(resultado["COD_USUARIO"]);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "ConsultarSyncProcesoOficina", ex);
                        return null;
                    }
                }
            }
        }

        public DateTime? ConsultarFecUltCierre(Int64 pCodOficina, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime? pFecUltCierre = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(fecha_proceso) as fecha_proceso from procesooficina WHERE tipo_proceso = 2 AND cod_oficina = " + pCodOficina;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (!resultado.IsDBNull(0)) pFecUltCierre = resultado.GetDateTime(0);
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return pFecUltCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncOficinaData", "ConsultarFecUltCierre", ex);
                        return null;
                    }
                }
            }
        }


        #endregion
    }
}
