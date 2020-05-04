using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Interfaces.Entities;

namespace Xpinn.Interfaces.Data
{
    public class WorkManagementData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public WorkManagementData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public WorkManagement_Aud CrearWorkManagement_Aud(WorkManagement_Aud pWorkManagement_Aud, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        if (pWorkManagement_Aud.consecutivo == null)
                            pconsecutivo.Value = DBNull.Value;
                        else
                            pconsecutivo.Value = pWorkManagement_Aud.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pexitoso = cmdTransaccionFactory.CreateParameter();
                        pexitoso.ParameterName = "p_exitoso";
                        if (pWorkManagement_Aud.exitoso == null)
                            pexitoso.Value = DBNull.Value;
                        else
                            pexitoso.Value = pWorkManagement_Aud.exitoso;
                        pexitoso.Direction = ParameterDirection.Input;
                        pexitoso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pexitoso);

                        DbParameter ptipooperacion = cmdTransaccionFactory.CreateParameter();
                        ptipooperacion.ParameterName = "p_tipooperacion";
                        if (pWorkManagement_Aud.tipooperacion == null)
                            ptipooperacion.Value = DBNull.Value;
                        else
                            ptipooperacion.Value = pWorkManagement_Aud.tipooperacion;
                        ptipooperacion.Direction = ParameterDirection.Input;
                        ptipooperacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipooperacion);

                        DbParameter p_jsonEntidadPeticion = cmdTransaccionFactory.CreateParameter();
                        p_jsonEntidadPeticion.ParameterName = "p_jsonEntidadPeticion";
                        if (pWorkManagement_Aud.jsonEntidadPeticion == null)
                            p_jsonEntidadPeticion.Value = DBNull.Value;
                        else
                            p_jsonEntidadPeticion.Value = pWorkManagement_Aud.jsonEntidadPeticion;
                        p_jsonEntidadPeticion.Direction = ParameterDirection.Input;
                        p_jsonEntidadPeticion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_jsonEntidadPeticion);

                        DbParameter p_jsonEntidadRespuesta = cmdTransaccionFactory.CreateParameter();
                        p_jsonEntidadRespuesta.ParameterName = "p_jsonEntidadRespuesta";
                        if (pWorkManagement_Aud.jsonEntidadRespuesta == null)
                            p_jsonEntidadRespuesta.Value = DBNull.Value;
                        else
                            p_jsonEntidadRespuesta.Value = pWorkManagement_Aud.jsonEntidadRespuesta;
                        p_jsonEntidadRespuesta.Direction = ParameterDirection.Input;
                        p_jsonEntidadRespuesta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_jsonEntidadRespuesta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_WORKMANAGE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pWorkManagement_Aud.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt32(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return pWorkManagement_Aud;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "CrearWorkManagement_Aud", ex);
                        return null;
                    }
                }
            }
        }

        public bool MarcarWorkFlowPagoProveedoresComoPagado(string radicado, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(radicado)) throw new ArgumentNullException("El barcode del radicado no puede ser nulo!.");

                        DbParameter p_barcodeRadicado = cmdTransaccionFactory.CreateParameter();
                        p_barcodeRadicado.ParameterName = "p_barcodeRadicado";
                        p_barcodeRadicado.Value = radicado;
                        p_barcodeRadicado.Direction = ParameterDirection.Input;
                        p_barcodeRadicado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_barcodeRadicado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_PAGOPROVEE_PAGAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "MarcarWorkFlowPagoProveedoresComoPagado", ex);
                        return false;
                    }
                }
            }
        }

        public List<WorkFlowPagoProveedores> ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(long codigoBeneficiario, Usuario usuario)
        {
            DbDataReader resultado;
            List<WorkFlowPagoProveedores> listaEntidades = new List<WorkFlowPagoProveedores>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select CONSECUTIVO, NUMEROFACTURA 
                                        From WORKFLOWPAGOPROVEEDORES
                                        WHERE ESTADO = 0 
                                        AND CODIGOBENEFICIARIO = " + codigoBeneficiario;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            WorkFlowPagoProveedores entidad = new WorkFlowPagoProveedores();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NUMEROFACTURA"] != DBNull.Value) entidad.numerofactura = Convert.ToString(resultado["NUMEROFACTURA"]);

                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores", ex);
                        return null;
                    }
                }
            }
        }

        public WorkFlowPagoProveedores CrearWorkFlowPagoProveedores(WorkFlowPagoProveedores workFlowPagoProveedores, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = workFlowPagoProveedores.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnumerocomprobante = cmdTransaccionFactory.CreateParameter();
                        pnumerocomprobante.ParameterName = "p_numerocomprobante";
                        pnumerocomprobante.Value = workFlowPagoProveedores.numerocomprobante;
                        pnumerocomprobante.Direction = ParameterDirection.Input;
                        pnumerocomprobante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumerocomprobante);

                        DbParameter ptipocomprobante = cmdTransaccionFactory.CreateParameter();
                        ptipocomprobante.ParameterName = "p_tipocomprobante";
                        ptipocomprobante.Value = workFlowPagoProveedores.tipocomprobante;
                        ptipocomprobante.Direction = ParameterDirection.Input;
                        ptipocomprobante.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipocomprobante);

                        DbParameter pcodigobeneficiario = cmdTransaccionFactory.CreateParameter();
                        pcodigobeneficiario.ParameterName = "p_codigobeneficiario";
                        pcodigobeneficiario.Value = workFlowPagoProveedores.codigobeneficiario;
                        pcodigobeneficiario.Direction = ParameterDirection.Input;
                        pcodigobeneficiario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigobeneficiario);

                        DbParameter pnumerofactura = cmdTransaccionFactory.CreateParameter();
                        pnumerofactura.ParameterName = "p_numerofactura";
                        pnumerofactura.Value = workFlowPagoProveedores.numerofactura;
                        pnumerofactura.Direction = ParameterDirection.Input;
                        pnumerofactura.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumerofactura);

                        DbParameter pbarcoderadicado = cmdTransaccionFactory.CreateParameter();
                        pbarcoderadicado.ParameterName = "p_barcoderadicado";
                        pbarcoderadicado.Value = workFlowPagoProveedores.barcoderadicado;
                        pbarcoderadicado.Direction = ParameterDirection.Input;
                        pbarcoderadicado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pbarcoderadicado);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = workFlowPagoProveedores.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_WORKFLOWPA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        workFlowPagoProveedores.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return workFlowPagoProveedores;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "CrearWorkFlowPagoProveedores", ex);
                        return null;
                    }
                }
            }
        }

        public List<WorkFlowCruceCuentas> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<WorkFlowCruceCuentas> listaEntidades = new List<WorkFlowCruceCuentas>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select wm.BARCODE
                                        from GIRO_REALIZADO gRea 
                                        JOIN WORKFLOWCRUCECUENTAS wm on wm.CODIGOIDGIRO = gRea.IDGIRO
                                        where gRea.cod_ope = " + codigoOperacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            WorkFlowCruceCuentas entidad = new WorkFlowCruceCuentas();
                            if (resultado["BARCODE"] != DBNull.Value) entidad.barcode = Convert.ToString(resultado["BARCODE"]);

                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion", ex);
                        return null;
                    }
                }
            }
        }

        public WorkFlowCruceCuentas CrearWorkFlowCruceCuentas(WorkFlowCruceCuentas workFlowCruceCuentas, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = workFlowCruceCuentas.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = workFlowCruceCuentas.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pcodigoidgiro = cmdTransaccionFactory.CreateParameter();
                        pcodigoidgiro.ParameterName = "p_codigoidgiro";
                        if (workFlowCruceCuentas.codigoidgiro.HasValue)
                        {
                            pcodigoidgiro.Value = workFlowCruceCuentas.codigoidgiro;
                        }
                        else
                        {
                            pcodigoidgiro.Value = DBNull.Value;
                        }

                        pcodigoidgiro.Direction = ParameterDirection.Input;
                        pcodigoidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoidgiro);

                        DbParameter pbarcode = cmdTransaccionFactory.CreateParameter();
                        pbarcode.ParameterName = "p_barcode";
                        pbarcode.Value = workFlowCruceCuentas.barcode;
                        pbarcode.Direction = ParameterDirection.Input;
                        pbarcode.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pbarcode);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_WORKFLOWCR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        workFlowCruceCuentas.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return workFlowCruceCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "CrearWorkFlowCruceCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public List<WorkFlowCreditos> ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(long codigoOperacion, Usuario usuario)
        {
            DbDataReader resultado;
            List<WorkFlowCreditos> listaEntidades = new List<WorkFlowCreditos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select wm.*
                                        from GIRO_REALIZADO gRea 
                                        join v_giro g on gRea.IDGIRO = g.IDGIRO
                                        join WORKFLOWCREDITOS wm on wm.NUMERORADICACION = g.NUMERO_RADICACION
                                        where gRea.cod_ope = " + codigoOperacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            WorkFlowCreditos entidad = new WorkFlowCreditos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["WORKFLOWID"] != DBNull.Value) entidad.workflowid = Convert.ToInt64(resultado["WORKFLOWID"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.numeroradicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["BARCODERADICACION"] != DBNull.Value) entidad.barCodeRadicacion = Convert.ToString(resultado["BARCODERADICACION"]);
                            if (resultado["DocumentosFueronGenerados"] != DBNull.Value) entidad.documentosFueronGenerados = Convert.ToBoolean(resultado["DocumentosFueronGenerados"]);

                            listaEntidades.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaEntidades;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion", ex);
                        return null;
                    }
                }
            }
        }

        public bool MarcarDocumentosGeneradosDeUnWorkFlow(WorkFlowCreditos workFlowCredito, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = workFlowCredito.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter p_documentosGenerados = cmdTransaccionFactory.CreateParameter();
                        p_documentosGenerados.ParameterName = "p_documentosGenerados";
                        p_documentosGenerados.Value = Convert.ToInt32(workFlowCredito.documentosFueronGenerados);
                        p_documentosGenerados.Direction = ParameterDirection.Input;
                        p_documentosGenerados.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_documentosGenerados);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_WorkFlowMarcarDocGen";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "MarcarDocumentosGeneradosDeUnWorkFlow", ex);
                        return false;
                    }
                }
            }
        }

        public WorkFlowCreditos ConsultarWorkFlowCreditoPorNumeroRadicacion(int numeroRadicacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            WorkFlowCreditos entidad = new WorkFlowCreditos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM WORKFLOWCREDITOS WHERE NUMERORADICACION = " + numeroRadicacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOPERSONA"] != DBNull.Value) entidad.codigopersona = Convert.ToInt64(resultado["CODIGOPERSONA"]);
                            if (resultado["WORKFLOWID"] != DBNull.Value) entidad.workflowid = Convert.ToInt64(resultado["WORKFLOWID"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.numeroradicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["BARCODERADICACION"] != DBNull.Value) entidad.barCodeRadicacion = Convert.ToString(resultado["BARCODERADICACION"]);
                            if (resultado["DocumentosFueronGenerados"] != DBNull.Value) entidad.documentosFueronGenerados = Convert.ToBoolean(resultado["DocumentosFueronGenerados"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "ConsultarWorkFlowCreditoPorNumeroRadicacion", ex);
                        return null;
                    }
                }
            }
        }

        public WorkFlowCreditos CrearWorkFlowCreditos(WorkFlowCreditos pWorkFlowCreditos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pWorkFlowCreditos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigopersona = cmdTransaccionFactory.CreateParameter();
                        pcodigopersona.ParameterName = "p_codigopersona";
                        pcodigopersona.Value = pWorkFlowCreditos.codigopersona;
                        pcodigopersona.Direction = ParameterDirection.Input;
                        pcodigopersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigopersona);

                        DbParameter pworkflowid = cmdTransaccionFactory.CreateParameter();
                        pworkflowid.ParameterName = "p_workflowid";
                        pworkflowid.Value = pWorkFlowCreditos.workflowid;
                        pworkflowid.Direction = ParameterDirection.Input;
                        pworkflowid.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pworkflowid);

                        DbParameter pnumeroradicacion = cmdTransaccionFactory.CreateParameter();
                        pnumeroradicacion.ParameterName = "p_numeroradicacion";
                        pnumeroradicacion.Value = pWorkFlowCreditos.numeroradicacion;
                        pnumeroradicacion.Direction = ParameterDirection.Input;
                        pnumeroradicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumeroradicacion);

                        DbParameter p_barCodeRadicacion = cmdTransaccionFactory.CreateParameter();
                        p_barCodeRadicacion.ParameterName = "p_barCodeRadicacion";
                        p_barCodeRadicacion.Value = pWorkFlowCreditos.barCodeRadicacion;
                        p_barCodeRadicacion.Direction = ParameterDirection.Input;
                        p_barCodeRadicacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_barCodeRadicacion);

                        connection.Open();

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_WORKFLOWCR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pWorkFlowCreditos.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);
                        return pWorkFlowCreditos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("WorkManagementData", "CrearWorkFlowCreditos", ex);
                        return null;
                    }
                }
            }
        }
    }
}